<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmPercentPayment
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
        Me.INDslePaymentConcept = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDsbAcept = New DevExpress.XtraEditors.SimpleButton()
        Me.INDtxtPaymentPercent = New DevExpress.XtraEditors.SpinEdit()
        Me.INDlcgPaymentPercent = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDliPaymentPercent = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDliAccept = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDliPaymentConcept = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit()
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton()
        'Me.IndigoLayoutControl1 = New Presentation.Controls.IndigoLayoutControl()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup()
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl()
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl()
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView()
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.INDslePaymentConcept.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtPaymentPercent.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcgPaymentPercent, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliPaymentPercent, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliAccept, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliPaymentConcept, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        'CType(Me.IndigoLayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.INDslePaymentConcept)
        Me.LayoutControl1.Controls.Add(Me.INDsbAcept)
        Me.LayoutControl1.Controls.Add(Me.INDtxtPaymentPercent)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.INDlcgPaymentPercent
        Me.LayoutControl1.Size = New System.Drawing.Size(413, 124)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'INDslePaymentConcept
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDslePaymentConcept, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDslePaymentConcept, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDslePaymentConcept, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDslePaymentConcept, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDslePaymentConcept, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDslePaymentConcept, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDslePaymentConcept, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDslePaymentConcept, False)
        Me.INDslePaymentConcept.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDslePaymentConcept, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDslePaymentConcept, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDslePaymentConcept, False)
        Me.INDslePaymentConcept.Location = New System.Drawing.Point(152, 48)
        Me.IndigoTextEdit1.SetMascara(Me.INDslePaymentConcept, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDslePaymentConcept.Name = "INDslePaymentConcept"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDslePaymentConcept, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDslePaymentConcept, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDslePaymentConcept, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDslePaymentConcept, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDslePaymentConcept, False)
        Me.INDslePaymentConcept.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDslePaymentConcept.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDslePaymentConcept.Properties.Appearance.Options.UseBackColor = True
        Me.INDslePaymentConcept.Properties.Appearance.Options.UseFont = True
        Me.INDslePaymentConcept.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDslePaymentConcept.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDslePaymentConcept.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDslePaymentConcept.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDslePaymentConcept.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDslePaymentConcept.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDslePaymentConcept.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "Limpiar selección (Supr)", Nothing, Nothing, True)})
        Me.INDslePaymentConcept.Properties.DisplayMember = "CodeName"
        Me.INDslePaymentConcept.Properties.NullText = ""
        Me.INDslePaymentConcept.Properties.PopupSizeable = False
        Me.INDslePaymentConcept.Properties.ShowFooter = False
        Me.INDslePaymentConcept.Properties.ValueMember = "Id"
        Me.INDslePaymentConcept.Properties.View = Me.SearchLookUpEdit1View
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDslePaymentConcept, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDslePaymentConcept, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDslePaymentConcept, True)
        Me.INDslePaymentConcept.Size = New System.Drawing.Size(246, 28)
        Me.INDslePaymentConcept.StyleController = Me.LayoutControl1
        Me.INDslePaymentConcept.TabIndex = 6
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDslePaymentConcept, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDslePaymentConcept, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDslePaymentConcept, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDslePaymentConcept, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDslePaymentConcept, False)
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
        Me.GridColumn1.Width = 258
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Nombre"
        Me.GridColumn2.FieldName = "Name"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        Me.GridColumn2.Width = 825
        '
        'INDsbAcept
        '
        Me.INDsbAcept.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsbAcept.Appearance.Options.UseFont = True
        Me.INDsbAcept.Location = New System.Drawing.Point(12, 84)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDsbAcept, True)
        Me.INDsbAcept.Name = "INDsbAcept"
        Me.INDsbAcept.Size = New System.Drawing.Size(389, 28)
        Me.INDsbAcept.StyleController = Me.LayoutControl1
        Me.INDsbAcept.TabIndex = 5
        Me.INDsbAcept.Text = "Aceptar"
        '
        'INDtxtPaymentPercent
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtPaymentPercent, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtPaymentPercent, False)
        Me.INDtxtPaymentPercent.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDtxtPaymentPercent.EnterMoveNextControl = True
        Me.INDtxtPaymentPercent.Location = New System.Drawing.Point(152, 12)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtPaymentPercent, Presentation.Controls.IndigoTextEdit.EMask.Porcentaje)
        Me.INDtxtPaymentPercent.Name = "INDtxtPaymentPercent"
        Me.INDtxtPaymentPercent.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDtxtPaymentPercent.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtPaymentPercent.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtPaymentPercent.Properties.Appearance.Options.UseFont = True
        Me.INDtxtPaymentPercent.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDtxtPaymentPercent.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDtxtPaymentPercent.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtPaymentPercent.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtPaymentPercent.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtxtPaymentPercent.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtPaymentPercent.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDtxtPaymentPercent.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.[Default]
        Me.INDtxtPaymentPercent.Properties.Mask.EditMask = "P"
        Me.INDtxtPaymentPercent.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDtxtPaymentPercent.Properties.MaxValue = New Decimal(New Integer() {100, 0, 0, 0})
        Me.INDtxtPaymentPercent.Size = New System.Drawing.Size(246, 28)
        Me.INDtxtPaymentPercent.StyleController = Me.LayoutControl1
        Me.INDtxtPaymentPercent.TabIndex = 4
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtPaymentPercent, 0)
        '
        'INDlcgPaymentPercent
        '
        Me.INDlcgPaymentPercent.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDlcgPaymentPercent.AppearanceGroup.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDlcgPaymentPercent.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgPaymentPercent.AppearanceItemCaption.Options.UseFont = True
        Me.INDlcgPaymentPercent.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgPaymentPercent.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlcgPaymentPercent.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDlcgPaymentPercent.AppearanceTabPage.HeaderActive.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDlcgPaymentPercent.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgPaymentPercent.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlcgPaymentPercent.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDlcgPaymentPercent.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDlcgPaymentPercent.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgPaymentPercent.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlcgPaymentPercent, False)
        Me.INDlcgPaymentPercent.CustomizationFormText = "INDlcgPaymentPercent"
        Me.INDlcgPaymentPercent.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDlcgPaymentPercent.GroupBordersVisible = False
        Me.INDlcgPaymentPercent.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDliPaymentPercent, Me.INDliAccept, Me.INDliPaymentConcept})
        Me.INDlcgPaymentPercent.Location = New System.Drawing.Point(0, 0)
        Me.INDlcgPaymentPercent.Name = "INDlcgPaymentPercent"
        Me.INDlcgPaymentPercent.Size = New System.Drawing.Size(413, 124)
        Me.INDlcgPaymentPercent.Text = "INDlcgPaymentPercent"
        Me.INDlcgPaymentPercent.TextVisible = False
        '
        'INDliPaymentPercent
        '
        Me.INDliPaymentPercent.Control = Me.INDtxtPaymentPercent
        Me.INDliPaymentPercent.CustomizationFormText = "LayoutControlItem1"
        Me.INDliPaymentPercent.Location = New System.Drawing.Point(0, 0)
        Me.INDliPaymentPercent.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDliPaymentPercent.MinSize = New System.Drawing.Size(390, 36)
        Me.INDliPaymentPercent.Name = "INDliPaymentPercent"
        Me.INDliPaymentPercent.Size = New System.Drawing.Size(393, 36)
        Me.INDliPaymentPercent.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDliPaymentPercent.Text = "Porcentaje a Pagar"
        Me.INDliPaymentPercent.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDliPaymentPercent.TextSize = New System.Drawing.Size(135, 13)
        Me.INDliPaymentPercent.TextToControlDistance = 5
        '
        'INDliAccept
        '
        Me.INDliAccept.Control = Me.INDsbAcept
        Me.INDliAccept.CustomizationFormText = "LayoutControlItem2"
        Me.INDliAccept.Location = New System.Drawing.Point(0, 72)
        Me.INDliAccept.Name = "INDliAccept"
        Me.INDliAccept.Size = New System.Drawing.Size(393, 32)
        Me.INDliAccept.Text = "INDliAccept"
        Me.INDliAccept.TextSize = New System.Drawing.Size(0, 0)
        Me.INDliAccept.TextToControlDistance = 0
        Me.INDliAccept.TextVisible = False
        '
        'INDliPaymentConcept
        '
        Me.INDliPaymentConcept.Control = Me.INDslePaymentConcept
        Me.INDliPaymentConcept.CustomizationFormText = "LayoutControlItem1"
        Me.INDliPaymentConcept.Location = New System.Drawing.Point(0, 36)
        Me.INDliPaymentConcept.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDliPaymentConcept.MinSize = New System.Drawing.Size(390, 36)
        Me.INDliPaymentConcept.Name = "INDliPaymentConcept"
        Me.INDliPaymentConcept.Size = New System.Drawing.Size(393, 36)
        Me.INDliPaymentConcept.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDliPaymentConcept.Text = "Concepto de Pago"
        Me.INDliPaymentConcept.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDliPaymentConcept.TextSize = New System.Drawing.Size(135, 21)
        Me.INDliPaymentConcept.TextToControlDistance = 5
        '
        'IndigoLayoutControl1
        '

        'Me.IndigoLayoutControl1.FormName = "FormBase20140829"

        '
        'FrmPercentPayment
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(413, 124)
        Me.Controls.Add(Me.LayoutControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmPercentPayment"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.Text = "Título"
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.INDslePaymentConcept.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtPaymentPercent.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcgPaymentPercent, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliPaymentPercent, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliAccept, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliPaymentConcept, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        'CType(Me.IndigoLayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDlcgPaymentPercent As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDliPaymentPercent As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDsbAcept As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents IndigoSimpleButton1 As Presentation.Controls.IndigoSimpleButton
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents INDliAccept As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDtxtPaymentPercent As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    'Friend WithEvents IndigoLayoutControl1 As Presentation.Controls.IndigoLayoutControl
    Friend WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl
    Friend WithEvents INDslePaymentConcept As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDliPaymentConcept As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
End Class
