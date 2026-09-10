'***********************************************************************
' Assembly         : Domain.Payroll
' Author           : Cristhian Mauricio Salazar
' Created          : 12-04-2011
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Payroll.Entities

Public Interface IEducationLevelsRepository
    Inherits IRepository(Of EducationLevel)

    ''' <summary>
    ''' Lista todos los niveles de educacion
    ''' </summary>
    ''' <returns>Lista de Niveles de educacion</returns>
    ''' <remarks></remarks>
    Function ListAllEducationLevels() As List(Of EducationLevel)

    ''' <summary>
    ''' Obtiene un nivel de educacion especifico
    ''' </summary>
    ''' <returns>Nivel de educacion</returns>
    ''' <remarks></remarks>
    Function GetEducationLevels(ByVal code As String, Optional desatach As Boolean = True) As EducationLevel

End Interface
