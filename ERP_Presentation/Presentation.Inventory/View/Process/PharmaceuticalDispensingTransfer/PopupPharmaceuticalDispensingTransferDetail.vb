#Region "Imports"

Imports DevExpress.Xpo
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo.InventoryRepository
Imports Presentation.Base
Imports Presentation.Inventory.MVP

#End Region

Public Class PopupPharmaceuticalDispensingTransferDetail

#Region "Properties"

    Dim _warehouseId As Integer
    WriteOnly Property WarehouseId As Integer
        Set(value As Integer)
            _warehouseId = value
        End Set
    End Property

    Dim _admissionNumber As String
    WriteOnly Property AdmissionNumber As String
        Set(value As String)
            _admissionNumber = value
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

#Region "Datasources"

    Property ListPharmaceuticalDispensingDetailBatchSerial As XPCollection(Of PharmaceuticalDispensingDetailBatchSerialXpo)
        Get
            Return CType(INDGcProducts.DataSource, XPCollection(Of PharmaceuticalDispensingDetailBatchSerialXpo))
        End Get
        Set(value As XPCollection(Of PharmaceuticalDispensingDetailBatchSerialXpo))
            INDGcProducts.DataSource = value
        End Set
    End Property

#End Region

#Region "Events"

#Region "Custom"

    ''' <summary>
    ''' evento para agregar un producto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event AddPharmaceuticalDispensingTransferDetail(sender As Object, e As AddPharmaceuticalDispensingTransferDetailEventArgs)

#End Region

#Region "Load"

    Private Sub PopupPharmaceuticalDispensingTransferDetail_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Using model As New MPharmaceuticalDispensingTransfer(Me.Tag)
            ListPharmaceuticalDispensingDetailBatchSerial = model.ListPharmaceuticalDispensingDetailBatchSerialByWarehouseAndAdmissionNumber(_warehouseId, _admissionNumber)
        End Using
    End Sub

#End Region

#Region "EditValueChanging"

    Private Sub RepositoryItemSpinEdit1_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles RepositoryItemSpinEdit1.EditValueChanging
        If e.NewValue Is String.Empty Then
            Exit Sub
        End If

        Dim pharmaceuticalDispensingDetailBatchSerial = DirectCast(INDGvProducts.GetFocusedRow(), PharmaceuticalDispensingDetailBatchSerialXpo)
        If e.NewValue > pharmaceuticalDispensingDetailBatchSerial.OutstandingQuantity Then
            e.Cancel = True
            Mensaje(EeventViewerImages.Advertencia) = "La cantidad a devolver no puede ser mayor que la cantidad pendiente"
        End If
    End Sub

#End Region

#Region "Click"

    Private Sub INDBtnOk_Click(sender As Object, e As EventArgs) Handles INDBtnOk.Click
        Try
            INDBtnOk.Enabled = False

            Dim ListPharmaceuticalDispensingTransferDetail = New List(Of PharmaceuticalDispensingTransferDetail)
            If ListPharmaceuticalDispensingDetailBatchSerial IsNot Nothing AndAlso ListPharmaceuticalDispensingDetailBatchSerial.Any(Function(d) d.QuantityDeliver > 0) Then
                For Each pharmaceuticalDispensingDetailBatchSerial In (From x In ListPharmaceuticalDispensingDetailBatchSerial Where x.QuantityDeliver > 0 Select x).ToList()
                    Dim PharmaceuticalDispensingTransferDetail = New PharmaceuticalDispensingTransferDetail
                    PharmaceuticalDispensingTransferDetail.PharmaceuticalDispensingCode = pharmaceuticalDispensingDetailBatchSerial.PharmaceuticalDispensingDetailId.PharmaceuticalDispensingId.Code
                    PharmaceuticalDispensingTransferDetail.PharmaceuticalDispensingDetailId = pharmaceuticalDispensingDetailBatchSerial.PharmaceuticalDispensingDetailId.Id
                    PharmaceuticalDispensingTransferDetail.ProductId = pharmaceuticalDispensingDetailBatchSerial.PharmaceuticalDispensingDetailId.ProductId.Id
                    PharmaceuticalDispensingTransferDetail.ProductCodeName = pharmaceuticalDispensingDetailBatchSerial.PharmaceuticalDispensingDetailId.ProductId.CodeName
                    PharmaceuticalDispensingTransferDetail.PharmaceuticalDispensingDetailBatchSerialId = pharmaceuticalDispensingDetailBatchSerial.Id
                    PharmaceuticalDispensingTransferDetail.Quantity = pharmaceuticalDispensingDetailBatchSerial.QuantityDeliver
                    ListPharmaceuticalDispensingTransferDetail.Add(PharmaceuticalDispensingTransferDetail)
                Next
            End If

            Dim args As New AddPharmaceuticalDispensingTransferDetailEventArgs
            args.ListPharmaceuticalDispensingTransferDetail = ListPharmaceuticalDispensingTransferDetail
            RaiseEvent AddPharmaceuticalDispensingTransferDetail(Nothing, args)
            Me.Close()
        Catch ex As Exception
            INDBtnOk.Enabled = True
            Throw ex
        End Try
    End Sub

#End Region

#End Region

End Class