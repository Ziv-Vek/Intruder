using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelExitPortal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player")
        {
            FindObjectOfType<GameSession>().LoadNextLevel();
        }
    }
    
}
