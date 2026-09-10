Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmPrintDashBoardPharmacy
    Inherits FormBase

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.SimpleButton1 = New DevExpress.XtraEditors.SimpleButton()
        Me.INDRbFuntionalUnit = New System.Windows.Forms.RadioButton()
        Me.INDRbPatient = New System.Windows.Forms.RadioButton()
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.INDRbHalfLetter = New System.Windows.Forms.RadioButton()
        Me.INDRbNeckband = New System.Windows.Forms.RadioButton()
        Me.INDGbPrintBy = New System.Windows.Forms.GroupBox()
        Me.INDGbPrintType = New System.Windows.Forms.GroupBox()
        Me.IndigoComboBoxEdit1 = New Presentation.Controls.IndigoComboBoxEdit(Me.components)
        Me.INDGbRegisterBy = New System.Windows.Forms.GroupBox()
        Me.INDRbNo = New System.Windows.Forms.RadioButton()
        Me.INDRbYes = New System.Windows.Forms.RadioButton()
        Me.INDGbRegisterType = New System.Windows.Forms.GroupBox()
        Me.INDRbPending = New System.Windows.Forms.RadioButton()
        Me.INDRbRequest = New System.Windows.Forms.RadioButton()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDGbPrintBy.SuspendLayout()
        Me.INDGbPrintType.SuspendLayout()
        CType(Me.IndigoComboBoxEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDGbRegisterBy.SuspendLayout()
        Me.INDGbRegisterType.SuspendLayout()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDGbRegisterType)
        Me.INDPanelControlBase.Controls.Add(Me.INDGbRegisterBy)
        Me.INDPanelControlBase.Controls.Add(Me.INDGbPrintType)
        Me.INDPanelControlBase.Controls.Add(Me.INDGbPrintBy)
        Me.INDPanelControlBase.Controls.Add(Me.SimpleButton1)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 24)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(284, 340)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Dock = System.Windows.Forms.DockStyle.None
        '
        'BarraBotones
        '
        Me.BarraBotones.Dock = System.Windows.Forms.DockStyle.None
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(1465, 75)
        '
        'SimpleButton1
        '
        Me.SimpleButton1.Location = New System.Drawing.Point(110, 311)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.SimpleButton1, False)
        Me.SimpleButton1.Name = "SimpleButton1"
        Me.SimpleButton1.Size = New System.Drawing.Size(75, 23)
        Me.SimpleButton1.TabIndex = 0
        Me.SimpleButton1.Text = "Imprimir"
        '
        'INDRbFuntionalUnit
        '
        Me.INDRbFuntionalUnit.AutoSize = True
        Me.INDRbFuntionalUnit.Location = New System.Drawing.Point(15, 20)
        Me.INDRbFuntionalUnit.Name = "INDRbFuntionalUnit"
        Me.INDRbFuntionalUnit.Size = New System.Drawing.Size(125, 17)
        Me.INDRbFuntionalUnit.TabIndex = 1
        Me.INDRbFuntionalUnit.TabStop = True
        Me.INDRbFuntionalUnit.Text = "Por Unidad Funcional"
        Me.INDRbFuntionalUnit.UseVisualStyleBackColor = True
        '
        'INDRbPatient
        '
        Me.INDRbPatient.AutoSize = True
        Me.INDRbPatient.Location = New System.Drawing.Point(15, 43)
        Me.INDRbPatient.Name = "INDRbPatient"
        Me.INDRbPatient.Size = New System.Drawing.Size(133, 17)
        Me.INDRbPatient.TabIndex = 2
        Me.INDRbPatient.TabStop = True
        Me.INDRbPatient.Text = "Detallado por Paciente"
        Me.INDRbPatient.UseVisualStyleBackColor = True
        '
        'INDRbHalfLetter
        '
        Me.INDRbHalfLetter.AutoSize = True
        Me.INDRbHalfLetter.Location = New System.Drawing.Point(15, 20)
        Me.INDRbHalfLetter.Name = "INDRbHalfLetter"
        Me.INDRbHalfLetter.Size = New System.Drawing.Size(142, 17)
        Me.INDRbHalfLetter.TabIndex = 4
        Me.INDRbHalfLetter.TabStop = True
        Me.INDRbHalfLetter.Text = "Impresion a Media Carta"
        Me.INDRbHalfLetter.UseVisualStyleBackColor = True
        '
        'INDRbNeckband
        '
        Me.INDRbNeckband.AutoSize = True
        Me.INDRbNeckband.Location = New System.Drawing.Point(15, 43)
        Me.INDRbNeckband.Name = "INDRbNeckband"
        Me.INDRbNeckband.Size = New System.Drawing.Size(106, 17)
        Me.INDRbNeckband.TabIndex = 5
        Me.INDRbNeckband.TabStop = True
        Me.INDRbNeckband.Text = "Impresion a Trilla"
        Me.INDRbNeckband.UseVisualStyleBackColor = True
        '
        'INDGbPrintBy
        '
        Me.INDGbPrintBy.Controls.Add(Me.INDRbFuntionalUnit)
        Me.INDGbPrintBy.Controls.Add(Me.INDRbPatient)
        Me.INDGbPrintBy.Location = New System.Drawing.Point(45, 3)
        Me.INDGbPrintBy.Name = "INDGbPrintBy"
        Me.INDGbPrintBy.Size = New System.Drawing.Size(222, 70)
        Me.INDGbPrintBy.TabIndex = 7
        Me.INDGbPrintBy.TabStop = False
        Me.INDGbPrintBy.Text = "Imprimir Por:"
        '
        'INDGbPrintType
        '
        Me.INDGbPrintType.Controls.Add(Me.INDRbHalfLetter)
        Me.INDGbPrintType.Controls.Add(Me.INDRbNeckband)
        Me.INDGbPrintType.Location = New System.Drawing.Point(45, 81)
        Me.INDGbPrintType.Name = "INDGbPrintType"
        Me.INDGbPrintType.Size = New System.Drawing.Size(222, 70)
        Me.INDGbPrintType.TabIndex = 8
        Me.INDGbPrintType.TabStop = False
        Me.INDGbPrintType.Text = "Tipo Impresion:"
        '
        'INDGbRegisterBy
        '
        Me.INDGbRegisterBy.Controls.Add(Me.INDRbNo)
        Me.INDGbRegisterBy.Controls.Add(Me.INDRbYes)
        Me.INDGbRegisterBy.Location = New System.Drawing.Point(45, 159)
        Me.INDGbRegisterBy.Name = "INDGbRegisterBy"
        Me.INDGbRegisterBy.Size = New System.Drawing.Size(222, 70)
        Me.INDGbRegisterBy.TabIndex = 9
        Me.INDGbRegisterBy.TabStop = False
        Me.INDGbRegisterBy.Text = "Registro Por Hoja:"
        '
        'INDRbNo
        '
        Me.INDRbNo.AutoSize = True
        Me.INDRbNo.Location = New System.Drawing.Point(15, 44)
        Me.INDRbNo.Name = "INDRbNo"
        Me.INDRbNo.Size = New System.Drawing.Size(38, 17)
        Me.INDRbNo.TabIndex = 7
        Me.INDRbNo.TabStop = True
        Me.INDRbNo.Text = "No"
        Me.INDRbNo.UseVisualStyleBackColor = True
        '
        'INDRbYes
        '
        Me.INDRbYes.AutoSize = True
        Me.INDRbYes.Location = New System.Drawing.Point(15, 21)
        Me.INDRbYes.Name = "INDRbYes"
        Me.INDRbYes.Size = New System.Drawing.Size(33, 17)
        Me.INDRbYes.TabIndex = 6
        Me.INDRbYes.TabStop = True
        Me.INDRbYes.Text = "Si"
        Me.INDRbYes.UseVisualStyleBackColor = True
        '
        'INDGbRegisterType
        '
        Me.INDGbRegisterType.Controls.Add(Me.INDRbPending)
        Me.INDGbRegisterType.Controls.Add(Me.INDRbRequest)
        Me.INDGbRegisterType.Location = New System.Drawing.Point(45, 235)
        Me.INDGbRegisterType.Name = "INDGbRegisterType"
        Me.INDGbRegisterType.Size = New System.Drawing.Size(222, 70)
        Me.INDGbRegisterType.TabIndex = 10
        Me.INDGbRegisterType.TabStop = False
        Me.INDGbRegisterType.Text = "Tipo:"
        '
        'INDRbPending
        '
        Me.INDRbPending.AutoSize = True
        Me.INDRbPending.Location = New System.Drawing.Point(15, 44)
        Me.INDRbPending.Name = "INDRbPending"
        Me.INDRbPending.Size = New System.Drawing.Size(78, 17)
        Me.INDRbPending.TabIndex = 7
        Me.INDRbPending.TabStop = True
        Me.INDRbPending.Text = "Pendientes"
        Me.INDRbPending.UseVisualStyleBackColor = True
        '
        'INDRbRequest
        '
        Me.INDRbRequest.AutoSize = True
        Me.INDRbRequest.Location = New System.Drawing.Point(15, 21)
        Me.INDRbRequest.Name = "INDRbRequest"
        Me.INDRbRequest.Size = New System.Drawing.Size(75, 17)
        Me.INDRbRequest.TabIndex = 6
        Me.INDRbRequest.TabStop = True
        Me.INDRbRequest.Text = "Solicitudes"
        Me.INDRbRequest.UseVisualStyleBackColor = True
        '
        'FrmPrintDashBoardPharmacy
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 364)
        Me.Name = "FrmPrintDashBoardPharmacy"
        Me.Opacity = 1.0R
        Me.Text = "Impresion Solicitud"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDGbPrintBy.ResumeLayout(False)
        Me.INDGbPrintBy.PerformLayout()
        Me.INDGbPrintType.ResumeLayout(False)
        Me.INDGbPrintType.PerformLayout()
        CType(Me.IndigoComboBoxEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDGbRegisterBy.ResumeLayout(False)
        Me.INDGbRegisterBy.PerformLayout()
        Me.INDGbRegisterType.ResumeLayout(False)
        Me.INDGbRegisterType.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents IndigoSimpleButton1 As Presentation.Controls.IndigoSimpleButton
    Friend WithEvents INDRbPatient As System.Windows.Forms.RadioButton
    Friend WithEvents INDRbFuntionalUnit As System.Windows.Forms.RadioButton
    Friend WithEvents SimpleButton1 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl
    Friend WithEvents INDRbNeckband As System.Windows.Forms.RadioButton
    Friend WithEvents INDRbHalfLetter As System.Windows.Forms.RadioButton
    Friend WithEvents INDGbPrintType As System.Windows.Forms.GroupBox
    Friend WithEvents INDGbPrintBy As System.Windows.Forms.GroupBox
    Friend WithEvents IndigoComboBoxEdit1 As Presentation.Controls.IndigoComboBoxEdit
    Friend WithEvents INDGbRegisterBy As System.Windows.Forms.GroupBox
    Friend WithEvents INDRbNo As System.Windows.Forms.RadioButton
    Friend WithEvents INDRbYes As System.Windows.Forms.RadioButton
    Friend WithEvents INDGbRegisterType As System.Windows.Forms.GroupBox
    Friend WithEvents INDRbPending As System.Windows.Forms.RadioButton
    Friend WithEvents INDRbRequest As System.Windows.Forms.RadioButton
End Class
