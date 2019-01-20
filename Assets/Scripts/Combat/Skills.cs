using System;

public class Skills : Target
{
    protected Skill _firstSkill;
    protected Skill _secondSkill;

    protected override void Start()
    {
        base.Start();
        _firstSkill = new DefaultAttack();
        _secondSkill = new SpecialAttack();
    }

    protected float GetSkillDamage(int input)
    {
        return input == 0 ? _firstSkill.GetDamage() : _secondSkill.GetDamage();
    }

    protected class SpecialAttack : Skill
    {
        public override float GetDamage()
        {
            return 15.0f;
        }
    }

    protected class DefaultAttack : Skill
    {
        public override float GetDamage()
        {
            return 0.0f;
        }
    }

    protected class Skill
    {
        public virtual float GetDamage()
        {
            throw new NotImplementedException();
        }
    }
}
