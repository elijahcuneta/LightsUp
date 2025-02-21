using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour {

    public float startingHealth = 60, startingMana = 50;
    public float healthAddRate = 1;

    Animator playerAnim;
    PlayerMovement playerMovement;
    AudioSource playerAudioSource;

    public static float currentHealth, currentMana;
    public static float originalHealth, originalMana;

    [SerializeField]
    AudioClip playerHit1SFX, playerHit2SFX, playerDeathSFX;

    [HideInInspector]
    public bool isDead;

    void Awake() {
        DontDestroyOnLoad(gameObject);

        originalHealth = currentHealth = startingHealth;
        originalMana = currentMana = startingMana;
        currentMana = 0;
        playerAnim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        playerAudioSource = GetComponent<AudioSource>();
    }

    void Update() {
        if (currentHealth < originalHealth) { 
            currentHealth += healthAddRate * Time.deltaTime;
        }
    }

    public void TakeDamage(int damageTaken) {
        currentHealth -= damageTaken;

        if (currentHealth <= 0 && !isDead) {
            Death();
        } else {
            playerAnim.SetTrigger("Hit");
            if(playerAudioSource.clip != playerHit1SFX || playerAudioSource.clip != playerHit2SFX) {
                playerAudioSource.clip = Random.Range(0, 2) == 0 ? playerHit1SFX : playerHit2SFX;
            }
            playerAudioSource.Play();
        }
    }

    public void Death() {
        playerAnim.SetTrigger("Dead");
        if (playerAudioSource.clip != playerDeathSFX) {
            playerAudioSource.clip = playerDeathSFX;
        }
        playerAudioSource.Play();
        isDead = true;
        playerMovement.playerCanDoAnything = false;
        currentHealth = 0;
        StartCoroutine(DeathTransition());
    }

    IEnumerator DeathTransition() {
        float fadingTime = FindObjectOfType<FadeEffect>().BeginFade(1);
        yield return new WaitForSeconds(fadingTime);
        foreach(GameObject g in FindObjectsOfType<GameObject>()) {
            if (g.name != "SaveManager")
            Destroy(g);
        }
        SceneManager.LoadScene(1);
        StopAllCoroutines();
        Destroy(gameObject);
    }
}
