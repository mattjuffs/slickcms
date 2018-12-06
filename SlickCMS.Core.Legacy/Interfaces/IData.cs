using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlickCMS.Core.Legacy.Interfaces
{
    /// <summary>
    /// Interface for common CRUD data operations, that all DBML partial classes should implement
    /// </summary>
    public interface IData<T>
    {
        // http://en.wikipedia.org/wiki/Create,_read,_update_and_delete
        // create/read/update/delete  - language agnostic
        // insert/select/update/delete - language (SQL) specific

        /// <summary>
        /// Inserts a new object into the database
        /// </summary>
        void Insert();

        /// <summary>
        /// Selects a new object using its unique identifier
        /// thanks to: http://stackoverflow.com/questions/4297715/interface-method-return-type-to-be-class-that-implements-the-interface
        /// </summary>
        /// <param name="id">Unique ID of object</param>
        /// <returns></returns>
        T Select(int id);

        /// <summary>
        /// Selects several objects with paging
        /// </summary>
        /// <returns>List(object)</returns>
        List<T> SelectMultiple(int skip, int take);

        /// <summary>
        /// Updates an existing object
        /// </summary>
        void Update();

        /// <summary>
        /// Deletes an existing object
        /// </summary>
        void Delete();
    }
}
