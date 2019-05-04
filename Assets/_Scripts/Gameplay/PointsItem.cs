using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsItem : MonoBehaviour
{
    public BoolVariableSO building;
    public IntVariableSO score;
    public GameObject cubeModel;
    public ParticleSystem GainSFX;
    [Space]
    public int pointsIncrease;
    
    public bool collected { get; private set; }

    void OnTriggerEnter(Collider col)
    {
        if (building.Value && col.gameObject.CompareTag("Player"))
        {
            score.Value += pointsIncrease;
            collected = true;

            transform.SetParent(col.transform);
            transform.position = col.transform.position;
            StartCoroutine(CollectedCoroutine());
        }
    }

    IEnumerator CollectedCoroutine()
    {
        cubeModel.SetActive(false);
        GainSFX.Play();
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
