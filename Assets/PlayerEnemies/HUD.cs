

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;        //Allows us to use SceneManager
using UnityEngine.UI;

namespace Completed
{
    //Player inherits from MovingObject, our base class for objects that can move, Enemy also inherits from this.
    public class HUD : MovingObject
    {
        public float restartLevelDelay = 1f;        //Delay time in seconds to restart level.                     
        public int attack = 4;                    //How much damage a player does to a wall when chopping it.
        public int healCrystal = 5;                //Number of points to add to player food points when picking up a food object.             
        //private Animator animator;                    //Used to store a reference to the Player's animator component.
        public int currentHealth;                            //Used to store player food points total during level.
        public Text Floor;
        public Text atkText;
        public Text Healthpoints;

        //Start overrides the Start function of MovingObject
        protected override void Start()
        {
            //Get a component reference to the Player's animator component
            //animator = GetComponent<Animator>();

            //Get the current food point total stored in GameManager.instance between levels.
            currentHealth = GameManager.instance.fullHealth;
            HUDsetup();
            Healthpoints.text = "HP " + currentHealth.ToString() + "/" + GameManager.instance.fullHealth.ToString();
            //Call the Start function of the MovingObject base class.
            base.Start();
        }


        //This function is called when the behaviour becomes disabled or inactive.
        private void OnDisable()
        {
            //When Player object is disabled, store the current local food total in the GameManager so it can be re-loaded in next level.
            GameManager.instance.fullHealth = currentHealth;
        }


        private void Update()
        {

            

        }

        //AttemptMove overrides the AttemptMove function in the base class MovingObject
        //AttemptMove takes a generic parameter T which for Player will be of the type Wall, it also takes integers for x and y direction to move in.
        protected override void AttemptMove<T>(int xDir, int yDir)
        {


        }

        //OnCantMove overrides the abstract function OnCantMove in MovingObject.
        //It takes a generic parameter T which in the case of Player is a Wall which the player can attack and destroy.
        protected override void OnCantMove<T>(T component)
        {
            
        }

        public void HUDsetup()
        {
            atkText.text = "Atk " + attack.ToString();
            Healthpoints.text = "HP " + currentHealth.ToString() + "/" + GameManager.instance.fullHealth.ToString();
            Floor.text = "Floor 1";
        }


        //OnTriggerEnter2D is sent when another object enters a trigger collider attached to this object (2D physics only).
        private void OnTriggerEnter2D(Collider2D other)
        {


            //Check if the tag of the trigger collided with is Food.
            if (other.CompareTag("healCrystal"))
            {
                //Add pointsPerFood to the players current food total.
                currentHealth += healCrystal;

                //Disable the food object the player collided with.
                other.gameObject.SetActive(false);

                Healthpoints.text = "HP " + currentHealth.ToString() + "/" + GameManager.instance.fullHealth.ToString();
                //Debug.Log("HP " + currentHealth.ToString() + "/" + GameManager.instance.fullHealth.ToString());
            }

            //Check if the tag of the trigger collided with is enemy.

        }


        //LoseFood is called when an enemy attacks the player.
        //It takes a parameter loss which specifies how many points to lose.
        public void LoseHealth(int loss)
        {
            //Set the trigger for the player animator to transition to the playerHit animation.
            //animator.SetTrigger("playerHit");

            //Subtract lost health points from the players total.
            currentHealth -= loss;

            Healthpoints.text = "HP " + currentHealth.ToString() + "/" + GameManager.instance.fullHealth.ToString();
            Debug.Log("HP " + currentHealth.ToString() + "/" + GameManager.instance.fullHealth.ToString());

        }





    }
}