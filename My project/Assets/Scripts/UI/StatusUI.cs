using UnityEngine;
using UnityEngine.UIElements;

public class StatusUI : MonoBehaviour
{
    GameObject player;
    HealthComponent playerHealth;

    private Label healthLabel;
    private Label pointsLabel;
    private Label waveLabel;
    private Label enemyCountLabel;

    [SerializeField] private CombatManager combatManager;

    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<HealthComponent>();
        var root = GetComponent<UIDocument>().rootVisualElement;

        healthLabel = root.Q<Label>("health-label");
        pointsLabel = root.Q<Label>("points-label");
        waveLabel = root.Q<Label>("wave-label");
        enemyCountLabel = root.Q<Label>("enemy-count-label");
    }

    private void Update()
    {
        if (healthLabel != null && playerHealth != null)
            healthLabel.text = $"Health: {playerHealth.GetHealth()} / {playerHealth.GetMaxHealth()}";

        if (pointsLabel != null && combatManager != null)
            pointsLabel.text = $"Points: {combatManager.points}";

        if (waveLabel != null && combatManager != null)
            waveLabel.text = $"Wave: {combatManager.waveNumber-1}";

        if (enemyCountLabel != null && combatManager != null)
            enemyCountLabel.text = $"Enemies: {combatManager.totalEnemies}";
    }
}