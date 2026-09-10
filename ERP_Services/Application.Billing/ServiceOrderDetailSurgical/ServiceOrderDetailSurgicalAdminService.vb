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
Imports Application.Base
Imports System.Data.Entity.Core

#End Region

Public Class ServiceOrderDetailSurgicalAdminService
    Implements IServiceOrderDetailSurgicalAdminService

    Private _serviceOrderDetailSurgicalRepository As IServiceOrderDetailSurgicalRepository

    Public Sub New(serviceOrderDetailSurgicalRepository As IServiceOrderDetailSurgicalRepository)
        If serviceOrderDetailSurgicalRepository Is Nothing Then
            Throw New ArgumentNullException("serviceOrderDetailSurgicalRepository")
        End If
        _serviceOrderDetailSurgicalRepository = serviceOrderDetailSurgicalRepository
    End Sub


    ''' <summary>
    ''' lista los detalles quirurgicos de la orden de servicio
    ''' </summary>
    ''' <param name="idServiceOrderDetail"></param>
    ''' <returns></returns>
    Public Function ListServiceOrderDetailSurgicalByIdDerviceOrderDetail(idServiceOrderDetail As Integer) As List(Of ServiceOrderDetailSurgical) Implements IServiceOrderDetailSurgicalAdminService.ListServiceOrderDetailSurgicalByIdDerviceOrderDetail
        Try
            Return _serviceOrderDetailSurgicalRepository.ListServiceOrderDetailSurgicalByIdDerviceOrderDetail(idServiceOrderDetail)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New List(Of ServiceOrderDetailSurgical)
        End Try
    End Function

    ''' <summary>
    ''' Guarda el detalle quirurgico
    ''' </summary>
    ''' <param name="ServiceOrderDetailSurgical"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveServiceOrderDetailSurgical(ServiceOrderDetailSurgical As ServiceOrderDetailSurgical, audit As Infrastructure.CrossCutting.Base.AuditMessage) As ActionResult(Of ServiceOrderDetailSurgical) Implements IServiceOrderDetailSurgicalAdminService.SaveServiceOrderDetailSurgical
        If ServiceOrderDetailSurgical Is Nothing Then
            Throw New ArgumentNullException("ServiceOrderDetailSurgical")
        End If
        Dim unitOfWork As IUnitWork = Me._serviceOrderDetailSurgicalRepository.UnitWork
        Try
            Dim auditProcess As IndigoAuditSimpleEntity(Of ServiceOrderDetailSurgical)
            Dim status As Integer
            status = Infrastructure.CrossCutting.Audit.Actions.Insert

            Me._serviceOrderDetailSurgicalRepository.SaveEntity(ServiceOrderDetailSurgical)
            unitOfWork.Commit()
            auditProcess = New IndigoAuditSimpleEntity(Of ServiceOrderDetailSurgical)(ServiceOrderDetailSurgical, audit, status)
            auditProcess.Execute()

            'Se marca la entidad como sin cambios
            ServiceOrderDetailSurgical.MarkAsUnchanged()

            Return New ActionResult(Of ServiceOrderDetailSurgical) With {.StateResult = True, .ObjectEmbbeded = ServiceOrderDetailSurgical}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of ServiceOrderDetailSurgical) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of ServiceOrderDetailSurgical) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Elimina un detalle quirurgico
    ''' </summary>
    ''' <param name="ServiceOrderDetailSurgical"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteServiceOrderDetailSurgical(ServiceOrderDetailSurgicalId As Integer, audit As Infrastructure.CrossCutting.Base.AuditMessage) As ActionResult Implements IServiceOrderDetailSurgicalAdminService.DeleteServiceOrderDetailSurgical
        If ServiceOrderDetailSurgicalId = 0 Then
            Throw New ArgumentNullException("ServiceOrderDetailSurgicalId")
        End If
        Dim unitOfWork As IUnitWork = Me._serviceOrderDetailSurgicalRepository.UnitWork
        Try
            Dim ServiceOrderDetailSurgical As ServiceOrderDetailSurgical = _serviceOrderDetailSurgicalRepository.GetServiceOrderDetailSurgicalById(ServiceOrderDetailSurgicalId)

            Dim auditProcess As IndigoAuditSimpleEntity(Of ServiceOrderDetailSurgical)
            auditProcess = New IndigoAuditSimpleEntity(Of ServiceOrderDetailSurgical)(ServiceOrderDetailSurgical, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            Me._serviceOrderDetailSurgicalRepository.DeleteEntity(ServiceOrderDetailSurgical)
            unitOfWork.Commit()
            auditProcess.Execute()
            Return New ActionResult With {.StateResult = True}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-999"})}
        Catch ex As UpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"})}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Valida que la causacion no exista en ninguna liquidacion
    ''' </summary>
    ''' <param name="MedicalFeesCausationId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidateDeleteServiceOrderDetailSurgical(MedicalFeesCausationId As Integer) As ActionResult(Of String) Implements IServiceOrderDetailSurgicalAdminService.ValidateDeleteServiceOrderDetailSurgical
        If MedicalFeesCausationId = 0 Then
            Throw New ArgumentNullException("MedicalFeesCausationId")
        End If
        Try
            Dim result As Boolean = Me._serviceOrderDetailSurgicalRepository.ValidateDeleteServiceOrderDetailSurgical(MedicalFeesCausationId)
            Return New ActionResult(Of String) With {.StateResult = True, .StateResultAux = result}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of String) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Actualiza los campos de medico y tercero del detalle quirurgico de la orden de servicio
    ''' </summary>
    ''' <param name="ServiceOrderDetailSurgicalId"></param>
    ''' <param name="HealthProfessionalCode"></param>
    ''' <param name="ThirdPartyId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdateFieldsServiceOrderDetailSurgical(ServiceOrderDetailSurgicalId As Integer, HealthProfessionalCode As String, ThirdPartyId As Integer) As ActionResult(Of ServiceOrderDetailSurgical) Implements IServiceOrderDetailSurgicalAdminService.UpdateFieldsServiceOrderDetailSurgical
        If ServiceOrderDetailSurgicalId = 0 Then
            Throw New ArgumentNullException("ServiceOrderDetailSurgicalId")
        End If
        If HealthProfessionalCode Is String.Empty Then
            Throw New ArgumentNullException("HealthProfessionalCode")
        End If
        If ThirdPartyId = 0 Then
            Throw New ArgumentNullException("ThirdPartyId")
        End If
        Dim unitOfWork As IUnitWork = Me._serviceOrderDetailSurgicalRepository.UnitWork
        Try
            Dim serviceOrderDetailSurgical As ServiceOrderDetailSurgical = _serviceOrderDetailSurgicalRepository.GetServiceOrderDetailSurgicalById(ServiceOrderDetailSurgicalId)
            If serviceOrderDetailSurgical Is Nothing Then
                Return New ActionResult(Of ServiceOrderDetailSurgical) With {.StateResult = False, .MessageResult = {"No existe el detalle quirúrgico de la Orden de Servicio."}.ToList}
            End If

            serviceOrderDetailSurgical.PerformsHealthProfessionalCode = HealthProfessionalCode
            serviceOrderDetailSurgical.PerformsHealthProfessionalThirdPartyId = ThirdPartyId
            serviceOrderDetailSurgical.MarkAsModified()

            _serviceOrderDetailSurgicalRepository.SaveEntity(serviceOrderDetailSurgical)
            unitOfWork.Commit()

            serviceOrderDetailSurgical.MarkAsUnchanged()
            Return New ActionResult(Of ServiceOrderDetailSurgical) With {.StateResult = True, .ObjectEmbbeded = serviceOrderDetailSurgical}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of ServiceOrderDetailSurgical) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of ServiceOrderDetailSurgical) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: elimine el estado administrado (objetos administrados).
            End If
            _serviceOrderDetailSurgicalRepository = Nothing
            Infrastructure.CrossCutting.Base.IndigoGC.Execute()
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
