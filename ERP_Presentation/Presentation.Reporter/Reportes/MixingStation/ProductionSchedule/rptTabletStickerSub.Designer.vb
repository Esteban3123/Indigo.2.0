<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Public Class rptTabletStickerSub
    Inherits DevExpress.XtraReports.UI.XtraReport

    'XtraReport overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Designer
    'It can be modified using the Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.TopMargin = New DevExpress.XtraReports.UI.TopMarginBand()
        Me.BottomMargin = New DevExpress.XtraReports.UI.BottomMarginBand()
        Me.Detail = New DevExpress.XtraReports.UI.DetailBand()
        Me.XrSubreportTabletSticker = New DevExpress.XtraReports.UI.XRSubreport()
        Me.BindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
        Me.INDCampaignDetailId = New DevExpress.XtraReports.Parameters.Parameter()
        CType(Me.BindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'TopMargin
        '
        Me.TopMargin.HeightF = 43.0!
        Me.TopMargin.Name = "TopMargin"
        '
        'BottomMargin
        '
        Me.BottomMargin.HeightF = 106.0!
        Me.BottomMargin.Name = "BottomMargin"
        '
        'Detail
        '
        Me.Detail.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrSubreportTabletSticker})
        Me.Detail.HeightF = 422.3645!
        Me.Detail.Name = "Detail"
        '
        'XrSubreportTabletSticker
        '
        Me.XrSubreportTabletSticker.LocationFloat = New DevExpress.Utils.PointFloat(0.00001589457!, 23.95833!)
        Me.XrSubreportTabletSticker.Name = "XrSubreportTabletSticker"
        Me.XrSubreportTabletSticker.ParameterBindings.Add(New DevExpress.XtraReports.UI.ParameterBinding("Id", Nothing, "Id"))
        Me.XrSubreportTabletSticker.ReportSource = New Presentation.Reporter.rptTabletSticker()
        Me.XrSubreportTabletSticker.SizeF = New System.Drawing.SizeF(799.0!, 248.9583!)
        '
        'BindingSource1
        '
        Me.BindingSource1.DataSource = GetType(Infrastructure.Data.Xpo.MixingStationRepository.CampaignDetailXpo)
        '
        'INDCampaignDetailId
        '
        Me.INDCampaignDetailId.Description = "ID del detalle de la campaña"
        Me.INDCampaignDetailId.Name = "INDCampaignDetailId"
        Me.INDCampaignDetailId.Type = GetType(Integer)
        Me.INDCampaignDetailId.ValueInfo = "0"
        Me.INDCampaignDetailId.Visible = False
        '
        'rptTabletStickerSub
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.TopMargin, Me.BottomMargin, Me.Detail})
        Me.ComponentStorage.AddRange(New System.ComponentModel.IComponent() {Me.BindingSource1})
        Me.DataSource = Me.BindingSource1
        Me.Font = New DevExpress.Drawing.DXFont("Arial", 9.75!)
        Me.Margins = New DevExpress.Drawing.DXMargins(22, 29, 43, 106)
        Me.Parameters.AddRange(New DevExpress.XtraReports.Parameters.Parameter() {Me.INDCampaignDetailId})
        Me.Version = "20.1"
        CType(Me.BindingSource1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub

    Friend WithEvents TopMargin As DevExpress.XtraReports.UI.TopMarginBand
    Friend WithEvents BottomMargin As DevExpress.XtraReports.UI.BottomMarginBand
    Friend WithEvents Detail As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents XrSubreportTabletSticker As DevExpress.XtraReports.UI.XRSubreport
    Friend WithEvents BindingSource1 As BindingSource
    Friend WithEvents INDCampaignDetailId As DevExpress.XtraReports.Parameters.Parameter
End Class
