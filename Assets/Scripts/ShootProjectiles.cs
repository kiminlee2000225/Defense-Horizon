using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootProjectiles : MonoBehaviour
{
    public GameObject arrowPrefab;
    public GameObject slowBombPrefab;
    public GameObject stickyBombPrefab;
    public float projectileSpeed = 10f;
    public AudioClip shotSFX;
    public AudioClip bombThrowSFX;
    public Image reticleImage;
    public Color reticleColor;
    public Color enemyColor;
    public GameObject arrowWeapon;
    public GameObject bowWeapon;
    public GameObject sludeBombWeapon;
    public GameObject stickyBombWeapon;
    private Vector3 cameraPosition;
    public static GameObject currentItem;
    public float coolDown = 0.3f;
    public bool canShoot;

    // Start is called before the first frame update
    void Start()
    {
        reticleColor = reticleImage.color;
        currentItem = arrowPrefab;
        coolDown = 0.3f;
        canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenuBehaviour.isGamePaused && !ShopBehavior.shopOpen)
        {
            cameraPosition = GameObject.FindGameObjectWithTag("MainCamera").transform.position;
            if (ChangeItem.selectedItem == 1)
            {
                bowWeapon.SetActive(false);
                arrowWeapon.SetActive(false);
                stickyBombWeapon.SetActive(false);
                if (Inventory.bomb > 0)
                {
                    sludeBombWeapon.SetActive(true);
                    if (Input.GetButtonDown("Fire1") && canShoot)
                    {
                        --Inventory.bomb;
                        GameObject projectile = Instantiate(slowBombPrefab, transform.position + transform.forward, transform.rotation)
                            as GameObject;

                        if (!projectile.GetComponent<Rigidbody>())
                        {
                            projectile.AddComponent<Rigidbody>();
                        }

                        var rb = projectile.GetComponent<Rigidbody>();
                        rb.AddForce(transform.forward * 20f, ForceMode.VelocityChange);

                        projectile.transform.SetParent(GameObject.FindGameObjectWithTag("ProjectileParent").transform);
                        AudioSource.PlayClipAtPoint(bombThrowSFX, cameraPosition);
                        sludeBombWeapon.SetActive(false);
                        if (Inventory.bomb > 0)
                        {
                            Invoke("SludeBombWeaponActivate", 0.2f);
                        }
                        canShoot = false;
                        StartCoroutine(ShootDelay());
                    }
                }
            }
            else if (ChangeItem.selectedItem == 0)
            {
                sludeBombWeapon.SetActive(false);
                stickyBombWeapon.SetActive(false);
                bowWeapon.SetActive(true);
                arrowWeapon.SetActive(true);
                if (Input.GetButtonDown("Fire1") && canShoot)
                {
                    GameObject projectile = Instantiate(arrowPrefab, transform.position + transform.forward, transform.rotation)
                        as GameObject;

                    if (!projectile.GetComponent<Rigidbody>())
                    {
                        projectile.AddComponent<Rigidbody>();
                    }

                    var rb = projectile.GetComponent<Rigidbody>();
                    rb.AddForce(transform.forward * projectileSpeed, ForceMode.VelocityChange);

                    projectile.transform.SetParent(GameObject.FindGameObjectWithTag("ProjectileParent").transform);
                    AudioSource.PlayClipAtPoint(shotSFX, cameraPosition);
                    arrowWeapon.SetActive(false);
                    Invoke("ArrowWeaponActivate", 0.2f);
                    canShoot = false;
                    StartCoroutine(ShootDelay());
                }
            }
            if (ChangeItem.selectedItem == 2)
            {
                bowWeapon.SetActive(false);
                arrowWeapon.SetActive(false);
                sludeBombWeapon.SetActive(false);
                if (Inventory.glue > 0)
                {
                    stickyBombWeapon.SetActive(true);
                    if (Input.GetButtonDown("Fire1") && canShoot)
                    {
                        --Inventory.glue;
                        GameObject projectile = Instantiate(stickyBombPrefab, transform.position + transform.forward, transform.rotation)
                            as GameObject;

                        if (!projectile.GetComponent<Rigidbody>())
                        {
                            projectile.AddComponent<Rigidbody>();
                        }

                        var rb = projectile.GetComponent<Rigidbody>();
                        rb.AddForce(transform.forward * 10f, ForceMode.VelocityChange);

                        projectile.transform.SetParent(GameObject.FindGameObjectWithTag("ProjectileParent").transform);
                        AudioSource.PlayClipAtPoint(bombThrowSFX, cameraPosition);
                        stickyBombWeapon.SetActive(false);
                        if (Inventory.glue > 0)
                        {
                            Invoke("StickyBombWeaponActivate", 0.2f);
                        }
                        canShoot = false;
                        StartCoroutine(ShootDelay());
                    }
                }
            }
            reticleImage.enabled = true;
            ReticleEffect();
        }
        else
        {
            reticleImage.enabled = false;
        }
        
    }

    IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(coolDown);
        canShoot = true;
    }

    void SludeBombWeaponActivate()
    {
        sludeBombWeapon.SetActive(true);
    }

    void ArrowWeaponActivate()
    {
        arrowWeapon.SetActive(true);
    }

    void StickyBombWeaponActivate() {
        stickyBombWeapon.SetActive(true);
    }

    void ReticleEffect()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity))
        {
            if (hit.collider.CompareTag("Enemy") || hit.collider.CompareTag("EnemyBig"))
            {
                reticleImage.color = new Color(enemyColor.r, enemyColor.g, enemyColor.b, 1f);
                reticleImage.transform.localScale = new Vector3(0.8f, 0.8f, 1f);
            }
            else
            {
                reticleImage.color = Color.Lerp(reticleImage.color, reticleColor, 2 * Time.deltaTime);
                reticleImage.transform.localScale = Vector3.Lerp(reticleImage.transform.localScale, new Vector3(1f, 1f, 1f), 2 * Time.deltaTime);
            }
        }
        else
        {
            reticleImage.color = Color.Lerp(reticleImage.color, reticleColor, 2 * Time.deltaTime);
            reticleImage.transform.localScale = Vector3.Lerp(reticleImage.transform.localScale, new Vector3(1f, 1f, 1f), 2 * Time.deltaTime);
        }
    }

}
