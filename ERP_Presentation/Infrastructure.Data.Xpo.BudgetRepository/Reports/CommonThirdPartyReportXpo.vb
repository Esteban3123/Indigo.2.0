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
    '<Indexed(Name:="IX_ThirdParty_Nit_UNI", Unique:=True)> _
    <Size(15)> _
    Public Property Nit() As String
        Get
            Return fNit
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Nit", fNit, value)
        End Set
    End Property

    <Size(50)> _
    <PersistentAlias("concat(concat(Nit,' - '),Name)")>
    Public ReadOnly Property NitName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("NitName"))
        End Get
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
    Dim fCodeCIIU As String
    <Size(10)> _
    Public Property CodeCIIU() As String
        Get
            Return fCodeCIIU
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CodeCIIU", fCodeCIIU, value)
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
    <Association("Budget_BudgetaryValidityReferencesCommon_ThirdParty", GetType(BudgetBudgetaryValidityReportXpo))> _
    Public ReadOnly Property Budget_BudgetaryValiditys() As XPCollection(Of BudgetBudgetaryValidityReportXpo)
        Get
            Return GetCollection(Of BudgetBudgetaryValidityReportXpo)("Budget_BudgetaryValiditys")
        End Get
    End Property
    <Association("Budget_BudgetaryValidityReferencesCommon_ThirdParty1", GetType(BudgetBudgetaryValidityReportXpo))> _
    Public ReadOnly Property Budget_BudgetaryValiditys1() As XPCollection(Of BudgetBudgetaryValidityReportXpo)
        Get
            Return GetCollection(Of BudgetBudgetaryValidityReportXpo)("Budget_BudgetaryValiditys1")
        End Get
    End Property
    <Association("Budget_BudgetaryValidityReferencesCommon_ThirdParty2", GetType(BudgetBudgetaryValidityReportXpo))> _
    Public ReadOnly Property Budget_BudgetaryValiditys2() As XPCollection(Of BudgetBudgetaryValidityReportXpo)
        Get
            Return GetCollection(Of BudgetBudgetaryValidityReportXpo)("Budget_BudgetaryValiditys2")
        End Get
    End Property
    <Association("Budget_BudgetaryValidityReferencesCommon_ThirdParty3", GetType(BudgetBudgetaryEntityReportXpo))> _
    Public ReadOnly Property Budget_BudgetaryValiditys3() As XPCollection(Of BudgetBudgetaryEntityReportXpo)
        Get
            Return GetCollection(Of BudgetBudgetaryEntityReportXpo)("Budget_BudgetaryValiditys3")
        End Get
    End Property

    <Association("Budget_DependencyReferencesCommon_ThirdParty", GetType(BudgetDependencyReportXpo))> _
    Public ReadOnly Property Budget_Dependency() As XPCollection(Of BudgetDependencyReportXpo)
        Get
            Return GetCollection(Of BudgetDependencyReportXpo)("Budget_Dependency")
        End Get
    End Property

    <Association("Budget_CollectionReferencesCommon_ThirdParty", GetType(BudgetCollectionXpo))>
    Public ReadOnly Property Budget_Collections() As XPCollection(Of BudgetCollectionXpo)
        Get
            Return GetCollection(Of BudgetCollectionXpo)("Budget_Collections")
        End Get
    End Property

    <Association("Budget_Commitment_References_Common_ThirdParty", GetType(BudgetCommitmentXpo))>
    Public ReadOnly Property Budget_Commitments() As XPCollection(Of BudgetCommitmentXpo)
        Get
            Return GetCollection(Of BudgetCommitmentXpo)("Budget_Commitments")
        End Get
    End Property

    <Association("Budget_ObligationReferencesCommon_ThirdParty", GetType(BudgetObligationXpo))>
    Public ReadOnly Property Budget_Obligations() As XPCollection(Of BudgetObligationXpo)
        Get
            Return GetCollection(Of BudgetObligationXpo)("Budget_Obligations")
        End Get
    End Property

    <Association("Budget_PaymentOrder_References_Common_ThirdParty", GetType(BudgetPaymentOrderXpo))>
    Public ReadOnly Property Budget_PaymentOrders() As XPCollection(Of BudgetPaymentOrderXpo)
        Get
            Return GetCollection(Of BudgetPaymentOrderXpo)("Budget_PaymentOrders")
        End Get
    End Property

    <Association("Budget_RecognitionReferencesCommon_ThirdParty", GetType(BudgetRecognitionReportXpo))>
    Public ReadOnly Property Budget_Recognition() As XPCollection(Of BudgetRecognitionReportXpo)
        Get
            Return GetCollection(Of BudgetRecognitionReportXpo)("Budget_Recognition")
        End Get
    End Property

    <Association("Budget_CollectionReportReferencesCommon_ThirdParty", GetType(BudgetCollectionReportXpo))>
    Public ReadOnly Property Budget_Collection() As XPCollection(Of BudgetCollectionReportXpo)
        Get
            Return GetCollection(Of BudgetCollectionReportXpo)("Budget_Collection")
        End Get
    End Property

    <Association("Budget_CommitmentReportReferencesCommon_ThirdParty", GetType(BudgetCommitmentReportXpo))>
    Public ReadOnly Property Budget_Commitment() As XPCollection(Of BudgetCommitmentReportXpo)
        Get
            Return GetCollection(Of BudgetCommitmentReportXpo)("Budget_Commitment")
        End Get
    End Property

    <Association("Budget_ObligationReportReferencesCommon_ThirdParty", GetType(BudgetObligationReportXpo))>
    Public ReadOnly Property Budget_Obligation() As XPCollection(Of BudgetObligationReportXpo)
        Get
            Return GetCollection(Of BudgetObligationReportXpo)("Budget_Obligation")
        End Get
    End Property

    <Association("Budget_PaymentOrderReferencesCommon_ThirdParty", GetType(BudgetPaymentOrderReportXpo))>
    Public ReadOnly Property Budget_PaymentOrder() As XPCollection(Of BudgetPaymentOrderReportXpo)
        Get
            Return GetCollection(Of BudgetPaymentOrderReportXpo)("Budget_PaymentOrder")
        End Get
    End Property

    <Association("Budget_Recognition_References_Common_ThirdParty", GetType(BudgetRecognitionXpo))>
    Public ReadOnly Property Budget_Recognitions() As XPCollection(Of BudgetRecognitionXpo)
        Get
            Return GetCollection(Of BudgetRecognitionXpo)("Budget_Recognitions")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
