'***********************************************************************
' Assembly         : Presentation.Reporter.ElectronicDocuments
' Stub para subreporte de detalle de IVA (adaptado para .NET Standard 2.0)
'***********************************************************************

Imports DevExpress.XtraReports.UI

Namespace Presentation.Reporter

    ''' <summary>
    ''' Subreporte stub para detalle de IVA
    ''' Este es un placeholder - implementar la lógica según necesidades
    ''' </summary>
    Public Class rptInvoiceIvaDetail
        Inherits XtraReport

        Public Sub New()
            MyBase.New()
            InitializeComponent()
        End Sub

        Private Sub InitializeComponent()
            ' Configuración básica del reporte
            Me.Bands.Add(New DetailBand() With {.HeightF = 50, .Name = "Detail"})
            Me.Version = "20.1"
        End Sub

    End Class

End Namespace
