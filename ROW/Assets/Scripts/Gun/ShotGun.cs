using UnityEngine;

public class ShotGun : Gun
{
    public override void Fire()
    {
        if (isReloading) return;

        if (_currentBulletCount <= 0)
        {
            Reload();
            return;
        }

        // Create Bullet Prefab
        GameObject[] bullets = new GameObject[3];
        bullets[0] = _bulletPool.GetObject();
        bullets[1] = _bulletPool.GetObject();
        bullets[2] = _bulletPool.GetObject();

        int i = -1;
        foreach (GameObject bullet in bullets)
        {
            bullet.transform.position = _firePoint.position;

            Vector3 fireRotation = _firePoint.eulerAngles;
            fireRotation.x = fireRotation.z = 0f;
            fireRotation.y = _firePoint.eulerAngles.y + 30 * i;
            i += 1;
            bullet.transform.rotation = Quaternion.Euler(fireRotation);

            TrailRenderer trail = bullet.GetComponent<TrailRenderer>();
            if (trail != null)
            {
                trail.Clear();
            }

            if (bullet.TryGetComponent<Bullet>(out Bullet bulletComponent))
            {
                bulletComponent.InitBullet(_playerStat.BulletSpeed, _playerStat.BulletSpeed);
            }
        }

        _currentBulletCount -= 1;

        // Fire Sound
        _audioPlayer.PlayAudioClip(_fireAudio);

        if (_currentBulletCount <= 0)
        {
            Reload();
        }
    }
}
