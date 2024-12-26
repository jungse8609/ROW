using UnityEngine;

public class BossMonster : Monster
{
    [SerializeField] private GameObject[] _dropGunPrefabs;
    [SerializeField] private float _teleportCooldown = 5.0f;
    [SerializeField] private float _teleportDistance = 10.0f;
    
    private Transform _playerTransform;
    private float _teleportTimer = 0.0f;

    protected override void Update()
    {
        base.Update();

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

    public override void TakeDamage(float damage)
    {
        _currentHp -= damage;
        if (_currentHp <= 0)
        {
            Die();
        }
    }

    protected override void Die()
    {
        _monsterPool.ReturnObject(this.gameObject);

        // 드롭할 무기 프리팹이 할당되어 있는지 확인
        if (_dropGunPrefabs != null && _dropGunPrefabs.Length > 0)
        {
            // 무기 드롭 프리팹 중 랜덤하게 하나 선택
            int randomIndex = Random.Range(0, _dropGunPrefabs.Length);
            GameObject selectedGunPrefab = _dropGunPrefabs[randomIndex];

            // 무기 드롭 인스턴스 생성
            Vector3 dropPosition = transform.position;
            dropPosition.y = playerTransform.position.y;
            GameObject gunDrop = Instantiate(selectedGunPrefab, dropPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("BossMonster.Die(): No drop gun prefabs assigned.");
        }
    }
}
