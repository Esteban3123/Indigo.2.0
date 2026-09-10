'************************************************************
' Assembly         : Infrastructure.Data.FixedAsset
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 27-01-2016
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Importar"
Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Domain.Base

#End Region

Public Class SettingFixedAssetRepository

    Inherits GenericRepository(Of SettingFixedAsset)
    Implements ISettingFixedAssetRepository

    'Devuelve el contexto en este repositorio 
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    '''inicializa la neva instancia d clase.
    ''' </summary>
    ''' <param name="contex">el contexto.</param>
    Public Sub New(ByVal contex As IGlobalModelUnitOfWork)
        MyBase.New(contex)
        _context = contex
    End Sub

    Public Function GetSettingFixedAssetByOperatingUnitId(OperatingUnitId As Integer, Optional tracking As Boolean = True) As SettingFixedAsset Implements ISettingFixedAssetRepository.GetSettingFixedAssetByOperatingUnitId
        If tracking = True Then
            Dim Busqueda = (From e In _context.SettingFixedAsset.Include("SettingFixedAssetByLegalBook").Include("Currency") Where e.OperatingUnitId = OperatingUnitId
                            Select e).FirstOrDefault()

            If Busqueda IsNot Nothing Then
                Dim DepreciationAccountingVoucher = (From jv In _context.JournalVoucherTypes Where jv.Id = Busqueda.IdDepreciationAccountingVoucher Select jv).FirstOrDefault()
                Dim IngressAccountingVoucher = (From jv In _context.JournalVoucherTypes Where jv.Id = Busqueda.IdIngressAccountingVoucher Select jv).FirstOrDefault()
                Dim OutputAccountingVoucher = (From jv In _context.JournalVoucherTypes Where jv.Id = Busqueda.IdOutputAccountingVoucher Select jv).FirstOrDefault()
                Dim IntangibleAssetAmortizationVoucher = (From jv In _context.JournalVoucherTypes Where jv.Id = Busqueda.IdIntangibleAssetAmortizationVoucher Select jv).FirstOrDefault()
                Dim ValorizationDevaluationAccountingVoucher = (From jv In _context.JournalVoucherTypes Where jv.Id = Busqueda.IdValorizationDevaluationAccountingVoucher Select jv).FirstOrDefault()
                Dim TransferJournalVoucher = (From jv In _context.JournalVoucherTypes Where jv.Id = Busqueda.TransferJournalVoucherId Select jv).FirstOrDefault()
                Dim ReclassificationJournalVoucher = (From jv In _context.JournalVoucherTypes Where jv.Id = Busqueda.ReclassificationJournalVoucherId Select jv).FirstOrDefault()
                Dim DevolutionJournalVoucher = (From jv In _context.JournalVoucherTypes Where jv.Id = Busqueda.DevolutionJournalVoucherId Select jv).FirstOrDefault()

                Dim OtherIngressAccountingAccount = (From ma In _context.MainAccounts Where ma.Id = Busqueda.OtherIngressMainAccountId Select ma).FirstOrDefault()
                Dim DonationAccountingAccount = (From ma In _context.MainAccounts Where ma.Id = Busqueda.DonationMainAccountId Select ma).FirstOrDefault()
                Dim TransferPropertyAccountingAccount = (From ma In _context.MainAccounts Where ma.Id = Busqueda.TransferPropertyMainAccountId Select ma).FirstOrDefault()
                Dim OtherConceptsAccountingAccount = (From ma In _context.MainAccounts Where ma.Id = Busqueda.OtherConceptsMainAccountId Select ma).FirstOrDefault()
                Dim RecuperationAccountingAccount = (From ma In _context.MainAccounts Where ma.Id = Busqueda.RecuperationMainAccountId Select ma).FirstOrDefault()

                If Busqueda.Currency Is Nothing Then
                    Busqueda.Currency = (From c In _context.Currency
                                         Join o In _context.CompanySettings On c.Id Equals o.OfficialCurrencyId
                                         Select c).FirstOrDefault()
                End If

                Dim DocumentsFixedAssetEntry = (From a In _context.FixedAssetEntry
                                                Where a.OperatingUnitId = OperatingUnitId
                                                Select a).Count()

                Dim DocumentsFixedAssetTransfer = (From t In _context.FixedAssetTransfer
                                                   Where t.OperatingUnitId = OperatingUnitId
                                                   Select t).Count()

                If DocumentsFixedAssetEntry > 0 OrElse DocumentsFixedAssetTransfer > 0 Then
                    Busqueda.CurrencyFieldEnabled = False
                Else
                    Busqueda.CurrencyFieldEnabled = True
                End If

                Dim Responsible = (From tp In _context.ThirdParty Where tp.Id = Busqueda.IdThirdPartyResponsible Select tp).FirstOrDefault()

                Busqueda.CodeNameDepreciationAccountingVoucher = DepreciationAccountingVoucher.Code + " - " + DepreciationAccountingVoucher.Name
                Busqueda.CodeNameIngressAccountingVoucher = IngressAccountingVoucher.Code + " - " + IngressAccountingVoucher.Name
                Busqueda.CodeNameOutputAccountingVoucher = OutputAccountingVoucher.Code + " - " + OutputAccountingVoucher.Name
                Busqueda.CodeNameIntangibleAssetAmortizationVoucher = IntangibleAssetAmortizationVoucher?.Code + " - " + IntangibleAssetAmortizationVoucher?.Name
                Busqueda.CodeNameValorizationDevaluationAccountingVoucher = ValorizationDevaluationAccountingVoucher.Code + " - " + ValorizationDevaluationAccountingVoucher.Name
                Busqueda.TransferJournalVoucherCodeName = TransferJournalVoucher.Code + " - " + TransferJournalVoucher.Name
                Busqueda.ReclassificationJournalVoucherCodeName = ReclassificationJournalVoucher.Code + " - " + ReclassificationJournalVoucher.Name
                Busqueda.DevolutionJournalVoucherCodeName = DevolutionJournalVoucher.Code + " - " + DevolutionJournalVoucher.Name

                Busqueda.NumberNameOtherIngressAccountingAccount = OtherIngressAccountingAccount.Number + " - " + OtherIngressAccountingAccount.Name
                Busqueda.NumberNameDonationAccountingAccount = DonationAccountingAccount.Number + " - " + DonationAccountingAccount.Name
                Busqueda.NumberNameTransferPropertyAccountingAccount = TransferPropertyAccountingAccount.Number + " - " + TransferPropertyAccountingAccount.Name
                Busqueda.NumberNameOtherConceptsAccountingAccount = OtherConceptsAccountingAccount.Number + " - " + OtherConceptsAccountingAccount.Name
                Busqueda.NumberNameRecuperationAccountingAccount = RecuperationAccountingAccount.Number + " - " + RecuperationAccountingAccount.Name
                Busqueda.CurrencyName = Busqueda.Currency.Abbreviation + " - " + Busqueda.Currency.Name

                Busqueda.NameResponsible = Responsible.Nit + " - " + Responsible.Name

                Dim account As MainAccounts = Nothing

                account = (From ma In _context.MainAccounts.AsNoTracking Where ma.Id = Busqueda.SalesMainAccountId Select ma).FirstOrDefault()
                Busqueda.SalesMainAccountCodeName = account.Number + " - " + account.Name

                account = (From ma In _context.MainAccounts.AsNoTracking Where ma.Id = Busqueda.ReplacementMainAccountId Select ma).FirstOrDefault()
                Busqueda.ReplacementMainAccountCodeName = account.Number + " - " + account.Name

                'Porcentaje de los fletes
                Dim concept = (From apc In _context.AccountPayableConcepts.AsNoTracking.Include("RetentionConcepts").AsNoTracking.Include("MainAccounts").AsNoTracking()
                               Select apc Where apc.Id = Busqueda.IVAFreightAccountPayableConceptId).FirstOrDefault()

                If concept.RetentionConcepts IsNot Nothing Then
                    Busqueda.FreightIVAPercentage = concept.RetentionConcepts.Rate
                End If

                Dim serviceMainAccount = (From m In _context.MainAccounts.AsNoTracking Where m.Id = Busqueda.ServiceMainAccountId Select m).FirstOrDefault
                Busqueda.ServiceMainAccountNumberName = serviceMainAccount.Number + " - " + serviceMainAccount.Name

                Dim concepts As AccountPayableConcepts = Nothing

                concept = GetConcept(Busqueda.IVAFreightAccountPayableConceptId)
                Busqueda.IVAFreightAccountPayableConceptCodeName = concept.Code + " - " + concept.Name

                concept = GetConcept(Busqueda.FreightAccountPayableConceptId)
                Busqueda.FreightAccountPayableConceptCodeName = concept.Code + " - " + concept.Name

                If Busqueda.IVARetentionAccountPayableConceptId IsNot Nothing Then
                    concept = GetConcept(Busqueda.IVARetentionAccountPayableConceptId)
                    Busqueda.IVARetentionAccountPayableConceptCodeName = concept.Code + " - " + concept.Name
                End If

                If Busqueda.IVAAccountPayableConceptId IsNot Nothing Then
                    concept = GetConcept(Busqueda.IVAAccountPayableConceptId)
                    Busqueda.IVAAccountPayableConceptCodeName = concept?.Code + " - " + concept?.Name
                End If

                If Busqueda.SettingFixedAssetByLegalBook IsNot Nothing AndAlso Busqueda.SettingFixedAssetByLegalBook.Count > 0 Then
                    For Each item In Busqueda.SettingFixedAssetByLegalBook
                        Dim LegalBook = (From l In _context.LegalBook.AsNoTracking Where l.Id = item.LegalBookId Select l).FirstOrDefault
                        item.LegalBookCodeName = LegalBook.Code + " - " + LegalBook.Name
                        item.OfficialBook = LegalBook.OfficialBook
                        item.StatusBook = LegalBook.Status
                    Next
                End If

                Busqueda.OriginalValue = (From e In _context.SettingFixedAsset.AsNoTracking Where e.OperatingUnitId = OperatingUnitId
                         Select e).FirstOrDefault()

            End If
            Return Busqueda
        Else
            Dim Busqueda = (From e In _context.SettingFixedAsset.AsNoTracking Where e.OperatingUnitId = OperatingUnitId
                         Select e).FirstOrDefault()

            If Busqueda IsNot Nothing Then
                Return Busqueda
            Else
                Return New SettingFixedAsset

            End If
        End If

    End Function

    Private Function GetConcept(Id As Integer) As AccountPayableConcepts
        Return (From c In _context.AccountPayableConcepts.AsNoTracking Where c.Id = Id Select c)?.FirstOrDefault
    End Function

End Class
