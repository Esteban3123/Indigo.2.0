'***********************************************************************
' Assembly         : Presentacion.Payroll
' Author           : Mariana Gonzalez Calderon
' Created          : 28/11/2025
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

''' <summary>
''' Clase wrapper para el historial de cambios con propiedades de agrupación
''' </summary>
Public Class HistoryChangesWrapper
    Public Property ContractStatus As String
    Public Property ContractId As Integer
    Public Property Type As String
    Public Property ValueOld As String
    Public Property ValueNew As String
    Public Property UserCodeName As String
    Public Property [Date] As DateTime
End Class

