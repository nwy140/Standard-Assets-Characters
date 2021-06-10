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
public class StandardRewiredInputWrapper : MonoBehaviour // Rewired Wrapper For StandardAsset's Character
{
    public int playerId;
    private Player player;

    private CharacterInput _charInput;

    // An Enum to store and access button names
    public enum Btns
    {
        #region Rewired Input Actions

        #region 0 : Default

        MoveHorizontal,
        MoveVertical,
        Empty,

        #endregion 0 : Default

        #region 1 : Motion

        Evade,
        Jump,
        Run,

        #endregion 1 : Motion

        Interact,
        Attack,

        #region 2 : Activity

        #endregion 2 : Activity

        #region 3 : Techniques

        #endregion 3 : Techniques

        #region 2 : Assist

        Pause,

        #endregion 2 : Assist

        #region 5 : Camera

        LockOn,
        LockOnCycle,
        CameraX,
        CameraY,

        #endregion 5 : Camera

        #endregion Rewired Input Actions
    }


    // Comp Refs
    private void Awake()
    {
        _charInput = GetComponent<CharacterInput>();
    }

    private void OnEnable()
    {
        SubscribeInputEvents();
    }

    void SubscribeInputEvents()
    {
        player = ReInput.players.GetPlayer(playerId);

        // SInt DONE:  // replaced context parameter from Unity New Input System with Rewired InputActionEventData
        // Rewired's Workflow is better than Unity's new input system.

        // Add delegates to receive input events from the Player

        // This event will be called every frame any input is updated
        player.AddInputEventDelegate(OnInputUpdate, UpdateLoopType.Update);

        #region Rewired Rewired Input Event Bindings

        #region 0 : Default

        // MoveHorizontal,
        // This event will be called every frame the "Move Horizontal" axis is non-zero and once more when it returns to zero.
        player.AddInputEventDelegate(_charInput.OnMoveX, UpdateLoopType.Update, InputActionEventType.AxisActive, 
            nameof(Btns.MoveHorizontal));
        
        // MoveVertical,
        // This event will be called every frame the "Move Horizontal" axis is non-zero and once more when it returns to zero.
        // player.AddInputEventDelegate(_charInput.OnMoveY, UpdateLoopType.Update, InputActionEventType.AxisActiveOrJustInactive, 
        //     nameof(Btns.MoveVertical));
        // Empty,
        // This event will be called when the "Empty" button is held for at least 1 seconds and then released
        player.AddInputEventDelegate(EmptyMethodLog, UpdateLoopType.Update, InputActionEventType.ButtonPressedForTimeJustReleased,   
            nameof(Btns.Empty), new object[] {1.0f});

        #endregion 0 : Default

        #region 1 : Motion

        // Evade,
        // Jump,
        // Run,

        #endregion 1 : Motion

        // Interact,
        // Attack,

        #region 2 : Activity
        // This event will be called every frame the "Attack" action is updated
        player.AddInputEventDelegate(_charInput.OnJump, UpdateLoopType.Update, 
            nameof(Btns.Attack));

        // This event will be called when the "Attack" button is first pressed
        player.AddInputEventDelegate(EmptyMethodStub, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed,
            nameof(Btns.Attack));

        // This event will be called when the "Attack" button is first released
        player.AddInputEventDelegate(EmptyMethodStub, UpdateLoopType.Update, InputActionEventType.ButtonJustReleased,
            nameof(Btns.Attack));
        #endregion 2 : Activity

        #region 3 : Techniques

        #endregion 3 : Techniques

        #region 2 : Assist

        // Pause,

        #endregion 2 : Assist

        #region 5 : Camera

        // LockOn,
        // LockOnCycle,
        // CameraX,
        // CameraY,

        #endregion 5 : Camera

        #endregion Rewired Input Event Bindings
        // The update loop you choose for the event matters. Make sure your chosen update loop is enabled in
        // the Settings page of the Rewired editor or you won't receive any events.
    }

    #region Rewired Input Update Bindings

    void OnInputUpdate(InputActionEventData data)
    {
        // You can't use ToString() in switch case so we'll have to use nameof instead. Read: https://stackoverflow.com/questions/1273228/how-can-i-use-the-string-value-of-a-c-sharp-enum-value-in-a-case-statement
        // nameof doesn't produce garbage Read: https://stackoverflow.com/questions/35523172/what-is-the-difference-between-myenum-item-tostring-and-nameofmyenum-item
        switch (data.actionName)
        {
            // determine which action this is
            case nameof(Btns.MoveHorizontal):
                if (data.GetAxis() != 0.0f) Debug.Log("Move Horizontal!" + data.GetAxis());
                break;
            case nameof(Btns.Attack):
                if (data.GetButtonDown()) Debug.Log("Fire!");
                break;
            case nameof(Btns.Jump):
                if (data.GetButtonDown()) Debug.Log("Jumpa!");
                break;
        }
    }

    public void EmptyMethodStub(InputActionEventData data)
    {
        // do nothing
    }

    public void EmptyMethodLog(InputActionEventData data)
    {
        Debug.Log("Action :" + data.actionName + "Held Time: " + data.GetButtonPrev());
    }

    #endregion Rewired Input Update Bindings

    #region Rewired Events Bindings

    // Empty for now

    #endregion Rewired Events Bindings

    void UnsubscribeInputEvents()
    {
        player.RemoveInputEventDelegate(OnInputUpdate);
    }

    private void OnDisable()
    {
        UnsubscribeInputEvents();
    }
}

#region Rewired Example Events Template

// void SubscribeInputEvents()
// {
//     player = ReInput.players.GetPlayer(playerId);
//
//     // SInt DONE:  // replaced context parameter from Unity New Input System with Rewired InputActionEventData
//     // Rewired's Workflow is better than Unity's new input system.
//         
//     // Add delegates to receive input events from the Player
//
//     // This event will be called every frame any input is updated
//     player.AddInputEventDelegate(OnInputUpdate, UpdateLoopType.Update);
//
//     // // This event will be called every frame the "Fire" action is updated
//     // player.AddInputEventDelegate(OnAttackUpdate, UpdateLoopType.Update, "Attack");
//     //
//     // // This event will be called when the "Fire" button is first pressed
//     // player.AddInputEventDelegate(OnAttackButtonDown, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed,
//     //     "Attack");
//     //
//     // // This event will be called when the "Fire" button is first released
//     // player.AddInputEventDelegate(OnAttackButtonUp, UpdateLoopType.Update, InputActionEventType.ButtonJustReleased,
//     //     "Attack");
//     //
//     // // This event will be called every frame the "Move Horizontal" axis is non-zero and once more when it returns to zero.
//     // player.AddInputEventDelegate(OnMoveHorizontal, UpdateLoopType.Update,
//     //     InputActionEventType.AxisActiveOrJustInactive, "Move Horizontal");
//
//     // This event will be called when the "Jump" button is held for at least 2 seconds and then released
//     player.AddInputEventDelegate(_charInput.OnJump, UpdateLoopType.Update,
//         InputActionEventType.ButtonPressedForTimeJustReleased, "Jump", new object[] {2.0f});
//
//     // The update loop you choose for the event matters. Make sure your chosen update loop is enabled in
//     // the Settings page of the Rewired editor or you won't receive any events.
// }
// void OnAttackUpdate(InputActionEventData data)
// {
//     if (data.GetButtonDown()) Debug.Log("Attack Action Data Updated!");
// }
//
// void OnAttackButtonDown(InputActionEventData data)
// {
//     Debug.Log("Attack Pressed");
// }
//
// void OnAttackButtonUp(InputActionEventData data)
// {
//     Debug.Log("Attack Released!");
// }
//
// void OnJumpButtonUp(InputActionEventData data)
// {
//     Debug.Log("Jump Held for 2 seconds and released!");
// }
//
// void OnMoveHorizontal(InputActionEventData data)
// {
//     Debug.Log("Move Horizontal: " + data.GetAxis());
// }
// void UnsubscribeInputEvents()
// {
//     // Unsubscribe from events when object is destroyed
//     player.RemoveInputEventDelegate(OnInputUpdate);
//     player.RemoveInputEventDelegate(OnAttackUpdate);
//     player.RemoveInputEventDelegate(OnAttackButtonDown);
//     player.RemoveInputEventDelegate(OnAttackButtonUp);
//     player.RemoveInputEventDelegate(OnMoveHorizontal);
//     player.RemoveInputEventDelegate(OnJumpButtonUp);
// }

#endregion Rewired Example Events Template