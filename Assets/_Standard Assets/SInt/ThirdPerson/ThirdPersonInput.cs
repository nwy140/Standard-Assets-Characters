using System;
using StandardAssets.Characters.Common;
using UnityEngine;
using UnityEngine.InputSystem;


namespace StandardAssets.Characters.ThirdPerson
{
	/// <summary>
	/// Implementation of the Third Person input
	/// </summary>
	public class ThirdPersonInput : CharacterInput, IThirdPersonInput
	{
		/// <summary>
		/// Fired when strafe input is started
		/// </summary>
		public event Action strafeStarted;

		/// <summary>
		/// Fired when the strafe input is ended
		/// </summary>
		public event Action strafeEnded;
		
		/// <summary>
		/// Fired when the recentre camera input is applied
		/// </summary>
		public event Action recentreCamera;

		// Tracks if the character is strafing 
		bool m_IsStrafing;
		
		// Handles the recentre input 
		public override void OnRecentre()
		{
			if (recentreCamera != null)
			{
				recentreCamera();
			}
		}

		// Handles the strafe input
		public override void  OnStrafe()
		{
			// if (context.performed)
			{
				BroadcastInputAction(ref m_IsStrafing, strafeStarted, strafeEnded);
			}
		}
	}
}