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
            Debug.LogError("새 프리팹이 할당되지 않았습니다.");
            return;
        }

        // 기존 오브젝트의 위치, 회전, 스케일 저장
        Vector3 currentPosition = currentGun.transform.position;
        Quaternion currentRotation = currentGun.transform.rotation;
        Vector3 currentScale = currentGun.transform.localScale;

        // 새 오브젝트 생성
        GameObject newObject = Instantiate(newPrefab, currentPosition, currentRotation, currentGun.transform.parent);
        newObject.transform.parent = currentGun.transform.parent;

        // 새 오브젝트의 스케일 설정
        newObject.transform.localScale = currentScale;

        // Gun 속성 설정
        Gun newGun = newObject.GetComponent<Gun>();
        newGun.InitPoolParent(currentGun.PoolParent);

        // 기존 오브젝트 제거
        Destroy(currentGun.gameObject);

        currentGun = newGun;
        _fireAction.ReplaceGun(newGun);
        _reloadAction.ReplaceGun(newGun);
        _movementAction.ReplaceGun(newGun);
    }
}
