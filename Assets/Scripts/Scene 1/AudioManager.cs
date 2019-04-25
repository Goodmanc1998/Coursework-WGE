using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public AudioClip dirtDestroy;
    public AudioClip dirtPlace;

    public AudioClip grassDestroy;
    public AudioClip grassPlace;

    public AudioClip sandDestroy;
    public AudioClip sandPlace;

    public AudioClip stoneDestroy;
    public AudioClip stonePlace;

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
