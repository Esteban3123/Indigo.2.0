Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel
<Persistent("FixedAsset.FixedAssetPolicy")> _
Public Class FixedAssetPolizaXpo
    Inherits XPLiteObject
    Dim fId As Integer
    <Key()> _
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
    Dim fPolicyTypeId As Integer
    Public Property PolicyTypeId() As Integer
        Get
            Return fPolicyTypeId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("PolicyTypeId", fPolicyTypeId, value)
        End Set
    End Property

    Dim fInsuranceId As FixedAssetInsuranceXpo
    <Association("FixedAssetPolizaReferencesFixedAssetInsurance")>
    Public Property InsuranceId() As FixedAssetInsuranceXpo
        Get
            Return fInsuranceId
        End Get
        Set(ByVal value As FixedAssetInsuranceXpo)
            SetPropertyValue(Of FixedAssetInsuranceXpo)("InsuranceId", fInsuranceId, value)
        End Set
    End Property

    Dim fName As String
    <Size(50)> _
    Public Property Name() As String
        Get
            Return fName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Name", fName, value)
        End Set
    End Property
    Dim fInitialDate As DateTime
    Public Property InitialDate() As DateTime
        Get
            Return fInitialDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("InitialDate", fInitialDate, value)
        End Set
    End Property
    Dim fEndDate As DateTime
    Public Property EndDate() As DateTime
        Get
            Return fEndDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("EndDate", fEndDate, value)
        End Set
    End Property
    Dim fObject1 As String
    <Size(200)> _
    <Persistent("Object")> _
    Public Property Object1() As String
        Get
            Return fObject1
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Object1", fObject1, value)
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

    'columna que devuelve el nit y el nombre concatenado
    <Size(50)> _
    <PersistentAlias("concat(concat(Code,' - '),Name)")>
    Public ReadOnly Property CodeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeName"))
        End Get
    End Property

    <Association("FixedAssetPolizaReferences", GetType(FixedAssetPurchaseOrderEquipmentXpo))> _
    Public ReadOnly Property FixedAssetPurchaseOrderEquipmentXpo() As XPCollection(Of FixedAssetPurchaseOrderEquipmentXpo)
        Get
            Return GetCollection(Of FixedAssetPurchaseOrderEquipmentXpo)("FixedAssetPurchaseOrderEquipmentXpo")
        End Get
    End Property

    <Association("RemissionEntranceItemReferencePolicy", GetType(FixedAssetRemissionEntranceItemXpo))> _
    Public ReadOnly Property FixedAssetRemissionEntranceItemXpo() As XPCollection(Of FixedAssetRemissionEntranceItemXpo)
        Get
            Return GetCollection(Of FixedAssetRemissionEntranceItemXpo)("FixedAssetRemissionEntranceItemXpo")
        End Get
    End Property

    <Association("PhysicalReferencePolicy", GetType(FixedAssetPhysicalAssetXpo))> _
    Public ReadOnly Property FixedAssetPhysicalAssetXpo() As XPCollection(Of FixedAssetPhysicalAssetXpo)
        Get
            Return GetCollection(Of FixedAssetPhysicalAssetXpo)("FixedAssetPhysicalAssetXpo")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class
