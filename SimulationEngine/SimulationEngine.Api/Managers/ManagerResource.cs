﻿using SimulationEngine.Api.Datas;
using SimulationEngine.Api.Models;
using SimulationEngine.Api.Models.Interfaces;

namespace SimulationEngine.Api.Managers
{
    public static class ManagerResource<TResource> where TResource : Resource, new()
    {
        public static void CreateResource(int qty)
        {
            for (int i = 0; i < qty; i++)
                new TResource();
        }

        private static List<TResource> listResourcesFree()
        {
            var resourcesInfoToLive = ManagerDatas<TResource>.ListToAlive();
            var resourcesFree = new List<TResource>();

            foreach(var info in resourcesInfoToLive)
            {
                if (info.Instance.Allocated == false)
                    resourcesFree.Add(info.Instance);
            }

            return resourcesFree;
        }

        public static bool CheckAvailability(int qty)
        {
            var resourcesFree = listResourcesFree();
            return resourcesFree.Count >= qty;
        }

        public static IEnumerable<IAllocatedManager<TResource>> Allocated(int qty)
        {
            var resourcesFree = listResourcesFree();

            if (resourcesFree.Count < qty)
                throw new Exception("Não existem recursos livres.");

            var newAllocateds = new List<IAllocatedManager<TResource>>();

            for(int i = 0; i < qty; i++)
            {
                var newAllocated = new AllocatedManager<TResource>(resourcesFree[i]);
                newAllocateds.Add(newAllocated);
            }

            return newAllocateds;
        }

        public static void Deallocate(IEnumerable<IAllocatedManager<TResource>> allocatedManagers)
        {
            foreach(var allocated in allocatedManagers)
                allocated.Deallocate();
        }
    }
}