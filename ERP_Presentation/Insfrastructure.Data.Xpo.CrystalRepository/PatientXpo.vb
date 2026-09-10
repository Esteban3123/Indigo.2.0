'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.CrystalRepository
' Author           : Juan F. Tamayo
' Created          : 2014-11-04
'
' Copyright        : (c) . All rights reserved.
'*************************************************************

#Region "Imports"

Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

#End Region

''' <summary>
''' Encapsula los datos de un paciente
''' </summary>
<Persistent("dbo.INPACIENT")>
Public Class PatientXpo
    Inherits XPLiteObject

#Region "Members"

    Dim fIPCODPACI As String
    <Key()>
    Public Property IPCODPACI() As String
        Get
            Return fIPCODPACI
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("IPCODPACI", fIPCODPACI, value)
        End Set
    End Property

    Dim fIPTIPODOC As Integer
    Public Property IPTIPODOC() As Integer
        Get
            Return fIPTIPODOC
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IPTIPODOC", fIPTIPODOC, value)
        End Set
    End Property

    Dim fCODIGONIT As String
    Public Property CODIGONIT() As String
        Get
            Return fCODIGONIT
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CODIGONIT", fCODIGONIT, value)
        End Set
    End Property

    Dim fIPEXPEDIC As String
    Public Property IPEXPEDIC() As String
        Get
            Return fIPEXPEDIC
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("IPEXPEDIC", fIPEXPEDIC, value)
        End Set
    End Property

    Dim fIPPRIAPEL As String
    Public Property IPPRIAPEL() As String
        Get
            Return fIPPRIAPEL
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("IPPRIAPEL", fIPPRIAPEL, value)
        End Set
    End Property

    Dim fIPSEGAPEL As String
    Public Property IPSEGAPEL() As String
        Get
            Return fIPSEGAPEL
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("IPSEGAPEL", fIPSEGAPEL, value)
        End Set
    End Property

    Dim fIPPRINOMB As String
    Public Property IPPRINOMB() As String
        Get
            Return fIPPRINOMB
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("IPPRINOMB", fIPPRINOMB, value)
        End Set
    End Property

    Dim fIPSEGNOMB As String
    Public Property IPSEGNOMB() As String
        Get
            Return fIPSEGNOMB
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("IPSEGNOMB", fIPSEGNOMB, value)
        End Set
    End Property

    Dim fIPNOMCOMP As String
    Public Property IPNOMCOMP() As String
        Get
            Return fIPNOMCOMP
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("IPNOMCOMP", fIPNOMCOMP, value)
        End Set
    End Property

    Dim fCODEMPRES As String
    Public Property CODEMPRES() As String
        Get
            Return fCODEMPRES
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CODEMPRES", fCODEMPRES, value)
        End Set
    End Property

    Dim fIPTIPOPAC As Integer
    Public Property IPTIPOPAC() As Integer
        Get
            Return fIPTIPOPAC
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IPTIPOPAC", fIPTIPOPAC, value)
        End Set
    End Property

    Dim fIPTIPOAFI As Integer
    Public Property IPTIPOAFI() As Integer
        Get
            Return fIPTIPOAFI
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IPTIPOAFI", fIPTIPOAFI, value)
        End Set
    End Property

    Dim fCAPACIPAG As Integer
    Public Property CAPACIPAG() As Integer
        Get
            Return fCAPACIPAG
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CAPACIPAG", fCAPACIPAG, value)
        End Set
    End Property

    Dim fCODENTIDA As String
    Public Property CODENTIDA() As String
        Get
            Return fCODENTIDA
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CODENTIDA", fCODENTIDA, value)
        End Set
    End Property

    Dim fCCCONTRAT As String
    Public Property CCCONTRAT() As String
        Get
            Return fCCCONTRAT
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CCCONTRAT", fCCCONTRAT, value)
        End Set
    End Property

    Dim fCPPLANBEN As String
    Public Property CPPLANBEN() As String
        Get
            Return fCPPLANBEN
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CPPLANBEN", fCPPLANBEN, value)
        End Set
    End Property

    Dim fAUUBICACI As String
    Public Property AUUBICACI() As String
        Get
            Return fAUUBICACI
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AUUBICACI", fAUUBICACI, value)
        End Set
    End Property

    Dim fNIVCODIGO As String
    Public Property NIVCODIGO() As String
        Get
            Return fNIVCODIGO
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NIVCODIGO", fNIVCODIGO, value)
        End Set
    End Property

    Dim fIPDIRECCI As String
    <Size(SizeAttribute.Unlimited)>
    Public Property IPDIRECCI() As String
        Get
            Return fIPDIRECCI
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("IPDIRECCI", fIPDIRECCI, value)
        End Set
    End Property

    Dim fIPTELEFON As String
    <Size(SizeAttribute.Unlimited)>
    Public Property IPTELEFON() As String
        Get
            Return fIPTELEFON
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("IPTELEFON", fIPTELEFON, value)
        End Set
    End Property

    Dim fIPTELMOVI As String
    <Size(SizeAttribute.Unlimited)>
    Public Property IPTELMOVI() As String
        Get
            Return fIPTELMOVI
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("IPTELMOVI", fIPTELMOVI, value)
        End Set
    End Property

    Dim fIPFECNACI As DateTime
    Public Property IPFECNACI() As DateTime
        Get
            Return fIPFECNACI
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("IPFECNACI", fIPFECNACI, value)
        End Set
    End Property

    Dim fCODACTIVI As String
    Public Property CODACTIVI() As String
        Get
            Return fCODACTIVI
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CODACTIVI", fCODACTIVI, value)
        End Set
    End Property

    Dim fIPSEXOPAC As Integer
    Public Property IPSEXOPAC() As Integer
        Get
            Return fIPSEXOPAC
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IPSEXOPAC", fIPSEXOPAC, value)
        End Set
    End Property

    Dim fIPESTADOC As Integer
    Public Property IPESTADOC() As Integer
        Get
            Return fIPESTADOC
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IPESTADOC", fIPESTADOC, value)
        End Set
    End Property

    Dim fIPGRUPSAN As String
    Public Property IPGRUPSAN() As String
        Get
            Return fIPGRUPSAN
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("IPGRUPSAN", fIPGRUPSAN, value)
        End Set
    End Property

    Dim fIPRHSANGR As Char
    Public Property IPRHSANGR() As Char
        Get
            Return fIPRHSANGR
        End Get
        Set(ByVal value As Char)
            SetPropertyValue(Of Char)("IPRHSANGR", fIPRHSANGR, value)
        End Set
    End Property

    Dim fTIPCOBSAL As Char
    Public Property TIPCOBSAL() As Char
        Get
            Return fTIPCOBSAL
        End Get
        Set(ByVal value As Char)
            SetPropertyValue(Of Char)("TIPCOBSAL", fTIPCOBSAL, value)
        End Set
    End Property

    Dim fCORELEPAC As String
    Public Property CORELEPAC() As String
        Get
            Return fCORELEPAC
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CORELEPAC", fCORELEPAC, value)
        End Set
    End Property

    Dim fCODGRUPOE As String
    Public Property CODGRUPOE() As String
        Get
            Return fCODGRUPOE
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CODGRUPOE", fCODGRUPOE, value)
        End Set
    End Property

    Dim fESTADOPAC As Boolean
    Public Property ESTADOPAC() As Boolean
        Get
            Return fESTADOPAC
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("ESTADOPAC", fESTADOPAC, value)
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

    Dim fINDAUDFOR As Decimal
    Public Property INDAUDFOR() As Decimal
        Get
            Return fINDAUDFOR
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("INDAUDFOR", fINDAUDFOR, value)
        End Set
    End Property

    Dim fPACIEFOTO() As Byte
    <Size(SizeAttribute.Unlimited)>
    Public Property PACIEFOTO() As Byte()
        Get
            Return fPACIEFOTO
        End Get
        Set(ByVal value As Byte())
            SetPropertyValue(Of Byte())("PACIEFOTO", fPACIEFOTO, value)
        End Set
    End Property

    Dim fPACIEHUELL() As Byte
    <Size(SizeAttribute.Unlimited)>
    Public Property PACIEHUELL() As Byte()
        Get
            Return fPACIEHUELL
        End Get
        Set(ByVal value As Byte())
            SetPropertyValue(Of Byte())("PACIEHUELL", fPACIEHUELL, value)
        End Set
    End Property

    Dim fNUMCARPET As String
    Public Property NUMCARPET() As String
        Get
            Return fNUMCARPET
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NUMCARPET", fNUMCARPET, value)
        End Set
    End Property

    Dim fCODUSUCRE As String
    Public Property CODUSUCRE() As String
        Get
            Return fCODUSUCRE
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CODUSUCRE", fCODUSUCRE, value)
        End Set
    End Property

    Dim fFECREGCRE As DateTime
    Public Property FECREGCRE() As DateTime
        Get
            Return fFECREGCRE
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("FECREGCRE", fFECREGCRE, value)
        End Set
    End Property

    Dim fCODUSUMOD As String
    Public Property CODUSUMOD() As String
        Get
            Return fCODUSUMOD
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CODUSUMOD", fCODUSUMOD, value)
        End Set
    End Property

    Dim fFECREGMOD As DateTime
    Public Property FECREGMOD() As DateTime
        Get
            Return fFECREGMOD
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("FECREGMOD", fFECREGMOD, value)
        End Set
    End Property

    Dim fIPESTRATO As Integer
    Public Property IPESTRATO() As Integer
        Get
            Return fIPESTRATO
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IPESTRATO", fIPESTRATO, value)
        End Set
    End Property

    Dim fCREDCODIGO As String
    Public Property CREDCODIGO() As String
        Get
            Return fCREDCODIGO
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CREDCODIGO", fCREDCODIGO, value)
        End Set
    End Property

    Dim fDISCCODIGO As String
    Public Property DISCCODIGO() As String
        Get
            Return fDISCCODIGO
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("DISCCODIGO", fDISCCODIGO, value)
        End Set
    End Property

    Dim fIDICODIGO As String
    Public Property IDICODIGO() As String
        Get
            Return fIDICODIGO
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("IDICODIGO", fIDICODIGO, value)
        End Set
    End Property

    Dim fNIVECODIGO As String
    Public Property NIVECODIGO() As String
        Get
            Return fNIVECODIGO
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NIVECODIGO", fNIVECODIGO, value)
        End Set
    End Property

    Dim fGRUPCODIGO As String
    Public Property GRUPCODIGO() As String
        Get
            Return fGRUPCODIGO
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("GRUPCODIGO", fGRUPCODIGO, value)
        End Set
    End Property

    Dim fGENCAREGROUP As Integer
    Public Property GENCAREGROUP() As Integer
        Get
            Return fGENCAREGROUP
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("GENCAREGROUP", fGENCAREGROUP, value)
        End Set
    End Property

    Dim fGENCONENTITY As Integer
    Public Property GENCONENTITY() As Integer
        Get
            Return fGENCONENTITY
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("GENCONENTITY", fGENCONENTITY, value)
        End Set
    End Property

    <PersistentAlias("Concat(Trim(IPCODPACI), ' - ', Trim(IPNOMCOMP))")>
    Public ReadOnly Property CodeFullName As String
        Get
            Return Convert.ToString(EvaluateAlias("CodeFullName"))
        End Get
    End Property

    <PersistentAlias("Trim(IPCODPACI)")>
    Public ReadOnly Property PatientCode As String
        Get
            Return Convert.ToString(EvaluateAlias("PatientCode"))
        End Get
    End Property

    <NonPersistent()>
    Public Property AdmissionCode() As String
#End Region

#Region "Builders"

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

#End Region

End Class