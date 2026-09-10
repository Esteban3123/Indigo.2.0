using Infrastructure.Data.CosmosModelRepository.Context;
using Infrastructure.Data.CosmosModelRepository.Repositories.Generic;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Data.CosmosModelRepository.UnitOfWork
{
    public class UnitOfWork : CosmosDbContext, IUnitOfWork
    {
        #region Private
        private Container _containerDB;
        private Container _containerBulkDB;
        private readonly CosmosDbContext _dbContext;
        private readonly string _cosmosDbDatabase;
        private readonly string _cosmosDbContainer;
        private readonly int _maxRetries = 3; // Número máximo de intentos
        private readonly TimeSpan _retryDelay = TimeSpan.FromSeconds(2); // Tiempo de espera entre intentos
        #endregion
        #region PublicProperties
        /// <summary>
        /// Obtiene el contenedor
        /// </summary>
        public Container ContainerDB { get => _containerDB; }

        /// <summary>
        /// Obtiene el contenedor con cliente bulk (AllowBulkExecution=true)
        /// </summary>
        public Container ContainerBulkDB { get => _containerBulkDB; }

        /// <summary>
        /// Obtiene el contexto
        /// </summary>
        public CosmosDbContext DbContext { get => _dbContext; }
        #endregion

        #region Builder
        public UnitOfWork(string cosmosDbDatabase, string cosmosDbContainer) : base()
        {
            _cosmosDbDatabase = cosmosDbDatabase;
            _cosmosDbContainer = cosmosDbContainer;

            _dbContext = this;

            if (string.IsNullOrEmpty(cosmosDbDatabase))
            {
                throw new ArgumentNullException("La base de datos de la Cosmos no pueden ser nulo.", nameof(cosmosDbDatabase));
            }

            if ( string.IsNullOrEmpty(cosmosDbContainer))
            {
                throw new ArgumentNullException("El valor del contenedor de la Cosmos no pueden ser nulo.",nameof(cosmosDbContainer));
            }

            this.InitializeContainerWithRetries(cosmosDbDatabase, cosmosDbContainer);
        }
        #endregion
        #region Methods
        /// <summary>
        /// verifica el contenedor asociado a la cosmos para reintentar conexion sino lanzar argument null exception
        /// </summary>
        public void VerifyConnectionCosmoDBContainer()
        {
            if (this.ContainerDB != null) { return; }

            this.InitializeContainerWithRetries(_cosmosDbDatabase, _cosmosDbContainer);

            if (this.ContainerDB is null) { throw new ArgumentNullException(nameof(this.ContainerDB), "No se logro establecer conexion con la Cosmos DB"); }

            return;
        }

        /// <summary>
        /// verifica el contenedor bulk asociado a la cosmos para reintentar conexion sino lanzar argument null exception
        /// </summary>
        public void VerifyConnectionCosmoDBContainerBulk()
        {
            if (this.ContainerBulkDB != null) { return; }

            this.InitializeContainerBulkWithRetries(_cosmosDbDatabase, _cosmosDbContainer);

            if (this.ContainerBulkDB is null) { throw new ArgumentNullException(nameof(this.ContainerBulkDB), "No se logro establecer conexion con la Cosmos DB (bulk)"); }

            return;
        }

        /// <summary>
        /// Inicializador del container con reintento
        /// </summary>
        /// <param name="cosmosDbDatabase"></param>
        /// <param name="cosmosDbContainer"></param>
        private void InitializeContainerWithRetries(string cosmosDbDatabase, string cosmosDbContainer)
        {
            int attempt = 0;
            while (attempt < _maxRetries)
            {
                try
                {
                    var database = DbContext.GetDatabase(cosmosDbDatabase);
                    _containerDB = database.GetContainer(cosmosDbContainer);

                    if (_containerDB != null)
                    {
                        break;
                    }
                }
                catch (Exception ex)
                {
                    if (attempt == _maxRetries - 1)
                    {
                        throw new InvalidOperationException("No se pudo inicializar el contenedor de Cosmos DB después de varios intentos.", ex);
                    }

                    Thread.Sleep(_retryDelay);
                }

                attempt++;
            }
        }

        /// <summary>
        /// Inicializador del container bulk con reintento
        /// </summary>
        /// <param name="cosmosDbDatabase"></param>
        /// <param name="cosmosDbContainer"></param>
        private void InitializeContainerBulkWithRetries(string cosmosDbDatabase, string cosmosDbContainer)
        {
            int attempt = 0;
            while (attempt < _maxRetries)
            {
                try
                {
                    var database = DbContext.GetDatabaseBulk(cosmosDbDatabase);
                    _containerBulkDB = database.GetContainer(cosmosDbContainer);

                    if (_containerBulkDB != null)
                    {
                        break;
                    }
                }
                catch (Exception ex)
                {
                    if (attempt == _maxRetries - 1)
                    {
                        throw new InvalidOperationException("No se pudo inicializar el contenedor bulk de Cosmos DB después de varios intentos.", ex);
                    }

                    Thread.Sleep(_retryDelay);
                }

                attempt++;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Task<int> CompleteAsync()
        {
            // CosmosDB doesn't require saving changes, but here you can manage transaction-like functionality if needed.
            return Task.FromResult(1);
        }

        public void Dispose()
        {
            // Dispose CosmosClient if needed
        }
        #endregion
    }
}
