Imports DevExpress.Xpo

<Persistent("Treasury.BankReconciliationAutomatic")>
Public Class BankReconciliationAutomaticReportXpo
    Inherits XPLiteObject

#Region "Members"

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
    <Size(20)>
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property

    Dim fEntityBankAccountId As TreasuryEntityBankAccountsXpo
    <Association("TreasuryBankReconciliationAutomatic_References_TreasuryEntityBankAccount")>
    Public Property EntityBankAccountId() As TreasuryEntityBankAccountsXpo
        Get
            Return fEntityBankAccountId
        End Get
        Set(ByVal value As TreasuryEntityBankAccountsXpo)
            SetPropertyValue(Of TreasuryEntityBankAccountsXpo)("EntityBankAccountId", fEntityBankAccountId, value)
        End Set
    End Property

    Dim fDocumentDate As DateTime
    Public Property DocumentDate() As DateTime
        Get
            Return fDocumentDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("DocumentDate", fDocumentDate, value)
        End Set
    End Property

    Dim fEntityBankAccountValue As Decimal
    Public Property EntityBankAccountValue() As Decimal
        Get
            Return fEntityBankAccountValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of String)("EntityBankAccountValue", fEntityBankAccountValue, value)
        End Set
    End Property

    Dim fExtractValue As Decimal
    Public Property ExtractValue() As Decimal
        Get
            Return fExtractValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of String)("ExtractValue", fExtractValue, value)
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

    Dim fCreationUser As String
    <Size(20)>
    Public Property CreationUser() As String
        Get
            Return fCreationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CreationUser", fCreationUser, value)
        End Set
    End Property

    Dim fCreationDate As DateTime
    Public Property CreationDate() As DateTime
        Get
            Return fCreationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("CreationDate", fCreationDate, value)
        End Set
    End Property

    Dim fModificationUser As String
    <Size(20)>
    Public Property ModificationUser() As String
        Get
            Return fModificationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ModificationUser", fModificationUser, value)
        End Set
    End Property

    Dim fModificationDate As DateTime
    Public Property ModificationDate() As DateTime
        Get
            Return fModificationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ModificationDate", fModificationDate, value)
        End Set
    End Property

#End Region

#Region "Builder"

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

#End Region

End Class
