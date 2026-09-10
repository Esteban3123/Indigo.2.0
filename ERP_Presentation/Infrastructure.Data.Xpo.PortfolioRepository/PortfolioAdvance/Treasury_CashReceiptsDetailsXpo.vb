Imports DevExpress.Xpo

<Persistent("Treasury.CashReceiptDetails")>
Public Class Treasury_CashReceiptsDetailsXpo
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

    Dim fIdCashReceipt As Integer
    Public Property IdCashReceipt() As Integer
        Get
            Return fIdCashReceipt
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdCashReceipt", fIdCashReceipt, value)
        End Set
    End Property

    Dim fIdThirdParty As Integer
    Public Property IdThirdParty() As Integer
        Get
            Return fIdThirdParty
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdThirdParty", fIdThirdParty, value)
        End Set
    End Property

    Dim fIdMainAccount As Integer
    Public Property IdMainAccount() As Integer
        Get
            Return fIdMainAccount
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdMainAccount", fIdMainAccount, value)
        End Set
    End Property

    Dim fIdCostCenter As Integer
    Public Property IdCostCenter() As Integer
        Get
            Return fIdCostCenter
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdCostCenter", fIdCostCenter, value)
        End Set
    End Property

    Dim fNature As Byte
    Public Property Nature() As Byte
        Get
            Return fNature
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Nature", fNature, value)
        End Set
    End Property

    Dim fIdCashReceiptConcept As Treasury_CashReceiptConceptsXpo
    <Association("Treasury_CashReceiptsDetailsReferencesTreasury_CashReceiptConceptsXpo")>
    Public Property IdCashReceiptConcept() As Treasury_CashReceiptConceptsXpo
        Get
            Return fIdCashReceiptConcept
        End Get
        Set(ByVal value As Treasury_CashReceiptConceptsXpo)
            SetPropertyValue(Of Treasury_CashReceiptConceptsXpo)("IdCashReceiptConcept", fIdCashReceiptConcept, value)
        End Set
    End Property
    Dim fCashReceiptConceptAffectation As Byte
    Public Property CashReceiptConceptAffectation() As Byte
        Get
            Return fCashReceiptConceptAffectation
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("CashReceiptConceptAffectation", fCashReceiptConceptAffectation, value)
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
    Dim fIdRetentionConcept As Integer
    Public Property IdRetentionConcept() As Integer
        Get
            Return fIdRetentionConcept
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdRetentionConcept", fIdRetentionConcept, value)
        End Set
    End Property
    Dim fPercentageRetention As Decimal
    Public Property PercentageRetention() As Decimal
        Get
            Return fPercentageRetention
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("PercentageRetention", fPercentageRetention, value)
        End Set
    End Property
    Dim fBaseValue As Decimal
    Public Property BaseValue() As Decimal
        Get
            Return fBaseValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("BaseValue", fBaseValue, value)
        End Set
    End Property
    Dim fCardNumber As String
    <Size(30)>
    Public Property CardNumber() As String
        Get
            Return fCardNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CardNumber", fCardNumber, value)
        End Set
    End Property

    <Association("Portfolio_PortfolioAdvanceReferencesTreasury_CashReceiptsDetails", GetType(Portfolio_PortfolioAdvance))>
    Public ReadOnly Property Portfolio_PortfolioAdvance() As XPCollection(Of Portfolio_PortfolioAdvance)
        Get
            Return GetCollection(Of Portfolio_PortfolioAdvance)("Portfolio_PortfolioAdvance")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Sub New()
        MyBase.New(Session.DefaultSession)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class
