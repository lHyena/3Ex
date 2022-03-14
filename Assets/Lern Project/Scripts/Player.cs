using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LernProject
{
    public class Player : MonoBehaviour

    {
        public GameObject ShieldPrefab;
        public Transform SpawnPosition;
             
        
        private bool _isSpawnShield;
        [HideInInspector] public int level = 1;// HideInInspector скрывает уровень игрока в инспекторе но не в самой игре


        public float speed = 2f; // Скорость движения, а в дальнейшем ускорение
        private Vector3 _direction; // Направление движения
        public float speedRotate = 20f;
        private bool _isSprint;

                      
        void Update()
        {
            if (Input.GetMouseButtonDown(1))
                _isSpawnShield = true; //(1) проверка, создал ли игрок щит, нажал ли кнопку

            _direction.x = Input.GetAxis("Horizontal");// перемещение игрока
            _direction.z = Input.GetAxis("Vertical");// перемещение игрока

            

        }

        private void FixedUpdate() // Движения лучше делать в FixeUpdate
        {
            
            if (_isSpawnShield)
            {
                _isSpawnShield = false;//(3) проверяет успел ли нанестись урон по игроку, если да, то щит не сработает
                SpawnShield(); //(2) создает щит
            }

            Move(Time.fixedDeltaTime);

            transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * speedRotate * Time.fixedDeltaTime , 0)); // позволяет врящать персонажа
        }

        
        private void SpawnShield()
        {
            var shieldObj = Instantiate(ShieldPrefab, SpawnPosition.position, SpawnPosition.rotation);
            var shield = shieldObj.GetComponent<Shield>(); // получем ссылку на экземпляр класса ( щита)
            shield.Init(10 * level);
            shield.transform.SetParent(SpawnPosition);// постоянное нахождение с игроком
        }

        private void Move(float delta) // привод движения
        {
            var fixedDirection = transform.TransformDirection(_direction.normalized); // игрок двигается в куазанном вращении
            transform.position += fixedDirection * (_isSprint ? speed * 2 : speed) * delta; // delta делит движение, чтобы не было скачков // transform.position - текущая позиция
            // normalized делает одинаковую скорость как при движении по горизонтали, так и по диагоналям
        }
    }

}
