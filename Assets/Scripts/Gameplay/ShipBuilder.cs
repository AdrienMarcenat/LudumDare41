﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum ShipSize
{
    XS,
    S,
    M,
    L,
    XL,
    XXL,
    OTHER
}

public static class ShipBuilderDependenceInjection
{
    public static bool TryBuild(this Enemy me)
    {
        ShipBuilder builder = me.GetComponent<ShipBuilder>();
        if(!builder)
        {
            return false;
        }
        builder.Build();
        return true;
    }
}

public class ShipBuilder : MonoBehaviour
{
    [Header("General")]
    public ShipSize size = ShipSize.M;
    public Sprite sprite;
    [Space(10)]
    public float health = 100;
    public float dommageOnCollision = 20;
    [Space(4)]
    [Header("Aiming")]
    public bool playerAim;
    public float fireRate = 20;
    [Space(4)]
    [Header("(Optional)")]
    public Transform target;
    public float precision;
    public float salveNumber;
    public float sizeModifier;
    [Space(4)]
    [Header("Movement (Optional)")]
    public BezierCurve path;
    public float duration;
    [Space(2)]
    [Header("Weapons (Optional)")]
    public GameObject[] weapons;
    [Space(2)]
    [Header("Effects (Optional)")]
    public float onHitColorTime = 2;
    public AudioClip onHitSound;

    public static float SizeToFloat(ShipSize size){
        switch(size){
            case(ShipSize.XS):
                return 24;
            case(ShipSize.S):
                return 32;
            case(ShipSize.M):
                return 48;
            case(ShipSize.L):
                return 64;
            case(ShipSize.XL):
                return 96;
        }
        return 0;
    }

    void Awake()
    {
        Build();
    }

    public void Build(){
        SetupWeapons();
        SetupGeneral();
        SetupPhysics();
        SetupAim();
    }

    IEnumerator DestroyFirstWhenSecondDestroyed(GameObject first, GameObject second)
    {
        while (second != null)
        {
            yield return new WaitForSeconds(1);
        }
        Destroy(first);
    }

    protected void SetupGeneral()
    {
        Health healthScript = GetComponent<Health>();
        if (!healthScript)
        {
            healthScript = gameObject.AddComponent<Health>();
        }
        healthScript.SetHealth(health);

        EnemyPath pathScript = GetComponent<EnemyPath>();
        if (!pathScript)
        {
            pathScript = gameObject.AddComponent<EnemyPath>();
        }
        pathScript.SetDamageOnCollision(dommageOnCollision);
        if (!path)
        {
            GameObject temp = new GameObject(name + "_BezierCurveHolder");
            StartCoroutine(DestroyFirstWhenSecondDestroyed(temp, gameObject));
            path = temp.AddComponent<BezierCurve>();
            Vector2 currentPosition = (Vector2)transform.position;
            float x = currentPosition.x;
            float y = currentPosition.y;
            path.points = new Vector2[]
                {
                    new Vector2(x+1,y),
                    new Vector2(x,y+1),
                    new Vector2(x-1,y),
                    new Vector2(x,y-1)
                };
        }
        pathScript.SetPath(path);
        if (duration <= 0)
        {
            Debug.LogWarning("The duration given wasn't stricly positive, a default duration of 15 secondes will be given instead");
            duration = 15;
        }
        pathScript.SetDuration(duration);
        if (onHitSound)
        {
            pathScript.SetHitSound(onHitSound);
        }
        pathScript.SetHitColorTime(onHitColorTime > 0 ? onHitColorTime : 1);
    }

    protected void SetupPhysics()
    {
        if (size != ShipSize.OTHER)
        {
            BoxCollider2D collider = GetComponent<BoxCollider2D>();
            if (!collider)
            {
                collider = gameObject.AddComponent<BoxCollider2D>();
            }
            collider.size = new Vector2(2 * SizeToFloat(size), 2 * SizeToFloat(size));
        }
        if(sprite)
        {
            SpriteRenderer renderer = GetComponentInChildren<SpriteRenderer>();
            if (!renderer)
            {
                GameObject temp = new GameObject();
                GameObject child = Instantiate(temp,transform);
                DestroyImmediate(temp);
                child.name = name + "_Sprite";
                renderer = child.AddComponent<SpriteRenderer>();
            }
            renderer.sprite = sprite;
        }

        if (!GetComponentInChildren<EnemyGUI>())
        {
            GameObject temp = new GameObject();
            GameObject child = Instantiate(temp,transform);
            DestroyImmediate(temp);
            child.name = name + "_HealthBar";
            SpriteRenderer renderer = child.AddComponent<SpriteRenderer>();
            child.AddComponent<EnemyGUI>().SetRenderer(renderer);
        }

        if (!GetComponent<Rigidbody2D>())
        {
            gameObject.AddComponent<Rigidbody2D>();
        }
    }

    protected void SetupAim()
    {
        EnemyAI aiming = GetComponent<EnemyAI>();
        if (!aiming)
        {
            if (playerAim)
            {
                aiming = gameObject.AddComponent<EnemyPlayerAiming>();
            }
            else
            {
                aiming = gameObject.AddComponent<EnemyFixedAiming>();
            }
        }
        aiming.SetFireParameters(fireRate, salveNumber, sizeModifier, precision);

        if (target)
        {
            aiming.SetShootDirection(target);
        }
        else if (!playerAim)
        {
            aiming.SetShootDirection(transform);
        }
    }

    protected void SetupWeapons()
    {
        if (!GetComponent<WeaponManager>())
        {
            gameObject.AddComponent<WeaponManager>();
        }
        if (weapons.Length == 0)
        {
            return;
        }
        foreach(GameObject weapon in weapons)
        {
            Instantiate(weapon, transform);
        }
    }
}