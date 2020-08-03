

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;        //Allows us to use SceneManager
using UnityEngine.UI;

namespace Completed
{
    //Player inherits from MovingObject, our base class for objects that can move, Enemy also inherits from this.
    public class Player : MovingObject
    {
        public float restartLevelDelay = 1f;        //Delay time in seconds to restart level.
        public int healCrystal = 5;                //Number of points to add to player food points when picking up a food object.                                        
        public int attack = 4;                    //How much damage a player does to a wall when chopping it.
        public GameObject GameOver;
        //private Animator animator;                    //Used to store a reference to the Player's animator component.
        public int currentHealth;                            //Used to store player food points total during level.
        public Slider slider;
        private Inventory inventory;
        public GameObject Wun;
        public Text Ending;


        //Start overrides the Start function of MovingObject
        protected override void Start()
        {
            //Get a component reference to the Player's animator component
            //animator = GetComponent<Animator>();

            //Get the current food point total stored in GameManager.instance between levels.
            currentHealth = GameManager.instance.fullHealth;

            slider.value = currentHealth;
            //Healthpoints.text = "HP " + currentHealth.ToString() + "/" + GameManager.instance.fullHealth.ToString();
            
            //Call the Start function of the MovingObject base class.
            base.Start();

            inventory = new Inventory();
        }

       

        //This function is called when the behaviour becomes disabled or inactive.
        private void OnDisable()
        {
            //When Player object is disabled, store the current local food total in the GameManager so it can be re-loaded in next level.
            GameManager.instance.fullHealth = currentHealth;
        }


        private void Update()
        {
            //If it's not the player's turn, exit the function.
            if (!GameManager.instance.playersTurn) return;

            int horizontal = 0;      //Used to store the horizontal move direction.
            int vertical = 0;        //Used to store the vertical move direction.

            Debug.Log(horizontal.ToString() + vertical.ToString());
            //Get input from the input manager, round it to an integer and store in horizontal to set x axis move direction
            horizontal = (int)(Input.GetAxisRaw("Horizontal"));

            //Get input from the input manager, round it to an integer and store in vertical to set y axis move direction
            vertical = (int)(Input.GetAxisRaw("Vertical"));

            //Check if moving horizontally, if so set vertical to zero.
            if (horizontal != 0)
            {
                vertical = 0;
            }

            //Check if we have a non-zero value for horizontal or vertical
            if (horizontal != 0 || vertical != 0)
            {
                //Call AttemptMove passing in the generic parameter Wall, since that is what Player may interact with if they encounter one (by attacking it)
                //Pass in horizontal and vertical as parameters to specify the direction to move Player in.
                AttemptMove<Wall>(horizontal, vertical);
            }
        }

        //AttemptMove overrides the AttemptMove function in the base class MovingObject
        //AttemptMove takes a generic parameter T which for Player will be of the type Wall, it also takes integers for x and y direction to move in.
        protected override void AttemptMove<T>(int xDir, int yDir)
        {

            //Call the AttemptMove method of the base class, passing in the component T (in this case Wall) and x and y direction to move.
            base.AttemptMove<T>(xDir, yDir);

            //Hit allows us to reference the result of the Linecast done in Move.
            RaycastHit2D hit;

            //If Move returns true, meaning Player was able to move into an empty space.
            if (Move(xDir, yDir, out hit))
            {
                //Call RandomizeSfx of SoundManager to play the move sound, passing in two audio clips to choose from.
            }

            //Since the player has moved and lost food points, check if the game has ended.
            CheckIfGameOver();

            //Set the playersTurn boolean of GameManager to false now that players turn is over.
            GameManager.instance.playersTurn = false;
        }

        //OnCantMove overrides the abstract function OnCantMove in MovingObject.
        //It takes a generic parameter T which in the case of Player is a Wall which the player can attack and destroy.
        protected override void OnCantMove<T>(T component)
        {
            //Set hitWall to equal the component passed in as a parameter.
            Enemy attackenemy = component as Enemy;

            Debug.Log("attack");
            //Call the DamageWall function of the Wall we are hitting.
            attackenemy.DamageEnemy(attack);
            Debug.Log("ruattacking");

            //Set the attack trigger of the player's animation controller in order to play the player's attack animation.
            //animator.SetTrigger ("playerChop");
        }



        //OnTriggerEnter2D is sent when another object enters a trigger collider attached to this object (2D physics only).
        private void OnTriggerEnter2D(Collider2D other)
        {
            //Check if the tag of the trigger collided with is Exit.
            if (other.CompareTag("Exit"))
            {
                Debug.Log("unity is garbage");
                Wun.SetActive(true);
                Ending.text = "Score: " + GameManager.instance.fullHealth.ToString();
                //Disable the player object since level is over.
                enabled = false;
            }

            //Check if the tag of the trigger collided with is enemy.
            else if (other.tag == "Enemy")
            {

                Debug.Log("enemy2dhitbox");
                //Add pointsPerSoda to players food points total
                //currentHealth += healCrystal;


                //Disable the soda object the player collided with.
                //other.gameObject.SetActive(false);
            }
        }


        //Restart reloads the scene when called.



        //LoseFood is called when an enemy attacks the player.
        //It takes a parameter loss which specifies how many points to lose.
        public void LoseHealth(int loss)
        {
            //Set the trigger for the player animator to transition to the playerHit animation.
            //animator.SetTrigger("playerHit");

            //Subtract lost food points from the players total.
            currentHealth -= loss;

            slider.value=currentHealth;

            //Check to see if game has ended.
            CheckIfGameOver();
        }

        public void SetMaxHealth(int currentHealth)
        {
            slider.maxValue = GameManager.instance.fullHealth;
            slider.value = currentHealth;
        }

        public void SetHealth(int health)
        {
            slider.value = currentHealth;
        }

        //Move returns true if it is able to move and false if not. 
        //Move takes parameters for x direction, y direction and a RaycastHit2D to check collision.
        protected override bool Move(int xDir, int yDir, out RaycastHit2D hit)
        {
            //Store start position to move from, based on objects current transform position.
            Vector2 start = transform.position;

            // Calculate end position based on the direction parameters passed in when calling Move.
            Vector2 end = start + new Vector2(xDir, yDir);

            //Disable the boxCollider so that linecast doesn't hit this object's own collider.
            boxCollider.enabled = false;

            //Cast a line from start point to end point checking collision on blockingLayer.
            hit = Physics2D.Linecast(start, end, blockingLayer);

            //Re-enable boxCollider after linecast
            boxCollider.enabled = true;

            if (hit.transform != null && hit.transform.tag == "Enemy" && !isMoving)
            {
                Enemy enemy = hit.transform.GetComponent<Enemy>();
                enemy.DamageEnemy(attack);
                return true;
            }
            //Check if anything was hit
            if (hit.transform == null && !isMoving)
            {
                //placing the moving marker
                movingMarker.SetActive(true);
                movingMarker.transform.position = new Vector3(end.x, end.y, 0);

                //If nothing was hit, start SmoothMovement co-routine passing in the Vector2 end as destination
                StartCoroutine(SmoothMovement(end));

                //Return true to say that Move was successful
                return true;
            }

            //If something was hit, return false, Move was unsuccesful.
            return false;
        }

        //CheckIfGameOver checks if the player is out of food points and if so, ends the game.
        private void CheckIfGameOver()
        {
            //Check if health point total is less than or equal to zero.
            if (currentHealth <= 0)
            {

                //Call the GameOver function of GameManager.
                GameManager.instance.GameOver();

                GameOver.SetActive(true);
                //reset the game later
                //SceneManager.LoadScene(0);

            }
        }

        
        
    }
}