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
    private bool IsSceneNameValid() // 무결성 체크
    {
        // 씬 목록을 가져옵니다.
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            // 씬 이름을 비교합니다.
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            string sceneFileName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
            if (sceneFileName == SceneName)
            {
                return true; // 유효한 씬 이름
            }
        }
        return false; // 유효하지 않은 씬 이름
    }

    public void Exit()
    {
        SceneManager.LoadScene(SceneName);
    }
}
