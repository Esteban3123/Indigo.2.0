<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmOptionalParametersBankFile
    Inherits DevExpress.XtraEditors.XtraForm

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
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.INDBtnOk = New DevExpress.XtraEditors.SimpleButton()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDTxtDestinationPlace = New DevExpress.XtraEditors.TextEdit()
        Me.INDTxtSourcePlace = New DevExpress.XtraEditors.TextEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciSourcePlace = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciDestinationPlace = New DevExpress.XtraLayout.LayoutControlItem()
        'Me.IndigoLayoutControl1 = New Presentation.Controls.IndigoLayoutControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.INDTxtDestinationPlace.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtSourcePlace.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciSourcePlace, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciDestinationPlace, System.ComponentModel.ISupportInitialize).BeginInit()
        'CType(Me.IndigoLayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.INDBtnOk)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelControl1.Location = New System.Drawing.Point(0, 92)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(412, 36)
        Me.PanelControl1.TabIndex = 0
        '
        'INDBtnOk
        '
        Me.INDBtnOk.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDBtnOk.Location = New System.Drawing.Point(2, 2)
        Me.INDBtnOk.Name = "INDBtnOk"
        Me.INDBtnOk.Size = New System.Drawing.Size(408, 32)
        Me.INDBtnOk.TabIndex = 0
        Me.INDBtnOk.Text = "Aceptar"
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.INDTxtDestinationPlace)
        Me.LayoutControl1.Controls.Add(Me.INDTxtSourcePlace)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(412, 92)
        Me.LayoutControl1.TabIndex = 1
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'INDTxtDestinationPlace
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtDestinationPlace, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtDestinationPlace, False)
        Me.INDTxtDestinationPlace.EnterMoveNextControl = True
        Me.INDTxtDestinationPlace.Location = New System.Drawing.Point(152, 48)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtDestinationPlace, Presentation.Controls.IndigoTextEdit.EMask.Numerico)
        Me.INDTxtDestinationPlace.Name = "INDTxtDestinationPlace"
        Me.INDTxtDestinationPlace.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDTxtDestinationPlace.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtDestinationPlace.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtDestinationPlace.Properties.Appearance.Options.UseFont = True
        Me.INDTxtDestinationPlace.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtDestinationPlace.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtDestinationPlace.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtDestinationPlace.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtDestinationPlace.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtDestinationPlace.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtDestinationPlace.Properties.Mask.EditMask = "[-a-zA-Z0-9|°¬!ñÑ""#$%&/()=?¡'¿\@¨´_.:,; ]+"
        Me.INDTxtDestinationPlace.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.INDTxtDestinationPlace.Properties.MaxLength = 4
        Me.INDTxtDestinationPlace.Size = New System.Drawing.Size(246, 28)
        Me.INDTxtDestinationPlace.StyleController = Me.LayoutControl1
        Me.INDTxtDestinationPlace.TabIndex = 5
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtDestinationPlace, 0)
        '
        'INDTxtSourcePlace
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtSourcePlace, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtSourcePlace, False)
        Me.INDTxtSourcePlace.EnterMoveNextControl = True
        Me.INDTxtSourcePlace.Location = New System.Drawing.Point(152, 12)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtSourcePlace, Presentation.Controls.IndigoTextEdit.EMask.Numerico)
        Me.INDTxtSourcePlace.Name = "INDTxtSourcePlace"
        Me.INDTxtSourcePlace.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDTxtSourcePlace.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtSourcePlace.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtSourcePlace.Properties.Appearance.Options.UseFont = True
        Me.INDTxtSourcePlace.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtSourcePlace.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtSourcePlace.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtSourcePlace.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtSourcePlace.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtSourcePlace.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtSourcePlace.Properties.Mask.EditMask = "[-a-zA-Z0-9|°¬!ñÑ""#$%&/()=?¡'¿\@¨´_.:,; ]+"
        Me.INDTxtSourcePlace.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.INDTxtSourcePlace.Properties.MaxLength = 4
        Me.INDTxtSourcePlace.Size = New System.Drawing.Size(246, 28)
        Me.INDTxtSourcePlace.StyleController = Me.LayoutControl1
        Me.INDTxtSourcePlace.TabIndex = 4
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtSourcePlace, 0)
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.LayoutControlGroup1.AppearanceGroup.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LayoutControlGroup1.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup1.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LayoutControlGroup1.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup1, False)
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciSourcePlace, Me.INDLciDestinationPlace})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(412, 92)
        Me.LayoutControlGroup1.Text = "LayoutControlGroup1"
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDLciSourcePlace
        '
        Me.INDLciSourcePlace.Control = Me.INDTxtSourcePlace
        Me.INDLciSourcePlace.CustomizationFormText = "Plaza Origen"
        Me.INDLciSourcePlace.Location = New System.Drawing.Point(0, 0)
        Me.INDLciSourcePlace.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDLciSourcePlace.MinSize = New System.Drawing.Size(390, 36)
        Me.INDLciSourcePlace.Name = "INDLciSourcePlace"
        Me.INDLciSourcePlace.Size = New System.Drawing.Size(392, 36)
        Me.INDLciSourcePlace.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciSourcePlace.Text = "Cod. Plaza Origen"
        Me.INDLciSourcePlace.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciSourcePlace.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciSourcePlace.TextToControlDistance = 5
        '
        'INDLciDestinationPlace
        '
        Me.INDLciDestinationPlace.Control = Me.INDTxtDestinationPlace
        Me.INDLciDestinationPlace.CustomizationFormText = "Plaza Destino"
        Me.INDLciDestinationPlace.Location = New System.Drawing.Point(0, 36)
        Me.INDLciDestinationPlace.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDLciDestinationPlace.MinSize = New System.Drawing.Size(390, 36)
        Me.INDLciDestinationPlace.Name = "INDLciDestinationPlace"
        Me.INDLciDestinationPlace.Size = New System.Drawing.Size(392, 36)
        Me.INDLciDestinationPlace.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciDestinationPlace.Text = "Cod. Plaza Destino"
        Me.INDLciDestinationPlace.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciDestinationPlace.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciDestinationPlace.TextToControlDistance = 5
        '
        'IndigoLayoutControl1
        '

        'Me.IndigoLayoutControl1.FormName = "FormBase20150818"

        '
        'FrmOptionalParametersBankFile
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(412, 128)
        Me.Controls.Add(Me.LayoutControl1)
        Me.Controls.Add(Me.PanelControl1)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmOptionalParametersBankFile"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.Text = "Parametros Opcionales"
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.INDTxtDestinationPlace.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtSourcePlace.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciSourcePlace, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciDestinationPlace, System.ComponentModel.ISupportInitialize).EndInit()
        'CType(Me.IndigoLayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDBtnOk As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDTxtDestinationPlace As DevExpress.XtraEditors.TextEdit
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents INDTxtSourcePlace As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents INDLciSourcePlace As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciDestinationPlace As DevExpress.XtraLayout.LayoutControlItem
    'Friend WithEvents IndigoLayoutControl1 As Presentation.Controls.IndigoLayoutControl
End Class
