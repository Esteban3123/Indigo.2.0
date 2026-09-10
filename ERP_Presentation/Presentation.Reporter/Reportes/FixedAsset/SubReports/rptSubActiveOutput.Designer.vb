<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class rptSubActiveOutput
    Inherits DevExpress.XtraReports.UI.XtraReport

    'XtraReport overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Detail = New DevExpress.XtraReports.UI.DetailBand()
        Me.INDOperatingUnit = New DevExpress.XtraReports.Parameters.Parameter()
        Me.TopMargin = New DevExpress.XtraReports.UI.TopMarginBand()
        Me.BottomMargin = New DevExpress.XtraReports.UI.BottomMarginBand()
        Me.BindingSource1 = New System.Windows.Forms.BindingSource()
        Me.XrSubreport1 = New DevExpress.XtraReports.UI.XRSubreport()
        Me.GroupHeader1 = New DevExpress.XtraReports.UI.GroupHeaderBand()
        CType(Me.BindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'Detail
        '
        Me.Detail.Expanded = False
        Me.Detail.HeightF = 100.0!
        Me.Detail.Name = "Detail"
        Me.Detail.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.Detail.PageBreak = DevExpress.XtraReports.UI.PageBreak.BeforeBand
        Me.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'INDOperatingUnit
        '
        Me.INDOperatingUnit.Description = "Parameter1"
        Me.INDOperatingUnit.Name = "INDOperatingUnit"
        Me.INDOperatingUnit.Type = GetType(Integer)
        Me.INDOperatingUnit.ValueInfo = "0"
        Me.INDOperatingUnit.Visible = False
        '
        'TopMargin
        '
        Me.TopMargin.HeightF = 23.0!
        Me.TopMargin.Name = "TopMargin"
        Me.TopMargin.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'BottomMargin
        '
        Me.BottomMargin.HeightF = 25.0!
        Me.BottomMargin.Name = "BottomMargin"
        Me.BottomMargin.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'BindingSource1
        '
        Me.BindingSource1.DataSource = GetType(Infrastructure.Data.Xpo.FixedAssetRepository.FixedAssetFixedAssetActiveOutputDetailReportXpo)
        '
        'XrSubreport1
        '
        Me.XrSubreport1.LocationFloat = New DevExpress.Utils.PointFloat(0.0!, 0.0!)
        Me.XrSubreport1.Name = "XrSubreport1"
        Me.XrSubreport1.ParameterBindings.Add(New DevExpress.XtraReports.UI.ParameterBinding("INDIdActiveOutput", Nothing, "FixedAssetActiveOutputId.Id"))
        Me.XrSubreport1.ParameterBindings.Add(New DevExpress.XtraReports.UI.ParameterBinding("INDOperationUnit", Me.INDOperatingUnit))
        Me.XrSubreport1.ReportSource = New Presentation.Reporter.rptFixedAssetActiveOutput()
        Me.XrSubreport1.SizeF = New System.Drawing.SizeF(802.0!, 100.0!)
        '
        'GroupHeader1
        '
        Me.GroupHeader1.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrSubreport1})
        Me.GroupHeader1.GroupFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("FixedAssetActiveOutputId.Code", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})
        Me.GroupHeader1.HeightF = 100.0!
        Me.GroupHeader1.Name = "GroupHeader1"
        '
        'rptSubActiveOutput
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail, Me.TopMargin, Me.BottomMargin, Me.GroupHeader1})
        Me.DataSource = Me.BindingSource1
        Me.Margins = New DevExpress.Drawing.DXMargins(25, 23, 23, 25)
        Me.Parameters.AddRange(New DevExpress.XtraReports.Parameters.Parameter() {Me.INDOperatingUnit})
        Me.ScriptLanguage = DevExpress.XtraReports.ScriptLanguage.VisualBasic
        Me.Version = "15.1"
        CType(Me.BindingSource1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents Detail As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents TopMargin As DevExpress.XtraReports.UI.TopMarginBand
    Friend WithEvents BottomMargin As DevExpress.XtraReports.UI.BottomMarginBand
    Friend WithEvents XrSubreport1 As DevExpress.XtraReports.UI.XRSubreport
    Friend WithEvents BindingSource1 As System.Windows.Forms.BindingSource
    Friend WithEvents INDOperatingUnit As DevExpress.XtraReports.Parameters.Parameter
    Friend WithEvents GroupHeader1 As DevExpress.XtraReports.UI.GroupHeaderBand
End Class
