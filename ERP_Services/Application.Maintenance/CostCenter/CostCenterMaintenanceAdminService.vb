'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 26-10-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Maintenance
Imports Domain.Maintenance.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Base.Entities
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports System.Data.Entity.Infrastructure
Imports System.Transactions
Imports Infrastructure.CrossCutting.Resources

Public Class CostCenterMaintenanceAdminService

    Implements ICostCenterMaintenanceAdminService

    Private _CostCenterRepository As ICostCenterMaintenanceRepository
    Private _secuenseDRepository As IMaintenanceSequenceDetailRepository
    Private Const FORM_NAME As String = "Centros de Costo"
    ''' <summary>
    ''' incia el repositorio de educationLevels
    ''' </summary>
    ''' <param name="repository">Repositorio de educations levels</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal repository As ICostCenterMaintenanceRepository,
                   secuenseDRepository As IMaintenanceSequenceDetailRepository)
        If (repository Is Nothing = True) Then
            Throw New ArgumentNullException("ICostCenterMaintenanceRepository Vacion")
        End If
        _CostCenterRepository = repository
        _secuenseDRepository = secuenseDRepository
    End Sub


    ''' <summary>
    ''' Elimina un centro de costo
    ''' </summary>
    ''' <param name="CostCenter">Centro de costo</param>
    ''' <param name="audit">Objeto auditoria</param>
    ''' <returns>True o False</returns>
    ''' <remarks></remarks>
    Public Function DeleteCostCenter(costCenter As CostCenter, audit As AuditMessage) As ActionResult Implements ICostCenterMaintenanceAdminService.DeleteCostCenter
        If costCenter Is Nothing Then
            Throw New ArgumentNullException("costCenter")
        End If
        Dim unitOfWork As IUnitWork = Me._CostCenterRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                costCenter.ModificationUserId = audit.IdUser
                costCenter.ModificationDate = Date.Now
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of CostCenter)(costCenter, audit, status)

                costCenter.MarkAsDeleted()
                Me._CostCenterRepository.SaveEntity(costCenter)
                unitOfWork.Commit()
                auditProcess.Execute()
                scope.Complete()
                Return New ActionResult With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .Message = ResourceManager.GetString("RecordDeleted")}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = New List(Of String)({"-999"}), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As UpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = New List(Of String)({"-000"}), .Message = ResourceManager.GetString("ErrorDependence")}
        Catch ex As DbUpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = New List(Of String)({"-000"}), .Message = ResourceManager.GetString("ErrorDependence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try


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
    End Function

    ''' <summary>
    ''' Obtiene un centro de costo especifico
    ''' </summary>
    ''' <param name="code">Codigo del centro de costo</param>
    ''' <returns>Centro de costo</returns>
    ''' <remarks></remarks>
    Public Function GetCostCenter(code As String) As CostCenter Implements ICostCenterMaintenanceAdminService.GetCostCenter
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
    ''' Centro de costo por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCostCenterById(id As Integer, tracking As Boolean) As CostCenter Implements ICostCenterMaintenanceAdminService.GetCostCenterById
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

    ''' <summary>
    ''' Lista todos los centros de costos
    ''' </summary>
    ''' <returns>Lista de centros de costos</returns>
    ''' <remarks></remarks>
    Public Function ListAllCostCenter() As List(Of CostCenter) Implements ICostCenterMaintenanceAdminService.ListAllCostCenter
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
    Public Function SaveCostCenter(costCenter As CostCenter, audit As AuditMessage, Optional idSequence As Long = 0) As ActionResult(Of CostCenter) Implements ICostCenterMaintenanceAdminService.SaveCostCenter
        If costCenter Is Nothing Then
            Throw New ArgumentNullException("costCenter")
        End If
        Dim unitOfWork As IUnitWork = Me._CostCenterRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty

                If String.IsNullOrEmpty(costCenter.Code) Then
                    Dim seq As Domain.Entities.MaintenanceSequenceDetail = Me._secuenseDRepository.GetSequenseDById(idSequence)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.MaintenanceSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            costCenter.Code = res
                            seq.Next += 1
                            Me._secuenseDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of CostCenter) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                        MessageResult = If(seq.MaintenanceSequence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), costCenter.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of CostCenter) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If

                Dim auxcostCenter As CostCenter = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of CostCenter)
                Dim status As Integer

                If costCenter.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    costCenter.CreadionUserId = audit.IdUser
                    costCenter.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    auxcostCenter = _CostCenterRepository.GetCostCenter(costCenter.Code, False)
                    costCenter.ModificationUserId = audit.IdUser
                    costCenter.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Me._CostCenterRepository.SaveEntity(costCenter)
                unitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of CostCenter)(costCenter, audit, status, auxcostCenter)
                auditProcess.Execute()

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


        'If costCenter Is Nothing Then
        '    Throw New ArgumentNullException("Centro de costo Vacio")
        'End If
        'Dim unitWork As IUnitWork = _CostCenterRepository.UnitWork
        'Try

        '    Dim auditProcess As IndigoAuditSimpleEntity(Of CostCenter)
        '    Dim AuxCostCenter As CostCenter = Nothing
        '    Dim status As Integer

        '    If costCenter.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
        '        costCenter.ModificationUserId = audit.CodeUser
        '        costCenter.ModificationDate = Date.Now()
        '        status = Infrastructure.CrossCutting.Audit.Actions.Update
        '        AuxCostCenter = _CostCenterRepository.GetCostCenter(costCenter.Code, False)
        '    Else
        '        costCenter.CreadionUserId = audit.CodeUser
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
    End Function

    Public Function ChangeStateCostCenter(code As String, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of CostCenter) Implements ICostCenterMaintenanceAdminService.ChangeStateCostCenter
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If String.IsNullOrEmpty(state) Then
            Throw New ArgumentNullException("state")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim costCenter As CostCenter = Me._CostCenterRepository.GetCostCenter(code.Trim())
            If costCenter IsNot Nothing AndAlso costCenter.Id > 0 Then
                costCenter.State = state
            End If
            Dim result = Me.SaveCostCenter(costCenter, audit)
            If result.StatusCode = eStatusResult.SUCCESS Then
                result.Message = ResourceManager.GetString("UpdateState")
            End If
            Return result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CostCenter) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function


    'Dim costCenter As CostCenter = _CostCenterRepository.GetCostCenter(code)
    '    costCenter.State = state
    '    Return SaveCostCenter(costCenter, audit)
    'End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _CostCenterRepository = Nothing
            _secuenseDRepository = Nothing
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
