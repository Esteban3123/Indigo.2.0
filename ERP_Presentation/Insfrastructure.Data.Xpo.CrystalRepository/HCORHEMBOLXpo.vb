Imports DevExpress.Xpo
<Persistent("dbo.HCORHEMBOL")>
Public Class HCORHEMBOLXpo
    Inherits XPLiteObject
    Dim fID As Integer
    <Key()>
    Public Property ID() As Integer
        Get
            Return fID
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ID", fID, value)
        End Set
    End Property

    <PersistentAlias("HCORHEMCO.ID")>
    Public ReadOnly Property HCORHEMCOID() As Integer
        Get
            Return Convert.ToInt32(EvaluateAlias("HCORHEMCOID"))
        End Get
    End Property


    Dim fHCORHEMCO As HCORHEMCOXpo
    <Persistent("HCORHEMCOID")>
    <Association("ReferenceHCORHEMBOLXpo")>
    Public Property HCORHEMCO() As HCORHEMCOXpo
        Get
            Return fHCORHEMCO
        End Get
        Set(ByVal value As HCORHEMCOXpo)
            SetPropertyValue("HCORHEMCO", fHCORHEMCO, value)
        End Set
    End Property

    <PersistentAlias("HCCOMSAN.ID")>
    Public ReadOnly Property COMSAMID() As Integer
        Get
            Return Convert.ToInt32(EvaluateAlias("COMSAMID"))
        End Get
    End Property

    Dim fHCCOMSAN As HCCOMSANXpo
    <Persistent("COMSAMID")>
    <Association("HCCOMSANReferencesHCORHEMBOLXpo")>
    Public Property HCCOMSAN() As HCCOMSANXpo
        Get
            Return fHCCOMSAN
        End Get
        Set(ByVal value As HCCOMSANXpo)
            SetPropertyValue("HCCOMSAN", fHCCOMSAN, value)
        End Set
    End Property

    Dim fFECENTREGA As DateTime?
    Public Property FECENTREGA() As DateTime?
        Get
            Return fFECENTREGA
        End Get
        Set(ByVal value As DateTime?)
            SetPropertyValue(Of DateTime?)("FECENTREGA", fFECENTREGA, value)
        End Set
    End Property

    Dim fNUMBOLSA As String
    <Size(20)>
    Public Property NUMBOLSA() As String
        Get
            Return fNUMBOLSA
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NUMBOLSA", fNUMBOLSA, value)
        End Set
    End Property


    Dim fTIPOSOLICI As Integer
    <Size(20)>
    Public Property TIPOSOLICI() As Integer
        Get
            Return fTIPOSOLICI
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("TIPOSOLICI", fTIPOSOLICI, value)
        End Set
    End Property

    Dim fSELLOCALIDAD As String
    <Size(20)>
    Public Property SELLOCALIDAD() As String
        Get
            Return fSELLOCALIDAD
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("SELLOCALIDAD", fSELLOCALIDAD, value)
        End Set
    End Property


    Dim fFECHAEXPIRA As DateTime?
    Public Property FECHAEXPIRA() As DateTime?
        Get
            Return fFECHAEXPIRA
        End Get
        Set(ByVal value As DateTime?)
            SetPropertyValue(Of DateTime?)("FECHAEXPIRA", fFECHAEXPIRA, value)
        End Set
    End Property

    Dim fREAPRUECRU As Boolean?
    Public Property REAPRUECRU() As Boolean?
        Get
            Return fREAPRUECRU
        End Get
        Set(ByVal value As Boolean?)
            SetPropertyValue(Of Boolean?)("REAPRUECRU", fREAPRUECRU, value)
        End Set
    End Property


    Dim fBACRESENTR As String
    <Size(20)> _
    Public Property BACRESENTR() As String
        Get
            Return fBACRESENTR
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("BACRESENTR", fBACRESENTR, value)
        End Set
    End Property

    Dim fPROFSOLTRA As String
    <Size(20)>
    Public Property PROFSOLTRA() As String
        Get
            Return fPROFSOLTRA
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PROFSOLTRA", fPROFSOLTRA, value)
        End Set
    End Property

    <PersistentAlias("INPROFSAL.CODPROSAL")>
    Public ReadOnly Property PROFSOLRES() As String
        Get
            Return Convert.ToInt32(EvaluateAlias("PROFSOLRES"))
        End Get
    End Property

    Dim fINPROFSAL As HealthCareProfessionalXpo
    <Persistent("PROFSOLRES")>
    <Association("INPROFSALReferencesHCORHEMBOLXpo")>
    Public Property INPROFSAL() As HealthCareProfessionalXpo
        Get
            Return fINPROFSAL
        End Get
        Set(ByVal value As HealthCareProfessionalXpo)
            SetPropertyValue("INPROFSAL", fINPROFSAL, value)
        End Set
    End Property

    Dim fPROFAPLICA As String
    <Size(20)> _
    Public Property PROFAPLICA() As String
        Get
            Return fPROFAPLICA
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PROFAPLICA", fPROFAPLICA, value)
        End Set
    End Property

    Dim fENFAPLICA As String
    <Size(20)> _
    Public Property ENFAPLICA() As String
        Get
            Return fENFAPLICA
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ENFAPLICA", fENFAPLICA, value)
        End Set
    End Property

    Dim fFECAPLICMED As DateTime?
    Public Property FECAPLICMED() As DateTime?
        Get
            Return fFECAPLICMED
        End Get
        Set(ByVal value As DateTime?)
            SetPropertyValue(Of DateTime?)("FECAPLICMED", fFECAPLICMED, value)
        End Set
    End Property

     Dim fFECAPLICENF As DateTime?
    Public Property FECAPLICENF() As DateTime?
        Get
            Return fFECAPLICENF
        End Get
        Set(ByVal value As DateTime?)
            SetPropertyValue(Of DateTime?)("FECAPLICENF", fFECAPLICENF, value)
        End Set
    End Property

    Dim fFECHINITRA As DateTime?
    Public Property FECHINITRA() As DateTime?
        Get
            Return fFECHINITRA
        End Get
        Set(ByVal value As DateTime?)
            SetPropertyValue(Of DateTime?)("FECHINITRA", fFECHINITRA, value)
        End Set
    End Property

    Dim fFECHFINTRAF As DateTime?
    Public Property FECHFINTRA() As DateTime?
        Get
            Return fFECHFINTRAF
        End Get
        Set(ByVal value As DateTime?)
            SetPropertyValue(Of DateTime?)("FECHFINTRA", fFECHFINTRAF, value)
        End Set
    End Property

    Dim fVolumeTransfuse As Decimal
    Public Property VolumeTransfuse() As Decimal
        Get
            Return fVolumeTransfuse
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("VolumeTransfuse", fVolumeTransfuse, value)
        End Set
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
End Class
