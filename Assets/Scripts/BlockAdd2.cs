using UnityEngine;
using System.Collections;

public class BlockAdd2 : MonoBehaviour {

	public GameObject myBlock, obstacle, coin; //блок для генерації, перешкода і монетка
    public int obstacleChance = 3; //шанс створення перешкоди, задається в відсотках
    public int minBlocks = 10, maxBlocks = 20, limitOfCreateBlocks = 100; //мінімальна і максимальна кількість блоків в лінії, дальність створення блоків
	public int coinChance = 50; //шанс створення монети, задається в відсотках
	public int minCoin = 20, maxCoin = 50; //мінімальна і максимальна кількість монет

    private GameObject player; //наш герой
    private int lenght, dirChanger; //довжина лінії, і змінювач напрямного вектора
    private Vector3 lastBlock = Vector3.zero, lastBlockDir = Vector3.forward; //останній блок, і напрямний вектор останнього блоку

    IEnumerator Start()
    {
        //шукаємо героя, щоб в подальшому звіряти координати
        player = GameObject.FindGameObjectWithTag("Player");

        //вічний цикл
        while (true)
        {
            //якщо герой не є задалеко від останнього блоку, тоді генеруємо світ
            if (Mathf.Abs(lastBlock.z - player.transform.position.z) < limitOfCreateBlocks || Mathf.Abs(lastBlock.x - player.transform.position.x) < limitOfCreateBlocks)
            {
                //визначаємо довжину блоків
                lenght = Random.Range(minBlocks, maxBlocks + 1);

                //створюємо лінію з блоків
                for (int i = 0; i < lenght; i++)
                {
                    CreateBlock(lastBlockDir);
                }

                //визначаємо напрямний вектор після постройки лінії(напрямний вектор може бути(вправо, вліво, вперед))
                dirChanger = Random.Range(-1, 2); 
                switch (dirChanger)
                {
                    case -1: lastBlockDir = Vector3.left; break;
                    case 0: lastBlockDir = Vector3.forward; break;
                    case 1: lastBlockDir = Vector3.right; break;
                }
            }
            //чекаємо 200 мс, перед створенням наступної лінії з блоків
            yield return new WaitForSeconds(0.2f);
        }
    }
    void CreateBlock(Vector3 dir) //функція створює блок з заданим напрямним вектором
    {
        GameObject temp = (GameObject)Instantiate(myBlock, lastBlock + dir * 2, Quaternion.identity);
        temp.name = "Block_" + temp.transform.position.x + "_" + temp.transform.position.z;

        //записуємо позицію та напрямний вектор останнього блоку
        lastBlock = temp.transform.position;
        lastBlockDir = dir;

        //якщо в нас задана перешкода, то створюємо її з деяким шансом
        if(obstacle != null)
            if (Random.Range(-100, obstacleChance + 1) > 0) //шанс створення перешкоди
                CreateObstacle(temp.transform.position, temp);

		//якщо в нас задана монета, то створюємо її з деяким шансом
		if(coin != null)
			if (Random.Range(-100, coinChance + 1) > 0) //шанс створення монети
				CreateCoin(temp.transform.position, temp);
    }
    void CreateObstacle(Vector3 pos, GameObject block) //функція створює перешкоду
    {
        GameObject temp = (GameObject)Instantiate(obstacle, pos + Vector3.up, Quaternion.identity);
        temp.name = "Obstacle";
        //робимо перешкоду дочірньою до блоку, на якому вона стоїть
        temp.transform.parent = block.transform;
    }
	void CreateCoin(Vector3 pos, GameObject block) //функція створює монету
	{
		GameObject temp = (GameObject)Instantiate(coin, pos + Vector3.up, Quaternion.identity);
		temp.name = "Coin";
		//робимо монету дочірньою до блоку, на якому вона стоїть
		temp.transform.parent = block.transform;
	}
}
