<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmUnemployedLiquidation
    Inherits Presentation.Controls.FormBase

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim GridLevelNode1 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode()
        Dim AppearanceObject1 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject2 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim EditorButtonImageOptions1 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim AppearanceObject3 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject4 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim EditorButtonImageOptions2 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject5 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject6 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject7 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject8 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmUnemployedLiquidation))
        Dim SuperToolTip1 As DevExpress.Utils.SuperToolTip = New DevExpress.Utils.SuperToolTip()
        Dim ToolTipItem1 As DevExpress.Utils.ToolTipItem = New DevExpress.Utils.ToolTipItem()
        Me.INDgvUnemployedLiquidationDetail = New DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView()
        Me.GridBand1 = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.INDColULDMonth = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.INDColULDWorkedDays = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.INDColULDSanctionDays = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.INDColULDUnemployedAverage = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.INDColULDUnemployedInterestAverage = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.INDgcLiquidationResult = New DevExpress.XtraGrid.GridControl()
        Me.INDgvLiquidationResult = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColEmployeeNit = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColEmployeeName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColState = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDRepPicEdit = New DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit()
        Me.INDColMessages = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDRepPopupConEdit = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.INDPopConCtrMessages = New DevExpress.XtraEditors.PopupContainerControl()
        Me.INDgcMessages = New DevExpress.XtraGrid.GridControl()
        Me.INDgvMessages = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColMessage = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColTotalUnemployed = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColUnemployedInterestTotal = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDTotalProvisionsUnemployed = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDTotalProvisionsUnemployedNotSanctions = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColDateInitial = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColDateEnding = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColWorkedDays = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColSanctionsDays = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColAverageMonthNumber = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColConfirm = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDRepRgSanctions = New DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup()
        Me.RepositoryItemPopupContainerEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.INDLyUnemployed = New DevExpress.XtraLayout.LayoutControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.CtrNavigation1 = New Presentation.Controls.CtrNavigation()
        Me.INDrgSanctionDays = New DevExpress.XtraEditors.RadioGroup()
        Me.INDrgConfirm = New DevExpress.XtraEditors.RadioGroup()
        Me.INDCbeYear = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.INDtxtResolutionNumber = New DevExpress.XtraEditors.TextEdit()
        Me.INDdeAuthorizationDate = New DevExpress.XtraEditors.DateEdit()
        Me.INDmeeUnemployedRetirementReason = New DevExpress.XtraEditors.MemoExEdit()
        Me.INDdeUnemployedLiquidationDate = New DevExpress.XtraEditors.DateEdit()
        Me.INDSleEmployee = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColNitEmployee = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColNameEmployee = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgcGroups = New DevExpress.XtraGrid.GridControl()
        Me.INDgvGroups = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColApply = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColGroupCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColGroupName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDSleCompany = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColCompanyCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColCompanyName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrgLiquidate = New DevExpress.XtraEditors.RadioGroup()
        Me.INDgleUnemployedInterestPaidWithPayroll = New Presentation.Controls.CtrYesNo()
        Me.CtrYesNo4View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDLyGrUnemployed = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyGrYearly = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemCompany = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemGroups = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemYear = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDliUnemployedInterestPaidWithPayroll = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyGrPartial = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemEmployee = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemUnemployedLiquidationDate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyUnemployedRetirementReason = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemAuthorizationDate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemResosultionNumber = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyGrLiquidationResult = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemGoBack = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyItemLiquidationResult = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemTextResult = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.INDlyGrLiquidationForm = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemSanctionDays = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemLiquidate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemConfirm = New DevExpress.XtraLayout.LayoutControlItem()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoRadioGroup1 = New Presentation.Controls.IndigoRadioGroup(Me.components)
        Me.IndigoComboBoxEdit1 = New Presentation.Controls.IndigoComboBoxEdit(Me.components)
        Me.IndigoDate1 = New Presentation.Controls.IndigoDate(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoTextEdit11 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.GridColumn564 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.IndigoGridView11 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.RepositoryItemPopupContainerEdit11 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgvUnemployedLiquidationDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgcLiquidationResult, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgvLiquidationResult, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDRepPicEdit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDRepPopupConEdit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPopConCtrMessages, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPopConCtrMessages.SuspendLayout()
        CType(Me.INDgcMessages, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgvMessages, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDRepRgSanctions, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyUnemployed, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLyUnemployed.SuspendLayout()
        CType(Me.INDrgSanctionDays.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrgConfirm.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDCbeYear.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtResolutionNumber.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdeAuthorizationDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdeAuthorizationDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDmeeUnemployedRetirementReason.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdeUnemployedLiquidationDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdeUnemployedLiquidationDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleEmployee.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgcGroups, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgvGroups, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleCompany.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrgLiquidate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgleUnemployedInterestPaidWithPayroll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgleUnemployedInterestPaidWithPayroll.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrYesNo4View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyGrUnemployed, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyGrYearly, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemCompany, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemGroups, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemYear, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliUnemployedInterestPaidWithPayroll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyGrPartial, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemEmployee, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemUnemployedLiquidationDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyUnemployedRetirementReason, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemAuthorizationDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemResosultionNumber, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyGrLiquidationResult, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemGoBack, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyItemLiquidationResult, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemTextResult, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyGrLiquidationForm, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemSanctionDays, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemLiquidate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemConfirm, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoRadioGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoComboBoxEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit11, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDLyUnemployed)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 136)
        Me.INDPanelControlBase.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDPanelControlBase.Padding = New System.Windows.Forms.Padding(0, 6, 0, 0)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1008, 605)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Location = New System.Drawing.Point(0, 6)
        Me.ToolBars.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ToolBars.Size = New System.Drawing.Size(1008, 130)
        '
        'BarraBotones
        '
        Me.BarraBotones.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.BarraBotones.Size = New System.Drawing.Size(1008, 130)
        '
        'INDgvUnemployedLiquidationDetail
        '
        Me.INDgvUnemployedLiquidationDetail.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDgvUnemployedLiquidationDetail.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDgvUnemployedLiquidationDetail.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDgvUnemployedLiquidationDetail.Appearance.FocusedRow.Options.UseFont = True
        Me.INDgvUnemployedLiquidationDetail.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDgvUnemployedLiquidationDetail.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvUnemployedLiquidationDetail.Appearance.GroupRow.Options.UseFont = True
        Me.INDgvUnemployedLiquidationDetail.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvUnemployedLiquidationDetail.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDgvUnemployedLiquidationDetail.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDgvUnemployedLiquidationDetail.Appearance.Row.Options.UseFont = True
        Me.INDgvUnemployedLiquidationDetail.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDgvUnemployedLiquidationDetail.Appearance.ViewCaption.Options.UseFont = True
        Me.INDgvUnemployedLiquidationDetail.Bands.AddRange(New DevExpress.XtraGrid.Views.BandedGrid.GridBand() {Me.GridBand1})
        Me.INDgvUnemployedLiquidationDetail.Columns.AddRange(New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn() {Me.INDColULDMonth, Me.INDColULDWorkedDays, Me.INDColULDSanctionDays, Me.INDColULDUnemployedAverage, Me.INDColULDUnemployedInterestAverage})
        Me.INDgvUnemployedLiquidationDetail.GridControl = Me.INDgcLiquidationResult
        Me.INDgvUnemployedLiquidationDetail.Name = "INDgvUnemployedLiquidationDetail"
        Me.INDgvUnemployedLiquidationDetail.OptionsBehavior.Editable = False
        Me.INDgvUnemployedLiquidationDetail.OptionsCustomization.ShowBandsInCustomizationForm = False
        Me.INDgvUnemployedLiquidationDetail.OptionsMenu.EnableColumnMenu = False
        Me.INDgvUnemployedLiquidationDetail.OptionsView.EnableAppearanceEvenRow = True
        Me.INDgvUnemployedLiquidationDetail.OptionsView.EnableAppearanceOddRow = True
        Me.INDgvUnemployedLiquidationDetail.OptionsView.ShowAutoFilterRow = True
        Me.INDgvUnemployedLiquidationDetail.OptionsView.ShowBands = False
        Me.INDgvUnemployedLiquidationDetail.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDgvUnemployedLiquidationDetail, False)
        Me.IndigoGridView11.SetTemaIndigoMetro(Me.INDgvUnemployedLiquidationDetail, False)
        Me.INDgvUnemployedLiquidationDetail.ViewCaption = "Meses Liquidados"
        '
        'GridBand1
        '
        Me.GridBand1.Caption = " "
        Me.GridBand1.Columns.Add(Me.INDColULDMonth)
        Me.GridBand1.Columns.Add(Me.INDColULDWorkedDays)
        Me.GridBand1.Columns.Add(Me.INDColULDSanctionDays)
        Me.GridBand1.Columns.Add(Me.INDColULDUnemployedAverage)
        Me.GridBand1.Columns.Add(Me.INDColULDUnemployedInterestAverage)
        Me.GridBand1.Name = "GridBand1"
        Me.GridBand1.VisibleIndex = 0
        Me.GridBand1.Width = 628
        '
        'INDColULDMonth
        '
        Me.INDColULDMonth.AppearanceCell.Options.UseTextOptions = True
        Me.INDColULDMonth.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.INDColULDMonth.Caption = "Mes"
        Me.INDColULDMonth.FieldName = "Month"
        Me.INDColULDMonth.Name = "INDColULDMonth"
        Me.INDColULDMonth.OptionsColumn.AllowFocus = False
        Me.INDColULDMonth.Visible = True
        Me.INDColULDMonth.Width = 39
        '
        'INDColULDWorkedDays
        '
        Me.INDColULDWorkedDays.AppearanceCell.Options.UseTextOptions = True
        Me.INDColULDWorkedDays.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDColULDWorkedDays.Caption = "N° Días"
        Me.INDColULDWorkedDays.FieldName = "WorkedDays"
        Me.INDColULDWorkedDays.Name = "INDColULDWorkedDays"
        Me.INDColULDWorkedDays.OptionsColumn.AllowFocus = False
        Me.INDColULDWorkedDays.Visible = True
        Me.INDColULDWorkedDays.Width = 68
        '
        'INDColULDSanctionDays
        '
        Me.INDColULDSanctionDays.AppearanceCell.Options.UseTextOptions = True
        Me.INDColULDSanctionDays.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDColULDSanctionDays.Caption = "Días Sanción"
        Me.INDColULDSanctionDays.FieldName = "SanctionsDays"
        Me.INDColULDSanctionDays.Name = "INDColULDSanctionDays"
        Me.INDColULDSanctionDays.OptionsColumn.AllowFocus = False
        Me.INDColULDSanctionDays.Visible = True
        Me.INDColULDSanctionDays.Width = 127
        '
        'INDColULDUnemployedAverage
        '
        Me.INDColULDUnemployedAverage.Caption = "Provisión De cesantía"
        Me.INDColULDUnemployedAverage.DisplayFormat.FormatString = "C2"
        Me.INDColULDUnemployedAverage.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.INDColULDUnemployedAverage.FieldName = "UnemployedAverage"
        Me.INDColULDUnemployedAverage.Name = "INDColULDUnemployedAverage"
        Me.INDColULDUnemployedAverage.OptionsColumn.AllowFocus = False
        Me.INDColULDUnemployedAverage.Visible = True
        Me.INDColULDUnemployedAverage.Width = 173
        '
        'INDColULDUnemployedInterestAverage
        '
        Me.INDColULDUnemployedInterestAverage.Caption = "Provisión Interes Cesantía"
        Me.INDColULDUnemployedInterestAverage.DisplayFormat.FormatString = "C2"
        Me.INDColULDUnemployedInterestAverage.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.INDColULDUnemployedInterestAverage.FieldName = "UnemployedInterestAverage"
        Me.INDColULDUnemployedInterestAverage.Name = "INDColULDUnemployedInterestAverage"
        Me.INDColULDUnemployedInterestAverage.OptionsColumn.AllowFocus = False
        Me.INDColULDUnemployedInterestAverage.Visible = True
        Me.INDColULDUnemployedInterestAverage.Width = 221
        '
        'INDgcLiquidationResult
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcLiquidationResult, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcLiquidationResult, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDgcLiquidationResult, True)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcLiquidationResult, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcLiquidationResult, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcLiquidationResult, False)
        GridLevelNode1.LevelTemplate = Me.INDgvUnemployedLiquidationDetail
        GridLevelNode1.RelationName = "UnemployedLiquidationDetail"
        Me.INDgcLiquidationResult.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode1})
        Me.INDgcLiquidationResult.Location = New System.Drawing.Point(24, 258)
        Me.INDgcLiquidationResult.MainView = Me.INDgvLiquidationResult
        Me.INDgcLiquidationResult.MaximumSize = New System.Drawing.Size(1024, 0)
        Me.INDgcLiquidationResult.MinimumSize = New System.Drawing.Size(1024, 0)
        Me.INDgcLiquidationResult.Name = "INDgcLiquidationResult"
        Me.INDgcLiquidationResult.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDRepPicEdit, Me.INDRepPopupConEdit, Me.INDRepRgSanctions})
        Me.INDgcLiquidationResult.Size = New System.Drawing.Size(1024, 296)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcLiquidationResult, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.INDgcLiquidationResult.TabIndex = 18
        Me.INDgcLiquidationResult.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDgvLiquidationResult, Me.INDgvUnemployedLiquidationDetail})
        '
        'INDgvLiquidationResult
        '
        Me.INDgvLiquidationResult.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDgvLiquidationResult.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDgvLiquidationResult.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDgvLiquidationResult.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDgvLiquidationResult.Appearance.FocusedRow.Options.UseFont = True
        Me.INDgvLiquidationResult.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDgvLiquidationResult.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvLiquidationResult.Appearance.GroupRow.Options.UseFont = True
        Me.INDgvLiquidationResult.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvLiquidationResult.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDgvLiquidationResult.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDgvLiquidationResult.Appearance.Row.Options.UseFont = True
        Me.INDgvLiquidationResult.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDgvLiquidationResult.Appearance.ViewCaption.Options.UseFont = True
        Me.INDgvLiquidationResult.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColEmployeeNit, Me.INDColEmployeeName, Me.INDColState, Me.INDColMessages, Me.INDColTotalUnemployed, Me.INDColUnemployedInterestTotal, Me.INDTotalProvisionsUnemployed, Me.INDTotalProvisionsUnemployedNotSanctions, Me.INDColDateInitial, Me.INDColDateEnding, Me.INDColWorkedDays, Me.INDColSanctionsDays, Me.INDColAverageMonthNumber, Me.INDColConfirm})
        Me.INDgvLiquidationResult.GridControl = Me.INDgcLiquidationResult
        Me.INDgvLiquidationResult.Name = "INDgvLiquidationResult"
        Me.INDgvLiquidationResult.OptionsCustomization.AllowColumnMoving = False
        Me.INDgvLiquidationResult.OptionsMenu.EnableColumnMenu = False
        Me.INDgvLiquidationResult.OptionsView.EnableAppearanceEvenRow = True
        Me.INDgvLiquidationResult.OptionsView.EnableAppearanceOddRow = True
        Me.INDgvLiquidationResult.OptionsView.ShowAutoFilterRow = True
        Me.INDgvLiquidationResult.OptionsView.ShowFooter = True
        Me.INDgvLiquidationResult.OptionsView.ShowGroupPanel = False
        Me.INDgvLiquidationResult.Tag = 300
        Me.IndigoGridView11.SetTemaIndigoMetro(Me.INDgvLiquidationResult, False)
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDgvLiquidationResult, False)
        '
        'INDColEmployeeNit
        '
        Me.INDColEmployeeNit.Caption = "Nit"
        Me.INDColEmployeeNit.FieldName = "Employee.ThirdParty.Nit"
        Me.INDColEmployeeNit.Name = "INDColEmployeeNit"
        Me.INDColEmployeeNit.OptionsColumn.AllowEdit = False
        Me.INDColEmployeeNit.OptionsColumn.AllowFocus = False
        Me.INDColEmployeeNit.Visible = True
        Me.INDColEmployeeNit.VisibleIndex = 0
        Me.INDColEmployeeNit.Width = 60
        '
        'INDColEmployeeName
        '
        Me.INDColEmployeeName.Caption = "Empleado"
        Me.INDColEmployeeName.FieldName = "Employee.ThirdParty.Name"
        Me.INDColEmployeeName.Name = "INDColEmployeeName"
        Me.INDColEmployeeName.OptionsColumn.AllowEdit = False
        Me.INDColEmployeeName.OptionsColumn.AllowFocus = False
        Me.INDColEmployeeName.Visible = True
        Me.INDColEmployeeName.VisibleIndex = 1
        Me.INDColEmployeeName.Width = 81
        '
        'INDColState
        '
        Me.INDColState.Caption = " "
        Me.INDColState.ColumnEdit = Me.INDRepPicEdit
        Me.INDColState.Name = "INDColState"
        Me.INDColState.OptionsColumn.AllowEdit = False
        Me.INDColState.OptionsColumn.AllowFocus = False
        Me.INDColState.Visible = True
        Me.INDColState.VisibleIndex = 2
        Me.INDColState.Width = 20
        '
        'INDRepPicEdit
        '
        Me.INDRepPicEdit.Name = "INDRepPicEdit"
        '
        'INDColMessages
        '
        Me.INDColMessages.Caption = "Info"
        Me.INDColMessages.ColumnEdit = Me.INDRepPopupConEdit
        Me.INDColMessages.Name = "INDColMessages"
        Me.INDColMessages.Visible = True
        Me.INDColMessages.VisibleIndex = 3
        Me.INDColMessages.Width = 31
        '
        'INDRepPopupConEdit
        '
        Me.INDRepPopupConEdit.AutoHeight = False
        Me.INDRepPopupConEdit.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDRepPopupConEdit.Name = "INDRepPopupConEdit"
        Me.INDRepPopupConEdit.PopupControl = Me.INDPopConCtrMessages
        Me.INDRepPopupConEdit.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        '
        'INDPopConCtrMessages
        '
        Me.INDPopConCtrMessages.Controls.Add(Me.INDgcMessages)
        Me.INDPopConCtrMessages.Location = New System.Drawing.Point(1, 1)
        Me.INDPopConCtrMessages.Name = "INDPopConCtrMessages"
        Me.INDPopConCtrMessages.Size = New System.Drawing.Size(700, 180)
        Me.INDPopConCtrMessages.TabIndex = 19
        '
        'INDgcMessages
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcMessages, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcMessages, Nothing)
        Me.INDgcMessages.Dock = System.Windows.Forms.DockStyle.Fill
        Me.IndigoGridControl1.SetExportButton(Me.INDgcMessages, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcMessages, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcMessages, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcMessages, False)
        Me.INDgcMessages.Location = New System.Drawing.Point(0, 0)
        Me.INDgcMessages.MainView = Me.INDgvMessages
        Me.INDgcMessages.Name = "INDgcMessages"
        Me.INDgcMessages.Size = New System.Drawing.Size(700, 180)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcMessages, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDgcMessages.TabIndex = 0
        Me.INDgcMessages.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDgvMessages})
        '
        'INDgvMessages
        '
        Me.INDgvMessages.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDgvMessages.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDgvMessages.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDgvMessages.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDgvMessages.Appearance.FocusedRow.Options.UseFont = True
        Me.INDgvMessages.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDgvMessages.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvMessages.Appearance.GroupRow.Options.UseFont = True
        Me.INDgvMessages.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvMessages.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDgvMessages.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDgvMessages.Appearance.Row.Options.UseFont = True
        Me.INDgvMessages.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDgvMessages.Appearance.ViewCaption.Options.UseFont = True
        Me.INDgvMessages.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColMessage})
        Me.INDgvMessages.GridControl = Me.INDgcMessages
        Me.INDgvMessages.Name = "INDgvMessages"
        Me.INDgvMessages.OptionsBehavior.Editable = False
        Me.INDgvMessages.OptionsView.EnableAppearanceEvenRow = True
        Me.INDgvMessages.OptionsView.EnableAppearanceOddRow = True
        Me.INDgvMessages.OptionsView.ShowAutoFilterRow = True
        Me.INDgvMessages.OptionsView.ShowGroupPanel = False
        Me.INDgvMessages.Tag = 180
        Me.IndigoGridView11.SetTemaIndigoMetro(Me.INDgvMessages, False)
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDgvMessages, False)
        '
        'INDColMessage
        '
        Me.INDColMessage.Caption = " "
        Me.INDColMessage.FieldName = "CodeMessage"
        Me.INDColMessage.Name = "INDColMessage"
        Me.INDColMessage.Visible = True
        Me.INDColMessage.VisibleIndex = 0
        '
        'INDColTotalUnemployed
        '
        Me.INDColTotalUnemployed.Caption = "Cesantías"
        Me.INDColTotalUnemployed.DisplayFormat.FormatString = "C2"
        Me.INDColTotalUnemployed.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.INDColTotalUnemployed.FieldName = "TotalUnemployed"
        Me.INDColTotalUnemployed.Name = "INDColTotalUnemployed"
        Me.INDColTotalUnemployed.OptionsColumn.AllowEdit = False
        Me.INDColTotalUnemployed.OptionsColumn.AllowFocus = False
        Me.INDColTotalUnemployed.Visible = True
        Me.INDColTotalUnemployed.VisibleIndex = 4
        Me.INDColTotalUnemployed.Width = 86
        '
        'INDColUnemployedInterestTotal
        '
        Me.INDColUnemployedInterestTotal.Caption = "Interes de Cesantías"
        Me.INDColUnemployedInterestTotal.DisplayFormat.FormatString = "C2"
        Me.INDColUnemployedInterestTotal.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.INDColUnemployedInterestTotal.FieldName = "UnemployedInterestTotal"
        Me.INDColUnemployedInterestTotal.Name = "INDColUnemployedInterestTotal"
        Me.INDColUnemployedInterestTotal.OptionsColumn.AllowEdit = False
        Me.INDColUnemployedInterestTotal.OptionsColumn.AllowFocus = False
        Me.INDColUnemployedInterestTotal.Visible = True
        Me.INDColUnemployedInterestTotal.VisibleIndex = 5
        Me.INDColUnemployedInterestTotal.Width = 68
        '
        'INDTotalProvisionsUnemployed
        '
        Me.INDTotalProvisionsUnemployed.Caption = "Provision Cesantía"
        Me.INDTotalProvisionsUnemployed.DisplayFormat.FormatString = "C2"
        Me.INDTotalProvisionsUnemployed.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.INDTotalProvisionsUnemployed.FieldName = "IBCAverage"
        Me.INDTotalProvisionsUnemployed.Name = "INDTotalProvisionsUnemployed"
        Me.INDTotalProvisionsUnemployed.OptionsColumn.AllowEdit = False
        Me.INDTotalProvisionsUnemployed.OptionsColumn.AllowFocus = False
        '
        'INDTotalProvisionsUnemployedNotSanctions
        '
        Me.INDTotalProvisionsUnemployedNotSanctions.Caption = "Provision Cesantía Sin Sanciones"
        Me.INDTotalProvisionsUnemployedNotSanctions.DisplayFormat.FormatString = "C2"
        Me.INDTotalProvisionsUnemployedNotSanctions.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.INDTotalProvisionsUnemployedNotSanctions.FieldName = "IBCAverageNotSanction"
        Me.INDTotalProvisionsUnemployedNotSanctions.Name = "INDTotalProvisionsUnemployedNotSanctions"
        Me.INDTotalProvisionsUnemployedNotSanctions.OptionsColumn.AllowEdit = False
        Me.INDTotalProvisionsUnemployedNotSanctions.OptionsColumn.AllowFocus = False
        '
        'INDColDateInitial
        '
        Me.INDColDateInitial.Caption = "Fecha Inicio"
        Me.INDColDateInitial.FieldName = "UnemployedInitialDate"
        Me.INDColDateInitial.Name = "INDColDateInitial"
        Me.INDColDateInitial.OptionsColumn.AllowEdit = False
        Me.INDColDateInitial.OptionsColumn.AllowFocus = False
        Me.INDColDateInitial.Visible = True
        Me.INDColDateInitial.VisibleIndex = 6
        Me.INDColDateInitial.Width = 89
        '
        'INDColDateEnding
        '
        Me.INDColDateEnding.Caption = "Fecha Fin"
        Me.INDColDateEnding.FieldName = "UnemployedEndingDate"
        Me.INDColDateEnding.Name = "INDColDateEnding"
        Me.INDColDateEnding.OptionsColumn.AllowEdit = False
        Me.INDColDateEnding.OptionsColumn.AllowFocus = False
        Me.INDColDateEnding.Visible = True
        Me.INDColDateEnding.VisibleIndex = 7
        Me.INDColDateEnding.Width = 89
        '
        'INDColWorkedDays
        '
        Me.INDColWorkedDays.AppearanceCell.Options.UseTextOptions = True
        Me.INDColWorkedDays.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDColWorkedDays.Caption = "N° Días"
        Me.INDColWorkedDays.FieldName = "WorkedTotalDays"
        Me.INDColWorkedDays.Name = "INDColWorkedDays"
        Me.INDColWorkedDays.OptionsColumn.AllowEdit = False
        Me.INDColWorkedDays.OptionsColumn.AllowFocus = False
        Me.INDColWorkedDays.Visible = True
        Me.INDColWorkedDays.VisibleIndex = 8
        Me.INDColWorkedDays.Width = 89
        '
        'INDColSanctionsDays
        '
        Me.INDColSanctionsDays.AppearanceCell.Options.UseTextOptions = True
        Me.INDColSanctionsDays.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDColSanctionsDays.Caption = "N° Días Sanción"
        Me.INDColSanctionsDays.FieldName = "SanctionTotalDays"
        Me.INDColSanctionsDays.Name = "INDColSanctionsDays"
        Me.INDColSanctionsDays.OptionsColumn.AllowEdit = False
        Me.INDColSanctionsDays.OptionsColumn.AllowFocus = False
        Me.INDColSanctionsDays.Visible = True
        Me.INDColSanctionsDays.VisibleIndex = 9
        Me.INDColSanctionsDays.Width = 91
        '
        'INDColAverageMonthNumber
        '
        Me.INDColAverageMonthNumber.AppearanceCell.Options.UseTextOptions = True
        Me.INDColAverageMonthNumber.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDColAverageMonthNumber.Caption = "N° Meses"
        Me.INDColAverageMonthNumber.FieldName = "AverageMothNumber"
        Me.INDColAverageMonthNumber.Name = "INDColAverageMonthNumber"
        Me.INDColAverageMonthNumber.OptionsColumn.AllowEdit = False
        Me.INDColAverageMonthNumber.OptionsColumn.AllowFocus = False
        Me.INDColAverageMonthNumber.Visible = True
        Me.INDColAverageMonthNumber.VisibleIndex = 10
        Me.INDColAverageMonthNumber.Width = 111
        '
        'INDColConfirm
        '
        Me.INDColConfirm.Caption = "Confirmado"
        Me.INDColConfirm.ColumnEdit = Me.INDRepRgSanctions
        Me.INDColConfirm.FieldName = "Status"
        Me.INDColConfirm.Name = "INDColConfirm"
        Me.INDColConfirm.OptionsColumn.AllowEdit = False
        Me.INDColConfirm.OptionsColumn.AllowFocus = False
        Me.INDColConfirm.Visible = True
        Me.INDColConfirm.VisibleIndex = 11
        Me.INDColConfirm.Width = 161
        '
        'INDRepRgSanctions
        '
        Me.INDRepRgSanctions.Items.AddRange(New DevExpress.XtraEditors.Controls.RadioGroupItem() {New DevExpress.XtraEditors.Controls.RadioGroupItem(False, "No"), New DevExpress.XtraEditors.Controls.RadioGroupItem(True, "Sí")})
        Me.INDRepRgSanctions.Name = "INDRepRgSanctions"
        '
        'RepositoryItemPopupContainerEdit1
        '
        Me.RepositoryItemPopupContainerEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit1.Name = "RepositoryItemPopupContainerEdit1"
        '
        'INDLyUnemployed
        '
        Me.INDLyUnemployed.AllowCustomization = False
        Me.INDLyUnemployed.Controls.Add(Me.LabelControl1)
        Me.INDLyUnemployed.Controls.Add(Me.INDgcLiquidationResult)
        Me.INDLyUnemployed.Controls.Add(Me.CtrNavigation1)
        Me.INDLyUnemployed.Controls.Add(Me.INDrgSanctionDays)
        Me.INDLyUnemployed.Controls.Add(Me.INDrgConfirm)
        Me.INDLyUnemployed.Controls.Add(Me.INDCbeYear)
        Me.INDLyUnemployed.Controls.Add(Me.INDtxtResolutionNumber)
        Me.INDLyUnemployed.Controls.Add(Me.INDdeAuthorizationDate)
        Me.INDLyUnemployed.Controls.Add(Me.INDmeeUnemployedRetirementReason)
        Me.INDLyUnemployed.Controls.Add(Me.INDdeUnemployedLiquidationDate)
        Me.INDLyUnemployed.Controls.Add(Me.INDSleEmployee)
        Me.INDLyUnemployed.Controls.Add(Me.INDgcGroups)
        Me.INDLyUnemployed.Controls.Add(Me.INDSleCompany)
        Me.INDLyUnemployed.Controls.Add(Me.INDrgLiquidate)
        Me.INDLyUnemployed.Controls.Add(Me.INDgleUnemployedInterestPaidWithPayroll)
        Me.INDLyUnemployed.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControls.SetIsCustomizable(Me.INDLyUnemployed, False)
        Me.INDLyUnemployed.Location = New System.Drawing.Point(2, 8)
        Me.INDLyUnemployed.Name = "INDLyUnemployed"
        Me.INDLyUnemployed.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(534, 36, 1386, 782)
        Me.INDLyUnemployed.Root = Me.INDLyGrUnemployed
        Me.INDLyUnemployed.Size = New System.Drawing.Size(1004, 595)
        Me.INDLyUnemployed.TabIndex = 0
        Me.INDLyUnemployed.Text = "LayoutControl1"
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Segoe UI Semilight", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl1.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(144, Byte), Integer), CType(CType(233, Byte), Integer))
        Me.LabelControl1.Appearance.Options.UseFont = True
        Me.LabelControl1.Appearance.Options.UseForeColor = True
        Me.LabelControl1.Location = New System.Drawing.Point(82, 222)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(988, 32)
        Me.LabelControl1.StyleController = Me.INDLyUnemployed
        Me.LabelControl1.TabIndex = 19
        '
        'CtrNavigation1
        '
        Me.CtrNavigation1.HideGroupContent = False
        Me.CtrNavigation1.Location = New System.Drawing.Point(24, 193)
        Me.CtrNavigation1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.CtrNavigation1.Name = "CtrNavigation1"
        Me.CtrNavigation1.Size = New System.Drawing.Size(54, 61)
        Me.CtrNavigation1.TabIndex = 17
        '
        'INDrgSanctionDays
        '
        Me.IndigoRadioGroup1.SetCampoObligatorio(Me.INDrgSanctionDays, False)
        Me.INDrgSanctionDays.EditValue = False
        Me.INDrgSanctionDays.Location = New System.Drawing.Point(266, -333)
        Me.INDrgSanctionDays.Name = "INDrgSanctionDays"
        Me.INDrgSanctionDays.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDrgSanctionDays.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDrgSanctionDays.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDrgSanctionDays.Properties.Appearance.Options.UseBackColor = True
        Me.INDrgSanctionDays.Properties.Appearance.Options.UseFont = True
        Me.INDrgSanctionDays.Properties.Appearance.Options.UseForeColor = True
        Me.INDrgSanctionDays.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDrgSanctionDays.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDrgSanctionDays.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDrgSanctionDays.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDrgSanctionDays.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDrgSanctionDays.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDrgSanctionDays.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDrgSanctionDays.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDrgSanctionDays.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.RadioGroupItem() {New DevExpress.XtraEditors.Controls.RadioGroupItem(False, "No"), New DevExpress.XtraEditors.Controls.RadioGroupItem(True, "Sí")})
        Me.INDrgSanctionDays.Size = New System.Drawing.Size(144, 32)
        Me.INDrgSanctionDays.StyleController = Me.INDLyUnemployed
        Me.INDrgSanctionDays.TabIndex = 16
        '
        'INDrgConfirm
        '
        Me.IndigoRadioGroup1.SetCampoObligatorio(Me.INDrgConfirm, False)
        Me.INDrgConfirm.EditValue = False
        Me.INDrgConfirm.Location = New System.Drawing.Point(266, -297)
        Me.INDrgConfirm.Name = "INDrgConfirm"
        Me.INDrgConfirm.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDrgConfirm.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDrgConfirm.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDrgConfirm.Properties.Appearance.Options.UseBackColor = True
        Me.INDrgConfirm.Properties.Appearance.Options.UseFont = True
        Me.INDrgConfirm.Properties.Appearance.Options.UseForeColor = True
        Me.INDrgConfirm.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDrgConfirm.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDrgConfirm.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDrgConfirm.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDrgConfirm.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDrgConfirm.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDrgConfirm.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDrgConfirm.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDrgConfirm.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.RadioGroupItem() {New DevExpress.XtraEditors.Controls.RadioGroupItem(False, "No"), New DevExpress.XtraEditors.Controls.RadioGroupItem(True, "Sí")})
        Me.INDrgConfirm.Size = New System.Drawing.Size(144, 32)
        Me.INDrgConfirm.StyleController = Me.INDLyUnemployed
        Me.INDrgConfirm.TabIndex = 15
        '
        'INDCbeYear
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDCbeYear, False)
        Me.IndigoTextEdit11.SetApplyStyle(Me.INDCbeYear, False)
        Me.IndigoTextEdit11.SetCampoObligatorio(Me.INDCbeYear, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDCbeYear, False)
        Me.IndigoComboBoxEdit1.SetCampoObligatorio(Me.INDCbeYear, False)
        Me.INDCbeYear.Location = New System.Drawing.Point(591, -208)
        Me.IndigoTextEdit11.SetMascara(Me.INDCbeYear, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit1.SetMascara(Me.INDCbeYear, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDCbeYear.Name = "INDCbeYear"
        Me.INDCbeYear.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDCbeYear.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDCbeYear.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDCbeYear.Properties.Appearance.Options.UseBackColor = True
        Me.INDCbeYear.Properties.Appearance.Options.UseFont = True
        Me.INDCbeYear.Properties.Appearance.Options.UseForeColor = True
        Me.INDCbeYear.Properties.AppearanceDropDown.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDCbeYear.Properties.AppearanceDropDown.Options.UseFont = True
        Me.INDCbeYear.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDCbeYear.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDCbeYear.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDCbeYear.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDCbeYear.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDCbeYear.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDCbeYear.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDCbeYear.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDCbeYear.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDCbeYear.Size = New System.Drawing.Size(89, 28)
        Me.INDCbeYear.StyleController = Me.INDLyUnemployed
        Me.INDCbeYear.TabIndex = 14
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDCbeYear, 0)
        Me.IndigoTextEdit11.SetTamañoMinimoString(Me.INDCbeYear, 0)
        '
        'INDtxtResolutionNumber
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtResolutionNumber, False)
        Me.IndigoTextEdit11.SetApplyStyle(Me.INDtxtResolutionNumber, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtResolutionNumber, False)
        Me.IndigoTextEdit11.SetCampoObligatorio(Me.INDtxtResolutionNumber, False)
        Me.INDtxtResolutionNumber.Location = New System.Drawing.Point(724, 153)
        Me.IndigoTextEdit11.SetMascara(Me.INDtxtResolutionNumber, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtResolutionNumber, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtResolutionNumber.Name = "INDtxtResolutionNumber"
        Me.INDtxtResolutionNumber.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDtxtResolutionNumber.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtResolutionNumber.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDtxtResolutionNumber.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtResolutionNumber.Properties.Appearance.Options.UseFont = True
        Me.INDtxtResolutionNumber.Properties.Appearance.Options.UseForeColor = True
        Me.INDtxtResolutionNumber.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDtxtResolutionNumber.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDtxtResolutionNumber.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtResolutionNumber.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDtxtResolutionNumber.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtResolutionNumber.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtxtResolutionNumber.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtResolutionNumber.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDtxtResolutionNumber.Properties.MaxLength = 15
        Me.INDtxtResolutionNumber.Size = New System.Drawing.Size(286, 28)
        Me.INDtxtResolutionNumber.StyleController = Me.INDLyUnemployed
        Me.INDtxtResolutionNumber.TabIndex = 13
        Me.IndigoTextEdit11.SetTamañoMinimoString(Me.INDtxtResolutionNumber, 0)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtResolutionNumber, 0)
        '
        'INDdeAuthorizationDate
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDdeAuthorizationDate, False)
        Me.IndigoTextEdit11.SetApplyStyle(Me.INDdeAuthorizationDate, False)
        Me.IndigoTextEdit11.SetCampoObligatorio(Me.INDdeAuthorizationDate, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDdeAuthorizationDate, False)
        Me.IndigoDate1.SetCampoObligatorio(Me.INDdeAuthorizationDate, False)
        Me.INDdeAuthorizationDate.EditValue = Nothing
        Me.INDdeAuthorizationDate.EnterMoveNextControl = True
        Me.INDdeAuthorizationDate.Location = New System.Drawing.Point(201, 153)
        Me.IndigoTextEdit11.SetMascara(Me.INDdeAuthorizationDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit1.SetMascara(Me.INDdeAuthorizationDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoDate1.SetMascaraDate(Me.INDdeAuthorizationDate, Presentation.Controls.IndigoDate.EMask.Fecha)
        Me.INDdeAuthorizationDate.Name = "INDdeAuthorizationDate"
        Me.INDdeAuthorizationDate.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDdeAuthorizationDate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdeAuthorizationDate.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDdeAuthorizationDate.Properties.Appearance.Options.UseBackColor = True
        Me.INDdeAuthorizationDate.Properties.Appearance.Options.UseFont = True
        Me.INDdeAuthorizationDate.Properties.Appearance.Options.UseForeColor = True
        Me.INDdeAuthorizationDate.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDdeAuthorizationDate.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDdeAuthorizationDate.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdeAuthorizationDate.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDdeAuthorizationDate.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDdeAuthorizationDate.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDdeAuthorizationDate.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDdeAuthorizationDate.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDdeAuthorizationDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdeAuthorizationDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdeAuthorizationDate.Properties.Mask.EditMask = "dd/MM/yyyy"
        Me.INDdeAuthorizationDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDdeAuthorizationDate.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDdeAuthorizationDate.Size = New System.Drawing.Size(239, 28)
        Me.INDdeAuthorizationDate.StyleController = Me.INDLyUnemployed
        Me.INDdeAuthorizationDate.TabIndex = 12
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDdeAuthorizationDate, 0)
        Me.IndigoTextEdit11.SetTamañoMinimoString(Me.INDdeAuthorizationDate, 0)
        '
        'INDmeeUnemployedRetirementReason
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDmeeUnemployedRetirementReason, False)
        Me.IndigoTextEdit11.SetApplyStyle(Me.INDmeeUnemployedRetirementReason, False)
        Me.IndigoTextEdit11.SetCampoObligatorio(Me.INDmeeUnemployedRetirementReason, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDmeeUnemployedRetirementReason, False)
        Me.INDmeeUnemployedRetirementReason.EnterMoveNextControl = True
        Me.INDmeeUnemployedRetirementReason.Location = New System.Drawing.Point(724, 117)
        Me.IndigoTextEdit1.SetMascara(Me.INDmeeUnemployedRetirementReason, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit11.SetMascara(Me.INDmeeUnemployedRetirementReason, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDmeeUnemployedRetirementReason.Name = "INDmeeUnemployedRetirementReason"
        Me.INDmeeUnemployedRetirementReason.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDmeeUnemployedRetirementReason.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDmeeUnemployedRetirementReason.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDmeeUnemployedRetirementReason.Properties.Appearance.Options.UseBackColor = True
        Me.INDmeeUnemployedRetirementReason.Properties.Appearance.Options.UseFont = True
        Me.INDmeeUnemployedRetirementReason.Properties.Appearance.Options.UseForeColor = True
        Me.INDmeeUnemployedRetirementReason.Properties.AppearanceDropDown.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDmeeUnemployedRetirementReason.Properties.AppearanceDropDown.Options.UseFont = True
        Me.INDmeeUnemployedRetirementReason.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDmeeUnemployedRetirementReason.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDmeeUnemployedRetirementReason.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDmeeUnemployedRetirementReason.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDmeeUnemployedRetirementReason.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDmeeUnemployedRetirementReason.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDmeeUnemployedRetirementReason.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDmeeUnemployedRetirementReason.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDmeeUnemployedRetirementReason.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDmeeUnemployedRetirementReason.Properties.PopupFormSize = New System.Drawing.Size(300, 60)
        Me.INDmeeUnemployedRetirementReason.Size = New System.Drawing.Size(286, 28)
        Me.INDmeeUnemployedRetirementReason.StyleController = Me.INDLyUnemployed
        Me.INDmeeUnemployedRetirementReason.TabIndex = 11
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDmeeUnemployedRetirementReason, 0)
        Me.IndigoTextEdit11.SetTamañoMinimoString(Me.INDmeeUnemployedRetirementReason, 0)
        '
        'INDdeUnemployedLiquidationDate
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDdeUnemployedLiquidationDate, False)
        Me.IndigoTextEdit11.SetApplyStyle(Me.INDdeUnemployedLiquidationDate, False)
        Me.IndigoTextEdit11.SetCampoObligatorio(Me.INDdeUnemployedLiquidationDate, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDdeUnemployedLiquidationDate, False)
        Me.IndigoDate1.SetCampoObligatorio(Me.INDdeUnemployedLiquidationDate, False)
        Me.INDdeUnemployedLiquidationDate.EditValue = Nothing
        Me.INDdeUnemployedLiquidationDate.EnterMoveNextControl = True
        Me.INDdeUnemployedLiquidationDate.Location = New System.Drawing.Point(201, 117)
        Me.IndigoTextEdit11.SetMascara(Me.INDdeUnemployedLiquidationDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit1.SetMascara(Me.INDdeUnemployedLiquidationDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoDate1.SetMascaraDate(Me.INDdeUnemployedLiquidationDate, Presentation.Controls.IndigoDate.EMask.Fecha)
        Me.INDdeUnemployedLiquidationDate.Name = "INDdeUnemployedLiquidationDate"
        Me.INDdeUnemployedLiquidationDate.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDdeUnemployedLiquidationDate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdeUnemployedLiquidationDate.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDdeUnemployedLiquidationDate.Properties.Appearance.Options.UseBackColor = True
        Me.INDdeUnemployedLiquidationDate.Properties.Appearance.Options.UseFont = True
        Me.INDdeUnemployedLiquidationDate.Properties.Appearance.Options.UseForeColor = True
        Me.INDdeUnemployedLiquidationDate.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDdeUnemployedLiquidationDate.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDdeUnemployedLiquidationDate.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdeUnemployedLiquidationDate.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDdeUnemployedLiquidationDate.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDdeUnemployedLiquidationDate.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDdeUnemployedLiquidationDate.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDdeUnemployedLiquidationDate.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDdeUnemployedLiquidationDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdeUnemployedLiquidationDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdeUnemployedLiquidationDate.Properties.Mask.EditMask = "dd/MM/yyyy"
        Me.INDdeUnemployedLiquidationDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDdeUnemployedLiquidationDate.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDdeUnemployedLiquidationDate.Size = New System.Drawing.Size(239, 28)
        Me.INDdeUnemployedLiquidationDate.StyleController = Me.INDLyUnemployed
        Me.INDdeUnemployedLiquidationDate.TabIndex = 10
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDdeUnemployedLiquidationDate, 0)
        Me.IndigoTextEdit11.SetTamañoMinimoString(Me.INDdeUnemployedLiquidationDate, 0)
        '
        'INDSleEmployee
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSleEmployee, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSleEmployee, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSleEmployee, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSleEmployee, False)
        Me.IndigoTextEdit11.SetApplyStyle(Me.INDSleEmployee, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDSleEmployee, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSleEmployee, False)
        Me.IndigoTextEdit11.SetCampoObligatorio(Me.INDSleEmployee, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSleEmployee, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSleEmployee, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSleEmployee, False)
        Me.INDSleEmployee.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSleEmployee, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSleEmployee, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSleEmployee, False)
        Me.INDSleEmployee.Location = New System.Drawing.Point(201, 81)
        Me.IndigoTextEdit1.SetMascara(Me.INDSleEmployee, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit11.SetMascara(Me.INDSleEmployee, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSleEmployee.Name = "INDSleEmployee"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSleEmployee, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSleEmployee, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleEmployee, False)
        Me.IndigoSearchLookUpControl1.SetPopupBestFitHeight(Me.INDSleEmployee, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDSleEmployee, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSleEmployee, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSleEmployee, False)
        Me.INDSleEmployee.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDSleEmployee.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSleEmployee.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDSleEmployee.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleEmployee.Properties.Appearance.Options.UseFont = True
        Me.INDSleEmployee.Properties.Appearance.Options.UseForeColor = True
        Me.INDSleEmployee.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDSleEmployee.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDSleEmployee.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSleEmployee.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDSleEmployee.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDSleEmployee.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDSleEmployee.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSleEmployee.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDSleEmployee.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete, "", -1, True, True, False, EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, "Limpiar selección (Supr)", Nothing, Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.INDSleEmployee.Properties.DisplayMember = "Name"
        Me.INDSleEmployee.Properties.NullText = "[Seleccione Empleado]"
        Me.INDSleEmployee.Properties.PopupSizeable = False
        Me.INDSleEmployee.Properties.PopupView = Me.GridView2
        Me.INDSleEmployee.Properties.ShowFooter = False
        Me.INDSleEmployee.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleEmployee, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleEmployee, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSleEmployee, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleEmployee, True)
        Me.INDSleEmployee.Size = New System.Drawing.Size(239, 28)
        Me.INDSleEmployee.StyleController = Me.INDLyUnemployed
        Me.INDSleEmployee.TabIndex = 9
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleEmployee, Nothing)
        Me.IndigoTextEdit11.SetTamañoMinimoString(Me.INDSleEmployee, 0)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSleEmployee, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSleEmployee, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSleEmployee, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSleEmployee, False)
        '
        'GridView2
        '
        Me.GridView2.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridView2.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridView2.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridView2.Appearance.FocusedRow.Options.UseFont = True
        Me.GridView2.Appearance.FocusedRow.Options.UseForeColor = True
        Me.GridView2.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView2.Appearance.GroupRow.Options.UseFont = True
        Me.GridView2.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView2.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView2.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridView2.Appearance.Row.Options.UseFont = True
        Me.GridView2.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColNitEmployee, Me.INDColNameEmployee})
        Me.GridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView2.Name = "GridView2"
        Me.GridView2.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView2.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView2.OptionsView.EnableAppearanceOddRow = True
        Me.GridView2.OptionsView.ShowAutoFilterRow = True
        Me.GridView2.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView11.SetTemaIndigoMetro(Me.GridView2, False)
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridView2, False)
        '
        'INDColNitEmployee
        '
        Me.INDColNitEmployee.Caption = "Nit"
        Me.INDColNitEmployee.FieldName = "Nit"
        Me.INDColNitEmployee.Name = "INDColNitEmployee"
        Me.INDColNitEmployee.Visible = True
        Me.INDColNitEmployee.VisibleIndex = 0
        '
        'INDColNameEmployee
        '
        Me.INDColNameEmployee.Caption = "Nombre"
        Me.INDColNameEmployee.FieldName = "Name"
        Me.INDColNameEmployee.Name = "INDColNameEmployee"
        Me.INDColNameEmployee.Visible = True
        Me.INDColNameEmployee.VisibleIndex = 1
        '
        'INDgcGroups
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcGroups, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcGroups, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDgcGroups, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcGroups, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcGroups, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcGroups, False)
        Me.INDgcGroups.Location = New System.Drawing.Point(24, -143)
        Me.INDgcGroups.MainView = Me.INDgvGroups
        Me.INDgcGroups.MinimumSize = New System.Drawing.Size(1050, 167)
        Me.INDgcGroups.Name = "INDgcGroups"
        Me.INDgcGroups.Size = New System.Drawing.Size(1050, 167)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcGroups, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.INDgcGroups.TabIndex = 6
        Me.INDgcGroups.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDgvGroups})
        '
        'INDgvGroups
        '
        Me.INDgvGroups.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDgvGroups.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDgvGroups.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDgvGroups.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDgvGroups.Appearance.FocusedRow.Options.UseFont = True
        Me.INDgvGroups.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDgvGroups.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvGroups.Appearance.GroupRow.Options.UseFont = True
        Me.INDgvGroups.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvGroups.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDgvGroups.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDgvGroups.Appearance.Row.Options.UseFont = True
        Me.INDgvGroups.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDgvGroups.Appearance.ViewCaption.Options.UseFont = True
        Me.INDgvGroups.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColApply, Me.INDColGroupCode, Me.INDColGroupName})
        Me.INDgvGroups.GridControl = Me.INDgcGroups
        Me.INDgvGroups.Name = "INDgvGroups"
        Me.INDgvGroups.OptionsCustomization.AllowColumnMoving = False
        Me.INDgvGroups.OptionsCustomization.AllowFilter = False
        Me.INDgvGroups.OptionsCustomization.AllowGroup = False
        Me.INDgvGroups.OptionsDetail.AllowZoomDetail = False
        Me.INDgvGroups.OptionsDetail.EnableMasterViewMode = False
        Me.INDgvGroups.OptionsDetail.SmartDetailExpand = False
        Me.INDgvGroups.OptionsMenu.EnableColumnMenu = False
        Me.INDgvGroups.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDgvGroups.OptionsView.EnableAppearanceEvenRow = True
        Me.INDgvGroups.OptionsView.EnableAppearanceOddRow = True
        Me.INDgvGroups.OptionsView.ShowAutoFilterRow = True
        Me.INDgvGroups.OptionsView.ShowGroupPanel = False
        Me.INDgvGroups.Tag = 48
        Me.IndigoGridView11.SetTemaIndigoMetro(Me.INDgvGroups, False)
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDgvGroups, False)
        '
        'INDColApply
        '
        Me.INDColApply.Caption = " "
        Me.INDColApply.FieldName = "Apply"
        Me.INDColApply.Name = "INDColApply"
        Me.INDColApply.Visible = True
        Me.INDColApply.VisibleIndex = 0
        Me.INDColApply.Width = 39
        '
        'INDColGroupCode
        '
        Me.INDColGroupCode.Caption = "Código"
        Me.INDColGroupCode.FieldName = "Code"
        Me.INDColGroupCode.Name = "INDColGroupCode"
        Me.INDColGroupCode.OptionsColumn.AllowEdit = False
        Me.INDColGroupCode.Visible = True
        Me.INDColGroupCode.VisibleIndex = 1
        Me.INDColGroupCode.Width = 92
        '
        'INDColGroupName
        '
        Me.INDColGroupName.Caption = "Nombre"
        Me.INDColGroupName.FieldName = "Name"
        Me.INDColGroupName.Name = "INDColGroupName"
        Me.INDColGroupName.OptionsColumn.AllowEdit = False
        Me.INDColGroupName.Visible = True
        Me.INDColGroupName.VisibleIndex = 2
        Me.INDColGroupName.Width = 1077
        '
        'INDSleCompany
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSleCompany, AppearanceObject3)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSleCompany, AppearanceObject4)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSleCompany, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSleCompany, False)
        Me.IndigoTextEdit11.SetApplyStyle(Me.INDSleCompany, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDSleCompany, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSleCompany, False)
        Me.IndigoTextEdit11.SetCampoObligatorio(Me.INDSleCompany, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSleCompany, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSleCompany, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSleCompany, False)
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSleCompany, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSleCompany, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSleCompany, False)
        Me.INDSleCompany.Location = New System.Drawing.Point(171, -208)
        Me.IndigoTextEdit1.SetMascara(Me.INDSleCompany, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit11.SetMascara(Me.INDSleCompany, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSleCompany.Name = "INDSleCompany"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSleCompany, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSleCompany, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleCompany, False)
        Me.IndigoSearchLookUpControl1.SetPopupBestFitHeight(Me.INDSleCompany, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDSleCompany, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSleCompany, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSleCompany, False)
        Me.INDSleCompany.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDSleCompany.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSleCompany.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDSleCompany.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleCompany.Properties.Appearance.Options.UseFont = True
        Me.INDSleCompany.Properties.Appearance.Options.UseForeColor = True
        Me.INDSleCompany.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDSleCompany.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDSleCompany.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSleCompany.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDSleCompany.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDSleCompany.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDSleCompany.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSleCompany.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDSleCompany.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete, "", -1, True, True, False, EditorButtonImageOptions2, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject5, SerializableAppearanceObject6, SerializableAppearanceObject7, SerializableAppearanceObject8, "Limpiar selección (Supr)", Nothing, Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.INDSleCompany.Properties.DisplayMember = "Descripcion"
        Me.INDSleCompany.Properties.NullText = "[Seleccione Empresa]"
        Me.INDSleCompany.Properties.PopupSizeable = False
        Me.INDSleCompany.Properties.PopupView = Me.SearchLookUpEdit1View
        Me.INDSleCompany.Properties.ShowFooter = False
        Me.INDSleCompany.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleCompany, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleCompany, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSleCompany, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleCompany, True)
        Me.INDSleCompany.Size = New System.Drawing.Size(239, 28)
        Me.INDSleCompany.StyleController = Me.INDLyUnemployed
        Me.INDSleCompany.TabIndex = 5
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleCompany, Nothing)
        Me.IndigoTextEdit11.SetTamañoMinimoString(Me.INDSleCompany, 0)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSleCompany, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSleCompany, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSleCompany, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSleCompany, False)
        '
        'SearchLookUpEdit1View
        '
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.Options.UseFont = True
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.Options.UseForeColor = True
        Me.SearchLookUpEdit1View.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit1View.Appearance.GroupRow.Options.UseFont = True
        Me.SearchLookUpEdit1View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit1View.Appearance.HeaderPanel.Options.UseFont = True
        Me.SearchLookUpEdit1View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.SearchLookUpEdit1View.Appearance.Row.Options.UseFont = True
        Me.SearchLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColCompanyCode, Me.INDColCompanyName})
        Me.SearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit1View.Name = "SearchLookUpEdit1View"
        Me.SearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView11.SetTemaIndigoMetro(Me.SearchLookUpEdit1View, False)
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEdit1View, False)
        '
        'INDColCompanyCode
        '
        Me.INDColCompanyCode.Caption = "Código"
        Me.INDColCompanyCode.FieldName = "Codigo"
        Me.INDColCompanyCode.Name = "INDColCompanyCode"
        Me.INDColCompanyCode.Visible = True
        Me.INDColCompanyCode.VisibleIndex = 0
        '
        'INDColCompanyName
        '
        Me.INDColCompanyName.Caption = "Nombre"
        Me.INDColCompanyName.FieldName = "Descripcion"
        Me.INDColCompanyName.Name = "INDColCompanyName"
        Me.INDColCompanyName.Visible = True
        Me.INDColCompanyName.VisibleIndex = 1
        '
        'INDrgLiquidate
        '
        Me.IndigoRadioGroup1.SetCampoObligatorio(Me.INDrgLiquidate, False)
        Me.INDrgLiquidate.EditValue = False
        Me.INDrgLiquidate.Location = New System.Drawing.Point(266, -369)
        Me.INDrgLiquidate.Name = "INDrgLiquidate"
        Me.INDrgLiquidate.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDrgLiquidate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDrgLiquidate.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDrgLiquidate.Properties.Appearance.Options.UseBackColor = True
        Me.INDrgLiquidate.Properties.Appearance.Options.UseFont = True
        Me.INDrgLiquidate.Properties.Appearance.Options.UseForeColor = True
        Me.INDrgLiquidate.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDrgLiquidate.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDrgLiquidate.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDrgLiquidate.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDrgLiquidate.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDrgLiquidate.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDrgLiquidate.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDrgLiquidate.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDrgLiquidate.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.RadioGroupItem() {New DevExpress.XtraEditors.Controls.RadioGroupItem(False, "Anual"), New DevExpress.XtraEditors.Controls.RadioGroupItem(True, "Parcial")})
        Me.INDrgLiquidate.Size = New System.Drawing.Size(144, 32)
        Me.INDrgLiquidate.StyleController = Me.INDLyUnemployed
        Me.INDrgLiquidate.TabIndex = 4
        '
        'INDgleUnemployedInterestPaidWithPayroll
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDgleUnemployedInterestPaidWithPayroll, False)
        Me.IndigoTextEdit11.SetApplyStyle(Me.INDgleUnemployedInterestPaidWithPayroll, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDgleUnemployedInterestPaidWithPayroll, False)
        Me.IndigoTextEdit11.SetCampoObligatorio(Me.INDgleUnemployedInterestPaidWithPayroll, False)
        Me.INDgleUnemployedInterestPaidWithPayroll.EnterMoveNextControl = True
        Me.INDgleUnemployedInterestPaidWithPayroll.Location = New System.Drawing.Point(1008, -208)
        Me.IndigoTextEdit11.SetMascara(Me.INDgleUnemployedInterestPaidWithPayroll, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit1.SetMascara(Me.INDgleUnemployedInterestPaidWithPayroll, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDgleUnemployedInterestPaidWithPayroll.Name = "INDgleUnemployedInterestPaidWithPayroll"
        Me.INDgleUnemployedInterestPaidWithPayroll.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDgleUnemployedInterestPaidWithPayroll.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDgleUnemployedInterestPaidWithPayroll.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDgleUnemployedInterestPaidWithPayroll.Properties.Appearance.Options.UseBackColor = True
        Me.INDgleUnemployedInterestPaidWithPayroll.Properties.Appearance.Options.UseFont = True
        Me.INDgleUnemployedInterestPaidWithPayroll.Properties.Appearance.Options.UseForeColor = True
        Me.INDgleUnemployedInterestPaidWithPayroll.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDgleUnemployedInterestPaidWithPayroll.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDgleUnemployedInterestPaidWithPayroll.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDgleUnemployedInterestPaidWithPayroll.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDgleUnemployedInterestPaidWithPayroll.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDgleUnemployedInterestPaidWithPayroll.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDgleUnemployedInterestPaidWithPayroll.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDgleUnemployedInterestPaidWithPayroll.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDgleUnemployedInterestPaidWithPayroll.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDgleUnemployedInterestPaidWithPayroll.Properties.DataSource = CType(resources.GetObject("INDgleUnemployedInterestPaidWithPayroll.Properties.DataSource"), Object)
        Me.INDgleUnemployedInterestPaidWithPayroll.Properties.DisplayMember = "Item2"
        Me.INDgleUnemployedInterestPaidWithPayroll.Properties.ImmediatePopup = True
        Me.INDgleUnemployedInterestPaidWithPayroll.Properties.NullText = ""
        Me.INDgleUnemployedInterestPaidWithPayroll.Properties.PopupView = Me.CtrYesNo4View
        Me.INDgleUnemployedInterestPaidWithPayroll.Properties.ValueMember = "Item1"
        Me.INDgleUnemployedInterestPaidWithPayroll.Size = New System.Drawing.Size(62, 28)
        Me.INDgleUnemployedInterestPaidWithPayroll.StyleController = Me.INDLyUnemployed
        ToolTipItem1.Text = "¿Intereses sobre cesantías se pagan con Nómina?"
        SuperToolTip1.Items.Add(ToolTipItem1)
        Me.INDgleUnemployedInterestPaidWithPayroll.SuperTip = SuperToolTip1
        Me.INDgleUnemployedInterestPaidWithPayroll.TabIndex = 22
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDgleUnemployedInterestPaidWithPayroll, 0)
        Me.IndigoTextEdit11.SetTamañoMinimoString(Me.INDgleUnemployedInterestPaidWithPayroll, 0)
        '
        'CtrYesNo4View
        '
        Me.CtrYesNo4View.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.CtrYesNo4View.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.CtrYesNo4View.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.CtrYesNo4View.Appearance.FocusedRow.Options.UseFont = True
        Me.CtrYesNo4View.Appearance.FocusedRow.Options.UseForeColor = True
        Me.CtrYesNo4View.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CtrYesNo4View.Appearance.GroupRow.Options.UseFont = True
        Me.CtrYesNo4View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CtrYesNo4View.Appearance.HeaderPanel.Options.UseFont = True
        Me.CtrYesNo4View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.CtrYesNo4View.Appearance.Row.Options.UseFont = True
        Me.CtrYesNo4View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn4})
        Me.CtrYesNo4View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.CtrYesNo4View.Name = "CtrYesNo4View"
        Me.CtrYesNo4View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.CtrYesNo4View.OptionsView.EnableAppearanceEvenRow = True
        Me.CtrYesNo4View.OptionsView.EnableAppearanceOddRow = True
        Me.CtrYesNo4View.OptionsView.ShowAutoFilterRow = True
        Me.CtrYesNo4View.OptionsView.ShowDetailButtons = False
        Me.CtrYesNo4View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView11.SetTemaIndigoMetro(Me.CtrYesNo4View, False)
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.CtrYesNo4View, False)
        '
        'GridColumn3
        '
        Me.GridColumn3.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn3.Caption = "Selección"
        Me.GridColumn3.FieldName = "Item2"
        Me.GridColumn3.Name = "GridColumn3"
        '
        'GridColumn4
        '
        Me.GridColumn4.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn4.Caption = "Selección"
        Me.GridColumn4.FieldName = "Item2"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 0
        '
        'INDLyGrUnemployed
        '
        Me.INDLyGrUnemployed.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLyGrUnemployed.AppearanceGroup.Options.UseFont = True
        Me.INDLyGrUnemployed.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLyGrUnemployed.AppearanceItemCaption.Options.UseFont = True
        Me.INDLyGrUnemployed.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLyGrUnemployed.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLyGrUnemployed.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLyGrUnemployed.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLyGrUnemployed.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLyGrUnemployed.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLyGrUnemployed.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLyGrUnemployed.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLyGrUnemployed.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLyGrUnemployed.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLyGrUnemployed, False)
        Me.INDLyGrUnemployed.CustomizationFormText = "Root"
        Me.INDLyGrUnemployed.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDLyGrUnemployed.GroupBordersVisible = False
        Me.INDLyGrUnemployed.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyGrYearly, Me.INDlyGrPartial, Me.INDLyGrLiquidationResult, Me.INDlyGrLiquidationForm})
        Me.INDLyGrUnemployed.Name = "Root"
        Me.INDLyGrUnemployed.Size = New System.Drawing.Size(1094, 1000)
        Me.INDLyGrUnemployed.TextVisible = False
        '
        'INDlyGrYearly
        '
        Me.INDlyGrYearly.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlyGrYearly.AppearanceGroup.Options.UseFont = True
        Me.INDlyGrYearly.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlyGrYearly.AppearanceItemCaption.Options.UseFont = True
        Me.INDlyGrYearly.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGrYearly.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlyGrYearly.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlyGrYearly.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlyGrYearly.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGrYearly.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlyGrYearly.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGrYearly.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlyGrYearly.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGrYearly.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlyGrYearly, False)
        Me.INDlyGrYearly.CustomizationFormText = " "
        Me.INDlyGrYearly.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemCompany, Me.INDlyItemGroups, Me.INDlyItemYear, Me.INDliUnemployedInterestPaidWithPayroll})
        Me.INDlyGrYearly.Location = New System.Drawing.Point(0, 161)
        Me.INDlyGrYearly.Name = "INDlyGrYearly"
        Me.INDlyGrYearly.Size = New System.Drawing.Size(1074, 289)
        Me.INDlyGrYearly.Text = " Liquidación De Cesantías Total Anual"
        '
        'INDlyItemCompany
        '
        Me.INDlyItemCompany.Control = Me.INDSleCompany
        Me.INDlyItemCompany.CustomizationFormText = "Empresa"
        Me.INDlyItemCompany.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemCompany.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemCompany.MinSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemCompany.Name = "INDlyItemCompany"
        Me.INDlyItemCompany.Size = New System.Drawing.Size(390, 36)
        Me.INDlyItemCompany.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemCompany.Text = "Empresa"
        Me.INDlyItemCompany.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemCompany.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemCompany.TextToControlDistance = 12
        '
        'INDlyItemGroups
        '
        Me.INDlyItemGroups.Control = Me.INDgcGroups
        Me.INDlyItemGroups.CustomizationFormText = "Grupos"
        Me.INDlyItemGroups.Location = New System.Drawing.Point(0, 36)
        Me.INDlyItemGroups.MaxSize = New System.Drawing.Size(1024, 0)
        Me.INDlyItemGroups.MinSize = New System.Drawing.Size(1024, 200)
        Me.INDlyItemGroups.Name = "INDlyItemGroups"
        Me.INDlyItemGroups.Size = New System.Drawing.Size(1050, 200)
        Me.INDlyItemGroups.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemGroups.Text = "Grupos"
        Me.INDlyItemGroups.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemGroups.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemGroups.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemGroups.TextToControlDistance = 8
        '
        'INDlyItemYear
        '
        Me.INDlyItemYear.Control = Me.INDCbeYear
        Me.INDlyItemYear.CustomizationFormText = "Periodo Año Fiscal"
        Me.INDlyItemYear.Location = New System.Drawing.Point(390, 0)
        Me.INDlyItemYear.MaxSize = New System.Drawing.Size(270, 36)
        Me.INDlyItemYear.MinSize = New System.Drawing.Size(270, 36)
        Me.INDlyItemYear.Name = "INDlyItemYear"
        Me.INDlyItemYear.Size = New System.Drawing.Size(270, 36)
        Me.INDlyItemYear.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemYear.Text = "Periodo o Año Fiscal"
        Me.INDlyItemYear.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemYear.TextSize = New System.Drawing.Size(165, 21)
        Me.INDlyItemYear.TextToControlDistance = 12
        '
        'INDliUnemployedInterestPaidWithPayroll
        '
        Me.INDliUnemployedInterestPaidWithPayroll.Control = Me.INDgleUnemployedInterestPaidWithPayroll
        Me.INDliUnemployedInterestPaidWithPayroll.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDliUnemployedInterestPaidWithPayroll.CustomizationFormText = "¿Intereses sobre cesantías se pagan con Nómina?"
        Me.INDliUnemployedInterestPaidWithPayroll.Location = New System.Drawing.Point(660, 0)
        Me.INDliUnemployedInterestPaidWithPayroll.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDliUnemployedInterestPaidWithPayroll.MinSize = New System.Drawing.Size(390, 36)
        Me.INDliUnemployedInterestPaidWithPayroll.Name = "INDliUnemployedInterestPaidWithPayroll"
        Me.INDliUnemployedInterestPaidWithPayroll.Size = New System.Drawing.Size(390, 36)
        Me.INDliUnemployedInterestPaidWithPayroll.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDliUnemployedInterestPaidWithPayroll.Text = "¿Intereses sobre cesantías se pagan con Nómina?"
        Me.INDliUnemployedInterestPaidWithPayroll.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize
        Me.INDliUnemployedInterestPaidWithPayroll.TextSize = New System.Drawing.Size(319, 17)
        Me.INDliUnemployedInterestPaidWithPayroll.TextToControlDistance = 5
        '
        'INDlyGrPartial
        '
        Me.INDlyGrPartial.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlyGrPartial.AppearanceGroup.Options.UseFont = True
        Me.INDlyGrPartial.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlyGrPartial.AppearanceItemCaption.Options.UseFont = True
        Me.INDlyGrPartial.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGrPartial.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlyGrPartial.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlyGrPartial.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlyGrPartial.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGrPartial.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlyGrPartial.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGrPartial.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlyGrPartial.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGrPartial.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlyGrPartial, False)
        Me.INDlyGrPartial.CustomizationFormText = " "
        Me.INDlyGrPartial.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemEmployee, Me.INDlyItemUnemployedLiquidationDate, Me.INDlyUnemployedRetirementReason, Me.INDlyItemAuthorizationDate, Me.INDlyItemResosultionNumber})
        Me.INDlyGrPartial.Location = New System.Drawing.Point(0, 450)
        Me.INDlyGrPartial.Name = "INDlyGrPartial"
        Me.INDlyGrPartial.Size = New System.Drawing.Size(1074, 161)
        Me.INDlyGrPartial.Text = " Liquidación de Cesantías Parcial"
        Me.INDlyGrPartial.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDlyItemEmployee
        '
        Me.INDlyItemEmployee.Control = Me.INDSleEmployee
        Me.INDlyItemEmployee.CustomizationFormText = "Empleado"
        Me.INDlyItemEmployee.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemEmployee.MaxSize = New System.Drawing.Size(420, 36)
        Me.INDlyItemEmployee.MinSize = New System.Drawing.Size(420, 36)
        Me.INDlyItemEmployee.Name = "INDlyItemEmployee"
        Me.INDlyItemEmployee.Size = New System.Drawing.Size(1050, 36)
        Me.INDlyItemEmployee.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemEmployee.Text = "Empleado"
        Me.INDlyItemEmployee.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemEmployee.TextSize = New System.Drawing.Size(165, 21)
        Me.INDlyItemEmployee.TextToControlDistance = 12
        '
        'INDlyItemUnemployedLiquidationDate
        '
        Me.INDlyItemUnemployedLiquidationDate.Control = Me.INDdeUnemployedLiquidationDate
        Me.INDlyItemUnemployedLiquidationDate.CustomizationFormText = "Liquidar Cesantías Hasta"
        Me.INDlyItemUnemployedLiquidationDate.Location = New System.Drawing.Point(0, 36)
        Me.INDlyItemUnemployedLiquidationDate.MaxSize = New System.Drawing.Size(420, 36)
        Me.INDlyItemUnemployedLiquidationDate.MinSize = New System.Drawing.Size(420, 36)
        Me.INDlyItemUnemployedLiquidationDate.Name = "INDlyItemUnemployedLiquidationDate"
        Me.INDlyItemUnemployedLiquidationDate.Size = New System.Drawing.Size(420, 36)
        Me.INDlyItemUnemployedLiquidationDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemUnemployedLiquidationDate.Text = "Liquidar Cesantías Hasta"
        Me.INDlyItemUnemployedLiquidationDate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemUnemployedLiquidationDate.TextSize = New System.Drawing.Size(165, 21)
        Me.INDlyItemUnemployedLiquidationDate.TextToControlDistance = 12
        '
        'INDlyUnemployedRetirementReason
        '
        Me.INDlyUnemployedRetirementReason.Control = Me.INDmeeUnemployedRetirementReason
        Me.INDlyUnemployedRetirementReason.CustomizationFormText = "Motivo de Retiro de Cesantías Parciales"
        Me.INDlyUnemployedRetirementReason.Location = New System.Drawing.Point(420, 36)
        Me.INDlyUnemployedRetirementReason.MaxSize = New System.Drawing.Size(570, 36)
        Me.INDlyUnemployedRetirementReason.MinSize = New System.Drawing.Size(390, 36)
        Me.INDlyUnemployedRetirementReason.Name = "INDlyUnemployedRetirementReason"
        Me.INDlyUnemployedRetirementReason.Padding = New DevExpress.XtraLayout.Utils.Padding(10, 2, 2, 2)
        Me.INDlyUnemployedRetirementReason.Size = New System.Drawing.Size(630, 36)
        Me.INDlyUnemployedRetirementReason.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyUnemployedRetirementReason.Text = "Motivo de Retiro de Cesantías Parciales"
        Me.INDlyUnemployedRetirementReason.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyUnemployedRetirementReason.TextSize = New System.Drawing.Size(260, 21)
        Me.INDlyUnemployedRetirementReason.TextToControlDistance = 12
        '
        'INDlyItemAuthorizationDate
        '
        Me.INDlyItemAuthorizationDate.Control = Me.INDdeAuthorizationDate
        Me.INDlyItemAuthorizationDate.CustomizationFormText = "Fecha Resolución (Autorización)"
        Me.INDlyItemAuthorizationDate.Location = New System.Drawing.Point(0, 72)
        Me.INDlyItemAuthorizationDate.MaxSize = New System.Drawing.Size(420, 36)
        Me.INDlyItemAuthorizationDate.MinSize = New System.Drawing.Size(420, 36)
        Me.INDlyItemAuthorizationDate.Name = "INDlyItemAuthorizationDate"
        Me.INDlyItemAuthorizationDate.Size = New System.Drawing.Size(420, 36)
        Me.INDlyItemAuthorizationDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemAuthorizationDate.Text = "Fecha Autorización"
        Me.INDlyItemAuthorizationDate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemAuthorizationDate.TextSize = New System.Drawing.Size(165, 21)
        Me.INDlyItemAuthorizationDate.TextToControlDistance = 12
        '
        'INDlyItemResosultionNumber
        '
        Me.INDlyItemResosultionNumber.Control = Me.INDtxtResolutionNumber
        Me.INDlyItemResosultionNumber.CustomizationFormText = "Numero de Resolución del Ministerio"
        Me.INDlyItemResosultionNumber.Location = New System.Drawing.Point(420, 72)
        Me.INDlyItemResosultionNumber.MaxSize = New System.Drawing.Size(570, 36)
        Me.INDlyItemResosultionNumber.MinSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemResosultionNumber.Name = "INDlyItemResosultionNumber"
        Me.INDlyItemResosultionNumber.Padding = New DevExpress.XtraLayout.Utils.Padding(10, 2, 2, 2)
        Me.INDlyItemResosultionNumber.Size = New System.Drawing.Size(630, 36)
        Me.INDlyItemResosultionNumber.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemResosultionNumber.Text = "Número de Resolución del Ministerio"
        Me.INDlyItemResosultionNumber.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemResosultionNumber.TextSize = New System.Drawing.Size(260, 21)
        Me.INDlyItemResosultionNumber.TextToControlDistance = 12
        '
        'INDLyGrLiquidationResult
        '
        Me.INDLyGrLiquidationResult.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLyGrLiquidationResult.AppearanceGroup.Options.UseFont = True
        Me.INDLyGrLiquidationResult.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLyGrLiquidationResult.AppearanceItemCaption.Options.UseFont = True
        Me.INDLyGrLiquidationResult.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLyGrLiquidationResult.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLyGrLiquidationResult.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLyGrLiquidationResult.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLyGrLiquidationResult.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLyGrLiquidationResult.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLyGrLiquidationResult.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLyGrLiquidationResult.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLyGrLiquidationResult.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLyGrLiquidationResult.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLyGrLiquidationResult, False)
        Me.INDLyGrLiquidationResult.CustomizationFormText = " "
        Me.INDLyGrLiquidationResult.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemGoBack, Me.INDLyItemLiquidationResult, Me.INDlyItemTextResult, Me.EmptySpaceItem1})
        Me.INDLyGrLiquidationResult.Location = New System.Drawing.Point(0, 611)
        Me.INDLyGrLiquidationResult.Name = "INDLyGrLiquidationResult"
        Me.INDLyGrLiquidationResult.Padding = New DevExpress.XtraLayout.Utils.Padding(9, 9, -40, 9)
        Me.INDLyGrLiquidationResult.Size = New System.Drawing.Size(1074, 369)
        Me.INDLyGrLiquidationResult.Text = " "
        Me.INDLyGrLiquidationResult.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDlyItemGoBack
        '
        Me.INDlyItemGoBack.Control = Me.CtrNavigation1
        Me.INDlyItemGoBack.CustomizationFormText = "Atrás"
        Me.INDlyItemGoBack.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemGoBack.MaxSize = New System.Drawing.Size(58, 65)
        Me.INDlyItemGoBack.MinSize = New System.Drawing.Size(58, 65)
        Me.INDlyItemGoBack.Name = "INDlyItemGoBack"
        Me.INDlyItemGoBack.Size = New System.Drawing.Size(58, 65)
        Me.INDlyItemGoBack.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemGoBack.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemGoBack.TextVisible = False
        '
        'INDLyItemLiquidationResult
        '
        Me.INDLyItemLiquidationResult.Control = Me.INDgcLiquidationResult
        Me.INDLyItemLiquidationResult.CustomizationFormText = "Resultado Liquidacion"
        Me.INDLyItemLiquidationResult.Location = New System.Drawing.Point(0, 65)
        Me.INDLyItemLiquidationResult.MaxSize = New System.Drawing.Size(1024, 0)
        Me.INDLyItemLiquidationResult.MinSize = New System.Drawing.Size(1024, 300)
        Me.INDLyItemLiquidationResult.Name = "INDLyItemLiquidationResult"
        Me.INDLyItemLiquidationResult.Size = New System.Drawing.Size(1050, 300)
        Me.INDLyItemLiquidationResult.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyItemLiquidationResult.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLyItemLiquidationResult.TextVisible = False
        '
        'INDlyItemTextResult
        '
        Me.INDlyItemTextResult.Control = Me.LabelControl1
        Me.INDlyItemTextResult.CustomizationFormText = "Resultado"
        Me.INDlyItemTextResult.Location = New System.Drawing.Point(58, 29)
        Me.INDlyItemTextResult.Name = "INDlyItemTextResult"
        Me.INDlyItemTextResult.Size = New System.Drawing.Size(992, 36)
        Me.INDlyItemTextResult.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemTextResult.TextVisible = False
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.AllowHotTrack = False
        Me.EmptySpaceItem1.CustomizationFormText = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(58, 0)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(992, 29)
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'INDlyGrLiquidationForm
        '
        Me.INDlyGrLiquidationForm.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlyGrLiquidationForm.AppearanceGroup.Options.UseFont = True
        Me.INDlyGrLiquidationForm.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlyGrLiquidationForm.AppearanceItemCaption.Options.UseFont = True
        Me.INDlyGrLiquidationForm.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGrLiquidationForm.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlyGrLiquidationForm.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlyGrLiquidationForm.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlyGrLiquidationForm.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGrLiquidationForm.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlyGrLiquidationForm.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGrLiquidationForm.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlyGrLiquidationForm.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGrLiquidationForm.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlyGrLiquidationForm, False)
        Me.INDlyGrLiquidationForm.CustomizationFormText = "Seleccione Forma De Liquidación De Cesantías"
        Me.INDlyGrLiquidationForm.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemSanctionDays, Me.INDlyItemLiquidate, Me.INDlyItemConfirm})
        Me.INDlyGrLiquidationForm.Location = New System.Drawing.Point(0, 0)
        Me.INDlyGrLiquidationForm.Name = "INDlyGrLiquidationForm"
        Me.INDlyGrLiquidationForm.Size = New System.Drawing.Size(1074, 161)
        Me.INDlyGrLiquidationForm.Text = "Seleccione Forma De Liquidación De Cesantías"
        '
        'INDlyItemSanctionDays
        '
        Me.INDlyItemSanctionDays.Control = Me.INDrgSanctionDays
        Me.INDlyItemSanctionDays.CustomizationFormText = "Tener En Cuenta Días De Sanción"
        Me.INDlyItemSanctionDays.Location = New System.Drawing.Point(0, 36)
        Me.INDlyItemSanctionDays.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemSanctionDays.MinSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemSanctionDays.Name = "INDlyItemSanctionDays"
        Me.INDlyItemSanctionDays.Size = New System.Drawing.Size(1050, 36)
        Me.INDlyItemSanctionDays.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemSanctionDays.Text = "Tener En Cuenta Días De Sanción"
        Me.INDlyItemSanctionDays.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemSanctionDays.TextSize = New System.Drawing.Size(230, 21)
        Me.INDlyItemSanctionDays.TextToControlDistance = 12
        Me.INDlyItemSanctionDays.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDlyItemLiquidate
        '
        Me.INDlyItemLiquidate.Control = Me.INDrgLiquidate
        Me.INDlyItemLiquidate.CustomizationFormText = "Liquidación"
        Me.INDlyItemLiquidate.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemLiquidate.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemLiquidate.MinSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemLiquidate.Name = "INDlyItemLiquidate"
        Me.INDlyItemLiquidate.Size = New System.Drawing.Size(1050, 36)
        Me.INDlyItemLiquidate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemLiquidate.Text = "Liquidación"
        Me.INDlyItemLiquidate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemLiquidate.TextSize = New System.Drawing.Size(230, 21)
        Me.INDlyItemLiquidate.TextToControlDistance = 12
        '
        'INDlyItemConfirm
        '
        Me.INDlyItemConfirm.Control = Me.INDrgConfirm
        Me.INDlyItemConfirm.CustomizationFormText = "Confirmar"
        Me.INDlyItemConfirm.Location = New System.Drawing.Point(0, 72)
        Me.INDlyItemConfirm.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemConfirm.MinSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemConfirm.Name = "INDlyItemConfirm"
        Me.INDlyItemConfirm.Size = New System.Drawing.Size(1050, 36)
        Me.INDlyItemConfirm.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemConfirm.Text = "Confirmar"
        Me.INDlyItemConfirm.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemConfirm.TextSize = New System.Drawing.Size(230, 21)
        Me.INDlyItemConfirm.TextToControlDistance = 12
        Me.INDlyItemConfirm.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'GridColumn2
        '
        Me.GridColumn2.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn2.Caption = "Selección"
        Me.GridColumn2.FieldName = "Item2"
        Me.GridColumn2.Name = "GridColumn2"
        '
        'GridColumn1
        '
        Me.GridColumn1.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn1.Caption = "Selección"
        Me.GridColumn1.FieldName = "Item2"
        Me.GridColumn1.Name = "GridColumn1"
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        Me.IndigoGridView1.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit1
        '
        'GridColumn564
        '
        Me.GridColumn564.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn564.Caption = "Selección"
        Me.GridColumn564.FieldName = "Item2"
        Me.GridColumn564.Name = "GridColumn564"
        '
        'IndigoGridView11
        '
        Me.IndigoGridView11.RaiseMenuPopUp = True
        Me.IndigoGridView11.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit11
        '
        'RepositoryItemPopupContainerEdit11
        '
        Me.RepositoryItemPopupContainerEdit11.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit11.Name = "RepositoryItemPopupContainerEdit11"
        '
        'FrmUnemployedLiquidation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1008, 741)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.IconOptions.ShowIcon = False
        Me.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.Name = "FrmUnemployedLiquidation"
        Me.Opacity = 1.0R
        Me.Padding = New System.Windows.Forms.Padding(0, 6, 0, 0)
        Me.Tag = "591"
        Me.Text = "Liquidación de Cesantías"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgvUnemployedLiquidationDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgcLiquidationResult, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgvLiquidationResult, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDRepPicEdit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDRepPopupConEdit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPopConCtrMessages, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPopConCtrMessages.ResumeLayout(False)
        CType(Me.INDgcMessages, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgvMessages, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDRepRgSanctions, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyUnemployed, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLyUnemployed.ResumeLayout(False)
        CType(Me.INDrgSanctionDays.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrgConfirm.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDCbeYear.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtResolutionNumber.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdeAuthorizationDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdeAuthorizationDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDmeeUnemployedRetirementReason.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdeUnemployedLiquidationDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdeUnemployedLiquidationDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleEmployee.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgcGroups, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgvGroups, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleCompany.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrgLiquidate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgleUnemployedInterestPaidWithPayroll.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgleUnemployedInterestPaidWithPayroll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrYesNo4View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyGrUnemployed, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyGrYearly, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemCompany, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemGroups, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemYear, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliUnemployedInterestPaidWithPayroll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyGrPartial, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemEmployee, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemUnemployedLiquidationDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyUnemployedRetirementReason, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemAuthorizationDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemResosultionNumber, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyGrLiquidationResult, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemGoBack, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyItemLiquidationResult, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemTextResult, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyGrLiquidationForm, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemSanctionDays, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemLiquidate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemConfirm, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoRadioGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoComboBoxEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit11, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDLyUnemployed As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDLyGrUnemployed As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDrgLiquidate As DevExpress.XtraEditors.RadioGroup
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents INDlyItemLiquidate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents IndigoRadioGroup1 As Presentation.Controls.IndigoRadioGroup
    Friend WithEvents INDSleCompany As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlyItemCompany As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDgcGroups As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDgvGroups As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlyGrYearly As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemGroups As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoComboBoxEdit1 As Presentation.Controls.IndigoComboBoxEdit
    Friend WithEvents INDSleEmployee As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlyGrPartial As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemEmployee As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDdeUnemployedLiquidationDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDlyItemUnemployedLiquidationDate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDmeeUnemployedRetirementReason As DevExpress.XtraEditors.MemoExEdit
    Friend WithEvents INDlyUnemployedRetirementReason As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDdeAuthorizationDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDlyItemAuthorizationDate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDtxtResolutionNumber As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDlyItemResosultionNumber As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDColNitEmployee As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColNameEmployee As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColCompanyCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColCompanyName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColApply As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColGroupCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColGroupName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDCbeYear As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents INDlyItemYear As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDrgConfirm As DevExpress.XtraEditors.RadioGroup
    Friend WithEvents INDlyItemConfirm As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDrgSanctionDays As DevExpress.XtraEditors.RadioGroup
    Friend WithEvents INDlyItemSanctionDays As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDgcLiquidationResult As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDgvLiquidationResult As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents CtrNavigation1 As Presentation.Controls.CtrNavigation
    Friend WithEvents INDLyGrLiquidationResult As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemGoBack As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLyItemLiquidationResult As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDColState As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDRepPicEdit As DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit
    Friend WithEvents INDColEmployeeName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColDateInitial As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColDateEnding As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColWorkedDays As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColAverageMonthNumber As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColTotalUnemployed As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColUnemployedInterestTotal As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDlyGrLiquidationForm As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDPopConCtrMessages As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents INDgcMessages As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDgvMessages As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDColMessage As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColMessages As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDRepPopupConEdit As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDColSanctionsDays As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoDate1 As Presentation.Controls.IndigoDate
    Friend WithEvents INDRepRgSanctions As DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup
    Friend WithEvents INDColConfirm As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDlyItemTextResult As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents INDgvUnemployedLiquidationDetail As DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView
    Friend WithEvents INDColULDMonth As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents INDColULDWorkedDays As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents INDColULDSanctionDays As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents INDColULDUnemployedAverage As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents INDColULDUnemployedInterestAverage As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents GridBand1 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents INDTotalProvisionsUnemployed As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDTotalProvisionsUnemployedNotSanctions As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents RepositoryItemPopupContainerEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents IndigoGridView11 As Controls.IndigoGridView
    Friend WithEvents RepositoryItemPopupContainerEdit11 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents IndigoTextEdit11 As Controls.IndigoTextEdit
    Friend WithEvents INDgleUnemployedInterestPaidWithPayroll As Controls.CtrYesNo
    Friend WithEvents CtrYesNo4View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDliUnemployedInterestPaidWithPayroll As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn564 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColEmployeeNit As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
End Class
