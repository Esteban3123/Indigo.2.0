'***********************************************************************
' Assembly         : Application.Crystal
' Author           : Diego Roldán
' Created          : 22-06-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Dynamic
Imports Domain.Crystal.Entities

Public Interface IBedRateAdminService
    Inherits IDisposable

#Region "Methods"
    ''' <summary>
    ''' obtiene una tariga de cama por codigo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetBedRatebyBedCode(code As Integer) As ActionResult(Of List(Of CHGENTARI))

    ''' <summary>
    ''' obtiene una tarifa de cama por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetBedRatebyCode(code As Integer) As CHGENTARI

    ''' <summary>
    ''' Guarda una tarifa para una cama
    ''' </summary>
    ''' <param name="bedRate">The bed rate.</param>
    ''' <returns></returns>
    Function SaveBedRate(bedRate As CHGENTARI) As ActionResult(Of CHGENTARI)

    ''' <summary>
    ''' Elimina una tarifa para una cama
    ''' </summary>
    ''' <param name="bedRate">The bed rate.</param>
    ''' <returns></returns>
    Function DeleteBedRate(bedRate As CHGENTARI) As ActionResult
#End Region

End Interface