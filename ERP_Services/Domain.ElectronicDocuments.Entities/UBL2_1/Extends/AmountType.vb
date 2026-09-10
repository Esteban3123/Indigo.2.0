Imports Domain.Base.Entities.Enums.ElectronicDocuments.v1_6
Imports Infrastructure.CrossCutting.Root

Namespace UBL2_1.common

    Partial Public Class AmountType

        Public Shared TlsDefaultCurrencyID As CurrencyCode

        Public Sub New()
            Me.currencyID = Utils.GetXmlEnumToString(Of CurrencyCode)(TlsDefaultCurrencyID)
        End Sub

    End Class

End Namespace