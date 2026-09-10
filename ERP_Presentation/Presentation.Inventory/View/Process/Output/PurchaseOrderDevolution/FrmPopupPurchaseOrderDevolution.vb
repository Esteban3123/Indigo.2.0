'***********************************************************************
' Assembly         : Presentacion.Inventory
' Author           : Carlos Ernesto Cordoba
' Created          : 19/05/2016
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
Imports Presentation.Common.MVP
Imports Infrastructure.Data.Xpo.InventoryRepository

#End Region

Public Class FrmPopupPurchaseOrderDevolution

#Region "EVENTS"
    Public Event AddDevolutionsEventArgs(sender As Object, e As AddDevolutionsEventArgs)
#End Region

#Region "GLOBALS"
    ''' <summary>
    ''' id del proveedor
    ''' </summary>
    ''' <remarks></remarks>
    Dim _supplierId As Integer
    ''' <summary>
    ''' listado de los detalles ya agregados
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listPurcharseOrderDetailTmp As New List(Of PurchaseOrderDevolutionDetail)
#End Region

#Region "PROPERTIES"
    WriteOnly Property SupplierId As Integer
        Set(value As Integer)
            _supplierId = value
        End Set
    End Property

    WriteOnly Property ListPurchaseOrderDevolutionDetail As List(Of PurchaseOrderDevolutionDetail)
        Set(value As List(Of PurchaseOrderDevolutionDetail))
            If value IsNot Nothing Then
                _listPurcharseOrderDetailTmp = New List(Of PurchaseOrderDevolutionDetail)(value.ToArray())
            End If
        End Set
    End Property

    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String
        Set(value As String)
            If Icono = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text, Me)
            ElseIf Icono = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text, Me)
            ElseIf Icono = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If
        End Set
    End Property
#End Region

#Region "HANDLES"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _supplierId = Nothing
        _listPurcharseOrderDetailTmp = Nothing
    End Sub

    Private Sub RepositoryItemSpinEdit1_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles RepositoryItemSpinEdit1.EditValueChanging
        If e.NewValue Is String.Empty Then
            Exit Sub
        End If
        Dim detailTmp = DirectCast(INDGvDevolution.GetFocusedRow, InventoryPurcharseOrderDetailXpo)
        If e.NewValue > detailTmp.OutstandingQuantity Then
            Mensaje(EeventViewerImages.Advertencia) = "La cantidad a devolver no puede ser mayor a la cantidad pendiente"
            e.Cancel = True
        End If
    End Sub

    Private Sub FrmPopupPurchaseOrderDevolution_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Using model As New MPurchaseOrderDevolution(Me.Tag)
            INDGcDevolution.DataSource = model.ListPurchaseOrderDetailBySupplierId(_supplierId)
            If _listPurcharseOrderDetailTmp IsNot Nothing Then
                For Each item In _listPurcharseOrderDetailTmp
                    Dim detail = CType(INDGcDevolution.DataSource, XPCollection(Of InventoryPurcharseOrderDetailXpo)).Where(Function(x) x.Id = item.PurchaseOrderDetailId).FirstOrDefault()
                    If detail IsNot Nothing Then
                        detail.DevolutionQuantity = item.Quantity
                    End If
                Next
                INDGcDevolution.RefreshDataSource()
            End If
        End Using
    End Sub

    Private Sub FrmPopupPurchaseOrderDevolution_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub INDBtnAdd_Click(sender As Object, e As EventArgs) Handles INDBtnAdd.Click
        Dim _listPurcharseOrderDetail = New List(Of PurchaseOrderDevolutionDetail)
        For Each item In CType(INDGcDevolution.DataSource, XPCollection(Of InventoryPurcharseOrderDetailXpo)).Where(Function(x) x.DevolutionQuantity > 0).ToList()
            Dim PurchaseOrderDevolutionDetail As New PurchaseOrderDevolutionDetail
            With PurchaseOrderDevolutionDetail
                .PurchaseCode = item.PurchaseOrderId.Code
                .ProductCode = item.ProductId.CodeName
                .OutstandingQuantity = item.OutstandingQuantity
                .Quantity = item.DevolutionQuantity
                .PurchaseOrderDetailId = item.Id
            End With
            _listPurcharseOrderDetail.Add(PurchaseOrderDevolutionDetail)
        Next
        Dim args As New AddDevolutionsEventArgs
        args.ListPurchaseOrderDevolutionDetail = _listPurcharseOrderDetail
        RaiseEvent AddDevolutionsEventArgs(Nothing, args)
        Me.Close()
    End Sub
#End Region

End Class

''' <summary>
''' clase para retornar el listado con los detalles
''' </summary>
''' <remarks></remarks>
Public Class AddDevolutionsEventArgs
    Inherits EventArgs

    Property ListPurchaseOrderDevolutionDetail As List(Of PurchaseOrderDevolutionDetail)
End Class