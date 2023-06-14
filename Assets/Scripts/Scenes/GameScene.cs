using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameScene : BaseScene
{
    public GameObject player;
    public Transform playerPosition;
    //public CinemachineFreeLook freelookCamera;

    protected override IEnumerator LoadingRoutine()
    {
        progress = 0f;
        Debug.Log("랜덤 맵 생성");
        yield return new WaitForSecondsRealtime(1f);

        progress = 0.2f;
        Debug.Log("랜덤 몬스터 생성");
        yield return new WaitForSecondsRealtime(1f);

        progress = 0.4f;
        Debug.Log("랜덤 아이템 생성");
        yield return new WaitForSecondsRealtime(1f);

        progress = 0.6f;
        //player.transform.position = playerPosition.position;
        yield return new WaitForSecondsRealtime(1f);

        progress = 0.8f;
        Debug.Log("카메라이동");
        //freelookCamera.Follow = player.transform;
        //freelookCamera.LookAt = player.transform;

        progress = 1.0f;
        yield return new WaitForSecondsRealtime(0.5f);

    }
}
