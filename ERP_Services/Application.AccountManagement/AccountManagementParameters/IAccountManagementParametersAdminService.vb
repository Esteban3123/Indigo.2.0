'***********************************************************************
' Assembly         : Presentacion.AccoungManagement
' Author           : Andres Felipe Quintero Garcia 
' Created          : 09-01-2025
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
#End Region

Public Interface IAccountManagementParametersAdminService
    Inherits IDisposable

    Function GetAccountManagementParametersById(id As Integer) As ActionResult(Of AccountManagementParameters)
    Function SaveAccountManagementParameters(ByVal AccountManagementParameters As AccountManagementParameters, audit As AuditMessage) As Task(Of ActionResult(Of AccountManagementParameters))
    Function GetAvailableUsers() As ActionResult(Of List(Of UsersAssignment))
    Function GetUsersWithDistributedAccounts() As Task(Of ActionResult(Of List(Of UsersAssignment)))
End Interface
