using NUnit.Framework;
using SmartHome;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Phone
{
    public class PhoneScreen : MonoSingleton<PhoneScreen>
    {
        [SerializeField] private BlockSwither blockSwitherPrefab;
        [SerializeField] private List<Block> blockSwithers = new List<Block>();


        [SerializeField] private BlockDoor blockDoorPrefab;
        [SerializeField] private BlockDoor blockDoor;

        [SerializeField] private BlockLamp blockLampPrefab;
        [SerializeField] private List<Block> blockLamps = new List<Block>();

        [SerializeField] private BlockGate blockGatePrefab;
        [SerializeField] private List<Block> blockGates = new List<Block>();



        [SerializeField] private Transform content;


        void Start()
        {
            //TODO; переделать на нормальный Init()
            StartCoroutine(DelayedStart());
        }

        IEnumerator DelayedStart()
        {
            yield return new WaitForSeconds(1);
            SpawnSwithers();
            SpawnDoor();
            SpawnLamps();
            SpawnGates();
        }

        // Update is called once per frame
        void Update()
        {

        }

        void SpawnSwithers()
        {
            SpawnCollection(PowerSource.Instance.GetSwithers, blockSwithers, blockSwitherPrefab);
        }

        void SpawnLamps()
        {
            SpawnCollection(PowerSource.Instance.GetLamps, blockLamps, blockLampPrefab);
        }

        void SpawnDoor()
        {

            BlockDoor b_door = Instantiate(blockDoorPrefab, content);
            b_door.Init(PowerSource.Instance.GetDoor);

            blockDoor =b_door;
        }

        void SpawnGates()
        {
            SpawnCollection(PowerSource.Instance.GetGates, blockGates, blockGatePrefab);
        }

        void SpawnCollection(List<ElectricObject> collection, List<Block>toList, Block prefab)
        {
            foreach (var gate in collection)
            {
                Block b_gate = Instantiate(prefab, content);
                b_gate.Init(gate);

                toList.Add(b_gate);
            }
        }
    }
}
