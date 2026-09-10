'***********************************************************************
' Assembly         : Infrastructure.Data.CrystalRepository
' Author           : Jhossept K. Garay Rodriguez
' Created          : 26-01-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Crystal
Imports Domain.Crystal.Entities
Imports Domain.Base.Entities
Imports Domain.Entities
Imports System.Dynamic
Imports Infrastructure.CrossCutting.Resources

Public Class PatientRepository
    Inherits GenericRepository(Of INPACIENT)
    Implements IPatientRepository

    'Contexto del repositorio de Indigo Vie Cloud Platform
    Private _crystalContext As ICrystalModelUnitOfWork

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="crystalContext">Contexto</param>
    Public Sub New(ByVal crystalContext As ICrystalModelUnitOfWork)
        MyBase.New(crystalContext)
        Me._crystalContext = crystalContext
    End Sub


    ''' <summary>
    ''' Obtiene un Paciente por identificación
    ''' </summary>
    ''' <param name="Identification">Identificación de paciente</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPatientByIdentification(Identification As String) As INPACIENT Implements IPatientRepository.GetPatientByIdentification
        Dim spPatient As SP_GetPatientByIdentification_Result = _crystalContext.SP_GetPatientByIdentification(Identification).FirstOrDefault()
        If spPatient IsNot Nothing Then
            Dim patient As New INPACIENT()
            With patient
                .IPCODPACI = spPatient.IPCODPACI
                .IPTIPODOC = spPatient.IPTIPODOC
                .CODIGONIT = spPatient.CODIGONIT
                .IPEXPEDIC = spPatient.IPEXPEDIC
                .IPPRIAPEL = spPatient.IPPRIAPEL
                .IPSEGAPEL = spPatient.IPSEGAPEL
                .IPPRINOMB = spPatient.IPPRINOMB
                .IPSEGNOMB = spPatient.IPSEGNOMB
                .IPNOMCOMP = spPatient.IPNOMCOMP
                .CODEMPRES = spPatient.CODEMPRES
                .IPTIPOPAC = spPatient.IPTIPOPAC
                .IPTIPOAFI = spPatient.IPTIPOAFI
                .CAPACIPAG = spPatient.CAPACIPAG
                .CODENTIDA = spPatient.CODENTIDA
                .CCCONTRAT = spPatient.CCCONTRAT
                .CPPLANBEN = spPatient.CPPLANBEN
                .AUUBICACI = spPatient.AUUBICACI
                .NIVCODIGO = spPatient.NIVCODIGO
                .IPDIRECCI = spPatient.IPDIRECCI
                .IPTELEFON = spPatient.IPTELEFON
                .IPTELMOVI = spPatient.IPTELMOVI
                .IPFECNACI = spPatient.IPFECNACI
                .CODACTIVI = spPatient.CODACTIVI
                .IPSEXOPAC = spPatient.IPSEXOPAC
                .IPESTADOC = spPatient.IPESTADOC
                .IPGRUPSAN = spPatient.IPGRUPSAN
                .IPRHSANGR = spPatient.IPRHSANGR
                .TIPCOBSAL = spPatient.TIPCOBSAL
                .CORELEPAC = spPatient.CORELEPAC
                .CODGRUPOE = spPatient.CODGRUPOE
                .ESTADOPAC = spPatient.ESTADOPAC
                .OBSERVACI = spPatient.OBSERVACI
                .INDAUDFOR = spPatient.INDAUDFOR
                .PACIEFOTO = spPatient.PACIEFOTO
                .PACIEHUELL = spPatient.PACIEHUELL
                .NUMCARPET = spPatient.NUMCARPET
                .CODUSUCRE = spPatient.CODUSUCRE
                .FECREGCRE = spPatient.FECREGCRE
                .CODUSUMOD = spPatient.CODUSUMOD
                .FECREGMOD = spPatient.FECREGMOD
                .IPESTRATO = spPatient.IPESTRATO
                .CREDCODIGO = spPatient.CREDCODIGO
                .DISCCODIGO = spPatient.DISCCODIGO
                .IDICODIGO = spPatient.IDICODIGO
                .NIVECODIGO = spPatient.NIVECODIGO
                .GRUPCODIGO = spPatient.GRUPCODIGO
                .ZONAPARTADA = spPatient.ZONAPARTADA
                .GENCAREGROUP = spPatient.GENCAREGROUP
                .GENCONENTITY = spPatient.GENCONENTITY
                .GENEXPEDITIONCITY = spPatient.GENEXPEDITIONCITY
                .IPORIENTSEXUAL = spPatient.IPORIENTSEXUAL
                .IPIDENTSEXUAL = spPatient.IPIDENTSEXUAL
                .IPORIENTSEXOTRO = spPatient.IPORIENTSEXOTRO
                .IPIDENTSEXOTRO = spPatient.IPIDENTSEXOTRO
                .PESO = spPatient.PESO
                .IPSEXO = spPatient.IPSEXO
                .IDENTMAMA = spPatient.IDENTMAMA
                .IDENTOBSERVAC = spPatient.IDENTOBSERVAC
                .ID = spPatient.ID

                .SonNumber = Convert.ToInt32(spPatient.SonNumber)
                .CompanyDesc = spPatient.CompanyDesc
                .LocationDesc = spPatient.LocationDesc
                .ActivityDesc = spPatient.ActivityDesc
                .EtGroupDesc = spPatient.EtGroupDesc
                .BeliefDesc = spPatient.BeliefDesc
                .DisabilityDesc = spPatient.DisabilityDesc
                .LanguageDesc = spPatient.LanguageDesc
                .LevelDescription = spPatient.LevelDescription
                .EducationLevelDesc = spPatient.EducationLevelDesc
                .SpecialGroupDesc = spPatient.SpecialGroupDesc
                .EntityDescription = spPatient.EntityDescription
            End With
            patient.MarkAsUnchanged()
            Dim topanu = (From e In _crystalContext.INPACIENTTOPANU Where e.IPCODPACI = patient.IPCODPACI Select e).ToList()
            If topanu IsNot Nothing AndAlso topanu.Any() Then
                topanu.ForEach(Sub(item)
                                   patient.INPACIENTTOPANU.Add(item)
                               End Sub)
            End If
            Return patient
        Else
            Return New INPACIENT()
        End If

        'Dim query = From e In _crystalContext.INPACIENT.Include("INPACIENTTOPANU")
        '            Where e.IPCODPACI = Identification
        '            Select e
        'If query.Count > 0 Then
        '    'Llenar Descripcion de la empresa
        '    Dim patient As INPACIENT = query.SingleOrDefault()

        '    If patient.IPSEXOPAC = 2 Then
        '        Dim _query = From e In _crystalContext.INCONSEPA.AsNoTracking()
        '                     Where e.IDDOCUMEN = patient.IPCODPACI And e.IDPOBLACI = "5"
        '                     Select e

        '        If _query.Count > 0 Then
        '            patient.SonNumber = _query.FirstOrDefault().CONSECUTI
        '        End If
        '    End If

        '    If patient.CODEMPRES IsNot String.Empty Then
        '        Dim _query = From e In _crystalContext.ADEMPRESA.AsNoTracking()
        '                     Where e.CODEMPRES = patient.CODEMPRES
        '                     Select e

        '        If _query.Count > 0 Then
        '            patient.CompanyDesc = _query.FirstOrDefault().DESEMPRES
        '        End If
        '    End If

        '    'Llenar Descripcion de la ubicacion
        '    If patient.AUUBICACI IsNot String.Empty Then
        '        Dim _query = From e In _crystalContext.INUBICACI.AsNoTracking()
        '                     Where e.AUUBICACI = patient.AUUBICACI
        '                     Select e

        '        If _query.Count > 0 Then patient.LocationDesc = _query.SingleOrDefault().UBINOMBRE
        '    End If

        '    'Llenar Descripcion de la actividad
        '    If patient.CODACTIVI IsNot String.Empty Then
        '        Dim _query = From e In _crystalContext.ADACTIVID.AsNoTracking()
        '                     Where e.codactivi = patient.CODACTIVI
        '                     Select e

        '        If _query.Count > 0 Then patient.ActivityDesc = _query.SingleOrDefault().desactivi
        '    End If

        '    'Llenar Descripcion del grupo etnico
        '    If patient.CODGRUPOE IsNot String.Empty Then
        '        Dim _query = From e In _crystalContext.ADGRUETNI.AsNoTracking()
        '                     Where e.CODGRUPOE = patient.CODGRUPOE
        '                     Select e

        '        If _query.Count > 0 Then patient.EtGroupDesc = _query.SingleOrDefault().DESGRUPET
        '    End If

        '    'Llenar Descripcion de la creencia
        '    If patient.CREDCODIGO IsNot String.Empty Then
        '        Dim _query = From e In _crystalContext.ADCREDO.AsNoTracking()
        '                     Where e.CREDCODIGO = patient.CREDCODIGO
        '                     Select e

        '        If _query.Count > 0 Then patient.BeliefDesc = _query.SingleOrDefault().CREDDESCRI
        '    End If

        '    'Llenar Descripcion de la discapacidad
        '    If patient.DISCCODIGO IsNot String.Empty Then
        '        Dim _query = From e In _crystalContext.ADDISCAPACI.AsNoTracking()
        '                     Where e.DISCCODIGO = patient.DISCCODIGO
        '                     Select e

        '        If _query.Count > 0 Then patient.DisabilityDesc = _query.SingleOrDefault().DISCDESCRI
        '    End If

        '    'Llenar Descripcion del idioma
        '    If patient.IDICODIGO IsNot String.Empty Then
        '        Dim _query = From e In _crystalContext.ADIDIOMA.AsNoTracking()
        '                     Where e.IDICODIGO = patient.IDICODIGO
        '                     Select e

        '        If _query.Count > 0 Then patient.LanguageDesc = _query.SingleOrDefault().IDIDESCRI
        '    End If

        '    'Llenar Descripcion del idioma
        '    If patient.NIVCODIGO IsNot String.Empty Then
        '        Dim _query = From e In _crystalContext.ADNIVELES.AsNoTracking()
        '                     Where e.NIVCODIGO = patient.NIVCODIGO
        '                     Select e

        '        If _query.Count > 0 Then patient.LevelDescription = _query.SingleOrDefault().NIVDESCRI
        '    End If

        '    'Llenar Descripcion del nivel de educacion
        '    If patient.NIVECODIGO IsNot String.Empty Then
        '        Dim _query = From e In _crystalContext.ADNIVELED.AsNoTracking()
        '                     Where e.NIVECODIGO = patient.NIVECODIGO
        '                     Select e

        '        If _query.Count > 0 Then patient.EducationLevelDesc = _query.SingleOrDefault().NIVEDESCRI
        '    End If

        '    'Llenar Descripcion del grupo especial
        '    If patient.GRUPCODIGO IsNot String.Empty Then
        '        Dim _query = From e In _crystalContext.ADGRUPESP.AsNoTracking()
        '                     Where e.GRUPCODIGO = patient.GRUPCODIGO
        '                     Select e

        '        If _query.Count > 0 Then patient.SpecialGroupDesc = _query.SingleOrDefault().GRUPCODIGO
        '    End If


        '    If patient.CODENTIDA IsNot String.Empty Then
        '        patient.EntityDescription = (From e In _crystalContext.INENTIDAD.AsNoTracking() Where e.CODENTIDA = patient.CODENTIDA Select e.NOMENTIDA).FirstOrDefault()
        '    End If

        '    Return patient
        'Else
        '    Return New INPACIENT()
        'End If
    End Function

    ''' <summary>
    ''' Obtener nivel por codigo 
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetLevelByCode(Code As String) As ADNIVELES Implements IPatientRepository.GetLevelByCode
        Dim res = From e In _crystalContext.ADNIVELES
                  Where e.NIVCODIGO = Code
                  Select e

        If res.Count > 0 Then
            Return res.SingleOrDefault
        Else
            Return New ADNIVELES
        End If
    End Function

    ''' <summary>
    ''' Obtiene el paciente
    ''' </summary>
    ''' <param name="CodePatient"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPatient(CodePatient As String) As INPACIENT Implements IPatientRepository.GetPatient
        Dim query = From e In _crystalContext.INPACIENT
                    Where e.IPCODPACI = CodePatient
                    Select e
        If query.Count > 0 Then
            Return query.SingleOrDefault
        End If
        Return New INPACIENT()
    End Function

    Public Function GetOnlyPatientByIdentification(Identification As String, tracking As Boolean) As INPACIENT Implements IPatientRepository.GetOnlyPatientByIdentification
        Dim query As List(Of INPACIENT) = Nothing
        If tracking Then
            query = (From e In _crystalContext.INPACIENT
                     Where e.IPCODPACI = Identification
                     Select e).ToList()
        Else
            query = (From e In _crystalContext.INPACIENT.AsNoTracking()
                     Where e.IPCODPACI = Identification
                     Select e).ToList()
        End If
        If query.Count > 0 Then
            Return query.FirstOrDefault()
        End If
        Return New INPACIENT()
    End Function

    Public Function ListarHemocomponentesPorIngreso(ingreso As String) As List(Of SP_ListarHemocomponentesPorIngreso_Result) Implements IPatientRepository.ListarHemocomponentesPorIngreso
        Return _crystalContext.SP_ListarHemocomponentesPorIngreso(ingreso).ToList()
    End Function

    ''' <summary>
    ''' Obtiene el paciente por número de ingreso
    ''' </summary>
    ''' <param name="admissionNumber"></param>
    ''' <returns></returns>
    Public Function GetPatientByAdmissionNumber(admissionNumber As String) As INPACIENT Implements IPatientRepository.GetPatientByAdmissionNumber
        Dim result = (From e In _crystalContext.INPACIENT
                      Join a In _crystalContext.ADINGRESO On e.IPCODPACI Equals a.IPCODPACI
                      Where a.NUMINGRES = admissionNumber
                      Select e).FirstOrDefault()
        Return If(result IsNot Nothing, result, String.Empty)
    End Function
End Class
