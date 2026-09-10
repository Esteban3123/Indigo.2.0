'***********************************************************************
' Assembly         : Application.Glosas
' Author           : Juan Diego Diaz
' Created          : 22-05-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.Data.Entity.Core
Imports System.Transactions
Imports Application.Base
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Common.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions

#End Region

''' <summary>
''' Servicio de Conciliación Cabecera.
''' </summary>
''' <remarks></remarks>
Public Class ConciliationCAdminService
    Implements IConciliationCAdminService

#Region "Fields"

    ''' <summary>
    ''' Repositorio de conciliacion
    ''' </summary>
    Private _conciliationCRepository As IConciliationCRepository

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase <see cref="ConciliationCAdminService" />.
    ''' </summary>
    ''' <param name="conciliationCRepository">El repositorio para el manejo de las cabeceras de conciliación.</param>
    Public Sub New(ByVal conciliationCRepository As IConciliationCRepository)
        _conciliationCRepository = conciliationCRepository
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene una conciliación cabecera según código.
    ''' </summary>
    ''' <param name="Id">Id Conciliación Cabecera</param>
    ''' <returns>Objeto Conciliación Cabecera</returns>
    Public Function GetConciliationC(Id As String, audit As AuditMessage) As ConciliationC Implements IConciliationCAdminService.GetConciliationC
        If String.IsNullOrEmpty(Id) Then
            Throw New ArgumentNullException("Id vacío")
        End If
        Try
            Dim Conciliation = _conciliationCRepository.GetConciliationC(Id)
            If Conciliation.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of ConciliationC)(Conciliation, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return Conciliation
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una conciliación cabecera según consecutivo.
    ''' </summary>
    ''' <param name="Consecutive">Consecutivo Conciliación Cabecera</param>
    ''' <returns>Objeto Conciliación Cabecera</returns>
    Public Function GetConciliationCByConsecutive(Consecutive As String, audit As AuditMessage) As ConciliationC Implements IConciliationCAdminService.GetConciliationCByConsecutive
        If String.IsNullOrEmpty(Consecutive) Then
            Throw New ArgumentNullException("Consecutivo vacío")
        End If
        Try
            Dim Conciliation = _conciliationCRepository.GetConciliationCByConsecutive(Consecutive)
            If Conciliation.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of ConciliationC)(Conciliation, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return _conciliationCRepository.GetConciliationCByConsecutive(Consecutive)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Guardar Conciliación Cabecera
    ''' </summary>
    ''' <param name="conciliacionC">Objeto Conciliacion Cabecera</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Action Result</returns>
    Public Function SaveConciliationC(conciliacionC As ConciliationC, audit As AuditMessage) As ActionResult(Of ConciliationC) Implements IConciliationCAdminService.SaveConciliationC
        If conciliacionC Is Nothing Then
            Throw New ArgumentNullException("Cabecera conciliación vacio")
        End If

        Dim unitOfWork As IUnitWork = Me._conciliationCRepository.UnitWork

        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                Dim conciliacionCXml As String = conciliacionC.ToXML()
                Dim auditStatus As Infrastructure.CrossCutting.Audit.Actions = Utils.GetAuditStatus(conciliacionC.Id, conciliacionC.State)

                Dim resultStore = Me._conciliationCRepository.SP_SaveConciliation(conciliacionCXml, audit.CodeUser)
                If resultStore.CodeResult <> 0 Then
                    unitOfWork.RollbackChanges()
                    transaction.Dispose()
                    Return New ActionResult(Of ConciliationC) With {.StateResult = False, .Message = resultStore.MessageResult}
                End If

                conciliacionC = Me._conciliationCRepository.GetConciliationC(resultStore.Id)

                Dim auditProcess = New IndigoAuditSimpleEntity(Of ConciliationC)(conciliacionC, audit, auditStatus, conciliacionC.OriginalValue)
                auditProcess.Execute()

                conciliacionC.MarkAsUnchanged()
                transaction.Complete()
                Return New ActionResult(Of ConciliationC) With {.StateResult = True, .ObjectEmbbeded = conciliacionC, .Message = resultStore.MessageResult}
            Catch ex As OptimisticConcurrencyException
                unitOfWork.RollbackChanges()
                transaction.Dispose()
                Return New ActionResult(Of ConciliationC) With {.StateResult = False, .MessageResult = {"-999"}.ToList(), .Message = ex.Message}
            Catch ex As Exception
                unitOfWork.RollbackChanges()
                transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of ConciliationC) With {.StateResult = False, .MessageResult = {Utils.GetInnerExceptionMessageToString(ex)}.ToList(), .Message = Utils.GetInnerExceptionMessageToString(ex)}
            End Try
        End Using
    End Function

    ''' <summary>
    ''' Borrar Conciliación Cabecera.
    ''' </summary>
    ''' <param name="ConciliacionC">Objeto Conciliación Cabecera</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Action Result</returns>
    Public Function DeleteConciliationC(ConciliacionC As ConciliationC, audit As AuditMessage) As ActionResult Implements IConciliationCAdminService.DeleteConciliationC
        If ConciliacionC Is Nothing Then
            Throw New ArgumentNullException("Cabecera Conciliación Vacia")
        End If
        Dim unitOfWork As IUnitWork = _conciliationCRepository.UnitWork
        Try
            'Elimino la cabecera de conciliación.
            _conciliationCRepository.DeleteEntity(ConciliacionC)
            unitOfWork.Commit()
            IndigoAuditBasic.Execute("ConciliationC", audit.Functional, ConciliacionC.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Eliminar, audit.Company, audit.ContainerSecurity)
            Dim auditObject As New IndigoAuditSimpleEntity(Of ConciliationC)(ConciliacionC, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            auditObject.Execute()
            Return New ActionResult With {.StateResult = True}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-999"})}
        Catch ex As UpdateException
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"})}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .MessageResult = {ex.Message}.ToList()}
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

            _conciliationCRepository = Nothing
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
