using Application.Events.Models;
using Application.Events.Models.CUPS;
using Application.Events.Repository;
using Infrastructure.CrossCutting.Base;
using Infrastructure.CrossCutting.Queue;
using Newtonsoft.Json;
using RabbitMQ.Bus.Events;
using RabbitMQ.Bus.Implements;
using System;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using System.Threading;

namespace Application.Events.Serializers
{
    public class Wrapper
    {
        public QueueParameter queueParameter;

        public Wrapper()
        {
            queueParameter = new QueueParameter();
        }

        /// <summary>
        /// Conection Validate
        /// </summary>
        /// <param name="eventObject"></param>
        /// <param name="audit"></param>
        /// <param name="actionEntity"></param>
        /// <param name="sourceType"></param>
        /// <returns></returns>
        public bool ConnectionParameter(Object eventObject, AuditMessage audit, String actionEntity, DittoSourceType sourceType)
        {
            ExecuteCommand execute = new ExecuteCommand();
            DataTable dtPublish = execute.ExecuteQueryPublish(audit);
            if (dtPublish == null || dtPublish.Rows.Count == 0) { return false; }
            if (!dtPublish.Columns.Contains("PublishEvent")) { return false; }
            if (Convert.ToBoolean(dtPublish.Rows[0]["PublishEvent"]))
            {
                queueParameter.audit = audit;
                queueParameter.db = ServerSessionValues.Current.CurrentContainer;
                queueParameter.PublishEvent = Convert.ToBoolean(dtPublish.Rows[0]["PublishEvent"]);
                if (dtPublish.Columns.Contains("UrlQueue"))
                {
                    queueParameter.UrlQueue = Convert.ToString(dtPublish.Rows[0]["UrlQueue"]);
                    return true;
                }
                queueParameter.Usercode = audit.CodeUser;
                queueParameter.action = actionEntity;
            }
            return false;
        }

        /// <summary>
        /// ReceivedEvent 
        /// </summary>
        /// <param name="eventObject">Event type : Domain.Entities</param>
        /// <param name="audit"></param>
        /// <param name="actionEntity">Added,modified,deleted</param>
        /// <param name="sourceType">1: Medicament, 2 : Thirdparty, 3: Supplier, 4 : Customer, 5 : healthAdministrator, 6 : PUC
        /// 7 : iPSService, 8 : cUPSEntity, 9 : product, 10 : pharmaceuticalForm ,11:administrationRoute, 19:packagingUnit</param>
        public void ReceivedEvent(Object eventObject, AuditMessage audit, String actionEntity, DittoSourceType sourceType)
        {
            try
            {
                Thread Th = new Thread(
                eventObj =>
                {
                    queueParameter.action = actionEntity;
                    queueParameter.Usercode = audit.CodeUser;
                    bool FlagConnection = ConnectionParameter(eventObject, audit, actionEntity, sourceType);
                    if (FlagConnection)
                    {
                        DittoQueue dittoQueue = GenerateWrapperAndEvent(eventObject, queueParameter, sourceType);
                        if (dittoQueue == null) { return; }
                        TriggerEvent(dittoQueue, queueParameter.UrlQueue);
                    }
                });
                Th.Start();
            }
            catch (Exception)
            {
                return;
            }
        }

        /// <summary>
        /// Build Event
        /// </summary>
        /// <param name="eventObject"></param>
        /// <param name="audit"></param>
        /// <param name="actionEntity"></param>
        /// <param name="sourceType"></param>
        /// <returns></returns>
        public DittoQueue BuildEvent(Object eventObject, AuditMessage audit, String actionEntity, DittoSourceType sourceType)
        {
            try
            {
                queueParameter.action = actionEntity;
                queueParameter.Usercode = audit.CodeUser;
                DittoQueue dittoQueue = null;
                bool FlagConnection = ConnectionParameter(eventObject, audit, actionEntity, sourceType);
                if (FlagConnection)
                {
                    dittoQueue = GenerateWrapperAndEvent(eventObject, queueParameter, sourceType);
                    if (dittoQueue == null) { return dittoQueue; }
                }
                return dittoQueue;
            }
            catch (Exception)
            {
                return new DittoQueue();
            }
        }        

        /// <summary>
        /// Obtiene el tipo de la entidad segú el sourcetype
        /// </summary>
        /// <param name="sourceType"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Type GetTypeBySourceType(DittoSourceType sourceType)
        {
            switch (sourceType)
            {
                case DittoSourceType.medicament: return typeof(Medicament);
                case DittoSourceType.thirdParty: return typeof(Thirdparty);
                case DittoSourceType.supplier: return typeof(Supplier);
                case DittoSourceType.customer: return typeof(Customer);
                case DittoSourceType.healthAdministrator: return typeof(HealthAdministrator);
                case DittoSourceType.mainAccounts: return typeof(ObjMainAccounts);
                case DittoSourceType.iPSService: return typeof(iPSService);
                case DittoSourceType.cUPSEntity: return typeof(cUPSEntity);
                case DittoSourceType.product: return typeof(Product);
                case DittoSourceType.pharmaceuticalForm: return typeof(pharmaceuticalForm);
                case DittoSourceType.administrationRoute: return typeof(administrationRoute);
                case DittoSourceType.inventoryMeasurementUnit: return typeof(inventoryMeasurementUnit);
                case DittoSourceType.pharmacologicalGroup: return typeof(pharmacologicalGroup);
                case DittoSourceType.inventoryRiskLevel: return typeof(inventoryRiskLevel);
                case DittoSourceType.inventorySupplie: return typeof(inventorySupplie);
                case DittoSourceType.productType: return typeof(productType);
                case DittoSourceType.productGroup: return typeof(ProductGroup);
                case DittoSourceType.productSubGroup: return typeof(ProductSubGroup);
                case DittoSourceType.packagingUnit: return typeof(packagingUnit);
                case DittoSourceType.manufacturer: return typeof(Manufacturer);
                case DittoSourceType.aTCEntity: return typeof(ATCEntity);
                case DittoSourceType.dCI: return typeof(DCI);
                case DittoSourceType.costCenter: return typeof(CostCenter);
                case DittoSourceType.electronicRIPS: return typeof(ElectronicRIPSEvent);
                case DittoSourceType.functionalUnit: return typeof(FunctionalUnit);
                case DittoSourceType.warehouse: return typeof(Warehouse);
                case DittoSourceType.InvoiceCapitated: return typeof(InvoiceCapitatedEvent);
                case DittoSourceType.CausationPending: return typeof(CausationPending);
                default: throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Generate Wrapper And Data 
        /// </summary>
        /// /// <param name="queueParameter">db : Base de datos donde se va a conectar, PublishEvent : Publicación de evento, UrlQueue : url de la queue, action, user_code</param>
        /// <param name="sourceType">Tipo de enum "medicament", "thirdparty", "supplier", "customer", "healthAdministrator", "mainAccounts", "iPSService", "cUPSEntity", "product","packagingUnit"
        /// "administrationRoute"</param>
        public DittoQueue GenerateWrapperAndEvent(object eventObject, QueueParameter queueParameter, DittoSourceType sourceType)
        {
            DittoQueue WrapperDitto = new DittoQueue() { source = sourceType.ToString() };
            IDittoDocument doc = Activator.CreateInstance(GetTypeBySourceType(sourceType)) as IDittoDocument;

            try
            {
                WrapperDitto.data = doc.Generate(eventObject, queueParameter);
            }
            catch
            {
                return WrapperDitto;
            }

            WrapperDitto.action = queueParameter.action;
            WrapperDitto.db = queueParameter.db;
            WrapperDitto.user_code = queueParameter.Usercode;
            var message = JsonConvert.SerializeObject(WrapperDitto);
            Debug.WriteLine(message);
            return WrapperDitto;
        }

        /// <summary>
        /// Generate Wrapper in object EventData 
        /// </summary>
        /// /// <param name="queueParameter">db : Base de datos donde se va a conectar, PublishEvent : Publicación de evento, UrlQueue : url de la queue, action, user_code</param>
        /// <param name="sourceType">Tipo de enum "medicament", "thirdparty", "supplier", "customer", "healthAdministrator", "mainAccounts", "iPSService", "cUPSEntity", "product","packagingUnit"
        /// "administrationRoute"</param>
        public EventData GenerateWrapperEventData(object eventObject, String userCode, String action, DittoSourceType sourceType)
        {
            string currentContainer = ServerSessionValues.Current.CurrentContainer;
            object dataMessage;
            try
            {
                IDittoDocument doc = Activator.CreateInstance(GetTypeBySourceType(sourceType)) as IDittoDocument;
                dataMessage = doc.Generate(eventObject, queueParameter);

                EventData eventData = new EventData(dataMessage, sourceType.ToString(), action, currentContainer, userCode, DateTime.Now.GetTimestamp());

                return eventData;
            }
            catch (Exception e)
            {
                //Se construye un ErrorWrapper con los detalles de los errores y se guarda el log.
                Type objectType = eventObject?.GetType();
                PropertyInfo codeProperty = objectType?.GetProperty("Code");
                string codeValue = codeProperty != null ? codeProperty.GetValue(eventObject)?.ToString() : $"Code property not found in the entity: {objectType?.ToString()}";

                string errorMessage = Utils.GetInnerExceptionMessageToString(e);

                ErrorWrapper errorLog = new ErrorWrapper(1,errorMessage,codeValue,sourceType.ToString(),action,currentContainer,"",e.ToString());
                errorLog.SaveErrorLog();

                return new EventData(null, null, null, null, null, DateTime.Now.GetTimestamp());
            }

        }

        public string TriggerEvent(Event dittoQueue, String UrlQueue)
        {
            RabbitEventBus rabbitEventBus = new RabbitEventBus();
            try
            {
                rabbitEventBus.Publish(dittoQueue, UrlQueue);
            }
            catch (Exception e)
            {
                return e.Message;
            }
            return "successfully sent";
        }

        /// <summary>
        /// Publis event deleted
        /// </summary>
        /// <param name="dittoQueue"></param>
        /// <param name="UrlQueue"></param>
        /// <returns></returns>
        public void PublishEventObjectDeleted(DittoQueue dittoQueue, String UrlQueue)
        {
            RabbitEventBus rabbitEventBus = new RabbitEventBus();
            try
            {
                Thread Th = new Thread(
                eventObj =>
                {
                    rabbitEventBus.Publish(dittoQueue, UrlQueue);

                });
                Th.Start();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
