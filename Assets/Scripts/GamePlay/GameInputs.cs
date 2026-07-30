using System;
using Core.DI;
using Game;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GamePlay
{
	public class GameInputs : MonoBehaviour
	{
		public Vector2 Move { private set; get; }
		public Vector2 Look { private set; get; }
		public bool Jump { set; get; }
		public bool Sprint { private set; get; }

		public bool AnalogMovement { private set; get; }

		[Header("Mouse Cursor Settings")]
		[SerializeField] private bool _cursorLocked = true;
		[SerializeField] private bool _cursorInputForLook = true;

		private IGameProgress _gameProgress;
		
		private void Awake()
		{
			_gameProgress = ServiceLocator.Container.Resolve<IGameProgress>();
		}

		private bool AreInputsLocked()
		{
			return !_gameProgress.IsLevelActive;
		}
		
		public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if (_cursorInputForLook)
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

		public void MoveInput(Vector2 newMoveDirection)
		{
			if(AreInputsLocked()) return;
			Move = newMoveDirection;
		}

		public void LookInput(Vector2 newLookDirection)
		{
			if(AreInputsLocked()) return;
			Look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			if(AreInputsLocked()) return;
			Jump = newJumpState;
		}

		public void SprintInput(bool newSprintState)
		{
			if(AreInputsLocked()) return;
			Sprint = newSprintState;
		}

		private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(_cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}
	}
}