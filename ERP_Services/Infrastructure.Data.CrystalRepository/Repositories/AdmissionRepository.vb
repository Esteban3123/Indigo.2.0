'***********************************************************************
' Assembly         : Infrastructure.Data.CrystalRepository
' Author           : Cristhian Mauricio Salazar
' Created          : 03-11-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Crystal
Imports Domain.Crystal.Entities
Imports Domain.Entities
Imports System.Dynamic
Imports Infrastructure.CrossCutting.Resources

Public Class AdmissionRepository
    Inherits GenericRepository(Of ADINGRESO)
    Implements IAdmissionRepository


    ''' <summary>
    ''' Prefijo del tipo de paciente en los recursos
    ''' </summary>
    Private Const PATIENTTYPE_PREFIX As String = "PatientType_"

    ''' <summary>
    ''' Nombre del modulo o archivo en los recursos
    ''' </summary>
    Private Const MODULENAME As String = "IndigoCrystalHis"

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
    ''' Obtiene un ingreso por código
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAdmissionByCode(Code As String, Optional tracking As Boolean = True) As ADINGRESO Implements IAdmissionRepository.GetAdmissionByCode
        Dim result As IQueryable(Of ADINGRESO)
        If tracking Then
            result = From e In _crystalContext.ADINGRESO
                Where e.NUMINGRES = Code
                    Select e
        Else
            result = From e In _crystalContext.ADINGRESO.AsNoTracking()
                Where e.NUMINGRES = Code
                    Select e
        End If

        If result.Count > 0 Then
            Dim a As ADINGRESO = result.SingleOrDefault

            If a.NUMINGRES IsNot Nothing AndAlso a.NUMINGRES.ToString.Trim IsNot String.Empty Then
                'descripcion de unidad funcional
                a.AdmissionCode = a.NUMINGRES
                If a.UFUCODIGO IsNot Nothing AndAlso a.UFUCODIGO.ToString.Trim IsNot String.Empty Then
                    Dim _query = From e In _crystalContext.INUNIFUNC
                                 Where e.UFUCODIGO = a.UFUCODIGO
                                 Select e

                    If _query.Count > 0 Then a.ExtFunctionalUnit = _query.SingleOrDefault().UFUCODIGO.ToString.Trim + " - " + _query.SingleOrDefault().UFUDESCRI
                End If
                'descripcion de centro de atencion
                If a.CODCENATE IsNot Nothing AndAlso a.CODCENATE.ToString.Trim IsNot String.Empty Then
                    Dim _query = From e In _crystalContext.ADCENATEN
                                 Where e.CODCENATE = a.CODCENATE
                                 Select e

                    If _query.Count > 0 Then a.ExtCenter = _query.SingleOrDefault().NOMCENATE
                End If
                'descripcion de municipio
                If a.DEPMUNCOD IsNot Nothing AndAlso a.DEPMUNCOD.ToString.Trim IsNot String.Empty Then
                    Dim _query = From e In _crystalContext.INMUNICIP
                                 Where e.DEPMUNCOD = a.DEPMUNCOD
                                 Select e

                    If _query.Count > 0 Then a.ExtTown = _query.SingleOrDefault().MUNNOMBRE
                End If
                'descripcion de IPS 
                If a.AIPSREMIS IsNot Nothing AndAlso a.AIPSREMIS.Trim IsNot String.Empty Then
                    Dim _query = From e In _crystalContext.ADCONTIPS
                                 Where e.CODIGOIPS = a.AIPSREMIS
                                 Select e

                    If _query.Count > 0 Then a.ExtIPS = _query.SingleOrDefault().CODIGOIPS & " - " & _query.SingleOrDefault().DSCRIPIPS
                End If
                'descripcion de salario minimo 
                If a.ISALCODIG IsNot Nothing AndAlso a.ISALCODIG.Trim IsNot String.Empty Then
                    Dim _query = From e In _crystalContext.INSALARIM
                                 Where e.ISALCODIG = a.ISALCODIG
                                 Select e

                    If _query.Count > 0 Then a.ExtMinWage = _query.SingleOrDefault().ISALNOMBR
                End If

                'descripcion de la cama
                If a.CODICAMHO IsNot Nothing AndAlso a.CODICAMHO.Trim IsNot String.Empty Then
                    Dim _query = From e In _crystalContext.CHCAMASHO
                                 Where e.NUMCAMHOS = a.CODICAMHO
                                 Select e

                    If _query.Count > 0 Then a.ExtHospitalizationBed = _query.FirstOrDefault().NUMCAMHOS.Trim & " - " & _query.FirstOrDefault().DESCCAMAS.Trim
                End If

            End If

            Return a
        Else
            Return New ADINGRESO
        End If
    End Function

    ''' <summary>
    ''' Obtiene los ingresos de un paciente
    ''' </summary>
    ''' <param name="PatientIdentification"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAdmissionByPatient(PatientIdentification As String) As List(Of ADINGRESO) Implements IAdmissionRepository.GetAdmissionByPatient

        Dim result = From e In _crystalContext.ADINGRESO
                     Where e.IPCODPACI = PatientIdentification
                     Select e

        If result.Count > 0 Then
            For Each _admission In result.ToList
                'descripcion de unidad funcional
                If _admission.UFUCODIGO IsNot Nothing AndAlso _admission.UFUCODIGO.ToString.Trim IsNot String.Empty Then
                    Dim _query = From e In _crystalContext.INUNIFUNC
                                 Where e.UFUCODIGO = _admission.UFUCODIGO
                                 Select e

                    If _query.Count > 0 Then _admission.ExtFunctionalUnit = _query.SingleOrDefault().UFUDESCRI
                End If
                'descripcion de centro de atencion
                If _admission.CODCENATE IsNot Nothing AndAlso _admission.CODCENATE.ToString.Trim IsNot String.Empty Then
                    Dim _query = From e In _crystalContext.ADCENATEN
                                 Where e.CODCENATE = _admission.CODCENATE
                                 Select e

                    If _query.Count > 0 Then _admission.ExtCenter = _query.SingleOrDefault().NOMCENATE
                End If
            Next
            Return result.ToList
        Else
            Return New List(Of ADINGRESO)
        End If
    End Function

    ''' <summary>
    ''' Obtiene un ingreso y sus agregados en una entidad plana y serializada en formato JSON
    ''' </summary>
    ''' <param name="code">Código del ingreso</param>
    ''' <returns>Entidad plana serializada</returns>
    Public Function GetAdmissionPOCOByCode(code As String) As ExpandoObject Implements IAdmissionRepository.GetAdmissionPOCOByCode
        Dim res As SP_GetAdmissionByCode_Result = _crystalContext.SP_GetAdmissionByCode(code).FirstOrDefault()
        'Dim res = (From TAdmissions In Me._crystalContext.ADINGRESO.AsNoTracking()
        '           Join TPatient In Me._crystalContext.INPACIENT.AsNoTracking() On TAdmissions.IPCODPACI Equals TPatient.IPCODPACI
        '           Join TNiveles In Me._crystalContext.ADNIVELES.AsNoTracking() On TPatient.ADNIVELES.NIVCODIGO Equals TNiveles.NIVCODIGO
        '           Join TCentAtenc In _crystalContext.ADCENATEN.AsNoTracking() On TAdmissions.CODCENATE Equals TCentAtenc.CODCENATE
        '           Join TUniFunc In _crystalContext.INUNIFUNC.AsNoTracking() On TAdmissions.UFUCODIGO Equals TUniFunc.UFUCODIGO
        '           Where (TAdmissions.NUMINGRES.Trim().Equals(code))
        '           Select New With {
        '                                              .AdmissionCode = TAdmissions.NUMINGRES,
        '                                              .AdmissionDate = TAdmissions.IFECHAING,
        '                                              .AdmissionType = TAdmissions.TIPOINGRE,
        '                                              .LiquidationType = TAdmissions.ILIQUIDAC,
        '                                              .AdmissionCaregroupId = TAdmissions.GENCAREGROUP,
        '                                              .AdmissionReason = TAdmissions.ICAUSAING,
        '                                              .AdmissionRiskType = TAdmissions.ITIPORIES,
        '                                              .AdmissionCentAtencCodeName = String.Concat(TCentAtenc.CODCENATE.Trim(), " - ", TCentAtenc.NOMCENATE.Trim()),
        '                                              .AdmissionUniFuncCodeName = String.Concat(TUniFunc.UFUCODIGO.Trim(), " - ", TUniFunc.UFUDESCRI.Trim()),
        '                                              .PlaceEntry = TAdmissions.IINGREPOR,
        '                                              .BenefitPlan = TAdmissions.CODPANATE,
        '                                              .AuthorizationNumber = TAdmissions.IAUTORIZA,
        '                                              .ResponsiblePhone = TAdmissions.IPTELEFON,
        '                                              .ResponsibleName = TAdmissions.IPRNOMBRE,
        '                                              .PatientCode = TPatient.IPCODPACI,
        '                                              .PatientBirth = TPatient.IPFECNACI,
        '                                              .PatientGenus = TPatient.IPSEXOPAC,
        '                                              .PatientEstrato = TPatient.IPESTRATO,
        '                                              .PatientType = TPatient.IPTIPOPAC,
        '                                              .PatientAfiliation = TPatient.IPTIPOAFI,
        '                                              .PatientDocumentType = TPatient.IPTIPODOC,
        '                                              .NivelCode = TNiveles.NIVCODIGO,
        '                                              .NivelName = TNiveles.NIVDESCRI,
        '                                              .NivelModeratorSharePercentage = TNiveles.NIVPORCMO,
        '                                              .NivelCoPayContribPercentage = TNiveles.NIVPORCOP,
        '                                              .NivelCoPaySubsiPercentage = TNiveles.NIVPORSUB,
        '                                              .NivelCoPayVincuPercentage = TNiveles.NIVPORVIN,
        '                                              .NivelSisben = TNiveles.NIVSISBEN,
        '                                              .NivelModeratorShareTop = TNiveles.TOPEVECMO,
        '                                              .NivelCoPayContribTop = TNiveles.TOPEVECOP,
        '                                              .NivelCoPaySubsibTop = TNiveles.TOPEVESUB,
        '                                              .NivelCoPayVincuTop = TNiveles.TOPEVEVIN,
        '                                              .NivelModeratorShareTopYear = TNiveles.TOPANUCMO,
        '                                              .NivelCoPayContribTopYear = TNiveles.TOPANUCOP,
        '                                              .NivelCoPaySubsibTopYear = TNiveles.TOPANUSUB,
        '                                              .NivelCoPayVincuTopYear = TNiveles.TOPANUVIN,
        '                                              .Status = TAdmissions.IESTADOIN,
        '                                              .EntityCode = "",
        '                                              .EntityName = "",
        '                                              .EntityId = TAdmissions.GENCONENTITY,
        '                                              .PatientEntityCode = "",
        '                                              .PatientEntityName = "",
        '                                              .PatientEntityId = TPatient.GENCONENTITY,
        '                                              .PatientPhone = TPatient.IPTELMOVI,
        '                                              .PatientCareGroupId = TPatient.GENCAREGROUP}).ToList()

        Dim admission As Object = New ExpandoObject()
        'Aqui se cargan los datos del ingreso y sus agregados
        If res IsNot Nothing Then
            admission.AdmissionCode = If(res.AdmissionCode Is Nothing, String.Empty, res.AdmissionCode.Trim())
            admission.AdmissionCodeWithOutTrim = If(res.AdmissionCode Is Nothing, String.Empty, res.AdmissionCode)
            admission.AdmissionDate = res.AdmissionDate
            admission.AdmissionType = res.AdmissionType
            admission.AdmissionReason = res.AdmissionReason
            admission.AdmissionCaregroupId = res.AdmissionCaregroupId
            admission.AdmissionRiskType = res.AdmissionRiskType
            admission.AdmissionCentAtencCodeName = res.AdmissionCentAtencCodeName
            admission.AdmissionUniFuncCodeName = res.AdmissionUniFuncCodeName
            admission.PlaceEntry = res.PlaceEntry
            admission.BenefitPlan = If(res.BenefitPlan Is Nothing, String.Empty, res.BenefitPlan.Trim())
            admission.EntityCode = res.EntityCode
            admission.LiquidationType = res.LiquidationType
            admission.EntityName = res.EntityName
            admission.HealthAdministratorId = res.EntityId
            admission.PatientBirth = res.PatientBirth
            admission.PatientCode = res.PatientCode
            admission.PatientGenus = res.PatientGenus
            admission.PatientEntityCode = res.PatientEntityCode
            admission.PatientEntityName = res.PatientEntityName
            admission.PatientEntityId = res.PatientEntityId
            admission.PatientPhone = res.PatientPhone
            admission.PatientEstrato = If(res.PatientEstrato Is Nothing, 0, res.PatientEstrato)
            admission.AuthorizationNumber = If(res.AuthorizationNumber Is Nothing, String.Empty, res.AuthorizationNumber.Trim())
            admission.ResponsibleName = res.ResponsibleName
            admission.ResponsiblePhone = If(res.ResponsiblePhone Is Nothing, String.Empty, res.ResponsiblePhone.Trim())
            admission.PatientType = res.PatientType
            admission.PatientDocumentType = res.PatientDocumentType
            admission.PatientAfiliation = res.PatientAfiliation
            admission.PatientCareGroupId = res.PatientCareGroupId
            admission.NivelCode = res?.NivelCode
            admission.NivelName = res?.NivelName?.Trim()
            admission.NivelModeratorSharePercentage = res?.NivelModeratorSharePercentage
            admission.NivelCoPayContribPercentage = res?.NivelCoPayContribPercentage
            admission.NivelCoPaySubsiPercentage = res?.NivelCoPaySubsiPercentage
            admission.NivelCoPayVincuPercentage = res?.NivelCoPayVincuPercentage
            admission.NivelSisben = res?.NivelSisben
            admission.NivelModeratorShareTop = res?.NivelModeratorShareTop
            admission.NivelCoPayContribTop = res?.NivelCoPayContribTop
            admission.NivelCoPaySubsibTop = res?.NivelCoPaySubsibTop
            admission.NivelCoPayVincuTop = res?.NivelCoPayVincuTop
            admission.NivelModeratorShareTopYear = res?.NivelModeratorShareTopYear
            admission.NivelCoPayContribTopYear = res?.NivelCoPayContribTopYear
            admission.NivelCoPaySubsibTopYear = res?.NivelCoPaySubsibTopYear
            admission.NivelCoPayVincuTopYear = res?.NivelCoPayVincuTopYear
            admission.Status = If(res.Status Is Nothing, String.Empty, res.Status.Trim())
            Return admission
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Obtiene un ingreso por codigo para utilizarlo en orden de servicio
    ''' </summary>
    ''' <param name="admissionNumber"></param>
    ''' <returns></returns>
    Public Function GetAdmissionByServiceOrder(admissionNumber As String) As ExpandoObject Implements IAdmissionRepository.GetAdmissionByServiceOrder
        Dim res = (From TAdmissions In _crystalContext.ADINGRESO
                                     Join TPatient In _crystalContext.INPACIENT On TAdmissions.IPCODPACI Equals TPatient.IPCODPACI
        Where (TAdmissions.NUMINGRES = admissionNumber)
                                     Select New With {.AdmissionCode = TAdmissions.NUMINGRES, _
                                                      .PatientNit = TPatient.CODIGONIT, _
                                                      .PatientCode = TPatient.IPCODPACI, _
                                                      .PatientName = TPatient.IPNOMCOMP, _
                                                      .PatientDateBirth = TPatient.IPFECNACI, _
                                                      .PatientGenus = TPatient.IPSEXOPAC, _
                                                      .AdmissionDate = TAdmissions.IFECHAING, _
                                                      .AdmissionType = TAdmissions.TIPOINGRE, _
                                                      .BedStay = TAdmissions.CODICAMHO, _
                                                      .PlaceEntry = TAdmissions.IINGREPOR, _
                                                      .LiquidationType = TAdmissions.ILIQUIDAC, _
                                                      .BenefitPlan = TAdmissions.CODPANATE, _
                                                      .AuthorizationNumber = TAdmissions.IAUTORIZA, _
                                                      .ResponsibleName = TAdmissions.IPRNOMBRE, _
                                                      .ResponsiblePhone = TAdmissions.IPTELEFON, _
                                                      .EntityNit = "", _
                                                      .EntityCode = "", _
                                                      .EntityName = "", _
                                                      .CareGroupId = TAdmissions.GENCAREGROUP, _
                                                      .HealthAdministratorId = TAdmissions.GENCONENTITY, _
                                                      .TRATAESPECIA = TAdmissions.TRATAESPECIA, _
                                                      .Status = TAdmissions.IESTADOIN}).FirstOrDefault()




        Dim admission As Object = New ExpandoObject()
        If res IsNot Nothing Then
            With admission
                .AdmissionCode = res.AdmissionCode.Trim()
                .PatientNit = res.PatientNit
                .PatientCode = res.PatientCode
                .PatientName = res.PatientName
                .PatientDateBirth = res.PatientDateBirth
                .PatientGenus = res.PatientGenus
                .AdmissionDate = res.AdmissionDate
                .AdmissionType = res.AdmissionType
                .BedStay = res.BedStay
                .PlaceEntry = res.PlaceEntry
                .LiquidationType = res.LiquidationType
                .BenefitPlan = res.BenefitPlan
                .EntityNit = res.EntityNit
                .EntityCode = res.EntityCode
                .EntityName = res.EntityName
                .AuthorizationNumber = res.AuthorizationNumber
                .ResponsibleName = res.ResponsibleName
                .ResponsiblePhone = res.ResponsiblePhone
                .CareGroupId = res.CareGroupId
                .HealthAdministratorId = res.HealthAdministratorId
                .Status = res.Status
                .TRATAESPECIA = res.TRATAESPECIA
            End With

            Return admission
        Else
            Return Nothing
        End If

    End Function

    ''' <summary>
    ''' Obtiene los parametros de un centro de atención
    ''' </summary>
    ''' <param name="_codcenate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCentersParameters(_codcenate As String) As ADPARAMET Implements IAdmissionRepository.GetCentersParameters
        Dim res = From e In _crystalContext.ADPARAMET
                  Where e.CODCENATE = _codcenate
                  Select e

        If res.Count > 0 Then
            Return res.SingleOrDefault()
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Obtiene fecha de triage para validar creación de ingreo
    ''' </summary>
    ''' <param name="paciente"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetTriageDate(paciente As String) As DateTime Implements IAdmissionRepository.GetTriageDate
        Dim res = From e In _crystalContext.ADCONTURG
                  Where e.IPCODPACI = paciente And e.CONESTADO = "3"
                  Select e

        If res.Count > 0 Then
            Return res.SingleOrDefault().IPFECLLEGA
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Obtiene el estado de la cama
    ''' </summary>
    ''' <param name="cama"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBedStatus(cama As String) As Boolean Implements IAdmissionRepository.GetBedStatus
        Dim res = From e In _crystalContext.CHCAMASHO
                  Where e.CODICAMAS = cama
                  Select e

        If res.Count > 0 Then
            If res.SingleOrDefault.ESTADCAMA = 1 Then
                Return True
            End If
        End If
        Return False
    End Function

    ''' <summary>
    ''' Funcion que valida si se puede anular un ingreso
    ''' </summary>
    ''' <param name="NUMINGRES"></param>
    ''' <param name="IPCODPACI"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function AdmissionAnulateValidation(NUMINGRES As String, IPCODPACI As String) As SP_AD_ValidaAnulacionIngreso_Result Implements IAdmissionRepository.AdmissionAnulateValidation
        Dim result = (From e In _crystalContext.SP_AD_ValidaAnulacionIngreso(NUMINGRES, IPCODPACI) Select e).SingleOrDefault
        Return result
    End Function

    Public Function GetAdmissionStatusByNumIngres(admissionNumber As String) As String Implements IAdmissionRepository.GetAdmissionStatusByNumIngres
        Return (From i In _crystalContext.ADINGRESO.AsNoTracking() Where i.NUMINGRES = admissionNumber Select i.IESTADOIN).FirstOrDefault()
    End Function

    

    Public Function GetINDIAGNOPByAdmissionNumber(adminssionNumber As String) As INDIAGNOP Implements IAdmissionRepository.GetINDIAGNOPByAdmissionNumber
        Return (From e In _crystalContext.INDIAGNOP.AsNoTracking() Where e.NUMINGRES = adminssionNumber And e.CODDIAPRI = True Order By e.FECDIAGNO Descending Select e).Take(1).FirstOrDefault()
    End Function
End Class
