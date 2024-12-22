using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Btn_Start : MonoBehaviour
{
    [SerializeField]
    private SceneAsset NextScene;

    private string SceneName;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(null == NextScene)
        {
            Debug.Log("Btn_Start : NextScene is Null");
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        SceneName = NextScene.name;

        if(!IsSceneNameValid())
        {
            Debug.Log("Btn_Start : NextScene is InValid");
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }

    public void SceneChange()
    {
        SceneManager.LoadScene(SceneName);
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
}
