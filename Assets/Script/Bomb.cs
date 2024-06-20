using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject explosionEffect; // 爆炸效果Prefab
    private bool hasExploded = false; // 标志变量
    public AudioClip explosionSound; // 爆炸音效
    public float explosionSoundSpeed = 1.3f; // 爆炸音效的播放速度

    public float explosionDuration = 0.4f; // 爆炸效果持续时间

    public void Explode()
    {
        if (hasExploded)
            return;

        hasExploded = true; // 标记为已爆炸

        // 实例化爆炸效果
        GameObject explosion = Instantiate(explosionEffect, transform.position, transform.rotation);

        // 播放爆炸音效
        if (explosionSound != null)
        {
            AudioSource audioSource = explosion.AddComponent<AudioSource>(); // 添加一个新的AudioSource组件
            audioSource.clip = explosionSound;
            audioSource.pitch = explosionSoundSpeed; // 设置音效的播放速度
            audioSource.Play();
        }

        // 在指定的持续时间后销毁爆炸效果
        Destroy(explosion, explosionDuration);

        // 销毁炸弹
        Destroy(gameObject);

        // 可以在这里添加其他爆炸逻辑，例如伤害、音效等


    }
}
