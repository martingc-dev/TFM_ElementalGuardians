using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CombatManager : MonoBehaviour
{

    public PlayerEnergy player;
    public List<Enemy2> enemies = new List<Enemy2>();
    private int currentEnemyIndex = 0;
    private bool playerTurn = true;

    //Referencia al mazo del jugador
    public Deck2 playerDeck;
    // Referencia al ScriptableObject con el mazo inicial
    public StartingDeck startingDeck;

   public GameManager gameManager;


    // Start is called before the first frame update
    void Start()
    {
        StartBattle();
    }

    /// <summary>
    /// Metodo que inicia la batalla
    /// </summary>
    public void StartBattle()
    {

        if (startingDeck != null)
        {
            //Inicializa el mazo del jugador
            playerDeck.InitializeDeck(startingDeck.cards);
        }
        else
        {
            Debug.LogError("StartingDeck no asignado en BattleManager.");
        }
        StartPlayerTurn();
    }

    private void StartPlayerTurn()
    {
        Debug.Log("Player's turn!");
        playerTurn = true;
        //Descarta las cartas actuales de la mano
        playerDeck.DiscardHand();
        //Roba ua nueva mano
        DrawHand();
        // Aquí puedes poner lógica para habilitar las cartas o acciones del jugador
    }

    /// <summary>
    /// metodo para robar una mano  completa
    /// </summary>
    public void DrawHand()
    {
        for (int i = 0; i < playerDeck.handSize; i++)
        {
            playerDeck.DrawCard();
        }

        //actualiza la interfaz de usuario para mostrar las cartas robadas en la mano
        UpdateHandUI();
        Debug.Log("Cartas robadas y mano actualizada.");
    }


    /// <summary>
    /// Metodo que actualiza la inerfaz de la mano del juagdor
    /// </summary>
    private void UpdateHandUI()
    {
        if (gameManager != null)
        {
            gameManager.DisplayHand();
        }
        else
        {
            Debug.LogWarning("uiManager no está asignado en BattleManager.");
        }
        Debug.Log("Mano actualizada.");
    }
    public void EndPlayerTurn()
    {
        Debug.Log("Player's turn ends.");
        playerTurn = false;
        StartEnemyTurn();
    }

    private void StartEnemyTurn()
    {
        Debug.Log("Enemies' turn!");
        foreach (Enemy2 enemy in enemies)
        {
            enemy.AttackPlayer(player);  // Cada enemigo ataca al jugador
            enemy.OnTurnStart();
        }
        EndEnemyTurn();
    }

    // Termina el turno de los enemigos
    void EndEnemyTurn()
    {
        Debug.Log("Enemies' turn ends.");
       
        StartPlayerTurn();
    }

    // Método para añadir un enemigo al combate
    public void AddEnemy(Enemy2 enemy)
    {
        enemies.Add(enemy);
    }

    // Método para eliminar un enemigo del combate cuando muere
    public void RemoveEnemy(Enemy2 enemy)
    {
        if (enemies.Contains(enemy))
        {
            enemies.Remove(enemy);
            Debug.Log("Enemy removed from battle");
        }

        if (enemies.Count == 0)
        {
            Debug.Log("All enemies defeated. Victory!");
            // Aquí puedes agregar lógica para terminar el combate
        }
    }
}
