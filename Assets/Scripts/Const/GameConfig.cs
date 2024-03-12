
using System.Collections.Generic;
using System.Numerics;

public class GameConfig
{
    public static readonly Dictionary<DirectionType, Vector2> AnimatorChrecter = new Dictionary<DirectionType, Vector2>
    {
        {DirectionType.None, new Vector2(0,0)},
        {DirectionType.Front, new Vector2(0,1)},
        {DirectionType.FrontLeft, new Vector2(-1,1)},
        {DirectionType.Left, new Vector2(-1,0)},
        {DirectionType.BackLeft, new Vector2(-1,-1)},
        {DirectionType.Back, new Vector2(0,-1)},
        {DirectionType.BackRight, new Vector2(1,-1)},
        {DirectionType.Right, new Vector2(1,0)},
        {DirectionType.FrontRight, new Vector2(1,1)},
    };

    public static readonly Dictionary<(DirectionType, MoveType), float> MoveSpeed = new Dictionary<(DirectionType, MoveType), float>
    {
        {(DirectionType.None, MoveType.Stand), 0f},
        {(DirectionType.Front, MoveType.Stand), 4f},
        {(DirectionType.FrontLeft, MoveType.Stand), 3f},
        {(DirectionType.Left, MoveType.Stand), 3f},
        {(DirectionType.BackLeft, MoveType.Stand), 3f},
        {(DirectionType.Back, MoveType.Stand), 3f},
        {(DirectionType.BackRight, MoveType.Stand), 3f},
        {(DirectionType.Right, MoveType.Stand), 3f},
        {(DirectionType.FrontRight, MoveType.Stand), 3f},
    };
}