using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIElements : MonoBehaviour
{
    [Header("Player stats")]
    public TextMeshProUGUI HpText;
    public Image HealthBar;
    
    [Header("Game")]
    public TextMeshProUGUI CoolPointsText;
    public TextMeshProUGUI RoundNumberText;
    public TextMeshProUGUI WaveNumberText;
    public TextMeshProUGUI RoundTimeRemainText;
    
    [Header("Weapons stats")]
    public TextMeshProUGUI LaserGunHeatValueText;
    public Image LaserGunHeatValueImage;
//  TODO:
    public TextMeshProUGUI LaserShotgunHeatValueText;
    public TextMeshProUGUI LaserBladeHeatValue;
}