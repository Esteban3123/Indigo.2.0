Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("FixedAsset.FixedAssetItemCatalog")> _
Public Class FixedAssetEquipmentCatalogXpo
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
    Dim fCode As String
    <Size(20)> _
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property
    Dim fDescription As String
    <Size(50)> _
    Public Property Description() As String
        Get
            Return fDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Description", fDescription, value)
        End Set
    End Property
    Dim fActive As Boolean
    Public Property Active() As Boolean
        Get
            Return fActive
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Active", fActive, value)
        End Set
    End Property

    Dim fClassification As Integer
    Public Property Classification() As Integer
        Get
            Return fClassification
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Classification", fClassification, value)
        End Set
    End Property

    Dim fIncomeAccountId As Integer
    Public Property IncomeAccountId() As Integer
        Get
            Return fIncomeAccountId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IncomeAccountId", fIncomeAccountId, value)
        End Set
    End Property

    Dim fIncomeLeasingAccountId As PUCServiceXpo
    <Association("ItemCatalogReferenceMainAccount")>
    Public Property IncomeLeasingAccountId() As PUCServiceXpo
        Get
            Return fIncomeLeasingAccountId
        End Get
        Set(ByVal value As PUCServiceXpo)
            SetPropertyValue(Of PUCServiceXpo)("IncomeLeasingAccountId", fIncomeLeasingAccountId, value)
        End Set
    End Property

    Dim fDebitLoanAccountId As Integer
    Public Property DebitLoanAccountId() As Integer
        Get
            Return fDebitLoanAccountId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("DebitLoanAccountId", fDebitLoanAccountId, value)
        End Set
    End Property
    Dim fCreditLoanAccountId As Integer
    Public Property CreditLoanAccountId() As Integer
        Get
            Return fCreditLoanAccountId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CreditLoanAccountId", fCreditLoanAccountId, value)
        End Set
    End Property
    Dim fDepreciationAccountId As Integer
    Public Property DepreciationAccountId() As Integer
        Get
            Return fDepreciationAccountId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("DepreciationAccountId", fDepreciationAccountId, value)
        End Set
    End Property
    Dim fDeclarantRetentionAccountPayableConceptId As Integer
    Public Property DeclarantRetentionAccountPayableConceptId() As Integer
        Get
            Return fDeclarantRetentionAccountPayableConceptId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("DeclarantRetentionAccountPayableConceptId", fDeclarantRetentionAccountPayableConceptId, value)
        End Set
    End Property
    Dim fNotDeclarantRetentionAccountPayableConceptId As Integer
    Public Property NotDeclarantRetentionAccountPayableConceptId() As Integer
        Get
            Return fNotDeclarantRetentionAccountPayableConceptId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("NotDeclarantRetentionAccountPayableConceptId", fNotDeclarantRetentionAccountPayableConceptId, value)
        End Set
    End Property
    Dim fStatus As Boolean
    Public Property Status() As Boolean
        Get
            Return fStatus
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Status", fStatus, value)
        End Set
    End Property
    Dim fCreationUser As String
    <Size(20)> _
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
    <Size(20)> _
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

    Dim fCodeNitName As String
    'columna que devuelve el nit y el nombre concatenado
    <Size(50)> _
    <PersistentAlias("concat(concat(Code,' - '),Description)")>
    Public ReadOnly Property CodeDescription() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeDescription"))
        End Get
    End Property

    <Association("ItemReferenceItemCatalog", GetType(FixedAssetEquipmentXpo))> _
    Public ReadOnly Property FixedAssetEquipmentXpo() As XPCollection(Of FixedAssetEquipmentXpo)
        Get
            Return GetCollection(Of FixedAssetEquipmentXpo)("FixedAssetEquipmentXpo")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class

