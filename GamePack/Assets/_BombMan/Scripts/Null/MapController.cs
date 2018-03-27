using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 作者: Foldcc
/// QQ: 1813547935
/// 生成地图以及敌人
/// 根据原作者Foldcc基础，进行修改添加
/// </summary>

public class MapController : MonoBehaviour {
    //创建单例模式
    public static MapController _instance;

	public GameObject propsWall;

	public GameObject doorWall;

	public GameObject superWall;

	public GameObject wall;

	public GameObject[] enemy;

	private List<Vector2> posList = new List<Vector2> ();

	private static List<Vector2> superWallPosList = new List<Vector2>();
	//地图范围
	private int xitm;
	private int yitm;

	void Start(){
        _instance = this;

    }

	//创建地图 初始化地图 count：地图中生成可销毁wall的数量
	public void initMap(int count ,int enemyCount, int x , int y ){
		//清空list数据
		superWallPosList.Clear();
		posList.Clear ();
		this.xitm = x;
		this.yitm = y;
        //清理地图函数
		destoryMap ();
		border ();
		superWallController ();
		wallContorller (count);
		enemyGenerator (enemyCount);
	}

	//创建不可销毁的墙体superwall
	private void superWallController(){
		//y从1开始 为基数 x为偶数 从0 创建一个网格状地图

		for (int x = 0; x <= xitm; x+=2) {
			for (int y = 1; y <= yitm; y += 2) {
				Instantiate (superWall , new Vector2(x , y) , Quaternion.identity);
				Instantiate (superWall , new Vector2(x , -y) , Quaternion.identity);
				superWallPosList.Add (new Vector2(x , y));
				superWallPosList.Add (new Vector2(x , -y));
				if (x != 0) {
					Instantiate  (superWall , new Vector2(-x , -y) , Quaternion.identity);
					Instantiate  (superWall , new Vector2(-x , y) , Quaternion.identity);
					superWallPosList.Add (new Vector2(-x , -y));
					superWallPosList.Add (new Vector2(-x , y));
				}
			}
		}

	}

	//在地图中随机创建指定数量的wall
	private void wallContorller(int count){
		//遍历空的位置
		for (int x = 1; x <= xitm + 1; x += 2) {
			for (int y = 0; y <= yitm + 1; y += 2) {
				posList.Add (new Vector2(x , y));
				posList.Add (new Vector2(-x , y));
				posList.Add (new Vector2(x , -y));
				posList.Add (new Vector2(-x , -y));
			}
		}

		//去除主角生成位置 ， 防止墙体等生成到主角位置 造成死局
		posList.Remove (new Vector2(-(xitm+1), yitm+1));
		posList.Remove (new Vector2(-(xitm+1) , yitm));
		posList.Remove (new Vector2(xitm , yitm+1));

		//当设置的数量大于空位置的数量时 取空位置数量的百分之七十
		if(count >= posList.Count){
			count = (int)(posList.Count * 0.7f);
		}

		//随机产生count个wall在空的位置中
		for(int k = 0; k < count ; k++){
			int x = Random.Range (0 , posList.Count);
			Instantiate (wall , posList[x] , Quaternion.identity);
			posList.RemoveAt (x);
		}

		//随机产生一个门
		int d = Random.Range (0 , posList.Count);
		Instantiate (doorWall , posList[d] , Quaternion.identity);
		posList.RemoveAt (d);

		//随机产生道具
		if(count >= 10){
			int s = Random.Range (0 , (int)(2+count*0.1f));
			for(int kp = 0; kp < s ; kp++){
				int x = Random.Range (0 , posList.Count);
				Instantiate (propsWall , posList[x] , Quaternion.identity);
				posList.RemoveAt (x);
			}
		}
	}

	//创建边界
	private void border(){
		for (int i = -(xitm +2); i <= xitm+2; i++) {
			if (i == xitm+2) {
				for (int j = -yitm-1; j <= (yitm+1); j++) {
					Instantiate (superWall , new Vector2(i , j) , Quaternion.identity);
					Instantiate (superWall , new Vector2(-i , j) , Quaternion.identity);
					superWallPosList.Add (new Vector2(i , j));
					superWallPosList.Add (new Vector2(-i , j));
				}
			}

			Instantiate (superWall , new Vector2(i , yitm+2) , Quaternion.identity);
			Instantiate (superWall , new Vector2(i , -(yitm+2)) , Quaternion.identity);
			superWallPosList.Add (new Vector2(i , yitm+2));
			superWallPosList.Add (new Vector2(i , -(yitm+2)));
		}
	}

	//创建敌人
	public void enemyGenerator(int enemyCount){
		
		
		//创建敌人到随机位置
		for (int i = 0; i < enemyCount; i++) {
			int x = Random.Range (0 , posList.Count);
			Instantiate (enemy[Random.Range(0,7)] , posList[x] , Quaternion.identity);
            
		}

        UIController.enemyCount = enemyCount;
        
    }

    //销毁场景中的物体
    public void destoryMap()
    {
        //清空怪物
        GameObject[] enemys1 = GameObject.FindGameObjectsWithTag("Mon1");
        GameObject[] enemys2 = GameObject.FindGameObjectsWithTag("Mon2");
        GameObject[] enemys3 = GameObject.FindGameObjectsWithTag("Mon3");
        GameObject[] enemys4 = GameObject.FindGameObjectsWithTag("Mon4");
        GameObject[] enemys5 = GameObject.FindGameObjectsWithTag("Mon5");
        GameObject[] enemys6 = GameObject.FindGameObjectsWithTag("Mon6");
        GameObject[] enemys7 = GameObject.FindGameObjectsWithTag("Mon7");
        //清空怪物
        GameObject[] walls = GameObject.FindGameObjectsWithTag("WallBomb");
        GameObject[] swalls = GameObject.FindGameObjectsWithTag("Wall");
        GameObject[] boo = GameObject.FindGameObjectsWithTag("Boom");
        GameObject[] boe = GameObject.FindGameObjectsWithTag("Bomb");
        GameObject[] props = GameObject.FindGameObjectsWithTag("Props");
        GameObject door = GameObject.FindGameObjectWithTag("Door");
        GameObject doorwall = GameObject.FindGameObjectWithTag("DoorWall");

        Destroy(door);
        Destroy(doorwall);
        //清空怪物
        foreach (GameObject w in enemys1)
        {
            Destroy(w);
        }
        foreach (GameObject w in enemys2)
        {
            Destroy(w);
        }
        foreach (GameObject w in enemys3)
        {
            Destroy(w);
        }
        foreach (GameObject w in enemys4)
        {
            Destroy(w);
        }
        foreach (GameObject w in enemys5)
        {
            Destroy(w);
        }
        foreach (GameObject w in enemys6)
        {
            Destroy(w);
        }
        foreach (GameObject w in enemys7)
        {
            Destroy(w);
        }
        //清空怪物
        foreach (GameObject w in walls)
        {
            Destroy(w);
        }

        foreach (GameObject w in swalls)
        {
            Destroy(w);
        }

        foreach (GameObject w in boo)
        {
            Destroy(w);
        }

        foreach (GameObject w in boe)
        {
            Destroy(w);
        }
        foreach (GameObject w in props)
        {
            Destroy(w);
        }
    }

    //用于判断制定位置是否有superWall
    public static bool isSuperWallPos(Vector2 ve){
		bool s = false;
		foreach (Vector2 v in superWallPosList) {
			if (v.x == ve.x && v.y == ve.y) {
				s = true;
				break;
			}
		}
		return s;
	}
}
