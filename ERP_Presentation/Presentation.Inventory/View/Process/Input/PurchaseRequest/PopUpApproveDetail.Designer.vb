<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PopUpApproveDetail
    Inherits Controls.FormBase

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
        Dim AppearanceObject1 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject2 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.CtrNavigationControlPanel1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDLcApproveDetail = New DevExpress.XtraLayout.LayoutControl()
        Me.INDSmbAccept = New DevExpress.XtraEditors.SimpleButton()
        Me.INDMmeObservation = New DevExpress.XtraEditors.MemoEdit()
        Me.INDSleApprove = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDGvApprove = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciAccept = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLcgApproveDetail = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciApprove = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciObservation = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.INDPanelControlBase,System.ComponentModel.ISupportInitialize).BeginInit
        Me.INDPanelControlBase.SuspendLayout
        CType(Me.ToolBars,System.ComponentModel.ISupportInitialize).BeginInit
        Me.ToolBars.SuspendLayout
        CType(Me.LayoutControls,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.CtrNavigationControlPanel1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDLcApproveDetail,System.ComponentModel.ISupportInitialize).BeginInit
        Me.INDLcApproveDetail.SuspendLayout
        CType(Me.INDMmeObservation.Properties,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDSleApprove.Properties,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDGvApprove,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.LayoutControlGroup1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDLciAccept,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDLcgApproveDetail,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDLciApprove,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.INDLciObservation,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.IndigoLayoutControlGroup1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.IndigoTextEdit1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.IndigoSearchLookUpControl1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.IndigoSimpleButton1,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDLcApproveDetail)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControlPanel1)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1008, 607)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = true
        Me.ToolBars.Size = New System.Drawing.Size(1008, 98)
        '
        'BarraBotones
        '
        Me.BarraBotones.LookAndFeel.SkinName = "Blue"
        Me.BarraBotones.OperatingUnitVisible = true
        Me.BarraBotones.Size = New System.Drawing.Size(1008, 98)
        '
        'CtrNavigationControlPanel1
        '
        Me.CtrNavigationControlPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControlPanel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControlPanel1.LayoutControl = Me.INDLcApproveDetail
        Me.CtrNavigationControlPanel1.Location = New System.Drawing.Point(2, 7)
        Me.CtrNavigationControlPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControlPanel1.Name = "CtrNavigationControlPanel1"
        Me.CtrNavigationControlPanel1.Size = New System.Drawing.Size(200, 598)
        Me.CtrNavigationControlPanel1.TabIndex = 0
        Me.CtrNavigationControlPanel1.UseDisabledStatePainter = false
        '
        'INDLcApproveDetail
        '
        Me.INDLcApproveDetail.Controls.Add(Me.INDSmbAccept)
        Me.INDLcApproveDetail.Controls.Add(Me.INDMmeObservation)
        Me.INDLcApproveDetail.Controls.Add(Me.INDSleApprove)
        Me.INDLcApproveDetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDLcApproveDetail.Location = New System.Drawing.Point(202, 7)
        Me.INDLcApproveDetail.Name = "INDLcApproveDetail"
        Me.INDLcApproveDetail.Root = Me.LayoutControlGroup1
        Me.INDLcApproveDetail.Size = New System.Drawing.Size(804, 598)
        Me.INDLcApproveDetail.TabIndex = 1
        Me.INDLcApproveDetail.Text = "LayoutControl1"
        '
        'INDSmbAccept
        '
        Me.INDSmbAccept.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.INDSmbAccept.Appearance.Options.UseFont = true
        Me.INDSmbAccept.Location = New System.Drawing.Point(12, 181)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDSmbAccept, false)
        Me.INDSmbAccept.Name = "INDSmbAccept"
        Me.INDSmbAccept.Size = New System.Drawing.Size(396, 32)
        Me.INDSmbAccept.StyleController = Me.INDLcApproveDetail
        Me.INDSmbAccept.TabIndex = 6
        Me.INDSmbAccept.Text = "Aceptar"
        '
        'INDMmeObservation
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDMmeObservation, true)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDMmeObservation, false)
        Me.INDMmeObservation.EnterMoveNextControl = true
        Me.INDMmeObservation.Location = New System.Drawing.Point(24, 114)
        Me.IndigoTextEdit1.SetMascara(Me.INDMmeObservation, Presentation.Controls.IndigoTextEdit.EMask.AlfaNumerico)
        Me.INDMmeObservation.Name = "INDMmeObservation"
        Me.INDMmeObservation.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDMmeObservation.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.INDMmeObservation.Properties.Appearance.Options.UseBackColor = true
        Me.INDMmeObservation.Properties.Appearance.Options.UseFont = true
        Me.INDMmeObservation.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.INDMmeObservation.Properties.AppearanceFocused.Options.UseFont = true
        Me.INDMmeObservation.Properties.MaxLength = 300
        Me.INDMmeObservation.Size = New System.Drawing.Size(386, 51)
        Me.INDMmeObservation.StyleController = Me.INDLcApproveDetail
        Me.INDMmeObservation.TabIndex = 5
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDMmeObservation, 0)
        '
        'INDSleApprove
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSleApprove, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSleApprove, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSleApprove, false)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSleApprove, true)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSleApprove, true)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSleApprove, false)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSleApprove, false)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSleApprove, false)
        Me.INDSleApprove.EnterMoveNextControl = true
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSleApprove, false)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSleApprove, false)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSleApprove, false)
        Me.INDSleApprove.Location = New System.Drawing.Point(24, 59)
        Me.IndigoTextEdit1.SetMascara(Me.INDSleApprove, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSleApprove.Name = "INDSleApprove"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSleApprove, false)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSleApprove, false)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleApprove, false)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSleApprove, false)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSleApprove, false)
        Me.INDSleApprove.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDSleApprove.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.INDSleApprove.Properties.Appearance.Options.UseBackColor = true
        Me.INDSleApprove.Properties.Appearance.Options.UseFont = true
        Me.INDSleApprove.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.INDSleApprove.Properties.AppearanceFocused.Options.UseFont = true
        Me.INDSleApprove.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "Limpiar selección (Supr)", Nothing, Nothing, true)})
        Me.INDSleApprove.Properties.DisplayMember = "Item2"
        Me.INDSleApprove.Properties.NullText = ""
        Me.INDSleApprove.Properties.PopupSizeable = false
        Me.INDSleApprove.Properties.ShowFooter = false
        Me.INDSleApprove.Properties.ValueMember = "Item1"
        Me.INDSleApprove.Properties.View = Me.INDGvApprove
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleApprove, false)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleApprove, true)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSleApprove, true)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleApprove, true)
        Me.INDSleApprove.Size = New System.Drawing.Size(386, 28)
        Me.INDSleApprove.StyleController = Me.INDLcApproveDetail
        Me.INDSleApprove.TabIndex = 4
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleApprove, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSleApprove, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSleApprove, "{0} - {1}")
        Me.INDSleApprove.ToolTip = "Este Campo es Necesario"
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSleApprove, false)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSleApprove, false)
        '
        'INDGvApprove
        '
        Me.INDGvApprove.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2})
        Me.INDGvApprove.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDGvApprove.Name = "INDGvApprove"
        Me.INDGvApprove.OptionsSelection.EnableAppearanceFocusedCell = false
        Me.INDGvApprove.OptionsView.ShowGroupPanel = false
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.LayoutControlGroup1.AppearanceGroup.Options.UseFont = true
        Me.LayoutControlGroup1.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.LayoutControlGroup1.AppearanceItemCaption.Options.UseFont = true
        Me.LayoutControlGroup1.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.LayoutControlGroup1.AppearanceTabPage.Header.Options.UseFont = true
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12!)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.Options.UseFont = true
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Options.UseFont = true
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Options.UseFont = true
        Me.LayoutControlGroup1.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.LayoutControlGroup1.AppearanceTabPage.PageClient.Options.UseFont = true
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup1, false)
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = false
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciAccept, Me.INDLcgApproveDetail})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(804, 598)
        Me.LayoutControlGroup1.TextVisible = false
        '
        'INDLciAccept
        '
        Me.INDLciAccept.AllowHide = false
        Me.INDLciAccept.Control = Me.INDSmbAccept
        Me.INDLciAccept.Location = New System.Drawing.Point(0, 169)
        Me.INDLciAccept.MaxSize = New System.Drawing.Size(400, 36)
        Me.INDLciAccept.MinSize = New System.Drawing.Size(400, 36)
        Me.INDLciAccept.Name = "INDLciAccept"
        Me.INDLciAccept.ShowInCustomizationForm = false
        Me.INDLciAccept.Size = New System.Drawing.Size(784, 409)
        Me.INDLciAccept.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciAccept.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciAccept.TextVisible = false
        '
        'INDLcgApproveDetail
        '
        Me.INDLcgApproveDetail.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.INDLcgApproveDetail.AppearanceGroup.Options.UseFont = true
        Me.INDLcgApproveDetail.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.INDLcgApproveDetail.AppearanceItemCaption.Options.UseFont = true
        Me.INDLcgApproveDetail.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.INDLcgApproveDetail.AppearanceTabPage.Header.Options.UseFont = true
        Me.INDLcgApproveDetail.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12!)
        Me.INDLcgApproveDetail.AppearanceTabPage.HeaderActive.Options.UseFont = true
        Me.INDLcgApproveDetail.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.INDLcgApproveDetail.AppearanceTabPage.HeaderDisabled.Options.UseFont = true
        Me.INDLcgApproveDetail.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.INDLcgApproveDetail.AppearanceTabPage.HeaderHotTracked.Options.UseFont = true
        Me.INDLcgApproveDetail.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12!)
        Me.INDLcgApproveDetail.AppearanceTabPage.PageClient.Options.UseFont = true
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgApproveDetail, false)
        Me.INDLcgApproveDetail.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciApprove, Me.INDLciObservation})
        Me.INDLcgApproveDetail.Location = New System.Drawing.Point(0, 0)
        Me.INDLcgApproveDetail.Name = "INDLcgApproveDetail"
        Me.INDLcgApproveDetail.Size = New System.Drawing.Size(784, 169)
        Me.INDLcgApproveDetail.Text = "Aprobar"
        '
        'INDLciApprove
        '
        Me.INDLciApprove.AllowHide = false
        Me.INDLciApprove.Control = Me.INDSleApprove
        Me.INDLciApprove.Location = New System.Drawing.Point(0, 0)
        Me.INDLciApprove.MaxSize = New System.Drawing.Size(390, 30)
        Me.INDLciApprove.MinSize = New System.Drawing.Size(390, 30)
        Me.INDLciApprove.Name = "INDLciApprove"
        Me.INDLciApprove.ShowInCustomizationForm = false
        Me.INDLciApprove.Size = New System.Drawing.Size(760, 30)
        Me.INDLciApprove.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciApprove.Text = "Aprobar"
        Me.INDLciApprove.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciApprove.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciApprove.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciApprove.TextToControlDistance = 0
        Me.INDLciApprove.TextVisible = false
        '
        'INDLciObservation
        '
        Me.INDLciObservation.Control = Me.INDMmeObservation
        Me.INDLciObservation.Location = New System.Drawing.Point(0, 30)
        Me.INDLciObservation.MaxSize = New System.Drawing.Size(390, 80)
        Me.INDLciObservation.MinSize = New System.Drawing.Size(390, 80)
        Me.INDLciObservation.Name = "INDLciObservation"
        Me.INDLciObservation.Size = New System.Drawing.Size(760, 80)
        Me.INDLciObservation.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciObservation.Text = "Observaciones"
        Me.INDLciObservation.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciObservation.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciObservation.TextSize = New System.Drawing.Size(50, 20)
        Me.INDLciObservation.TextToControlDistance = 5
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Id"
        Me.GridColumn1.FieldName = "Item1"
        Me.GridColumn1.Name = "GridColumn1"
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Aprobar"
        Me.GridColumn2.FieldName = "Item2"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = true
        Me.GridColumn2.VisibleIndex = 0
        '
        'PopUpApproveDetail
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1008, 729)
        Me.Name = "PopUpApproveDetail"
        Me.Opacity = 1R
        Me.Text = "Aprobar Item"
        CType(Me.INDPanelControlBase,System.ComponentModel.ISupportInitialize).EndInit
        Me.INDPanelControlBase.ResumeLayout(false)
        CType(Me.ToolBars,System.ComponentModel.ISupportInitialize).EndInit
        Me.ToolBars.ResumeLayout(false)
        CType(Me.LayoutControls,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.CtrNavigationControlPanel1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDLcApproveDetail,System.ComponentModel.ISupportInitialize).EndInit
        Me.INDLcApproveDetail.ResumeLayout(false)
        CType(Me.INDMmeObservation.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDSleApprove.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDGvApprove,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlGroup1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDLciAccept,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDLcgApproveDetail,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDLciApprove,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.INDLciObservation,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoLayoutControlGroup1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoTextEdit1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoSearchLookUpControl1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.IndigoSimpleButton1,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)

End Sub

    Friend WithEvents CtrNavigationControlPanel1 As Controls.CtrNavigationControlPanel
    Friend WithEvents INDLcApproveDetail As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDSmbAccept As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDMmeObservation As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents INDSleApprove As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDGvApprove As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLciAccept As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLcgApproveDetail As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLciApprove As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciObservation As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoLayoutControlGroup1 As Controls.IndigoLayoutControlGroup
    Friend WithEvents IndigoTextEdit1 As Controls.IndigoTextEdit
    Friend WithEvents IndigoSearchLookUpControl1 As Controls.IndigoSearchLookUpControl
    Friend WithEvents IndigoSimpleButton1 As Controls.IndigoSimpleButton
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
End Class
