using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private UITexts _texts;
    [SerializeField] private GameObject _player;


    private void Update()
    {
        UpdateHP(_player.GetComponent<PlayerController>()._playerStats.HP.HP);
    }

//  player
    public void UpdateHP(float hp)
    {
        _texts.HpText.text = string.Format("{0:F1} / 100", hp);
    }

//  game
    public void UpdateCoolPoints(int points)
    {
        _texts.CoolPointsText.text = points.ToString();
    }

    public void UpdateRoundTime(string time)
    {
        _texts.RoundTimeRemainText.text = time;
    }

    public void UpdateRoundNumberText(int roundNumber)
    {
        _texts.RoundNumberText.text = roundNumber.ToString();
    }
    
//  weapons
    public void UpdateLaserGunStats(float heatValue)
    {
        _texts.LaserGunHeatValueText.text = heatValue.ToString();
    }

    public void UpdateLaserShotgunStats(float heatValue)
    {
        _texts.LaserShotgunHeatValueText.text = heatValue.ToString();
    }

    public void UpdateLaserBladeStats(float heatValue)
    {
        _texts.LaserBladeHeatValue.text = heatValue.ToString();
    }
}