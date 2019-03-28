using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip destroyBlockSound;
    public AudioClip placeBlockSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // play the destroy block sound
    void PlayDestroyBlockSound()
    {
        GetComponent<AudioSource>().PlayOneShot(destroyBlockSound);
    }

    // play the place block sound
    void PlayPlaceBlockSound()
    {
        GetComponent<AudioSource>().PlayOneShot(
        placeBlockSound);
    }

    // When game object is enabled
    void OnEnable()
    {
        VoxelChunk.OnEventBlockDestroyed += PlayDestroyBlockSound;
        VoxelChunk.OnEventBlockPlaced += PlayPlaceBlockSound;
    }
    // When game object is disabled
    void OnDisable()
    {
        VoxelChunk.OnEventBlockDestroyed -= PlayDestroyBlockSound;
        VoxelChunk.OnEventBlockPlaced -= PlayPlaceBlockSound;
    }

}
