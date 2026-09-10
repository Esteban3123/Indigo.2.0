Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel
<Persistent("FixedAsset.PhysicalFixedAsset")> _
Public Class PhysicalFixedAssetXpo
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

    Dim fDetailEquipmentId As Integer
    Public Property DetailEquipmentId() As Integer
        Get
            Return fDetailEquipmentId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("DetailEquipmentId", fDetailEquipmentId, value)
        End Set
    End Property

    Dim fEquipmentId As Integer
    Public Property EquipmentId() As Integer
        Get
            Return fEquipmentId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("EquipmentId", fEquipmentId, value)
        End Set
    End Property

    Dim fResponsibleId As Integer
    Public Property ResponsibleId() As Integer
        Get
            Return fResponsibleId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ResponsibleId", fResponsibleId, value)
        End Set
    End Property

    Dim fSupplierDistributionLinesId As Integer
    Public Property SupplierDistributionLinesId() As Integer
        Get
            Return fSupplierDistributionLinesId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("SupplierDistributionLinesId", fSupplierDistributionLinesId, value)
        End Set
    End Property

    Dim fEquipmentValue As Decimal
    Public Property EquipmentValue() As Decimal
        Get
            Return fEquipmentValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("EquipmentValue", fEquipmentValue, value)
        End Set
    End Property

    Dim fLicensePlate As String
    Public Property LicensePlate() As String
        Get
            Return fLicensePlate
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("LicensePlate", fLicensePlate, value)
        End Set
    End Property

    Dim fDepreciate As Boolean
    Public Property Depreciate() As Boolean
        Get
            Return fDepreciate
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Depreciate", fDepreciate, value)
        End Set
    End Property

    Dim fAlwaysDepreciateMayorAmount As Boolean
    Public Property AlwaysDepreciateMayorAmount() As Boolean
        Get
            Return fAlwaysDepreciateMayorAmount
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("AlwaysDepreciateMayorAmount", fAlwaysDepreciateMayorAmount, value)
        End Set
    End Property

    Dim fCarryingAmount As Decimal
    Public Property CarryingAmount() As Decimal
        Get
            Return fCarryingAmount
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("CarryingAmount", fCarryingAmount, value)
        End Set
    End Property

    Dim fDevaluationValue As Decimal
    Public Property DevaluationValue() As Decimal
        Get
            Return fDevaluationValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("DevaluationValue", fDevaluationValue, value)
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

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
