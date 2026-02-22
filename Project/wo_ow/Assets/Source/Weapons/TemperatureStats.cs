public class TemperatureStats
{
    public float MaxHeatValue;
    public float HeatPerShot;
    public float HeatPerAbility;

    public TemperatureStats(float maxHeatValue, float heatPerAbility, float heatPerShot)
    {
        MaxHeatValue = maxHeatValue;
        HeatPerAbility = heatPerAbility;
        HeatPerShot = heatPerShot;
    }
}