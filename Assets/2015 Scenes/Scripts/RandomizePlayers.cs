using UnityEngine;
using System.Collections;

public class RandomizePlayers : MonoBehaviour {

    public RuntimeAnimatorController lightHairMale;
    public RuntimeAnimatorController lightHairFemale;
    public RuntimeAnimatorController darkHairMale;
    public RuntimeAnimatorController darkHairFemale;
	public GameObject player1;
	public GameObject player2;

    void Awake()
    {
        // Maybe swap which player is which
        if (Random.value >= 0.5f)
        {
            GameObject buffer = player2;
            player2 = player1;
            player1 = buffer;
        }
        Animator player1Anim = player1.GetComponent<Animator>();
        Animator player2Anim = player2.GetComponent<Animator>();
        PlayerController p1Control = player1.GetComponent<PlayerController>();
        PlayerController p2Control = player2.GetComponent<PlayerController>();

        // Randomize player1
        if (Random.value >= 0.5f)
        {
            player1Anim.runtimeAnimatorController = lightHairMale;
            p1Control.gender = PlayerController.Gender.M;
        }
        else
        {
            player1Anim.runtimeAnimatorController = lightHairFemale;
            p1Control.gender = PlayerController.Gender.F;
        }

        // Randomize player2
        if (Random.value >= 0.5f)
        {
            player2Anim.runtimeAnimatorController = darkHairMale;
            p2Control.gender = PlayerController.Gender.M;
        }
        else
        {
            player2Anim.runtimeAnimatorController = darkHairFemale;
            p2Control.gender = PlayerController.Gender.F;
        }
    }
}
