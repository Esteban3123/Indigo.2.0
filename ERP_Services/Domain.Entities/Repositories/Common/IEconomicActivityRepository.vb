'***********************************************************************
' Assembly         : Domain.Common
' Author           : Carlos Mario Arias Rubiano
' Created          : 03/12/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities
Public Interface IEconomicActivityRepository
    Inherits IRepository(Of EconomicActivity)

    ''' <summary>
    ''' Obtiene una actividad economica por id
    ''' </summary>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetEconomicActivityById(id As Integer, Optional tracking As Boolean = True) As EconomicActivity

    ''' <summary>
    ''' Obtiene una actividad economica por codigo
    ''' </summary>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetEconomicActivity(code As String, Optional tracking As Boolean = True) As EconomicActivity

End Interface
