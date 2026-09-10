'***********************************************************************
' Assembly         : Presentacion.Inventory
' Author           : Henry Alejandro Vargas Polania 
' Created          : 23/12/2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Controls
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Resources
Imports System.ComponentModel
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Infrastructure.CrossCutting.Exceptions
Imports Presentation.Accounting
Imports System.Text
Imports Presentation.Inventory.MVP
Imports Presentation.Payroll
Imports Presentation.Controls.MVP
Imports DevExpress.Xpo
Imports System.Windows.Forms
Imports System.Drawing
Imports Presentation.Maintenance

#End Region

Public Class FrmPopUpProductPurchaseOrder

    Private _advanceValue As Decimal
    Dim ctrAdvance As CtrAdvanceTreasury

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        ctrAdvance = New CtrAdvanceTreasury()
        ctrAdvance.Title = "Total"
        ctrAdvance.WithEvent = False
        ctrAdvance.SetAdvance(AddressOf getAdvance)
        ctrAdvance.PrintAdvance()
        ctrAdvance.Dock = DockStyle.Fill
        AdditionalControlPanel.Controls.Add(ctrAdvance)
    End Sub

    ''' <summary>
    ''' Obtiene el valor del anticipo
    ''' </summary>
    ''' <returns></returns>
    Private Function getAdvance() As Decimal
        Dim _advance As Decimal = 0
        _advance = _advanceValue
        Return _advance
    End Function


    Private Sub INDSleProduct_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleProduct.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using form As New FrmProducts()
                form.ViewModeEditHold = True
                form.Size = New Size(800, 700)
                form.StartPosition = FormStartPosition.CenterParent
                form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
                form.MaximizeBox = False
                form.MinimizeBox = False
                Dim transparent = New FrmTransparent(form, False)
                transparent.ShowDialog()
            End Using
        End If
    End Sub
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _advanceValue = Nothing
        ctrAdvance = Nothing
    End Sub

    Private Sub FrmPopUpProductPurchaseOrder_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        BarraBotones.StatusRecordVisible = True
    End Sub
End Class