Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Taxes.TaxesPropertyOwner")> _
Public Class TaxesTaxesPropertyOwnerReportXpo
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
    Dim fTaxesPropertyId As TaxesTaxesPropertyReportXpo
    <Association("Taxes_TaxesPropertyOwnerReferencesTaxes_TaxesProperty")> _
    Public Property TaxesPropertyId() As TaxesTaxesPropertyReportXpo
        Get
            Return fTaxesPropertyId
        End Get
        Set(ByVal value As TaxesTaxesPropertyReportXpo)
            SetPropertyValue(Of TaxesTaxesPropertyReportXpo)("TaxesPropertyId", fTaxesPropertyId, value)
        End Set
    End Property
    Dim fThirdPartyId As CommonThirdPartyXpo
    <Association("Taxes_TaxesPropertyOwnerReferencesCommon_ThirdParty")> _
    Public Property ThirdPartyId() As CommonThirdPartyXpo
        Get
            Return fThirdPartyId
        End Get
        Set(ByVal value As CommonThirdPartyXpo)
            SetPropertyValue(Of CommonThirdPartyXpo)("ThirdPartyId", fThirdPartyId, value)
        End Set
    End Property
    Dim fOrderOwner As Integer
    Public Property OrderOwner() As Integer
        Get
            Return fOrderOwner
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("OrderOwner", fOrderOwner, value)
        End Set
    End Property
    Dim fPercentage As Decimal
    Public Property Percentage() As Decimal
        Get
            Return fPercentage
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Percentage", fPercentage, value)
        End Set
    End Property
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class
