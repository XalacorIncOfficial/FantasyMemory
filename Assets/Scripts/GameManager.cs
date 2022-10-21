using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    // Variablen
    public GameObject[] allCards;
    public List<Vector3> allPositions = new List<Vector3>();                                                // Position in der Liste ist ein Vektor; Listen als Variablen müssen IMMER initialisiert werden

    private MemoryCard firstSelectedCard;
    private MemoryCard secondSelectedCard;

    public AudioSource audioSource;
    public AudioClip clipCardUp;
    public AudioClip clipCardDown;
    public AudioClip clipCardMatch;

    private bool canClick = true;                                                                           // wir können eine Karte anklicken

    // Methoden
    private void Awake()
    {
        // I.: Get all card positions and save in list
        foreach(GameObject card in allCards)
        {
            allPositions.Add(card.transform.position);                                                      // Speicherung der Kartenposition in die Liste
        }

        // II.: Randomize positions
        System.Random randomNumber = new System.Random();
        allPositions = allPositions.OrderBy(position => randomNumber.Next()).ToList();

        // III.: Assign new positions
        for (int i = 0; i < allCards.Length; i++)
        {
            allCards[i].transform.position = allPositions[i];
        }

    }

    public void CardClicked(MemoryCard card)
    {
        if (canClick == false || card == firstSelectedCard)
        {
            return;
        }

        // Always rotate card up to show its image
        card.targetRotation = 90;
        card.targetHeight = 0.05f;
        audioSource.PlayOneShot(clipCardUp);


        // Second card clicked
        if (firstSelectedCard == null)
        {
            firstSelectedCard = card;                                                                       // firstSelctedCard ist die Karte, die ich angeklickt habe und der Wert dieser Variablen ist die Karte die ich aus der Methode übergeben bekomme
        }
        else
        {
            // Second card clicked
            secondSelectedCard = card;

            canClick = false;                                                                               // ich kann nicht mehr klicken

            Invoke("CheckMatch", 1);
        }                          
    }

    public void CheckMatch()
    {
        // Result
        if (firstSelectedCard.identifier == secondSelectedCard.identifier)
        {
            Destroy(firstSelectedCard.gameObject);
            Destroy(secondSelectedCard.gameObject);

            audioSource.PlayOneShot(clipCardMatch);
        }
        else
        {
            firstSelectedCard.targetRotation = -90;
            secondSelectedCard.targetRotation = -90;

            firstSelectedCard.targetHeight = 0.01f;
            secondSelectedCard.targetHeight = 0.01f;

            audioSource.PlayOneShot(clipCardDown);
        }

        // Reset
        firstSelectedCard = null;
        secondSelectedCard = null;

        canClick = true;                                                                                    // ich kann wieder klicken
    }
}
