using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    #region objects

    [SerializeField] private ParticleSystem muzzleflash, StoneImpact, WoodImpact, MetalImpact;

    [SerializeField] private Transform MuzzleSpawnPoint;

    [SerializeField] private GameObject ShootPoint;

    [SerializeField] private LayerMask LayerMask;

    [SerializeField] private string StoneTag, WoodTag, MetalTag;

    [SerializeField] private ArmsAnimationController AnimationController;

    public AudioClip[] clips, ImpactSounds;

    [SerializeField] private AudioSource source, sourceImpact;

    #endregion

    #region mechanic
    public float ShootCoolDown = 0f;
    private bool InScope;
    private bool IsFire;
    #endregion

    #region parameters
    private float Rate = 0f;
    private float Range = 0f;
    private float HeadDamage = 0f;
    private float LegsDamage = 0f;
    private float ChestDamage = 0f;
    private float StomachDamage = 0f;
    private bool IsKnife, IsScopeWeapon;
    #endregion
    

    private void Update()
    {
        if(IsFire && Time.time >= ShootCoolDown)
        {   
            ShootCoolDown = Time.time + 1f / Rate; 
            Shoot();
        }     
        targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, returnSpeed * Time.deltaTime);
        currentRotation = Vector3.Lerp(currentRotation, targetRotation, snappiness * Time.fixedDeltaTime);
        recoil_camera.localRotation = Quaternion.Euler(currentRotation);   
    }

    private void Shoot()
    {
        if(AnimationController.CanInspect)
        {
            AnimationController.Shoot();
            RaycastHit hit;
            if(!IsKnife) { source.clip = clips[Random.Range(0, clips.Length)]; source.Play(); }
            if(!IsKnife) { ParticleSystem s = Instantiate (muzzleflash, MuzzleSpawnPoint); Destroy(s, 2f); }
            if (Physics.Raycast(ShootPoint.transform.position, ShootPoint.transform.forward, out hit, Range, LayerMask))
            {
                #region Impacts
                if(hit.transform.gameObject.tag == MetalTag)
                {
                    ParticleSystem instantiated = Instantiate(MetalImpact, hit.point, Quaternion.LookRotation(hit.normal));
                    Destroy(instantiated, 0.5f);   
                    if(IsKnife)
                    {
                        sourceImpact.clip = ImpactSounds[0]; sourceImpact.Play();
                    }       
                }
                if(hit.transform.gameObject.tag == StoneTag)
                {
                    ParticleSystem instantiated = Instantiate(StoneImpact, hit.point, Quaternion.LookRotation(hit.normal));
                    Destroy(instantiated, 0.5f);  
                    if(IsKnife)
                    {
                        sourceImpact.clip = ImpactSounds[1]; sourceImpact.Play();
                    }                            
                }
                if(hit.transform.gameObject.tag == WoodTag)
                {
                    ParticleSystem instantiated = Instantiate(WoodImpact, hit.point, Quaternion.LookRotation(hit.normal));
                    Destroy(instantiated, 0.5f);         
                    if(IsKnife)
                    {
                        sourceImpact.clip = ImpactSounds[2]; sourceImpact.Play();
                    }                     
                }
                #endregion

            }
            RecoilFire();
        }
    }

    public void Fire(bool excepted)
    {
        IsFire = excepted;
    }

    public void Set(AnimationObject animation_object, Transform object_transform, AudioClip[] audio)
    {
        Rate = animation_object.Rate;
        Range = animation_object.Range;
        HeadDamage = animation_object.HeadDamage;
        ChestDamage = animation_object.ChestDamage;
        StomachDamage = animation_object.StomachDamage;
        LegsDamage = animation_object.LegsDamage;
        MuzzleSpawnPoint = object_transform;
        IsKnife = animation_object.knife;
        clips = audio;
        recoilX = animation_object.Recoil.x; recoilY = animation_object.Recoil.y; recoilZ = animation_object.Recoil.z;
        snappiness = animation_object.snappiness;
        returnSpeed = animation_object.returnSpeed;
    }

    #region recoil
    [Header ("Recoil")]

    private Vector3 currentRotation;
    private Vector3 targetRotation;
    [SerializeField] MovementController ch_control;
    [SerializeField] private Transform recoil_camera;

    public float recoilX;
    public float recoilY;
    public float recoilZ;

    public float snappiness;
    public float returnSpeed;

    public void RecoilFire()
    {
        if(ch_control.crouch_del > 1f)
        {
            targetRotation += new Vector3(recoilX / ch_control.crouch_del, Random.Range(-recoilY/ ch_control.crouch_del , recoilY / ch_control.crouch_del), Random.Range(-recoilZ / ch_control.crouch_del, recoilZ / ch_control.crouch_del));
        }
        else
        {
            targetRotation += new Vector3(recoilX, Random.Range(-recoilY, recoilY), Random.Range(-recoilZ, recoilZ));
        }
    }
    #endregion
}
