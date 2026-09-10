'***********************************************************************
' Assembly         : Application.Glosas
' Author           : Rafael Eduardo Patiño
' Created          : 12-09-2013
'
'***********************************************************************
#Region "Imports"
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.IOC
Imports Application.Glosas
Imports Infrastructure.CrossCutting.Base

#End Region

Partial Class GlosasService

    ''' <summary>
    ''' consulta parametros por Codigo
    ''' </summary>
    ''' <param name="ContainerName">codigo del contenedor</param>
    ''' <returns>objecto tipo parametro</returns>
    Public Function GetInterfacesParameters(ByVal ContainerName As String, session As SessionValues) As GlosasParametersInterface Implements IGlosasInterfaceParameters.GetInterfacesParameters
        Using _InterfacesParametersAdminService As IInterfaceParameterAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IInterfaceParameterAdminService)()
            Return _InterfacesParametersAdminService.GetInterfacesParameters(ContainerName)
        End Using
    End Function

    ''' <summary>
    ''' graba cambios parametro interfaz
    ''' </summary>
    ''' <param name="InterfaceParameter">Parametros de interfaz</param>
    ''' <returns></returns>
    Public Function SaveInterfaceParameter(InterfaceParameter As GlosasParametersInterface, session As SessionValues) As ActionResult Implements IGlosasInterfaceParameters.SaveInterfaceParameter
        Using _InterfacesParametersAdminService As IInterfaceParameterAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IInterfaceParameterAdminService)()
            Return _InterfacesParametersAdminService.SaveInterfaceParameter(InterfaceParameter, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Funcion para Cargar las Cuentas Contables DGH
    ''' </summary>
    ''' <param name="container">nombre del conteneder a traer datos</param>
    ''' <returns>Listado de Cuentas</returns>
    Public Function ListAccounts(ByVal container As String, session As SessionValues) As List(Of SP_AccountsList_Result) Implements IGlosasInterfaceParameters.ListAccounts
        Using _InterfacesParameterAdminService As IInterfaceParameterAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IInterfaceParameterAdminService)()
            Return _InterfacesParameterAdminService.ListAccounts(container)
        End Using
    End Function


    ''' <summary>
    ''' Funcion para cargar los conceptos contables
    ''' </summary>
    ''' <param name="container">nombre del conteneder a traer datos</param>
    '''<param name="TypeConcept">Tipo de concepto cartera</param>
    ''' <returns>Lista de Conceptos</returns>
    ''' <remarks></remarks>
    Public Function ListAccountingConcept(ByVal container As String, TypeConcept As String, session As SessionValues) As List(Of SP_AccountingConceptList_Result) Implements IGlosasInterfaceParameters.ListAccountingConcept
        Using _InterfacesParameterAdminService As IInterfaceParameterAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IInterfaceParameterAdminService)()
            Return _InterfacesParameterAdminService.ListAccountingConcept(container, TypeConcept)
        End Using
    End Function

    ''' <summary>
    ''' Lista de Parametros Interface
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListInterfacesParameters(ByVal session As SessionValues) As List(Of GlosasParametersInterface) Implements IGlosasInterfaceParameters.ListInterfacesParameters
        Using _InterfacesParameterAdminService As IInterfaceParameterAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IInterfaceParameterAdminService)()
            Return _InterfacesParameterAdminService.ListInterfacesParameters()
        End Using
    End Function

    ''' <summary>
    ''' validar si el contenedor existe
    ''' </summary>
    ''' <param name="containerName">nombre de contenedor</param>
    ''' <returns>true o false</returns>
    ''' <remarks></remarks>
    Public Function ValidateContainer(containerName As String, ByVal session As SessionValues) As Boolean Implements IGlosasInterfaceParameters.ValidateContainer
        Using _InterfacesParameterAdminService As IInterfaceParameterAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IInterfaceParameterAdminService)()
            Return _InterfacesParameterAdminService.SP_ValidateContainer(containerName)
        End Using
    End Function

    ''' <summary>
    ''' Lista configuracion cuenta FOX metodo privado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function List_AccountSettingsFOX_PrivateMethod(ByVal session As SessionValues) As List(Of AccountSettingsFOX_PrivateMethod) Implements IGlosasInterfaceParameters.List_AccountSettingsFOX_PrivateMethod
        Using _InterfacesParameterAdminService As IInterfaceParameterAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IInterfaceParameterAdminService)()
            Return _InterfacesParameterAdminService.List_AccountSettingsFOX_PrivateMethod()
        End Using
    End Function
    ''' <summary>
    ''' Lista configuracion cuenta FOX metodo publico
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function List_AccountSettingsFOX_PublicMethod(ByVal session As SessionValues) As List(Of AccountSettingsFOX_PublicMethod) Implements IGlosasInterfaceParameters.List_AccountSettingsFOX_PublicMethod
        Using _InterfacesParameterAdminService As IInterfaceParameterAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IInterfaceParameterAdminService)()
            Return _InterfacesParameterAdminService.List_AccountSettingsFOX_PublicMethod()
        End Using
    End Function
    ''' <summary>
    ''' Lista configuracion cuenta NET metodo privaado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function List_AccountSettingsNET_PrivateMethod(ByVal session As SessionValues) As List(Of AccountSettingsNET_PrivateMethod) Implements IGlosasInterfaceParameters.List_AccountSettingsNET_PrivateMethod
        Using _InterfacesParameterAdminService As IInterfaceParameterAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IInterfaceParameterAdminService)()
            Return _InterfacesParameterAdminService.List_AccountSettingsNET_PrivateMethod()
        End Using
    End Function

    ''' <summary>
    ''' Lista configuracion cuenta NET metodo publico
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function List_AccountSettingsNET_PublicMethod(session As SessionValues) As List(Of AccountSettingsNET_PublicMethod) Implements IGlosasInterfaceParameters.List_AccountSettingsNET_PublicMethod
        Using _InterfacesParameterAdminService As IInterfaceParameterAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IInterfaceParameterAdminService)()
            Return _InterfacesParameterAdminService.List_AccountSettingsNET_PublicMethod()
        End Using
    End Function


    ''' <summary>
    ''' Funcion para validar que la cuenta este configurada en los parametros FOX privado
    ''' </summary>
    ''' <param name="AccountValidate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidateAccountTableFOxPrivate(AccountValidate As String, ByVal session As SessionValues, ByVal ObjectionReception As Boolean) As Boolean Implements IGlosasInterfaceParameters.ValidateAccountTableFOxPrivate
        Using _InterfacesParameterAdminService As IInterfaceParameterAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IInterfaceParameterAdminService)()
            Return _InterfacesParameterAdminService.ValidateAccountTableFOxPrivate(AccountValidate, ObjectionReception)
        End Using
    End Function

    ''' <summary>
    ''' Funcion para Cargar los tipos de documento para notas contables
    ''' </summary>
    ''' <param name="container">nombre del conteneder a traer datos</param>
    ''' <returns>Cargar los tipos de documento para notas contables</returns>
    Public Function ListTypeDocument(container As String, session As SessionValues) As List(Of SP_TypeDocumentList_Result) Implements IGlosasInterfaceParameters.ListTypeDocument
        Using _InterfacesParameterAdminService As IInterfaceParameterAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IInterfaceParameterAdminService)()
            Return _InterfacesParameterAdminService.ListTypeDocument(container)
        End Using
    End Function
    ''' <summary>
    ''' Funcion para validar cuentas metodo privado version NET
    ''' </summary>
    ''' <param name="AccountValidate"></param>
    ''' <param name="session"></param>
    ''' <param name="ObjectionReception"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidateAccountTableNEtPrivate(AccountValidate As String, ByVal session As SessionValues, ObjectionReception As Boolean) As Boolean Implements IGlosasInterfaceParameters.ValidateAccountTableNEtPrivate
        Using _InterfacesParameterAdminService As IInterfaceParameterAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IInterfaceParameterAdminService)()
            Return _InterfacesParameterAdminService.ValidateAccountTableNEtPrivate(AccountValidate, ObjectionReception)
        End Using
    End Function
    ''' <summary>
    ''' Funcion para retornar agrupacion de cunetas segun norma 1121 y validar homologacion de cuentas
    ''' </summary>
    ''' <param name="session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidateListInvoiceAccountTableFOxPrivate(session As SessionValues) As List(Of AccountSettingsFOX_PrivateMethod) Implements IGlosasInterfaceParameters.ValidateListInvoiceAccountTableFOxPrivate
        Using _InterfacesParameterAdminService As IInterfaceParameterAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IInterfaceParameterAdminService)()
            Return _InterfacesParameterAdminService.ValidateListInvoiceAccountTableFOxPrivate()
        End Using
    End Function
End Class
