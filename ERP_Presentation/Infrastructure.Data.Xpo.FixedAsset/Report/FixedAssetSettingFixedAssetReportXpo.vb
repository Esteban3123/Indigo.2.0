Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("FixedAsset.SettingFixedAsset")> _
Public Class FixedAssetSettingFixedAssetReportXpo
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
    Dim fOperatingUnitId As Integer
    Public Property OperatingUnitId() As Integer
        Get
            Return fOperatingUnitId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("OperatingUnitId", fOperatingUnitId, value)
        End Set
    End Property
    Dim fIdThirdPartyResponsible As CommonThirdPartyReportXpo
    <Association("FixedAssetSettingFixedAssetReportXpoReferencesCommonThirdPartyReportXpo")>
    Public Property IdThirdPartyResponsible() As CommonThirdPartyReportXpo
        Get
            Return fIdThirdPartyResponsible
        End Get
        Set(ByVal value As CommonThirdPartyReportXpo)
            SetPropertyValue(Of CommonThirdPartyReportXpo)("IdThirdPartyResponsible", fIdThirdPartyResponsible, value)
        End Set
    End Property

    Dim fCurrencyId As CommonCurrencyXpo
    <Association("CurrencyReferenceFixedAssetSetting")>
    Public Property CurrencyId() As CommonCurrencyXpo
        Get
            Return fCurrencyId
        End Get
        Set(ByVal value As CommonCurrencyXpo)
            SetPropertyValue("CurrencyId", fCurrencyId, value)
        End Set
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class
