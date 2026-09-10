'***********************************************************************
' Assembly         : Application.Glosas
' Author           : JulianCardozo
' Created          : 11-03-2011
'
' Last Modified By : RafaelPatiño
' Last Modified On : 2013-04-18
'
' Last Modified By : Juan Diego Diaz
' Last Modified On : 2013-04-25
' Description      : Se agregó la auditoria
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.Transactions
Imports Domain.Base
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Entities
Imports Application.Base
Imports System.Data.Entity.Core
Imports System.Data.Entity.Infrastructure
Imports Infrastructure.CrossCutting.Queue


#End Region
Public Class CustomerAdminService
    Implements ICustomerAdminService

    Private _CustomerRepository As ICustomerRepository

    ''' <summary>
    ''' Fabrica de Indiigo Queue
    ''' </summary>
    Private _factoryQueue As IFactoryQueue

    ''' <summary>
    ''' Initializa una nueva instancia de la clase <see cref="CustomerAdminService" />.
    ''' </summary>
    ''' <param name="CustomerRepository">el repositorio para el manejo de los Customered.</param>
    Public Sub New(ByVal CustomerRepository As ICustomerRepository, FactoryQueue As IFactoryQueue)
        If CustomerRepository Is Nothing Then
            Throw New ArgumentNullException("CustomerRepository Vacio")
        End If

        _CustomerRepository = CustomerRepository
        _factoryQueue = FactoryQueue
    End Sub
    ''' <summary>
    ''' Elimina un Customer
    ''' </summary>
    ''' <param name="Customer">el Grupo</param>
    ''' <returns></returns>
    Public Function DeleteCustomer(Customer As Customer, audit As AuditMessage) As ActionResult Implements ICustomerAdminService.DeleteCustomer
        If Customer Is Nothing Then
            Throw New ArgumentNullException("Customer Vacio")
        End If
        Dim unitOfWork As IUnitWork = _CustomerRepository.UnitWork
        Try
            Dim retentionDetail As List(Of CustomerRetention)
            If Customer.CustomerRetention IsNot Nothing OrElse Customer.CustomerRetention.Count > 0 Then
                retentionDetail = (From e In Customer.CustomerRetention Select e).ToList()
                For Each item In retentionDetail
                    item.MarkAsDeleted()
                Next

                For Each item In retentionDetail
                    Customer.CustomerRetention.Remove(item)
                Next
            End If
            Customer.MarkAsDeleted()

            'Elimino el Customer de detalle 
            _CustomerRepository.SaveEntity(Customer)
            unitOfWork.Commit()

            TriggerEvent(Customer, audit)
            '/***** Auditoria Basica ********/
            IndigoAuditBasic.Execute("Customer", audit.Functional, Customer.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Eliminar, audit.Company, audit.ContainerSecurity)
            Dim auditObject As New IndigoAuditSimpleEntity(Of Customer)(Customer, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            auditObject.Execute()
            Return New ActionResult With {.StateResult = True}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-999"})}
        Catch ex As DbUpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"})}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False}
        End Try
    End Function
    ''' <summary>
    ''' consulta un Customer especifico
    ''' </summary>
    ''' <param name="codeCustomer">el codigo del Customer</param>
    ''' <returns></returns>
    Public Function GetCustomer(codeCustomer As String, audit As AuditMessage) As Customer Implements ICustomerAdminService.GetCustomer
        If String.IsNullOrEmpty(codeCustomer) = True Then
            Throw New ArgumentNullException("CodeCustomer Vacio")
        End If
        Try
            Dim Customer = _CustomerRepository.GetCustomer(codeCustomer)
            If Customer.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of Customer)(Customer, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return Customer
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' Lists the Customer all.
    ''' </summary>
    ''' <returns></returns>
    Public Function ListCustomeredConceptAll(audit As AuditMessage) As List(Of Customer) Implements ICustomerAdminService.ListCustomerAll
        Try
            Dim Customers = _CustomerRepository.ListCustomertAll
            If Customers.Count > 0 Then
                For Each item As Customer In Customers
                    Dim auditObject As New IndigoAuditSimpleEntity(Of Customer)(item, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                    auditObject.Execute()
                Next
            End If
            Return Customers
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' graba un Customer
    ''' </summary>
    ''' <param name="Customer">el Customer</param>
    ''' <returns></returns>
    Public Function SaveCustomer(Customer As Customer, audit As AuditMessage) As ActionResult(Of Customer) Implements ICustomerAdminService.SaveCustomer
        If Customer Is Nothing Then
            Throw New ArgumentNullException("Customer Vacio")
        End If
        Dim unitOfWork As IUnitWork = _CustomerRepository.UnitWork
        Try
            Dim AuxCustomer As Customer = Nothing
            If Customer.ChangeTracker.State = ObjectState.Added Then
                Customer.CreationUser = audit.CodeUser
                Customer.CreationDate = DateTime.Now
            Else
                Customer.ModificationUser = audit.CodeUser
                Customer.ModificationDate = DateTime.Now
                AuxCustomer = Customer.OriginalValue
            End If
            If GetCustomerAndThirdpartyById(Customer.ThirdPartyId, If(Customer Is Nothing OrElse Customer.Id = 0, 0, Customer.Id)) Then
                Return New ActionResult(Of Customer) With {.StateResult = False, .Message = "Ya existe un cliente asociado al mismo tercero"}
            End If
            _CustomerRepository.SaveEntity(Customer)
            'confirmo la unidad de trabajo
            unitOfWork.Commit()
            TriggerEvent(Customer, audit)
            If (Customer.ChangeTracker.State = ObjectState.Added) Then
                '/***** Auditoria Basica ********/
                IndigoAuditBasic.Execute("Customer", audit.Functional, Customer.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Crear, audit.Company, audit.ContainerSecurity)
                '/***** Auditoria Avanzada ******/
                Dim auditObject As New IndigoAuditSimpleEntity(Of Customer)(Customer, audit, Infrastructure.CrossCutting.Audit.Actions.Insert)
                auditObject.Execute()
            ElseIf Customer.ChangeTracker.State = ObjectState.Modified Then
                '/***** Auditoria Basica ********/
                IndigoAuditBasic.Execute("Customer", audit.Functional, Customer.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Modificar, audit.Company, audit.ContainerSecurity)
                '/***** Auditoria Avanzada ******/
                Dim auditObject As New IndigoAuditSimpleEntity(Of Customer)(Customer, audit, Infrastructure.CrossCutting.Audit.Actions.Update, AuxCustomer)
                auditObject.Execute()
            End If

            Return New ActionResult(Of Customer) With {.StateResult = True, .ObjectEmbbeded = Customer}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of Customer) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of Customer) With {.StateResult = False}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene cliente sin tener encuenta el estado
    ''' </summary>
    ''' <param name="codeCustomer"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCustomerWithouState(codeCustomer As String, audit As AuditMessage) As Customer Implements ICustomerAdminService.GetCustomerWithouState
        If String.IsNullOrEmpty(codeCustomer) = True Then
            Throw New ArgumentNullException("CodeCustomer Vacio")
        End If
        Try
            Dim Customer = _CustomerRepository.GetCustomerWithouState(codeCustomer)
            If Customer.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of Customer)(Customer, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return Customer
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function GetCustomerByNit(Nit As String) As Customer Implements ICustomerAdminService.GetCustomerByNit
        If String.IsNullOrEmpty(Nit) Then
            Throw New ArgumentNullException("nit de cliente vacio")
        End If
        Try
            Dim customer As Customer = _CustomerRepository.GetCustomerByNit(Nit)
            Return customer
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function GetCustomerById(Id As String) As Customer Implements ICustomerAdminService.GetCustomerById
        If String.IsNullOrEmpty(Id) Then
            Throw New ArgumentNullException("nit de cliente vacio")
        End If
        Try
            Dim customer As Customer = _CustomerRepository.GetCustomerById(Id)
            Return customer
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Verificar si existe mas de un tercero como cliente
    ''' </summary>
    ''' <param name="IdThirdParty"></param>
    ''' <returns></returns>
    Public Function GetCustomerAndThirdpartyById(IdThirdParty As String, Optional CustomerId As Integer = 0) As Boolean
        If String.IsNullOrEmpty(IdThirdParty) Then
            Throw New ArgumentNullException("Tercero vacio")
        End If
        Try
            Dim customer As List(Of Customer) = _CustomerRepository.GetCustomerAndThirdpartyById(IdThirdParty, CustomerId)
            If customer.Count >= 1 Then
                Return True
            End If
            Return False
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

#Region "Events"

    Public Sub TriggerEvent(Customer As Customer, audit As AuditMessage)
        Dim wrapperEvent As New Events.Serializers.Wrapper
        Dim ChangeTracker As String = Customer.ChangeTracker.State.ToString().ToLower()

        If {ObjectState.Unchanged, ObjectState.Modified}.Contains(Customer.ChangeTracker.State) Then
            ChangeTracker = "modified"

        ElseIf Customer.ChangeTracker.State = ObjectState.Added Then
            ChangeTracker = "added"
        End If

        Dim eventData As EventData = wrapperEvent.GenerateWrapperEventData(Customer, audit.CodeUser, ChangeTracker, DittoSourceType.customer)
        Dim Queue As IIndigoQueue = _factoryQueue.CreateQueue()
        Queue.Publish(eventData)
    End Sub

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _CustomerRepository = Nothing
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
