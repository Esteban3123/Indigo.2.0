'***********************************************************************
' Assembly         : Application.Glosas
' Author           : Rafael Patiño
' Created          : 12-09-2013
'
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region

Public Interface IInterfaceParameterAdminService
    Inherits IDisposable

    ''' <summary>
    ''' consulta parametros por Id
    ''' </summary>
    ''' <param name="ContainerName">Codigo de contenedor</param>
    ''' <returns>objecto tipo parametro</returns>
    Function GetInterfacesParameters(ByVal ContainerName As String) As GlosasParametersInterface

    ''' <summary>
    ''' graba cambios parametro interfaz
    ''' </summary>
    ''' <param name="InterfaceParameter">Parametros de interfaz</param>
    ''' <returns></returns>
    Function SaveInterfaceParameter(InterfaceParameter As GlosasParametersInterface, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Funcion para Cargar las Cuentas Contables DGH
    ''' </summary>
    ''' <param name="container">nombre del conteneder a traer datos</param>
    ''' <returns>Listado de Cuentas</returns>
    Function ListAccounts(ByVal container As String) As List(Of SP_AccountsList_Result)

    ''' <summary>
    ''' Funcion para cargar los conceptos contables
    ''' </summary>
    ''' <param name="container">nombre del conteneder a traer datos</param>
    '''<param name="TypeConcept">Tipo de concepto cartera</param>
    ''' <returns>Lista de Conceptos</returns>
    ''' <remarks></remarks>
    Function ListAccountingConcept(ByVal container As String, ByVal TypeConcept As String) As List(Of SP_AccountingConceptList_Result)

    ''' <summary>
    ''' Lista de Parametros de Interface
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListInterfacesParameters() As List(Of GlosasParametersInterface)

    ''' <summary>
    ''' Validar contenedor si existe
    ''' </summary>
    ''' <param name="containerName">Nombre del contenedor</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SP_ValidateContainer(containerName As String) As Boolean

    ''' <summary>
    ''' Lista configuracion cuenta FOX metodo privado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function List_AccountSettingsFOX_PrivateMethod() As List(Of AccountSettingsFOX_PrivateMethod)
    ''' <summary>
    ''' Lista configuracion cuenta FOX metodo publico
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function List_AccountSettingsFOX_PublicMethod() As List(Of AccountSettingsFOX_PublicMethod)
    ''' <summary>
    ''' Lista configuracion cuenta NET metodo privaado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function List_AccountSettingsNET_PrivateMethod() As List(Of AccountSettingsNET_PrivateMethod)

    ''' <summary>
    ''' Lista configuracion cuenta NET metodo publico
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function List_AccountSettingsNET_PublicMethod() As List(Of AccountSettingsNET_PublicMethod)

    ''' <summary>
    ''' Funcion para validar que la cuenta este configurada en los parametros FOX privado
    ''' </summary>
    ''' <param name="AccountValidate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ValidateAccountTableFOxPrivate(ByVal AccountValidate As String, ByVal ObjectionReception As Boolean) As Boolean

    ''' <summary>
    ''' Funcion para Cargar los tipos de documento para notas contables
    ''' </summary>
    ''' <param name="container">nombre del conteneder a traer datos</param>
    ''' <returns>Cargar los tipos de documento para notas contables</returns>
    Function ListTypeDocument(ByVal container As String) As List(Of SP_TypeDocumentList_Result)
    ''' <summary>
    ''' Funcion para validar que la cuenta este configurada en los parametros FOx publico
    ''' </summary>
    ''' <param name="AccountValidate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ValidateAccountTableNEtPrivate(ByVal AccountValidate As String, ByVal ObjectionReception As Boolean) As Boolean

    ''' <summary>
    ''' Funcion para retornar agrupacion de cunetas segun norma 1121 y validar homologacion de cuentas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ValidateListInvoiceAccountTableFOxPrivate() As List(Of AccountSettingsFOX_PrivateMethod)

End Interface
