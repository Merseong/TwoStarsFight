using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject weaponBox;

    public bool isWeaponSpawned = false;

    public Sprite[] weaponSprites;

    private float lastTime;

    private void Update()
    {
        if (!isWeaponSpawned && lastTime + 10 < Time.time)
        {
            var box = Instantiate(weaponBox, transform);
            GetComponent<AudioSource>().Play();
            isWeaponSpawned = true;
            box.GetComponent<WeaponBox>().parent = this;
            box.GetComponent<WeaponBox>().inside = (WeaponName)Random.Range(1, 5);
            box.GetComponent<SpriteRenderer>().sprite = weaponSprites[(int)box.GetComponent<WeaponBox>().inside];
            lastTime = Time.time;
        }
    }
}
