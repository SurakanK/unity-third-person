
using UnityEngine;

public static class GameUtile
{
    //0
    //-90,90
    // -180,180
    static public Vector2Int AnimatorDirection(Vector2 direction)
    {
        if (direction.magnitude == 0)
        {
            return Vector2Int.zero;
        }

        var angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        angle = (angle + 360) % 360;
        int x = 0;
        int y = 0;

        if (angle > 22.5 && angle <= 67.5) //FR
        {
            y = 1;
            x = 1;
        }
        else if (angle > 67.5 && angle <= 112.5) //R
        {
            y = 0;
            x = 1;
        }
        else if (angle > 112.5 && angle <= 157.5) //RB
        {
            y = -1;
            x = 1;

        }
        else if (angle > 157.5 && angle <= 202.5) //B
        {
            y = -1;
            x = 0;
        }
        else if (angle > 202.5 && angle <= 247.5) //BL
        {
            y = -1;
            x = -1;
        }
        else if (angle > 247.5 && angle <= 292.5) //L
        {
            y = 0;
            x = -1;
        }
        else if (angle > 292.5 && angle <= 337.5) //FL
        {
            y = 1;
            x = -1;
        }
        else //F
        {
            y = 1;
            x = 0;
        }
        return new Vector2Int(x, y);
    }
}