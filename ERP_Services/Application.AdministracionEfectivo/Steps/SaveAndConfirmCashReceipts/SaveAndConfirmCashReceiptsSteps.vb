Imports System
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports TechTalk.SpecFlow

Namespace Application.AdministracionEfectivo

    <Binding()>
    Public Class SaveAndConfirmCashReceiptsSteps
#Region "Fields"
        Private ReadOnly _TreasuryContext As TreasuryContext
        Private _CashReceipts As New CashReceipts
        Private _listCashReceiptDetails As New Domain.Entities.TrackableCollection(Of CashReceiptDetails)
        Private _listPaymentMethods As New Domain.Entities.TrackableCollection(Of PaymentMethods)
        Private _listPortfolioAdvance As New Domain.Entities.TrackableCollection(Of PortfolioAdvance)
        Private _actionResult As New ActionResult(Of CashReceipts)
#End Region

#Region "Builder"
        Public Sub New(TreasuryContext As TreasuryContext)
            _TreasuryContext = TreasuryContext
        End Sub
#End Region
        <TechTalk.SpecFlow.Given("Genero los maestros y detalles")>
        Public Sub DadoGeneroLosMaestrosYDetalles()
            GeneratePaymentMethod(1)
            GeneratePaymentMethod(2)
            GeneratePaymentMethod(3)

            Dim _cashReceiptDetails As New CashReceiptDetails With
                {.Id = 0,
                .IdCashReceipt = 0,
                .IdThirdParty = 18,
                .IdMainAccount = 3953,
                .Nature = 2,
                .IdCashReceiptConcept = 1,
                .CashReceiptConceptAffectation = 2,
                .Value = 3}
            _listCashReceiptDetails.Add(_cashReceiptDetails)

            Dim _PortfolioAdvance As New PortfolioAdvance With
                {.CrossingValue = 0,
                .CashReceiptDetailIdTmp = 4,
                .Id = 0,
                .CashReceiptId = 0,
                .CashReceiptDetailId = 0,
                .ThirdPartyId = 18,
                .MainAccountId = 3953,
                .DocumentDate = DateTime.Now,
                .Value = 3,
                .TransferValue = 0,
                .DebitValue = 0,
                .CreditValue = 0,
                .DistributionValue = 0,
                .Balance = 3,
                .Observations = "Prueba UnitTest",
                .OpeningBalance = False,
                .Status = 1}

            _listPortfolioAdvance.Add(_PortfolioAdvance)
            With _CashReceipts
                .PaymentResponsibles = "Someone from boyaca"
                .Id = 0
                .Code = ""
                .IdThirdParty = 18
                .CollectType = 1
                .IdMainAccount = 3869
                .Detail = "Prueba UnitTest"
                .DocumentDate = DateTime.Now
                .IdCashRegister = 50
                .Value = 3
                .OperatingUnitId = 14
                .Status = 1
                .AllowBudgetInterface = True
                .PortfolioAdvance = _listPortfolioAdvance
                .CashReceiptDetails = _listCashReceiptDetails
                .PaymentMethods = _listPaymentMethods
            End With


        End Sub

        <TechTalk.SpecFlow.Given("Guardo y confirmo los Recibos de caja (.*)")>
        Public Sub DadoGuardoYConfirmoLosRecibosDeCaja(ByVal p0 As Int32)
            Dim _currentSequence As Long
            Dim sequence = _TreasuryContext._treasurySequenseAdminService.GetSequenseByIdForm("635")
            If sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                _currentSequence = sequence.TreasurySequenceDetail(0).Id
            ElseIf sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If sequence.TreasurySequenceDetail.Any(Function(S) S.IdOperatingUnit = _CashReceipts.OperatingUnitId) Then
                    _currentSequence = sequence.TreasurySequenceDetail.Where(Function(s) s.IdOperatingUnit = _CashReceipts.OperatingUnitId).SingleOrDefault().Id
                End If
            End If
            _actionResult = _TreasuryContext._cashReceiptsAdminService.SaveAndConfirm(_CashReceipts, _TreasuryContext._audit, Nothing, Nothing, Nothing)

            If Not _actionResult.StateResult Then
                Assert.Fail("Hubo un Error: " + _actionResult.Message)
            End If
        End Sub

        <TechTalk.SpecFlow.Then("Estos son almacenados y Confirmados")>
        Public Sub EntoncesEstosSonAlmacenadosYConfirmados()
            Assert.IsTrue(_actionResult.StateResult, _actionResult.Message)
        End Sub



        ''' <summary>
        ''' metodo para agregar un metodo de pago
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub GeneratePaymentMethod(ByVal PaymentMethod As Int32)
            Dim PaymentMethods As New PaymentMethods
            Select Case PaymentMethod
                Case 1
                    PaymentMethods.Value = 1
                    PaymentMethods.PaymentMethodTypes = 1
                Case 2
                    PaymentMethods.IdBank = 17
                    PaymentMethods.CheckNumber = DateTime.Now.ToString("yyyyMMddhhmmssmmm")
                    PaymentMethods.DepositDate = DateTime.Now
                    PaymentMethods.Value = 1
                    PaymentMethods.PaymentMethodTypes = 2
                Case 3
                    PaymentMethods.IdCard = 1
                    PaymentMethods.CardNumber = DateTime.Now.ToString("yyyyMMddhhmmssmmm")
                    PaymentMethods.Value = 0.78
                    PaymentMethods.BaseValue = 1
                    PaymentMethods.CommissionValue = 0.1
                    PaymentMethods.PercentageCommission = 10
                    PaymentMethods.RTFValue = 0.01
                    PaymentMethods.PercentageRTF = 1
                    PaymentMethods.ICAValue = 0.11
                    PaymentMethods.PercentageICA = 11
                    PaymentMethods.PaymentMethodTypes = 3
            End Select
            If PaymentMethod = 3 Then
                AddCashReceiptDetailICA(PaymentMethods)
                AddCashReceiptDetailRTF(PaymentMethods)
                AddCashReceiptDetailCommision(PaymentMethods)
            End If
            _listPaymentMethods.Add(PaymentMethods)
        End Sub

        ''' <summary>
        ''' metodo para agregar el registro al detalle del recibo de caja por el valor del ica
        ''' </summary>
        ''' <param name="paymentMethodAdd"></param>
        ''' <remarks></remarks>
        Private Sub AddCashReceiptDetailICA(paymentMethodAdd As PaymentMethods)
            If paymentMethodAdd.ICAValue = 0 Then
                Exit Sub
            End If
            Dim card = _TreasuryContext._treasuryServiceCard.GetCardById(paymentMethodAdd.IdCard)
            Dim cashReceiptDetailRetention As CashReceiptDetails = New CashReceiptDetails
            Dim cashReceiptConceptICA = _TreasuryContext._cashReceiptConceptAdminService.GetCashReceiptConceptById(card.IdCashReceiptConceptICA)
            Dim account = _TreasuryContext._PUCAdminService.GetAccountById(cashReceiptConceptICA.IdMainAccount, True)
            If account.HandlesCostCenter Then
                If card.GetCostCenter = 1 Then
                    cashReceiptDetailRetention.IdCostCenter = card.ICACostCenterId
                ElseIf card.GetCostCenter = 2 Then
                    If card.CardCostCenter Is Nothing OrElse Not card.CardCostCenter.Any(Function(x) x.OperatingUnitId = 14) Then
                        Exit Sub
                    End If
                    cashReceiptDetailRetention.IdCostCenter = card.CardCostCenter.Where(Function(x) x.OperatingUnitId = 14).FirstOrDefault().CostCenterId
                End If
            End If
            cashReceiptDetailRetention.CodeNameCashReceiptConcept = cashReceiptConceptICA.Code + " - " + cashReceiptConceptICA.Name
            cashReceiptDetailRetention.IdThirdParty = card.IdThirdParty
            cashReceiptDetailRetention.IdMainAccount = cashReceiptConceptICA.IdMainAccount
            cashReceiptDetailRetention.Nature = 1
            cashReceiptDetailRetention.IdCashReceiptConcept = cashReceiptConceptICA.Id
            cashReceiptDetailRetention.Value = Decimal.Round(CDec(paymentMethodAdd.ICAValue), 2)
            cashReceiptDetailRetention.IdRetentionConcept = card.IdRetentionConceptICA
            cashReceiptDetailRetention.PercentageRetention = paymentMethodAdd.PercentageICA
            cashReceiptDetailRetention.BaseValue = paymentMethodAdd.BaseValue
            cashReceiptDetailRetention.CardNumber = paymentMethodAdd.CardNumber
            cashReceiptDetailRetention.CashReceiptConceptAffectation = cashReceiptConceptICA.Affectation
            _listCashReceiptDetails.Add(cashReceiptDetailRetention)
        End Sub

        ''' <summary>
        ''' metodo para agregar el registro al detalle del recibo de caja por el valor de lña retefuente
        ''' </summary>
        ''' <param name="paymentMethodAdd"></param>
        ''' <remarks></remarks>
        Private Sub AddCashReceiptDetailRTF(paymentMethodAdd As PaymentMethods)
            If paymentMethodAdd.RTFValue = 0 Then
                Exit Sub
            End If

            Dim card = _TreasuryContext._treasuryServiceCard.GetCardById(paymentMethodAdd.IdCard)
            Dim cashReceiptDetailRetention As CashReceiptDetails = New CashReceiptDetails
            Dim cashReceiptConceptRTF = _TreasuryContext._cashReceiptConceptAdminService.GetCashReceiptConceptById(card.IdCashReceiptConceptRTF)
            Dim account = _TreasuryContext._PUCAdminService.GetAccountById(cashReceiptConceptRTF.IdMainAccount, True)
            If account.HandlesCostCenter Then
                If card.GetCostCenter = 1 Then
                    cashReceiptDetailRetention.IdCostCenter = card.RTFCostCenterId
                ElseIf card.GetCostCenter = 2 Then
                    If card.CardCostCenter Is Nothing OrElse Not card.CardCostCenter.Any(Function(x) x.OperatingUnitId = 14) Then
                        Exit Sub
                    End If
                    cashReceiptDetailRetention.IdCostCenter = card.CardCostCenter.Where(Function(x) x.OperatingUnitId = 14).FirstOrDefault().CostCenterId
                End If
            End If
            cashReceiptDetailRetention.CodeNameCashReceiptConcept = cashReceiptConceptRTF.Code + " - " + cashReceiptConceptRTF.Name
            cashReceiptDetailRetention.IdThirdParty = card.IdThirdParty
            cashReceiptDetailRetention.IdMainAccount = cashReceiptConceptRTF.IdMainAccount
            cashReceiptDetailRetention.Nature = 1
            cashReceiptDetailRetention.IdCashReceiptConcept = cashReceiptConceptRTF.Id
            cashReceiptDetailRetention.Value = Decimal.Round(CDec(paymentMethodAdd.RTFValue), 2)
            cashReceiptDetailRetention.IdRetentionConcept = card.IdRetentionConceptRTF
            cashReceiptDetailRetention.PercentageRetention = paymentMethodAdd.PercentageRTF
            cashReceiptDetailRetention.BaseValue = paymentMethodAdd.BaseValue
            cashReceiptDetailRetention.CardNumber = paymentMethodAdd.CardNumber
            cashReceiptDetailRetention.CashReceiptConceptAffectation = cashReceiptConceptRTF.Affectation
            _listCashReceiptDetails.Add(cashReceiptDetailRetention)
        End Sub

        ''' <summary>
        ''' metodo para agregar el registro al detalle del recibo de caja por el valor de la comision
        ''' </summary>
        ''' <param name="paymentMethodAdd"></param>
        ''' <remarks></remarks>
        Private Sub AddCashReceiptDetailCommision(paymentMethodAdd As PaymentMethods)
            If paymentMethodAdd.CommissionValue = 0 Then
                Exit Sub
            End If

            Dim card = _TreasuryContext._treasuryServiceCard.GetCardById(paymentMethodAdd.IdCard)
            Dim cashReceiptDetailRetention As CashReceiptDetails = New CashReceiptDetails
            Dim cashReceiptConceptCommision = _TreasuryContext._cashReceiptConceptAdminService.GetCashReceiptConceptById(card.IdCashReceiptConceptCommision)
            Dim account = _TreasuryContext._PUCAdminService.GetAccountById(cashReceiptConceptCommision.IdMainAccount, True)
            If account.HandlesCostCenter Then
                If card.GetCostCenter = 1 Then
                    cashReceiptDetailRetention.IdCostCenter = card.CommisionCostCenterId
                ElseIf card.GetCostCenter = 2 Then
                    If card.CardCostCenter Is Nothing OrElse Not card.CardCostCenter.Any(Function(x) x.OperatingUnitId = 14) Then
                        Exit Sub
                    End If
                    cashReceiptDetailRetention.IdCostCenter = card.CardCostCenter.Where(Function(x) x.OperatingUnitId = 14).FirstOrDefault().CostCenterId
                End If
            End If

            cashReceiptDetailRetention.CodeNameCashReceiptConcept = cashReceiptConceptCommision.Code + " - " + cashReceiptConceptCommision.Name
            cashReceiptDetailRetention.IdThirdParty = card.IdThirdParty
            cashReceiptDetailRetention.IdMainAccount = cashReceiptConceptCommision.IdMainAccount
            cashReceiptDetailRetention.Nature = 1
            cashReceiptDetailRetention.IdCashReceiptConcept = cashReceiptConceptCommision.Id
            cashReceiptDetailRetention.Value = Decimal.Round(CDec(paymentMethodAdd.CommissionValue), 2)
            cashReceiptDetailRetention.IdRetentionConcept = card.IdRetentionConceptCommision
            cashReceiptDetailRetention.PercentageRetention = paymentMethodAdd.PercentageCommission
            cashReceiptDetailRetention.BaseValue = paymentMethodAdd.BaseValue
            cashReceiptDetailRetention.CardNumber = paymentMethodAdd.CardNumber
            cashReceiptDetailRetention.CashReceiptConceptAffectation = cashReceiptConceptCommision.Affectation
            _listCashReceiptDetails.Add(cashReceiptDetailRetention)

        End Sub

    End Class

End Namespace
