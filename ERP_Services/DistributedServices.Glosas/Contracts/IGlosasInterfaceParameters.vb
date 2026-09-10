'***********************************************************************
' Assembly         : DistributedService.Glosas
' Author           : Rafael Eduardo Patiño
' Created          : 12-09-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports System.ServiceModel
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base


<ServiceContract()> _
Public Interface IGlosasInterfaceParameters

    ''' <summary>
    ''' consulta parametros por Codigo
    ''' </summary>
    ''' <param name="ContainerName">codigo del contenedor</param>
    ''' <returns>objecto tipo parametro</returns>
    <OperationContract()>
    Function GetInterfacesParameters(ByVal ContainerName As String, ByVal session As SessionValues) As GlosasParametersInterface

    ''' <summary>
    ''' graba cambios parametro interfaz
    ''' </summary>
    ''' <param name="InterfaceParameter">Parametros de interfaz</param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveInterfaceParameter(InterfaceParameter As GlosasParametersInterface, session As SessionValues) As ActionResult

    ''' <summary>
    ''' Funcion para Cargar las Cuentas Contables DGH
    ''' </summary>
    ''' <param name="container">nombre del conteneder a traer datos</param>
    ''' <returns>Listado de Cuentas</returns>
    <OperationContract>
    Function ListAccounts(ByVal container As String, ByVal session As SessionValues) As List(Of SP_AccountsList_Result)

    ''' <summary>
    ''' Funcion para cargar los conceptos contables
    ''' </summary>
    ''' <param name="container">nombre del conteneder a traer datos</param>
    '''<param name="TypeConcept">Tipo de concepto cartera</param>
    ''' <returns>Lista de Conceptos</returns>
    ''' <remarks></remarks>
    <OperationContract>
    Function ListAccountingConcept(ByVal container As String, ByVal TypeConcept As String, ByVal session As SessionValues) As List(Of SP_AccountingConceptList_Result)

    ''' <summary>
    ''' Lista de Parametros de Interface
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract>
    Function ListInterfacesParameters(ByVal session As SessionValues) As List(Of GlosasParametersInterface)


    ''' <summary>
    ''' Validar contenedor si existe
    ''' </summary>
    ''' <param name="containerName">Nombre del contenedor</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract>
    Function ValidateContainer(containerName As String, ByVal session As SessionValues) As Boolean


    ''' <summary>
    ''' Lista configuracion cuenta FOX metodo privado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract>
    Function List_AccountSettingsFOX_PrivateMethod(ByVal session As SessionValues) As List(Of AccountSettingsFOX_PrivateMethod)
    ''' <summary>
    ''' Lista configuracion cuenta FOX metodo publico
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract>
    Function List_AccountSettingsFOX_PublicMethod(ByVal session As SessionValues) As List(Of AccountSettingsFOX_PublicMethod)
    ''' <summary>
    ''' Lista configuracion cuenta NET metodo privaado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract>
    Function List_AccountSettingsNET_PrivateMethod(ByVal session As SessionValues) As List(Of AccountSettingsNET_PrivateMethod)


    ''' <summary>
    ''' Lista configuracion cuenta NET metodo publico
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract>
    Function List_AccountSettingsNET_PublicMethod(ByVal session As SessionValues) As List(Of AccountSettingsNET_PublicMethod)

    ''' <summary>
    ''' Funcion para validar que la cuenta este configurada en los parametros FOX privado
    ''' </summary>
    ''' <param name="AccountValidate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract>
    Function ValidateAccountTableFOxPrivate(ByVal AccountValidate As String, ByVal session As SessionValues, ByVal ObjectionReception As Boolean) As Boolean

    ''' <summary>
    ''' Funcion para Cargar los tipos de documento para notas contables
    ''' </summary>
    ''' <param name="container">nombre del conteneder a traer datos</param>
    ''' <returns>Cargar los tipos de documento para notas contables</returns>
    <OperationContract>
    Function ListTypeDocument(ByVal container As String, ByVal session As SessionValues) As List(Of SP_TypeDocumentList_Result)


    ''' <summary>
    ''' Funcion para validar que la cuenta este configurada en los parametros FOx publico
    ''' </summary>
    ''' <param name="AccountValidate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract>
    Function ValidateAccountTableNEtPrivate(ByVal AccountValidate As String, ByVal session As SessionValues, ByVal ObjectionReception As Boolean) As Boolean

    ''' <summary>
    ''' Funcion para retornar agrupacion de cunetas segun norma 1121 y validar homologacion de cuentas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract>
    Function ValidateListInvoiceAccountTableFOxPrivate(ByVal session As SessionValues) As List(Of AccountSettingsFOX_PrivateMethod)

End Interface
