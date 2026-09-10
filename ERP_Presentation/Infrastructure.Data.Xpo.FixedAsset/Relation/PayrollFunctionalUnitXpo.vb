Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Payroll.FunctionalUnit")> _
Partial Public Class PayrollFunctionalUnitXpo
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
    Dim fBranchOfficeId As Integer
    Public Property BranchOfficeId() As Integer
        Get
            Return fBranchOfficeId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("BranchOfficeId", fBranchOfficeId, value)
        End Set
    End Property
    Dim fCostCenterId As Integer
    Public Property CostCenterId() As Integer
        Get
            Return fCostCenterId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CostCenterId", fCostCenterId, value)
        End Set
    End Property
    Dim fProductionCenterId As Integer
    Public Property ProductionCenterId() As Integer
        Get
            Return fProductionCenterId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ProductionCenterId", fProductionCenterId, value)
        End Set
    End Property
    Dim fAccountingStructureId As Integer
    Public Property AccountingStructureId() As Integer
        Get
            Return fAccountingStructureId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("AccountingStructureId", fAccountingStructureId, value)
        End Set
    End Property
    Dim fUnitType As Byte
    Public Property UnitType() As Byte
        Get
            Return fUnitType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("UnitType", fUnitType, value)
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

    <Association("FixedAssetPurchaseOrderDetailReferencesFunctionalUnit", GetType(FixedAssetPurchaseOrderEquipmentDetailXpo))> _
    Public ReadOnly Property FixedAssetPurchaseOrderEquipmentDetailXpo() As XPCollection(Of FixedAssetPurchaseOrderEquipmentDetailXpo)
        Get
            Return GetCollection(Of FixedAssetPurchaseOrderEquipmentDetailXpo)("FixedAssetPurchaseOrderEquipmentDetailXpo")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class

