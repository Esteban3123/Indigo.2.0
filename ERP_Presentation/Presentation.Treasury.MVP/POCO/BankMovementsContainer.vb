'***********************************************************************
' Assembly         : Presentacion.Treasury.MVP
' Author           : Cristian Camilo Fierro Rojas
' Created          : 30-11-2023
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Newtonsoft.Json
#End Region

Public Class BankMovementsContainer
    <JsonProperty("Movimientos Bancarios")>
    Public Property BankMovements As List(Of BankMovements)
    <JsonProperty("Saldo final")>
    Public Property endingBalance As Double
    <JsonProperty("Saldo inicial")>
    Public Property initialBalance As Double


End Class
Public Class BankMovements

    <JsonProperty("Credito")>
    Public Property ValueCredit As Double
    <JsonProperty("Debito")>
    Public Property ValueDebit As Double
    <JsonProperty("dcto.")>
    Public Property discount As String
    <JsonProperty("descripción")>
    Public Property DescriptionTransaction As String
    <JsonProperty("fecha")>
    Public Property TransactionDate As String
    <JsonProperty("saldo")>
    Public Property balance As Double
    <JsonProperty("sucursal")>
    Public Property branch As String
    <JsonProperty("valor")>
    Public Property value As Double
    <JsonProperty("cod trans")>
    Public Property TransactionCode As String
    <JsonProperty("doc.")>
    Public Property Document As String

End Class