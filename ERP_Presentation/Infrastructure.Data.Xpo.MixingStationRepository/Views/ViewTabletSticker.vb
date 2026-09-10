'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.MixingStation
' Author           : Andrea Coqueco
' Created          : 2022-10-24
'
' Copyright        : (c) . All rights reserved.
'*************************************************************
Imports DevExpress.Xpo

<Persistent("MixingStation.ViewTabletSticker")>
Partial Public Class ViewTabletSticker
    Inherits XPLiteObject

    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

    Dim fId As String
    <Key(True)>
    Public Property Id() As String
        Get
            Return fId
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Id", fId, value)
        End Set
    End Property

    Dim fRequestPackageDetailStatusId As Integer
    Public Property RequestPackageDetailStatusId() As Integer
        Get
            Return fRequestPackageDetailStatusId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("RequestPackageDetailStatusId", fRequestPackageDetailStatusId, value)
        End Set
    End Property

    Dim fCampaignDetailId As Integer
    Public Property CampaignDetailId() As Integer
        Get
            Return fCampaignDetailId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CampaignDetailId", fCampaignDetailId, value)
        End Set
    End Property

    Dim fProductId As Integer
    Public Property ProductId() As Integer
        Get
            Return fProductId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ProductId", fProductId, value)
        End Set
    End Property

    Dim fProductCode As String
    Public Property ProductCode() As String
        Get
            Return fProductCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ProductCode", fProductCode, value)
        End Set
    End Property

    Dim fProductName As String
    Public Property ProductName() As String
        Get
            Return fProductName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ProductName", fProductName, value)
        End Set
    End Property

    Dim fProductAbbreviation As String
    Public Property ProductAbbreviation() As String
        Get
            Return fProductAbbreviation
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ProductAbbreviation", fProductAbbreviation, value)
        End Set
    End Property

    Dim fDosis As String
    Public Property Dosis() As String
        Get
            Return fDosis
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Dosis", fDosis, value)
        End Set
    End Property

    Dim fAdministrationRoute As String
    Public Property AdministrationRoute() As String
        Get
            Return fAdministrationRoute
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AdministrationRoute", fAdministrationRoute, value)
        End Set
    End Property

    Dim fPharmaceuticalForm As String
    Public Property PharmaceuticalForm() As String
        Get
            Return fPharmaceuticalForm
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PharmaceuticalForm", fPharmaceuticalForm, value)
        End Set
    End Property

    Dim fInternalBatchCode As String
    Public Property InternalBatchCode() As String
        Get
            Return fInternalBatchCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("InternalBatchCode", fInternalBatchCode, value)
        End Set
    End Property

    Dim fProductBatchCode As String
    Public Property ProductBatchCode() As String
        Get
            Return fProductBatchCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ProductBatchCode", fProductBatchCode, value)
        End Set
    End Property

    Dim fCampaignDate As Date?
    Public Property CampaignDate() As Date?
        Get
            Return fCampaignDate
        End Get
        Set(ByVal value As Date?)
            SetPropertyValue(Of Date?)("CampaignDate", fCampaignDate, value)
        End Set
    End Property

    Dim fExpirationDate As Date?
    Public Property ExpirationDate() As Date?
        Get
            Return fExpirationDate
        End Get
        Set(ByVal value As Date?)
            SetPropertyValue(Of Date?)("ExpirationDate", fExpirationDate, value)
        End Set
    End Property

    Dim fHealthRegistration As String
    Public Property HealthRegistration() As String
        Get
            Return fHealthRegistration
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("HealthRegistration", fHealthRegistration, value)
        End Set
    End Property

    Dim fUnitDoseTypeClass As Integer
    Public Property UnitDoseTypeClass() As Integer
        Get
            Return fUnitDoseTypeClass
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("UnitDoseTypeClass", fUnitDoseTypeClass, value)
        End Set
    End Property

    Dim fCreatedBy As String
    Public Property CreatedBy() As String
        Get
            Return fCreatedBy
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CreatedBy", fCreatedBy, value)
        End Set
    End Property

    Dim fVerifiedBy As String
    Public Property VerifiedBy() As String
        Get
            Return fVerifiedBy
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("VerifiedBy", fVerifiedBy, value)
        End Set
    End Property

#Region "Builders"
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

    Public Sub New()
        MyBase.New(Session.DefaultSession)
    End Sub
#End Region

End Class