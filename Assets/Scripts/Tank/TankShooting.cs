using UnityEngine;
using UnityEngine.UI;

public class TankShooting : MonoBehaviour
{
    public int m_PlayerNumber = 1;       
    public Rigidbody m_Shell;            
    public Transform m_FireTransform;    
    public Slider m_AimSlider;
    public float m_MinLaunchForce = 15f; 
    public float m_MaxLaunchForce = 30f; 
    public float m_MaxChargeTime = 0.75f;


    private string m_FireButton;         
    private float m_CurrentLaunchForce;  
    private float m_ChargeSpeed;         
    private bool m_Fired;

    [SerializeField] private AudioSource m_ChargingSound;

    [SerializeField] private int m_NumberOfFireSounds;
    [SerializeField] private AudioSource m_FiringSound;
    [SerializeField] private AudioClip[] m_FireClips;

    private void OnEnable()
    {
        m_CurrentLaunchForce = m_MinLaunchForce;
        m_AimSlider.value = m_MinLaunchForce;
    }


    private void Start()
    {
        m_FireButton = "Fire" + m_PlayerNumber;

        m_ChargeSpeed = (m_MaxLaunchForce - m_MinLaunchForce) / m_MaxChargeTime;
    }


    private void Update()
    {
        // Track the current state of the fire button and make decisions based on the current launch force.
        m_AimSlider.value = m_MinLaunchForce;

        if(m_CurrentLaunchForce >= m_MaxLaunchForce && !m_Fired)
        {
            // reset and fire
            m_CurrentLaunchForce = m_MaxLaunchForce;
            Fire();
        }
        else if(Input.GetButtonDown(m_FireButton))
        {
            // start charging
            m_Fired = false;
            m_CurrentLaunchForce = m_MinLaunchForce;

            //Play charging soud
            m_ChargingSound.Play();
        }
        else if(Input.GetButton(m_FireButton) && !m_Fired)
        {
            // keep charging
            m_CurrentLaunchForce += m_ChargeSpeed * Time.deltaTime;
            m_AimSlider.value = m_CurrentLaunchForce;
        }
        else if (Input.GetButtonUp(m_FireButton) && !m_Fired)
        {
            // fire
            Fire();
        }
    }


    private void Fire()
    {
        // Instantiate and launch the shell.
        m_Fired = true;

        Rigidbody shellInstance = Instantiate(m_Shell, m_FireTransform.position, m_FireTransform.rotation) as Rigidbody;

        shellInstance.velocity = m_CurrentLaunchForce * m_FireTransform.forward;

        m_CurrentLaunchForce = m_MinLaunchForce;

        //Stop charging sound and play shooting sound
        m_ChargingSound.Stop();
        PlayShootingSound();
    }

    private void PlayShootingSound()
    {
        int soundToPlay;

        soundToPlay = Random.Range(0, m_NumberOfFireSounds);

        foreach(AudioClip value in m_FireClips)
        {
            Debug.Log(value);
        }
        m_FiringSound.PlayOneShot(m_FireClips[soundToPlay], 1);
    }
}