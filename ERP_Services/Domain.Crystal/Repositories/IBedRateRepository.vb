'***********************************************************************
' Assembly         : Domain.Crystal
' Author           : Diego Roldán
' Created          : 22-06-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Crystal.Entities

Public Interface IBedRateRepository
    Inherits IRepository(Of CHGENTARI)
    ''' <summary>
    ''' obtiene el listado de tarifas de camas por codigo de la cama
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetBedRatebyBedCode(bedCode As Integer) As List(Of CHGENTARI)

    ''' <summary>
    ''' obtiene una tarifa de cama por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetBedRatebyCode(code As Integer) As CHGENTARI

End Interface