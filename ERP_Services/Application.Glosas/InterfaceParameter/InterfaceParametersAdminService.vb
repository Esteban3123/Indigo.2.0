'***********************************************************************
' Assembly         : Application.Glosas
' Author           : Rafael Eduardo PAtiño
' Created          : 12-09-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Base
Imports Domain.Entities
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Base.Entities
Imports Domain.Common.Entities
Imports System.Data.Entity.Core

#End Region

Public Class InterfaceParametersAdminService
    Implements IInterfaceParameterAdminService


    Private _InterfaceParameters As IInterfaceParametersRepository

    ''' <summary>
    ''' Initializa una nueva instancia de la clase <see cref="InterfaceParametersadminService" />.
    ''' </summary>
    ''' <param name="InterfaceParameters">el repositorio para el manejo configuracion de interfaces.</param>
    Public Sub New(ByVal InterfaceParameters As IInterfaceParametersRepository)
        If InterfaceParameters Is Nothing Then
            Throw New ArgumentNullException("InterfaceParameters Vacio")
        End If
        _InterfaceParameters = InterfaceParameters
    End Sub

    ''' <summary>
    ''' consulta parametros por Id
    ''' </summary>
    ''' <param name="ContainerName">codigo de contenedor</param>
    ''' <returns>objecto tipo parametro</returns>
    Public Function GetInterfacesParameters(ByVal ContainerName As String) As GlosasParametersInterface Implements IInterfaceParameterAdminService.GetInterfacesParameters
        If String.IsNullOrEmpty(ContainerName) Then
            Throw New ArgumentException("Codigo Contenedort Vacio")
        End If
        Try
            Return _InterfaceParameters.GetInterfacesParameters(ContainerName)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' graba cambios parametro interfaz
    ''' </summary>
    ''' <param name="InterfaceParameter">Parametros de interfaz</param>
    ''' <returns></returns>
    Public Function SaveInterfaceParameter(InterfaceParameter As GlosasParametersInterface, audit As AuditMessage) As ActionResult Implements IInterfaceParameterAdminService.SaveInterfaceParameter
        If InterfaceParameter Is Nothing Then
            Throw New ArgumentNullException("InterfaceParameter Vacio")
        End If
        Dim unitOfWork As IUnitWork = _InterfaceParameters.UnitWork
        Try

            Dim auditProcess As IndigoAuditSimpleEntity(Of GlosasParametersInterface)
            Dim AuxGlosasParametersInterface As GlosasParametersInterface = Nothing
            Dim status As Integer
            If InterfaceParameter.ChangeTracker.State = ObjectState.Modified Then
                AuxGlosasParametersInterface = InterfaceParameter.OriginalValue
                InterfaceParameter.ModificationUser = audit.CodeUser
                InterfaceParameter.ModificationDate = Date.Now()
                status = Infrastructure.CrossCutting.Audit.Actions.Update
            ElseIf InterfaceParameter.ChangeTracker.State = ObjectState.Added Then
                InterfaceParameter.CreationUser = audit.CodeUser
                InterfaceParameter.CreationDate = Date.Now()
                status = Infrastructure.CrossCutting.Audit.Actions.Insert
            End If
            _InterfaceParameters.SaveEntity(InterfaceParameter)
            'confirmo la unidad de trabajo
            unitOfWork.Commit()

            auditProcess = New IndigoAuditSimpleEntity(Of GlosasParametersInterface)(InterfaceParameter, audit, status, AuxGlosasParametersInterface)
            auditProcess.Execute()

            'If (InterfaceParameter.ChangeTracker.State = ObjectState.Added) Then
            '    '/***** Auditoria Basica ********/
            '    IndigoAuditBasic.Execute("GlosasParametersInterface", audit.Functional, InterfaceParameter.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, Infrastructure.CrossCutting.Base.ActionsAudit.Crear, audit.Company, audit.ContainerSecurity)
            '    IndigoAuditSimpleEntity(Of GlosasParametersInterface).Execute(InterfaceParameter, audit, Infrastructure.CrossCutting.Audit.Actions.Insert, audit.Company)
            'ElseIf InterfaceParameter.ChangeTracker.State = ObjectState.Modified Then
            '    '/***** Auditoria Basica ********/
            '    IndigoAuditBasic.Execute("GlosasParametersInterface", audit.Functional, InterfaceParameter.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, Infrastructure.CrossCutting.Base.ActionsAudit.Modificar, audit.Company, audit.ContainerSecurity)
            '    IndigoAuditSimpleEntity(Of GlosasParametersInterface).Execute(InterfaceParameter, audit, Infrastructure.CrossCutting.Audit.Actions.Update, audit.Company, InterfaceParameter)
            'End If

            Return New ActionResult With {.StateResult = True}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False}
        End Try
    End Function

    ''' <summary>
    ''' Funcion para Cargar las Cuentas Contables DGH
    ''' </summary>
    ''' <param name="container">nombre del conteneder a traer datos</param>
    ''' <returns>Listado de Cuentas</returns>
    Public Function ListAccounts(ByVal container As String) As List(Of SP_AccountsList_Result) Implements IInterfaceParameterAdminService.ListAccounts
        Try
            Return _InterfaceParameters.ListAccounts(container)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Funcion para cargar los conceptos contables
    ''' </summary>
    ''' <param name="container">nombre del conteneder a traer datos</param>
    '''<param name="TypeConcept">Tipo de concepto cartera</param>
    ''' <returns>Lista de Conceptos</returns>
    ''' <remarks></remarks>
    Public Function ListAccountingConcept(ByVal container As String, ByVal TypeConcept As String) As List(Of SP_AccountingConceptList_Result) Implements IInterfaceParameterAdminService.ListAccountingConcept
        Try
            Return _InterfaceParameters.ListAccountingConcept(container, TypeConcept)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Lista de Parametros de Interface
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListInterfacesParameters() As List(Of GlosasParametersInterface) Implements IInterfaceParameterAdminService.ListInterfacesParameters
        Try
            Return _InterfaceParameters.ListInterfacesParameters()
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Validar contenedor si existe
    ''' </summary>
    ''' <param name="containerName">nombre del contenedor</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SP_ValidateContainer(containerName As String) As Boolean Implements IInterfaceParameterAdminService.SP_ValidateContainer
        Try
            Dim Obj As SP_ValidateContainer_Result = _InterfaceParameters.SP_ValidateContainer(containerName)
            If Obj.Respuesta = 1 Then
                Return True
            ElseIf Obj.Respuesta = 0 Then
                Return False
            End If
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Lista configuracion cuenta FOX metodo privado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function List_AccountSettingsFOX_PrivateMethod() As List(Of AccountSettingsFOX_PrivateMethod) Implements IInterfaceParameterAdminService.List_AccountSettingsFOX_PrivateMethod
        Try
            Return _InterfaceParameters.List_AccountSettingsFOX_PrivateMethod()
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Lista configuracion cuenta FOX metodo publico
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function List_AccountSettingsFOX_PublicMethod() As List(Of AccountSettingsFOX_PublicMethod) Implements IInterfaceParameterAdminService.List_AccountSettingsFOX_PublicMethod
        Try
            Return _InterfaceParameters.List_AccountSettingsFOX_PublicMethod()
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Lista configuracion cuenta NET metodo privaado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function List_AccountSettingsNET_PrivateMethod() As List(Of AccountSettingsNET_PrivateMethod) Implements IInterfaceParameterAdminService.List_AccountSettingsNET_PrivateMethod
        Try
            Return _InterfaceParameters.List_AccountSettingsNET_PrivateMethod()
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function


    ''' <summary>
    ''' Lista configuracion cuenta NET metodo publico
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function List_AccountSettingsNET_PublicMethod() As List(Of AccountSettingsNET_PublicMethod) Implements IInterfaceParameterAdminService.List_AccountSettingsNET_PublicMethod
        Try
            Return _InterfaceParameters.List_AccountSettingsNET_PublicMethod()
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function


    ''' <summary>
    ''' Funcion para validar que la cuenta este configurada en los parametros FOX privado
    ''' </summary>
    ''' <param name="AccountValidate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidateAccountTableFOxPrivate(AccountValidate As String, ByVal ObjectionReception As Boolean) As Boolean Implements IInterfaceParameterAdminService.ValidateAccountTableFOxPrivate
        Try
            Return _InterfaceParameters.ValidateAccountTableFOxPrivate(AccountValidate, ObjectionReception)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Funcion para Cargar los tipos de documento para notas contables
    ''' </summary>
    ''' <param name="container">nombre del conteneder a traer datos</param>
    ''' <returns>Cargar los tipos de documento para notas contables</returns>
    Public Function ListTypeDocument(container As String) As List(Of SP_TypeDocumentList_Result) Implements IInterfaceParameterAdminService.ListTypeDocument
        Try
            Return _InterfaceParameters.ListTypeDocument(container)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function


    Public Function ValidateAccountTableNEtPrivate(AccountValidate As String, ObjectionReception As Boolean) As Boolean Implements IInterfaceParameterAdminService.ValidateAccountTableNEtPrivate
        Try
            Return _InterfaceParameters.ValidateAccountTableNEtPrivate(AccountValidate, ObjectionReception)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' Funcion para retornar agrupacion de cunetas segun norma 1121 y validar homologacion de cuentas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidateListInvoiceAccountTableFOxPrivate() As List(Of AccountSettingsFOX_PrivateMethod) Implements IInterfaceParameterAdminService.ValidateListInvoiceAccountTableFOxPrivate
        Try
            Return _InterfaceParameters.ValidateListInvoiceAccountTableFOxPrivate()
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
            _InterfaceParameters = Nothing
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
