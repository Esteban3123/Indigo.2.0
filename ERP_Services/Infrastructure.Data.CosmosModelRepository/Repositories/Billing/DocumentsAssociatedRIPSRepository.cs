using Domain.Billing.POCO.E_RIPS;
using Infrastructure.CrossCutting.AzureBlobStorage;
using Infrastructure.CrossCutting.AzureBlobStorage.Factory;
using Infrastructure.CrossCutting.Base;
using Infrastructure.Data.CosmosModelRepository.UnitOfWork;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.CosmosModelRepository.Repositories.Billing
{
   public class DocumentsAssociatedRIPSRepository : CosmosDbRepository<DocumentsAssociatedRIPS>, IDocumentsAssociatedRIPSRepository
    {
        private readonly IUnitOfWork _context;
        private readonly IFactoryStorage _factoryStorage;
        private readonly IStorage _storage;

        public DocumentsAssociatedRIPSRepository(IUnitOfWork context, IFactoryStorage factoryStorage) : base(context)
        {
            _context = context;
            this._factoryStorage = factoryStorage;
            this._storage = this._factoryStorage.CreateStorageControl();
        }

        public async Task <DocumentsAssociatedRIPS> GetDocumentsAssociatedRIPSByRIPSId(string cosmoDBId)
        {
            try
            {

                var document =   (await this.GetByFilterAsync(" SELECT top 1 * " +
                                            " FROM c WHERE c.EntityName= 'ConsultarCUV'" +
                                            " AND c.CosmosRIPSId = @CosmosRIPSId AND c.Container = @Container",
                                            new Dictionary<string, string> {{ "@CosmosRIPSId", cosmoDBId },
                                                                            { "@Container", ServerSessionValues.Current.CurrentContainer ?? string.Empty}})).FirstOrDefault();

                if (document is null) { return new DocumentsAssociatedRIPS(); }
                
                // Si ya tiene datos, retornar directamente
                if (document.Data != null) { return document; }
                
                // Si no tiene datos pero tiene BlobUrl, leer del Blob
                if (!string.IsNullOrEmpty(document.BlobUrl))
                {
                    document.Data =  this.GetDataFromBlob(document.BlobUrl);
                }

                return document;
            }
            catch (Exception)
            {
                throw;
            }
        }


        private object GetDataFromBlob(string blobUrl)
        {
            try
            {
                string stringRipsJson = _storage.ReadFileFromBlobUrl(blobUrl);

                return JsonConvert.DeserializeObject(stringRipsJson);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener datos del Blob: {ex.Message}");
                return null;
            }
        }

    }
}
