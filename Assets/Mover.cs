using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public Vector3 target;
    private Vector3 basePos;

    private void Start()
    {
        basePos = transform.localPosition;
    }

    public void ReturnBase()
    {
        transform.localPosition = basePos;
    }

    public void Move()
    {
        StartCoroutine(moving());
    }

    public IEnumerator moving()
    {
        while (Vector3.Distance(transform.localPosition, basePos+target) > 0.1f)
        {
            yield return new WaitForEndOfFrame();

            transform.localPosition = Vector3.Lerp(transform.localPosition, basePos + target, 2f * Time.deltaTime);
        }
    }
}
