using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private Slider swipeIntensivity;
    [SerializeField] private Joystick joystick;
    [SerializeField] private SwipeDetector swipeDetector;
    [SerializeField] private MyButton spaceButton;

    [SerializeField] private GameObject mobileInout;

    public float HorizontalMovement { get; private set; }
    public float VerticalMovement { get; private set; }
    public float HorizontalSwipe { get; private set; }
    public float VerticalSwipe { get; private set; }
    public bool LeftMouseButtonDown { get; private set; }
    public bool RightMouseButtonDown { get; private set; }
    public bool Space { get; private set; }
    public bool Settings { get; private set; }

    private void Start()
    {
        if (YandexGame.EnvironmentData.isMobile)
        {
            MobileInputSubscribe();
        }
    }

    private void Update()
    {
        if (YandexGame.EnvironmentData.isMobile)
        {
            MobileInputUpdate();
        }
        else
        {
            DescInputUpdate();
        }
    }

    private void DescInputUpdate()
    {
        HorizontalMovement = Input.GetAxis("Horizontal");
        VerticalMovement = Input.GetAxis("Vertical");

        HorizontalSwipe = Input.GetAxis("Mouse X")  * swipeIntensivity.value;
        VerticalSwipe = Input.GetAxis("Mouse Y") * swipeIntensivity.value;

        Space = Input.GetKeyDown(KeyCode.Space);

        Settings = Input.GetKeyDown(KeyCode.Q);

        LeftMouseButtonDown = Input.GetMouseButtonDown(0);
        RightMouseButtonDown = Input.GetMouseButtonDown(1);
    }

    private void MobileInputUpdate()
    {
        HorizontalMovement = joystick.Horizontal;
        VerticalMovement = joystick.Vertical;

        HorizontalSwipe = swipeDetector.swipeDelta.x *swipeIntensivity.value;
        VerticalSwipe = swipeDetector.swipeDelta.y * swipeIntensivity.value;


    }

    private void MobileInputSubscribe()
    {
        spaceButton.OnHandDown += () => Space = true;
        spaceButton.OnHandUp += () => Space = false;
    }

    private IEnumerator WaitFrame(Action action)
    {
        yield return new WaitForEndOfFrame();

        action.Invoke();
    }

}