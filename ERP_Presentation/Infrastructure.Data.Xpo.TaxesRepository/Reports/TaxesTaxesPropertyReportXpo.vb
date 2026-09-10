Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Taxes.TaxesProperty")> _
Public Class TaxesTaxesPropertyReportXpo
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
    <Size(50)> _
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
    <Association("Taxes_TaxesPropertyReferencesCommon_ThirdParty")> _
    Public Property ThirdPartyId() As CommonThirdPartyXpo
        Get
            Return fThirdPartyId
        End Get
        Set(ByVal value As CommonThirdPartyXpo)
            SetPropertyValue(Of CommonThirdPartyXpo)("ThirdPartyId", fThirdPartyId, value)
        End Set
    End Property
    Dim fAddres As String
    <Size(200)> _
    Public Property Addres() As String
        Get
            Return fAddres
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Addres", fAddres, value)
        End Set
    End Property
    Dim fCommune As String
    <Size(5)> _
    Public Property Commune() As String
        Get
            Return fCommune
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Commune", fCommune, value)
        End Set
    End Property
    Dim fEconomicDestiny As String
    <Size(5)> _
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
    <Association("Taxes_TaxesPropertyAppraisalReferencesTaxes_TaxesProperty", GetType(TaxesTaxesPropertyAppraisalReportXpo))> _
    Public ReadOnly Property TaxesTaxesPropertyAppraisalReportXpo() As XPCollection(Of TaxesTaxesPropertyAppraisalReportXpo)
        Get
            Return GetCollection(Of TaxesTaxesPropertyAppraisalReportXpo)("TaxesTaxesPropertyAppraisalReportXpo")
        End Get
    End Property
    <Association("Taxes_TaxesPropertyOwnerReferencesTaxes_TaxesProperty", GetType(TaxesTaxesPropertyOwnerReportXpo))> _
    Public ReadOnly Property TaxesTaxesPropertyOwnerReportXpo() As XPCollection(Of TaxesTaxesPropertyOwnerReportXpo)
        Get
            Return GetCollection(Of TaxesTaxesPropertyOwnerReportXpo)("TaxesTaxesPropertyOwnerReportXpo")
        End Get
    End Property
    <Association("Taxes_TaxesInvoiceReferencesTaxes_Property", GetType(TaxesTaxesInvoiceReportXpo))> _
    Public ReadOnly Property TaxesTaxesInvoiceReportXpo() As XPCollection(Of TaxesTaxesInvoiceReportXpo)
        Get
            Return GetCollection(Of TaxesTaxesInvoiceReportXpo)("TaxesTaxesInvoiceReportXpo")
        End Get
    End Property
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class
