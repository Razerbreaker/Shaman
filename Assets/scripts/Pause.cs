using UnityEngine;

public class Pause : MonoBehaviour
{

    public bool isPaused;
    public KeyCode pauseButton;

    [SerializeField]
    private GameObject PausePanel;

    void Update()
    {
        if (Input.GetKeyDown(pauseButton))
        {
            isPaused = !isPaused;
        }
        if (isPaused)
        {
            PausePanel.SetActive(true);
            Cursor.visible = true;
            Time.timeScale = 0;
        }
        else
        {
            PausePanel.SetActive(false);
            Cursor.visible = false;
            Time.timeScale = 1;
        }
    }
}
