﻿// Designed & Developed by Alexander Ameye
// https://alexander-ameye.gitbook.io/stylized-water/
// Version 1.1.0

#if UNIVERSAL_RENDERER
using UnityEngine;
using UnityEditor;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace StylizedWater
{
    public class CausticsFeature : ScriptableRendererFeature
    {
#if UNITY_EDITOR
        [CustomPropertyDrawer(typeof(MinMaxSliderAttribute))]
        public class MinMaxSliderDrawer : PropertyDrawer
        {

            public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
            {
                var minMaxAttribute = (MinMaxSliderAttribute)attribute;
                var propertyType = property.propertyType;

                label.tooltip = minMaxAttribute.min.ToString("F2") + " to " + minMaxAttribute.max.ToString("F2");

                Rect controlRect = EditorGUI.PrefixLabel(position, label);

                Rect[] splittedRect = SplitRect(controlRect, 3);

                if (propertyType == SerializedPropertyType.Vector2)
                {
                    EditorGUI.BeginChangeCheck();

                    Vector2 vector = property.vector2Value;
                    float minVal = vector.x;
                    float maxVal = vector.y;

                    minVal = EditorGUI.FloatField(splittedRect[0], float.Parse(minVal.ToString("F2")));
                    maxVal = EditorGUI.FloatField(splittedRect[2], float.Parse(maxVal.ToString("F2")));

                    EditorGUI.MinMaxSlider(splittedRect[1], ref minVal, ref maxVal,
                    minMaxAttribute.min, minMaxAttribute.max);

                    if (minVal < minMaxAttribute.min) minVal = minMaxAttribute.min;
                    if (maxVal > minMaxAttribute.max) maxVal = minMaxAttribute.max;

                    vector = new Vector2(minVal > maxVal ? maxVal : minVal, maxVal);

                    if (EditorGUI.EndChangeCheck()) property.vector2Value = vector;
                }
            }

            Rect[] SplitRect(Rect rectToSplit, int n)
            {
                Rect[] rects = new Rect[n];

                for (int i = 0; i < n; i++) rects[i] = new Rect(rectToSplit.position.x + (i * rectToSplit.width / n), rectToSplit.position.y, rectToSplit.width / n, rectToSplit.height);

                int padding = (int)rects[0].width - 40;
                int space = 5;

                rects[0].width -= padding + space;
                rects[2].width -= padding + space;

                rects[1].x -= padding;
                rects[1].width += padding * 2;

                rects[2].x += padding + space;

                return rects;
            }
        }

        public class MinMaxSliderAttribute : PropertyAttribute
        {
            public float min;
            public float max;

            public MinMaxSliderAttribute(float min, float max)
            {
                this.min = min;
                this.max = max;
            }
        }
#endif

        [System.Serializable]
        public class CausticsSettings
        {
            [Header("Visuals")]
            [Range(0f, 3f)] public float strength = 3f;
            [Range(0f, 1f)] public float rgbSplit = 0.3f;
            [Range(0f, 1f)] public float shadowMask = 1f;

            [Header("Movement")]
            public Texture2D texture;
            [Range(0.1f, 10f)] public float scale = 5f;
            [Range(0f, 0.3f)] public float speed = 0.125f;

            [Header("Depth")]
            public float waterLevel = 2f;
#if UNITY_EDITOR
            [MinMaxSlider(0, 10)] public Vector2 depth = new Vector2(0f, 4f);
#else
            public Vector2 depth = new Vector2(0f, 4f);
#endif
            [Range(0f, 1f)] public float fade = 1f;

            [Header("Rendering")]
            public RenderPassEvent renderPassEvent = RenderPassEvent.AfterRenderingSkybox;

            public DebugMode debug = DebugMode.Disabled;
            public enum DebugMode
            {
                Disabled,
                Caustics,
                Mask
            }
        }

        public CausticsSettings settings = new CausticsSettings();
        CausticsPass causticsPass;

        private Material _causticMaterial;

        private static readonly int SrcBlend = Shader.PropertyToID("_SrcBlend");
        private static readonly int DstBlend = Shader.PropertyToID("_DstBlend");

        private static readonly int causticsTextureID = Shader.PropertyToID("_CausticsTexture");
        private static readonly int causticsStrengthID = Shader.PropertyToID("_CausticsStrength");
        private static readonly int causticsScaleID = Shader.PropertyToID("_CausticsScale");
        private static readonly int causticsSpeedID = Shader.PropertyToID("_CausticsSpeed");
        private static readonly int causticsSplitID = Shader.PropertyToID("_CausticsSplit");
        private static readonly int shadowMaskID = Shader.PropertyToID("_CausticsShadowMask");
        private static readonly int causticsFadeID = Shader.PropertyToID("_CausticsFade");

        private static readonly int waterLevelID = Shader.PropertyToID("_WaterLevel");
        private static readonly int causticsStartID = Shader.PropertyToID("_CausticsStart");
        private static readonly int causticsEndID = Shader.PropertyToID("_CausticsEnd");

        public override void Create()
        {
            causticsPass = new CausticsPass(settings.waterLevel);

            if (_causticMaterial) DestroyImmediate(_causticMaterial);
            Shader shader = Shader.Find("Hidden/Stylized Water/Caustics");
            if (shader != null) _causticMaterial = CoreUtils.CreateEngineMaterial(shader);

            if (!_causticMaterial) return;

            _causticMaterial.SetTexture(causticsTextureID, settings.texture);
            _causticMaterial.SetFloat(causticsStrengthID, settings.strength);
            _causticMaterial.SetFloat(causticsScaleID, settings.scale);
            _causticMaterial.SetFloat(causticsSpeedID, settings.speed);
            _causticMaterial.SetFloat(causticsSplitID, settings.rgbSplit);
            _causticMaterial.SetFloat(shadowMaskID, settings.shadowMask);
            _causticMaterial.SetFloat(causticsFadeID, settings.fade);

            _causticMaterial.SetFloat(waterLevelID, settings.waterLevel);
            _causticMaterial.SetFloat(causticsStartID, settings.depth.x);
            _causticMaterial.SetFloat(causticsEndID, settings.depth.y);

            switch (settings.debug)
            {
                case CausticsSettings.DebugMode.Disabled:
                    _causticMaterial.SetFloat(SrcBlend, 2f);
                    _causticMaterial.SetFloat(DstBlend, 0f);
                    _causticMaterial.DisableKeyword("DEBUG_MASK");
                    _causticMaterial.DisableKeyword("DEBUG_CAUSTICS");
                    causticsPass.renderPassEvent = settings.renderPassEvent;
                    break;

                case CausticsSettings.DebugMode.Caustics:
                    _causticMaterial.SetFloat(SrcBlend, 1f);
                    _causticMaterial.SetFloat(DstBlend, 0f);
                    _causticMaterial.DisableKeyword("DEBUG_MASK");
                    _causticMaterial.EnableKeyword("DEBUG_CAUSTICS");
                    causticsPass.renderPassEvent = RenderPassEvent.AfterRenderingPostProcessing;
                    break;

                case CausticsSettings.DebugMode.Mask:
                    _causticMaterial.SetFloat(SrcBlend, 1f);
                    _causticMaterial.SetFloat(DstBlend, 0f);
                    _causticMaterial.DisableKeyword("DEBUG_CAUSTICS");
                    _causticMaterial.EnableKeyword("DEBUG_MASK");
                    causticsPass.renderPassEvent = RenderPassEvent.AfterRenderingPostProcessing;
                    break;
            }

            causticsPass.causticsMaterial = _causticMaterial;
        }

        public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
        {
            renderer.EnqueuePass(causticsPass);
        }
    }
}
#endif