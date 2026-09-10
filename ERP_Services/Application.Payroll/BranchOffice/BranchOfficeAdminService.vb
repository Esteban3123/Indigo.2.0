Imports Domain.Payroll.Entities
Imports Domain.Payroll
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Data.Entity.Infrastructure

Public Class BranchOfficeAdminService
    Implements IBranchOfficeAdminService


    'Repositorio de Sucursal
    Private _BranchOfficeRepository As IBranchOfficeRepository

    ''' <summary>
    ''' inicia el repositorio de la sucursal
    ''' </summary>
    ''' <param name="BranchOfficeRepository">Repositorio de Sucursal</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal branchOfficeRepository As IBranchOfficeRepository)
        If (branchOfficeRepository Is Nothing) Then
            Throw New ArgumentNullException("BusinessRepository vacio")
        End If
        _BranchOfficeRepository = branchOfficeRepository
    End Sub

    ''' <summary>
    ''' Elimina una sucursal
    ''' </summary>
    ''' <param name="branchOffice">Sucursal</param>
    ''' <returns></returns>
    Public Function DeleteBranchOffice(branchOffice As BranchOffice, audit As Infrastructure.CrossCutting.Base.AuditMessage) As ActionMessageResult(Of BranchOffice) Implements IBranchOfficeAdminService.DeleteBranchOffice
        Dim result As New ActionMessageResult(Of BranchOffice)
        result.StateResult = True
        If branchOffice Is Nothing Then
            Throw New ArgumentNullException("Sucursal Vacia")
        End If
        Dim unitWork As IUnitWork = _BranchOfficeRepository.UnitWork
        Try
            _BranchOfficeRepository.DeleteEntity(branchOffice)
            unitWork.Commit()
            '/***** Auditoria Basica ********/
            IndigoAuditBasic.Execute("BranchOffice", audit.Functional, branchOffice.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Eliminar, audit.Company, audit.ContainerSecurity)
            '/*****Auditoria Avanzada ******/
            Dim auditObject As New IndigoAuditSimpleEntity(Of BranchOffice)(branchOffice, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            auditObject.Execute()
            'IndigoAuditSimpleEntity(Of BranchOffice).Execute(branchOffice, audit, Infrastructure.CrossCutting.Audit.Actions.Delete, branchOffice)
            Return result
        Catch ex As DbUpdateException
            unitWork.RollbackChanges()
            result.StateResult = False
            result.MessageResult.Add(New MessageResult("c-0000", branchOffice.Code))
            Return result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            result.StateResult = False
            Return result
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una Sucursal
    ''' </summary>
    ''' <param name="code">Código de la sucursal</param>
    ''' <returns> Sucursal</returns>
    Public Function GetBranchOffice(code As String) As BranchOffice Implements IBranchOfficeAdminService.GetBranchOffice
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("Codigo Sucursal Vacio")
        End If
        Try
            Return _BranchOfficeRepository.GetBranchOffice(code)

        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Lista de sucursales
    ''' </summary>
    ''' <returns>Lista de sucursales</returns>
    Public Function ListAllBranchOffice() As List(Of BranchOffice) Implements IBranchOfficeAdminService.ListAllBranchOffice
        Try
            Return _BranchOfficeRepository.ListAllBranchOffice()

        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Guarda una sucursal
    ''' </summary>
    ''' <param name="branchOffice">Sucursal</param>
    ''' <returns></returns>
    Public Function SaveBranchOffice(branchOffice As BranchOffice, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Boolean Implements IBranchOfficeAdminService.SaveBranchOffice
        If branchOffice Is Nothing Then
            Throw New ArgumentNullException("Sucursal Vacia")
        End If
        Dim unitWork As IUnitWork = _BranchOfficeRepository.UnitWork
        Try

            Dim auditProcess As IndigoAuditSimpleEntity(Of BranchOffice)
            Dim AuxBranchOffice As BranchOffice = Nothing
            Dim status As Integer

            If branchOffice.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                branchOffice.ModificationUser = audit.CodeUser
                branchOffice.ModificationDate = Date.Now()
                status = Infrastructure.CrossCutting.Audit.Actions.Update
                AuxBranchOffice = _BranchOfficeRepository.GetBranchOffice(branchOffice.Code, False)
            Else
                branchOffice.CreationUser = audit.CodeUser
                branchOffice.CreationDate = Date.Now()
                branchOffice.State = 1
                status = Infrastructure.CrossCutting.Audit.Actions.Insert
            End If

            'Valido si se va a guardar o a eliminar
            _BranchOfficeRepository.SaveEntity(branchOffice)
            unitWork.Commit()
            auditProcess = New IndigoAuditSimpleEntity(Of BranchOffice)(branchOffice, audit, status, AuxBranchOffice)
            auditProcess.Execute()
            Return True
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un listado de sucursales filtrado por el id de la empresa
    ''' </summary>
    ''' <param name="CompanyId">id de la empresa</param>
    ''' <returns>listado de sucursales</returns>
    ''' <remarks></remarks>
    Public Function GetBranchOfficeByCompanyId(CompanyId As Integer) As List(Of BranchOffice) Implements IBranchOfficeAdminService.GetBranchOfficeByCompanyId
        If CompanyId < 0 Then
            Throw New ArgumentNullException("Id Empresa Vacio")
        End If
        Try
            Return _BranchOfficeRepository.GetBranchOfficeByCompanyId(CompanyId)

        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _BranchOfficeRepository = Nothing
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
