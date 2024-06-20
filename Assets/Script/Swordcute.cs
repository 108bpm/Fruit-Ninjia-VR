using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swordcute : MonoBehaviour
{
    // Start is called before the first frame update

    public Material capMaterial;
    public AudioClip cuttingSound;  // 切割音效
    private AudioSource audioSource;  // 音频源

    void Start()
    {
        // 添加AudioSource组件
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = cuttingSound;

    }

    // Update is called once per frame
    void Update()
    {

    }
    public class ProcessedFragment : MonoBehaviour
    {
        // 这个类可以保持为空，只用于标记已经被处理过的碎片
    }


    public void OnCollisionEnter(Collision collision)
    {
        GameObject victim = collision.collider.gameObject;

        // 如果已经是处理过的碎片，则跳过
        if (victim.GetComponent<ProcessedFragment>() != null)
        {
            return;
        }

        // 播放切割音效
        audioSource.Play();

        GameObject[] pieces = BLINDED_AM_ME.MeshCut.Cut(victim, transform.position, transform.right, capMaterial);

        // 给新生成的碎片添加标记，表示它们已经被处理过
        foreach (var piece in pieces)
        {
            piece.AddComponent<ProcessedFragment>();
        }

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
        if (victim.tag == "Bomb")
        {
            //切到炸弹
            fruitspawn.instance.AddScore(-40);
            Bomb bomb = collision.collider.GetComponent<Bomb>();
            if (bomb != null)
            {
                bomb.Explode();
            }

        }
        if (victim.tag == "Tomato" || victim.tag == "Pineapple")
        {

            fruitspawn.instance.AddScore(5);
        }
        if (victim.tag == "Carrot")
        {

            fruitspawn.instance.AddScore(20);
        }
        if (victim.tag == "Banana")
        {

            fruitspawn.instance.AddScore(10);
        }



    }
}
