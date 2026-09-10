#Region "imports"
Imports Domain.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Transactions
Imports System.Data.Entity.Infrastructure
Imports System.Data.SqlClient
#End Region

Public Class CPCCatalogAdminService
    Implements ICPCCatalogAdminService

#Region "Fields"

    ''' <summary>
    ''' Repositorio de los rubros presupuestales
    ''' </summary>
    Private _CPCCatalogRepository As ICPCCatalogRepository

#End Region

#Region "Constructor"
    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(ByVal CPCCatalogRepository As ICPCCatalogRepository)
        If CPCCatalogRepository Is Nothing Then
            Throw New ArgumentNullException("repositoryCPCCatalogRepository")
        End If
        Me._CPCCatalogRepository = CPCCatalogRepository
    End Sub
#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene un presupuesto por Id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCPCCatalogById(Id As Integer, audit As AuditMessage) As ActionResult(Of CPCCatalog) Implements ICPCCatalogAdminService.GetCPCCatalogById
        If Id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim CPCCatalog As CPCCatalog = Me._CPCCatalogRepository.GetCPCCatalogById(Id)
            If CPCCatalog IsNot Nothing AndAlso CPCCatalog.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of CPCCatalog)(CPCCatalog, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of CPCCatalog) With {.StateResult = True, .ObjectEmbbeded = CPCCatalog}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CPCCatalog) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un rubro por código 
    ''' </summary>
    '''<param name="Code">Código del Rubro</param>
    ''' <returns></returns>
    Function GetCPCCatalogByCode(Code As String, audit As AuditMessage) As ActionResult(Of CPCCatalog) Implements ICPCCatalogAdminService.GetCPCCatalogByCode
        If String.IsNullOrEmpty(Code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim CPCCatalog As CPCCatalog = Me._CPCCatalogRepository.GetCPCCatalogByCode(Code.Trim())
            Return New ActionResult(Of CPCCatalog) With {.StateResult = True, .ObjectEmbbeded = CPCCatalog}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CPCCatalog) With {.StateResult = False, .Message = ex.Message}
        End Try
    End Function

    ''' <summary>
    ''' Guarda o Actualiza un CPCCatalog
    ''' </summary>
    ''' <param name="CPCCatalog">la entidad</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">entidad Vacio</exception>
    Function SaveCPCCatalog(CPCCatalog As CPCCatalog, audit As AuditMessage) As ActionResult(Of CPCCatalog) Implements ICPCCatalogAdminService.SaveCPCCatalog
        If CPCCatalog Is Nothing Then
            Throw New ArgumentNullException("CPCCatalog")
        End If
        Dim unitOfWork As IUnitWork = Me._CPCCatalogRepository.UnitWork
        Try
            Dim auxCPCCatalog As CPCCatalog = Nothing
            Dim auditProcess As IndigoAuditSimpleEntity(Of CPCCatalog)
            Dim status As Integer

            If CPCCatalog.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                CPCCatalog.CreationUser = audit.CodeUser
                CPCCatalog.CreationDate = DateTime.Now
                status = Infrastructure.CrossCutting.Audit.Actions.Insert
            Else
                auxCPCCatalog = CPCCatalog.OriginalValue
                CPCCatalog.ModificationUser = audit.CodeUser
                CPCCatalog.ModificationDate = DateTime.Now
                status = Infrastructure.CrossCutting.Audit.Actions.Update
            End If

            Me._CPCCatalogRepository.SaveEntity(CPCCatalog)
            unitOfWork.Commit()
            auditProcess = New IndigoAuditSimpleEntity(Of CPCCatalog)(CPCCatalog, audit, status, auxCPCCatalog)
            auditProcess.Execute()

            'Se marca la entidad como sin cambios
            CPCCatalog.MarkAsUnchanged()

            Return New ActionResult(Of CPCCatalog) With {.StateResult = True, .ObjectEmbbeded = CPCCatalog}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of CPCCatalog) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CPCCatalog) With {.StateResult = False, .MessageResult = {Utils.GetInnerExceptionMessageToString(ex)}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Metodo para cambiar el estado de la entidad.
    ''' </summary>
    ''' <param name="CPCCatalog">The CPCCatalog.</param>
    ''' <param name="status">if set to <c>true</c> [status].</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Public Function ChangeStatusCPCCatalog(CPCCatalog As CPCCatalog, status As Boolean, audit As AuditMessage) As ActionResult(Of CPCCatalog) Implements ICPCCatalogAdminService.ChangeStatusCPCCatalog
        CPCCatalog.Status = status
        CPCCatalog.MarkAsModified()
        Return SaveCPCCatalog(CPCCatalog, audit)
    End Function

    ''' <summary>
    ''' Elimina 
    ''' </summary>
    ''' <param name="CPCCatalog">La entidad</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">company Vacio</exception>
    Function DeleteCPCCatalog(CPCCatalog As CPCCatalog, audit As AuditMessage) As ActionResult Implements ICPCCatalogAdminService.DeleteCPCCatalog
        If CPCCatalog Is Nothing Then
            Throw New ArgumentNullException("CPCCatalog")
        End If
        Dim unitOfWork As IUnitWork = Me._CPCCatalogRepository.UnitWork
        Try
            Dim auditProcess As IndigoAuditSimpleEntity(Of CPCCatalog)
            auditProcess = New IndigoAuditSimpleEntity(Of CPCCatalog)(CPCCatalog, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            Me._CPCCatalogRepository.DeleteEntity(CPCCatalog)
            unitOfWork.Commit()
            auditProcess.Execute()
            Return New ActionResult With {.StateResult = True}
        Catch ex As DbUpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-111"})}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-999"})}
        Catch ex As UpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"})}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .MessageResult = {Utils.GetInnerExceptionMessageToString(ex)}.ToList}
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
            Me._CPCCatalogRepository = Nothing
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
