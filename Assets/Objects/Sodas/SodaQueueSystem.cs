using System.Collections.Generic;
using UnityEngine;

public class SodaQueueSystem : MonoBehaviour
{
    [SerializeField] List<Soda> queue = new List<Soda>();
    [SerializeField] int MaxQueueCount = 6;
    [SerializeField] Transform spawnTransform;

    public Soda currSoda;

    //LOOK FOR SODA PICKUPS
    private void OnTriggerEnter(Collider other)
    {
        SodaPickUp sodaPU = other.gameObject.GetComponent<SodaPickUp>();

        //GUARD: Soda doesn't exist or Queue is Full
        if (!sodaPU || queue.Count >= MaxQueueCount) return;

        AddSoda(sodaPU.get_Soda());
        Destroy(sodaPU.gameObject);
    }


    void EquipSoda(Soda soda) {
        if (soda == null) return;
        
        currSoda = soda;

        Instantiate(currSoda.get_Prefab(), spawnTransform);

        currSoda.OnDurationEnd += RemoveSoda;
        currSoda.OnThrow += RemoveSoda;
    }

    /*Adds a Soda to the Soda Queue*/
    public void AddSoda(Soda soda) {
        
        if (queue.Count+1 > MaxQueueCount) return; //GUARD: Queue Too Full
        
        queue.Add(soda);
        Debug.Log(queue.Count);
        
        if (queue.Count <= 1) {
            EquipSoda(queue[0]);
        }
    }
    /*Removes the top soda of the queue*/
    public void RemoveSoda(){
        //Unsubscribe the soda before killing it(dunno if this matters but it feels like good practice)
        currSoda.OnDurationEnd -= RemoveSoda;
        currSoda.OnThrow -= RemoveSoda;

        //Remove the current soda from the queue list
        queue.Remove(currSoda);
        //Equip the next soda in Queue, or send as null if there's no soda left
        EquipSoda(queue.Count > 0 ? queue[0] : null);
    }

    /*Get the list of Soda class references in the queue*/
    public List<Soda> get_Queue() {
        Debug.Log(queue);
        return queue;
    }
    /*Get the Soda that's at the top of the list*/
    public Soda get_Soda() {
        return currSoda;
    }
}
