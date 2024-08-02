using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void Start()
    {
        transform.DORotate(new Vector3(0, 0, 360f), 5f, RotateMode.LocalAxisAdd)
            .SetLoops(-1, LoopType.Incremental)
            .SetEase(Ease.Linear);

        float delay = Random.Range(0, 1f);
        DOVirtual.DelayedCall(delay, () => {
            transform.DOMoveY(transform.position.y + 0.5f, 3f)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.InOutSine);
        });
    }
}
