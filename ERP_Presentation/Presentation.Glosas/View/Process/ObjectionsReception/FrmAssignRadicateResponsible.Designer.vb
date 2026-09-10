Imports Presentation.Controls
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmAssignRadicateResponsible
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
        Dim AppearanceObject1 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject2 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.INDSleRadicateResponsibleId = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDGvRadicateResponsibleId = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDGvRadicateResponsibleId_Code = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvRadicateResponsibleId_Name = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDlyRoot = New DevExpress.XtraLayout.LayoutControl()
        Me.Root = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciRadicateResponsibleId = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.INDbtnAccept = New DevExpress.XtraEditors.SimpleButton()
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleRadicateResponsibleId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvRadicateResponsibleId, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyRoot.SuspendLayout()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciRadicateResponsibleId, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlyRoot)
        Me.INDPanelControlBase.Controls.Add(Me.PanelControl1)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(426, 161)
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
        'INDSleRadicateResponsibleId
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSleRadicateResponsibleId, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSleRadicateResponsibleId, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSleRadicateResponsibleId, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSleRadicateResponsibleId, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSleRadicateResponsibleId, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSleRadicateResponsibleId, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSleRadicateResponsibleId, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSleRadicateResponsibleId, False)
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSleRadicateResponsibleId, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSleRadicateResponsibleId, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSleRadicateResponsibleId, False)
        Me.INDSleRadicateResponsibleId.Location = New System.Drawing.Point(12, 38)
        Me.IndigoTextEdit1.SetMascara(Me.INDSleRadicateResponsibleId, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSleRadicateResponsibleId.Name = "INDSleRadicateResponsibleId"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSleRadicateResponsibleId, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSleRadicateResponsibleId, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleRadicateResponsibleId, True)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDSleRadicateResponsibleId, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSleRadicateResponsibleId, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSleRadicateResponsibleId, False)
        Me.INDSleRadicateResponsibleId.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDSleRadicateResponsibleId.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSleRadicateResponsibleId.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleRadicateResponsibleId.Properties.Appearance.Options.UseFont = True
        Me.INDSleRadicateResponsibleId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleRadicateResponsibleId.Properties.DisplayMember = "responsibleCodeName"
        Me.INDSleRadicateResponsibleId.Properties.NullText = ""
        Me.INDSleRadicateResponsibleId.Properties.PopupSizeable = False
        Me.INDSleRadicateResponsibleId.Properties.PopupView = Me.INDGvRadicateResponsibleId
        Me.INDSleRadicateResponsibleId.Properties.ShowFooter = False
        Me.INDSleRadicateResponsibleId.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleRadicateResponsibleId, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleRadicateResponsibleId, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSleRadicateResponsibleId, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleRadicateResponsibleId, True)
        Me.INDSleRadicateResponsibleId.Size = New System.Drawing.Size(386, 28)
        Me.INDSleRadicateResponsibleId.StyleController = Me.INDlyRoot
        Me.INDSleRadicateResponsibleId.TabIndex = 4
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleRadicateResponsibleId, "2135")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSleRadicateResponsibleId, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSleRadicateResponsibleId, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSleRadicateResponsibleId, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSleRadicateResponsibleId, False)
        '
        'INDGvRadicateResponsibleId
        '
        Me.INDGvRadicateResponsibleId.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvRadicateResponsibleId.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvRadicateResponsibleId.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvRadicateResponsibleId.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvRadicateResponsibleId.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvRadicateResponsibleId.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvRadicateResponsibleId.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvRadicateResponsibleId.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvRadicateResponsibleId.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvRadicateResponsibleId.Appearance.Row.Options.UseFont = True
        Me.INDGvRadicateResponsibleId.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDGvRadicateResponsibleId_Code, Me.INDGvRadicateResponsibleId_Name})
        Me.INDGvRadicateResponsibleId.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDGvRadicateResponsibleId.Name = "INDGvRadicateResponsibleId"
        Me.INDGvRadicateResponsibleId.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDGvRadicateResponsibleId.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvRadicateResponsibleId.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvRadicateResponsibleId.OptionsView.ShowAutoFilterRow = True
        Me.INDGvRadicateResponsibleId.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvRadicateResponsibleId, False)
        '
        'INDGvRadicateResponsibleId_Code
        '
        Me.INDGvRadicateResponsibleId_Code.Caption = "Código"
        Me.INDGvRadicateResponsibleId_Code.FieldName = "responsibleCode"
        Me.INDGvRadicateResponsibleId_Code.Name = "INDGvRadicateResponsibleId_Code"
        Me.INDGvRadicateResponsibleId_Code.Visible = True
        Me.INDGvRadicateResponsibleId_Code.VisibleIndex = 0
        Me.INDGvRadicateResponsibleId_Code.Width = 342
        '
        'INDGvRadicateResponsibleId_Name
        '
        Me.INDGvRadicateResponsibleId_Name.Caption = "Nombre"
        Me.INDGvRadicateResponsibleId_Name.FieldName = "responsibleName"
        Me.INDGvRadicateResponsibleId_Name.Name = "INDGvRadicateResponsibleId_Name"
        Me.INDGvRadicateResponsibleId_Name.Visible = True
        Me.INDGvRadicateResponsibleId_Name.VisibleIndex = 1
        Me.INDGvRadicateResponsibleId_Name.Width = 1040
        '
        'INDlyRoot
        '
        Me.INDlyRoot.Controls.Add(Me.INDSleRadicateResponsibleId)
        Me.INDlyRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlyRoot.Location = New System.Drawing.Point(2, 7)
        Me.INDlyRoot.Name = "INDlyRoot"
        Me.INDlyRoot.Root = Me.Root
        Me.INDlyRoot.Size = New System.Drawing.Size(422, 112)
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
        Me.Root.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciRadicateResponsibleId})
        Me.Root.Name = "Root"
        Me.Root.Size = New System.Drawing.Size(422, 112)
        Me.Root.TextVisible = False
        '
        'INDLciRadicateResponsibleId
        '
        Me.INDLciRadicateResponsibleId.Control = Me.INDSleRadicateResponsibleId
        Me.INDLciRadicateResponsibleId.Location = New System.Drawing.Point(0, 0)
        Me.INDLciRadicateResponsibleId.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciRadicateResponsibleId.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciRadicateResponsibleId.Name = "INDLciRadicateResponsibleId"
        Me.INDLciRadicateResponsibleId.Size = New System.Drawing.Size(402, 92)
        Me.INDLciRadicateResponsibleId.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciRadicateResponsibleId.Text = "Responsable"
        Me.INDLciRadicateResponsibleId.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciRadicateResponsibleId.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciRadicateResponsibleId.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciRadicateResponsibleId.TextToControlDistance = 5
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
        Me.PanelControl1.Location = New System.Drawing.Point(2, 119)
        Me.PanelControl1.MaximumSize = New System.Drawing.Size(0, 40)
        Me.PanelControl1.MinimumSize = New System.Drawing.Size(0, 40)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(422, 40)
        Me.PanelControl1.TabIndex = 0
        '
        'FrmAssignRadicateResponsible
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(426, 283)
        Me.IconOptions.ShowIcon = False
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmAssignRadicateResponsible"
        Me.Opacity = 1.0R
        Me.Text = "Asignación de Responsable Radicación Respuesta EAPB"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleRadicateResponsibleId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvRadicateResponsibleId, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyRoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyRoot.ResumeLayout(False)
        CType(Me.Root, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciRadicateResponsibleId, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents INDbtnAccept As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDlyRoot As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents Root As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDSleRadicateResponsibleId As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDGvRadicateResponsibleId As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLciRadicateResponsibleId As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDGvRadicateResponsibleId_Code As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvRadicateResponsibleId_Name As DevExpress.XtraGrid.Columns.GridColumn
End Class
