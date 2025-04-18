using System.Collections;
using UnityEngine;

public class PreviewChar : MonoBehaviour
{
    public Animator myAnim { get; private set; }    
    public Transform orgPos { get; private set; }
    [SerializeField] float moveSpeed = 5f;
    Coroutine moveCo;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myAnim = GetComponent<Animator>();
        orgPos = transform.parent;
    }

    public void Move(Transform pos) 
    {
        if(moveCo!=null)StopCoroutine(moveCo);
        moveCo = StartCoroutine(MoveCo(pos));
    }
    IEnumerator MoveCo(Transform pos)
    {
        myAnim.SetBool("IsRun", true);
        transform.LookAt(pos);
        float dist = Vector3.Distance(transform.position, pos.position);
        while(dist > 0)
        {
            float delta = Mathf.Min(Time.deltaTime * moveSpeed, dist);
            dist -= delta;
            transform.Translate(transform.forward * delta, Space.World);
            yield return null;
        }
        myAnim.SetBool("IsRun", false);
        transform.rotation = Quaternion.Euler(0, 180, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
