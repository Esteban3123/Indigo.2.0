'***********************************************************************
' Assembly         : Domain.Payroll
' Author           : Juan Diego Díaz
' Created          : 05-09-2018
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base

Public Interface IFreeTimeUseRepository
    Inherits IRepository(Of FreeTimeUse)

    ''' <summary>
    ''' Obtiene una Actividad en Tiempo Libre
    ''' </summary>
    ''' <param name="code">Código de la Actividad en Tiempo Libre</param>
    ''' <param name="tracking">Tracking</param>
    ''' <returns>Actividad en Tiempo Libre</returns>
    ''' <remarks></remarks>
    Function GetFreeTimeUse(code As String, tracking As Boolean) As FreeTimeUse

    ''' <summary>
    ''' Obtiene una Actividad en Tiempo Libre por ID
    ''' </summary>
    ''' <param name="ID">Id de la Actividad en Tiempo Libre</param>
    ''' <param name="tracking">Tracking</param>
    ''' <returns>Actividad en Tiempo Libre</returns>
    ''' <remarks></remarks>
    Function GetFreeTimeUseById(ID As Integer, tracking As Boolean) As FreeTimeUse


End Interface
