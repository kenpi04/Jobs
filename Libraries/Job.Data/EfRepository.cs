using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Data.Entity.Validation;
using Job.Data;
using System.Linq.Expressions;


namespace Job.Data
{
    /// <summary>
    /// Generic repository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EfRepository<T> : IRepository<T> where T : class 
    {
  
        private readonly IDbContext _context;
        private IDbSet<T> _entities;

        protected virtual IDbSet<T> Entities
        {
            get
            {
                if (_entities == null)
                    _entities = _context.Set<T>();
                return _entities;
            }
        }

    public EfRepository(IDbContext context)
    {
        this._context = context;

    }

    public IQueryable<T> GetAll()
    {
        return Entities.AsQueryable();
    }

    public T GetById(object id)
    {
        return Entities.Find(id);
    }

    public void Insert(T entity)
    {
        try
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            this.Entities.Add(entity);

            this._context.SaveChanges();
        }
        catch (DbEntityValidationException dbEx)
        {
            var msg = string.Empty;

            foreach (var validationErrors in dbEx.EntityValidationErrors)
                foreach (var validationError in validationErrors.ValidationErrors)
                    msg += string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine;

            var fail = new Exception(msg, dbEx);
            //Debug.WriteLine(fail.Message, fail);
            throw fail;
        }
    }
    public virtual void Insert(IEnumerable<T> entities)
    {
        try
        {
            if (entities == null)
                throw new ArgumentNullException("entities");

            foreach (var entity in entities)
                this.Entities.Add(entity);

            this._context.SaveChanges();
        }
        catch (DbEntityValidationException dbEx)
        {
            var msg = string.Empty;

            foreach (var validationErrors in dbEx.EntityValidationErrors)
                foreach (var validationError in validationErrors.ValidationErrors)
                    msg += string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine;

            var fail = new Exception(msg, dbEx);
            //Debug.WriteLine(fail.Message, fail);
            throw fail;
        }
    }

    public void Update(T entity)
    {
        try
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            this._context.SaveChanges();
        }
        catch (DbEntityValidationException dbEx)
        {
            var msg = string.Empty;

            foreach (var validationErrors in dbEx.EntityValidationErrors)
                foreach (var validationError in validationErrors.ValidationErrors)
                    msg += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);

            var fail = new Exception(msg, dbEx);
            //Debug.WriteLine(fail.Message, fail);
            throw fail;
        }
    }

    public void Delete(T entity)
    {
        try
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            this.Entities.Remove(entity);

            this._context.SaveChanges();
        }
        catch (DbEntityValidationException dbEx)
        {
            var msg = string.Empty;

            foreach (var validationErrors in dbEx.EntityValidationErrors)
                foreach (var validationError in validationErrors.ValidationErrors)
                    msg += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);

            var fail = new Exception(msg, dbEx);
            //Debug.WriteLine(fail.Message, fail);
            throw fail;
        }
    }
    /// <summary>
    /// Delete entities
    /// </summary>
    /// <param name="entities">Entities</param>
    public virtual void Delete(IEnumerable<T> entities)
    {
        try
        {
            if (entities == null)
                throw new ArgumentNullException("entities");

            foreach (var entity in entities)
                this.Entities.Remove(entity);

            this._context.SaveChanges();
        }
        catch (DbEntityValidationException dbEx)
        {
            var msg = string.Empty;

            foreach (var validationErrors in dbEx.EntityValidationErrors)
                foreach (var validationError in validationErrors.ValidationErrors)
                    msg += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);

            var fail = new Exception(msg, dbEx);
            //Debug.WriteLine(fail.Message, fail);
            throw fail;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (this._context != null)
            {
                this._context.Dispose();
              
            }
        }
    }


   

    public IEnumerable<T> Find(Expression<Func<T, bool>> where)
    {
        return Entities.Where(where);
    }

    public T Single(Expression<Func<T, bool>> where)
    {
        return Entities.SingleOrDefault(where);
    }

    public T First(Expression<Func<T, bool>> where)
    {
        return Entities.Where(where).FirstOrDefault();
    }

    public IQueryable<T> Table
    {
        get { return Entities; }
    }

    public IEnumerable<T> GetAll(Expression<Func<T, bool>> where = null)
    {
      
        if (where != null)
           return Entities.Where(where);
        return Entities;
    }
    }
}
