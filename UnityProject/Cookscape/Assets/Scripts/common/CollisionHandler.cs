using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        switch(other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("This thing is Friendly");
                break;
            case "Hostile":
                Debug.Log("This thing is Hostile");
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                break;
            case "MakeRoom":
                Debug.Log("방만들기");
                break;
            //default:
            //    Debug.Log("other something");
            //    break;
        }
    }
}
