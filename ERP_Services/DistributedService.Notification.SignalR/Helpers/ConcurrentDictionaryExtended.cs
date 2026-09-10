using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Base.Entities;
using Domain.Security.Entities;
using System.Collections.Concurrent;

namespace DistributedService.Notification.SignalR.Helpers
{
    /// <summary>
    /// Métodos extendidos de ConcurrentDictionary
    /// </summary>
    public static class ConcurrentDictionaryExtended
    {
        #region Methods

        /// <summary>
        /// Retorna una lista de los distintos AppUser que existen en el diccionario
        /// </summary>
        /// <param name="obj">Objeto quien extiende el metodo</param>
        /// <param name="nonUser">Usuario a ignorar en la lista</param>
        /// <param name="listUsersOnGroupUser"></param>
        /// <returns>Lista de usuarios</returns>
        public static List<AppUser> ToListOfUsers(this ConcurrentDictionary<string, Subscriber> obj, AppUser nonUser = null, List<UsersGroupUser> listUsersOnGroupUser = null)
        {
            List<AppUser> list = new List<AppUser>();
            if (obj == null)
                return list;
            if (listUsersOnGroupUser != null)
            {
                foreach (var kv in obj)
                {
                    if (!list.Contains(kv.Value.App.AppUser) && listUsersOnGroupUser.Any((f) => f.User.UserCode.Trim().Equals(kv.Value.App.AppUser.ID.Trim())))
                        list.Add(kv.Value.App.AppUser);
                }
            }
            else
            {
                foreach (var kv in obj)
                {
                    if (!list.Contains(kv.Value.App.AppUser))
                    {
                        if(nonUser == null)
                            list.Add(kv.Value.App.AppUser);
                        else if(!nonUser.Equals(kv.Value.App.AppUser))
                            list.Add(kv.Value.App.AppUser);
                    }
                }
            }
            return list;
        }

        #endregion
    }
}