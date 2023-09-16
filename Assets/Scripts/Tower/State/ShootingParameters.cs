using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingParameters
{
    [SerializeField] private float aboba;
    public IReadOnlyCollection<Monster> ActiveEnemies { get; set; }
    
}
