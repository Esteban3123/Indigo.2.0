'***********************************************************************
' Assembly         : Application.Billing
' Author           : Juan F. Tamayo
' Created          : 2014-11-19
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Dynamic
Imports Domain.Crystal.Entities
Imports System.Threading.Tasks

Public Interface ILiquidationAdminService
    Inherits IDisposable

#Region "Methods"

    ''' <summary>
    ''' Metodo para re enviar la notificación de la factura electrónica desde el formulario de trazabilidad electrónica
    ''' </summary>
    ''' <param name="listElectronicDocumentNotification">Lista de los registros que se van a persistir</param>
    ''' <returns></returns>
    Function SendNotification(listElectronicDocumentNotification As List(Of ElectronicDocumentNotification)) As ActionResult(Of String)

    Function SaveApplyRecoveryFeeServiceOrderDetailDistribution(ServiceOrderDetailDistributionId As Integer, ApplyRecoveryFee As Integer) As ActionResult

    ''' <summary>
    ''' metodo para liquidar las estancias manualmente
    ''' </summary>
    ''' <param name="admissionNumber"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function StayManualLiquidation(admissionNumber As String, ByVal endDate As DateTime, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Distributes the folio.
    ''' </summary>
    ''' <param name="objParams">The object parameters.</param>
    ''' <param name="listServiceOrderDetail">Listado de Detallles de ordenes de servicio cuando en la retarificación había algún item quirurgico</param>
    ''' <returns></returns>
    Function DistributeFolio(objParams As Object, homologations As List(Of Homologation), listServiceOrderDetail As List(Of ServiceOrderDetail)) As ActionResult(Of List(Of Homologation), List(Of ServiceOrderDetail))

    ''' <summary>
    ''' Obtiene un ingreso y sus agregados en una entidad plana y serializada en formato JSON
    ''' </summary>
    ''' <param name="code">Código del ingreso</param>
    ''' <returns>Entidad plana serializada</returns>
    Function GetAdmissionPOCOByCode(ByVal code As String) As ActionResult(Of String)

    ''' <summary>
    ''' Gets the revenue control poco by admission code.
    ''' </summary>
    ''' <param name="admissionCode">The admission code.</param>
    ''' <returns></returns>
    Function GetRevenueControlPOCOByAdmissionCodeWithCareGroup(ByVal admissionCode As String, careGroupAdmissionId As Integer, patientCaregroupId As Integer, patientNit As String) As ActionResult(Of String)

    ''' <summary>
    ''' Gets the control poco by code.
    ''' </summary>
    ''' <param name="admissionCode">The admission code.</param>
    ''' <returns></returns>
    Function GetControlPOCOByCode(admissionCode As String) As ActionResult(Of FolioDataHeader)

    ''' <summary>
    ''' Gets the admission poco by admission code.
    ''' </summary>
    ''' <param name="admissionCode">The admission code.</param>
    ''' <returns></returns>
    Function GetAdmissionPOCOByAdmissionCode(ByVal admissionCode As String) As ActionResult(Of String)

    ''' <summary>
    ''' Gets the hcregegre by admission code.
    ''' </summary>
    ''' <param name="admissionCode">The admission code.</param>
    ''' <returns></returns>
    Function GetHCREGEGREByAdmissionCode(ByVal admissionCode As String) As HCREGEGRE

    ''' <summary>
    ''' Ejecuta el método de acción seleccionado
    ''' </summary>
    ''' <param name="actionMethod">Método de acción seleccionado</param>
    ''' <param name="arguments">Objeto dinámico con los argumentos del método de acción
    ''' seleccionado, serializado en formato JSON</param>
    ''' <returns>Resultado de la ejecución del método de acción</returns>
    Function ExecuteActionMethod(ByVal actionMethod As LiquidationActionMethod, ByVal arguments As String) As ActionResult(Of String)

    ''' <summary>
    ''' Realiza la retarificación de servicios en un folio
    ''' </summary>
    ''' <param name="idFolio">Id del folio a retarificar</param>
    ''' <param name="careGroupId">Id del nuevo grupo de atención</param>
    ''' <param name="patientGenus">Género del paciente. 1-Masculino, 2-Femenino</param>
    ''' <param name="patientBirth">Fecha de nacimiento del paciente</param>
    ''' <returns>Resultado de la acción</returns>
    Function ChangeRateServices(ByVal idFolio As Integer, ByVal careGroupId As Integer, ByVal patientGenus As Integer, ByVal patientBirth As Date, ByVal listHomologations As List(Of Homologation),
                                onlyRateChange As Boolean, ThirdPartyPatientId As String, HealthAdministratorId As Integer, listServiceOrderDetailWithQx As List(Of ServiceOrderDetail),
                                operativeUnitId As Integer?) As ActionResult(Of List(Of Homologation), List(Of ServiceOrderDetail))

    ''' <summary>
    ''' Funcion encargada de empaquetar los items seleccionados
    ''' </summary>
    ''' <param name="serviceOrderDetail">The service order detail.</param>
    ''' <param name="arguments">The arguments.</param>
    ''' <returns></returns>
    Function PackageItems(serviceOrderDetail As ServiceOrderDetail, arguments As String, audit As AuditMessage) As ActionResult(Of String)

    ''' <summary>
    ''' Liquidates the folio.
    ''' </summary>
    ''' <returns></returns>
    Function LiquidateFolio(revenueControlDetailCrossingList As List(Of RevenueControlDetailCrossing), patientCode As String, admissionNumber As String, containerCrystal As String, BillingAuthorizationId As Decimal, OperativeUnitId As Integer, ThirdPartyPatientId As Integer, skipAccountControlValidations As Boolean, audit As AuditMessage, session As SessionValues) As Task(Of ActionResult(Of List(Of InvoiceResult)))

    Function GetATCPOSByATCNoPOSAndAdmissionCode(listProductATC As List(Of ProductATC), AdmissionCode As String, caregroupId As Integer) As ActionResult(Of List(Of ProductATC))

    Function GetPharmaceuticalDispensionAndDevolutionWithOutConfirm(admissionNumber As String) As ActionResult

    ''' <summary>
    ''' Funcion encargada de cambiar el estado del ingreso a 'Cerrado'
    ''' </summary>
    Function CloseAdmission(admissionNumber As String, containerCrystal As String, ByVal audit As AuditMessage) As ActionResult(Of SP_CloseAdmission_Result)

    Function AssociateInvoice(RevenueControlDetailId As Integer, InvoiceId As Integer, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Guarda los códigos mipres
    ''' </summary>
    ''' <param name="serviceOrderDetailIds"></param>
    ''' <param name="mipresCodes"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Function SaveMipresCodes(serviceOrderDetailIds As List(Of Integer), mipresCodes As List(Of MipresCode), audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene todos los códigos mipres por id de órden de servicio
    ''' </summary>
    ''' <param name="serviceOrderDetailId"></param>
    ''' <returns></returns>
    Function GetMipresByServiceOrderDetailId(serviceOrderDetailId As Integer) As List(Of MipresCode)

    ''' <summary>
    ''' Liquida los detalles de producción
    ''' </summary>
    ''' <returns></returns>
    Function LiquidateDetailProduction(serviceOrderDetailId As Integer, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Liquida un item de produccion
    ''' </summary>
    ''' <param name="serviceOrderDetailId"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Function LiquidateItemProduction(serviceOrderDetailId As Integer, audit As AuditMessage) As ActionResult

	''' <summary>
	''' Obtiene los datos de la vista
	''' </summary>
	''' <param name="InvoiceId"></param>
	''' <returns></returns>
	Function GetViewListNoSurgical(InvoiceId As Integer, invoiceDetailId As Integer?) As List(Of ViewListNoSurgical)

    ''' <summary>
    ''' Obtiene una lista de ordenes de servicios de una admisión para incluir a otro servicio
    ''' </summary>
    ''' <param name="listIds">Lista de IDs a excluir</param>
    ''' <param name="admissionNumber">Número de admisión</param>
    ''' <returns>Lista de detalles de orden de servicio</returns>
    Function GetListServiceOrderDetail(listIds As List(Of Integer), admissionNumber As String) As List(Of SP_GetListServiceOrderDetail)

    ''' <summary>
    ''' Obtiene una lista de ingresos unificados por la lista de ids de folios y el numero de ingreso cabecera
    ''' </summary>
    ''' <param name="listFoliosIds">Lista de ids de folios</param>
    ''' <param name="admissionNumber">Numero de ingreso cabecera</param>
    ''' <returns>Lista de ingresos unificados</returns>
    Function GetListUnifiedAdmissions(listFoliosIds As List(Of Integer), admissionNumber As String) As ActionResult(Of List(Of ADINGRESO))
#End Region

#Region "Action Methods"

    ''' <summary>
    ''' Liquida la cuota de recuperación en un folio
    ''' </summary>
    ''' <param name="admission">Objeto dinamico con los datos del ingreso</param>
    ''' <param name="idFolio">Id del fólio a liquidar</param>
    ''' <returns>Resultado de la acción</returns>
    Function LiquidateRecoveryFee(transactionContainer As String, ByVal admission As Object, ByVal idFolio As Integer, serviceOrderDetailDistributionDetail As Integer, LiquidationType As eLiquidateRecoveryType, Optional ServiceDistributionList As List(Of Object) = Nothing, Optional liquidateRecovery As Boolean = False, Optional args As Object = Nothing) As ActionResult
    Function LiquidateRecoveryFeeSP(transactionContainer As String, ByVal admission As Object, ByVal idFolio As Integer, serviceOrderDetailDistributionDetail As Integer, LiquidationType As eLiquidateRecoveryType, Optional ServiceDistributionList As List(Of Object) = Nothing, Optional liquidateRecovery As Boolean = False, Optional args As Object = Nothing) As ActionResult

    ' ''' <summary>
    ' ''' Asigna un valor al campo ApplyRecoveryFee de un servicio asignado a un folio
    ' ''' </summary>
    ' ''' <param name="admission">Objeto dinamico con los datos del ingreso</param>
    ' ''' <param name="idFolio">Id del fólio a liquidar</param>
    ' ''' <param name="idDetail">Id del detalle</param>
    ' ''' <param name="value">Valor a asignar</param>
    ' ''' <returns>Resultado de la acción</returns>
    'Function SetRecoveryFeeOnService(ByVal admission As Object, ByVal idFolio As Integer, ByVal idDetail As Integer, ByVal value As Byte) As ActionResult

    ''' <summary>
    ''' Método que obtiene la información del sp de estadísitco de facturación
    ''' </summary>
    ''' <returns></returns>
    Function SP_ReportBillingStatistics(InitialDate As Date, EndDate As Date, ReportType As Integer, AgrupedBy As Integer, DocumentType As String, StatusInvoice As Integer, CareCenterCodes As String, ThirdPartyIds As String, HealthAdministratorIds As String, CareGroupIds As String, UserCodes As String, session As SessionValues) As DataSet

    ''' <summary>
    ''' Funcion que retorna los datos para el reporte de estadistico de facturacion
    ''' </summary>
    ''' <param name="XmlCriterials"></param>
    ''' <param name="XmlFilters"></param>
    ''' <returns></returns>
    Function ReportBillingStadistics(XmlCriterials As String, XmlFilters As String, session As SessionValues) As List(Of SP_ReportBillingStadistics_Result)

    ''' <summary>
    ''' Funcion que retorna la cantidades de datos que retorna el reporte de estadistico de facturacion
    ''' </summary>
    ''' <param name="XmlCriterials"></param>
    ''' <param name="XmlFilters"></param>
    ''' <returns></returns>
    Function ReportBillingStadisticsCount(XmlCriterials As String, XmlFilters As String, session As SessionValues) As SP_ReportBillingStadistics_Count_Result

    ''' <summary>
    ''' Método que obtiene la información del sp de estadísitco de Ingresos
    ''' </summary>
    ''' <returns></returns>
    Function SPCH_ReportAdmissionStatistics(ParametrosString As String(), ParametrosDate As DateTime(), session As SessionValues) As DataTable

    ''' <summary>
    ''' Elimina un mipre
    ''' </summary>
    ''' <param name="mipresCodeId"></param>
    ''' <returns></returns>
    Function DeleteMipresCode(mipresCodeId As Integer) As ActionResult


#End Region

End Interface
