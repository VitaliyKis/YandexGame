using UnityEngine;

public class DiamondMobCreator : MobCreator
{
    public DiamondMob diamondMobPrefab;
    public override Mob CreateMob(Vector2 mobPlace)
    {
        return InitMob(mobPlace);
    }

    protected override Mob InitMob(Vector2 mobPlace)
    {
        DiamondMob mob = Instantiate(diamondMobPrefab, mobPlace, Quaternion.identity);
        mob.ÑreatorType = this;
        mob.IsBlocked = true;
        mob.particleColor = Color.blue;

        return mob;
    }
}
