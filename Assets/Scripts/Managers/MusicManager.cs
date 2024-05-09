using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioSource m_BaseBGM;
    [SerializeField] private AudioSource m_TenseBGM;
    [SerializeField] private AudioSource m_LowEnergyStinger;

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
        WaitForClip(m_LowEnergyStinger);
        m_TenseBGM.Play();
      
    }

    private IEnumerator WaitForClip(AudioSource audioClip)
    {
        yield return new WaitForSeconds(m_LowEnergyStinger.clip.length);
    }
}
