Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmNoveltyGroundsProperties
    Inherits FormBase

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.INDNcp = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDLcBase = New DevExpress.XtraLayout.LayoutControl()
        Me.INDTextEAgreementOrResolution = New DevExpress.XtraEditors.TextEdit()
        Me.INDTextEName = New DevExpress.XtraEditors.TextEdit()
        Me.INDBeCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.INDLcgBase = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLcgTypeResolution = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciName = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciAgreementOrResolution = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl()
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDNcp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLcBase.SuspendLayout()
        CType(Me.INDTextEAgreementOrResolution.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTextEName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDBeCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgBase, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgTypeResolution, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciAgreementOrResolution, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDLcBase)
        Me.INDPanelControlBase.Controls.Add(Me.INDNcp)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 118)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1465, 583)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        '
        'BarraBotones
        '
        Me.BarraBotones.LookAndFeel.SkinName = "Blue"
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(1465, 94)
        '
        'INDNcp
        '
        Me.INDNcp.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDNcp.Dock = System.Windows.Forms.DockStyle.Left
        Me.INDNcp.LayoutControl = Me.INDLcBase
        Me.INDNcp.Location = New System.Drawing.Point(2, 7)
        Me.INDNcp.Margin = New System.Windows.Forms.Padding(0)
        Me.INDNcp.Name = "INDNcp"
        Me.INDNcp.Size = New System.Drawing.Size(200, 574)
        Me.INDNcp.TabIndex = 0
        Me.INDNcp.UseDisabledStatePainter = False
        '
        'INDLcBase
        '
        Me.INDLcBase.Controls.Add(Me.INDTextEAgreementOrResolution)
        Me.INDLcBase.Controls.Add(Me.INDTextEName)
        Me.INDLcBase.Controls.Add(Me.INDBeCode)
        Me.INDLcBase.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDLcBase.Location = New System.Drawing.Point(202, 7)
        Me.INDLcBase.Name = "INDLcBase"
        Me.INDLcBase.Root = Me.INDLcgBase
        Me.INDLcBase.Size = New System.Drawing.Size(1261, 574)
        Me.INDLcBase.TabIndex = 1
        Me.INDLcBase.Text = "LayoutControl1"
        '
        'INDTextEAgreementOrResolution
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTextEAgreementOrResolution, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTextEAgreementOrResolution, False)
        Me.INDTextEAgreementOrResolution.EnterMoveNextControl = True
        Me.INDTextEAgreementOrResolution.Location = New System.Drawing.Point(24, 211)
        Me.IndigoTextEdit1.SetMascara(Me.INDTextEAgreementOrResolution, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTextEAgreementOrResolution.Name = "INDTextEAgreementOrResolution"
        Me.INDTextEAgreementOrResolution.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTextEAgreementOrResolution.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTextEAgreementOrResolution.Properties.Appearance.Options.UseBackColor = True
        Me.INDTextEAgreementOrResolution.Properties.Appearance.Options.UseFont = True
        Me.INDTextEAgreementOrResolution.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTextEAgreementOrResolution.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTextEAgreementOrResolution.Size = New System.Drawing.Size(386, 28)
        Me.INDTextEAgreementOrResolution.StyleController = Me.INDLcBase
        Me.INDTextEAgreementOrResolution.TabIndex = 3
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTextEAgreementOrResolution, 0)
        '
        'INDTextEName
        '
        Me.INDTextEName.AllowHtmlTextInToolTip = DevExpress.Utils.DefaultBoolean.[False]
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTextEName, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTextEName, False)
        Me.INDTextEName.EnterMoveNextControl = True
        Me.INDTextEName.Location = New System.Drawing.Point(24, 147)
        Me.IndigoTextEdit1.SetMascara(Me.INDTextEName, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTextEName.Name = "INDTextEName"
        Me.INDTextEName.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTextEName.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTextEName.Properties.Appearance.Options.UseBackColor = True
        Me.INDTextEName.Properties.Appearance.Options.UseFont = True
        Me.INDTextEName.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTextEName.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTextEName.Size = New System.Drawing.Size(386, 28)
        Me.INDTextEName.StyleController = Me.INDLcBase
        Me.INDTextEName.TabIndex = 2
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTextEName, 0)
        '
        'INDBeCode
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDBeCode, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDBeCode, False)
        Me.INDBeCode.EnterMoveNextControl = True
        Me.INDBeCode.Location = New System.Drawing.Point(24, 83)
        Me.IndigoTextEdit1.SetMascara(Me.INDBeCode, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDBeCode.Name = "INDBeCode"
        Me.INDBeCode.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDBeCode.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBeCode.Properties.Appearance.Options.UseBackColor = True
        Me.INDBeCode.Properties.Appearance.Options.UseFont = True
        Me.INDBeCode.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBeCode.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDBeCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Global.Presentation.Taxes.My.Resources.Resources.BuscarMetro, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", Nothing, Nothing, True)})
        Me.INDBeCode.Size = New System.Drawing.Size(386, 28)
        Me.INDBeCode.StyleController = Me.INDLcBase
        Me.INDBeCode.TabIndex = 1
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDBeCode, 0)
        '
        'INDLcgBase
        '
        Me.INDLcgBase.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgBase.AppearanceGroup.Options.UseFont = True
        Me.INDLcgBase.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgBase.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgBase.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgBase.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgBase.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgBase.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgBase.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgBase.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgBase.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgBase.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgBase.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgBase.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgBase, False)
        Me.INDLcgBase.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDLcgBase.GroupBordersVisible = False
        Me.INDLcgBase.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLcgTypeResolution})
        Me.INDLcgBase.Location = New System.Drawing.Point(0, 0)
        Me.INDLcgBase.Name = "INDLcgBase"
        Me.INDLcgBase.Size = New System.Drawing.Size(1261, 574)
        Me.INDLcgBase.TextVisible = False
        '
        'INDLcgTypeResolution
        '
        Me.INDLcgTypeResolution.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgTypeResolution.AppearanceGroup.Options.UseFont = True
        Me.INDLcgTypeResolution.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgTypeResolution.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgTypeResolution.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgTypeResolution.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgTypeResolution.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgTypeResolution.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgTypeResolution.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgTypeResolution.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgTypeResolution.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgTypeResolution.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgTypeResolution.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgTypeResolution.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgTypeResolution, False)
        Me.INDLcgTypeResolution.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciCode, Me.INDLciName, Me.INDLciAgreementOrResolution})
        Me.INDLcgTypeResolution.Location = New System.Drawing.Point(0, 0)
        Me.INDLcgTypeResolution.Name = "INDLcgTypeResolution"
        Me.INDLcgTypeResolution.Size = New System.Drawing.Size(1241, 554)
        Me.INDLcgTypeResolution.Text = "Datos Generales"
        '
        'INDLciCode
        '
        Me.INDLciCode.Control = Me.INDBeCode
        Me.INDLciCode.Location = New System.Drawing.Point(0, 0)
        Me.INDLciCode.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLciCode.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLciCode.Name = "INDLciCode"
        Me.INDLciCode.Size = New System.Drawing.Size(1217, 64)
        Me.INDLciCode.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciCode.Text = "Codigo"
        Me.INDLciCode.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciCode.TextSize = New System.Drawing.Size(150, 21)
        '
        'INDLciName
        '
        Me.INDLciName.Control = Me.INDTextEName
        Me.INDLciName.Location = New System.Drawing.Point(0, 64)
        Me.INDLciName.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLciName.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLciName.Name = "INDLciName"
        Me.INDLciName.Size = New System.Drawing.Size(1217, 64)
        Me.INDLciName.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciName.Text = "Nombre"
        Me.INDLciName.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciName.TextSize = New System.Drawing.Size(150, 21)
        '
        'INDLciAgreementOrResolution
        '
        Me.INDLciAgreementOrResolution.Control = Me.INDTextEAgreementOrResolution
        Me.INDLciAgreementOrResolution.Location = New System.Drawing.Point(0, 128)
        Me.INDLciAgreementOrResolution.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLciAgreementOrResolution.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLciAgreementOrResolution.Name = "INDLciAgreementOrResolution"
        Me.INDLciAgreementOrResolution.Size = New System.Drawing.Size(1217, 367)
        Me.INDLciAgreementOrResolution.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciAgreementOrResolution.Text = "Acuerdo ó Resolución:"
        Me.INDLciAgreementOrResolution.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciAgreementOrResolution.TextSize = New System.Drawing.Size(150, 21)
        '
        'FrmNoveltyGroundsProperties
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1465, 701)
        Me.Name = "FrmNoveltyGroundsProperties"
        Me.Opacity = 1.0R
        Me.Tag = "1841"
        Me.Text = "Motivos de Novedad de Predios"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDNcp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLcBase.ResumeLayout(False)
        CType(Me.INDTextEAgreementOrResolution.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTextEName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDBeCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgBase, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgTypeResolution, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciAgreementOrResolution, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDNcp As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents INDLcBase As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDLcgBase As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDBeCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents INDLcgTypeResolution As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLciCode As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDTextEName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDLciName As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDTextEAgreementOrResolution As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDLciAgreementOrResolution As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
    Friend WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
End Class
