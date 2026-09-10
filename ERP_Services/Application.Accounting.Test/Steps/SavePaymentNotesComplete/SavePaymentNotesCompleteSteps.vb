Imports System
Imports Domain.Base.Entities
Imports Domain.Entities
Imports TechTalk.SpecFlow

Namespace Application.Accounting.Test

    <Binding()>
    Public Class SavePaymentNotesCompleteSteps
#Region "Fields"
        Private ReadOnly _AccountingContext As AccountingContext
        Private _PaymentNotes As New Domain.Entities.PaymentNotes
        Private _ListPaymentsNoteDetails As New Domain.Entities.TrackableCollection(Of PaymentsNoteDetails)
        Private _ListAccountPayable As New List(Of AccountPayable)
        'Private _EntranceVoucherDetail As New Domain.Entities.EntranceVoucherDetail
        Private _actionResult As New ActionResult(Of Domain.Entities.PaymentNotes)
        Private dato As Int16
#End Region


#Region "Builder"
        Public Sub New(AccountingContext As AccountingContext)
            _AccountingContext = AccountingContext
        End Sub
#End Region
        <TechTalk.SpecFlow.Given("Genero las Notespayments y las AccountPayables")>
        Public Sub DadoGeneroLasNotespaymentsYLasAccountPayables()
            Dim _idAccountPayables = New Int32() {2297, 2266, 2229}
            Dim _adjustment = 10

            For y As Double = 1 To _idAccountPayables.Count
                Dim _AccountPayable As New AccountPayable
                _AccountPayable = _AccountingContext._accountPayableAdminService.GetAccountPayableByIdForNotes(_idAccountPayables(y - 1), _AccountingContext._audit)
                _AccountPayable.Adjustment = _adjustment
                _AccountPayable.Percentage = 0
                _AccountPayable.CostCenter = Nothing
                _AccountPayable.HandlesAddModifyDelete = 1
                _ListAccountPayable.Add(_AccountPayable)
            Next


            For i As Double = 1 To _adjustment * 3

                _ListPaymentsNoteDetails.Add(New PaymentsNoteDetails With
                                             {.IdAccountPayableConceptNotes = 695,
                                             .IdAccount = 5179,
                                             .IdThirdParty = 59943,
                                             .BaseValue = 1,
                                             .BillingValue = 0,
                                             .Value = 1,
                                             .Nature = 2,
                                             .Comments = "Prueba detalle Concepto",
                                             .IdCostCenter = 2})
            Next

            With _PaymentNotes
                .BudgetInterface = False
                .Code = ""
                .Comment = "Prueba Test"
                .CreationDate = DateTime.Now
                .Id = 0
                .IdOperatingUnit = 14
                .IdSupplier = 1246
                .IdSupplierDistributionLines = 1257
                .IndicatesBillAdvance = 0
                .Nature = 1
                .NoteDate = DateTime.Now
                .Reinstatement = False
                .Status = 1
                .IndicatesBillAdvance = 0
                .PaymentsNoteDetails = _ListPaymentsNoteDetails
            End With
        End Sub

        <TechTalk.SpecFlow.Given("Yo guardo y confirmo las Notas debito y credito (.*)")>
        Public Sub DadoYoGuardoYConfirmoLasNotasDebitoYCredito(ByVal p0 As Int32)
            Dim _currentSequence As Long
            Dim sequence = _AccountingContext._paymentsSequenseAdminService.GetSequenseByIdForm(731)
            If sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                _currentSequence = sequence.PaymentsSecuenceDetail(0).Id
            ElseIf sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If sequence.PaymentsSecuenceDetail.Any(Function(S) S.IdOperatingUnit = _PaymentNotes.IdOperatingUnit) Then
                    _currentSequence = sequence.PaymentsSecuenceDetail.Where(Function(s) s.IdOperatingUnit = _PaymentNotes.IdOperatingUnit).SingleOrDefault().Id
                End If
            End If
            _actionResult = _AccountingContext._notesDebitCreditAdminService.SavePaymentNotesComplete(_PaymentNotes, _ListAccountPayable, Nothing, True, _AccountingContext._audit, _currentSequence)
            If Not _actionResult.StateResult Then
                Assert.Fail("Hubo un Error: " + _actionResult.Message)
            End If
        End Sub

        <TechTalk.SpecFlow.Then("Estos son almacenados y Confirmados")> _
        Public Sub EntoncesEstosSonAlmacenadosYConfirmados()
            Assert.IsTrue(_actionResult.StateResult, _actionResult.Message)
        End Sub

    End Class

End Namespace
