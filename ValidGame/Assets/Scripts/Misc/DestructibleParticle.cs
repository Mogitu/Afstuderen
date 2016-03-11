using UnityEngine;
/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   Particles that are "done" will be deleted automatically with this script attached.
/// </summary>
public class DestructibleParticle : MonoBehaviour {
    private ParticleSystem ParticleSystem;
	// Use this for initialization
	void Start () {
        ParticleSystem = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
	    if(ParticleSystem && !ParticleSystem.IsAlive())
        {
            Destroy(gameObject);
        }
	}
}