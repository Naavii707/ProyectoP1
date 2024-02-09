using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    public GameObject[] slots;
    Text text;

    private int numSlotsMax = 59;

    // Start is called before the first frame update
    void Start()
    {
        slots = new GameObject[numSlotsMax];
        for (int i = 0; i < numSlotsMax; i++)
        {
            slots[i] = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool removeItems(Component[] inventario)
    {
        for (int e = 1; e<inventario.Length; e++)
        {
            GameObject child = inventario[e].gameObject;
            if ( child.tag == "slot" && child.transform.childCount > 0)
            {
                for (int a = 0; a < child.transform.childCount; a++)
                {
                    Destroy(child.transform.GetChild(a).gameObject);
                }
            }
        }

        return true;
    }

    public GameObject[] getSlots()
    {
        return this.slots;
    }

    public void setSlot(GameObject slot, int pos, int cant)
    {
        bool exist = false;

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i] != null)
            {
                if (slots[i].tag == slot.tag)
                {
                    int alreadyCant = slots[i].GetComponent<AttributeController>().getCantidad();
                    this.slots[i].GetComponent<AttributeController>().setCantidad(alreadyCant);
                    exist = true;
                }
            }
        }

        if (!exist)
        {
            Debug.Log(cant);
            slot.GetComponent<AttributeController>().setCantidad(cant); 
            this.slots[pos] = slot;
        }
    }


    public void showInventory()
    {
        //Hace referencia al inventario y a todos los hijos de ese 
        Component[] inventario = GameObject.FindGameObjectWithTag("Inventario").GetComponentsInChildren<Transform>();
        bool slotUsed = false;

        if (removeItems(inventario)) 
        { 
            for(int i = 0;i < slots.Length;i++) 
            { 
                if (slots[i] != null) 
                {
                    slotUsed = false;
                    
                    for (int e = 0; e< inventario.Length;e++)
                    {
                        GameObject child = inventario[e].gameObject;

                        //si el slot no tiene hijos y es menor o igual a uno entonces significa que podemos utilizar el slot
                        if (child.tag == "slot" && child.transform.childCount <=1 && !slotUsed)
                        {
                            //instanciamos el objeto
                            GameObject item = Instantiate(slots[i], child.transform.position, Quaternion.identity);
                            item.transform.SetParent (child.transform, false);
                            item.transform.localPosition = new Vector3(0, 0, 0);
                            item.name = item.name.Replace("Clone", "");
                            text = item.GetComponentInChildren<Text>();
                            int cant = 1;
                            text.text = cant + "";

                            slotUsed = true;
                        }
                    }
                }
            }
        }
    }
}
