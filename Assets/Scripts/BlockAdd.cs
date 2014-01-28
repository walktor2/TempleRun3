using UnityEngine;
using System.Collections;

public class BlockAdd : MonoBehaviour
{

    public Object myBlock;
    public Vector3 spawnLocation = new Vector3(0, 0, 2);
    int dlina_z = 0, dlina_x = 0, x, z;


    IEnumerator Start()
    {
        /*while (true)
        {*/
            dlina_z += Random.Range(10, 64);
            dlina_x += Random.Range(10, 64);

            for ( ; z < dlina_z; z += 2)
            {
                CreateBlock(z);
            }
            for ( ; x < dlina_x; x += 2)
            {
                CreateBlockX(x, dlina_x);
            }
            yield return null;
        //}
    }



    void CreateBlock(int z)
    {
        spawnLocation = new Vector3(0, 0, z + 2);
        GameObject temp = (GameObject)Instantiate(myBlock, spawnLocation, Quaternion.identity);
        temp.name = "Block_" + z;


    }

    void CreateBlockX(int x, int dlina_x)
    {
        spawnLocation = new Vector3(x + 2, 0, dlina_x);
        GameObject temp = (GameObject)Instantiate(myBlock, spawnLocation, Quaternion.identity);
        temp.name = "Block_" + z;
    }

}
