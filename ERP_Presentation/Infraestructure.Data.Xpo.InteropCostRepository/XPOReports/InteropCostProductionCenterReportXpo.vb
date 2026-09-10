Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("InteropCost.ProductionCenter")> _
Public Class InteropCostProductionCenterReportXpo
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
    Public Property Name() As String
        Get
            Return fName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Name", fName, value)
        End Set
    End Property
    Dim fOrganizationalStructureOfCostId As Integer
    Public Property OrganizationalStructureOfCostId() As Integer
        Get
            Return fOrganizationalStructureOfCostId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("OrganizationalStructureOfCostId", fOrganizationalStructureOfCostId, value)
        End Set
    End Property
    Dim fArea As Decimal
    Public Property Area() As Decimal
        Get
            Return fArea
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Area", fArea, value)
        End Set
    End Property
    Dim fCenterType As Byte
    Public Property CenterType() As Byte
        Get
            Return fCenterType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("CenterType", fCenterType, value)
        End Set
    End Property
    Dim fCancellationCostMainAccountId As Integer
    Public Property CancellationCostMainAccountId() As Integer
        Get
            Return fCancellationCostMainAccountId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CancellationCostMainAccountId", fCancellationCostMainAccountId, value)
        End Set
    End Property
    Dim fDescription As String
    <Size(300)> _
    Public Property Description() As String
        Get
            Return fDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Description", fDescription, value)
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
    <Association("InteropCost_DistributionDirectCostDetailReferencesInteropCost_ProductionCenter", GetType(InteropCostDistributionDirectCostDetailReportXpo))> _
    Public ReadOnly Property InteropCost_DistributionDirectCostDetails() As XPCollection(Of InteropCostDistributionDirectCostDetailReportXpo)
        Get
            Return GetCollection(Of InteropCostDistributionDirectCostDetailReportXpo)("InteropCost_DistributionDirectCostDetails")
        End Get
    End Property



    <Association("InteropCost_LogisticsProductionCenterRecordReferencesInteropCost_ProductionCenter", GetType(InteropCostLogisticsProductionCenterRecordReportXpo))> _
    Public ReadOnly Property InteropCost_LogisticsProductionCenterRecords() As XPCollection(Of InteropCostLogisticsProductionCenterRecordReportXpo)
        Get
            Return GetCollection(Of InteropCostLogisticsProductionCenterRecordReportXpo)("InteropCost_LogisticsProductionCenterRecords")
        End Get
    End Property
    <Association("InteropCost_LogisticsProductionCenterRecordDetailReferencesInteropCost_ProductionCenter", GetType(InteropCostLogisticsProductionCenterRecordDetailReportXpo))> _
    Public ReadOnly Property InteropCost_LogisticsProductionCenterRecordDetails() As XPCollection(Of InteropCostLogisticsProductionCenterRecordDetailReportXpo)
        Get
            Return GetCollection(Of InteropCostLogisticsProductionCenterRecordDetailReportXpo)("InteropCost_LogisticsProductionCenterRecordDetails")
        End Get
    End Property




    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class
