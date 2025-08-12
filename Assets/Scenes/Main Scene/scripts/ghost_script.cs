using UnityEngine;
using System.IO;
using System.Collections.Generic;
public class ghost
    {
    public Vector3 position;
    public Quaternion rotation;
    }
namespace Ashsvp
{
    //public class ghost_frames
    //{
    //    public List<ghost> ghosts = new List<ghost>();
    //}


    public class ghost_script : MonoBehaviour
    {
        int i = 0;
        public List<ghost> ghosts = new List<ghost>();
        public bool start = false;


        void Start()
        {
            //current_frames = LoadGhosts();
            Debug.Log("Successfully loaded ");
        }
        //void FixedUpdate()
        //{
         //   if (start)
        //   {
        //        transform.position = ghosts[i].position;
        //       transform.rotation = ghosts[i].rotation;
        //        Debug.Log(ghosts[i].position);

                //if (i < current_frames.ghosts.Count)
          //      i++;
            //}
     //   }

    }
}
