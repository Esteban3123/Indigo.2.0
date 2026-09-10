'***********************************************************************
' Assembly         : Infrestructure.Data.GlosasRepository
' Author           : Jlian Cardozo
' Created          : 06-04-2013
'
' Last Modified By : 
' Last Modified On : 
'
' Copyright        : (c) . All rights reserved.
'*********************************************************************** 

Imports Domain.Entities
Imports Domain.Base
Public Interface ICustomerRepository


    Inherits IRepository(Of Customer)

    ''' <summary>
    ''' Lists the Customer all.
    ''' </summary>
    ''' <returns></returns>
    Function ListCustomertAll() As List(Of Customer)
    ''' <summary>
    ''' consulta un Customer especifico
    ''' </summary>
    ''' <param name="codeCustomer">el codigo del Customer</param>
    ''' <returns></returns>
    Function GetCustomer(ByVal codeCustomer As String) As Customer

    ''' <summary>
    ''' obtiene un tercero sin tener encuenta el estado
    ''' </summary>
    ''' <param name="codeCustomer">codigo tercero y/o cliente</param>
    ''' <returns>un tercero</returns>
    Function GetCustomerWithouState(codeCustomer As String) As Customer

    ''' <summary>
    ''' Obtiene un cliente por id del tercero
    ''' </summary>
    Function GetCustomerByThirdPartyId(ThirdPartyId As Integer, Optional tracking As Boolean = True) As Customer

    ''' <summary>
    ''' Obtiene un cliente por nit
    ''' </summary>
    Function GetCustomerByNit(Nit As String) As Customer


    ''' <summary>
    ''' Obtiene un cliente por el Id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetCustomerById(Id As String) As Customer

    Function GetCustomerPOCO(codeCustomer As String) As Customer

    Function GetListCustomerPOCO(listCodeCustomer As List(Of String)) As List(Of Customer)

    ''' <summary>
    ''' Obtiene la asociacion de un cliente por id del tercero
    ''' </summary>
    ''' <param name="ThirdPartyId">Id del tercero</param> 
    ''' <returns></returns>
    Function GetCustomerAndThirdpartyById(ThirdPartyId As Integer, Optional CustomerId As Integer = Nothing) As List(Of Customer)
End Interface
