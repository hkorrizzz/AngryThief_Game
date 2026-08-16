using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ExitGame()
    {
        Debug.Log("quit");
        Application.Quit();
    }

    public void Lvl1()
    {
        SceneManager.LoadScene(1);
    }

    public void Lvl2()
    {
        SceneManager.LoadScene(2);
    }
    public void Lvl3()
    {
        SceneManager.LoadScene(3);
    }

    public void Lvl4()
    {
        SceneManager.LoadScene(4);
    }
    public void Lvl5()
    {
        SceneManager.LoadScene(5);
    }
    public void Lvl6()
    {
        SceneManager.LoadScene(3); //6
    }
    public void Lvl7()
    {
        SceneManager.LoadScene(2); //7
    }
    public void Lvl8()
    {
        SceneManager.LoadScene(5);  //8
    }


}
