#Region "Imports"
Imports DevExpress.Xpo
#End Region
<Persistent("Treasury.CardCollectionDetails")>
Public Class CardCollectionDetailsXpo
    Inherits XPLiteObject

    Dim fId As Integer
    <Key(True)>
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
        End Set
    End Property

    Dim fCardCollectionId As CardCollectionsXpo
    <Association("CardCollections-CardCollectionDetails")>
    Public Property CardCollectionId() As CardCollectionsXpo
        Get
            Return fCardCollectionId
        End Get
        Set(ByVal value As CardCollectionsXpo)
            SetPropertyValue("CardCollectionId", fCardCollectionId, value)
        End Set
    End Property

    Dim fCashReceiptId As CashReceiptsXpo
    <Association("CashReceipts-CardCollectionDetails")>
    Public Property CashReceiptId() As CashReceiptsXpo
        Get
            Return fCashReceiptId
        End Get
        Set(ByVal value As CashReceiptsXpo)
            SetPropertyValue("CashReceiptId", fCashReceiptId, value)
        End Set
    End Property

End Class
