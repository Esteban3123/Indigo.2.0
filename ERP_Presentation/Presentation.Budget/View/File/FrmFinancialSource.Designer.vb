Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmFinancialSource
    Inherits FormBase

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim AppearanceObject1 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject2 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmFinancialSource))
        Dim AppearanceObject3 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject4 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim EditorButtonImageOptions1 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim AppearanceObject5 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject6 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject7 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject8 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Me.CtrNavigationControl1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDlyFinancialSource = New DevExpress.XtraLayout.LayoutControl()
        Me.INDsleValidity = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDgvValidity = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDYear = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDStatus = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDResolutionNumber = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDResolutionValue = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDsleEntity = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDtxtName = New DevExpress.XtraEditors.TextEdit()
        Me.INDbtnCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.INDsleClasification = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GridLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDLycFundingSource = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyGrGeneralInformation = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemEntity = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciValidity = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLygFundingSource = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemName = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemClasification = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoGridLookUpControl1 = New Presentation.Controls.IndigoGridLookUpControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlciEntityPopUp = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDsleEntityPopUp = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControl2 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDsleValidityPopUp = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDGvValidityPopUp = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgcStatusValidityPopUp = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDlciValidityPopUp = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoDate1 = New Presentation.Controls.IndigoDate(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.INDpccChangeEntity = New DevExpress.XtraEditors.PopupContainerControl()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyFinancialSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyFinancialSource.SuspendLayout()
        CType(Me.INDsleValidity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgvValidity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleEntity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDbtnCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleClasification.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLycFundingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyGrGeneralInformation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemEntity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciValidity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLygFundingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemClasification, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciEntityPopUp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleEntityPopUp.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl2.SuspendLayout()
        CType(Me.INDsleValidityPopUp.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvValidityPopUp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciValidityPopUp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDpccChangeEntity, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDpccChangeEntity.SuspendLayout()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlyFinancialSource)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControl1)
        resources.ApplyResources(Me.INDPanelControlBase, "INDPanelControlBase")
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        resources.ApplyResources(Me.ToolBars, "ToolBars")
        '
        'BarraBotones
        '
        Me.BarraBotones.OperatingUnitVisible = True
        resources.ApplyResources(Me.BarraBotones, "BarraBotones")
        '
        'CtrNavigationControl1
        '
        Me.CtrNavigationControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        resources.ApplyResources(Me.CtrNavigationControl1, "CtrNavigationControl1")
        Me.CtrNavigationControl1.LayoutControl = Me.INDlyFinancialSource
        Me.CtrNavigationControl1.Name = "CtrNavigationControl1"
        Me.CtrNavigationControl1.UseDisabledStatePainter = False
        '
        'INDlyFinancialSource
        '
        Me.INDlyFinancialSource.AllowCustomization = False
        Me.INDlyFinancialSource.Controls.Add(Me.INDsleValidity)
        Me.INDlyFinancialSource.Controls.Add(Me.INDsleEntity)
        Me.INDlyFinancialSource.Controls.Add(Me.INDtxtName)
        Me.INDlyFinancialSource.Controls.Add(Me.INDbtnCode)
        Me.INDlyFinancialSource.Controls.Add(Me.INDsleClasification)
        resources.ApplyResources(Me.INDlyFinancialSource, "INDlyFinancialSource")
        Me.LayoutControls.SetIsCustomizable(Me.INDlyFinancialSource, False)
        Me.INDlyFinancialSource.Name = "INDlyFinancialSource"
        Me.INDlyFinancialSource.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(2556, 310, 574, 569)
        Me.INDlyFinancialSource.Root = Me.INDLycFundingSource
        '
        'INDsleValidity
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleValidity, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleValidity, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleValidity, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleValidity, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleValidity, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleValidity, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleValidity, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleValidity, False)
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleValidity, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleValidity, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleValidity, False)
        resources.ApplyResources(Me.INDsleValidity, "INDsleValidity")
        Me.IndigoTextEdit1.SetMascara(Me.INDsleValidity, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleValidity.Name = "INDsleValidity"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleValidity, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleValidity, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleValidity, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleValidity, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleValidity, False)
        Me.INDsleValidity.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDsleValidity.Properties.Appearance.Font = CType(resources.GetObject("INDsleValidity.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDsleValidity.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleValidity.Properties.Appearance.Options.UseFont = True
        Me.INDsleValidity.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDsleValidity.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.INDsleValidity.Properties.DisplayMember = "Year"
        Me.INDsleValidity.Properties.NullText = resources.GetString("INDsleValidity.Properties.NullText")
        Me.INDsleValidity.Properties.PopupSizeable = False
        Me.INDsleValidity.Properties.PopupView = Me.INDgvValidity
        Me.INDsleValidity.Properties.ShowFooter = False
        Me.INDsleValidity.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleValidity, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleValidity, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleValidity, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleValidity, True)
        Me.INDsleValidity.StyleController = Me.INDlyFinancialSource
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleValidity, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleValidity, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleValidity, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleValidity, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleValidity, False)
        '
        'INDgvValidity
        '
        Me.INDgvValidity.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDgvValidity.Appearance.FocusedRow.Font = CType(resources.GetObject("INDgvValidity.Appearance.FocusedRow.Font"), System.Drawing.Font)
        Me.INDgvValidity.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDgvValidity.Appearance.FocusedRow.Options.UseFont = True
        Me.INDgvValidity.Appearance.GroupRow.Font = CType(resources.GetObject("INDgvValidity.Appearance.GroupRow.Font"), System.Drawing.Font)
        Me.INDgvValidity.Appearance.GroupRow.Options.UseFont = True
        Me.INDgvValidity.Appearance.HeaderPanel.Font = CType(resources.GetObject("INDgvValidity.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.INDgvValidity.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDgvValidity.Appearance.Row.Font = CType(resources.GetObject("INDgvValidity.Appearance.Row.Font"), System.Drawing.Font)
        Me.INDgvValidity.Appearance.Row.Options.UseFont = True
        Me.INDgvValidity.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDYear, Me.INDStatus, Me.INDResolutionNumber, Me.INDResolutionValue})
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
        resources.ApplyResources(Me.INDYear, "INDYear")
        Me.INDYear.FieldName = "Year"
        Me.INDYear.Name = "INDYear"
        '
        'INDStatus
        '
        resources.ApplyResources(Me.INDStatus, "INDStatus")
        Me.INDStatus.FieldName = "StatusText"
        Me.INDStatus.Name = "INDStatus"
        '
        'INDResolutionNumber
        '
        resources.ApplyResources(Me.INDResolutionNumber, "INDResolutionNumber")
        Me.INDResolutionNumber.FieldName = "ResolutionNumber"
        Me.INDResolutionNumber.Name = "INDResolutionNumber"
        '
        'INDResolutionValue
        '
        resources.ApplyResources(Me.INDResolutionValue, "INDResolutionValue")
        Me.INDResolutionValue.DisplayFormat.FormatString = "c0"
        Me.INDResolutionValue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDResolutionValue.FieldName = "ResolutionValue"
        Me.INDResolutionValue.Name = "INDResolutionValue"
        '
        'INDsleEntity
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleEntity, AppearanceObject3)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleEntity, AppearanceObject4)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleEntity, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleEntity, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleEntity, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleEntity, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleEntity, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleEntity, False)
        Me.INDsleEntity.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleEntity, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleEntity, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleEntity, False)
        resources.ApplyResources(Me.INDsleEntity, "INDsleEntity")
        Me.IndigoTextEdit1.SetMascara(Me.INDsleEntity, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleEntity.Name = "INDsleEntity"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleEntity, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleEntity, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleEntity, True)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleEntity, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleEntity, False)
        Me.INDsleEntity.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDsleEntity.Properties.Appearance.Font = CType(resources.GetObject("INDsleEntity.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDsleEntity.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleEntity.Properties.Appearance.Options.UseFont = True
        Me.INDsleEntity.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDsleEntity.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.INDsleEntity.Properties.DisplayMember = "NameCode"
        Me.INDsleEntity.Properties.NullText = resources.GetString("INDsleEntity.Properties.NullText")
        Me.INDsleEntity.Properties.PopupSizeable = False
        Me.INDsleEntity.Properties.PopupView = Me.SearchLookUpEdit1View
        Me.INDsleEntity.Properties.ShowFooter = False
        Me.INDsleEntity.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleEntity, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleEntity, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleEntity, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleEntity, True)
        Me.INDsleEntity.StyleController = Me.INDlyFinancialSource
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleEntity, "200")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleEntity, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleEntity, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleEntity, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleEntity, False)
        '
        'SearchLookUpEdit1View
        '
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.Font = CType(resources.GetObject("SearchLookUpEdit1View.Appearance.FocusedRow.Font"), System.Drawing.Font)
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.Options.UseFont = True
        Me.SearchLookUpEdit1View.Appearance.GroupRow.Font = CType(resources.GetObject("SearchLookUpEdit1View.Appearance.GroupRow.Font"), System.Drawing.Font)
        Me.SearchLookUpEdit1View.Appearance.GroupRow.Options.UseFont = True
        Me.SearchLookUpEdit1View.Appearance.HeaderPanel.Font = CType(resources.GetObject("SearchLookUpEdit1View.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.SearchLookUpEdit1View.Appearance.HeaderPanel.Options.UseFont = True
        Me.SearchLookUpEdit1View.Appearance.Row.Font = CType(resources.GetObject("SearchLookUpEdit1View.Appearance.Row.Font"), System.Drawing.Font)
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
        resources.ApplyResources(Me.INDCode, "INDCode")
        Me.INDCode.FieldName = "Code"
        Me.INDCode.Name = "INDCode"
        '
        'INDName
        '
        resources.ApplyResources(Me.INDName, "INDName")
        Me.INDName.FieldName = "Name"
        Me.INDName.Name = "INDName"
        '
        'INDtxtName
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtName, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtName, True)
        Me.INDtxtName.EnterMoveNextControl = True
        resources.ApplyResources(Me.INDtxtName, "INDtxtName")
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtName, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtName.Name = "INDtxtName"
        Me.INDtxtName.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDtxtName.Properties.Appearance.Font = CType(resources.GetObject("INDtxtName.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDtxtName.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtName.Properties.Appearance.Options.UseFont = True
        Me.INDtxtName.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDtxtName.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDtxtName.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDtxtName.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDtxtName.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtName.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtxtName.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtName.Properties.MaxLength = 100
        Me.INDtxtName.StyleController = Me.INDlyFinancialSource
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtName, 0)
        '
        'INDbtnCode
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDbtnCode, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDbtnCode, True)
        resources.ApplyResources(Me.INDbtnCode, "INDbtnCode")
        Me.IndigoTextEdit1.SetMascara(Me.INDbtnCode, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDbtnCode.Name = "INDbtnCode"
        Me.INDbtnCode.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDbtnCode.Properties.Appearance.Font = CType(resources.GetObject("INDbtnCode.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDbtnCode.Properties.Appearance.Options.UseBackColor = True
        Me.INDbtnCode.Properties.Appearance.Options.UseFont = True
        Me.INDbtnCode.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDbtnCode.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDbtnCode.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDbtnCode.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDbtnCode.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDbtnCode.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDbtnCode.Properties.AppearanceFocused.Options.UseFont = True
        EditorButtonImageOptions1.Image = Global.Presentation.Budget.My.Resources.Resources.BuscarMetro
        EditorButtonImageOptions1.Location = DevExpress.XtraEditors.ImageLocation.[Default]
        Me.INDbtnCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDbtnCode.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines), resources.GetString("INDbtnCode.Properties.Buttons1"), CType(resources.GetObject("INDbtnCode.Properties.Buttons2"), Integer), CType(resources.GetObject("INDbtnCode.Properties.Buttons3"), Boolean), CType(resources.GetObject("INDbtnCode.Properties.Buttons4"), Boolean), CType(resources.GetObject("INDbtnCode.Properties.Buttons5"), Boolean), EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, resources.GetString("INDbtnCode.Properties.Buttons6"), CType(resources.GetObject("INDbtnCode.Properties.Buttons7"), Object), CType(resources.GetObject("INDbtnCode.Properties.Buttons8"), DevExpress.Utils.SuperToolTip), CType(resources.GetObject("INDbtnCode.Properties.Buttons9"), DevExpress.Utils.ToolTipAnchor))})
        Me.INDbtnCode.Properties.MaxLength = 20
        Me.INDbtnCode.StyleController = Me.INDlyFinancialSource
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDbtnCode, 0)
        '
        'INDsleClasification
        '
        Me.IndigoGridLookUpControl1.SetAbrirFormularioArchivo(Me.INDsleClasification, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleClasification, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleClasification, True)
        Me.IndigoGridLookUpControl1.SetGuardarXmlGrid(Me.INDsleClasification, False)
        resources.ApplyResources(Me.INDsleClasification, "INDsleClasification")
        Me.IndigoTextEdit1.SetMascara(Me.INDsleClasification, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleClasification.Name = "INDsleClasification"
        Me.INDsleClasification.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDsleClasification.Properties.Appearance.Font = CType(resources.GetObject("INDsleClasification.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDsleClasification.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleClasification.Properties.Appearance.Options.UseFont = True
        Me.INDsleClasification.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDsleClasification.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDsleClasification.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDsleClasification.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDsleClasification.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDsleClasification.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDsleClasification.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDsleClasification.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDsleClasification.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.INDsleClasification.Properties.DisplayMember = "Item2"
        Me.INDsleClasification.Properties.ImmediatePopup = True
        Me.INDsleClasification.Properties.NullText = resources.GetString("INDsleClasification.Properties.NullText")
        Me.INDsleClasification.Properties.PopupView = Me.GridLookUpEdit1View
        Me.INDsleClasification.Properties.ValueMember = "Item1"
        Me.INDsleClasification.StyleController = Me.INDlyFinancialSource
        Me.IndigoGridLookUpControl1.SetTagFormularioAbrir(Me.INDsleClasification, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleClasification, 0)
        '
        'GridLookUpEdit1View
        '
        Me.GridLookUpEdit1View.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridLookUpEdit1View.Appearance.FocusedRow.Font = CType(resources.GetObject("GridLookUpEdit1View.Appearance.FocusedRow.Font"), System.Drawing.Font)
        Me.GridLookUpEdit1View.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridLookUpEdit1View.Appearance.FocusedRow.Options.UseFont = True
        Me.GridLookUpEdit1View.Appearance.GroupRow.Font = CType(resources.GetObject("GridLookUpEdit1View.Appearance.GroupRow.Font"), System.Drawing.Font)
        Me.GridLookUpEdit1View.Appearance.GroupRow.Options.UseFont = True
        Me.GridLookUpEdit1View.Appearance.HeaderPanel.Font = CType(resources.GetObject("GridLookUpEdit1View.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.GridLookUpEdit1View.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridLookUpEdit1View.Appearance.Row.Font = CType(resources.GetObject("GridLookUpEdit1View.Appearance.Row.Font"), System.Drawing.Font)
        Me.GridLookUpEdit1View.Appearance.Row.Options.UseFont = True
        Me.GridLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1})
        Me.GridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridLookUpEdit1View.Name = "GridLookUpEdit1View"
        Me.GridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridLookUpEdit1View.OptionsView.EnableAppearanceEvenRow = True
        Me.GridLookUpEdit1View.OptionsView.EnableAppearanceOddRow = True
        Me.GridLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.GridLookUpEdit1View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridLookUpEdit1View, False)
        '
        'GridColumn1
        '
        resources.ApplyResources(Me.GridColumn1, "GridColumn1")
        Me.GridColumn1.FieldName = "Item2"
        Me.GridColumn1.Name = "GridColumn1"
        '
        'INDLycFundingSource
        '
        Me.INDLycFundingSource.AppearanceGroup.Font = CType(resources.GetObject("INDLycFundingSource.AppearanceGroup.Font"), System.Drawing.Font)
        Me.INDLycFundingSource.AppearanceGroup.Options.UseFont = True
        Me.INDLycFundingSource.AppearanceItemCaption.Font = CType(resources.GetObject("INDLycFundingSource.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.INDLycFundingSource.AppearanceItemCaption.Options.UseFont = True
        Me.INDLycFundingSource.AppearanceTabPage.Header.Font = CType(resources.GetObject("INDLycFundingSource.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.INDLycFundingSource.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLycFundingSource.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("INDLycFundingSource.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        Me.INDLycFundingSource.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLycFundingSource.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("INDLycFundingSource.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.INDLycFundingSource.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLycFundingSource.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("INDLycFundingSource.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        Me.INDLycFundingSource.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLycFundingSource.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("INDLycFundingSource.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.INDLycFundingSource.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLycFundingSource, False)
        resources.ApplyResources(Me.INDLycFundingSource, "INDLycFundingSource")
        Me.INDLycFundingSource.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDLycFundingSource.GroupBordersVisible = False
        Me.INDLycFundingSource.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyGrGeneralInformation, Me.INDLygFundingSource})
        Me.INDLycFundingSource.Name = "Root"
        Me.INDLycFundingSource.Size = New System.Drawing.Size(878, 581)
        Me.INDLycFundingSource.TextVisible = False
        '
        'INDlyGrGeneralInformation
        '
        Me.INDlyGrGeneralInformation.AppearanceGroup.Font = CType(resources.GetObject("INDlyGrGeneralInformation.AppearanceGroup.Font"), System.Drawing.Font)
        Me.INDlyGrGeneralInformation.AppearanceGroup.Options.UseFont = True
        Me.INDlyGrGeneralInformation.AppearanceItemCaption.Font = CType(resources.GetObject("INDlyGrGeneralInformation.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.INDlyGrGeneralInformation.AppearanceItemCaption.Options.UseFont = True
        Me.INDlyGrGeneralInformation.AppearanceTabPage.Header.Font = CType(resources.GetObject("INDlyGrGeneralInformation.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.INDlyGrGeneralInformation.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlyGrGeneralInformation.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("INDlyGrGeneralInformation.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        Me.INDlyGrGeneralInformation.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlyGrGeneralInformation.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("INDlyGrGeneralInformation.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.INDlyGrGeneralInformation.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlyGrGeneralInformation.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("INDlyGrGeneralInformation.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        Me.INDlyGrGeneralInformation.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlyGrGeneralInformation.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("INDlyGrGeneralInformation.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.INDlyGrGeneralInformation.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlyGrGeneralInformation, False)
        resources.ApplyResources(Me.INDlyGrGeneralInformation, "INDlyGrGeneralInformation")
        Me.INDlyGrGeneralInformation.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemEntity, Me.INDlciValidity})
        Me.INDlyGrGeneralInformation.Location = New System.Drawing.Point(0, 0)
        Me.INDlyGrGeneralInformation.Name = "INDlyGrGeneralInformation"
        Me.INDlyGrGeneralInformation.Size = New System.Drawing.Size(414, 561)
        '
        'INDlyItemEntity
        '
        Me.INDlyItemEntity.Control = Me.INDsleEntity
        resources.ApplyResources(Me.INDlyItemEntity, "INDlyItemEntity")
        Me.INDlyItemEntity.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemEntity.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlyItemEntity.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlyItemEntity.Name = "INDlyItemEntity"
        Me.INDlyItemEntity.Size = New System.Drawing.Size(390, 64)
        Me.INDlyItemEntity.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemEntity.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemEntity.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemEntity.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemEntity.TextToControlDistance = 5
        '
        'INDlciValidity
        '
        Me.INDlciValidity.Control = Me.INDsleValidity
        resources.ApplyResources(Me.INDlciValidity, "INDlciValidity")
        Me.INDlciValidity.Location = New System.Drawing.Point(0, 64)
        Me.INDlciValidity.Name = "INDlciValidity"
        Me.INDlciValidity.Size = New System.Drawing.Size(390, 438)
        Me.INDlciValidity.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciValidity.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciValidity.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlciValidity.TextToControlDistance = 5
        '
        'INDLygFundingSource
        '
        Me.INDLygFundingSource.AppearanceGroup.Font = CType(resources.GetObject("INDLygFundingSource.AppearanceGroup.Font"), System.Drawing.Font)
        Me.INDLygFundingSource.AppearanceGroup.Options.UseFont = True
        Me.INDLygFundingSource.AppearanceItemCaption.Font = CType(resources.GetObject("INDLygFundingSource.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.INDLygFundingSource.AppearanceItemCaption.Options.UseFont = True
        Me.INDLygFundingSource.AppearanceTabPage.Header.Font = CType(resources.GetObject("INDLygFundingSource.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.INDLygFundingSource.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLygFundingSource.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("INDLygFundingSource.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        Me.INDLygFundingSource.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLygFundingSource.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("INDLygFundingSource.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.INDLygFundingSource.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLygFundingSource.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("INDLygFundingSource.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        Me.INDLygFundingSource.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLygFundingSource.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("INDLygFundingSource.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.INDLygFundingSource.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLygFundingSource, False)
        resources.ApplyResources(Me.INDLygFundingSource, "INDLygFundingSource")
        Me.INDLygFundingSource.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemName, Me.INDlyItemClasification, Me.INDlyItemCode})
        Me.INDLygFundingSource.Location = New System.Drawing.Point(414, 0)
        Me.INDLygFundingSource.Name = "INDLygFundingSource"
        Me.INDLygFundingSource.Size = New System.Drawing.Size(444, 561)
        Me.INDLygFundingSource.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDlyItemName
        '
        Me.INDlyItemName.Control = Me.INDtxtName
        resources.ApplyResources(Me.INDlyItemName, "INDlyItemName")
        Me.INDlyItemName.Location = New System.Drawing.Point(0, 36)
        Me.INDlyItemName.MaxSize = New System.Drawing.Size(420, 36)
        Me.INDlyItemName.MinSize = New System.Drawing.Size(420, 36)
        Me.INDlyItemName.Name = "INDlyItemName"
        Me.INDlyItemName.ShowInCustomizationForm = False
        Me.INDlyItemName.Size = New System.Drawing.Size(420, 36)
        Me.INDlyItemName.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemName.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemName.TextSize = New System.Drawing.Size(160, 21)
        Me.INDlyItemName.TextToControlDistance = 12
        '
        'INDlyItemClasification
        '
        Me.INDlyItemClasification.AllowHide = False
        Me.INDlyItemClasification.Control = Me.INDsleClasification
        resources.ApplyResources(Me.INDlyItemClasification, "INDlyItemClasification")
        Me.INDlyItemClasification.Location = New System.Drawing.Point(0, 72)
        Me.INDlyItemClasification.MaxSize = New System.Drawing.Size(420, 36)
        Me.INDlyItemClasification.MinSize = New System.Drawing.Size(420, 36)
        Me.INDlyItemClasification.Name = "INDlyItemClasification"
        Me.INDlyItemClasification.ShowInCustomizationForm = False
        Me.INDlyItemClasification.Size = New System.Drawing.Size(420, 430)
        Me.INDlyItemClasification.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemClasification.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemClasification.TextSize = New System.Drawing.Size(160, 21)
        Me.INDlyItemClasification.TextToControlDistance = 12
        '
        'INDlyItemCode
        '
        Me.INDlyItemCode.AllowHide = False
        Me.INDlyItemCode.Control = Me.INDbtnCode
        resources.ApplyResources(Me.INDlyItemCode, "INDlyItemCode")
        Me.INDlyItemCode.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemCode.MaxSize = New System.Drawing.Size(420, 36)
        Me.INDlyItemCode.MinSize = New System.Drawing.Size(420, 36)
        Me.INDlyItemCode.Name = "INDlyItemCode"
        Me.INDlyItemCode.ShowInCustomizationForm = False
        Me.INDlyItemCode.Size = New System.Drawing.Size(420, 36)
        Me.INDlyItemCode.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemCode.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemCode.TextSize = New System.Drawing.Size(160, 21)
        Me.INDlyItemCode.TextToControlDistance = 12
        '
        'LayoutControlGroup2
        '
        Me.LayoutControlGroup2.AppearanceGroup.Font = CType(resources.GetObject("LayoutControlGroup2.AppearanceGroup.Font"), System.Drawing.Font)
        Me.LayoutControlGroup2.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceItemCaption.Font = CType(resources.GetObject("LayoutControlGroup2.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LayoutControlGroup2.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.Header.Font = CType(resources.GetObject("LayoutControlGroup2.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.LayoutControlGroup2.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("LayoutControlGroup2.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("LayoutControlGroup2.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("LayoutControlGroup2.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("LayoutControlGroup2.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.LayoutControlGroup2.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup2, False)
        resources.ApplyResources(Me.LayoutControlGroup2, "LayoutControlGroup2")
        Me.LayoutControlGroup2.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup2.GroupBordersVisible = False
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlciEntityPopUp, Me.INDlciValidityPopUp})
        Me.LayoutControlGroup2.Name = "Root"
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(299, 150)
        Me.LayoutControlGroup2.TextVisible = False
        '
        'INDlciEntityPopUp
        '
        Me.INDlciEntityPopUp.Control = Me.INDsleEntityPopUp
        resources.ApplyResources(Me.INDlciEntityPopUp, "INDlciEntityPopUp")
        Me.INDlciEntityPopUp.Location = New System.Drawing.Point(0, 0)
        Me.INDlciEntityPopUp.MaxSize = New System.Drawing.Size(279, 64)
        Me.INDlciEntityPopUp.MinSize = New System.Drawing.Size(279, 64)
        Me.INDlciEntityPopUp.Name = "INDlciEntityPopUp"
        Me.INDlciEntityPopUp.Size = New System.Drawing.Size(279, 64)
        Me.INDlciEntityPopUp.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciEntityPopUp.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciEntityPopUp.TextSize = New System.Drawing.Size(134, 17)
        '
        'INDsleEntityPopUp
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleEntityPopUp, AppearanceObject5)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleEntityPopUp, AppearanceObject6)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleEntityPopUp, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleEntityPopUp, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleEntityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleEntityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleEntityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleEntityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleEntityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleEntityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleEntityPopUp, False)
        resources.ApplyResources(Me.INDsleEntityPopUp, "INDsleEntityPopUp")
        Me.IndigoTextEdit1.SetMascara(Me.INDsleEntityPopUp, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleEntityPopUp.Name = "INDsleEntityPopUp"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleEntityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleEntityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleEntityPopUp, True)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleEntityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleEntityPopUp, False)
        Me.INDsleEntityPopUp.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDsleEntityPopUp.Properties.Appearance.Font = CType(resources.GetObject("INDsleEntityPopUp.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDsleEntityPopUp.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleEntityPopUp.Properties.Appearance.Options.UseFont = True
        Me.INDsleEntityPopUp.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDsleEntityPopUp.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.INDsleEntityPopUp.Properties.DisplayMember = "NameCode"
        Me.INDsleEntityPopUp.Properties.NullText = resources.GetString("INDsleEntityPopUp.Properties.NullText")
        Me.INDsleEntityPopUp.Properties.PopupSizeable = False
        Me.INDsleEntityPopUp.Properties.PopupView = Me.GridView2
        Me.INDsleEntityPopUp.Properties.ShowFooter = False
        Me.INDsleEntityPopUp.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleEntityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleEntityPopUp, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleEntityPopUp, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleEntityPopUp, True)
        Me.INDsleEntityPopUp.StyleController = Me.LayoutControl2
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleEntityPopUp, "200")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleEntityPopUp, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleEntityPopUp, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleEntityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleEntityPopUp, False)
        '
        'GridView2
        '
        Me.GridView2.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridView2.Appearance.FocusedRow.Font = CType(resources.GetObject("GridView2.Appearance.FocusedRow.Font"), System.Drawing.Font)
        Me.GridView2.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridView2.Appearance.FocusedRow.Options.UseFont = True
        Me.GridView2.Appearance.GroupRow.Font = CType(resources.GetObject("GridView2.Appearance.GroupRow.Font"), System.Drawing.Font)
        Me.GridView2.Appearance.GroupRow.Options.UseFont = True
        Me.GridView2.Appearance.HeaderPanel.Font = CType(resources.GetObject("GridView2.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.GridView2.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView2.Appearance.Row.Font = CType(resources.GetObject("GridView2.Appearance.Row.Font"), System.Drawing.Font)
        Me.GridView2.Appearance.Row.Options.UseFont = True
        Me.GridView2.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn3, Me.GridColumn4})
        Me.GridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView2.Name = "GridView2"
        Me.GridView2.OptionsCustomization.AllowGroup = False
        Me.GridView2.OptionsDetail.EnableMasterViewMode = False
        Me.GridView2.OptionsDetail.ShowDetailTabs = False
        Me.GridView2.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView2.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView2.OptionsView.EnableAppearanceOddRow = True
        Me.GridView2.OptionsView.ShowAutoFilterRow = True
        Me.GridView2.OptionsView.ShowDetailButtons = False
        Me.GridView2.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridView2, False)
        '
        'GridColumn3
        '
        resources.ApplyResources(Me.GridColumn3, "GridColumn3")
        Me.GridColumn3.FieldName = "Code"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.OptionsColumn.AllowEdit = False
        Me.GridColumn3.OptionsColumn.AllowFocus = False
        '
        'GridColumn4
        '
        resources.ApplyResources(Me.GridColumn4, "GridColumn4")
        Me.GridColumn4.FieldName = "Name"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.OptionsColumn.AllowEdit = False
        Me.GridColumn4.OptionsColumn.AllowFocus = False
        '
        'LayoutControl2
        '
        Me.LayoutControl2.Controls.Add(Me.INDsleValidityPopUp)
        Me.LayoutControl2.Controls.Add(Me.INDsleEntityPopUp)
        resources.ApplyResources(Me.LayoutControl2, "LayoutControl2")
        Me.LayoutControl2.Name = "LayoutControl2"
        Me.LayoutControl2.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(2342, 195, 401, 350)
        Me.LayoutControl2.Root = Me.LayoutControlGroup2
        '
        'INDsleValidityPopUp
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleValidityPopUp, AppearanceObject7)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleValidityPopUp, AppearanceObject8)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleValidityPopUp, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleValidityPopUp, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleValidityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleValidityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleValidityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleValidityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleValidityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleValidityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleValidityPopUp, False)
        resources.ApplyResources(Me.INDsleValidityPopUp, "INDsleValidityPopUp")
        Me.IndigoTextEdit1.SetMascara(Me.INDsleValidityPopUp, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleValidityPopUp.Name = "INDsleValidityPopUp"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleValidityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleValidityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleValidityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleValidityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleValidityPopUp, False)
        Me.INDsleValidityPopUp.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDsleValidityPopUp.Properties.Appearance.Font = CType(resources.GetObject("INDsleValidityPopUp.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDsleValidityPopUp.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleValidityPopUp.Properties.Appearance.Options.UseFont = True
        Me.INDsleValidityPopUp.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDsleValidityPopUp.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.INDsleValidityPopUp.Properties.DisplayMember = "Year"
        Me.INDsleValidityPopUp.Properties.NullText = resources.GetString("INDsleValidityPopUp.Properties.NullText")
        Me.INDsleValidityPopUp.Properties.PopupSizeable = False
        Me.INDsleValidityPopUp.Properties.PopupView = Me.INDGvValidityPopUp
        Me.INDsleValidityPopUp.Properties.ShowFooter = False
        Me.INDsleValidityPopUp.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleValidityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleValidityPopUp, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleValidityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleValidityPopUp, True)
        Me.INDsleValidityPopUp.StyleController = Me.LayoutControl2
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleValidityPopUp, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleValidityPopUp, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleValidityPopUp, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleValidityPopUp, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleValidityPopUp, False)
        '
        'INDGvValidityPopUp
        '
        Me.INDGvValidityPopUp.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvValidityPopUp.Appearance.FocusedRow.Font = CType(resources.GetObject("INDGvValidityPopUp.Appearance.FocusedRow.Font"), System.Drawing.Font)
        Me.INDGvValidityPopUp.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvValidityPopUp.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvValidityPopUp.Appearance.GroupRow.Font = CType(resources.GetObject("INDGvValidityPopUp.Appearance.GroupRow.Font"), System.Drawing.Font)
        Me.INDGvValidityPopUp.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvValidityPopUp.Appearance.HeaderPanel.Font = CType(resources.GetObject("INDGvValidityPopUp.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.INDGvValidityPopUp.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvValidityPopUp.Appearance.Row.Font = CType(resources.GetObject("INDGvValidityPopUp.Appearance.Row.Font"), System.Drawing.Font)
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
        resources.ApplyResources(Me.GridColumn5, "GridColumn5")
        Me.GridColumn5.FieldName = "Year"
        Me.GridColumn5.Name = "GridColumn5"
        '
        'INDgcStatusValidityPopUp
        '
        resources.ApplyResources(Me.INDgcStatusValidityPopUp, "INDgcStatusValidityPopUp")
        Me.INDgcStatusValidityPopUp.FieldName = "StatusText"
        Me.INDgcStatusValidityPopUp.Name = "INDgcStatusValidityPopUp"
        '
        'GridColumn7
        '
        resources.ApplyResources(Me.GridColumn7, "GridColumn7")
        Me.GridColumn7.FieldName = "ResolutionNumber"
        Me.GridColumn7.Name = "GridColumn7"
        '
        'GridColumn8
        '
        resources.ApplyResources(Me.GridColumn8, "GridColumn8")
        Me.GridColumn8.DisplayFormat.FormatString = "c0"
        Me.GridColumn8.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn8.FieldName = "ResolutionValue"
        Me.GridColumn8.Name = "GridColumn8"
        '
        'INDlciValidityPopUp
        '
        Me.INDlciValidityPopUp.Control = Me.INDsleValidityPopUp
        resources.ApplyResources(Me.INDlciValidityPopUp, "INDlciValidityPopUp")
        Me.INDlciValidityPopUp.Location = New System.Drawing.Point(0, 64)
        Me.INDlciValidityPopUp.MaxSize = New System.Drawing.Size(279, 64)
        Me.INDlciValidityPopUp.MinSize = New System.Drawing.Size(279, 64)
        Me.INDlciValidityPopUp.Name = "INDlciValidityPopUp"
        Me.INDlciValidityPopUp.Size = New System.Drawing.Size(279, 66)
        Me.INDlciValidityPopUp.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciValidityPopUp.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciValidityPopUp.TextSize = New System.Drawing.Size(134, 17)
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        '
        'INDpccChangeEntity
        '
        Me.INDpccChangeEntity.Controls.Add(Me.LayoutControl2)
        resources.ApplyResources(Me.INDpccChangeEntity, "INDpccChangeEntity")
        Me.INDpccChangeEntity.Name = "INDpccChangeEntity"
        '
        'FrmFinancialSource
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.INDpccChangeEntity)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmFinancialSource"
        Me.Opacity = 1.0R
        Me.Tag = "201"
        Me.Controls.SetChildIndex(Me.ToolBars, 0)
        Me.Controls.SetChildIndex(Me.INDPanelControlBase, 0)
        Me.Controls.SetChildIndex(Me.INDpccChangeEntity, 0)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyFinancialSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyFinancialSource.ResumeLayout(False)
        CType(Me.INDsleValidity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgvValidity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleEntity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDbtnCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleClasification.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLycFundingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyGrGeneralInformation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemEntity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciValidity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLygFundingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemClasification, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciEntityPopUp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleEntityPopUp.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl2.ResumeLayout(False)
        CType(Me.INDsleValidityPopUp.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvValidityPopUp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciValidityPopUp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDpccChangeEntity, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDpccChangeEntity.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDlyFinancialSource As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDLycFundingSource As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents CtrNavigationControl1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents IndigoDate1 As Presentation.Controls.IndigoDate
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents IndigoGridLookUpControl1 As Presentation.Controls.IndigoGridLookUpControl
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
    Friend WithEvents INDbtnCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents INDlyItemCode As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDtxtName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDlyItemName As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemClasification As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDsleClasification As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GridLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents INDLygFundingSource As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDsleEntity As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlyGrGeneralInformation As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemEntity As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDsleValidity As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDgvValidity As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlciValidity As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDYear As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDStatus As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDResolutionNumber As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDResolutionValue As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDpccChangeEntity As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents LayoutControl2 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDsleValidityPopUp As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDGvValidityPopUp As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgcStatusValidityPopUp As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDsleEntityPopUp As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlciEntityPopUp As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlciValidityPopUp As DevExpress.XtraLayout.LayoutControlItem
End Class
