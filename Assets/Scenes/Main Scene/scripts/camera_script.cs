using UnityEngine;
namespace Ashsvp
{
    public class camera_script : MonoBehaviour
    {
        private Transform main_car;    //reference main car
        private SimcadeVehicleController main_car_controller; //car script

        private Transform look_point;   //point to look at(must be parent)
        private Animator idle_animation; //spin while start animation

        private bool follow_car = false;

        public float mouse_sen = 100f;

        void Start()
        {
            main_car = GameObject.Find("main_car").transform;
            main_car_controller = main_car.GetComponent<SimcadeVehicleController>();

            look_point = transform.parent;
            idle_animation = look_point.GetComponent<Animator>();

            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false; 
        }

        void Update()
        {
            if (!follow_car)
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    idle_animation.enabled = false;
                    main_car_controller.CanDrive = true;
                    look_point.rotation = Quaternion.Euler(0, 0, 0);
                    follow_car = true;
                }
            }
            else
            {
                look_point.position = main_car.position;

                Look_with_Mouse();
            }
        }

        void Look_with_Mouse()
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            look_point.transform.Rotate(Vector3.up, mouseX * mouse_sen * Time.deltaTime);
            look_point.transform.Rotate(Vector3.right, -mouseY * mouse_sen * Time.deltaTime);
        }
    }
}