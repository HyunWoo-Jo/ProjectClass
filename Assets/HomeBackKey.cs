using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeBackKey : MonoBehaviour
{
    int count = 0;
    float timer = 0;
    private void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            count++;
        }
        if(count != 0) {
            timer += Time.deltaTime;
            if(timer > 0.7f) {
                count = 0;
                timer = 0;
            }
        }
        if( count == 2) {
            Application.Quit();
        }
    }
}
