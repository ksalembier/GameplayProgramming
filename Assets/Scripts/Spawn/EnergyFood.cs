using UnityEngine;

public class EnergyFood : FoodBase
{
    [SerializeField] private float energyRestoreAmount = 25f;

    protected override void OnCollected(PlayerController player)
    {
        player.stats.RestoreEnergy(energyRestoreAmount);
    }
}