using TMPro;
using UnityEngine;

public class UITexts : MonoBehaviour
{
    [Header("Player stats")]
    public TextMeshProUGUI HpText;
    
    [Header("Game")]
    public TextMeshProUGUI CoolPointsText;
    public TextMeshProUGUI RoundNumberText;
    public TextMeshProUGUI WaveNumberText;
    public TextMeshProUGUI RoundTimeRemainText;
    
    [Header("Weapons stats")]
    public TextMeshProUGUI LaserGunHeatValueText;
//  TODO:
    public TextMeshProUGUI LaserShotgunHeatValueText;
    public TextMeshProUGUI LaserBladeHeatValue;
}