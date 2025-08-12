using UnityEngine;
using UnityEngine.UI;
namespace Ashsvp
{
    public class lapui : MonoBehaviour
    {
        private GameObject camera;

        void Start()
        {
             camera = GameObject.Find("Camera");
        }


        void Update()
        {
            int lap = camera.GetComponent<camera_script>().lap;
            GetComponent<Text>().text = "current lap - " + (lap+1).ToString();
        }
    }
}