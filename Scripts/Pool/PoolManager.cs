using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PoolManager : Singleton<PoolManager>
{

    public List<GameObject> poolPrefabs;

    //0-Enemy 1-Bullet 2-DeadEnemy 3-Boom 4-GunFire 5-Sound
    private List<ObjectPool<GameObject>> poolList;
    protected override void Awake()
    {
        base.Awake();
        poolList = new List<ObjectPool<GameObject>>();
        CreatePool();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnEnable()
    {
        EventHandler.CreateEnemy += OnCreateEnemy;
        EventHandler.KillEnemy += OnKillEnemy;
        EventHandler.CreateBullet += OnCreateBullet;
        EventHandler.ReleaseBullet += OnReleaseBullet;
        EventHandler.CreateDeadEnemy += OnCreateDeadEnemy;
        EventHandler.StartNewGame += OnStartNewGame;
        EventHandler.CreateBoom += OnCreateBoom;
        EventHandler.CreateGunFire += OnCreateGunFire;
        EventHandler.CreateSound += OnCreateSound;
    }
    private void OnDisable()
    {
        EventHandler.CreateEnemy -= OnCreateEnemy;
        EventHandler.KillEnemy -= OnKillEnemy;
        EventHandler.CreateBullet -= OnCreateBullet;
        EventHandler.ReleaseBullet -= OnReleaseBullet;
        EventHandler.CreateDeadEnemy -= OnCreateDeadEnemy;
        EventHandler.StartNewGame -= OnStartNewGame;
        EventHandler.CreateGunFire -= OnCreateGunFire;
        EventHandler.CreateSound += OnCreateSound;
    }
    private void CreatePool()
    {
        foreach (GameObject item in poolPrefabs)
        {
            Transform parent = new GameObject(item.name).transform;
            parent.SetParent(transform);

            var newPool = new ObjectPool<GameObject>(
                () => Instantiate(item, parent),
                e => { e.SetActive(true); },
                e => { e.SetActive(false); },
                e => { Destroy(e); }
            );

            poolList.Add(newPool);
        }
    }


    public void OnCreateEnemy(Vector3 pos)
    {
        ObjectPool<GameObject> objPool = poolList[0];
        GameObject obj = objPool.Get();
        obj.transform.position = pos;
    }
    public void ReleaseEnemy(GameObject obj)
    {
        ObjectPool<GameObject> objPool = poolList[0];
        objPool.Release(obj);
    }
    public void OnKillEnemy(GameObject obj)
    {
        ReleaseEnemy(obj);
        EventHandler.CallCreateDeadEnemy(obj.transform.position);
    }

    void OnCreateBullet(Vector3 pos, Vector2 force)
    {
        // var randomShifting = Random.Range(-Settings.maxRandomShifting, Settings.maxRandomShifting);
        ObjectPool<GameObject> objPool = poolList[1];
        GameObject obj = objPool.Get();
        obj.transform.position = pos;
        obj.GetComponent<Rigidbody2D>()?.AddForce(force);

    }

    public void OnReleaseBullet(GameObject obj)
    {
        ObjectPool<GameObject> objPool = poolList[1];
        objPool.Release(obj);
    }
    public void OnCreateDeadEnemy(Vector3 pos)
    {
        ObjectPool<GameObject> objPool = poolList[2];
        GameObject obj = objPool.Get();
        pos.y -= 0.6f;//尸体位置
        obj.transform.position = pos;
    }
    void OnStartNewGame()
    {
    }
    public void ReleaseDeadEnemy(GameObject obj)
    {
        ObjectPool<GameObject> objPool = poolList[2];
        objPool.Release(obj);
    }

    void OnCreateBoom(Vector3 pos)
    {
        StartCoroutine(CreateBoomEffect(pos));
    }
    IEnumerator CreateBoomEffect(Vector3 pos)
    {
        ObjectPool<GameObject> objPool = poolList[3];
        GameObject obj = objPool.Get();
        obj.transform.position = pos;
        yield return new WaitForSeconds(0.5f);
        objPool.Release(obj);
    }

    void OnCreateGunFire(Vector3 pos)
    {
        StartCoroutine(CreateGunFireEffect(pos));
    }
    IEnumerator CreateGunFireEffect(Vector3 pos)
    {
        ObjectPool<GameObject> objPool = poolList[4];
        GameObject obj = objPool.Get();
        obj.transform.position = pos;
        yield return new WaitForSeconds(0.5f);
        objPool.Release(obj);
    }

    void OnCreateSound(SoundDetails soundDetails)
    {
        StartCoroutine(CreateSound(soundDetails));
    }

    IEnumerator CreateSound(SoundDetails soundDetails)
    {
        ObjectPool<GameObject> objPool = poolList[5];
        GameObject obj = objPool.Get();
        obj.GetComponent<Sound>()?.SetSound(soundDetails);
        yield return new WaitForSeconds(soundDetails.soundClip.length);
        objPool.Release(obj);
    }
}
