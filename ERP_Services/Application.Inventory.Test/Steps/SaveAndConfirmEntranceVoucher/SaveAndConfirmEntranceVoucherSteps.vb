

Imports System
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports TechTalk.SpecFlow

Namespace Application.Inventory.Test



    <Binding()>
    Public Class SaveAndConfirmEntranceVoucherSteps
#Region "Fields"
        Private ReadOnly _InventoryContext As InventoryContext
        Private _entranceVoucher As New Domain.Entities.EntranceVoucher
        Private _listEntranceVoucher As New Domain.Entities.TrackableCollection(Of EntranceVoucherDetail)
        Private _EntranceVoucherDetail As New Domain.Entities.EntranceVoucherDetail
        Private _actionResult As New ActionResult(Of Domain.Entities.EntranceVoucher)
        Private dato As Int16
#End Region


#Region "Builder"
        Public Sub New(InventoryContext As InventoryContext)
            _InventoryContext = InventoryContext
        End Sub
#End Region
        <TechTalk.SpecFlow.Given("Genero el comprobante de  entrada (.*)")>
        Public Sub DadoGeneroElComprobandeDeEntrada(ByVal reg_no As Int32)

            For i As Integer = 0 To 50
                _listEntranceVoucher.Add(New EntranceVoucherDetail With
               {.ProductId = 6362,
                 .Quantity = 2,
                .EntranceSource = 1,
                .UnitValue = 509.24,
                .LastValue = 509.2436,
                .SubTotalValue = 1018.0,
                .IvaPercentage = 19,
                .IvaValue = 194.0,
                .DiscountPercentage = 0,
                .DiscountValue = 0,
                .TotalValue = 1212.0,
                .RTFPercentage = 0,
                .RTFValue = 0})
            Next
            _entranceVoucher = New Domain.Entities.EntranceVoucher()
            With _entranceVoucher
                .OperatingUnitId = 14
                .DocumentDate = DateTime.Now.Date
                .SupplierId = 1211
                .SupplierDistributionLineId = 1218
                .SupplierTypeId = 86
                .WarehouseId = 1323
                .Description = "Prueba No " & reg_no
                .ResourceType = 0
                .RoundService = 1
                .IcaPercentage = 0
                .InvoiceNumber = DateTime.Now.ToString("yyyyMMddhhmmssmmm") + Rnd().ToString

                '.Prefix = Me._prefixSelected
                .InvoiceDate = DateTime.Now.Date
                .DayPeriod = 60
                .FreightValue = 0
                .FreightValueOutstanding = 0
                .FreightIVAPercentage = 3.0
                .FreightIVAValue = 0
                .FreightIVAValueOutstanding = 0
                .Value = 1018
                .ValueDiscount = 0
                .ValueTax = 194
                .WithholdingTax = 29
                .WithholdingICA = 0
                .RetentionSource = 0
                .RetentionOther = 0
                .DeductionOther = 0
                .DistrictTax = 0
                .TotalValue = 1183
                .WithholdingIvaPercentage = 0
                .Status = 1
                '.EntranceVoucherDetail.Add(_EntranceVoucherDetail)
                .EntranceVoucherDetail = _listEntranceVoucher
                'For Each item In ListEntranceVoucherDetail
                '    .EntranceVoucherDetail.Add(item)
                'Next
                'If ListEntranceVoucherDetailDelete IsNot Nothing AndAlso ListEntranceVoucherDetailDelete.Count > 0 Then
                '    For Each item In ListEntranceVoucherDetailDelete
                '        .EntranceVoucherDetail.Add(item)
                '    Next
                'End If
            End With
            _entranceVoucher.Value = _entranceVoucher.EntranceVoucherDetail.Sum(Function(x) x.SubTotalValue)
            _entranceVoucher.ValueTax = _entranceVoucher.EntranceVoucherDetail.Sum(Function(x) x.IvaValue)
            _entranceVoucher.ValueDiscount = _entranceVoucher.EntranceVoucherDetail.Sum(Function(x) x.DiscountValue)
            _entranceVoucher.RetentionSource = CDec(Utils.RoundValue(_entranceVoucher.EntranceVoucherDetail.Sum(Function(x) x.RTFValue), _entranceVoucher.RoundService))
            _entranceVoucher.WithholdingTax = CDec(Utils.RoundValue((_entranceVoucher.ValueTax + _entranceVoucher.FreightIVAValue) * (_entranceVoucher.WithholdingIvaPercentage / 100), _entranceVoucher.RoundService))
            _entranceVoucher.TotalValue = _entranceVoucher.Value + _entranceVoucher.ValueTax - _entranceVoucher.ValueDiscount - _entranceVoucher.WithholdingTax - _entranceVoucher.WithholdingICA - _entranceVoucher.RetentionSource - _entranceVoucher.RetentionOther - _entranceVoucher.DeductionOther - _entranceVoucher.DistrictTax + _entranceVoucher.FreightIVAValue + _entranceVoucher.FreightValue

        End Sub

        <TechTalk.SpecFlow.Given("Yo guardo y confirmo el comprobante de entrada (.*) (.*)")>
        Public Async Sub DadoYoGuardoElComprobanteDeEntrada(ByVal Id As Int32, ByVal reg_no As Int32)
            Dim _audit As AuditMessage = _InventoryContext._audit
            Dim IdSequence As Long = Id
            'Dim SequenceC As Domain.Entities.InventorySequence = _InventoryContext._InventorySequenceAdminService.GetSequenseByIdForm(1402)
            _actionResult = Await _InventoryContext._entranceVoucherAdminService.SaveAndConfirmbEntranceVoucherAsync(_entranceVoucher, ServerSessionValues.Current.CurrentHISContainer, _audit, IdSequence, Infrastructure.CrossCutting.Audit.Actions.Confirm, Nothing)
            If _actionResult.StateResult = False AndAlso _actionResult.StateResultAux = False Then
                _actionResult.StateResult = False
                Assert.Fail("No fue posible guardar el comprobante de entrada. Error: " + _actionResult.Message)
            ElseIf _actionResult.StateResult = True AndAlso _actionResult.StateResultAux = False Then
                _actionResult.StateResult = False
                Assert.Fail("No fue posible Confirmar el comprobante de entrada. Error: " + _actionResult.Message)
            ElseIf _actionResult.StateResult = True AndAlso _actionResult.StateResultAux = True Then
                _actionResult.StateResult = True
            End If
        End Sub


        <TechTalk.SpecFlow.Then("Este es almacenado y Confirmado")>
        Public Sub EntoncesEsteEsAlmacenadoYConfirmado()
            Assert.IsTrue(_actionResult.StateResult, _actionResult.Message)
        End Sub

    End Class

End Namespace

