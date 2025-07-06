using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGameplaySceneButton : MonoBehaviour
{
    public void Execute()
    {
        SceneManager.LoadScene("GameplayScene");
    }
}
