

#Region "Imports"
Imports Domain.Base
Imports Infrastructure.CrossCutting.Base
Imports Application.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Audit
Imports Domain.Common
Imports Domain.Entities
Imports System.Data.Entity.Core

#End Region

Public Class SupplierAdminService
    Implements Application.Common.ISupplierAdminService


    'Repositorio de tipo de fabricante
    Private _SupplierRepository As Domain.Entities.ISupplierRepository
    'Repositorio de tipo de fabricante
    Private _SupplierCommitRepository As Domain.Entities.ISupplierRepository
    'Repositorio de tipo de fabricante
    Private _personRepository As IPersonRepository
    ''' <summary>
    ''' Repositorio de secuencias numericas
    ''' </summary>
    Private _secuenseDRepository As Domain.Entities.IMaintenanceSequenceDetailRepository

    ''' <summary>
    ''' inicia el repositorio de fabricante
    ''' </summary>
    ''' <param name="SupplierRepository">Repositorio de fabricante</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal SupplierRepository As Domain.Entities.ISupplierRepository, ByVal secuenseDRepository As Domain.Entities.IMaintenanceSequenceDetailRepository, supplierCommitRepository As Domain.Entities.ISupplierRepository)
        If (SupplierRepository Is Nothing) Then
            Throw New ArgumentNullException("Repositorio de fabricante")
        End If
        _SupplierRepository = SupplierRepository
        _secuenseDRepository = secuenseDRepository
        _SupplierCommitRepository = supplierCommitRepository
    End Sub

    Public Function DeleteSupplier(Supplier As Domain.Entities.Supplier, audit As AuditMessage) As ActionResult Implements ISupplierAdminService.DeleteSupplier
        If Supplier Is Nothing Then
            Throw New ArgumentNullException("Supplier")
        End If
        Dim unitOfWork As IUnitWork = Me._SupplierRepository.UnitWork
        Try
            If Supplier.ChangeTracker.State = Domain.Base.Entities.ObjectState.Deleted Then
                Me._SupplierRepository.DeleteEntity(Supplier)
                unitOfWork.Commit()
                'Auditoria básica
                IndigoAuditBasic.Execute(GetType(Domain.Entities.Supplier).Name, audit.Functional, Supplier.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, Infrastructure.CrossCutting.Base.ActionsAudit.Eliminar, audit.Company, audit.ContainerSecurity)
                'Ausitoria avanzada
                Dim auditObject As New IndigoAuditSimpleEntity(Of Domain.Entities.Supplier)(Supplier, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
                auditObject.Execute()
                Return New ActionResult With {.StateResult = True}
            Else
                Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"})}
            End If
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-999"})}
        Catch ex As System.Data.Entity.Infrastructure.DbUpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"})}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False}
        End Try
    End Function

    Public Function GetSupplierByIdThirdParty(Id As Integer) As Domain.Entities.Supplier Implements ISupplierAdminService.GetSupplierByIdThirdParty
        If Id = 0 Then
            Throw New ArgumentNullException("IdThird")
        End If
        Try

            Return _SupplierRepository.GetSupplierByIdThirdParty(Id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "ApplicationPolicy")
            Return New Domain.Entities.Supplier()
        End Try
    End Function

    

    ''' <summary>
    ''' Obtiene el id del proveedor por el id de la linea de distribucción
    ''' </summary>
    ''' <param name="IdDistributionLines"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSupplierIdByIdDistributionLines(IdDistributionLines As Integer) As Integer Implements ISupplierAdminService.GetSupplierIdByIdDistributionLines
        If IdDistributionLines = 0 Then
            Throw New ArgumentNullException("IdDistributionLines")
        End If
        Try

            Return _SupplierRepository.GetSupplierIdByIdDistributionLines(IdDistributionLines)
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "ApplicationPolicy")
            Return 0
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un proveedor por id del tercero e id de la cuenta contable
    ''' </summary>
    ''' <param name="IdThird">The identifier third.</param>
    Public Function GetSupplierByIdThirdPartyAndIdAccountAccounting(IdThird As Integer, IdAccountAccounting As Integer) As Domain.Entities.Supplier Implements ISupplierAdminService.GetSupplierByIdThirdPartyAndIdAccountAccounting
        If IdThird = 0 Then
            Throw New ArgumentNullException("IdThird")
        End If
        If IdAccountAccounting = 0 Then
            Throw New ArgumentNullException("IdAccountAccounting")
        End If
        Try

            Return _SupplierRepository.GetSupplierByIdThirdPartyAndIdAccountAccounting(IdThird, IdAccountAccounting)
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "ApplicationPolicy")
            Return New Domain.Entities.Supplier()
        End Try
    End Function

    Public Function GetSupplier(codeSupplier As String) As Domain.Entities.Supplier Implements ISupplierAdminService.GetSupplier
        If String.IsNullOrEmpty(codeSupplier) Then
            Throw New ArgumentNullException("Codigo de tipo de inventario vacio")
        End If
        Try

            Return _SupplierRepository.GetSupplier(codeSupplier)
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "ApplicationPolicy")
            Return New Domain.Entities.Supplier()
        End Try
    End Function

    Public Function ListAllSupplier() As List(Of Domain.Entities.Supplier) Implements ISupplierAdminService.ListAllSupplier
        Try
            Return _SupplierRepository.ListAllSupplier
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function SaveSupplier(Supplier As Domain.Entities.Supplier, audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of Domain.Entities.Supplier) Implements ISupplierAdminService.SaveSupplier
        If Supplier Is Nothing Then
            Throw New ArgumentNullException("Supplier")
        End If
        Dim unitOfWork As IUnitWork = Me._SupplierCommitRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        Try
            Dim seq As MaintenanceSequenceDetail = Nothing
            Dim auxSupplier = Nothing
            If Supplier.Code IsNot String.Empty Then
                auxSupplier = _SupplierRepository.GetSupplier(Supplier.Code, False)
            End If
            If Supplier.Code Is Nothing OrElse Supplier.Code.Trim().Equals(String.Empty) Then
                seq = Me._secuenseDRepository.GetSequenseDById(idSequense)
                If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.MaintenanceSequence.Sequential Then
                    Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                    If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                        Supplier.Code = res
                        seq.Next += 1
                        Me._secuenseDRepository.SaveEntity(seq)
                    Else
                        Return New ActionResult(Of Domain.Entities.Supplier) With {.StateResult = False, .MessageResult = {"_Seq02_"}.ToList()}
                    End If
                Else
                    Return New ActionResult(Of Domain.Entities.Supplier) With {.StateResult = False, .MessageResult = {"_Seq01_"}.ToList()}
                End If
            End If

            If Supplier.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added OrElse Supplier.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                Me._SupplierCommitRepository.SaveEntity(Supplier)
            End If
            unitOfWork.Commit()
            sequenseUnitOfWork.Commit()
            If Supplier.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                '/***** Auditoria Basica ********/
                IndigoAuditBasic.Execute(GetType(Domain.Entities.Supplier).Name, audit.Functional, Supplier.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Crear, audit.Company, audit.ContainerSecurity)
                '/***** Auditoria Avanzada ******/
                Dim auditObject As New IndigoAuditSimpleEntity(Of Domain.Entities.Supplier)(Supplier, audit, Infrastructure.CrossCutting.Audit.Actions.Insert)
                auditObject.Execute()
            ElseIf Supplier.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                '/***** Auditoria Basica ********/
                IndigoAuditBasic.Execute(GetType(Domain.Entities.Supplier).Name, audit.Functional, Supplier.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Modificar, audit.Company, audit.ContainerSecurity)
                '/***** Auditoria Avanzada ******/
                Dim auditObject As New IndigoAuditSimpleEntity(Of Domain.Entities.Supplier)(Supplier, audit, Infrastructure.CrossCutting.Audit.Actions.Update, auxSupplier)
                auditObject.Execute()
            End If

            'Se marca la entidad como sin cambios
            Supplier.MarkAsUnchanged()

            Return New ActionResult(Of Domain.Entities.Supplier) With {.StateResult = True, .ObjectEmbbeded = Supplier}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of Domain.Entities.Supplier) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of Domain.Entities.Supplier) With {.StateResult = False}
        End Try
    End Function

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ChangeState(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of Domain.Entities.Supplier) Implements ISupplierAdminService.ChangeState
        Dim supplier As Domain.Entities.Supplier = _SupplierRepository.GetSupplier(code)
        supplier.Status = state
        supplier.MarkAsModified()
        Return SaveSupplier(supplier, audit)
    End Function

    Public Function GetThirdPartyById(id As Integer) As Domain.Entities.ThirdParty Implements ISupplierAdminService.GetThirdPartyById
        If id = 0 Then
            Throw New ArgumentNullException("Id de tipo de inventario vacio")
        End If
        Try

            Return _SupplierRepository.GetThirdPartyById(id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "ApplicationPolicy")
            Return New Domain.Entities.ThirdParty()
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un proveedor por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSupplierById(id As Integer) As Domain.Entities.Supplier Implements ISupplierAdminService.GetSupplierById
        If id = 0 Then
            Throw New ArgumentNullException("id de proveedor vacio")
        End If
        Try
            Return _SupplierRepository.GetSupplierById(id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "ApplicationPolicy")
            Return New Domain.Entities.Supplier()
        End Try
    End Function

    Public Function GetThirdPartyByIdSupplier(Id As Integer) As Domain.Entities.ThirdParty Implements ISupplierAdminService.GetThirdPartyByIdSupplier
        If Id = 0 Then
            Throw New ArgumentNullException("id de proveedor vacio")
        End If
        Try
            Return _SupplierRepository.GetThirdPartyByIdSupplier(Id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "ApplicationPolicy")
            Return New Domain.Entities.ThirdParty
        End Try
    End Function

    Public Function GetIVARetentionPercentageBySupplierId(SupplierId As Integer) As Decimal Implements ISupplierAdminService.GetIVARetentionPercentageBySupplierId
        If SupplierId = 0 Then
            Throw New ArgumentNullException("id de proveedor vacio")
        End If
        Try
            Return _SupplierRepository.GetIVARetentionPercentageBySupplierId(SupplierId)
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "ApplicationPolicy")
            Return 0
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
            _secuenseDRepository = Nothing
            _SupplierCommitRepository = Nothing
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
