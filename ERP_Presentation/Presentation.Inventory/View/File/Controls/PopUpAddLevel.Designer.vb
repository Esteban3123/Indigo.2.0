<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PopUpAddLevel
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
        Dim AppearanceObject1 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject2 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDsbAddLevel = New DevExpress.XtraEditors.SimpleButton()
        Me.INDtxtConversionUnit = New DevExpress.XtraEditors.TextEdit()
        Me.INDsleProduct = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDliConversionUnit = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDliAddLevel = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl()
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView()
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton()
        'Me.IndigoLayoutControl1 = New Presentation.Controls.IndigoLayoutControl()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit()
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl()
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.INDtxtConversionUnit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleProduct.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliConversionUnit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliAddLevel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        'CType(Me.IndigoLayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.INDsbAddLevel)
        Me.LayoutControl1.Controls.Add(Me.INDtxtConversionUnit)
        Me.LayoutControl1.Controls.Add(Me.INDsleProduct)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(438, 131)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'INDsbAddLevel
        '
        Me.INDsbAddLevel.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsbAddLevel.Appearance.Options.UseFont = True
        Me.INDsbAddLevel.Location = New System.Drawing.Point(12, 84)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDsbAddLevel, True)
        Me.INDsbAddLevel.Name = "INDsbAddLevel"
        Me.INDsbAddLevel.Size = New System.Drawing.Size(406, 32)
        Me.INDsbAddLevel.StyleController = Me.LayoutControl1
        Me.INDsbAddLevel.TabIndex = 6
        Me.INDsbAddLevel.Text = "Agregar"
        '
        'INDtxtConversionUnit
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtConversionUnit, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtConversionUnit, False)
        Me.INDtxtConversionUnit.EnterMoveNextControl = True
        Me.INDtxtConversionUnit.Location = New System.Drawing.Point(167, 48)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtConversionUnit, Presentation.Controls.IndigoTextEdit.EMask.Numerico)
        Me.INDtxtConversionUnit.Name = "INDtxtConversionUnit"
        Me.INDtxtConversionUnit.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDtxtConversionUnit.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtConversionUnit.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtConversionUnit.Properties.Appearance.Options.UseFont = True
        Me.INDtxtConversionUnit.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDtxtConversionUnit.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDtxtConversionUnit.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtConversionUnit.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtConversionUnit.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtxtConversionUnit.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtConversionUnit.Properties.Mask.EditMask = "[0-9]+"
        Me.INDtxtConversionUnit.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.INDtxtConversionUnit.Size = New System.Drawing.Size(251, 28)
        Me.INDtxtConversionUnit.StyleController = Me.LayoutControl1
        Me.INDtxtConversionUnit.TabIndex = 5
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtConversionUnit, 0)
        '
        'INDsleProduct
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleProduct, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleProduct, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleProduct, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleProduct, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleProduct, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleProduct, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleProduct, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleProduct, False)
        Me.INDsleProduct.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleProduct, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleProduct, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleProduct, False)
        Me.INDsleProduct.Location = New System.Drawing.Point(167, 12)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleProduct, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleProduct.Name = "INDsleProduct"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleProduct, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleProduct, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleProduct, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleProduct, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleProduct, False)
        Me.INDsleProduct.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDsleProduct.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleProduct.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleProduct.Properties.Appearance.Options.UseFont = True
        Me.INDsleProduct.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "Limpiar selección (Supr)", Nothing, Nothing, True)})
        Me.INDsleProduct.Properties.DisplayMember = "CodeName"
        Me.INDsleProduct.Properties.NullText = ""
        Me.INDsleProduct.Properties.PopupSizeable = False
        Me.INDsleProduct.Properties.ShowFooter = False
        Me.INDsleProduct.Properties.ValueMember = "Id"
        Me.INDsleProduct.Properties.View = Me.SearchLookUpEdit1View
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleProduct, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleProduct, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleProduct, True)
        Me.INDsleProduct.Size = New System.Drawing.Size(251, 28)
        Me.INDsleProduct.StyleController = Me.LayoutControl1
        Me.INDsleProduct.TabIndex = 4
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleProduct, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleProduct, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleProduct, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleProduct, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleProduct, False)
        '
        'SearchLookUpEdit1View
        '
        Me.SearchLookUpEdit1View.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.SearchLookUpEdit1View.Appearance.GroupRow.Options.UseFont = True
        'Cadena reemplazada... True
        Me.SearchLookUpEdit1View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.SearchLookUpEdit1View.Appearance.HeaderPanel.Options.UseFont = True
        'Cadena reemplazada... True
        Me.SearchLookUpEdit1View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.SearchLookUpEdit1View.Appearance.Row.Options.UseFont = True
        Me.SearchLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2, Me.GridColumn3, Me.GridColumn4})
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
        Me.GridColumn1.Width = 246
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Nombre"
        Me.GridColumn2.FieldName = "Name"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        Me.GridColumn2.Width = 382
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Tipo"
        Me.GridColumn3.FieldName = "ProductTypeId.Name"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 2
        Me.GridColumn3.Width = 382
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Clase Producto"
        Me.GridColumn4.FieldName = "ProductTypeId.ClassName"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 3
        Me.GridColumn4.Width = 382
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
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.INDliConversionUnit, Me.INDliAddLevel})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(438, 131)
        Me.LayoutControlGroup1.Text = "LayoutControlGroup1"
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDsleProduct
        Me.LayoutControlItem1.CustomizationFormText = "LayoutControlItem1"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.MaxSize = New System.Drawing.Size(410, 36)
        Me.LayoutControlItem1.MinSize = New System.Drawing.Size(410, 36)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(418, 36)
        Me.LayoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem1.Text = "Producto"
        Me.LayoutControlItem1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(150, 21)
        Me.LayoutControlItem1.TextToControlDistance = 5
        '
        'INDliConversionUnit
        '
        Me.INDliConversionUnit.Control = Me.INDtxtConversionUnit
        Me.INDliConversionUnit.CustomizationFormText = "LayoutControlItem2"
        Me.INDliConversionUnit.Location = New System.Drawing.Point(0, 36)
        Me.INDliConversionUnit.MaxSize = New System.Drawing.Size(410, 36)
        Me.INDliConversionUnit.MinSize = New System.Drawing.Size(410, 36)
        Me.INDliConversionUnit.Name = "INDliConversionUnit"
        Me.INDliConversionUnit.Size = New System.Drawing.Size(418, 36)
        Me.INDliConversionUnit.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDliConversionUnit.Text = "Unidad de Conversión"
        Me.INDliConversionUnit.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDliConversionUnit.TextSize = New System.Drawing.Size(150, 21)
        Me.INDliConversionUnit.TextToControlDistance = 5
        '
        'INDliAddLevel
        '
        Me.INDliAddLevel.Control = Me.INDsbAddLevel
        Me.INDliAddLevel.CustomizationFormText = "LayoutControlItem3"
        Me.INDliAddLevel.Location = New System.Drawing.Point(0, 72)
        Me.INDliAddLevel.MaxSize = New System.Drawing.Size(410, 36)
        Me.INDliAddLevel.MinSize = New System.Drawing.Size(410, 36)
        Me.INDliAddLevel.Name = "INDliAddLevel"
        Me.INDliAddLevel.Size = New System.Drawing.Size(418, 39)
        Me.INDliAddLevel.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDliAddLevel.Text = "INDliAddLevel"
        Me.INDliAddLevel.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDliAddLevel.TextSize = New System.Drawing.Size(0, 0)
        Me.INDliAddLevel.TextToControlDistance = 0
        Me.INDliAddLevel.TextVisible = False
        '
        'IndigoLayoutControl1
        '

        'Me.IndigoLayoutControl1.FormName = "FormBase20141126"

        '
        'PopUpAddLevel
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(438, 131)
        Me.Controls.Add(Me.LayoutControl1)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "PopUpAddLevel"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.Text = "Agregar Nivel"
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.INDtxtConversionUnit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleProduct.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliConversionUnit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliAddLevel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        'CType(Me.IndigoLayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDsbAddLevel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents IndigoSimpleButton1 As Presentation.Controls.IndigoSimpleButton
    Friend WithEvents INDtxtConversionUnit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDsleProduct As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents SearchLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDliConversionUnit As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDliAddLevel As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    'Friend WithEvents IndigoLayoutControl1 As Presentation.Controls.IndigoLayoutControl
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
    Friend WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
End Class
