Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmAvailability
    Inherits FormBase

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim GridFormatRule1 As DevExpress.XtraGrid.GridFormatRule = New DevExpress.XtraGrid.GridFormatRule()
        Dim FormatConditionRuleValue1 As DevExpress.XtraEditors.FormatConditionRuleValue = New DevExpress.XtraEditors.FormatConditionRuleValue()
        Dim EditorButtonImageOptions1 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim AppearanceObject1 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject2 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject3 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject4 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject5 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject6 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject7 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject8 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmAvailability))
        Me.INDValue = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrptValue = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.INDCnc = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDLcAvailability = New DevExpress.XtraLayout.LayoutControl()
        Me.INDTxeTermDate = New DevExpress.XtraEditors.TextEdit()
        Me.INDGleAvailabilityType = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GridLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDAvailabilityType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGcCategory = New DevExpress.XtraGrid.GridControl()
        Me.viewBudget = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDCodeCategory = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDNameCategory = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDResource = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDBalance = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDSbAdd = New DevExpress.XtraEditors.SimpleButton()
        Me.INDMemObservation = New DevExpress.XtraEditors.MemoEdit()
        Me.INDTxeValue = New DevExpress.XtraEditors.TextEdit()
        Me.INDSleDependency = New Presentation.Controls.SearchLookUpEditEx()
        Me.SearchLookUpEditExView5 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDCodeDependency = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDNameDependency = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDSeTermDays = New DevExpress.XtraEditors.SpinEdit()
        Me.INDDteDocumentDate = New DevExpress.XtraEditors.DateEdit()
        Me.INDBtnCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.INDsleValidity = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDgvValidity = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDYear = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDStatus = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDResolutionName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDResolutionValue = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDsleBudgetEntity = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDLcgAvailability = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLcgPrincipalData = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemEntity = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciValidity = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLcgGeneralInformation = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciDocumentDate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciTermDays = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciDependency = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciValue = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciObservations = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciAvailabilityType = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciTermDate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLcgCategory = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciAdd = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoDate1 = New Presentation.Controls.IndigoDate(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoGridLookUpControl1 = New Presentation.Controls.IndigoGridLookUpControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.INDGvValidityPopUp = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgcStatusValidityPopUp = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridView3 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.INDsleValidityPopUp = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.LayoutControl2 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDsleEntityPopUp = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlciEntityPopUp = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciValidityPopUp = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.INDpccChangeEntity = New DevExpress.XtraEditors.PopupContainerControl()
        Me.INDCPC = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemSleCPC = New DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit()
        Me.RepositoryItemGvCPC = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrptValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDCnc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcAvailability, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLcAvailability.SuspendLayout()
        CType(Me.INDTxeTermDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGleAvailabilityType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGcCategory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.viewBudget, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDMemObservation.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxeValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleDependency, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEditExView5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSeTermDays.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDteDocumentDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDteDocumentDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDBtnCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleValidity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgvValidity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleBudgetEntity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgAvailability, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgPrincipalData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemEntity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciValidity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgGeneralInformation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciDocumentDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciTermDays, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciDependency, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciObservations, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciAvailabilityType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciTermDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgCategory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciAdd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvValidityPopUp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleValidityPopUp.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl2.SuspendLayout()
        CType(Me.INDsleEntityPopUp.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciEntityPopUp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciValidityPopUp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDpccChangeEntity, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDpccChangeEntity.SuspendLayout()
        CType(Me.RepositoryItemSleCPC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemGvCPC, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDLcAvailability)
        Me.INDPanelControlBase.Controls.Add(Me.INDCnc)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Location = New System.Drawing.Point(0, 24)
        '
        'BarraBotones
        '
        Me.BarraBotones.OperatingUnitVisible = True
        '
        'INDValue
        '
        Me.INDValue.Caption = "Valor"
        Me.INDValue.ColumnEdit = Me.INDrptValue
        Me.INDValue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDValue.FieldName = "InitialValue"
        Me.INDValue.Name = "INDValue"
        Me.INDValue.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "InitialValue", "{0:c0}")})
        Me.INDValue.Visible = True
        Me.INDValue.VisibleIndex = 6
        '
        'INDrptValue
        '
        Me.INDrptValue.AutoHeight = False
        Me.INDrptValue.Mask.EditMask = "c0"
        Me.INDrptValue.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDrptValue.Mask.UseMaskAsDisplayFormat = True
        Me.INDrptValue.MaxLength = 20
        Me.INDrptValue.Name = "INDrptValue"
        '
        'INDCnc
        '
        Me.INDCnc.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDCnc.Dock = System.Windows.Forms.DockStyle.Left
        Me.INDCnc.LayoutControl = Me.INDLcAvailability
        Me.INDCnc.Location = New System.Drawing.Point(2, 7)
        Me.INDCnc.Margin = New System.Windows.Forms.Padding(0)
        Me.INDCnc.Name = "INDCnc"
        Me.INDCnc.Size = New System.Drawing.Size(200, 570)
        Me.INDCnc.TabIndex = 0
        Me.INDCnc.UseDisabledStatePainter = False
        '
        'INDLcAvailability
        '
        Me.INDLcAvailability.Controls.Add(Me.INDTxeTermDate)
        Me.INDLcAvailability.Controls.Add(Me.INDGleAvailabilityType)
        Me.INDLcAvailability.Controls.Add(Me.INDGcCategory)
        Me.INDLcAvailability.Controls.Add(Me.INDSbAdd)
        Me.INDLcAvailability.Controls.Add(Me.INDMemObservation)
        Me.INDLcAvailability.Controls.Add(Me.INDTxeValue)
        Me.INDLcAvailability.Controls.Add(Me.INDSleDependency)
        Me.INDLcAvailability.Controls.Add(Me.INDSeTermDays)
        Me.INDLcAvailability.Controls.Add(Me.INDDteDocumentDate)
        Me.INDLcAvailability.Controls.Add(Me.INDBtnCode)
        Me.INDLcAvailability.Controls.Add(Me.INDsleValidity)
        Me.INDLcAvailability.Controls.Add(Me.INDsleBudgetEntity)
        Me.INDLcAvailability.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDLcAvailability.Location = New System.Drawing.Point(202, 7)
        Me.INDLcAvailability.Name = "INDLcAvailability"
        Me.INDLcAvailability.Root = Me.INDLcgAvailability
        Me.INDLcAvailability.Size = New System.Drawing.Size(1261, 570)
        Me.INDLcAvailability.TabIndex = 1
        Me.INDLcAvailability.Text = "LayoutControl1"
        '
        'INDTxeTermDate
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxeTermDate, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxeTermDate, False)
        Me.INDTxeTermDate.Location = New System.Drawing.Point(52, 271)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxeTermDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxeTermDate.Name = "INDTxeTermDate"
        Me.INDTxeTermDate.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTxeTermDate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxeTermDate.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxeTermDate.Properties.Appearance.Options.UseFont = True
        Me.INDTxeTermDate.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxeTermDate.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxeTermDate.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxeTermDate.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxeTermDate.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxeTermDate.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxeTermDate.Properties.ReadOnly = True
        Me.INDTxeTermDate.Size = New System.Drawing.Size(316, 28)
        Me.INDTxeTermDate.StyleController = Me.INDLcAvailability
        Me.INDTxeTermDate.TabIndex = 25
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxeTermDate, 0)
        '
        'INDGleAvailabilityType
        '
        Me.IndigoGridLookUpControl1.SetAbrirFormularioArchivo(Me.INDGleAvailabilityType, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDGleAvailabilityType, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDGleAvailabilityType, True)
        Me.INDGleAvailabilityType.EnterMoveNextControl = True
        Me.IndigoGridLookUpControl1.SetGuardarXmlGrid(Me.INDGleAvailabilityType, False)
        Me.INDGleAvailabilityType.Location = New System.Drawing.Point(-18, 207)
        Me.IndigoTextEdit1.SetMascara(Me.INDGleAvailabilityType, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDGleAvailabilityType.Name = "INDGleAvailabilityType"
        Me.INDGleAvailabilityType.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDGleAvailabilityType.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGleAvailabilityType.Properties.Appearance.Options.UseBackColor = True
        Me.INDGleAvailabilityType.Properties.Appearance.Options.UseFont = True
        Me.INDGleAvailabilityType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDGleAvailabilityType.Properties.DisplayMember = "Item2"
        Me.INDGleAvailabilityType.Properties.ImmediatePopup = True
        Me.INDGleAvailabilityType.Properties.NullText = ""
        Me.INDGleAvailabilityType.Properties.PopupView = Me.GridLookUpEdit1View
        Me.INDGleAvailabilityType.Properties.ValueMember = "Item1"
        Me.INDGleAvailabilityType.Size = New System.Drawing.Size(386, 28)
        Me.INDGleAvailabilityType.StyleController = Me.INDLcAvailability
        Me.INDGleAvailabilityType.TabIndex = 8
        Me.IndigoGridLookUpControl1.SetTagFormularioAbrir(Me.INDGleAvailabilityType, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDGleAvailabilityType, 0)
        Me.INDGleAvailabilityType.ToolTip = "Este Campo es Necesario"
        '
        'GridLookUpEdit1View
        '
        Me.GridLookUpEdit1View.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridLookUpEdit1View.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridLookUpEdit1View.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridLookUpEdit1View.Appearance.FocusedRow.Options.UseFont = True
        Me.GridLookUpEdit1View.Appearance.FocusedRow.Options.UseForeColor = True
        Me.GridLookUpEdit1View.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridLookUpEdit1View.Appearance.GroupRow.Options.UseFont = True
        Me.GridLookUpEdit1View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridLookUpEdit1View.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridLookUpEdit1View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridLookUpEdit1View.Appearance.Row.Options.UseFont = True
        Me.GridLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDAvailabilityType})
        Me.GridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridLookUpEdit1View.Name = "GridLookUpEdit1View"
        Me.GridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridLookUpEdit1View.OptionsView.EnableAppearanceEvenRow = True
        Me.GridLookUpEdit1View.OptionsView.EnableAppearanceOddRow = True
        Me.GridLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.GridLookUpEdit1View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridLookUpEdit1View, False)
        '
        'INDAvailabilityType
        '
        Me.INDAvailabilityType.Caption = "Tipo Disponibilidad"
        Me.INDAvailabilityType.FieldName = "Item2"
        Me.INDAvailabilityType.Name = "INDAvailabilityType"
        Me.INDAvailabilityType.Visible = True
        Me.INDAvailabilityType.VisibleIndex = 0
        '
        'INDGcCategory
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcCategory, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcCategory, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDGcCategory, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcCategory, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcCategory, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcCategory, False)
        Me.INDGcCategory.Location = New System.Drawing.Point(396, 89)
        Me.INDGcCategory.MainView = Me.viewBudget
        Me.INDGcCategory.Name = "INDGcCategory"
        Me.INDGcCategory.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDrptValue, Me.RepositoryItemSleCPC})
        Me.INDGcCategory.Size = New System.Drawing.Size(824, 444)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcCategory, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDGcCategory.TabIndex = 15
        Me.INDGcCategory.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.viewBudget})
        '
        'viewBudget
        '
        Me.viewBudget.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.viewBudget.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.viewBudget.Appearance.FocusedRow.Options.UseBackColor = True
        Me.viewBudget.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.viewBudget.Appearance.FocusedRow.Options.UseFont = True
        Me.viewBudget.Appearance.FocusedRow.Options.UseForeColor = True
        Me.viewBudget.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.viewBudget.Appearance.GroupRow.Options.UseFont = True
        Me.viewBudget.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.viewBudget.Appearance.HeaderPanel.Options.UseFont = True
        Me.viewBudget.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.viewBudget.Appearance.Row.Options.UseFont = True
        Me.viewBudget.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.viewBudget.Appearance.ViewCaption.Options.UseFont = True
        Me.viewBudget.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDCodeCategory, Me.INDNameCategory, Me.INDResource, Me.INDType, Me.INDCPC, Me.INDBalance, Me.INDValue})
        GridFormatRule1.Column = Me.INDValue
        GridFormatRule1.Name = "Format0"
        FormatConditionRuleValue1.Appearance.BackColor = System.Drawing.Color.MistyRose
        FormatConditionRuleValue1.Appearance.Options.UseBackColor = True
        FormatConditionRuleValue1.Condition = DevExpress.XtraEditors.FormatCondition.Expression
        FormatConditionRuleValue1.Expression = "[ValueBalance] < [InitialValue] Or [InitialValue] = 0 Or [InitialValue] < 0"
        GridFormatRule1.Rule = FormatConditionRuleValue1
        Me.viewBudget.FormatRules.Add(GridFormatRule1)
        Me.viewBudget.GridControl = Me.INDGcCategory
        Me.viewBudget.Name = "viewBudget"
        Me.viewBudget.OptionsView.EnableAppearanceEvenRow = True
        Me.viewBudget.OptionsView.EnableAppearanceOddRow = True
        Me.viewBudget.OptionsView.ShowAutoFilterRow = True
        Me.viewBudget.OptionsView.ShowDetailButtons = False
        Me.viewBudget.OptionsView.ShowFooter = True
        Me.viewBudget.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.viewBudget, False)
        '
        'INDCodeCategory
        '
        Me.INDCodeCategory.Caption = "Código"
        Me.INDCodeCategory.FieldName = "CodeCategory"
        Me.INDCodeCategory.Name = "INDCodeCategory"
        Me.INDCodeCategory.OptionsColumn.AllowEdit = False
        Me.INDCodeCategory.OptionsColumn.AllowFocus = False
        Me.INDCodeCategory.Visible = True
        Me.INDCodeCategory.VisibleIndex = 0
        '
        'INDNameCategory
        '
        Me.INDNameCategory.Caption = "Nombre"
        Me.INDNameCategory.FieldName = "NameCategory"
        Me.INDNameCategory.Name = "INDNameCategory"
        Me.INDNameCategory.OptionsColumn.AllowEdit = False
        Me.INDNameCategory.OptionsColumn.AllowFocus = False
        Me.INDNameCategory.Visible = True
        Me.INDNameCategory.VisibleIndex = 1
        '
        'INDResource
        '
        Me.INDResource.Caption = "Recurso"
        Me.INDResource.FieldName = "CodeNameFinancialSource"
        Me.INDResource.Name = "INDResource"
        Me.INDResource.OptionsColumn.AllowEdit = False
        Me.INDResource.OptionsColumn.AllowFocus = False
        Me.INDResource.Visible = True
        Me.INDResource.VisibleIndex = 2
        '
        'INDType
        '
        Me.INDType.Caption = "Tipo"
        Me.INDType.FieldName = "CodeNameRevenueType"
        Me.INDType.Name = "INDType"
        Me.INDType.OptionsColumn.AllowEdit = False
        Me.INDType.OptionsColumn.AllowFocus = False
        Me.INDType.Visible = True
        Me.INDType.VisibleIndex = 3
        '
        'INDBalance
        '
        Me.INDBalance.Caption = "Saldo"
        Me.INDBalance.DisplayFormat.FormatString = "c0"
        Me.INDBalance.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDBalance.FieldName = "ValueBalance"
        Me.INDBalance.Name = "INDBalance"
        Me.INDBalance.OptionsColumn.AllowEdit = False
        Me.INDBalance.OptionsColumn.AllowFocus = False
        Me.INDBalance.Visible = True
        Me.INDBalance.VisibleIndex = 5
        '
        'INDSbAdd
        '
        Me.INDSbAdd.Location = New System.Drawing.Point(396, 53)
        Me.INDSbAdd.Name = "INDSbAdd"
        Me.INDSbAdd.Size = New System.Drawing.Size(824, 32)
        Me.INDSbAdd.StyleController = Me.INDLcAvailability
        Me.INDSbAdd.TabIndex = 13
        Me.INDSbAdd.Text = "Agregar"
        '
        'INDMemObservation
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDMemObservation, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDMemObservation, True)
        Me.INDMemObservation.EnterMoveNextControl = True
        Me.INDMemObservation.Location = New System.Drawing.Point(-18, 399)
        Me.IndigoTextEdit1.SetMascara(Me.INDMemObservation, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDMemObservation.Name = "INDMemObservation"
        Me.INDMemObservation.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDMemObservation.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDMemObservation.Properties.Appearance.Options.UseBackColor = True
        Me.INDMemObservation.Properties.Appearance.Options.UseFont = True
        Me.INDMemObservation.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDMemObservation.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDMemObservation.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDMemObservation.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDMemObservation.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDMemObservation.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDMemObservation.Properties.MaxLength = 5000
        Me.INDMemObservation.Size = New System.Drawing.Size(386, 70)
        Me.INDMemObservation.StyleController = Me.INDLcAvailability
        Me.INDMemObservation.TabIndex = 11
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDMemObservation, 0)
        Me.INDMemObservation.ToolTip = "Este Campo es Necesario"
        '
        'INDTxeValue
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxeValue, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxeValue, False)
        Me.INDTxeValue.EnterMoveNextControl = True
        Me.INDTxeValue.Location = New System.Drawing.Point(-18, 499)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxeValue, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxeValue.Name = "INDTxeValue"
        Me.INDTxeValue.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTxeValue.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxeValue.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxeValue.Properties.Appearance.Options.UseFont = True
        Me.INDTxeValue.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxeValue.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxeValue.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxeValue.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxeValue.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxeValue.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxeValue.Properties.Mask.EditMask = "c0"
        Me.INDTxeValue.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDTxeValue.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDTxeValue.Properties.ReadOnly = True
        Me.INDTxeValue.Size = New System.Drawing.Size(386, 28)
        Me.INDTxeValue.StyleController = Me.INDLcAvailability
        Me.INDTxeValue.TabIndex = 12
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxeValue, 0)
        '
        'INDSleDependency
        '
        Me.INDSleDependency.AllowQueryOne = True
        Me.INDSleDependency.Datasource = Nothing
        Me.INDSleDependency.DisplayMember = "{Code} - {Name}"
        Me.INDSleDependency.DisplayNullText = ""
        Me.INDSleDependency.EditValue = Nothing
        Me.INDSleDependency.EnterMoveNextControl = True
        Me.INDSleDependency.FuncQueryOnKeyEnterPressed = Nothing
        Me.INDSleDependency.IdOpenForm = 0
        Me.INDSleDependency.Location = New System.Drawing.Point(-18, 335)
        Me.INDSleDependency.MaximumSize = New System.Drawing.Size(5000, 28)
        Me.INDSleDependency.MinimumSize = New System.Drawing.Size(100, 28)
        Me.INDSleDependency.Name = "INDSleDependency"
        Me.INDSleDependency.PopUpFormSize = New System.Drawing.Size(500, 400)
        Me.INDSleDependency.Size = New System.Drawing.Size(386, 28)
        Me.INDSleDependency.TabIndex = 10
        Me.INDSleDependency.ValueMember = "Id"
        Me.INDSleDependency.View = Me.SearchLookUpEditExView5
        '
        'SearchLookUpEditExView5
        '
        Me.SearchLookUpEditExView5.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.SearchLookUpEditExView5.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.SearchLookUpEditExView5.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.SearchLookUpEditExView5.Appearance.FocusedRow.Options.UseFont = True
        Me.SearchLookUpEditExView5.Appearance.FocusedRow.Options.UseForeColor = True
        Me.SearchLookUpEditExView5.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEditExView5.Appearance.GroupRow.Options.UseFont = True
        Me.SearchLookUpEditExView5.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEditExView5.Appearance.HeaderPanel.Options.UseFont = True
        Me.SearchLookUpEditExView5.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.SearchLookUpEditExView5.Appearance.Row.Options.UseFont = True
        Me.SearchLookUpEditExView5.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.SearchLookUpEditExView5.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDCodeDependency, Me.INDNameDependency})
        Me.SearchLookUpEditExView5.Name = "SearchLookUpEditExView5"
        Me.SearchLookUpEditExView5.OptionsCustomization.AllowGroup = False
        Me.SearchLookUpEditExView5.OptionsDetail.EnableMasterViewMode = False
        Me.SearchLookUpEditExView5.OptionsDetail.ShowDetailTabs = False
        Me.SearchLookUpEditExView5.OptionsFind.AlwaysVisible = True
        Me.SearchLookUpEditExView5.OptionsFind.FindFilterColumns = "Code"
        Me.SearchLookUpEditExView5.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEditExView5.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEditExView5.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEditExView5.OptionsView.ShowDetailButtons = False
        Me.SearchLookUpEditExView5.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEditExView5, False)
        '
        'INDCodeDependency
        '
        Me.INDCodeDependency.Caption = "Código"
        Me.INDCodeDependency.FieldName = "Code"
        Me.INDCodeDependency.Name = "INDCodeDependency"
        Me.INDCodeDependency.OptionsColumn.AllowEdit = False
        Me.INDCodeDependency.Visible = True
        Me.INDCodeDependency.VisibleIndex = 0
        '
        'INDNameDependency
        '
        Me.INDNameDependency.Caption = "Dependencia"
        Me.INDNameDependency.FieldName = "Name"
        Me.INDNameDependency.Name = "INDNameDependency"
        Me.INDNameDependency.OptionsColumn.AllowEdit = False
        Me.INDNameDependency.Visible = True
        Me.INDNameDependency.VisibleIndex = 1
        '
        'INDSeTermDays
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSeTermDays, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSeTermDays, True)
        Me.INDSeTermDays.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDSeTermDays.Location = New System.Drawing.Point(-18, 271)
        Me.IndigoTextEdit1.SetMascara(Me.INDSeTermDays, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSeTermDays.Name = "INDSeTermDays"
        Me.INDSeTermDays.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDSeTermDays.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSeTermDays.Properties.Appearance.Options.UseBackColor = True
        Me.INDSeTermDays.Properties.Appearance.Options.UseFont = True
        Me.INDSeTermDays.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDSeTermDays.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDSeTermDays.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSeTermDays.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDSeTermDays.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDSeTermDays.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSeTermDays.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSeTermDays.Properties.DisplayFormat.FormatString = "0"
        Me.INDSeTermDays.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDSeTermDays.Properties.EditFormat.FormatString = "0"
        Me.INDSeTermDays.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDSeTermDays.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None
        Me.INDSeTermDays.Size = New System.Drawing.Size(66, 28)
        Me.INDSeTermDays.StyleController = Me.INDLcAvailability
        Me.INDSeTermDays.TabIndex = 9
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSeTermDays, 0)
        '
        'INDDteDocumentDate
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDDteDocumentDate, True)
        Me.IndigoDate1.SetCampoObligatorio(Me.INDDteDocumentDate, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDDteDocumentDate, True)
        Me.INDDteDocumentDate.EditValue = Nothing
        Me.INDDteDocumentDate.EnterMoveNextControl = True
        Me.INDDteDocumentDate.Location = New System.Drawing.Point(-18, 143)
        Me.IndigoTextEdit1.SetMascara(Me.INDDteDocumentDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoDate1.SetMascaraDate(Me.INDDteDocumentDate, Presentation.Controls.IndigoDate.EMask.FechaHoraSegundos)
        Me.INDDteDocumentDate.Name = "INDDteDocumentDate"
        Me.INDDteDocumentDate.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDDteDocumentDate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDteDocumentDate.Properties.Appearance.Options.UseBackColor = True
        Me.INDDteDocumentDate.Properties.Appearance.Options.UseFont = True
        Me.INDDteDocumentDate.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDDteDocumentDate.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDDteDocumentDate.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDteDocumentDate.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDDteDocumentDate.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDDteDocumentDate.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDDteDocumentDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDteDocumentDate.Properties.CalendarTimeEditing = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDDteDocumentDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDteDocumentDate.Properties.Mask.EditMask = "dd \de MMMM \de yyyy  HH:mm:ss"
        Me.INDDteDocumentDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDDteDocumentDate.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDDteDocumentDate.Size = New System.Drawing.Size(386, 28)
        Me.INDDteDocumentDate.StyleController = Me.INDLcAvailability
        Me.INDDteDocumentDate.TabIndex = 7
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDDteDocumentDate, 0)
        Me.INDDteDocumentDate.ToolTip = "Este Campo es Necesario"
        '
        'INDBtnCode
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDBtnCode, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDBtnCode, True)
        Me.INDBtnCode.Location = New System.Drawing.Point(-18, 79)
        Me.IndigoTextEdit1.SetMascara(Me.INDBtnCode, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDBtnCode.Name = "INDBtnCode"
        Me.INDBtnCode.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDBtnCode.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBtnCode.Properties.Appearance.Options.UseBackColor = True
        Me.INDBtnCode.Properties.Appearance.Options.UseFont = True
        Me.INDBtnCode.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDBtnCode.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDBtnCode.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBtnCode.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDBtnCode.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDBtnCode.Properties.AppearanceFocused.Options.UseFont = True
        EditorButtonImageOptions1.Image = Global.Presentation.Budget.My.Resources.Resources.BuscarMetro
        Me.INDBtnCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, True, True, False, EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, "", Nothing, Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.INDBtnCode.Size = New System.Drawing.Size(386, 28)
        Me.INDBtnCode.StyleController = Me.INDLcAvailability
        Me.INDBtnCode.TabIndex = 6
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDBtnCode, 0)
        Me.INDBtnCode.ToolTip = "Este Campo es Necesario"
        '
        'INDsleValidity
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleValidity, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleValidity, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleValidity, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleValidity, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDsleValidity, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleValidity, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleValidity, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleValidity, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleValidity, False)
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleValidity, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleValidity, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleValidity, False)
        Me.INDsleValidity.Location = New System.Drawing.Point(-432, 143)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleValidity, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleValidity.Name = "INDsleValidity"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleValidity, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleValidity, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleValidity, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDsleValidity, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleValidity, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleValidity, False)
        Me.INDsleValidity.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDsleValidity.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleValidity.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDsleValidity.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleValidity.Properties.Appearance.Options.UseFont = True
        Me.INDsleValidity.Properties.Appearance.Options.UseForeColor = True
        Me.INDsleValidity.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleValidity.Properties.DisplayMember = "Year"
        Me.INDsleValidity.Properties.NullText = ""
        Me.INDsleValidity.Properties.PopupSizeable = False
        Me.INDsleValidity.Properties.PopupView = Me.INDgvValidity
        Me.INDsleValidity.Properties.ShowFooter = False
        Me.INDsleValidity.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleValidity, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleValidity, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleValidity, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleValidity, True)
        Me.INDsleValidity.Size = New System.Drawing.Size(386, 28)
        Me.INDsleValidity.StyleController = Me.INDLcAvailability
        Me.INDsleValidity.TabIndex = 5
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleValidity, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleValidity, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleValidity, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleValidity, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleValidity, False)
        '
        'INDgvValidity
        '
        Me.INDgvValidity.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDgvValidity.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDgvValidity.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDgvValidity.Appearance.FocusedRow.Options.UseFont = True
        Me.INDgvValidity.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDgvValidity.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvValidity.Appearance.GroupRow.Options.UseFont = True
        Me.INDgvValidity.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvValidity.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDgvValidity.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDgvValidity.Appearance.Row.Options.UseFont = True
        Me.INDgvValidity.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDYear, Me.INDStatus, Me.INDResolutionName, Me.INDResolutionValue})
        Me.INDgvValidity.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDgvValidity.Name = "INDgvValidity"
        Me.INDgvValidity.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDgvValidity.OptionsView.EnableAppearanceEvenRow = True
        Me.INDgvValidity.OptionsView.EnableAppearanceOddRow = True
        Me.INDgvValidity.OptionsView.ShowAutoFilterRow = True
        Me.INDgvValidity.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDgvValidity, False)
        '
        'INDYear
        '
        Me.INDYear.Caption = "Año"
        Me.INDYear.FieldName = "Year"
        Me.INDYear.Name = "INDYear"
        Me.INDYear.Visible = True
        Me.INDYear.VisibleIndex = 0
        '
        'INDStatus
        '
        Me.INDStatus.Caption = "Estado"
        Me.INDStatus.FieldName = "StatusText"
        Me.INDStatus.Name = "INDStatus"
        Me.INDStatus.Visible = True
        Me.INDStatus.VisibleIndex = 1
        '
        'INDResolutionName
        '
        Me.INDResolutionName.Caption = "Resolución"
        Me.INDResolutionName.FieldName = "ResolutionNumber"
        Me.INDResolutionName.Name = "INDResolutionName"
        Me.INDResolutionName.Visible = True
        Me.INDResolutionName.VisibleIndex = 2
        '
        'INDResolutionValue
        '
        Me.INDResolutionValue.Caption = "Valor"
        Me.INDResolutionValue.DisplayFormat.FormatString = "c0"
        Me.INDResolutionValue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDResolutionValue.FieldName = "ResolutionValue"
        Me.INDResolutionValue.Name = "INDResolutionValue"
        Me.INDResolutionValue.Visible = True
        Me.INDResolutionValue.VisibleIndex = 3
        '
        'INDsleBudgetEntity
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleBudgetEntity, AppearanceObject3)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleBudgetEntity, AppearanceObject4)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleBudgetEntity, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleBudgetEntity, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDsleBudgetEntity, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleBudgetEntity, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleBudgetEntity, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleBudgetEntity, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleBudgetEntity, False)
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleBudgetEntity, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleBudgetEntity, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleBudgetEntity, False)
        Me.INDsleBudgetEntity.Location = New System.Drawing.Point(-432, 79)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleBudgetEntity, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleBudgetEntity.Name = "INDsleBudgetEntity"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleBudgetEntity, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleBudgetEntity, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleBudgetEntity, True)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDsleBudgetEntity, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleBudgetEntity, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleBudgetEntity, False)
        Me.INDsleBudgetEntity.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDsleBudgetEntity.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleBudgetEntity.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDsleBudgetEntity.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleBudgetEntity.Properties.Appearance.Options.UseFont = True
        Me.INDsleBudgetEntity.Properties.Appearance.Options.UseForeColor = True
        Me.INDsleBudgetEntity.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleBudgetEntity.Properties.DisplayMember = "NameCode"
        Me.INDsleBudgetEntity.Properties.NullText = ""
        Me.INDsleBudgetEntity.Properties.PopupSizeable = False
        Me.INDsleBudgetEntity.Properties.PopupView = Me.SearchLookUpEdit1View
        Me.INDsleBudgetEntity.Properties.ShowFooter = False
        Me.INDsleBudgetEntity.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleBudgetEntity, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleBudgetEntity, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleBudgetEntity, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleBudgetEntity, True)
        Me.INDsleBudgetEntity.Size = New System.Drawing.Size(386, 28)
        Me.INDsleBudgetEntity.StyleController = Me.INDLcAvailability
        Me.INDsleBudgetEntity.TabIndex = 4
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleBudgetEntity, "200")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleBudgetEntity, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleBudgetEntity, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleBudgetEntity, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleBudgetEntity, False)
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
        Me.SearchLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDCode, Me.INDName})
        Me.SearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit1View.Name = "SearchLookUpEdit1View"
        Me.SearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEdit1View, False)
        '
        'INDCode
        '
        Me.INDCode.Caption = "Código"
        Me.INDCode.FieldName = "Code"
        Me.INDCode.Name = "INDCode"
        Me.INDCode.Visible = True
        Me.INDCode.VisibleIndex = 0
        '
        'INDName
        '
        Me.INDName.Caption = "Nombre"
        Me.INDName.FieldName = "Name"
        Me.INDName.Name = "INDName"
        Me.INDName.Visible = True
        Me.INDName.VisibleIndex = 1
        '
        'INDLcgAvailability
        '
        Me.INDLcgAvailability.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgAvailability.AppearanceGroup.Options.UseFont = True
        Me.INDLcgAvailability.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgAvailability.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgAvailability.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgAvailability.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgAvailability.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgAvailability.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgAvailability.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgAvailability.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgAvailability.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgAvailability.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgAvailability.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgAvailability.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgAvailability, False)
        Me.INDLcgAvailability.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDLcgAvailability.GroupBordersVisible = False
        Me.INDLcgAvailability.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLcgPrincipalData, Me.INDLcgGeneralInformation, Me.INDLcgCategory})
        Me.INDLcgAvailability.Name = "INDLcgAvailability"
        Me.INDLcgAvailability.Size = New System.Drawing.Size(1700, 557)
        Me.INDLcgAvailability.TextVisible = False
        '
        'INDLcgPrincipalData
        '
        Me.INDLcgPrincipalData.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgPrincipalData.AppearanceGroup.Options.UseFont = True
        Me.INDLcgPrincipalData.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgPrincipalData.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgPrincipalData.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgPrincipalData.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgPrincipalData.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgPrincipalData.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgPrincipalData.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgPrincipalData.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgPrincipalData.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgPrincipalData.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgPrincipalData.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgPrincipalData.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgPrincipalData, False)
        Me.INDLcgPrincipalData.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemEntity, Me.INDlciValidity})
        Me.INDLcgPrincipalData.Location = New System.Drawing.Point(0, 0)
        Me.INDLcgPrincipalData.Name = "INDLcgPrincipalData"
        Me.INDLcgPrincipalData.Size = New System.Drawing.Size(414, 537)
        Me.INDLcgPrincipalData.Text = "Datos Principales"
        '
        'INDlyItemEntity
        '
        Me.INDlyItemEntity.Control = Me.INDsleBudgetEntity
        Me.INDlyItemEntity.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemEntity.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlyItemEntity.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlyItemEntity.Name = "INDlyItemEntity"
        Me.INDlyItemEntity.Size = New System.Drawing.Size(390, 64)
        Me.INDlyItemEntity.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemEntity.Text = "Entidad Presupuestal"
        Me.INDlyItemEntity.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemEntity.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemEntity.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemEntity.TextToControlDistance = 5
        '
        'INDlciValidity
        '
        Me.INDlciValidity.Control = Me.INDsleValidity
        Me.INDlciValidity.Location = New System.Drawing.Point(0, 64)
        Me.INDlciValidity.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlciValidity.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlciValidity.Name = "INDlciValidity"
        Me.INDlciValidity.Size = New System.Drawing.Size(390, 420)
        Me.INDlciValidity.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciValidity.Text = "Vigencia"
        Me.INDlciValidity.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciValidity.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciValidity.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlciValidity.TextToControlDistance = 5
        '
        'INDLcgGeneralInformation
        '
        Me.INDLcgGeneralInformation.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgGeneralInformation.AppearanceGroup.Options.UseFont = True
        Me.INDLcgGeneralInformation.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgGeneralInformation.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgGeneralInformation.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgGeneralInformation.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgGeneralInformation.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgGeneralInformation.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgGeneralInformation.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgGeneralInformation.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgGeneralInformation.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgGeneralInformation.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgGeneralInformation.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgGeneralInformation.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgGeneralInformation, False)
        Me.INDLcgGeneralInformation.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciCode, Me.INDLciDocumentDate, Me.INDLciTermDays, Me.INDLciDependency, Me.INDLciValue, Me.INDLciObservations, Me.INDLciAvailabilityType, Me.INDLciTermDate})
        Me.INDLcgGeneralInformation.Location = New System.Drawing.Point(414, 0)
        Me.INDLcgGeneralInformation.Name = "INDLcgGeneralInformation"
        Me.INDLcgGeneralInformation.Size = New System.Drawing.Size(414, 537)
        Me.INDLcgGeneralInformation.Text = "Información General"
        '
        'INDLciCode
        '
        Me.INDLciCode.Control = Me.INDBtnCode
        Me.INDLciCode.Location = New System.Drawing.Point(0, 0)
        Me.INDLciCode.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLciCode.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLciCode.Name = "INDLciCode"
        Me.INDLciCode.Size = New System.Drawing.Size(390, 64)
        Me.INDLciCode.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciCode.Text = "Código"
        Me.INDLciCode.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciCode.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciCode.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciCode.TextToControlDistance = 5
        '
        'INDLciDocumentDate
        '
        Me.INDLciDocumentDate.Control = Me.INDDteDocumentDate
        Me.INDLciDocumentDate.Location = New System.Drawing.Point(0, 64)
        Me.INDLciDocumentDate.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLciDocumentDate.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLciDocumentDate.Name = "INDLciDocumentDate"
        Me.INDLciDocumentDate.Size = New System.Drawing.Size(390, 64)
        Me.INDLciDocumentDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciDocumentDate.Text = "Fecha"
        Me.INDLciDocumentDate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciDocumentDate.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciDocumentDate.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciDocumentDate.TextToControlDistance = 5
        '
        'INDLciTermDays
        '
        Me.INDLciTermDays.Control = Me.INDSeTermDays
        Me.INDLciTermDays.Location = New System.Drawing.Point(0, 192)
        Me.INDLciTermDays.MaxSize = New System.Drawing.Size(70, 64)
        Me.INDLciTermDays.MinSize = New System.Drawing.Size(70, 64)
        Me.INDLciTermDays.Name = "INDLciTermDays"
        Me.INDLciTermDays.Size = New System.Drawing.Size(70, 64)
        Me.INDLciTermDays.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciTermDays.Text = "Plazo"
        Me.INDLciTermDays.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciTermDays.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciTermDays.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciTermDays.TextToControlDistance = 5
        '
        'INDLciDependency
        '
        Me.INDLciDependency.Control = Me.INDSleDependency
        Me.INDLciDependency.Location = New System.Drawing.Point(0, 256)
        Me.INDLciDependency.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLciDependency.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLciDependency.Name = "INDLciDependency"
        Me.INDLciDependency.Size = New System.Drawing.Size(390, 64)
        Me.INDLciDependency.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciDependency.Text = "Dependencia"
        Me.INDLciDependency.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciDependency.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciDependency.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciDependency.TextToControlDistance = 5
        '
        'INDLciValue
        '
        Me.INDLciValue.Control = Me.INDTxeValue
        Me.INDLciValue.Location = New System.Drawing.Point(0, 420)
        Me.INDLciValue.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLciValue.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLciValue.Name = "INDLciValue"
        Me.INDLciValue.Size = New System.Drawing.Size(390, 64)
        Me.INDLciValue.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciValue.Text = "Vr. Total CDP"
        Me.INDLciValue.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciValue.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciValue.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciValue.TextToControlDistance = 5
        '
        'INDLciObservations
        '
        Me.INDLciObservations.Control = Me.INDMemObservation
        Me.INDLciObservations.Location = New System.Drawing.Point(0, 320)
        Me.INDLciObservations.MaxSize = New System.Drawing.Size(390, 100)
        Me.INDLciObservations.MinSize = New System.Drawing.Size(390, 100)
        Me.INDLciObservations.Name = "INDLciObservations"
        Me.INDLciObservations.Size = New System.Drawing.Size(390, 100)
        Me.INDLciObservations.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciObservations.Text = "Observaciones"
        Me.INDLciObservations.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciObservations.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciObservations.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciObservations.TextToControlDistance = 5
        '
        'INDLciAvailabilityType
        '
        Me.INDLciAvailabilityType.Control = Me.INDGleAvailabilityType
        Me.INDLciAvailabilityType.Location = New System.Drawing.Point(0, 128)
        Me.INDLciAvailabilityType.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLciAvailabilityType.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLciAvailabilityType.Name = "INDLciAvailabilityType"
        Me.INDLciAvailabilityType.Size = New System.Drawing.Size(390, 64)
        Me.INDLciAvailabilityType.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciAvailabilityType.Text = "Tipo Disponibilidad"
        Me.INDLciAvailabilityType.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciAvailabilityType.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciAvailabilityType.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciAvailabilityType.TextToControlDistance = 5
        '
        'INDLciTermDate
        '
        Me.INDLciTermDate.Control = Me.INDTxeTermDate
        Me.INDLciTermDate.Location = New System.Drawing.Point(70, 192)
        Me.INDLciTermDate.MaxSize = New System.Drawing.Size(320, 64)
        Me.INDLciTermDate.MinSize = New System.Drawing.Size(320, 64)
        Me.INDLciTermDate.Name = "INDLciTermDate"
        Me.INDLciTermDate.Size = New System.Drawing.Size(320, 64)
        Me.INDLciTermDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciTermDate.Text = " "
        Me.INDLciTermDate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciTermDate.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciTermDate.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciTermDate.TextToControlDistance = 5
        '
        'INDLcgCategory
        '
        Me.INDLcgCategory.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgCategory.AppearanceGroup.Options.UseFont = True
        Me.INDLcgCategory.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgCategory.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgCategory.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgCategory.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgCategory.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgCategory.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgCategory.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgCategory.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgCategory.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgCategory.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgCategory.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgCategory.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgCategory, False)
        Me.INDLcgCategory.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciAdd, Me.LayoutControlItem1})
        Me.INDLcgCategory.Location = New System.Drawing.Point(828, 0)
        Me.INDLcgCategory.Name = "INDLcgCategory"
        Me.INDLcgCategory.Size = New System.Drawing.Size(852, 537)
        Me.INDLcgCategory.Text = "Rubros"
        '
        'INDLciAdd
        '
        Me.INDLciAdd.Control = Me.INDSbAdd
        Me.INDLciAdd.Location = New System.Drawing.Point(0, 0)
        Me.INDLciAdd.MaxSize = New System.Drawing.Size(828, 36)
        Me.INDLciAdd.MinSize = New System.Drawing.Size(828, 36)
        Me.INDLciAdd.Name = "INDLciAdd"
        Me.INDLciAdd.Size = New System.Drawing.Size(828, 36)
        Me.INDLciAdd.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciAdd.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciAdd.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDGcCategory
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 36)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(828, 448)
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        '
        'INDGvValidityPopUp
        '
        Me.INDGvValidityPopUp.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvValidityPopUp.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvValidityPopUp.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvValidityPopUp.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvValidityPopUp.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvValidityPopUp.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvValidityPopUp.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvValidityPopUp.Appearance.Row.Options.UseFont = True
        Me.INDGvValidityPopUp.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn5, Me.INDgcStatusValidityPopUp, Me.GridColumn7, Me.GridColumn8})
        Me.INDGvValidityPopUp.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDGvValidityPopUp.Name = "INDGvValidityPopUp"
        Me.INDGvValidityPopUp.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDGvValidityPopUp.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvValidityPopUp.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvValidityPopUp.OptionsView.ShowAutoFilterRow = True
        Me.INDGvValidityPopUp.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvValidityPopUp, False)
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "Año"
        Me.GridColumn5.FieldName = "Year"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 0
        '
        'INDgcStatusValidityPopUp
        '
        Me.INDgcStatusValidityPopUp.Caption = "Estado"
        Me.INDgcStatusValidityPopUp.FieldName = "StatusText"
        Me.INDgcStatusValidityPopUp.Name = "INDgcStatusValidityPopUp"
        Me.INDgcStatusValidityPopUp.Visible = True
        Me.INDgcStatusValidityPopUp.VisibleIndex = 1
        '
        'GridColumn7
        '
        Me.GridColumn7.Caption = "Resolución"
        Me.GridColumn7.FieldName = "ResolutionNumber"
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.Visible = True
        Me.GridColumn7.VisibleIndex = 2
        '
        'GridColumn8
        '
        Me.GridColumn8.Caption = "Valor"
        Me.GridColumn8.DisplayFormat.FormatString = "c0"
        Me.GridColumn8.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn8.FieldName = "ResolutionValue"
        Me.GridColumn8.Name = "GridColumn8"
        Me.GridColumn8.Visible = True
        Me.GridColumn8.VisibleIndex = 3
        '
        'GridView3
        '
        Me.GridView3.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridView3.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridView3.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView3.Appearance.GroupRow.Options.UseFont = True
        Me.GridView3.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView3.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView3.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridView3.Appearance.Row.Options.UseFont = True
        Me.GridView3.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn3, Me.GridColumn4})
        Me.GridView3.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView3.Name = "GridView3"
        Me.GridView3.OptionsCustomization.AllowGroup = False
        Me.GridView3.OptionsDetail.EnableMasterViewMode = False
        Me.GridView3.OptionsDetail.ShowDetailTabs = False
        Me.GridView3.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView3.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView3.OptionsView.EnableAppearanceOddRow = True
        Me.GridView3.OptionsView.ShowAutoFilterRow = True
        Me.GridView3.OptionsView.ShowDetailButtons = False
        Me.GridView3.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridView3, False)
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Código"
        Me.GridColumn3.FieldName = "Code"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.OptionsColumn.AllowEdit = False
        Me.GridColumn3.OptionsColumn.AllowFocus = False
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 0
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Nombre"
        Me.GridColumn4.FieldName = "Name"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.OptionsColumn.AllowEdit = False
        Me.GridColumn4.OptionsColumn.AllowFocus = False
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 1
        '
        'INDsleValidityPopUp
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleValidityPopUp, AppearanceObject5)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleValidityPopUp, AppearanceObject6)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleValidityPopUp, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleValidityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDsleValidityPopUp, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleValidityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleValidityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleValidityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleValidityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleValidityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleValidityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleValidityPopUp, False)
        Me.INDsleValidityPopUp.Location = New System.Drawing.Point(12, 96)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleValidityPopUp, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleValidityPopUp.Name = "INDsleValidityPopUp"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleValidityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleValidityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleValidityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDsleValidityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleValidityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleValidityPopUp, False)
        Me.INDsleValidityPopUp.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDsleValidityPopUp.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleValidityPopUp.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDsleValidityPopUp.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleValidityPopUp.Properties.Appearance.Options.UseFont = True
        Me.INDsleValidityPopUp.Properties.Appearance.Options.UseForeColor = True
        Me.INDsleValidityPopUp.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleValidityPopUp.Properties.DisplayMember = "Year"
        Me.INDsleValidityPopUp.Properties.NullText = ""
        Me.INDsleValidityPopUp.Properties.PopupSizeable = False
        Me.INDsleValidityPopUp.Properties.PopupView = Me.INDGvValidityPopUp
        Me.INDsleValidityPopUp.Properties.ShowFooter = False
        Me.INDsleValidityPopUp.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleValidityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleValidityPopUp, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleValidityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleValidityPopUp, True)
        Me.INDsleValidityPopUp.Size = New System.Drawing.Size(275, 28)
        Me.INDsleValidityPopUp.StyleController = Me.LayoutControl2
        Me.INDsleValidityPopUp.TabIndex = 17
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleValidityPopUp, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleValidityPopUp, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleValidityPopUp, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleValidityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleValidityPopUp, False)
        '
        'LayoutControl2
        '
        Me.LayoutControl2.Controls.Add(Me.INDsleValidityPopUp)
        Me.LayoutControl2.Controls.Add(Me.INDsleEntityPopUp)
        Me.LayoutControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl2.Name = "LayoutControl2"
        Me.LayoutControl2.Root = Me.LayoutControlGroup2
        Me.LayoutControl2.Size = New System.Drawing.Size(299, 150)
        Me.LayoutControl2.TabIndex = 0
        Me.LayoutControl2.Text = "LayoutControl2"
        '
        'INDsleEntityPopUp
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleEntityPopUp, AppearanceObject7)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleEntityPopUp, AppearanceObject8)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleEntityPopUp, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleEntityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDsleEntityPopUp, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleEntityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleEntityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleEntityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleEntityPopUp, False)
        Me.INDsleEntityPopUp.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleEntityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleEntityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleEntityPopUp, False)
        Me.INDsleEntityPopUp.Location = New System.Drawing.Point(12, 32)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleEntityPopUp, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleEntityPopUp.Name = "INDsleEntityPopUp"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleEntityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleEntityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleEntityPopUp, True)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDsleEntityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleEntityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleEntityPopUp, False)
        Me.INDsleEntityPopUp.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDsleEntityPopUp.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleEntityPopUp.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDsleEntityPopUp.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleEntityPopUp.Properties.Appearance.Options.UseFont = True
        Me.INDsleEntityPopUp.Properties.Appearance.Options.UseForeColor = True
        Me.INDsleEntityPopUp.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleEntityPopUp.Properties.DisplayMember = "NameCode"
        Me.INDsleEntityPopUp.Properties.NullText = ""
        Me.INDsleEntityPopUp.Properties.PopupSizeable = False
        Me.INDsleEntityPopUp.Properties.PopupView = Me.GridView3
        Me.INDsleEntityPopUp.Properties.ShowFooter = False
        Me.INDsleEntityPopUp.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleEntityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleEntityPopUp, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleEntityPopUp, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleEntityPopUp, True)
        Me.INDsleEntityPopUp.Size = New System.Drawing.Size(275, 28)
        Me.INDsleEntityPopUp.StyleController = Me.LayoutControl2
        Me.INDsleEntityPopUp.TabIndex = 16
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleEntityPopUp, "200")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleEntityPopUp, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleEntityPopUp, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleEntityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleEntityPopUp, False)
        '
        'LayoutControlGroup2
        '
        Me.LayoutControlGroup2.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup2.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup2.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup2.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup2.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup2, False)
        Me.LayoutControlGroup2.CustomizationFormText = "LayoutControlGroup2"
        Me.LayoutControlGroup2.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup2.GroupBordersVisible = False
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlciEntityPopUp, Me.INDlciValidityPopUp})
        Me.LayoutControlGroup2.Name = "LayoutControlGroup2"
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(299, 150)
        Me.LayoutControlGroup2.TextVisible = False
        '
        'INDlciEntityPopUp
        '
        Me.INDlciEntityPopUp.Control = Me.INDsleEntityPopUp
        Me.INDlciEntityPopUp.CustomizationFormText = "LayoutControlItem3"
        Me.INDlciEntityPopUp.Location = New System.Drawing.Point(0, 0)
        Me.INDlciEntityPopUp.MaxSize = New System.Drawing.Size(279, 64)
        Me.INDlciEntityPopUp.MinSize = New System.Drawing.Size(279, 64)
        Me.INDlciEntityPopUp.Name = "INDlciEntityPopUp"
        Me.INDlciEntityPopUp.Size = New System.Drawing.Size(279, 64)
        Me.INDlciEntityPopUp.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciEntityPopUp.Text = "Entidad Presupuestal"
        Me.INDlciEntityPopUp.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciEntityPopUp.TextSize = New System.Drawing.Size(134, 17)
        '
        'INDlciValidityPopUp
        '
        Me.INDlciValidityPopUp.Control = Me.INDsleValidityPopUp
        Me.INDlciValidityPopUp.CustomizationFormText = "LayoutControlItem4"
        Me.INDlciValidityPopUp.Location = New System.Drawing.Point(0, 64)
        Me.INDlciValidityPopUp.MaxSize = New System.Drawing.Size(279, 64)
        Me.INDlciValidityPopUp.MinSize = New System.Drawing.Size(279, 64)
        Me.INDlciValidityPopUp.Name = "INDlciValidityPopUp"
        Me.INDlciValidityPopUp.Size = New System.Drawing.Size(279, 66)
        Me.INDlciValidityPopUp.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciValidityPopUp.Text = "Vigencia"
        Me.INDlciValidityPopUp.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciValidityPopUp.TextSize = New System.Drawing.Size(134, 17)
        '
        'INDpccChangeEntity
        '
        Me.INDpccChangeEntity.Controls.Add(Me.LayoutControl2)
        Me.INDpccChangeEntity.Location = New System.Drawing.Point(277, 350)
        Me.INDpccChangeEntity.Name = "INDpccChangeEntity"
        Me.INDpccChangeEntity.Size = New System.Drawing.Size(299, 150)
        Me.INDpccChangeEntity.TabIndex = 14
        '
        'INDCPC
        '
        Me.INDCPC.Caption = "CPC"
        Me.INDCPC.ColumnEdit = Me.RepositoryItemSleCPC
        Me.INDCPC.FieldName = "CPCCodeId"
        Me.INDCPC.Name = "INDCPC"
        Me.INDCPC.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowOnlyInEditor
        Me.INDCPC.Visible = True
        Me.INDCPC.VisibleIndex = 4
        '
        'RepositoryItemSleCPC
        '
        Me.RepositoryItemSleCPC.AutoHeight = False
        Me.RepositoryItemSleCPC.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup
        Me.RepositoryItemSleCPC.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemSleCPC.DisplayMember = "NameCode"
        Me.RepositoryItemSleCPC.Name = "RepositoryItemSleCPC"
        Me.RepositoryItemSleCPC.NullText = ""
        Me.RepositoryItemSleCPC.PopupView = Me.RepositoryItemGvCPC
        Me.RepositoryItemSleCPC.ValueMember = "Id"
        '
        'RepositoryItemGvCPC
        '
        Me.RepositoryItemGvCPC.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2})
        Me.RepositoryItemGvCPC.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.RepositoryItemGvCPC.Name = "RepositoryItemGvCPC"
        Me.RepositoryItemGvCPC.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.RepositoryItemGvCPC.OptionsView.EnableAppearanceEvenRow = True
        Me.RepositoryItemGvCPC.OptionsView.EnableAppearanceOddRow = True
        Me.RepositoryItemGvCPC.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.RepositoryItemGvCPC, False)
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Código"
        Me.GridColumn1.FieldName = "Code"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        Me.GridColumn1.Width = 104
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Nombre"
        Me.GridColumn2.FieldName = "Name"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        Me.GridColumn2.Width = 397
        '
        'FrmAvailability
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1465, 701)
        Me.Controls.Add(Me.INDpccChangeEntity)
        Me.IconOptions.Icon = CType(resources.GetObject("FrmAvailability.IconOptions.Icon"), System.Drawing.Icon)
        Me.IconOptions.ShowIcon = False
        Me.Name = "FrmAvailability"
        Me.Opacity = 1.0R
        Me.Tag = "228"
        Me.Text = "Disponibilidad"
        Me.Controls.SetChildIndex(Me.ToolBars, 0)
        Me.Controls.SetChildIndex(Me.INDPanelControlBase, 0)
        Me.Controls.SetChildIndex(Me.INDpccChangeEntity, 0)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrptValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDCnc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcAvailability, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLcAvailability.ResumeLayout(False)
        CType(Me.INDTxeTermDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGleAvailabilityType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGcCategory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.viewBudget, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDMemObservation.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxeValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleDependency, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEditExView5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSeTermDays.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDteDocumentDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDteDocumentDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDBtnCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleValidity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgvValidity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleBudgetEntity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgAvailability, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgPrincipalData, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemEntity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciValidity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgGeneralInformation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciDocumentDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciTermDays, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciDependency, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciObservations, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciAvailabilityType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciTermDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgCategory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciAdd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvValidityPopUp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleValidityPopUp.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl2.ResumeLayout(False)
        CType(Me.INDsleEntityPopUp.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciEntityPopUp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciValidityPopUp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDpccChangeEntity, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDpccChangeEntity.ResumeLayout(False)
        CType(Me.RepositoryItemSleCPC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemGvCPC, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDCnc As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents INDLcAvailability As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDLcgAvailability As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDsleBudgetEntity As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlyItemEntity As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoDate1 As Presentation.Controls.IndigoDate
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents IndigoGridLookUpControl1 As Presentation.Controls.IndigoGridLookUpControl
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
    Friend WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents INDsleValidity As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDgvValidity As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlciValidity As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLcgPrincipalData As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDpccChangeEntity As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents LayoutControl2 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDsleValidityPopUp As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDGvValidityPopUp As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgcStatusValidityPopUp As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDsleEntityPopUp As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents GridView3 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlciEntityPopUp As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlciValidityPopUp As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDBtnCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents INDLcgGeneralInformation As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLciCode As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDYear As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDStatus As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDResolutionName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDResolutionValue As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDDteDocumentDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDLciDocumentDate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDSeTermDays As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents INDLciTermDays As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDSleDependency As Presentation.Controls.SearchLookUpEditEx
    Friend WithEvents SearchLookUpEditExView5 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLciDependency As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDCodeDependency As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDNameDependency As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDTxeValue As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDLciValue As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDMemObservation As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents INDLciObservations As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDSbAdd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDLcgCategory As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLciAdd As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDGcCategory As DevExpress.XtraGrid.GridControl
    Friend WithEvents viewBudget As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDCodeCategory As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDNameCategory As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDResource As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDBalance As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDValue As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGleAvailabilityType As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GridLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLciAvailabilityType As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDAvailabilityType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDTxeTermDate As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDLciTermDate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDrptValue As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents INDCPC As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemSleCPC As DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit
    Friend WithEvents RepositoryItemGvCPC As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
End Class
