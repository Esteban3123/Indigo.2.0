#Region "Imports"
Imports Domain.Base
Imports Infrastructure.CrossCutting.Base
Imports Application.Base
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Base.Entities
Imports System.Data.Entity.Core
Imports Infrastructure.CrossCutting.Resources
Imports System.Transactions
Imports System.Data.Entity.Infrastructure
#End Region

Public Class FixedAssetStatusAdminService

    Implements IFixedAssetStatusAdminService

    'Repositorio de la aseguradora
    Private _FixedAssetStatusRepository As IFixedAssetStatusRepository

    'repositorio de la secuencia
    Private _sequenceRepository As IFixedAssetSequenceDetailRepository

    Private Const FORM_NAME As String = "FrmFixedAssetStatus"

    ''' <summary>
    ''' inicia el repositorio de bancos
    ''' </summary>
    ''' <param name="InsuranceRepository">Repositorio de bancos</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal FixedAssetStatusRepository As IFixedAssetStatusRepository, sequenceRepository As IFixedAssetSequenceDetailRepository)
        If (FixedAssetStatusRepository Is Nothing) Then
            Throw New ArgumentNullException("Repositorio de FixedAssetStatusRepository vacio")
        End If
        _sequenceRepository = sequenceRepository
        _FixedAssetStatusRepository = FixedAssetStatusRepository
    End Sub


    Public Function DeleteFixedAssetStatus(FixedAssetStatusAsset As FixedAssetStatusAsset, audit As AuditMessage) As ActionResult Implements IFixedAssetStatusAdminService.DeleteFixedAssetStatus
        'If FixedAssetStatusAsset Is Nothing Then
        '    Throw New ArgumentNullException("FixedAssetInventoryType vacio")
        'End If
        'Dim unitWork As IUnitWork = _FixedAssetStatusRepository.UnitWork
        'Try
        '    _FixedAssetStatusRepository.DeleteEntity(FixedAssetStatusAsset)
        '    unitWork.Commit()
        '    Dim auditObject As New IndigoAuditSimpleEntity(Of FixedAssetStatusAsset)(FixedAssetStatusAsset, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
        '    auditObject.Execute()
        '    Return True
        'Catch ex As Exception
        '    unitWork.RollbackChanges()
        '    IndigoManagementExceptions.HandleExceptionUI(ex, "ApplicationPolicy")
        '    Return False
        'End Try


        If FixedAssetStatusAsset Is Nothing Then
            Throw New ArgumentNullException("invoiceCategory")
        End If
        Dim unitOfWork As IUnitWork = Me._FixedAssetStatusRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                FixedAssetStatusAsset.ModificationUser = audit.CodeUser
                FixedAssetStatusAsset.ModificationDate = Date.Now
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of FixedAssetStatusAsset)(FixedAssetStatusAsset, audit, status)

                'While FixedAssetStatusAsset.InvoiceCategoriesUser.Count > 0
                '    invoiceCategory.InvoiceCategoriesUser(invoiceCategory.InvoiceCategoriesUser.Count - 1).MarkAsDeleted()
                'End While
                FixedAssetStatusAsset.MarkAsDeleted()
                Me._FixedAssetStatusRepository.SaveEntity(FixedAssetStatusAsset)
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
    End Function

    Public Function GetFixedAssetStatusAsset(Code As String) As FixedAssetStatusAsset Implements IFixedAssetStatusAdminService.GetFixedAssetStatusAsset
        If String.IsNullOrEmpty(Code) Then
            Throw New ArgumentNullException("Codigo de Marca vacio")
        End If
        Try
            Return _FixedAssetStatusRepository.GetFixedAssetStatusByCode(Code)
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "ApplicationPolicy")
            Return New FixedAssetStatusAsset()
        End Try
    End Function

    Public Function ListAllFixedAssetStatus() As List(Of FixedAssetStatusAsset) Implements IFixedAssetStatusAdminService.ListAllFixedAssetStatus
        Try
            Return _FixedAssetStatusRepository.ListAllFixedAssetStatus()
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function SaveFixedAssetStatusAsset(FixedAssetStatusAsset As FixedAssetStatusAsset, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of FixedAssetStatusAsset) Implements IFixedAssetStatusAdminService.SaveFixedAssetStatusAsset
        'If FixedAssetStatusAsset Is Nothing Then
        '    Throw New ArgumentNullException("FixedAssetStatusAsset")
        'End If
        'Dim unitOfWork As IUnitWork = Me._FixedAssetStatusRepository.UnitWork
        'Dim sequenseUnitOfWork As IUnitWork = Me._sequenceRepository.UnitWork
        'Try
        '    Dim seq As FixedAssetSequenceDetail = Nothing
        '    If FixedAssetStatusAsset.Code Is Nothing OrElse FixedAssetStatusAsset.Code.Trim().Equals(String.Empty) Then
        '        seq = Me._sequenceRepository.GetSequenseDById(idSequense)
        '        If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.FixedAssetSequence.Sequential Then
        '            Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
        '            If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
        '                FixedAssetStatusAsset.Code = res
        '                seq.Next += 1
        '                Me._sequenceRepository.SaveEntity(seq)
        '            Else
        '                Return False
        '            End If
        '        Else
        '            Return False
        '        End If
        '    End If

        '    Dim auxFixedAssetStatusAsset As FixedAssetStatusAsset = Nothing
        '    Dim auditProcess As IndigoAuditSimpleEntity(Of FixedAssetStatusAsset)
        '    Dim status As Integer

        '    If FixedAssetStatusAsset.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
        '        FixedAssetStatusAsset.CreationUser = audit.CodeUser
        '        FixedAssetStatusAsset.CreationDate = DateTime.Now
        '        status = Infrastructure.CrossCutting.Audit.Actions.Insert
        '    Else
        '        auxFixedAssetStatusAsset = FixedAssetStatusAsset.OriginalValue
        '        FixedAssetStatusAsset.ModificationUser = audit.CodeUser
        '        FixedAssetStatusAsset.ModificationDate = DateTime.Now
        '        status = Infrastructure.CrossCutting.Audit.Actions.Update
        '    End If

        '    Me._FixedAssetStatusRepository.SaveEntity(FixedAssetStatusAsset)
        '    unitOfWork.Commit()
        '    sequenseUnitOfWork.Commit()
        '    auditProcess = New IndigoAuditSimpleEntity(Of FixedAssetStatusAsset)(FixedAssetStatusAsset, audit, status, auxFixedAssetStatusAsset)
        '    auditProcess.Execute()

        '    'Se marca la entidad como sin cambios
        '    FixedAssetStatusAsset.MarkAsUnchanged()

        '    Return True
        'Catch ex As OptimisticConcurrencyException
        '    unitOfWork.RollbackChanges()
        '    Return False
        'Catch ex As Exception
        '    unitOfWork.RollbackChanges()
        '    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
        '    Return False
        'End Try


        If FixedAssetStatusAsset Is Nothing Then
            Throw New ArgumentNullException("ObjEntity")
        End If
        Dim unitOfWork As IUnitWork = Me._FixedAssetStatusRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._sequenceRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty

                If String.IsNullOrEmpty(FixedAssetStatusAsset.Code) Then
                    Dim seq As FixedAssetSequenceDetail = Me._sequenceRepository.GetSequenseDetailUpdatedById(idSequense)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.FixedAssetSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            FixedAssetStatusAsset.Code = res
                            seq.Next += 1
                            Me._sequenceRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of FixedAssetStatusAsset) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                        MessageResult = If(seq.FixedAssetSequence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), FixedAssetStatusAsset.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of FixedAssetStatusAsset) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If

                Dim auxObjEntity As FixedAssetStatusAsset = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of FixedAssetStatusAsset)
                Dim status As Integer

                If FixedAssetStatusAsset.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    FixedAssetStatusAsset.CreationUser = audit.CodeUser
                    FixedAssetStatusAsset.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    auxObjEntity = FixedAssetStatusAsset.OriginalValue
                    FixedAssetStatusAsset.ModificationUser = audit.CodeUser
                    FixedAssetStatusAsset.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Me._FixedAssetStatusRepository.SaveEntity(FixedAssetStatusAsset)
                unitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of FixedAssetStatusAsset)(FixedAssetStatusAsset, audit, status, auxObjEntity)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                FixedAssetStatusAsset.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of FixedAssetStatusAsset) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = FixedAssetStatusAsset, .Message = MessageResult}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of FixedAssetStatusAsset) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of FixedAssetStatusAsset) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function


    ''' <summary>
    ''' metodo para cambiar el estado de la entidad
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Public Function FixedAssetStatusChangeState(code As String, state As Boolean, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.FixedAssetStatusAsset) Implements IFixedAssetStatusAdminService.FixedAssetStatusChangeState
        'Dim ObjFixedAssetStatus As FixedAssetStatusAsset = _FixedAssetStatusRepository.GetFixedAssetStatusByCode(code)
        'ObjFixedAssetStatus.Status = state
        'Return SaveFixedAssetStatusAsset(ObjFixedAssetStatus, audit)


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
            Dim ObjFixedAssetStatus As FixedAssetStatusAsset = Me._FixedAssetStatusRepository.GetFixedAssetStatusByCode(code.Trim())
            If ObjFixedAssetStatus IsNot Nothing AndAlso ObjFixedAssetStatus.Id > 0 Then
                ObjFixedAssetStatus.Status = state
            End If
            Dim result = Me.SaveFixedAssetStatusAsset(ObjFixedAssetStatus, audit)
            If result.StatusCode = eStatusResult.SUCCESS Then
                result.Message = ResourceManager.GetString("UpdateState")
            End If
            Return result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of FixedAssetStatusAsset) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _sequenceRepository = Nothing
            _FixedAssetStatusRepository = Nothing
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
