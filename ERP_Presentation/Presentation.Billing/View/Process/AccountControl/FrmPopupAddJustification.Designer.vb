Imports Presentation.Controls
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmPopupAddJustification
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.INDlyRoot = New DevExpress.XtraLayout.LayoutControl()
        Me.INDsleJustification = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit6View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn127 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn128 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLcgProduct = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDliJustification = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.RepositoryItemPopupContainerEdit11 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyRoot.SuspendLayout()
        CType(Me.INDsleJustification.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit6View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgProduct, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliJustification, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit11, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlyRoot)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 135)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(475, 224)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(475, 130)
        '
        'BarraBotones
        '
        Me.BarraBotones.Size = New System.Drawing.Size(475, 130)
        '
        'INDlyRoot
        '
        Me.INDlyRoot.AllowCustomization = False
        Me.INDlyRoot.Controls.Add(Me.INDsleJustification)
        Me.INDlyRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControls.SetIsCustomizable(Me.INDlyRoot, False)
        Me.INDlyRoot.Location = New System.Drawing.Point(2, 7)
        Me.INDlyRoot.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.INDlyRoot.Name = "INDlyRoot"
        Me.INDlyRoot.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(712, 105, 574, 569)
        Me.INDlyRoot.Root = Me.LayoutControlGroup1
        Me.INDlyRoot.Size = New System.Drawing.Size(471, 215)
        Me.INDlyRoot.TabIndex = 2
        Me.INDlyRoot.Text = "LayoutControl1"
        '
        'INDsleJustification
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleJustification, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleJustification, False)
        Me.INDsleJustification.EnterMoveNextControl = True
        Me.INDsleJustification.Location = New System.Drawing.Point(24, 79)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleJustification, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleJustification.Name = "INDsleJustification"
        Me.INDsleJustification.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDsleJustification.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleJustification.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDsleJustification.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleJustification.Properties.Appearance.Options.UseFont = True
        Me.INDsleJustification.Properties.Appearance.Options.UseForeColor = True
        Me.INDsleJustification.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDsleJustification.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDsleJustification.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleJustification.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDsleJustification.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDsleJustification.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDsleJustification.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleJustification.Properties.DisplayMember = "CodeName"
        Me.INDsleJustification.Properties.NullText = ""
        Me.INDsleJustification.Properties.PopupView = Me.SearchLookUpEdit6View
        Me.INDsleJustification.Properties.ShowFooter = False
        Me.INDsleJustification.Properties.ValueMember = "Id"
        Me.INDsleJustification.Size = New System.Drawing.Size(423, 28)
        Me.INDsleJustification.StyleController = Me.INDlyRoot
        Me.INDsleJustification.TabIndex = 5
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleJustification, 0)
        '
        'SearchLookUpEdit6View
        '
        Me.SearchLookUpEdit6View.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.SearchLookUpEdit6View.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.SearchLookUpEdit6View.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.SearchLookUpEdit6View.Appearance.FocusedRow.Options.UseFont = True
        Me.SearchLookUpEdit6View.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit6View.Appearance.GroupRow.Options.UseFont = True
        Me.SearchLookUpEdit6View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit6View.Appearance.HeaderPanel.Options.UseFont = True
        Me.SearchLookUpEdit6View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.SearchLookUpEdit6View.Appearance.Row.Options.UseFont = True
        Me.SearchLookUpEdit6View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn127, Me.GridColumn128})
        Me.SearchLookUpEdit6View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit6View.Name = "SearchLookUpEdit6View"
        Me.SearchLookUpEdit6View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit6View.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEdit6View.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEdit6View.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEdit6View.OptionsView.ShowGroupPanel = False
        '
        'GridColumn127
        '
        Me.GridColumn127.Caption = "Código"
        Me.GridColumn127.FieldName = "Code"
        Me.GridColumn127.Name = "GridColumn127"
        Me.GridColumn127.OptionsColumn.AllowEdit = False
        Me.GridColumn127.OptionsColumn.AllowFocus = False
        Me.GridColumn127.Visible = True
        Me.GridColumn127.VisibleIndex = 0
        '
        'GridColumn128
        '
        Me.GridColumn128.Caption = "Nombre"
        Me.GridColumn128.FieldName = "Description"
        Me.GridColumn128.Name = "GridColumn128"
        Me.GridColumn128.OptionsColumn.AllowEdit = False
        Me.GridColumn128.OptionsColumn.AllowFocus = False
        Me.GridColumn128.Visible = True
        Me.GridColumn128.VisibleIndex = 1
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup1.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup1.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup1, False)
        Me.LayoutControlGroup1.CustomizationFormText = "Agregar Producto"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLcgProduct})
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(471, 215)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDLcgProduct
        '
        Me.INDLcgProduct.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgProduct.AppearanceGroup.Options.UseFont = True
        Me.INDLcgProduct.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgProduct.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgProduct.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgProduct.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgProduct.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgProduct.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgProduct.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgProduct.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgProduct.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgProduct.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgProduct.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgProduct.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgProduct, False)
        Me.INDLcgProduct.CustomizationFormText = "Datos Principales"
        Me.INDLcgProduct.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDliJustification})
        Me.INDLcgProduct.Location = New System.Drawing.Point(0, 0)
        Me.INDLcgProduct.Name = "INDLcgProduct"
        Me.INDLcgProduct.Size = New System.Drawing.Size(451, 195)
        Me.INDLcgProduct.Text = "Datos Principales"
        '
        'INDliJustification
        '
        Me.INDliJustification.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDliJustification.AppearanceItemCaption.Options.UseFont = True
        Me.INDliJustification.Control = Me.INDsleJustification
        Me.INDliJustification.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDliJustification.CustomizationFormText = "Justificación"
        Me.INDliJustification.Location = New System.Drawing.Point(0, 0)
        Me.INDliJustification.MinSize = New System.Drawing.Size(1, 1)
        Me.INDliJustification.Name = "INDliJustification"
        Me.INDliJustification.ShowInCustomizationForm = False
        Me.INDliJustification.Size = New System.Drawing.Size(427, 142)
        Me.INDliJustification.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDliJustification.Text = "Justificación"
        Me.INDliJustification.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDliJustification.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDliJustification.TextSize = New System.Drawing.Size(108, 21)
        Me.INDliJustification.TextToControlDistance = 5
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Opción"
        Me.GridColumn3.FieldName = "Item2"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.OptionsColumn.AllowMove = False
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 0
        '
        'RepositoryItemPopupContainerEdit11
        '
        Me.RepositoryItemPopupContainerEdit11.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit11.Name = "RepositoryItemPopupContainerEdit11"
        '
        'FrmPopupAddJustification
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(475, 359)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.IconOptions.ShowIcon = False
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmPopupAddJustification"
        Me.Opacity = 1.0R
        Me.Text = "Observaciones"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyRoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyRoot.ResumeLayout(False)
        CType(Me.INDsleJustification.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit6View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgProduct, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliJustification, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit11, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents IndigoTextEdit1 As Controls.IndigoTextEdit
    Friend WithEvents IndigoSimpleButton1 As Controls.IndigoSimpleButton
    Friend WithEvents INDlyRoot As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLcgProduct As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoLayoutControlGroup1 As IndigoLayoutControlGroup
    Friend WithEvents INDsleJustification As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit6View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn127 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn128 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDliJustification As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents RepositoryItemPopupContainerEdit11 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
End Class
