using UnityEngine;
using System.Collections;

public class DestructiblePlatform : MonoBehaviour
{
    public float destructionDelay = 2.1f;
    private bool isPlayerInContact = false;
    // public GameObject playerObject;

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Player") && !isPlayerInContact) {
            isPlayerInContact = true;
            StartCoroutine(Destroy());
        }
    }

    private IEnumerator Destroy() {
        yield return new WaitForSeconds(destructionDelay);
        Destroy(gameObject);
    }
}
