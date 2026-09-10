<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class CtrJustification
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.INDPceJustificationPrescription = New DevExpress.XtraEditors.PopupContainerEdit()
        Me.INDPceJustification = New DevExpress.XtraEditors.PopupContainerEdit()
        Me.PopupContainerEdit1 = New DevExpress.XtraEditors.PopupContainerEdit()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoPopUpContainerEdit2 = New Presentation.Controls.IndigoPopUpContainerEdit(Me.components)
        CType(Me.INDPceJustificationPrescription.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPceJustification.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PopupContainerEdit1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoPopUpContainerEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPceJustificationPrescription
        '
        Me.IndigoPopUpContainerEdit2.SetButtonMoreOptions(Me.INDPceJustificationPrescription, False)
        Me.INDPceJustificationPrescription.CausesValidation = False
        Me.INDPceJustificationPrescription.Cursor = System.Windows.Forms.Cursors.Hand
        Me.INDPceJustificationPrescription.EditValue = "100000"
        Me.IndigoPopUpContainerEdit2.SetHostControl(Me.INDPceJustificationPrescription, Nothing)
        Me.INDPceJustificationPrescription.Location = New System.Drawing.Point(0, 5)
        Me.INDPceJustificationPrescription.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.INDPceJustificationPrescription.MinimumSize = New System.Drawing.Size(538, 32)
        Me.INDPceJustificationPrescription.Name = "INDPceJustificationPrescription"
        Me.IndigoPopUpContainerEdit2.SetOpenForm(Me.INDPceJustificationPrescription, False)
        Me.IndigoPopUpContainerEdit2.SetPopUpAnimation(Me.INDPceJustificationPrescription, False)
        Me.INDPceJustificationPrescription.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDPceJustificationPrescription.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDPceJustificationPrescription.Properties.Appearance.Options.UseBackColor = True
        Me.INDPceJustificationPrescription.Properties.Appearance.Options.UseFont = True
        Me.INDPceJustificationPrescription.Properties.Appearance.Options.UseTextOptions = True
        Me.INDPceJustificationPrescription.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDPceJustificationPrescription.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDPceJustificationPrescription.Properties.Mask.IgnoreMaskBlank = False
        Me.INDPceJustificationPrescription.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDPceJustificationPrescription.Properties.PopupSizeable = False
        Me.INDPceJustificationPrescription.Properties.ShowPopupCloseButton = False
        Me.INDPceJustificationPrescription.Properties.ShowPopupShadow = False
        Me.INDPceJustificationPrescription.Size = New System.Drawing.Size(538, 36)
        Me.INDPceJustificationPrescription.TabIndex = 11
        Me.IndigoPopUpContainerEdit2.SetTagForm(Me.INDPceJustificationPrescription, Nothing)
        Me.IndigoPopUpContainerEdit2.SetWpfControl(Me.INDPceJustificationPrescription, Nothing)
        '
        'INDPceJustification
        '
        Me.IndigoPopUpContainerEdit2.SetButtonMoreOptions(Me.INDPceJustification, False)
        Me.INDPceJustification.Cursor = System.Windows.Forms.Cursors.Hand
        Me.INDPceJustification.EditValue = "Justificación de Prescripción:"
        Me.IndigoPopUpContainerEdit2.SetHostControl(Me.INDPceJustification, Nothing)
        Me.INDPceJustification.Location = New System.Drawing.Point(0, 0)
        Me.INDPceJustification.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.INDPceJustification.MinimumSize = New System.Drawing.Size(292, 32)
        Me.INDPceJustification.Name = "INDPceJustification"
        Me.IndigoPopUpContainerEdit2.SetOpenForm(Me.INDPceJustification, False)
        Me.IndigoPopUpContainerEdit2.SetPopUpAnimation(Me.INDPceJustification, False)
        Me.INDPceJustification.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDPceJustification.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDPceJustification.Properties.Appearance.Options.UseBackColor = True
        Me.INDPceJustification.Properties.Appearance.Options.UseFont = True
        Me.INDPceJustification.Properties.Appearance.Options.UseTextOptions = True
        Me.INDPceJustification.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.INDPceJustification.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.INDPceJustification.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDPceJustification.Properties.PopupSizeable = False
        Me.INDPceJustification.Properties.ShowPopupCloseButton = False
        Me.INDPceJustification.Properties.ShowPopupShadow = False
        Me.INDPceJustification.Size = New System.Drawing.Size(292, 36)
        Me.INDPceJustification.TabIndex = 9
        Me.IndigoPopUpContainerEdit2.SetTagForm(Me.INDPceJustification, Nothing)
        Me.IndigoPopUpContainerEdit2.SetWpfControl(Me.INDPceJustification, Nothing)
        '
        'PopupContainerEdit1
        '
        Me.IndigoPopUpContainerEdit2.SetButtonMoreOptions(Me.PopupContainerEdit1, False)
        Me.PopupContainerEdit1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PopupContainerEdit1.EditValue = ""
        Me.IndigoPopUpContainerEdit2.SetHostControl(Me.PopupContainerEdit1, Nothing)
        Me.PopupContainerEdit1.Location = New System.Drawing.Point(-2, 32)
        Me.PopupContainerEdit1.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.PopupContainerEdit1.MinimumSize = New System.Drawing.Size(292, 32)
        Me.PopupContainerEdit1.Name = "PopupContainerEdit1"
        Me.IndigoPopUpContainerEdit2.SetOpenForm(Me.PopupContainerEdit1, False)
        Me.IndigoPopUpContainerEdit2.SetPopUpAnimation(Me.PopupContainerEdit1, False)
        Me.PopupContainerEdit1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.PopupContainerEdit1.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.PopupContainerEdit1.Properties.Appearance.Options.UseBackColor = True
        Me.PopupContainerEdit1.Properties.Appearance.Options.UseFont = True
        Me.PopupContainerEdit1.Properties.Appearance.Options.UseTextOptions = True
        Me.PopupContainerEdit1.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.PopupContainerEdit1.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.PopupContainerEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.PopupContainerEdit1.Properties.PopupSizeable = False
        Me.PopupContainerEdit1.Properties.ShowPopupCloseButton = False
        Me.PopupContainerEdit1.Properties.ShowPopupShadow = False
        Me.PopupContainerEdit1.Size = New System.Drawing.Size(292, 36)
        Me.PopupContainerEdit1.TabIndex = 12
        Me.IndigoPopUpContainerEdit2.SetTagForm(Me.PopupContainerEdit1, Nothing)
        Me.IndigoPopUpContainerEdit2.SetWpfControl(Me.PopupContainerEdit1, Nothing)
        '
        'CtrJustification
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Transparent
        Me.Controls.Add(Me.PopupContainerEdit1)
        Me.Controls.Add(Me.INDPceJustification)
        Me.Controls.Add(Me.INDPceJustificationPrescription)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.MaximumSize = New System.Drawing.Size(292, 57)
        Me.MinimumSize = New System.Drawing.Size(292, 57)
        Me.Name = "CtrJustification"
        Me.Size = New System.Drawing.Size(292, 57)
        CType(Me.INDPceJustificationPrescription.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPceJustification.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PopupContainerEdit1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoPopUpContainerEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents INDPceJustificationPrescription As DevExpress.XtraEditors.PopupContainerEdit
    Friend WithEvents INDPceJustification As DevExpress.XtraEditors.PopupContainerEdit
    Friend WithEvents IndigoPopUpContainerEdit2 As Controls.IndigoPopUpContainerEdit
    Friend WithEvents PopupContainerEdit1 As DevExpress.XtraEditors.PopupContainerEdit
End Class
