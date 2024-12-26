using UnityEngine;

public class Riple : Gun
{
    private float _prevBulletCooltime;

    private void OnEnable()
    {
        _prevBulletCooltime = _playerStat.BulletCooltime;
        _playerStat.BulletCooltime *= 0.5F;
    }

    private void OnDisable()
    {
        _playerStat.BulletCooltime = _prevBulletCooltime;
    }
}
