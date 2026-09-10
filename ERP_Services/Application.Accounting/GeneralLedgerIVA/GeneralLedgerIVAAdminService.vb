'***********************************************************************
' Assembly         : Application.Accounting
' Author           : Diego Andrés Roldán Lozano
' Created          : 02-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Data.Entity.Infrastructure
Imports System.Data.Entity.Core
Imports Infrastructure.CrossCutting.Resources
Imports System.Transactions

#End Region

''' <summary>
''' Gestiona los servicios disponibles para todas las operaciones
''' con la entidad GeneralLedgerIVA
''' </summary>
Public Class GeneralLedgerIVAAdminService
    Implements IGeneralLedgerIVAAdminService

#Region "Fileds"

    ''' <summary>
    ''' Repositorio de la entidad GeneralLedgerIVA
    ''' </summary>
    Dim _generalLedgerIVARepository As IGeneralLedgerIVARepository

    ''' <summary>
    ''' repositorio de las secuencias
    ''' </summary>
    Private _secuenseDRepository As ISequenseAccountingDRepository

    Private Const FORM_NAME As String = "IVA"
#End Region

#Region "Builders"
    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="generalLedgerIVARepository">Repositorio de la entidad GeneralLedgerIVA</param>
    Public Sub New(ByVal generalLedgerIVARepository As IGeneralLedgerIVARepository, ByVal secuenseDRepository As ISequenseAccountingDRepository)
        If generalLedgerIVARepository Is Nothing Then
            Throw New ArgumentNullException("generalLedgerIVARepository")
        End If
        If secuenseDRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseDRepository")
        End If
        Me._generalLedgerIVARepository = generalLedgerIVARepository
        Me._secuenseDRepository = secuenseDRepository
    End Sub
#End Region

#Region "Methods"

    ''' <summary>
    ''' Elimina un Iva
    ''' </summary>
    ''' <param name="doc">Iva a eliminar</param>
    ''' <param name="audit">Mensaje de auditoria</param>
    ''' <returns>Resultado de la acción</returns>
    Public Function DeleteGeneralLedgerIVA(doc As GeneralLedgerIVA, audit As AuditMessage) As ActionResult Implements IGeneralLedgerIVAAdminService.DeleteGeneralLedgerIVA
        If doc Is Nothing Then
            Throw New ArgumentNullException("Iva")
        End If
        Dim unitOfWork As IUnitWork = Me._generalLedgerIVARepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                doc.ModificationUser = audit.CodeUser
                doc.ModificationDate = Date.Now
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
                Dim auditProcess As New IndigoAuditSimpleEntity(Of GeneralLedgerIVA)(doc, audit, status)

                doc.MarkAsDeleted()
                Me._generalLedgerIVARepository.SaveEntity(doc)
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

    ''' <summary>
    ''' Obtiene un Iva por su código
    ''' </summary>
    ''' <param name="code">Código del Iva a consultar</param>
    ''' <param name="audit">Mensaje de auditoria</param>
    ''' <returns>Iva consultado</returns>
    Public Function GetGeneralLedgerIVAByCode(code As String, audit As AuditMessage) As GeneralLedgerIVA Implements IGeneralLedgerIVAAdminService.GetGeneralLedgerIVAByCode
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim doc As GeneralLedgerIVA = Me._generalLedgerIVARepository.GetGeneralLedgerIVAByCode(code.Trim())
            If doc IsNot Nothing AndAlso doc.Id > 0 Then
                Dim auditProcess As New IndigoAuditSimpleEntity(Of GeneralLedgerIVA)(doc, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditProcess.Execute()
            End If
            Return doc
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un tipo de documento por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetGeneralLedgerIVAById(id As Integer, audit As AuditMessage) As ActionResult(Of GeneralLedgerIVA) Implements IGeneralLedgerIVAAdminService.GetGeneralLedgerIVAById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim generalLedgerIVA As GeneralLedgerIVA = Me._generalLedgerIVARepository.GetGeneralLedgerIVAById(id)
            Return New ActionResult(Of GeneralLedgerIVA) With {.StateResult = True, .ObjectEmbbeded = generalLedgerIVA}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of GeneralLedgerIVA) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
        End Try
    End Function

    ''' <summary>
    ''' Graba un Iva
    ''' </summary>
    ''' <param name="audit">Mensaje de auditoria</param>
    ''' <returns>Resultado de la acción</returns>
    Public Function SaveGeneralLedgerIVA(glIVA As GeneralLedgerIVA, audit As AuditMessage, Optional idSequence As Long = 0) As ActionResult(Of GeneralLedgerIVA) Implements IGeneralLedgerIVAAdminService.SaveGeneralLedgerIVA
        If glIVA Is Nothing Then
            Throw New ArgumentNullException("bankCity")
        End If
        Dim unitOfWork As IUnitWork = Me._generalLedgerIVARepository.UnitWork
        Dim unitWorkSequence As IUnitWork = Me._secuenseDRepository.UnitWork

        Try

            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})

                Dim MessageResult As String = String.Empty
                If glIVA.Code Is Nothing OrElse glIVA.Code.Trim().Equals(String.Empty) Then
                    Dim seq As GeneralLedgerSequenceDetail = Me._secuenseDRepository.GetSequenseDetailUpdatedById(idSequence)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.GeneralLedgerSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            glIVA.Code = res
                            seq.Next += 1
                            Me._secuenseDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of GeneralLedgerIVA) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                        MessageResult = If(seq.GeneralLedgerSequence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), glIVA.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of GeneralLedgerIVA) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .MessageResult = {"_Seq02_"}.ToList(), .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If

                Dim auxDocumentType As GeneralLedgerIVA = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of GeneralLedgerIVA)
                Dim status As Integer

                If glIVA.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    glIVA.CreationDate = DateTime.Now
                    glIVA.CreationUser = audit.CodeUser
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    glIVA.ModificationDate = DateTime.Now
                    glIVA.ModificationUser = audit.CodeUser
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                    auxDocumentType = glIVA.OriginalValue
                End If

                Me._generalLedgerIVARepository.SaveEntity(glIVA)
                unitOfWork.Commit()
                unitWorkSequence.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of GeneralLedgerIVA)(glIVA, audit, status, auxDocumentType)
                auditProcess.Execute()

                scope.Complete()
                Return New ActionResult(Of GeneralLedgerIVA) With {.StateResult = True, .StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = glIVA, .Message = MessageResult}
            End Using

        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of GeneralLedgerIVA) With {.StateResult = False, .StatusCode = eStatusResult.WARNING, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of GeneralLedgerIVA) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .MessageResult = {ex.Message}.ToList, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Updates the state
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Public Function UpdateStateGeneralLedgerIVA(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of GeneralLedgerIVA) Implements IGeneralLedgerIVAAdminService.UpdateStateGeneralLedgerIVA
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
            Dim glIva As GeneralLedgerIVA = Me._generalLedgerIVARepository.GetGeneralLedgerIVAByCode(code.Trim())
            If glIva IsNot Nothing AndAlso glIva.Id > 0 Then
                glIva.Status = state
                glIva.MarkAsModified()
            End If
            Return Me.SaveGeneralLedgerIVA(glIva, audit)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of GeneralLedgerIVA) With {.StateResult = False}
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
            Me._generalLedgerIVARepository = Nothing
            Me._secuenseDRepository = Nothing
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
