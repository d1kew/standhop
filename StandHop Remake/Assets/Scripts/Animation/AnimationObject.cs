using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "AnimationObject", menuName = "Yonomiru/AnimationObject")]
public class AnimationObject : ScriptableObject 
{
	public int id, cost;
	
	public string name;

	public float CoolDownBeforeInspectAndShoot;

    public GameObject model;

	public Sprite icon, line;

    public bool knife, pistol;

    public Vector3 pos, rot;

	public float FpsCamFOV = 45f;

	public float HeadDamage, ChestDamage, StomachDamage, LegsDamage;

	public float Rate, Range;

	public Vector3 Recoil;

	public AudioClip[] ShootAudioClips;

    public float snappiness;
	
    public float returnSpeed;

	public float FpsCamFOVClassic = 36f;

	public string magazine1AnimationName = "";

	public string magazine2AnimationName = "";

	public string magazine3AnimationName = "";

	public string gunlock1AnimationName = "";

	public string gunlock2AnimationName = "";

	public string sightAnimationName = "";

	public string collimatorSightAnimationName = "";

	public string sightLenseAnimationName = "";

	public string sightReticleAnimationName = "";

	public string cartridge1AnimationName = "";

	public string cartridge2AnimationName = "";
}
