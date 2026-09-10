'***********************************************************************
' Assembly         : Presentacion.Billing.MVP
' Author           : Juan F. Tamayo
' Created          : 2014-11-10
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities
Imports Presentation.CloudAgent
Imports Domain.Base.Entities
Imports Presentation.Base
Imports System.ServiceModel
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.BillingRepository
Imports DevExpress.Xpo
Imports Domain.Security.Entities
Imports Domain.Crystal.Entities
Imports System.Data
Imports Infrastructure.Data.Xpo.ContractRepository
Imports RestSharp
Imports Domain.Billing.POCO
Imports Infrastructure.Data.Xpo.MixingStationRepository
Imports Infrastructure.Data.Xpo.CommonRepository
Imports Infrastructure.Base.Security

#End Region

Public Class MLiquidation
    Implements IDisposable

#Region "Consts"

    ''' <summary>
    ''' Tag del frontal de ordenes de servicio
    ''' </summary>
    Private Const TAG_SERVICE_ORDER_FORM As String = "755"

    ''' <summary>
    ''' Tag del frontal de recibos de caja
    ''' </summary>
    Private Const TAG_CASH_RECEIPTS_FORM As String = "532"

    ''' <summary>
    ''' tAG FORMULARIO DE LIQUIDACIÓN
    ''' </summary>
    Private Const TAG_LIQUIDATION As String = "756"

#End Region

#Region "Fields"

    ''' <summary>
    ''' Referencia a los valores de session
    ''' </summary>
    Private _indigoSessionValues As SessionValues

    ' ''' <summary>
    ' ''' Id del frontal
    ' ''' </summary>
    'Private _tagForm As String

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    Public Sub New()
        'Me._tagForm = tag
        Me._indigoSessionValues = SessionValues.Instance
    End Sub

#End Region

#Region "Methods"
    ''' <summary>
    ''' metodo para liquidar manualmente las estancias
    ''' </summary>
    ''' <param name="admissionNumber"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function StayManualLiquidation(admissionNumber As String, ByVal endDate As DateTime) As Task(Of ActionResult)
        Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoBilling.InnerChannel)
            _indigoSessionValues.AuditMessageWcf.Functional = TAG_LIQUIDATION
            _indigoSessionValues.AuditMessageWcf.CodeUser = _indigoSessionValues.UserIndigo

            Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.StayManualLiquidationAsync(admissionNumber, endDate, _indigoSessionValues.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Liquida detalle de producto
    ''' </summary>
    ''' <param name="serviceOrderDetailIds"></param>
    ''' <returns></returns>
    Public Function LiquidateDetailProductionAsync(serviceOrderDetailIds As List(Of Integer)) As Task(Of ActionResult)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoBilling.LiquidateDetailProductionAsync(serviceOrderDetailIds(0), _indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Consulta los mipres por sod
    ''' </summary>
    ''' <param name="v"></param>
    ''' <returns></returns>
    Public Function GetMipresCodesByServiceOrderDetailIdAsync(serviceOrderDetailId As Integer) As Task(Of List(Of MipresCode))
        Return IndigoConecta.Instancia.CurrentCloud.IndigoBilling.GetMipresByServiceOrderDetailIdAsync(serviceOrderDetailId)
    End Function

    Public Async Function ExecuteCommandDt(query As String, container As String) As Task(Of DataTable)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.ExecuteQueryDtAsync(query, container)
    End Function

    Public Async Function ExecuteQuery(query As String, container As String) As Task(Of Boolean)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.ExecuteQueryAsync(query, container)
    End Function

    ''' <summary>
    ''' guarda los códigos mipres
    ''' </summary>
    ''' <param name="serviceOrderDetailIds"></param>
    ''' <param name="mipresCodes"></param>
    ''' <returns></returns>
    Public Function SaveMipresCode(serviceOrderDetailIds As List(Of Integer), mipresCodes As List(Of MipresCode)) As Task(Of ActionResult)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoBilling.SaveMipresCodesAsync(serviceOrderDetailIds, mipresCodes, _indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Elimina un código mipres
    ''' </summary>
    ''' <param name="mipresId"></param>
    ''' <returns></returns>
    Public Function DeleteMipresAsync(mipresId As Integer) As Task(Of ActionResult)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoBilling.DeleteMipresCodeAsync(mipresId)
    End Function

    ''' <summary>
    ''' Obtiene la secuencia numerica asignada al formulario de ordenes de servicio
    ''' </summary>
    ''' <returns>Secuencia numerica</returns>
    Public Async Function GetSequenseServiceOrder() As Task(Of Domain.Entities.BillingSequence)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.GetSequenseByIdFormAsync(TAG_SERVICE_ORDER_FORM)
    End Function

    ''' <summary>
    ''' Obtiene la secuencia numerica asignada al formulario de recibos de caja
    ''' </summary>
    ''' <returns>Secuencia numerica</returns>
    Public Async Function GetSequenseCashReceipts() As Task(Of Domain.Entities.TreasurySequence)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.GetSequenseByIdFormAsync(TAG_CASH_RECEIPTS_FORM)
    End Function

    ''' <summary>
    ''' Consultar los permisos que tiene el usuario en éste formulario
    ''' </summary>
    Public Function GetPermissions(ByVal tagForm As String) As List(Of PermissionUserToolbar)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.ListPermissionsUserToolbar(Infrastructure.CrossCutting.Base.SessionValues.Instance.UserIndigo, Infrastructure.CrossCutting.Base.SessionValues.Instance.UserRol, tagForm, Infrastructure.CrossCutting.Base.SessionValues.Instance)
    End Function

    Public Async Function CloseAdmission(admissionNumber As String) As Task(Of ActionResult(Of SP_CloseAdmission_Result))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.CloseAdmissionAsync(admissionNumber, _indigoSessionValues.HisContainer, _indigoSessionValues.AuditMessageWcf)
    End Function

    Public Async Function AssociateInvoice(ByVal RevenueControlDetailId As Integer, ByVal InvoiceId As Integer) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.AssociateInvoiceAsync(RevenueControlDetailId, InvoiceId, _indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Lista las resoluciones de facturación autorizadas para el usuario
    ''' </summary>
    ''' <param name="userCode">Código del usuario a consultar</param>
    ''' <returns>Lista de resoluciones</returns>
    Public Async Function ListBillingAuthorizationByUserCode(ByVal userCode As String) As Task(Of List(Of BillingAuthorization))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.ListBillingAuthorizationByUserCodeAsync(userCode)
    End Function

    Public Async Function GetATCPOSByATCNoPOSAndAdmissionCode(listProductATC As List(Of Domain.Crystal.Entities.ProductATC), AdmissionCode As String, careGroupId As Integer) As Task(Of ActionResult(Of List(Of ProductATC)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.GetATCPOSByATCNoPOSAndAdmissionCodeAsync(listProductATC, AdmissionCode, careGroupId)
    End Function

    ''' <summary>
    ''' Obtiene un ingresos plano por su numero
    ''' </summary>
    ''' <returns>Ingreso plano</returns>
    Public Async Function GetRevenueControlPOCOByAdmissionCodeWithCareGroupAsync(ByVal admissionCode As String, admissionCaregroupId As Integer, patientCaregroupId As Integer, patientNit As String) As Task(Of ActionResult(Of Object))
        Dim res = Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.GetRevenueControlPOCOByAdmissionCodeWithCareGroupAsync(admissionCode, admissionCaregroupId, patientCaregroupId, patientNit)
        If res.StateResult Then
            If res IsNot Nothing AndAlso Not res.ObjectEmbbeded.Trim().Equals(String.Empty) Then
                Return New ActionResult(Of Object) With {.StateResult = True, .ObjectEmbbeded = Utils.DeserializeJsonToObject(res.ObjectEmbbeded)}
            Else
                Return New ActionResult(Of Object) With {.StateResult = False, .Message = "No se encontraron datos para la admisión"}
            End If
        Else
            Return New ActionResult(Of Object) With {.StateResult = res.StateResult, .Message = res.Message}
        End If
    End Function

    Public Async Function GetRevenueControlPOCOByCodeAsync(ByVal admissionCode As String) As Task(Of ActionResult(Of FolioDataHeader))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.GetControlPOCOByCodeAsync(admissionCode)
    End Function

    ''' <summary>
    ''' Gets the control poco by code.
    ''' </summary>
    ''' <param name="admissionCode">The admission code.</param>
    ''' <returns></returns>
    Public Async Function GetControlPOCOByCode(admissionCode As String) As Task(Of ActionResult(Of FolioDataHeader))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.GetControlPOCOByCodeAsync(admissionCode)

    End Function

    ''' <summary>
    ''' Busca solicitudes provenientes de ordenes medicas en una campaña que se encuentre en estado "Abierta, Cerrada y Procesada"
    ''' </summary>
    ''' <param name="AdmissionNumber">The admission code.</param>
    ''' <returns></returns>
    Public Function GetPendingRequestsToProcess(AdmissionNumber As String) As PendingRequestsCampaignForIngressXpo
        Dim filter = $"NUMINGRES = '{AdmissionNumber}'"
        Return XpoServiceEx.Instance(Me._indigoSessionValues.HisContainer).MixingStationService.GetXPOObject(Of PendingRequestsCampaignForIngressXpo)(filter)
    End Function

    Public Function ModifyAuthorizationAdmission(admissionNumber As String, authorizationNumber As String) As Task(Of ActionResult)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoCrystal.ModifyAuthorizationAdmissionAsync(admissionNumber, authorizationNumber)
    End Function

    Public Function GetINDIAGNOPByAdmissionNumber(admissionNumber As String) As INDIAGNOP
        Return IndigoConecta.Instancia.CurrentCloud.IndigoCrystal.GetINDIAGNOPByAdmissionNumber(admissionNumber)
    End Function

    ''' <summary>
    ''' Lista los ids de las facturas anuladas
    ''' </summary>
    ''' <returns></returns>
    Public Function ListAnnullateInvoiceIdByAdmission(admission As String) As List(Of Integer)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoBilling.ListAnnullateInvoiceIdByAdmission(admission)
    End Function

    ''' <summary>
    ''' Lista los ids de las facturas anuladas
    ''' </summary>
    ''' <returns></returns>
    Public Async Function ListAnnullateInvoiceIdByAdmissionAsync(admission As String) As Task(Of List(Of Integer))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.ListAnnullateInvoiceIdByAdmissionAsync(admission)
    End Function

    Public Function GetAdmissionsToLiquidationCollection(code As String) As XPCollection(Of CrystalRepository.ViewAdmissionsToLiquidation)
        Return XpoServiceEx.Instance(Me._indigoSessionValues.HisContainer).CrystalService.GetAdmissionObjectByNumIngres(code)
    End Function

    Public Function GetAdmissionObjectByNumIngresAndIINGREPOR(code As String) As XPCollection(Of CrystalRepository.ViewAdmissionsToLiquidation)
        Return XpoServiceEx.Instance(Me._indigoSessionValues.HisContainer).CrystalService.GetAdmissionObjectByNumIngresAndIINGREPOR(code)
    End Function

    Public Function GetAdmissionsToLiquidationConfirmCollection(code As String) As XPCollection(Of CrystalRepository.ViewAdmissionsToLiquidationConfirm)
        Return XpoServiceEx.Instance(Me._indigoSessionValues.HisContainer).CrystalService.GetAdmissionConfirmByNumIngres(code)
    End Function

    Public Function GetAdmissionConfirmByNumIngresAndIINGREPOR(code As String) As XPCollection(Of CrystalRepository.ViewAdmissionsToLiquidationConfirm)
        Return XpoServiceEx.Instance(Me._indigoSessionValues.HisContainer).CrystalService.GetAdmissionConfirmByNumIngresAndIINGREPOR(code)
    End Function

    Public Async Function GetPharmaceuticalDispensionAndDevolutionWithOutConfirm(admissionNumber As String) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.GetPharmaceuticalDispensionAndDevolutionWithOutConfirmAsync(admissionNumber)
    End Function

    Public Function ListRIPSInvoices(dateInit As DateTime?, dateEnd As DateTime?, invoiceType As Byte?, fechaCorte As DateTime?, entityId As String, careGroupId As String, AdmissionCode As String,
                                     careCenterId As String, contractId As String, categoryId As String, incomeCauseId As String, typeRiskId As String, Optional Reso676 As Boolean = False) As XPCollection(Of ViewRIPSInvoice)

        Return XpoServiceEx.Instance(Me._indigoSessionValues.TransactionalContainer).BillingService.ListRIPSInvoices(dateInit, dateEnd, invoiceType, fechaCorte, entityId, careGroupId, AdmissionCode,
                                                                careCenterId, contractId, categoryId, incomeCauseId, typeRiskId, Reso676)
    End Function

    Public Function ListRIPSInvoicesPopulationGroup(dateInit As DateTime?, dateEnd As DateTime?, invoiceType As Byte?, fechaCorte As DateTime?, entityId As String, careGroupId As String, AdmissionCode As String,
                                     careCenterId As String, contractId As String, categoryId As String, incomeCauseId As String, typeRiskId As String, populationGroupId As String) As XPCollection(Of ViewRIPSInvoice)

        Return XpoServiceEx.Instance(Me._indigoSessionValues.TransactionalContainer).BillingService.ListRIPSInvoicesPopulationGroup(dateInit, dateEnd, invoiceType, fechaCorte, entityId, careGroupId, AdmissionCode,
                                                                careCenterId, contractId, categoryId, incomeCauseId, typeRiskId, populationGroupId)
    End Function

    ''' <summary>
    ''' Lista todos los ingresos
    ''' </summary>
    ''' <returns>Lista de ingresos</returns>
    Public Function ListAdmissionsToLiquidation() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Me._indigoSessionValues.HisContainer).CrystalService.ListAdmissionsToLiquidation()
    End Function

    ''' <summary>
    ''' Lista todos los ingresos
    ''' </summary>
    ''' <returns>Lista de ingresos</returns>
    Public Function ListAdmissionsToLiquidationByIINGREPOR() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Me._indigoSessionValues.HisContainer).CrystalService.ListAdmissionsToLiquidationByIINGREPOR()
    End Function

    Public Function ListAdmissionsToLiquidationDashboard(codenate As String) As XPCollection(Of CrystalRepository.ViewAdmissionsToLiquidationDashboard)
        Return XpoServiceEx.Instance(Me._indigoSessionValues.HisContainer).CrystalService.ListAdmissionsToLiquidationDashboard(codenate)
    End Function

    Public Function ListAdmissionsToLiquidationOncologycal(admissionCode As String, patientCode As String) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Me._indigoSessionValues.HisContainer).CrystalService.ListAdmissionsToLiquidationOncologycal(admissionCode, patientCode)
    End Function

    Public Function ListViewRelatedInvoices(AdmissionNumber As String) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Me._indigoSessionValues.TransactionalContainer).BillingService.ListViewRelatedInvoices(AdmissionNumber)
    End Function

    Public Function ListPortfolioAdvanceByAdmissionAndThirdPartyPatient(thirdPartyPatientId As Integer, ThirdPartyId As Integer, admissionCode As String) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Me._indigoSessionValues.TransactionalContainer).PortfolioService.GetPortfolioAdvanceByThirdPartyIdAndAdmission(thirdPartyPatientId, admissionCode, ThirdPartyId)
    End Function

    Public Function ListAdmissionsToLiquidationByAdmissionCode(admissionCode As String) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Me._indigoSessionValues.HisContainer).CrystalService.ListAdmissionsToLiquidation()
    End Function

    Public Function ListAdmissionsToLiquidationConfirm() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Me._indigoSessionValues.HisContainer).CrystalService.ListAdmissionsToLiquidationConfirm()
    End Function

    Public Function ListAdmissionsToLiquidationConfirmByIINGREPOR() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Me._indigoSessionValues.HisContainer).CrystalService.ListAdmissionsToLiquidationConfirmByIINGREPOR()
    End Function

    Public Function ListViewAdmissionsToLiquidationConfirmOnlyLiquidation() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Me._indigoSessionValues.HisContainer).CrystalService.ListViewAdmissionsToLiquidationConfirmOnlyLiquidation()
    End Function

    Public Function ListServiceOrderDetailSurgicalByServiceOrderDetailId(serviceorderDetailId As Integer) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Me._indigoSessionValues.TransactionalContainer).BillingService.ListServiceOrderDetailSurgicalByServiceOrderDetailId(serviceorderDetailId)
    End Function

    Public Function ListServiceOrderDetailBySurgeryNumberAndServiceOrderId(surgeryNumber As Byte, serviceOrderId As Integer) As XPCollection(Of ServiceOrderDetailXpo)
        Return XpoServiceEx.Instance(Me._indigoSessionValues.TransactionalContainer).BillingService.ListServiceOrderDetailBySurgeryNumberAndServiceOrderId(surgeryNumber, serviceOrderId)
    End Function

    Public Function GetSettingsBillingByOperatingUnitId(OperatingUnitId As Integer) As SettingsBillingXpo
        Return XpoServiceEx.Instance(Me._indigoSessionValues.TransactionalContainer).BillingService.GetSettingsBillingByOperatingUnitId(OperatingUnitId)
    End Function

    Public Function ListInvoiceCategoryByPermissionCategories(PermissionCategories As Integer, careGroupId As Integer) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Me._indigoSessionValues.TransactionalContainer).BillingService.ListInvoiceCategoryByPermissionCategories(PermissionCategories, _indigoSessionValues.UserIndigoId, careGroupId)
    End Function

    Public Function GetCategoriesByStatus(status As Boolean) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Me._indigoSessionValues.TransactionalContainer).BillingService.GetCategoriesByStatusAndUser(status, _indigoSessionValues.UserIndigoId)
    End Function

    Public Function GetCourtDate(revenueControlId As Integer) As Date
        Return XpoServiceEx.Instance(Me._indigoSessionValues.TransactionalContainer).BillingService.GetCourtDate(revenueControlId)
    End Function

    Public Function GetInvoiceByAdminssionNumber(admissionNumber As String) As InvoiceXpo
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).BillingService.GetInvoiceByAdminssionNumber(admissionNumber)
    End Function

    Public Function ListInvoiceByAdminssionNumber(admissionNumber As String) As XPCollection(Of InvoiceXpo)
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).BillingService.ListInvoiceByAdminssionNumber(admissionNumber)
    End Function

    Public Function ListAllINDIAGNOS() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Me._indigoSessionValues.HisContainer).CrystalService.ListAllINDIAGNOS()
    End Function

    Public Function GetAdmissionXpo(admissionNumber As String) As Infrastructure.Data.Xpo.CrystalRepository.AdmissionXpo
        Return XpoServiceEx.Instance(Me._indigoSessionValues.HisContainer).CrystalService.GetAdmissionXpo(admissionNumber)
    End Function

    Public Function GetHCREGEGREByAdmissionNumber(admissionNumber As String) As Infrastructure.Data.Xpo.CrystalRepository.HCREGEGRE
        Return XpoServiceEx.Instance(Me._indigoSessionValues.HisContainer).CrystalService.GetHCREGEGREByAdmissionNumber(admissionNumber)
    End Function
    Public Function GetCHREGEGREByAdmissionNumber(admissionNumber As String) As Infrastructure.Data.Xpo.CrystalRepository.CHREGEGRE
        Return XpoServiceEx.Instance(Me._indigoSessionValues.HisContainer).CrystalService.GetCHREGEGREByAdmissionNumber(admissionNumber)
    End Function

    ''' <summary>
    ''' Lista las ordenes de servicio por admisión y que no se encuentren dentro del listado de id que se envían
    ''' </summary>
    ''' <returns></returns>
    Public Function ListServiceOrderDetailByAdmissionNumberAndNotListServiceOrderDetailId(admissionNumber As String, listId As List(Of Integer)) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Me._indigoSessionValues.TransactionalContainer).BillingService.ListServiceOrderDetailByAdmissionNumberAndNotListServiceOrderDetailId(admissionNumber, listId)
    End Function

    ''' <summary>
    ''' lista los motivos de anulación
    ''' </summary>
    ''' <returns></returns>
    Public Function ListAnnulmentReason() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Me._indigoSessionValues.TransactionalContainer).BillingService.ListAnnulmentReason()
    End Function

    ''' <summary>
    ''' lista las condiciones de venta
    ''' </summary>
    ''' <returns></returns>
    Public Async Function ListConditionSales() As Task(Of List(Of ConditionSalesXpo))
        Dim filter = $"Status = 1 AND ConditionSalesUserXpo[UserId={_indigoSessionValues.UserIndigoId}]"
        Return Await Task.Factory.StartNew(Function() As List(Of ConditionSalesXpo)
                                               Return XpoServiceEx.Instance(Me._indigoSessionValues.TransactionalContainer).BillingService.GetCollectionAsList(Of ConditionSalesXpo)(Nothing, filter)
                                           End Function)
    End Function

    ''' <summary>
    ''' lista las actividades economicas del tercero
    ''' </summary>
    ''' <returns></returns>
    Public Async Function ListEconomicActivities(_ThirdPartyId As Integer) As Task(Of List(Of CommonEconomicActivity))
        Dim filter = $"Status = 1 AND CommonThirdPartyEconomicActivitiesXpo[ThirdPartyId.Id={_ThirdPartyId}]"
        Return Await Task.Factory.StartNew(Function() As List(Of CommonEconomicActivity)
                                               Return XpoServiceEx.Instance(Me._indigoSessionValues.TransactionalContainer).CommonService.GetCollectionAsList(Of CommonEconomicActivity)(Nothing, filter)
                                           End Function)
    End Function

    Function ListIpsService() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).ContractService.ListIPSServicesByStatus(True)
    End Function

    ''' <summary>
    ''' Liquidates the folio.
    ''' </summary>
    ''' <returns></returns>
    Public Async Function LiquidateFolioAsync(revenueControlDetailCrossingList As List(Of RevenueControlDetailCrossing), patientCode As String, admissionNumber As String, BillingAuthorizationId As Decimal, OperativeUnitId As Integer, ThirdPartyPatientId As Integer, skipAccountControlValidations As Boolean) As Task(Of ActionResult(Of List(Of InvoiceResult)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.LiquidateFolioAsync(revenueControlDetailCrossingList, patientCode, admissionNumber, _indigoSessionValues, BillingAuthorizationId, OperativeUnitId, ThirdPartyPatientId, skipAccountControlValidations)
    End Function

    ''' <summary>
    ''' Ejecuta un método de acción
    ''' </summary>
    ''' <param name="actionMethod">Método de acción a ejecutar</param>
    ''' <param name="arguments">Objeto dinámico con los argumentos del método</param>
    ''' <returns>Resultado de la acción</returns>
    Public Async Function ExecuteActionMethod(ByVal actionMethod As LiquidationActionMethod, ByVal arguments As Object) As Task(Of ActionResult(Of String))
        arguments.TransactionContainer = _indigoSessionValues.TransactionalContainer
        Dim args As String = Utils.SerializeObjectToJson(arguments)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.ExecuteActionMethodAsync(actionMethod, args)
    End Function

    ''' <summary>
    ''' Distributes the folio.
    ''' </summary>
    ''' <param name="objParams">The object parameters.</param>
    ''' <param name="listServiceOrderDetail">Listado de Detallles de ordenes de servicio cuando en la retarificación había algún item quirurgico</param>
    ''' <returns></returns>
    Public Async Function DistributeFolio(objParams As Object, homologations As List(Of Homologation), listServiceOrderDetail As List(Of ServiceOrderDetail)) As Task(Of ActionResult(Of List(Of Homologation), List(Of ServiceOrderDetail)))
        Dim args As String = Utils.SerializeObjectToJson(objParams)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.DistributeFolioAsync(args, homologations, listServiceOrderDetail)
    End Function

    ''' <summary>
    ''' Executes the action method simple.
    ''' </summary>
    ''' <param name="actionMethod">The action method.</param>
    ''' <param name="arguments">The arguments.</param>
    ''' <returns></returns>
    Public Function ExecuteActionMethodSimple(ByVal actionMethod As LiquidationActionMethod, ByVal arguments As Object) As ActionResult(Of String)
        arguments.TransactionContainer = _indigoSessionValues.TransactionalContainer
        Dim args As String = Utils.SerializeObjectToJson(arguments)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoBilling.ExecuteActionMethod(actionMethod, args)
    End Function

    ''' <summary>
    ''' Packages the items.
    ''' </summary>
    ''' <param name="serviceOrderDetail">The service order detail.</param>
    ''' <param name="arguments">The arguments.</param>
    ''' <returns></returns>
    Public Async Function PackageItems(ByVal serviceOrderDetail As ServiceOrderDetail, ByVal arguments As Object) As Task(Of ActionResult(Of String))
        Dim args As String = Utils.SerializeObjectToJson(arguments)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.PackageItemsAsync(serviceOrderDetail, args, _indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Realiza la retarificación de servicios en un folio
    ''' </summary>
    ''' <param name="idFolio">Id del folio a retarificar</param>
    ''' <param name="careGroupId">Id del nuevo grupo de atención</param>
    ''' <param name="patientGenus">Género del paciente. 1-Masculino, 2-Femenino</param>
    ''' <param name="patientBirth">Fecha de nacimiento del paciente</param>
    ''' <returns>Resultado de la acción</returns>
    Public Async Function ChangeRateServices(ByVal idFolio As Integer, ByVal careGroupId As Integer, ByVal patientGenus As Integer, ByVal patientBirth As Date,
                                             ByVal listHomologations As List(Of Homologation), onlyRateChange As Boolean, ThirdPartyPatientId As Integer, HealthAdministratorId As Integer,
                                             listServiceOrderDetailWithQx As List(Of ServiceOrderDetail), operativeUnitId As Integer?) As Task(Of ActionResult(Of List(Of Homologation), List(Of ServiceOrderDetail)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.ChangeRateServicesAsync(idFolio, careGroupId, patientGenus, patientBirth, listHomologations, onlyRateChange, ThirdPartyPatientId, HealthAdministratorId, listServiceOrderDetailWithQx, operativeUnitId)
    End Function

    ''' <summary>
    ''' Lista las estancias que se encuentran liquidadas por su numero de ingreso
    ''' </summary>
    ''' <param name="admissionCode">Numero de ingreso</param>
    ''' <returns>Lista de estancias liquidadas</returns>
    Public Async Function ListLiquidatedStaysByAdmissionCode(ByVal admissionCode As String) As Task(Of Domain.Base.Entities.ActionResult(Of List(Of Domain.Crystal.Entities.CHREGESTA)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCrystal.ListLiquidatedStaysByAdmissionCodeAsync(admissionCode, True)
    End Function

    ''' <summary>
    ''' Lista las estancias que no se encuentran liquidadas por su numero de ingreso
    ''' </summary>
    ''' <param name="admissionCode">Numero de ingreso</param>
    ''' <param name="caregroupId">Id del grupo de atención</param>
    ''' <param name="medicalOrderDate">Fechs de la orden medica para hospitalización</param>
    ''' <param name="endDate">Fecha de finalización o corte</param>
    ''' <param name="asNoTracking">Valor que indica si se consulta las estancias con seguimiento</param>
    ''' <returns>Lista de estancias liquidadas</returns>
    Public Async Function ListDontLiquidatedStaysByAdmissionCode(ByVal admissionCode As String, ByVal caregroupId As Integer, stayOption As Domain.Entities.eLiquidateStayOption, ByVal medicalOrderDate As DateTime?, ByVal endDate As DateTime?, ByVal asNoTracking As Boolean) As Task(Of Domain.Base.Entities.ActionResult(Of List(Of Domain.Crystal.Entities.CHREGESTA)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCrystal.ListDontLiquidatedStaysByAdmissionCodeAsync(admissionCode, caregroupId, stayOption, medicalOrderDate, endDate, asNoTracking)
    End Function

    ''' <summary>
    ''' Realiza la persistencia de la liquidación de estancias y genera la orden de servicio
    ''' </summary>
    ''' <param name="admissionCode">Numero de ingreso</param>
    ''' <param name="caregroupId">Id del grupo de atención</param>
    ''' <param name="medicalOrderDate">Fechs de la orden medica para hospitalización</param>
    ''' <param name="endDate">Fecha de finalización o corte</param>
    ''' <returns>Resultado de la acción</returns>
    Public Async Function LiquidateStays(ByVal admissionCode As String, healthAdministratorId As Integer, thirdPartyId As Integer, ByVal caregroupId As Integer, stayOption As Domain.Entities.eLiquidateStayOption, ByVal medicalOrderDate As DateTime?, ByVal endDate As DateTime?, ByVal patientCode As String, ByVal idSequence As Integer, ByVal idOperatingUnit As Integer, ByVal audit As AuditMessage) As Task(Of Domain.Base.Entities.ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCrystal.LiquidateStaysAsync(admissionCode, healthAdministratorId, thirdPartyId, caregroupId, stayOption, medicalOrderDate, endDate, patientCode, idSequence, idOperatingUnit, audit)
    End Function

    ''' <summary>
    ''' Lista todas las autorizaciones de facturación por código de usuario
    ''' </summary>
    ''' <returns></returns>
    Public Function LoadBillingAuthorization() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Me._indigoSessionValues.TransactionalContainer).BillingService.ListAllBillingAuthorizationByUserCode(_indigoSessionValues.UserIndigo)
    End Function

    Function GetAdmissionByNumIngres(admissionNumber As String) As XPCollection(Of Infrastructure.Data.Xpo.CrystalRepository.ViewAdmissionsToLiquidation)
        Return XpoServiceEx.Instance(Me._indigoSessionValues.HisContainer).CrystalService.GetAdmissionByNumIngres(admissionNumber)
    End Function

    Function GetAdmissionConfirmByNumIngres(admissionNumber As String) As XPCollection(Of CrystalRepository.ViewAdmissionsToLiquidationConfirm)
        Return XpoServiceEx.Instance(Me._indigoSessionValues.HisContainer).CrystalService.GetAdmissionConfirmByNumIngres(admissionNumber)
    End Function

    Public Async Function GetAdmissionStatusByNumIngresAsync(admissionNumber As String) As Task(Of String)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCrystal.GetAdmissionStatusByNumIngresAsync(admissionNumber)
    End Function

    Public Function LiquidateItemProductionAsync(serviceOrderDetailIds As List(Of Integer)) As Task(Of ActionResult)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoBilling.LiquidateItemProductionAsync(serviceOrderDetailIds(0), _indigoSessionValues.AuditMessageWcf)
    End Function

    ' ''' <summary>
    ' ''' Updates the revenue control detail values.
    ' ''' </summary>
    ' ''' <param name="revenueControlDetailId">The revenue control detail identifier.</param>
    ' ''' <returns></returns>
    'Public Async Function UpdateRevenueControlDetailValues(revenueControlDetailId As Integer) As Task(Of Domain.Base.Entities.ActionResult)
    '    Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.UpdateRevenueControlDetailValuesAsync(revenueControlDetailId)
    'End Function

    Public Async Function ReclasificateDistributions(serviciosAReclasificar As List(Of Integer), revenueControlId As Integer) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.ReclasificateDistributionsAsync(serviciosAReclasificar, revenueControlId, _indigoSessionValues.UserIndigo, _indigoSessionValues.TransactionalContainer)
    End Function


    Public Async Function SaveApplyRecoveryFeeServiceOrderDetailDistribution(ServiceOrderDetailDistributionId As Integer, ApplyRecoveryFee As Integer) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.SaveApplyRecoveryFeeServiceOrderDetailDistributionAsync(ServiceOrderDetailDistributionId, ApplyRecoveryFee)
    End Function

    ''' <summary>
    ''' Obtiene el grupo de atención
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetCareGroupById(CareGroupId As Integer) As ContractCareGroupReportXpo
        Dim filter As String = "Id = " & CareGroupId
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).ContractService.GetCollection(Of ContractCareGroupReportXpo)(Nothing, filter).FirstOrDefault()
    End Function

    ''' <summary>
    ''' funcion para traer la configuracion del modulo de central de mezclas
    ''' </summary>
    ''' <returns></returns>
    Public Async Function ListAllCMConfigAsync() As Task(Of ActionResult(Of MixingStationSetting))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.GetMixingStationSettingByOperativeUnitIdAsync(_indigoSessionValues.IndigoOperatingUnitId, _indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' servicio rest para Recalcular los valores de folio
    ''' </summary>
    ''' <param name="admissionNumber"></param>
    ''' <param name="RevenueControlDetailId"></param>
    ''' <returns></returns>
    Public Function RecalculateFolioRest(ByVal admissionNumber As String, ByVal RevenueControlDetailId As Integer) As ActionResult(Of String)

        Dim endpoint = SessionValues.Instance.GetEndpointByCode(EndpointCodes.Revenue_Cycle)
        Dim client = New RestClient(String.Format("{0}/billing/RecalculateFolio/{1}/{2}", endpoint.UrlBase, admissionNumber, RevenueControlDetailId))
        client.Authenticator = New BearerTokenAuthenticator()

        Dim req = New RestRequest()
        req.AddHeader("_ContainerName_", SessionValues.Instance.TransactionalContainer)
        req.AddHeader("_ContainerHisName_", SessionValues.Instance.HisContainer)
        Dim response = client.ExecuteAsync(Of ServiceResponse(Of String))(req)
        response.Wait()

        If response?.Result?.Data Is Nothing Then
            Return New ActionResult(Of String) With {.StateResult = False, .Message = response?.Result?.StatusDescription}
        End If

        Return New ActionResult(Of String) With {.StateResult = response.Result.Data.Status, .Message = response.Result.Data.Message}
    End Function

    ''' <summary>
    ''' Obtiene la lista del valor del Iva devuelto por metodo de pago
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetTaxDevolutionByRevenueControlDetail(revenueControlDetailId As Integer, PaymentMethodType As Byte) As ViewTaxDevolutionXpo
        Dim filter As String = $"RevenueControlDetailId = {revenueControlDetailId} AND PaymentMethodType = {PaymentMethodType}"
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).BillingService.GetXPOObject(Of ViewTaxDevolutionXpo)(filter)
    End Function

    ''' <summary>
    ''' obtiene los datos de la pre-factura por medio de un servicio REST
    ''' </summary>
    ''' <param name="revenueControlDetailId"></param>
    ''' <param name="CurrencyId"></param>
    ''' <param name="dateTRM"></param>
    ''' <returns></returns>
    Public Async Function GetVReportInvoicePartial(revenueControlDetailId As Integer,
                                                Optional CurrencyId As Integer? = Nothing,
                                                Optional dateTRM As Date? = Nothing) As Task(Of ActionResult(Of InvoicePartialMasterAccount))
        Dim endpoint = SessionValues.Instance.GetEndpointByCode(EndpointCodes.Revenue_Cycle)
        If endpoint IsNot Nothing Then
            Dim client = New RestClient(String.Format("{0}/billing/GetVReportInvoicePartial/{1}/{2}/{3}", endpoint.UrlBase, revenueControlDetailId, CurrencyId, dateTRM?.ToString("yyyy-MM-dd")))
            client.Authenticator = New BearerTokenAuthenticator()

            Dim req = New RestRequest()
            req.AddHeader("_ContainerName_", SessionValues.Instance.TransactionalContainer)
            req.AddHeader("_ContainerHisName_", SessionValues.Instance.HisContainer)
            Dim response = Await client.ExecuteAsync(Of ServiceResponse(Of InvoicePartialMasterAccount))(req)

            If response?.Data Is Nothing OrElse Not response?.Data?.Status Then
                Return New ActionResult(Of InvoicePartialMasterAccount) With {.StateResult = False, .Message = response?.Data?.Message}
            End If
            Return New ActionResult(Of InvoicePartialMasterAccount) With {.StateResult = True, .ObjectEmbbeded = response?.Data.Data}
        Else
            Return New ActionResult(Of InvoicePartialMasterAccount) With {.StateResult = False, .Message = "Error al establecer el endpoint"}
        End If
    End Function

    ''' <summary>
    ''' Obtiene una lista de ordenes de servicios de una admisión para incluir a otro servicio
    ''' </summary>
    ''' <param name="listIds">Lista de IDs a excluir</param>
    ''' <param name="admissionNumber">Número de admisión</param>
    ''' <returns>Lista de detalles de orden de servicio</returns>
    Public Async Function GetListServiceOrderDetail(listIds As List(Of Integer), admissionNumber As String) As Task(Of List(Of SP_GetListServiceOrderDetail))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.GetListServiceOrderDetailAsync(listIds, admissionNumber)
    End Function

    ''' <summary>
    ''' Obtiene una lista de ingresos unificados por la lista de ids de folios y el numero de ingreso cabecera
    ''' </summary>
    ''' <param name="listFoliosIds">Lista de ids de folios</param>
    ''' <param name="admissionNumber">Numero de ingreso cabecera</param>
    ''' <returns>Lista de ingresos unificados</returns>
    Public Async Function GetListUnifiedAdmissions(listFoliosIds As List(Of Integer), admissionNumber As String) As Task(Of ActionResult(Of List(Of ADINGRESO)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.GetListUnifiedAdmissionsAsync(listFoliosIds, admissionNumber)
    End Function

    ''' <summary>
    ''' Valida si el tercero cumple con la mayoría de edad para facturación
    ''' </summary>
    ''' <param name="thirdPartyId">Id del tercero a validar</param>
    ''' <param name="operativeUnitId">Id de la unidad operativa</param>
    ''' <param name="admissionNumber">Número de ingreso para buscar responsable sugerido</param>
    ''' <returns>Resultado de la validación con información del responsable sugerido si aplica</returns>
    Public Async Function ValidateAgeOfMajorityForLiquidationAsync(thirdPartyId As Integer, operativeUnitId As Integer, admissionNumber As String) As Task(Of ActionResult(Of AgeValidationResult))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.ValidateAgeOfMajorityForLiquidationAsync(thirdPartyId, operativeUnitId, admissionNumber)
    End Function

    ''' <summary>
    ''' Verifica si el parámetro de validación de mayoría de edad está activo
    ''' </summary>
    ''' <param name="operativeUnitId">Id de la unidad operativa</param>
    ''' <returns>True si el parámetro está activo</returns>
    Public Function IsAgeValidationEnabled(operativeUnitId As Integer) As Boolean
        Return IndigoConecta.Instancia.CurrentCloud.IndigoBilling.IsAgeValidationEnabled(operativeUnitId)
    End Function
#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
