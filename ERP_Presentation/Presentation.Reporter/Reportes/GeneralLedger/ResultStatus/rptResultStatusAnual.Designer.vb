<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Public Class rptResultStatusAnual
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
        Dim XrSummary1 As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(rptResultStatusAnual))
        Me.Detail = New DevExpress.XtraReports.UI.DetailBand()
        Me.TopMargin = New DevExpress.XtraReports.UI.TopMarginBand()
        Me.BottomMargin = New DevExpress.XtraReports.UI.BottomMarginBand()
        Me.INDGhClass = New DevExpress.XtraReports.UI.GroupHeaderBand()
        Me.XrTable10 = New DevExpress.XtraReports.UI.XRTable()
        Me.XrTableRow10 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.XrTableCell28 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell29 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell30 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.INDBalanceCurrent = New DevExpress.XtraReports.UI.CalculatedField()
        Me.INDBalanceCurrentIncome = New DevExpress.XtraReports.UI.CalculatedField()
        Me.INDBalanceCurrentExpensesCosts = New DevExpress.XtraReports.UI.CalculatedField()
        Me.INDBalancePreviousIncome = New DevExpress.XtraReports.UI.CalculatedField()
        Me.INDBalancePreviousExpensesCosts = New DevExpress.XtraReports.UI.CalculatedField()
        Me.INDTotalExercise = New DevExpress.XtraReports.UI.CalculatedField()
        Me.INDDescriptionExercise = New DevExpress.XtraReports.UI.CalculatedField()
        Me.INDBalanceCurrentAccountLvl1 = New DevExpress.XtraReports.UI.CalculatedField()
        Me.INDPreviousBalanceAccountLvl1 = New DevExpress.XtraReports.UI.CalculatedField()
        Me.INDGhSubAuxiliar = New DevExpress.XtraReports.UI.GroupHeaderBand()
        Me.DataSet11 = New Presentation.Reporter.DataSet1()
        Me.SP_ReportResulStatusTableAdapter = New Presentation.Reporter.DataSet1TableAdapters.SP_ReportResulStatusTableAdapter()
        Me.IncludedAccountNumber = New DevExpress.XtraReports.Parameters.Parameter()
        CType(Me.XrTable10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataSet11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'Detail
        '
        Me.Detail.HeightF = 44.37501!
        Me.Detail.Name = "Detail"
        Me.Detail.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'TopMargin
        '
        Me.TopMargin.Name = "TopMargin"
        Me.TopMargin.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'BottomMargin
        '
        Me.BottomMargin.Name = "BottomMargin"
        Me.BottomMargin.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'INDGhClass
        '
        Me.INDGhClass.GroupFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("classCode", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})
        Me.INDGhClass.HeightF = 0!
        Me.INDGhClass.Name = "INDGhClass"
        '
        'XrTable10
        '
        Me.XrTable10.Font = New DevExpress.Drawing.DXFont("Arial", 7.0!)
        Me.XrTable10.LocationFloat = New DevExpress.Utils.PointFloat(0!, 0!)
        Me.XrTable10.Name = "XrTable10"
        Me.XrTable10.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.XrTableRow10})
        Me.XrTable10.SizeF = New System.Drawing.SizeF(799.0001!, 15.0!)
        Me.XrTable10.StylePriority.UseFont = False
        Me.XrTable10.StylePriority.UseTextAlignment = False
        Me.XrTable10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrTableRow10
        '
        Me.XrTableRow10.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell28, Me.XrTableCell29, Me.XrTableCell30})
        Me.XrTableRow10.Name = "XrTableRow10"
        Me.XrTableRow10.Weight = 1.0R
        '
        'XrTableCell28
        '
        Me.XrTableCell28.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SP_ReportResulStatus.mainAccountCode")})
        Me.XrTableCell28.Name = "XrTableCell28"
        Me.XrTableCell28.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100.0!)
        Me.XrTableCell28.StylePriority.UsePadding = False
        Me.XrTableCell28.Weight = 0.34167722304879833R
        '
        'XrTableCell29
        '
        Me.XrTableCell29.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SP_ReportResulStatus.mainAccountNameByAnual")})
        Me.XrTableCell29.Font = New DevExpress.Drawing.DXFont("Arial", 7.0!)
        Me.XrTableCell29.Name = "XrTableCell29"
        Me.XrTableCell29.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100.0!)
        Me.XrTableCell29.StylePriority.UseFont = False
        Me.XrTableCell29.StylePriority.UsePadding = False
        Me.XrTableCell29.Weight = 2.0800997595471671R
        '
        'XrTableCell30
        '
        Me.XrTableCell30.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SP_ReportResulStatus.newBalance")})
        Me.XrTableCell30.Name = "XrTableCell30"
        Me.XrTableCell30.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100.0!)
        Me.XrTableCell30.StylePriority.UsePadding = False
        Me.XrTableCell30.StylePriority.UseTextAlignment = False
        XrSummary1.FormatString = "{0:c2}"
        XrSummary1.Func = DevExpress.XtraReports.UI.SummaryFunc.Custom
        XrSummary1.IgnoreNullValues = True
        XrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.XrTableCell30.Summary = XrSummary1
        Me.XrTableCell30.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        Me.XrTableCell30.Weight = 0.578223463186709R
        '
        'INDBalanceCurrent
        '
        Me.INDBalanceCurrent.DataMember = "SP_ReportResulStatus"
        Me.INDBalanceCurrent.Expression = "[valueDebitMovement] - [valueCreditMovement]"
        Me.INDBalanceCurrent.Name = "INDBalanceCurrent"
        '
        'INDBalanceCurrentIncome
        '
        Me.INDBalanceCurrentIncome.DataMember = "SP_ReportResulStatus"
        Me.INDBalanceCurrentIncome.Expression = "Iif([nature] = 'Credito' And [mainAccountClassType] = 2, [INDBalanceCurrentAccoun" &
    "tLvl1] ,0 )"
        Me.INDBalanceCurrentIncome.Name = "INDBalanceCurrentIncome"
        '
        'INDBalanceCurrentExpensesCosts
        '
        Me.INDBalanceCurrentExpensesCosts.DataMember = "SP_ReportResulStatus"
        Me.INDBalanceCurrentExpensesCosts.Expression = "Iif([nature] = 'Debito' And [mainAccountClassType] = 2, [INDBalanceCurrentAccount" &
    "Lvl1] ,0 )"
        Me.INDBalanceCurrentExpensesCosts.Name = "INDBalanceCurrentExpensesCosts"
        '
        'INDBalancePreviousIncome
        '
        Me.INDBalancePreviousIncome.DataMember = "SP_ReportResulStatus"
        Me.INDBalancePreviousIncome.Expression = "Iif([nature] = 'Credito' And [mainAccountClassType] = 2, [INDPreviousBalanceAccou" &
    "ntLvl1]  ,0 )"
        Me.INDBalancePreviousIncome.Name = "INDBalancePreviousIncome"
        '
        'INDBalancePreviousExpensesCosts
        '
        Me.INDBalancePreviousExpensesCosts.DataMember = "SP_ReportResulStatus"
        Me.INDBalancePreviousExpensesCosts.Expression = "Iif([nature] = 'Debito' And [mainAccountClassType] = 2, [INDPreviousBalanceAccoun" &
    "tLvl1] ,0 )"
        Me.INDBalancePreviousExpensesCosts.Name = "INDBalancePreviousExpensesCosts"
        '
        'INDTotalExercise
        '
        Me.INDTotalExercise.DataMember = "SP_ReportResulStatus"
        Me.INDTotalExercise.Expression = "([INDBalanceCurrentIncome]-[INDBalanceCurrentExpensesCosts])-([INDBalancePrevious" &
    "Income]-[INDBalancePreviousExpensesCosts])"
        Me.INDTotalExercise.Name = "INDTotalExercise"
        '
        'INDDescriptionExercise
        '
        Me.INDDescriptionExercise.DataMember = "SP_ReportResulStatus"
        Me.INDDescriptionExercise.Expression = "Iif([].Sum([INDTotalExercise]) < 0, 'DEFICIT DEL EJERCICIO' ,'SUPERAVIT DEL EJERC" &
    "ICIO' )"
        Me.INDDescriptionExercise.Name = "INDDescriptionExercise"
        '
        'INDBalanceCurrentAccountLvl1
        '
        Me.INDBalanceCurrentAccountLvl1.DataMember = "SP_ReportResulStatus"
        Me.INDBalanceCurrentAccountLvl1.Expression = "Iif([mainAccountLevel] = 1, [INDBalanceCurrent] , 0)"
        Me.INDBalanceCurrentAccountLvl1.Name = "INDBalanceCurrentAccountLvl1"
        '
        'INDPreviousBalanceAccountLvl1
        '
        Me.INDPreviousBalanceAccountLvl1.DataMember = "SP_ReportResulStatus"
        Me.INDPreviousBalanceAccountLvl1.Expression = "Iif([mainAccountLevel] = 1, [previousBalance] , 0)"
        Me.INDPreviousBalanceAccountLvl1.Name = "INDPreviousBalanceAccountLvl1"
        '
        'INDGhSubAuxiliar
        '
        Me.INDGhSubAuxiliar.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrTable10})
        Me.INDGhSubAuxiliar.GroupFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("mainAccountCode", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})
        Me.INDGhSubAuxiliar.HeightF = 15.0!
        Me.INDGhSubAuxiliar.Level = 1
        Me.INDGhSubAuxiliar.Name = "INDGhSubAuxiliar"
        '
        'DataSet11
        '
        Me.DataSet11.DataSetName = "DataSet1"
        Me.DataSet11.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'SP_ReportResulStatusTableAdapter
        '
        Me.SP_ReportResulStatusTableAdapter.ClearBeforeFill = True
        '
        'IncludedAccountNumber
        '
        Me.IncludedAccountNumber.Description = "Permite saber si se muestra o no la columna de no. cuenta"
        Me.IncludedAccountNumber.Name = "IncludedAccountNumber"
        Me.IncludedAccountNumber.Type = GetType(Boolean)
        Me.IncludedAccountNumber.ValueInfo = "True"
        Me.IncludedAccountNumber.Visible = False
        '
        'rptResultStatusAnual
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail, Me.TopMargin, Me.BottomMargin, Me.INDGhClass, Me.INDGhSubAuxiliar})
        Me.CalculatedFields.AddRange(New DevExpress.XtraReports.UI.CalculatedField() {Me.INDBalanceCurrent, Me.INDBalanceCurrentIncome, Me.INDBalanceCurrentExpensesCosts, Me.INDBalancePreviousIncome, Me.INDBalancePreviousExpensesCosts, Me.INDTotalExercise, Me.INDDescriptionExercise, Me.INDBalanceCurrentAccountLvl1, Me.INDPreviousBalanceAccountLvl1})
        Me.ComponentStorage.AddRange(New System.ComponentModel.IComponent() {Me.DataSet11})
        Me.DataMember = "SP_ReportResulStatus"
        Me.DataSource = Me.DataSet11
        Me.DataSourceSchema = resources.GetString("$this.DataSourceSchema")
        Me.Font = New DevExpress.Drawing.DXFont("Arial", 9.5!)
        Me.Margins = New DevExpress.Drawing.DXMargins(26, 25, 100, 100)
        Me.Parameters.AddRange(New DevExpress.XtraReports.Parameters.Parameter() {Me.IncludedAccountNumber})
        Me.ScriptLanguage = DevExpress.XtraReports.ScriptLanguage.VisualBasic
        Me.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.Version = "19.1"
        CType(Me.XrTable10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataSet11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents Detail As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents TopMargin As DevExpress.XtraReports.UI.TopMarginBand
    Friend WithEvents BottomMargin As DevExpress.XtraReports.UI.BottomMarginBand
    Friend WithEvents INDGhClass As DevExpress.XtraReports.UI.GroupHeaderBand
    Friend WithEvents XrTable10 As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents XrTableRow10 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell28 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell29 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell30 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents INDBalanceCurrent As DevExpress.XtraReports.UI.CalculatedField
    Friend WithEvents INDBalanceCurrentIncome As DevExpress.XtraReports.UI.CalculatedField
    Friend WithEvents INDBalanceCurrentExpensesCosts As DevExpress.XtraReports.UI.CalculatedField
    Friend WithEvents INDBalancePreviousIncome As DevExpress.XtraReports.UI.CalculatedField
    Friend WithEvents INDBalancePreviousExpensesCosts As DevExpress.XtraReports.UI.CalculatedField
    Friend WithEvents INDTotalExercise As DevExpress.XtraReports.UI.CalculatedField
    Friend WithEvents INDDescriptionExercise As DevExpress.XtraReports.UI.CalculatedField
    Friend WithEvents INDBalanceCurrentAccountLvl1 As DevExpress.XtraReports.UI.CalculatedField
    Friend WithEvents INDPreviousBalanceAccountLvl1 As DevExpress.XtraReports.UI.CalculatedField
    Friend WithEvents INDGhSubAuxiliar As DevExpress.XtraReports.UI.GroupHeaderBand
    Friend WithEvents DataSet11 As DataSet1
    Friend WithEvents SP_ReportResulStatusTableAdapter As DataSet1TableAdapters.SP_ReportResulStatusTableAdapter
    Friend WithEvents IncludedAccountNumber As DevExpress.XtraReports.Parameters.Parameter
End Class
