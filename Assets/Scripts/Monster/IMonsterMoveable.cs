using UnityEngine;

public interface IMonsterMoveable : IMoveable
{
    public Vector3 MoveTarget { get; set; }

}
