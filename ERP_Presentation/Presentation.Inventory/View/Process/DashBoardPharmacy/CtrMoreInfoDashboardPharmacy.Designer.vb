<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CtrMoreInfoDashboardPharmacy
    Inherits DevExpress.XtraEditors.XtraUserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.PcePopUpEditAdmission = New DevExpress.XtraEditors.PopupContainerEdit()
        Me.INDLblPatient = New DevExpress.XtraEditors.LabelControl()
        Me.INDLblBirthDay = New DevExpress.XtraEditors.LabelControl()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLycBirthDay = New DevExpress.XtraLayout.LayoutControlItem()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.PcePopUpEditAdmission.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLycBirthDay, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LayoutControl1
        '
        Me.LayoutControl1.BackColor = System.Drawing.Color.Transparent
        Me.LayoutControl1.Controls.Add(Me.PcePopUpEditAdmission)
        Me.LayoutControl1.Controls.Add(Me.INDLblPatient)
        Me.LayoutControl1.Controls.Add(Me.INDLblBirthDay)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Margin = New System.Windows.Forms.Padding(0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(377, 71)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'PcePopUpEditAdmission
        '
        Me.PcePopUpEditAdmission.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PcePopUpEditAdmission.EditValue = "Ingreso: 2545899"
        Me.PcePopUpEditAdmission.Location = New System.Drawing.Point(0, 40)
        Me.PcePopUpEditAdmission.Name = "PcePopUpEditAdmission"
        Me.PcePopUpEditAdmission.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.PcePopUpEditAdmission.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.PcePopUpEditAdmission.Properties.Appearance.Options.UseBackColor = True
        Me.PcePopUpEditAdmission.Properties.Appearance.Options.UseFont = True
        Me.PcePopUpEditAdmission.Properties.Appearance.Options.UseTextOptions = True
        Me.PcePopUpEditAdmission.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.PcePopUpEditAdmission.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.PcePopUpEditAdmission.Properties.PopupSizeable = False
        Me.PcePopUpEditAdmission.Properties.ShowPopupCloseButton = False
        Me.PcePopUpEditAdmission.Size = New System.Drawing.Size(188, 22)
        Me.PcePopUpEditAdmission.StyleController = Me.LayoutControl1
        Me.PcePopUpEditAdmission.TabIndex = 5
        '
        'INDLblPatient
        '
        Me.INDLblPatient.Appearance.Font = New System.Drawing.Font("Segoe UI", 11.0!)
        Me.INDLblPatient.Appearance.Options.UseFont = True
        Me.INDLblPatient.Appearance.Options.UseTextOptions = True
        Me.INDLblPatient.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDLblPatient.Location = New System.Drawing.Point(2, 2)
        Me.INDLblPatient.Margin = New System.Windows.Forms.Padding(0)
        Me.INDLblPatient.Name = "INDLblPatient"
        Me.INDLblPatient.Size = New System.Drawing.Size(373, 36)
        Me.INDLblPatient.StyleController = Me.LayoutControl1
        Me.INDLblPatient.TabIndex = 4
        Me.INDLblPatient.Text = "1075235263 - Carlos Ernesto Cordoba Bautista"
        '
        'INDLblBirthDay
        '
        Me.INDLblBirthDay.Appearance.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.INDLblBirthDay.Appearance.Options.UseFont = True
        Me.INDLblBirthDay.Appearance.Options.UseTextOptions = True
        Me.INDLblBirthDay.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDLblBirthDay.Location = New System.Drawing.Point(190, 42)
        Me.INDLblBirthDay.Margin = New System.Windows.Forms.Padding(0)
        Me.INDLblBirthDay.Name = "INDLblBirthDay"
        Me.INDLblBirthDay.Size = New System.Drawing.Size(185, 21)
        Me.INDLblBirthDay.StyleController = Me.LayoutControl1
        Me.INDLblBirthDay.TabIndex = 4
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.LayoutControlItem2, Me.INDLycBirthDay})
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(377, 71)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDLblPatient
        Me.LayoutControlItem1.CustomizationFormText = "LayoutControlItem1"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.MaxSize = New System.Drawing.Size(0, 40)
        Me.LayoutControlItem1.MinSize = New System.Drawing.Size(291, 40)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(377, 40)
        Me.LayoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextToControlDistance = 0
        Me.LayoutControlItem1.TextVisible = False
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.PcePopUpEditAdmission
        Me.LayoutControlItem2.CustomizationFormText = "LayoutControlItem2"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 40)
        Me.LayoutControlItem2.MaxSize = New System.Drawing.Size(0, 25)
        Me.LayoutControlItem2.MinSize = New System.Drawing.Size(50, 25)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlItem2.Size = New System.Drawing.Size(188, 31)
        Me.LayoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextVisible = False
        '
        'INDLycBirthDay
        '
        Me.INDLycBirthDay.Control = Me.INDLblBirthDay
        Me.INDLycBirthDay.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDLycBirthDay.CustomizationFormText = "Fecha Nacimiento"
        Me.INDLycBirthDay.Location = New System.Drawing.Point(188, 40)
        Me.INDLycBirthDay.MaxSize = New System.Drawing.Size(0, 25)
        Me.INDLycBirthDay.MinSize = New System.Drawing.Size(50, 25)
        Me.INDLycBirthDay.Name = "INDLycBirthDay"
        Me.INDLycBirthDay.Size = New System.Drawing.Size(189, 31)
        Me.INDLycBirthDay.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLycBirthDay.Text = "LayoutControlItem1"
        Me.INDLycBirthDay.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLycBirthDay.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLycBirthDay.TextToControlDistance = 0
        Me.INDLycBirthDay.TextVisible = False
        '
        'CtrMoreInfoDashboardPharmacy
        '
        Me.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.Appearance.Options.UseBackColor = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.LayoutControl1)
        Me.Name = "CtrMoreInfoDashboardPharmacy"
        Me.Size = New System.Drawing.Size(377, 71)
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.PcePopUpEditAdmission.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLycBirthDay, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLblPatient As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents PcePopUpEditAdmission As DevExpress.XtraEditors.PopupContainerEdit
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLblBirthDay As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDLycBirthDay As DevExpress.XtraLayout.LayoutControlItem
End Class
