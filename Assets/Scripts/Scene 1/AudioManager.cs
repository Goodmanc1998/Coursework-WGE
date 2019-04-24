using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public AudioClip dirtDestroy;
    public AudioClip dirtPickup;

    public AudioClip grassDestroy;
    public AudioClip grassPickup;

    public AudioClip sandDestroy;
    public AudioClip sandPickup;

    public AudioClip stoneDestroy;
    public AudioClip stonePickup;

    // play the destroy block sound
    void PlayDestroyBlockSound()
    {
        //GetComponent<AudioSource>().PlayOneShot(destroyBlockSound);
    }

    // play the place block sound
    void PlayPlaceBlockSound()
    {
        //GetComponent<AudioSource>().PlayOneShot(placeBlockSound);
    }

    void GrassDestroy()
    {
        Debug.Log("GrassDestroy");
    }

    void GrassPlace()
    {
        Debug.Log("GrassPlace");
    }

    void DirtDestroy()
    {
        Debug.Log("DD");
    }

    void DirtPlace()
    {
        Debug.Log("DP");
    }

    void StonePlace()
    {
        Debug.Log("StP");
    }

    void StoneDestroy()
    {
        Debug.Log("StD");
    }

    void SandPlace()
    {
        Debug.Log("SaP");
    }

    void SandDestroy()
    {
        Debug.Log("SaD");
    }

    // When game object is enabled
    void OnEnable()
    {
        VoxelChunk.OnEventBlockDestroyed += PlayDestroyBlockSound;
        VoxelChunk.OnEventBlockPlaced += PlayPlaceBlockSound;

        VoxelChunk.OnEventGrassDestroy += GrassDestroy;
        VoxelChunk.OnEventGrassPlaced += GrassPlace;

        VoxelChunk.OnEventDirtDestroy += DirtDestroy;
        VoxelChunk.OnEventDirtPlaced += DirtPlace;

        VoxelChunk.OnEventStoneDestroy += StoneDestroy;
        VoxelChunk.OnEventStonePlaced += StonePlace;

        VoxelChunk.OnEventSandDestroy += SandDestroy;
        VoxelChunk.OnEventSandPlaced += SandPlace;

    }
    // When game object is disabled
    void OnDisable()
    {
        VoxelChunk.OnEventBlockDestroyed -= PlayDestroyBlockSound;
        VoxelChunk.OnEventBlockPlaced -= PlayPlaceBlockSound;

        VoxelChunk.OnEventGrassDestroy -= GrassDestroy;
        VoxelChunk.OnEventGrassPlaced -= GrassPlace;

        VoxelChunk.OnEventDirtDestroy -= DirtDestroy;
        VoxelChunk.OnEventDirtPlaced -= DirtPlace;

        VoxelChunk.OnEventStoneDestroy -= StoneDestroy;
        VoxelChunk.OnEventStonePlaced -= StonePlace;

        VoxelChunk.OnEventSandDestroy -= SandDestroy;
        VoxelChunk.OnEventSandPlaced -= SandPlace;
    }

}
