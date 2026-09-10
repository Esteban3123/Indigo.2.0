'************************************************************
' Assembly         : Domain.Entities
' Author           : Felix Camilo Salazar Roldan
' Created          : 14-11-2024
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Domain.Base

#End Region

Public Interface IManagementAreasRepository
    Inherits IRepository(Of ManagementAreas)

    ''' <summary>
    ''' Lista todas las areas de gestion por código 
    ''' </summary>
    ''' <param name="userCode">Código de usuario</param>
    ''' <returns>Lista de resoluciones</returns>
    Function ListManagementAreasByUserCode(ByVal userCode As String) As List(Of ManagementAreas)

    ''' <summary>
    ''' Obtiene del area de gestion por id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    Function GetManagementAreasById(ByVal Id As Integer) As ManagementAreas

    ''' <summary>
    ''' Obtiene la area de gestion por codigo
    ''' </summary>
    ''' <returns></returns>
    Function GetManagementAreasByCode(ByVal code As String) As ManagementAreas

    ''' <summary>
    ''' Obtiene todas las areas de gestión con las usuarios de cada una incluidos
    ''' </summary>
    ''' <returns></returns>
    Function GetAllWithUsers() As List(Of ManagementAreas)
End Interface

