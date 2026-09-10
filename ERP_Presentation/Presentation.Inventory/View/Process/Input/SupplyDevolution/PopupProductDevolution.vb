'***********************************************************************
' Assembly         : Presentacion.Treasury
' Author           : Carlos Ernesto Córdoba
' Created          : 22-04-2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Inventory.MVP
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Controls
Imports Domain.Entities
Imports Domain.Base.Entities
Imports DevExpress.Xpo
Imports System.Drawing
Imports Presentation.Base.BaseClass
Imports Infrastructure.Data.Xpo.CommonRepository
Imports System.Text
Imports Presentation.Glosas
Imports Presentation.Maintenance
Imports Infrastructure.Data.Xpo.InventoryRepository

#End Region

Public Class PopupProductDevolution
    Dim dataSourceProducts As XPCollection(Of PharmaceuticalDispensingDetailBatchSerialXpo)
    ''' <summary>
    ''' listado del detalle de la devolucion
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listPharmaceuticalDispensingDevolutionDetail As List(Of PharmaceuticalDispensingDevolutionDetail)
    Property ListPharmaceuticalDispensingDevolutionDetail As List(Of PharmaceuticalDispensingDevolutionDetail)
        Get
            Return _listPharmaceuticalDispensingDevolutionDetail
        End Get
        Set(value As List(Of PharmaceuticalDispensingDevolutionDetail))
            _listPharmaceuticalDispensingDevolutionDetail = New List(Of PharmaceuticalDispensingDevolutionDetail)(value.ToArray())
        End Set
    End Property

    Dim _admissionNumber As String
    WriteOnly Property AdmissionNumber As String
        Set(value As String)
            _admissionNumber = value
        End Set
    End Property

    Dim _warehouseId As Integer
    WriteOnly Property WarehouseId As Integer
        Set(value As Integer)
            _warehouseId = value
        End Set
    End Property

    Private Sub PopupProductDevolution_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Using model As New MSupplyDevolution(Me.Tag)
            dataSourceProducts = model.ListPharmaceuticalDispensingDetailBatchSerialByWarehouseAndAdmissionNumber(_warehouseId, _admissionNumber)
            INDGcProducts.DataSource = dataSourceProducts
        End Using
    End Sub

    Private Sub RepositoryItemSpinEdit1_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles RepositoryItemSpinEdit1.EditValueChanging
        If e.NewValue Is String.Empty Then
            Exit Sub
        End If
        Dim product = DirectCast(INDGvProducts.GetFocusedRow(), PharmaceuticalDispensingDetailBatchSerialXpo)
        If e.NewValue > product.OutstandingQuantity Then
            e.Cancel = True
            Mensaje(EeventViewerImages.Advertencia) = "La cantidad a devolver no puede ser mayor que la cantidad pendiente"
        End If
    End Sub

    Private Async Sub INDBtnOk_Click(sender As Object, e As EventArgs) Handles INDBtnOk.Click
        If (From x In dataSourceProducts Where x.QuantityDeliver > 0 Select x).ToList().Count = 0 Then
            Exit Sub
        End If
        If ListPharmaceuticalDispensingDevolutionDetail Is Nothing Then
            ListPharmaceuticalDispensingDevolutionDetail = New List(Of PharmaceuticalDispensingDevolutionDetail)
        End If
        Dim errors As StringBuilder = New StringBuilder()
        For Each product In (From x In dataSourceProducts Where x.QuantityDeliver > 0 Or x.RecordAdd = True Select x).ToList()
            Dim listAux = ListPharmaceuticalDispensingDevolutionDetail.Where(Function(d) d.ProductId = product.PharmaceuticalDispensingDetailId.ProductId.Id And d.PharmaceuticalDispensingDetailBatchSerialId = product.Id).ToList()
            If listAux.Count > 0 Then
                Dim idx As Int32 = IndexOfById(ListPharmaceuticalDispensingDevolutionDetail, listAux(0).Id)
                If ListPharmaceuticalDispensingDevolutionDetail(idx).Quantity <> product.QuantityDeliver Then
                    ListPharmaceuticalDispensingDevolutionDetail(idx).Quantity = product.QuantityDeliver
                    ListPharmaceuticalDispensingDevolutionDetail(idx).MarkAsModified()
                Else
                    ListPharmaceuticalDispensingDevolutionDetail(idx).MarkAsUnchanged()
                End If
            Else
                Dim PharmaceuticalDispensingDevolutionDetail = New PharmaceuticalDispensingDevolutionDetail
                PharmaceuticalDispensingDevolutionDetail.PharmaceuticalDispensingDetailBatchSerialId = product.Id
                PharmaceuticalDispensingDevolutionDetail.Quantity = product.QuantityDeliver
                PharmaceuticalDispensingDevolutionDetail.CodePharmaceuticalDispensing = product.PharmaceuticalDispensingDetailId.PharmaceuticalDispensingId.Code
                PharmaceuticalDispensingDevolutionDetail.ProductId = product.PharmaceuticalDispensingDetailId.ProductId.Id
                PharmaceuticalDispensingDevolutionDetail.CodeNameProduct = product.PharmaceuticalDispensingDetailId.ProductId.CodeName
                PharmaceuticalDispensingDevolutionDetail.PharmaceuticalDispensingDetailId = product.PharmaceuticalDispensingDetailId.Id

                'Es necesario consultar si el almacén de la dispensación es de consignación
                Using model As New MWarehouse("302")
                    Dim resultOperation = Await model.GetWarehouseById(product.PharmaceuticalDispensingDetailId.WarehouseId)
                    Dim warehouse = resultOperation.ObjectEmbbeded
                    If warehouse IsNot Nothing AndAlso warehouse.Id > 0 Then
                        'De ser de consignación, advierto que el almacén de la devolución debe ser el mismo de la dispensación
                        If product.PharmaceuticalDispensingDetailId.WarehouseId <> _warehouseId Then
                            errors.AppendLine(String.Format("El producto '{0}' de la dispensación '{1}' debe ser devuelto al almacén: {2} - {3}.", PharmaceuticalDispensingDevolutionDetail.CodeNameProduct.Trim, PharmaceuticalDispensingDevolutionDetail.CodePharmaceuticalDispensing.Trim, warehouse.Code.Trim, warehouse.Name.Trim))
                        End If
                    End If
                End Using

                ListPharmaceuticalDispensingDevolutionDetail.Add(PharmaceuticalDispensingDevolutionDetail)
            End If
        Next
        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors.ToString
        End If
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    ''' <summary>
    ''' Obtiene el inde
    ''' </summary>
    ''' <param name="list">Lista fuente</param>
    ''' <param name="id">Id del objeto a buscar</param>
    ''' <returns>Indice en la lista</returns>
    Private Function IndexOfById(ByVal list As List(Of PharmaceuticalDispensingDevolutionDetail), ByVal id As Int32)
        Dim idx As Int32 = -1
        For i = 0 To list.Count - 1
            If list(i).Id = id Then
                Return i
            End If
        Next
        Return idx
    End Function

    Private Sub PopupProductDevolution_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        If _listPharmaceuticalDispensingDevolutionDetail IsNot Nothing Then
            For Each item In _listPharmaceuticalDispensingDevolutionDetail
                Dim product = (From x In dataSourceProducts Where item.PharmaceuticalDispensingDetailBatchSerialId = x.Id And item.ProductId = x.PharmaceuticalDispensingDetailId.ProductId.Id Select x).FirstOrDefault()
                If product IsNot Nothing Then
                    product.QuantityDeliver = item.Quantity
                    product.RecordAdd = True
                End If
            Next
            INDGcProducts.RefreshDataSource()
        End If
    End Sub


    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
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
End Class