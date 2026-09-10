<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmBranchOffice
    Inherits Presentation.Controls.FormBase

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmBranchOffice))
        Dim EditorButtonImageOptions1 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.INDlyBusinessUnit = New DevExpress.XtraLayout.LayoutControl()
        Me.INDGleCompanyId = New Presentation.Controls.GridLookUpMultiFilter()
        Me.GridLookUpMultiFilter1View = New Presentation.Controls.CustomGridView()
        Me.CompanyNit = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CompanyName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDTxtPhone = New DevExpress.XtraEditors.TextEdit()
        Me.INDTxtAddress = New DevExpress.XtraEditors.TextEdit()
        Me.INDTxtName = New DevExpress.XtraEditors.TextEdit()
        Me.INDBteCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.INDGleDepartment = New Presentation.Controls.GridLookUpMultiFilter()
        Me.CustomGridView1 = New Presentation.Controls.CustomGridView()
        Me.DepartmentCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.DepartmentName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGleCityId = New Presentation.Controls.GridLookUpMultiFilter()
        Me.CustomGridView2 = New Presentation.Controls.CustomGridView()
        Me.CityCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CityName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDGrBranchOffice = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemName = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemAddress = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemCity = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemPhone = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemDepartment = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemCompanyId = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoGridLookUpControl1 = New Presentation.Controls.IndigoGridLookUpControl(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyBusinessUnit, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyBusinessUnit.SuspendLayout()
        CType(Me.INDGleCompanyId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridLookUpMultiFilter1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtPhone.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtAddress.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDBteCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGleDepartment.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CustomGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGleCityId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CustomGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGrBranchOffice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemAddress, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemCity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemPhone, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemDepartment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemCompanyId, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlyBusinessUnit)
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
        'INDlyBusinessUnit
        '
        Me.INDlyBusinessUnit.AllowCustomization = False
        Me.INDlyBusinessUnit.Controls.Add(Me.INDGleCompanyId)
        Me.INDlyBusinessUnit.Controls.Add(Me.INDTxtPhone)
        Me.INDlyBusinessUnit.Controls.Add(Me.INDTxtAddress)
        Me.INDlyBusinessUnit.Controls.Add(Me.INDTxtName)
        Me.INDlyBusinessUnit.Controls.Add(Me.INDBteCode)
        Me.INDlyBusinessUnit.Controls.Add(Me.INDGleDepartment)
        Me.INDlyBusinessUnit.Controls.Add(Me.INDGleCityId)
        resources.ApplyResources(Me.INDlyBusinessUnit, "INDlyBusinessUnit")
        Me.LayoutControls.SetIsCustomizable(Me.INDlyBusinessUnit, False)
        Me.INDlyBusinessUnit.Name = "INDlyBusinessUnit"
        Me.INDlyBusinessUnit.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(536, 239, 744, 541)
        Me.INDlyBusinessUnit.Root = Me.LayoutControlGroup1
        '
        'INDGleCompanyId
        '
        Me.IndigoGridLookUpControl1.SetAbrirFormularioArchivo(Me.INDGleCompanyId, True)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDGleCompanyId, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDGleCompanyId, False)
        Me.IndigoGridLookUpControl1.SetGuardarXmlGrid(Me.INDGleCompanyId, False)
        resources.ApplyResources(Me.INDGleCompanyId, "INDGleCompanyId")
        Me.IndigoTextEdit1.SetMascara(Me.INDGleCompanyId, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDGleCompanyId.Name = "INDGleCompanyId"
        Me.INDGleCompanyId.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDGleCompanyId.Properties.Appearance.Font = CType(resources.GetObject("INDGleCompanyId.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDGleCompanyId.Properties.Appearance.Options.UseBackColor = True
        Me.INDGleCompanyId.Properties.Appearance.Options.UseFont = True
        Me.INDGleCompanyId.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDGleCompanyId.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDGleCompanyId.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDGleCompanyId.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDGleCompanyId.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDGleCompanyId.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDGleCompanyId.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDGleCompanyId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDGleCompanyId.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.INDGleCompanyId.Properties.DisplayMember = "Name"
        Me.INDGleCompanyId.Properties.ImmediatePopup = True
        Me.INDGleCompanyId.Properties.NullText = resources.GetString("INDGleCompanyId.Properties.NullText")
        Me.INDGleCompanyId.Properties.PopupView = Me.GridLookUpMultiFilter1View
        Me.INDGleCompanyId.Properties.SearchMode = DevExpress.XtraEditors.Repository.GridLookUpSearchMode.None
        Me.INDGleCompanyId.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.INDGleCompanyId.Properties.ValueMember = "Id"
        Me.INDGleCompanyId.StyleController = Me.INDlyBusinessUnit
        Me.IndigoGridLookUpControl1.SetTagFormularioAbrir(Me.INDGleCompanyId, "525")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDGleCompanyId, 0)
        '
        'GridLookUpMultiFilter1View
        '
        Me.GridLookUpMultiFilter1View.Appearance.HeaderPanel.Font = CType(resources.GetObject("GridLookUpMultiFilter1View.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.GridLookUpMultiFilter1View.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridLookUpMultiFilter1View.Appearance.Row.Font = CType(resources.GetObject("GridLookUpMultiFilter1View.Appearance.Row.Font"), System.Drawing.Font)
        Me.GridLookUpMultiFilter1View.Appearance.Row.Options.UseFont = True
        Me.GridLookUpMultiFilter1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.CompanyNit, Me.CompanyName})
        Me.GridLookUpMultiFilter1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridLookUpMultiFilter1View.Name = "GridLookUpMultiFilter1View"
        Me.GridLookUpMultiFilter1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridLookUpMultiFilter1View.OptionsView.EnableAppearanceEvenRow = True
        Me.GridLookUpMultiFilter1View.OptionsView.EnableAppearanceOddRow = True
        Me.GridLookUpMultiFilter1View.OptionsView.ShowGroupPanel = False
        '
        'CompanyNit
        '
        Me.CompanyNit.FieldName = "Nit"
        Me.CompanyNit.Name = "CompanyNit"
        resources.ApplyResources(Me.CompanyNit, "CompanyNit")
        '
        'CompanyName
        '
        Me.CompanyName.FieldName = "Name"
        Me.CompanyName.Name = "CompanyName"
        resources.ApplyResources(Me.CompanyName, "CompanyName")
        '
        'INDTxtPhone
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtPhone, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtPhone, True)
        resources.ApplyResources(Me.INDTxtPhone, "INDTxtPhone")
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtPhone, Presentation.Controls.IndigoTextEdit.EMask.Numerico)
        Me.INDTxtPhone.Name = "INDTxtPhone"
        Me.INDTxtPhone.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDTxtPhone.Properties.Appearance.Font = CType(resources.GetObject("INDTxtPhone.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDTxtPhone.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDTxtPhone.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtPhone.Properties.Appearance.Options.UseFont = True
        Me.INDTxtPhone.Properties.Appearance.Options.UseForeColor = True
        Me.INDTxtPhone.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtPhone.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtPhone.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDTxtPhone.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDTxtPhone.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtPhone.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtPhone.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtPhone.Properties.Mask.EditMask = resources.GetString("INDTxtPhone.Properties.Mask.EditMask")
        Me.INDTxtPhone.Properties.Mask.MaskType = CType(resources.GetObject("INDTxtPhone.Properties.Mask.MaskType"), DevExpress.XtraEditors.Mask.MaskType)
        Me.INDTxtPhone.Properties.MaxLength = 15
        Me.INDTxtPhone.StyleController = Me.INDlyBusinessUnit
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtPhone, 0)
        '
        'INDTxtAddress
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtAddress, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtAddress, True)
        Me.INDTxtAddress.EnterMoveNextControl = True
        resources.ApplyResources(Me.INDTxtAddress, "INDTxtAddress")
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtAddress, Presentation.Controls.IndigoTextEdit.EMask.AlfaNumerico)
        Me.INDTxtAddress.Name = "INDTxtAddress"
        Me.INDTxtAddress.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDTxtAddress.Properties.Appearance.Font = CType(resources.GetObject("INDTxtAddress.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDTxtAddress.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDTxtAddress.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtAddress.Properties.Appearance.Options.UseFont = True
        Me.INDTxtAddress.Properties.Appearance.Options.UseForeColor = True
        Me.INDTxtAddress.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtAddress.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtAddress.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDTxtAddress.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDTxtAddress.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtAddress.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtAddress.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtAddress.Properties.Mask.EditMask = resources.GetString("INDTxtAddress.Properties.Mask.EditMask")
        Me.INDTxtAddress.Properties.Mask.MaskType = CType(resources.GetObject("INDTxtAddress.Properties.Mask.MaskType"), DevExpress.XtraEditors.Mask.MaskType)
        Me.INDTxtAddress.Properties.MaxLength = 80
        Me.INDTxtAddress.StyleController = Me.INDlyBusinessUnit
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtAddress, 0)
        '
        'INDTxtName
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtName, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtName, True)
        Me.INDTxtName.EnterMoveNextControl = True
        resources.ApplyResources(Me.INDTxtName, "INDTxtName")
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtName, Presentation.Controls.IndigoTextEdit.EMask.AlfaNumerico)
        Me.INDTxtName.Name = "INDTxtName"
        Me.INDTxtName.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDTxtName.Properties.Appearance.Font = CType(resources.GetObject("INDTxtName.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDTxtName.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDTxtName.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtName.Properties.Appearance.Options.UseFont = True
        Me.INDTxtName.Properties.Appearance.Options.UseForeColor = True
        Me.INDTxtName.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtName.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtName.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDTxtName.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDTxtName.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtName.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtName.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtName.Properties.Mask.EditMask = resources.GetString("INDTxtName.Properties.Mask.EditMask")
        Me.INDTxtName.Properties.Mask.MaskType = CType(resources.GetObject("INDTxtName.Properties.Mask.MaskType"), DevExpress.XtraEditors.Mask.MaskType)
        Me.INDTxtName.Properties.MaxLength = 50
        Me.INDTxtName.StyleController = Me.INDlyBusinessUnit
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtName, 0)
        '
        'INDBteCode
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDBteCode, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDBteCode, True)
        resources.ApplyResources(Me.INDBteCode, "INDBteCode")
        Me.IndigoTextEdit1.SetMascara(Me.INDBteCode, Presentation.Controls.IndigoTextEdit.EMask.AlfaNumerico)
        Me.INDBteCode.Name = "INDBteCode"
        Me.INDBteCode.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDBteCode.Properties.Appearance.Font = CType(resources.GetObject("INDBteCode.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDBteCode.Properties.Appearance.Options.UseBackColor = True
        Me.INDBteCode.Properties.Appearance.Options.UseFont = True
        Me.INDBteCode.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDBteCode.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDBteCode.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDBteCode.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDBteCode.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDBteCode.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDBteCode.Properties.AppearanceFocused.Options.UseFont = True
        EditorButtonImageOptions1.Image = Global.Presentation.Payroll.My.Resources.Resources.BuscarMetro
        Me.INDBteCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDBteCode.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines), resources.GetString("INDBteCode.Properties.Buttons1"), CType(resources.GetObject("INDBteCode.Properties.Buttons2"), Integer), CType(resources.GetObject("INDBteCode.Properties.Buttons3"), Boolean), CType(resources.GetObject("INDBteCode.Properties.Buttons4"), Boolean), CType(resources.GetObject("INDBteCode.Properties.Buttons5"), Boolean), EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, resources.GetString("INDBteCode.Properties.Buttons6"), CType(resources.GetObject("INDBteCode.Properties.Buttons7"), Object), CType(resources.GetObject("INDBteCode.Properties.Buttons8"), DevExpress.Utils.SuperToolTip), CType(resources.GetObject("INDBteCode.Properties.Buttons9"), DevExpress.Utils.ToolTipAnchor))})
        Me.INDBteCode.Properties.Mask.EditMask = resources.GetString("INDBteCode.Properties.Mask.EditMask")
        Me.INDBteCode.Properties.Mask.MaskType = CType(resources.GetObject("INDBteCode.Properties.Mask.MaskType"), DevExpress.XtraEditors.Mask.MaskType)
        Me.INDBteCode.Properties.MaxLength = 20
        Me.INDBteCode.StyleController = Me.INDlyBusinessUnit
        Me.INDBteCode.Tag = ""
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDBteCode, 0)
        '
        'INDGleDepartment
        '
        Me.IndigoGridLookUpControl1.SetAbrirFormularioArchivo(Me.INDGleDepartment, True)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDGleDepartment, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDGleDepartment, False)
        Me.INDGleDepartment.EnterMoveNextControl = True
        Me.IndigoGridLookUpControl1.SetGuardarXmlGrid(Me.INDGleDepartment, False)
        resources.ApplyResources(Me.INDGleDepartment, "INDGleDepartment")
        Me.IndigoTextEdit1.SetMascara(Me.INDGleDepartment, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDGleDepartment.Name = "INDGleDepartment"
        Me.INDGleDepartment.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDGleDepartment.Properties.Appearance.Font = CType(resources.GetObject("INDGleDepartment.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDGleDepartment.Properties.Appearance.Options.UseBackColor = True
        Me.INDGleDepartment.Properties.Appearance.Options.UseFont = True
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
        Me.INDGleDepartment.Properties.PopupView = Me.CustomGridView1
        Me.INDGleDepartment.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.INDGleDepartment.Properties.ValueMember = "Id"
        Me.INDGleDepartment.StyleController = Me.INDlyBusinessUnit
        Me.IndigoGridLookUpControl1.SetTagFormularioAbrir(Me.INDGleDepartment, "510")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDGleDepartment, 0)
        '
        'CustomGridView1
        '
        Me.CustomGridView1.Appearance.HeaderPanel.Font = CType(resources.GetObject("CustomGridView1.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.CustomGridView1.Appearance.HeaderPanel.Options.UseFont = True
        Me.CustomGridView1.Appearance.Row.Font = CType(resources.GetObject("CustomGridView1.Appearance.Row.Font"), System.Drawing.Font)
        Me.CustomGridView1.Appearance.Row.Options.UseFont = True
        Me.CustomGridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.DepartmentCode, Me.DepartmentName})
        Me.CustomGridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.CustomGridView1.Name = "CustomGridView1"
        Me.CustomGridView1.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.CustomGridView1.OptionsView.EnableAppearanceEvenRow = True
        Me.CustomGridView1.OptionsView.EnableAppearanceOddRow = True
        Me.CustomGridView1.OptionsView.ShowGroupPanel = False
        '
        'DepartmentCode
        '
        Me.DepartmentCode.FieldName = "Code"
        Me.DepartmentCode.Name = "DepartmentCode"
        resources.ApplyResources(Me.DepartmentCode, "DepartmentCode")
        '
        'DepartmentName
        '
        Me.DepartmentName.FieldName = "Name"
        Me.DepartmentName.Name = "DepartmentName"
        resources.ApplyResources(Me.DepartmentName, "DepartmentName")
        '
        'INDGleCityId
        '
        Me.IndigoGridLookUpControl1.SetAbrirFormularioArchivo(Me.INDGleCityId, True)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDGleCityId, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDGleCityId, False)
        resources.ApplyResources(Me.INDGleCityId, "INDGleCityId")
        Me.INDGleCityId.EnterMoveNextControl = True
        Me.IndigoGridLookUpControl1.SetGuardarXmlGrid(Me.INDGleCityId, False)
        Me.IndigoTextEdit1.SetMascara(Me.INDGleCityId, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDGleCityId.Name = "INDGleCityId"
        Me.INDGleCityId.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDGleCityId.Properties.Appearance.Font = CType(resources.GetObject("INDGleCityId.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDGleCityId.Properties.Appearance.Options.UseBackColor = True
        Me.INDGleCityId.Properties.Appearance.Options.UseFont = True
        Me.INDGleCityId.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDGleCityId.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDGleCityId.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDGleCityId.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDGleCityId.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDGleCityId.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDGleCityId.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDGleCityId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDGleCityId.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.INDGleCityId.Properties.DisplayMember = "Name"
        Me.INDGleCityId.Properties.ImmediatePopup = True
        Me.INDGleCityId.Properties.NullText = resources.GetString("INDGleCityId.Properties.NullText")
        Me.INDGleCityId.Properties.PopupView = Me.CustomGridView2
        Me.INDGleCityId.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.INDGleCityId.Properties.ValueMember = "Id"
        Me.INDGleCityId.StyleController = Me.INDlyBusinessUnit
        Me.IndigoGridLookUpControl1.SetTagFormularioAbrir(Me.INDGleCityId, "513")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDGleCityId, 0)
        '
        'CustomGridView2
        '
        Me.CustomGridView2.Appearance.HeaderPanel.Font = CType(resources.GetObject("CustomGridView2.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.CustomGridView2.Appearance.HeaderPanel.Options.UseFont = True
        Me.CustomGridView2.Appearance.Row.Font = CType(resources.GetObject("CustomGridView2.Appearance.Row.Font"), System.Drawing.Font)
        Me.CustomGridView2.Appearance.Row.Options.UseFont = True
        Me.CustomGridView2.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.CityCode, Me.CityName})
        Me.CustomGridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.CustomGridView2.Name = "CustomGridView2"
        Me.CustomGridView2.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.CustomGridView2.OptionsView.EnableAppearanceEvenRow = True
        Me.CustomGridView2.OptionsView.EnableAppearanceOddRow = True
        Me.CustomGridView2.OptionsView.ShowGroupPanel = False
        '
        'CityCode
        '
        Me.CityCode.FieldName = "Code"
        Me.CityCode.Name = "CityCode"
        resources.ApplyResources(Me.CityCode, "CityCode")
        '
        'CityName
        '
        Me.CityName.FieldName = "Name"
        Me.CityName.Name = "CityName"
        resources.ApplyResources(Me.CityName, "CityName")
        '
        'LayoutControlGroup1
        '
        resources.ApplyResources(Me.LayoutControlGroup1, "LayoutControlGroup1")
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDGrBranchOffice})
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(731, 360)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDGrBranchOffice
        '
        Me.INDGrBranchOffice.AppearanceGroup.Font = CType(resources.GetObject("INDGrBranchOffice.AppearanceGroup.Font"), System.Drawing.Font)
        Me.INDGrBranchOffice.AppearanceGroup.Options.UseFont = True
        Me.INDGrBranchOffice.AppearanceItemCaption.Font = CType(resources.GetObject("INDGrBranchOffice.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.INDGrBranchOffice.AppearanceItemCaption.Options.UseFont = True
        resources.ApplyResources(Me.INDGrBranchOffice, "INDGrBranchOffice")
        Me.INDGrBranchOffice.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemName, Me.INDlyItemAddress, Me.INDlyItemCity, Me.INDlyItemPhone, Me.INDlyItemCode, Me.INDlyItemDepartment, Me.INDlyItemCompanyId})
        Me.INDGrBranchOffice.Location = New System.Drawing.Point(0, 0)
        Me.INDGrBranchOffice.Name = "INDGrBranchOffice"
        Me.INDGrBranchOffice.Size = New System.Drawing.Size(711, 340)
        '
        'INDlyItemName
        '
        Me.INDlyItemName.Control = Me.INDTxtName
        resources.ApplyResources(Me.INDlyItemName, "INDlyItemName")
        Me.INDlyItemName.Location = New System.Drawing.Point(0, 72)
        Me.INDlyItemName.MaxSize = New System.Drawing.Size(410, 36)
        Me.INDlyItemName.MinSize = New System.Drawing.Size(410, 36)
        Me.INDlyItemName.Name = "INDlyItemName"
        Me.INDlyItemName.Size = New System.Drawing.Size(687, 36)
        Me.INDlyItemName.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemName.Tag = "Name"
        Me.INDlyItemName.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemName.TextSize = New System.Drawing.Size(165, 21)
        Me.INDlyItemName.TextToControlDistance = 12
        '
        'INDlyItemAddress
        '
        Me.INDlyItemAddress.Control = Me.INDTxtAddress
        resources.ApplyResources(Me.INDlyItemAddress, "INDlyItemAddress")
        Me.INDlyItemAddress.Location = New System.Drawing.Point(0, 108)
        Me.INDlyItemAddress.MaxSize = New System.Drawing.Size(410, 36)
        Me.INDlyItemAddress.MinSize = New System.Drawing.Size(410, 36)
        Me.INDlyItemAddress.Name = "INDlyItemAddress"
        Me.INDlyItemAddress.Size = New System.Drawing.Size(687, 36)
        Me.INDlyItemAddress.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemAddress.Tag = "Address"
        Me.INDlyItemAddress.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemAddress.TextSize = New System.Drawing.Size(165, 21)
        Me.INDlyItemAddress.TextToControlDistance = 12
        '
        'INDlyItemCity
        '
        Me.INDlyItemCity.Control = Me.INDGleCityId
        resources.ApplyResources(Me.INDlyItemCity, "INDlyItemCity")
        Me.INDlyItemCity.Location = New System.Drawing.Point(0, 180)
        Me.INDlyItemCity.MaxSize = New System.Drawing.Size(410, 36)
        Me.INDlyItemCity.MinSize = New System.Drawing.Size(410, 36)
        Me.INDlyItemCity.Name = "INDlyItemCity"
        Me.INDlyItemCity.Size = New System.Drawing.Size(687, 36)
        Me.INDlyItemCity.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemCity.Tag = "CityId"
        Me.INDlyItemCity.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemCity.TextSize = New System.Drawing.Size(165, 21)
        Me.INDlyItemCity.TextToControlDistance = 12
        '
        'INDlyItemPhone
        '
        Me.INDlyItemPhone.Control = Me.INDTxtPhone
        resources.ApplyResources(Me.INDlyItemPhone, "INDlyItemPhone")
        Me.INDlyItemPhone.Location = New System.Drawing.Point(0, 216)
        Me.INDlyItemPhone.MaxSize = New System.Drawing.Size(410, 36)
        Me.INDlyItemPhone.MinSize = New System.Drawing.Size(410, 36)
        Me.INDlyItemPhone.Name = "INDlyItemPhone"
        Me.INDlyItemPhone.Size = New System.Drawing.Size(687, 71)
        Me.INDlyItemPhone.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemPhone.Tag = "Phone"
        Me.INDlyItemPhone.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemPhone.TextSize = New System.Drawing.Size(165, 21)
        Me.INDlyItemPhone.TextToControlDistance = 12
        '
        'INDlyItemCode
        '
        Me.INDlyItemCode.Control = Me.INDBteCode
        resources.ApplyResources(Me.INDlyItemCode, "INDlyItemCode")
        Me.INDlyItemCode.Location = New System.Drawing.Point(0, 36)
        Me.INDlyItemCode.MaxSize = New System.Drawing.Size(410, 36)
        Me.INDlyItemCode.MinSize = New System.Drawing.Size(410, 36)
        Me.INDlyItemCode.Name = "INDlyItemCode"
        Me.INDlyItemCode.Size = New System.Drawing.Size(687, 36)
        Me.INDlyItemCode.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemCode.Tag = "Code"
        Me.INDlyItemCode.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemCode.TextSize = New System.Drawing.Size(165, 21)
        Me.INDlyItemCode.TextToControlDistance = 12
        '
        'INDlyItemDepartment
        '
        Me.INDlyItemDepartment.AppearanceItemCaption.Font = CType(resources.GetObject("INDlyItemDepartment.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.INDlyItemDepartment.AppearanceItemCaption.Options.UseFont = True
        Me.INDlyItemDepartment.Control = Me.INDGleDepartment
        resources.ApplyResources(Me.INDlyItemDepartment, "INDlyItemDepartment")
        Me.INDlyItemDepartment.Location = New System.Drawing.Point(0, 144)
        Me.INDlyItemDepartment.MaxSize = New System.Drawing.Size(410, 36)
        Me.INDlyItemDepartment.MinSize = New System.Drawing.Size(410, 36)
        Me.INDlyItemDepartment.Name = "INDlyItemDepartment"
        Me.INDlyItemDepartment.Size = New System.Drawing.Size(687, 36)
        Me.INDlyItemDepartment.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemDepartment.Tag = "DepartmentId"
        Me.INDlyItemDepartment.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemDepartment.TextSize = New System.Drawing.Size(165, 21)
        Me.INDlyItemDepartment.TextToControlDistance = 12
        '
        'INDlyItemCompanyId
        '
        Me.INDlyItemCompanyId.Control = Me.INDGleCompanyId
        resources.ApplyResources(Me.INDlyItemCompanyId, "INDlyItemCompanyId")
        Me.INDlyItemCompanyId.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemCompanyId.MaxSize = New System.Drawing.Size(410, 36)
        Me.INDlyItemCompanyId.MinSize = New System.Drawing.Size(410, 36)
        Me.INDlyItemCompanyId.Name = "INDlyItemCompanyId"
        Me.INDlyItemCompanyId.Size = New System.Drawing.Size(687, 36)
        Me.INDlyItemCompanyId.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemCompanyId.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemCompanyId.TextSize = New System.Drawing.Size(165, 21)
        Me.INDlyItemCompanyId.TextToControlDistance = 12
        Me.INDlyItemCompanyId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'FrmBranchOffice
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.IconOptions.ShowIcon = False
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmBranchOffice"
        Me.Opacity = 1.0R
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.Tag = "520"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyBusinessUnit, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyBusinessUnit.ResumeLayout(False)
        CType(Me.INDGleCompanyId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridLookUpMultiFilter1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtPhone.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtAddress.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDBteCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGleDepartment.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CustomGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGleCityId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CustomGridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGrBranchOffice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemAddress, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemCity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemPhone, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemDepartment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemCompanyId, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDlyBusinessUnit As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDBteCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDGrBranchOffice As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemCode As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoGridLookUpControl1 As Presentation.Controls.IndigoGridLookUpControl
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents INDTxtPhone As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDTxtAddress As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDTxtName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDlyItemName As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemAddress As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemCity As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemPhone As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl
    Friend WithEvents INDlyItemDepartment As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDGleDepartment As Presentation.Controls.GridLookUpMultiFilter
    Friend WithEvents CustomGridView1 As Presentation.Controls.CustomGridView
    Friend WithEvents INDGleCityId As Presentation.Controls.GridLookUpMultiFilter
    Friend WithEvents CustomGridView2 As Presentation.Controls.CustomGridView
    Friend WithEvents INDGleCompanyId As Presentation.Controls.GridLookUpMultiFilter
    Friend WithEvents GridLookUpMultiFilter1View As Presentation.Controls.CustomGridView
    Friend WithEvents INDlyItemCompanyId As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents CompanyNit As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CompanyName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents DepartmentCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents DepartmentName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CityCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CityName As DevExpress.XtraGrid.Columns.GridColumn
End Class
