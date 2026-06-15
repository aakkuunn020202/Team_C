using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    public float m_speed = 0.001f;// Playerの移動 速度

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        if (Keyboard.current.upArrowKey.isPressed)
        {
            transform.position += new Vector3(0.0f, 0.0f, 0.1f) * m_speed;
        }
        if (Keyboard.current.downArrowKey.isPressed)
        {
            transform.position -= new Vector3(0.0f, 0.0f, 0.1f) * m_speed;
        }
        if (Keyboard.current.leftArrowKey.isPressed)
        {
            transform.position -= new Vector3(0.1f, 0.0f, 0.0f) * m_speed;
        }
        if (Keyboard.current.rightArrowKey.isPressed)
        {
            transform.position += new Vector3(0.1f, 0.0f, 0.0f) * m_speed;
        }


    }
}
