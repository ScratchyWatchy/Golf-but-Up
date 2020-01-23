using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchLaunch : MonoBehaviour
{
    AudioSource audioSource;
    public List<AudioClip> audioClips;
    public AudioClip hitSound;
    public AudioClip deathSound;

    bool IsTouching = false;
    public bool isGrounded = false;

    Rigidbody2D rb;
    public bool IsStatic;

    Vector2 startTouchPosition;
    Vector2 currentTouchPosition;

    LineRenderer lineRenderer;

    Vector2 startingPosition;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = gameObject.transform.position;
        audioSource = gameObject.GetComponent<AudioSource>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        lineRenderer = gameObject.GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckGround();

        if(Input.touchCount > 0 && !IsTouching)
        {
            IsTouching = true;
            startTouchPosition = Input.GetTouch(0).position;
        }
        else if(Input.touchCount > 0 && IsTouching)
        {
            currentTouchPosition = Input.GetTouch(0).position;
            if (IsStatic && isGrounded)
            {
                DrawPower();
            }
        }
        else if(IsTouching && Input.touchCount == 0)
        {
            IsTouching = false;
            if (IsStatic && isGrounded)
            {
                Launch(startTouchPosition, currentTouchPosition);
                DrawPower();
            }

            startTouchPosition = new Vector2(0, 0);
            currentTouchPosition = new Vector2(0, 0);
            ClearPower();
        }
    }

    void Launch(Vector2 start, Vector2 current)
    {
        gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.ClampMagnitude(new Vector2((current.x - start.x)/2, (current.y - start.y)/2), 395f));
        //gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2((current.x - start.x) / 2, (current.y - start.y) / 2));

        PlaySoundRandomPitch(hitSound);
    }

    void DrawPower()
    {
        Vector3 lineEnd = Vector2.ClampMagnitude(new Vector2(
            currentTouchPosition.x - startTouchPosition.x,
            currentTouchPosition.y - startTouchPosition.y),
            395f * 2f);

        lineEnd.z = -1;

        Vector3 position = Camera.main.WorldToScreenPoint(gameObject.transform.position);

        lineEnd = new Vector3(position.x + lineEnd.x, position.y + lineEnd.y, -1);

        lineEnd = Camera.main.ScreenToWorldPoint(lineEnd);


        lineRenderer.SetPosition(0, gameObject.transform.position);
        lineRenderer.SetPosition(1, lineEnd);
    }

    void ClearPower()
    {
        lineRenderer.SetPosition(0, new Vector3(0, 0, 0));
        lineRenderer.SetPosition(1, new Vector3(0, 0, 0));
    }

    void CheckGround()
    {
        if((rb.velocity.x > 0.06 || rb.velocity.y > 0.06) && !isGrounded)
        {
            IsStatic = false;
        }
        else
        {
            IsStatic = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Grass" && Mathf.Abs(rb.velocity.y) > 1)
        {
            PlaySoundRandomPitch(audioClips[0]);
        }
        if (collision.gameObject.tag == "OneWay" && Mathf.Abs(rb.velocity.y) > 1)
        {
            PlaySoundRandomPitch(audioClips[1]);
        }
    }

    void PlaySoundRandomPitch(AudioClip sound)
    {
        audioSource.clip = sound;
        audioSource.pitch = Random.Range(1, 1.3f);
        audioSource.Play();
    }

    Vector2 CutOff(Vector2 start, Vector2 end)
    {
        float x = Mathf.Abs(start.x - end.x);
        float y = Mathf.Abs(start.y - end.y);

        if(Mathf.Sqrt(Vector2.SqrMagnitude(new Vector2(x,y))) > 395f)
        {
            return Vector2.ClampMagnitude(new Vector2(x, y), 395f);
        }

        return new Vector2(x, y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Death")
        {
            gameObject.GetComponent<ParticleSystem>().Play();
            PlaySoundRandomPitch(deathSound);
            rb.isKinematic = true;          
            rb.velocity = new Vector2(0, 0);
            //gameObject.GetComponent<TrailRenderer>().Clear();
            Invoke("TeleportToStart", 0.3f);
        }
    }

    private void TeleportToStart()
    {
        gameObject.GetComponent<TrailRenderer>().Clear();
        gameObject.transform.position = startingPosition;
        gameObject.GetComponent<TrailRenderer>().Clear();
        rb.isKinematic = false;
    }
}
