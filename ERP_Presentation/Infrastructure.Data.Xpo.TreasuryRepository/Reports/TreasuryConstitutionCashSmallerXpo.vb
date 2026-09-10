#Region "Imports"

Imports DevExpress.Xpo

#End Region

<Persistent("Treasury.ConstitutionCashSmaller")>
Public Class TreasuryConstitutionCashSmallerXpo
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
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property

    Dim fDocumentDate As Date
    Public Property DocumentDate() As Date
        Get
            Return fDocumentDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue(Of Date)("DocumentDate", fDocumentDate, value)
        End Set
    End Property

    Dim fDocumentType As Integer
    Public Property DocumentType() As Integer
        Get
            Return fDocumentType
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("DocumentType", fDocumentType, value)
        End Set
    End Property

    Dim fCashRegisterSmallerId As TreasuryCashRegistersXpo
    <Association("Treasury_ConstitutionCashSmaller_References_Treasury_CashRegisters_By_CashRegisterSmallerId")>
    Public Property CashRegisterSmallerId() As TreasuryCashRegistersXpo
        Get
            Return fCashRegisterSmallerId
        End Get
        Set(ByVal value As TreasuryCashRegistersXpo)
            SetPropertyValue(Of TreasuryCashRegistersXpo)("CashRegisterSmallerId", fCashRegisterSmallerId, value)
        End Set
    End Property

    Dim fSourceType As Integer
    <Persistent("SourceType")>
    Public Property SourceType() As Integer
        Get
            Return fSourceType
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("SourceType", fSourceType, value)
        End Set
    End Property

    Dim fCashRegisterId As TreasuryCashRegistersXpo
    <Association("Treasury_ConstitutionCashSmaller_References_Treasury_CashRegisters_By_CashRegisterId")>
    Public Property CashRegisterId() As TreasuryCashRegistersXpo
        Get
            Return fCashRegisterId
        End Get
        Set(ByVal value As TreasuryCashRegistersXpo)
            SetPropertyValue(Of TreasuryCashRegistersXpo)("CashRegisterId", fCashRegisterId, value)
        End Set
    End Property

    Dim fEntityBankAccountId As TreasuryEntityBankAccountsXpo
    <Association("Treasury_ConstitutionCashSmaller_References_Treasury_EntityBankAccounts")>
    Public Property EntityBankAccountId() As TreasuryEntityBankAccountsXpo
        Get
            Return fEntityBankAccountId
        End Get
        Set(ByVal value As TreasuryEntityBankAccountsXpo)
            SetPropertyValue(Of TreasuryEntityBankAccountsXpo)("EntityBankAccountId", fEntityBankAccountId, value)
        End Set
    End Property

    Dim fValue As Decimal
    Public Property Value() As Decimal
        Get
            Return fValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Value", fValue, value)
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
    Public Property CreationUser() As String
        Get
            Return fCreationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CreationUser", fCreationUser, value)
        End Set
    End Property

#End Region

#Region "Builders"

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Sub New()
        MyBase.New(Session.DefaultSession)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

#End Region

End Class
