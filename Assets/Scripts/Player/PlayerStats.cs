using UnityEngine;

[System.Serializable]
public class PlayerStats
{
    public float score;
    public float energy;
    public float maxEnergy = 100f;

    public PlayerSpeedState speedState;

    [System.NonSerialized]
    public PlayerController owner;

    public PlayerStats()
    {
        score = 0;
        energy = maxEnergy;
        speedState = new SpeedLevel0();
    }

    public void RestoreEnergy(float amount)
    {
        energy += amount;

        // with no overfill
        if (energy > maxEnergy) { energy = maxEnergy; }

        // to allow overfill (up to 150%)
        //float overfillCap = maxEnergy * 1.5f;
        //if (energy > overfillCap)
        //{
        //    energy = overfillCap;
        //}

        // recover from lethargic state
        if (energy > 0 && speedState.Level == -1)
        {
            speedState = new SpeedLevel0();
            EventBus.Publish("PlayerRecovered", this);
        }

        EventBus.Publish("EnergyChanged", owner);
        Debug.Log($"Energy: {energy}, Speed State: {speedState.Level}");
    }
}

