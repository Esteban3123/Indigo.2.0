Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base

Public Interface IPortfolioService
    Inherits IDisposable

    Function SetCopyPasteOrImportFilePortfolioNote(dataImportFile As List(Of ImportFileRow), dataCopyPaste As List(Of List(Of String)), CompanyType As Integer, OperatingUnit As Integer, ParamArray parameters As Object()) As ActionResult(Of List(Of PortfolioNoteAccountReceivableAdvance))

    Function CreateAccountingAccount(_IdJournalVoucher As Integer, _Coments As String, _EntityCode As String, _EntityId As Integer, _EntityName As String, _IdAccountCredit As Integer, _IdAccountDebit As Integer, _IdThirdParty As Integer, _ComentsDetails As String, _Value As Decimal, _CostCenterId As Integer?) As JournalVouchers

End Interface
