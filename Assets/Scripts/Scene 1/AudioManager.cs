using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //Storing sound
    public AudioClip dirtDestroy;
    public AudioClip dirtPlace;

    public AudioClip grassDestroy;
    public AudioClip grassPlace;

    public AudioClip sandDestroy;
    public AudioClip sandPlace;

    public AudioClip stoneDestroy;
    public AudioClip stonePlace;

    //Required methods for destroying and placing blocks
    void GrassDestroy()
    {
        GetComponent<AudioSource>().PlayOneShot(grassDestroy);
    }

    void GrassPlace()
    {
        GetComponent<AudioSource>().PlayOneShot(grassPlace);
    }

    void DirtDestroy()
    {
        GetComponent<AudioSource>().PlayOneShot(dirtDestroy);
    }

    void DirtPlace()
    {
        GetComponent<AudioSource>().PlayOneShot(dirtPlace);
    }

    void StonePlace()
    {
        GetComponent<AudioSource>().PlayOneShot(stonePlace);
    }

    void StoneDestroy()
    {
        GetComponent<AudioSource>().PlayOneShot(stoneDestroy);
    }

    void SandPlace()
    {
        GetComponent<AudioSource>().PlayOneShot(sandPlace);
    }

    void SandDestroy()
    {
        GetComponent<AudioSource>().PlayOneShot(sandDestroy);
    }

    // When game object is enabled
    void OnEnable()
    {
        //Subscribing to required events
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
        //Unsubscribing to events
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
