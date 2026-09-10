'***********************************************************************
' Assembly         : Application.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 17/10/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Data.Entity.Infrastructure
Imports System.Data.Entity.Core
Imports System.Transactions
Imports Infrastructure.CrossCutting.Resources

Public Class RateManualDetailAdminService
    Implements IRateManualDetailAdminService

#Region "Variables"

    ''' <summary>
    ''' Variable tipo repositorio para dependencia
    ''' </summary>
    ''' <remarks></remarks>
    Private _rateManualDetailRepository As IRateManualDetailRepository
    ' ''' <summary>
    ' ''' Repositorio de secuencias numericas
    ' ''' </summary>
    'Private _serviceFeesRepository As IServiceFeesRepository
    ' ''' <summary>
    ' ''' Servicio de aplicacion para rangos de valores
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Private _serviceFeesAdminService As IServiceFeesAdminService
#End Region

#Region "Builder"

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(ByVal rateManualDetailRepository As IRateManualDetailRepository)
        If rateManualDetailRepository Is Nothing Then
            Throw New ArgumentNullException("rateManualDetailRepository Vacio")
        End If
        'If serviceFeesRepository Is Nothing Then
        '    Throw New ArgumentNullException("serviceFeesRepository")
        'End If
        'If serviceFeesAdminService Is Nothing Then
        '    Throw New ArgumentNullException("serviceFeesAdminService")
        'End If
        _rateManualDetailRepository = rateManualDetailRepository
        '_serviceFeesRepository = serviceFeesRepository
        '_serviceFeesAdminService = serviceFeesAdminService
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="state"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ChangeStateRateManualDetail1(id As Integer, state As Boolean, audit As AuditMessage) As ActionResult(Of RateManualDetail) Implements IRateManualDetailAdminService.ChangeStateRateManualDetail
        'Dim rateManualDetail As RateManualDetail = _rateManualDetailRepository.GetRateManualDetailById(id)
        'rateManualDetail.Status = state
        ''For Each itemService As ServiceFees In rateManualDetail.ServiceFees
        ''    itemService.MarkAsUnchanged()
        ''Next
        'Return SaveRateManualDetail(rateManualDetail, audit)
    End Function

    ''' <summary>
    ''' Elimina la entidad
    ''' </summary>
    ''' <param name="RateManualDetail"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteRateManualDetail(RateManualDetail As RateManualDetail, audit As AuditMessage) As ActionResult Implements IRateManualDetailAdminService.DeleteRateManualDetail
        If RateManualDetail Is Nothing Then
            Throw New ArgumentNullException("RateManualDetail")
        End If
        Dim unitOfWork As IUnitWork = Me._rateManualDetailRepository.UnitWork
        Using Transaction As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
            Try
                Dim rateMD As RateManualDetail = _rateManualDetailRepository.GetRateManualDetailById(RateManualDetail.Id)
                'While rateMD.ServiceFees.Count > 0
                '    Dim resultDelete As ActionResult = _serviceFeesAdminService.DeleteServiceFees(rateMD.ServiceFees.Item(0))
                '    If resultDelete.StateResult = False Then
                '        Transaction.Dispose()
                '        Return New ActionResult With {.StateResult = False, .MessageResult = {"Error al Eliminar."}.ToList}
                '    End If
                'End While
                'rateMD.ServiceFees.Clear()
                rateMD.MarkAsDeleted()

                Dim auditProcess As IndigoAuditSimpleEntity(Of RateManualDetail)
                auditProcess = New IndigoAuditSimpleEntity(Of RateManualDetail)(rateMD, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
                Me._rateManualDetailRepository.DeleteEntity(rateMD)
                unitOfWork.Commit()
                auditProcess.Execute()
                Transaction.Complete()
                Return New ActionResult With {.StateResult = True}
            Catch ex As OptimisticConcurrencyException
                unitOfWork.RollbackChanges()
                Transaction.Dispose()
                Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-999"})}
            Catch ex As UpdateException
                unitOfWork.RollbackChanges()
                Transaction.Dispose()
                Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"})}
            Catch ex As Exception
                unitOfWork.RollbackChanges()
                Transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
            End Try
        End Using
    End Function

    ''' <summary>
    ''' Obtiene la entidad por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetRateManualDetailById(id As Integer, audit As AuditMessage) As ActionResult(Of RateManualDetail) Implements IRateManualDetailAdminService.GetRateManualDetailById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim RateManualDetail As RateManualDetail = Me._rateManualDetailRepository.GetRateManualDetailById(id)
            If RateManualDetail IsNot Nothing AndAlso RateManualDetail.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of RateManualDetail)(RateManualDetail, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of RateManualDetail) With {.StateResult = True, .ObjectEmbbeded = RateManualDetail}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of RateManualDetail) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Guarda o actualiza el listado de manuales de servicios
    ''' </summary>
    ''' <param name="ListRateManualDetail"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveListRateManualDetail(ListRateManualDetail As List(Of RateManualDetail), ListDeleteRateManualDetail As List(Of RateManualDetail), audit As AuditMessage) As ActionResult(Of List(Of RateManualDetail)) Implements IRateManualDetailAdminService.SaveListRateManualDetail
        If ListRateManualDetail Is Nothing Then
            Throw New ArgumentNullException("ListRateManualDetail")
        End If
        Dim unitOfWork As IUnitWork = Me._rateManualDetailRepository.UnitWork
        'Dim unitOfWorkServiceFees As IUnitWork = Me._serviceFeesRepository.UnitWork
        Using Transaction As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
            Try
                If ListDeleteRateManualDetail IsNot Nothing Then
                    For Each item As RateManualDetail In ListDeleteRateManualDetail

                        'While item.ServiceFees.Count > 0
                        '    item.ServiceFees.Item(0).MarkAsDeleted()
                        'End While
                        item.MarkAsDeleted()
                        _rateManualDetailRepository.SaveEntity(item)
                        unitOfWork.Commit()

                    Next
                End If

                For Each item As RateManualDetail In ListRateManualDetail
                    Dim result As ActionResult(Of RateManualDetail) = SaveRateManualDetail(item, audit)
                    If result.StateResult = False Then
                        Transaction.Dispose()
                        Return New ActionResult(Of List(Of RateManualDetail)) With {.StateResult = False, .MessageResult = {ResourceManager.GetString("ErrorSave", "Contract")}.ToList}
                    End If
                Next

                Transaction.Complete()
                Return New ActionResult(Of List(Of RateManualDetail)) With {.StateResult = True, .ObjectEmbbeded = ListRateManualDetail}

            Catch ex As OptimisticConcurrencyException
                Transaction.Dispose()
                Return New ActionResult(Of List(Of RateManualDetail)) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
            Catch ex As Exception
                Transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of List(Of RateManualDetail)) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
            End Try
        End Using
    End Function

    ''' <summary>
    ''' Guarda o actualiza la entidad
    ''' </summary>
    ''' <param name="RateManualDetail"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveRateManualDetail(RateManualDetail As RateManualDetail, audit As AuditMessage) As ActionResult(Of RateManualDetail) Implements IRateManualDetailAdminService.SaveRateManualDetail
        If RateManualDetail Is Nothing Then
            Throw New ArgumentNullException("RateManualDetail")
        End If
        Dim rateMD As RateManualDetail = Nothing
        Dim unitOfWork As IUnitWork = Me._rateManualDetailRepository.UnitWork
        Try
            Dim auxRateManualDetail As RateManualDetail = Nothing
            Dim auditProcess As IndigoAuditSimpleEntity(Of RateManualDetail)
            Dim status As Integer

            'If RateManualDetail.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
            '    RateManualDetail.CreationUser = audit.CodeUser
            '    RateManualDetail.CreationDate = DateTime.Now
            '    status = Infrastructure.CrossCutting.Audit.Actions.Insert
            'Else
            '    rateMD = _rateManualDetailRepository.GetRateManualDetailById(RateManualDetail.Id)
            '    With rateMD
            '        .RateManualId = RateManualDetail.RateManualId
            '        .IPSServiceId = RateManualDetail.IPSServiceId
            '        .IPSServiceDescription = RateManualDetail.IPSServiceDescription
            '        '.ScoreProcedure = RateManualDetail.ScoreProcedure
            '        '.DiscountPercentage = RateManualDetail.DiscountPercentage
            '        '.SurgicalGroupId = RateManualDetail.SurgicalGroupId
            '        .SurgicalGroupDescription = RateManualDetail.SurgicalGroupDescription
            '        .OutPatientRecoveryFeeType = RateManualDetail.OutPatientRecoveryFeeType
            '        .OutPatientRecoveryFeeTypeDescription = RateManualDetail.OutPatientRecoveryFeeTypeDescription
            '        .InPatientRecoveryFeeType = RateManualDetail.InPatientRecoveryFeeType
            '        .InPatientRecoveryFeeTypeDescription = RateManualDetail.InPatientRecoveryFeeTypeDescription
            '        .Status = RateManualDetail.Status
            '        .ModificationUser = audit.CodeUser
            '        .ModificationDate = DateTime.Now
            '        .MarkAsModified()

            '        'For Each itemService As ServiceFees In .ServiceFees
            '        '    Dim sefees As ServiceFees = RateManualDetail.ServiceFees.ToList.Find(Function(item) item.Id = itemService.Id AndAlso item.ChangeTracker.State = ObjectState.Modified)
            '        '    If sefees IsNot Nothing Then
            '        '        itemService.RateManualDetailId = sefees.RateManualDetailId
            '        '        itemService.InitialDate = sefees.InitialDate
            '        '        itemService.EndDate = sefees.EndDate
            '        '        itemService.SalesValue = sefees.SalesValue
            '        '        itemService.SalesValueWithSurcharge = sefees.SalesValueWithSurcharge
            '        '        itemService.ContractMinimumWageId = sefees.ContractMinimumWageId
            '        '        itemService.ContractMinimumWageDescription = sefees.ContractMinimumWageDescription
            '        '        itemService.MarkAsModified()
            '        '    End If
            '        'Next

            '        'Dim cont As Integer = 0
            '        'While RateManualDetail.ServiceFees.Count > cont
            '        '    If RateManualDetail.ServiceFees.Item(cont).ChangeTracker.State = ObjectState.Added Then
            '        '        .ServiceFees.Add(RateManualDetail.ServiceFees.Item(cont))
            '        '        cont = 0
            '        '    Else
            '        '        cont += 1
            '        '    End If
            '        'End While

            '        'Dim sf As ServiceFees = RateManualDetail.ServiceFees.ToList.Find(Function(item) item.ChangeTracker.State = ObjectState.Deleted)
            '        'If sf IsNot Nothing Then
            '        '    Dim delete As ServiceFees = .ServiceFees.ToList.Find(Function(item) sf.Id = item.Id)
            '        '    If delete IsNot Nothing Then
            '        '        delete.MarkAsDeleted()
            '        '    End If
            '        'End If

            '    End With

            '    auxRateManualDetail = _rateManualDetailRepository.GetRateManualDetailById(RateManualDetail.Id)
            '    status = Infrastructure.CrossCutting.Audit.Actions.Update
            'End If


            If RateManualDetail.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                Me._rateManualDetailRepository.SaveEntity(RateManualDetail)
                unitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of RateManualDetail)(RateManualDetail, audit, status, auxRateManualDetail)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                'For Each itemService As ServiceFees In RateManualDetail.ServiceFees
                '    itemService.MarkAsUnchanged()
                'Next
                RateManualDetail.MarkAsUnchanged()

                Return New ActionResult(Of RateManualDetail) With {.StateResult = True, .ObjectEmbbeded = RateManualDetail}
            Else
                Me._rateManualDetailRepository.SaveEntity(rateMD)
                unitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of RateManualDetail)(RateManualDetail, audit, status, auxRateManualDetail)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                'For Each itemService As ServiceFees In rateMD.ServiceFees
                '    itemService.MarkAsUnchanged()
                'Next
                'rateMD.MarkAsUnchanged()

                Return New ActionResult(Of RateManualDetail) With {.StateResult = True, .ObjectEmbbeded = rateMD}
            End If
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of RateManualDetail) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of RateManualDetail) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _rateManualDetailRepository = Nothing
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
