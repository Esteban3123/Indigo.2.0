Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

#Region "Structure"

Public Structure keyViewAdmission

    <Persistent("NUMEROINGRESO")>
    Public Property NUMEROINGRESO As String
    <Persistent("CAMA")>
    Public Property CAMA As String
    <Persistent("FECHAHOSPITALIZACION")>
    Public Property FECHAHOSPITALIZACION As DateTime
    <Persistent("NOMBREACOMPANANTE")>
    Public Property NOMBREACOMPANANTE As String
    <Persistent("PARENTESCO")>
    Public Property PARENTESCO As String
End Structure

#End Region


<Persistent("dbo.ViewAdmissionReport")>
Public Class VAdmission
    Inherits XPLiteObject

    <Key(True), Persistent()>
    Public Property Key As keyViewAdmission

    Dim fNUMEROINGRESO As String
    <Size(10)>
    Public Property NUMEROINGRESO() As String
        Get
            Return fNUMEROINGRESO
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NUMEROINGRESO", fNUMEROINGRESO, value)
        End Set
    End Property
    Dim fCODIGOPACIENTE As String
    <Size(15)>
    Public Property CODIGOPACIENTE() As String
        Get
            Return fCODIGOPACIENTE
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CODIGOPACIENTE", fCODIGOPACIENTE, value)
        End Set
    End Property
    Dim fNOMBREDELPACIENTE As String
    <Size(250)>
    <Persistent("NOMBRE DEL PACIENTE")>
    Public Property NOMBREDELPACIENTE() As String
        Get
            Return fNOMBREDELPACIENTE
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NOMBREDELPACIENTE", fNOMBREDELPACIENTE, value)
        End Set
    End Property
    Dim fFECHADENACIMIENTOCORTA As DateTime
    <Persistent("FECHA DE NACIMIENTO CORTA")>
    Public Property FECHADENACIMIENTOCORTA() As DateTime
        Get
            Return fFECHADENACIMIENTOCORTA
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("FECHADENACIMIENTOCORTA", fFECHADENACIMIENTOCORTA, value)
        End Set
    End Property
    Dim fFECHADENACIMIENTO As String
    <Size(1)>
    <Persistent("FECHA DE NACIMIENTO")>
    Public Property FECHADENACIMIENTO() As String
        Get
            Return fFECHADENACIMIENTO
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("FECHADENACIMIENTO", fFECHADENACIMIENTO, value)
        End Set
    End Property
    Dim fSEXO As String
    <Size(9)>
    Public Property SEXO() As String
        Get
            Return fSEXO
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("SEXO", fSEXO, value)
        End Set
    End Property
    Dim fTIPODOCUMENTO As String
    <Size(2)>
    <Persistent("TIPO DOCUMENTO")>
    Public Property TIPODOCUMENTO() As String
        Get
            Return fTIPODOCUMENTO
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("TIPODOCUMENTO", fTIPODOCUMENTO, value)
        End Set
    End Property
    Dim fDIRECCIONPACIENTE As String
    <Size(SizeAttribute.Unlimited)>
    <Persistent("DIRECCION PACIENTE")>
    Public Property DIRECCIONPACIENTE() As String
        Get
            Return fDIRECCIONPACIENTE
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("DIRECCIONPACIENTE", fDIRECCIONPACIENTE, value)
        End Set
    End Property
    Dim fTELEFONO As String
    <Size(SizeAttribute.Unlimited)>
    Public Property TELEFONO() As String
        Get
            Return fTELEFONO
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("TELEFONO", fTELEFONO, value)
        End Set
    End Property
    Dim fTIPODEINGRESO As String
    <Size(12)>
    <Persistent("TIPO DE INGRESO")>
    Public Property TIPODEINGRESO() As String
        Get
            Return fTIPODEINGRESO
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("TIPODEINGRESO", fTIPODEINGRESO, value)
        End Set
    End Property
    Dim fINGRESODELPACIENTE As String
    <Size(28)>
    <Persistent("INGRESODELPACIENTE")>
    Public Property INGRESODELPACIENTE() As String
        Get
            Return fINGRESODELPACIENTE
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("INGRESODELPACIENTE", fINGRESODELPACIENTE, value)
        End Set
    End Property
    Dim fTIPODERIESGO As String
    <Size(31)>
    <Persistent("TIPODERIESGO")>
    Public Property TIPODERIESGO() As String
        Get
            Return fTIPODERIESGO
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("TIPODERIESGO", fTIPODERIESGO, value)
        End Set
    End Property
    Dim fCAUSADELINGRESO As String
    <Size(28)>
    <Persistent("CAUSADELINGRESO")>
    Public Property CAUSADELINGRESO() As String
        Get
            Return fCAUSADELINGRESO
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CAUSADELINGRESO", fCAUSADELINGRESO, value)
        End Set
    End Property
    Dim fCODIGODEENTIDADDEINGRESO As String
    <Size(9)>
    <Persistent("CODIGO DE ENTIDAD DE INGRESO")>
    Public Property CODIGODEENTIDADDEINGRESO() As String
        Get
            Return fCODIGODEENTIDADDEINGRESO
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CODIGODEENTIDADDEINGRESO", fCODIGODEENTIDADDEINGRESO, value)
        End Set
    End Property
    Dim fNOMBREDEENTIDADDEINGRESO As String
    <Size(150)>
    <Persistent("NOMBRE DE ENTIDAD DE INGRESO")>
    Public Property NOMBREDEENTIDADDEINGRESO() As String
        Get
            Return fNOMBREDEENTIDADDEINGRESO
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NOMBREDEENTIDADDEINGRESO", fNOMBREDEENTIDADDEINGRESO, value)
        End Set
    End Property
    Dim fCODIGODEENTIDADDELPACIENTE As String
    <Size(9)>
    <Persistent("CODIGO DE ENTIDAD DEL PACIENTE")>
    Public Property CODIGODEENTIDADDELPACIENTE() As String
        Get
            Return fCODIGODEENTIDADDELPACIENTE
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CODIGODEENTIDADDELPACIENTE", fCODIGODEENTIDADDELPACIENTE, value)
        End Set
    End Property
    Dim fNOMBREDEENTIDADDELPACIENTE As String
    <Size(150)>
    <Persistent("NOMBRE DE ENTIDAD DEL PACIENTE")>
    Public Property NOMBREDEENTIDADDELPACIENTE() As String
        Get
            Return fNOMBREDEENTIDADDELPACIENTE
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NOMBREDEENTIDADDELPACIENTE", fNOMBREDEENTIDADDELPACIENTE, value)
        End Set
    End Property
    Dim fCODIGODELCONTRATODEEAPB As String
    <Size(6)>
    <Persistent("CODIGO DEL CONTRATO DE EAPB")>
    Public Property CODIGODELCONTRATODEEAPB() As String
        Get
            Return fCODIGODELCONTRATODEEAPB
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CODIGODELCONTRATODEEAPB", fCODIGODELCONTRATODEEAPB, value)
        End Set
    End Property
    Dim fNOMBRECONTRATO As String
    <Size(90)>
    <Persistent("NOMBRE CONTRATO")>
    Public Property NOMBRECONTRATO() As String
        Get
            Return fNOMBRECONTRATO
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NOMBRECONTRATO", fNOMBRECONTRATO, value)
        End Set
    End Property
    Dim fCODIGOPLANDEBENEFICIOS As String
    <Size(2)>
    <Persistent("CODIGO PLAN DE BENEFICIOS")>
    Public Property CODIGOPLANDEBENEFICIOS() As String
        Get
            Return fCODIGOPLANDEBENEFICIOS
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CODIGOPLANDEBENEFICIOS", fCODIGOPLANDEBENEFICIOS, value)
        End Set
    End Property
    Dim fPLANDEBENEFICIOS As String
    <Size(40)>
    <Persistent("PLAN DE BENEFICIOS")>
    Public Property PLANDEBENEFICIOS() As String
        Get
            Return fPLANDEBENEFICIOS
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PLANDEBENEFICIOS", fPLANDEBENEFICIOS, value)
        End Set
    End Property
    Dim fFECHADEINGRESO As DateTime
    <Persistent("FECHA DE INGRESO")>
    Public Property FECHADEINGRESO() As DateTime
        Get
            Return fFECHADEINGRESO
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("FECHADEINGRESO", fFECHADEINGRESO, value)
        End Set
    End Property
    Dim fLIQUIDAPACIENTE As String
    <Size(25)>
    <Persistent("LIQUIDA PACIENTE")>
    Public Property LIQUIDAPACIENTE() As String
        Get
            Return fLIQUIDAPACIENTE
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("LIQUIDAPACIENTE", fLIQUIDAPACIENTE, value)
        End Set
    End Property
    Dim fCONTROLADMINISTRATIVO As String
    <Size(15)>
    <Persistent("CONTROL ADMINISTRATIVO")>
    Public Property CONTROLADMINISTRATIVO() As String
        Get
            Return fCONTROLADMINISTRATIVO
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CONTROLADMINISTRATIVO", fCONTROLADMINISTRATIVO, value)
        End Set
    End Property
    Dim fCODIGOCENTRODEATENCION As String
    <Size(10)>
    <Persistent("CODIGO CENTRO DE ATENCION")>
    Public Property CODIGOCENTRODEATENCION() As String
        Get
            Return fCODIGOCENTRODEATENCION
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CODIGOCENTRODEATENCION", fCODIGOCENTRODEATENCION, value)
        End Set
    End Property
    Dim fCENTRODEATENCION As String
    <Persistent("CENTRO DE ATENCION")>
    Public Property CENTRODEATENCION() As String
        Get
            Return fCENTRODEATENCION
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CENTRODEATENCION", fCENTRODEATENCION, value)
        End Set
    End Property
    Dim fCODIGOUNIDADFUNCIONAL As String
    <Size(10)>
    <Persistent("CODIGO UNIDAD FUNCIONAL")>
    Public Property CODIGOUNIDADFUNCIONAL() As String
        Get
            Return fCODIGOUNIDADFUNCIONAL
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CODIGOUNIDADFUNCIONAL", fCODIGOUNIDADFUNCIONAL, value)
        End Set
    End Property
    Dim fUNIDADFUNCIONAL As String
    <Size(60)>
    <Persistent("UNIDAD FUNCIONAL")>
    Public Property UNIDADFUNCIONAL() As String
        Get
            Return fUNIDADFUNCIONAL
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("UNIDADFUNCIONAL", fUNIDADFUNCIONAL, value)
        End Set
    End Property
    Dim fNUMERODEAUTORIZACION As String
    <Size(15)>
    <Persistent("NUMERO DE AUTORIZACION")>
    Public Property NUMERODEAUTORIZACION() As String
        Get
            Return fNUMERODEAUTORIZACION
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NUMERODEAUTORIZACION", fNUMERODEAUTORIZACION, value)
        End Set
    End Property
    Dim fESTADODELINGRESO As String
    <Size(29)>
    <Persistent("ESTADO DEL INGRESO")>
    Public Property ESTADODELINGRESO() As String
        Get
            Return fESTADODELINGRESO
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ESTADODELINGRESO", fESTADODELINGRESO, value)
        End Set
    End Property
    Dim fNUMEROINGRESOACCIDENTEDETRANSITO As String
    <Size(10)>
    <Persistent("NUMERO INGRESO ACCIDENTE DE TRANSITO")>
    Public Property NUMEROINGRESOACCIDENTEDETRANSITO() As String
        Get
            Return fNUMEROINGRESOACCIDENTEDETRANSITO
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NUMEROINGRESOACCIDENTEDETRANSITO", fNUMEROINGRESOACCIDENTEDETRANSITO, value)
        End Set
    End Property
    Dim fVALORFACTURADOSOAT As Decimal
    <Persistent("VALOR FACTURADO SOAT")>
    Public Property VALORFACTURADOSOAT() As Decimal
        Get
            Return fVALORFACTURADOSOAT
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("VALORFACTURADOSOAT", fVALORFACTURADOSOAT, value)
        End Set
    End Property
    Dim fCODIGODELSMLV As String
    <Size(3)>
    <Persistent("CODIGO DEL SMLV")>
    Public Property CODIGODELSMLV() As String
        Get
            Return fCODIGODELSMLV
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CODIGODELSMLV", fCODIGODELSMLV, value)
        End Set
    End Property
    Dim fNOMBREDELSMLV As String
    <Size(50)>
    <Persistent("NOMBRE DEL SMLV")>
    Public Property NOMBREDELSMLV() As String
        Get
            Return fNOMBREDELSMLV
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NOMBREDELSMLV", fNOMBREDELSMLV, value)
        End Set
    End Property
    Dim fNUMERODEREMISION As String
    <Size(8)>
    <Persistent("NUMERO DE REMISION")>
    Public Property NUMERODEREMISION() As String
        Get
            Return fNUMERODEREMISION
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NUMERODEREMISION", fNUMERODEREMISION, value)
        End Set
    End Property
    Dim fFECHADEREMISION As DateTime
    <Persistent("FECHA DE REMISION")>
    Public Property FECHADEREMISION() As DateTime
        Get
            Return fFECHADEREMISION
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("FECHADEREMISION", fFECHADEREMISION, value)
        End Set
    End Property
    Dim fAUTORIZACIONDELAREMISION As String
    <Size(15)>
    <Persistent("AUTORIZACION DE LA REMISION")>
    Public Property AUTORIZACIONDELAREMISION() As String
        Get
            Return fAUTORIZACIONDELAREMISION
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AUTORIZACIONDELAREMISION", fAUTORIZACIONDELAREMISION, value)
        End Set
    End Property
    Dim fMUNICIPIODELAIPS As String
    <Size(5)>
    <Persistent("MUNICIPIO DE LA IPS")>
    Public Property MUNICIPIODELAIPS() As String
        Get
            Return fMUNICIPIODELAIPS
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("MUNICIPIODELAIPS", fMUNICIPIODELAIPS, value)
        End Set
    End Property
    Dim fNOMBREMUNICIPIODELAIPS As String
    <Size(40)>
    <Persistent("NOMBRE MUNICIPIO DE LA IPS")>
    Public Property NOMBREMUNICIPIODELAIPS() As String
        Get
            Return fNOMBREMUNICIPIODELAIPS
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NOMBREMUNICIPIODELAIPS", fNOMBREMUNICIPIODELAIPS, value)
        End Set
    End Property
    Dim fIPSDEREMISION As String
    <Size(50)>
    <Persistent("IPS DE REMISION")>
    Public Property IPSDEREMISION() As String
        Get
            Return fIPSDEREMISION
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("IPSDEREMISION", fIPSDEREMISION, value)
        End Set
    End Property
    Dim fOBSERVACIONES As String
    <Size(2000)>
    Public Property OBSERVACIONES() As String
        Get
            Return fOBSERVACIONES
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("OBSERVACIONES", fOBSERVACIONES, value)
        End Set
    End Property
    Dim fJUSTIFICACIONANULACION As String
    <Size(254)>
    <Persistent("JUSTIFICACION ANULACION")>
    Public Property JUSTIFICACIONANULACION() As String
        Get
            Return fJUSTIFICACIONANULACION
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("JUSTIFICACIONANULACION", fJUSTIFICACIONANULACION, value)
        End Set
    End Property
    Dim fCODIGODELCONTRATODEEAPBDELPACIENTE As String
    <Size(6)>
    <Persistent("CODIGO DEL CONTRATO DE EAPB DEL PACIENTE")>
    Public Property CODIGODELCONTRATODEEAPBDELPACIENTE() As String
        Get
            Return fCODIGODELCONTRATODEEAPBDELPACIENTE
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CODIGODELCONTRATODEEAPBDELPACIENTE", fCODIGODELCONTRATODEEAPBDELPACIENTE, value)
        End Set
    End Property
    Dim fNOMBRECONTRATODELPACIENTE As String
    <Size(90)>
    <Persistent("NOMBRE CONTRATO DEL PACIENTE")>
    Public Property NOMBRECONTRATODELPACIENTE() As String
        Get
            Return fNOMBRECONTRATODELPACIENTE
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NOMBRECONTRATODELPACIENTE", fNOMBRECONTRATODELPACIENTE, value)
        End Set
    End Property
    Dim fCODIGOPLANDEBENEFICIOSDELPACIENTE As String
    <Size(2)>
    <Persistent("CODIGO PLAN DE BENEFICIOS DEL PACIENTE")>
    Public Property CODIGOPLANDEBENEFICIOSDELPACIENTE() As String
        Get
            Return fCODIGOPLANDEBENEFICIOSDELPACIENTE
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CODIGOPLANDEBENEFICIOSDELPACIENTE", fCODIGOPLANDEBENEFICIOSDELPACIENTE, value)
        End Set
    End Property
    Dim fPLANDEBENEFICIOSDELPACIENTE As String
    <Size(40)>
    <Persistent("PLAN DE BENEFICIOS DEL PACIENTE")>
    Public Property PLANDEBENEFICIOSDELPACIENTE() As String
        Get
            Return fPLANDEBENEFICIOSDELPACIENTE
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PLANDEBENEFICIOSDELPACIENTE", fPLANDEBENEFICIOSDELPACIENTE, value)
        End Set
    End Property
    Dim fDESCRIPCIONDELNIVEL As String
    <Persistent("DESCRIPCION DEL NIVEL")>
    Public Property DESCRIPCIONDELNIVEL() As String
        Get
            Return fDESCRIPCIONDELNIVEL
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("DESCRIPCIONDELNIVEL", fDESCRIPCIONDELNIVEL, value)
        End Set
    End Property
    Dim fCAMA As String
    <Size(10)>
    Public Property CAMA() As String
        Get
            Return fCAMA
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CAMA", fCAMA, value)
        End Set
    End Property
    Dim fFECHAHOSPITALIZACION As DateTime
    '<Persistent("FECHA HOSPITALIZACION")> _
    Public Property FECHAHOSPITALIZACION() As DateTime
        Get
            Return fFECHAHOSPITALIZACION
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("FECHAHOSPITALIZACION", fFECHAHOSPITALIZACION, value)
        End Set
    End Property
    Dim fNUMERODECARPETA As String
    <Size(15)>
    <Persistent("NUMERO DE CARPETA")>
    Public Property NUMERODECARPETA() As String
        Get
            Return fNUMERODECARPETA
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NUMERODECARPETA", fNUMERODECARPETA, value)
        End Set
    End Property
    Dim fCODIGOGRUPOATENCIONINGRESO As String
    <Size(20)>
    <Persistent("CODIGO GRUPO ATENCION INGRESO")>
    Public Property CODIGOGRUPOATENCIONINGRESO() As String
        Get
            Return fCODIGOGRUPOATENCIONINGRESO
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CODIGOGRUPOATENCIONINGRESO", fCODIGOGRUPOATENCIONINGRESO, value)
        End Set
    End Property
    Dim fNOMBREGRUPOATENCIONINGRESO As String
    <Persistent("NOMBRE GRUPO ATENCION INGRESO")>
    Public Property NOMBREGRUPOATENCIONINGRESO() As String
        Get
            Return fNOMBREGRUPOATENCIONINGRESO
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NOMBREGRUPOATENCIONINGRESO", fNOMBREGRUPOATENCIONINGRESO, value)
        End Set
    End Property
    Dim fCODIGOGRUPOATENCIONPACIENTE As String
    <Size(20)>
    <Persistent("CODIGO GRUPO ATENCION PACIENTE")>
    Public Property CODIGOGRUPOATENCIONPACIENTE() As String
        Get
            Return fCODIGOGRUPOATENCIONPACIENTE
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CODIGOGRUPOATENCIONPACIENTE", fCODIGOGRUPOATENCIONPACIENTE, value)
        End Set
    End Property
    Dim fNOMBREGRUPOATENCIONPACIENTE As String
    <Persistent("NOMBRE GRUPO ATENCION PACIENTE")>
    Public Property NOMBREGRUPOATENCIONPACIENTE() As String
        Get
            Return fNOMBREGRUPOATENCIONPACIENTE
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NOMBREGRUPOATENCIONPACIENTE", fNOMBREGRUPOATENCIONPACIENTE, value)
        End Set
    End Property
    Dim fNOMBREACOMPANANTE As String
    <Size(243)>
    <Persistent("NOMBREACOMPANANTE")>
    Public Property NOMBREACOMPANANTE() As String
        Get
            Return fNOMBREACOMPANANTE
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NOMBREACOMPANANTE", fNOMBREACOMPANANTE, value)
        End Set
    End Property
    Dim fPARENTESCO As String
    <Size(10)>
    Public Property PARENTESCO() As String
        Get
            Return fPARENTESCO
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PARENTESCO", fPARENTESCO, value)
        End Set
    End Property
    Dim fCODIGOACOMPANANTE As String
    <Size(15)>
    <Persistent("CODIGOACOMPANANTE")>
    Public Property CODIGOACOMPANANTE() As String
        Get
            Return fCODIGOACOMPANANTE
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CODIGOACOMPANANTE", fCODIGOACOMPANANTE, value)
        End Set
    End Property
    Dim fCODIGOUSARIO As String
    <Size(20)>
    <Persistent("CODIGO USARIO")>
    Public Property CODIGOUSARIO() As String
        Get
            Return fCODIGOUSARIO
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CODIGOUSARIO", fCODIGOUSARIO, value)
        End Set
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class
