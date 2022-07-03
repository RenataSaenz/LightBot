using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using IA2;

//IA2-P1
    public class BallUserControl : MonoBehaviour
    { 
        public enum PlayerInputs { MOVE, JUMP, IDLE, Restart}
        private EventFSM<PlayerInputs> _myFsm;

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
            var restart = new State<PlayerInputs>("DIE");
            
            //creo las transiciones
            StateConfigurer.Create(idle)
                .SetTransition(PlayerInputs.MOVE, moving)
                .SetTransition(PlayerInputs.JUMP, jumping)
                .SetTransition(PlayerInputs.Restart, restart)
                .Done(); //aplico y asigno

            StateConfigurer.Create(moving)
                .SetTransition(PlayerInputs.IDLE, idle)
                .SetTransition(PlayerInputs.JUMP, jumping)
                .SetTransition(PlayerInputs.Restart, restart)
                .Done();

            StateConfigurer.Create(jumping)
                .SetTransition(PlayerInputs.IDLE, idle)
                .SetTransition(PlayerInputs.MOVE, moving)
                .SetTransition(PlayerInputs.JUMP, jumping)
                .SetTransition(PlayerInputs.Restart, restart)
                .Done();
            
            StateConfigurer.Create(restart)
                .SetTransition(PlayerInputs.IDLE, idle)
                .SetTransition(PlayerInputs.MOVE, moving)
                .SetTransition(PlayerInputs.JUMP, jumping)
                .Done();

            idle.OnEnter += x =>
            {
            };
            
            idle.OnUpdate += () => 
            {
                
                if (h != 0 || v != 0)
                    SendInputToFSM(PlayerInputs.MOVE);
                
                
                if (jump)
                    SendInputToFSM(PlayerInputs.JUMP);
            };
            
            restart.OnEnter += x =>
            {
                SoundManager.instance.Play(SoundManager.Types.Restart);
            };
            
            restart.OnUpdate += () => 
            {
                
                if (h != 0 || v != 0)
                    SendInputToFSM(PlayerInputs.MOVE);
                
                
                if (jump)
                    SendInputToFSM(PlayerInputs.JUMP);
            };
            
            //MOVING
            moving.OnEnter += x =>
            {
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
            };
            
            //JUMPING
            jumping.OnEnter += x => 
            {
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

        public void RestartState()
        {
            SendInputToFSM(PlayerInputs.Restart);
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
            _myFsm.FixedUpdate();
        }
    }

