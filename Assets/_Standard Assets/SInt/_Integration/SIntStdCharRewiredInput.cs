using System;
using System.Collections;
using System.Collections.Generic;
using Rewired;
using StandardAssets.Characters.Common;
using UnityEngine;

// Read: https://guavaman.com/projects/rewired/docs/HowTos.html Method 2 section
// https://guavaman.com/projects/rewired/docs/api-reference/html/T_Rewired_InputActionEventType.htm
// https://forum.unity.com/threads/rewired-advanced-input-for-unity.270693/page-19
// Rewired also supports gamepad vibration: https://guavaman.com/projects/rewired/docs/FAQ.html#force-feedback
// InputActionEventType is an enum with all sort of properties i.e ButtonJustDoublePressed	
public class SIntStdCharRewiredInput : MonoBehaviour
{
    public int playerId;
    private Player player;

    private CharacterInput _charInput;

    // Comp Refs
    private void Awake()
    {
        _charInput.GetComponent<CharacterInput>();
    }

    void OnEnable()
    {
        player = ReInput.players.GetPlayer(playerId);

        // Add delegates to receive input events from the Player

        // This event will be called every frame any input is updated
        player.AddInputEventDelegate(OnInputUpdate, UpdateLoopType.Update);

        // This event will be called every frame the "Fire" action is updated
        player.AddInputEventDelegate(OnAttackUpdate, UpdateLoopType.Update, "Attack");

        // This event will be called when the "Fire" button is first pressed
        player.AddInputEventDelegate(OnAttackButtonDown, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed,
            "Attack");

        // This event will be called when the "Fire" button is first released
        player.AddInputEventDelegate(OnAttackButtonUp, UpdateLoopType.Update, InputActionEventType.ButtonJustReleased,
            "Attack");

        // This event will be called every frame the "Move Horizontal" axis is non-zero and once more when it returns to zero.
        player.AddInputEventDelegate(OnMoveHorizontal, UpdateLoopType.Update,
            InputActionEventType.AxisActiveOrJustInactive, "Move Horizontal");

        // This event will be called when the "Jump" button is held for at least 2 seconds and then released
        player.AddInputEventDelegate(OnJumpButtonUp, UpdateLoopType.Update,
            InputActionEventType.ButtonPressedForTimeJustReleased, "Jump", new object[] {2.0f});

        // The update loop you choose for the event matters. Make sure your chosen update loop is enabled in
        // the Settings page of the Rewired editor or you won't receive any events.
    }
    #region Rewired Input Update Bindings
    void OnInputUpdate(InputActionEventData data)
    {
        switch (data.actionName)
        {
            // determine which action this is
            case "Move Horizontal":
                if (data.GetAxis() != 0.0f) Debug.Log("Move Horizontal!");
                break;
            case "Attack":
                if (data.GetButtonDown()) Debug.Log("Fire!");
                break;
            case "Jump":
                if (data.GetButtonDown()) Debug.Log("Jumpa!");
                break;
        }
    }

    #endregion Rewired Input Update Bindings

    #region Rewired Events Bindings
    #region Rewired Example Events Template
    void OnAttackUpdate(InputActionEventData data)
    {
        if (data.GetButtonDown()) Debug.Log("Attack Action Data Updated!");
    }

    void OnAttackButtonDown(InputActionEventData data)
    {
        Debug.Log("Attack Pressed");
    }

    void OnAttackButtonUp(InputActionEventData data)
    {
        Debug.Log("Attack Released!");
    }

    void OnJumpButtonUp(InputActionEventData data)
    {
        Debug.Log("Jump Held for 2 seconds and released!");
    }

    void OnMoveHorizontal(InputActionEventData data)
    {
        Debug.Log("Move Horizontal: " + data.GetAxis());
    }
    #endregion Rewired Example Events Template
    #endregion Rewired Events Bindings

    private void OnDisable()
    {
        // Unsubscribe from events when object is destroyed
        player.RemoveInputEventDelegate(OnInputUpdate);
        player.RemoveInputEventDelegate(OnAttackUpdate);
        player.RemoveInputEventDelegate(OnAttackButtonDown);
        player.RemoveInputEventDelegate(OnAttackButtonUp);
        player.RemoveInputEventDelegate(OnMoveHorizontal);
        player.RemoveInputEventDelegate(OnJumpButtonUp);
    }
}