using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Application_Manager : MonoBehaviour
{
    public void ExitGame(){
        #if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
    	#endif
    }
}
