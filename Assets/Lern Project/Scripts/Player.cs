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
        [HideInInspector] public int level = 1;// HideInInspector �������� ������� ������ � ���������� �� �� � ����� ����


        public float speed = 2f; // �������� ��������, � � ���������� ���������
        private Vector3 _direction; // ����������� ��������
        public float speedRotate = 20f;
        private bool _isSprint;

                      
        void Update()
        {
            if (Input.GetMouseButtonDown(1))
                _isSpawnShield = true; //(1) ��������, ������ �� ����� ���, ����� �� ������

            _direction.x = Input.GetAxis("Horizontal");// ����������� ������
            _direction.z = Input.GetAxis("Vertical");// ����������� ������

            

        }

        private void FixedUpdate() // �������� ����� ������ � FixeUpdate
        {
            
            if (_isSpawnShield)
            {
                _isSpawnShield = false;//(3) ��������� ����� �� ��������� ���� �� ������, ���� ��, �� ��� �� ���������
                SpawnShield(); //(2) ������� ���
            }

            Move(Time.fixedDeltaTime);

            transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * speedRotate * Time.fixedDeltaTime , 0)); // ��������� ������� ���������
        }

        
        private void SpawnShield()
        {
            var shieldObj = Instantiate(ShieldPrefab, SpawnPosition.position, SpawnPosition.rotation);
            var shield = shieldObj.GetComponent<Shield>(); // ������� ������ �� ��������� ������ ( ����)
            shield.Init(10 * level);
            shield.transform.SetParent(SpawnPosition);// ���������� ���������� � �������
        }

        private void Move(float delta) // ������ ��������
        {
            var fixedDirection = transform.TransformDirection(_direction.normalized); // ����� ��������� � ��������� ��������
            transform.position += fixedDirection * (_isSprint ? speed * 2 : speed) * delta; // delta ����� ��������, ����� �� ���� ������� // transform.position - ������� �������
            // normalized ������ ���������� �������� ��� ��� �������� �� �����������, ��� � �� ����������
        }
    }

}
