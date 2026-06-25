using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSkybox : MonoBehaviour
{
        public Material skyBubulak;
        public Material skyBus;

        public void setSkyBubulak(){
            RenderSettings.skybox = skyBubulak;
        }

        public void setSkyBus(){
            RenderSettings.skybox = skyBus;
        }
}
