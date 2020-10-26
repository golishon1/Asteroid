using UnityEngine;

public class ObjectPassed : MonoBehaviour
{
    [SerializeField] private float addScore = 5f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameController.instance.asteroidPassed++;
            GameController.instance.score += addScore;
            GameController.instance.audioManager.PlaySound("Pass");
            Destroy(this);
        }
    }
}
