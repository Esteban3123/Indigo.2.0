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

Public Class CCPETAdminService
    Implements ICCPETAdminService

#Region "Fields"

    ''' <summary>
    ''' Repositorio de los rubros presupuestales
    ''' </summary>
    Private _CCPETRepository As ICCPETRepository

#End Region

#Region "Constructor"
    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(ByVal CCPETRepository As ICCPETRepository)
        If CCPETRepository Is Nothing Then
            Throw New ArgumentNullException("repositoryCCPETRepository")
        End If
        Me._CCPETRepository = CCPETRepository
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
    Public Function GetCCPETById(Id As Integer, audit As AuditMessage) As ActionResult(Of CCPET) Implements ICCPETAdminService.GetCCPETById
        If Id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim CCPET As CCPET = Me._CCPETRepository.GetCCPETById(Id)
            If CCPET IsNot Nothing AndAlso CCPET.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of CCPET)(CCPET, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of CCPET) With {.StateResult = True, .ObjectEmbbeded = CCPET}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CCPET) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un rubro por código y vigencia
    ''' </summary>
    '''<param name="Code">Código del Rubro</param>
    ''' <returns></returns>
    Function GetCCPETByCode(Code As String, audit As AuditMessage) As ActionResult(Of CCPET) Implements ICCPETAdminService.GetCCPETByCode
        If String.IsNullOrEmpty(Code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim CCPET As CCPET = Me._CCPETRepository.GetCCPETByCode(Code.Trim())
            Return New ActionResult(Of CCPET) With {.StateResult = True, .ObjectEmbbeded = CCPET}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CCPET) With {.StateResult = False, .Message = ex.Message}
        End Try
    End Function

    ''' <summary>
    ''' Guarda o Actualiza un CCPET
    ''' </summary>
    ''' <param name="CCPET">la entidad</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">entidad Vacio</exception>
    Function SaveCCPET(CCPET As CCPET, audit As AuditMessage) As ActionResult(Of CCPET) Implements ICCPETAdminService.SaveCCPET
        If CCPET Is Nothing Then
            Throw New ArgumentNullException("CCPET")
        End If
        Dim unitOfWork As IUnitWork = Me._CCPETRepository.UnitWork
        Try
            Dim auxCCPET As CCPET = Nothing
            Dim auditProcess As IndigoAuditSimpleEntity(Of CCPET)
            Dim status As Integer

            If CCPET.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                CCPET.CreationUser = audit.CodeUser
                CCPET.CreationDate = DateTime.Now
                status = Infrastructure.CrossCutting.Audit.Actions.Insert
            Else
                auxCCPET = CCPET.OriginalValue
                CCPET.ModificationUser = audit.CodeUser
                CCPET.ModificationDate = DateTime.Now
                status = Infrastructure.CrossCutting.Audit.Actions.Update
            End If

            Me._CCPETRepository.SaveEntity(CCPET)
            unitOfWork.Commit()
            auditProcess = New IndigoAuditSimpleEntity(Of CCPET)(CCPET, audit, status, auxCCPET)
            auditProcess.Execute()

            'Se marca la entidad como sin cambios
            CCPET.MarkAsUnchanged()

            Return New ActionResult(Of CCPET) With {.StateResult = True, .ObjectEmbbeded = CCPET}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of CCPET) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CCPET) With {.StateResult = False, .MessageResult = {Utils.GetInnerExceptionMessageToString(ex)}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Metodo para cambiar el estado de la entidad.
    ''' </summary>
    ''' <param name="CCPET">The CCPET.</param>
    ''' <param name="status">if set to <c>true</c> [status].</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Public Function ChangeStatusCCPET(CCPET As CCPET, status As Boolean, audit As AuditMessage) As ActionResult(Of CCPET) Implements ICCPETAdminService.ChangeStatusCCPET
        CCPET.Status = status
        CCPET.MarkAsModified()
        Return SaveCCPET(CCPET, audit)
    End Function

    ''' <summary>
    ''' Elimina un rubro
    ''' </summary>
    ''' <param name="CCPET">La entidad</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">company Vacio</exception>
    Function DeleteCCPET(CCPET As CCPET, audit As AuditMessage) As ActionResult Implements ICCPETAdminService.DeleteCCPET
        If CCPET Is Nothing Then
            Throw New ArgumentNullException("CCPET")
        End If
        Dim unitOfWork As IUnitWork = Me._CCPETRepository.UnitWork
        Try
            Dim auditProcess As IndigoAuditSimpleEntity(Of CCPET)
            auditProcess = New IndigoAuditSimpleEntity(Of CCPET)(CCPET, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            Me._CCPETRepository.DeleteEntity(CCPET)
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
            Me._CCPETRepository = Nothing
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
