using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMenu : MonoBehaviour
{
    // Public method to load the menu scene
    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu"); // Replace "MenuScene" with the actual name of your menu scene
    }

    // Detect mouse click on the object
    private void OnMouseDown()
    {
             Debug.Log("errpr");
        LoadMenu();
    }
}