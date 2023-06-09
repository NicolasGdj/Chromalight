using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserWay : MonoBehaviour
{
    public LaserEmitterController[] emitters;
    public float timeBetweenStates = 0.1f;
    public int maxDeactivatedLasers = 5;
    public bool reverse;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AnimateLasers());
    }

    // Coroutine to animate lasers
    IEnumerator AnimateLasers()
    {
        while (true)
        {
            foreach (LaserEmitterController emitter in emitters) {
                emitter.enabled = !reverse;
            }
            yield return new WaitForSeconds(timeBetweenStates);

            for (int i = 0; i < emitters.Length + maxDeactivatedLasers; ++i) {
                if(0 <= i-maxDeactivatedLasers)
                    emitters[i-maxDeactivatedLasers].enabled = !reverse;

                if(i < emitters.Length)
                    emitters[i].enabled = reverse;
                
                yield return new WaitForSeconds(timeBetweenStates);
            }

            foreach (LaserEmitterController emitter in emitters) {
                emitter.enabled = !reverse;
            }
            yield return new WaitForSeconds(timeBetweenStates);

            for (int i = 0; i < emitters.Length + maxDeactivatedLasers; ++i) {
                int index = emitters.Length - 1 - i;

                if (index + maxDeactivatedLasers < emitters.Length)
                    emitters[index + maxDeactivatedLasers].enabled = !reverse;

                if (index >= 0)
                    emitters[index].enabled = reverse;

                yield return new WaitForSeconds(timeBetweenStates);
            }

        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
