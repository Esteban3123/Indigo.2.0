Imports System
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Resources
Imports TechTalk.SpecFlow

Namespace Application.AdministracionEfectivo

    <Binding()>
    Public Class SaveAndConfirmVoucherTransactionSteps
#Region "Fields"
        Private ReadOnly _treasuryContext As TreasuryContext
        Private _voucherTransaction As New Domain.Entities.VoucherTransaction
        'Private _listPortfolioNoteDetail As New Domain.Entities.TrackableCollection(Of PortfolioNoteDetail)
        'Private _ListPortfolioNoteAccountReceivableAdvance As New Domain.Entities.TrackableCollection(Of PortfolioNoteAccountReceivableAdvance)
        'Private _EntranceVoucherDetail As New Domain.Entities.EntranceVoucherDetail
        Private _actionResult As New ActionResult(Of Domain.Entities.VoucherTransaction)
        Private _voucherDetail As Domain.Entities.VoucherTransactionDetails
        Private _listVoucherDetail As New Domain.Entities.TrackableCollection(Of Domain.Entities.VoucherTransactionDetails)
#End Region


#Region "Builder"
        Public Sub New(TreasuryContext As TreasuryContext)
            _treasuryContext = TreasuryContext
        End Sub
#End Region
        <TechTalk.SpecFlow.Given("Genero Maestro y Detalles")>
        Public Sub DadoGeneroMaestroYDetalles()
            Dim expenseId As Int16() = {1, 2, 3, 4, 5, 6, 7, 8, 9}
            Dim valueTotal As Decimal = 0
            With _voucherTransaction
                .Id = 0
                .Code = ""
                .IdThirdParty = 59943
                .IdMainAccount = 3886
                .VoucherClass = 1
                .ExpenseType = 1
                .Detail = "UnitTest"
                .DocumentDate = DateTime.Now
                .IdEntityBankAccount = 2
                '<Value>2</Value> calcular luego
                .PaymentMethod = 2
                .NoteNumber = DateTime.Now.ToString("yyyyMMddhhmmssmmm") + Rnd().ToString
                .CheckNumber = 0
                .TaxByMil = False
                .TaxByMilValue = 0
                .CashRegisterExpense = False
                .RefundCashRegisterExpense = False
                .BeneficiaryIdentification = "1075237834"
                .Beneficiary = "CARLOS MARIO ARIAS RUBIANO"
                .TransactionRelationship = False
                .CheckReconciled = False
                .Printed = False
                .RTEValue = 0 'calcular luego
                .IVAValue = 0 'calcular luego
                .ICAValue = 0 'calcular luego
                .OtherValue = 0
                .BankAccountNumber = "4560-0140533"
                .BankName = "BANCOLOMBIA SA"
                .IdUnitOperative = 14
                .Status = 2
                '<CreationDate>01/01/0001 00:00:00</CreationDate>
                '<ModificationUser>999</ModificationUser>
                '<ModificationDate>27/07/2017 10:25:40</ModificationDate>
                '<ConfirmationUser>999</ConfirmationUser>
                '<ConfirmationDate>27/07/2017 10:25:40</ConfirmationDate>
                '<EmailSent>False</EmailSent>
                '<Prefix>N</Prefix>
                '<IsDispersionFundGenerated>False</IsDispersionFundGenerated>

            End With





            For i As Int16 = 1 To expenseId.Length
                Dim _expenseConcepts As Domain.Entities.ExpenseConcepts
                _expenseConcepts = _treasuryContext._expenseConceptAdminService.GetExpenseConceptById(expenseId(i - 1), _treasuryContext._audit)
                Dim _mainAccounts As Domain.Entities.MainAccounts
                _mainAccounts = _treasuryContext._PUCAdminService.GetAccountById(_expenseConcepts.IdMainAccount, False)
                If _mainAccounts.HandlesCostCenter Then
                    Assert.Fail("No se ha definido centro de costo")
                End If
                If _mainAccounts.RetencionType <> 0 Then
                    Assert.Fail("No se ha definido retencion")
                End If
                AssigningValues(_expenseConcepts, _mainAccounts)

                Dim advance = New Domain.Entities.TreasuryAdvances()
                With advance
                    .IdVoucherTransactionDetail = _voucherDetail.Id
                    .Detail = _voucherDetail.AdvanceDetail
                    .Value = _voucherDetail.AdvanceValue
                End With
                _voucherDetail.TreasuryAdvances.Add(advance)


                If _treasuryContext._accountPayableAdminService.GetCountAccountPayableShareByAccountPayIdThirdIdAccountAndState(_voucherTransaction.IdThirdParty, _mainAccounts.Id, 2, _treasuryContext._audit) <> 0 Then
                    Dim _accountPayable As Domain.Entities.AccountPayable = _treasuryContext._accountPayableAdminService.GetAccountPayableByIdThirdIdAccountAndState(_voucherTransaction.IdThirdParty, _mainAccounts.Id, 2, _treasuryContext._audit).FirstOrDefault
                    Dim _accountPayableShares = _accountPayable.AccountPayableShares.Where(Function(x) x.Balance > 1).FirstOrDefault
                    If (_accountPayableShares IsNot Nothing) Then
                        Dim _dischargeBill As New Domain.Entities.DischargeBill()
                        With _dischargeBill
                            .IdAccountPayable = _accountPayable.AccountPayableShares.FirstOrDefault.IdAccountPayable
                            .IdAccountPayableShare = _accountPayableShares.Id
                            .AccountPayableBillNumber = _accountPayable.BillNumber
                            .AccountPayableShareDateExpires = _accountPayableShares.DateExpires
                            .AccountPayableShareBalance = _accountPayableShares.Balance
                            .AccountPayableShareShare = _accountPayableShares.Share
                            .AdvancePercent = 0
                            .AdvancedValue = 1
                            .OperativeUnitId = _accountPayable.IdOperatingUnit
                            .IdPaymentConcept = 1
                            .ExpenseConceptId = _expenseConcepts.Id
                            ''Consulto las edades de cartera
                            'If Not DictionaryAgesPayments.ContainsKey(.OperativeUnitId) Then
                            '    Using Model As New MAgesPayment(Me.Tag)
                            '        Dim _agesPayment As List(Of AgesPayments) = (Model.ListAgesPaymentByUnitOperativeIdSimple(.OperativeUnitId)).ObjectEmbbeded
                            '        DictionaryAgesPayments.Add(.OperativeUnitId, _agesPayment)
                            '    End Using
                            'End If
                            Dim daysExpired As Integer = (Date.Now - .AccountPayableShareDateExpires).TotalDays
                            If daysExpired <= 0 Then
                                .ColorAgePortFolio = -1 'Color.FromArgb(Convert.ToInt32(ePortfolioAge.ColorDefault))
                            Else
                                .ColorAgePortFolio = -1
                            End If
                        End With
                        _voucherDetail.DischargeBill.Add(_dischargeBill)
                        _voucherDetail.Value = _dischargeBill.AdvancedValue + _voucherDetail.AdvanceValue
                        valueTotal = valueTotal + _voucherDetail.Value
                    Else
                        _voucherDetail.Value = _voucherDetail.AdvanceValue
                        valueTotal = valueTotal + _voucherDetail.Value
                    End If

                End If

                _listVoucherDetail.Add(_voucherDetail)
                'Dim _listAdvancePayment As List(Of Domain.Entities.AdvancePayments) = _treasuryContext._moneyAdvanceAdminService.ListAdvancePaymentByThirdId(_voucherTransaction.IdThirdParty).ObjectEmbbeded
            Next

            _voucherTransaction.Value = valueTotal
            _voucherTransaction.VoucherTransactionDetails = _listVoucherDetail


        End Sub

        <TechTalk.SpecFlow.Given("Guardo y confirmo el comprobante de egreso (.*)")>
        Public Sub DadoGuardoYConfirmoElComprobanteDeEgreso(ByVal p0 As Int32)

            Dim _currentSequence As Long
            Dim sequence = _treasuryContext._treasurySequenseAdminService.GetSequenseByIdForm("636")
            If sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                _currentSequence = sequence.TreasurySequenceDetail(0).Id
            ElseIf sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If sequence.TreasurySequenceDetail.Any(Function(S) S.IdOperatingUnit = _voucherTransaction.IdUnitOperative) Then
                    _currentSequence = sequence.TreasurySequenceDetail.Where(Function(s) s.IdOperatingUnit = _voucherTransaction.IdUnitOperative).SingleOrDefault().Id
                End If
            ElseIf sequence.Scope.Equals("OU") Then 'El ambito es por tipo
                If sequence.TreasurySequenceDetail.Any(Function(S) S.Type = _voucherTransaction.VoucherClass) Then
                    _currentSequence = sequence.TreasurySequenceDetail.Where(Function(s) s.Type = _voucherTransaction.VoucherClass).SingleOrDefault().Id
                End If
            End If
            _actionResult = _treasuryContext._voucherTransactionAdminService.SaveVoucherTransaction(_voucherTransaction, _treasuryContext._audit, True)

            If Not _actionResult.StateResult Then
                Assert.Fail("Hubo un Error al guardar: " + _actionResult.Message)
            End If
        End Sub

        <TechTalk.SpecFlow.Then("Este es almacenado y Confirmado")>
        Public Sub EntoncesEsteEsAlmacenadoYConfirmado()
            Assert.IsTrue(_actionResult.StateResult, _actionResult.Message)
        End Sub


        ''' <summary>
        ''' Asigna los valores a la entidad detalle
        ''' </summary>
        Private Sub AssigningValues(ByVal _expenseConcepts As Domain.Entities.ExpenseConcepts, ByVal _mainAccounts As Domain.Entities.MainAccounts)
            _voucherDetail = New Domain.Entities.VoucherTransactionDetails()
            With _voucherDetail
                .Id = 0
                '.CashRegisterId = Nothing
                '.IdEntityBankAccount = EntityBankAccountId
                .IdMainAccount = _mainAccounts.Id
                .Nature = _expenseConcepts.Nature
                .IdCostCenter = Nothing
                '.Value = ValueConcept calcular despues
                .Detail = ""
                .FullNameMainAccount = _mainAccounts.NumberName + " - " + _mainAccounts.Name
                '.FullNameCostCenter = INDsleCostCenter.Text

                .IdThirdParty = _voucherTransaction.IdThirdParty
                .IdExpenseConcept = _expenseConcepts.Id
                .PercentRetention = 0
                '.IdRetentionConcept = IdRetentionConcept
                .FullNameThirdParty = "1075237834 - CARLOS MARIO ARIAS RUBIANO"
                .ExpenseConceptCode = _expenseConcepts.Code
                .ExpenseConceptName = _expenseConcepts.Description
                .ExpenseConceptBehavior = _expenseConcepts.Behavior
                .AdvanceValue = 1
                .AdvanceDetail = "UnitTest Anticipo"
                '.BillingValue = INDTxtBillingValue.EditValue
                '.BaseValue = INDtxtBaseValue.EditValue
                If .Nature = 1 Then
                    .NatureName = ResourceManager.GetString("AccountNatureDebit")
                Else
                    .NatureName = ResourceManager.GetString("AccountNatureCredit")
                End If
                'Asignar el listado de las facturas a eliminar
                '.DischargeBillDelete = _listDischargeBillDelete
            End With
        End Sub
    End Class

End Namespace
