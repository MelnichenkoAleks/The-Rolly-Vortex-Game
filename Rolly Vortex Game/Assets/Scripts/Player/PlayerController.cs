using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private float _speed = 7f;
    private float _rotationSpeed = 0.8f;
    private float _rotation = 100f;
    private const float maxSpeed = 28;

    void Start()
    {
        StartCoroutine(SpeedIncrease());
        StartCoroutine(RotateIncrease());
    }

    void Update()
    {
        if (!PlayerManager.levelStarted)
            return;
        if (PlayerManager.gameOver)
            return;

        // Вращение вокруг дочернего объекта
        Transform childTransform = transform.GetChild(0);
        childTransform.Rotate(Vector3.right, _rotation * Time.deltaTime);

        // Перемещение вдоль оси Z
        transform.Translate(0, 0, _speed * Time.deltaTime);

        if (Touchscreen.current != null)
        {
            // Вращение при касании
            Vector2 delta = Touchscreen.current.primaryTouch.delta.ReadValue();
            transform.Rotate(0, 0, delta.x * _rotationSpeed);
        }
    }

    private IEnumerator SpeedIncrease()
    {
        yield return new WaitForSeconds(2);
        if (_speed < maxSpeed)
        {
            _speed += 0.5f;
            StartCoroutine(SpeedIncrease());
        }
    }

    private IEnumerator RotateIncrease()
    {
        yield return new WaitForSeconds(2);
        if (_rotation < maxSpeed)
        {
            _rotation += 6f;
            StartCoroutine(RotateIncrease());
        }
    }
}
