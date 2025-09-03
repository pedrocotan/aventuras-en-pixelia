using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonajeMovimiento : MonoBehaviour
{
    [SerializeField] private float velocidad;

    public bool EnMovimiento => _direccionMovimiento.magnitude > 0f;
    public Vector2 DireccionMovimiento => _direccionMovimiento;

    private Rigidbody2D _rigidbody2D;
    private Vector2 _direccionMovimiento;
    private Vector2 _input;

    private void Awake ()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        RestaurarPosicionSiExiste();
    }

    // Update is called once per frame
    private void Update()
    {
        _input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        // X
        if (_input.x > 0.1f)
        {
            _direccionMovimiento.x = 1f;
        }
        else if (_input.x < 0f)
        {
            _direccionMovimiento.x = -1f;
        }
        else
        {
            _direccionMovimiento.x = 0f;
        }

        // Y
        if (_input.y > 0.1f)
        {
            _direccionMovimiento.y = 1f;
        }
        else if (_input.y < 0f)
        {
            _direccionMovimiento.y = -1f;
        }
        else
        {
            _direccionMovimiento.y = 0f;
        }
    }

    private void FixedUpdate()
    {
        _rigidbody2D.MovePosition(_rigidbody2D.position + _direccionMovimiento * velocidad * Time.fixedDeltaTime);
    }
    
    private void RestaurarPosicionSiExiste()
    {
        if (PlayerPrefs.HasKey("PlayerX") && PlayerPrefs.HasKey("PlayerY") && PlayerPrefs.HasKey("PlayerZ"))
        {
            float x = PlayerPrefs.GetFloat("PlayerX");
            float y = PlayerPrefs.GetFloat("PlayerY");
            float z = PlayerPrefs.GetFloat("PlayerZ");

            transform.position = new Vector3(x, y, z);
            Debug.Log("Posición restaurada desde PlayerPrefs.");
        }
        else
        {
            Debug.Log("No se encontró posición guardada. Comienza en la posición por defecto.");
        }
    }
}
