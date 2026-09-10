'************************************************************
' Assembly         : Infraestructure.Data.GlosasRepository
' Author           : Rafael Eduardo Patiño
' Created          : 12-09-2013
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Infrastructure.Data.Base
Imports Domain.Entities
#End Region

Public Class InterfacesParametersRepository
    Inherits GenericRepository(Of GlosasParametersInterface)
    Implements IInterfaceParametersRepository


    'Devuelve el contexto en este repositorio 
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    '''Inicializa la nueva instancia de clase.
    ''' </summary>
    ''' <param name="contex">El contexto.</param>
    Public Sub New(ByVal contex As IGlobalModelUnitOfWork)
        MyBase.New(contex)
        _context = contex
    End Sub

    ''' <summary>
    ''' consulta parametros por empresa
    ''' </summary>
    ''' <param name="ContainerName">Codigo de contenedor</param>
    ''' <returns>objecto tipo parametro</returns>
    Public Function GetInterfacesParameters(ByVal ContainerName As String) As GlosasParametersInterface Implements IInterfaceParametersRepository.GetInterfacesParameters
        Dim parametersInterface = From p In _context.GlosasParametersInterface
                Where p.ContainerName = ContainerName
                Select p
        If parametersInterface.Count > 0 Then
            Dim ParametersInterfaceData = parametersInterface.SingleOrDefault
            ParametersInterfaceData.OriginalValue = (From p In _context.GlosasParametersInterface.AsNoTracking.AsNoTracking
            Where p.ContainerName = ContainerName
            Select p).SingleOrDefault
            Return ParametersInterfaceData
        Else
            Return New GlosasParametersInterface
        End If
    End Function

    ''' <summary>
    ''' Carga parametros por el Id
    ''' </summary>
    ''' <param name="Id">Id de parameters Interface</param>
    ''' <returns>Objeto parametros de Interfaz</returns>
    ''' <remarks></remarks>
    Public Function GetInterfacesParametersById(ByVal Id As String) As GlosasParametersInterface Implements IInterfaceParametersRepository.GetInterfacesParametersById
        Dim parametersInterface = From p In _context.GlosasParametersInterface
                Where p.Id = Id And p.Interface = True
                Select p
        If parametersInterface.Count > 0 Then
            Return parametersInterface.SingleOrDefault
        Else
            Return New GlosasParametersInterface
        End If
    End Function

    ''' <summary>
    ''' Lista de Parametros de Interface
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListInterfacesParameters() As List(Of GlosasParametersInterface) Implements IInterfaceParametersRepository.ListInterfacesParameters
        Dim BusquedaInterface = (From c In _context.GlosasParametersInterface
                        Where c.Interface = True
                         Select c).ToList()
        For i As Integer = 0 To BusquedaInterface.Count() - 1
            BusquedaInterface(i).ParametersInterfaceCodeName = BusquedaInterface(i).ContainerName & " - " & BusquedaInterface(i).CompanyName
        Next
        Return BusquedaInterface.ToList()
    End Function


    ''' <summary>
    ''' Lista configuracion cuenta FOX metodo privado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function List_AccountSettingsFOX_PrivateMethod() As List(Of AccountSettingsFOX_PrivateMethod) Implements IInterfaceParametersRepository.List_AccountSettingsFOX_PrivateMethod
        Dim BusquedaInterface = (From c In _context.AccountSettingsFOX_PrivateMethod Select c)
        Return BusquedaInterface.ToList()
    End Function

    ''' <summary>
    ''' Lista configuracion cuenta FOX metodo publico
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function List_AccountSettingsFOX_PublicMethod() As List(Of AccountSettingsFOX_PublicMethod) Implements IInterfaceParametersRepository.List_AccountSettingsFOX_PublicMethod
        Dim BusquedaInterface = (From c In _context.AccountSettingsFOX_PublicMethod Select c)
        Return BusquedaInterface.ToList()
    End Function

    ''' <summary>
    ''' Lista configuracion cuenta NET metodo privaado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function List_AccountSettingsNET_PrivateMethod() As List(Of AccountSettingsNET_PrivateMethod) Implements IInterfaceParametersRepository.List_AccountSettingsNET_PrivateMethod
        Dim BusquedaInterface = (From c In _context.AccountSettingsNET_PrivateMethod Select c)
        Return BusquedaInterface.ToList()
    End Function

    ''' <summary>
    ''' Lista configuracion cuenta NET metodo publico
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function List_AccountSettingsNET_PublicMethod() As List(Of AccountSettingsNET_PublicMethod) Implements IInterfaceParametersRepository.List_AccountSettingsNET_PublicMethod
        Dim BusquedaInterface = (From c In _context.AccountSettingsNET_PublicMethod Select c)
        Return BusquedaInterface.ToList()
    End Function


    ''' <summary>
    ''' Funcion para Cargar los tipos de documento para notas contables
    ''' </summary>
    ''' <param name="container">nombre del conteneder a traer datos</param>
    ''' <returns>Cargar los tipos de documento para notas contables</returns>
    Public Function ListTypeDocument(ByVal container As String) As List(Of SP_TypeDocumentList_Result) Implements IInterfaceParametersRepository.ListTypeDocument
        Dim Busqueda = (From c In _context.SP_TypeDocumentList(container)).ToList
        Return Busqueda
    End Function


    ''' <summary>
    ''' Funcion para Cargar las Cuentas Contables DGH
    ''' </summary>
    ''' <param name="container">nombre del conteneder a traer datos</param>
    ''' <returns>Listado de Cuentas</returns>
    Public Function ListAccounts(ByVal container As String) As List(Of SP_AccountsList_Result) Implements IInterfaceParametersRepository.ListAccounts
        Dim Busqueda = (From c In _context.SP_AccountsList(container)).ToList
        Return Busqueda
    End Function

    ''' <summary>
    ''' Funcion para cargar los conceptos contables
    ''' </summary>
    ''' <param name="container">nombre del conteneder a traer datos</param>
    '''<param name="TypeConcept">Tipo de concepto cartera</param>
    ''' <returns>Lista de Conceptos</returns>
    ''' <remarks></remarks>
    Public Function ListAccountingConcept(ByVal container As String, ByVal TypeConcept As String) As List(Of SP_AccountingConceptList_Result) Implements IInterfaceParametersRepository.ListAccountingConcept
        Dim Busqueda = (From c In _context.SP_AccountingConceptList(container, TypeConcept)).ToList
        Return Busqueda
    End Function

    ''' <summary>
    ''' Validar contenedor si existe
    ''' </summary>
    ''' <param name="containerName">Nombre del contenedor</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SP_ValidateContainer(containerName As String) As SP_ValidateContainer_Result Implements IInterfaceParametersRepository.SP_ValidateContainer
        Dim obj = (From c In _context.SP_ValidateContainer(containerName)).ToList()
        If obj.Count > 0 Then
            Return obj.SingleOrDefault
        Else
            Return New SP_ValidateContainer_Result
        End If
    End Function



    ''' <summary>
    ''' Funcion para validar que la cuenta este configurada en los parametros FOX privado
    ''' </summary>
    ''' <param name="AccountValidate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidateAccountTableFOxPrivate(ByVal AccountValidate As String, ByVal ObjectionReception As Boolean) As Boolean Implements IInterfaceParametersRepository.ValidateAccountTableFOxPrivate
        Dim result As Boolean
        Dim Busqueda As AccountSettingsFOX_PrivateMethod
        If ObjectionReception = False Then
            Busqueda = (From e In _context.AccountSettingsFOX_PrivateMethod
            Where e.InvoiceNotRadicate = AccountValidate.Trim
         Select e).FirstOrDefault
        Else
            Busqueda = (From e In _context.AccountSettingsFOX_PrivateMethod
        Where e.InvoiceRadicate = AccountValidate.Trim
         Select e).FirstOrDefault
        End If
        If Busqueda IsNot Nothing AndAlso Busqueda.Id > 0 Then
            result = True
        Else
            result = False
        End If
        Return result
    End Function

    ' ''' <summary>
    ' ''' Funcion para retornar agrupacion de cunetas segun norma 1121 y validar homologacion de cuentas
    ' ''' </summary>
    ' ''' <param name="AccountValidate"></param>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    Public Function ValidateListInvoiceAccountTableFOxPrivate() As List(Of AccountSettingsFOX_PrivateMethod) Implements IInterfaceParametersRepository.ValidateListInvoiceAccountTableFOxPrivate
        Dim Busqueda = (From e In _context.AccountSettingsFOX_PrivateMethod Select e).ToList()
        Return Busqueda
    End Function

    ''' <summary>
    ''' Funcion para validar que la cuenta este configurada en los parametros FOx publico
    ''' </summary>
    ''' <param name="AccountValidate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidateAccountTableNEtPrivate(ByVal AccountValidate As String, ByVal ObjectionReception As Boolean) As Boolean Implements IInterfaceParametersRepository.ValidateAccountTableNEtPrivate
        Dim result As Boolean
        Dim Busqueda As AccountSettingsNET_PrivateMethod
        If ObjectionReception = False Then
            Busqueda = (From e In _context.AccountSettingsNET_PrivateMethod
                  Where e.InvoiceRadicate = AccountValidate.Trim
               Select e).FirstOrDefault
        Else
            Busqueda = (From e In _context.AccountSettingsNET_PrivateMethod
              Where e.InvoiceRadicate = AccountValidate.Trim
               Select e).FirstOrDefault
        End If

        If Busqueda IsNot Nothing AndAlso Busqueda.Id > 0 Then
            result = True
        Else
            result = False
        End If
        Return result
    End Function
End Class
