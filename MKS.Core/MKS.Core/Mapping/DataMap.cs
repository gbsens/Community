using MKS.Core.Model;
using MKS.Core.Resources;
using System;
using System.Data;

namespace MKS.Core.Mapping
{
    public abstract class DataMap<TObject> : IDataOperations<TObject> 
    {
        /// <summary>
        /// Le business passe la connection par cette méthode afin de appel à la base de donnée
        /// </summary>
        /// <param name="connection">Conncection lu à partir de la classe de configuration</param>
        public virtual void Initialize(IDbConnection connection)
        {
            throw new Exception(string.Format(CoreResources.EX0001, "Initialize"));
        }

        /// <summary>
        /// Est appelé lorsque la même fonction du Business est appelé
        /// </summary>
        /// <param name="myObject">Objet d'information à traiter</param>
        /// <returns></returns>
        public virtual TObject Add(TObject myObject)
        {
            throw new Exception(string.Format(CoreResources.EX0001, "Add"));
        }

        /// <summary>
        /// Est appelé lorsque la même fonction du Business est appelé
        /// </summary>
        /// <param name="myObject"></param>
        /// <returns></returns>
        public virtual TObject Update(TObject myObject)
        {
            throw new Exception(string.Format(CoreResources.EX0001, "Update"));
        }

        /// <summary>
        /// Est appelé lorsque la même fonction du Business est appelé
        /// </summary>
        /// <param name="myObject"></param>
        /// <returns></returns>
        public virtual TObject Select(TObject myObject)
        {
            throw new Exception(string.Format(CoreResources.EX0001, "Select(TObject myObject)"));
        }

        /// <summary>
        /// Est appelé lorsque la même fonction du Business est appelé
        /// </summary>
        /// <param name="myObject"></param>
        /// <returns></returns>
        public virtual int Delete(TObject myObject)
        {
            throw new Exception(string.Format(CoreResources.EX0001, "Delete(TObject myObject)"));
        }

        public virtual void Dispose()
        {
        }

        /// <summary>
        /// Est appelé lorsque la même fonction du Business est appelé
        /// </summary>
        /// <param name="myObject"></param>
        /// <returns></returns>
        public TObject Edit(TObject myObject)
        {
            //throw new Exception(string.Format(CoreResources.EX0001, "Edit(TObject myObject)"));
            return this.Select(myObject);
        }
    }

    public abstract class DataMap<TObject, TKey> : IDataOperations<TObject, TKey>
        where TKey : IKey
    {
        /// <summary>
        /// Le business passe la connection par cette méthode afin de appel à la base de donnée
        /// </summary>
        /// <param name="connection"></param>
        public virtual void Initialize(IDbConnection connection)
        {
            throw new Exception(string.Format(CoreResources.EX0001, "Initialize"));
        }

        /// <summary>
        /// Est appelé lorsque la même fonction du Business est appelé
        /// </summary>
        /// <param name="myObject"></param>
        /// <returns></returns>
        public virtual TObject Add(TObject myObject)
        {
            throw new Exception(string.Format(CoreResources.EX0001, "Add"));
        }

        /// <summary>
        /// Est appelé lorsque la même fonction du Business est appelé
        /// </summary>
        /// <param name="myObject"></param>
        /// <returns></returns>
        public virtual TObject Update(TObject myObject)
        {
            throw new Exception(string.Format(CoreResources.EX0001, "Update"));
        }

        /// <summary>
        /// Est appelé lorsque la même fonction du Business est appelé
        /// </summary>
        /// <param name="myObject"></param>
        /// <returns></returns>
        public virtual TObject Select(TObject myObject)
        {
            throw new Exception(string.Format(CoreResources.EX0001, "Select(TObject myObject)"));
        }

        /// <summary>
        /// Est appelé lorsque la même fonction du Business est appelé
        /// </summary>
        /// <param name="myObject"></param>
        /// <returns></returns>
        public virtual int Delete(TObject myObject)
        {
            throw new Exception(string.Format(CoreResources.EX0001, "Delete(TObject myObject)"));
        }

        /// <summary>
        /// Est appelé lorsque la même fonction du Business est appelé
        /// </summary>
        /// <param name="keyObject"></param>
        /// <returns></returns>
        public virtual TObject Select(TKey keyObject)
        {
            throw new Exception(string.Format(CoreResources.EX0001, "Select(TKey keyObject)"));
        }

        /// <summary>
        /// Est appelé lorsque la même fonction du Business est appelé
        /// </summary>
        /// <param name="keyObject"></param>
        /// <returns></returns>
        public virtual int Delete(TKey keyObject)
        {
            throw new Exception(string.Format(CoreResources.EX0001, "Delete(TKey keyObject)"));
        }

        public virtual void Dispose()
        {
        }

        /// <summary>
        /// Est appelé lorsque la même fonction du Business est appelé
        /// </summary>
        /// <param name="myObject"></param>
        /// <returns></returns>
        public TObject Edit(TObject myObject)
        {
            //throw new Exception(string.Format(CoreResources.EX0001, "Edit(TObject myObject)"));
            return this.Select(myObject);
        }

        /// <summary>
        /// Est appelé lorsque la même fonction du Business est appelé
        /// </summary>
        /// <param name="keyObject"></param>
        /// <returns></returns>
        public TObject Edit(TKey keyObject)
        {
            //throw new Exception(string.Format(CoreResources.EX0001, "Edit(TKey myObject)"));
            return this.Select(keyObject);
        }
    }

    public abstract class DataMap<TObject, TResult, TSearch> : IDataOperations<TObject, TResult, TSearch>
        where TSearch : ISearch
    {
        /// <summary>
        /// Le business passe la connection par cette méthode afin de appel à la base de donnée
        /// </summary>
        /// <param name="connection"></param>
        public virtual void Initialize(IDbConnection connection)
        {
            throw new Exception(string.Format(CoreResources.EX0001, "Initialize"));
        }

        /// <summary>
        /// Est appelé lorsque la même fonction du Business est appelé
        /// </summary>
        /// <param name="myObject"></param>
        /// <returns></returns>
        public virtual TObject Add(TObject myObject)
        {
            throw new Exception(string.Format(CoreResources.EX0001, "Add"));
        }

        /// <summary>
        /// Est appelé lorsque la même fonction du Business est appelé
        /// </summary>
        /// <param name="myObject"></param>
        /// <returns></returns>
        public virtual TObject Update(TObject myObject)
        {
            throw new Exception(string.Format(CoreResources.EX0001, "Update"));
        }

        /// <summary>
        /// Est appelé lorsque la même fonction du Business est appelé
        /// </summary>
        /// <param name="myObject"></param>
        /// <returns></returns>
        public virtual TObject Select(TObject myObject)
        {
            throw new Exception(string.Format(CoreResources.EX0001, "Select(TObject myObject)"));
        }

        /// <summary>
        /// Est appelé lorsque la même fonction du Business est appelé
        /// </summary>
        /// <param name="myObject"></param>
        /// <returns></returns>
        public virtual int Delete(TObject myObject)
        {
            throw new Exception(string.Format(CoreResources.EX0001, "Delete(TObject myObject)"));
        }

        /// <summary>
        /// Est appelé lorsque la même fonction du Business est appelé
        /// </summary>
        /// <param name="searchObject"></param>
        /// <returns></returns>
        public virtual TResult Select(TSearch searchObject)
        {
            throw new Exception(string.Format(CoreResources.EX0001, "Select(TSearch searchObject)"));
        }

        /// <summary>
        /// Est appelé lorsque la même fonction du Business est appelé
        /// </summary>
        /// <param name="searchObject"></param>
        /// <returns></returns>
        public virtual int Delete(TSearch searchObject)
        {
            throw new Exception(string.Format(CoreResources.EX0001, "Delete(TSearch searchObject)"));
        }

        public virtual void Dispose()
        {
        }

        /// <summary>
        /// Est appelé lorsque la même fonction du Business est appelé
        /// </summary>
        /// <param name="myObject"></param>
        /// <returns></returns>
        public TObject Edit(TObject myObject)
        {
            //throw new Exception(string.Format(CoreResources.EX0001, "Delete(TObject myObject)"));
            return this.Edit(myObject);
        }
    }

    public abstract class DataMap<TObject, TResult, TSearch, TKey> : IDataOperations<TObject, TResult, TSearch, TKey>
        where TSearch : ISearch
        where TKey : IKey
    {
        /// <summary>
        /// Le business passe la connection par cette méthode afin de appel à la base de donnée
        /// </summary>
        /// <param name="connection"></param>
        public virtual void Initialize(IDbConnection connection)
        {
            throw new Exception(string.Format(CoreResources.EX0001, "Initialize"));
        }

        /// <summary>
        /// Est appelé lorsque la même fonction du Business est appelé
        /// </summary>
        /// <param name="myObject"></param>
        /// <returns></returns>
        public virtual TObject Add(TObject myObject)
        {
            throw new Exception(string.Format(CoreResources.EX0001, "Add"));
        }

        /// <summary>
        /// Est appelé lorsque la même fonction du Business est appelé
        /// </summary>
        /// <param name="myObject"></param>
        /// <returns></returns>
        public virtual TObject Update(TObject myObject)
        {
            throw new Exception(string.Format(CoreResources.EX0001, "Update"));
        }

        /// <summary>
        /// Est appelé lorsque la même fonction du Business est appelé
        /// </summary>
        /// <param name="myObject"></param>
        /// <returns></returns>
        public virtual TObject Select(TObject myObject)
        {
            throw new Exception(string.Format(CoreResources.EX0001, "Select(TObject myObject)"));
        }

        /// <summary>
        /// Est appelé lorsque la même fonction du Business est appelé
        /// </summary>
        /// <param name="myObject"></param>
        /// <returns></returns>
        public virtual int Delete(TObject myObject)
        {
            throw new Exception(string.Format(CoreResources.EX0001, "Delete(TObject myObject)"));
        }

        /// <summary>
        /// Est appelé lorsque la même fonction du Business est appelé
        /// </summary>
        /// <param name="searchObject"></param>
        /// <returns></returns>
        public virtual TResult Select(TSearch searchObject)
        {
            throw new Exception(string.Format(CoreResources.EX0001, "Select(TSearch searchObject)"));
        }

        /// <summary>
        /// Est appelé lorsque la même fonction du Business est appelé
        /// </summary>
        /// <param name="searchObject"></param>
        /// <returns></returns>
        public virtual int Delete(TSearch searchObject)
        {
            throw new Exception(string.Format(CoreResources.EX0001, "Delete(TSearch searchObject)"));
        }

        /// <summary>
        /// Est appelé lorsque la même fonction du Business est appelé
        /// </summary>
        /// <param name="keyObject"></param>
        /// <returns></returns>
        public virtual TObject Select(TKey keyObject)
        {
            throw new Exception(string.Format(CoreResources.EX0001, "Select(TKey keyObject)"));
        }

        /// <summary>
        /// Est appelé lorsque la même fonction du Business est appelé
        /// </summary>
        /// <param name="keyObject"></param>
        /// <returns></returns>
        public virtual int Delete(TKey keyObject)
        {
            throw new Exception(string.Format(CoreResources.EX0001, "Delete(TKey keyObject)"));
        }

        public virtual void Dispose()
        {
        }

        /// <summary>
        /// Est appelé lorsque la même fonction du Business est appelé
        /// </summary>
        /// <param name="myObject"></param>
        /// <returns></returns>
        public virtual TObject Edit(TObject myObject)
        {
            throw new Exception(string.Format(CoreResources.EX0001, "Edit(TObject myObject)"));
            //return this.Select(myObject);
        }

        /// <summary>
        /// Est appelé lorsque la même fonction du Business est appelé
        /// </summary>
        /// <param name="keyObject"></param>
        /// <returns></returns>
        public virtual TObject Edit(TKey keyObject)
        {
            throw new Exception(string.Format(CoreResources.EX0001, "Edit(TKey myObject)"));
            //return this.Select(keyObject);
        }
    }
}