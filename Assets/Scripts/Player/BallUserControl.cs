using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using IA2;

    public class BallUserControl : MonoBehaviour
    {
        
        public enum PlayerInputs { MOVE, JUMP, IDLE, DIE}
        private EventFSM<PlayerInputs> _myFsm;
      //  private Rigidbody _myRb;
      //  public Renderer _myRen;

        
        private Ball ball; 

        private Vector3 move;

        private Transform cam; 
        private Vector3 camForward; 
        private bool jump;
        private float h;
        private float v;


        private void Awake()
        {
            ball = GetComponent<Ball>();


            if (Camera.main != null)
            {
                cam = Camera.main.transform;
            }
            else
            {
                Debug.LogWarning(
                    "Warning: no main camera found. Ball needs a Camera tagged \"MainCamera\", for camera-relative controls."); 
            }
            
            var idle = new State<PlayerInputs>("IDLE");
            var moving = new State<PlayerInputs>("Moving");
            var jumping = new State<PlayerInputs>("Jumping");
            var die = new State<PlayerInputs>("DIE");
            
            //creo las transiciones
            StateConfigurer.Create(idle)
                .SetTransition(PlayerInputs.MOVE, moving)
                .SetTransition(PlayerInputs.JUMP, jumping)
                .SetTransition(PlayerInputs.DIE, die)
                .Done(); //aplico y asigno

            StateConfigurer.Create(moving)
                .SetTransition(PlayerInputs.IDLE, idle)
                .SetTransition(PlayerInputs.JUMP, jumping)
                .SetTransition(PlayerInputs.DIE, die)
                .Done();

            StateConfigurer.Create(jumping)
                .SetTransition(PlayerInputs.IDLE, idle)
                .SetTransition(PlayerInputs.MOVE, moving)
                .SetTransition(PlayerInputs.JUMP, jumping)
                .SetTransition(PlayerInputs.DIE, die)
                .Done();

            //die no va a tener ninguna transición HACIA nada (uno puede morirse, pero no puede pasar de morirse a caminar)
            //entonces solo lo creo e inmediatamente lo aplico asi el diccionario de transiciones no es nulo y no se rompe nada.
            StateConfigurer.Create(die).Done();

            idle.OnEnter += x =>
            {
                Debug.Log("Rama estas tarde");
            };
            
            idle.OnUpdate += () => 
            {
                
                if (h != 0 || v != 0)
                    SendInputToFSM(PlayerInputs.MOVE);
                
                
                if (jump)
                    SendInputToFSM(PlayerInputs.JUMP);
            };
            
            //MOVING
            moving.OnEnter += x =>
            {
              //  _myRen.material.color = Color.blue;
            };
            moving.OnUpdate += () => 
            {
                if ( h == 0 && v == 0)
                    SendInputToFSM(PlayerInputs.IDLE);
                
                 if (jump)
                    SendInputToFSM(PlayerInputs.JUMP);
                 
                if (cam != null)
                {
                    camForward = Vector3.Scale(cam.forward, new Vector3(1, 0, 1)).normalized;
                    move = (v*camForward + h*cam.right).normalized;
                }
                else
                {
                    move = (v*Vector3.forward + h*Vector3.right).normalized;
                }
                
            };
            moving.OnFixedUpdate += () => 
            {
                ball.Move(move);
            };
            moving.OnExit += x => 
            {
                //x es el input que recibí, por lo que puedo modificar el comportamiento según a donde estoy llendo
               // if(x != PlayerInputs.JUMP)
                 //   _myRb.velocity = Vector3.zero;
            };
            
            //JUMPING
            jumping.OnEnter += x => 
            {
              //  jump = CrossPlatformInputManager.GetButton("Jump");
                Debug.Log("jumping");
                ball.Jump();
            };
            jumping.OnUpdate += () =>
            {
                if (h != 0 || v != 0)
                    SendInputToFSM(PlayerInputs.MOVE);
                if (h == 0 && v == 0)
                    SendInputToFSM(PlayerInputs.IDLE);
            };
            
            _myFsm = new EventFSM<PlayerInputs>(idle);
        }

        private void Start()
        {
            SendInputToFSM(PlayerInputs.IDLE);
        }
        
        private void SendInputToFSM(PlayerInputs inp)
        {
            _myFsm.SendInput(inp);
        }  
        private void Update()
        {
            h = CrossPlatformInputManager.GetAxis("Horizontal");
            v = CrossPlatformInputManager.GetAxis("Vertical");
            jump = CrossPlatformInputManager.GetButtonDown("Jump");
            
            _myFsm.Update();

        }
        
        private void FixedUpdate()
        {
            // ball.Move(move, jump);
            _myFsm.FixedUpdate();
            //jump = false;
            Debug.Log(jump);
        }
    }

