Imports System
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base
Imports TechTalk.SpecFlow

Namespace Application.Inventory.Test

    <Binding()>
    Public Class SaveAndConfirmTransferOrderSteps


#Region "Fields"
        Private ReadOnly _InventoryContext As InventoryContext
        Private _transferOrder As New Domain.Entities.TransferOrder
        Private _transferOrderDetail As New Domain.Entities.TransferOrderDetail
        Private _listTransferOrderDetail As New Domain.Entities.TrackableCollection(Of Domain.Entities.TransferOrderDetail)
        Private _transferOrderDetailBatchSerial As New Domain.Entities.TransferOrderDetailBatchSerial

        Private _actionResult As New ActionResult(Of Domain.Entities.TransferOrder)
#End Region


#Region "Builder"
        Public Sub New(InventoryContext As InventoryContext)
            _InventoryContext = InventoryContext
        End Sub
#End Region
        <TechTalk.SpecFlow.Given("Genero la orden de traslado")>
        Public Sub DadoGeneroLaOrdenDeTraslado()

            'batchserial
            Dim PhysicalInventoryId = New Integer() {1310, 1311, 1312, 1313, 1314, 1315, 1316, 1317, 1318}
            'detail
            Dim ProductId = New Integer() {9044, 9043, 9042, 9041, 9040, 9039, 9038, 9037, 9036}


            For i As Double = 0 To 50

                Dim _listTransferOrderDetailBatchSerial As New Domain.Entities.TrackableCollection(Of Domain.Entities.TransferOrderDetailBatchSerial)
                _listTransferOrderDetailBatchSerial.Add(New Domain.Entities.TransferOrderDetailBatchSerial With
                                                    {.PhysicalInventoryId = 62,
                                                     .Quantity = 1,
                                                     .OutstandingQuantity = 1})

                _listTransferOrderDetail.Add(New Domain.Entities.TransferOrderDetail With
                                             {.ProductId = 6362,
                .InventoryQuantity = 253,
                .Quantity = 1,
                .Value = 606,
                .Description = "Prueba Test Orden de Traslado Detalle",
                .TransferOrderDetailBatchSerial = _listTransferOrderDetailBatchSerial})
            Next

            'With _transferOrderDetailBatchSerial
            '    .PhysicalInventoryId = 62
            '    .Quantity = 1
            '    .OutstandingQuantity = 1
            'End With

            'With _transferOrderDetail
            '    .ProductId = 6362
            '    .InventoryQuantity = 253
            '    .Quantity = 1
            '    .Value = 606
            '    .Description = "Prueba Test Orden de Traslado Detalle"
            '    .TransferOrderDetailBatchSerial.Add(_transferOrderDetailBatchSerial)
            'End With

            With _transferOrder
                .OperatingUnitId = 14
                .DocumentDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                .OrderType = 1
                .DispatchTo = 1
                .SourceWarehouseId = 1319
                .TargetWarehouseId = 1318
                .Description = "Prueba Test Orden de Traslado"
                .Status = 2
                .TransferOrderDetail = _listTransferOrderDetail
            End With
        End Sub

        <TechTalk.SpecFlow.Given("Luego guardo y confirmo la orden de traslado (.*)")>
        Public Sub DadoLuegoGuardoYConfirmoLaOrdenDeTraslado(ByVal Id As Int32)
            Dim _audit As AuditMessage = _InventoryContext._audit
            Dim IdSequence As Long = Id
            Dim SequenceC As Domain.Entities.InventorySequence = _InventoryContext._InventorySequenceAdminService.GetSequenseByIdForm(1519)
            _actionResult = _InventoryContext._TransferOrderAdminService.SaveTrasnferOrder(_transferOrder, _audit)

            If _actionResult.StateResult = False OrElse _actionResult.StateResultAux = False Then
                _actionResult.StateResult = False
                Assert.Fail("Ocurrio el siguiente Error: " + _actionResult.Message)
            End If
        End Sub

        <TechTalk.SpecFlow.Then("Esta es almacenada y Confirmada")> _
        Public Sub EntoncesEstaEsAlmacenadaYConfirmada()
            Assert.IsTrue(_actionResult.StateResult, _actionResult.Message)
        End Sub

    End Class

End Namespace
