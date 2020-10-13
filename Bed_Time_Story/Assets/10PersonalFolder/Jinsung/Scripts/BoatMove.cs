using UnityEngine;

public class BoatMove : MonoBehaviour
{
    public float rotateSpeed;
    public float accelSpeed;
    public float maxSpeed;

    public GameObject Boat;

    private bool forward = true;
    private float currentSpeed = 0;
    private float currentAccelSpeed;
    private float mouseX = 0;
    private float mouseY = 0;

    void Start()
    {
        currentAccelSpeed = accelSpeed;    
    }

    void Update()
    {
        // 마우스 움직임 감지
        mouseX += Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime;
        mouseY += Input.GetAxis("Mouse Y") * rotateSpeed * Time.deltaTime;

        mouseX = Mathf.Clamp(mouseX, -90, 90);
        mouseY = Mathf.Clamp(mouseY, -90, 90);
        transform.eulerAngles = new Vector3(-mouseY, mouseX, 0);
        Boat.transform.eulerAngles = new Vector3(0, mouseX, 0);

        // 마우스 입력 감지
        if (forward != Input.GetMouseButton(0)) currentAccelSpeed = accelSpeed;
        forward = Input.GetMouseButton(0);
        currentAccelSpeed *= 1.005f;
        currentAccelSpeed = Mathf.Clamp(currentAccelSpeed, 0, maxSpeed);

        currentSpeed += Input.GetMouseButton(0) ? currentAccelSpeed : -currentAccelSpeed;
        currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);
        Boat.transform.Translate(new Vector3(0, 0, currentSpeed));
    }
}
