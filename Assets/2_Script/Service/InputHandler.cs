using System;
using System.Collections;
using UnityEditor;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private Joystick joystick;
    [SerializeField] private SwipeDetector swipeDetector;
    [SerializeField] private MyButton spaceButton;


    public float HorizontalMovement { get; private set; }
    public float VerticalMovement { get; private set; }
    public float HorizontalSwipe { get; private set; }
    public float VerticalSwipe { get; private set; }
    public bool LeftMouseButtonDown { get; private set; }
    public bool RightMouseButtonDown { get; private set; }
    public bool Space { get; private set; }

    private void Start()
    {
        MobileInputSubscribe();
    }


    private void Update()
    {
        MobileInputUpdate();
    }

    private void DescInputUpdate()
    {
        HorizontalMovement = Input.GetAxis("Horizontal");
        VerticalMovement = Input.GetAxis("Vertical");

        HorizontalSwipe = Input.GetAxis("Mouse X");
        VerticalSwipe = Input.GetAxis("Mouse Y");

        Space = Input.GetKeyDown(KeyCode.Space);

        LeftMouseButtonDown = Input.GetMouseButtonDown(0);
        RightMouseButtonDown = Input.GetMouseButtonDown(1);
    }

    private void MobileInputUpdate()
    {
        HorizontalMovement = joystick.Horizontal;
        VerticalMovement = joystick.Vertical;

        HorizontalSwipe = swipeDetector.swipeDelta.x;
        VerticalSwipe = swipeDetector.swipeDelta.y;
    }

    private void MobileInputSubscribe()
    {
        spaceButton.OnHandDown += () => Space = true;
        spaceButton.OnHandUp += () => Space = false;
    }

    private void MobileInputUnsubscribe()
    {
        spaceButton.OnHandDown -= () => Space = true;
        spaceButton.OnHandUp -= () => Space = false;
    }

    private IEnumerator WaitFrame(Action action)
    {
        yield return new WaitForEndOfFrame();

        action.Invoke();
    }


    private void OnDestroy()
    {
        MobileInputUnsubscribe();
    }
}