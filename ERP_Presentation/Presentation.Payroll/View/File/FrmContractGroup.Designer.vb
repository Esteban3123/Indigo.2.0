Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmContractGroup
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmContractGroup))
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.INDlyCtrContractGroup = New DevExpress.XtraLayout.LayoutControl()
        Me.INDtxtDescription = New DevExpress.XtraEditors.TextEdit()
        Me.INDtxtName = New DevExpress.XtraEditors.TextEdit()
        Me.INDbteCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.INDlyContractGroup = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.INDlyGrContractGroup = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemName = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemDescription = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyCtrContractGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyCtrContractGroup.SuspendLayout()
        CType(Me.INDtxtDescription.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDbteCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyContractGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyGrContractGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        resources.ApplyResources(Me.INDPanelControlBase, "INDPanelControlBase")
        Me.INDPanelControlBase.Controls.Add(Me.INDlyCtrContractGroup)
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
        'INDlyCtrContractGroup
        '
        resources.ApplyResources(Me.INDlyCtrContractGroup, "INDlyCtrContractGroup")
        Me.INDlyCtrContractGroup.AllowCustomizationMenu = False
        Me.INDlyCtrContractGroup.Appearance.DisabledLayoutGroupCaption.FontSizeDelta = CType(resources.GetObject("INDlyCtrContractGroup.Appearance.DisabledLayoutGroupCaption.FontSizeDelta"), Integer)
        Me.INDlyCtrContractGroup.Appearance.DisabledLayoutGroupCaption.FontStyleDelta = CType(resources.GetObject("INDlyCtrContractGroup.Appearance.DisabledLayoutGroupCaption.FontStyleDelta"), System.Drawing.FontStyle)
        Me.INDlyCtrContractGroup.Appearance.DisabledLayoutGroupCaption.GradientMode = CType(resources.GetObject("INDlyCtrContractGroup.Appearance.DisabledLayoutGroupCaption.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.INDlyCtrContractGroup.Appearance.DisabledLayoutGroupCaption.Image = CType(resources.GetObject("INDlyCtrContractGroup.Appearance.DisabledLayoutGroupCaption.Image"), System.Drawing.Image)
        Me.INDlyCtrContractGroup.Appearance.DisabledLayoutItem.FontSizeDelta = CType(resources.GetObject("INDlyCtrContractGroup.Appearance.DisabledLayoutItem.FontSizeDelta"), Integer)
        Me.INDlyCtrContractGroup.Appearance.DisabledLayoutItem.FontStyleDelta = CType(resources.GetObject("INDlyCtrContractGroup.Appearance.DisabledLayoutItem.FontStyleDelta"), System.Drawing.FontStyle)
        Me.INDlyCtrContractGroup.Appearance.DisabledLayoutItem.GradientMode = CType(resources.GetObject("INDlyCtrContractGroup.Appearance.DisabledLayoutItem.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.INDlyCtrContractGroup.Appearance.DisabledLayoutItem.Image = CType(resources.GetObject("INDlyCtrContractGroup.Appearance.DisabledLayoutItem.Image"), System.Drawing.Image)
        Me.INDlyCtrContractGroup.Controls.Add(Me.INDtxtDescription)
        Me.INDlyCtrContractGroup.Controls.Add(Me.INDtxtName)
        Me.INDlyCtrContractGroup.Controls.Add(Me.INDbteCode)
        Me.LayoutControls.SetIsCustomizable(Me.INDlyCtrContractGroup, False)
        Me.INDlyCtrContractGroup.Name = "INDlyCtrContractGroup"
        Me.INDlyCtrContractGroup.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(487, 262, 250, 350)
        Me.INDlyCtrContractGroup.Root = Me.INDlyContractGroup
        '
        'INDtxtDescription
        '
        resources.ApplyResources(Me.INDtxtDescription, "INDtxtDescription")
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtDescription, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtDescription, True)
        Me.INDtxtDescription.EnterMoveNextControl = True
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtDescription, Presentation.Controls.IndigoTextEdit.EMask.AlfaNumerico)
        Me.INDtxtDescription.Name = "INDtxtDescription"
        Me.INDtxtDescription.Properties.AccessibleDescription = resources.GetString("INDtxtDescription.Properties.AccessibleDescription")
        Me.INDtxtDescription.Properties.AccessibleName = resources.GetString("INDtxtDescription.Properties.AccessibleName")
        Me.INDtxtDescription.Properties.Appearance.BackColor = CType(resources.GetObject("INDtxtDescription.Properties.Appearance.BackColor"), System.Drawing.Color)
        Me.INDtxtDescription.Properties.Appearance.Font = CType(resources.GetObject("INDtxtDescription.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDtxtDescription.Properties.Appearance.FontSizeDelta = CType(resources.GetObject("INDtxtDescription.Properties.Appearance.FontSizeDelta"), Integer)
        Me.INDtxtDescription.Properties.Appearance.FontStyleDelta = CType(resources.GetObject("INDtxtDescription.Properties.Appearance.FontStyleDelta"), System.Drawing.FontStyle)
        Me.INDtxtDescription.Properties.Appearance.GradientMode = CType(resources.GetObject("INDtxtDescription.Properties.Appearance.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.INDtxtDescription.Properties.Appearance.Image = CType(resources.GetObject("INDtxtDescription.Properties.Appearance.Image"), System.Drawing.Image)
        Me.INDtxtDescription.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtDescription.Properties.Appearance.Options.UseFont = True
        Me.INDtxtDescription.Properties.AppearanceFocused.BackColor = CType(resources.GetObject("INDtxtDescription.Properties.AppearanceFocused.BackColor"), System.Drawing.Color)
        Me.INDtxtDescription.Properties.AppearanceFocused.BorderColor = CType(resources.GetObject("INDtxtDescription.Properties.AppearanceFocused.BorderColor"), System.Drawing.Color)
        Me.INDtxtDescription.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDtxtDescription.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDtxtDescription.Properties.AppearanceFocused.FontSizeDelta = CType(resources.GetObject("INDtxtDescription.Properties.AppearanceFocused.FontSizeDelta"), Integer)
        Me.INDtxtDescription.Properties.AppearanceFocused.FontStyleDelta = CType(resources.GetObject("INDtxtDescription.Properties.AppearanceFocused.FontStyleDelta"), System.Drawing.FontStyle)
        Me.INDtxtDescription.Properties.AppearanceFocused.GradientMode = CType(resources.GetObject("INDtxtDescription.Properties.AppearanceFocused.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.INDtxtDescription.Properties.AppearanceFocused.Image = CType(resources.GetObject("INDtxtDescription.Properties.AppearanceFocused.Image"), System.Drawing.Image)
        Me.INDtxtDescription.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtDescription.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtxtDescription.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtDescription.Properties.AutoHeight = CType(resources.GetObject("INDtxtDescription.Properties.AutoHeight"), Boolean)
        Me.INDtxtDescription.Properties.Mask.AutoComplete = CType(resources.GetObject("INDtxtDescription.Properties.Mask.AutoComplete"), DevExpress.XtraEditors.Mask.AutoCompleteType)
        Me.INDtxtDescription.Properties.Mask.BeepOnError = CType(resources.GetObject("INDtxtDescription.Properties.Mask.BeepOnError"), Boolean)
        Me.INDtxtDescription.Properties.Mask.EditMask = resources.GetString("INDtxtDescription.Properties.Mask.EditMask")
        Me.INDtxtDescription.Properties.Mask.IgnoreMaskBlank = CType(resources.GetObject("INDtxtDescription.Properties.Mask.IgnoreMaskBlank"), Boolean)
        Me.INDtxtDescription.Properties.Mask.MaskType = CType(resources.GetObject("INDtxtDescription.Properties.Mask.MaskType"), DevExpress.XtraEditors.Mask.MaskType)
        Me.INDtxtDescription.Properties.Mask.PlaceHolder = CType(resources.GetObject("INDtxtDescription.Properties.Mask.PlaceHolder"), Char)
        Me.INDtxtDescription.Properties.Mask.SaveLiteral = CType(resources.GetObject("INDtxtDescription.Properties.Mask.SaveLiteral"), Boolean)
        Me.INDtxtDescription.Properties.Mask.ShowPlaceHolders = CType(resources.GetObject("INDtxtDescription.Properties.Mask.ShowPlaceHolders"), Boolean)
        Me.INDtxtDescription.Properties.Mask.UseMaskAsDisplayFormat = CType(resources.GetObject("INDtxtDescription.Properties.Mask.UseMaskAsDisplayFormat"), Boolean)
        Me.INDtxtDescription.Properties.MaxLength = 100
        Me.INDtxtDescription.Properties.NullValuePrompt = resources.GetString("INDtxtDescription.Properties.NullValuePrompt")
        Me.INDtxtDescription.Properties.NullValuePromptShowForEmptyValue = CType(resources.GetObject("INDtxtDescription.Properties.NullValuePromptShowForEmptyValue"), Boolean)
        Me.INDtxtDescription.StyleController = Me.INDlyCtrContractGroup
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtDescription, 5)
        '
        'INDtxtName
        '
        resources.ApplyResources(Me.INDtxtName, "INDtxtName")
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtName, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtName, True)
        Me.INDtxtName.EnterMoveNextControl = True
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtName, Presentation.Controls.IndigoTextEdit.EMask.AlfaNumerico)
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
        Me.INDtxtName.Properties.Mask.AutoComplete = CType(resources.GetObject("INDtxtName.Properties.Mask.AutoComplete"), DevExpress.XtraEditors.Mask.AutoCompleteType)
        Me.INDtxtName.Properties.Mask.BeepOnError = CType(resources.GetObject("INDtxtName.Properties.Mask.BeepOnError"), Boolean)
        Me.INDtxtName.Properties.Mask.EditMask = resources.GetString("INDtxtName.Properties.Mask.EditMask")
        Me.INDtxtName.Properties.Mask.IgnoreMaskBlank = CType(resources.GetObject("INDtxtName.Properties.Mask.IgnoreMaskBlank"), Boolean)
        Me.INDtxtName.Properties.Mask.MaskType = CType(resources.GetObject("INDtxtName.Properties.Mask.MaskType"), DevExpress.XtraEditors.Mask.MaskType)
        Me.INDtxtName.Properties.Mask.PlaceHolder = CType(resources.GetObject("INDtxtName.Properties.Mask.PlaceHolder"), Char)
        Me.INDtxtName.Properties.Mask.SaveLiteral = CType(resources.GetObject("INDtxtName.Properties.Mask.SaveLiteral"), Boolean)
        Me.INDtxtName.Properties.Mask.ShowPlaceHolders = CType(resources.GetObject("INDtxtName.Properties.Mask.ShowPlaceHolders"), Boolean)
        Me.INDtxtName.Properties.Mask.UseMaskAsDisplayFormat = CType(resources.GetObject("INDtxtName.Properties.Mask.UseMaskAsDisplayFormat"), Boolean)
        Me.INDtxtName.Properties.MaxLength = 50
        Me.INDtxtName.Properties.NullValuePrompt = resources.GetString("INDtxtName.Properties.NullValuePrompt")
        Me.INDtxtName.Properties.NullValuePromptShowForEmptyValue = CType(resources.GetObject("INDtxtName.Properties.NullValuePromptShowForEmptyValue"), Boolean)
        Me.INDtxtName.StyleController = Me.INDlyCtrContractGroup
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtName, 5)
        '
        'INDbteCode
        '
        resources.ApplyResources(Me.INDbteCode, "INDbteCode")
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDbteCode, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDbteCode, True)
        Me.IndigoTextEdit1.SetMascara(Me.INDbteCode, Presentation.Controls.IndigoTextEdit.EMask.AlfaNumerico)
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
        resources.ApplyResources(SerializableAppearanceObject1, "SerializableAppearanceObject1")
        Me.INDbteCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDbteCode.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines), resources.GetString("INDbteCode.Properties.Buttons1"), CType(resources.GetObject("INDbteCode.Properties.Buttons2"), Integer), CType(resources.GetObject("INDbteCode.Properties.Buttons3"), Boolean), CType(resources.GetObject("INDbteCode.Properties.Buttons4"), Boolean), CType(resources.GetObject("INDbteCode.Properties.Buttons5"), Boolean), CType(resources.GetObject("INDbteCode.Properties.Buttons6"), DevExpress.XtraEditors.ImageLocation), Global.Presentation.Payroll.My.Resources.Resources.BuscarMetro, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, resources.GetString("INDbteCode.Properties.Buttons7"), CType(resources.GetObject("INDbteCode.Properties.Buttons8"), Object), CType(resources.GetObject("INDbteCode.Properties.Buttons9"), DevExpress.Utils.SuperToolTip), CType(resources.GetObject("INDbteCode.Properties.Buttons10"), Boolean))})
        Me.INDbteCode.Properties.Mask.AutoComplete = CType(resources.GetObject("INDbteCode.Properties.Mask.AutoComplete"), DevExpress.XtraEditors.Mask.AutoCompleteType)
        Me.INDbteCode.Properties.Mask.BeepOnError = CType(resources.GetObject("INDbteCode.Properties.Mask.BeepOnError"), Boolean)
        Me.INDbteCode.Properties.Mask.EditMask = resources.GetString("INDbteCode.Properties.Mask.EditMask")
        Me.INDbteCode.Properties.Mask.IgnoreMaskBlank = CType(resources.GetObject("INDbteCode.Properties.Mask.IgnoreMaskBlank"), Boolean)
        Me.INDbteCode.Properties.Mask.MaskType = CType(resources.GetObject("INDbteCode.Properties.Mask.MaskType"), DevExpress.XtraEditors.Mask.MaskType)
        Me.INDbteCode.Properties.Mask.PlaceHolder = CType(resources.GetObject("INDbteCode.Properties.Mask.PlaceHolder"), Char)
        Me.INDbteCode.Properties.Mask.SaveLiteral = CType(resources.GetObject("INDbteCode.Properties.Mask.SaveLiteral"), Boolean)
        Me.INDbteCode.Properties.Mask.ShowPlaceHolders = CType(resources.GetObject("INDbteCode.Properties.Mask.ShowPlaceHolders"), Boolean)
        Me.INDbteCode.Properties.Mask.UseMaskAsDisplayFormat = CType(resources.GetObject("INDbteCode.Properties.Mask.UseMaskAsDisplayFormat"), Boolean)
        Me.INDbteCode.Properties.MaxLength = 20
        Me.INDbteCode.Properties.NullValuePrompt = resources.GetString("INDbteCode.Properties.NullValuePrompt")
        Me.INDbteCode.Properties.NullValuePromptShowForEmptyValue = CType(resources.GetObject("INDbteCode.Properties.NullValuePromptShowForEmptyValue"), Boolean)
        Me.INDbteCode.StyleController = Me.INDlyCtrContractGroup
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDbteCode, 1)
        '
        'INDlyContractGroup
        '
        Me.INDlyContractGroup.AppearanceGroup.Font = CType(resources.GetObject("INDlyContractGroup.AppearanceGroup.Font"), System.Drawing.Font)
        Me.INDlyContractGroup.AppearanceGroup.FontSizeDelta = CType(resources.GetObject("INDlyContractGroup.AppearanceGroup.FontSizeDelta"), Integer)
        Me.INDlyContractGroup.AppearanceGroup.FontStyleDelta = CType(resources.GetObject("INDlyContractGroup.AppearanceGroup.FontStyleDelta"), System.Drawing.FontStyle)
        'Cadena reemplazada... CType(resources.GetObject("INDlyContractGroup.AppearanceGroup.ForeColor"), System.Drawing.Color)
        Me.INDlyContractGroup.AppearanceGroup.GradientMode = CType(resources.GetObject("INDlyContractGroup.AppearanceGroup.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.INDlyContractGroup.AppearanceGroup.Image = CType(resources.GetObject("INDlyContractGroup.AppearanceGroup.Image"), System.Drawing.Image)
        Me.INDlyContractGroup.AppearanceGroup.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDlyContractGroup.AppearanceItemCaption.Font = CType(resources.GetObject("INDlyContractGroup.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.INDlyContractGroup.AppearanceItemCaption.FontSizeDelta = CType(resources.GetObject("INDlyContractGroup.AppearanceItemCaption.FontSizeDelta"), Integer)
        Me.INDlyContractGroup.AppearanceItemCaption.FontStyleDelta = CType(resources.GetObject("INDlyContractGroup.AppearanceItemCaption.FontStyleDelta"), System.Drawing.FontStyle)
        Me.INDlyContractGroup.AppearanceItemCaption.GradientMode = CType(resources.GetObject("INDlyContractGroup.AppearanceItemCaption.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.INDlyContractGroup.AppearanceItemCaption.Image = CType(resources.GetObject("INDlyContractGroup.AppearanceItemCaption.Image"), System.Drawing.Image)
        Me.INDlyContractGroup.AppearanceItemCaption.Options.UseFont = True
        resources.ApplyResources(Me.INDlyContractGroup, "INDlyContractGroup")
        Me.INDlyContractGroup.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDlyContractGroup.GroupBordersVisible = False
        Me.INDlyContractGroup.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.EmptySpaceItem1, Me.INDlyGrContractGroup})
        Me.INDlyContractGroup.Location = New System.Drawing.Point(0, 0)
        Me.INDlyContractGroup.Name = "INDlyContractGroup"
        Me.INDlyContractGroup.Size = New System.Drawing.Size(680, 335)
        Me.INDlyContractGroup.TextVisible = False
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.AllowHotTrack = False
        resources.ApplyResources(Me.EmptySpaceItem1, "EmptySpaceItem1")
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(0, 168)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(660, 147)
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'INDlyGrContractGroup
        '
        resources.ApplyResources(Me.INDlyGrContractGroup, "INDlyGrContractGroup")
        Me.INDlyGrContractGroup.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemCode, Me.INDlyItemName, Me.INDlyItemDescription})
        Me.INDlyGrContractGroup.Location = New System.Drawing.Point(0, 0)
        Me.INDlyGrContractGroup.Name = "INDlyGrContractGroup"
        Me.INDlyGrContractGroup.Size = New System.Drawing.Size(660, 168)
        '
        'INDlyItemCode
        '
        Me.INDlyItemCode.Control = Me.INDbteCode
        resources.ApplyResources(Me.INDlyItemCode, "INDlyItemCode")
        Me.INDlyItemCode.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemCode.MaxSize = New System.Drawing.Size(300, 36)
        Me.INDlyItemCode.MinSize = New System.Drawing.Size(300, 36)
        Me.INDlyItemCode.Name = "INDlyItemCode"
        Me.INDlyItemCode.Size = New System.Drawing.Size(636, 36)
        Me.INDlyItemCode.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemCode.Tag = "Code"
        Me.INDlyItemCode.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemCode.TextSize = New System.Drawing.Size(100, 20)
        Me.INDlyItemCode.TextToControlDistance = 12
        '
        'INDlyItemName
        '
        Me.INDlyItemName.Control = Me.INDtxtName
        resources.ApplyResources(Me.INDlyItemName, "INDlyItemName")
        Me.INDlyItemName.Location = New System.Drawing.Point(0, 36)
        Me.INDlyItemName.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemName.MinSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemName.Name = "INDlyItemName"
        Me.INDlyItemName.Size = New System.Drawing.Size(636, 36)
        Me.INDlyItemName.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemName.Tag = "Name"
        Me.INDlyItemName.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemName.TextSize = New System.Drawing.Size(100, 20)
        Me.INDlyItemName.TextToControlDistance = 12
        '
        'INDlyItemDescription
        '
        Me.INDlyItemDescription.Control = Me.INDtxtDescription
        resources.ApplyResources(Me.INDlyItemDescription, "INDlyItemDescription")
        Me.INDlyItemDescription.Location = New System.Drawing.Point(0, 72)
        Me.INDlyItemDescription.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemDescription.MinSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemDescription.Name = "INDlyItemDescription"
        Me.INDlyItemDescription.Size = New System.Drawing.Size(636, 36)
        Me.INDlyItemDescription.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemDescription.Tag = "Description"
        Me.INDlyItemDescription.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemDescription.TextSize = New System.Drawing.Size(100, 20)
        Me.INDlyItemDescription.TextToControlDistance = 12
        '
        'FrmContractGroup
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Name = "FrmContractGroup"
        Me.Opacity = 1.0R
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.Tag = "539"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyCtrContractGroup, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyCtrContractGroup.ResumeLayout(False)
        CType(Me.INDtxtDescription.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDbteCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyContractGroup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyGrContractGroup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDlyCtrContractGroup As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDtxtDescription As DevExpress.XtraEditors.TextEdit
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents INDtxtName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDbteCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents INDlyContractGroup As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemCode As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemName As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemDescription As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents INDlyGrContractGroup As DevExpress.XtraLayout.LayoutControlGroup
End Class
