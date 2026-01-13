using UnityEngine;
using UnityEngine.UI;

public class EnergyBarUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Slider energySlider;
    [SerializeField] private Image fillImage;

    [Header("Player")]
    [SerializeField] private int playerID;

    [Header("Thresholds")]
    [SerializeField] private float lowThreshold = 0.5f;
    [SerializeField] private float criticalThreshold = 0.25f;

    [Header("Colors")]
    [SerializeField] private Color normalColor = Color.green;
    [SerializeField] private Color lowColor = Color.yellow;
    [SerializeField] private Color criticalColor = Color.red;
    [SerializeField] private Color lethargicColor = Color.gray;

    [Header("Animation")]
    [SerializeField] private Animator animator;
    private static readonly int ShakeParam = Animator.StringToHash("IsShaking");

    private void Awake()
    {
        if (energySlider == null)
            energySlider = GetComponent<Slider>();

        if (fillImage == null)
            fillImage = energySlider.fillRect.GetComponent<Image>();
    }

    private void OnEnable()
    {
        EventBus.Subscribe("EnergyChanged", OnEnergyChanged);
        EventBus.Subscribe("PlayerLethargic", OnPlayerLethargic);
        EventBus.Subscribe("PlayerRecovered", OnPlayerRecovered);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe("EnergyChanged", OnEnergyChanged);
        EventBus.Unsubscribe("PlayerLethargic", OnPlayerLethargic);
        EventBus.Unsubscribe("PlayerRecovered", OnPlayerRecovered);
    }

    private void OnEnergyChanged(object payload)
    {
        PlayerController player = payload as PlayerController;
        if (player == null) return;
        if (player.playerID != playerID) return;

        PlayerStats stats = player.stats;

        energySlider.maxValue = stats.maxEnergy;
        energySlider.value = stats.energy;

        UpdateColor(stats);
    }

    private void UpdateColor(PlayerStats stats)
    {
        float percent = stats.energy / stats.maxEnergy;

        if (stats.speedState.Level == -1)
        {
            fillImage.color = lethargicColor;
            return;
        }

        if (percent <= criticalThreshold)
            fillImage.color = criticalColor;
        else if (percent <= lowThreshold)
            fillImage.color = lowColor;
        else
            fillImage.color = normalColor;
    }

    private void OnPlayerLethargic(object payload)
    {
        PlayerController player = payload as PlayerController;
        if (player == null) return;
        if (player.playerID != playerID) return;

        if (animator != null)
            animator.SetBool(ShakeParam, true);
    }

    private void OnPlayerRecovered(object payload)
    {
        if (animator != null)
            animator.SetBool(ShakeParam, false);

        PlayerController player = payload as PlayerController;
        if (player == null) return;
        if (player.playerID != playerID) return;
    }
}
