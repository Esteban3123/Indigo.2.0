'************************************************************
' Assembly         : Infraestructure.Data.Xpo.CommonRepository
' Author           : Oscar stiven astudillo
' Created          : 2024-11-18
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"
Imports DevExpress.Xpo
#End Region



<Persistent("dbo.HCINCAPAC")>
Partial Public Class HCINCAPACXpo
    Inherits XPLiteObject

    Dim fCODCONSEC As Decimal
    <Key()>
    Public Property CODCONSEC() As Decimal
        Get
            Return fCODCONSEC
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("CODCONSEC", fCODCONSEC, value)
        End Set
    End Property

    Dim fIPCODPACI As String
    Public Property IPCODPACI() As String
        Get
            Return fIPCODPACI
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("IPCODPACI", fIPCODPACI, value)
        End Set
    End Property

    Dim fCODCENATE As String
    Public Property CODCENATE() As String
        Get
            Return fCODCENATE
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CODCENATE", fCODCENATE, value)
        End Set
    End Property

    Dim fUFUCODIGO As String
    Public Property UFUCODIGO() As String
        Get
            Return fUFUCODIGO
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("UFUCODIGO", fUFUCODIGO, value)
        End Set
    End Property

    Dim fCODPROSAL As String
    Public Property CODPROSAL() As String
        Get
            Return fCODPROSAL
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CODPROSAL", fCODPROSAL, value)
        End Set
    End Property


    Dim fFECINIINC As DateTime
    Public Property FECINIINC() As DateTime
        Get
            Return fFECINIINC
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("FECINIINC", fFECINIINC, value)
        End Set
    End Property

    Dim fFECFININC As DateTime
    Public Property FECFININC() As DateTime
        Get
            Return fFECFININC
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("FECFININC", fFECFININC, value)
        End Set
    End Property

    Dim fNUMDIAINC As Integer
    Public Property NUMDIAINC() As Integer
        Get
            Return fNUMDIAINC
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("NUMDIAINC", fNUMDIAINC, value)
        End Set
    End Property

    Dim fCODDIAGNO As String
    Public Property CODDIAGNO() As String
        Get
            Return fCODDIAGNO
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CODDIAGNO", fCODDIAGNO, value)
        End Set
    End Property

    Dim fICAUSAING As Integer
    Public Property ICAUSAING() As Integer
        Get
            Return fICAUSAING
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ICAUSAING", fICAUSAING, value)
        End Set
    End Property

    Dim fTIPOINCAP As Integer?
    Public Property TIPOINCAP() As Integer?
        Get
            Return fTIPOINCAP
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("TIPOINCAP", fTIPOINCAP, value)
        End Set
    End Property

    Dim fFECREGIST As DateTime
    Public Property FECREGIST() As DateTime
        Get
            Return fFECREGIST
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("FECREGIST", fFECREGIST, value)
        End Set
    End Property

    Dim fFECACCLAB As DateTime?
    Public Property FECACCLAB() As DateTime?
        Get
            Return fFECACCLAB
        End Get
        Set(ByVal value As DateTime?)
            SetPropertyValue(Of DateTime?)("FECACCLAB", fFECACCLAB, value)
        End Set
    End Property

    Dim fESPRORROG As Boolean?
    Public Property ESPRORROG() As Boolean?
        Get
            Return fESPRORROG
        End Get
        Set(ByVal value As Boolean?)
            SetPropertyValue(Of Boolean?)("ESPRORROG", fESPRORROG, value)
        End Set
    End Property

    Dim fCONSECANT As Decimal?
    Public Property CONSECANT() As Decimal?
        Get
            Return fCONSECANT
        End Get
        Set(ByVal value As Decimal?)
            SetPropertyValue(Of Decimal?)("CONSECANT", fCONSECANT, value)
        End Set
    End Property

    Dim fNUMINGRES As String
    Public Property NUMINGRES() As String
        Get
            Return fNUMINGRES
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NUMINGRES", fNUMINGRES, value)
        End Set
    End Property

    Dim fOBSERVACI As String
    Public Property OBSERVACI() As String
        Get
            Return fOBSERVACI
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("OBSERVACI", fOBSERVACI, value)
        End Set
    End Property

    Dim fCODJUSINCA As String
    Public Property CODJUSINCA() As String
        Get
            Return fCODJUSINCA
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CODJUSINCA", fCODJUSINCA, value)
        End Set
    End Property

    Dim fOBSERVAJUSINCA As String
    Public Property OBSERVAJUSINCA() As String
        Get
            Return fOBSERVAJUSINCA
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("OBSERVAJUSINCA", fOBSERVAJUSINCA, value)
        End Set
    End Property

    Dim fNUMEFOLIO As String
    Public Property NUMEFOLIO() As String
        Get
            Return fNUMEFOLIO
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NUMEFOLIO", fNUMEFOLIO, value)
        End Set
    End Property

    Dim fDisabilityClass As Integer?
    Public Property DisabilityClass() As Integer?
        Get
            Return fDisabilityClass
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("DisabilityClass", fDisabilityClass, value)
        End Set
    End Property

    Dim fGroupServices As Integer?
    Public Property GroupServices() As Integer?
        Get
            Return fGroupServices
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("GroupServices", fGroupServices, value)
        End Set
    End Property

    Dim fIdAdmissionModalities As Integer?
    Public Property IdAdmissionModalities() As Integer?
        Get
            Return fIdAdmissionModalities
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("IdAdmissionModalities", fIdAdmissionModalities, value)
        End Set
    End Property

    Dim fRetroactiveDisability As Boolean?
    Public Property RetroactiveDisability() As Boolean?
        Get
            Return fRetroactiveDisability
        End Get
        Set(ByVal value As Boolean?)
            SetPropertyValue(Of Boolean?)("RetroactiveDisability", fRetroactiveDisability, value)
        End Set
    End Property

    Dim fCODESPECI As String
    Public Property CODESPECI() As String
        Get
            Return fCODESPECI
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CODESPECI", fCODESPECI, value)
        End Set
    End Property

    Dim fCauseOfRetroactivity As Integer?
    Public Property CauseOfRetroactivity() As Integer?
        Get
            Return fCauseOfRetroactivity
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("CauseOfRetroactivity", fCauseOfRetroactivity, value)
        End Set
    End Property

    Dim fPresumedOrigin As Integer?
    Public Property PresumedOrigin() As Integer?
        Get
            Return fPresumedOrigin
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("PresumedOrigin", fPresumedOrigin, value)
        End Set
    End Property

    Dim fNameOfParentOrCaregiver As String
    Public Property NameOfParentOrCaregiver() As String
        Get
            Return fNameOfParentOrCaregiver
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NameOfParentOrCaregiver", fNameOfParentOrCaregiver, value)
        End Set
    End Property

    Dim fGestationalAge As Decimal?
    Public Property GestationalAge() As Decimal?
        Get
            Return fGestationalAge
        End Get
        Set(ByVal value As Decimal?)
            SetPropertyValue(Of Decimal?)("GestationalAge", fGestationalAge, value)
        End Set
    End Property

    Dim fMultiplePregnancyGestational As Integer?
    Public Property MultiplePregnancyGestational() As Integer?
        Get
            Return fMultiplePregnancyGestational
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("MultiplePregnancyGestational", fMultiplePregnancyGestational, value)
        End Set
    End Property

    Dim fFlexibleParentingResponses As String
    Public Property FlexibleParentingResponses() As String
        Get
            Return fFlexibleParentingResponses
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("FlexibleParentingResponses", fFlexibleParentingResponses, value)
        End Set
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

End Class
