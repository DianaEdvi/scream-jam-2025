using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class PlayerController : MonoBehaviour
{
    private Camera _camera;
    private new Light2D _light;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get components
        _light = GetComponent<Light2D>();
        _camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        // Get mouse position in screen coordinates
        var mouseScreen = Mouse.current.position.ReadValue();

        // Convert screen point to world point
        var mouseWorld = _camera.ScreenToWorldPoint(new Vector3(mouseScreen.x, mouseScreen.y, _camera.nearClipPlane));

        // Move the sprite to that position
        _light.transform.position = new Vector3(mouseWorld.x, mouseWorld.y, _light.transform.position.z);

    }
}
