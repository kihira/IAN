using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Lever : MonoBehaviour
{
    [SerializeField] private LeverManager manager;
    [SerializeField] private Image image;
    private Animator animator;
    private float timer; // Hacky fix

    private LeverManager.State state;

    void Start()
    {
        animator = GetComponent<Animator>();
        manager.RegisterLever(gameObject.name);
    }

    void Update()
    {
        if (state == LeverManager.State.Changing)
        {
            if (timer > 1.5f)
            {
                manager.SetLeverState(gameObject.name, LeverManager.State.Flipped);
                state = LeverManager.State.Flipped;
                image.color = Color.red;
            }
            else
            {
                timer += Time.deltaTime;
            }
        } 
    }

    void OnTriggerEnter(Collider other)
    {
        if (state == LeverManager.State.Default)
        {
            animator.SetBool("flip", true);
            state = LeverManager.State.Changing;
            manager.SetLeverState(gameObject.name, LeverManager.State.Changing);
        }
    }
}
