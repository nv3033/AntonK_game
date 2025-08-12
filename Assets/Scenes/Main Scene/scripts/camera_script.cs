using UnityEngine;
using System.IO;
using System.Collections.Generic;

namespace Ashsvp
{
    public class ghost
    {
    public Vector3 position;
    public Quaternion rotation;
    }
    public class camera_script : MonoBehaviour
    {
        private Transform main_car;    //reference main car
        private SimcadeVehicleController main_car_controller; //car script

        private Transform look_point;   //point to look at(must be parent)
        private Animator idle_animation; //spin while start animation
        private Canvas ui;

        private bool follow_car = false;

        public float mouse_sen = 100f;

        public List<ghost> ghosts = new List<ghost>();
        private string json;
        private string fileName = "car_json.json";
        private  string filePath;

        public int lap = 0;
        private int current_lap = 0;

        int i = 0; 

        public GameObject ghost_car_obj;

        void Start()
        {
            main_car = GameObject.Find("main_car").transform;
            main_car_controller = main_car.GetComponent<SimcadeVehicleController>();

            look_point = transform.parent;
            idle_animation = look_point.GetComponent<Animator>();
            ui = transform.GetChild(0).GetComponent<Canvas>();

            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            filePath = Path.Combine(Application.dataPath, fileName);
        }

        void Update()
        {
            if (!follow_car)
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    Start_Game();
                }
            }
            else
            {
                look_point.position = main_car.position;

                Look_with_Mouse();
            }

            if (lap != current_lap)
            {
                current_lap = lap;
                Restart_Game();
                //for (int i2= 0; i2 < ghosts.Count; i ++)
                //    Debug.Log(ghosts[i2]);
            }
        }

        void FixedUpdate()
        {
            if (current_lap == 0)
            {
                ghost ghost_car = new ghost();
                ghost_car.position = main_car.transform.position;
                ghost_car.rotation = main_car.transform.rotation;

                ghosts.Add(ghost_car);

            }
            else if (current_lap == 1)
            {
                if (i < ghosts.Count)
                ghost_car_obj.transform.position = ghosts[i].position;
                ghost_car_obj.transform.rotation = ghosts[i].rotation;
                Debug.Log(ghosts[i].position);
                i += 1;

            }
            else if (current_lap == 2)
                Application.Quit();
            //ghost_car_obj.transform.position = main_car.transform.position;
        }

        void Start_Game() {
            Debug.Log("Start");
            ui.enabled = false;
            idle_animation.enabled = false;
            main_car_controller.CanDrive = true;
            main_car_controller.CanAccelerate = true;
            look_point.rotation = Quaternion.Euler(0, 0, 0);
            follow_car = true;
        }

        void Restart_Game()
        {
            Debug.Log("Restart");
            ui.enabled = true;
            idle_animation.enabled = true;
            main_car_controller.CanDrive = false;
            main_car_controller.CanAccelerate = false;
            follow_car = false;
            //for (int i = 0; i < ghosts.Count; i ++)
            //ghost_car_obj.GetComponent<ghost_script>().start = true;
        }

        void Write_Json()
        {
            
            //Debug.Log(ghost_car.position);
            //json += JsonUtility.ToJson(ghost_car);
            //File.WriteAllText(filePath, json);
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