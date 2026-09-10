'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Cristhian Mauricio Salazar
' Created          : 20-04-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll
Imports Domain.Payroll.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Base.Entities
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports System.Data.Entity.Infrastructure
Imports System.Transactions
Imports Infrastructure.CrossCutting.Resources
Imports System.Data.Entity.Core
Imports Infrastructure.CrossCutting.Queue

Public Class CostCenterAdminService
    Implements ICostCenterAdminService
    Private Const FORM_NAME As String = "FrmCostCenter"
    Private _CostCenterRepository As ICostCenterRepository
    Private _sequenseAccountingDRepository As Domain.Entities.ISequenseAccountingDRepository

    ''' <summary>
    ''' Fabrica de Indiigo Queue
    ''' </summary>
    Private _factoryQueue As IFactoryQueue

    ''' <summary>
    ''' incia el repositorio de educationLevels
    ''' </summary>
    ''' <param name="repository">Repositorio de educations levels</param>
    ''' <remarks></remarks>226956367
    Public Sub New(ByVal repository As ICostCenterRepository, ByVal sequenseRepository As Domain.Entities.ISequenseAccountingDRepository, FactoryQueue As IFactoryQueue)
        If (repository Is Nothing = True) Then
            Throw New ArgumentNullException("EducationLevelsRepository Vacion")
        End If
        If sequenseRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseRepository")
        End If
        _CostCenterRepository = repository
        _sequenseAccountingDRepository = sequenseRepository
        _factoryQueue = FactoryQueue
    End Sub

    ''' <summary>
    ''' Elimina un centro de costo
    ''' </summary>
    ''' <param name="CostCenter">Centro de costo</param>
    ''' <param name="audit">Objeto auditoria</param>
    ''' <returns>True o False</returns>
    ''' <remarks></remarks>
    Public Function DeleteCostCenter(costCenter As CostCenter, audit As Infrastructure.CrossCutting.Base.AuditMessage) As ActionMessageResult(Of CostCenter) Implements ICostCenterAdminService.DeleteCostCenter

        'Dim result As New ActionMessageResult(Of CostCenter)
        'result.StateResult = True
        'If costCenter Is Nothing Then
        '    Throw New ArgumentNullException("Centro de costo vacio")
        'End If
        'Dim unitWork As IUnitWork = _CostCenterRepository.UnitWork
        'Try
        '    _CostCenterRepository.DeleteEntity(costCenter)
        '    unitWork.Commit()

        '    '/***** Auditoria Basica ********/
        '    IndigoAuditBasic.Execute("CostCenter", audit.Functional, costCenter.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Eliminar, audit.Company, audit.ContainerSecurity)
        '    '/*****Auditoria Avanzada ******/
        '    Dim auditObject As New IndigoAuditSimpleEntity(Of CostCenter)(costCenter, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
        '    auditObject.Execute()
        '    Return result
        '    'Catch exDelete As UpdateException
        '    '    result.StateResult = False
        '    '    result.MessageResult.Add(New MessageResult("c-001", costCenter.Code))
        '    '    Return result
        'Catch ex As DbUpdateException
        '    result.StateResult = False
        '    result.MessageResult.Add(New MessageResult("c-0000", costCenter.Code))
        '    Return result
        '    unitWork.RollbackChanges()
        'Catch ex As Exception
        '    unitWork.RollbackChanges()
        '    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
        '    result.StateResult = False
        '    Return result
        'End Try




        If costCenter Is Nothing Then
            Throw New ArgumentNullException("CostCenter")
        End If
        Dim unitOfWork As IUnitWork = Me._CostCenterRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                costCenter.ModificationUser = audit.CodeUser
                costCenter.ModificationDate = Date.Now
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of CostCenter)(costCenter, audit, status)
                costCenter.MarkAsDeleted()
                _CostCenterRepository.DeleteEntity(costCenter)
                unitOfWork.Commit()
                auditProcess.Execute()
                scope.Complete()
                Return New ActionMessageResult(Of CostCenter) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .Message = ResourceManager.GetString("RecordDeleted")}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionMessageResult(Of CostCenter) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As UpdateException
            unitOfWork.RollbackChanges()
            Return New ActionMessageResult(Of CostCenter) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("ErrorDependence")}
        Catch ex As DbUpdateException
            unitOfWork.RollbackChanges()
            Return New ActionMessageResult(Of CostCenter) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .Message = ResourceManager.GetString("ErrorDependence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionMessageResult(Of CostCenter) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un centro de costo especifico
    ''' </summary>
    ''' <param name="code">Codigo del centro de costo</param>
    ''' <returns>Centro de costo</returns>
    ''' <remarks></remarks>
    Public Function GetCostCenter(code As String) As CostCenter Implements ICostCenterAdminService.GetCostCenter
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("Codigo Vacio")
        End If
        Try
            Return _CostCenterRepository.GetCostCenter(code)

        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New CostCenter()
        End Try
    End Function

    ''' <summary>
    ''' Lista todos los centros de costos
    ''' </summary>
    ''' <returns>Lista de centros de costos</returns>
    ''' <remarks></remarks>
    Public Function ListAllCostCenter() As List(Of CostCenter) Implements ICostCenterAdminService.ListAllCostCenter
        Try

            Return _CostCenterRepository.ListAllCostCenter()

        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Graba o actualiza un centro de costo
    ''' </summary>
    ''' <param name="CostCenter">Centro de costo a guardar</param>
    ''' <param name="audit">Objeto auditoria</param>
    ''' <returns>True o False</returns>
    ''' <remarks></remarks>
    Public Function SaveCostCenter(costCenter As CostCenter, audit As Infrastructure.CrossCutting.Base.AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of Domain.Payroll.Entities.CostCenter) Implements ICostCenterAdminService.SaveCostCenter
        'If costCenter Is Nothing Then
        '    Throw New ArgumentNullException("Centro de costo Vacio")
        'End If
        'Dim unitWork As IUnitWork = _CostCenterRepository.UnitWork
        'Try

        '    Dim auditProcess As IndigoAuditSimpleEntity(Of CostCenter)
        '    Dim AuxCostCenter As CostCenter = Nothing
        '    Dim status As Integer

        '    If costCenter.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
        '        costCenter.ModificationUser = audit.CodeUser
        '        costCenter.ModificationDate = Date.Now()
        '        status = Infrastructure.CrossCutting.Audit.Actions.Update
        '        AuxCostCenter = _CostCenterRepository.GetCostCenter(costCenter.Code, False)
        '    Else
        '        costCenter.CreationUser = audit.CodeUser
        '        costCenter.CreationDate = Date.Now()
        '        status = Infrastructure.CrossCutting.Audit.Actions.Insert
        '    End If

        '    'Valido si se va a guardar o a eliminar
        '    _CostCenterRepository.SaveEntity(costCenter)
        '    unitWork.Commit()
        '    auditProcess = New IndigoAuditSimpleEntity(Of CostCenter)(costCenter, audit, status, AuxCostCenter)
        '    auditProcess.Execute()
        '    Return True

        'Catch ex As Exception
        '    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
        '    Return False
        'End Try




        If costCenter Is Nothing Then
            Throw New ArgumentNullException("CostCenter")
        End If
        Dim unitOfWork As IUnitWork = Me._CostCenterRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._sequenseAccountingDRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty
                Dim seq As Domain.Entities.GeneralLedgerSequenceDetail = Nothing
                If costCenter.Code Is Nothing OrElse costCenter.Code.Trim().Equals(String.Empty) Then
                    seq = Me._sequenseAccountingDRepository.GetSequenseDetailUpdatedById(idSequense)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.GeneralLedgerSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            costCenter.Code = res
                            seq.Next += 1
                            Me._sequenseAccountingDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of CostCenter) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                        MessageResult = If(seq.GeneralLedgerSequence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), costCenter.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of CostCenter) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If

                Dim auxObjEntity As CostCenter = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of CostCenter)
                Dim status As Integer

                If costCenter.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    costCenter.CreationUser = audit.CodeUser
                    costCenter.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    auxObjEntity = _CostCenterRepository.GetCostCenter(costCenter.Code, False)
                    costCenter.ModificationUser = audit.CodeUser
                    costCenter.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Me._CostCenterRepository.SaveEntity(costCenter)
                unitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of CostCenter)(costCenter, audit, status, auxObjEntity)
                auditProcess.Execute()

                'se asegura que se hace commit y se se dispara el evento
                TriggerEvent(costCenter, audit)
                'Se marca la entidad como sin cambios
                costCenter.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of CostCenter) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = costCenter, .Message = MessageResult}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of CostCenter) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CostCenter) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Centro de costo por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCostCenterById(id As Integer, tracking As Boolean) As CostCenter Implements ICostCenterAdminService.GetCostCenterById
        If String.IsNullOrEmpty(id) Then
            Throw New ArgumentNullException("id Vacio")
        End If
        Try
            Return _CostCenterRepository.GetCostCenterById(id, tracking)

        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New CostCenter()
        End Try
    End Function

    Public Function ChangeStateCostCenter(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of Domain.Payroll.Entities.CostCenter) Implements ICostCenterAdminService.ChangeStateCostCenter
        Dim costCenter As CostCenter = _CostCenterRepository.GetCostCenter(code)
        costCenter.State = state
        Return SaveCostCenter(costCenter, audit)
    End Function

    Public Sub TriggerEvent(costCenter As CostCenter, audit As AuditMessage)
        Dim wrapperEvent As New Events.Serializers.Wrapper
        Dim ChangeTracker As String = ""
        If costCenter.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
            ChangeTracker = "added"
        ElseIf costCenter.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Or costCenter.ChangeTracker.State = Domain.Base.Entities.ObjectState.Unchanged Then
            ChangeTracker = "modified"
        End If
        Dim eventData = wrapperEvent.GenerateWrapperEventData(costCenter, audit.CodeUser, ChangeTracker, DittoSourceType.costCenter)
        Dim queue = _factoryQueue.CreateQueue()
        queue.Publish(eventData)
    End Sub

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _CostCenterRepository = Nothing
            _sequenseAccountingDRepository = Nothing
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