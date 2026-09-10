Imports Domain.Base.Entities

Public Interface IGlosasServices
    Inherits IDisposable

    Function TransferJuridicalDebtImportFile(data As List(Of ImportFileRow), customerId As Integer, TransferJuridicalDebtCollectionCId As Integer, ByVal _IdUnitoperating As Integer) As ActionResult(Of List(Of TransferJuridicalDebtCollectionD))

    Function TransferJuridicalDebtCopyPaste(data As List(Of List(Of String)), customerId As Integer, TransferJuridicalDebtCollectionCId As Integer, ByVal _IdUnitoperating As Integer) As ActionResult(Of List(Of TransferJuridicalDebtCollectionD))

End Interface
