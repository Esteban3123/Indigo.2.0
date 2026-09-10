'***********************************************************************
' Assembly         : DistributedServices.Billing
' Author           : Juan F. Tamayo
' Created          : 2014-11-19
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Application.Billing
Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel
Imports Domain.Base
Imports Domain.Entities
Imports System.Data.SqlClient
Imports System.Configuration
Imports Domain.Base.Entities
Imports System.Text
Imports Microsoft.Practices.Unity
Imports DistribuitedServices.Billing

#End Region

Partial Class BillingService
    Implements IBillingServiceLiquidation

    Public Function GetAdmissionPOCOByAdmissionCode(admissionCode As String) As Entities.ActionResult(Of String) Implements IBillingServiceLiquidation.GetAdmissionPOCOByAdmissionCode
        Using service As ILiquidationAdminService = Container.Current.Resolve(Of ILiquidationAdminService)()
            Return service.GetAdmissionPOCOByAdmissionCode(admissionCode)
        End Using
        'Return Me._liquidationAdminService.GetAdmissionPOCOByAdmissionCode(admissionCode)
    End Function

    ''' <summary>
    ''' is the billing service liquidation execute query dt.
    ''' </summary>
    ''' <param name="query">The query.</param>
    ''' <returns></returns>
    Private Function IBillingServiceLiquidation_ExecuteQueryDt(query As String, container As String) As DataTable Implements IBillingServiceLiquidation.ExecuteQueryDt
        Dim conx = String.Format(ConfigurationManager.ConnectionStrings(Infrastructure.CrossCutting.Base.ConfigurationFile.CONX_GENESIS).ConnectionString, "", container)
        Dim sqlWebConexion = New SqlConnection(conx)
        Dim da As SqlDataAdapter = New SqlDataAdapter(query, sqlWebConexion)
        da.SelectCommand.CommandTimeout = 90

        If sqlWebConexion.State = ConnectionState.Closed Then
            sqlWebConexion.Open()
        End If

        Dim ds As New DataSet
        da.Fill(ds, "Datos")
        Dim dtResult As DataTable = ds.Tables("Datos")
        da = Nothing
        ds = Nothing
        sqlWebConexion.Close()
        Return dtResult
    End Function

    Public Function ExecuteQuery(ByVal Comando As String, container As String) As Boolean Implements IBillingServiceLiquidation.ExecuteQuery
        Dim conx = String.Format(ConfigurationManager.ConnectionStrings(Infrastructure.CrossCutting.Base.ConfigurationFile.CONX_GENESIS).ConnectionString, "", container)
        Dim sqlWebConexion = New SqlConnection(conx)
        Dim SQL As New SqlCommand(Comando, sqlWebConexion)
        'aumento el tiempo
        SQL.CommandTimeout = 90
        'tipo de comando
        SQL.CommandType = CommandType.Text
        Try
            sqlWebConexion.Open()
            'Ejecutar
            SQL.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Throw ex
        Finally
            SQL.Dispose()
            sqlWebConexion.Close()
            sqlWebConexion.Dispose()
        End Try
    End Function

    Public Function ReclasificateDistributions(listDistributions As List(Of Integer), revenueControlId As Integer, userCode As String, container As String) As ActionResult Implements IBillingServiceLiquidation.ReclasificateDistributions
        Try
            Dim xmlData As New StringBuilder()
            xmlData.AppendLine("<ServiceOrderDetailDistributionIds>")
            For Each id In listDistributions
                xmlData.AppendLine($"<Ids><Id>{id}</Id></Ids>")
            Next
            xmlData.AppendLine("</ServiceOrderDetailDistributionIds>")

            Dim conx = String.Format(ConfigurationManager.ConnectionStrings(Infrastructure.CrossCutting.Base.ConfigurationFile.CONX_GENESIS).ConnectionString, "", container)
            Using connection As New SqlConnection(conx)
                connection.Open()
                Dim command As New SqlCommand("[Billing].[SP_ReclasificateDistributions]")
                Dim transaction As SqlTransaction
                transaction = connection.BeginTransaction()
                Try
                    command.Connection = connection
                    command.Transaction = transaction
                    command.CommandType = CommandType.StoredProcedure

                    command.Parameters.Add(New SqlParameter("@ServiceOrderDetailDistributionIds", xmlData.ToString()))
                    command.Parameters.Add(New SqlParameter("@RevenueControlId", revenueControlId))
                    command.Parameters.Add(New SqlParameter("@UserCode", userCode))
                    Dim dt As New DataTable()
                    Using adapter As New SqlDataAdapter(command)
                        adapter.SelectCommand.CommandTimeout = 90
                        adapter.Fill(dt)
                    End Using

                    If CBool(dt.Rows(0).Item(0)) Then
                        transaction.Commit()
                        Return New ActionResult(True, dt.Rows(0).Item(1).ToString())
                    Else
                        transaction.Rollback()
                        Return New ActionResult(False, dt.Rows(0).Item(1).ToString())
                    End If
                Catch ex As Exception
                    transaction.Rollback()
                    Return New ActionResult(False, ex.Message)
                Finally
                    connection.Close()
                End Try
            End Using
        Catch ex As Exception
            Return New ActionResult(False, ex.Message)
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un ingreso y sus agregados en una entidad plana y serializada en formato JSON
    ''' </summary>
    ''' <param name="code">Código del ingreso</param>
    ''' <returns>Entidad plana serializada</returns>
    Public Function GetAdmissionPOCOByCode(code As String) As Entities.ActionResult(Of String) Implements IBillingService.GetAdmissionPOCOByCode
        Using service As ILiquidationAdminService = Container.Current.Resolve(Of ILiquidationAdminService)()
            Return service.GetAdmissionPOCOByCode(code)
        End Using
        'Return Me._liquidationAdminService.GetAdmissionPOCOByCode(code)
    End Function

    ''' <summary>
    ''' Ejecuta el método de acción seleccionado
    ''' </summary>
    ''' <param name="actionMethod">Método de acción seleccionado</param>
    ''' <param name="arguments">Objeto dinámico con los argumentos del método de acción
    ''' seleccionado, serializado en formato JSON</param>
    ''' <returns>Resultado de la ejecución del método de acción</returns>
    Public Function ExecuteActionMethod(actionMethod As Domain.Entities.LiquidationActionMethod, arguments As String) As Domain.Base.Entities.ActionResult(Of String) Implements IBillingServiceLiquidation.ExecuteActionMethod
        Using service As ILiquidationAdminService = Container.Current.Resolve(Of ILiquidationAdminService)()
            Return service.ExecuteActionMethod(actionMethod, arguments)
        End Using
        'Return Me._liquidationAdminService.ExecuteActionMethod(actionMethod, arguments)
    End Function

    ''' <summary>
    ''' Realiza la retarificación de servicios en un folio
    ''' </summary>
    ''' <param name="idFolio">Id del folio a retarificar</param>
    ''' <param name="careGroupId">Id del nuevo grupo de atención</param>
    ''' <param name="patientGenus">Género del paciente. 1-Masculino, 2-Femenino</param>
    ''' <param name="patientBirth">Fecha de nacimiento del paciente</param>
    ''' <returns>Resultado de la acción</returns>
    Public Function ChangeRateServices(idFolio As Integer, careGroupId As Integer, patientGenus As Integer, patientBirth As Date, listHomologations As List(Of Domain.Entities.Homologation), onlyRateChange As Boolean, ThirdPartyPatientId As String, HealthAdministratorId As Integer,
                                       listServiceOrderDetailWithQx As List(Of Domain.Entities.ServiceOrderDetail), operativeUnitId As Integer?) As Domain.Base.Entities.ActionResult(Of List(Of Domain.Entities.Homologation), List(Of Domain.Entities.ServiceOrderDetail)) Implements IBillingServiceLiquidation.ChangeRateServices
        Using service As ILiquidationAdminService = Container.Current.Resolve(Of ILiquidationAdminService)()
            Return service.ChangeRateServices(idFolio, careGroupId, patientGenus, patientBirth, listHomologations, onlyRateChange, ThirdPartyPatientId, HealthAdministratorId, listServiceOrderDetailWithQx, operativeUnitId)
        End Using
        'Return Me._liquidationAdminService.ChangeRateServices(idFolio, careGroupId, patientGenus, patientBirth, listHomologations, onlyRateChange, ThirdPartyPatientId, HealthAdministratorId, listServiceOrderDetailWithQx)
    End Function

    ''' <summary>
    ''' Funcion encargada de empaquetar los items seleccionados
    ''' </summary>
    ''' <param name="serviceOrderDetail">The service order detail.</param>
    ''' <param name="arguments">The arguments.</param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function PackageItems(serviceOrderDetail As Domain.Entities.ServiceOrderDetail, arguments As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of String) Implements IBillingServiceLiquidation.PackageItems
        Using service As ILiquidationAdminService = Container.Current.Resolve(Of ILiquidationAdminService)()
            Return service.PackageItems(serviceOrderDetail, arguments, audit)
        End Using
        'Return Me._liquidationAdminService.PackageItems(serviceOrderDetail, arguments, audit)
    End Function

    ''' <summary>
    ''' Distributes the folio.
    ''' </summary>
    ''' <param name="listServiceOrderDetail">Listado de Detallles de ordenes de servicio cuando en la retarificación había algún item quirurgico</param>
    ''' <param name="objParams"></param>
    ''' <returns></returns>
    Public Function DistributeFolio(objParams As Object, homologations As List(Of Domain.Entities.Homologation), listServiceOrderDetail As List(Of Domain.Entities.ServiceOrderDetail)) As Domain.Base.Entities.ActionResult(Of List(Of Domain.Entities.Homologation), List(Of Domain.Entities.ServiceOrderDetail)) Implements IBillingServiceLiquidation.DistributeFolio
        Using service As ILiquidationAdminService = Container.Current.Resolve(Of ILiquidationAdminService)()
            Return service.DistributeFolio(objParams, homologations, listServiceOrderDetail)
        End Using
        'Return Me._liquidationAdminService.DistributeFolio(objParams, homologations, listServiceOrderDetail)
    End Function

    ''' <summary>
    ''' Lista los ids de las facturas anuladas
    ''' </summary>
    ''' <returns></returns>
    Function ListAnnullateInvoiceIdByAdmission(admission As String) As List(Of Integer) Implements IBillingServiceLiquidation.ListAnnullateInvoiceIdByAdmission
        Using service As IInvoiceAdminService = Container.Current.Resolve(Of IInvoiceAdminService)()
            Return service.ListAnnullateInvoiceIdByAdmission(admission)
        End Using
        'Return Me._invoiceAdminService.ListAnnullateInvoiceIdByAdmission(admission)
    End Function

    ''' <summary>
    ''' Liquidates the folio.
    ''' </summary>
    ''' <returns></returns>
    Public Async Function LiquidateFolio(revenueControlDetailCrossingList As List(Of RevenueControlDetailCrossing), patientCode As String, admissionNumber As String, session As SessionValues, BillingAuthorizationId As Decimal, OperativeUnitId As Integer, ThirdPartyPatientId As Integer, skipAccountControlValidations As Boolean) As Task(Of ActionResult(Of List(Of InvoiceResult))) Implements IBillingServiceLiquidation.LiquidateFolio
        Using service As ILiquidationAdminService = Container.Current.Resolve(Of ILiquidationAdminService)()
            Return Await service.LiquidateFolio(revenueControlDetailCrossingList, patientCode, admissionNumber, session.HisContainer, BillingAuthorizationId, OperativeUnitId, ThirdPartyPatientId, skipAccountControlValidations, session.AuditMessageWcf, session)
        End Using
        'Return Me._liquidationAdminService.LiquidateFolio(revenueControlDetailCrossingList, patientCode, admissionNumber, containerCrystal, BillingAuthorizationId, OperativeUnitId, ThirdPartyPatientId, audit)
    End Function

    ''' <summary>
    ''' Gets the hcregegre by admission code.
    ''' </summary>
    ''' <param name="admissionCode">The admission code.</param>
    ''' <returns></returns>
    Public Function GetHCREGEGREByAdmissionCode(admissionCode As String) As Domain.Crystal.Entities.HCREGEGRE Implements IBillingServiceLiquidation.GetHCREGEGREByAdmissionCode
        Using service As ILiquidationAdminService = Container.Current.Resolve(Of ILiquidationAdminService)()
            Return service.GetHCREGEGREByAdmissionCode(admissionCode)
        End Using
        'Return Me._liquidationAdminService.GetHCREGEGREByAdmissionCode(admissionCode)
    End Function

    ''' <summary>
    ''' Gets the revenue control poco by admission code.
    ''' </summary>
    ''' <param name="admissionCode">The admission code.</param>
    ''' <returns></returns>
    Public Function GetRevenueControlPOCOByAdmissionCodeWithCareGroup(admissionCode As String, careGroupAdmissionId As Integer, patientCaregroupId As Integer, patientNit As String) As Entities.ActionResult(Of String) Implements IBillingServiceLiquidation.GetRevenueControlPOCOByAdmissionCodeWithCareGroup
        Using service As ILiquidationAdminService = Container.Current.Resolve(Of ILiquidationAdminService)()
            Return service.GetRevenueControlPOCOByAdmissionCodeWithCareGroup(admissionCode, careGroupAdmissionId, patientCaregroupId, patientNit)
        End Using
        'Return Me._liquidationAdminService.GetRevenueControlPOCOByAdmissionCodeWithCareGroup(admissionCode, careGroupAdmissionId, patientCaregroupId, patientNit)
    End Function

    Public Function GetControlPOCOByCode(admissionCode As String) As Entities.ActionResult(Of FolioDataHeader) Implements IBillingServiceLiquidation.GetControlPOCOByCode
        Using service As ILiquidationAdminService = Container.Current.Resolve(Of ILiquidationAdminService)()
            Return service.GetControlPOCOByCode(admissionCode)
        End Using
        'Return _liquidationAdminService.GetControlPOCOByCode(admissionCode)
    End Function

    ''' <summary>
    ''' Metodo para re enviar la notificación de la factura electrónica desde el formulario de trazabilidad electrónica
    ''' </summary>
    ''' <param name="listElectronicDocumentNotification">Lista de los registros que se van a persistir</param>
    ''' <returns></returns>
    Public Function SendNotification(listElectronicDocumentNotification As List(Of ElectronicDocumentNotification)) As ActionResult(Of String) Implements IBillingServiceLiquidation.SendNotification
        Using service As ILiquidationAdminService = Container.Current.Resolve(Of ILiquidationAdminService)()
            Return service.SendNotification(listElectronicDocumentNotification)
        End Using
        'Return _liquidationAdminService.StayManualLiquidation(admissionNumber, endDate, audit)
    End Function

    ''' <summary>
    ''' metodo para liquidar las estancias manualmente
    ''' </summary>
    ''' <param name="admissionNumber"></param>
    ''' <returns></returns>
    Public Function StayManualLiquidation(admissionNumber As String, ByVal endDate As DateTime, ByVal audit As AuditMessage) As Entities.ActionResult Implements IBillingServiceLiquidation.StayManualLiquidation
        Using service As ILiquidationAdminService = Container.Current.Resolve(Of ILiquidationAdminService)()
            Return service.StayManualLiquidation(admissionNumber, endDate, audit)
        End Using
        'Return _liquidationAdminService.StayManualLiquidation(admissionNumber, endDate, audit)
    End Function

    ''' <summary>
    ''' Obtiene una factura por numero de factura
    ''' </summary>
    ''' <param name="invoiceNumber"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetInvoiceByInvoiceNumber(invoiceNumber As String, audit As AuditMessage) As Domain.Entities.Invoice Implements IBillingServiceLiquidation.GetInvoiceByInvoiceNumber
        Using service As IInvoiceAdminService = Container.Current.Resolve(Of IInvoiceAdminService)()
            Return service.GetInvoiceByInvoiceNumber(invoiceNumber, audit)
        End Using
        'Return _invoiceAdminService.GetInvoiceByInvoiceNumber(invoiceNumber, audit)
    End Function

    Public Function GetATCPOSByATCNoPOSAndAdmissionCode(listProductATC As List(Of Domain.Crystal.Entities.ProductATC), AdmissionCode As String, caregroupId As Integer) As Entities.ActionResult(Of List(Of Domain.Crystal.Entities.ProductATC)) Implements IBillingServiceLiquidation.GetATCPOSByATCNoPOSAndAdmissionCode
        Using service As ILiquidationAdminService = Container.Current.Resolve(Of ILiquidationAdminService)()
            Return service.GetATCPOSByATCNoPOSAndAdmissionCode(listProductATC, AdmissionCode, caregroupId)
        End Using
        'Return _liquidationAdminService.GetATCPOSByATCNoPOSAndAdmissionCode(listProductATC, AdmissionCode, caregroupId)
    End Function

    Public Function GetPharmaceuticalDispensionAndDevolutionWithOutConfirm(admissionNumber As String) As Entities.ActionResult Implements IBillingServiceLiquidation.GetPharmaceuticalDispensionAndDevolutionWithOutConfirm
        Using service As ILiquidationAdminService = Container.Current.Resolve(Of ILiquidationAdminService)()
            Return service.GetPharmaceuticalDispensionAndDevolutionWithOutConfirm(admissionNumber)
        End Using
        'Return _liquidationAdminService.GetPharmaceuticalDispensionAndDevolutionWithOutConfirm(admissionNumber)
    End Function

    ''' <summary>
    ''' Funcion encargada de cambiar el estado del ingreso a 'Cerrado'
    ''' </summary>
    Public Function CloseAdmission(admissionNumber As String, containerCrystal As String, audit As AuditMessage) As Entities.ActionResult(Of SP_CloseAdmission_Result) Implements IBillingServiceLiquidation.CloseAdmission
        Using service As ILiquidationAdminService = Container.Current.Resolve(Of ILiquidationAdminService)()
            Return service.CloseAdmission(admissionNumber, containerCrystal, audit)
        End Using
    End Function

    Public Function SaveApplyRecoveryFeeServiceOrderDetailDistribution(ServiceOrderDetailDistributionId As Integer, ApplyRecoveryFee As Integer) As Entities.ActionResult Implements IBillingServiceLiquidation.SaveApplyRecoveryFeeServiceOrderDetailDistribution
        Using service As ILiquidationAdminService = Container.Current.Resolve(Of ILiquidationAdminService)()
            Return service.SaveApplyRecoveryFeeServiceOrderDetailDistribution(ServiceOrderDetailDistributionId, ApplyRecoveryFee)
        End Using
        'Return _liquidationAdminService.SaveApplyRecoveryFeeServiceOrderDetailDistribution(ServiceOrderDetailDistributionId, ApplyRecoveryFee)
    End Function

    Public Function SP_ReportBillingStatistics(InitialDate As Date, EndDate As Date, ReportType As Integer, AgrupedBy As Integer, DocumentType As String, StatusInvoice As Integer, CareCenterCodes As String, ThirdPartyIds As String, HealthAdministratorIds As String, CareGroupIds As String, UserCodes As String, session As SessionValues) As DataSet Implements IBillingServiceLiquidation.SP_ReportBillingStatistics
        Using service As ILiquidationAdminService = Container.Current.Resolve(Of ILiquidationAdminService)()
            Return service.SP_ReportBillingStatistics(InitialDate, EndDate, ReportType, AgrupedBy, DocumentType, StatusInvoice, CareCenterCodes, ThirdPartyIds, HealthAdministratorIds, CareGroupIds, UserCodes, session)
        End Using
    End Function

    Public Function ReportBillingStadistics(XmlCriterials As String, XmlFilters As String, session As SessionValues) As List(Of SP_ReportBillingStadistics_Result) Implements IBillingServiceLiquidation.ReportBillingStadistics
        Using service As ILiquidationAdminService = Container.Current.Resolve(Of ILiquidationAdminService)()
            Return service.ReportBillingStadistics(XmlCriterials, XmlFilters, session)
        End Using
    End Function

    Public Function ReportBillingStadisticsCount(XmlCriterials As String, XmlFilters As String, session As SessionValues) As SP_ReportBillingStadistics_Count_Result Implements IBillingServiceLiquidation.ReportBillingStadisticsCount
        Using service As ILiquidationAdminService = Container.Current.Resolve(Of ILiquidationAdminService)()
            Return service.ReportBillingStadisticsCount(XmlCriterials, XmlFilters, session)
        End Using
    End Function

    Public Function SPCH_ReportAdmissionStatistics(ParametrosString As String(), ParametrosDate As DateTime(), session As SessionValues) As DataTable Implements IBillingServiceLiquidation.SPCH_ReportAdmissionStatistics
        Using service As ILiquidationAdminService = Container.Current.Resolve(Of ILiquidationAdminService)()
            Return service.SPCH_ReportAdmissionStatistics(ParametrosString, ParametrosDate, session)
        End Using
    End Function

    Public Function AssociateInvoice(RevenueControlDetailId As Integer, InvoiceId As Integer, ByVal audit As AuditMessage) As ActionResult Implements IBillingServiceLiquidation.AssociateInvoice
        Using service As ILiquidationAdminService = Container.Current.Resolve(Of ILiquidationAdminService)()
            Return service.AssociateInvoice(RevenueControlDetailId, InvoiceId, audit)
        End Using
    End Function

    ''' <summary>
    ''' Guarda los códigos mipres
    ''' </summary>
    ''' <param name="serviceOrderDetailIds"></param>
    ''' <param name="mipresCodes"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function SaveMipresCodes(serviceOrderDetailIds As List(Of Integer), mipresCodes As List(Of MipresCode), audit As AuditMessage) As ActionResult Implements IBillingServiceLiquidation.SaveMipresCodes
        Using service As ILiquidationAdminService = Container.Current.Resolve(Of ILiquidationAdminService)()
            Return service.SaveMipresCodes(serviceOrderDetailIds, mipresCodes, audit)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene todos los códigos mipres por id de órden de servicio
    ''' </summary>
    ''' <param name="serviceOrderDetailId"></param>
    ''' <returns></returns>
    Public Function GetMipresByServiceOrderDetailId(serviceOrderDetailId As Integer) As List(Of MipresCode) Implements IBillingServiceLiquidation.GetMipresByServiceOrderDetailId
        Using service As ILiquidationAdminService = Container.Current.Resolve(Of ILiquidationAdminService)()
            Return service.GetMipresByServiceOrderDetailId(serviceOrderDetailId)
        End Using
    End Function

    ''' <summary>
    ''' Elimina un mipre
    ''' </summary>
    ''' <param name="mipresCodeId"></param>
    ''' <returns></returns>
    Public Function DeleteMipresCode(mipresCodeId As Integer) As ActionResult Implements IBillingServiceLiquidation.DeleteMipresCode
        Using service As ILiquidationAdminService = Container.Current.Resolve(Of ILiquidationAdminService)()
            Return service.DeleteMipresCode(mipresCodeId)
        End Using
    End Function

    ''' <summary>
    ''' Liquida los detalles de producción
    ''' </summary>
    ''' <returns></returns>
    Public Function LiquidateDetailProduction(serviceOrderDetailId As Integer, audit As AuditMessage) As ActionResult Implements IBillingServiceLiquidation.LiquidateDetailProduction
        Using service As ILiquidationAdminService = Container.Current.Resolve(Of ILiquidationAdminService)()
            Return service.LiquidateDetailProduction(serviceOrderDetailId, audit)
        End Using
    End Function

    ''' <summary>
    ''' Liquida un item de produccion
    ''' </summary>
    ''' <param name="serviceOrderDetailId"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function LiquidateItemProduction(serviceOrderDetailId As Integer, audit As AuditMessage) As ActionResult Implements IBillingServiceLiquidation.LiquidateItemProduction
        Using service As ILiquidationAdminService = Container.Current.Resolve(Of ILiquidationAdminService)()
            Return service.LiquidateItemProduction(serviceOrderDetailId, audit)
        End Using
    End Function

	''' <summary>
	''' Obtiene datos de item no qx
	''' </summary>
	''' <param name="InvoiceId"></param>
	''' <param name="audit"></param>
	''' <returns></returns>
	Public Function GetViewListNoSurgical(InvoiceId As Integer, invoiceDetailId As Integer?, audit As AuditMessage) As List(Of ViewListNoSurgical) Implements IBillingServiceLiquidation.GetViewListNoSurgical
		Using service As ILiquidationAdminService = Container.Current.Resolve(Of ILiquidationAdminService)()
			Return service.GetViewListNoSurgical(InvoiceId, invoiceDetailId)
		End Using
	End Function

	''' <summary>
	''' Obtiene una lista de ordenes de servicios de una admisión para incluir a otro servicio
	''' </summary>
	''' <param name="listIds">Lista de IDs a excluir</param>
	''' <param name="admissionNumber">Número de admisión</param>
	''' <returns>Lista de detalles de orden de servicio</returns>
	Public Function GetListServiceOrderDetail(listIds As List(Of Integer), admissionNumber As String) As List(Of SP_GetListServiceOrderDetail) Implements IBillingServiceLiquidation.GetListServiceOrderDetail
		Using service As ILiquidationAdminService = Container.Current.Resolve(Of ILiquidationAdminService)()
			Return service.GetListServiceOrderDetail(listIds, admissionNumber)
		End Using
	End Function

	''' <summary>
	''' Obtiene una lista de ingresos unificados por IDs de folios
	''' </summary>
	''' <param name="listFoliosIds">Lista de IDs de folios</param>
	''' <param name="admissionNumber">Número de admisión</param>
	''' <returns>Lista de ingresos unificados</returns>
	Public Function GetListUnifiedAdmissions(listFoliosIds As List(Of Integer), admissionNumber As String) As ActionResult(Of List(Of Domain.Crystal.Entities.ADINGRESO)) Implements IBillingServiceLiquidation.GetListUnifiedAdmissions
		Using service As ILiquidationAdminService = Container.Current.Resolve(Of ILiquidationAdminService)()
			Return service.GetListUnifiedAdmissions(listFoliosIds, admissionNumber)
		End Using
	End Function
End Class
