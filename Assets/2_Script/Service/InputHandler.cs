using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public float HorizontalMovement { get; private set; }

    public float VerticalMovement { get; private set; }

    public float HorizontalSwipe { get; private set; }

    public float VerticalSwipe { get; private set; }



    private void Update()
    {
        HorizontalMovement = Input.GetAxis("Horizontal");
        VerticalMovement = Input.GetAxis("Vertical");

        HorizontalSwipe = Input.GetAxis("Mouse X");
        VerticalSwipe = Input.GetAxis("Mouse Y");
    }
}