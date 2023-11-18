using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Pet : MonoBehaviour
{
    [SerializeField] GameObject petObject;
    Player player;
    Vector2 startPosition;
    bool isFinishCollect = true;
    bool isMoveToStartPostion = true;
    Tween autoMove;
    Sequence autoMoveSeuquence;
    private void Awake()
    {
        player = GetComponentInParent<Player>();
    }
    void Start()
    {
        autoMoveSeuquence = DOTween.Sequence();
        startPosition = petObject.transform.position;
        autoMove = this.transform?.DORotate(new Vector3(0, 0, 360), 2, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(999, LoopType.Incremental);
        autoMoveSeuquence.Append(autoMove);
    }

    // Update is called once per frame
    void Update()
    {
        CollectReward();
        // CollectRewardNew();

    }
    public void CollectReward()
    {
        if (player.GetRewardInArea().Count >= 1 && isFinishCollect) // nếu phát hiện có reward , đặt flag để thực hiện 1 lần
        {
            autoMoveSeuquence.Pause();
            isFinishCollect = false;
            Sequence mySequence = DOTween.Sequence();
            foreach (var item in player.GetRewardInArea())
            {
                mySequence.Append(petObject.transform?.DOMove(item, 0.5f));
            }
            DOVirtual.DelayedCall(0.5f * player.GetRewardInArea().Count, () =>
            {
                isFinishCollect = true; // đã DO xong
                isMoveToStartPostion = false; // đã move ra khỏi vị trí ban đầu
                //
            });
            //
        }

        if (player.GetRewardInArea().Count == 0 & !isMoveToStartPostion) //nếu không phát hiện reward thì quay về vị trí ban đầu, đặt flag để thực hiện 1 lần
        {
            isMoveToStartPostion = true;
            MoveToPosition(new Vector2(this.transform.position.x, this.transform.position.y + 2), 0.5f);
            autoMoveSeuquence.Play();

        }
    }
    public void MoveToPosition(Vector2 position, float time)
    {
        petObject.transform?.DOMove(position, time);
    }

    public void CollectRewardNew()
    {
        if (!isFinishCollect)
        {
            return;
        }
        if (player.GetRewardInArea().Count >= 1) // nếu phát hiện có reward , đặt flag để thực hiện 1 lần
        {
            isFinishCollect = false; //set flag
            autoMoveSeuquence.Pause();
            Sequence mySequence = DOTween.Sequence();
            foreach (var item in player.GetRewardInArea())
            {
                mySequence.Append(petObject.transform?.DOMove(item, 0.5f)); //move to reward position to collect
            }
            mySequence.Append(petObject.transform?.DOMove(new Vector2(this.transform.position.x, this.transform.position.y + 2), 0.1f));
            DOVirtual.DelayedCall(0.5f * (player.GetRewardInArea().Count + 1), () =>
            {
                isFinishCollect = true;
                autoMoveSeuquence.Play();
            }
            ); // set flag after finish all collect and move back



        }
    }
}
