'***********************************************************************
' Assembly         : Presentacion.AccoungManagement
' Author           : Andres Felipe Quintero Garcia 
' Created          : 09-01-2025
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.Threading.Tasks
Imports Domain.Base
#End Region

Public Interface IAccountManagementParametersRepository
    Inherits IRepository(Of AccountManagementParameters)

    ''' <summary>
    ''' Obtiene el parametro de gestión de cuentas por id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    Function GetAccountManagementParametersById(id As Integer) As AccountManagementParameters

    ''' <summary>
    ''' Obtiene los usuarios disponibles para asignación de ingresos
    ''' </summary>
    ''' <returns>Lista de usuarios disponibles para asignación de ingresos</returns>
    Function GetAvailableUsers() As List(Of UsersAssignment)

    ''' <summary>
    ''' Obtiene los usuarios con ingresos distribuidos
    ''' </summary>
    ''' <returns>Lista de usuarios con ingresos distribuidos</returns>
    Function GetUsersWithDistributedAccounts() As Task(Of List(Of UsersAssignment))
End Interface
