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
''' Encapsula los datos de un profesional de la salud (Médico)
''' </summary>
<Persistent("dbo.INPROFSAL")>
Public Class HealthCareProfessionalXpo
    Inherits XPLiteObject

#Region "Members"

    'columna que devuelve el nit y el nombre concatenado
    <Size(80)>
    <PersistentAlias("concat(concat(Trim(CODPROSAL),' - '),Trim(NOMMEDICO))")>
    Public ReadOnly Property CodeName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeName"))
        End Get
    End Property

    Dim fCODPROSAL As String
    <Key()>
    Public Property CODPROSAL() As String
        Get
            Return fCODPROSAL
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CODPROSAL", fCODPROSAL, value)
        End Set
    End Property
    <PersistentAlias("Trim(CODPROSAL)")>
    Public ReadOnly Property CODMEDICO() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CODMEDICO"))
        End Get
    End Property
    Dim fCODIGONIT As String
    <Size(15)>
    Public Property CODIGONIT() As String
        Get
            Return fCODIGONIT
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CODIGONIT", fCODIGONIT, value)
        End Set
    End Property
    Dim fNOMMEDICO As String
    <Size(60)>
    Public Property NOMMEDICO() As String
        Get
            Return fNOMMEDICO
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NOMMEDICO", fNOMMEDICO, value)
        End Set
    End Property
    Dim fMEDPRINOM As String
    <Size(15)>
    Public Property MEDPRINOM() As String
        Get
            Return fMEDPRINOM
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("MEDPRINOM", fMEDPRINOM, value)
        End Set
    End Property
    Dim fMEDSEGNOM As String
    <Size(15)>
    Public Property MEDSEGNOM() As String
        Get
            Return fMEDSEGNOM
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("MEDSEGNOM", fMEDSEGNOM, value)
        End Set
    End Property
    Dim fMEDPRIAPEL As String
    <Size(15)>
    Public Property MEDPRIAPEL() As String
        Get
            Return fMEDPRIAPEL
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("MEDPRIAPEL", fMEDPRIAPEL, value)
        End Set
    End Property
    Dim fMEDSEGAPEL As String
    <Size(15)>
    Public Property MEDSEGAPEL() As String
        Get
            Return fMEDSEGAPEL
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("MEDSEGAPEL", fMEDSEGAPEL, value)
        End Set
    End Property
    Dim fCODESPEC1 As SpecialtyXpo
    <Size(3)>
    <Association("INPROFSALReferencesINESPECIA")>
    Public Property CODESPEC1() As SpecialtyXpo
        Get
            Return fCODESPEC1
        End Get
        Set(ByVal value As SpecialtyXpo)
            SetPropertyValue(Of SpecialtyXpo)("CODESPEC1", fCODESPEC1, value)
        End Set
    End Property

    Dim fCODESPEC2 As SpecialtyXpo
    <Size(3)>
    <Association("INPROFSALReferencesINESPECIA1")>
    Public Property CODESPEC2() As SpecialtyXpo
        Get
            Return fCODESPEC2
        End Get
        Set(ByVal value As SpecialtyXpo)
            SetPropertyValue(Of SpecialtyXpo)("CODESPEC2", fCODESPEC2, value)
        End Set
    End Property
    Dim fCODESPEC3 As SpecialtyXpo
    <Size(3)>
    <Association("INPROFSALReferencesINESPECIA2")>
    Public Property CODESPEC3() As SpecialtyXpo
        Get
            Return fCODESPEC3
        End Get
        Set(ByVal value As SpecialtyXpo)
            SetPropertyValue(Of SpecialtyXpo)("CODESPEC3", fCODESPEC3, value)
        End Set
    End Property
    Dim fIMDIRECCI As String
    Public Property IMDIRECCI() As String
        Get
            Return fIMDIRECCI
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("IMDIRECCI", fIMDIRECCI, value)
        End Set
    End Property
    Dim fIMTELEFON As String
    <Size(15)>
    Public Property IMTELEFON() As String
        Get
            Return fIMTELEFON
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("IMTELEFON", fIMTELEFON, value)
        End Set
    End Property
    Dim fIMTELMOVI As String
    <Size(15)>
    Public Property IMTELMOVI() As String
        Get
            Return fIMTELMOVI
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("IMTELMOVI", fIMTELMOVI, value)
        End Set
    End Property
    Dim fTARJETAPR As String
    <Size(15)>
    Public Property TARJETAPR() As String
        Get
            Return fTARJETAPR
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("TARJETAPR", fTARJETAPR, value)
        End Set
    End Property
    Dim fESTADOMED As Integer
    Public Property ESTADOMED() As Integer
        Get
            Return fESTADOMED
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ESTADOMED", fESTADOMED, value)
        End Set
    End Property
    <PersistentAlias("Iif(ESTADOMED = 1, 'Activo', 'Inactivo')")>
    Public ReadOnly Property StatusName As String
        Get
            'If fESTADOMED = 1 Then
            '    Return "Activo"
            'Else
            '    Return "Inactivo"
            'End If
            Return Convert.ToString(Me.EvaluateAlias("StatusName"))
        End Get
    End Property

    Dim fMEDTIPVIN As Integer
    Public Property MEDTIPVIN() As Integer
        Get
            Return fMEDTIPVIN
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("MEDTIPVIN", fMEDTIPVIN, value)
        End Set
    End Property
    Dim fCODUSUARI As String
    <Indexed(Name:="IX_INPROFSAL", Unique:=True)>
    <Size(20)>
    Public Property CODUSUARI() As String
        Get
            Return fCODUSUARI
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CODUSUARI", fCODUSUARI, value)
        End Set
    End Property
    Dim fTIPPROFES As Integer
    Public Property TIPPROFES() As Integer
        Get
            Return fTIPPROFES
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("TIPPROFES", fTIPPROFES, value)
        End Set
    End Property
    Dim fMEDPERCIR As Char
    Public Property MEDPERCIR() As Char
        Get
            Return fMEDPERCIR
        End Get
        Set(ByVal value As Char)
            SetPropertyValue(Of Char)("MEDPERCIR", fMEDPERCIR, value)
        End Set
    End Property
    Dim fREACONEXT As Boolean
    Public Property REACONEXT() As Boolean
        Get
            Return fREACONEXT
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("REACONEXT", fREACONEXT, value)
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
    Dim fGENCONTRA As Integer
    Public Property GENCONTRA() As Integer
        Get
            Return fGENCONTRA
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("GENCONTRA", fGENCONTRA, value)
        End Set
    End Property
    Dim fFECULTLIQ As DateTime
    Public Property FECULTLIQ() As DateTime
        Get
            Return fFECULTLIQ
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("FECULTLIQ", fFECULTLIQ, value)
        End Set
    End Property
    Dim fFECULTLIQTMP As DateTime
    Public Property FECULTLIQTMP() As DateTime
        Get
            Return fFECULTLIQTMP
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("FECULTLIQTMP", fFECULTLIQTMP, value)
        End Set
    End Property
    Dim fGENPROVEE As Integer
    Public Property GENPROVEE() As Integer
        Get
            Return fGENPROVEE
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("GENPROVEE", fGENPROVEE, value)
        End Set
    End Property
    Dim fGENLINDIST As Integer
    Public Property GENLINDIST() As Integer
        Get
            Return fGENLINDIST
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("GENLINDIST", fGENLINDIST, value)
        End Set
    End Property
    <Association("HCQXEQUIPReferencesINPROFSAL", GetType(HCQXEQUIPXpo))>
    Public ReadOnly Property HCQXEQUIPXpo() As XPCollection(Of HCQXEQUIPXpo)
        Get
            Return GetCollection(Of HCQXEQUIPXpo)("HCQXEQUIPXpo")
        End Get
    End Property

    <Association("HCQXINFORReferencesINPROFSAL")>
    Public ReadOnly Property HCQXINFORXpo() As XPCollection(Of HCQXINFORXpo)
        Get
            Return GetCollection(Of HCQXINFORXpo)("HCQXINFORXpo")
        End Get
    End Property

    <Association("AGDISPONSALAXpoReferencesHealthCareProfessionalXpo", GetType(AGDISPONSALAXpo))>
    Public ReadOnly Property AGDISPONSALAXpo() As XPCollection(Of AGDISPONSALAXpo)
        Get
            Return GetCollection(Of AGDISPONSALAXpo)("AGDISPONSALAXpo")
        End Get
    End Property


    <Association("INPROFSALReferencesHCORHEMBOLXpo", GetType(HCORHEMBOLXpo))>
    Public ReadOnly Property HCORHEMBOLXpo() As XPCollection(Of HCORHEMBOLXpo)
        Get
            Return GetCollection(Of HCORHEMBOLXpo)("HCORHEMBOLXpo")
        End Get
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

End Class