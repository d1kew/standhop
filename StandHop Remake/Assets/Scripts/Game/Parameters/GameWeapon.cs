using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWeapon : MonoBehaviour
{
    [SerializeField] private AnimationObject animation_object;
    [SerializeField] private GameObject[] ForLayer;

    #region childs
    [SerializeField] private GameObject magazine1AnimationName;
    [SerializeField] private GameObject magazine2AnimationName;
    [SerializeField] private GameObject magazine3AnimationName;
    [SerializeField] private GameObject gunlock1AnimationName;
    [SerializeField] private GameObject gunlock2AnimationName;
    [SerializeField] private GameObject sightAnimationName;
    [SerializeField] private GameObject collimatorSightAnimationName;
    [SerializeField] private GameObject sightLenseAnimationName;
    [SerializeField] private GameObject sightReticleAnimationName;
    [SerializeField] private GameObject cartridge1AnimationName;
    [SerializeField] private GameObject cartridge2AnimationName;

    public GameObject MuzzlePoint;
    #endregion

    #region set name child

    private void Awake()
    {
        foreach(GameObject fl in ForLayer)
        {
            fl.layer = 8;
        }
        try
        {
            magazine1AnimationName.name = animation_object.magazine1AnimationName;
        }
        catch
        {
        }

        try
        {
            magazine2AnimationName.name = animation_object.magazine2AnimationName;
        }
        catch
        {
        }

        try
        {
            magazine3AnimationName.name = animation_object.magazine3AnimationName;
        }
        catch
        {

        }

        try
        {
            gunlock1AnimationName.name = animation_object.gunlock1AnimationName;
        }
        catch
        {
        }

        try
        {
            gunlock2AnimationName.name = animation_object.gunlock2AnimationName;
        }
        catch
        {
        }

        try
        {
            sightAnimationName.name = animation_object.sightAnimationName;
        }
        catch
        {
        }

        try
        {
            collimatorSightAnimationName.name = animation_object.collimatorSightAnimationName;
        }
        catch
        {
        }
        
        try
        {
            sightLenseAnimationName.name = animation_object.sightLenseAnimationName;
        }
        catch
        {
        }

        try
        {
        sightReticleAnimationName.name = animation_object.sightReticleAnimationName;
        }
        catch
        {
        }

        try
        {
            cartridge1AnimationName.name = animation_object.cartridge1AnimationName;
        }
        catch
        {
        }

        try
        {
        cartridge2AnimationName.name = animation_object.cartridge2AnimationName;
        }
        catch
        {
        }
    }
    #endregion
}
