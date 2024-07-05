using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmsAnimationController : MonoBehaviour
{
    [SerializeField] private Vector3 position, rotation;
    [SerializeField] private int WeaponID, PistolID, KnifeID;
    [SerializeField] private Animator animator, weapon_animator;
    [SerializeField] private bool IsKnife;
    [SerializeField] private string weaponName;
    [SerializeField] private GameObject delete_gameObject;
    [SerializeField] private Transform R_ARM;
    [SerializeField] private RuntimeAnimatorController WeaponAnimatorController;
    [SerializeField] private WeaponController weapon_controller;

    public AnimationObject pistol_object, knife_object, weapon_object;
    public int currentWeapon;
    public bool CanInspect;

    private void Update()
    {
        if(Input.GetKeyDown("f"))
        {
            Inspect();
        }
        if(Input.GetKeyDown("1"))
        {
            if(weapon_object != null)
            {
                SetInWeaponary(weapon_object);
            }
        }
        if(Input.GetKeyDown("2"))
        {
            if(pistol_object != null)
            {
                SetInWeaponary(pistol_object);
            }
        }
        if(Input.GetKeyDown("3"))
        {
            if(knife_object != null)
            {
                SetInWeaponary(knife_object);
            }
        }
        try
        {
            AnimatorStateInfo currentAnimatorStateInfo = animator.GetCurrentAnimatorStateInfo(1);
            weapon_animator.Play(currentAnimatorStateInfo.shortNameHash, 0, currentAnimatorStateInfo.normalizedTime);    
        }
        catch
        {

        }   
    }

    public void Inspect()
    {
        if(CanInspect)
        {
            animator.SetTrigger("Inspect");
        }
    }

    public void Shoot()
    {
        if(IsKnife) 
        {
            animator.SetInteger("ShootType", Random.Range(0, 2));
        }
        animator.SetTrigger("Shoot");
    }

    public void Take(int weaponId)
    {
        animator.SetInteger("WeaponNO", weaponId);
        animator.SetTrigger("TakeWeapon");
        SetPosition();
        weapon_controller.ShootCoolDown = 0f;
    }

    public void Set(string path)
    {
        AnimationObject animation_object = Resources.Load("Yonomiru/weapon/" + path) as AnimationObject;
        position = animation_object.pos;
        rotation = animation_object.rot;
        IsKnife = animation_object.knife;
        Destroy(delete_gameObject);
        GameObject weapon = Instantiate(animation_object.model, R_ARM);
        weapon.AddComponent<Animator>();
        weapon.GetComponent<Animator>().runtimeAnimatorController = WeaponAnimatorController;
        weapon_animator = weapon.GetComponent<Animator>();
        delete_gameObject = weapon;
        weapon.layer = 8;
        if(animation_object.knife)
        {
            KnifeID = animation_object.id;
            knife_object = animation_object;
        }
        else if(animation_object.pistol)
        {
            PistolID = animation_object.id;
            pistol_object = animation_object;
        }
        else
        {
            WeaponID = animation_object.id;
            weapon_object = animation_object;
        }
        Take(animation_object.id);
        Transform detected = null; try { detected = weapon.GetComponent<GameWeapon>().MuzzlePoint.transform; } catch {}
        weapon_controller.Set(animation_object, detected, animation_object.ShootAudioClips);
        Object.FindObjectOfType<GameInterfaceController>().icon.sprite = animation_object.line;
        CanInspect = false;
        StartCoroutine(SetCan(animation_object));
    }

    public void SetInWeaponary(AnimationObject animation_object)
    {
        position = animation_object.pos;
        rotation = animation_object.rot;
        IsKnife = animation_object.knife;
        Destroy(delete_gameObject);
        GameObject weapon = Instantiate(animation_object.model, R_ARM);
        weapon.AddComponent<Animator>();
        weapon.GetComponent<Animator>().runtimeAnimatorController = WeaponAnimatorController;
        weapon_animator = weapon.GetComponent<Animator>();
        delete_gameObject = weapon;
        weapon.layer = 8;
        if(animation_object.knife)
        {
            KnifeID = animation_object.id;
        }
        else if(animation_object.pistol)
        {
            PistolID = animation_object.id;
        }
        else
        {
            WeaponID = animation_object.id;
        }
        Take(animation_object.id);
        Transform detected = null; try { detected = weapon.GetComponent<GameWeapon>().MuzzlePoint.transform; } catch {}
        try{ weapon_controller.Set(animation_object, detected, animation_object.ShootAudioClips); } catch {}
        Object.FindObjectOfType<GameInterfaceController>().icon.sprite = animation_object.line;
        CanInspect = false;
        StartCoroutine(SetCan(animation_object));
    }

    IEnumerator SetCan(AnimationObject animation_object)
    {
        yield return new WaitForSeconds(animation_object.CoolDownBeforeInspectAndShoot);

        CanInspect = true;
    }

    public void SetPosition()
    {
        gameObject.transform.localPosition = position;
        gameObject.transform.localRotation = Quaternion.Euler(rotation);
    }

}
