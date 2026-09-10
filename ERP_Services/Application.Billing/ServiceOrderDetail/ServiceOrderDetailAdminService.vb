'***********************************************************************
' Assembly         : Application.Billing
' Author           : Carlos Ernesto Cordoba
' Created          : 20-11-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Entities.Service
Imports Domain.Base.Entities
Imports Domain.Payroll
Imports Domain.Base
Imports System.Data.Entity.Core
Imports System.Transactions
Imports Infrastructure.CrossCutting.Resources
Imports Application.Billing
Imports Infrastructure.CrossCutting.Base
Imports System.Text

#End Region

Public Class ServiceOrderDetailAdminService
    Implements IServiceOrderDetailAdminService

#Region "Fields"

    ''' <summary>
    ''' Repositorio del detalle de ordenes de servicio
    ''' </summary>
    Private _serviceOrderDetailRepository As IServiceOrderDetailRepository
    Private _rateManualRepository As IRateManualRepository
    Private _cupsHomologationRepository As ICupsHomologationRepository
    Private _functionalUnitRepository As IFunctionalUnitRepository
    Private _costCenterRepository As ICostCenterRepository
    Private _cupsRepository As ICupsEntityRepository
    Private _ipsServiceRepository As IIPSServicesRepository
    Private _surgicalProcedureServiceRepository As ISurgicalProcedureServiceRepository
    Private _rateManualDetailSurgicalRepository As IRateManualDetailSurgicalRepository
    Private _rateManualDetailRepository As IRateManualDetailRepository
    Private _revenueControlDetailRepository As IRevenueControlDetailRepository
    Private _billingServ As IBillingServices
    Private _serviceOrderDetailDistribution As IServiceOrderDetailDistributionRepository
    Private _serviceOrderDetailSurgicalRepository As IServiceOrderDetailSurgicalRepository

#End Region

#Region "Builder"
    Public Sub New(serviceOrderDetailRepository As IServiceOrderDetailRepository, rateManualRepository As IRateManualRepository,
                   cupsHomologationRepository As ICupsHomologationRepository, functionalUnitRepository As IFunctionalUnitRepository, costCenterRepository As ICostCenterRepository,
                   cupsRepository As ICupsEntityRepository, ipsServiceRepository As IIPSServicesRepository, surgicalProcedureServiceRepository As ISurgicalProcedureServiceRepository,
                   rateManualDetailSurgicalRepository As IRateManualDetailSurgicalRepository, rateManualDetailRepository As IRateManualDetailRepository,
                   revenueControlDetailRepository As IRevenueControlDetailRepository, billingServ As IBillingServices, serviceOrderDetailDistribution As IServiceOrderDetailDistributionRepository,
                   serviceOrderDetailSurgicalRepository As IServiceOrderDetailSurgicalRepository)
        If serviceOrderDetailRepository Is Nothing Then
            Throw New ArgumentNullException("serviceOrderDetailRepository")
        End If
        If rateManualRepository Is Nothing Then
            Throw New ArgumentNullException("rateManualRepository")
        End If
        If cupsHomologationRepository Is Nothing Then
            Throw New ArgumentNullException("cupsHomologationRepository")
        End If
        If functionalUnitRepository Is Nothing Then
            Throw New ArgumentNullException("functionalUnitRepository")
        End If
        If costCenterRepository Is Nothing Then
            Throw New ArgumentNullException("costCenterRepository")
        End If
        If cupsRepository Is Nothing Then
            Throw New ArgumentNullException("cupsRepository")
        End If
        If ipsServiceRepository Is Nothing Then
            Throw New ArgumentNullException("ipsServiceRepository")
        End If
        If surgicalProcedureServiceRepository Is Nothing Then
            Throw New ArgumentNullException("surgicalProcedureServiceRepository")
        End If
        If rateManualDetailSurgicalRepository Is Nothing Then
            Throw New ArgumentNullException("rateManualDetailSurgicalRepository")
        End If
        If rateManualDetailRepository Is Nothing Then
            Throw New ArgumentNullException("rateManualDetailRepository")
        End If
        If revenueControlDetailRepository Is Nothing Then
            Throw New ArgumentNullException("revenueControlDetailRepository")
        End If
        If serviceOrderDetailSurgicalRepository Is Nothing Then
            Throw New ArgumentNullException("serviceOrderDetailSurgical")
        End If
        _revenueControlDetailRepository = revenueControlDetailRepository
        _serviceOrderDetailRepository = serviceOrderDetailRepository
        _rateManualRepository = rateManualRepository
        _cupsHomologationRepository = cupsHomologationRepository
        _functionalUnitRepository = functionalUnitRepository
        _costCenterRepository = costCenterRepository
        _cupsRepository = cupsRepository
        _ipsServiceRepository = ipsServiceRepository
        _surgicalProcedureServiceRepository = surgicalProcedureServiceRepository
        _rateManualDetailSurgicalRepository = rateManualDetailSurgicalRepository
        _rateManualDetailRepository = rateManualDetailRepository
        _billingServ = billingServ
        _serviceOrderDetailDistribution = serviceOrderDetailDistribution
        _serviceOrderDetailSurgicalRepository = serviceOrderDetailSurgicalRepository
    End Sub
#End Region

    ''' <summary>
    ''' lista los detalles de la orden que podran ser incluidos en otro
    ''' </summary>
    ''' <param name="admissionNumber"></param>
    ''' <returns></returns>
    Public Function ListServicesOrderDetailByAdminssionNumber(admissionNumber As String) As List(Of ServiceOrderDetail) Implements IServiceOrderDetailAdminService.ListServicesOrderDetailByAdminssionNumber
        Try
            Return _serviceOrderDetailRepository.ListServicesOrderDetailByAdminssionNumber(admissionNumber)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New List(Of ServiceOrderDetail)
        End Try
    End Function

    Public Function GetValueServiceWithRefactorValue(AdmissionNumber As String, CenterAttentionCode As String, listCupsHomologation As List(Of CupsHomologation), CareGroupId As Integer, FunctionalUnitId As Integer, Specialty As String, ServiceDate As Date, PatientGenus As Integer, PatientDateBirth As Date, InvoicedQuantity As Integer, ProfessionalHealthCode As String, ProfessionalHealthThirdPartyId As Integer, Optional RiasId As Integer? = Nothing, Optional ContractDescriptionId As Integer? = Nothing) As ActionResult(Of List(Of ServiceOrderDetail)) Implements IServiceOrderDetailAdminService.GetValueServiceWithRefactorValue
        Try
            Dim listServiceOrderDetail As New List(Of ServiceOrderDetail)
            'Dim billingServices As New BillingServices(_careGroupRateRepository, _rateManualRepository, _cupsHomologationRepository, _functionalUnitRepository, _costCenterRepository, _cupsRepository, _ipsServiceRepository, _surgicalProcedureServiceRepository, _rateManualDetailSurgicalRepository, _rateManualDetailRepository, _rateByFixed, _revenueControlDetailRepository)
            Dim GUIDNumber As Guid
            If listCupsHomologation.Count > 1 Then
                GUIDNumber = Guid.NewGuid()
            End If
            For Each item In listCupsHomologation
                Dim result = _billingServ.GetValueService(AdmissionNumber, CenterAttentionCode, item.CupsEntityId, item.IPSServiceId, CareGroupId, FunctionalUnitId, Specialty, ServiceDate, PatientGenus, PatientDateBirth, InvoicedQuantity, ProfessionalHealthCode, ProfessionalHealthThirdPartyId, RiasId, ContractDescriptionId)
                If result.StateResult = True Then
                    If listServiceOrderDetail.Count = 0 Then
                        result.ObjectEmbbeded.CUPSAssociateService = False
                    Else
                        result.ObjectEmbbeded.CUPSAssociateService = True
                    End If
                    If listCupsHomologation.Count > 1 Then
                        result.ObjectEmbbeded.CodeAssociateService = GUIDNumber.ToString()
                    End If
                    listServiceOrderDetail.Add(result.ObjectEmbbeded)
                Else
                    Return New ActionResult(Of List(Of ServiceOrderDetail)) With {.StateResult = False, .Message = result.Message}
                End If
            Next
            Return New ActionResult(Of List(Of ServiceOrderDetail)) With {.StateResult = True, .ObjectEmbbeded = listServiceOrderDetail}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of ServiceOrderDetail))
        End Try
    End Function

    ''' <summary>
    ''' metodo para obtener el valor de los servicios
    ''' </summary>
    Public Function GetServiceValue(AdmissionNumber As String, CenterAttentionCode As String, listCupsHomologation As List(Of CupsHomologation), CareGroupId As Integer, FunctionalUnitId As Integer, Specialty As String, ServiceDate As Date, PatientGenus As Integer, PatientDateBirth As Date, InvoicedQuantity As Integer, ProfessionalHealthCode As String, ProfessionalHealthThirdPartyId As Integer, Optional RiasId As Integer? = Nothing, Optional ContractDescriptionId As Integer? = Nothing) As ActionResult(Of List(Of ServiceOrderDetail)) Implements IServiceOrderDetailAdminService.GetServiceValue
        Try
            'Verifica lista de homologacion
            If listCupsHomologation Is Nothing OrElse Not listCupsHomologation?.Any() Then
                Return New ActionResult(Of List(Of ServiceOrderDetail)) With {.StateResult = False, .Message = "No existen homologaciones"}
            End If
            Dim listServiceOrderDetail As New List(Of ServiceOrderDetail)
            'Dim billingServices As New BillingServices(_careGroupRateRepository, _rateManualRepository, _cupsHomologationRepository, _functionalUnitRepository, _costCenterRepository, _cupsRepository, _ipsServiceRepository, _surgicalProcedureServiceRepository, _rateManualDetailSurgicalRepository, _rateManualDetailRepository, _rateByFixed, _revenueControlDetailRepository)
            Dim GUIDNumber As Guid
            If listCupsHomologation.Count > 1 Then
                GUIDNumber = Guid.NewGuid()
            End If
            For Each item In listCupsHomologation
                Dim result = _billingServ.GetServiceValue(AdmissionNumber, CenterAttentionCode, item.CupsEntityId, item.IPSServiceId, CareGroupId, FunctionalUnitId, Specialty, ServiceDate, PatientGenus, PatientDateBirth, InvoicedQuantity, ProfessionalHealthCode, ProfessionalHealthThirdPartyId, RiasId, ContractDescriptionId)
                If result.StateResult = True Then
                    If listServiceOrderDetail.Count = 0 Then
                        result.ObjectEmbbeded.CUPSAssociateService = False
                    Else
                        result.ObjectEmbbeded.CUPSAssociateService = True
                    End If
                    If listCupsHomologation.Count > 1 Then
                        result.ObjectEmbbeded.CodeAssociateService = GUIDNumber.ToString()
                    End If
                    listServiceOrderDetail.Add(result.ObjectEmbbeded)
                Else
                    Return New ActionResult(Of List(Of ServiceOrderDetail)) With {.StateResult = False, .Message = result.Message}
                End If
            Next
            Return New ActionResult(Of List(Of ServiceOrderDetail)) With {.StateResult = True, .ObjectEmbbeded = listServiceOrderDetail}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of ServiceOrderDetail))
        End Try
    End Function

    ''' <summary>
    ''' Metodo que calcula el valor del servicio que se utiliza cuando el usuario cambie el valor en el control de rias en ordenes de servicio
    ''' </summary>
    ''' <returns></returns>
    Public Function GetServiceValueByRIAS(AdmissionNumber As String, CenterAttentionCode As String, CupsEntityId As Integer, IPSServiceId As Integer, CareGroupId As Integer, FunctionalUnitId As Integer, Specialty As String, ServiceDate As Date, PatientGenus As Integer, PatientDateBirth As Date, InvoicedQuantity As Integer, ProfessionalHealthCode As String, ProfessionalHealthThirdPartyId As Integer, Optional RiasId As Integer? = Nothing) As ActionResult(Of ServiceOrderDetail) Implements IServiceOrderDetailAdminService.GetServiceValueByRIAS
        Try
            Dim result = _billingServ.GetServiceValue(AdmissionNumber, CenterAttentionCode, CupsEntityId, IPSServiceId, CareGroupId, FunctionalUnitId, Specialty, ServiceDate, PatientGenus, PatientDateBirth, InvoicedQuantity, ProfessionalHealthCode, ProfessionalHealthThirdPartyId, RiasId)

            If result.StateResult = False Then
                Return New ActionResult(Of ServiceOrderDetail) With {.StateResult = False, .Message = result.Message}
            End If

            result.ObjectEmbbeded.CUPSAssociateService = False
            Return New ActionResult(Of ServiceOrderDetail) With {.StateResult = True, .ObjectEmbbeded = result.ObjectEmbbeded}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of ServiceOrderDetail) With {.StateResult = False, .Message = ex.Message}
        End Try
    End Function

    ''' <summary>
    ''' metodo para obterner el valor con recargo
    ''' </summary>
    ''' <param name="serviceOrderDetail"></param>
    ''' <returns></returns>
    Public Function GetServiceValueSurcharge(serviceOrderDetail As ServiceOrderDetail) As ServiceOrderDetail Implements IServiceOrderDetailAdminService.GetServiceValueSurcharge
        Try
            'Dim billingServices As New BillingServices(_careGroupRateRepository, _rateManualRepository, _cupsHomologationRepository, _functionalUnitRepository, _costCenterRepository, _cupsRepository, _ipsServiceRepository, _surgicalProcedureServiceRepository, _rateManualDetailSurgicalRepository, _rateManualDetailRepository, _rateByFixed, _revenueControlDetailRepository)
            Return _billingServ.GetServiceValueSurcharge(serviceOrderDetail)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ServiceOrderDetail
        End Try
    End Function

    ''' <summary>
    ''' obtener el valor de los detalles del ips quirurgico cuando el usuario cambia los valores por defecto en el formulario
    ''' </summary>
    ''' <param name="serviceOrderDetail"></param>
    ''' <param name="listSurgicalProcedureServiceDefault"></param>
    ''' <returns></returns>
    Public Function GetServiceValueBySurgicalProcedureService(serviceOrderDetail As ServiceOrderDetail, listSurgicalProcedureServiceDefault As List(Of SurgicalProcedureService)) As ActionResult(Of ServiceOrderDetail) Implements IServiceOrderDetailAdminService.GetServiceValueBySurgicalProcedureService
        Try
            'Dim billingServices As New BillingServices(_careGroupRateRepository, _rateManualRepository, _cupsHomologationRepository, _functionalUnitRepository, _costCenterRepository, _cupsRepository, _ipsServiceRepository, _surgicalProcedureServiceRepository, _rateManualDetailSurgicalRepository, _rateManualDetailRepository, _rateByFixed, _revenueControlDetailRepository)
            Return _billingServ.GetServiceValueBySurgicalProcedureService(serviceOrderDetail, listSurgicalProcedureServiceDefault)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of ServiceOrderDetail)
        End Try
    End Function

    Public Function RecalculateSurgicalEvents(serviceOrderDetail As ServiceOrderDetail) As ServiceOrderDetail Implements IServiceOrderDetailAdminService.RecalculateSurgicalEvents
        Try
            Return _billingServ.RecalculateSurgicalEvents(serviceOrderDetail)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ServiceOrderDetail
        End Try
    End Function

    ''' <summary>
    ''' Actualiza los campos de médico y tercero del detalle de la orden de servicio
    ''' </summary>
    ''' <param name="ServiceOrderDetailId"></param>
    ''' <param name="HealthProfessionalCode"></param>
    ''' <param name="ThirdPartyId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdateFieldsServiceOrderDetail(ServiceOrderDetailId As Integer, HealthProfessionalCode As String, ThirdPartyId As Integer) As ActionResult(Of ServiceOrderDetail) Implements IServiceOrderDetailAdminService.UpdateFieldsServiceOrderDetail
        If ServiceOrderDetailId = 0 Then
            Throw New ArgumentNullException("ServiceOrderDetailId")
        End If
        If HealthProfessionalCode Is String.Empty Then
            Throw New ArgumentNullException("HealthProfessionalCode")
        End If
        If ThirdPartyId = 0 Then
            Throw New ArgumentNullException("ThirdPartyId")
        End If
        Dim unitOfWork As IUnitWork = Me._serviceOrderDetailRepository.UnitWork
        Try
            Dim serviceOrderDetail As ServiceOrderDetail = _serviceOrderDetailRepository.GetServiceOrderDetailById(ServiceOrderDetailId)
            If serviceOrderDetail Is Nothing Then
                Return New ActionResult(Of ServiceOrderDetail) With {.StateResult = False, .MessageResult = {"No existe el detalle de la Orden de Servicio."}.ToList}
            End If

            serviceOrderDetail.PerformsHealthProfessionalCode = HealthProfessionalCode
            serviceOrderDetail.PerformsHealthProfessionalThirdPartyId = ThirdPartyId
            serviceOrderDetail.MarkAsModified()

            _serviceOrderDetailRepository.SaveEntity(serviceOrderDetail)
            unitOfWork.Commit()

            serviceOrderDetail.MarkAsUnchanged()
            Return New ActionResult(Of ServiceOrderDetail) With {.StateResult = True, .ObjectEmbbeded = serviceOrderDetail}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of ServiceOrderDetail) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of ServiceOrderDetail) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Lista las ordenes de servicio por ingreso y que no estén dentro del listado que se envia como parámetro
    ''' </summary>
    ''' <param name="admissionNumber">The admission number.</param>
    ''' <param name="serviceOrderDetailIds">The service order detail ids.</param>
    ''' <returns></returns>
    Public Function ListServiceOrderDetailByAdmissionNumberAndNotListServiceOrderDetailId(admissionNumber As String, serviceOrderDetailIds As List(Of Integer)) As List(Of ServiceOrderDetail) Implements IServiceOrderDetailAdminService.ListServiceOrderDetailByAdmissionNumberAndNotListServiceOrderDetailId
        Try
            Return _serviceOrderDetailRepository.ListServiceOrderDetailByAdmissionNumberAndNotListServiceOrderDetailId(admissionNumber, serviceOrderDetailIds)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function UpdateServiceOrderDetailList(serviceOrderDetailList As List(Of ServiceOrderDetail)) As ActionResult Implements IServiceOrderDetailAdminService.UpdateServiceOrderDetailList
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})

                Dim listFoliosServiceOrder As New List(Of Integer)()
                For Each sod In serviceOrderDetailList
                    Dim serviceOrderDetailDistribution As ServiceOrderDetailDistribution = _serviceOrderDetailDistribution.GetServiceOrderDetailDistributionByServideOrderDetailId(sod.Id)(0)
                    If serviceOrderDetailDistribution.GrandTotalSalesPrice <> sod.GrandTotalSalesPrice Then
                        serviceOrderDetailDistribution.GrandTotalSalesPrice = sod.GrandTotalSalesPrice
                        serviceOrderDetailDistribution.ThirdPartySalesPrice = sod.GrandTotalSalesPrice
                        serviceOrderDetailDistribution.SubTotalPatientSalesPrice = 0
                        serviceOrderDetailDistribution.PatientPercentage = 0
                        If serviceOrderDetailDistribution.GrandTotalSalesPrice = 0 Then
                            serviceOrderDetailDistribution.ThirdPartyPercentage = 0
                        Else
                            serviceOrderDetailDistribution.ThirdPartyPercentage = 100
                        End If
                        serviceOrderDetailDistribution.LastCaregroupId = sod.CareGroupId
                    End If
                    _serviceOrderDetailRepository.SaveEntity(sod)
                    _serviceOrderDetailRepository.UnitWork.Commit()
                    listFoliosServiceOrder.AddRange(sod.ServiceOrderDetailDistribution.Select(Function(o) o.RevenueControlDetailId).ToList())
                Next
                For Each i In listFoliosServiceOrder.Distinct().ToList()
                    _billingServ.UpdateRevenueControlDetailValues(i)
                Next
                scope.Complete()
                Return New ActionResult With {.StateResult = True}
            End Using
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
        End Try
    End Function

    ''' <summary>
    ''' Actualiza los campos de médico y tercero del detalle de la orden de servicio
    ''' </summary>
    ''' <param name="ListTuple">Este listado contiene los id para poder consultar y actualizar</param>
    ''' <param name="SelectionSurgical">Permite saber si se esta cambiando el medico en la rejilla Qx o NoQx (True=Qx, False=NoQx)</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdateHealthProfessionalForMedicalFeesCausation(ListTuple As List(Of Tuple(Of Integer, Integer, String, Integer)), SelectionSurgical As Boolean, Company As String) As ActionResult Implements IServiceOrderDetailAdminService.UpdateHealthProfessionalForMedicalFeesCausation
        If ListTuple Is Nothing OrElse ListTuple.Count = 0 Then
            Throw New ArgumentNullException("ListTuple")
        End If

        'Dim unitOfWork As IUnitWork = Me._serviceOrderDetailRepository.UnitWork
        'Dim unitOfWorkSurgical As IUnitWork = Me._serviceOrderDetailSurgicalRepository.UnitWork

        Using Transaction As New TransactionScope

            Using cnx As New System.Data.SqlClient.SqlConnection(Infrastructure.CrossCutting.Base.Utils.GetEntityConnectionString(Infrastructure.CrossCutting.Base.ConfigurationFile.CONX_GENESIS, String.Empty, Company, False))
                cnx.Open()
                Dim tx As System.Data.SqlClient.SqlTransaction = cnx.BeginTransaction()
                Dim command As New System.Data.SqlClient.SqlCommand("", cnx, tx)
                command.CommandTimeout = 30000
                command.CommandType = CommandType.Text

                Try
                    If SelectionSurgical Then 'Rejilla Qx
                        command.CommandText = "Update [Billing].ServiceOrderDetailSurgical set PerformsHealthProfessionalCode = '" + ListTuple(0).Item3.ToString + "', PerformsHealthProfessionalThirdPartyId = " + ListTuple(0).Item4.ToString + " where Id in (" + String.Join(",", ListTuple.Select(Function(x) x.Item2).ToArray) + ")"
                        command.ExecuteNonQuery()
                    Else 'Rejilla NoQx
                        command.CommandText = "Update [Billing].ServiceOrderDetail set PerformsHealthProfessionalCode = '" + ListTuple(0).Item3.ToString + "', PerformsHealthProfessionalThirdPartyId = " + ListTuple(0).Item4.ToString + " where Id in (" + String.Join(",", ListTuple.Select(Function(x) x.Item1).ToArray) + ")"
                        command.ExecuteNonQuery()
                    End If

                    tx.Commit()
                    Transaction.Complete()
                    Return New ActionResult With {.StateResult = True}
                Catch ex As OptimisticConcurrencyException
                    tx.Rollback()
                    Transaction.Dispose()
                    Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-999"})}
                Catch ex As UpdateException
                    tx.Rollback()
                    Transaction.Dispose()
                    Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"})}
                Catch ex As Exception
                    tx.Rollback()
                    Transaction.Dispose()
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                    Return New ActionResult With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
                Finally
                    cnx.Close()
                End Try

            End Using
        End Using
    End Function

    Public Function GetServiceValueByManual(ServiceDetail As ServiceOrderDetail, IPSServiceId As Integer, CareGroupId As Integer, FunctionalUnitId As Integer, Specialty As String, ServiceDate As Date, PatientGenus As Integer, PatientDateBirth As Date, InvoicedQuantity As Integer, ProfessionalHealthCode As String, ProfessionalHealthThirdPartyId As Integer) As ActionResult(Of ServiceOrderDetail) Implements IServiceOrderDetailAdminService.GetServiceValueByManual
        Try
            Dim service As New ServiceOrderDetail()

            Dim result = _billingServ.GetServiceValueByManualId(ServiceDetail, IPSServiceId, CareGroupId, FunctionalUnitId, Specialty, ServiceDate, PatientGenus, PatientDateBirth, InvoicedQuantity, ProfessionalHealthCode, ProfessionalHealthThirdPartyId)
            If result.StateResult = True Then
                result.ObjectEmbbeded.CUPSAssociateService = False
                service = result.ObjectEmbbeded
            Else
                Return New ActionResult(Of ServiceOrderDetail) With {.StateResult = False, .Message = result.Message}
            End If
            Return New ActionResult(Of ServiceOrderDetail) With {.StateResult = True, .ObjectEmbbeded = service}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of ServiceOrderDetail)
        End Try
    End Function

    Public Function ValidateServiceOrderDetail(listServiceOrderDetail As List(Of ServiceOrderDetail), audit As AuditMessage) As ActionResult(Of ServiceOrderDetail) Implements IServiceOrderDetailAdminService.ValidateServiceOrderDetail
        Try
            Dim serviceOrderDetailXml As New StringBuilder
            For Each serviceOrderDetail In listServiceOrderDetail
                serviceOrderDetailXml.AppendLine(serviceOrderDetail.ToXml())
            Next

            Dim resultStore = _serviceOrderDetailRepository.SP_ValidateServiceOrderDetail(serviceOrderDetailXml.ToString(), audit.CodeUser)
            If resultStore.CodeResult <> 0 Then
                Return New ActionResult(Of ServiceOrderDetail) With {.StateResult = False, .Message = resultStore.MessageResult}
            End If

            Return New ActionResult(Of ServiceOrderDetail) With {.StateResult = True, .Message = resultStore.MessageResult}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of ServiceOrderDetail) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                _billingServ.Dispose()
            End If
            _revenueControlDetailRepository = Nothing
            _serviceOrderDetailRepository = Nothing
            _rateManualRepository = Nothing
            _cupsHomologationRepository = Nothing
            _functionalUnitRepository = Nothing
            _costCenterRepository = Nothing
            _cupsRepository = Nothing
            _ipsServiceRepository = Nothing
            _surgicalProcedureServiceRepository = Nothing
            _rateManualDetailSurgicalRepository = Nothing
            _rateManualDetailRepository = Nothing
            _billingServ = Nothing
            _serviceOrderDetailDistribution = Nothing
            _serviceOrderDetailSurgicalRepository = Nothing
            IndigoGC.Execute()
        End If
        disposedValue = True
    End Sub

    ' Visual Basic agrega este código para implementar correctamente el patrón descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

#End Region

End Class
