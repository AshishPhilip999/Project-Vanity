using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.Rendering.HighDefinition;

public class Player_Controls : MonoBehaviourPunCallbacks , IPunObservable
{
    public float angle;
    public float speedSenstivity;

    public Vector3 localpos;

    public PhotonView PlayerView;
    public Camera PlayerCamera;

    public int CurrLightnumber;

    public Light_System LightSystem;
    public Movable_Object_System MovablesSystem;

    public Transform RefPoint;
    
    public bool IsControllable = true;
    public bool Crouch;
    public bool prone;

    public GameObject Culler;
    public GameObject LightCuller;

    void Start()
    {
        PlayerView = GetComponent<PhotonView>();

        LightSystem = GameObject.FindGameObjectWithTag("LightSystem").GetComponent<Light_System>();
        MovablesSystem = GameObject.FindGameObjectWithTag("Movable System").GetComponent<Movable_Object_System>();

        if(!PlayerView.IsMine)
        {
            Destroy(Culler);
            Destroy(LightCuller);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerView.IsMine)
        {
           if(IsControllable)
            {
                RaycastHit hit;
                Ray ray = PlayerCamera.ScreenPointToRay(Input.mousePosition);

                // Light Controls
                if (Input.GetMouseButtonDown(0))
                {
                    if (Physics.Raycast(ray, out hit, 1f))
                    {
                        // Light Controls
                       if(hit.collider.tag == "Light")
                        {
                           ServerCallLightSystem(LightSystem.SingleParse(hit.collider.name));

                        }else if(hit.collider.tag == "Roof Light Switch")
                        {
                            ServerCallRoofLights( LightSystem.SingleParse(hit.collider.name) );
                        }

                    }
                }

                // Movable Controls
                if(Input.GetMouseButton(0))
                {
                    float MouseX = Input.GetAxis("Mouse X");
                    float MouseY = Input.GetAxis("Mouse Y");
                    Vector3 MoveAxis;

                    if (Physics.Raycast(ray , out hit , 5f))
                    {
                        // Drawers
                        if(hit.collider.tag == "Movable")
                        {
                            GameObject currObject = hit.collider.gameObject;
                            int Index = Movable_Object_System.SingleParse(currObject.name);
                            Vector3 Direction = currObject.transform.position - transform.position;

                            angle = Vector3.Angle(currObject.transform.up , Direction);

                            if (angle >= 0 && angle <= 45)
                            {
                                MoveAxis = new Vector3(MouseX, 0, 0);
                                ServerCallMovablesSystem(Index , MoveAxis , -0.5f, -0.13f, "drawer");

                            }
                            else if(angle >= 45 && angle <= 107)
                            {                                
                                MoveAxis = new Vector3(MouseY, 0, 0);
                                ServerCallMovablesSystem(Index, MoveAxis, -0.5f, -0.13f, "drawer");

                            }
                            else if (angle >= 107 && angle <= 180 )
                            {
                                 MoveAxis = new Vector3(-MouseX, 0, 0);
                                ServerCallMovablesSystem(Index, MoveAxis, -0.5f, -0.13f, "drawer");
                            }

                        }

                        //Doors
                        if (hit.collider.tag == "Door")
                        {
                            GameObject currObject = hit.collider.gameObject;
                            int Index = Movable_Object_System.SingleParse(currObject.name);
                            Vector3 Direction = currObject.transform.position - transform.position;
                            int sign = (Direction.z >= 0) ? -1 : 1;

                            angle = Vector3.Angle(currObject.transform.right, Direction) * sign;

                            if (angle >= 0 && angle <= 30)
                            {
                                ServerCallDoorSystem(Index, MouseX);

                            }
                            else if (angle >= 30 && angle <= 50)
                            {
                                MoveAxis = new Vector3(0, MouseY, 0);
                                ServerCallDoorSystem(Index, MouseY);

                            }
                            else if (angle >= 50 && angle <= 180)
                            {
                                MoveAxis = new Vector3(0, -MouseX, 0);
                                ServerCallDoorSystem(Index, -MouseX);
                            }
                            else if (angle <= 0 && angle >= -40)
                            {
                                ServerCallDoorSystem(Index, MouseX);
                            }
                            else if(angle <= -30 && angle >= -50 )
                            {
                                ServerCallDoorSystem(Index, -MouseY);
                            }
                            else if (angle <= -50 && angle >= -180)
                            {
                                MoveAxis = new Vector3(0, -MouseX, 0);
                                ServerCallDoorSystem(Index, -MouseX);
                            }

                        }
                    }
                }

            }

           if(Input.GetKeyDown("c"))
            {
                if(!Crouch)
                {
                    PlayerCamera.transform.localPosition = new Vector3(0,-0.6f, -0.419f);
                    Crouch = true;
                }else
                {
                    PlayerCamera.transform.localPosition = new Vector3(0, 0, -0.419f);
                    Crouch = false;
                }
            }

            if (Input.GetKeyDown("z"))
            {
                if (!prone)
                {
                    PlayerCamera.transform.localPosition = new Vector3(0, -1.35f, -0.419f);
                    prone = true;
                }
                else
                {
                    PlayerCamera.transform.localPosition = new Vector3(0, 0 , -0.419f);
                    prone = false;
                }
            }
        }

    }

    public void ClamObjectPosition(Transform Object ,float Max_Position , float Min_Position)
    {
        if (Object.localPosition.x < Max_Position)
        {
            Object.localPosition = new Vector3(Max_Position, Object.localPosition.y, Object.localPosition.z);
        }
        else if (Object.localPosition.x > Min_Position)
        {
            Object.localPosition = new Vector3(Min_Position, Object.localPosition.y, Object.localPosition.z);
        }
    }

    public void ClamDoorPosition(Transform Object)
    {
        Vector3 Rotation = Object.transform.eulerAngles;
        if(Rotation.y >= 90.0f && Rotation.y <= 220.0f)
        {
            Rotation.y = 90.0f;
            Object.transform.rotation = Quaternion.Euler(Rotation);
        }
        else if(Rotation.y <= 270.0f && Rotation.y > 220.0f)
        {
            Rotation.y = 270.0f;
            Object.transform.rotation = Quaternion.Euler(Rotation);
        }
    }




    // Server Calls ...........................................................

    // Lamps
    public void ServerCallLightSystem(int key)
    {
        PlayerView.RPC("LightOnSystem", RpcTarget.AllBuffered , key );
    }

    // Roof Lights
    public void ServerCallRoofLights(int key)
    {
        PlayerView.RPC("RoofLightOnSystem", RpcTarget.AllBuffered, key);
    }


    public void ServerCallMovablesSystem(int ObjectIndex, Vector3 MoveAxis, float Max_Position, float Min_Position , string function)
    {
       if(function == "drawer")
        {
            PlayerView.RPC("DrawerSystemCall", RpcTarget.AllBuffered, ObjectIndex, MoveAxis, Max_Position, Min_Position);

        }
    }

    public void ServerCallDoorSystem(int ObjectIndex , float MoveAngle )
    {
        PlayerView.RPC("DoorSystemCall" , RpcTarget.AllBuffered , ObjectIndex , MoveAngle);
    }





    // RPC Functions...................................

    [PunRPC]
    void LightOnSystem(int index)
    {
        GameObject[] LightSource = LightSystem.LightSource;
        bool[] LightSourceStatuts = LightSystem.LightSourceStatuts;
        GameObject parent = LightSource[index].transform.parent.gameObject;
        Material DimLight = LightSystem.DimLight;
        Material BrightLight = LightSystem.BrightLight;

        if (LightSourceStatuts[index])
        {
            parent.GetComponent<Renderer>().material = DimLight;
            LightSource[index].GetComponent<Light>().enabled = false;
            LightSourceStatuts[index] = false;

        }
        else
        {
            parent.GetComponent<Renderer>().material = BrightLight;
            LightSource[index].GetComponent<Light>().enabled = true;
            LightSourceStatuts[index] = true;
        }
    }

    [PunRPC]
    void RoofLightOnSystem(int index)
    {
        GameObject[] RoofLightSwitch = LightSystem.RoofLightSwitche;
        GameObject[] RoofLight = LightSystem.RoofLight;
        GameObject LightPrefab = RoofLight[index].transform.GetChild(0).transform.GetChild(0).gameObject;

        bool[] RoofLightStatus = LightSystem.RoofLightSwitchStatus;

        Material RoofDimLight = LightSystem.RoofDimLight;
        Material BrightLight = LightSystem.BrightLight;

        HDAdditionalLightData LightSource = RoofLight[index].GetComponent<HDAdditionalLightData>();

        if (RoofLightStatus[index])
        {          
            LightPrefab.GetComponent<Renderer>().material = RoofDimLight;
            RoofLightSwitch[index].transform.rotation = Quaternion.Euler(new Vector3(-89.98f , 0 , 0));
            RoofLight[index].GetComponent<HDAdditionalLightData>().SetIntensity(0);

            RoofLightStatus[index] = false;

        }else
        {
            LightPrefab.GetComponent<Renderer>().material = BrightLight;
            RoofLightSwitch[index].transform.rotation = Quaternion.Euler(new Vector3(-109.98f, 0, 0));
            RoofLight[index].GetComponent<HDAdditionalLightData>().SetIntensity(400000);

            RoofLightStatus[index] = true;
        }
    }

    [PunRPC]
    void DrawerSystemCall(int ObjectIndex , Vector3 MoveAxis , float Max_Position , float Min_Position)
    {
        GameObject[] Movables = MovablesSystem.Movables;

        Movables[ObjectIndex].transform.localPosition += MoveAxis * Time.deltaTime * speedSenstivity;
        ClamObjectPosition(Movables[ObjectIndex].transform , Max_Position , Min_Position);
    }

    [PunRPC]
    void DoorSystemCall(int ObjectIndex , float MoveAngle)
    {
        GameObject[] Door = MovablesSystem.Doors;
        Vector3 Rotation = Door[ObjectIndex].transform.eulerAngles;
        Rotation.y -= MoveAngle * (Time.deltaTime + 5.0f);
        Door[ObjectIndex].transform.rotation = Quaternion.Euler(Rotation);
       ClamDoorPosition(Door[ObjectIndex].transform);
    }

    

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
       
    }
}
