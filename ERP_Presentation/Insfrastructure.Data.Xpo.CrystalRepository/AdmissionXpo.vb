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
Imports Infrastructure.CrossCutting.Resources

#End Region

''' <summary>
''' Encapsula los datos de un ingreso de paciente
''' </summary>
<Persistent("dbo.ADINGRESO")>
Public Class AdmissionXpo
    Inherits XPLiteObject

#Region "Members"

    Dim fNUMINGRES As String
    <Key()>
    <Size(10)>
    Public Property NUMINGRES() As String
        Get
            Return fNUMINGRES
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NUMINGRES", fNUMINGRES, value)
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

    Dim fCODENTIDA As EntityXpo
    <Size(9)>
    <Association("ADINGRESOReferencesINENTIDAD")>
    Public Property CODENTIDA() As EntityXpo
        Get
            Return fCODENTIDA
        End Get
        Set(ByVal value As EntityXpo)
            SetPropertyValue(Of EntityXpo)("CODENTIDA", fCODENTIDA, value)
        End Set
    End Property

    Dim fTIPOINGRE As Integer
    Public Property TIPOINGRE() As Integer
        Get
            Return fTIPOINGRE
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("TIPOINGRE", fTIPOINGRE, value)
        End Set
    End Property

    Dim fIINGREPOR As Integer
    Public Property IINGREPOR() As Integer
        Get
            Return fIINGREPOR
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IINGREPOR", fIINGREPOR, value)
        End Set
    End Property

    Dim fITIPORIES As Integer
    Public Property ITIPORIES() As Integer
        Get
            Return fITIPORIES
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ITIPORIES", fITIPORIES, value)
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

    Dim fCODCONTRA As String
    <Size(6)>
    Public Property CODCONTRA() As String
        Get
            Return fCODCONTRA
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CODCONTRA", fCODCONTRA, value)
        End Set
    End Property

    Dim fCODPANATE As String
    <Size(2)>
    Public Property CODPANATE() As String
        Get
            Return fCODPANATE
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CODPANATE", fCODPANATE, value)
        End Set
    End Property

    Dim fIFECHAING As DateTime
    Public Property IFECHAING() As DateTime
        Get
            Return fIFECHAING
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("IFECHAING", fIFECHAING, value)
        End Set
    End Property

    Dim fILIQUIDAC As Integer
    Public Property ILIQUIDAC() As Integer
        Get
            Return fILIQUIDAC
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ILIQUIDAC", fILIQUIDAC, value)
        End Set
    End Property

    Dim fICONTROLI As String
    <Size(15)>
    Public Property ICONTROLI() As String
        Get
            Return fICONTROLI
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ICONTROLI", fICONTROLI, value)
        End Set
    End Property

    Dim fCODCENATE As String
    <Size(10)>
    Public Property CODCENATE() As String
        Get
            Return fCODCENATE
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CODCENATE", fCODCENATE, value)
        End Set
    End Property

    Dim fUFUCODIGO As String
    <Size(10)>
    Public Property UFUCODIGO() As String
        Get
            Return fUFUCODIGO
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("UFUCODIGO", fUFUCODIGO, value)
        End Set
    End Property

    Dim fIAUTORIZA As String
    <Size(15)>
    Public Property IAUTORIZA() As String
        Get
            Return fIAUTORIZA
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("IAUTORIZA", fIAUTORIZA, value)
        End Set
    End Property

    Dim fIESTADOIN As Char
    Public Property IESTADOIN() As Char
        Get
            Return fIESTADOIN
        End Get
        Set(ByVal value As Char)
            SetPropertyValue(Of Char)("IESTADOIN", fIESTADOIN, value)
        End Set
    End Property

    Dim fIINGRESOA As String
    <Size(10)>
    Public Property IINGRESOA() As String
        Get
            Return fIINGRESOA
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("IINGRESOA", fIINGRESOA, value)
        End Set
    End Property

    Dim fISOATVALO As Decimal
    Public Property ISOATVALO() As Decimal
        Get
            Return fISOATVALO
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ISOATVALO", fISOATVALO, value)
        End Set
    End Property

    Dim fISALCODIG As String
    <Size(3)>
    Public Property ISALCODIG() As String
        Get
            Return fISALCODIG
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ISALCODIG", fISALCODIG, value)
        End Set
    End Property

    Dim fINUMERORE As String
    <Size(8)>
    Public Property INUMERORE() As String
        Get
            Return fINUMERORE
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("INUMERORE", fINUMERORE, value)
        End Set
    End Property

    Dim fIFECHAREM As DateTime
    Public Property IFECHAREM() As DateTime
        Get
            Return fIFECHAREM
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("IFECHAREM", fIFECHAREM, value)
        End Set
    End Property

    Dim fIAUTORREM As String
    <Size(15)>
    Public Property IAUTORREM() As String
        Get
            Return fIAUTORREM
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("IAUTORREM", fIAUTORREM, value)
        End Set
    End Property

    Dim fDEPMUNCOD As String
    <Size(5)>
    Public Property DEPMUNCOD() As String
        Get
            Return fDEPMUNCOD
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("DEPMUNCOD", fDEPMUNCOD, value)
        End Set
    End Property

    Dim fAIPSREMIS As String
    Public Property AIPSREMIS() As String
        Get
            Return fAIPSREMIS
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AIPSREMIS", fAIPSREMIS, value)
        End Set
    End Property

    Dim fIOBSERVAC As String
    <Size(2000)>
    Public Property IOBSERVAC() As String
        Get
            Return fIOBSERVAC
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("IOBSERVAC", fIOBSERVAC, value)
        End Set
    End Property

    Dim fIJUSTIFIC As String
    <Size(254)>
    Public Property IJUSTIFIC() As String
        Get
            Return fIJUSTIFIC
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("IJUSTIFIC", fIJUSTIFIC, value)
        End Set
    End Property

    Dim fIREINGRES As Integer
    Public Property IREINGRES() As Integer
        Get
            Return fIREINGRES
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IREINGRES", fIREINGRES, value)
        End Set
    End Property

    Dim fUFUINGMED As String
    <Size(10)>
    Public Property UFUINGMED() As String
        Get
            Return fUFUINGMED
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("UFUINGMED", fUFUINGMED, value)
        End Set
    End Property

    Dim fCODPROING As String
    <Size(20)>
    Public Property CODPROING() As String
        Get
            Return fCODPROING
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CODPROING", fCODPROING, value)
        End Set
    End Property

    Dim fUFUEGRMED As String
    <Size(10)>
    Public Property UFUEGRMED() As String
        Get
            Return fUFUEGRMED
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("UFUEGRMED", fUFUEGRMED, value)
        End Set
    End Property

    Dim fCODPROEGR As String
    <Size(20)>
    Public Property CODPROEGR() As String
        Get
            Return fCODPROEGR
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CODPROEGR", fCODPROEGR, value)
        End Set
    End Property

    Dim fUFUINGHOS As String
    <Size(10)>
    Public Property UFUINGHOS() As String
        Get
            Return fUFUINGHOS
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("UFUINGHOS", fUFUINGHOS, value)
        End Set
    End Property

    Dim fUFUEGRHOS As String
    <Size(10)>
    Public Property UFUEGRHOS() As String
        Get
            Return fUFUEGRHOS
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("UFUEGRHOS", fUFUEGRHOS, value)
        End Set
    End Property

    Dim fCODESPTRA As String
    <Size(3)>
    Public Property CODESPTRA() As String
        Get
            Return fCODESPTRA
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CODESPTRA", fCODESPTRA, value)
        End Set
    End Property

    Dim fTIPOPROFE As String
    <Size(2)>
    Public Property TIPOPROFE() As String
        Get
            Return fTIPOPROFE
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("TIPOPROFE", fTIPOPROFE, value)
        End Set
    End Property

    Dim fUFUAACTMED As String
    <Size(10)>
    Public Property UFUAACTMED() As String
        Get
            Return fUFUAACTMED
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("UFUAACTMED", fUFUAACTMED, value)
        End Set
    End Property

    Dim fUFUAACTHOS As String
    <Size(10)>
    Public Property UFUAACTHOS() As String
        Get
            Return fUFUAACTHOS
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("UFUAACTHOS", fUFUAACTHOS, value)
        End Set
    End Property

    Dim fCODCAMACT As Integer
    Public Property CODCAMACT() As Integer
        Get
            Return fCODCAMACT
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CODCAMACT", fCODCAMACT, value)
        End Set
    End Property

    Dim fCODDIAING As String
    <Size(4)>
    Public Property CODDIAING() As String
        Get
            Return fCODDIAING
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CODDIAING", fCODDIAING, value)
        End Set
    End Property

    Dim fCODDIAEGR As String
    <Size(4)>
    Public Property CODDIAEGR() As String
        Get
            Return fCODDIAEGR
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CODDIAEGR", fCODDIAEGR, value)
        End Set
    End Property

    Dim fUFUACTPAC As String
    <Size(10)>
    Public Property UFUACTPAC() As String
        Get
            Return fUFUACTPAC
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("UFUACTPAC", fUFUACTPAC, value)
        End Set
    End Property

    Dim fCODUSUCRE As String
    <Size(20)>
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
    <Size(20)>
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

    Dim fCODUSUANU As String
    <Size(20)>
    Public Property CODUSUANU() As String
        Get
            Return fCODUSUANU
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CODUSUANU", fCODUSUANU, value)
        End Set
    End Property

    Dim fFECREGANU As DateTime
    Public Property FECREGANU() As DateTime
        Get
            Return fFECREGANU
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("FECREGANU", fFECREGANU, value)
        End Set
    End Property

    Dim fNUMINGREI As String
    <Size(10)>
    Public Property NUMINGREI() As String
        Get
            Return fNUMINGREI
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NUMINGREI", fNUMINGREI, value)
        End Set
    End Property

    Dim fCODICAMHO As String
    <Size(10)>
    Public Property CODICAMHO() As String
        Get
            Return fCODICAMHO
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CODICAMHO", fCODICAMHO, value)
        End Set
    End Property

    Dim fFECHOSPIT As DateTime
    Public Property FECHOSPIT() As DateTime
        Get
            Return fFECHOSPIT
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("FECHOSPIT", fFECHOSPIT, value)
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

    Dim fIPRNOMBRE As String
    <Size(80)>
    Public Property IPRNOMBRE() As String
        Get
            Return fIPRNOMBRE
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("IPRNOMBRE", fIPRNOMBRE, value)
        End Set
    End Property

    Dim fIPCODACTR As String
    <Size(15)>
    Public Property IPCODACTR() As String
        Get
            Return fIPCODACTR
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("IPCODACTR", fIPCODACTR, value)
        End Set
    End Property

    Dim fIPEXPEDIC As String
    <Size(40)>
    Public Property IPEXPEDIC() As String
        Get
            Return fIPEXPEDIC
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("IPEXPEDIC", fIPEXPEDIC, value)
        End Set
    End Property

    Dim fFECACTRAN As DateTime
    Public Property FECACTRAN() As DateTime
        Get
            Return fFECACTRAN
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("FECACTRAN", fFECACTRAN, value)
        End Set
    End Property

    Dim fHORACIDEN As String
    <Size(5)>
    Public Property HORACIDEN() As String
        Get
            Return fHORACIDEN
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("HORACIDEN", fHORACIDEN, value)
        End Set
    End Property

    Dim fIPTELEFON As String
    <Size(15)>
    Public Property IPTELEFON() As String
        Get
            Return fIPTELEFON
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("IPTELEFON", fIPTELEFON, value)
        End Set
    End Property

    Dim fOBSERACIT As String
    <Size(250)>
    Public Property OBSERACIT() As String
        Get
            Return fOBSERACIT
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("OBSERACIT", fOBSERACIT, value)
        End Set
    End Property

    Dim fOBSERAREM As String
    <Size(250)>
    Public Property OBSERAREM() As String
        Get
            Return fOBSERAREM
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("OBSERAREM", fOBSERAREM, value)
        End Set
    End Property

    Dim fINGRECEXT As Boolean
    Public Property INGRECEXT() As Boolean
        Get
            Return fINGRECEXT
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("INGRECEXT", fINGRECEXT, value)
        End Set
    End Property

    Dim fPACATENDI As Boolean
    Public Property PACATENDI() As Boolean
        Get
            Return fPACATENDI
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("PACATENDI", fPACATENDI, value)
        End Set
    End Property

    Dim fESCADOWNT As Char
    Public Property ESCADOWNT() As Char
        Get
            Return fESCADOWNT
        End Get
        Set(ByVal value As Char)
            SetPropertyValue(Of Char)("ESCADOWNT", fESCADOWNT, value)
        End Set
    End Property

    Dim fESCABIERI As Char
    Public Property ESCABIERI() As Char
        Get
            Return fESCABIERI
        End Get
        Set(ByVal value As Char)
            SetPropertyValue(Of Char)("ESCABIERI", fESCABIERI, value)
        End Set
    End Property

    Dim fESCARASS As Char
    Public Property ESCARASS() As Char
        Get
            Return fESCARASS
        End Get
        Set(ByVal value As Char)
            SetPropertyValue(Of Char)("ESCARASS", fESCARASS, value)
        End Set
    End Property

    Dim fESCNORPAC As Char
    Public Property ESCNORPAC() As Char
        Get
            Return fESCNORPAC
        End Get
        Set(ByVal value As Char)
            SetPropertyValue(Of Char)("ESCNORPAC", fESCNORPAC, value)
        End Set
    End Property

    Dim fSERSUSCEP As Boolean
    Public Property SERSUSCEP() As Boolean
        Get
            Return fSERSUSCEP
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("SERSUSCEP", fSERSUSCEP, value)
        End Set
    End Property

    Dim fESCVASPAC As Char
    Public Property ESCVASPAC() As Char
        Get
            Return fESCVASPAC
        End Get
        Set(ByVal value As Char)
            SetPropertyValue(Of Char)("ESCVASPAC", fESCVASPAC, value)
        End Set
    End Property

    Dim fESCAPAPAC As Char
    Public Property ESCAPAPAC() As Char
        Get
            Return fESCAPAPAC
        End Get
        Set(ByVal value As Char)
            SetPropertyValue(Of Char)("ESCAPAPAC", fESCAPAPAC, value)
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

    Dim fGENULTLIQUI As DateTime
    Public Property GENULTLIQUI() As DateTime
        Get
            Return fGENULTLIQUI
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("GENULTLIQUI", fGENULTLIQUI, value)
        End Set
    End Property

    Dim fVIVESOLO As Boolean
    Public Property VIVESOLO() As Boolean
        Get
            Return fVIVESOLO
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("VIVESOLO", fVIVESOLO, value)
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

    Dim fTRATAESPECIA As Integer
    Public Property TRATAESPECIA() As Integer
        Get
            Return fTRATAESPECIA
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("TRATAESPECIA", fTRATAESPECIA, value)
        End Set
    End Property

    Dim fIdAdmissionType As Integer?
    Public Property IdAdmissionType() As Integer?
        Get
            Return fIdAdmissionType
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("IdAdmissionType", fIdAdmissionType, value)
        End Set
    End Property
#End Region

#Region "Builders"

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

#End Region

#Region "Methods"

#End Region

End Class