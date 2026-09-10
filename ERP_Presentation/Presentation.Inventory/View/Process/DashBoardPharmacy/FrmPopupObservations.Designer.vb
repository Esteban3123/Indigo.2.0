Imports Presentation.Controls
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmPopupObservations
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
        Dim AppearanceObject1 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject2 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Me.INDlyRoot = New DevExpress.XtraLayout.LayoutControl()
        Me.INDMeDescription = New DevExpress.XtraEditors.MemoEdit()
        Me.INDsleReasonType = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit6View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn127 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn128 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLcgProduct = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLcDescription = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDliReasonType = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoSearchLookUpControl11 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.RepositoryItemPopupContainerEdit11 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyRoot.SuspendLayout()
        CType(Me.INDMeDescription.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleReasonType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit6View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgProduct, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliReasonType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit11, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlyRoot)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 135)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(666, 401)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(666, 130)
        '
        'BarraBotones
        '
        Me.BarraBotones.Size = New System.Drawing.Size(666, 130)
        '
        'INDlyRoot
        '
        Me.INDlyRoot.AllowCustomization = False
        Me.INDlyRoot.Controls.Add(Me.INDMeDescription)
        Me.INDlyRoot.Controls.Add(Me.INDsleReasonType)
        Me.INDlyRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControls.SetIsCustomizable(Me.INDlyRoot, False)
        Me.INDlyRoot.Location = New System.Drawing.Point(2, 7)
        Me.INDlyRoot.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.INDlyRoot.Name = "INDlyRoot"
        Me.INDlyRoot.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(712, 105, 574, 569)
        Me.INDlyRoot.Root = Me.LayoutControlGroup1
        Me.INDlyRoot.Size = New System.Drawing.Size(662, 392)
        Me.INDlyRoot.TabIndex = 2
        Me.INDlyRoot.Text = "LayoutControl1"
        '
        'INDMeDescription
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDMeDescription, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDMeDescription, False)
        Me.INDMeDescription.Location = New System.Drawing.Point(24, 148)
        Me.IndigoTextEdit1.SetMascara(Me.INDMeDescription, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDMeDescription.Name = "INDMeDescription"
        Me.INDMeDescription.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Semilight", 17.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDMeDescription.Properties.Appearance.Options.UseFont = True
        Me.INDMeDescription.Properties.MaxLength = 200
        Me.INDMeDescription.Size = New System.Drawing.Size(614, 220)
        Me.INDMeDescription.StyleController = Me.INDlyRoot
        Me.INDMeDescription.TabIndex = 10
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDMeDescription, 0)
        '
        'INDsleReasonType
        '
        Me.IndigoSearchLookUpControl11.SetAppearanceEmbeddedNavigator(Me.INDsleReasonType, AppearanceObject1)
        Me.IndigoSearchLookUpControl11.SetAppearanceTextFindControl(Me.INDsleReasonType, AppearanceObject2)
        Me.IndigoSearchLookUpControl11.SetAppendButtonNavigator(Me.INDsleReasonType, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleReasonType, False)
        Me.IndigoSearchLookUpControl11.SetAutomaticOpenForm(Me.INDsleReasonType, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleReasonType, False)
        Me.IndigoSearchLookUpControl11.SetCancelEditButtonNavigator(Me.INDsleReasonType, False)
        Me.IndigoSearchLookUpControl11.SetEditButtonNavigator(Me.INDsleReasonType, False)
        Me.IndigoSearchLookUpControl11.SetEndEditButtonNavigator(Me.INDsleReasonType, False)
        Me.INDsleReasonType.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl11.SetExportButton(Me.INDsleReasonType, False)
        Me.IndigoSearchLookUpControl11.SetFirstButtonNavigator(Me.INDsleReasonType, False)
        Me.IndigoSearchLookUpControl11.SetLastButtonNavigator(Me.INDsleReasonType, False)
        Me.INDsleReasonType.Location = New System.Drawing.Point(24, 79)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleReasonType, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleReasonType.Name = "INDsleReasonType"
        Me.IndigoSearchLookUpControl11.SetNextButtonNavigator(Me.INDsleReasonType, False)
        Me.IndigoSearchLookUpControl11.SetNextPageButtonNavigator(Me.INDsleReasonType, False)
        Me.IndigoSearchLookUpControl11.SetOpenForm(Me.INDsleReasonType, False)
        Me.IndigoSearchLookUpControl11.SetPopupSizeable(Me.INDsleReasonType, False)
        Me.IndigoSearchLookUpControl11.SetPrevButtonNavigator(Me.INDsleReasonType, False)
        Me.IndigoSearchLookUpControl11.SetPrevPageButtonNavigator(Me.INDsleReasonType, False)
        Me.INDsleReasonType.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDsleReasonType.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleReasonType.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDsleReasonType.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleReasonType.Properties.Appearance.Options.UseFont = True
        Me.INDsleReasonType.Properties.Appearance.Options.UseForeColor = True
        Me.INDsleReasonType.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDsleReasonType.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDsleReasonType.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleReasonType.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDsleReasonType.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDsleReasonType.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDsleReasonType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleReasonType.Properties.DisplayMember = "DESMOTANU"
        Me.INDsleReasonType.Properties.NullText = ""
        Me.INDsleReasonType.Properties.PopupSizeable = False
        Me.INDsleReasonType.Properties.PopupView = Me.SearchLookUpEdit6View
        Me.INDsleReasonType.Properties.ShowClearButton = False
        Me.INDsleReasonType.Properties.ShowFooter = False
        Me.INDsleReasonType.Properties.ValueMember = "CODMOTANU"
        Me.IndigoSearchLookUpControl11.SetRemoveButtonNavigator(Me.INDsleReasonType, False)
        Me.IndigoSearchLookUpControl11.SetSaveXmlGrid(Me.INDsleReasonType, True)
        Me.IndigoSearchLookUpControl11.SetShowDeleteButton(Me.INDsleReasonType, False)
        Me.IndigoSearchLookUpControl11.SetShowFindButton(Me.INDsleReasonType, False)
        Me.INDsleReasonType.Size = New System.Drawing.Size(614, 28)
        Me.INDsleReasonType.StyleController = Me.INDlyRoot
        Me.INDsleReasonType.TabIndex = 5
        Me.IndigoSearchLookUpControl11.SetTagForm(Me.INDsleReasonType, "")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleReasonType, 0)
        Me.IndigoSearchLookUpControl11.SetTextStringFormat(Me.INDsleReasonType, "{0} - {1}")
        Me.IndigoSearchLookUpControl11.SetTxtFindEnterEnabled(Me.INDsleReasonType, False)
        Me.IndigoSearchLookUpControl11.SetUseEmbeddedNavigator(Me.INDsleReasonType, False)
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
        Me.GridColumn127.FieldName = "CODMOTANU"
        Me.GridColumn127.Name = "GridColumn127"
        Me.GridColumn127.Visible = True
        Me.GridColumn127.VisibleIndex = 0
        '
        'GridColumn128
        '
        Me.GridColumn128.Caption = "Nombre"
        Me.GridColumn128.FieldName = "DESMOTANU"
        Me.GridColumn128.Name = "GridColumn128"
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
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(662, 392)
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
        Me.INDLcgProduct.CustomizationFormText = "Producto"
        Me.INDLcgProduct.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLcDescription, Me.INDliReasonType})
        Me.INDLcgProduct.Location = New System.Drawing.Point(0, 0)
        Me.INDLcgProduct.Name = "INDLcgProduct"
        Me.INDLcgProduct.Size = New System.Drawing.Size(642, 372)
        Me.INDLcgProduct.Text = "Datos Principales"
        '
        'INDLcDescription
        '
        Me.INDLcDescription.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI", 13.0!)
        Me.INDLcDescription.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcDescription.Control = Me.INDMeDescription
        Me.INDLcDescription.CustomizationFormText = "Descripción"
        Me.INDLcDescription.Location = New System.Drawing.Point(0, 60)
        Me.INDLcDescription.Name = "INDLcDescription"
        Me.INDLcDescription.OptionsPrint.AppearanceItem.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.INDLcDescription.OptionsPrint.AppearanceItem.Options.UseFont = True
        Me.INDLcDescription.OptionsPrint.AppearanceItemText.Options.UseFont = True
        Me.INDLcDescription.Size = New System.Drawing.Size(618, 259)
        Me.INDLcDescription.Text = "Descripción"
        Me.INDLcDescription.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLcDescription.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLcDescription.TextSize = New System.Drawing.Size(108, 30)
        Me.INDLcDescription.TextToControlDistance = 5
        '
        'INDliReasonType
        '
        Me.INDliReasonType.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI", 13.0!)
        Me.INDliReasonType.AppearanceItemCaption.Options.UseFont = True
        Me.INDliReasonType.Control = Me.INDsleReasonType
        Me.INDliReasonType.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDliReasonType.CustomizationFormText = "Razón de Cambio"
        Me.INDliReasonType.Location = New System.Drawing.Point(0, 0)
        Me.INDliReasonType.MinSize = New System.Drawing.Size(1, 1)
        Me.INDliReasonType.Name = "INDliReasonType"
        Me.INDliReasonType.ShowInCustomizationForm = False
        Me.INDliReasonType.Size = New System.Drawing.Size(618, 60)
        Me.INDliReasonType.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDliReasonType.Text = "Razón de Cambio"
        Me.INDliReasonType.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDliReasonType.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDliReasonType.TextSize = New System.Drawing.Size(108, 21)
        Me.INDliReasonType.TextToControlDistance = 5
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
        'FrmPopupObservations
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(666, 536)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.IconOptions.ShowIcon = False
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmPopupObservations"
        Me.Opacity = 1.0R
        Me.Text = "Observaciones"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyRoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyRoot.ResumeLayout(False)
        CType(Me.INDMeDescription.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleReasonType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit6View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgProduct, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliReasonType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl11, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents INDMeDescription As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents INDLcDescription As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDsleReasonType As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents IndigoSearchLookUpControl11 As IndigoSearchLookUpControl
    Friend WithEvents SearchLookUpEdit6View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn127 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn128 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDliReasonType As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents RepositoryItemPopupContainerEdit11 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
End Class
