
#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base
Imports Application.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Base.Entities
Imports System.Data.Entity.Core
Imports Infrastructure.CrossCutting.Resources
Imports System.Transactions
Imports System.Data.Entity.Infrastructure

#End Region


''' <summary>
''' interface que especifica las metodos y funciones que manejara todas las acciones sobre la entidad poliza
''' </summary>
''' <remarks></remarks>

Public Class FixedAssetPolicyAdminService
    Implements IFixedAssetPolicyAdminService
    Private Const FORM_NAME As String = "FrmFixedAssetPoliza"
    ''' <summary>
    ''' Repositorio de secuencias numericas
    ''' </summary>
    Private _secuenseDRepository As IFixedAssetSequenceDetailRepository

    'Repositorio de tipo de fabricante
    Private _PolizaRepository As IFixedAssetPolicyRepository

    ''' <summary>
    ''' inicia el repositorio de fabricante
    ''' </summary>
    ''' <param name="PolizaRepository">Repositorio de fabricante</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal PolizaRepository As IFixedAssetPolicyRepository, ByVal secuenseDRepository As IFixedAssetSequenceDetailRepository)
        If (PolizaRepository Is Nothing) Then
            Throw New ArgumentNullException("Repositorio de poliza")
        End If
        If (secuenseDRepository Is Nothing) Then
            Throw New ArgumentNullException("secuenseDRepository")
        End If
        _PolizaRepository = PolizaRepository
        _secuenseDRepository = secuenseDRepository
    End Sub

    Public Function DeletePoliza(Poliza As FixedAssetPolicy, audit As AuditMessage) As ActionResult Implements IFixedAssetPolicyAdminService.DeletePoliza

        If Poliza Is Nothing Then
            Throw New ArgumentNullException("invoiceCategory")
        End If
        Dim unitOfWork As IUnitWork = Me._PolizaRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Poliza.ModificationUser = audit.CodeUser
                Poliza.ModificationDate = Date.Now
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of FixedAssetPolicy)(Poliza, audit, status)

                'While invoiceCategory.InvoiceCategoriesUser.Count > 0
                '    invoiceCategory.InvoiceCategoriesUser(invoiceCategory.InvoiceCategoriesUser.Count - 1).MarkAsDeleted()
                'End While
                Poliza.MarkAsDeleted()
                Me._PolizaRepository.SaveEntity(Poliza)
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

    Public Function GetPoliza(codePoliza As String, audit As AuditMessage) As FixedAssetPolicy Implements IFixedAssetPolicyAdminService.GetPoliza
        If String.IsNullOrEmpty(codePoliza) Then
            Throw New ArgumentNullException("Codigo de tipo de inventario vacio")
        End If
        Try
            Dim poliza As Domain.Entities.FixedAssetPolicy = _PolizaRepository.GetPoliza(codePoliza)
            If poliza IsNot Nothing AndAlso poliza.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of Domain.Entities.FixedAssetPolicy)(poliza, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return poliza
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New FixedAssetPolicy()
        End Try
    End Function

    Public Function ListAllPoliza() As List(Of FixedAssetPolicy) Implements IFixedAssetPolicyAdminService.ListAllPoliza
        Try
            Return _PolizaRepository.ListAllPoliza
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function SavePoliza(Poliza As FixedAssetPolicy, audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of FixedAssetPolicy) Implements IFixedAssetPolicyAdminService.SavePoliza

        If Poliza Is Nothing Then
            Throw New ArgumentNullException("ObjEntity")
        End If
        Dim unitOfWork As IUnitWork = Me._PolizaRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty

                If String.IsNullOrEmpty(Poliza.Code) Then
                    Dim seq As FixedAssetSequenceDetail = Me._secuenseDRepository.GetSequenseDetailUpdatedById(idSequense)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.FixedAssetSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            Poliza.Code = res
                            seq.Next += 1
                            Me._secuenseDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of FixedAssetPolicy) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                        MessageResult = If(seq.FixedAssetSequence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), Poliza.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of FixedAssetPolicy) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If

                Dim auxObjEntity As FixedAssetPolicy = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of FixedAssetPolicy)
                Dim status As Integer

                If Poliza.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    Poliza.CreationUser = audit.CodeUser
                    Poliza.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    auxObjEntity = Poliza.OriginalValue
                    Poliza.ModificationUser = audit.CodeUser
                    Poliza.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Me._PolizaRepository.SaveEntity(Poliza)
                unitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of FixedAssetPolicy)(Poliza, audit, status, auxObjEntity)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                Poliza.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of FixedAssetPolicy) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = Poliza, .Message = MessageResult}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of FixedAssetPolicy) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of FixedAssetPolicy) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Metodo para cambiar el estado de la entidad
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function ChangeStatePoliza(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of FixedAssetPolicy) Implements IFixedAssetPolicyAdminService.ChangeStatePoliza
        'Dim poliza As Domain.Entities.FixedAssetPolicy = GetPoliza(code, audit)
        'poliza.Status = state
        'poliza.MarkAsModified()
        'Return SavePoliza(poliza, audit)

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
            Dim poliza As FixedAssetPolicy = GetPoliza(code.Trim(), audit)
            If poliza IsNot Nothing AndAlso poliza.Id > 0 Then
                poliza.Status = state
            End If
            Dim result = Me.SavePoliza(poliza, audit)
            If result.StatusCode = eStatusResult.SUCCESS Then
                result.Message = ResourceManager.GetString("UpdateState")
            End If
            Return result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of FixedAssetPolicy) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try

    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _PolizaRepository = Nothing
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
