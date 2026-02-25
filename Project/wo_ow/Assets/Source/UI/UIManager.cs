using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private UITexts _texts;
    [SerializeField] private GameObject _player;


    private void Update()
    {
        UpdateHP(_player.GetComponent<PlayerController>()._playerStats.HP);
    }

//  playerp
    private void UpdateHP(HealthPoint playerHP)
    {
        _texts.HpText.text = string.Format("{0:F0} / {1:F0}", playerHP.HP, playerHP.MaxHP);
    }

//  game
    private void UpdateCoolPoints(int points)
    {
        _texts.CoolPointsText.text = points.ToString();
    }

    private void UpdateRoundTime(string time)
    {
        _texts.RoundTimeRemainText.text = time;
    }

    private void UpdateRoundNumberText(int roundNumber)
    {
        _texts.RoundNumberText.text = roundNumber.ToString();
    }
    
//  weapons
    private void UpdateLaserGunStats(float heatValue)
    {
        _texts.LaserGunHeatValueText.text = heatValue.ToString();
    }

    private void UpdateLaserShotgunStats(float heatValue)
    {
        _texts.LaserShotgunHeatValueText.text = heatValue.ToString();
    }

    private void UpdateLaserBladeStats(float heatValue)
    {
        _texts.LaserBladeHeatValue.text = heatValue.ToString();
    }
}