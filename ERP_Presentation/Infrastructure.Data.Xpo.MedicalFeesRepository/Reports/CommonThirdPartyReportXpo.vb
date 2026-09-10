Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Common.ThirdParty")> _
Public Class CommonThirdPartyReportXpo
    Inherits XPLiteObject
    Dim fId As Integer
    <Key(True)> _
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
        End Set
    End Property
    Dim fPersonId As Integer
    Public Property PersonId() As Integer
        Get
            Return fPersonId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("PersonId", fPersonId, value)
        End Set
    End Property
    Dim fNit As String
    <Indexed(Name:="IX_ThirdParty_Nit_UNI", Unique:=True)> _
    <Size(15)> _
    Public Property Nit() As String
        Get
            Return fNit
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Nit", fNit, value)
        End Set
    End Property
    Dim fDigitVerification As String
    <Size(1)> _
    Public Property DigitVerification() As String
        Get
            Return fDigitVerification
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("DigitVerification", fDigitVerification, value)
        End Set
    End Property
    Dim fName As String
    <Size(300)> _
    Public Property Name() As String
        Get
            Return fName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Name", fName, value)
        End Set
    End Property
    Dim fNitName As String
    'columna que devuelve el Número y el nombre concatenado
    <Size(50)> _
    <PersistentAlias("concat(concat(Nit,' - '),Name)")>
    Public ReadOnly Property NitName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("NitName"))
        End Get
    End Property
    Dim fPersonType As Byte
    Public Property PersonType() As Byte
        Get
            Return fPersonType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("PersonType", fPersonType, value)
        End Set
    End Property
    Dim fRetentionType As Byte
    Public Property RetentionType() As Byte
        Get
            Return fRetentionType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("RetentionType", fRetentionType, value)
        End Set
    End Property
    Dim fContributionType As Byte
    Public Property ContributionType() As Byte
        Get
            Return fContributionType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("ContributionType", fContributionType, value)
        End Set
    End Property
    Dim fIVARetentionAccountPayableConceptId As Integer
    Public Property IVARetentionAccountPayableConceptId() As Integer
        Get
            Return fIVARetentionAccountPayableConceptId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IVARetentionAccountPayableConceptId", fIVARetentionAccountPayableConceptId, value)
        End Set
    End Property
    Dim fIca As Boolean
    Public Property Ica() As Boolean
        Get
            Return fIca
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Ica", fIca, value)
        End Set
    End Property
    Dim fIcaPercentage As Decimal
    Public Property IcaPercentage() As Decimal
        Get
            Return fIcaPercentage
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("IcaPercentage", fIcaPercentage, value)
        End Set
    End Property
    Dim fIcaTop As Boolean
    Public Property IcaTop() As Boolean
        Get
            Return fIcaTop
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("IcaTop", fIcaTop, value)
        End Set
    End Property
    Dim fIcaTopValue As Decimal
    Public Property IcaTopValue() As Decimal
        Get
            Return fIcaTopValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("IcaTopValue", fIcaTopValue, value)
        End Set
    End Property
    Dim fEntityCode As String
    <Size(15)> _
    Public Property EntityCode() As String
        Get
            Return fEntityCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("EntityCode", fEntityCode, value)
        End Set
    End Property
    Dim fEconomicActivityId As Integer
    Public Property EconomicActivityId() As Integer
        Get
            Return fEconomicActivityId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("EconomicActivityId", fEconomicActivityId, value)
        End Set
    End Property
    Dim fClass1 As Byte
    <Persistent("Class")> _
    Public Property Class1() As Byte
        Get
            Return fClass1
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Class1", fClass1, value)
        End Set
    End Property
    Dim fDigitalSignature() As Byte
    <Size(SizeAttribute.Unlimited)> _
    Public Property DigitalSignature() As Byte()
        Get
            Return fDigitalSignature
        End Get
        Set(ByVal value As Byte())
            SetPropertyValue(Of Byte())("DigitalSignature", fDigitalSignature, value)
        End Set
    End Property
    Dim fState As Boolean
    Public Property State() As Boolean
        Get
            Return fState
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("State", fState, value)
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
    Dim fUserId As Integer
    Public Property UserId() As Integer
        Get
            Return fUserId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("UserId", fUserId, value)
        End Set
    End Property
    <Association("Common_SupplierReferencesCommon_ThirdParty", GetType(CommonSupplierReportXpo))> _
    Public ReadOnly Property Common_Suppliers() As XPCollection(Of CommonSupplierReportXpo)
        Get
            Return GetCollection(Of CommonSupplierReportXpo)("Common_Suppliers")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
