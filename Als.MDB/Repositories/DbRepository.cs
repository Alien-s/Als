using Als.Interfaces;
using Als.MDB.Context;
using Als.MDB.Entities.BaseEntity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Als.MDB.Repositories
{
    internal class DbRepository<T> : IRepository<T> where T : Entity, new()
    {
        //Field for Database Context
        private readonly AlsDB _db;

        //Property of the Collection of Data for Current Repository
        private readonly DbSet<T> _Set;

        //Save or NOT save the changes at Database
        public bool AutoSaveChanges { get; set; } = true;


        //Constructor => Request for Database Context
        public DbRepository(AlsDB dB)
        {
            _db = dB;
            _Set = dB.Set<T>();
        }


        public virtual IQueryable<T> Items => _Set;


        public T Get(int id) => Items.SingleOrDefault(item => item.Id == id);

        public async Task<T> GetAsync(int id, CancellationToken Cancel = default) => await Items
            .SingleOrDefaultAsync(item => item.Id == id, Cancel)
            .ConfigureAwait(false);


        public T Add(T item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));

            _db.Entry(item).State = EntityState.Added;
            if (AutoSaveChanges)
            {
                _db.SaveChanges();
            }

            return item;
        }

        public async Task<T> AddAsync(T item, CancellationToken Cancel = default)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));

            _db.Entry(item).State = EntityState.Added;
            if (AutoSaveChanges)
            {
                await _db.SaveChangesAsync(Cancel).ConfigureAwait(false);
            }

            return item;
        }


        public void Update(T item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));

            _db.Entry(item).State = EntityState.Modified;
            if (AutoSaveChanges)
            {
                _db.SaveChanges();
            }
        }

        public async Task UpdateAsync(T item, CancellationToken Cancel = default)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));

            _db.Entry(item).State = EntityState.Modified;
            if (AutoSaveChanges)
            {
                await _db.SaveChangesAsync(Cancel).ConfigureAwait(false);
            }
        }


        public void Remove(int id)
        {
            //var item = Get(id);
            //if (item is null) return;
            //alphaDB.Remove(item);

            _db.Remove(new T { Id = id });

            if (AutoSaveChanges)
            {
                _db.SaveChanges();
            }
        }


        public async Task RemoveAsync(int id, CancellationToken Cancel = default)
        {
            _db.Remove(new T { Id = id });
            if (AutoSaveChanges)
            {
                await _db.SaveChangesAsync(Cancel).ConfigureAwait(false);
            }
        }
    }
}
