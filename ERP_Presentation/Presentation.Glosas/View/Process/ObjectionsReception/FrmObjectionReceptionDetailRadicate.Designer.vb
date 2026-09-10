Imports Presentation.Controls
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmObjectionReceptionDetailRadicate
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.INDlyRoot = New DevExpress.XtraLayout.LayoutControl()
        Me.Root = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.INDbtnAccept = New DevExpress.XtraEditors.SimpleButton()
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.INDDeRadicatedDate = New DevExpress.XtraEditors.DateEdit()
        Me.INDLciRadicatedDate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDTeRadicatedReceiver = New DevExpress.XtraEditors.TextEdit()
        Me.INDLciRadicatedReceiver = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDMeRadicatedObservation = New DevExpress.XtraEditors.MemoEdit()
        Me.INDLciRadicatedObservation = New DevExpress.XtraLayout.LayoutControlItem()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyRoot.SuspendLayout()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.INDDeRadicatedDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDeRadicatedDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciRadicatedDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTeRadicatedReceiver.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciRadicatedReceiver, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDMeRadicatedObservation.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciRadicatedObservation, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlyRoot)
        Me.INDPanelControlBase.Controls.Add(Me.PanelControl1)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(426, 318)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(426, 98)
        '
        'BarraBotones
        '
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(426, 98)
        '
        'INDlyRoot
        '
        Me.INDlyRoot.Controls.Add(Me.INDMeRadicatedObservation)
        Me.INDlyRoot.Controls.Add(Me.INDTeRadicatedReceiver)
        Me.INDlyRoot.Controls.Add(Me.INDDeRadicatedDate)
        Me.INDlyRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlyRoot.Location = New System.Drawing.Point(2, 7)
        Me.INDlyRoot.Name = "INDlyRoot"
        Me.INDlyRoot.Root = Me.Root
        Me.INDlyRoot.Size = New System.Drawing.Size(422, 269)
        Me.INDlyRoot.TabIndex = 1
        Me.INDlyRoot.Text = "LayoutControl1"
        '
        'Root
        '
        Me.Root.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Root.AppearanceGroup.Options.UseFont = True
        Me.Root.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Root.AppearanceItemCaption.Options.UseFont = True
        Me.Root.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.Root.AppearanceTabPage.Header.Options.UseFont = True
        Me.Root.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.Root.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.Root.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.Root.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.Root.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.Root.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.Root.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.Root.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.Root, False)
        Me.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.Root.GroupBordersVisible = False
        Me.Root.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciRadicatedDate, Me.INDLciRadicatedReceiver, Me.INDLciRadicatedObservation})
        Me.Root.Name = "Root"
        Me.Root.Size = New System.Drawing.Size(422, 269)
        Me.Root.TextVisible = False
        '
        'INDbtnAccept
        '
        Me.INDbtnAccept.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbtnAccept.Appearance.Options.UseFont = True
        Me.INDbtnAccept.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDbtnAccept.Location = New System.Drawing.Point(2, 2)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDbtnAccept, True)
        Me.INDbtnAccept.Name = "INDbtnAccept"
        Me.INDbtnAccept.Size = New System.Drawing.Size(418, 36)
        Me.INDbtnAccept.TabIndex = 0
        Me.INDbtnAccept.Text = "Aceptar"
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.INDbtnAccept)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelControl1.Location = New System.Drawing.Point(2, 276)
        Me.PanelControl1.MaximumSize = New System.Drawing.Size(0, 40)
        Me.PanelControl1.MinimumSize = New System.Drawing.Size(0, 40)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(422, 40)
        Me.PanelControl1.TabIndex = 0
        '
        'INDDeRadicatedDate
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDDeRadicatedDate, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDDeRadicatedDate, False)
        Me.INDDeRadicatedDate.EditValue = Nothing
        Me.INDDeRadicatedDate.Location = New System.Drawing.Point(12, 38)
        Me.IndigoTextEdit1.SetMascara(Me.INDDeRadicatedDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDDeRadicatedDate.Name = "INDDeRadicatedDate"
        Me.INDDeRadicatedDate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDeRadicatedDate.Properties.Appearance.Options.UseFont = True
        Me.INDDeRadicatedDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDeRadicatedDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDeRadicatedDate.Size = New System.Drawing.Size(386, 28)
        Me.INDDeRadicatedDate.StyleController = Me.INDlyRoot
        Me.INDDeRadicatedDate.TabIndex = 5
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDDeRadicatedDate, 0)
        '
        'INDLciRadicatedDate
        '
        Me.INDLciRadicatedDate.Control = Me.INDDeRadicatedDate
        Me.INDLciRadicatedDate.Location = New System.Drawing.Point(0, 0)
        Me.INDLciRadicatedDate.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciRadicatedDate.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciRadicatedDate.Name = "INDLciRadicatedDate"
        Me.INDLciRadicatedDate.Size = New System.Drawing.Size(402, 60)
        Me.INDLciRadicatedDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciRadicatedDate.Text = "Fecha de Entrega"
        Me.INDLciRadicatedDate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciRadicatedDate.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciRadicatedDate.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciRadicatedDate.TextToControlDistance = 5
        '
        'INDTeRadicatedReceiver
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTeRadicatedReceiver, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTeRadicatedReceiver, False)
        Me.INDTeRadicatedReceiver.Location = New System.Drawing.Point(12, 98)
        Me.IndigoTextEdit1.SetMascara(Me.INDTeRadicatedReceiver, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTeRadicatedReceiver.Name = "INDTeRadicatedReceiver"
        Me.INDTeRadicatedReceiver.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTeRadicatedReceiver.Properties.Appearance.Options.UseFont = True
        Me.INDTeRadicatedReceiver.Properties.MaxLength = 100
        Me.INDTeRadicatedReceiver.Size = New System.Drawing.Size(386, 28)
        Me.INDTeRadicatedReceiver.StyleController = Me.INDlyRoot
        Me.INDTeRadicatedReceiver.TabIndex = 6
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTeRadicatedReceiver, 0)
        '
        'INDLciRadicatedReceiver
        '
        Me.INDLciRadicatedReceiver.Control = Me.INDTeRadicatedReceiver
        Me.INDLciRadicatedReceiver.Location = New System.Drawing.Point(0, 60)
        Me.INDLciRadicatedReceiver.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciRadicatedReceiver.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciRadicatedReceiver.Name = "INDLciRadicatedReceiver"
        Me.INDLciRadicatedReceiver.Size = New System.Drawing.Size(402, 60)
        Me.INDLciRadicatedReceiver.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciRadicatedReceiver.Text = "Recibe"
        Me.INDLciRadicatedReceiver.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciRadicatedReceiver.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciRadicatedReceiver.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciRadicatedReceiver.TextToControlDistance = 5
        '
        'INDMeRadicatedObservation
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDMeRadicatedObservation, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDMeRadicatedObservation, False)
        Me.INDMeRadicatedObservation.Location = New System.Drawing.Point(12, 158)
        Me.IndigoTextEdit1.SetMascara(Me.INDMeRadicatedObservation, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDMeRadicatedObservation.Name = "INDMeRadicatedObservation"
        Me.INDMeRadicatedObservation.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDMeRadicatedObservation.Properties.Appearance.Options.UseFont = True
        Me.INDMeRadicatedObservation.Properties.MaxLength = 200
        Me.INDMeRadicatedObservation.Size = New System.Drawing.Size(398, 99)
        Me.INDMeRadicatedObservation.StyleController = Me.INDlyRoot
        Me.INDMeRadicatedObservation.TabIndex = 7
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDMeRadicatedObservation, 0)
        '
        'INDLciRadicatedObservation
        '
        Me.INDLciRadicatedObservation.Control = Me.INDMeRadicatedObservation
        Me.INDLciRadicatedObservation.Location = New System.Drawing.Point(0, 120)
        Me.INDLciRadicatedObservation.Name = "INDLciRadicatedObservation"
        Me.INDLciRadicatedObservation.Size = New System.Drawing.Size(402, 129)
        Me.INDLciRadicatedObservation.Text = "Observaciones"
        Me.INDLciRadicatedObservation.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciRadicatedObservation.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciRadicatedObservation.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciRadicatedObservation.TextToControlDistance = 5
        '
        'FrmObjectionReceptionDetailRadicate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(426, 440)
        Me.IconOptions.ShowIcon = False
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmObjectionReceptionDetailRadicate"
        Me.Opacity = 1.0R
        Me.Text = "Radicación Respuesta ante EAPB"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyRoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyRoot.ResumeLayout(False)
        CType(Me.Root, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.INDDeRadicatedDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDeRadicatedDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciRadicatedDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTeRadicatedReceiver.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciRadicatedReceiver, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDMeRadicatedObservation.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciRadicatedObservation, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents IndigoTextEdit1 As IndigoTextEdit
    Friend WithEvents IndigoSimpleButton1 As IndigoSimpleButton
    Friend WithEvents IndigoSearchLookUpControl1 As IndigoSearchLookUpControl
    Friend WithEvents IndigoLayoutControlGroup1 As IndigoLayoutControlGroup
    Friend WithEvents IndigoLabelControl1 As IndigoLabelControl
    Friend WithEvents IndigoGroupControl1 As IndigoGroupControl
    Friend WithEvents IndigoGridView1 As IndigoGridView
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDbtnAccept As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDlyRoot As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents Root As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDMeRadicatedObservation As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents INDTeRadicatedReceiver As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDDeRadicatedDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDLciRadicatedDate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciRadicatedReceiver As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciRadicatedObservation As DevExpress.XtraLayout.LayoutControlItem
End Class
