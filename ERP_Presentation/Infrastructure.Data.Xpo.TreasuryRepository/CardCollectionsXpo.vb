#Region "imports"
Imports DevExpress.Xpo
#End Region
<Persistent("Treasury.CardCollections")>
Public Class CardCollectionsXpo
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

    Dim fCode As String
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property

    Dim fEntityBankAccountId As EntityBankAccountXpo
    <Association("TreasuryBankReconciliationAutomatic_Reference_TreasuryEntityBankAccounts")>
    Public Property EntityBankAccountId() As EntityBankAccountXpo
        Get
            Return fEntityBankAccountId
        End Get
        Set(ByVal value As EntityBankAccountXpo)
            SetPropertyValue("EntityBankAccountId", fEntityBankAccountId, value)
        End Set
    End Property

    Dim fTransactionCode As String
    <Size(60)>
    Public Property TransactionCode() As String
        Get
            Return fTransactionCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("TransactionCode", fTransactionCode, value)
        End Set
    End Property

    Dim fInitialDate As Date
    Public Property InitialDate() As Date
        Get
            Return fInitialDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue(Of Date)("InitialDate", fInitialDate, value)
        End Set
    End Property

    Dim fEndDate As Date
    Public Property EndDate() As Date
        Get
            Return fEndDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue(Of Date)("EndDate", fEndDate, value)
        End Set
    End Property

    Dim fDetail As String
    <Size(200)>
    Public Property Detail() As String
        Get
            Return fDetail
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Detail", fDetail, value)
        End Set
    End Property

    Dim fStatus As Byte
    Public Property Status() As Byte
        Get
            Return fStatus
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Status", fStatus, value)
        End Set
    End Property

    Dim fCardCollectionDetails As XPCollection(Of CardCollectionDetailsXpo)
    <Association("CardCollections-CardCollectionDetails")>
    Public ReadOnly Property CardCollectionDetails() As XPCollection(Of CardCollectionDetailsXpo)
        Get
            Return GetCollection(Of CardCollectionDetailsXpo)("CardCollectionDetails")
        End Get
    End Property


End Class
