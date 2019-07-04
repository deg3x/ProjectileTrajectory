using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bullet;
    public GameObject start;
    public Camera rayCamera;
    public LineRenderer traj;
    [Range(1f, 10f)]
    public float fireRate;
    [Range(1f, 100f)]
    public float bulletVelocity;

    private float timeSinceFire = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
        timeSinceFire = fireRate;
    }

    void Update()
    {
        timeSinceFire += Time.deltaTime;

        if(Input.GetAxisRaw("Fire1") != 0f)
        {
            if(timeSinceFire >= 1f/fireRate)
            {
                GameObject g = Instantiate(bullet, start.transform.position, start.transform.rotation);
                g.GetComponent<Rigidbody>().AddForce(g.transform.forward * bulletVelocity, ForceMode.VelocityChange);
                timeSinceFire = 0f;
            }
        }

        Ray ray = rayCamera.ScreenPointToRay(Input.mousePosition);
        this.transform.rotation = Quaternion.LookRotation(ray.direction, Vector3.up);
    }
}
