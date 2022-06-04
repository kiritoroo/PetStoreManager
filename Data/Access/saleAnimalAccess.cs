﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Entities.Models;

namespace Data.Access
{
    public class saleAnimalAccess
    {
        DataContext db = new DataContext();
        public IQueryable<saleAnimal> GetAll()
        {
            return this.db.saleAnimals;
        }

        public saleAnimal Find(int saleId, string animalId)
        {
            return this.db.saleAnimals.Find(saleId, animalId);
        }

        public void Add(saleAnimal _saleAnimal)
        {
            this.db.saleAnimals.Add(_saleAnimal);
            this.db.SaveChanges();
        }

        public void Modify(saleAnimal _saleAnimal)
        {
            this.db.Entry(_saleAnimal).State = EntityState.Modified;
            this.db.SaveChanges();
        }

        public void Delete(int saleId, string animalId)
        {
            saleAnimal _saleAnimal = this.Find(saleId, animalId);
            this.db.saleAnimals.Remove(_saleAnimal);
            this.db.SaveChanges();
        }

        public int GetTotalAnimalSale()
        {
            // Task Complete - Huy
            // Querry

            int total = 0;
            var data = from saleAnimal in db.saleAnimals
                       select saleAnimal;

            total = data.Count();
            return total;
        }
    }
}
