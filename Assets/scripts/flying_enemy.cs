using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flying_enemy : MonoBehaviour
{
    Animator leAnimator;

    [SerializeField] float distance;
    [SerializeField] GameObject player;
    [SerializeField] AudioClip deathsound;

    AIDestinationSetter maAIDestinationSetter;

    // Start is called before the first frame update
    void Start()
    {
        maAIDestinationSetter = GetComponent<AIDestinationSetter>();
        leAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update(){
        PlayerIsClose();
    }

    private void PlayerIsClose()
    {
        if(player != null && Vector2.Distance(player.transform.position, transform.position) <= distance){
            maAIDestinationSetter.target = player.transform;
            SetOrientation();
        }
        else
        {
            maAIDestinationSetter.target = transform;
        }
    }

    private void SetOrientation()
    {
        transform.localScale = new Vector2(player.transform.position.x > transform.position.x ? -1 : 1, 1);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 0f, 0f, 0.35f);
        Gizmos.DrawSphere(transform.position, distance);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            AudioSource.PlayClipAtPoint(deathsound, transform.position);
            leAnimator.SetBool("death", true);
        }
    }

    void AfterExplosion()
    {
        Destroy(this.gameObject);
    }
}
