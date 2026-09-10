using Infrastructure.Data.CosmosModelRepository.Repositories.Generic;
using Infrastructure.Data.CosmosModelRepository.UnitOfWork;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data.CosmosModelRepository
{

    public class CosmosDbRepository<T> :  ICosmosDbRepository<T> where T: class
    {
        private readonly IUnitOfWork _unitOfWork ; 

        public CosmosDbRepository(IUnitOfWork iUnitOfWork) : base()
        {
            _unitOfWork = iUnitOfWork;  
        }

        public IUnitOfWork UnitOfWork { get => _unitOfWork; }

        public T GetById(string id)
        {
            try
            {
                this._unitOfWork.VerifyConnectionCosmoDBContainer();

                var response = _unitOfWork.ContainerDB.ReadItemAsync<T>(id, new PartitionKey(id)).Result;
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                // Maneja la excepción de documento no encontrado según tus necesidades
                return default(T);
            }
            catch (Exception ex)
            {
                // Maneja otras excepciones según tus necesidades
                throw;
            }
        }


        public IEnumerable<T> GetByFilter( string query, Dictionary<string,string> valuePairs)
        {
            try
            {
                this._unitOfWork.VerifyConnectionCosmoDBContainer();

                if (string.IsNullOrEmpty(query))
                {
                    throw new  ArgumentNullException(nameof(query));
                }

                if (valuePairs is null || !valuePairs.Any())
                {
                    throw new ArgumentNullException(nameof(query));
                }

                var queryDefinition = new QueryDefinition(query);
                
                foreach (var item in valuePairs)
                {
                    queryDefinition.WithParameter(item.Key, item.Value);
                };

                var feedIterator = _unitOfWork.ContainerDB.GetItemQueryIterator<T>(queryDefinition);

                List<T> listOfT = new List<T>();

                while (feedIterator.HasMoreResults)
                {
                    FeedResponse<T> response =  feedIterator.ReadNextAsync().GetAwaiter().GetResult();
                    foreach (var item in response)
                    {
                        listOfT.Add(item);
                    }
                }

                return listOfT;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                // Maneja la excepción de documento no encontrado según tus necesidades
                return new List<T>();
            }
            catch (Exception)
            {
                // Maneja otras excepciones según tus necesidades
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="valuePairs"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetByFilterAsync(string query, Dictionary<string, string> valuePairs)
        {
            try
            {
                this._unitOfWork.VerifyConnectionCosmoDBContainer();

                if (string.IsNullOrEmpty(query))
                {
                    throw new ArgumentNullException(nameof(query));
                }

                if (valuePairs is null || !valuePairs.Any())
                {
                    throw new ArgumentNullException(nameof(query));
                }

                var queryDefinition = new QueryDefinition(query);

                foreach (var item in valuePairs)
                {
                    queryDefinition.WithParameter(item.Key, item.Value);
                };

                var feedIterator = _unitOfWork.ContainerDB.GetItemQueryIterator<T>(queryDefinition);

                List<T> listOfT = new List<T>();

                while (feedIterator.HasMoreResults)
                {
                    FeedResponse<T> response = await feedIterator.ReadNextAsync();
                    foreach (var item in response)
                    {
                        listOfT.Add(item);
                    }
                }

                return listOfT;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                // Maneja la excepción de documento no encontrado según tus necesidades
                return new List<T>();
            }
            catch (Exception)
            {
                // Maneja otras excepciones según tus necesidades
                throw;
            }
        }

        public void Save(T item, string partitionKeyValue)
        {
            try
            {
                this._unitOfWork.VerifyConnectionCosmoDBContainer();

                var response = _unitOfWork.ContainerDB.CreateItemAsync(item, new PartitionKey(partitionKeyValue)).Result;
                // Puedes manejar la respuesta de la inserción según tus necesidades
            }
            catch (Exception ex)
            {
                // Maneja la excepción de inserción fallida según tus necesidades
                throw;
            }
        }
    }
}
