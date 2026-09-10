'***********************************************************************
' Assembly         : DistributedService.Crystal
' Author           : Diego A. Roldán
' Created          : 22-06-2015
'
' Last Modified By : 
' Last Modified On : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel
Imports Domain.Base.Entities
Imports Domain.Crystal.Entities

#End Region
<ServiceContract()> _
Public Interface ICrystalServiceBedRate
    ''' <summary>
    ''' obtiene una tariga de cama por codigo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetBedRatebyBedCode(bedCode As Integer) As ActionResult(Of List(Of CHGENTARI))

    ''' <summary>
    ''' Guarda una tarifa para una cama
    ''' </summary>
    ''' <param name="bedRate">The bed rate.</param>
    ''' <returns></returns>
    <OperationContract()> _
    Function SaveBedRate(bedRate As CHGENTARI) As ActionResult(Of CHGENTARI)

    ''' <summary>
    ''' Elimina una tarifa para una cama
    ''' </summary>
    ''' <param name="bedRate">The bed rate.</param>
    ''' <returns></returns>
    <OperationContract()> _
    Function DeleteBedRate(bedRate As CHGENTARI) As ActionResult

    ''' <summary>
    ''' obtiene una tarifa de cama por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    <OperationContract()> _
    Function GetBedRatebyCode(code As Integer) As CHGENTARI

End Interface
