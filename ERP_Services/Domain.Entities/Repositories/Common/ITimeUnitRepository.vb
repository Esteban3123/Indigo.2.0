'***********************************************************************
' Assembly         : Domain.Common
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 09-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Common.Entities

Public Interface ITimeUnitRepository

    Inherits IRepository(Of TimeUnit)

    ''' <summary>
    ''' Lista Todas las Unidades de Tiempo
    ''' </summary>
    ''' <returns>Unidades de Tiempo</returns>
    ''' <remarks></remarks>
    Function ListAllTimeUnit() As List(Of TimeUnit)

    ''' <summary>
    ''' Obtiene una Unidad de Tiempo
    ''' </summary>
    ''' <param name="code">Código de la Unidad de Tiempo</param>
    ''' <returns>Unidad de Tiempo</returns>
    ''' <remarks></remarks>
    Function GetTimeUnit(ByVal code As String) As TimeUnit

    ''' <summary>
    ''' Obtiene un centro de costo especifico
    ''' </summary>
    ''' <param name="code">Codigo del centro de costo</param>
    ''' <returns>Centro de costo</returns>
    ''' <remarks></remarks>
    Function GetTimeUnitByCode(ByVal code As String, Optional desatach As Boolean = True) As TimeUnit

End Interface
