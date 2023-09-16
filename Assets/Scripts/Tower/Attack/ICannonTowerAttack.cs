public interface ICannonTowerAttack : ITowerAttack
{
    public float LaunchAngle { get; }

    public Monster Target { get; }

    public bool AlternativeAttack { get; set; }
}
