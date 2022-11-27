using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
[RequireComponent(typeof(Rigidbody2D))]

public class PlayerMovemant : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotationSpeed;

    private bool _gameOver = false;

    private Rigidbody2D _rb;
    private Camera _cam;

    private float _waitTime = 2f;

    private void Start(){
        _rb = GetComponent<Rigidbody2D>();
        _cam = Camera.main;
    }

    private void Update(){
        //get player input
        if (!_gameOver){
            if (Input.GetKey(KeyCode.RightArrow)){
                transform.Rotate(Vector3.forward * (-_rotationSpeed) * Time.deltaTime);
            }else if (Input.GetKey(KeyCode.LeftArrow)){
                transform.Rotate(Vector3.forward * _rotationSpeed * Time.deltaTime);
            }
        }
    }

    private void FixedUpdate(){
        //move the player (push the rigidbody)
        if (!_gameOver){
            _rb.AddRelativeForce(new Vector3(0f, _moveSpeed * Time.fixedDeltaTime, 0f));
        }
    }

    private void LateUpdate(){
        //camera follows the player
        if (!_gameOver){
            _cam.transform.position = new Vector3(transform.position.x, transform.position.y, _cam.transform.position.z);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!_gameOver){
            _gameOver = true;
            GetComponentInChildren<SpriteRenderer>().enabled = false;
            GetComponentInChildren<PolygonCollider2D>().enabled = false;
            GetComponentInChildren<ParticleSystem>().Play();
            //restart this game after 2 seconds
            StartCoroutine(RestartThisScene());
        }
    }

    private IEnumerator RestartThisScene()
    {
        yield return new WaitForSeconds(_waitTime);
        Restart();
    }

    private void Restart()
    {
        SceneManager.LoadScene("SpaceShip");
    }
}
