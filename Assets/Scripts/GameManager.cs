using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameData _gameData;
    [SerializeField] private LightsManager _lightsManager;
    [SerializeField] private Timer _timer;

    [SerializeField] private TextMeshProUGUI ChipherTMP;
    private string ChipherSide;
    private GenerationSide _generationSide;

    private void Start()
    {
        _generationSide = new GenerationSide();

        CreateNewSide();
    }

    public void CreateNewSide()
    {
        print(_gameData.difficult);
        ChipherSide = _generationSide.GenerateSide(_gameData.difficult, _lightsManager.lightColor, out _gameData.Side, out _gameData.OneRightSide);
        ChipherTMP.text = ChipherSide;

        _timer.ResetTimerBar();
    }
    public void Loos()
    {
        print("Nope");
    }
}
