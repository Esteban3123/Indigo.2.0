Imports Application.Base
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Infrastructure.CrossCutting.Resources
Imports System.Data.Entity.Core
Imports System.Transactions

Public Class HealthSuperParametersAdminService
    Implements IHealthSuperParametersAdminService

#Region "Properties"

    Private Const FORM_NAME As String = "FrmHealthSuperParameters"

    Private _sequenseDRepository As ISequenseAccountingDRepository
    Private _HealthSuperParametersRepository As IHealthSuperParametersRepository
    Private Table As HealthSuperParameters

#End Region

#Region "Builder"

    Public Sub New(sequenseDRepository As ISequenseAccountingDRepository, HealthSuperParametersRepository As IHealthSuperParametersRepository)
        If sequenseDRepository Is Nothing Then
            Throw New ArgumentNullException("sequenseDRepository Vacio")
        End If
        If HealthSuperParametersRepository Is Nothing Then
            Throw New ArgumentNullException("HealthSuperParametersRepository Vacio")
        End If

        _sequenseDRepository = sequenseDRepository
        _HealthSuperParametersRepository = HealthSuperParametersRepository
    End Sub

#End Region

#Region "Methods"

    Public Function GetHealthSuperParametersById(id As Integer, audit As AuditMessage) As ActionResult(Of HealthSuperParameters) Implements IHealthSuperParametersAdminService.GetHealthSuperParametersById
        Try
            Dim HealthSuperParameters As HealthSuperParameters = Me._HealthSuperParametersRepository.GetHealthSuperParametersById(id)
            If HealthSuperParameters IsNot Nothing AndAlso HealthSuperParameters.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of HealthSuperParameters)(HealthSuperParameters, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of HealthSuperParameters) With {.StateResult = True, .ObjectEmbbeded = HealthSuperParameters}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of HealthSuperParameters) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    Public Function GetHealthSuperParametersByCode(code As String, audit As AuditMessage) As ActionResult(Of HealthSuperParameters) Implements IHealthSuperParametersAdminService.GetHealthSuperParametersByCode
        Try
            Dim HealthSuperParameters As HealthSuperParameters = Me._HealthSuperParametersRepository.GetHealthSuperParametersByCode(code)
            If HealthSuperParameters IsNot Nothing AndAlso HealthSuperParameters.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of HealthSuperParameters)(HealthSuperParameters, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of HealthSuperParameters) With {.StateResult = True, .ObjectEmbbeded = HealthSuperParameters}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of HealthSuperParameters) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    Public Function SaveHealthSuperParameters(HealthSuperParameters As HealthSuperParameters, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of HealthSuperParameters) Implements IHealthSuperParametersAdminService.SaveHealthSuperParameters
        Dim unitOfWork As IUnitWork = Me._HealthSuperParametersRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._sequenseDRepository.UnitWork
        Try
            Dim txSettings As New TransactionOptions()
            txSettings.Timeout = TransactionManager.MaximumTimeout
            txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
            Using scope As New TransactionScope(TransactionScopeOption.Required, txSettings)
                Dim MessageResult As String = String.Empty

                If String.IsNullOrEmpty(HealthSuperParameters.Code) Then
                    Dim seq As GeneralLedgerSequenceDetail = Me._sequenseDRepository.GetSequenseDById(idSequense)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.GeneralLedgerSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            HealthSuperParameters.Code = res
                            seq.Next += 1
                            Me._sequenseDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of HealthSuperParameters) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                        MessageResult = If(seq.GeneralLedgerSequence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), HealthSuperParameters.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of HealthSuperParameters) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If

                Dim auxObjEntity As HealthSuperParameters = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of HealthSuperParameters)
                Dim status As Integer

                'Valida si ya existe un mismo Formato en estado Activo para hacer el cambio a Inactivo
                Dim ListHealthSuperParameters As List(Of HealthSuperParameters) = Me._HealthSuperParametersRepository.GetHealthSuperParametersAll()
                Me.Table = New HealthSuperParameters
                    Table = (From d In ListHealthSuperParameters Where d.Format = HealthSuperParameters.Format And d.Status = True Select d).FirstOrDefault

                If HealthSuperParameters.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    HealthSuperParameters.CreationUser = audit.CodeUser
                    HealthSuperParameters.CreationDate = DateTime.Now
                    If Table IsNot Nothing AndAlso Table.Status = HealthSuperParameters.Status Then
                        HealthSuperParameters.Status = Not (Table.Status)
                    End If
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    auxObjEntity = HealthSuperParameters.OriginalValue
                    HealthSuperParameters.ModificationUser = audit.CodeUser
                    HealthSuperParameters.ModificationDate = DateTime.Now
                    If Table IsNot Nothing AndAlso Table.Status = HealthSuperParameters.Status Then
                        HealthSuperParameters.Status = Not (Table.Status)
                    End If
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Me._HealthSuperParametersRepository.SaveEntity(HealthSuperParameters)
                unitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of HealthSuperParameters)(HealthSuperParameters, audit, status, auxObjEntity)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                HealthSuperParameters.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of HealthSuperParameters) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = HealthSuperParameters, .Message = MessageResult}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of HealthSuperParameters) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of HealthSuperParameters) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    Public Function ChangeStateHealthSuperParameters(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of HealthSuperParameters) Implements IHealthSuperParametersAdminService.ChangeStateHealthSuperParameters
        Try
            Dim HealthSuperParameters As HealthSuperParameters = Me._HealthSuperParametersRepository.GetHealthSuperParametersByCode(code.Trim())
            If HealthSuperParameters IsNot Nothing AndAlso HealthSuperParameters.Id > 0 Then
                HealthSuperParameters.Status = state
            End If
            Dim result = Me.SaveHealthSuperParameters(HealthSuperParameters, audit)
            If result.StatusCode = eStatusResult.SUCCESS Then
                result.Message = ResourceManager.GetString("UpdateState")
            End If
            Return result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of HealthSuperParameters) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    Public Function DeleteStateHealthSuperParameters(HealthSuperParameters As HealthSuperParameters, audit As AuditMessage) As ActionResult(Of HealthSuperParameters) Implements IHealthSuperParametersAdminService.DeleteHealthSuperParameters
        Dim unitOfWork As IUnitWork = Me._HealthSuperParametersRepository.UnitWork
        Try
            If HealthSuperParameters Is Nothing Then
                Throw New ArgumentNullException("")
            ElseIf HealthSuperParameters IsNot Nothing AndAlso HealthSuperParameters.Id > 0 Then
                Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                    HealthSuperParameters.ModificationUser = audit.CodeUser
                    HealthSuperParameters.ModificationDate = Date.Now
                    Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                    Dim auditProcess As New IndigoAuditSimpleEntity(Of HealthSuperParameters)(HealthSuperParameters, audit, status)

                    While HealthSuperParameters.HealthSuperParametersFt003.Count > 0
                        HealthSuperParameters.HealthSuperParametersFt003(HealthSuperParameters.HealthSuperParametersFt003.Count - 1).MarkAsDeleted()
                    End While
                    While HealthSuperParameters.HealthSuperParametersFt004.Count > 0
                        For Each Item In HealthSuperParameters.HealthSuperParametersFt004
                            While Item.HealthSuperParametersFt004Detail.Count > 0
                                Item.HealthSuperParametersFt004Detail(Item.HealthSuperParametersFt004Detail.Count - 1).MarkAsDeleted()
                            End While
                        Next
                        HealthSuperParameters.HealthSuperParametersFt004(HealthSuperParameters.HealthSuperParametersFt004.Count - 1).MarkAsDeleted()
                    End While
                    While HealthSuperParameters.HealthSuperParametersFt006.Count > 0
                        HealthSuperParameters.HealthSuperParametersFt006(HealthSuperParameters.HealthSuperParametersFt006.Count - 1).MarkAsDeleted()
                    End While
                    While HealthSuperParameters.HealthSuperParametersFt006.Count > 0
                        HealthSuperParameters.HealthSuperParametersFt006(HealthSuperParameters.HealthSuperParametersFt006.Count - 1).MarkAsDeleted()
                    End While
                    While HealthSuperParameters.HealthSuperParametersFt007.Count > 0
                        HealthSuperParameters.HealthSuperParametersFt007(HealthSuperParameters.HealthSuperParametersFt007.Count - 1).MarkAsDeleted()
                    End While
                    While HealthSuperParameters.HealthSuperParametersFt008.Count > 0
                        HealthSuperParameters.HealthSuperParametersFt008(HealthSuperParameters.HealthSuperParametersFt008.Count - 1).MarkAsDeleted()
                    End While
                    While HealthSuperParameters.HealthSuperParametersFt009.Count > 0
                        HealthSuperParameters.HealthSuperParametersFt009(HealthSuperParameters.HealthSuperParametersFt009.Count - 1).MarkAsDeleted()
                    End While
                    HealthSuperParameters.MarkAsDeleted()
                    Me._HealthSuperParametersRepository.SaveEntity(HealthSuperParameters)
                    unitOfWork.Commit()
                    auditProcess.Execute()
                    scope.Complete()
                    Return New ActionResult(Of HealthSuperParameters) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .Message = ResourceManager.GetString("RecordDeleted")}
                End Using
            End If
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of HealthSuperParameters) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
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

            _sequenseDRepository = Nothing
            _HealthSuperParametersRepository = Nothing
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
