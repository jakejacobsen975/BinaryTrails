using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoBlock : MonoBehaviour
{   
    // for som reason in the prefab I can't reset the PanelOpener so just open the gameobject and search PannelOpener and add the script
    public SpriteRenderer spriteRenderer;
    
    public PanelOpener infoPanel;

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.CompareTag("Player") ){
            infoPanel.OpenPanel();
        }
    }
}
