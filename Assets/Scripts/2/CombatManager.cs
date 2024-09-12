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


    // Start is called before the first frame update
    void Start()
    {
        StartPlayerTurn();
    }

    private void StartPlayerTurn()
    {
        Debug.Log("Player's turn!");
        playerTurn = true;
        // Aquí puedes poner lógica para habilitar las cartas o acciones del jugador
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
