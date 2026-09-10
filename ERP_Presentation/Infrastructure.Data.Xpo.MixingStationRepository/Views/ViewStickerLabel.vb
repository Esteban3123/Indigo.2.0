'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.MixingStation
' Author           : Diego A. Roldán
' Created          : 2022-07-22
'
' Copyright        : (c) . All rights reserved.
'*************************************************************
Imports DevExpress.Xpo

<Persistent("MixingStation.ViewStickerLabel")>
Partial Public Class ViewStickerLabel
    Inherits XPLiteObject

    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

    Dim fId As Integer
    <Key(True)>
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
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

    Dim fCampaignNumber As Integer
    Public Property CampaignNumber() As Integer
        Get
            Return fCampaignNumber
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CampaignNumber", fCampaignNumber, value)
        End Set
    End Property

    Dim fPackageId As Integer
    Public Property PackageId() As Integer
        Get
            Return fPackageId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("PackageId", fPackageId, value)
        End Set
    End Property

    Dim fPackageName As String
    Public Property PackageName() As String
        Get
            Return fPackageName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PackageName", fPackageName, value)
        End Set
    End Property

    Dim fPackageDescription As String
    Public Property PackageDescription() As String
        Get
            Return fPackageDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PackageDescription", fPackageDescription, value)
        End Set
    End Property

    Dim fMain As String
    Public Property Main() As String
        Get
            Return fMain
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Main", fMain, value)
        End Set
    End Property

    Dim fVehicle As String
    Public Property Vehicle() As String
        Get
            Return fVehicle
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Vehicle", fVehicle, value)
        End Set
    End Property

    Dim fThinner As String
    Public Property Thinner() As String
        Get
            Return fThinner
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Thinner", fThinner, value)
        End Set
    End Property

    Dim fBatchCode As String
    Public Property BatchCode() As String
        Get
            Return fBatchCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("BatchCode", fBatchCode, value)
        End Set
    End Property

    Dim fVolumeTotalOrder As Decimal
    Public Property VolumeTotalOrder() As Decimal
        Get
            Return fVolumeTotalOrder
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("VolumeTotalOrder", fVolumeTotalOrder, value)
        End Set
    End Property

    Dim fAbbreviationUnitTotalOrder As String
    Public Property AbbreviationUnitTotalOrder() As String
        Get
            Return fAbbreviationUnitTotalOrder
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AbbreviationUnitTotalOrder", fAbbreviationUnitTotalOrder, value)
        End Set
    End Property

    Dim fPatientName As String
    Public Property PatientName() As String
        Get
            Return fPatientName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PatientName", fPatientName, value)
        End Set
    End Property

    Dim fPatientCode As String
    Public Property PatientCode() As String
        Get
            Return fPatientCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PatientCode", fPatientCode, value)
        End Set
    End Property

    Dim fFunctionalUnitName As String
    Public Property FunctionalUnitName() As String
        Get
            Return fFunctionalUnitName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("FunctionalUnitName", fFunctionalUnitName, value)
        End Set
    End Property

    Dim fBed As String
    Public Property Bed() As String
        Get
            Return fBed
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Bed", fBed, value)
        End Set
    End Property
    Dim fAdministrationName As String
    Public Property AdministrationName() As String
        Get
            Return fAdministrationName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AdministrationName", fAdministrationName, value)
        End Set
    End Property
    Dim fExpirationDate As Date
    Public Property ExpirationDate() As Date
        Get
            Return fExpirationDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue(Of Date)("ExpirationDate", fExpirationDate, value)
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
    Dim fDosisAbbreviation As String
    Public Property DosisAbbreviation() As String
        Get
            Return fDosisAbbreviation
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("DosisAbbreviation", fDosisAbbreviation, value)
        End Set
    End Property
    Dim fConcentration As String
    Public Property Concentration() As String
        Get
            Return fConcentration
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Concentration", fConcentration, value)
        End Set
    End Property
    Dim fQualityChemist As String
    Public Property QualityChemist() As String
        Get
            Return fQualityChemist
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("QualityChemist", fQualityChemist, value)
        End Set
    End Property
    Dim fProductionChemist As String
    Public Property ProductionChemist() As String
        Get
            Return fProductionChemist
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ProductionChemist", fProductionChemist, value)
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