using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class AttachedBauHPBar : MonoBehaviour
{
    public Slider healthSlider;
    public Canvas panel;
    public Entity obj;
    private Color lastColor;
    

    void Update(){
        Player player = Player.localPlayer;
        if (player != null)
        {
            Entity target = player.nextTarget ?? player.target;
            var hpvalue = obj.GetComponent<Health>().Percent();

            if(healthSlider.value != hpvalue){
                healthSlider.value = hpvalue;
            }  
                                 
            if (target != null && target != player)
            {       
                if(obj.netId == target.netId){           
                    ChangeColor(Color.red); 
               }else{ 
                    ChangeColor(Color.yellow);
               }               
            }else{
                ChangeColor(Color.yellow);    
            }
        }
    }

    public void ChangeColor(Color color){
        if(lastColor!=color){            
            healthSlider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = color;
            lastColor = color;
        }
    }

    void LateUpdate(){        
            CamCorrection();
    }

    public void CamCorrection(){
            Camera cam = Camera.main; 
            Vector3 v = cam.transform.position - transform.position;
            v.x = v.z = 0.0f;
            panel.transform.LookAt(cam.transform.position - v); 
            panel.transform.Rotate(0,180,0);
    }

}
