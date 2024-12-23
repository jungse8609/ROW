using UnityEngine;

public class BossMonster : Monster
{
    [SerializeField] private float _teleportCooldown = 5.0f;
    [SerializeField] private float _teleportDistance = 10.0f;

    private Transform _playerTransform;
    private float _teleportTimer = 0.0f;

    private void Update()
    {
        _teleportTimer -= Time.deltaTime;
        TeleportIfFar();
    }

    private void TeleportIfFar()
    {
        if (_teleportTimer > 0 || _playerTransform == null) return;

        float distance = Vector3.Distance(transform.position, _playerTransform.position);
        if (distance > _teleportDistance)
        {
            transform.position = _playerTransform.position + (Vector3.right * 2.0f); // 플레이어 근처로 이동
            _teleportTimer = _teleportCooldown;
        }
    }
}
