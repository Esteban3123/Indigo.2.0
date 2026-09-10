Imports Presentation.Controls
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmAddCancellationReasons
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
        Dim AppearanceObject3 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject4 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.INDsleCancellationReasons = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDlyRoot = New DevExpress.XtraLayout.LayoutControl()
        Me.INDmemoCancellationReasonsObservations = New DevExpress.XtraEditors.MemoEdit()
        Me.Root = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemCancellationReasons = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemCancellationReasonsObservations = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.INDbtnAddCancellationReason = New DevExpress.XtraEditors.SimpleButton()
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleCancellationReasons.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyRoot.SuspendLayout()
        CType(Me.INDmemoCancellationReasonsObservations.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemCancellationReasons, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemCancellationReasonsObservations, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlyRoot)
        Me.INDPanelControlBase.Controls.Add(Me.PanelControl1)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(421, 298)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(421, 98)
        '
        'BarraBotones
        '
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(421, 98)
        '
        'INDsleCancellationReasons
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleCancellationReasons, AppearanceObject3)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleCancellationReasons, AppearanceObject4)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleCancellationReasons, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleCancellationReasons, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleCancellationReasons, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleCancellationReasons, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleCancellationReasons, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleCancellationReasons, False)
        Me.INDsleCancellationReasons.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleCancellationReasons, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleCancellationReasons, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleCancellationReasons, False)
        Me.INDsleCancellationReasons.Location = New System.Drawing.Point(12, 38)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleCancellationReasons, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleCancellationReasons.Name = "INDsleCancellationReasons"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleCancellationReasons, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleCancellationReasons, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleCancellationReasons, True)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleCancellationReasons, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleCancellationReasons, False)
        Me.INDsleCancellationReasons.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDsleCancellationReasons.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleCancellationReasons.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleCancellationReasons.Properties.Appearance.Options.UseFont = True
        Me.INDsleCancellationReasons.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleCancellationReasons.Properties.DisplayMember = "CodeName"
        Me.INDsleCancellationReasons.Properties.NullText = ""
        Me.INDsleCancellationReasons.Properties.PopupSizeable = False
        Me.INDsleCancellationReasons.Properties.PopupView = Me.SearchLookUpEdit1View
        Me.INDsleCancellationReasons.Properties.ShowFooter = False
        Me.INDsleCancellationReasons.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleCancellationReasons, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleCancellationReasons, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleCancellationReasons, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleCancellationReasons, True)
        Me.INDsleCancellationReasons.Size = New System.Drawing.Size(386, 28)
        Me.INDsleCancellationReasons.StyleController = Me.INDlyRoot
        Me.INDsleCancellationReasons.TabIndex = 0
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleCancellationReasons, "2131")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleCancellationReasons, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleCancellationReasons, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleCancellationReasons, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleCancellationReasons, False)
        '
        'SearchLookUpEdit1View
        '
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.Options.UseFont = True
        Me.SearchLookUpEdit1View.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit1View.Appearance.GroupRow.Options.UseFont = True
        Me.SearchLookUpEdit1View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit1View.Appearance.HeaderPanel.Options.UseFont = True
        Me.SearchLookUpEdit1View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.SearchLookUpEdit1View.Appearance.Row.Options.UseFont = True
        Me.SearchLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2})
        Me.SearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit1View.Name = "SearchLookUpEdit1View"
        Me.SearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEdit1View, False)
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Código"
        Me.GridColumn1.FieldName = "Code"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        Me.GridColumn1.Width = 232
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Nombre"
        Me.GridColumn2.FieldName = "Name"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        Me.GridColumn2.Width = 1150
        '
        'INDlyRoot
        '
        Me.INDlyRoot.Controls.Add(Me.INDmemoCancellationReasonsObservations)
        Me.INDlyRoot.Controls.Add(Me.INDsleCancellationReasons)
        Me.INDlyRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlyRoot.Location = New System.Drawing.Point(2, 7)
        Me.INDlyRoot.Name = "INDlyRoot"
        Me.INDlyRoot.Root = Me.Root
        Me.INDlyRoot.Size = New System.Drawing.Size(417, 249)
        Me.INDlyRoot.TabIndex = 1
        Me.INDlyRoot.Text = "LayoutControl1"
        '
        'INDmemoCancellationReasonsObservations
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDmemoCancellationReasonsObservations, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDmemoCancellationReasonsObservations, False)
        Me.INDmemoCancellationReasonsObservations.Location = New System.Drawing.Point(12, 98)
        Me.IndigoTextEdit1.SetMascara(Me.INDmemoCancellationReasonsObservations, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDmemoCancellationReasonsObservations.Name = "INDmemoCancellationReasonsObservations"
        Me.INDmemoCancellationReasonsObservations.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDmemoCancellationReasonsObservations.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDmemoCancellationReasonsObservations.Properties.Appearance.Options.UseBackColor = True
        Me.INDmemoCancellationReasonsObservations.Properties.Appearance.Options.UseFont = True
        Me.INDmemoCancellationReasonsObservations.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDmemoCancellationReasonsObservations.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDmemoCancellationReasonsObservations.Size = New System.Drawing.Size(386, 120)
        Me.INDmemoCancellationReasonsObservations.StyleController = Me.INDlyRoot
        Me.INDmemoCancellationReasonsObservations.TabIndex = 1
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDmemoCancellationReasonsObservations, 0)
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
        Me.Root.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemCancellationReasons, Me.INDlyItemCancellationReasonsObservations})
        Me.Root.Name = "Root"
        Me.Root.Size = New System.Drawing.Size(417, 249)
        Me.Root.TextVisible = False
        '
        'INDlyItemCancellationReasons
        '
        Me.INDlyItemCancellationReasons.Control = Me.INDsleCancellationReasons
        Me.INDlyItemCancellationReasons.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemCancellationReasons.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemCancellationReasons.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemCancellationReasons.Name = "INDlyItemCancellationReasons"
        Me.INDlyItemCancellationReasons.Size = New System.Drawing.Size(397, 60)
        Me.INDlyItemCancellationReasons.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemCancellationReasons.Text = "Motivo"
        Me.INDlyItemCancellationReasons.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemCancellationReasons.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemCancellationReasons.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemCancellationReasons.TextToControlDistance = 5
        '
        'INDlyItemCancellationReasonsObservations
        '
        Me.INDlyItemCancellationReasonsObservations.Control = Me.INDmemoCancellationReasonsObservations
        Me.INDlyItemCancellationReasonsObservations.Location = New System.Drawing.Point(0, 60)
        Me.INDlyItemCancellationReasonsObservations.MaxSize = New System.Drawing.Size(390, 150)
        Me.INDlyItemCancellationReasonsObservations.MinSize = New System.Drawing.Size(390, 150)
        Me.INDlyItemCancellationReasonsObservations.Name = "INDlyItemCancellationReasonsObservations"
        Me.INDlyItemCancellationReasonsObservations.Size = New System.Drawing.Size(397, 169)
        Me.INDlyItemCancellationReasonsObservations.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemCancellationReasonsObservations.Text = "Descripción"
        Me.INDlyItemCancellationReasonsObservations.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemCancellationReasonsObservations.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemCancellationReasonsObservations.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemCancellationReasonsObservations.TextToControlDistance = 5
        '
        'INDbtnAddCancellationReason
        '
        Me.INDbtnAddCancellationReason.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbtnAddCancellationReason.Appearance.Options.UseFont = True
        Me.INDbtnAddCancellationReason.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDbtnAddCancellationReason.Location = New System.Drawing.Point(2, 2)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDbtnAddCancellationReason, True)
        Me.INDbtnAddCancellationReason.Name = "INDbtnAddCancellationReason"
        Me.INDbtnAddCancellationReason.Size = New System.Drawing.Size(413, 36)
        Me.INDbtnAddCancellationReason.TabIndex = 0
        Me.INDbtnAddCancellationReason.Text = "Aceptar"
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.INDbtnAddCancellationReason)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelControl1.Location = New System.Drawing.Point(2, 256)
        Me.PanelControl1.MaximumSize = New System.Drawing.Size(0, 40)
        Me.PanelControl1.MinimumSize = New System.Drawing.Size(0, 40)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(417, 40)
        Me.PanelControl1.TabIndex = 0
        '
        'FrmAddCancellationReasons
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(421, 420)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmAddCancellationReasons"
        Me.Opacity = 1.0R
        Me.Text = "Motivo de Cancelación"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleCancellationReasons.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyRoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyRoot.ResumeLayout(False)
        CType(Me.INDmemoCancellationReasonsObservations.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemCancellationReasons, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemCancellationReasonsObservations, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents IndigoTextEdit1 As IndigoTextEdit
    Friend WithEvents IndigoSimpleButton1 As IndigoSimpleButton
    Friend WithEvents IndigoSearchLookUpControl1 As IndigoSearchLookUpControl
    Friend WithEvents IndigoLayoutControlGroup1 As IndigoLayoutControlGroup
    Friend WithEvents IndigoLabelControl1 As IndigoLabelControl
    Friend WithEvents IndigoGroupControl1 As IndigoGroupControl
    Friend WithEvents IndigoGridView1 As IndigoGridView
    Friend WithEvents IndigoGridControl1 As IndigoGridControl
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDbtnAddCancellationReason As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDlyRoot As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents Root As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDmemoCancellationReasonsObservations As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents INDsleCancellationReasons As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlyItemCancellationReasons As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemCancellationReasonsObservations As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
End Class
