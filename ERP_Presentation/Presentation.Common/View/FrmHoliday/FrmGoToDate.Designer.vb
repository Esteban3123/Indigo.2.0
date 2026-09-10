<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmGoToDate
    Inherits DevExpress.XtraScheduler.UI.GotoDateForm

    'Form reemplaza a Dispose para limpiar la lista de componentes.
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

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemDate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDdeDate = New DevExpress.XtraEditors.DateEdit()
        Me.INDlyGotoDate = New DevExpress.XtraLayout.LayoutControl()
        Me.INDsbCancel = New DevExpress.XtraEditors.SimpleButton()
        Me.INDsbOK = New DevExpress.XtraEditors.SimpleButton()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoDate1 = New Presentation.Controls.IndigoDate(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        CType(Me.grpGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpGroup.SuspendLayout()
        CType(Me.edtDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.edtDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbShowIn.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdeDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdeDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyGotoDate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyGotoDate.SuspendLayout()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblDate
        '
        Me.lblDate.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDate.Size = New System.Drawing.Size(35, 21)
        '
        'grpGroup
        '
        Me.grpGroup.Size = New System.Drawing.Size(267, 84)
        '
        'edtDate
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.edtDate, False)
        Me.IndigoDate1.SetCampoObligatorio(Me.edtDate, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.edtDate, False)
        Me.edtDate.EditValue = New Date(2005, 4, 13, 0, 0, 0, 0)
        Me.IndigoTextEdit1.SetMascara(Me.edtDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoDate1.SetMascaraDate(Me.edtDate, Presentation.Controls.IndigoDate.EMask.Fecha)
        Me.edtDate.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.edtDate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.edtDate.Properties.Appearance.Options.UseBackColor = True
        Me.edtDate.Properties.Appearance.Options.UseFont = True
        Me.edtDate.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.edtDate.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.edtDate.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.edtDate.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.edtDate.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.edtDate.Properties.AppearanceFocused.Options.UseFont = True
        Me.edtDate.Properties.Mask.EditMask = "dd \de MMMM \de yyyy"
        Me.edtDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.edtDate.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.edtDate.Size = New System.Drawing.Size(158, 28)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.edtDate, 0)
        '
        'cbShowIn
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.cbShowIn, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.cbShowIn, False)
        Me.IndigoTextEdit1.SetMascara(Me.cbShowIn, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.cbShowIn.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.cbShowIn.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.cbShowIn.Properties.Appearance.Options.UseBackColor = True
        Me.cbShowIn.Properties.Appearance.Options.UseFont = True
        Me.cbShowIn.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cbShowIn.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.cbShowIn.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.cbShowIn.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.cbShowIn.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.cbShowIn.Properties.AppearanceFocused.Options.UseFont = True
        Me.cbShowIn.Size = New System.Drawing.Size(158, 28)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.cbShowIn, 0)
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
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemDate, Me.LayoutControlItem2, Me.EmptySpaceItem1, Me.LayoutControlItem3})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(410, 150)
        Me.LayoutControlGroup1.Text = "LayoutControlGroup1"
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDlyItemDate
        '
        Me.INDlyItemDate.Control = Me.INDdeDate
        Me.INDlyItemDate.CustomizationFormText = "Fecha"
        Me.INDlyItemDate.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemDate.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemDate.MinSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemDate.Name = "INDlyItemDate"
        Me.INDlyItemDate.Size = New System.Drawing.Size(390, 36)
        Me.INDlyItemDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemDate.Text = "Fecha"
        Me.INDlyItemDate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemDate.TextSize = New System.Drawing.Size(120, 21)
        Me.INDlyItemDate.TextToControlDistance = 12
        '
        'INDdeDate
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDdeDate, False)
        Me.IndigoDate1.SetCampoObligatorio(Me.INDdeDate, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDdeDate, False)
        Me.INDdeDate.EditValue = Nothing
        Me.INDdeDate.EnterMoveNextControl = True
        Me.INDdeDate.Location = New System.Drawing.Point(144, 12)
        Me.IndigoTextEdit1.SetMascara(Me.INDdeDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoDate1.SetMascaraDate(Me.INDdeDate, Presentation.Controls.IndigoDate.EMask.Fecha)
        Me.INDdeDate.Name = "INDdeDate"
        Me.INDdeDate.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDdeDate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdeDate.Properties.Appearance.Options.UseBackColor = True
        Me.INDdeDate.Properties.Appearance.Options.UseFont = True
        Me.INDdeDate.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDdeDate.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDdeDate.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdeDate.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDdeDate.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDdeDate.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDdeDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdeDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdeDate.Properties.Mask.EditMask = "dd \de MMMM \de yyyy"
        Me.INDdeDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDdeDate.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDdeDate.Size = New System.Drawing.Size(254, 28)
        Me.INDdeDate.StyleController = Me.INDlyGotoDate
        Me.INDdeDate.TabIndex = 4
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDdeDate, 0)
        '
        'INDlyGotoDate
        '
        Me.INDlyGotoDate.Controls.Add(Me.INDsbCancel)
        Me.INDlyGotoDate.Controls.Add(Me.INDsbOK)
        Me.INDlyGotoDate.Controls.Add(Me.INDdeDate)
        Me.INDlyGotoDate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlyGotoDate.Location = New System.Drawing.Point(0, 0)
        Me.INDlyGotoDate.Name = "INDlyGotoDate"
        Me.INDlyGotoDate.Root = Me.LayoutControlGroup1
        Me.INDlyGotoDate.Size = New System.Drawing.Size(410, 150)
        Me.INDlyGotoDate.TabIndex = 0
        Me.INDlyGotoDate.Text = " "
        '
        'INDsbCancel
        '
        Me.INDsbCancel.Appearance.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.INDsbCancel.Appearance.Options.UseFont = True
        Me.INDsbCancel.Location = New System.Drawing.Point(207, 114)
        Me.INDsbCancel.Name = "INDsbCancel"
        Me.INDsbCancel.Size = New System.Drawing.Size(191, 24)
        Me.INDsbCancel.StyleController = Me.INDlyGotoDate
        Me.INDsbCancel.TabIndex = 6
        Me.INDsbCancel.Text = "Cancelar"
        '
        'INDsbOK
        '
        Me.INDsbOK.Appearance.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsbOK.Appearance.Options.UseFont = True
        Me.INDsbOK.Location = New System.Drawing.Point(12, 114)
        Me.INDsbOK.Name = "INDsbOK"
        Me.INDsbOK.Size = New System.Drawing.Size(191, 24)
        Me.INDsbOK.StyleController = Me.INDlyGotoDate
        Me.INDsbOK.TabIndex = 5
        Me.INDsbOK.Text = "Ir"
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.INDsbOK
        Me.LayoutControlItem2.CustomizationFormText = "LayoutControlItem2"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 102)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(195, 28)
        Me.LayoutControlItem2.Text = "LayoutControlItem2"
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextToControlDistance = 0
        Me.LayoutControlItem2.TextVisible = False
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.AllowHotTrack = False
        Me.EmptySpaceItem1.CustomizationFormText = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(0, 36)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(390, 66)
        Me.EmptySpaceItem1.Text = "EmptySpaceItem1"
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.INDsbCancel
        Me.LayoutControlItem3.CustomizationFormText = "LayoutControlItem3"
        Me.LayoutControlItem3.Location = New System.Drawing.Point(195, 102)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(195, 28)
        Me.LayoutControlItem3.Text = "LayoutControlItem3"
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem3.TextToControlDistance = 0
        Me.LayoutControlItem3.TextVisible = False
        '
        'FrmGoToDate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(410, 150)
        Me.Controls.Add(Me.INDlyGotoDate)
        Me.Name = "FrmGoToDate"
        Me.ShowIcon = False
        Me.ShowInTaskbar = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Ir a fecha"
        Me.Controls.SetChildIndex(Me.btnOK, 0)
        Me.Controls.SetChildIndex(Me.btnCancel, 0)
        Me.Controls.SetChildIndex(Me.grpGroup, 0)
        Me.Controls.SetChildIndex(Me.INDlyGotoDate, 0)
        CType(Me.grpGroup, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpGroup.ResumeLayout(False)
        Me.grpGroup.PerformLayout()
        CType(Me.edtDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.edtDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbShowIn.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdeDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdeDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyGotoDate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyGotoDate.ResumeLayout(False)
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents IndigoDate1 As Presentation.Controls.IndigoDate
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents INDlyGotoDate As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDsbCancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDsbOK As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDdeDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemDate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
End Class
