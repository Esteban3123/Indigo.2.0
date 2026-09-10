Imports DevExpress.XtraEditors

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ProgrammingOptionsModal
    Inherits XtraForm

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
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.INDBtnAccept = New DevExpress.XtraEditors.SimpleButton()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDSpnTotalPerDay = New DevExpress.XtraEditors.SpinEdit()
        Me.INDSpnTotalFixedAsset = New DevExpress.XtraEditors.SpinEdit()
        Me.INDSpnDisponibleResponsable = New DevExpress.XtraEditors.SpinEdit()
        Me.INDSpnProtocol = New DevExpress.XtraEditors.TextEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciTiempoProtocolo = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciTiempoResponsable = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciActivosDia = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDPcLayout = New DevExpress.XtraEditors.PanelControl()
        Me.INDProgress = New DevExpress.XtraWaitForm.ProgressPanel()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGridLookUpControl1 = New Presentation.Controls.IndigoGridLookUpControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.INDSpnTotalPerDay.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSpnTotalFixedAsset.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSpnDisponibleResponsable.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSpnProtocol.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciTiempoProtocolo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciTiempoResponsable, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciActivosDia, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPcLayout, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPcLayout.SuspendLayout()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.INDBtnAccept)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelControl1.Location = New System.Drawing.Point(0, 246)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(377, 41)
        Me.PanelControl1.TabIndex = 0
        '
        'INDBtnAccept
        '
        Me.INDBtnAccept.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDBtnAccept.Appearance.Options.UseFont = True
        Me.INDBtnAccept.Location = New System.Drawing.Point(285, 6)
        Me.INDBtnAccept.Name = "INDBtnAccept"
        Me.INDBtnAccept.Size = New System.Drawing.Size(80, 30)
        Me.INDBtnAccept.TabIndex = 0
        Me.INDBtnAccept.Text = "Aceptar"
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.INDSpnTotalPerDay)
        Me.LayoutControl1.Controls.Add(Me.INDSpnTotalFixedAsset)
        Me.LayoutControl1.Controls.Add(Me.INDSpnDisponibleResponsable)
        Me.LayoutControl1.Controls.Add(Me.INDSpnProtocol)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(377, 246)
        Me.LayoutControl1.TabIndex = 1
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'INDSpnTotalPerDay
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSpnTotalPerDay, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSpnTotalPerDay, False)
        Me.INDSpnTotalPerDay.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDSpnTotalPerDay.Location = New System.Drawing.Point(12, 188)
        Me.IndigoTextEdit1.SetMascara(Me.INDSpnTotalPerDay, Presentation.Controls.IndigoTextEdit.EMask.Numerico)
        Me.INDSpnTotalPerDay.Name = "INDSpnTotalPerDay"
        Me.INDSpnTotalPerDay.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDSpnTotalPerDay.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSpnTotalPerDay.Properties.Appearance.Options.UseBackColor = True
        Me.INDSpnTotalPerDay.Properties.Appearance.Options.UseFont = True
        Me.INDSpnTotalPerDay.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSpnTotalPerDay.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSpnTotalPerDay.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSpnTotalPerDay.Properties.Mask.EditMask = "[0-9]+"
        Me.INDSpnTotalPerDay.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.INDSpnTotalPerDay.Properties.ReadOnly = True
        Me.INDSpnTotalPerDay.Size = New System.Drawing.Size(353, 28)
        Me.INDSpnTotalPerDay.StyleController = Me.LayoutControl1
        Me.INDSpnTotalPerDay.TabIndex = 10
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSpnTotalPerDay, 0)
        '
        'INDSpnTotalFixedAsset
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSpnTotalFixedAsset, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSpnTotalFixedAsset, False)
        Me.INDSpnTotalFixedAsset.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDSpnTotalFixedAsset.Location = New System.Drawing.Point(12, 32)
        Me.IndigoTextEdit1.SetMascara(Me.INDSpnTotalFixedAsset, Presentation.Controls.IndigoTextEdit.EMask.Numerico)
        Me.INDSpnTotalFixedAsset.Name = "INDSpnTotalFixedAsset"
        Me.INDSpnTotalFixedAsset.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDSpnTotalFixedAsset.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSpnTotalFixedAsset.Properties.Appearance.Options.UseBackColor = True
        Me.INDSpnTotalFixedAsset.Properties.Appearance.Options.UseFont = True
        Me.INDSpnTotalFixedAsset.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSpnTotalFixedAsset.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSpnTotalFixedAsset.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSpnTotalFixedAsset.Properties.Mask.EditMask = "[0-9]+"
        Me.INDSpnTotalFixedAsset.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.INDSpnTotalFixedAsset.Properties.ReadOnly = True
        Me.INDSpnTotalFixedAsset.Size = New System.Drawing.Size(353, 28)
        Me.INDSpnTotalFixedAsset.StyleController = Me.LayoutControl1
        Me.INDSpnTotalFixedAsset.TabIndex = 5
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSpnTotalFixedAsset, 0)
        '
        'INDSpnDisponibleResponsable
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSpnDisponibleResponsable, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSpnDisponibleResponsable, False)
        Me.INDSpnDisponibleResponsable.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDSpnDisponibleResponsable.Location = New System.Drawing.Point(12, 136)
        Me.IndigoTextEdit1.SetMascara(Me.INDSpnDisponibleResponsable, Presentation.Controls.IndigoTextEdit.EMask.Numerico)
        Me.INDSpnDisponibleResponsable.Name = "INDSpnDisponibleResponsable"
        Me.INDSpnDisponibleResponsable.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDSpnDisponibleResponsable.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSpnDisponibleResponsable.Properties.Appearance.Options.UseBackColor = True
        Me.INDSpnDisponibleResponsable.Properties.Appearance.Options.UseFont = True
        Me.INDSpnDisponibleResponsable.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSpnDisponibleResponsable.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSpnDisponibleResponsable.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSpnDisponibleResponsable.Properties.Mask.EditMask = "[0-9]+"
        Me.INDSpnDisponibleResponsable.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.INDSpnDisponibleResponsable.Properties.ReadOnly = True
        Me.INDSpnDisponibleResponsable.Size = New System.Drawing.Size(353, 28)
        Me.INDSpnDisponibleResponsable.StyleController = Me.LayoutControl1
        Me.INDSpnDisponibleResponsable.TabIndex = 8
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSpnDisponibleResponsable, 0)
        '
        'INDSpnProtocol
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSpnProtocol, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSpnProtocol, False)
        Me.INDSpnProtocol.EditValue = ""
        Me.INDSpnProtocol.Location = New System.Drawing.Point(12, 84)
        Me.IndigoTextEdit1.SetMascara(Me.INDSpnProtocol, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSpnProtocol.Name = "INDSpnProtocol"
        Me.INDSpnProtocol.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDSpnProtocol.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSpnProtocol.Properties.Appearance.Options.UseBackColor = True
        Me.INDSpnProtocol.Properties.Appearance.Options.UseFont = True
        Me.INDSpnProtocol.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSpnProtocol.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSpnProtocol.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Buffered
        Me.INDSpnProtocol.Properties.Mask.EditMask = "[0-9]+"
        Me.INDSpnProtocol.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.INDSpnProtocol.Properties.ReadOnly = True
        Me.INDSpnProtocol.Size = New System.Drawing.Size(353, 28)
        Me.INDSpnProtocol.StyleController = Me.LayoutControl1
        Me.INDSpnProtocol.TabIndex = 6
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSpnProtocol, 0)
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
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem2, Me.INDLciTiempoProtocolo, Me.INDLciTiempoResponsable, Me.INDLciActivosDia})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(377, 246)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.INDSpnTotalFixedAsset
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(357, 52)
        Me.LayoutControlItem2.Text = "Total Activos Seleccionados"
        Me.LayoutControlItem2.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(256, 17)
        '
        'INDLciTiempoProtocolo
        '
        Me.INDLciTiempoProtocolo.Control = Me.INDSpnProtocol
        Me.INDLciTiempoProtocolo.Location = New System.Drawing.Point(0, 52)
        Me.INDLciTiempoProtocolo.Name = "INDLciTiempoProtocolo"
        Me.INDLciTiempoProtocolo.Size = New System.Drawing.Size(357, 52)
        Me.INDLciTiempoProtocolo.Text = "Tiempo Protocolo"
        Me.INDLciTiempoProtocolo.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciTiempoProtocolo.TextSize = New System.Drawing.Size(256, 17)
        '
        'INDLciTiempoResponsable
        '
        Me.INDLciTiempoResponsable.Control = Me.INDSpnDisponibleResponsable
        Me.INDLciTiempoResponsable.Location = New System.Drawing.Point(0, 104)
        Me.INDLciTiempoResponsable.Name = "INDLciTiempoResponsable"
        Me.INDLciTiempoResponsable.Size = New System.Drawing.Size(357, 52)
        Me.INDLciTiempoResponsable.Text = "Tiempo Disponible Responsable (Horas)"
        Me.INDLciTiempoResponsable.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciTiempoResponsable.TextSize = New System.Drawing.Size(256, 17)
        '
        'INDLciActivosDia
        '
        Me.INDLciActivosDia.Control = Me.INDSpnTotalPerDay
        Me.INDLciActivosDia.Location = New System.Drawing.Point(0, 156)
        Me.INDLciActivosDia.Name = "INDLciActivosDia"
        Me.INDLciActivosDia.Size = New System.Drawing.Size(357, 70)
        Me.INDLciActivosDia.Text = "Total Activos por Día"
        Me.INDLciActivosDia.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciActivosDia.TextSize = New System.Drawing.Size(256, 17)
        '
        'INDPcLayout
        '
        Me.INDPcLayout.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDPcLayout.Controls.Add(Me.LayoutControl1)
        Me.INDPcLayout.Controls.Add(Me.PanelControl1)
        Me.INDPcLayout.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDPcLayout.Location = New System.Drawing.Point(0, 0)
        Me.INDPcLayout.Name = "INDPcLayout"
        Me.INDPcLayout.Size = New System.Drawing.Size(377, 287)
        Me.INDPcLayout.TabIndex = 2
        '
        'INDProgress
        '
        Me.INDProgress.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDProgress.Appearance.Options.UseBackColor = True
        Me.INDProgress.AppearanceCaption.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!)
        Me.INDProgress.AppearanceCaption.Options.UseFont = True
        Me.INDProgress.AppearanceDescription.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.INDProgress.AppearanceDescription.Options.UseFont = True
        Me.INDProgress.Caption = "Por favor espere"
        Me.INDProgress.Description = "Cargando información..."
        Me.INDProgress.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDProgress.Location = New System.Drawing.Point(0, 0)
        Me.INDProgress.Name = "INDProgress"
        Me.INDProgress.Padding = New System.Windows.Forms.Padding(105, 0, 0, 0)
        Me.INDProgress.Size = New System.Drawing.Size(377, 287)
        Me.INDProgress.TabIndex = 3
        Me.INDProgress.Text = "ProgressPanel1"
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        '
        'ProgrammingOptionsModal
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(377, 287)
        Me.Controls.Add(Me.INDPcLayout)
        Me.Controls.Add(Me.INDProgress)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ProgrammingOptionsModal"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Opciones de Intervalos"
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.INDSpnTotalPerDay.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSpnTotalFixedAsset.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSpnDisponibleResponsable.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSpnProtocol.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciTiempoProtocolo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciTiempoResponsable, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciActivosDia, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPcLayout, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPcLayout.ResumeLayout(False)
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents PanelControl1 As PanelControl
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDSpnTotalFixedAsset As SpinEdit
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciTiempoProtocolo As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciTiempoResponsable As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDSpnTotalPerDay As SpinEdit
    Friend WithEvents INDLciActivosDia As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoTextEdit1 As Controls.IndigoTextEdit
    Friend WithEvents IndigoGridLookUpControl1 As Controls.IndigoGridLookUpControl
    Friend WithEvents IndigoGridView1 As Controls.IndigoGridView
    Friend WithEvents IndigoLayoutControlGroup1 As Controls.IndigoLayoutControlGroup
    Friend WithEvents IndigoGroupControl1 As Controls.IndigoGroupControl
    Friend WithEvents INDBtnAccept As SimpleButton
    Friend WithEvents INDPcLayout As PanelControl
    Friend WithEvents INDProgress As DevExpress.XtraWaitForm.ProgressPanel
    Friend WithEvents INDSpnDisponibleResponsable As SpinEdit
    Friend WithEvents INDSpnProtocol As TextEdit
End Class
