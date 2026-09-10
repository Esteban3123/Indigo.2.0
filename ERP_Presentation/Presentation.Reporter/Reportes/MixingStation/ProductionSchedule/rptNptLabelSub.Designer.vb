<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Public Class rptNptLabelSub
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
        Me.XrSubreportNptLabel = New DevExpress.XtraReports.UI.XRSubreport()
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
        Me.BottomMargin.HeightF = 31.5211!
        Me.BottomMargin.Name = "BottomMargin"
        '
        'Detail
        '
        Me.Detail.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrSubreportNptLabel})
        Me.Detail.HeightF = 395.2812!
        Me.Detail.Name = "Detail"
        Me.Detail.PageBreak = DevExpress.XtraReports.UI.PageBreak.AfterBand
        '
        'XrSubreportNptLabel
        '
        Me.XrSubreportNptLabel.LocationFloat = New DevExpress.Utils.PointFloat(0.00001589457!, 0!)
        Me.XrSubreportNptLabel.Name = "XrSubreportNptLabel"
        Me.XrSubreportNptLabel.ParameterBindings.Add(New DevExpress.XtraReports.UI.ParameterBinding("INDRequestPackageDetailStatusId", Nothing, "RequestPackageDetailStatusId"))
        Me.XrSubreportNptLabel.ParameterBindings.Add(New DevExpress.XtraReports.UI.ParameterBinding("Entity", Nothing, "Entity"))
        Me.XrSubreportNptLabel.ReportSource = New Presentation.Reporter.rptNptLabel()
        Me.XrSubreportNptLabel.SizeF = New System.Drawing.SizeF(799.0!, 395.2812!)
        '
        'BindingSource1
        '
        Me.BindingSource1.DataSource = GetType(Infrastructure.Data.Xpo.MixingStationRepository.ViewParenteralNutritionLabelCampaignXpo)
        '
        'INDCampaignDetailId
        '
        Me.INDCampaignDetailId.Description = "ID detalle de la campaña"
        Me.INDCampaignDetailId.Name = "INDCampaignDetailId"
        Me.INDCampaignDetailId.Type = GetType(Integer)
        Me.INDCampaignDetailId.ValueInfo = "0"
        Me.INDCampaignDetailId.Visible = False
        '
        'rptNptLabelSub
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.TopMargin, Me.BottomMargin, Me.Detail})
        Me.ComponentStorage.AddRange(New System.ComponentModel.IComponent() {Me.BindingSource1})
        Me.DataSource = Me.BindingSource1
        Me.Font = New DevExpress.Drawing.DXFont("Arial", 9.75!)
        Me.Margins = New DevExpress.Drawing.DXMargins(22, 29, 43, 32)
        Me.Parameters.AddRange(New DevExpress.XtraReports.Parameters.Parameter() {Me.INDCampaignDetailId})
        Me.Version = "20.1"
        CType(Me.BindingSource1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub

    Friend WithEvents TopMargin As DevExpress.XtraReports.UI.TopMarginBand
    Friend WithEvents BottomMargin As DevExpress.XtraReports.UI.BottomMarginBand
    Friend WithEvents Detail As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents XrSubreportNptLabel As DevExpress.XtraReports.UI.XRSubreport
    Friend WithEvents BindingSource1 As BindingSource
    Friend WithEvents INDCampaignDetailId As DevExpress.XtraReports.Parameters.Parameter
End Class
