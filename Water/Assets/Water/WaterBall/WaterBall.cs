using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBall : MonoBehaviour
{
    [SerializeField] ParticleSystem _WaterBallParticleSystem;
    [SerializeField] AnimationCurve _SpeedCurve;
    [SerializeField] float _Speed;
    [SerializeField] ParticleSystem _SplashPrefab;
    [SerializeField] ParticleSystem _SpillPrefab;
    public void Throw(Vector3 target)
    {
        StopAllCoroutines();
        StartCoroutine(Coroutine_Throw(target));
    }

    IEnumerator Coroutine_Throw(Vector3 target) 
    {
        float lerp = 0;
        Vector3 startPos = transform.position;
        Vector3 endPos = new Vector3(startPos.x, target.y, startPos.z); // Set the end position to have the same x and z, but the y of the target

        while (lerp < 1)
        {
            transform.position = Vector3.Lerp(startPos, endPos, _SpeedCurve.Evaluate(lerp));
            lerp += Time.deltaTime * _Speed;
            yield return null;
        }

        _WaterBallParticleSystem.Stop(false, ParticleSystemStopBehavior.StopEmittingAndClear);
        ParticleSystem splas = Instantiate(_SplashPrefab, endPos, Quaternion.identity);
        Vector3 forward = endPos - startPos;
        forward.y = 0;
        splas.transform.forward = forward;
        if (Vector3.Angle(startPos - endPos, Vector3.up) > 30)
        {
            ParticleSystem spill = Instantiate(_SpillPrefab, endPos, Quaternion.identity);
            spill.transform.forward = forward;
        }
        Destroy(gameObject, 0.5f);
    }
}
