using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    public void Quit () => SceneChanger.ChangeScene(Scene.Title);
    public void Restart () => SceneChanger.ChangeScene(Scene.GameScene);

    public void SetActive (bool active) => gameObject.SetActive(active);
}