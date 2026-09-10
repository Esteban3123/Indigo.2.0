Imports DevExpress.Xpo

<Persistent("Payments.ViewThirdPartyRetention")>
Public Class ViewThirdPartyRetentionXpo
    Inherits XPLiteObject

#Region "Members"

    Dim fId As Guid
    <Key(True)>
    Public Property Id() As Guid
        Get
            Return fId
        End Get
        Set(ByVal value As Guid)
            SetPropertyValue(Of Guid)("Id", fId, value)
        End Set
    End Property

    Dim fThirdPartyId As Integer?
    Public Property ThirdPartyId() As Integer?
        Get
            Return fThirdPartyId
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("ThirdPartyId", fThirdPartyId, value)
        End Set
    End Property

    Dim fYear As Integer?
    Public Property Year() As Integer?
        Get
            Return fYear
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("Year", fYear, value)
        End Set
    End Property

    Dim fType As Integer
    Public Property Type() As Integer
        Get
            Return fType
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Type", fType, value)
        End Set
    End Property

    Dim fTypeName As String
    Public Property TypeName() As String
        Get
            Return fTypeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("TypeName", fTypeName, value)
        End Set
    End Property

    Dim fConceptType As Byte
    Public Property ConceptType() As Byte
        Get
            Return fConceptType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("ConceptType", fConceptType, value)
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

    Dim fBillNumber As String
    Public Property BillNumber() As String
        Get
            Return fBillNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("BillNumber", fBillNumber, value)
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

    Dim fDebitValue As Decimal
    Public Property DebitValue() As Decimal
        Get
            Return fDebitValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("DebitValue", fDebitValue, value)
        End Set
    End Property

    Dim fCreditValue As Decimal
    Public Property CreditValue() As Decimal
        Get
            Return fCreditValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("CreditValue", fCreditValue, value)
        End Set
    End Property

    Dim fRetentionValue383 As Decimal
    Public Property RetentionValue383() As Decimal
        Get
            Return fRetentionValue383
        End Get
        Set(value As Decimal)
            SetPropertyValue(Of Decimal)("RetentionValue383", fRetentionValue383, value)
        End Set
    End Property

    Dim fExemptIncome As Decimal?
    Public Property ExemptIncome() As Decimal?
        Get
            Return fExemptIncome
        End Get
        Set(ByVal value As Decimal?)
            SetPropertyValue(Of Decimal?)("ExemptIncome", fExemptIncome, value)
        End Set
    End Property

    Dim fMaxDeductionsAndRentExents As Decimal?
    Public Property MaxDeductionsAndRentExents() As Decimal?
        Get
            Return fMaxDeductionsAndRentExents
        End Get
        Set(ByVal value As Decimal?)
            SetPropertyValue(Of Decimal?)("MaxDeductionsAndRentExents", fMaxDeductionsAndRentExents, value)
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
#End Region

#Region "NonPersistent Members"

    <NonPersistent()>
    Public Property AdjustmentUUID() As String

#End Region

#Region "Builder"

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
