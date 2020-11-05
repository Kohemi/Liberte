using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleManager : MonoBehaviour
{
    public TurtleMove[] turtles;

    public void MoveTurtle()
    {
        foreach (TurtleMove turtle in turtles)
            turtle.move = true;
    }
}
