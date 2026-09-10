Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmCompany
    Inherits FormBase

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmCompany))
        Dim EditorButtonImageOptions1 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim EditorButtonImageOptions2 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject5 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject6 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject7 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject8 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim RepositoryItemPopupContainerEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.INDlyCompany = New DevExpress.XtraLayout.LayoutControl()
        Me.INDTxtJudgementCode = New DevExpress.XtraEditors.TextEdit()
        Me.INDSlThirdParty = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDchkAttachmentsType = New DevExpress.XtraEditors.CheckEdit()
        Me.INDpccContactData = New DevExpress.XtraEditors.PopupContainerControl()
        Me.CtrContacts = New Presentation.Controls.CtrContactos()
        Me.INDSlARLCompany = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDchkAgreementsType = New DevExpress.XtraEditors.CheckEdit()
        Me.INDchkPayrolType = New DevExpress.XtraEditors.CheckEdit()
        Me.INDpceControlData = New DevExpress.XtraEditors.PopupContainerEdit()
        Me.INDGleCity = New Presentation.Controls.GridLookUpMultiFilter()
        Me.GridLookUpMultiFilter3View = New Presentation.Controls.CustomGridView()
        Me.CityCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CityName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGleDepartment = New Presentation.Controls.GridLookUpMultiFilter()
        Me.GridLookUpMultiFilter2View = New Presentation.Controls.CustomGridView()
        Me.DepartmentCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.DepartmentName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDTxtLegalRepresentative = New DevExpress.XtraEditors.TextEdit()
        Me.INDTxtNameCompany = New DevExpress.XtraEditors.TextEdit()
        Me.INDBteNitCompany = New DevExpress.XtraEditors.ButtonEdit()
        Me.INDLcgCompany = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDGrCompany = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDItemNitCompany = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDItemNameCompany = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDItemLegalRepresentative = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDItemDepartment = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDItemCity = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemControlData = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciARLCompany = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciThirdParty = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciJudgmentAccount = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLcgCompanyType = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyitemAgreementsType = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlycitemPayrolType = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlycitemAttachmentType = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoGridLookUpControl1 = New Presentation.Controls.IndigoGridLookUpControl()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup()
        Me.IndigoCheckEdit1 = New Presentation.Controls.IndigoCheckEdit()
        Me.CtrNavigationControlPanel1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView()
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyCompany, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyCompany.SuspendLayout()
        CType(Me.INDTxtJudgementCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSlThirdParty.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDchkAttachmentsType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDpccContactData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDpccContactData.SuspendLayout()
        CType(Me.INDSlARLCompany.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDchkAgreementsType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDchkPayrolType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDpceControlData.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGleCity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridLookUpMultiFilter3View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGleDepartment.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridLookUpMultiFilter2View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtLegalRepresentative.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtNameCompany.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDBteNitCompany.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgCompany, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGrCompany, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDItemNitCompany, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDItemNameCompany, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDItemLegalRepresentative, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDItemDepartment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDItemCity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemControlData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciARLCompany, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciThirdParty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciJudgmentAccount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgCompanyType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyitemAgreementsType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlycitemPayrolType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlycitemAttachmentType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlyCompany)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControlPanel1)
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
        resources.ApplyResources(Me.BarraBotones, "BarraBotones")
        '
        'INDlyCompany
        '
        Me.INDlyCompany.AllowCustomization = False
        Me.INDlyCompany.Controls.Add(Me.INDTxtJudgementCode)
        Me.INDlyCompany.Controls.Add(Me.INDSlThirdParty)
        Me.INDlyCompany.Controls.Add(Me.INDchkAttachmentsType)
        Me.INDlyCompany.Controls.Add(Me.INDpccContactData)
        Me.INDlyCompany.Controls.Add(Me.INDSlARLCompany)
        Me.INDlyCompany.Controls.Add(Me.INDchkAgreementsType)
        Me.INDlyCompany.Controls.Add(Me.INDchkPayrolType)
        Me.INDlyCompany.Controls.Add(Me.INDpceControlData)
        Me.INDlyCompany.Controls.Add(Me.INDGleCity)
        Me.INDlyCompany.Controls.Add(Me.INDGleDepartment)
        Me.INDlyCompany.Controls.Add(Me.INDTxtLegalRepresentative)
        Me.INDlyCompany.Controls.Add(Me.INDTxtNameCompany)
        Me.INDlyCompany.Controls.Add(Me.INDBteNitCompany)
        resources.ApplyResources(Me.INDlyCompany, "INDlyCompany")
        Me.LayoutControls.SetIsCustomizable(Me.INDlyCompany, False)
        Me.INDlyCompany.Name = "INDlyCompany"
        Me.INDlyCompany.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(486, 254, 250, 350)
        Me.INDlyCompany.Root = Me.INDLcgCompany
        '
        'INDTxtJudgementCode
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtJudgementCode, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtJudgementCode, False)
        resources.ApplyResources(Me.INDTxtJudgementCode, "INDTxtJudgementCode")
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtJudgementCode, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxtJudgementCode.Name = "INDTxtJudgementCode"
        Me.INDTxtJudgementCode.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTxtJudgementCode.Properties.Appearance.Font = CType(resources.GetObject("INDTxtJudgementCode.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDTxtJudgementCode.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtJudgementCode.Properties.Appearance.Options.UseFont = True
        Me.INDTxtJudgementCode.Properties.MaxLength = 50
        Me.INDTxtJudgementCode.StyleController = Me.INDlyCompany
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtJudgementCode, 0)
        '
        'INDSlThirdParty
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSlThirdParty, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSlThirdParty, False)
        resources.ApplyResources(Me.INDSlThirdParty, "INDSlThirdParty")
        Me.IndigoTextEdit1.SetMascara(Me.INDSlThirdParty, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSlThirdParty.Name = "INDSlThirdParty"
        Me.INDSlThirdParty.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDSlThirdParty.Properties.Appearance.Font = CType(resources.GetObject("INDSlThirdParty.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDSlThirdParty.Properties.Appearance.Options.UseBackColor = True
        Me.INDSlThirdParty.Properties.Appearance.Options.UseFont = True
        Me.INDSlThirdParty.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDSlThirdParty.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines)), New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDSlThirdParty.Properties.Buttons1"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.INDSlThirdParty.Properties.DisplayMember = "NitName"
        Me.INDSlThirdParty.Properties.NullText = resources.GetString("INDSlThirdParty.Properties.NullText")
        Me.INDSlThirdParty.Properties.PopupView = Me.GridView1
        Me.INDSlThirdParty.Properties.ValueMember = "Id"
        Me.INDSlThirdParty.StyleController = Me.INDlyCompany
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSlThirdParty, 0)
        '
        'GridView1
        '
        Me.GridView1.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridView1.Appearance.FocusedRow.Font = CType(resources.GetObject("GridView1.Appearance.FocusedRow.Font"), System.Drawing.Font)
        Me.GridView1.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridView1.Appearance.FocusedRow.Options.UseFont = True
        Me.GridView1.Appearance.GroupRow.Font = CType(resources.GetObject("GridView1.Appearance.GroupRow.Font"), System.Drawing.Font)
        Me.GridView1.Appearance.GroupRow.Options.UseFont = True
        Me.GridView1.Appearance.HeaderPanel.Font = CType(resources.GetObject("GridView1.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.GridView1.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView1.Appearance.Row.Font = CType(resources.GetObject("GridView1.Appearance.Row.Font"), System.Drawing.Font)
        Me.GridView1.Appearance.Row.Options.UseFont = True
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn3, Me.GridColumn4})
        Me.GridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView1.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView1.OptionsView.EnableAppearanceOddRow = True
        Me.GridView1.OptionsView.ShowAutoFilterRow = True
        Me.GridView1.OptionsView.ShowGroupPanel = False
        Me.GridView1.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.GridColumn4, DevExpress.Data.ColumnSortOrder.Ascending)})
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridView1, False)
        '
        'GridColumn3
        '
        resources.ApplyResources(Me.GridColumn3, "GridColumn3")
        Me.GridColumn3.FieldName = "Nit"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.OptionsColumn.AllowEdit = False
        '
        'GridColumn4
        '
        resources.ApplyResources(Me.GridColumn4, "GridColumn4")
        Me.GridColumn4.FieldName = "Name"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.OptionsColumn.AllowEdit = False
        '
        'INDchkAttachmentsType
        '
        Me.IndigoCheckEdit1.SetCampoObligatorio(Me.INDchkAttachmentsType, False)
        resources.ApplyResources(Me.INDchkAttachmentsType, "INDchkAttachmentsType")
        Me.INDchkAttachmentsType.Name = "INDchkAttachmentsType"
        Me.INDchkAttachmentsType.Properties.Appearance.Font = CType(resources.GetObject("INDchkAttachmentsType.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDchkAttachmentsType.Properties.Appearance.Options.UseFont = True
        Me.INDchkAttachmentsType.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDchkAttachmentsType.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDchkAttachmentsType.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDchkAttachmentsType.Properties.Caption = resources.GetString("INDchkAttachmentsType.Properties.Caption")
        Me.INDchkAttachmentsType.StyleController = Me.INDlyCompany
        '
        'INDpccContactData
        '
        Me.INDpccContactData.Controls.Add(Me.CtrContacts)
        resources.ApplyResources(Me.INDpccContactData, "INDpccContactData")
        Me.INDpccContactData.Name = "INDpccContactData"
        '
        'CtrContacts
        '
        resources.ApplyResources(Me.CtrContacts, "CtrContacts")
        Me.CtrContacts.Name = "CtrContacts"
        Me.CtrContacts.OpenByFormUser = False
        '
        'INDSlARLCompany
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSlARLCompany, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSlARLCompany, False)
        resources.ApplyResources(Me.INDSlARLCompany, "INDSlARLCompany")
        Me.IndigoTextEdit1.SetMascara(Me.INDSlARLCompany, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSlARLCompany.Name = "INDSlARLCompany"
        Me.INDSlARLCompany.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDSlARLCompany.Properties.Appearance.Font = CType(resources.GetObject("INDSlARLCompany.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDSlARLCompany.Properties.Appearance.Options.UseBackColor = True
        Me.INDSlARLCompany.Properties.Appearance.Options.UseFont = True
        Me.INDSlARLCompany.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDSlARLCompany.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.INDSlARLCompany.Properties.DisplayMember = "Name"
        Me.INDSlARLCompany.Properties.NullText = resources.GetString("INDSlARLCompany.Properties.NullText")
        Me.INDSlARLCompany.Properties.PopupView = Me.SearchLookUpEdit1View
        Me.INDSlARLCompany.Properties.ValueMember = "Id"
        Me.INDSlARLCompany.StyleController = Me.INDlyCompany
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSlARLCompany, 0)
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
        Me.SearchLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2})
        Me.SearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit1View.Name = "SearchLookUpEdit1View"
        Me.SearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEdit1View, False)
        '
        'GridColumn1
        '
        resources.ApplyResources(Me.GridColumn1, "GridColumn1")
        Me.GridColumn1.FieldName = "Code"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowEdit = False
        '
        'GridColumn2
        '
        resources.ApplyResources(Me.GridColumn2, "GridColumn2")
        Me.GridColumn2.FieldName = "Name"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        '
        'INDchkAgreementsType
        '
        Me.IndigoCheckEdit1.SetCampoObligatorio(Me.INDchkAgreementsType, False)
        resources.ApplyResources(Me.INDchkAgreementsType, "INDchkAgreementsType")
        Me.INDchkAgreementsType.Name = "INDchkAgreementsType"
        Me.INDchkAgreementsType.Properties.Appearance.Font = CType(resources.GetObject("INDchkAgreementsType.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDchkAgreementsType.Properties.Appearance.Options.UseFont = True
        Me.INDchkAgreementsType.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDchkAgreementsType.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDchkAgreementsType.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDchkAgreementsType.Properties.Caption = resources.GetString("INDchkAgreementsType.Properties.Caption")
        Me.INDchkAgreementsType.StyleController = Me.INDlyCompany
        '
        'INDchkPayrolType
        '
        Me.IndigoCheckEdit1.SetCampoObligatorio(Me.INDchkPayrolType, False)
        resources.ApplyResources(Me.INDchkPayrolType, "INDchkPayrolType")
        Me.INDchkPayrolType.Name = "INDchkPayrolType"
        Me.INDchkPayrolType.Properties.Appearance.Font = CType(resources.GetObject("INDchkPayrolType.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDchkPayrolType.Properties.Appearance.Options.UseFont = True
        Me.INDchkPayrolType.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDchkPayrolType.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDchkPayrolType.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDchkPayrolType.Properties.Caption = resources.GetString("INDchkPayrolType.Properties.Caption")
        Me.INDchkPayrolType.StyleController = Me.INDlyCompany
        '
        'INDpceControlData
        '
        resources.ApplyResources(Me.INDpceControlData, "INDpceControlData")
        Me.INDpceControlData.Name = "INDpceControlData"
        Me.INDpceControlData.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDpceControlData.Properties.Appearance.Font = CType(resources.GetObject("INDpceControlData.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDpceControlData.Properties.Appearance.Options.UseBackColor = True
        Me.INDpceControlData.Properties.Appearance.Options.UseFont = True
        Me.INDpceControlData.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDpceControlData.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDpceControlData.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDpceControlData.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDpceControlData.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDpceControlData.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDpceControlData.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDpceControlData.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        SerializableAppearanceObject1.Options.UseFont = True
        SerializableAppearanceObject2.Options.UseFont = True
        SerializableAppearanceObject3.Options.UseFont = True
        SerializableAppearanceObject4.Options.UseFont = True
        Me.INDpceControlData.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDpceControlData.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines), resources.GetString("INDpceControlData.Properties.Buttons1"), CType(resources.GetObject("INDpceControlData.Properties.Buttons2"), Integer), CType(resources.GetObject("INDpceControlData.Properties.Buttons3"), Boolean), CType(resources.GetObject("INDpceControlData.Properties.Buttons4"), Boolean), CType(resources.GetObject("INDpceControlData.Properties.Buttons5"), Boolean), EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, resources.GetString("INDpceControlData.Properties.Buttons6"), CType(resources.GetObject("INDpceControlData.Properties.Buttons7"), Object), CType(resources.GetObject("INDpceControlData.Properties.Buttons8"), DevExpress.Utils.SuperToolTip), CType(resources.GetObject("INDpceControlData.Properties.Buttons9"), DevExpress.Utils.ToolTipAnchor))})
        Me.INDpceControlData.Properties.PopupControl = Me.INDpccContactData
        Me.INDpceControlData.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        Me.INDpceControlData.StyleController = Me.INDlyCompany
        '
        'INDGleCity
        '
        Me.IndigoGridLookUpControl1.SetAbrirFormularioArchivo(Me.INDGleCity, True)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDGleCity, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDGleCity, True)
        Me.IndigoGridLookUpControl1.SetGuardarXmlGrid(Me.INDGleCity, False)
        resources.ApplyResources(Me.INDGleCity, "INDGleCity")
        Me.IndigoTextEdit1.SetMascara(Me.INDGleCity, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDGleCity.Name = "INDGleCity"
        Me.INDGleCity.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDGleCity.Properties.Appearance.Font = CType(resources.GetObject("INDGleCity.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDGleCity.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDGleCity.Properties.Appearance.Options.UseBackColor = True
        Me.INDGleCity.Properties.Appearance.Options.UseFont = True
        Me.INDGleCity.Properties.Appearance.Options.UseForeColor = True
        Me.INDGleCity.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDGleCity.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDGleCity.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDGleCity.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDGleCity.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDGleCity.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDGleCity.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDGleCity.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDGleCity.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.INDGleCity.Properties.DisplayMember = "Name"
        Me.INDGleCity.Properties.ImmediatePopup = True
        Me.INDGleCity.Properties.NullText = resources.GetString("INDGleCity.Properties.NullText")
        Me.INDGleCity.Properties.PopupView = Me.GridLookUpMultiFilter3View
        Me.INDGleCity.Properties.SearchMode = DevExpress.XtraEditors.Repository.GridLookUpSearchMode.None
        Me.INDGleCity.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.INDGleCity.Properties.ValueMember = "Id"
        Me.INDGleCity.StyleController = Me.INDlyCompany
        Me.IndigoGridLookUpControl1.SetTagFormularioAbrir(Me.INDGleCity, "513")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDGleCity, 0)
        '
        'GridLookUpMultiFilter3View
        '
        Me.GridLookUpMultiFilter3View.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridLookUpMultiFilter3View.Appearance.FocusedRow.Font = CType(resources.GetObject("GridLookUpMultiFilter3View.Appearance.FocusedRow.Font"), System.Drawing.Font)
        Me.GridLookUpMultiFilter3View.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridLookUpMultiFilter3View.Appearance.FocusedRow.Options.UseFont = True
        Me.GridLookUpMultiFilter3View.Appearance.GroupRow.Font = CType(resources.GetObject("GridLookUpMultiFilter3View.Appearance.GroupRow.Font"), System.Drawing.Font)
        Me.GridLookUpMultiFilter3View.Appearance.GroupRow.Options.UseFont = True
        Me.GridLookUpMultiFilter3View.Appearance.HeaderPanel.Font = CType(resources.GetObject("GridLookUpMultiFilter3View.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.GridLookUpMultiFilter3View.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridLookUpMultiFilter3View.Appearance.Row.Font = CType(resources.GetObject("GridLookUpMultiFilter3View.Appearance.Row.Font"), System.Drawing.Font)
        Me.GridLookUpMultiFilter3View.Appearance.Row.Options.UseFont = True
        Me.GridLookUpMultiFilter3View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.CityCode, Me.CityName})
        Me.GridLookUpMultiFilter3View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridLookUpMultiFilter3View.Name = "GridLookUpMultiFilter3View"
        Me.GridLookUpMultiFilter3View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridLookUpMultiFilter3View.OptionsView.EnableAppearanceEvenRow = True
        Me.GridLookUpMultiFilter3View.OptionsView.EnableAppearanceOddRow = True
        Me.GridLookUpMultiFilter3View.OptionsView.ShowAutoFilterRow = True
        Me.GridLookUpMultiFilter3View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridLookUpMultiFilter3View, False)
        '
        'CityCode
        '
        resources.ApplyResources(Me.CityCode, "CityCode")
        Me.CityCode.FieldName = "Code"
        Me.CityCode.Name = "CityCode"
        '
        'CityName
        '
        resources.ApplyResources(Me.CityName, "CityName")
        Me.CityName.FieldName = "Name"
        Me.CityName.Name = "CityName"
        '
        'INDGleDepartment
        '
        Me.IndigoGridLookUpControl1.SetAbrirFormularioArchivo(Me.INDGleDepartment, True)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDGleDepartment, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDGleDepartment, True)
        Me.INDGleDepartment.EnterMoveNextControl = True
        Me.IndigoGridLookUpControl1.SetGuardarXmlGrid(Me.INDGleDepartment, False)
        resources.ApplyResources(Me.INDGleDepartment, "INDGleDepartment")
        Me.IndigoTextEdit1.SetMascara(Me.INDGleDepartment, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDGleDepartment.Name = "INDGleDepartment"
        Me.INDGleDepartment.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDGleDepartment.Properties.Appearance.Font = CType(resources.GetObject("INDGleDepartment.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDGleDepartment.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDGleDepartment.Properties.Appearance.Options.UseBackColor = True
        Me.INDGleDepartment.Properties.Appearance.Options.UseFont = True
        Me.INDGleDepartment.Properties.Appearance.Options.UseForeColor = True
        Me.INDGleDepartment.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDGleDepartment.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDGleDepartment.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDGleDepartment.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDGleDepartment.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDGleDepartment.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDGleDepartment.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDGleDepartment.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDGleDepartment.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.INDGleDepartment.Properties.DisplayMember = "Name"
        Me.INDGleDepartment.Properties.ImmediatePopup = True
        Me.INDGleDepartment.Properties.NullText = resources.GetString("INDGleDepartment.Properties.NullText")
        Me.INDGleDepartment.Properties.PopupView = Me.GridLookUpMultiFilter2View
        Me.INDGleDepartment.Properties.SearchMode = DevExpress.XtraEditors.Repository.GridLookUpSearchMode.None
        Me.INDGleDepartment.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.INDGleDepartment.Properties.ValueMember = "Id"
        Me.INDGleDepartment.StyleController = Me.INDlyCompany
        Me.IndigoGridLookUpControl1.SetTagFormularioAbrir(Me.INDGleDepartment, "510")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDGleDepartment, 0)
        '
        'GridLookUpMultiFilter2View
        '
        Me.GridLookUpMultiFilter2View.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridLookUpMultiFilter2View.Appearance.FocusedRow.Font = CType(resources.GetObject("GridLookUpMultiFilter2View.Appearance.FocusedRow.Font"), System.Drawing.Font)
        Me.GridLookUpMultiFilter2View.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridLookUpMultiFilter2View.Appearance.FocusedRow.Options.UseFont = True
        Me.GridLookUpMultiFilter2View.Appearance.GroupRow.Font = CType(resources.GetObject("GridLookUpMultiFilter2View.Appearance.GroupRow.Font"), System.Drawing.Font)
        Me.GridLookUpMultiFilter2View.Appearance.GroupRow.Options.UseFont = True
        Me.GridLookUpMultiFilter2View.Appearance.HeaderPanel.Font = CType(resources.GetObject("GridLookUpMultiFilter2View.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.GridLookUpMultiFilter2View.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridLookUpMultiFilter2View.Appearance.Row.Font = CType(resources.GetObject("GridLookUpMultiFilter2View.Appearance.Row.Font"), System.Drawing.Font)
        Me.GridLookUpMultiFilter2View.Appearance.Row.Options.UseFont = True
        Me.GridLookUpMultiFilter2View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.DepartmentCode, Me.DepartmentName})
        Me.GridLookUpMultiFilter2View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridLookUpMultiFilter2View.Name = "GridLookUpMultiFilter2View"
        Me.GridLookUpMultiFilter2View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridLookUpMultiFilter2View.OptionsView.EnableAppearanceEvenRow = True
        Me.GridLookUpMultiFilter2View.OptionsView.EnableAppearanceOddRow = True
        Me.GridLookUpMultiFilter2View.OptionsView.ShowAutoFilterRow = True
        Me.GridLookUpMultiFilter2View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridLookUpMultiFilter2View, False)
        '
        'DepartmentCode
        '
        resources.ApplyResources(Me.DepartmentCode, "DepartmentCode")
        Me.DepartmentCode.FieldName = "Code"
        Me.DepartmentCode.Name = "DepartmentCode"
        '
        'DepartmentName
        '
        resources.ApplyResources(Me.DepartmentName, "DepartmentName")
        Me.DepartmentName.FieldName = "Name"
        Me.DepartmentName.Name = "DepartmentName"
        '
        'INDTxtLegalRepresentative
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtLegalRepresentative, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtLegalRepresentative, True)
        Me.INDTxtLegalRepresentative.EnterMoveNextControl = True
        resources.ApplyResources(Me.INDTxtLegalRepresentative, "INDTxtLegalRepresentative")
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtLegalRepresentative, Presentation.Controls.IndigoTextEdit.EMask.SoloLetraMayusculaMinuscula)
        Me.INDTxtLegalRepresentative.Name = "INDTxtLegalRepresentative"
        Me.INDTxtLegalRepresentative.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDTxtLegalRepresentative.Properties.Appearance.Font = CType(resources.GetObject("INDTxtLegalRepresentative.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDTxtLegalRepresentative.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDTxtLegalRepresentative.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtLegalRepresentative.Properties.Appearance.Options.UseFont = True
        Me.INDTxtLegalRepresentative.Properties.Appearance.Options.UseForeColor = True
        Me.INDTxtLegalRepresentative.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtLegalRepresentative.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtLegalRepresentative.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDTxtLegalRepresentative.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDTxtLegalRepresentative.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtLegalRepresentative.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtLegalRepresentative.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtLegalRepresentative.Properties.Mask.EditMask = resources.GetString("INDTxtLegalRepresentative.Properties.Mask.EditMask")
        Me.INDTxtLegalRepresentative.Properties.Mask.MaskType = CType(resources.GetObject("INDTxtLegalRepresentative.Properties.Mask.MaskType"), DevExpress.XtraEditors.Mask.MaskType)
        Me.INDTxtLegalRepresentative.Properties.MaxLength = 50
        Me.INDTxtLegalRepresentative.StyleController = Me.INDlyCompany
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtLegalRepresentative, 0)
        '
        'INDTxtNameCompany
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtNameCompany, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtNameCompany, True)
        Me.INDTxtNameCompany.EnterMoveNextControl = True
        resources.ApplyResources(Me.INDTxtNameCompany, "INDTxtNameCompany")
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtNameCompany, Presentation.Controls.IndigoTextEdit.EMask.AlfaNumerico)
        Me.INDTxtNameCompany.Name = "INDTxtNameCompany"
        Me.INDTxtNameCompany.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDTxtNameCompany.Properties.Appearance.Font = CType(resources.GetObject("INDTxtNameCompany.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDTxtNameCompany.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDTxtNameCompany.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtNameCompany.Properties.Appearance.Options.UseFont = True
        Me.INDTxtNameCompany.Properties.Appearance.Options.UseForeColor = True
        Me.INDTxtNameCompany.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtNameCompany.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtNameCompany.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDTxtNameCompany.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDTxtNameCompany.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtNameCompany.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtNameCompany.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtNameCompany.Properties.Mask.EditMask = resources.GetString("INDTxtNameCompany.Properties.Mask.EditMask")
        Me.INDTxtNameCompany.Properties.Mask.MaskType = CType(resources.GetObject("INDTxtNameCompany.Properties.Mask.MaskType"), DevExpress.XtraEditors.Mask.MaskType)
        Me.INDTxtNameCompany.Properties.MaxLength = 50
        Me.INDTxtNameCompany.StyleController = Me.INDlyCompany
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtNameCompany, 0)
        '
        'INDBteNitCompany
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDBteNitCompany, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDBteNitCompany, True)
        resources.ApplyResources(Me.INDBteNitCompany, "INDBteNitCompany")
        Me.IndigoTextEdit1.SetMascara(Me.INDBteNitCompany, Presentation.Controls.IndigoTextEdit.EMask.AlfaNumerico)
        Me.INDBteNitCompany.Name = "INDBteNitCompany"
        Me.INDBteNitCompany.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDBteNitCompany.Properties.Appearance.Font = CType(resources.GetObject("INDBteNitCompany.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDBteNitCompany.Properties.Appearance.Options.UseBackColor = True
        Me.INDBteNitCompany.Properties.Appearance.Options.UseFont = True
        Me.INDBteNitCompany.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDBteNitCompany.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDBteNitCompany.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDBteNitCompany.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDBteNitCompany.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDBteNitCompany.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDBteNitCompany.Properties.AppearanceFocused.Options.UseFont = True
        EditorButtonImageOptions2.Image = Global.Presentation.Payroll.My.Resources.Resources.BuscarMetro
        Me.INDBteNitCompany.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDBteNitCompany.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines), resources.GetString("INDBteNitCompany.Properties.Buttons1"), CType(resources.GetObject("INDBteNitCompany.Properties.Buttons2"), Integer), CType(resources.GetObject("INDBteNitCompany.Properties.Buttons3"), Boolean), CType(resources.GetObject("INDBteNitCompany.Properties.Buttons4"), Boolean), CType(resources.GetObject("INDBteNitCompany.Properties.Buttons5"), Boolean), EditorButtonImageOptions2, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject5, SerializableAppearanceObject6, SerializableAppearanceObject7, SerializableAppearanceObject8, resources.GetString("INDBteNitCompany.Properties.Buttons6"), CType(resources.GetObject("INDBteNitCompany.Properties.Buttons7"), Object), CType(resources.GetObject("INDBteNitCompany.Properties.Buttons8"), DevExpress.Utils.SuperToolTip), CType(resources.GetObject("INDBteNitCompany.Properties.Buttons9"), DevExpress.Utils.ToolTipAnchor))})
        Me.INDBteNitCompany.Properties.Mask.EditMask = resources.GetString("INDBteNitCompany.Properties.Mask.EditMask")
        Me.INDBteNitCompany.Properties.Mask.MaskType = CType(resources.GetObject("INDBteNitCompany.Properties.Mask.MaskType"), DevExpress.XtraEditors.Mask.MaskType)
        Me.INDBteNitCompany.Properties.MaxLength = 50
        Me.INDBteNitCompany.StyleController = Me.INDlyCompany
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDBteNitCompany, 0)
        '
        'INDLcgCompany
        '
        Me.INDLcgCompany.AppearanceGroup.Font = CType(resources.GetObject("INDLcgCompany.AppearanceGroup.Font"), System.Drawing.Font)
        Me.INDLcgCompany.AppearanceGroup.Options.UseFont = True
        Me.INDLcgCompany.AppearanceItemCaption.Font = CType(resources.GetObject("INDLcgCompany.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.INDLcgCompany.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgCompany.AppearanceTabPage.Header.Font = CType(resources.GetObject("INDLcgCompany.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.INDLcgCompany.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgCompany.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("INDLcgCompany.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        Me.INDLcgCompany.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgCompany.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("INDLcgCompany.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.INDLcgCompany.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgCompany.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("INDLcgCompany.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        Me.INDLcgCompany.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgCompany.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("INDLcgCompany.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.INDLcgCompany.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgCompany, False)
        resources.ApplyResources(Me.INDLcgCompany, "INDLcgCompany")
        Me.INDLcgCompany.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDLcgCompany.GroupBordersVisible = False
        Me.INDLcgCompany.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDGrCompany, Me.INDLcgCompanyType})
        Me.INDLcgCompany.Name = "INDLcgCompany"
        Me.INDLcgCompany.Size = New System.Drawing.Size(908, 568)
        Me.INDLcgCompany.TextVisible = False
        '
        'INDGrCompany
        '
        Me.INDGrCompany.AppearanceGroup.Font = CType(resources.GetObject("INDGrCompany.AppearanceGroup.Font"), System.Drawing.Font)
        Me.INDGrCompany.AppearanceGroup.Options.UseFont = True
        Me.INDGrCompany.AppearanceItemCaption.Font = CType(resources.GetObject("INDGrCompany.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.INDGrCompany.AppearanceItemCaption.Options.UseFont = True
        Me.INDGrCompany.AppearanceTabPage.Header.Font = CType(resources.GetObject("INDGrCompany.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.INDGrCompany.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDGrCompany.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("INDGrCompany.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        Me.INDGrCompany.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDGrCompany.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("INDGrCompany.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.INDGrCompany.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDGrCompany.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("INDGrCompany.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        Me.INDGrCompany.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDGrCompany.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("INDGrCompany.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.INDGrCompany.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDGrCompany, False)
        resources.ApplyResources(Me.INDGrCompany, "INDGrCompany")
        Me.INDGrCompany.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDItemNitCompany, Me.INDItemNameCompany, Me.INDItemLegalRepresentative, Me.INDItemDepartment, Me.INDItemCity, Me.INDlyItemControlData, Me.INDLciARLCompany, Me.INDLciThirdParty, Me.INDLciJudgmentAccount})
        Me.INDGrCompany.Location = New System.Drawing.Point(0, 0)
        Me.INDGrCompany.Name = "INDGrCompany"
        Me.INDGrCompany.Size = New System.Drawing.Size(474, 548)
        '
        'INDItemNitCompany
        '
        Me.INDItemNitCompany.Control = Me.INDBteNitCompany
        resources.ApplyResources(Me.INDItemNitCompany, "INDItemNitCompany")
        Me.INDItemNitCompany.Location = New System.Drawing.Point(0, 0)
        Me.INDItemNitCompany.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDItemNitCompany.MinSize = New System.Drawing.Size(390, 36)
        Me.INDItemNitCompany.Name = "INDItemNitCompany"
        Me.INDItemNitCompany.Size = New System.Drawing.Size(450, 36)
        Me.INDItemNitCompany.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDItemNitCompany.Tag = "Nit"
        Me.INDItemNitCompany.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDItemNitCompany.TextSize = New System.Drawing.Size(165, 21)
        Me.INDItemNitCompany.TextToControlDistance = 12
        '
        'INDItemNameCompany
        '
        Me.INDItemNameCompany.Control = Me.INDTxtNameCompany
        resources.ApplyResources(Me.INDItemNameCompany, "INDItemNameCompany")
        Me.INDItemNameCompany.Location = New System.Drawing.Point(0, 72)
        Me.INDItemNameCompany.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDItemNameCompany.MinSize = New System.Drawing.Size(390, 36)
        Me.INDItemNameCompany.Name = "INDItemNameCompany"
        Me.INDItemNameCompany.Size = New System.Drawing.Size(450, 36)
        Me.INDItemNameCompany.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDItemNameCompany.Tag = "Name"
        Me.INDItemNameCompany.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDItemNameCompany.TextSize = New System.Drawing.Size(165, 21)
        Me.INDItemNameCompany.TextToControlDistance = 12
        '
        'INDItemLegalRepresentative
        '
        Me.INDItemLegalRepresentative.Control = Me.INDTxtLegalRepresentative
        resources.ApplyResources(Me.INDItemLegalRepresentative, "INDItemLegalRepresentative")
        Me.INDItemLegalRepresentative.Location = New System.Drawing.Point(0, 108)
        Me.INDItemLegalRepresentative.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDItemLegalRepresentative.MinSize = New System.Drawing.Size(390, 36)
        Me.INDItemLegalRepresentative.Name = "INDItemLegalRepresentative"
        Me.INDItemLegalRepresentative.Size = New System.Drawing.Size(450, 36)
        Me.INDItemLegalRepresentative.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDItemLegalRepresentative.Tag = "LegalRepresentative"
        Me.INDItemLegalRepresentative.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDItemLegalRepresentative.TextSize = New System.Drawing.Size(165, 21)
        Me.INDItemLegalRepresentative.TextToControlDistance = 12
        '
        'INDItemDepartment
        '
        Me.INDItemDepartment.Control = Me.INDGleDepartment
        resources.ApplyResources(Me.INDItemDepartment, "INDItemDepartment")
        Me.INDItemDepartment.Location = New System.Drawing.Point(0, 144)
        Me.INDItemDepartment.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDItemDepartment.MinSize = New System.Drawing.Size(390, 36)
        Me.INDItemDepartment.Name = "INDItemDepartment"
        Me.INDItemDepartment.Size = New System.Drawing.Size(450, 36)
        Me.INDItemDepartment.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDItemDepartment.Tag = "DepartmentId"
        Me.INDItemDepartment.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDItemDepartment.TextSize = New System.Drawing.Size(165, 21)
        Me.INDItemDepartment.TextToControlDistance = 12
        '
        'INDItemCity
        '
        Me.INDItemCity.Control = Me.INDGleCity
        resources.ApplyResources(Me.INDItemCity, "INDItemCity")
        Me.INDItemCity.Location = New System.Drawing.Point(0, 180)
        Me.INDItemCity.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDItemCity.MinSize = New System.Drawing.Size(390, 36)
        Me.INDItemCity.Name = "INDItemCity"
        Me.INDItemCity.Size = New System.Drawing.Size(450, 36)
        Me.INDItemCity.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDItemCity.Tag = "CityId"
        Me.INDItemCity.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDItemCity.TextSize = New System.Drawing.Size(165, 21)
        Me.INDItemCity.TextToControlDistance = 12
        '
        'INDlyItemControlData
        '
        Me.INDlyItemControlData.Control = Me.INDpceControlData
        resources.ApplyResources(Me.INDlyItemControlData, "INDlyItemControlData")
        Me.INDlyItemControlData.Location = New System.Drawing.Point(0, 288)
        Me.INDlyItemControlData.MaxSize = New System.Drawing.Size(450, 36)
        Me.INDlyItemControlData.MinSize = New System.Drawing.Size(450, 36)
        Me.INDlyItemControlData.Name = "INDlyItemControlData"
        Me.INDlyItemControlData.Size = New System.Drawing.Size(450, 207)
        Me.INDlyItemControlData.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemControlData.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemControlData.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemControlData.TextToControlDistance = 0
        Me.INDlyItemControlData.TextVisible = False
        '
        'INDLciARLCompany
        '
        Me.INDLciARLCompany.Control = Me.INDSlARLCompany
        Me.INDLciARLCompany.Location = New System.Drawing.Point(0, 216)
        Me.INDLciARLCompany.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDLciARLCompany.MinSize = New System.Drawing.Size(390, 36)
        Me.INDLciARLCompany.Name = "INDLciARLCompany"
        Me.INDLciARLCompany.Size = New System.Drawing.Size(450, 36)
        Me.INDLciARLCompany.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        resources.ApplyResources(Me.INDLciARLCompany, "INDLciARLCompany")
        Me.INDLciARLCompany.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciARLCompany.TextSize = New System.Drawing.Size(165, 21)
        Me.INDLciARLCompany.TextToControlDistance = 12
        Me.INDLciARLCompany.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDLciThirdParty
        '
        Me.INDLciThirdParty.Control = Me.INDSlThirdParty
        Me.INDLciThirdParty.Location = New System.Drawing.Point(0, 36)
        Me.INDLciThirdParty.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDLciThirdParty.MinSize = New System.Drawing.Size(390, 36)
        Me.INDLciThirdParty.Name = "INDLciThirdParty"
        Me.INDLciThirdParty.Size = New System.Drawing.Size(450, 36)
        Me.INDLciThirdParty.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        resources.ApplyResources(Me.INDLciThirdParty, "INDLciThirdParty")
        Me.INDLciThirdParty.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciThirdParty.TextSize = New System.Drawing.Size(165, 21)
        Me.INDLciThirdParty.TextToControlDistance = 12
        '
        'INDLciJudgmentAccount
        '
        Me.INDLciJudgmentAccount.Control = Me.INDTxtJudgementCode
        Me.INDLciJudgmentAccount.Location = New System.Drawing.Point(0, 252)
        Me.INDLciJudgmentAccount.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDLciJudgmentAccount.MinSize = New System.Drawing.Size(390, 36)
        Me.INDLciJudgmentAccount.Name = "INDLciJudgmentAccount"
        Me.INDLciJudgmentAccount.Size = New System.Drawing.Size(450, 36)
        Me.INDLciJudgmentAccount.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        resources.ApplyResources(Me.INDLciJudgmentAccount, "INDLciJudgmentAccount")
        Me.INDLciJudgmentAccount.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciJudgmentAccount.TextLocation = DevExpress.Utils.Locations.Left
        Me.INDLciJudgmentAccount.TextSize = New System.Drawing.Size(165, 21)
        Me.INDLciJudgmentAccount.TextToControlDistance = 12
        Me.INDLciJudgmentAccount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDLcgCompanyType
        '
        Me.INDLcgCompanyType.AppearanceGroup.Font = CType(resources.GetObject("INDLcgCompanyType.AppearanceGroup.Font"), System.Drawing.Font)
        Me.INDLcgCompanyType.AppearanceGroup.Options.UseFont = True
        Me.INDLcgCompanyType.AppearanceItemCaption.Font = CType(resources.GetObject("INDLcgCompanyType.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.INDLcgCompanyType.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgCompanyType.AppearanceTabPage.Header.Font = CType(resources.GetObject("INDLcgCompanyType.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.INDLcgCompanyType.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgCompanyType.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("INDLcgCompanyType.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        Me.INDLcgCompanyType.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgCompanyType.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("INDLcgCompanyType.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.INDLcgCompanyType.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgCompanyType.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("INDLcgCompanyType.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        Me.INDLcgCompanyType.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgCompanyType.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("INDLcgCompanyType.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.INDLcgCompanyType.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgCompanyType, False)
        Me.INDLcgCompanyType.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyitemAgreementsType, Me.INDlycitemPayrolType, Me.INDlycitemAttachmentType})
        Me.INDLcgCompanyType.Location = New System.Drawing.Point(474, 0)
        Me.INDLcgCompanyType.Name = "INDLcgCompanyType"
        Me.INDLcgCompanyType.Size = New System.Drawing.Size(414, 548)
        resources.ApplyResources(Me.INDLcgCompanyType, "INDLcgCompanyType")
        '
        'INDlyitemAgreementsType
        '
        Me.INDlyitemAgreementsType.Control = Me.INDchkAgreementsType
        resources.ApplyResources(Me.INDlyitemAgreementsType, "INDlyitemAgreementsType")
        Me.INDlyitemAgreementsType.Location = New System.Drawing.Point(0, 0)
        Me.INDlyitemAgreementsType.MaxSize = New System.Drawing.Size(390, 32)
        Me.INDlyitemAgreementsType.MinSize = New System.Drawing.Size(390, 32)
        Me.INDlyitemAgreementsType.Name = "INDlyitemAgreementsType"
        Me.INDlyitemAgreementsType.Size = New System.Drawing.Size(390, 32)
        Me.INDlyitemAgreementsType.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyitemAgreementsType.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyitemAgreementsType.TextVisible = False
        '
        'INDlycitemPayrolType
        '
        Me.INDlycitemPayrolType.Control = Me.INDchkPayrolType
        resources.ApplyResources(Me.INDlycitemPayrolType, "INDlycitemPayrolType")
        Me.INDlycitemPayrolType.Location = New System.Drawing.Point(0, 32)
        Me.INDlycitemPayrolType.MaxSize = New System.Drawing.Size(390, 32)
        Me.INDlycitemPayrolType.MinSize = New System.Drawing.Size(390, 32)
        Me.INDlycitemPayrolType.Name = "INDlycitemPayrolType"
        Me.INDlycitemPayrolType.Size = New System.Drawing.Size(390, 32)
        Me.INDlycitemPayrolType.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlycitemPayrolType.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlycitemPayrolType.TextVisible = False
        '
        'INDlycitemAttachmentType
        '
        Me.INDlycitemAttachmentType.Control = Me.INDchkAttachmentsType
        Me.INDlycitemAttachmentType.Location = New System.Drawing.Point(0, 64)
        Me.INDlycitemAttachmentType.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDlycitemAttachmentType.MinSize = New System.Drawing.Size(390, 36)
        Me.INDlycitemAttachmentType.Name = "INDlycitemAttachmentType"
        Me.INDlycitemAttachmentType.Size = New System.Drawing.Size(390, 431)
        Me.INDlycitemAttachmentType.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlycitemAttachmentType.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlycitemAttachmentType.TextVisible = False
        '
        'CtrNavigationControlPanel1
        '
        Me.CtrNavigationControlPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        resources.ApplyResources(Me.CtrNavigationControlPanel1, "CtrNavigationControlPanel1")
        Me.CtrNavigationControlPanel1.LayoutControl = Me.INDlyCompany
        Me.CtrNavigationControlPanel1.Name = "CtrNavigationControlPanel1"
        Me.CtrNavigationControlPanel1.UseDisabledStatePainter = False
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        RepositoryItemPopupContainerEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("RepositoryItemPopupContainerEdit1.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.IndigoGridView1.RepositoryItemPopupContainerEdit = RepositoryItemPopupContainerEdit1
        '
        'FrmCompany
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.IconOptions.Icon = CType(resources.GetObject("FrmCompany.IconOptions.Icon"), System.Drawing.Icon)
        Me.IconOptions.ShowIcon = False
        Me.Name = "FrmCompany"
        Me.Opacity = 1.0R
        Me.Tag = "525"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyCompany, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyCompany.ResumeLayout(False)
        CType(Me.INDTxtJudgementCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSlThirdParty.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDchkAttachmentsType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDpccContactData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDpccContactData.ResumeLayout(False)
        CType(Me.INDSlARLCompany.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDchkAgreementsType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDchkPayrolType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDpceControlData.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGleCity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridLookUpMultiFilter3View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGleDepartment.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridLookUpMultiFilter2View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtLegalRepresentative.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtNameCompany.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDBteNitCompany.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgCompany, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGrCompany, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDItemNitCompany, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDItemNameCompany, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDItemLegalRepresentative, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDItemDepartment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDItemCity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemControlData, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciARLCompany, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciThirdParty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciJudgmentAccount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgCompanyType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyitemAgreementsType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlycitemPayrolType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlycitemAttachmentType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDlyCompany As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDLcgCompany As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDTxtLegalRepresentative As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDTxtNameCompany As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDBteNitCompany As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents INDGrCompany As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDItemNitCompany As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDItemNameCompany As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDItemLegalRepresentative As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents IndigoGridLookUpControl1 As Presentation.Controls.IndigoGridLookUpControl
    Friend WithEvents INDGleCity As Presentation.Controls.GridLookUpMultiFilter
    Friend WithEvents GridLookUpMultiFilter3View As Presentation.Controls.CustomGridView
    Friend WithEvents INDGleDepartment As Presentation.Controls.GridLookUpMultiFilter
    Friend WithEvents GridLookUpMultiFilter2View As Presentation.Controls.CustomGridView
    Friend WithEvents INDItemDepartment As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDItemCity As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents CityCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CityName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents DepartmentCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents DepartmentName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDpceControlData As DevExpress.XtraEditors.PopupContainerEdit
    Friend WithEvents INDlyItemControlData As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDpccContactData As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents CtrContacts As Presentation.Controls.CtrContactos
    Friend WithEvents INDchkAgreementsType As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents INDchkPayrolType As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents INDlyitemAgreementsType As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents INDSlARLCompany As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLciARLCompany As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDchkAttachmentsType As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents INDlycitemAttachmentType As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoCheckEdit1 As Controls.IndigoCheckEdit
    Friend WithEvents CtrNavigationControlPanel1 As Controls.CtrNavigationControlPanel
    Friend WithEvents INDSlThirdParty As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLciThirdParty As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLcgCompanyType As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlycitemPayrolType As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoGridView1 As Controls.IndigoGridView
    Friend WithEvents IndigoGridControl1 As Controls.IndigoGridControl
    Friend WithEvents INDTxtJudgementCode As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDLciJudgmentAccount As DevExpress.XtraLayout.LayoutControlItem
End Class
