using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: cinemachine?
public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float lerpForwardDuration = 0.5f;

    //TODO: only move with mouse click

    /*[SerializeField] float pitchSpeed = 100f;
    [SerializeField] private float yawSpeed = 100f;

    private float xRot = 0.0f;

     private void Update()
    {
        float yaw = yawSpeed * Input.GetAxis("Mouse X") * Time.deltaTime;
        float pitch = pitchSpeed * Input.GetAxis("Mouse Y") * Time.deltaTime;

        xRot += pitch;

        if (xRot > 90)
        {
            xRot = 90.0f;
            pitch = 0.0f;
            Clamp(270f);
        }
        else if (xRot < -90)
        {
            xRot = -90.0f;
            pitch = 0.0f;
            Clamp(90f);
        }

        //vertical movement was finicky as could look at goofy angles
        //transform.Rotate(-transform.right * pitch);
       // transform.eulerAngles = new Vector3(pitch, yaw, 0f);
        //player.transform.Rotate(Vector3.up * yaw);
    }

    private void Clamp(float clampVal)
    {
        Vector3 eulerRot = transform.eulerAngles;
        eulerRot.x = clampVal;
        transform.eulerAngles = eulerRot;
    }*/

    void LateUpdate()
    {
        transform.position = player.transform.position;

        StartCoroutine(RotateToPlayer(transform.forward, player.transform.forward));
    }

    IEnumerator RotateToPlayer(Vector3 camPos, Vector3 playerPos)
    {
        for (float t = 0f; t < lerpForwardDuration; t += Time.deltaTime)
        {
            transform.forward = Vector3.Slerp(camPos, playerPos, t / lerpForwardDuration);
            yield return 0;
        }
    }
}
