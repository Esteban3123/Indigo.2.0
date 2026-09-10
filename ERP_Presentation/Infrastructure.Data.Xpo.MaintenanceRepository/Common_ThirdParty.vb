Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Common.ThirdParty")>
Public Class Common_ThirdParty
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
    <Indexed(Name:="IX_ThirdParty_Nit_UNI", Unique:=True)>
    <Size(20)>
    Public Property Nit() As String
        Get
            Return fNit
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Nit", fNit, value)
        End Set
    End Property
    Dim fDigitVerification As String
    <Size(1)>
    Public Property DigitVerification() As String
        Get
            Return fDigitVerification
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("DigitVerification", fDigitVerification, value)
        End Set
    End Property
    Dim fName As String
    <Size(300)>
    Public Property Name() As String
        Get
            Return fName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Name", fName, value)
        End Set
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
    Dim fStateEnterpriseType As Byte
    Public Property StateEnterpriseType() As Byte
        Get
            Return fStateEnterpriseType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("StateEnterpriseType", fStateEnterpriseType, value)
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
    <Size(15)>
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
    <Persistent("Class")>
    Public Property Class1() As Byte
        Get
            Return fClass1
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Class1", fClass1, value)
        End Set
    End Property
    Dim fDigitalSignature() As Byte
    <Size(SizeAttribute.Unlimited)>
    <MemberDesignTimeVisibility(True)>
    Public Property DigitalSignature() As Byte()
        Get
            Return fDigitalSignature
        End Get
        Set(ByVal value() As Byte)
            SetPropertyValue(Of Byte())("DigitalSignature", fDigitalSignature, value)
        End Set
    End Property
    Dim fCodeCIIU As String
    <Size(10)>
    Public Property CodeCIIU() As String
        Get
            Return fCodeCIIU
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CodeCIIU", fCodeCIIU, value)
        End Set
    End Property
    Dim fHandlesBranchOffice As Boolean
    Public Property HandlesBranchOffice() As Boolean
        Get
            Return fHandlesBranchOffice
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("HandlesBranchOffice", fHandlesBranchOffice, value)
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
    Dim fCodeDivipola As String
    <Size(20)>
    Public Property CodeDivipola() As String
        Get
            Return fCodeDivipola
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CodeDivipola", fCodeDivipola, value)
        End Set
    End Property
    Dim fIVARetentionConceptId As Integer
    Public Property IVARetentionConceptId() As Integer
        Get
            Return fIVARetentionConceptId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IVARetentionConceptId", fIVARetentionConceptId, value)
        End Set
    End Property

#End Region

#Region "Custom Members"

    Dim fNitName As String
    'columna que devuelve el nit y el nombre concatenado
    <Size(50)>
    <PersistentAlias("concat(concat(Nit,' - '),Name)")>
    Public ReadOnly Property NitName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("NitName"))
        End Get
    End Property

#End Region

#Region "Association Members"

    <Association("FixedAsset_FixedAssetResponsibleReferencesCommon_ThirdParty")>
    Public ReadOnly Property FixedAsset_FixedAssetResponsibles() As XPCollection(Of FixedAsset_FixedAssetResponsible)
        Get
            Return GetCollection(Of FixedAsset_FixedAssetResponsible)("FixedAsset_FixedAssetResponsibles")
        End Get
    End Property
    <Association("Payroll_ForeclousureReferencesCommon_ThirdParty")>
    Public ReadOnly Property Payroll_Foreclousures() As XPCollection(Of Payroll_Foreclousure)
        Get
            Return GetCollection(Of Payroll_Foreclousure)("Payroll_Foreclousures")
        End Get
    End Property
    <Association("Payroll_ForeclousureReferencesCommon_ThirdParty1")>
    Public ReadOnly Property Payroll_Foreclousures1() As XPCollection(Of Payroll_Foreclousure)
        Get
            Return GetCollection(Of Payroll_Foreclousure)("Payroll_Foreclousures1")
        End Get
    End Property

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