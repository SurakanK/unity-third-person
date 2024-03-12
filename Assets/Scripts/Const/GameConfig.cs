
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

    public static readonly Dictionary<(DirectionType, CharacterState), float> MoveSpeed = new Dictionary<(DirectionType, CharacterState), float>
    {
        {(DirectionType.None, CharacterState.Stand), 0f},
        {(DirectionType.Front, CharacterState.Stand), 4f},
        {(DirectionType.FrontLeft, CharacterState.Stand), 3f},
        {(DirectionType.Left, CharacterState.Stand), 3f},
        {(DirectionType.BackLeft, CharacterState.Stand), 3f},
        {(DirectionType.Back, CharacterState.Stand), 3f},
        {(DirectionType.BackRight, CharacterState.Stand), 3f},
        {(DirectionType.Right, CharacterState.Stand), 3f},
        {(DirectionType.FrontRight, CharacterState.Stand), 3f},
    };
}