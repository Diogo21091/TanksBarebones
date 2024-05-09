using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioSource m_BaseBGM;
    [SerializeField] private AudioSource m_TenseBGM;
    [SerializeField] private AudioSource m_LowEnergyStinger;

    public TankHealth health;

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

    private IEnumerator WaitForClip(AudioSource audioClip)
    {
        yield return new WaitForSeconds(audioClip.clip.length);
        m_TenseBGM.Play();
    }
}
