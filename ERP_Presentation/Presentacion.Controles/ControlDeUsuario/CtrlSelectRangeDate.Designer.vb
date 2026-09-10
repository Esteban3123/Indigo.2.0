<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class CtrlSelectRangeDate
    Inherits System.Windows.Forms.UserControl

    'UserControl reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.INDLcRoot = New DevExpress.XtraLayout.LayoutControl()
        Me.INDDeEndDate = New DevExpress.XtraEditors.DateEdit()
        Me.INDDeInitialDate = New DevExpress.XtraEditors.DateEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciFechaInicial = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciFechaFinal = New DevExpress.XtraLayout.LayoutControlItem()
        CType(Me.INDLcRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLcRoot.SuspendLayout()
        CType(Me.INDDeEndDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDeEndDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDeInitialDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDeInitialDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciFechaInicial, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciFechaFinal, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDLcRoot
        '
        Me.INDLcRoot.Controls.Add(Me.INDDeEndDate)
        Me.INDLcRoot.Controls.Add(Me.INDDeInitialDate)
        Me.INDLcRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDLcRoot.HiddenItems.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciFechaFinal})
        Me.INDLcRoot.Location = New System.Drawing.Point(0, 0)
        Me.INDLcRoot.Name = "INDLcRoot"
        Me.INDLcRoot.Root = Me.LayoutControlGroup1
        Me.INDLcRoot.Size = New System.Drawing.Size(292, 65)
        Me.INDLcRoot.TabIndex = 0
        Me.INDLcRoot.Text = "LayoutControl1"
        '
        'INDDeEndDate
        '
        Me.INDDeEndDate.EditValue = Nothing
        Me.INDDeEndDate.Location = New System.Drawing.Point(90, 33)
        Me.INDDeEndDate.Name = "INDDeEndDate"
        Me.INDDeEndDate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDDeEndDate.Properties.Appearance.Options.UseFont = True
        Me.INDDeEndDate.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDDeEndDate.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDDeEndDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDeEndDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDeEndDate.Properties.Mask.EditMask = "dd \de MMMM \de yyyy"
        Me.INDDeEndDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDDeEndDate.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDDeEndDate.Size = New System.Drawing.Size(197, 24)
        Me.INDDeEndDate.StyleController = Me.INDLcRoot
        Me.INDDeEndDate.TabIndex = 5
        '
        'INDDeInitialDate
        '
        Me.INDDeInitialDate.EditValue = Nothing
        Me.INDDeInitialDate.Location = New System.Drawing.Point(5, 25)
        Me.INDDeInitialDate.Name = "INDDeInitialDate"
        Me.INDDeInitialDate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDDeInitialDate.Properties.Appearance.Options.UseFont = True
        Me.INDDeInitialDate.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDDeInitialDate.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDDeInitialDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDeInitialDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDeInitialDate.Properties.Mask.EditMask = "dd \de MMMM \de yyyy"
        Me.INDDeInitialDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDDeInitialDate.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDDeInitialDate.Size = New System.Drawing.Size(282, 24)
        Me.INDDeInitialDate.StyleController = Me.INDLcRoot
        Me.INDDeInitialDate.TabIndex = 4
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciFechaInicial})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Padding = New DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3)
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(292, 65)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDLciFechaInicial
        '
        Me.INDLciFechaInicial.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 9.0!)
        Me.INDLciFechaInicial.AppearanceItemCaption.Options.UseFont = True
        Me.INDLciFechaInicial.Control = Me.INDDeInitialDate
        Me.INDLciFechaInicial.Location = New System.Drawing.Point(0, 0)
        Me.INDLciFechaInicial.MaxSize = New System.Drawing.Size(0, 28)
        Me.INDLciFechaInicial.MinSize = New System.Drawing.Size(130, 28)
        Me.INDLciFechaInicial.Name = "INDLciFechaInicial"
        Me.INDLciFechaInicial.Size = New System.Drawing.Size(286, 59)
        Me.INDLciFechaInicial.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciFechaInicial.Text = "Fecha Reconocimiento"
        Me.INDLciFechaInicial.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciFechaInicial.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciFechaInicial.TextSize = New System.Drawing.Size(80, 15)
        Me.INDLciFechaInicial.TextToControlDistance = 5
        '
        'INDLciFechaFinal
        '
        Me.INDLciFechaFinal.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 9.0!)
        Me.INDLciFechaFinal.AppearanceItemCaption.Options.UseFont = True
        Me.INDLciFechaFinal.Control = Me.INDDeEndDate
        Me.INDLciFechaFinal.Location = New System.Drawing.Point(0, 28)
        Me.INDLciFechaFinal.MaxSize = New System.Drawing.Size(0, 28)
        Me.INDLciFechaFinal.MinSize = New System.Drawing.Size(130, 28)
        Me.INDLciFechaFinal.Name = "INDLciFechaFinal"
        Me.INDLciFechaFinal.Size = New System.Drawing.Size(286, 31)
        Me.INDLciFechaFinal.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciFechaFinal.Text = "Fecha Final"
        Me.INDLciFechaFinal.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciFechaFinal.TextSize = New System.Drawing.Size(80, 15)
        Me.INDLciFechaFinal.TextToControlDistance = 5
        '
        'CtrlSelectRangeDate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.INDLcRoot)
        Me.Margin = New System.Windows.Forms.Padding(0)
        Me.MaximumSize = New System.Drawing.Size(292, 65)
        Me.MinimumSize = New System.Drawing.Size(292, 65)
        Me.Name = "CtrlSelectRangeDate"
        Me.Size = New System.Drawing.Size(292, 65)
        CType(Me.INDLcRoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLcRoot.ResumeLayout(False)
        CType(Me.INDDeEndDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDeEndDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDeInitialDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDeInitialDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciFechaInicial, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciFechaFinal, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents INDLcRoot As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDDeEndDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDDeInitialDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDLciFechaInicial As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciFechaFinal As DevExpress.XtraLayout.LayoutControlItem
End Class
