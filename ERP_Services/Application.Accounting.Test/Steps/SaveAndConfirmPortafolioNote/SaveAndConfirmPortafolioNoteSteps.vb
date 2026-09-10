Imports Domain.Base.Entities
Imports Domain.Entities
Imports TechTalk.SpecFlow

Namespace Application.Accounting.Test

    <Binding()>
    Public Class SaveAndConfirmPortafolioNoteSteps
#Region "Fields"
        Private ReadOnly _AccountingContext As AccountingContext
        Private _PortfolioNote As New Domain.Entities.PortfolioNote
        Private _listPortfolioNoteDetail As New Domain.Entities.TrackableCollection(Of PortfolioNoteDetail)
        Private _ListPortfolioNoteAccountReceivableAdvance As New Domain.Entities.TrackableCollection(Of PortfolioNoteAccountReceivableAdvance)
        'Private _EntranceVoucherDetail As New Domain.Entities.EntranceVoucherDetail
        Private _actionResult As New ActionResult(Of PortfolioNote)

        Private Structure PortfolioNoteAccountReceivableAdvance_struct
            Public AccountReceivableId(), AccountReceivableShareId(), MainAccountId(), AccountReceivableAccountingId(), PortfolioAdvanceId As Int32
            Public AdjusmentValue, PercentageValue, PreviousBalance, Balance() As Double
        End Structure
        Private Structure PortfolioNoteDetail_struct
            Public PortfolioNoteConceptId(), MainAccountId(), ThirdPartyId, Nature As Int32
            Public Value As Double
            Public Observations As String
        End Structure
#End Region


#Region "Builder"
        Public Sub New(AccountingContext As AccountingContext)
            _AccountingContext = AccountingContext
        End Sub
#End Region
        <TechTalk.SpecFlow.Given("Genero los maestros y detalles")>
        Public Sub DadoGeneroLosMaestrosYDetalles()

            Dim _portfolioNoteAccountReceivableAdvance_struct As PortfolioNoteAccountReceivableAdvance_struct
            _portfolioNoteAccountReceivableAdvance_struct.AccountReceivableId = {598, 668, 80023, 80026, 80012, 79982, 79991, 33528, 79965, 79966}
            _portfolioNoteAccountReceivableAdvance_struct.AccountReceivableShareId = {1666, 1736, 81100, 81103, 81080, 81050, 81059, 34597, 81033, 81034}
            _portfolioNoteAccountReceivableAdvance_struct.MainAccountId = {3937, 3937, 3937, 3921, 3937, 3937, 3937, 3937, 3937, 3937}
            _portfolioNoteAccountReceivableAdvance_struct.AccountReceivableAccountingId = {1676, 1802, 81126, 81130, 81131, 81132, 81134, 81135, 81136, 81137}
            ' _portfolioNoteAccountReceivableAdvance_struct.PortfolioAdvanceId = 0
            _portfolioNoteAccountReceivableAdvance_struct.AdjusmentValue = 1
            _portfolioNoteAccountReceivableAdvance_struct.PercentageValue = 0
            _portfolioNoteAccountReceivableAdvance_struct.PreviousBalance = 0
            _portfolioNoteAccountReceivableAdvance_struct.Balance = {735, 80735, 46535, 1319210, 1883921, 1062331, 20000, 42992438, 13761, 13761}

            Dim _portfolioNoteDetail_struct As PortfolioNoteDetail_struct
            _portfolioNoteDetail_struct.PortfolioNoteConceptId = {364, 365, 366, 367, 368, 369, 370, 371, 372, 373}
            _portfolioNoteDetail_struct.MainAccountId = {3886, 3887, 3889, 3890, 3891, 3892, 3893, 3894, 3895, 3896}
            _portfolioNoteDetail_struct.ThirdPartyId = 239
            _portfolioNoteDetail_struct.Nature = 1
            _portfolioNoteDetail_struct.Value = 1
            _portfolioNoteDetail_struct.Observations = "Prueba unitTest"

            For i As Double = 1 To 10
                Dim _PortfolioNoteDetail As New PortfolioNoteDetail With
                    {.PortfolioNoteConceptId = _portfolioNoteDetail_struct.PortfolioNoteConceptId(i - 1),
                     .MainAccountId = _portfolioNoteDetail_struct.MainAccountId(i - 1),
                     .ThirdPartyId = _portfolioNoteDetail_struct.ThirdPartyId,
                     .Nature = _portfolioNoteDetail_struct.Nature,
                     .Value = _portfolioNoteDetail_struct.Value,
                     .Observations = _portfolioNoteDetail_struct.Observations,
                     .PortfolioNoteId = 0}


                Dim _PortfolioNoteAccountReceivableAdvance As New PortfolioNoteAccountReceivableAdvance With
                    {.AccountReceivableId = _portfolioNoteAccountReceivableAdvance_struct.AccountReceivableId(i - 1),
                .AccountReceivableShareId = _portfolioNoteAccountReceivableAdvance_struct.AccountReceivableShareId(i - 1),
                .MainAccountId = _portfolioNoteAccountReceivableAdvance_struct.MainAccountId(i - 1),
                .AccountReceivableAccountingId = _portfolioNoteAccountReceivableAdvance_struct.AccountReceivableAccountingId(i - 1),
                .AdjusmentValue = _portfolioNoteAccountReceivableAdvance_struct.AdjusmentValue,
                .PercentageValue = _portfolioNoteAccountReceivableAdvance_struct.PercentageValue,
                .PreviousBalance = _portfolioNoteAccountReceivableAdvance_struct.PreviousBalance,
                .Balance = _portfolioNoteAccountReceivableAdvance_struct.Balance(i - 1),
                .Nature = 1}
                _listPortfolioNoteDetail.Add(_PortfolioNoteDetail)
                _ListPortfolioNoteAccountReceivableAdvance.Add(_PortfolioNoteAccountReceivableAdvance)
            Next
            With _PortfolioNote
                .NoteDate = DateTime.Now
                .Code = ""
                .CustomerId = 333
                .Observations = " PRUEBA LAST"
                .Nature = 2
                .NoteType = 1
                .PortfolioAdvanceId = Nothing
                .OperatingUnitId = 14
                .Status = 1
                .PortfolioNoteDetail = _listPortfolioNoteDetail
                .PortfolioNoteAccountReceivableAdvance = _ListPortfolioNoteAccountReceivableAdvance

            End With



        End Sub

        <TechTalk.SpecFlow.Given("Guardo y confirmo las Notas debito y credito (.*)")>
        Public Sub DadoGuardoYConfirmoLasNotasDebitoYCredito(ByVal p0 As Int32)
            Dim _currentSequence As Long
            Dim sequence = _AccountingContext._portfolioSequenseAdminService.GetSequenseByIdForm("686")
            If sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                _currentSequence = sequence.PortfolioSequenceDetail(0).Id
            ElseIf sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If sequence.PortfolioSequenceDetail.Any(Function(S) S.IdOperatingUnit = _PortfolioNote.OperatingUnitId) Then
                    _currentSequence = sequence.PortfolioSequenceDetail.Where(Function(s) s.IdOperatingUnit = _PortfolioNote.OperatingUnitId).SingleOrDefault().Id
                End If
            End If

            _PortfolioNote.Status = 2
            _actionResult = _AccountingContext._portfolioNoteAdminService.SavePortfolioNote(_PortfolioNote, _AccountingContext._audit, _AccountingContext._session, _currentSequence)

            If Not _actionResult.StateResult Then
                Assert.Fail("Hubo un Error: " + _actionResult.MessageResult(0))
            End If
        End Sub

        <TechTalk.SpecFlow.Then("Estas son almacenados y Confirmados")> _
        Public Sub EntoncesEstasSonAlmacenadosYConfirmados()
            Assert.IsTrue(_actionResult.StateResult, _actionResult.MessageResult(0))
        End Sub

    End Class

End Namespace
