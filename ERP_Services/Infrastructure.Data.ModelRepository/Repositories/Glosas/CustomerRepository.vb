'***********************************************************************
' Assembly         : Infrestructure.Data.GlosasRepository
' Author           : Juliancardozo
' Created          : 06-04-2013
'
' Last Modified By : RafaelPatiño
' Last Modified On : 22-04-2013
'
' Copyright        : (c) . All rights reserved.
'*********************************************************************** 

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class CustomerRepository

    Inherits GenericRepository(Of Customer)
    Implements ICustomerRepository

    'Devuelve el contexto en este repositorio 
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    '''inicializa la neva instancia d clase.
    ''' </summary>
    ''' <param name="contex">el contexto.</param>
    Public Sub New(ByVal contex As IGlobalModelUnitOfWork)
        MyBase.New(contex)
        _context = contex
    End Sub

    ''' <summary>
    ''' obtiene un tercero
    ''' </summary>
    ''' <param name="codeCustomer">codigo tercero y/o cliente</param>
    ''' <returns>un tercero</returns>
    Public Function GetCustomer(codeCustomer As String) As Customer Implements ICustomerRepository.GetCustomer
        Dim Customer = From e In _context.Customer.Include("ThirdParty")
                       Where e.Nit = codeCustomer And e.State = True
                       Select e
        If Customer.Count > 0 Then
            Dim CustomerData = Customer.SingleOrDefault
            CustomerData.OriginalValue = (From e In _context.Customer.AsNoTracking
                                          Where e.Nit = codeCustomer
                                          Select e).SingleOrDefault
            Return CustomerData
        End If
        Return New Customer
    End Function

    Public Function GetCustomerPOCO(codeCustomer As String) As Customer Implements ICustomerRepository.GetCustomerPOCO
        Return (From e In _context.Customer
                Where e.Nit = codeCustomer And e.State = True
                Select e).FirstOrDefault()
    End Function

    Public Function GetListCustomerPOCO(listCodeCustomer As List(Of String)) As List(Of Customer) Implements ICustomerRepository.GetListCustomerPOCO
        If listCodeCustomer Is Nothing OrElse listCodeCustomer.Count = 0 Then
            Return New List(Of Customer)
        End If
        Return (From e In _context.Customer.AsNoTracking() Where listCodeCustomer.Contains(e.Nit) And e.State = True Select e).ToList()
    End Function

    ''' <summary>
    ''' obtiene un tercero sin tener encuenta el estado
    ''' </summary>
    ''' <param name="codeCustomer">codigo tercero y/o cliente</param>
    ''' <returns>un tercero</returns>
    Public Function GetCustomerWithouState(codeCustomer As String) As Customer Implements ICustomerRepository.GetCustomerWithouState
        Dim Customer = From e In _context.Customer.Include("ThirdParty").Include("MainAccounts").Include("CustomerRetention")
                       Where e.Nit = codeCustomer
                       Select e
        If Customer.Count > 0 Then
            Dim CustomerData = Customer.SingleOrDefault

            If CustomerData.CustomerRetention IsNot Nothing AndAlso CustomerData.CustomerRetention.Any() Then
                For Each customerRetention In CustomerData.CustomerRetention
                    customerRetention.PortfolioNoteConceptCodeName = (From pnc In _context.PortfolioNoteConcept.AsNoTracking Where pnc.Id = customerRetention.PortfolioNoteConceptId Select pnc.Code + " - " + pnc.Name).FirstOrDefault
                    customerRetention.RetentionConceptCodeName = (From rc In _context.RetentionConcepts.AsNoTracking Where rc.Id = customerRetention.RetentionConceptId Select rc.Code + " - " + rc.Name).FirstOrDefault
                Next
            End If

            CustomerData.OriginalValue = (From e In _context.Customer.AsNoTracking Where e.Nit = codeCustomer Select e).FirstOrDefault
            Return CustomerData
        End If
        Return New Customer
    End Function


    ''' <summary>
    ''' lista de terceros
    ''' </summary>
    ''' <returns>lista de terceros</returns>
    Public Function ListCustomertAll() As List(Of Customer) Implements ICustomerRepository.ListCustomertAll
        Dim Busqueda = From e In _context.Customer.Include("ThirdParty")
                       Where e.State = True
                                      Select e

        Return Busqueda.ToList
    End Function

    ''' <summary>
    ''' Obtiene un cliente por id del tercero
    ''' </summary>
    ''' <param name="ThirdPartyId"></param>
    ''' <returns></returns>
    Public Function GetCustomerByThirdPartyId(ThirdPartyId As Integer, Optional tracking As Boolean = True) As Customer Implements ICustomerRepository.GetCustomerByThirdPartyId

        Dim query As Customer = Nothing
        If tracking Then
            query = (From c In _context.Customer.Include("ThirdParty") Where c.ThirdPartyId = ThirdPartyId Select c).FirstOrDefault()
        Else
            query = (From c In _context.Customer.AsNoTracking().Include("ThirdParty").AsNoTracking() Where c.ThirdPartyId = ThirdPartyId Select c).FirstOrDefault()
        End If

        If query IsNot Nothing AndAlso query.Id > 0 Then
            Return query
        Else
            Return New Customer()
        End If
    End Function

    ''' <summary>
    ''' Obtiene la asociacion de un cliente por id del tercero
    ''' </summary>
    ''' <param name="ThirdPartyId"></param>
    ''' <returns></returns>
    Public Function GetCustomerAndThirdpartyById(ThirdPartyId As Integer, Optional CustomerId As Integer = Nothing) As List(Of Customer) Implements ICustomerRepository.GetCustomerAndThirdpartyById
        Dim query As List(Of Customer) = Nothing
        If CustomerId = 0 Then 'Agregar
            query = (From c In _context.Customer.Include("ThirdParty") Where c.ThirdPartyId = ThirdPartyId Select c).ToList()
        Else
            query = (From c In _context.Customer.Include("ThirdParty") Where c.ThirdPartyId = ThirdPartyId And c.Id <> CustomerId Select c).ToList()
        End If

        If query IsNot Nothing AndAlso query.Count > 0 Then
            Return query
        Else
            Return New List(Of Customer)()
        End If
    End Function

    ''' <summary>
    ''' Obtiene un cliente por el nit
    ''' </summary>
    ''' <param name="Nit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCustomerByNit(Nit As String) As Customer Implements ICustomerRepository.GetCustomerByNit
        If Nit Is Nothing OrElse Nit.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("Nit")
        End If
        Dim query = (From c In _context.Customer
                     Where c.Nit = Nit
                     Select c).FirstOrDefault
        If query IsNot Nothing Then
            Return query
        Else
            Return New Customer()
        End If
    End Function

    ''' <summary>
    ''' Obtiene un cliente por el nit
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCustomerById(Id As String) As Customer Implements ICustomerRepository.GetCustomerById
        If Id Is Nothing OrElse Id.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("Id")
        End If
        Dim query = (From c In _context.Customer
                     Where c.Id = Id
                     Select c).FirstOrDefault
        If query IsNot Nothing Then
            Return query
        Else
            Return New Customer()
        End If
    End Function

End Class
