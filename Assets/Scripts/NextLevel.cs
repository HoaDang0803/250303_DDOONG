using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    private bool LevelComplete = false;
    public SceneTransition sceneTransition;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "Player" && !LevelComplete)
        {
            LevelComplete = true;
            sceneTransition.ChangeScene();
            Invoke("CompleteLevel", 1.2f);
        }
    }

    private void CompleteLevel()
    {
        SoundController.instance.PlayBackgroundMusic(SoundController.instance.bgMusicClip2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
