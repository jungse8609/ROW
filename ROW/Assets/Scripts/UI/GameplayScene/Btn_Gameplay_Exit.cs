using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Btn_Gameplay_Exit : MonoBehaviour
{
    [SerializeField]
    private SceneAsset TitleScene;
    private string SceneName;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (null == TitleScene)
        {
            Debug.Log("Btn_Gameplay_Exit : TitleScene is Null");
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        SceneName = TitleScene.name;

        if (!IsSceneNameValid())
        {
            Debug.Log("Btn_Gameplay_Exit : TitleScene is InValid");
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
    private bool IsSceneNameValid() // ���Ἲ üũ
    {
        // �� ����� �����ɴϴ�.
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            // �� �̸��� ���մϴ�.
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            string sceneFileName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
            if (sceneFileName == SceneName)
            {
                return true; // ��ȿ�� �� �̸�
            }
        }
        return false; // ��ȿ���� ���� �� �̸�
    }

    public void Exit()
    {
        SceneManager.LoadScene(SceneName);
    }
}
