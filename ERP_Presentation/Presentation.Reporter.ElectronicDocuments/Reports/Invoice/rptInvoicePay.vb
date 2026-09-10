'***********************************************************************
' Assembly         : Presentation.Reporter.ElectronicDocuments
' Stub para subreporte de pagos (adaptado para .NET Standard 2.0)
'***********************************************************************

Imports DevExpress.XtraReports.UI

Namespace Presentation.Reporter

    ''' <summary>
    ''' Subreporte stub para información de pagos
    ''' Este es un placeholder - implementar la lógica según necesidades
    ''' </summary>
    Public Class rptInvoicePay
        Inherits XtraReport

        Private INDPrAccountReceivableId As New DevExpress.XtraReports.Parameters.Parameter()
        Private INDfromSaleInvoice As New DevExpress.XtraReports.Parameters.Parameter()

        Public Sub New()
            MyBase.New()
            InitializeComponent()
        End Sub

        Private Sub InitializeComponent()
            ' Configurar parámetros
            INDPrAccountReceivableId.Name = "INDPrAccountReceivableId"
            INDPrAccountReceivableId.Type = GetType(Integer)
            INDPrAccountReceivableId.Value = 0

            INDfromSaleInvoice.Name = "INDfromSaleInvoice"
            INDfromSaleInvoice.Type = GetType(Boolean)
            INDfromSaleInvoice.Value = False

            Me.Parameters.Add(INDPrAccountReceivableId)
            Me.Parameters.Add(INDfromSaleInvoice)

            ' Configuración básica del reporte
            Me.Bands.Add(New DetailBand() With {.HeightF = 50, .Name = "Detail"})
            Me.Version = "20.1"
        End Sub

    End Class

End Namespace
