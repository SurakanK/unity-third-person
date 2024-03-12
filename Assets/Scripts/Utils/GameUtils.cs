
using UnityEngine;

public static class GameUtile
{
    static public DirectionType DirectionControl(Vector2 direction)
    {
        if (direction.magnitude == 0)
        {
            return DirectionType.None;
        }

        var angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        angle = (angle + 360) % 360;

        if (angle > 22.5 && angle <= 67.5) //FR
        {
            return DirectionType.FrontRight;
        }
        else if (angle > 67.5 && angle <= 112.5) //R
        {
            return DirectionType.Right;
        }
        else if (angle > 112.5 && angle <= 157.5) //RB
        {
            return DirectionType.BackRight;
        }
        else if (angle > 157.5 && angle <= 202.5) //B
        {
            return DirectionType.Back;
        }
        else if (angle > 202.5 && angle <= 247.5) //BL
        {
            return DirectionType.BackLeft;
        }
        else if (angle > 247.5 && angle <= 292.5) //L
        {
            return DirectionType.Left;
        }
        else if (angle > 292.5 && angle <= 337.5) //FL
        {
            return DirectionType.FrontLeft;
        }
        else //F
        {
            return DirectionType.Front;
        }
    }
}