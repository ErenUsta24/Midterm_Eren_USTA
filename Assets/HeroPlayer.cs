using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class HeroPlayer : MonoBehaviour
{
    [Header("Animators")]
    public Animator p1Animator;
    public Animator enemyAnimator;

    [Header("Particle Effects")]
    public GameObject attack1Effect;
    public GameObject attack2Effect;

    [Header("UI Elements")]
    public TMP_Text enemyHealthText;
    public Image enemyHealthBar;
    public GameObject gameOverPanel;

    [Header("Health Settings")]
    public int maxEnemyHealth = 100;
    private int currentEnemyHealth;
    private bool isEnemyDead = false;

    void Start()
    {
        currentEnemyHealth = maxEnemyHealth;
        UpdateHealthUI();

        if (attack1Effect != null) attack1Effect.SetActive(false);
        if (attack2Effect != null) attack2Effect.SetActive(false);
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
    }

    public void Attack1()
    {
        if (isEnemyDead) return;

        if (p1Animator != null) p1Animator.SetTrigger("Attack");
        PlayEffect(attack1Effect);
        DamageEnemy(15);
    }

    public void Attack2()
    {
        if (isEnemyDead) return;

        if (p1Animator != null) p1Animator.SetTrigger("Attack");
        PlayEffect(attack2Effect);
        DamageEnemy(25);
    }

    private void DamageEnemy(int damage)
    {
        currentEnemyHealth = Mathf.Max(0, currentEnemyHealth - damage);
        UpdateHealthUI();

        if (currentEnemyHealth <= 0)
        {
            isEnemyDead = true;
            if (enemyAnimator != null) enemyAnimator.SetBool("Death", true);
            Invoke(nameof(ShowGameOver), 1.2f); 
        }
        else
        {
            if (enemyAnimator != null) enemyAnimator.SetTrigger("Hurt");
        }
    }

    private void PlayEffect(GameObject effectObj)
    {
        if (effectObj == null) return;
        effectObj.SetActive(false);
        effectObj.SetActive(true);
    }

    private void UpdateHealthUI()
    {
        if (enemyHealthText != null)
            enemyHealthText.text = "Enemy Health: " + currentEnemyHealth;

        if (enemyHealthBar != null)
            enemyHealthBar.fillAmount = (float)currentEnemyHealth / maxEnemyHealth;
    }

    private void ShowGameOver()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}