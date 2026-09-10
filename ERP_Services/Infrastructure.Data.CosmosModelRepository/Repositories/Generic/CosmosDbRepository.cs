using Infrastructure.Data.CosmosModelRepository.Repositories.Generic;
using Infrastructure.Data.CosmosModelRepository.UnitOfWork;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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

        public async Task<BulkUpsertResult<T>> SaveBulkAsync(
            IEnumerable<(T item, string partitionKey)> items,
            IProgress<BulkProgress> progress = null,
            CancellationToken cancellationToken = default)
        {
            this._unitOfWork.VerifyConnectionCosmoDBContainerBulk();

            var itemList = items?.ToList() ?? new List<(T item, string partitionKey)>();
            var result = new BulkUpsertResult<T>();

            if (itemList.Count == 0)
            {
                progress?.Report(new BulkProgress { Total = 0, Sent = 0, Succeeded = 0, Failed = 0 });
                return result;
            }

            int succeeded = 0;
            int failed = 0;
            object reportLock = new object();

            var tasks = itemList.Select(pair =>
                _unitOfWork.ContainerBulkDB
                    .UpsertItemAsync(pair.item, new PartitionKey(pair.partitionKey), cancellationToken: cancellationToken)
                    .ContinueWith(t =>
                    {
                        BulkUpsertItemResult<T> itemResult;

                        if (t.IsCanceled)
                        {
                            itemResult = new BulkUpsertItemResult<T>
                            {
                                Item = pair.item,
                                PartitionKey = pair.partitionKey,
                                IsSuccess = false,
                                ErrorMessage = "Operación cancelada"
                            };
                        }
                        else if (t.IsFaulted)
                        {
                            var ex = t.Exception?.GetBaseException();
                            int? statusCode = null;
                            string message = ex?.Message;

                            if (ex is CosmosException cex)
                            {
                                statusCode = (int)cex.StatusCode;
                            }

                            itemResult = new BulkUpsertItemResult<T>
                            {
                                Item = pair.item,
                                PartitionKey = pair.partitionKey,
                                IsSuccess = false,
                                ErrorMessage = message,
                                StatusCode = statusCode
                            };
                        }
                        else
                        {
                            itemResult = new BulkUpsertItemResult<T>
                            {
                                Item = pair.item,
                                PartitionKey = pair.partitionKey,
                                IsSuccess = true,
                                StatusCode = (int)t.Result.StatusCode
                            };
                        }

                        lock (reportLock)
                        {
                            if (itemResult.IsSuccess) succeeded++;
                            else failed++;

                            progress?.Report(new BulkProgress
                            {
                                Total = itemList.Count,
                                Sent = succeeded + failed,
                                Succeeded = succeeded,
                                Failed = failed
                            });
                        }

                        return itemResult;
                    })
            ).ToList();

            var completed = await Task.WhenAll(tasks).ConfigureAwait(false);

            foreach (var r in completed)
            {
                if (r.IsSuccess) result.Succeeded.Add(r);
                else result.Failed.Add(r);
            }

            return result;
        }
    }
}
