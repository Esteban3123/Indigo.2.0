Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmPensionaryType
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmPensionaryType))
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.INDlyCtrPensionaryType = New DevExpress.XtraLayout.LayoutControl()
        Me.INDbteCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.INDtxtName = New DevExpress.XtraEditors.TextEdit()
        Me.INDlyCgrPensionaryType = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyGrPensionaryType = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemName = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyCtrPensionaryType, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyCtrPensionaryType.SuspendLayout()
        CType(Me.INDbteCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyCgrPensionaryType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyGrPensionaryType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        resources.ApplyResources(Me.INDPanelControlBase, "INDPanelControlBase")
        Me.INDPanelControlBase.Controls.Add(Me.INDlyCtrPensionaryType)
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
        'INDlyCtrPensionaryType
        '
        resources.ApplyResources(Me.INDlyCtrPensionaryType, "INDlyCtrPensionaryType")
        Me.INDlyCtrPensionaryType.AllowCustomizationMenu = False
        Me.INDlyCtrPensionaryType.Appearance.DisabledLayoutGroupCaption.FontSizeDelta = CType(resources.GetObject("INDlyCtrPensionaryType.Appearance.DisabledLayoutGroupCaption.FontSizeDelta"), Integer)
        Me.INDlyCtrPensionaryType.Appearance.DisabledLayoutGroupCaption.FontStyleDelta = CType(resources.GetObject("INDlyCtrPensionaryType.Appearance.DisabledLayoutGroupCaption.FontStyleDelta"), System.Drawing.FontStyle)
        Me.INDlyCtrPensionaryType.Appearance.DisabledLayoutGroupCaption.GradientMode = CType(resources.GetObject("INDlyCtrPensionaryType.Appearance.DisabledLayoutGroupCaption.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.INDlyCtrPensionaryType.Appearance.DisabledLayoutGroupCaption.Image = CType(resources.GetObject("INDlyCtrPensionaryType.Appearance.DisabledLayoutGroupCaption.Image"), System.Drawing.Image)
        Me.INDlyCtrPensionaryType.Appearance.DisabledLayoutItem.FontSizeDelta = CType(resources.GetObject("INDlyCtrPensionaryType.Appearance.DisabledLayoutItem.FontSizeDelta"), Integer)
        Me.INDlyCtrPensionaryType.Appearance.DisabledLayoutItem.FontStyleDelta = CType(resources.GetObject("INDlyCtrPensionaryType.Appearance.DisabledLayoutItem.FontStyleDelta"), System.Drawing.FontStyle)
        Me.INDlyCtrPensionaryType.Appearance.DisabledLayoutItem.GradientMode = CType(resources.GetObject("INDlyCtrPensionaryType.Appearance.DisabledLayoutItem.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.INDlyCtrPensionaryType.Appearance.DisabledLayoutItem.Image = CType(resources.GetObject("INDlyCtrPensionaryType.Appearance.DisabledLayoutItem.Image"), System.Drawing.Image)
        Me.INDlyCtrPensionaryType.Controls.Add(Me.INDbteCode)
        Me.INDlyCtrPensionaryType.Controls.Add(Me.INDtxtName)
        Me.LayoutControls.SetIsCustomizable(Me.INDlyCtrPensionaryType, False)
        Me.INDlyCtrPensionaryType.Name = "INDlyCtrPensionaryType"
        Me.INDlyCtrPensionaryType.Root = Me.INDlyCgrPensionaryType
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
        Me.INDbteCode.StyleController = Me.INDlyCtrPensionaryType
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDbteCode, 1)
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
        Me.INDtxtName.Properties.MaxLength = 100
        Me.INDtxtName.Properties.NullValuePrompt = resources.GetString("INDtxtName.Properties.NullValuePrompt")
        Me.INDtxtName.Properties.NullValuePromptShowForEmptyValue = CType(resources.GetObject("INDtxtName.Properties.NullValuePromptShowForEmptyValue"), Boolean)
        Me.INDtxtName.StyleController = Me.INDlyCtrPensionaryType
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtName, 0)
        '
        'INDlyCgrPensionaryType
        '
        Me.INDlyCgrPensionaryType.AppearanceGroup.Font = CType(resources.GetObject("INDlyCgrPensionaryType.AppearanceGroup.Font"), System.Drawing.Font)
        Me.INDlyCgrPensionaryType.AppearanceGroup.FontSizeDelta = CType(resources.GetObject("INDlyCgrPensionaryType.AppearanceGroup.FontSizeDelta"), Integer)
        Me.INDlyCgrPensionaryType.AppearanceGroup.FontStyleDelta = CType(resources.GetObject("INDlyCgrPensionaryType.AppearanceGroup.FontStyleDelta"), System.Drawing.FontStyle)
        'Cadena reemplazada... CType(resources.GetObject("INDlyCgrPensionaryType.AppearanceGroup.ForeColor"), System.Drawing.Color)
        Me.INDlyCgrPensionaryType.AppearanceGroup.GradientMode = CType(resources.GetObject("INDlyCgrPensionaryType.AppearanceGroup.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.INDlyCgrPensionaryType.AppearanceGroup.Image = CType(resources.GetObject("INDlyCgrPensionaryType.AppearanceGroup.Image"), System.Drawing.Image)
        Me.INDlyCgrPensionaryType.AppearanceGroup.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDlyCgrPensionaryType.AppearanceItemCaption.Font = CType(resources.GetObject("INDlyCgrPensionaryType.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.INDlyCgrPensionaryType.AppearanceItemCaption.FontSizeDelta = CType(resources.GetObject("INDlyCgrPensionaryType.AppearanceItemCaption.FontSizeDelta"), Integer)
        Me.INDlyCgrPensionaryType.AppearanceItemCaption.FontStyleDelta = CType(resources.GetObject("INDlyCgrPensionaryType.AppearanceItemCaption.FontStyleDelta"), System.Drawing.FontStyle)
        Me.INDlyCgrPensionaryType.AppearanceItemCaption.GradientMode = CType(resources.GetObject("INDlyCgrPensionaryType.AppearanceItemCaption.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.INDlyCgrPensionaryType.AppearanceItemCaption.Image = CType(resources.GetObject("INDlyCgrPensionaryType.AppearanceItemCaption.Image"), System.Drawing.Image)
        Me.INDlyCgrPensionaryType.AppearanceItemCaption.Options.UseFont = True
        Me.INDlyCgrPensionaryType.AppearanceTabPage.Header.Font = CType(resources.GetObject("INDlyCgrPensionaryType.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.INDlyCgrPensionaryType.AppearanceTabPage.Header.FontSizeDelta = CType(resources.GetObject("INDlyCgrPensionaryType.AppearanceTabPage.Header.FontSizeDelta"), Integer)
        Me.INDlyCgrPensionaryType.AppearanceTabPage.Header.FontStyleDelta = CType(resources.GetObject("INDlyCgrPensionaryType.AppearanceTabPage.Header.FontStyleDelta"), System.Drawing.FontStyle)
        Me.INDlyCgrPensionaryType.AppearanceTabPage.Header.GradientMode = CType(resources.GetObject("INDlyCgrPensionaryType.AppearanceTabPage.Header.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.INDlyCgrPensionaryType.AppearanceTabPage.Header.Image = CType(resources.GetObject("INDlyCgrPensionaryType.AppearanceTabPage.Header.Image"), System.Drawing.Image)
        Me.INDlyCgrPensionaryType.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlyCgrPensionaryType.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("INDlyCgrPensionaryType.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        Me.INDlyCgrPensionaryType.AppearanceTabPage.HeaderActive.FontSizeDelta = CType(resources.GetObject("INDlyCgrPensionaryType.AppearanceTabPage.HeaderActive.FontSizeDelta"), Integer)
        Me.INDlyCgrPensionaryType.AppearanceTabPage.HeaderActive.FontStyleDelta = CType(resources.GetObject("INDlyCgrPensionaryType.AppearanceTabPage.HeaderActive.FontStyleDelta"), System.Drawing.FontStyle)
        'Cadena reemplazada... CType(resources.GetObject("INDlyCgrPensionaryType.AppearanceTabPage.HeaderActive.ForeColor"), System.Drawing.Color)
        Me.INDlyCgrPensionaryType.AppearanceTabPage.HeaderActive.GradientMode = CType(resources.GetObject("INDlyCgrPensionaryType.AppearanceTabPage.HeaderActive.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.INDlyCgrPensionaryType.AppearanceTabPage.HeaderActive.Image = CType(resources.GetObject("INDlyCgrPensionaryType.AppearanceTabPage.HeaderActive.Image"), System.Drawing.Image)
        Me.INDlyCgrPensionaryType.AppearanceTabPage.HeaderActive.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDlyCgrPensionaryType.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("INDlyCgrPensionaryType.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.INDlyCgrPensionaryType.AppearanceTabPage.HeaderDisabled.FontSizeDelta = CType(resources.GetObject("INDlyCgrPensionaryType.AppearanceTabPage.HeaderDisabled.FontSizeDelta"), Integer)
        Me.INDlyCgrPensionaryType.AppearanceTabPage.HeaderDisabled.FontStyleDelta = CType(resources.GetObject("INDlyCgrPensionaryType.AppearanceTabPage.HeaderDisabled.FontStyleDelta"), System.Drawing.FontStyle)
        Me.INDlyCgrPensionaryType.AppearanceTabPage.HeaderDisabled.GradientMode = CType(resources.GetObject("INDlyCgrPensionaryType.AppearanceTabPage.HeaderDisabled.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.INDlyCgrPensionaryType.AppearanceTabPage.HeaderDisabled.Image = CType(resources.GetObject("INDlyCgrPensionaryType.AppearanceTabPage.HeaderDisabled.Image"), System.Drawing.Image)
        Me.INDlyCgrPensionaryType.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlyCgrPensionaryType.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("INDlyCgrPensionaryType.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        Me.INDlyCgrPensionaryType.AppearanceTabPage.HeaderHotTracked.FontSizeDelta = CType(resources.GetObject("INDlyCgrPensionaryType.AppearanceTabPage.HeaderHotTracked.FontSizeDelta"), Integer)
        Me.INDlyCgrPensionaryType.AppearanceTabPage.HeaderHotTracked.FontStyleDelta = CType(resources.GetObject("INDlyCgrPensionaryType.AppearanceTabPage.HeaderHotTracked.FontStyleDelta"), System.Drawing.FontStyle)
        'Cadena reemplazada... CType(resources.GetObject("INDlyCgrPensionaryType.AppearanceTabPage.HeaderHotTracked.ForeColor"), System.Drawing.Color)
        Me.INDlyCgrPensionaryType.AppearanceTabPage.HeaderHotTracked.GradientMode = CType(resources.GetObject("INDlyCgrPensionaryType.AppearanceTabPage.HeaderHotTracked.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.INDlyCgrPensionaryType.AppearanceTabPage.HeaderHotTracked.Image = CType(resources.GetObject("INDlyCgrPensionaryType.AppearanceTabPage.HeaderHotTracked.Image"), System.Drawing.Image)
        Me.INDlyCgrPensionaryType.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDlyCgrPensionaryType.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("INDlyCgrPensionaryType.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.INDlyCgrPensionaryType.AppearanceTabPage.PageClient.FontSizeDelta = CType(resources.GetObject("INDlyCgrPensionaryType.AppearanceTabPage.PageClient.FontSizeDelta"), Integer)
        Me.INDlyCgrPensionaryType.AppearanceTabPage.PageClient.FontStyleDelta = CType(resources.GetObject("INDlyCgrPensionaryType.AppearanceTabPage.PageClient.FontStyleDelta"), System.Drawing.FontStyle)
        Me.INDlyCgrPensionaryType.AppearanceTabPage.PageClient.GradientMode = CType(resources.GetObject("INDlyCgrPensionaryType.AppearanceTabPage.PageClient.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.INDlyCgrPensionaryType.AppearanceTabPage.PageClient.Image = CType(resources.GetObject("INDlyCgrPensionaryType.AppearanceTabPage.PageClient.Image"), System.Drawing.Image)
        Me.INDlyCgrPensionaryType.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlyCgrPensionaryType, False)
        resources.ApplyResources(Me.INDlyCgrPensionaryType, "INDlyCgrPensionaryType")
        Me.INDlyCgrPensionaryType.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDlyCgrPensionaryType.GroupBordersVisible = False
        Me.INDlyCgrPensionaryType.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyGrPensionaryType})
        Me.INDlyCgrPensionaryType.Location = New System.Drawing.Point(0, 0)
        Me.INDlyCgrPensionaryType.Name = "INDlyCgrPensionaryType"
        Me.INDlyCgrPensionaryType.Size = New System.Drawing.Size(653, 465)
        Me.INDlyCgrPensionaryType.TextVisible = False
        '
        'INDlyGrPensionaryType
        '
        Me.INDlyGrPensionaryType.AppearanceGroup.Font = CType(resources.GetObject("INDlyGrPensionaryType.AppearanceGroup.Font"), System.Drawing.Font)
        Me.INDlyGrPensionaryType.AppearanceGroup.FontSizeDelta = CType(resources.GetObject("INDlyGrPensionaryType.AppearanceGroup.FontSizeDelta"), Integer)
        Me.INDlyGrPensionaryType.AppearanceGroup.FontStyleDelta = CType(resources.GetObject("INDlyGrPensionaryType.AppearanceGroup.FontStyleDelta"), System.Drawing.FontStyle)
        'Cadena reemplazada... CType(resources.GetObject("INDlyGrPensionaryType.AppearanceGroup.ForeColor"), System.Drawing.Color)
        Me.INDlyGrPensionaryType.AppearanceGroup.GradientMode = CType(resources.GetObject("INDlyGrPensionaryType.AppearanceGroup.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.INDlyGrPensionaryType.AppearanceGroup.Image = CType(resources.GetObject("INDlyGrPensionaryType.AppearanceGroup.Image"), System.Drawing.Image)
        Me.INDlyGrPensionaryType.AppearanceGroup.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDlyGrPensionaryType.AppearanceItemCaption.Font = CType(resources.GetObject("INDlyGrPensionaryType.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.INDlyGrPensionaryType.AppearanceItemCaption.FontSizeDelta = CType(resources.GetObject("INDlyGrPensionaryType.AppearanceItemCaption.FontSizeDelta"), Integer)
        Me.INDlyGrPensionaryType.AppearanceItemCaption.FontStyleDelta = CType(resources.GetObject("INDlyGrPensionaryType.AppearanceItemCaption.FontStyleDelta"), System.Drawing.FontStyle)
        Me.INDlyGrPensionaryType.AppearanceItemCaption.GradientMode = CType(resources.GetObject("INDlyGrPensionaryType.AppearanceItemCaption.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.INDlyGrPensionaryType.AppearanceItemCaption.Image = CType(resources.GetObject("INDlyGrPensionaryType.AppearanceItemCaption.Image"), System.Drawing.Image)
        Me.INDlyGrPensionaryType.AppearanceItemCaption.Options.UseFont = True
        Me.INDlyGrPensionaryType.AppearanceTabPage.Header.Font = CType(resources.GetObject("INDlyGrPensionaryType.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.INDlyGrPensionaryType.AppearanceTabPage.Header.FontSizeDelta = CType(resources.GetObject("INDlyGrPensionaryType.AppearanceTabPage.Header.FontSizeDelta"), Integer)
        Me.INDlyGrPensionaryType.AppearanceTabPage.Header.FontStyleDelta = CType(resources.GetObject("INDlyGrPensionaryType.AppearanceTabPage.Header.FontStyleDelta"), System.Drawing.FontStyle)
        Me.INDlyGrPensionaryType.AppearanceTabPage.Header.GradientMode = CType(resources.GetObject("INDlyGrPensionaryType.AppearanceTabPage.Header.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.INDlyGrPensionaryType.AppearanceTabPage.Header.Image = CType(resources.GetObject("INDlyGrPensionaryType.AppearanceTabPage.Header.Image"), System.Drawing.Image)
        Me.INDlyGrPensionaryType.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlyGrPensionaryType.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("INDlyGrPensionaryType.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        Me.INDlyGrPensionaryType.AppearanceTabPage.HeaderActive.FontSizeDelta = CType(resources.GetObject("INDlyGrPensionaryType.AppearanceTabPage.HeaderActive.FontSizeDelta"), Integer)
        Me.INDlyGrPensionaryType.AppearanceTabPage.HeaderActive.FontStyleDelta = CType(resources.GetObject("INDlyGrPensionaryType.AppearanceTabPage.HeaderActive.FontStyleDelta"), System.Drawing.FontStyle)
        'Cadena reemplazada... CType(resources.GetObject("INDlyGrPensionaryType.AppearanceTabPage.HeaderActive.ForeColor"), System.Drawing.Color)
        Me.INDlyGrPensionaryType.AppearanceTabPage.HeaderActive.GradientMode = CType(resources.GetObject("INDlyGrPensionaryType.AppearanceTabPage.HeaderActive.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.INDlyGrPensionaryType.AppearanceTabPage.HeaderActive.Image = CType(resources.GetObject("INDlyGrPensionaryType.AppearanceTabPage.HeaderActive.Image"), System.Drawing.Image)
        Me.INDlyGrPensionaryType.AppearanceTabPage.HeaderActive.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDlyGrPensionaryType.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("INDlyGrPensionaryType.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.INDlyGrPensionaryType.AppearanceTabPage.HeaderDisabled.FontSizeDelta = CType(resources.GetObject("INDlyGrPensionaryType.AppearanceTabPage.HeaderDisabled.FontSizeDelta"), Integer)
        Me.INDlyGrPensionaryType.AppearanceTabPage.HeaderDisabled.FontStyleDelta = CType(resources.GetObject("INDlyGrPensionaryType.AppearanceTabPage.HeaderDisabled.FontStyleDelta"), System.Drawing.FontStyle)
        Me.INDlyGrPensionaryType.AppearanceTabPage.HeaderDisabled.GradientMode = CType(resources.GetObject("INDlyGrPensionaryType.AppearanceTabPage.HeaderDisabled.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.INDlyGrPensionaryType.AppearanceTabPage.HeaderDisabled.Image = CType(resources.GetObject("INDlyGrPensionaryType.AppearanceTabPage.HeaderDisabled.Image"), System.Drawing.Image)
        Me.INDlyGrPensionaryType.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlyGrPensionaryType.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("INDlyGrPensionaryType.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        Me.INDlyGrPensionaryType.AppearanceTabPage.HeaderHotTracked.FontSizeDelta = CType(resources.GetObject("INDlyGrPensionaryType.AppearanceTabPage.HeaderHotTracked.FontSizeDelta"), Integer)
        Me.INDlyGrPensionaryType.AppearanceTabPage.HeaderHotTracked.FontStyleDelta = CType(resources.GetObject("INDlyGrPensionaryType.AppearanceTabPage.HeaderHotTracked.FontStyleDelta"), System.Drawing.FontStyle)
        'Cadena reemplazada... CType(resources.GetObject("INDlyGrPensionaryType.AppearanceTabPage.HeaderHotTracked.ForeColor"), System.Drawing.Color)
        Me.INDlyGrPensionaryType.AppearanceTabPage.HeaderHotTracked.GradientMode = CType(resources.GetObject("INDlyGrPensionaryType.AppearanceTabPage.HeaderHotTracked.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.INDlyGrPensionaryType.AppearanceTabPage.HeaderHotTracked.Image = CType(resources.GetObject("INDlyGrPensionaryType.AppearanceTabPage.HeaderHotTracked.Image"), System.Drawing.Image)
        Me.INDlyGrPensionaryType.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDlyGrPensionaryType.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("INDlyGrPensionaryType.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.INDlyGrPensionaryType.AppearanceTabPage.PageClient.FontSizeDelta = CType(resources.GetObject("INDlyGrPensionaryType.AppearanceTabPage.PageClient.FontSizeDelta"), Integer)
        Me.INDlyGrPensionaryType.AppearanceTabPage.PageClient.FontStyleDelta = CType(resources.GetObject("INDlyGrPensionaryType.AppearanceTabPage.PageClient.FontStyleDelta"), System.Drawing.FontStyle)
        Me.INDlyGrPensionaryType.AppearanceTabPage.PageClient.GradientMode = CType(resources.GetObject("INDlyGrPensionaryType.AppearanceTabPage.PageClient.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.INDlyGrPensionaryType.AppearanceTabPage.PageClient.Image = CType(resources.GetObject("INDlyGrPensionaryType.AppearanceTabPage.PageClient.Image"), System.Drawing.Image)
        Me.INDlyGrPensionaryType.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlyGrPensionaryType, False)
        resources.ApplyResources(Me.INDlyGrPensionaryType, "INDlyGrPensionaryType")
        Me.INDlyGrPensionaryType.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemCode, Me.INDlyItemName, Me.EmptySpaceItem1})
        Me.INDlyGrPensionaryType.Location = New System.Drawing.Point(0, 0)
        Me.INDlyGrPensionaryType.Name = "INDlyGrPensionaryType"
        Me.INDlyGrPensionaryType.Size = New System.Drawing.Size(633, 445)
        '
        'INDlyItemCode
        '
        Me.INDlyItemCode.Control = Me.INDbteCode
        resources.ApplyResources(Me.INDlyItemCode, "INDlyItemCode")
        Me.INDlyItemCode.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemCode.MaxSize = New System.Drawing.Size(300, 36)
        Me.INDlyItemCode.MinSize = New System.Drawing.Size(300, 36)
        Me.INDlyItemCode.Name = "INDlyItemCode"
        Me.INDlyItemCode.Size = New System.Drawing.Size(609, 36)
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
        Me.INDlyItemName.Size = New System.Drawing.Size(609, 36)
        Me.INDlyItemName.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemName.Tag = "Name"
        Me.INDlyItemName.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemName.TextSize = New System.Drawing.Size(100, 20)
        Me.INDlyItemName.TextToControlDistance = 12
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.AllowHotTrack = False
        resources.ApplyResources(Me.EmptySpaceItem1, "EmptySpaceItem1")
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(0, 72)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(609, 313)
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'FrmPensionaryType
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Name = "FrmPensionaryType"
        Me.Opacity = 1.0R
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.Tag = "531"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyCtrPensionaryType, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyCtrPensionaryType.ResumeLayout(False)
        CType(Me.INDbteCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyCgrPensionaryType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyGrPensionaryType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDlyCtrPensionaryType As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDlyCgrPensionaryType As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDbteCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents INDtxtName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDlyGrPensionaryType As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemCode As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemName As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
End Class
