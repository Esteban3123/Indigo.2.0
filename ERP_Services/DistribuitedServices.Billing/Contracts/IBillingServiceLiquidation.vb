'***********************************************************************
' Assembly         : DistributedService.Billing
' Author           : Juan F. Tamayo
' Created          : 2014-11-19
'
' Last Modified By : 
' Last Modified On : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.ServiceModel
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Billing.POCO
Imports Domain.Entities
Imports Domain.Crystal.Entities

#End Region

<ServiceContract()>
Public Interface IBillingServiceLiquidation
    <OperationContract()>
    Function SaveApplyRecoveryFeeServiceOrderDetailDistribution(ServiceOrderDetailDistributionId As Integer, ApplyRecoveryFee As Integer) As ActionResult

    ''' <summary>
    ''' Metodo para re enviar la notificación de la factura electrónica desde el formulario de trazabilidad electrónica
    ''' </summary>
    ''' <param name="listElectronicDocumentNotification">Lista de los registros que se van a persistir</param>
    ''' <returns></returns>
    <OperationContract()>
    Function SendNotification(listElectronicDocumentNotification As List(Of ElectronicDocumentNotification)) As ActionResult(Of String)

    ''' <summary>
    ''' metodo para liquidar las estancias manualmente
    ''' </summary>
    ''' <param name="admissionNumber"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function StayManualLiquidation(admissionNumber As String, ByVal endDate As DateTime, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene un ingreso y sus agregados en una entidad plana y serializada en formato JSON
    ''' </summary>
    ''' <param name="code">Código del ingreso</param>
    ''' <returns>Entidad plana serializada</returns>
    <OperationContract()>
    Function GetAdmissionPOCOByCode(code As String) As ActionResult(Of String)

    <OperationContract()>
    Function GetRevenueControlPOCOByAdmissionCodeWithCareGroup(ByVal admissionCode As String, careGroupAdmissionId As Integer, patientCaregroupId As Integer, patientNit As String) As ActionResult(Of String)

    <OperationContract()>
    Function GetControlPOCOByCode(admissionCode As String) As ActionResult(Of FolioDataHeader)

    ''' <summary>
    ''' Gets the admission poco by admission code.
    ''' </summary>
    ''' <param name="admissionCode">The admission code.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetAdmissionPOCOByAdmissionCode(ByVal admissionCode As String) As ActionResult(Of String)

    <OperationContract()>
    Function ExecuteQueryDt(query As String, container As String) As DataTable

    <OperationContract()>
    Function ExecuteQuery(Comando As String, container As String) As Boolean

    <OperationContract()>
    Function ReclasificateDistributions(listDistributions As List(Of Integer), revenueControlId As Integer, userCode As String, container As String) As ActionResult

    ''' <summary>
    ''' Ejecuta el método de acción seleccionado
    ''' </summary>
    ''' <param name="actionMethod">Método de acción seleccionado</param>
    ''' <param name="arguments">Objeto dinámico con los argumentos del método de acción
    ''' seleccionado, serializado en formato JSON</param>
    ''' <returns>Resultado de la ejecución del método de acción</returns>
    <OperationContract()>
    Function ExecuteActionMethod(ByVal actionMethod As LiquidationActionMethod, ByVal arguments As String) As ActionResult(Of String)

    ''' <summary>
    ''' Realiza la retarificación de servicios en un folio
    ''' </summary>
    ''' <param name="idFolio">Id del folio a retarificar</param>
    ''' <param name="careGroupId">Id del nuevo grupo de atención</param>
    ''' <param name="patientGenus">Género del paciente. 1-Masculino, 2-Femenino</param>
    ''' <param name="patientBirth">Fecha de nacimiento del paciente</param>
    ''' <returns>Resultado de la acción</returns>
    <OperationContract()>
    Function ChangeRateServices(ByVal idFolio As Integer, ByVal careGroupId As Integer, ByVal patientGenus As Integer, ByVal patientBirth As Date, ByVal listHomologations As List(Of Homologation),
                                onlyRateChange As Boolean, ThirdPartyPatientId As String, HealthAdministratorId As Integer, listServiceOrderDetailWithQx As List(Of ServiceOrderDetail),
                                operativeUnitId As Integer?) As ActionResult(Of List(Of Homologation), List(Of ServiceOrderDetail))

    ''' <summary>
    ''' Funcion encargada de empaquetar los items seleccionados
    ''' </summary>
    ''' <param name="serviceOrderDetail">The service order detail.</param>
    ''' <param name="arguments">The arguments.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function PackageItems(serviceOrderDetail As ServiceOrderDetail, arguments As String, audit As AuditMessage) As ActionResult(Of String)

    ''' <summary>
    ''' Distributes the folio.
    ''' </summary>
    ''' <param name="listServiceOrderDetail">Listado de Detallles de ordenes de servicio cuando en la retarificación había algún item quirurgico</param>
    ''' <returns></returns>
    <OperationContract()>
    Function DistributeFolio(objParams As Object, homologations As List(Of Homologation), listServiceOrderDetail As List(Of ServiceOrderDetail)) As ActionResult(Of List(Of Homologation), List(Of ServiceOrderDetail))

    ''' <summary>
    ''' Lista los ids de las facturas anuladas
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function ListAnnullateInvoiceIdByAdmission(admission As String) As List(Of Integer)

    ''' <summary>
    ''' Gets the hcregegre by admission code.
    ''' </summary>
    ''' <param name="admissionCode">The admission code.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetHCREGEGREByAdmissionCode(ByVal admissionCode As String) As HCREGEGRE

    ''' <summary>
    ''' Liquidates the folio.
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function LiquidateFolio(revenueControlDetailCrossingList As List(Of RevenueControlDetailCrossing), patientCode As String, admissionNumber As String, session As SessionValues, BillingAuthorizationId As Decimal, OperativeUnitId As Integer, ThirdPartyPatientId As Integer, skipAccountControlValidations As Boolean) As Task(Of ActionResult(Of List(Of InvoiceResult)))

    ''' <summary>
    ''' Obtiene una factura por numero de factura
    ''' </summary>
    ''' <param name="invoiceNumber"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetInvoiceByInvoiceNumber(invoiceNumber As String, audit As AuditMessage) As Domain.Entities.Invoice

    <OperationContract()> _
    Function GetATCPOSByATCNoPOSAndAdmissionCode(listProductATC As List(Of ProductATC), AdmissionCode As String, caregroupId As Integer) As ActionResult(Of List(Of ProductATC))

    <OperationContract()> _
    Function GetPharmaceuticalDispensionAndDevolutionWithOutConfirm(admissionNumber As String) As ActionResult

    ''' <summary>
    ''' Funcion encargada de cambiar el estado del ingreso a 'Cerrado'
    ''' </summary>
    <OperationContract()>
    Function CloseAdmission(admissionNumber As String, containerCrystal As String, audit As AuditMessage) As ActionResult(Of SP_CloseAdmission_Result)

    ''' <summary>
    ''' Método que obtiene la información del sp de estadísitco de facturación
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function SP_ReportBillingStatistics(InitialDate As Date, EndDate As Date, ReportType As Integer, AgrupedBy As Integer, DocumentType As String, StatusInvoice As Integer, CareCenterCodes As String, ThirdPartyIds As String, HealthAdministratorIds As String, CareGroupIds As String, UserCodes As String, session As SessionValues) As DataSet

    ''' <summary>
    ''' Funcion que retorna los datos para el reporte de estadistico de facturacion
    ''' </summary>
    ''' <param name="XmlCriterials"></param>
    ''' <param name="XmlFilters"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function ReportBillingStadistics(XmlCriterials As String, XmlFilters As String, session As SessionValues) As List(Of SP_ReportBillingStadistics_Result)

    ''' <summary>
    ''' Funcion que retorna la cantidades de datos que retorna el reporte de estadistico de facturacion
    ''' </summary>
    ''' <param name="XmlCriterials"></param>
    ''' <param name="XmlFilters"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function ReportBillingStadisticsCount(XmlCriterials As String, XmlFilters As String, session As SessionValues) As SP_ReportBillingStadistics_Count_Result

    ''' <summary>
    ''' Método que obtiene la información del sp de estadísitco de ingresos
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function SPCH_ReportAdmissionStatistics(ParametrosString As String(), ParametrosDate As DateTime(), session As SessionValues) As DataTable

    <OperationContract()>
    Function AssociateInvoice(RevenueControlDetailId As Integer, InvoiceId As Integer, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Guarda los códigos mipres
    ''' </summary>
    ''' <param name="serviceOrderDetailIds"></param>
    ''' <param name="mipresCodes"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveMipresCodes(serviceOrderDetailIds As List(Of Integer), mipresCodes As List(Of MipresCode), audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene todos los códigos mipres por id de órden de servicio
    ''' </summary>
    ''' <param name="serviceOrderDetailId"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetMipresByServiceOrderDetailId(serviceOrderDetailId As Integer) As List(Of MipresCode)

    ''' <summary>
    ''' Elimina un mipre
    ''' </summary>
    ''' <param name="mipresCodeId"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteMipresCode(mipresCodeId As Integer) As ActionResult

    ''' <summary>
    ''' Liquida los detalles de producción
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function LiquidateDetailProduction(serviceOrderDetailId As Integer, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Liquida un item de produccion
    ''' </summary>
    ''' <param name="serviceOrderDetailId"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function LiquidateItemProduction(serviceOrderDetailId As Integer, audit As AuditMessage) As ActionResult

	''' <summary>
	''' Consulta la vista por Id de la factura
	''' </summary>
	''' <param name="InvoiceId"></param>
	''' <param name="audit"></param>
	''' <returns></returns>
	<OperationContract()>
	Function GetViewListNoSurgical(InvoiceId As Integer, invoiceDetailId As Integer?, audit As AuditMessage) As List(Of ViewListNoSurgical)

	''' <summary>
	''' Obtiene una lista de ordenes de servicios de una admisión para incluir a otro servicio
	''' </summary>
	''' <param name="listIds">Lista de IDs a excluir</param>
	''' <param name="admissionNumber">Número de admisión</param>
	''' <returns>Lista de detalles de orden de servicio</returns>
	<OperationContract()>
	Function GetListServiceOrderDetail(listIds As List(Of Integer), admissionNumber As String) As List(Of SP_GetListServiceOrderDetail)

	''' <summary>
	''' Obtiene una lista de ingresos unificados por IDs de folios
	''' </summary>
	''' <param name="listFoliosIds">Lista de IDs de folios</param>
	''' <param name="admissionNumber">Número de admisión</param>
	''' <returns>Lista de ingresos unificados</returns>
	<OperationContract()>
	Function GetListUnifiedAdmissions(listFoliosIds As List(Of Integer), admissionNumber As String) As ActionResult(Of List(Of ADINGRESO))

    ''' <summary>
    ''' Valida si el tercero cumple con la mayoría de edad para facturación
    ''' </summary>
    ''' <param name="thirdPartyId">Id del tercero a validar</param>
    ''' <param name="operativeUnitId">Id de la unidad operativa</param>
    ''' <param name="admissionNumber">Número de ingreso para buscar responsable sugerido</param>
    ''' <returns>Resultado de la validación con información del responsable sugerido si aplica</returns>
    <OperationContract()>
    Function ValidateAgeOfMajorityForLiquidation(thirdPartyId As Integer, operativeUnitId As Integer, admissionNumber As String) As Task(Of ActionResult(Of AgeValidationResult))

    ''' <summary>
    ''' Verifica si el parámetro de validación de mayoría de edad está activo
    ''' </summary>
    ''' <param name="operativeUnitId">Id de la unidad operativa</param>
    ''' <returns>True si el parámetro está activo</returns>
    <OperationContract()>
    Function IsAgeValidationEnabled(operativeUnitId As Integer) As Boolean

End Interface