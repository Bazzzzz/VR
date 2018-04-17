using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAnimation : MonoBehaviour {
    public Texture[] textures;

    public float changeInterval = 0.33F;
    private Renderer rend;

    // Use this for initialization
    void Start () {
        rend = GetComponent<Renderer>();
    }
	
	// Update is called once per frame
	void Update () {
        if (textures.Length == 0)
        {
            return;
        }

        int index = Mathf.FloorToInt(Time.time / changeInterval);
        index = index % textures.Length;
        
        rend.materials[1].mainTexture = textures[index];
    }
}
