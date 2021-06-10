using System;
using System.Collections;
using System.Collections.Generic;
using StandardAssets.Characters.Common;
using UnityEngine;
 
    public class SIntRewiredInputEventsWrapper : MonoBehaviour
    {
        private CharacterInput _characterInput;

        private void Awake()
        {
            _characterInput = GetComponent<CharacterInput>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _characterInput.OnJump();
            }
        }
    }
     