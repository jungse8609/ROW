using UnityEngine;

public class GunReplacer : MonoBehaviour
{
    [SerializeField] private Gun currentGun;

    private FireAction _fireAction;
    private ReloadAction _reloadAction;
    private MovementAction _movementAction;

    private void Awake()
    {
        _fireAction = GetComponent<FireAction>();
        _reloadAction = GetComponent<ReloadAction>();
        _movementAction = GetComponent<MovementAction>();
    }

    public void ReplaceObject(GameObject newPrefab)
    {
        if (newPrefab == null)
        {
            Debug.LogError("�� �������� �Ҵ���� �ʾҽ��ϴ�.");
            return;
        }

        // ���� ������Ʈ�� ��ġ, ȸ��, ������ ����
        Vector3 currentPosition = currentGun.transform.position;
        Quaternion currentRotation = currentGun.transform.rotation;
        Vector3 currentScale = currentGun.transform.localScale;

        // �� ������Ʈ ����
        GameObject newObject = Instantiate(newPrefab, currentPosition, currentRotation, currentGun.transform.parent);
        newObject.transform.parent = currentGun.transform.parent;

        // �� ������Ʈ�� ������ ����
        newObject.transform.localScale = currentScale;

        // Gun �Ӽ� ����
        Gun newGun = newObject.GetComponent<Gun>();
        newGun.InitPoolParent(currentGun.PoolParent);

        // ���� ������Ʈ ����
        Destroy(currentGun.gameObject);

        currentGun = newGun;
        _fireAction.ReplaceGun(newGun);
        _reloadAction.ReplaceGun(newGun);
        _movementAction.ReplaceGun(newGun);
    }
}
