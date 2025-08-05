using UnityEngine;

public class TutorialRouteCollider: MonoBehaviour
{
    [SerializeField] TutorialManager manager;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(this.name == "route1")
            {
                manager.Tutorial_Clear(1);
            }
            if(this.name == "route2")
            {
                manager.Tutorial_Clear(2);
            }
            if(this.name == "route3")
            {
                manager.Tutorial_Clear(3);
            }
            if (this.name == "route4")
            {
                manager.Tutorial_Clear(4);
            }
            if (this.name == "route5")
            {
                manager.Tutorial_Clear(5);
            }
        }
    }
}
