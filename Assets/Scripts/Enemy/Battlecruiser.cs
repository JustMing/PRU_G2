using UnityEngine;

public class Battlecruiser : Enemy
{
    [SerializeField] private Sprite[] sprites;
    private float timer;
    private float frequency;
    private float amplitude;
    private float centerX;


    public override void OnEnable()
    {
        base.OnEnable();
        timer = transform.position.x;
        frequency = Random.Range(0.3f,1f);
        amplitude = Random.Range(0.3f,1.5f);
        centerX = transform.position.x;
    }

    public override void Start()
    {
        base.Start();
        spriteRenderer.sprite = sprites[Random.Range(0,sprites.Length)];
        destroyEffectPool = GameObject.Find("BattlecruiseBoomPool").GetComponent<ObjectPooler>();

        speedY = Random.Range(-0.8f, -1.5f);
    }

    public override void Update()
    {
        base.Update();

        timer -= Time.deltaTime;
        float sine = Mathf.Sin(timer * frequency) * amplitude;
        transform.position = new Vector3(sine + centerX, transform.position.y);
    }

}
