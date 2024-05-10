using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TankHealth : MonoBehaviour
{
    public float m_StartingHealth = 100f;
    public float m_LowHealth = 15f;
    public Slider m_Slider;                        
    public Image m_FillImage;                      
    public Color m_FullHealthColor = Color.green;  
    public Color m_ZeroHealthColor = Color.red;    
    public GameObject m_ExplosionPrefab;
        
    private ParticleSystem m_ExplosionParticles;   
    public float m_CurrentHealth;  
    private bool m_Dead;

    public GameObject m_GameManager;
    private MusicManager m_MusicManager;
    private bool m_PlayedStinger = false;

    private void Awake()
    {
        m_ExplosionParticles = Instantiate(m_ExplosionPrefab).GetComponent<ParticleSystem>();

        m_ExplosionParticles.gameObject.SetActive(false);

        m_MusicManager = m_GameManager.GetComponent<MusicManager>();
    }


    private void OnEnable()
    {
        m_CurrentHealth = m_StartingHealth;
        m_Dead = false;

        SetHealthUI();
    }
    

    public void TakeDamage(float amount)
    {
        // Adjust the tank's current health, update the UI based on the new health and check whether or not the tank is dead.
        m_CurrentHealth -= amount;
    }

    private void Update()
    {
        SetHealthUI();

        if (m_CurrentHealth <= 0f && !m_Dead)
        {
            OnDeath();
        }
        else if(!m_PlayedStinger && m_CurrentHealth <= m_LowHealth)
        {
            m_MusicManager.PlayStinger();
            m_PlayedStinger = true;
        }
    }

    private void SetHealthUI()
    {
        // Adjust the value and colour of the slider.
        m_Slider.value = m_CurrentHealth;
        m_FillImage.color = Color.Lerp(m_ZeroHealthColor, m_FullHealthColor, m_CurrentHealth / m_StartingHealth);
    }


    private void OnDeath()
    {
        m_MusicManager.PlayDeathSound();

        // Play the effects for the death of the tank and deactivate it.
        m_Dead = true;

        m_ExplosionParticles.transform.position = transform.position;
        m_ExplosionParticles.gameObject.SetActive(true);
        m_ExplosionParticles.Play();

        gameObject.SetActive(false);

        Invoke(nameof(Reset), m_ExplosionParticles.main.duration);
    }

    private void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}