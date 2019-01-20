using UnityEngine;

public class Stats : Killable
{
    [SerializeField]
    protected float AttackRange = 2.0f;
    [SerializeField]
    private float AttackSpeedMultiplier = 1.5f;
    private float _defaultDamage = 5.0f;

    public float GetStatsDamage()
    {
        return _defaultDamage;
    }
}