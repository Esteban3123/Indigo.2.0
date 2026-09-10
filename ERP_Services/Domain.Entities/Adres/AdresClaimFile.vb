'***********************************************************************
' Assembly         : Domain.Entities
' Feature          : Circular Externa 003 de 2026 ADRES
'***********************************************************************

Imports System.Runtime.Serialization

''' <summary>
''' Resultado de la generación de un formulario ADRES (FUR, FUR SERVICIOS,
''' FUR RG). Lleva el JSON ya serializado (con los nombres ADRES exactos)
''' y un DataSet plano listo para que la UI lo asigne a un GridControl y
''' lo exporte a XLSX con DevExpress.XtraPrinting.XlsxExportOptions.
''' </summary>
<DataContract(), Serializable()>
Public Class AdresClaimFile

    ''' <summary>Nombre del archivo sin extensión, p. ej. "SER12345678".</summary>
    <DataMember()>
    Public Property FileName As String

    ''' <summary>JSON ya serializado con la estructura ADRES.</summary>
    <DataMember()>
    Public Property JsonContent As String

    ''' <summary>DataSet con una tabla plana lista para exportar a XLSX.</summary>
    <DataMember()>
    Public Property ExcelData As DataSet

    ''' <summary>Conteo de registros incluidos.</summary>
    <DataMember()>
    Public Property RecordCount As Integer

    ''' <summary>Avisos no fatales (truncamientos, validaciones, etc.).</summary>
    <DataMember()>
    Public Property Warnings As List(Of String)

    Public Sub New()
        Me.Warnings = New List(Of String)()
    End Sub

End Class
