using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioSource m_BaseBGM;
    [SerializeField] private AudioSource m_TenseBGM;
    [SerializeField] private AudioSource m_LowEnergyStinger;
    [SerializeField] private AudioSource m_DeathSound;

    void Start()
    {
        
    }

    void Update()
    {

    }

    public void PlayStinger()
    {
        m_BaseBGM.Stop();
        m_LowEnergyStinger.Play();
        StartCoroutine(WaitForClip(m_LowEnergyStinger));
        //m_TenseBGM.Play();
      
    }

    public void PlayDeathSound()
    {
        m_DeathSound.Play();
    }

    private IEnumerator WaitForClip(AudioSource audioClip)
    {
        yield return new WaitForSeconds(audioClip.clip.length);
        m_TenseBGM.Play();
    }
}
