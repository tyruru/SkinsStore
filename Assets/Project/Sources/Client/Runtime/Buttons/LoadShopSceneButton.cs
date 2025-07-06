using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadShopSceneButton : MonoBehaviour
{
    public void Execute()
    {
        SceneManager.LoadScene("ShopScene");
    }
}
