Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("FixedAsset.FixedAssetActiveOutputDetail")> _
Public Class FixedAssetFixedAssetActiveOutputDetailReportXpo
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
    Dim fFixedAssetActiveOutputId As FixedAssetFixedAssetActiveOutputReportXpo
    <Association("FixedAsset_FixedAssetActiveOutputDetailReferencesFixedAsset_FixedAssetActiveOutput")> _
    Public Property FixedAssetActiveOutputId() As FixedAssetFixedAssetActiveOutputReportXpo
        Get
            Return fFixedAssetActiveOutputId
        End Get
        Set(ByVal value As FixedAssetFixedAssetActiveOutputReportXpo)
            SetPropertyValue(Of FixedAssetFixedAssetActiveOutputReportXpo)("FixedAssetActiveOutputId", fFixedAssetActiveOutputId, value)
        End Set
    End Property
    Dim fActiveType As Byte
    Public Property ActiveType() As Byte
        Get
            Return fActiveType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("ActiveType", fActiveType, value)
        End Set
    End Property
    Dim fPhysicalAssetId As FixedAssetPhysicalAssetReportXpo
    <Association("FixedAsset_FixedAssetActiveOutputDetailReferencesFixedAsset_FixedAssetPhysicalAsset")> _
    Public Property PhysicalAssetId() As FixedAssetPhysicalAssetReportXpo
        Get
            Return fPhysicalAssetId
        End Get
        Set(ByVal value As FixedAssetPhysicalAssetReportXpo)
            SetPropertyValue(Of FixedAssetPhysicalAssetReportXpo)("PhysicalAssetId", fPhysicalAssetId, value)
        End Set
    End Property
    Dim fPhysicalAssetPartsId As FixedAssetFixedAssetPhysicalAssetPartsReportXpo
    <Association("FixedAsset_FixedAssetActiveOutputDetailReferencesFixedAsset_FixedAssetPhysicalAssetParts")> _
    Public Property PhysicalAssetPartsId() As FixedAssetFixedAssetPhysicalAssetPartsReportXpo
        Get
            Return fPhysicalAssetPartsId
        End Get
        Set(ByVal value As FixedAssetFixedAssetPhysicalAssetPartsReportXpo)
            SetPropertyValue(Of FixedAssetFixedAssetPhysicalAssetPartsReportXpo)("PhysicalAssetPartsId", fPhysicalAssetPartsId, value)
        End Set
    End Property
    Dim fMainAccountId As GeneralLedgerMainAccountsReportXpo
    <Association("FixedAsset_FixedAssetActiveOutputDetailReferencesGeneralLedger_MainAccounts")> _
    Public Property MainAccountId() As GeneralLedgerMainAccountsReportXpo
        Get
            Return fMainAccountId
        End Get
        Set(ByVal value As GeneralLedgerMainAccountsReportXpo)
            SetPropertyValue(Of GeneralLedgerMainAccountsReportXpo)("MainAccountId", fMainAccountId, value)
        End Set
    End Property
    Dim fOutputType As Byte
    Public Property OutputType() As Byte
        Get
            Return fOutputType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("OutputType", fOutputType, value)
        End Set
    End Property
    Dim fLowType As Byte
    Public Property LowType() As Byte
        Get
            Return fLowType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("LowType", fLowType, value)
        End Set
    End Property
    Dim fSalesValue As Decimal
    Public Property SalesValue() As Decimal
        Get
            Return fSalesValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("SalesValue", fSalesValue, value)
        End Set
    End Property
    Dim fThirdPartyId As CommonThirdPartyReportXpo
    <Association("FixedAsset_FixedAssetActiveOutputDetailReferencesCommon_ThirdParty")> _
    Public Property ThirdPartyId() As CommonThirdPartyReportXpo
        Get
            Return fThirdPartyId
        End Get
        Set(ByVal value As CommonThirdPartyReportXpo)
            SetPropertyValue(Of CommonThirdPartyReportXpo)("ThirdPartyId", fThirdPartyId, value)
        End Set
    End Property
    Dim fAccountReceivableId As Integer
    Public Property AccountReceivableId() As Integer
        Get
            Return fAccountReceivableId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("AccountReceivableId", fAccountReceivableId, value)
        End Set
    End Property
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class
