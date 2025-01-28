using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI Points;
    private int _points;
    private Vector3 _initialPosition;

    void Start()
    {
        _points = 0;
        _initialPosition = transform.position;
    }

    void Update()
    {
        Points.text = $"Points:\n{_points}";

        if (Input.GetKeyDown(KeyCode.B))
        {
            StartCoroutine(Respawn());
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Goal"))
        {
            _points++;
            StartCoroutine(Respawn());
        }
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(0.5f);

        transform.position = _initialPosition;
        GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
    }

}
