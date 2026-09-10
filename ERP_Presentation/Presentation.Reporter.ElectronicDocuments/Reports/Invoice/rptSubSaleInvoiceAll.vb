'***********************************************************************
' Assembly         : Presentation.Reporter.ElectronicDocuments
' Clase auxiliar para cargar layouts personalizados
' Adaptado para .NET Standard 2.0
'***********************************************************************

Imports System.IO
Imports Presentation.Reporter.ElectronicDocuments.Infrastructure

Namespace Reports

    ''' <summary>
    ''' Clase auxiliar para cargar layouts personalizados de reportes
    ''' </summary>
    Public Class rptSubSaleInvoiceAll

        ''' <summary>
        ''' Carga el layout personalizado de un subreporte
        ''' </summary>
        ''' <param name="_tag">Tag del reporte</param>
        ''' <param name="Name">Nombre del reporte</param>
        ''' <returns>Ruta del archivo .repx personalizado o Nothing</returns>
        Public Function LoadCustomLayout(_tag As Object, Name As String) As String
            If Not Directory.Exists(ConfigurationFile.Instance.ReportsPath) Then
                Return Nothing
            End If

            Dim nameRepDefault As String = Nothing
            Dim defaultPath = Path.Combine(ConfigurationFile.Instance.ReportsPath, _tag & "Repx.Default")
            If File.Exists(defaultPath) Then
                nameRepDefault = File.ReadAllText(defaultPath)?.Trim()
            End If
            If String.IsNullOrEmpty(nameRepDefault) Then
                nameRepDefault = "*"
            End If

            Dim pattern As String = $"{_tag}.{Name}.{nameRepDefault}.repx"
            Return Directory.GetFiles(ConfigurationFile.Instance.ReportsPath, pattern, SearchOption.TopDirectoryOnly)?.FirstOrDefault
        End Function

    End Class

End Namespace
