using UnityEngine;
using Random = UnityEngine.Random;

public class DVD : MonoBehaviour
{
    public Camera MainCamera;
    private Vector2 _screenBounds;
    private float _objectWidth;
    private float _objectHeight;
    private float _moveSpeedX;
    private float _moveSpeedY;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _screenBounds = MainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, MainCamera.transform.position.z));
        _objectWidth = transform.GetComponent<SpriteRenderer>().bounds.extents.x; 
        _objectHeight = transform.GetComponent<SpriteRenderer>().bounds.extents.y; 
        _moveSpeedX = Random.Range(-1f, 1f) * 6;
        _moveSpeedY = Random.Range(-1f, 1f) * 6;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Move();
        ClampPosition();
    }

    private void Move()
    {
        var position = transform.position;
        position = new Vector2(position.x + _moveSpeedX * Time.deltaTime, position.y + _moveSpeedY * Time.deltaTime);
        transform.position = position;
    }

    private void ClampPosition()
    {
        var viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, _screenBounds.x * -1 + _objectWidth, _screenBounds.x - _objectWidth);
        viewPos.y = Mathf.Clamp(viewPos.y, _screenBounds.y * -1 + _objectHeight, _screenBounds.y - _objectHeight);
        transform.position = viewPos;

        CheckScreenBounds(viewPos);
    }

    private void CheckScreenBounds(Vector3 viewPos)
    {
        // Check if hitting the screen edges, and reverse direction if necessary
        if (viewPos.x <= _screenBounds.x * -1 + _objectWidth || viewPos.x >= _screenBounds.x - _objectWidth)
        {
            _moveSpeedX *= -1;
            ChangeColor();
        }

        if (viewPos.y <= _screenBounds.y * -1 + _objectHeight || viewPos.y >= _screenBounds.y - _objectHeight)
        {
            _moveSpeedY *= -1;
            ChangeColor();
        }
    }

    private void ChangeColor()
    {
        _spriteRenderer.color = Random.ColorHSV(0, 1, 1, 1, 1, 1);
    }
}