<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CtrAdvancedFilterLink
    Inherits DevExpress.XtraEditors.XtraUserControl

    'UserControl overrides dispose to clean up the component list.
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CtrAdvancedFilterLink))
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.IMCIcons = New DevExpress.Utils.ImageCollection(Me.components)
        Me.INDbteCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.INDlblAux = New DevExpress.XtraEditors.LabelControl()
        CType(Me.IMCIcons, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDbteCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'IMCIcons
        '
        Me.IMCIcons.ImageStream = CType(resources.GetObject("IMCIcons.ImageStream"), DevExpress.Utils.ImageCollectionStreamer)
        Me.IMCIcons.Images.SetKeyName(0, "eliminar1.png")
        Me.IMCIcons.Images.SetKeyName(1, "eliminar2.png")
        Me.IMCIcons.Images.SetKeyName(2, "editar1.png")
        Me.IMCIcons.Images.SetKeyName(3, "editar2.png")
        '
        'INDbteCode
        '
        resources.ApplyResources(Me.INDbteCode, "INDbteCode")
        Me.INDbteCode.Name = "INDbteCode"
        Me.INDbteCode.Properties.AccessibleDescription = resources.GetString("INDbteCode.Properties.AccessibleDescription")
        Me.INDbteCode.Properties.AccessibleName = resources.GetString("INDbteCode.Properties.AccessibleName")
        Me.INDbteCode.Properties.Appearance.BackColor = CType(resources.GetObject("INDbteCode.Properties.Appearance.BackColor"), System.Drawing.Color)
        Me.INDbteCode.Properties.Appearance.Font = CType(resources.GetObject("INDbteCode.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDbteCode.Properties.Appearance.GradientMode = CType(resources.GetObject("INDbteCode.Properties.Appearance.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.INDbteCode.Properties.Appearance.Image = CType(resources.GetObject("INDbteCode.Properties.Appearance.Image"), System.Drawing.Image)
        Me.INDbteCode.Properties.Appearance.Options.UseBackColor = True
        Me.INDbteCode.Properties.Appearance.Options.UseFont = True
        Me.INDbteCode.Properties.AutoHeight = CType(resources.GetObject("INDbteCode.Properties.AutoHeight"), Boolean)
        Me.INDbteCode.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        resources.ApplyResources(SerializableAppearanceObject1, "SerializableAppearanceObject1")
        Me.INDbteCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDbteCode.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines), resources.GetString("INDbteCode.Properties.Buttons1"), CType(resources.GetObject("INDbteCode.Properties.Buttons2"), Integer), CType(resources.GetObject("INDbteCode.Properties.Buttons3"), Boolean), CType(resources.GetObject("INDbteCode.Properties.Buttons4"), Boolean), CType(resources.GetObject("INDbteCode.Properties.Buttons5"), Boolean), CType(resources.GetObject("INDbteCode.Properties.Buttons6"), DevExpress.XtraEditors.ImageLocation), CType(resources.GetObject("INDbteCode.Properties.Buttons7"), System.Drawing.Image), New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, resources.GetString("INDbteCode.Properties.Buttons8"), CType(resources.GetObject("INDbteCode.Properties.Buttons9"), Object), CType(resources.GetObject("INDbteCode.Properties.Buttons10"), DevExpress.Utils.SuperToolTip), CType(resources.GetObject("INDbteCode.Properties.Buttons11"), Boolean))})
        Me.INDbteCode.Properties.Mask.AutoComplete = CType(resources.GetObject("INDbteCode.Properties.Mask.AutoComplete"), DevExpress.XtraEditors.Mask.AutoCompleteType)
        Me.INDbteCode.Properties.Mask.BeepOnError = CType(resources.GetObject("INDbteCode.Properties.Mask.BeepOnError"), Boolean)
        Me.INDbteCode.Properties.Mask.EditMask = resources.GetString("INDbteCode.Properties.Mask.EditMask")
        Me.INDbteCode.Properties.Mask.IgnoreMaskBlank = CType(resources.GetObject("INDbteCode.Properties.Mask.IgnoreMaskBlank"), Boolean)
        Me.INDbteCode.Properties.Mask.MaskType = CType(resources.GetObject("INDbteCode.Properties.Mask.MaskType"), DevExpress.XtraEditors.Mask.MaskType)
        Me.INDbteCode.Properties.Mask.PlaceHolder = CType(resources.GetObject("INDbteCode.Properties.Mask.PlaceHolder"), Char)
        Me.INDbteCode.Properties.Mask.SaveLiteral = CType(resources.GetObject("INDbteCode.Properties.Mask.SaveLiteral"), Boolean)
        Me.INDbteCode.Properties.Mask.ShowPlaceHolders = CType(resources.GetObject("INDbteCode.Properties.Mask.ShowPlaceHolders"), Boolean)
        Me.INDbteCode.Properties.Mask.UseMaskAsDisplayFormat = CType(resources.GetObject("INDbteCode.Properties.Mask.UseMaskAsDisplayFormat"), Boolean)
        Me.INDbteCode.Properties.NullValuePrompt = resources.GetString("INDbteCode.Properties.NullValuePrompt")
        Me.INDbteCode.Properties.NullValuePromptShowForEmptyValue = CType(resources.GetObject("INDbteCode.Properties.NullValuePromptShowForEmptyValue"), Boolean)
        Me.INDbteCode.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.INDbteCode.TabStop = False
        '
        'INDlblAux
        '
        resources.ApplyResources(Me.INDlblAux, "INDlblAux")
        Me.INDlblAux.Name = "INDlblAux"
        '
        'CtrAdvancedFilterLink
        '
        resources.ApplyResources(Me, "$this")
        Me.Appearance.BackColor = CType(resources.GetObject("CtrAdvancedFilterLink.Appearance.BackColor"), System.Drawing.Color)
        Me.Appearance.GradientMode = CType(resources.GetObject("CtrAdvancedFilterLink.Appearance.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.Appearance.Image = CType(resources.GetObject("CtrAdvancedFilterLink.Appearance.Image"), System.Drawing.Image)
        Me.Appearance.Options.UseBackColor = True
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.INDbteCode)
        Me.Controls.Add(Me.INDlblAux)
        Me.Name = "CtrAdvancedFilterLink"
        CType(Me.IMCIcons, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDbteCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents IMCIcons As DevExpress.Utils.ImageCollection
    Friend WithEvents INDbteCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents INDlblAux As DevExpress.XtraEditors.LabelControl

End Class
