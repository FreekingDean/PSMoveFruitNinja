using UnityEngine;
using System.Collections;
using System;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject left;
    public GameObject right;
    public int hits;
    public int points;
    int chits;
    public Vector3 velocity;
    // Use this for initialization
    void OnCollisionEnter(Collision cursor)
    {
        if ((cursor.gameObject.name == "mouseCursor") || (cursor.gameObject.name == "moveMeCursor1") || (cursor.gameObject.name == "moveMeCursor2"))
            {
                chits++;
                if (chits == hits)
                {
                    Transform theClonedExplosion;
                    theClonedExplosion = Instantiate(left, transform.position, transform.rotation) as Transform;
                    //theClonedExplosion.rigidbody.velocity = transform.TransformDirection(Vector3.forward);
                    theClonedExplosion = Instantiate(right, transform.position, transform.rotation) as Transform;
                    //GameObject left2= GameObject.FindGameObjectWithTag("wml(clone)");
                    //left2.rigidbody.velocity = transform.rigidbody.velocity;
                    //theClonedExplosion.rigidbody.velocity = new Vector3(transform.rigidbody.velocity.x, transform.rigidbody.velocity.y, transform.rigidbody.velocity.z);
                    //theClonedExplosion.rigidbody.AddForce(transform.rigidbody.velocity.x, transform.rigidbody.velocity.y, transform.rigidbody.velocity.z);
                    //Instantiate(gameObject);
                    Destroy(gameObject);
					ScoreKeeper.updateScore(points);
					GameObject.Find("NinjaNoise").transform.audio.Play();
                } 
				else
				{
					GameObject.Find("AxeNoise").transform.audio.Play();
				}
            }        
    }
    void Start()
    {
        //1190 max height, 850 min height, 330 max distance for max height, 490 max distance for min height
        float RndH4ce=UnityEngine.Random.Range(850f, 1190f);//force to get the range of heights in top 2/3 of space
        float RndXCo =UnityEngine.Random.Range(-16f, 16f);//random point in x of space
        float MaxS4ce = Convert.ToSingle(490 - (((RndH4ce - 850) / 3.4) * 1.6));//max height's max distance - (((the height - min height, to make a 0 possible)/the amount of force needed for 1% height, to make a % height)*1% force needed for distance,to give the force of unneeded distance force), to get the max force for the specific height
        float MaxMobS4ce = Convert.ToSingle(((MaxS4ce / 100) * ((RndXCo + 16) / .32)));//(specific max distance/100, to give a % of max distance)*((the x point + 16, to keep the range but in positive numbers)/1% of new xpoint range, for the xpoint on a % scale), to give max distance with specific height and x point factored in
        float oneperL=(MaxMobS4ce + 30)/100;
        float oneperR = ((MaxS4ce - MaxMobS4ce - 30) / 100);
        float S4ceR = UnityEngine.Random.Range(0f, Convert.ToSingle(MaxS4ce - MaxMobS4ce - 30));//a number between 0 and the max distance with height - the current max distance(as mor force is needed for low heights then for high heights)-30 to go left
        float S4ceL = UnityEngine.Random.Range(Convert.ToSingle(-MaxMobS4ce + 30), 0f);//a number between thenegitive current max distance + 30 and 0 to go right
        float S4ce;
        transform.position = new Vector3(RndXCo, -16, -3);
        if (UnityEngine.Random.Range(0, 2) == 1)
            S4ce = S4ceL;
        else
            S4ce = S4ceR;
        if (RndXCo < -8)
            if ((S4ceR / oneperR) < 69)
                S4ce = S4ceR + (oneperR * 30);
            else
                S4ce = S4ceR;
        if (RndXCo > 8)
            if ((S4ceL / oneperL) > -69)
                S4ce = S4ceL - (oneperL * 30);
            else
                S4ce = S4ceL;
        rigidbody.AddForce(S4ce/*Convert.ToSingle(- for opp direc MaxS4ce -+ for opp direc MaxMobS4ce)*/, RndH4ce, 0);//1190 max height 850 min height 330 max distance for max height 490 max distance for min height UnityEngine.Random.Range(330f,1190f)
        //3.6 1.7 340 1.9 178.9 ((test/178.9)+1.7)*test
        
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.position = new Vector3(transform.position.x, transform.position.y, -3);
        if (transform.position.y > -10)
        {
            transform.collider.isTrigger = false;
        }
        else
        {
            transform.collider.isTrigger = true;
        }
        if (transform.position.y < -17 || transform.position.y > 20 || transform.position.x > 60 || transform.position.x < -60)
        {
            //transform.position = new Vector3(-16/*UnityEngine.Random.Range(-16f, 16f)*/, -16, -3);
            //Start();
            //Instantiate(gameObject);   //<--will make a new snowman in hiarchy as snowman(clone) with more (clone) each time may cause peoblems
            Destroy(gameObject);
        }
        //~ System.Random random = new System.Random(14);
        //~ float vertexY = random.Next(14);
        //~ int up = 0;
        //~ up++;
        //~ //guitext.text = up;
        //~ //y=-1x^2+(Random num 0-11)*x-15;
        //~ //transform.Translate(0, 1, 0,Space.Self);
        //~ if (transform.position.x < 17 )
        //~ {
            //~ transform.position = new Vector3(Convert.ToSingle(transform.position.x + .1)/*moving left to right*/, 
                //~ Convert.ToSingle/*converts to a float as integer isnt good enough and double causes errors*/
                //~ ((transform.position.x * transform.position.x * -1) + (1 * transform.position.x) + vertexY/*y or s positon of vertex seems to be offset by +2(total +3 for object height)*/)/*up and down*/,
                //~ -3/*front/back*/);
        //~ }
    }
}
/*The equation you wrote for the earth should be h = -16t2 + vt + s 
"s" is the starting height. "v" is the initial velocity (positive v means upwards). "h" is the height of the projectile (ball).

 
 
http://wiki.etc.cmu.edu/unity3d/index.php/Sony_PlayStation_Move

*/