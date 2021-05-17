using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource audioSourceMusic;
    public AudioSource audioSourceSFX;
    public AudioSource audioSourceEnemySFX;

    public AudioClip levelBackgroundMusic;
    public AudioClip mainMenuBackgroundMusic;
    public AudioClip defeatBackgroundMusic;
    public AudioClip victoryBackgroundMusic;
    public AudioClip lobbyBackgroundMusic;

    public AudioClip playerShootSFX;
    public AudioClip playerHurtSFX;
    public AudioClip enemyShootSFX;
    public AudioClip enemyDeathSFX;
    public AudioClip healthPickupSFX;
    public AudioClip bossGrowlSFX;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void playLevelMusic()
    {
        audioSourceMusic.clip = levelBackgroundMusic;
        audioSourceMusic.loop = true;
        audioSourceMusic.Play();
    }
    public void playMainMenuMusic()
    {
        audioSourceMusic.clip = mainMenuBackgroundMusic;
        audioSourceMusic.loop = true;
        audioSourceMusic.Play();
    }
    public void playLobbyMusic()
    {
        audioSourceMusic.clip = lobbyBackgroundMusic;
        audioSourceMusic.loop = true;
        audioSourceMusic.Play();
    }
    public void playDefeatMusic()
    {
        audioSourceMusic.clip = defeatBackgroundMusic;
        audioSourceMusic.loop = false;
        audioSourceMusic.Play();
    }
    public void playVictoryMusic()
    {
        audioSourceMusic.clip = victoryBackgroundMusic;
        audioSourceMusic.loop = false;
        audioSourceMusic.Play();
    }

    public void playPlayerShootSFX()
    {
        audioSourceSFX.clip = playerShootSFX;
        audioSourceSFX.Play();
    }
    public void playPlayerHurtSFX()
    {
        audioSourceSFX.clip = playerHurtSFX;
        audioSourceSFX.Play();
    }
    public void playEnemyShootSFX()
    {
        audioSourceEnemySFX.clip = enemyShootSFX;
        audioSourceSFX.Play();
    }
    public void playEnemyDeathSFX()
    {
        audioSourceEnemySFX.clip = enemyDeathSFX;
        audioSourceSFX.Play();
    }
    public void playHealthPickupSFX()
    {
        audioSourceSFX.clip = healthPickupSFX;
        audioSourceSFX.Play();
    }
    public void playBossGrowlSFX()
    {
        audioSourceEnemySFX.clip = bossGrowlSFX;
        audioSourceSFX.Play();
    }

}
