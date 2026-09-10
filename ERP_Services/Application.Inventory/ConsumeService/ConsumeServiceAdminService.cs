//***********************************************************************
// Assembly         : Aplication.Inventory
// Author           : Carlos Mario Arias Rubiano
// Created          : 07/05/2018
//
// Copyright        : (c) . All rights reserved.
//***********************************************************************

#region Imports
using Domain.Base.Entities;
using Domain.Entities;
using Infrastructure.CrossCutting.Base;
using Infrastructure.CrossCutting.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
#endregion

namespace Application.Inventory.ConsumeService
{
    public class ConsumeServiceAdminService : IConsumeServiceAdminService
    {
        #region Fields

        private string uri;

        private IConsumeServiceRepository _consumeServiceRepository;

        #endregion Fields

        #region Builder

        public ConsumeServiceAdminService(IConsumeServiceRepository consumeServiceRepository)
        {
            if (consumeServiceRepository == null)
            {
                throw new ArgumentNullException("Repositorio de dciRepository vacio");
            }

            this.uri = "http://www.heon.com.co/ApiMedicationV2/MedicationRequest/";
            _consumeServiceRepository = consumeServiceRepository;
        }

        #endregion Builder

        #region CreateUrls

        private string GetUrlListMedicalOrderRecipesByDate(int LogisticOperator, int OfficeType, string HabilitationCode, string InitialDate, string EndDate)
        {
            return this.uri + "Despacho/Listado/" + LogisticOperator.ToString() + "/" + OfficeType.ToString() + "/?codigoHabilitacion=" + HabilitationCode.Trim() + "&fechaInicial=" + InitialDate + "&fechaFinal=" + EndDate;
        }

        private string GetUrlMedicalOrderRecipe(int LogisticOperator, int OfficeType, string PatientIdentification, int TypeIdentification)
        {
            return this.uri + "Despacho/DespachoPaciente/" + LogisticOperator.ToString() + "/" + OfficeType.ToString() + "/?tipoIdentificacion=" + TypeIdentification.ToString() + "&identificacion=" + PatientIdentification;
        }

        private string GetUrlListMedicalOrderRecipesReturnedByDate(int LogisticOperator, int OfficeType, string HabilitationCode, DateTime InitialDate, DateTime EndDate)
        {
            return this.uri + "Devolucion/Listado/" + LogisticOperator.ToString() + "/" + OfficeType.ToString() + "/?codigoHabilitacion=" + HabilitationCode + "&fechaInicial=" + InitialDate.ToString("yyyy-MM-dd") + "&fechaFinal=" + EndDate.ToString("yyyy-MM-dd");
        }

        private string GetUrlMedicalOrderRecipeReturned(int LogisticOperator, int OfficeType, string recetarioOMedica)
        {
            return this.uri + "Devolucion/RecetarioOrdenMedica/" + LogisticOperator.ToString() + "/" + OfficeType.ToString() + "/?recetarioOMedica=" + recetarioOMedica;
        }

        private string GetUrlRecetarioOrdenMedicaConfirmed()
        {
            return this.uri + "Confirmacion";
        }

        #endregion

        #region Methods

        /// <summary>
        /// Obtiene dispensación por rango de fecha
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public ActionResult<string> GetDispensingByDateRange(string parameters)
        {
            try
            {
                object args = Utils.DeserializeJsonToObject(parameters);

                string InitialDateString = ((dynamic)args).InitialDate.Year.ToString() + "-" + ((dynamic)args).InitialDate.Month.ToString().PadLeft(2, '0') + "-" + ((dynamic)args).InitialDate.Day.ToString().PadLeft(2, '0') + "T" + ((dynamic)args).InitialDate.Hour.ToString().PadLeft(2, '0') + ":" + ((dynamic)args).InitialDate.Minute.ToString().PadLeft(2, '0') + ":" + ((dynamic)args).InitialDate.Second.ToString().PadLeft(2, '0');
                string EndDateString = ((dynamic)args).EndDate.Year.ToString() + "-" + ((dynamic)args).EndDate.Month.ToString().PadLeft(2, '0') + "-" + ((dynamic)args).EndDate.Day.ToString().PadLeft(2, '0') + "T" + ((dynamic)args).EndDate.Hour.ToString().PadLeft(2, '0') + ":" + ((dynamic)args).EndDate.Minute.ToString().PadLeft(2, '0') + ":" + ((dynamic)args).EndDate.Second.ToString().PadLeft(2, '0');

                var resultResponse = ConsumeServiceIntegration.GetInstance().GetObjectByUrl(GetUrlListMedicalOrderRecipesByDate((int)((dynamic)args).LogisticOperator, (int)((dynamic)args).OfficeType, (string)((dynamic)args).HabilitationCode, (string)InitialDateString, (string)EndDateString));
                if (resultResponse.StateResult == false)
                {
                    return new ActionResult<string> { ObjectEmbbeded = null, StateResult = false, StatusCode = resultResponse.StatusCode, Message = resultResponse.Message };
                }

                return new ActionResult<string> { ObjectEmbbeded = resultResponse.ObjectEmbbeded, StateResult = true, StatusCode = eStatusResult.SUCCESS };
            }
            catch (WebException ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<string> { ObjectEmbbeded = null, StateResult = false, StatusCode = eStatusResult.EXCEPTION, Message = ex.Message };
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<string> { ObjectEmbbeded = null, StateResult = false, StatusCode = eStatusResult.EXCEPTION, Message = ex.Message };
            }
        }

        /// <summary>
        /// Obtiene dispensación por paciente
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public ActionResult<WebServiceObject> GetDispensingByPatient(string parameters)
        {
            try
            {
                object args = Utils.DeserializeJsonToObject(parameters);

                var resultResponse = ConsumeServiceIntegration.GetInstance().GetObjectByUrl(GetUrlMedicalOrderRecipe((int)((dynamic)args).LogisticOperator, (int)((dynamic)args).OfficeType, (string)((dynamic)args).PatientIdentification, (int)((dynamic)args).TypeIdentification));
                if (resultResponse.StateResult == false)
                {
                    return new ActionResult<WebServiceObject> { ObjectEmbbeded = null, StateResult = false, StatusCode = resultResponse.StatusCode, Message = resultResponse.Message };
                }


                var result = JsonConvert.DeserializeObject<WebServiceObject>(resultResponse.ObjectEmbbeded);
                result.JsonSolicitud = resultResponse.ObjectEmbbeded;

                return new ActionResult<WebServiceObject> { ObjectEmbbeded = result, StateResult = true, StatusCode = eStatusResult.SUCCESS };
            }
            catch (WebException ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<WebServiceObject> { ObjectEmbbeded = null, StateResult = false, StatusCode = eStatusResult.EXCEPTION, Message = ex.Message };
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult<WebServiceObject> { ObjectEmbbeded = null, StateResult = false, StatusCode = eStatusResult.EXCEPTION, Message = ex.Message };
            }

        }

        public ActionResult PostConfirmIntegration(string parameters, Domain.Entities.PharmaceuticalDispensing PharmaceuticalDispensing, AuditMessage audit)
        {
            ActionResult resultSave = null;
            string JsonEntrega = "";
            object args = null;
            try
            {
                args = Utils.DeserializeJsonToObject(parameters);

                JsonEntrega = CreateJsonEntrega((int)((dynamic)args).LogisticOperator, (int)((dynamic)args).OfficeType, PharmaceuticalDispensing);

                //Se procede a guardar con el JsonRespuesta vacio
                resultSave = SaveTablesControlIntegration(args, JsonEntrega, false, PharmaceuticalDispensing, audit);
                if (resultSave.StateResult == false)
                {
                    return new ActionResult { StateResult = false, StatusCode = eStatusResult.WARNING, Message = resultSave.Message };
                }

                ////------------- Se crea este objeto pero se debe borrar, es solo para probar ----------------------------
                //ConfirmationObject ConfirmationObject = new ConfirmationObject();
                //ConfirmationObject.idOperador = 5;
                //ConfirmationObject.idTipoDespacho = 1;
                //ConfirmationObject.tipoMovimiento = "E";
                //ConfirmationObject.recetarioOMedica = "568823410";
                //ConfirmationObject.idProducto = 1454;
                //ConfirmationObject.idLote = 14654;
                //ConfirmationObject.alfaNumericoLote = "DFK00021864";
                //ConfirmationObject.cantidad = 10;
                //ConfirmationObject.cantidadAEntregar = 0;
                //ConfirmationObject.idConfirmacion = 0;
                //ConfirmationObject.mensaje = "No hay cantidades para dispensar";
                //List<ConfirmationObject> ListTest = new List<ConfirmationObject>();
                //ListTest.Add(ConfirmationObject);
                //DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(List<ConfirmationObject>));
                //MemoryStream ms = new MemoryStream();
                //ser.WriteObject(ms, ListTest);
                //string jsonString = Encoding.UTF8.GetString(ms.ToArray());
                //ms.Close();
                ////----------------- Fin borrar ---------------------------------------------------------------------------------------

                //Esta linea se debe descomentar que es la que ejecuta el proceso de HEON y se debe quitar jsonString por resultResponse.ObjectEmbbeded en el metodo de SaveTablesControlIntegration
                var resultResponse = ConsumeServiceIntegration.GetInstance().PostObjectByUrlAndJson(GetUrlRecetarioOrdenMedicaConfirmed(), JsonEntrega);

                resultSave = SaveTablesControlIntegration(args, resultResponse.ObjectEmbbeded, true, PharmaceuticalDispensing, audit);
                if(resultSave.StateResult == false)
                {
                    return new ActionResult { StateResult = false, StatusCode = eStatusResult.WARNING, Message = resultSave.Message };
                }

                return new ActionResult { StateResult = true, StatusCode = eStatusResult.SUCCESS, Message = resultSave.Message };
            }
            catch (WebException ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                resultSave = SaveTablesControlIntegration(args, JsonEntrega, true, PharmaceuticalDispensing, audit, ex.Message);
                return new ActionResult { StateResult = false, StatusCode = eStatusResult.WARNING, Message = resultSave.Message };
            }
            catch (Exception ex)
            {
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                return new ActionResult { StateResult = false, StatusCode = eStatusResult.WARNING, Message = ex.Message };
            }
        }

        private ActionResult SaveTablesControlIntegration(object args, string JsonObject, bool IsWithResponseHeon, Domain.Entities.PharmaceuticalDispensing PharmaceuticalDispensing, AuditMessage audit, string MessageError = "")
        {
            TransactionOptions txSettings = new TransactionOptions();
            txSettings.Timeout = TransactionManager.MaximumTimeout;
            txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, txSettings))
            {
                try
                {
                    ActionResult<string> resultXmlObject = CreateXmlObject(args, JsonObject, PharmaceuticalDispensing, IsWithResponseHeon, MessageError);

                    var result = _consumeServiceRepository.SP_SaveIntegration(resultXmlObject.ObjectEmbbeded, audit.CodeUser);
                    if (result.CodeMessage > 0)
                    {
                        scope.Dispose();
                        return new ActionResult { StateResult = false, StatusCode = eStatusResult.WARNING, Message = result.MessageReturn };
                    }

                    scope.Complete();

                    if(resultXmlObject.StateResult == false)
                    {
                        return new ActionResult { StateResult = false, StatusCode = eStatusResult.WARNING, Message = resultXmlObject.Message };
                    }

                    return new ActionResult { StateResult = true, StatusCode = eStatusResult.SUCCESS, Message = resultXmlObject.Message };
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy");
                    return new ActionResult { StateResult = false, StatusCode = eStatusResult.WARNING, Message = ex.Message };
                }
            }
        }

        private ActionResult<string> CreateXmlObject(object args, string JsonObject, Domain.Entities.PharmaceuticalDispensing PharmaceuticalDispensing, bool IsWithResponseHeon, string MessageError)
        {
            StringBuilder XmlObject = new StringBuilder();
            var ListObjects = JsonConvert.DeserializeObject<List<ConfirmationObject>>(JsonObject);
            int status = ((IsWithResponseHeon == true) ? 2 : 1);

            if(ListObjects != null && ListObjects.Count() > 0)
            {
                status = (from x in ListObjects where x.idConfirmacion == 0 && IsWithResponseHeon == true select x).Count() > 0 ? 2 : 1;
            }

            XmlObject.Append("<Header>");

            XmlObject.Append("<LogisticOperator>" + ((int)((dynamic)args).LogisticOperator).ToString() + "</LogisticOperator>");
            XmlObject.Append("<OfficeType>" + ((int)((dynamic)args).OfficeType).ToString() + "</OfficeType>");
            XmlObject.Append("<CareCenterCode>" + (string)((dynamic)args).CareCenterCode + "</CareCenterCode>");
            XmlObject.Append("<JsonSolicitud>" + (string)((dynamic)args).JsonSolicitud + "</JsonSolicitud>");
            XmlObject.Append("<JsonEntrega>" + JsonObject + "</JsonEntrega>");
            XmlObject.Append("<JsonRespuestaHeon>" + ((MessageError == "") ? ((IsWithResponseHeon == true) ? JsonObject : "") : MessageError) + "</JsonRespuestaHeon>");
            XmlObject.Append("<EntityId>" + PharmaceuticalDispensing.Id.ToString() + "</EntityId>");
            XmlObject.Append("<EntityCode>" + PharmaceuticalDispensing.Code + "</EntityCode>");
            XmlObject.Append("<EntityName>" + "PharmaceuticalDispensing" + "</EntityName>");
            XmlObject.Append("<Status>" + status + "</Status>");

            if (ListObjects != null && ListObjects.Count() > 0)
            {
                foreach (var item in ListObjects)
                {
                    XmlObject.Append("<Details>");
                    XmlObject.Append("<MedicalOrderRecipe>" + item.recetarioOMedica + "</MedicalOrderRecipe>");
                    XmlObject.Append("<ProductCodeHeon>" + item.idProducto.ToString() + "</ProductCodeHeon>");
                    XmlObject.Append("<TotalQuantity>" + ((int)item.cantidadAEntregar).ToString() + "</TotalQuantity>");
                    XmlObject.Append("<Status>" + ((item.idConfirmacion == 0 && IsWithResponseHeon == true) ? 2 : 1) + "</Status>");
                    XmlObject.Append("<Message>" + ((MessageError == "") ? item.mensaje : MessageError) + "</Message>");
                    XmlObject.Append("</Details>");
                }
            }
            else
            {
                XmlObject.Append("<Details>");
                XmlObject.Append("<MedicalOrderRecipe>" + "" + "</MedicalOrderRecipe>");
                XmlObject.Append("<ProductCodeHeon>" + "" + "</ProductCodeHeon>");
                XmlObject.Append("<TotalQuantity>" + 0 + "</TotalQuantity>");
                XmlObject.Append("<Status>" + 2 + "</Status>");
                XmlObject.Append("<Message>" + MessageError + "</Message>");
                XmlObject.Append("</Details>");
            }

            XmlObject.Append("</Header>");

            StringBuilder messageReturn = new StringBuilder();
            messageReturn.AppendLine("Se crearon los registros en la tabla de control de integración para la dispensación " + PharmaceuticalDispensing.Code + " arrojando los siguientes resultados en sus detalles: ");
            if (ListObjects != null && ListObjects.Count() > 0)
            {
                ListObjects.ForEach(x => {
                    string ProductCode = (from y in PharmaceuticalDispensing.PharmaceuticalDispensingDetail where y.recetarioOMedica == x.recetarioOMedica select y.CodeProduct).FirstOrDefault();
                    if (IsWithResponseHeon == true)
                    {
                        if (x.idConfirmacion == 0)
                        {
                            messageReturn.AppendLine("Error, para la orden médica HEON " + x.recetarioOMedica + " con código producto VIE " + ProductCode + ": " + ((MessageError == "") ? x.mensaje : MessageError));
                        }
                        else
                        {
                            messageReturn.AppendLine("Correcto, para la orden médica HEON " + x.recetarioOMedica + " con código producto VIE " + ProductCode);
                        }
                    }
                });
            }
            else
            {
                messageReturn.AppendLine("Xml creado con errores en sus detalles");
            }

            return new ActionResult<string> { StateResult = ((status == 2) ? false : true), ObjectEmbbeded = XmlObject.ToString(), Message = messageReturn.ToString() };
        }

        private string CreateJsonEntrega(int LogisticOperator, int OfficeType, Domain.Entities.PharmaceuticalDispensing PharmaceuticalDispensing)
        {
            List<ConfirmationObject> ListObjects = new List<ConfirmationObject>();

            foreach(var item in PharmaceuticalDispensing.PharmaceuticalDispensingDetail)
            {
                ConfirmationObject ConfirmationObject = new ConfirmationObject();
                ConfirmationObject.idOperador = LogisticOperator;
                ConfirmationObject.idTipoDespacho = OfficeType;
                ConfirmationObject.tipoMovimiento = "E";
                ConfirmationObject.recetarioOMedica = item.recetarioOMedica;
                ConfirmationObject.idProducto = item.idProductoHeon;
                ConfirmationObject.idLote = item.Id;
                ConfirmationObject.alfaNumericoLote = PharmaceuticalDispensing.Code;
                ConfirmationObject.cantidad = item.Quantity;
                ConfirmationObject.cantidadAEntregar = item.CantidadSolicitada;
                ConfirmationObject.idConfirmacion = 0;
                ConfirmationObject.mensaje = "";

                ListObjects.Add(ConfirmationObject);
            }

            //string json = Utils.SerializeObjectToJson(ListObjects);

            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(List<ConfirmationObject>));
            MemoryStream ms = new MemoryStream();
            ser.WriteObject(ms, ListObjects);
            string jsonString = Encoding.UTF8.GetString(ms.ToArray());
            ms.Close();
            return jsonString;
        }

        #endregion

        #region IDisposable Support
        private bool disposedValue;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {

                }
                //_consignmentInventoryRemissionDetailBatchSerialRepository = null;
                IndigoGC.Execute();
            }
            disposedValue = true;
        }
        #endregion 
    }
}
