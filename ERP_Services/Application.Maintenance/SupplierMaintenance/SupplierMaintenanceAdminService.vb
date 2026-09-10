#Region "Imports"
Imports Domain.Maintenance
Imports Domain.Base
Imports Infrastructure.CrossCutting.Base
Imports Application.Base
Imports Domain.Maintenance.Entities
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Audit
Imports Domain.Common
Imports Domain.Entities
Imports Application.Common
Imports System.Transactions
Imports Infrastructure.CrossCutting.Resources

#End Region


Public Class SupplierMaintenanceAdminService

    Implements ISupplierMaintenanceAdminService

    'Repositorio de tipo de fabricante
    Private _SupplierRepository As Domain.Maintenance.ISupplierMaintenanceRepository
    'Repositorio de tipo de fabricante
    Private _SupplierCommitRepository As Domain.Maintenance.ISupplierMaintenanceRepository

    Private _AdressMaintenaceRepository As Domain.Maintenance.IAddressRepositoryMaintenance

    Private _EmailMaintenanceRepository As Domain.Maintenance.IEmailMaintenanceRepository

    Private _PhoneMaintenanceRepository As Domain.Maintenance.IPhoneMaintenanceRepository

    ''' <summary>
    ''' inicia el repositorio de fabricante
    ''' </summary>
    ''' <param name="SupplierRepository">Repositorio de fabricante</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal SupplierRepository As Domain.Maintenance.ISupplierMaintenanceRepository, supplierCommitRepository As Domain.Maintenance.ISupplierMaintenanceRepository, AddressMaintenanceRepository As Domain.Maintenance.IAddressRepositoryMaintenance, EmailMaintenanceRepository As Domain.Maintenance.IEmailMaintenanceRepository, PhoneMaintenanceRepository As Domain.Maintenance.IPhoneMaintenanceRepository)
        If (SupplierRepository Is Nothing) Then
            Throw New ArgumentNullException("Repositorio de fabricante")
        End If
        _SupplierRepository = SupplierRepository
        _SupplierCommitRepository = supplierCommitRepository
        _AdressMaintenaceRepository = AddressMaintenanceRepository
        _EmailMaintenanceRepository = EmailMaintenanceRepository
        _PhoneMaintenanceRepository = PhoneMaintenanceRepository
    End Sub

    Public Function ChangeState(code As String, state As Boolean, session As SessionValues) As ActionResult(Of SupplierMaintenance) Implements ISupplierMaintenanceAdminService.ChangeState
        'Dim supplier As Domain.Maintenance.Entities.SupplierMaintenance = _SupplierRepository.GetSupplier(code)
        'supplier.Status = state
        'supplier.MarkAsModified()
        'Return SaveSupplierMaintenance(supplier, session)


        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If String.IsNullOrEmpty(state) Then
            Throw New ArgumentNullException("state")
        End If
        If session Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim supplier As Domain.Maintenance.Entities.SupplierMaintenance = _SupplierRepository.GetSupplier(code)
            If supplier IsNot Nothing AndAlso supplier.Id > 0 Then
                supplier.Status = state
                supplier.MarkAsModified()
            End If
            Dim result = Me.SaveSupplierMaintenance(supplier, session)
            If result.StatusCode = eStatusResult.SUCCESS Then
                result.Message = ResourceManager.GetString("UpdateState")
            End If
            Return result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of Domain.Maintenance.Entities.SupplierMaintenance) With {.StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Elimina un Proveedor 
    ''' </summary>
    ''' <param name="Supplier">Supplier</param>
    ''' <param name="audit">audit</param>
    ''' <returns>ActionResult</returns>
    ''' <remarks></remarks>
    Public Function DeleteSupplierMaintenance(Supplier As SupplierMaintenance, session As SessionValues) As ActionResult Implements ISupplierMaintenanceAdminService.DeleteSupplierMaintenance
        If Supplier Is Nothing Then
            Throw New ArgumentNullException("Supplier")
        End If
        Dim unitOfWork As IUnitWork = Me._SupplierRepository.UnitWork
        Dim AddressUnifOfWork As IUnitWork = Me._AdressMaintenaceRepository.UnitWork
        Dim EmailUnitOfWork As IUnitWork = Me._EmailMaintenanceRepository.UnitWork
        Dim PhoneUnitOfWork As IUnitWork = Me._PhoneMaintenanceRepository.UnitWork
        Using Transaction As New TransactionScope
            Try


                While Supplier.AddressMaintenance.Count > 0 'Elimino las Direcciones
                    Dim index = Supplier.AddressMaintenance.Count - 1
                    'Supplier.AddressMaintenance(index).MarkAsDeleted()
                    _AdressMaintenaceRepository.DeleteEntity(Supplier.AddressMaintenance(index))
                End While
                AddressUnifOfWork.Commit()

                While Supplier.EmailMaintenance.Count > 0 'Elimino los Correos
                    Dim index = Supplier.EmailMaintenance.Count - 1
                    _EmailMaintenanceRepository.DeleteEntity(Supplier.EmailMaintenance(index))
                End While
                EmailUnitOfWork.Commit()

                While Supplier.PhoneMaintenance.Count > 0 'Elimino los Teléfonos
                    Dim index = Supplier.PhoneMaintenance.Count - 1
                    _PhoneMaintenanceRepository.DeleteEntity(Supplier.PhoneMaintenance(index))
                End While
                PhoneUnitOfWork.Commit()

                _SupplierRepository.DeleteEntity(Supplier)
                unitOfWork.Commit()

                '/***** Auditoria Basica ********/
                IndigoAuditBasic.Execute("SupplierMaintenance", session.AuditMessageWcf.Functional, Supplier.Id, session.AuditMessageWcf.NameUser, session.AuditMessageWcf.CodeUser, session.AuditMessageWcf.WindowsUser, DateTime.Now, ActionsAudit.Eliminar, session.AuditMessageWcf.Company, session.AuditMessageWcf.ContainerSecurity)
                '/*****Auditoria Avanzada ******/
                Dim auditObject As New IndigoAuditSimpleEntity(Of SupplierMaintenance)(Supplier, session.AuditMessageWcf, Infrastructure.CrossCutting.Audit.Actions.Delete)
                auditObject.Execute()
                'Catch exDelete As UpdateException
                '    result.StateResult = False
                '    result.MessageResult.Add(New MessageResult("c-001", costCenter.Code))
                '    Return result

                Transaction.Complete()
                Return New ActionResult With {.StateResult = True}
            Catch ex As OptimisticConcurrencyException
                AddressUnifOfWork.RollbackChanges()
                PhoneUnitOfWork.RollbackChanges()
                EmailUnitOfWork.RollbackChanges()
                unitOfWork.RollbackChanges()
                Transaction.Dispose()
                Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-999"})}
            Catch ex As System.Data.Entity.Infrastructure.DbUpdateException
                unitOfWork.RollbackChanges()
                AddressUnifOfWork.RollbackChanges()
                PhoneUnitOfWork.RollbackChanges()
                EmailUnitOfWork.RollbackChanges()
                Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"})}
            Catch ex As Exception
                unitOfWork.RollbackChanges()
                AddressUnifOfWork.RollbackChanges()
                PhoneUnitOfWork.RollbackChanges()
                EmailUnitOfWork.RollbackChanges()
                Transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult With {.StateResult = False}
            End Try
        End Using
    End Function

    ''' <summary>
    ''' Obtiene el Proveedor por Nit
    ''' </summary>
    ''' <param name="Nit">Nit</param>
    ''' <returns>SupplierMaintenance</returns>
    ''' <remarks></remarks>
    Public Function GetSupplierMaintenance(Nit As String) As SupplierMaintenance Implements ISupplierMaintenanceAdminService.GetSupplierMaintenance
        If String.IsNullOrEmpty(Nit) Then
            Throw New ArgumentNullException("Nit de Proveedores vacio")
        End If
        Try
            Return _SupplierRepository.GetSupplier(Nit)
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "ApplicationPolicy")
            Return New Domain.Maintenance.Entities.SupplierMaintenance()
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un Proveedor por ID
    ''' </summary>
    ''' <param name="id">ID</param>
    ''' <returns>SupplierMaintenance</returns>
    ''' <remarks></remarks>
    Public Function GetSupplierMaintenanceById(id As Integer) As SupplierMaintenance Implements ISupplierMaintenanceAdminService.GetSupplierMaintenanceById
        If id = 0 Then
            Throw New ArgumentNullException("id de proveedor vacio")
        End If
        Try
            Return _SupplierRepository.GetSupplierById(id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "ApplicationPolicy")
            Return New Domain.Maintenance.Entities.SupplierMaintenance()
        End Try
    End Function


    ''' <summary>
    ''' Lista todos los Proveedores (Mantenimiento)
    ''' </summary>
    ''' <returns>List(Of SupplierMaintenance)</returns>
    ''' <remarks></remarks>
    Public Function ListAllSupplierMaintenance() As List(Of SupplierMaintenance) Implements ISupplierMaintenanceAdminService.ListAllSupplierMaintenance
        Try
            Return _SupplierRepository.ListAllSupplier
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Almacena un Proveedor (Mantenimiento)
    ''' </summary>
    ''' <param name="Supplier">Supplier</param>
    ''' <param name="audit">audit</param>
    ''' <returns>ActionResult(Of SupplierMaintenance)</returns>
    ''' <remarks></remarks>
    Public Function SaveSupplierMaintenance(Supplier As SupplierMaintenance, session As SessionValues) As ActionResult(Of SupplierMaintenance) Implements ISupplierMaintenanceAdminService.SaveSupplierMaintenance
        If Supplier Is Nothing Then
            Throw New ArgumentNullException("Supplier vacio")
        End If

        Dim unitWork As IUnitWork = _SupplierCommitRepository.UnitWork
        Try

            Dim auxObjEntity As SupplierMaintenance = Nothing
            Dim auditProcess As IndigoAuditSimpleEntity(Of SupplierMaintenance)
            Dim status As Integer

            If Supplier.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                Supplier.CreationDate = Date.Now
                Supplier.CreationUser = session.UserIndigo
                status = Infrastructure.CrossCutting.Audit.Actions.Insert
            Else
                auxObjEntity = _SupplierRepository.GetSupplier(Supplier.Nit, False)
                Supplier.ModificationDate = Date.Now
                Supplier.ModificationUser = session.UserIndigo
                status = Infrastructure.CrossCutting.Audit.Actions.Update
            End If

            _SupplierCommitRepository.SaveEntity(Supplier)
            unitWork.Commit()

            'If Supplier.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
            '    Dim auditObject As New IndigoAuditSimpleEntity(Of SupplierMaintenance)(Supplier, session.AuditMessageWcf, Infrastructure.CrossCutting.Audit.Actions.Insert)
            '    auditObject.Execute()
            'ElseIf Supplier.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
            '    Dim auditObject As New IndigoAuditSimpleEntity(Of SupplierMaintenance)(Supplier, session.AuditMessageWcf, Infrastructure.CrossCutting.Audit.Actions.Update, Supplier)
            '    auditObject.Execute()
            'End If
            auditProcess = New IndigoAuditSimpleEntity(Of SupplierMaintenance)(Supplier, session.AuditMessageWcf, status, auxObjEntity)
            auditProcess.Execute()

            Supplier.MarkAsUnchanged()
            Return New ActionResult(Of Domain.Maintenance.Entities.SupplierMaintenance) With {.StateResult = True, .ObjectEmbbeded = Supplier}
        Catch ex As Exception
            unitWork.RollbackChanges()
            IndigoManagementExceptions.HandleExceptionUI(ex, "UIPolicy")
            Return New ActionResult(Of Domain.Maintenance.Entities.SupplierMaintenance) With {.StateResult = False}
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _SupplierRepository = Nothing
            _SupplierCommitRepository = Nothing
            _AdressMaintenaceRepository = Nothing
            _EmailMaintenanceRepository = Nothing
            _PhoneMaintenanceRepository = Nothing
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
