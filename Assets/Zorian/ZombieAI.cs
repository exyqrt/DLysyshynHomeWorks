using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class ZombieAI : MonoBehaviour
{
    public float detectionRadius = 10f; // Радіус виявлення гравця
    public float attackDistance = 2f;   // Відстань для атаки гравця
    public float moveSpeed = 3.5f;      // Швидкість пересування зомбі
    public float rotationSpeed = 5f;    // Швидкість повороту зомбі
    public int damageCount = 10;        // Кількість ушкоджень
    public AnimatorOverrideController attackAnimatorController; // Анімаційний контролер для атаки

    private Transform Player;           // Посилання на гравця
    private NavMeshAgent navMeshAgent;  // Посилання на компонент навігації
    private ZombieSpawner spawner;      // Посилання на спавнер для обліку вбитих зомбі
    private Animator animator;          // Компонент анімації
    private bool isAttacking = false;   // Прапор для перевірки атаки

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform; // Пошук гравця за тегом
        navMeshAgent = GetComponent<NavMeshAgent>(); // Отримання компонента навігації
        spawner = FindObjectOfType<ZombieSpawner>(); // Знайти спавнер на сцені
        animator = GetComponent<Animator>(); // Отримання компонента анімації

        if (Player == null)
        {
            Debug.LogError("Гравець не знайдений!");
        }

        if (navMeshAgent == null)
        {
            Debug.LogError("NavMeshAgent не знайдений на зомбі!");
        }

        if (!navMeshAgent.isActiveAndEnabled)
        {
            navMeshAgent.enabled = true;
        }

        if (animator == null)
        {
            Debug.LogError("Аніматор не знайдений на зомбі!");
        }
        else if (attackAnimatorController != null)
        {
            animator.runtimeAnimatorController = attackAnimatorController;
        }
    }

    private void Update()
    {
        if (Player == null || navMeshAgent == null)
            return;

        float distance = Vector3.Distance(transform.position, Player.position);

        if (distance <= detectionRadius)
        {
            if (distance > attackDistance)
            {
                // Переслідування гравця
                if (navMeshAgent.isOnNavMesh)
                {
                    navMeshAgent.SetDestination(Player.position);
                }
                animator.SetFloat("Speed", moveSpeed); // Встановлення параметра швидкості

                // Поворот зомбі до гравця
                Vector3 direction = (Player.position - transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

                // Скидання прапора атаки, якщо зомбі не в радіусі атаки
                isAttacking = false;
            }
            else if (!isAttacking)
            {
                // Зупинка зомбі перед атакою
                if (navMeshAgent.isOnNavMesh)
                {
                    navMeshAgent.SetDestination(transform.position);
                }
                animator.SetFloat("Speed", 0); // Зупинка анімації руху

                // Встановлення прапора атаки
                isAttacking = true;

                // Програвання анімації атаки
                animator.SetTrigger("Attack");

                // Зняття HP у гравця
                StartCoroutine(FindObjectOfType<PlayerManager>().Damage(damageCount));
            }
        }
        else
        {
            // Скидання прапора атаки, якщо зомбі більше не в радіусі атаки
            isAttacking = false;
            animator.SetFloat("Speed", 0); // Зупинка анімації руху
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