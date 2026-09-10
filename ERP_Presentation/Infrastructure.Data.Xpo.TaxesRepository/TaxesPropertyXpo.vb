Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Taxes.TaxesProperty")> _
Public Class TaxesPropertyXpo
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

    Dim fCodeDeparment As String
    Public Property CodeDeparment() As String
        Get
            Return fCodeDeparment
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CodeDeparment", fCodeDeparment, value)
        End Set
    End Property

    Dim fCodeCity As String
    Public Property CodeCity() As String
        Get
            Return fCodeCity
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CodeCity", fCodeCity, value)
        End Set
    End Property

    Dim fCode As String
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property

    Dim fTaxed As Boolean
    Public Property Taxed() As Boolean
        Get
            Return fTaxed
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Taxed", fTaxed, value)
        End Set
    End Property

    Dim fThirdPartyId As CommonThirdPartyXpo
    <Association("TaxesPropertyItemReferenceThirdParty")> _
    Public Property ThirdPartyId() As CommonThirdPartyXpo
        Get
            Return fThirdPartyId
        End Get
        Set(ByVal value As CommonThirdPartyXpo)
            SetPropertyValue(Of CommonThirdPartyXpo)("ThirdPartyId", fThirdPartyId, value)
        End Set
    End Property

    Dim fAddres As String
    Public Property Addres() As String
        Get
            Return fAddres
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Addres", fAddres, value)
        End Set
    End Property

    Dim fCommune As String
    Public Property Commune() As String
        Get
            Return fCommune
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Commune", fCommune, value)
        End Set
    End Property

    Dim fEconomicDestiny As String
    Public Property EconomicDestiny() As String
        Get
            Return fEconomicDestiny
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("EconomicDestiny", fEconomicDestiny, value)
        End Set
    End Property

    Dim fLandArea As Decimal
    Public Property LandArea() As Decimal
        Get
            Return fLandArea
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("LandArea", fLandArea, value)
        End Set
    End Property

    Dim fBuiltArea As Decimal
    Public Property BuiltArea() As Decimal
        Get
            Return fBuiltArea
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("BuiltArea", fBuiltArea, value)
        End Set
    End Property

    Dim fAppraisal As Decimal
    Public Property Appraisal() As Decimal
        Get
            Return fAppraisal
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Appraisal", fAppraisal, value)
        End Set
    End Property

    Dim fLatitude As Decimal
    Public Property Latitude() As Decimal
        Get
            Return fLatitude
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Latitude", fLatitude, value)
        End Set
    End Property

    Dim fLongitude As Decimal
    Public Property Longitude() As Decimal
        Get
            Return fLongitude
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Longitude", fLongitude, value)
        End Set
    End Property

    Dim fStatus As Byte
    Public Property Status() As Byte
        Get
            Return fStatus
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Status", fStatus, value)
        End Set
    End Property

    <Association("TaxesLiquidationDetailItemReferenceTaxesProperty", GetType(TaxesLiquidationDetailXpo))> _
    Public ReadOnly Property TaxesLiquidationDetailXpo() As XPCollection(Of TaxesLiquidationDetailXpo)
        Get
            Return GetCollection(Of TaxesLiquidationDetailXpo)("TaxesLiquidationDetailXpo")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
