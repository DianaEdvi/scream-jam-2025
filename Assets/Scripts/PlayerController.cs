using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class PlayerController : MonoBehaviour
{
    private Camera _camera;
    private Light2D _light;
    
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
        var mouseWorld = _camera.ScreenToWorldPoint(mouseScreen);

        // Move the sprite to that position
        _light.transform.position = new Vector3(mouseWorld.x, mouseWorld.y, _light.transform.position.z);
        
        // Send out raycast
        var hit = Physics2D.Raycast(mouseWorld, Vector2.zero);

        // If ray is hitting a collider and user clicks mouse, then trigger Interact event
        if (hit.collider != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            var hitObject = hit.collider.gameObject;
            Debug.Log("Interacted with " + hitObject.name);
            Events.OnInteract?.Invoke(hitObject);
        }

    }
}
