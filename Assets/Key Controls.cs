// GENERATED AUTOMATICALLY FROM 'Assets/Key Controls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @KeyControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @KeyControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Key Controls"",
    ""maps"": [
        {
            ""name"": ""Gameplay"",
            ""id"": ""60e79be3-0a92-4ae7-b4e5-ff1c132054a3"",
            ""actions"": [
                {
                    ""name"": ""MoveUp"",
                    ""type"": ""Button"",
                    ""id"": ""8544fc5a-c37e-4acb-bfcb-bbd48115bff5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MoveRight"",
                    ""type"": ""Button"",
                    ""id"": ""28c0f812-138d-470a-a380-fe1deb1f6e9e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MoveLeft"",
                    ""type"": ""Button"",
                    ""id"": ""347cebe5-d443-46d2-b5ec-4a1a388573d6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MoveDown"",
                    ""type"": ""Button"",
                    ""id"": ""99253f6b-93a6-4b88-b436-480298686cef"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""e092d2c6-679a-4a87-ae27-50cebbe96b30"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""05ce9806-8ee3-4244-a4b2-8f1612ba06ac"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""45dc46a3-5eee-4120-a43c-740c05c6aeb8"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f8174e9b-ce97-4553-b384-18b86e4d659a"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Gameplay
        m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
        m_Gameplay_MoveUp = m_Gameplay.FindAction("MoveUp", throwIfNotFound: true);
        m_Gameplay_MoveRight = m_Gameplay.FindAction("MoveRight", throwIfNotFound: true);
        m_Gameplay_MoveLeft = m_Gameplay.FindAction("MoveLeft", throwIfNotFound: true);
        m_Gameplay_MoveDown = m_Gameplay.FindAction("MoveDown", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Gameplay
    private readonly InputActionMap m_Gameplay;
    private IGameplayActions m_GameplayActionsCallbackInterface;
    private readonly InputAction m_Gameplay_MoveUp;
    private readonly InputAction m_Gameplay_MoveRight;
    private readonly InputAction m_Gameplay_MoveLeft;
    private readonly InputAction m_Gameplay_MoveDown;
    public struct GameplayActions
    {
        private @KeyControls m_Wrapper;
        public GameplayActions(@KeyControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @MoveUp => m_Wrapper.m_Gameplay_MoveUp;
        public InputAction @MoveRight => m_Wrapper.m_Gameplay_MoveRight;
        public InputAction @MoveLeft => m_Wrapper.m_Gameplay_MoveLeft;
        public InputAction @MoveDown => m_Wrapper.m_Gameplay_MoveDown;
        public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void SetCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
            {
                @MoveUp.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMoveUp;
                @MoveUp.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMoveUp;
                @MoveUp.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMoveUp;
                @MoveRight.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMoveRight;
                @MoveRight.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMoveRight;
                @MoveRight.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMoveRight;
                @MoveLeft.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMoveLeft;
                @MoveLeft.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMoveLeft;
                @MoveLeft.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMoveLeft;
                @MoveDown.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMoveDown;
                @MoveDown.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMoveDown;
                @MoveDown.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMoveDown;
            }
            m_Wrapper.m_GameplayActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MoveUp.started += instance.OnMoveUp;
                @MoveUp.performed += instance.OnMoveUp;
                @MoveUp.canceled += instance.OnMoveUp;
                @MoveRight.started += instance.OnMoveRight;
                @MoveRight.performed += instance.OnMoveRight;
                @MoveRight.canceled += instance.OnMoveRight;
                @MoveLeft.started += instance.OnMoveLeft;
                @MoveLeft.performed += instance.OnMoveLeft;
                @MoveLeft.canceled += instance.OnMoveLeft;
                @MoveDown.started += instance.OnMoveDown;
                @MoveDown.performed += instance.OnMoveDown;
                @MoveDown.canceled += instance.OnMoveDown;
            }
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);
    public interface IGameplayActions
    {
        void OnMoveUp(InputAction.CallbackContext context);
        void OnMoveRight(InputAction.CallbackContext context);
        void OnMoveLeft(InputAction.CallbackContext context);
        void OnMoveDown(InputAction.CallbackContext context);
    }
}
