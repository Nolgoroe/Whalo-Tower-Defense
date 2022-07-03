using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsPanel : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText = null;

    [SerializeField]
    private Text _fundsText = null;

    [SerializeField]
    private Text _coreHealthText = null;

    [SerializeField]
    private GameManager _gameManager = null;

    [SerializeField]
    private PlayerCore _playerCore = null;

    private void Update()
    {
        if (!GameManager.gameRunning)
        {
            return;
        }

        _scoreText.text = _gameManager.playerState.Score.ToString();
        _fundsText.text = _gameManager.playerState.Funds.ToString();
        _coreHealthText.text = _playerCore.Hp.ToString();
    }
}
