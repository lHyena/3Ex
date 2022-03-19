using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LernProject
{
    public class Enemy : MonoBehaviour , ITakeDamage
    {
        [SerializeField] private Player _player; // ���������� ������
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private Transform _spawnPosition;
        private Vector3 _direction; // ����������� ����� *
        public float speed = 1.5f; // �������� �����
        [SerializeField] private float _durability = 1f; //*

        void Start()
        {
            _player = FindObjectOfType<Player>();// ��� ������������� �����, �� ��� ������� ���-�� �������, ����� ��������� ����


        }

        private void Update()
        {

            transform.LookAt(_player.transform); // ������� ���������� (���������)�� ������
            transform.Translate(Vector3.forward * Time.deltaTime * speed); // ��������� �� �������. ��������.

            if (Vector3.Distance(transform.position, _player.transform.position) < 6)
            {
                if (Input.GetKeyDown(KeyCode.F))
                    Fire();
            }
        }

        private void Fire()
        {
            var shieldObj = Instantiate(_bulletPrefab, _spawnPosition.position, _spawnPosition.rotation);
            var shield = shieldObj.GetComponent<Bullet>(); // ������� ������ �� ��������� ������ ( ����)
            shield.Init(_player.transform, 10, 0.6f);

        }

        public void Init(float durability) //*
        {
            _durability = durability;

            Destroy(gameObject, t: 1f);
        }

        public void Hit(float damage) //����������� *
        {
            _durability -= damage;

            if (_durability <= 0)
            {
                Destroy(gameObject);
            }
        }


    }
}




