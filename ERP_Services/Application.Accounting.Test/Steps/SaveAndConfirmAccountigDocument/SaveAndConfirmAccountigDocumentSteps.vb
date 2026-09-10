Imports System
Imports TechTalk.SpecFlow
Imports Domain.Entities
Imports Domain.Base.Entities

Namespace Application.Accounting.Test

    <Binding()>
    Public Class SaveAndConfirmAccountigDocumentSteps
#Region "Fields"
        Private ReadOnly _AccountingContext As AccountingContext
        Private _journalVouchers As New Domain.Entities.JournalVouchers
        Private _journalVoucherDetails As New Domain.Entities.TrackableCollection(Of JournalVoucherDetails)
        'Private _EntranceVoucherDetail As New Domain.Entities.EntranceVoucherDetail
        Private _actionResult As New ActionMessageResult(Of Domain.Entities.JournalVouchers)
        Private dato As Int16
#End Region


#Region "Builder"
        Public Sub New(AccountingContext As AccountingContext)
            _AccountingContext = AccountingContext
        End Sub
#End Region
        <TechTalk.SpecFlow.Given("Genero el comprobante Contable")>
        Public Sub DadoGeneroElComprobanteContable()
            Dim IdMainAccountDebit = New Integer() {3874, 3875, 3876, 3877, 3890, 3891, 3892, 3894, 3908, 3909, 3910, 3924, 3925, 3926, 3927, 3940, 5361, 5429, 5431, 5448, 6350, 6365, 6434, 5282, 5284, 5291}

            Dim IdMainAccountCredit = New Integer() {4049, 4052, 4069, 4100, 4133, 4134, 4135, 4136, 4153, 4167, 4168, 4169, 4183, 4185, 4233, 4234, 5293, 5300, 5316, 5318, 5332, 5334, 5341, 5348, 5350, 5441}

            For i As Double = 0 To 15
                _journalVoucherDetails.Add(New JournalVoucherDetails With
                    {.Id = 0,
                    .IdMainAccount = IdMainAccountCredit(i),
                    .IdThirdParty = 18,
                    .DebitValue = 0,
                    .CreditValue = 1,
                    .Detail = "Prueba Testing",
                    .BaseValue = 0,
                    .BillingValue = 0})

                _journalVoucherDetails.Add(New JournalVoucherDetails With
                    {.Id = 0,
                    .IdMainAccount = IdMainAccountDebit(i),
                    .IdThirdParty = 19,
                    .DebitValue = 1,
                    .CreditValue = 0,
                    .Detail = "Prueba Testing 2",
                    .BaseValue = 0,
                    .BillingValue = 0}
                )
            Next




            With _journalVouchers
                .Id = 0
                .Consecutive = 0
                .LegalBookId = 9
                .IdJournalVoucher = 43
                .VoucherDate = DateTime.Now
                .Imported = False
                .Status = 2
                .Detail = "Prueba Testing"
                .EntityName = "JournalVouchers"
                .IsClosedYear = 0
                .JournalVoucherDetails = _journalVoucherDetails
            End With
        End Sub

        <TechTalk.SpecFlow.Given("Yo guardo y confirmo el comprobante Contable (.*)")>
        Public Sub DadoYoGuardoYConfirmoElComprobanteContable(ByVal Int As Int32)
            _actionResult = _AccountingContext._accountingDocumentAdminService.SaveAccountingDocument(_journalVouchers, _AccountingContext._audit)
            If Not _actionResult.StateResult Then
                Assert.Fail("Hubo un Error: " + _actionResult.Message)
            End If
        End Sub

        <TechTalk.SpecFlow.Then("Este es almacenado y Confirmado")> _
        Public Sub EntoncesEsteEsAlmacenadoYConfirmado()
            Assert.IsTrue(_actionResult.StateResult, _actionResult.Message)
        End Sub

    End Class

End Namespace
