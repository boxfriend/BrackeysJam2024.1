using Michsky.MUIP;
using UnityEngine;

public class TitleScreenManager : MonoBehaviour
{
    [SerializeField] private ButtonManager _resetScoreButton;

    [SerializeField] private string _discordInviteURL;
    [SerializeField] private PlayerShooter _player;
    private void Awake ()
    {
        if (PlayerPrefs.GetInt(ScoreTracker.HighScoreKey) == 0)
            _resetScoreButton.Interactable(false);
    }

    public void ResetScore () => ScoreTracker.ResetHighScore();

#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
    public void PlayGame () => SceneChanger.ChangeScene(Scene.GameScene);
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed

    public void DiscordInvite () => Application.OpenURL(_discordInviteURL);

    public void PlayerShoot () => _player.SendMessage("OnShoot");
}
