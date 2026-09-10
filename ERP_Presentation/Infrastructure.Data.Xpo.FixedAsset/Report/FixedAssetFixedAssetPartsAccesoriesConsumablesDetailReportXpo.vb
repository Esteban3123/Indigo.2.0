Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("FixedAsset.FixedAssetPartsAccesoriesConsumablesDetail")> _
Public Class FixedAssetFixedAssetPartsAccesoriesConsumablesDetailReportXpo
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
    Dim fPartsAccesoriesConsumiblesId As FixedAssetFixedAssetPartsAccesoriesConsumablesReportXpo
    <Association("FixedAsset_FixedAssetPartsAccesoriesConsumablesDetailReferencesFixedAsset_FixedAssetPartsAccesoriesConsumables")> _
    Public Property PartsAccesoriesConsumiblesId() As FixedAssetFixedAssetPartsAccesoriesConsumablesReportXpo
        Get
            Return fPartsAccesoriesConsumiblesId
        End Get
        Set(ByVal value As FixedAssetFixedAssetPartsAccesoriesConsumablesReportXpo)
            SetPropertyValue(Of FixedAssetFixedAssetPartsAccesoriesConsumablesReportXpo)("PartsAccesoriesConsumiblesId", fPartsAccesoriesConsumiblesId, value)
        End Set
    End Property
    Dim fLegalBookId As GeneralLedgerLegalBookReportXpo
    <Association("FixedAsset_FixedAssetPartsAccesoriesConsumablesDetailReferencesGeneralLedger_LegalBook")> _
    Public Property LegalBookId() As GeneralLedgerLegalBookReportXpo
        Get
            Return fLegalBookId
        End Get
        Set(ByVal value As GeneralLedgerLegalBookReportXpo)
            SetPropertyValue(Of GeneralLedgerLegalBookReportXpo)("LegalBookId", fLegalBookId, value)
        End Set
    End Property
    Dim fLifeTime As Integer
    Public Property LifeTime() As Integer
        Get
            Return fLifeTime
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("LifeTime", fLifeTime, value)
        End Set
    End Property
    Dim fUnitLifeTime As Byte
    Public Property UnitLifeTime() As Byte
        Get
            Return fUnitLifeTime
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("UnitLifeTime", fUnitLifeTime, value)
        End Set
    End Property
    Dim fDepreciationType As Byte
    Public Property DepreciationType() As Byte
        Get
            Return fDepreciationType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("DepreciationType", fDepreciationType, value)
        End Set
    End Property
    Dim fTotalProductionUnit As Decimal
    Public Property TotalProductionUnit() As Decimal
        Get
            Return fTotalProductionUnit
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TotalProductionUnit", fTotalProductionUnit, value)
        End Set
    End Property
    Dim fPercentageRescue As Decimal
    Public Property PercentageRescue() As Decimal
        Get
            Return fPercentageRescue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("PercentageRescue", fPercentageRescue, value)
        End Set
    End Property
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class
