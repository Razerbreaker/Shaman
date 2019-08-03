using UnityEngine;

public class ButtonManager : MonoBehaviour
{

    public void Resume(GameObject obj)
    {
        obj.GetComponent<Pause>().isPaused = false;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
