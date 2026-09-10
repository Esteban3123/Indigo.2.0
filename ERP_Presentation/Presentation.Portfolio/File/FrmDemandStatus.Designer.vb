Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmDemandStatus
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
        Me.components = New System.ComponentModel.Container()
        Dim EditorButtonImageOptions1 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.INDNcpDemandStatus = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDLcDemandStatus = New DevExpress.XtraLayout.LayoutControl()
        Me.INDTxeDescription = New DevExpress.XtraEditors.TextEdit()
        Me.INDBtnCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.INDLcgDemandStatus = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLcgGeneralData = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciDescription = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoGridLookUpControl1 = New Presentation.Controls.IndigoGridLookUpControl(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDNcpDemandStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcDemandStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLcDemandStatus.SuspendLayout()
        CType(Me.INDTxeDescription.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDBtnCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgDemandStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgGeneralData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDLcDemandStatus)
        Me.INDPanelControlBase.Controls.Add(Me.INDNcpDemandStatus)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 135)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1008, 594)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(1008, 130)
        '
        'BarraBotones
        '
        Me.BarraBotones.Size = New System.Drawing.Size(1008, 130)
        '
        'INDNcpDemandStatus
        '
        Me.INDNcpDemandStatus.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDNcpDemandStatus.Dock = System.Windows.Forms.DockStyle.Left
        Me.INDNcpDemandStatus.LayoutControl = Me.INDLcDemandStatus
        Me.INDNcpDemandStatus.Location = New System.Drawing.Point(2, 7)
        Me.INDNcpDemandStatus.Margin = New System.Windows.Forms.Padding(0)
        Me.INDNcpDemandStatus.Name = "INDNcpDemandStatus"
        Me.INDNcpDemandStatus.Size = New System.Drawing.Size(200, 585)
        Me.INDNcpDemandStatus.TabIndex = 0
        Me.INDNcpDemandStatus.UseDisabledStatePainter = False
        '
        'INDLcDemandStatus
        '
        Me.INDLcDemandStatus.Controls.Add(Me.INDTxeDescription)
        Me.INDLcDemandStatus.Controls.Add(Me.INDBtnCode)
        Me.INDLcDemandStatus.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDLcDemandStatus.Location = New System.Drawing.Point(202, 7)
        Me.INDLcDemandStatus.Name = "INDLcDemandStatus"
        Me.INDLcDemandStatus.Root = Me.INDLcgDemandStatus
        Me.INDLcDemandStatus.Size = New System.Drawing.Size(804, 585)
        Me.INDLcDemandStatus.TabIndex = 1
        Me.INDLcDemandStatus.Text = "LayoutControl1"
        '
        'INDTxeDescription
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxeDescription, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxeDescription, True)
        Me.INDTxeDescription.EnterMoveNextControl = True
        Me.INDTxeDescription.Location = New System.Drawing.Point(24, 143)
        Me.INDTxeDescription.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxeDescription, Presentation.Controls.IndigoTextEdit.EMask.AlfaNumerico)
        Me.INDTxeDescription.Name = "INDTxeDescription"
        Me.INDTxeDescription.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDTxeDescription.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxeDescription.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDTxeDescription.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxeDescription.Properties.Appearance.Options.UseFont = True
        Me.INDTxeDescription.Properties.Appearance.Options.UseForeColor = True
        Me.INDTxeDescription.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxeDescription.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxeDescription.Properties.Mask.EditMask = "[-a-zA-Z0-9|°¬!ñÑ""#$%&/()=?¡'¿\@¨´_.:,; ]+"
        Me.INDTxeDescription.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.INDTxeDescription.Properties.MaxLength = 100
        Me.INDTxeDescription.Size = New System.Drawing.Size(386, 28)
        Me.INDTxeDescription.StyleController = Me.INDLcDemandStatus
        Me.INDTxeDescription.TabIndex = 5
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxeDescription, 0)
        Me.INDTxeDescription.ToolTip = "Este Campo es Necesario"
        '
        'INDBtnCode
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDBtnCode, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDBtnCode, True)
        Me.INDBtnCode.Location = New System.Drawing.Point(24, 79)
        Me.IndigoTextEdit1.SetMascara(Me.INDBtnCode, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDBtnCode.Name = "INDBtnCode"
        Me.INDBtnCode.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDBtnCode.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBtnCode.Properties.Appearance.Options.UseBackColor = True
        Me.INDBtnCode.Properties.Appearance.Options.UseFont = True
        EditorButtonImageOptions1.Image = Global.Presentation.Portfolio.My.Resources.Resources.BuscarMetro
        Me.INDBtnCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, True, True, False, EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, "", Nothing, Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.INDBtnCode.Properties.MaxLength = 20
        Me.INDBtnCode.Size = New System.Drawing.Size(386, 28)
        Me.INDBtnCode.StyleController = Me.INDLcDemandStatus
        Me.INDBtnCode.TabIndex = 4
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDBtnCode, 0)
        Me.INDBtnCode.ToolTip = "Este Campo es Necesario"
        '
        'INDLcgDemandStatus
        '
        Me.INDLcgDemandStatus.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgDemandStatus.AppearanceGroup.Options.UseFont = True
        Me.INDLcgDemandStatus.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgDemandStatus.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgDemandStatus.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgDemandStatus.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgDemandStatus.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgDemandStatus.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgDemandStatus.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgDemandStatus.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgDemandStatus.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgDemandStatus.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgDemandStatus.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgDemandStatus.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgDemandStatus, False)
        Me.INDLcgDemandStatus.CustomizationFormText = "Estados de demanda"
        Me.INDLcgDemandStatus.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDLcgDemandStatus.GroupBordersVisible = False
        Me.INDLcgDemandStatus.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLcgGeneralData})
        Me.INDLcgDemandStatus.Name = "INDLcgDemandStatus"
        Me.INDLcgDemandStatus.Size = New System.Drawing.Size(804, 585)
        Me.INDLcgDemandStatus.TextVisible = False
        '
        'INDLcgGeneralData
        '
        Me.INDLcgGeneralData.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgGeneralData.AppearanceGroup.Options.UseFont = True
        Me.INDLcgGeneralData.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgGeneralData.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgGeneralData.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgGeneralData.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgGeneralData.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgGeneralData.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgGeneralData.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgGeneralData.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgGeneralData.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgGeneralData.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgGeneralData.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgGeneralData.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgGeneralData, False)
        Me.INDLcgGeneralData.CustomizationFormText = "Datos generales"
        Me.INDLcgGeneralData.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciCode, Me.INDLciDescription})
        Me.INDLcgGeneralData.Location = New System.Drawing.Point(0, 0)
        Me.INDLcgGeneralData.Name = "INDLcgGeneralData"
        Me.INDLcgGeneralData.Size = New System.Drawing.Size(784, 565)
        Me.INDLcgGeneralData.Text = "Datos generales"
        '
        'INDLciCode
        '
        Me.INDLciCode.AllowHide = False
        Me.INDLciCode.Control = Me.INDBtnCode
        Me.INDLciCode.CustomizationFormText = "Código"
        Me.INDLciCode.Location = New System.Drawing.Point(0, 0)
        Me.INDLciCode.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLciCode.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLciCode.Name = "INDLciCode"
        Me.INDLciCode.ShowInCustomizationForm = False
        Me.INDLciCode.Size = New System.Drawing.Size(760, 64)
        Me.INDLciCode.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciCode.Text = "Código"
        Me.INDLciCode.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciCode.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciCode.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciCode.TextToControlDistance = 5
        '
        'INDLciDescription
        '
        Me.INDLciDescription.AllowHide = False
        Me.INDLciDescription.Control = Me.INDTxeDescription
        Me.INDLciDescription.CustomizationFormText = "Descripción"
        Me.INDLciDescription.Location = New System.Drawing.Point(0, 64)
        Me.INDLciDescription.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLciDescription.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLciDescription.Name = "INDLciDescription"
        Me.INDLciDescription.ShowInCustomizationForm = False
        Me.INDLciDescription.Size = New System.Drawing.Size(760, 448)
        Me.INDLciDescription.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciDescription.Text = "Descripción"
        Me.INDLciDescription.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciDescription.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciDescription.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciDescription.TextToControlDistance = 5
        '
        'FrmDemandStatus
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1008, 729)
        Me.IconOptions.ShowIcon = False
        Me.Name = "FrmDemandStatus"
        Me.Opacity = 1R
        Me.Tag = "2074"
        Me.Text = "Estados de demanda"
        CType(Me.INDPanelControlBase,System.ComponentModel.ISupportInitialize).EndInit
        Me.INDPanelControlBase.ResumeLayout(false)
        CType(Me.ToolBars,System.ComponentModel.ISupportInitialize).EndInit
        Me.ToolBars.ResumeLayout(false)
        CType(Me.LayoutControls,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDNcpDemandStatus,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDLcDemandStatus,System.ComponentModel.ISupportInitialize).EndInit
        Me.INDLcDemandStatus.ResumeLayout(false)
        CType(Me.INDTxeDescription.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDBtnCode.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDLcgDemandStatus,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDLcgGeneralData,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDLciCode,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDLciDescription,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoTextEdit1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoGridLookUpControl1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoSearchLookUpControl1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoLayoutControlGroup1,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)

End Sub

    Friend WithEvents INDNcpDemandStatus As CtrNavigationControlPanel
    Friend WithEvents INDLcDemandStatus As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDTxeDescription As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDBtnCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents INDLcgDemandStatus As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLciCode As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciDescription As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoTextEdit1 As IndigoTextEdit
    Friend WithEvents IndigoGridLookUpControl1 As IndigoGridLookUpControl
    Friend WithEvents IndigoSearchLookUpControl1 As IndigoSearchLookUpControl
    Friend WithEvents IndigoLayoutControlGroup1 As IndigoLayoutControlGroup
    Friend WithEvents INDLcgGeneralData As DevExpress.XtraLayout.LayoutControlGroup
End Class
