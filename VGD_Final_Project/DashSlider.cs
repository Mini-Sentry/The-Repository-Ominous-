using UnityEngine;
using UnityEngine.UI;

public class DashIcon : MonoBehaviour {

    public Slider dashSlider; // UI Slider for the icon
    public float dashdelay = 1.0f; // Duration of the dash cooldown
    private float cooldownTimer; // Current timer for cooldown
    private bool isCooldownActive = true; // Flag to track if cooldown is active

    // Start is called before the first frame update
    void Start() {
        cooldownTimer = 0;
        isCooldownActive = false;
    }

    // Update is called once per frame
    void Update() {
        // Check if cooldown is active
        if (isCooldownActive) {
            // Reduce timer
            cooldownTimer -= Time.deltaTime;

            // Update Slider fill amount
            dashSlider.value = Mathf.Clamp01(cooldownTimer / dashdelay); // Clamp to ensure between 0 and 1

            // If cooldown is finished
            if (cooldownTimer <= 0) {
                isCooldownActive = false;
                dashSlider.value = 1; // Reset fill
            }
        }
    }

    // Call this function when the dash ability is used
    public void StartCooldown() {
        cooldownTimer = dashdelay;
        isCooldownActive = true;
    }
}