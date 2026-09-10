Imports Domain.Entities
Public Class AddBankReconciliationDetailEventArgs
    Inherits EventArgs


    Property _listConciliationAutomaticDetailFilter As List(Of BankReconciliationAutomaticDetail)

    Property _listConciliationAutomaticExtractDetailFilter As List(Of BankReconciliationAutomaticExtractDetail)

    Property FlagCriterias As Integer

End Class
