using UnityEngine;
using UnityEngine.SceneManagement; 

public class SceneChanger : MonoBehaviour
{
    public int sceneBuildIndexer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("enterd");

        if(collision != null)
        {
            SceneManager.LoadScene(sceneBuildIndexer, LoadSceneMode.Single);
        }
    }

    public void ChangeSceneOnbuttonPressed()
    {
        SceneManager.LoadScene(sceneBuildIndexer, LoadSceneMode.Single);
    }
}
