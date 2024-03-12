using System;
using System.Collections.Generic;

[Serializable]
public class MoveSpeed
{
    public CharacterState moveType;
    public List<DirectionSpeed> directionSpeed;
}

[Serializable]
public class DirectionSpeed
{
    public DirectionType directionType;
    public float speed;
}