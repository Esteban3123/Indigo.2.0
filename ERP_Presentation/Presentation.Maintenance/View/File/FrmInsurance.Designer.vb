Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmInsurance
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmInsurance))
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.INDbteCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.INDlyInsurance = New DevExpress.XtraLayout.LayoutControl()
        Me.INDpccContactsControl = New DevExpress.XtraEditors.PopupContainerControl()
        Me.CtrContacts = New Presentation.Controls.CtrContactos()
        Me.INDpceContactData = New DevExpress.XtraEditors.PopupContainerEdit()
        Me.INDglDepartamentos = New Presentation.Controls.GridLookUpMultiFilter()
        Me.CustomGridView1 = New Presentation.Controls.CustomGridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDglCity = New Presentation.Controls.GridLookUpMultiFilter()
        Me.GridLookUpMultiFilter2View = New Presentation.Controls.CustomGridView()
        Me.DepartmentCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.DepartmentName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDtxtWebSite = New DevExpress.XtraEditors.TextEdit()
        Me.INDtxtName = New DevExpress.XtraEditors.TextEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyGrResponsibles = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemName = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemWebSite = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemCity = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemDepartamento = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemContacts = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoGridLookUpControl1 = New Presentation.Controls.IndigoGridLookUpControl(Me.components)
        Me.CtrNavigationControl1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDbteCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyInsurance, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyInsurance.SuspendLayout()
        CType(Me.INDpccContactsControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDpccContactsControl.SuspendLayout()
        CType(Me.INDpceContactData.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDglDepartamentos.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CustomGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDglCity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridLookUpMultiFilter2View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtWebSite.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyGrResponsibles, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemWebSite, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemCity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemDepartamento, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemContacts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        resources.ApplyResources(Me.INDPanelControlBase, "INDPanelControlBase")
        Me.INDPanelControlBase.Controls.Add(Me.INDlyInsurance)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControl1)
        '
        'ToolBars
        '
        resources.ApplyResources(Me.ToolBars, "ToolBars")
        Me.ToolBars.Appearance.BackColor = CType(resources.GetObject("ToolBars.Appearance.BackColor"), System.Drawing.Color)
        Me.ToolBars.Appearance.FontSizeDelta = CType(resources.GetObject("ToolBars.Appearance.FontSizeDelta"), Integer)
        Me.ToolBars.Appearance.FontStyleDelta = CType(resources.GetObject("ToolBars.Appearance.FontStyleDelta"), System.Drawing.FontStyle)
        Me.ToolBars.Appearance.GradientMode = CType(resources.GetObject("ToolBars.Appearance.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.ToolBars.Appearance.Image = CType(resources.GetObject("ToolBars.Appearance.Image"), System.Drawing.Image)
        Me.ToolBars.Appearance.Options.UseBackColor = True
        '
        'BarraBotones
        '
        resources.ApplyResources(Me.BarraBotones, "BarraBotones")
        Me.BarraBotones.LookAndFeel.SkinName = "Blue"
        Me.BarraBotones.OperatingUnitVisible = True
        '
        'INDbteCode
        '
        resources.ApplyResources(Me.INDbteCode, "INDbteCode")
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDbteCode, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDbteCode, True)
        Me.IndigoTextEdit1.SetMascara(Me.INDbteCode, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDbteCode.Name = "INDbteCode"
        Me.INDbteCode.Properties.AccessibleDescription = resources.GetString("INDbteCode.Properties.AccessibleDescription")
        Me.INDbteCode.Properties.AccessibleName = resources.GetString("INDbteCode.Properties.AccessibleName")
        Me.INDbteCode.Properties.Appearance.BackColor = CType(resources.GetObject("INDbteCode.Properties.Appearance.BackColor"), System.Drawing.Color)
        Me.INDbteCode.Properties.Appearance.Font = CType(resources.GetObject("INDbteCode.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDbteCode.Properties.Appearance.FontSizeDelta = CType(resources.GetObject("INDbteCode.Properties.Appearance.FontSizeDelta"), Integer)
        Me.INDbteCode.Properties.Appearance.FontStyleDelta = CType(resources.GetObject("INDbteCode.Properties.Appearance.FontStyleDelta"), System.Drawing.FontStyle)
        Me.INDbteCode.Properties.Appearance.GradientMode = CType(resources.GetObject("INDbteCode.Properties.Appearance.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.INDbteCode.Properties.Appearance.Image = CType(resources.GetObject("INDbteCode.Properties.Appearance.Image"), System.Drawing.Image)
        Me.INDbteCode.Properties.Appearance.Options.UseBackColor = True
        Me.INDbteCode.Properties.Appearance.Options.UseFont = True
        Me.INDbteCode.Properties.AppearanceFocused.BackColor = CType(resources.GetObject("INDbteCode.Properties.AppearanceFocused.BackColor"), System.Drawing.Color)
        Me.INDbteCode.Properties.AppearanceFocused.BorderColor = CType(resources.GetObject("INDbteCode.Properties.AppearanceFocused.BorderColor"), System.Drawing.Color)
        Me.INDbteCode.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDbteCode.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDbteCode.Properties.AppearanceFocused.FontSizeDelta = CType(resources.GetObject("INDbteCode.Properties.AppearanceFocused.FontSizeDelta"), Integer)
        Me.INDbteCode.Properties.AppearanceFocused.FontStyleDelta = CType(resources.GetObject("INDbteCode.Properties.AppearanceFocused.FontStyleDelta"), System.Drawing.FontStyle)
        Me.INDbteCode.Properties.AppearanceFocused.GradientMode = CType(resources.GetObject("INDbteCode.Properties.AppearanceFocused.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.INDbteCode.Properties.AppearanceFocused.Image = CType(resources.GetObject("INDbteCode.Properties.AppearanceFocused.Image"), System.Drawing.Image)
        Me.INDbteCode.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDbteCode.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDbteCode.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDbteCode.Properties.AutoHeight = CType(resources.GetObject("INDbteCode.Properties.AutoHeight"), Boolean)
        resources.ApplyResources(SerializableAppearanceObject2, "SerializableAppearanceObject2")
        SerializableAppearanceObject2.Options.UseFont = True
        Me.INDbteCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDbteCode.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines), resources.GetString("INDbteCode.Properties.Buttons1"), CType(resources.GetObject("INDbteCode.Properties.Buttons2"), Integer), CType(resources.GetObject("INDbteCode.Properties.Buttons3"), Boolean), CType(resources.GetObject("INDbteCode.Properties.Buttons4"), Boolean), CType(resources.GetObject("INDbteCode.Properties.Buttons5"), Boolean), CType(resources.GetObject("INDbteCode.Properties.Buttons6"), DevExpress.XtraEditors.ImageLocation), Global.Presentation.Maintenance.My.Resources.Resources.BuscarMetro, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, resources.GetString("INDbteCode.Properties.Buttons7"), CType(resources.GetObject("INDbteCode.Properties.Buttons8"), Object), CType(resources.GetObject("INDbteCode.Properties.Buttons9"), DevExpress.Utils.SuperToolTip), CType(resources.GetObject("INDbteCode.Properties.Buttons10"), Boolean))})
        Me.INDbteCode.Properties.Mask.AutoComplete = CType(resources.GetObject("INDbteCode.Properties.Mask.AutoComplete"), DevExpress.XtraEditors.Mask.AutoCompleteType)
        Me.INDbteCode.Properties.Mask.BeepOnError = CType(resources.GetObject("INDbteCode.Properties.Mask.BeepOnError"), Boolean)
        Me.INDbteCode.Properties.Mask.EditMask = resources.GetString("INDbteCode.Properties.Mask.EditMask")
        Me.INDbteCode.Properties.Mask.IgnoreMaskBlank = CType(resources.GetObject("INDbteCode.Properties.Mask.IgnoreMaskBlank"), Boolean)
        Me.INDbteCode.Properties.Mask.MaskType = CType(resources.GetObject("INDbteCode.Properties.Mask.MaskType"), DevExpress.XtraEditors.Mask.MaskType)
        Me.INDbteCode.Properties.Mask.PlaceHolder = CType(resources.GetObject("INDbteCode.Properties.Mask.PlaceHolder"), Char)
        Me.INDbteCode.Properties.Mask.SaveLiteral = CType(resources.GetObject("INDbteCode.Properties.Mask.SaveLiteral"), Boolean)
        Me.INDbteCode.Properties.Mask.ShowPlaceHolders = CType(resources.GetObject("INDbteCode.Properties.Mask.ShowPlaceHolders"), Boolean)
        Me.INDbteCode.Properties.Mask.UseMaskAsDisplayFormat = CType(resources.GetObject("INDbteCode.Properties.Mask.UseMaskAsDisplayFormat"), Boolean)
        Me.INDbteCode.Properties.MaxLength = 15
        Me.INDbteCode.Properties.NullValuePrompt = resources.GetString("INDbteCode.Properties.NullValuePrompt")
        Me.INDbteCode.Properties.NullValuePromptShowForEmptyValue = CType(resources.GetObject("INDbteCode.Properties.NullValuePromptShowForEmptyValue"), Boolean)
        Me.INDbteCode.StyleController = Me.INDlyInsurance
        Me.INDbteCode.Tag = ""
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDbteCode, 0)
        '
        'INDlyInsurance
        '
        resources.ApplyResources(Me.INDlyInsurance, "INDlyInsurance")
        Me.INDlyInsurance.AllowCustomization = False
        Me.INDlyInsurance.Appearance.DisabledLayoutGroupCaption.FontSizeDelta = CType(resources.GetObject("INDlyInsurance.Appearance.DisabledLayoutGroupCaption.FontSizeDelta"), Integer)
        Me.INDlyInsurance.Appearance.DisabledLayoutGroupCaption.FontStyleDelta = CType(resources.GetObject("INDlyInsurance.Appearance.DisabledLayoutGroupCaption.FontStyleDelta"), System.Drawing.FontStyle)
        Me.INDlyInsurance.Appearance.DisabledLayoutGroupCaption.GradientMode = CType(resources.GetObject("INDlyInsurance.Appearance.DisabledLayoutGroupCaption.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.INDlyInsurance.Appearance.DisabledLayoutGroupCaption.Image = CType(resources.GetObject("INDlyInsurance.Appearance.DisabledLayoutGroupCaption.Image"), System.Drawing.Image)
        Me.INDlyInsurance.Appearance.DisabledLayoutItem.FontSizeDelta = CType(resources.GetObject("INDlyInsurance.Appearance.DisabledLayoutItem.FontSizeDelta"), Integer)
        Me.INDlyInsurance.Appearance.DisabledLayoutItem.FontStyleDelta = CType(resources.GetObject("INDlyInsurance.Appearance.DisabledLayoutItem.FontStyleDelta"), System.Drawing.FontStyle)
        Me.INDlyInsurance.Appearance.DisabledLayoutItem.GradientMode = CType(resources.GetObject("INDlyInsurance.Appearance.DisabledLayoutItem.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.INDlyInsurance.Appearance.DisabledLayoutItem.Image = CType(resources.GetObject("INDlyInsurance.Appearance.DisabledLayoutItem.Image"), System.Drawing.Image)
        Me.INDlyInsurance.Controls.Add(Me.INDpccContactsControl)
        Me.INDlyInsurance.Controls.Add(Me.INDpceContactData)
        Me.INDlyInsurance.Controls.Add(Me.INDglDepartamentos)
        Me.INDlyInsurance.Controls.Add(Me.INDglCity)
        Me.INDlyInsurance.Controls.Add(Me.INDtxtWebSite)
        Me.INDlyInsurance.Controls.Add(Me.INDtxtName)
        Me.INDlyInsurance.Controls.Add(Me.INDbteCode)
        Me.LayoutControls.SetIsCustomizable(Me.INDlyInsurance, False)
        Me.INDlyInsurance.Name = "INDlyInsurance"
        Me.INDlyInsurance.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(741, 239, 675, 350)
        Me.INDlyInsurance.OptionsPrint.AppearanceGroupCaption.FontSizeDelta = CType(resources.GetObject("INDlyInsurance.OptionsPrint.AppearanceGroupCaption.FontSizeDelta"), Integer)
        Me.INDlyInsurance.OptionsPrint.AppearanceGroupCaption.FontStyleDelta = CType(resources.GetObject("INDlyInsurance.OptionsPrint.AppearanceGroupCaption.FontStyleDelta"), System.Drawing.FontStyle)
        Me.INDlyInsurance.OptionsPrint.AppearanceGroupCaption.GradientMode = CType(resources.GetObject("INDlyInsurance.OptionsPrint.AppearanceGroupCaption.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.INDlyInsurance.OptionsPrint.AppearanceGroupCaption.Image = CType(resources.GetObject("INDlyInsurance.OptionsPrint.AppearanceGroupCaption.Image"), System.Drawing.Image)
        Me.INDlyInsurance.OptionsPrint.AppearanceItemCaption.FontSizeDelta = CType(resources.GetObject("INDlyInsurance.OptionsPrint.AppearanceItemCaption.FontSizeDelta"), Integer)
        Me.INDlyInsurance.OptionsPrint.AppearanceItemCaption.FontStyleDelta = CType(resources.GetObject("INDlyInsurance.OptionsPrint.AppearanceItemCaption.FontStyleDelta"), System.Drawing.FontStyle)
        Me.INDlyInsurance.OptionsPrint.AppearanceItemCaption.GradientMode = CType(resources.GetObject("INDlyInsurance.OptionsPrint.AppearanceItemCaption.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.INDlyInsurance.OptionsPrint.AppearanceItemCaption.Image = CType(resources.GetObject("INDlyInsurance.OptionsPrint.AppearanceItemCaption.Image"), System.Drawing.Image)
        Me.INDlyInsurance.Root = Me.LayoutControlGroup1
        '
        'INDpccContactsControl
        '
        resources.ApplyResources(Me.INDpccContactsControl, "INDpccContactsControl")
        Me.INDpccContactsControl.Controls.Add(Me.CtrContacts)
        Me.INDpccContactsControl.Name = "INDpccContactsControl"
        '
        'CtrContacts
        '
        resources.ApplyResources(Me.CtrContacts, "CtrContacts")
        Me.CtrContacts.Name = "CtrContacts"
        Me.CtrContacts.OpenByFormUser = False
        '
        'INDpceContactData
        '
        resources.ApplyResources(Me.INDpceContactData, "INDpceContactData")
        Me.INDpceContactData.Name = "INDpceContactData"
        Me.INDpceContactData.Properties.AccessibleDescription = resources.GetString("INDpceContactData.Properties.AccessibleDescription")
        Me.INDpceContactData.Properties.AccessibleName = resources.GetString("INDpceContactData.Properties.AccessibleName")
        Me.INDpceContactData.Properties.Appearance.BackColor = CType(resources.GetObject("INDpceContactData.Properties.Appearance.BackColor"), System.Drawing.Color)
        Me.INDpceContactData.Properties.Appearance.Font = CType(resources.GetObject("INDpceContactData.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDpceContactData.Properties.Appearance.FontSizeDelta = CType(resources.GetObject("INDpceContactData.Properties.Appearance.FontSizeDelta"), Integer)
        Me.INDpceContactData.Properties.Appearance.FontStyleDelta = CType(resources.GetObject("INDpceContactData.Properties.Appearance.FontStyleDelta"), System.Drawing.FontStyle)
        Me.INDpceContactData.Properties.Appearance.GradientMode = CType(resources.GetObject("INDpceContactData.Properties.Appearance.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.INDpceContactData.Properties.Appearance.Image = CType(resources.GetObject("INDpceContactData.Properties.Appearance.Image"), System.Drawing.Image)
        Me.INDpceContactData.Properties.Appearance.Options.UseBackColor = True
        Me.INDpceContactData.Properties.Appearance.Options.UseFont = True
        Me.INDpceContactData.Properties.AppearanceFocused.BackColor = CType(resources.GetObject("INDpceContactData.Properties.AppearanceFocused.BackColor"), System.Drawing.Color)
        Me.INDpceContactData.Properties.AppearanceFocused.BorderColor = CType(resources.GetObject("INDpceContactData.Properties.AppearanceFocused.BorderColor"), System.Drawing.Color)
        Me.INDpceContactData.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDpceContactData.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDpceContactData.Properties.AppearanceFocused.FontSizeDelta = CType(resources.GetObject("INDpceContactData.Properties.AppearanceFocused.FontSizeDelta"), Integer)
        Me.INDpceContactData.Properties.AppearanceFocused.FontStyleDelta = CType(resources.GetObject("INDpceContactData.Properties.AppearanceFocused.FontStyleDelta"), System.Drawing.FontStyle)
        Me.INDpceContactData.Properties.AppearanceFocused.GradientMode = CType(resources.GetObject("INDpceContactData.Properties.AppearanceFocused.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.INDpceContactData.Properties.AppearanceFocused.Image = CType(resources.GetObject("INDpceContactData.Properties.AppearanceFocused.Image"), System.Drawing.Image)
        Me.INDpceContactData.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDpceContactData.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDpceContactData.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDpceContactData.Properties.AutoHeight = CType(resources.GetObject("INDpceContactData.Properties.AutoHeight"), Boolean)
        resources.ApplyResources(SerializableAppearanceObject1, "SerializableAppearanceObject1")
        SerializableAppearanceObject1.Options.UseFont = True
        Me.INDpceContactData.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDpceContactData.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines), resources.GetString("INDpceContactData.Properties.Buttons1"), CType(resources.GetObject("INDpceContactData.Properties.Buttons2"), Integer), CType(resources.GetObject("INDpceContactData.Properties.Buttons3"), Boolean), CType(resources.GetObject("INDpceContactData.Properties.Buttons4"), Boolean), CType(resources.GetObject("INDpceContactData.Properties.Buttons5"), Boolean), CType(resources.GetObject("INDpceContactData.Properties.Buttons6"), DevExpress.XtraEditors.ImageLocation), CType(resources.GetObject("INDpceContactData.Properties.Buttons7"), System.Drawing.Image), New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, resources.GetString("INDpceContactData.Properties.Buttons8"), CType(resources.GetObject("INDpceContactData.Properties.Buttons9"), Object), CType(resources.GetObject("INDpceContactData.Properties.Buttons10"), DevExpress.Utils.SuperToolTip), CType(resources.GetObject("INDpceContactData.Properties.Buttons11"), Boolean))})
        Me.INDpceContactData.Properties.Mask.AutoComplete = CType(resources.GetObject("INDpceContactData.Properties.Mask.AutoComplete"), DevExpress.XtraEditors.Mask.AutoCompleteType)
        Me.INDpceContactData.Properties.Mask.BeepOnError = CType(resources.GetObject("INDpceContactData.Properties.Mask.BeepOnError"), Boolean)
        Me.INDpceContactData.Properties.Mask.EditMask = resources.GetString("INDpceContactData.Properties.Mask.EditMask")
        Me.INDpceContactData.Properties.Mask.IgnoreMaskBlank = CType(resources.GetObject("INDpceContactData.Properties.Mask.IgnoreMaskBlank"), Boolean)
        Me.INDpceContactData.Properties.Mask.MaskType = CType(resources.GetObject("INDpceContactData.Properties.Mask.MaskType"), DevExpress.XtraEditors.Mask.MaskType)
        Me.INDpceContactData.Properties.Mask.PlaceHolder = CType(resources.GetObject("INDpceContactData.Properties.Mask.PlaceHolder"), Char)
        Me.INDpceContactData.Properties.Mask.SaveLiteral = CType(resources.GetObject("INDpceContactData.Properties.Mask.SaveLiteral"), Boolean)
        Me.INDpceContactData.Properties.Mask.ShowPlaceHolders = CType(resources.GetObject("INDpceContactData.Properties.Mask.ShowPlaceHolders"), Boolean)
        Me.INDpceContactData.Properties.Mask.UseMaskAsDisplayFormat = CType(resources.GetObject("INDpceContactData.Properties.Mask.UseMaskAsDisplayFormat"), Boolean)
        Me.INDpceContactData.Properties.NullValuePrompt = resources.GetString("INDpceContactData.Properties.NullValuePrompt")
        Me.INDpceContactData.Properties.NullValuePromptShowForEmptyValue = CType(resources.GetObject("INDpceContactData.Properties.NullValuePromptShowForEmptyValue"), Boolean)
        Me.INDpceContactData.Properties.PopupControl = Me.INDpccContactsControl
        Me.INDpceContactData.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        Me.INDpceContactData.StyleController = Me.INDlyInsurance
        '
        'INDglDepartamentos
        '
        Me.IndigoGridLookUpControl1.SetAbrirFormularioArchivo(Me.INDglDepartamentos, False)
        resources.ApplyResources(Me.INDglDepartamentos, "INDglDepartamentos")
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDglDepartamentos, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDglDepartamentos, True)
        Me.INDglDepartamentos.EnterMoveNextControl = True
        Me.IndigoGridLookUpControl1.SetGuardarXmlGrid(Me.INDglDepartamentos, False)
        Me.IndigoTextEdit1.SetMascara(Me.INDglDepartamentos, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDglDepartamentos.Name = "INDglDepartamentos"
        Me.INDglDepartamentos.Properties.AccessibleDescription = resources.GetString("INDglDepartamentos.Properties.AccessibleDescription")
        Me.INDglDepartamentos.Properties.AccessibleName = resources.GetString("INDglDepartamentos.Properties.AccessibleName")
        Me.INDglDepartamentos.Properties.Appearance.BackColor = CType(resources.GetObject("INDglDepartamentos.Properties.Appearance.BackColor"), System.Drawing.Color)
        Me.INDglDepartamentos.Properties.Appearance.Font = CType(resources.GetObject("INDglDepartamentos.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDglDepartamentos.Properties.Appearance.FontSizeDelta = CType(resources.GetObject("INDglDepartamentos.Properties.Appearance.FontSizeDelta"), Integer)
        Me.INDglDepartamentos.Properties.Appearance.FontStyleDelta = CType(resources.GetObject("INDglDepartamentos.Properties.Appearance.FontStyleDelta"), System.Drawing.FontStyle)
        Me.INDglDepartamentos.Properties.Appearance.GradientMode = CType(resources.GetObject("INDglDepartamentos.Properties.Appearance.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.INDglDepartamentos.Properties.Appearance.Image = CType(resources.GetObject("INDglDepartamentos.Properties.Appearance.Image"), System.Drawing.Image)
        Me.INDglDepartamentos.Properties.Appearance.Options.UseBackColor = True
        Me.INDglDepartamentos.Properties.Appearance.Options.UseFont = True
        Me.INDglDepartamentos.Properties.AppearanceFocused.BackColor = CType(resources.GetObject("INDglDepartamentos.Properties.AppearanceFocused.BackColor"), System.Drawing.Color)
        Me.INDglDepartamentos.Properties.AppearanceFocused.BorderColor = CType(resources.GetObject("INDglDepartamentos.Properties.AppearanceFocused.BorderColor"), System.Drawing.Color)
        Me.INDglDepartamentos.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDglDepartamentos.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDglDepartamentos.Properties.AppearanceFocused.FontSizeDelta = CType(resources.GetObject("INDglDepartamentos.Properties.AppearanceFocused.FontSizeDelta"), Integer)
        Me.INDglDepartamentos.Properties.AppearanceFocused.FontStyleDelta = CType(resources.GetObject("INDglDepartamentos.Properties.AppearanceFocused.FontStyleDelta"), System.Drawing.FontStyle)
        Me.INDglDepartamentos.Properties.AppearanceFocused.GradientMode = CType(resources.GetObject("INDglDepartamentos.Properties.AppearanceFocused.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.INDglDepartamentos.Properties.AppearanceFocused.Image = CType(resources.GetObject("INDglDepartamentos.Properties.AppearanceFocused.Image"), System.Drawing.Image)
        Me.INDglDepartamentos.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDglDepartamentos.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDglDepartamentos.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDglDepartamentos.Properties.AutoComplete = False
        Me.INDglDepartamentos.Properties.AutoHeight = CType(resources.GetObject("INDglDepartamentos.Properties.AutoHeight"), Boolean)
        Me.INDglDepartamentos.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDglDepartamentos.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.INDglDepartamentos.Properties.DisplayMember = "Name"
        Me.INDglDepartamentos.Properties.ImmediatePopup = True
        Me.INDglDepartamentos.Properties.NullText = resources.GetString("INDglDepartamentos.Properties.NullText")
        Me.INDglDepartamentos.Properties.NullValuePrompt = resources.GetString("INDglDepartamentos.Properties.NullValuePrompt")
        Me.INDglDepartamentos.Properties.NullValuePromptShowForEmptyValue = CType(resources.GetObject("INDglDepartamentos.Properties.NullValuePromptShowForEmptyValue"), Boolean)
        Me.INDglDepartamentos.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.INDglDepartamentos.Properties.ValueMember = "Id"
        Me.INDglDepartamentos.Properties.View = Me.CustomGridView1
        Me.INDglDepartamentos.StyleController = Me.INDlyInsurance
        Me.IndigoGridLookUpControl1.SetTagFormularioAbrir(Me.INDglDepartamentos, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDglDepartamentos, 0)
        '
        'CustomGridView1
        '
        Me.CustomGridView1.Appearance.FocusedRow.BorderColor = CType(resources.GetObject("CustomGridView1.Appearance.FocusedRow.BorderColor"), System.Drawing.Color)
        Me.CustomGridView1.Appearance.FocusedRow.Font = CType(resources.GetObject("CustomGridView1.Appearance.FocusedRow.Font"), System.Drawing.Font)
        Me.CustomGridView1.Appearance.FocusedRow.FontSizeDelta = CType(resources.GetObject("CustomGridView1.Appearance.FocusedRow.FontSizeDelta"), Integer)
        Me.CustomGridView1.Appearance.FocusedRow.FontStyleDelta = CType(resources.GetObject("CustomGridView1.Appearance.FocusedRow.FontStyleDelta"), System.Drawing.FontStyle)
        Me.CustomGridView1.Appearance.FocusedRow.GradientMode = CType(resources.GetObject("CustomGridView1.Appearance.FocusedRow.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.CustomGridView1.Appearance.FocusedRow.Image = CType(resources.GetObject("CustomGridView1.Appearance.FocusedRow.Image"), System.Drawing.Image)
        Me.CustomGridView1.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.CustomGridView1.Appearance.FocusedRow.Options.UseFont = True
        Me.CustomGridView1.Appearance.GroupRow.Font = CType(resources.GetObject("CustomGridView1.Appearance.GroupRow.Font"), System.Drawing.Font)
        Me.CustomGridView1.Appearance.GroupRow.FontSizeDelta = CType(resources.GetObject("CustomGridView1.Appearance.GroupRow.FontSizeDelta"), Integer)
        Me.CustomGridView1.Appearance.GroupRow.FontStyleDelta = CType(resources.GetObject("CustomGridView1.Appearance.GroupRow.FontStyleDelta"), System.Drawing.FontStyle)
        Me.CustomGridView1.Appearance.GroupRow.GradientMode = CType(resources.GetObject("CustomGridView1.Appearance.GroupRow.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.CustomGridView1.Appearance.GroupRow.Image = CType(resources.GetObject("CustomGridView1.Appearance.GroupRow.Image"), System.Drawing.Image)
        Me.CustomGridView1.Appearance.GroupRow.Options.UseFont = True
        Me.CustomGridView1.Appearance.HeaderPanel.Font = CType(resources.GetObject("CustomGridView1.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.CustomGridView1.Appearance.HeaderPanel.FontSizeDelta = CType(resources.GetObject("CustomGridView1.Appearance.HeaderPanel.FontSizeDelta"), Integer)
        Me.CustomGridView1.Appearance.HeaderPanel.FontStyleDelta = CType(resources.GetObject("CustomGridView1.Appearance.HeaderPanel.FontStyleDelta"), System.Drawing.FontStyle)
        Me.CustomGridView1.Appearance.HeaderPanel.GradientMode = CType(resources.GetObject("CustomGridView1.Appearance.HeaderPanel.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.CustomGridView1.Appearance.HeaderPanel.Image = CType(resources.GetObject("CustomGridView1.Appearance.HeaderPanel.Image"), System.Drawing.Image)
        Me.CustomGridView1.Appearance.HeaderPanel.Options.UseFont = True
        Me.CustomGridView1.Appearance.Row.Font = CType(resources.GetObject("CustomGridView1.Appearance.Row.Font"), System.Drawing.Font)
        Me.CustomGridView1.Appearance.Row.FontSizeDelta = CType(resources.GetObject("CustomGridView1.Appearance.Row.FontSizeDelta"), Integer)
        Me.CustomGridView1.Appearance.Row.FontStyleDelta = CType(resources.GetObject("CustomGridView1.Appearance.Row.FontStyleDelta"), System.Drawing.FontStyle)
        Me.CustomGridView1.Appearance.Row.GradientMode = CType(resources.GetObject("CustomGridView1.Appearance.Row.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.CustomGridView1.Appearance.Row.Image = CType(resources.GetObject("CustomGridView1.Appearance.Row.Image"), System.Drawing.Image)
        Me.CustomGridView1.Appearance.Row.Options.UseFont = True
        resources.ApplyResources(Me.CustomGridView1, "CustomGridView1")
        Me.CustomGridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2})
        Me.CustomGridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.CustomGridView1.Name = "CustomGridView1"
        Me.CustomGridView1.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.CustomGridView1.OptionsView.EnableAppearanceEvenRow = True
        Me.CustomGridView1.OptionsView.EnableAppearanceOddRow = True
        Me.CustomGridView1.OptionsView.ShowAutoFilterRow = True
        Me.CustomGridView1.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.CustomGridView1, False)
        '
        'GridColumn1
        '
        resources.ApplyResources(Me.GridColumn1, "GridColumn1")
        Me.GridColumn1.FieldName = "Code"
        Me.GridColumn1.Name = "GridColumn1"
        '
        'GridColumn2
        '
        resources.ApplyResources(Me.GridColumn2, "GridColumn2")
        Me.GridColumn2.FieldName = "Name"
        Me.GridColumn2.Name = "GridColumn2"
        '
        'INDglCity
        '
        Me.IndigoGridLookUpControl1.SetAbrirFormularioArchivo(Me.INDglCity, False)
        resources.ApplyResources(Me.INDglCity, "INDglCity")
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDglCity, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDglCity, True)
        Me.INDglCity.EnterMoveNextControl = True
        Me.IndigoGridLookUpControl1.SetGuardarXmlGrid(Me.INDglCity, False)
        Me.IndigoTextEdit1.SetMascara(Me.INDglCity, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDglCity.Name = "INDglCity"
        Me.INDglCity.Properties.AccessibleDescription = resources.GetString("INDglCity.Properties.AccessibleDescription")
        Me.INDglCity.Properties.AccessibleName = resources.GetString("INDglCity.Properties.AccessibleName")
        Me.INDglCity.Properties.Appearance.BackColor = CType(resources.GetObject("INDglCity.Properties.Appearance.BackColor"), System.Drawing.Color)
        Me.INDglCity.Properties.Appearance.Font = CType(resources.GetObject("INDglCity.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDglCity.Properties.Appearance.FontSizeDelta = CType(resources.GetObject("INDglCity.Properties.Appearance.FontSizeDelta"), Integer)
        Me.INDglCity.Properties.Appearance.FontStyleDelta = CType(resources.GetObject("INDglCity.Properties.Appearance.FontStyleDelta"), System.Drawing.FontStyle)
        Me.INDglCity.Properties.Appearance.GradientMode = CType(resources.GetObject("INDglCity.Properties.Appearance.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.INDglCity.Properties.Appearance.Image = CType(resources.GetObject("INDglCity.Properties.Appearance.Image"), System.Drawing.Image)
        Me.INDglCity.Properties.Appearance.Options.UseBackColor = True
        Me.INDglCity.Properties.Appearance.Options.UseFont = True
        Me.INDglCity.Properties.AppearanceFocused.BackColor = CType(resources.GetObject("INDglCity.Properties.AppearanceFocused.BackColor"), System.Drawing.Color)
        Me.INDglCity.Properties.AppearanceFocused.BorderColor = CType(resources.GetObject("INDglCity.Properties.AppearanceFocused.BorderColor"), System.Drawing.Color)
        Me.INDglCity.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDglCity.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDglCity.Properties.AppearanceFocused.FontSizeDelta = CType(resources.GetObject("INDglCity.Properties.AppearanceFocused.FontSizeDelta"), Integer)
        Me.INDglCity.Properties.AppearanceFocused.FontStyleDelta = CType(resources.GetObject("INDglCity.Properties.AppearanceFocused.FontStyleDelta"), System.Drawing.FontStyle)
        Me.INDglCity.Properties.AppearanceFocused.GradientMode = CType(resources.GetObject("INDglCity.Properties.AppearanceFocused.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.INDglCity.Properties.AppearanceFocused.Image = CType(resources.GetObject("INDglCity.Properties.AppearanceFocused.Image"), System.Drawing.Image)
        Me.INDglCity.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDglCity.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDglCity.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDglCity.Properties.AutoComplete = False
        Me.INDglCity.Properties.AutoHeight = CType(resources.GetObject("INDglCity.Properties.AutoHeight"), Boolean)
        Me.INDglCity.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDglCity.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.INDglCity.Properties.DisplayMember = "Name"
        Me.INDglCity.Properties.ImmediatePopup = True
        Me.INDglCity.Properties.NullText = resources.GetString("INDglCity.Properties.NullText")
        Me.INDglCity.Properties.NullValuePrompt = resources.GetString("INDglCity.Properties.NullValuePrompt")
        Me.INDglCity.Properties.NullValuePromptShowForEmptyValue = CType(resources.GetObject("INDglCity.Properties.NullValuePromptShowForEmptyValue"), Boolean)
        Me.INDglCity.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.INDglCity.Properties.ValueMember = "Id"
        Me.INDglCity.Properties.View = Me.GridLookUpMultiFilter2View
        Me.INDglCity.StyleController = Me.INDlyInsurance
        Me.IndigoGridLookUpControl1.SetTagFormularioAbrir(Me.INDglCity, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDglCity, 0)
        '
        'GridLookUpMultiFilter2View
        '
        Me.GridLookUpMultiFilter2View.Appearance.FocusedRow.BorderColor = CType(resources.GetObject("GridLookUpMultiFilter2View.Appearance.FocusedRow.BorderColor"), System.Drawing.Color)
        Me.GridLookUpMultiFilter2View.Appearance.FocusedRow.Font = CType(resources.GetObject("GridLookUpMultiFilter2View.Appearance.FocusedRow.Font"), System.Drawing.Font)
        Me.GridLookUpMultiFilter2View.Appearance.FocusedRow.FontSizeDelta = CType(resources.GetObject("GridLookUpMultiFilter2View.Appearance.FocusedRow.FontSizeDelta"), Integer)
        Me.GridLookUpMultiFilter2View.Appearance.FocusedRow.FontStyleDelta = CType(resources.GetObject("GridLookUpMultiFilter2View.Appearance.FocusedRow.FontStyleDelta"), System.Drawing.FontStyle)
        Me.GridLookUpMultiFilter2View.Appearance.FocusedRow.GradientMode = CType(resources.GetObject("GridLookUpMultiFilter2View.Appearance.FocusedRow.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.GridLookUpMultiFilter2View.Appearance.FocusedRow.Image = CType(resources.GetObject("GridLookUpMultiFilter2View.Appearance.FocusedRow.Image"), System.Drawing.Image)
        Me.GridLookUpMultiFilter2View.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridLookUpMultiFilter2View.Appearance.FocusedRow.Options.UseFont = True
        Me.GridLookUpMultiFilter2View.Appearance.GroupRow.Font = CType(resources.GetObject("GridLookUpMultiFilter2View.Appearance.GroupRow.Font"), System.Drawing.Font)
        Me.GridLookUpMultiFilter2View.Appearance.GroupRow.FontSizeDelta = CType(resources.GetObject("GridLookUpMultiFilter2View.Appearance.GroupRow.FontSizeDelta"), Integer)
        Me.GridLookUpMultiFilter2View.Appearance.GroupRow.FontStyleDelta = CType(resources.GetObject("GridLookUpMultiFilter2View.Appearance.GroupRow.FontStyleDelta"), System.Drawing.FontStyle)
        Me.GridLookUpMultiFilter2View.Appearance.GroupRow.GradientMode = CType(resources.GetObject("GridLookUpMultiFilter2View.Appearance.GroupRow.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.GridLookUpMultiFilter2View.Appearance.GroupRow.Image = CType(resources.GetObject("GridLookUpMultiFilter2View.Appearance.GroupRow.Image"), System.Drawing.Image)
        Me.GridLookUpMultiFilter2View.Appearance.GroupRow.Options.UseFont = True
        Me.GridLookUpMultiFilter2View.Appearance.HeaderPanel.Font = CType(resources.GetObject("GridLookUpMultiFilter2View.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.GridLookUpMultiFilter2View.Appearance.HeaderPanel.FontSizeDelta = CType(resources.GetObject("GridLookUpMultiFilter2View.Appearance.HeaderPanel.FontSizeDelta"), Integer)
        Me.GridLookUpMultiFilter2View.Appearance.HeaderPanel.FontStyleDelta = CType(resources.GetObject("GridLookUpMultiFilter2View.Appearance.HeaderPanel.FontStyleDelta"), System.Drawing.FontStyle)
        Me.GridLookUpMultiFilter2View.Appearance.HeaderPanel.GradientMode = CType(resources.GetObject("GridLookUpMultiFilter2View.Appearance.HeaderPanel.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.GridLookUpMultiFilter2View.Appearance.HeaderPanel.Image = CType(resources.GetObject("GridLookUpMultiFilter2View.Appearance.HeaderPanel.Image"), System.Drawing.Image)
        Me.GridLookUpMultiFilter2View.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridLookUpMultiFilter2View.Appearance.Row.Font = CType(resources.GetObject("GridLookUpMultiFilter2View.Appearance.Row.Font"), System.Drawing.Font)
        Me.GridLookUpMultiFilter2View.Appearance.Row.FontSizeDelta = CType(resources.GetObject("GridLookUpMultiFilter2View.Appearance.Row.FontSizeDelta"), Integer)
        Me.GridLookUpMultiFilter2View.Appearance.Row.FontStyleDelta = CType(resources.GetObject("GridLookUpMultiFilter2View.Appearance.Row.FontStyleDelta"), System.Drawing.FontStyle)
        Me.GridLookUpMultiFilter2View.Appearance.Row.GradientMode = CType(resources.GetObject("GridLookUpMultiFilter2View.Appearance.Row.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.GridLookUpMultiFilter2View.Appearance.Row.Image = CType(resources.GetObject("GridLookUpMultiFilter2View.Appearance.Row.Image"), System.Drawing.Image)
        Me.GridLookUpMultiFilter2View.Appearance.Row.Options.UseFont = True
        resources.ApplyResources(Me.GridLookUpMultiFilter2View, "GridLookUpMultiFilter2View")
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
        'INDtxtWebSite
        '
        resources.ApplyResources(Me.INDtxtWebSite, "INDtxtWebSite")
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtWebSite, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtWebSite, True)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtWebSite, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtWebSite.Name = "INDtxtWebSite"
        Me.INDtxtWebSite.Properties.AccessibleDescription = resources.GetString("INDtxtWebSite.Properties.AccessibleDescription")
        Me.INDtxtWebSite.Properties.AccessibleName = resources.GetString("INDtxtWebSite.Properties.AccessibleName")
        Me.INDtxtWebSite.Properties.Appearance.BackColor = CType(resources.GetObject("INDtxtWebSite.Properties.Appearance.BackColor"), System.Drawing.Color)
        Me.INDtxtWebSite.Properties.Appearance.Font = CType(resources.GetObject("INDtxtWebSite.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDtxtWebSite.Properties.Appearance.FontSizeDelta = CType(resources.GetObject("INDtxtWebSite.Properties.Appearance.FontSizeDelta"), Integer)
        Me.INDtxtWebSite.Properties.Appearance.FontStyleDelta = CType(resources.GetObject("INDtxtWebSite.Properties.Appearance.FontStyleDelta"), System.Drawing.FontStyle)
        Me.INDtxtWebSite.Properties.Appearance.GradientMode = CType(resources.GetObject("INDtxtWebSite.Properties.Appearance.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.INDtxtWebSite.Properties.Appearance.Image = CType(resources.GetObject("INDtxtWebSite.Properties.Appearance.Image"), System.Drawing.Image)
        Me.INDtxtWebSite.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtWebSite.Properties.Appearance.Options.UseFont = True
        Me.INDtxtWebSite.Properties.AppearanceFocused.BackColor = CType(resources.GetObject("INDtxtWebSite.Properties.AppearanceFocused.BackColor"), System.Drawing.Color)
        Me.INDtxtWebSite.Properties.AppearanceFocused.BorderColor = CType(resources.GetObject("INDtxtWebSite.Properties.AppearanceFocused.BorderColor"), System.Drawing.Color)
        Me.INDtxtWebSite.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDtxtWebSite.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDtxtWebSite.Properties.AppearanceFocused.FontSizeDelta = CType(resources.GetObject("INDtxtWebSite.Properties.AppearanceFocused.FontSizeDelta"), Integer)
        Me.INDtxtWebSite.Properties.AppearanceFocused.FontStyleDelta = CType(resources.GetObject("INDtxtWebSite.Properties.AppearanceFocused.FontStyleDelta"), System.Drawing.FontStyle)
        Me.INDtxtWebSite.Properties.AppearanceFocused.GradientMode = CType(resources.GetObject("INDtxtWebSite.Properties.AppearanceFocused.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.INDtxtWebSite.Properties.AppearanceFocused.Image = CType(resources.GetObject("INDtxtWebSite.Properties.AppearanceFocused.Image"), System.Drawing.Image)
        Me.INDtxtWebSite.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtWebSite.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtxtWebSite.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtWebSite.Properties.AutoHeight = CType(resources.GetObject("INDtxtWebSite.Properties.AutoHeight"), Boolean)
        Me.INDtxtWebSite.Properties.Mask.AutoComplete = CType(resources.GetObject("INDtxtWebSite.Properties.Mask.AutoComplete"), DevExpress.XtraEditors.Mask.AutoCompleteType)
        Me.INDtxtWebSite.Properties.Mask.BeepOnError = CType(resources.GetObject("INDtxtWebSite.Properties.Mask.BeepOnError"), Boolean)
        Me.INDtxtWebSite.Properties.Mask.EditMask = resources.GetString("INDtxtWebSite.Properties.Mask.EditMask")
        Me.INDtxtWebSite.Properties.Mask.IgnoreMaskBlank = CType(resources.GetObject("INDtxtWebSite.Properties.Mask.IgnoreMaskBlank"), Boolean)
        Me.INDtxtWebSite.Properties.Mask.MaskType = CType(resources.GetObject("INDtxtWebSite.Properties.Mask.MaskType"), DevExpress.XtraEditors.Mask.MaskType)
        Me.INDtxtWebSite.Properties.Mask.PlaceHolder = CType(resources.GetObject("INDtxtWebSite.Properties.Mask.PlaceHolder"), Char)
        Me.INDtxtWebSite.Properties.Mask.SaveLiteral = CType(resources.GetObject("INDtxtWebSite.Properties.Mask.SaveLiteral"), Boolean)
        Me.INDtxtWebSite.Properties.Mask.ShowPlaceHolders = CType(resources.GetObject("INDtxtWebSite.Properties.Mask.ShowPlaceHolders"), Boolean)
        Me.INDtxtWebSite.Properties.Mask.UseMaskAsDisplayFormat = CType(resources.GetObject("INDtxtWebSite.Properties.Mask.UseMaskAsDisplayFormat"), Boolean)
        Me.INDtxtWebSite.Properties.MaxLength = 80
        Me.INDtxtWebSite.Properties.NullValuePrompt = resources.GetString("INDtxtWebSite.Properties.NullValuePrompt")
        Me.INDtxtWebSite.Properties.NullValuePromptShowForEmptyValue = CType(resources.GetObject("INDtxtWebSite.Properties.NullValuePromptShowForEmptyValue"), Boolean)
        Me.INDtxtWebSite.StyleController = Me.INDlyInsurance
        Me.INDtxtWebSite.Tag = ""
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtWebSite, 0)
        '
        'INDtxtName
        '
        resources.ApplyResources(Me.INDtxtName, "INDtxtName")
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtName, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtName, True)
        Me.INDtxtName.EnterMoveNextControl = True
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtName, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtName.Name = "INDtxtName"
        Me.INDtxtName.Properties.AccessibleDescription = resources.GetString("INDtxtName.Properties.AccessibleDescription")
        Me.INDtxtName.Properties.AccessibleName = resources.GetString("INDtxtName.Properties.AccessibleName")
        Me.INDtxtName.Properties.Appearance.BackColor = CType(resources.GetObject("INDtxtName.Properties.Appearance.BackColor"), System.Drawing.Color)
        Me.INDtxtName.Properties.Appearance.Font = CType(resources.GetObject("INDtxtName.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDtxtName.Properties.Appearance.FontSizeDelta = CType(resources.GetObject("INDtxtName.Properties.Appearance.FontSizeDelta"), Integer)
        Me.INDtxtName.Properties.Appearance.FontStyleDelta = CType(resources.GetObject("INDtxtName.Properties.Appearance.FontStyleDelta"), System.Drawing.FontStyle)
        Me.INDtxtName.Properties.Appearance.GradientMode = CType(resources.GetObject("INDtxtName.Properties.Appearance.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.INDtxtName.Properties.Appearance.Image = CType(resources.GetObject("INDtxtName.Properties.Appearance.Image"), System.Drawing.Image)
        Me.INDtxtName.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtName.Properties.Appearance.Options.UseFont = True
        Me.INDtxtName.Properties.AppearanceFocused.BackColor = CType(resources.GetObject("INDtxtName.Properties.AppearanceFocused.BackColor"), System.Drawing.Color)
        Me.INDtxtName.Properties.AppearanceFocused.BorderColor = CType(resources.GetObject("INDtxtName.Properties.AppearanceFocused.BorderColor"), System.Drawing.Color)
        Me.INDtxtName.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDtxtName.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDtxtName.Properties.AppearanceFocused.FontSizeDelta = CType(resources.GetObject("INDtxtName.Properties.AppearanceFocused.FontSizeDelta"), Integer)
        Me.INDtxtName.Properties.AppearanceFocused.FontStyleDelta = CType(resources.GetObject("INDtxtName.Properties.AppearanceFocused.FontStyleDelta"), System.Drawing.FontStyle)
        Me.INDtxtName.Properties.AppearanceFocused.GradientMode = CType(resources.GetObject("INDtxtName.Properties.AppearanceFocused.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.INDtxtName.Properties.AppearanceFocused.Image = CType(resources.GetObject("INDtxtName.Properties.AppearanceFocused.Image"), System.Drawing.Image)
        Me.INDtxtName.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtName.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtxtName.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtName.Properties.AutoHeight = CType(resources.GetObject("INDtxtName.Properties.AutoHeight"), Boolean)
        Me.INDtxtName.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.INDtxtName.Properties.Mask.AutoComplete = CType(resources.GetObject("INDtxtName.Properties.Mask.AutoComplete"), DevExpress.XtraEditors.Mask.AutoCompleteType)
        Me.INDtxtName.Properties.Mask.BeepOnError = CType(resources.GetObject("INDtxtName.Properties.Mask.BeepOnError"), Boolean)
        Me.INDtxtName.Properties.Mask.EditMask = resources.GetString("INDtxtName.Properties.Mask.EditMask")
        Me.INDtxtName.Properties.Mask.IgnoreMaskBlank = CType(resources.GetObject("INDtxtName.Properties.Mask.IgnoreMaskBlank"), Boolean)
        Me.INDtxtName.Properties.Mask.MaskType = CType(resources.GetObject("INDtxtName.Properties.Mask.MaskType"), DevExpress.XtraEditors.Mask.MaskType)
        Me.INDtxtName.Properties.Mask.PlaceHolder = CType(resources.GetObject("INDtxtName.Properties.Mask.PlaceHolder"), Char)
        Me.INDtxtName.Properties.Mask.SaveLiteral = CType(resources.GetObject("INDtxtName.Properties.Mask.SaveLiteral"), Boolean)
        Me.INDtxtName.Properties.Mask.ShowPlaceHolders = CType(resources.GetObject("INDtxtName.Properties.Mask.ShowPlaceHolders"), Boolean)
        Me.INDtxtName.Properties.Mask.UseMaskAsDisplayFormat = CType(resources.GetObject("INDtxtName.Properties.Mask.UseMaskAsDisplayFormat"), Boolean)
        Me.INDtxtName.Properties.MaxLength = 60
        Me.INDtxtName.Properties.NullValuePrompt = resources.GetString("INDtxtName.Properties.NullValuePrompt")
        Me.INDtxtName.Properties.NullValuePromptShowForEmptyValue = CType(resources.GetObject("INDtxtName.Properties.NullValuePromptShowForEmptyValue"), Boolean)
        Me.INDtxtName.StyleController = Me.INDlyInsurance
        Me.INDtxtName.Tag = ""
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtName, 0)
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.AppearanceGroup.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceGroup.Font"), System.Drawing.Font)
        Me.LayoutControlGroup1.AppearanceGroup.FontSizeDelta = CType(resources.GetObject("LayoutControlGroup1.AppearanceGroup.FontSizeDelta"), Integer)
        Me.LayoutControlGroup1.AppearanceGroup.FontStyleDelta = CType(resources.GetObject("LayoutControlGroup1.AppearanceGroup.FontStyleDelta"), System.Drawing.FontStyle)
        Me.LayoutControlGroup1.AppearanceGroup.GradientMode = CType(resources.GetObject("LayoutControlGroup1.AppearanceGroup.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.LayoutControlGroup1.AppearanceGroup.Image = CType(resources.GetObject("LayoutControlGroup1.AppearanceGroup.Image"), System.Drawing.Image)
        Me.LayoutControlGroup1.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceItemCaption.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LayoutControlGroup1.AppearanceItemCaption.FontSizeDelta = CType(resources.GetObject("LayoutControlGroup1.AppearanceItemCaption.FontSizeDelta"), Integer)
        Me.LayoutControlGroup1.AppearanceItemCaption.FontStyleDelta = CType(resources.GetObject("LayoutControlGroup1.AppearanceItemCaption.FontStyleDelta"), System.Drawing.FontStyle)
        Me.LayoutControlGroup1.AppearanceItemCaption.GradientMode = CType(resources.GetObject("LayoutControlGroup1.AppearanceItemCaption.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.LayoutControlGroup1.AppearanceItemCaption.Image = CType(resources.GetObject("LayoutControlGroup1.AppearanceItemCaption.Image"), System.Drawing.Image)
        Me.LayoutControlGroup1.AppearanceItemCaption.Options.UseFont = True
        resources.ApplyResources(Me.LayoutControlGroup1, "LayoutControlGroup1")
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyGrResponsibles})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(804, 598)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDlyGrResponsibles
        '
        resources.ApplyResources(Me.INDlyGrResponsibles, "INDlyGrResponsibles")
        Me.INDlyGrResponsibles.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemCode, Me.INDlyItemName, Me.INDlyItemWebSite, Me.INDlyItemCity, Me.INDlyItemDepartamento, Me.INDlyItemContacts})
        Me.INDlyGrResponsibles.Location = New System.Drawing.Point(0, 0)
        Me.INDlyGrResponsibles.Name = "INDlyGrResponsibles"
        Me.INDlyGrResponsibles.Size = New System.Drawing.Size(804, 598)
        '
        'INDlyItemCode
        '
        Me.INDlyItemCode.AllowHide = False
        resources.ApplyResources(Me.INDlyItemCode, "INDlyItemCode")
        Me.INDlyItemCode.Control = Me.INDbteCode
        Me.INDlyItemCode.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemCode.MaxSize = New System.Drawing.Size(420, 36)
        Me.INDlyItemCode.MinSize = New System.Drawing.Size(420, 36)
        Me.INDlyItemCode.Name = "INDlyItemCode"
        Me.INDlyItemCode.ShowInCustomizationForm = False
        Me.INDlyItemCode.Size = New System.Drawing.Size(780, 36)
        Me.INDlyItemCode.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemCode.Tag = "Code"
        Me.INDlyItemCode.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemCode.TextSize = New System.Drawing.Size(160, 21)
        Me.INDlyItemCode.TextToControlDistance = 12
        '
        'INDlyItemName
        '
        Me.INDlyItemName.AllowHide = False
        resources.ApplyResources(Me.INDlyItemName, "INDlyItemName")
        Me.INDlyItemName.Control = Me.INDtxtName
        Me.INDlyItemName.Location = New System.Drawing.Point(0, 36)
        Me.INDlyItemName.MaxSize = New System.Drawing.Size(420, 36)
        Me.INDlyItemName.MinSize = New System.Drawing.Size(420, 36)
        Me.INDlyItemName.Name = "INDlyItemName"
        Me.INDlyItemName.Padding = New DevExpress.XtraLayout.Utils.Padding(2, 3, 2, 2)
        Me.INDlyItemName.ShowInCustomizationForm = False
        Me.INDlyItemName.Size = New System.Drawing.Size(780, 36)
        Me.INDlyItemName.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemName.Tag = "Name"
        Me.INDlyItemName.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemName.TextSize = New System.Drawing.Size(160, 21)
        Me.INDlyItemName.TextToControlDistance = 12
        '
        'INDlyItemWebSite
        '
        resources.ApplyResources(Me.INDlyItemWebSite, "INDlyItemWebSite")
        Me.INDlyItemWebSite.Control = Me.INDtxtWebSite
        Me.INDlyItemWebSite.Location = New System.Drawing.Point(0, 108)
        Me.INDlyItemWebSite.MaxSize = New System.Drawing.Size(420, 36)
        Me.INDlyItemWebSite.MinSize = New System.Drawing.Size(420, 36)
        Me.INDlyItemWebSite.Name = "INDlyItemWebSite"
        Me.INDlyItemWebSite.Padding = New DevExpress.XtraLayout.Utils.Padding(2, 3, 2, 2)
        Me.INDlyItemWebSite.ShowInCustomizationForm = False
        Me.INDlyItemWebSite.Size = New System.Drawing.Size(780, 36)
        Me.INDlyItemWebSite.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemWebSite.Tag = "WebSite"
        Me.INDlyItemWebSite.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemWebSite.TextSize = New System.Drawing.Size(160, 21)
        Me.INDlyItemWebSite.TextToControlDistance = 12
        '
        'INDlyItemCity
        '
        resources.ApplyResources(Me.INDlyItemCity, "INDlyItemCity")
        Me.INDlyItemCity.Control = Me.INDglCity
        Me.INDlyItemCity.Location = New System.Drawing.Point(0, 180)
        Me.INDlyItemCity.MaxSize = New System.Drawing.Size(420, 36)
        Me.INDlyItemCity.MinSize = New System.Drawing.Size(420, 36)
        Me.INDlyItemCity.Name = "INDlyItemCity"
        Me.INDlyItemCity.ShowInCustomizationForm = False
        Me.INDlyItemCity.Size = New System.Drawing.Size(780, 359)
        Me.INDlyItemCity.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemCity.Tag = "City"
        Me.INDlyItemCity.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemCity.TextSize = New System.Drawing.Size(160, 21)
        Me.INDlyItemCity.TextToControlDistance = 12
        '
        'INDlyItemDepartamento
        '
        resources.ApplyResources(Me.INDlyItemDepartamento, "INDlyItemDepartamento")
        Me.INDlyItemDepartamento.Control = Me.INDglDepartamentos
        Me.INDlyItemDepartamento.Location = New System.Drawing.Point(0, 144)
        Me.INDlyItemDepartamento.MaxSize = New System.Drawing.Size(420, 36)
        Me.INDlyItemDepartamento.MinSize = New System.Drawing.Size(420, 36)
        Me.INDlyItemDepartamento.Name = "INDlyItemDepartamento"
        Me.INDlyItemDepartamento.ShowInCustomizationForm = False
        Me.INDlyItemDepartamento.Size = New System.Drawing.Size(780, 36)
        Me.INDlyItemDepartamento.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemDepartamento.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemDepartamento.TextSize = New System.Drawing.Size(160, 21)
        Me.INDlyItemDepartamento.TextToControlDistance = 12
        '
        'INDlyItemContacts
        '
        resources.ApplyResources(Me.INDlyItemContacts, "INDlyItemContacts")
        Me.INDlyItemContacts.Control = Me.INDpceContactData
        Me.INDlyItemContacts.Location = New System.Drawing.Point(0, 72)
        Me.INDlyItemContacts.MaxSize = New System.Drawing.Size(420, 36)
        Me.INDlyItemContacts.MinSize = New System.Drawing.Size(420, 36)
        Me.INDlyItemContacts.Name = "INDlyItemContacts"
        Me.INDlyItemContacts.Size = New System.Drawing.Size(780, 36)
        Me.INDlyItemContacts.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemContacts.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemContacts.TextSize = New System.Drawing.Size(160, 21)
        Me.INDlyItemContacts.TextToControlDistance = 12
        '
        'CtrNavigationControl1
        '
        resources.ApplyResources(Me.CtrNavigationControl1, "CtrNavigationControl1")
        Me.CtrNavigationControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControl1.LayoutControl = Me.INDlyInsurance
        Me.CtrNavigationControl1.Name = "CtrNavigationControl1"
        Me.CtrNavigationControl1.UseDisabledStatePainter = False
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        '
        'FrmInsurance
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Name = "FrmInsurance"
        Me.Opacity = 1.0R
        Me.Tag = "557"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDbteCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyInsurance, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyInsurance.ResumeLayout(False)
        CType(Me.INDpccContactsControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDpccContactsControl.ResumeLayout(False)
        CType(Me.INDpceContactData.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDglDepartamentos.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CustomGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDglCity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridLookUpMultiFilter2View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtWebSite.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyGrResponsibles, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemWebSite, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemCity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemDepartamento, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemContacts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents INDlyInsurance As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDtxtName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDbteCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemCode As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemName As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyGrResponsibles As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDtxtWebSite As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDlyItemWebSite As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDglCity As Presentation.Controls.GridLookUpMultiFilter
    Friend WithEvents GridLookUpMultiFilter2View As Presentation.Controls.CustomGridView
    Friend WithEvents DepartmentCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents DepartmentName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoGridLookUpControl1 As Presentation.Controls.IndigoGridLookUpControl
    Friend WithEvents INDlyItemCity As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDglDepartamentos As Presentation.Controls.GridLookUpMultiFilter
    Friend WithEvents CustomGridView1 As Presentation.Controls.CustomGridView
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDlyItemDepartamento As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDpccContactsControl As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents CtrContacts As Presentation.Controls.CtrContactos
    Friend WithEvents INDpceContactData As DevExpress.XtraEditors.PopupContainerEdit
    Friend WithEvents INDlyItemContacts As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents CtrNavigationControl1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
End Class
