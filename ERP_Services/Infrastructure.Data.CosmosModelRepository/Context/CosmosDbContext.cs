using Infrastructure.CrossCutting.Root;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Data.CosmosModelRepository.Context
{


    public class CosmosDbContext : ICosmosDbContext
    {
        private static readonly object _lock = new object();
        private static Lazy<CosmosClient> _lazyClient = new Lazy<CosmosClient>(
            () => CreateCosmosClient(),
            LazyThreadSafetyMode.ExecutionAndPublication 
        );


        public CosmosDbContext()
        {
        }

        public static CosmosClient CosmosClient => _lazyClient.Value;

        private static CosmosClient CreateCosmosClient()
        {
            string endpoint = Environment.GetEnvironmentVariable(ConfigurationFile.CONX_DB_URI_AZCOS);
            string key = Environment.GetEnvironmentVariable(ConfigurationFile.CONX_DB_KEY_AZCOS);

            if (string.IsNullOrEmpty(endpoint) || string.IsNullOrEmpty(key))
            {
                throw new InvalidOperationException("Las credenciales de Cosmos DB no están configuradas correctamente.");
            }

            try
            {
                return new CosmosClient(endpoint, key, new CosmosClientOptions
                {
                    ConnectionMode = ConnectionMode.Direct,
                    MaxRetryAttemptsOnRateLimitedRequests = 5,
                    MaxRetryWaitTimeOnRateLimitedRequests = TimeSpan.FromSeconds(10),
                    EnableTcpConnectionEndpointRediscovery = true // 🔹 Mejora la resiliencia en fallos de conexión
                });
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al inicializar CosmosClient", ex);
            }
        }

        public Database GetDatabase(string databaseId)
        {
            try
            {
                return CosmosClient.GetDatabase(databaseId);
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.ServiceUnavailable ||
                                             ex.StatusCode == System.Net.HttpStatusCode.RequestTimeout ||
                                             (int)ex.StatusCode == 429)
            {
                //_logger?.LogWarning("Fallo en la conexión con Cosmos DB. Reintentando...");

                // 🔹 Intentar re-inicializar el cliente en caso de error crítico
                ReinitializeClient();

                return CosmosClient.GetDatabase(databaseId);
            }
            catch (Exception ex)
            {
                //_logger?.LogError(ex, "Error inesperado al obtener la base de datos de Cosmos DB.");
                throw;
            }
        }

        private void ReinitializeClient()
        {
            lock (_lock)
            {
                if (_lazyClient.IsValueCreated)
                {
                    //_logger?.LogWarning("Reiniciando CosmosClient...");
                    _lazyClient.Value.Dispose();
                }

                _lazyClient = new Lazy<CosmosClient>(() => CreateCosmosClient(), LazyThreadSafetyMode.ExecutionAndPublication);
            }
        }
    }

}
