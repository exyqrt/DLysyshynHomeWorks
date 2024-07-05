using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    public float detectionRadius = 10f; // Радиус обнаружения игрока
    public float attackDistance = 2f;   // Расстояние для атаки игрока
    public float moveSpeed = 3.5f;      // Скорость передвижения зомби
    public float rotationSpeed = 5f;    // Скорость поворота зомби
    public int damageCount = 10;        // Количество урона
    public AnimatorOverrideController attackAnimatorController; // Анимационный контроллер для атаки

    private Transform player;           // Ссылка на игрока
    private NavMeshAgent navMeshAgent;  // Ссылка на компонент навигации
    private ZombieSpawner spawner;      // Ссылка на спавнер для учета убитых зомби
    private Animator animator;          // Компонент анимации
    private bool isAttacking = false;   // Флаг для проверки атаки

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Поиск игрока по тегу
        navMeshAgent = GetComponent<NavMeshAgent>(); // Получение компонента навигации
        spawner = FindObjectOfType<ZombieSpawner>(); // Найти спавнер в сцене
        animator = GetComponent<Animator>(); // Получение компонента анимации

        if (player == null)
        {
            Debug.LogError("Player not found!");
        }

        if (navMeshAgent == null)
        {
            Debug.LogError("NavMeshAgent not found on Zombie!");
        }

        if (animator == null)
        {
            Debug.LogError("Animator not found on Zombie!");
        }
        else if (attackAnimatorController != null)
        {
            animator.runtimeAnimatorController = attackAnimatorController;
        }
    }

    private void Update()
    {
        if (player == null)
            return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= detectionRadius)
        {
            if (distance > attackDistance)
            {
                // Преследование игрока
                navMeshAgent.SetDestination(player.position);
                animator.SetFloat("Speed", moveSpeed); // Установка параметра скорости

                // Поворот зомби к игроку
                Vector3 direction = (player.position - transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

                // Сбросить флаг атаки, если зомби не в радиусе атаки
                isAttacking = false;
            }
            else if (!isAttacking)
            {
                // Остановить зомби перед атакой
                navMeshAgent.SetDestination(transform.position);
                animator.SetFloat("Speed", 0); // Остановка анимации движения

                // Установить флаг атаки
                isAttacking = true;

                // Проигрывание анимации атаки
                animator.SetTrigger("Attack");

                // Снять HP у игрока
                StartCoroutine(FindObjectOfType<PlayerManager>().Damage(damageCount));
            }
        }
        else
        {
            // Сбросить флаг атаки, если зомби больше не в радиусе атаки
            isAttacking = false;
            animator.SetFloat("Speed", 0); // Остановка анимации движения
        }
    }

    private void OnDestroy()
    {
        if (spawner != null)
        {
            spawner.ZombieKilled();
        }
    }
}




