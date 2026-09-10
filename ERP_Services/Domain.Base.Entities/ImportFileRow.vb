'***********************************************************************
' Assembly         : Domain.Base
' Author           : Carlos Cordoba
' Created          : 09-07-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.Runtime.Serialization
Imports DevExpress.Spreadsheet

''' <summary>
''' clase para enviar las filas del archivo de excel
''' </summary>
''' <remarks></remarks>
<DataContract(IsReference:=True), Serializable(), KnownType(GetType(DevExpress.Spreadsheet.ErrorType))>
Public Class ImportFileRow

    <DataMember>
    Property IndexRow As Integer

    <DataMember>
    Property Row As List(Of Object)
End Class

''' <summary>
''' Encapsula los mètodos extendidos usados en los tipos de datos DevExpress.Spreadsheet
''' </summary>
Public Module SpreadsheetExtentions

    ''' <summary>
    ''' Convierte la fila en una lista de objetos :P
    ''' </summary>
    ''' <param name="objRow">Fila a convertir</param>
    ''' <returns>Lista de objetos</returns>
    <System.Runtime.CompilerServices.Extension>
    Public Function SpreadsheetRowToList(ByVal objRow As Object, colums As Integer) As List(Of Object)
        Dim res As New List(Of Object)()
        For x As Integer = 0 To colums - 1
            res.Add(objRow(x).Value.ToObject())
        Next
        Return res
    End Function

End Module