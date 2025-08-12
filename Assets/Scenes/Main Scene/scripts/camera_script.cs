using UnityEngine;
using System.IO;
using System.Collections.Generic;

namespace Ashsvp
{
    public class ghost              //class for ghost car states list
    {
    public Vector3 position;
    public Quaternion rotation;
    }
    public class camera_script : MonoBehaviour
    {
        private Transform main_car;                                         //reference main car
        private SimcadeVehicleController main_car_controller;               //car script

        private Transform look_point;                                       //point to look at(must be parent)
        private Animator idle_animation;                                    //spin while start animation
        private Canvas ui;                                                  //"press start" ui

        private bool follow_car = false;                                    //if true, camera follows main car

        public float mouse_sen = 100f;                                      //sensetivity

        public List<ghost> ghost_car_transform_frames = new List<ghost>();  //frames for the ghost car path

        public int lap = 0;                                                 //canges when on trigger enter
        private int current_lap = 0;                                        //value to detect if lap has changed

        int iframes_iterator = 0;                                           //value to run through car states list

        public GameObject ghost_car_obj;                                    //ghost car object

        void Start()
        {
            main_car = GameObject.Find("main_car").transform;               //find main car in scene
            ghost_car_obj = GameObject.Find("ghost");
            main_car_controller = main_car.GetComponent<SimcadeVehicleController>();//loading main car script

            look_point = transform.parent;                                  //find point for camera to look when the game is paused
            idle_animation = look_point.GetComponent<Animator>();           //animation of spining camera
            ui = transform.GetChild(0).GetComponent<Canvas>();              //ui text

            UnityEngine.Cursor.lockState = CursorLockMode.Locked;           //lock cursor
            Cursor.visible = false;                                         //hide cursor
        }

        void Update()
        {
            if (!follow_car)                        //if game is paused
            {
                if (Input.GetKey(KeyCode.Space))    //start game when space is pressed
                {
                    Start_Game();
                }
            }
            else                                    //if game is running
            {
                look_point.position = main_car.position;//follow car with camera

                Look_with_Mouse();                  //add mouse input
            }

            if (lap != current_lap)                 //detect lap change
            {
                current_lap = lap;                 
                Restart_Game();                     //restart if lap completed
            }
        }

        void FixedUpdate()
        {
            switch (current_lap)                    //ghost car controll
            {
                case 0:
                    Write_Frame();                  //remember path if first lap
                    break;
                case 1:
                    Ghost_Car_Ride();               //start ride if second lap
                    break;
                case 2:
                    Application.Quit();             //end game when ride is completed
                    break;
            }
        }

        void Write_Frame(){                                                         //func to remmber main car position in every frame
                ghost ghost_car_current_transform = new ghost();                    //create empty frame
                ghost_car_current_transform.position = main_car.transform.position; //add position data to the frame
                ghost_car_current_transform.rotation = main_car.transform.rotation; //add rotation data to the frame

                ghost_car_transform_frames.Add(ghost_car_current_transform);        //add frames to the list with frames
        }

        void Ghost_Car_Ride()                                                       //func to move ghost car
            {
                if (iframes_iterator < ghost_car_transform_frames.Count)            //catch out of bound  
                {
                    ghost_car_obj.transform.position = ghost_car_transform_frames[iframes_iterator].position;  //move ghost
                    ghost_car_obj.transform.rotation = ghost_car_transform_frames[iframes_iterator].rotation;  //rotate ghost
                }
                iframes_iterator += 1;                                              //go to the next frame
        }

        void Start_Game() {                                                         //when space is presse
            Debug.Log("Start");
            ui.enabled = false;                                                     //hide ui
            idle_animation.enabled = false;                                         //end camera spin
            main_car_controller.CanDrive = true;                                    //unlock car movement
            main_car_controller.CanAccelerate = true;                               
            look_point.rotation = Quaternion.Euler(0, 0, 0);                        //restart camera position
            follow_car = true;                                                      //start to follow car with camera
        }

        void Restart_Game()                                                         //when lap completed but space is not pressed yet
        {
            Debug.Log("Restart");       
            ui.enabled = true;                                                      
            idle_animation.enabled = true;                                          //start to spin camera
            main_car_controller.CanDrive = false;                                   //stop car movement
            main_car_controller.CanAccelerate = false;
            follow_car = false;                                                     //unlock camera from car
            //for (int i = 0; i < ghosts.Count; i ++)
            //ghost_car_obj.GetComponent<ghost_script>().start = true;
        }

        void Look_with_Mouse()                                                      //mouse input
        {
            float mouseX = Input.GetAxis("Mouse X");
            //float mouseY = Input.GetAxis("Mouse Y");

            look_point.transform.Rotate(Vector3.up, mouseX * mouse_sen * Time.deltaTime);
            //look_point.transform.Rotate(Vector3.right, -mouseY * mouse_sen * Time.deltaTime);
        }
    }
}