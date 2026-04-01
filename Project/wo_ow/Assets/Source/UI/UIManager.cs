using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private UIElements _elements;
    [SerializeField] private PlayerController _player;
    [SerializeField] private GameManager _gameManager;


    private void Update()
    {
        UpdateHP(_player.Health);
        
        UpdateWaveNumberText(_gameManager.GetGameStats().WaveNumber);
        UpdateRoundNumberText(_gameManager.GetGameStats().RoundNumber);
        UpdateRoundTime(Math.Round(_gameManager.GetGameStats().RoundTime, 0).ToString());
    }

//  playerp
    private void UpdateHP(HealthPoint playerHP)
    {
        _elements.HpText.text = string.Format("{0:F0} / {1:F0}", playerHP.HP, playerHP.MaxHP);
        
        UpdateHealthBar(playerHP);
    }

    private void UpdateHealthBar(HealthPoint playerHP)
        => _elements.HealthBar.fillAmount = playerHP.GetHealthPrecent();

//  game
    private void UpdateCoolPoints(int points)
    {
        _elements.CoolPointsText.text = points.ToString();
    }

    private void UpdateRoundTime(string time)
    {
        _elements.RoundTimeRemainText.text = time;
    }

    private void UpdateRoundNumberText(int roundNumber)
    {
        _elements.RoundNumberText.text = roundNumber.ToString();
    }

    private void UpdateWaveNumberText(int waveNumber) {
        _elements.WaveNumberText.text = "Wave: " + waveNumber;
    }

//  weapons
    private void UpdateLaserGunStats(float heatValue)
    {
        _elements.LaserGunHeatValueText.text = heatValue.ToString();
    }

    private void UpdateLaserShotgunStats(float heatValue)
    {
        _elements.LaserShotgunHeatValueText.text = heatValue.ToString();
    }

    private void UpdateLaserBladeStats(float heatValue)
    {
        _elements.LaserBladeHeatValue.text = heatValue.ToString();
    }
    
    
// help methods
    private string ConvertFloatTimeToString(float time) {
        // TODO: implement this method
        return "";
    }
}