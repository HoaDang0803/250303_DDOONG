using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    private bool LevelComplete = false;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "Player" && !LevelComplete)
        {
            LevelComplete = true;
            Invoke("CompleteLevel", 0.1f);
        }
    }

    private void CompleteLevel()
    {
        SoundController.instance.PlayBackgroundMusic(SoundController.instance.bgMusicClip2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
