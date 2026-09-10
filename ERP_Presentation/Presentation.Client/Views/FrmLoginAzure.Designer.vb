Imports DevExpress.XtraBars.Docking

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmLoginAzure
    Inherits DevExpress.XtraEditors.XtraForm
    'Inherits DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm
    'Inherits DevExpress.XtraBars.Ribbon.RibbonForm

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            Dispose_Form(True, True)
            MyBase.Dispose(disposing)
            GC.SuppressFinalize(Me)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmLoginAzure))
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
        Me.INDProgress = New DevExpress.XtraWaitForm.ProgressPanel()
        Me.INDicIconosMensaje = New DevExpress.Utils.ImageCollection(Me.components)
        Me.INDPce = New DevExpress.XtraEditors.PopupContainerEdit()
        CType(Me.INDicIconosMensaje, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPce.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDProgress
        '
        Me.INDProgress.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDProgress.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDProgress.Appearance.Options.UseBackColor = True
        Me.INDProgress.Appearance.Options.UseFont = True
        Me.INDProgress.AppearanceCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDProgress.AppearanceCaption.Options.UseFont = True
        Me.INDProgress.AppearanceDescription.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDProgress.AppearanceDescription.Options.UseFont = True
        Me.INDProgress.Location = New System.Drawing.Point(402, 337)
        Me.INDProgress.Name = "INDProgress"
        Me.INDProgress.Padding = New System.Windows.Forms.Padding(30, 0, 0, 0)
        Me.INDProgress.Size = New System.Drawing.Size(309, 66)
        Me.INDProgress.TabIndex = 7
        Me.INDProgress.Text = "ProgressPanel1"
        Me.INDProgress.Visible = False
        '
        'INDicIconosMensaje
        '
        Me.INDicIconosMensaje.ImageSize = New System.Drawing.Size(48, 48)
        Me.INDicIconosMensaje.ImageStream = CType(resources.GetObject("INDicIconosMensaje.ImageStream"), DevExpress.Utils.ImageCollectionStreamer)
        Me.INDicIconosMensaje.Images.SetKeyName(0, "Info.png")
        Me.INDicIconosMensaje.Images.SetKeyName(1, "Adve.png")
        '
        'INDPce
        '
        Me.INDPce.Dock = System.Windows.Forms.DockStyle.Right
        Me.INDPce.Location = New System.Drawing.Point(1536, 0)
        Me.INDPce.Margin = New System.Windows.Forms.Padding(0)
        Me.INDPce.Name = "INDPce"
        Me.INDPce.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDPce.Properties.Appearance.Options.UseBackColor = True
        Me.INDPce.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        EditorButtonImageOptions1.Image = Global.Presentation.Client.My.Resources.Resources.minimizar_lila
        SerializableAppearanceObject1.BackColor = System.Drawing.Color.White
        SerializableAppearanceObject1.Options.UseBackColor = True
        SerializableAppearanceObject2.BackColor = System.Drawing.Color.LightGray
        SerializableAppearanceObject2.Options.UseBackColor = True
        EditorButtonImageOptions2.Image = Global.Presentation.Client.My.Resources.Resources.cerrar_lila
        SerializableAppearanceObject5.BackColor = System.Drawing.Color.White
        SerializableAppearanceObject5.Options.UseBackColor = True
        SerializableAppearanceObject6.BackColor = System.Drawing.Color.LightGray
        SerializableAppearanceObject6.Options.UseBackColor = True
        Me.INDPce.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "Minimizar", -1, True, True, False, EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, "Minimizar", Nothing, Nothing, DevExpress.Utils.ToolTipAnchor.[Default]), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "Salir", -1, True, True, False, EditorButtonImageOptions2, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject5, SerializableAppearanceObject6, SerializableAppearanceObject7, SerializableAppearanceObject8, "Salir", Nothing, Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.INDPce.Properties.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDPce.Properties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.NoBorder
        Me.INDPce.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        Me.INDPce.Size = New System.Drawing.Size(66, 20)
        Me.INDPce.TabIndex = 27
        '
        'FrmLoginAzure
        '
        Me.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.Appearance.Options.UseBackColor = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1602, 807)
        Me.Controls.Add(Me.INDPce)
        Me.Controls.Add(Me.INDProgress)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.IconOptions.SvgImage = CType(resources.GetObject("FrmLoginAzure.IconOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.MaximizeBox = False
        Me.Name = "FrmLoginAzure"
        Me.Opacity = 0R
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Login Vie Cloud Native Client"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.INDicIconosMensaje, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPce.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDProgress As DevExpress.XtraWaitForm.ProgressPanel
    Friend WithEvents INDicIconosMensaje As DevExpress.Utils.ImageCollection
    Friend WithEvents INDPce As DevExpress.XtraEditors.PopupContainerEdit
End Class


