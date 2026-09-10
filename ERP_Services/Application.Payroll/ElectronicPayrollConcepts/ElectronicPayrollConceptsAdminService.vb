'***********************************************************************
' Assembly         : DistributedServices.Common
' Author           : Andres Felipe Quintero Garcia
' Created          : 10-02-2025
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Application.Base
Imports System.Data.Entity.Core
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Domain.Payroll
Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Infrastructure.Data.ModelRepository
Imports Infrastructure.CrossCutting
Imports Infrastructure.CrossCutting.Resources
Imports System.Transactions
Imports System.Data.Entity.Infrastructure
#End Region

Public Class ElectronicPayrollConceptsAdminService
    Implements IElectronicPayrollConceptsAdminService, Inject
    Private Const FORM_NAME As String = "FrmElectronicPayrollConcepts"

    Private ReadOnly _electronicPayrollConceptsRepository As IElectronicPayrollConceptsRepository
    ''' <summary>
    ''' Repositorio de secuencias numericas
    ''' </summary>
    Private _secuenseDRepository As Domain.Entities.IPayrollSequenceDetailRepository
    ''' <summary>
    ''' incia el repositorio de ElectronicPayrollConcepts
    ''' </summary>
    ''' <param name="repository">Repositorio de Electronic Payroll Concepts</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal repository As IElectronicPayrollConceptsRepository, ByVal secuenseDRepository As Domain.Entities.IPayrollSequenceDetailRepository)
        If (repository Is Nothing = True) Then
            Throw New ArgumentNullException("ElectronicPayrollConceptsRepository Vacion")
        End If
        If secuenseDRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseDRepository")
        End If
        _electronicPayrollConceptsRepository = repository
        _secuenseDRepository = secuenseDRepository
    End Sub

    ''' <summary>
    ''' Obtiene un Conceptos de nómina electrónica
    ''' </summary>
    ''' <param name="code">Codigo Conceptos de nómina electrónica</param>
    ''' <returns>Conceptos de nómina electrónica</returns>
    ''' <remarks></remarks>
    Public Function GetElectronicPayrollConceptsByCode(code As String) As ElectronicPayrollConcepts Implements IElectronicPayrollConceptsAdminService.GetElectronicPayrollConceptsByCode
        If (String.IsNullOrEmpty(code) = True) Then
            Throw New ArgumentNullException("Code Vacio")
        End If
        Try

            Return _electronicPayrollConceptsRepository.GetElectronicPayrollConcepts(code)

        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Lista todos los Conceptos de nómina electrónica
    ''' </summary>
    ''' <returns>Lista de Conceptos de nómina electrónica</returns>
    ''' <remarks></remarks>
    Public Function ListAllElectronicPayrollConcepts() As List(Of Domain.Payroll.Entities.ElectronicPayrollConcepts) Implements IElectronicPayrollConceptsAdminService.ListAllElectronicPayrollConcepts
        Try

            Return _electronicPayrollConceptsRepository.ListAllElectronicPayrollConcepts()


        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Graba o actualiza los Conceptos de nómina electrónica
    ''' </summary>
    ''' <param name="electronicPayrollConcepts">Conceptos de nómina electrónica</param>
    ''' <param name="audit">Objeto auditoria</param>
    ''' <returns>True o False</returns>
    ''' <remarks></remarks>
    Public Function SaveElectronicPayrollConcepts(electronicPayrollConcepts As ElectronicPayrollConcepts, audit As AuditMessage, Optional idSequence As Int64 = 0) As ActionResult(Of ElectronicPayrollConcepts) Implements IElectronicPayrollConceptsAdminService.SaveElectronicPayrollConcepts
        If electronicPayrollConcepts Is Nothing Then
            Throw New ArgumentNullException("ObjEntity")
        End If
        Dim unitOfWork As IUnitWork = Me._electronicPayrollConceptsRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty

                If String.IsNullOrEmpty(electronicPayrollConcepts.Code) Then
                    Dim seq As Domain.Entities.PayrollSequenceDetail = Me._secuenseDRepository.GetSequenseDetailUpdatedById(idSequence)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.PayrollSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            electronicPayrollConcepts.Code = res
                            seq.Next += 1
                            Me._secuenseDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of ElectronicPayrollConcepts) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                        MessageResult = If(seq.PayrollSequence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), electronicPayrollConcepts.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of ElectronicPayrollConcepts) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If

                Dim auxObjEntity As ElectronicPayrollConcepts = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of ElectronicPayrollConcepts)
                Dim status As Integer

                If electronicPayrollConcepts.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    electronicPayrollConcepts.CreationUser = audit.CodeUser
                    electronicPayrollConcepts.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    MessageResult = ResourceManager.GetString("UpdateMessage")
                    auxObjEntity = _electronicPayrollConceptsRepository.GetElectronicPayrollConcepts(electronicPayrollConcepts.Code, False)
                    electronicPayrollConcepts.ModificationUser = audit.CodeUser
                    electronicPayrollConcepts.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Me._electronicPayrollConceptsRepository.SaveEntity(electronicPayrollConcepts)
                unitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of ElectronicPayrollConcepts)(electronicPayrollConcepts, audit, status, auxObjEntity)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                electronicPayrollConcepts.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of ElectronicPayrollConcepts) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = electronicPayrollConcepts, .Message = MessageResult}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of ElectronicPayrollConcepts) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of ElectronicPayrollConcepts) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Elimina Conceptos de nómina electrónica
    ''' </summary>
    ''' <param name="electronicPayrollConcepts">Conceptos de nómina electrónica</param>
    ''' <param name="audit">Objeto auditoria</param>
    ''' <returns>True o False</returns>
    ''' <remarks></remarks>
    Public Function DeleteElectronicPayrollConcepts(electronicPayrollConcepts As ElectronicPayrollConcepts, audit As AuditMessage) As ActionResult Implements IElectronicPayrollConceptsAdminService.DeleteElectronicPayrollConcepts
        If electronicPayrollConcepts Is Nothing Then
            Throw New ArgumentNullException("invoiceCategory")
        End If
        Dim unitOfWork As IUnitWork = Me._electronicPayrollConceptsRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                electronicPayrollConcepts.ModificationUser = audit.CodeUser
                electronicPayrollConcepts.ModificationDate = Date.Now
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of ElectronicPayrollConcepts)(electronicPayrollConcepts, audit, status)

                electronicPayrollConcepts.MarkAsDeleted()
                Me._electronicPayrollConceptsRepository.SaveEntity(electronicPayrollConcepts)
                unitOfWork.Commit()
                IndigoAuditBasic.Execute("ElectronicPayrollConcepts", audit.Functional, electronicPayrollConcepts.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Eliminar, audit.Company, audit.ContainerSecurity)
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

    Function ChangeStateElectronicPayrollConcepts(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of ElectronicPayrollConcepts) Implements IElectronicPayrollConceptsAdminService.ChangeStateElectronicPayrollConcepts
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
            Dim ElectronicPayrollConcepts As ElectronicPayrollConcepts = Me._electronicPayrollConceptsRepository.GetElectronicPayrollConcepts(code.Trim())
            If ElectronicPayrollConcepts IsNot Nothing AndAlso ElectronicPayrollConcepts.Id > 0 Then
                ElectronicPayrollConcepts.State = state
            End If
            Dim result = Me.SaveElectronicPayrollConcepts(ElectronicPayrollConcepts, audit)
            If result.StatusCode = eStatusResult.SUCCESS Then
                result.Message = ResourceManager.GetString("UpdateState")
            End If
            Return result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of ElectronicPayrollConcepts) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean

    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: eliminar el estado administrado (objetos administrados)
            End If

            ' TODO: liberar los recursos no administrados (objetos no administrados) y reemplazar el finalizador
            ' TODO: establecer los campos grandes como NULL
            disposedValue = True
        End If
    End Sub

    ' ' TODO: reemplazar el finalizador solo si "Dispose(disposing As Boolean)" tiene código para liberar los recursos no administrados
    ' Protected Overrides Sub Finalize()
    '     ' No cambie este código. Coloque el código de limpieza en el método "Dispose(disposing As Boolean)".
    '     Dispose(disposing:=False)
    '     MyBase.Finalize()
    ' End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        ' No cambie este código. Coloque el código de limpieza en el método "Dispose(disposing As Boolean)".
        Dispose(disposing:=True)
        GC.SuppressFinalize(Me)
    End Sub

End Class
#End Region
