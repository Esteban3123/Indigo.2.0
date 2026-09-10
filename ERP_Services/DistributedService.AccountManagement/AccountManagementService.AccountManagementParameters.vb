'***********************************************************************
' Assembly         : Presentacion.AccoungManagement
' Author           : Andres Felipe Quintero Garcia 
' Created          : 09-01-2025
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "imports"
Imports Application.AccountManagement
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity
#End Region

Partial Public Class AccountManagementService
    Implements IAccountManagementServiceAccountManagementParameters, Inject

    ''' <summary>
    ''' Obtiene la gestion de cuenta por Id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAccountManagementParametersById(id As Integer) As ActionResult(Of AccountManagementParameters) Implements IAccountManagementServiceAccountManagementParameters.GetAccountManagementParametersById
        Using service As IAccountManagementParametersAdminService = Container.Current.Resolve(Of IAccountManagementParametersAdminService)()
            Return service.GetAccountManagementParametersById(id)
        End Using
    End Function

    ''' <summary>
    ''' Guarda o actualiza la gestion de cuenta
    ''' </summary>
    ''' <param name="accountManagementParameters"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveAccountManagementParameters(accountManagementParameters As Domain.Entities.AccountManagementParameters, audit As AuditMessage) As Task(Of Domain.Base.Entities.ActionResult(Of AccountManagementParameters)) Implements IAccountManagementServiceAccountManagementParameters.SaveAccountManagementParameters
        Using service As IAccountManagementParametersAdminService = Container.Current.Resolve(Of IAccountManagementParametersAdminService)()
            Return Await service.SaveAccountManagementParameters(accountManagementParameters, audit)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene los usuarios activos para asignación de ingresos
    ''' </summary>
    ''' <returns></returns>
    Public Function GetAvailableUsers() As ActionResult(Of List(Of UsersAssignment)) Implements IAccountManagementServiceAccountManagementParameters.GetAvailableUsers
        Using service As IAccountManagementParametersAdminService = Container.Current.Resolve(Of IAccountManagementParametersAdminService)()
            Return service.GetAvailableUsers()
        End Using
    End Function

    ''' <summary>
    ''' Obtiene los usuarios con ingresos o folios distribuidos
    ''' </summary>
    ''' <returns></returns>
    Public Async Function GetUsersWithDistributedAccounts() As Task(Of ActionResult(Of List(Of UsersAssignment))) Implements IAccountManagementServiceAccountManagementParameters.GetUsersWithDistributedAccounts
        Using service As IAccountManagementParametersAdminService = Container.Current.Resolve(Of IAccountManagementParametersAdminService)()
            Return Await service.GetUsersWithDistributedAccounts()
        End Using
    End Function
End Class
