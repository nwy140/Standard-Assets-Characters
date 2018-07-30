﻿using StandardAssets.Characters.CharacterInput;
using StandardAssets.Characters.Effects;
using StandardAssets.Characters.Physics;
using UnityEngine;

namespace StandardAssets.Characters.Common
{
	/// <summary>
	/// Abstract bass class for character brains
	/// </summary>
	[RequireComponent(typeof(ICharacterPhysics))]
	[RequireComponent(typeof(ICharacterInput))]
	public abstract class CharacterBrain : MonoBehaviour
	{
		/// <summary>
		/// The Physic implementation used to do the movement
		/// e.g. CharacterController or Rigidbody (or New C# CharacterController analog)
		/// </summary>
		protected ICharacterPhysics characterPhysics;

		/// <summary>
		/// The Input implementation to be used
		/// e.g. Default unity input or (in future) the new new input system
		/// </summary>
		protected ICharacterInput characterInput;

		public ICharacterPhysics physicsForCharacter
		{
			get { return characterPhysics; }
		}

		public ICharacterInput inputForCharacter
		{
			get { return characterInput; }
		}

		public abstract MovementEventHandler movementEventHandler { get; }

		/// <summary>
		/// Get physics and input on Awake
		/// </summary>
		protected virtual void Awake()
		{
			characterPhysics = GetComponent<ICharacterPhysics>();
			characterInput = GetComponent<ICharacterInput>();
		}
	}
}