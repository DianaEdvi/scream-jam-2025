using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    private Camera _camera;
    private Light2D _light;
    private Coroutine _flickeringCoroutine;
    
    // ======== TEMP =========
    private Keyboard _keyboard;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get components
        _light = GetComponent<Light2D>();
        _camera = Camera.main;

        Events.OnMothmanIsNear += StartFlickering;
        Events.OnMothmanIsFar += StopFlickering;
        
        // ========== TEMP ==========
        _keyboard = Keyboard.current;
    }

    // Update is called once per frame
    void Update()
    {
        
        // Move the sprite to that position
        _light.transform.position = new Vector3(MousePos().x, MousePos().y, _light.transform.position.z);
        
        // Checks if mouse is clicking interactable object and invokes Interact if yes 
        CheckForCollision();
        
        
        // ============== TEMP ================

        // Eventually move Invoke logic to when mothman is deemed far/near 
        if (_keyboard.spaceKey.wasPressedThisFrame)
        {
            Events.OnMothmanIsNear?.Invoke();
        }

        if (_keyboard.enterKey.wasPressedThisFrame)
        {
            Events.OnMothmanIsFar?.Invoke();
        }
    }
    
    /**
     * Returns a Vector3 that is the mouse position in the world space
     */
    private Vector3 MousePos()
    {
        // Get mouse position in screen coordinates
        var mouseScreen = Mouse.current.position.ReadValue();

        // Convert screen point to world point
        var mouseWorld = _camera.ScreenToWorldPoint(mouseScreen);
        
        return mouseWorld;
    }
    
    /**
     * Checks if the player clicks on an interactable object (an object with a collider) 
     */
    private void CheckForCollision()
    {
        // Send out raycast
        var hit = Physics2D.Raycast(MousePos(), Vector2.zero);

        // If ray is hitting a collider and user clicks mouse, then trigger Interact event
        if (hit.collider != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            var hitObject = hit.collider.gameObject;
            // Debug.Log("Interacted with " + hitObject.name);
            Events.OnInteract?.Invoke(hitObject);
        }
    }

    /**
     * Sets the light to begin flickering
     */
    private void StartFlickering()
    {
        // If Coroutine is null (inactive), begin it 
        _flickeringCoroutine ??= StartCoroutine(FlickerCoroutine());
    }
    
    /**
     * Stops the light from flickering
     */
    private void StopFlickering()
    {
        if (_flickeringCoroutine == null) return;
        // If Coroutine is not null (is active), then stop it 
        StopCoroutine(_flickeringCoroutine);
        _flickeringCoroutine = null;
        _light.enabled = true;
    }
    

    /**
     * Calculates a random amount of time for the light to be on/off and flickers infinitely until Coroutine is stopped  
     */
    private IEnumerator FlickerCoroutine()
    {
        while (true)
        {
            const float minFlickerTime = 0.01f;
            const float maxFlickerTime = 0.3f;
        
            var waitTime = Random.Range(minFlickerTime, maxFlickerTime);
            _light.enabled = !_light.enabled;
            yield return new WaitForSeconds(waitTime);
        }
    }
}
