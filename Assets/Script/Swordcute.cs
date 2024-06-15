using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swordcute : MonoBehaviour
{
    // Start is called before the first frame update

    public Material capMaterial;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnCollisionEnter(Collision collision)
    {
        GameObject victim = collision.collider.gameObject;

        GameObject[] pieces = BLINDED_AM_ME.MeshCut.Cut(victim, transform.position, transform.right, capMaterial);

        if (!pieces[1].GetComponent<Rigidbody>())
            pieces[1].AddComponent<Rigidbody>();

        Destroy(pieces[1], 1);

        if (victim.tag == "GameoverUI")
        {
            fruitspawn.instance.RestartGame();
            Rigidbody rigidbody = pieces[0].GetComponent<Rigidbody>();
            rigidbody.useGravity = true;
            Destroy(pieces[0], 1);
        }

        //增加分数,一个水果10分
        fruitspawn.instance.AddScore(5);
    }
}
