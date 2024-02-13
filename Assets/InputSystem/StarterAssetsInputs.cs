using System;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;
		public bool mark;
		public bool teleport;
		public bool teleportTaken;
		public bool getDown;
		public bool pauseMenu;

		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;

#if ENABLE_INPUT_SYSTEM
		public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if(cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
		}

		public void OnJump(InputValue value)
		{
			JumpInput(value.isPressed);
		}

		public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);
		}

		public void OnMark(InputValue value)
		{
			MarkInput(value.isPressed);
		}

		public void OnTeleport(InputValue value)
		{
			TeleportInput(value.isPressed);
		}

        private void TeleportInput(bool NewTeleportState)
        {
            teleport = NewTeleportState;
        }

        private void MarkInput(bool newMarkState)
        {
			mark = newMarkState;
        }
		public void OnGetDown(InputValue value)
		{
			GetDownInput(value.isPressed);
		}
		public void OnPauseMenu(InputValue value)
		{
			PauseMenuInput(value.isPressed);
		}

        public void OnTPtakenMark(InputValue value)
		{
			TPtakenMarkInput(value.isPressed);
		}

        private void TPtakenMarkInput(bool newTPtakenState)
        {
            teleportTaken = newTPtakenState;
        }

#endif


        public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		} 

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}
		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}
        public void GetDownInput(bool newDownState)
        {
            getDown = newDownState;
        }
        public void PauseMenuInput(bool newPauseState)
        {
            pauseMenu = newPauseState;
        }

        private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
			
		}
	}
	
}