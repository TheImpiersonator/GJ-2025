using UnityEngine;

public class SodaPickUp : MonoBehaviour
{
    [SerializeField] GameObject pickupPrefab;
    [SerializeField] Soda sodaClass;
    [SerializeField] float rotateSpeed;

    private void Start()
    {
        if(pickupPrefab == null)
        {
            pickupPrefab = gameObject;
        }
    }

    private void Update()
    {
        Spin();
    }

    void Spin() {
        pickupPrefab.transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
    }
    //Gets the soda that the pickup correlates to
    public Soda get_Soda() {
        return sodaClass;
    }
}
