Imports System
Imports Domain.Base.Entities
Imports Domain.Entities
Imports TechTalk.SpecFlow

Namespace Application.AdministracionEfectivo

    <Binding()>
    Public Class SaveAndConfirmCrossingAccountSteps
#Region "Fields"
        Private ReadOnly _treasuryContext As TreasuryContext
        Private _crossingAccount As New Domain.Entities.CrossingAccount
        'Private _listPortfolioNoteDetail As New Domain.Entities.TrackableCollection(Of PortfolioNoteDetail)
        'Private _ListPortfolioNoteAccountReceivableAdvance As New Domain.Entities.TrackableCollection(Of PortfolioNoteAccountReceivableAdvance)
        'Private _EntranceVoucherDetail As New Domain.Entities.EntranceVoucherDetail
        Private _actionResult As New ActionResult(Of Domain.Entities.CrossingAccount)

#End Region


#Region "Builder"
        Public Sub New(TreasuryContext As TreasuryContext)
            _treasuryContext = TreasuryContext
        End Sub
#End Region
        <TechTalk.SpecFlow.Given("Genero los Maestros y Detalles_")>
        Public Sub DadoGeneroLosMaestrosYDetalles_()
            Dim idCxC = New Integer() {307, 308, 309, 310, 415, 453, 580, 598, 54396, 54397, 54398, 54399, 54404, 54410}
            Dim idCxP = New Integer() {2209, 2217, 2222, 2224, 2226, 2227, 2228, 2229, 2230, 2231, 2232, 2236, 2237, 2257}

            With _crossingAccount
                .Code = ""
                .Description = "Prueba UnitTest"
                .ThirdPartyId = 59943
                .DocumentDate = DateTime.Now
                .OperatingUnitId = 14
                .CrossingType = 2
                .Status = 1
            End With

            For i As Double = 1 To 14
                Dim _accountPayable As New Domain.Entities.AccountPayable
                Dim _mainAccount As New Domain.Entities.MainAccounts
                _accountPayable = _treasuryContext._accountPayableAdminService.GetAccountPayableById(idCxP(i - 1), _treasuryContext._audit, False)
                _mainAccount = _treasuryContext._PUCAdminService.GetAccountById(_accountPayable.IdAccount, False)
                Dim _crossingAccountPayableDetailCxP As New CrossingAccountDetailCxP

                With _crossingAccountPayableDetailCxP
                    .AccountPayableId = _accountPayable.Id
                    .MainAccountId = _mainAccount.Id
                    .CrossingValue = 1
                    .MainAccountDescription = _mainAccount.NumberName
                    .BillNumber = _accountPayable.BillNumber
                    .Value = _accountPayable.Value
                    .Balance = _accountPayable.Balance
                    .ThirdPartyDescription = ""
                    .Detail = "Nota de Tesoreria : {0}, Factura CxP : " & _accountPayable.BillNumber
                End With



                Dim _accountReceivable As New Domain.Entities.AccountReceivable
                Dim _mainAccountCxC As New Domain.Entities.MainAccounts
                _accountReceivable = _treasuryContext._accountReceivableAdminService.GetAccountReceivableById(idCxC(i - 1))
                _mainAccountCxC = _treasuryContext._PUCAdminService.GetAccountById(_accountReceivable.AccountReceivableAccounting.FirstOrDefault.MainAccountId, False)
                Dim id = _accountReceivable.AccountReceivableAccounting.Where(Function(x) x.Balance > 0).FirstOrDefault.Id

                Dim _crossingAccountDetailCxC As New CrossingAccountDetailCxC()
                With _crossingAccountDetailCxC
                    .AccountReceivableId = _accountReceivable.Id
                    .AccountReceivableAccountingId = id
                    .MainAccountId = _accountReceivable.AccountReceivableAccounting.Where(Function(x) x.Id = id).FirstOrDefault.MainAccountId
                    .CrossingValue = 1
                    .MainAccountDescription = _mainAccountCxC.NumberName
                    .BillNumber = _accountReceivable.InvoiceNumber
                    .Value = _accountReceivable.Value
                    .Balance = _accountReceivable.AccountReceivableAccounting.Where(Function(x) x.Id = id).FirstOrDefault.Balance
                    .ThirdPartyDescription = ""
                    .Detail = "Nota de Tesoreria : {0}, Factura CxC : " & _accountReceivable.InvoiceNumber
                End With
                _crossingAccount.CrossingAccountDetailCxC.Add(_crossingAccountDetailCxC)
                _crossingAccount.CrossingAccountDetailCxP.Add(_crossingAccountPayableDetailCxP)
            Next
            Dim issd As Integer
            issd = 2




        End Sub

        <TechTalk.SpecFlow.Given("Guardo y confirmo los Cruces de Cuentas_ (.*)")>
        Public Sub DadoGuardoYConfirmoLosCrucesDeCuentas_(ByVal p0 As Int32)
            Dim _currentSequence As Long
            Dim sequence = _treasuryContext._treasurySequenseAdminService.GetSequenseByIdForm("640")
            If sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                _currentSequence = sequence.TreasurySequenceDetail(0).Id
            ElseIf sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If sequence.TreasurySequenceDetail.Any(Function(S) S.IdOperatingUnit = _crossingAccount.OperatingUnitId) Then
                    _currentSequence = sequence.TreasurySequenceDetail.Where(Function(s) s.IdOperatingUnit = _crossingAccount.OperatingUnitId).SingleOrDefault().Id
                End If
            End If
            _actionResult = _treasuryContext._CrossingAccountAdminService.SaveCrossingAccount(_crossingAccount, _treasuryContext._audit, True, _currentSequence)

            If Not _actionResult.StateResult Then
                Assert.Fail("Hubo un Error al guardar: " + _actionResult.Message)
            End If
        End Sub

        <TechTalk.SpecFlow.Then("Estos son almacenados y Confirmados_")>
        Public Sub EntoncesEstosSonAlmacenadosYConfirmados_()
            Assert.IsTrue(_actionResult.StateResult, _actionResult.Message)
        End Sub

    End Class

End Namespace
