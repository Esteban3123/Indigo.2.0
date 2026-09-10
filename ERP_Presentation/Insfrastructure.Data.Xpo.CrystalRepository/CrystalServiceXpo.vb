'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.CrystalRepository
' Author           : Juan F. Tamayo
' Created          : 2014-11-04
'
' Copyright        : (c) . All rights reserved.
'*************************************************************

#Region "Imports"

Imports DevExpress.Xpo.DB
Imports DevExpress.Xpo
Imports DevExpress.Xpo.Metadata
Imports System.ServiceModel
Imports Infrastructure.CrossCutting.Xpo.Base
Imports System.IO
Imports Infrastructure.CrossCutting.Base
Imports DevExpress.Data.Filtering
Imports DevExpress.Data.Linq
Imports System.Configuration
Imports DevExpress.Data.PLinq
Imports System.Dynamic
Imports Infrastructure.Data.Xpo.CrystalRepository.INDIGO001

#End Region

''' <summary>
''' Expone los servicios Xpo de Indigo Crystal HIS
''' </summary>
Public Class CrystalServiceXpo
    Inherits XpoBaseService

#Region "Fields"

    ''' <summary>
    ''' uri donde estan localizado los servicios xpo
    ''' </summary>
    Dim uriServiceEntitiesXpo As String

    ''' <summary>
    ''' protocolo utilizado para los servicios xpo
    ''' </summary>
    Dim protocolServicesXpo As Protocol

    ''' <summary>
    ''' Variable de Tipo Consultas asincronas de xpo
    ''' </summary>
    Dim serverMode As XPInstantFeedbackSource

    ''' <summary>
    ''' variable que contiene el mapeo especifo por entidad para realizar la consulta mediante xpo
    ''' </summary>
    Dim classEntity As XPClassInfo


#End Region

#Region "Builders"

    ''' <summary>
    ''' Incializa una nueva instancia de la clase
    ''' </summary>
    Public Sub New(Company As String)
        'verifico que exista el archivo
        ReadConfiguration()

        'establezclo la capa de datos para XPO
        XpoDefault.DataLayer = New SimpleDataLayer(New WCFServiceDataStore(GetEndPoint, GetRemoteAddress, Company))
    End Sub

#End Region

#Region "Private Methods"

    ''' <summary>
    ''' metodo necesario para leer la configuracion xml de la aplicacion
    ''' </summary>
    Private Sub ReadConfiguration()
        uriServiceEntitiesXpo = ConfigurationFile.Instance.UrlXpoWebServer
        'cargo el protocolo
        protocolServicesXpo = ConfigurationFile.Instance.ProtocolUrlXpoWebServer
    End Sub

    ''' <summary>
    ''' funcion para contatenar el nombre del endpoint por cada protocolo
    ''' </summary>
    ''' <returns>El Nombre de la configuracion del Endpoint Correspondiente</returns>
    Private Function GetEndPoint() As String
        Return System.String.Format("{0}_Endpoint", [Enum].GetName(GetType(Protocol), protocolServicesXpo))
    End Function

    ''' <summary>
    ''' funcion para contatenar el remoteaddress por cada protocolo
    ''' </summary>
    ''' <returns>El Nombre del remoteaddress del Endpoint Correspondiente</returns>
    Private Function GetRemoteAddress() As String
        Return System.String.Format("{0}XpoGate.svc/{1}", uriServiceEntitiesXpo, [Enum].GetName(GetType(Protocol), protocolServicesXpo))
    End Function

#End Region

#Region "Public Methods"

#Region "ListPatients"

    Public Function ListPatients() As PLinqInstantFeedbackSource

    End Function

    ''' <summary>
    ''' Funcion para listar todos los pacientes
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAllPatients() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(PatientXpo))
        serverMode = New XPInstantFeedbackSource(classEntity)
        Return serverMode
    End Function

#End Region

#Region "ListAdmissions"

    ''' <summary>
    ''' Objeto de consulta
    ''' </summary>
    Private WithEvents linqListAdmissions As New LinqInstantFeedbackSource

    ''' <summary>
    ''' Lista todos los ingresos que no estan anulados ni cancelados
    ''' </summary>
    Public Function ListAdmissions() As LinqInstantFeedbackSource
        linqListAdmissions.KeyExpression = "AdmissionCode"
        Return linqListAdmissions
    End Function

    ''' <summary>
    ''' Ejecuta y obtiene un conjunto de datos
    ''' </summary>
    Private Sub plinqListAdmissions_GetQueryable(sender As Object, e As GetQueryableEventArgs) Handles linqListAdmissions.GetQueryable
        Try
            Dim sessionNew = New Session(XpoDefault.DataLayer)

            Dim tableAdmissions As XPQuery(Of AdmissionXpo) = New XPQuery(Of AdmissionXpo)(sessionNew)
            Dim tableProfessional As XPQuery(Of HealthCareProfessionalXpo) = New XPQuery(Of HealthCareProfessionalXpo)(sessionNew)
            Dim tablePatient As XPQuery(Of PatientXpo) = New XPQuery(Of PatientXpo)(sessionNew)
            Dim tableEntity As XPQuery(Of EntityXpo) = New XPQuery(Of EntityXpo)(sessionNew)

            'Dim CLOSED As Char = "C" 'Cerrado
            'Dim CANCELLED As Char = "A" 'Anulado

            Dim TmpQueryableSource = From TAdmissions In tableAdmissions
                                     Join TPatient In tablePatient On TAdmissions.IPCODPACI.IPCODPACI Equals TPatient.IPCODPACI
                                     Join TEntity In tableEntity On TAdmissions.CODENTIDA.CODENTIDA Equals TEntity.CODENTIDA
            Where (TAdmissions.IESTADOIN = " " Or TAdmissions.IESTADOIN = "P")
                                     Select New With {.AdmissionCode = TAdmissions.NUMINGRES, _
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
                                                      .EntityCode = TEntity.CODENTIDA, _
                                                      .EntityName = TEntity.NOMENTIDA, _
                                                      .AuthorizationNumber = TAdmissions.IAUTORIZA, _
                                                      .ResponsibleName = TAdmissions.IPRNOMBRE, _
                                                      .ResponsiblePhone = TAdmissions.IPTELEFON, _
                                                      .CareGroupId = TAdmissions.GENCAREGROUP, _
                                                      .HealthAdministratorId = TAdmissions.GENCONENTITY, _
                                                      .FunctionalUnitCode = TAdmissions.UFUCODIGO, _
                                                      .Status = TAdmissions.IESTADOIN, _
                                                      .FullNameAdmission = String.Format(Infrastructure.CrossCutting.Resources.ResourceManager.GetString("AdmissionResume", "IndigoCrystalHis"), TAdmissions.NUMINGRES.ToString().Trim(), TPatient.IPCODPACI.ToString().Trim(), TPatient.IPNOMCOMP.ToString().Trim())}

            e.QueryableSource = TmpQueryableSource
            e.Tag = tableAdmissions
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

#End Region

#Region "ListAllAdmissions"

    ''' <summary>
    ''' Objeto de consulta
    ''' </summary>
    Private WithEvents linqListAllAdmissions As New LinqInstantFeedbackSource

    ''' <summary>
    ''' Lista todos los ingresos que no estan anulados ni cancelados
    ''' </summary>
    Public Function ListAllAdmissions() As LinqInstantFeedbackSource
        linqListAllAdmissions.KeyExpression = "AdmissionCode"
        Return linqListAllAdmissions
    End Function

    ''' <summary>
    ''' Ejecuta y obtiene un conjunto de datos
    ''' </summary>
    Private Sub plinqListAllAdmissions_GetQueryable(sender As Object, e As GetQueryableEventArgs) Handles linqListAllAdmissions.GetQueryable
        Try
            Dim sessionNew = New Session(XpoDefault.DataLayer)

            Dim tableAdmissions As XPQuery(Of AdmissionXpo) = New XPQuery(Of AdmissionXpo)(sessionNew)
            Dim tableProfessional As XPQuery(Of HealthCareProfessionalXpo) = New XPQuery(Of HealthCareProfessionalXpo)(sessionNew)
            Dim tablePatient As XPQuery(Of PatientXpo) = New XPQuery(Of PatientXpo)(sessionNew)
            Dim tableEntity As XPQuery(Of EntityXpo) = New XPQuery(Of EntityXpo)(sessionNew)

            Dim CLOSED As Char = "C" 'Cerrado
            Dim CANCELLED As Char = "A" 'Anulado
            Dim FACTURED As Char = "F" 'Facturado
            Dim PARTIALSTATUS As Char = "P" 'Parcial

            Dim TmpQueryableSource = From TAdmissions In tableAdmissions
                                     Join TPatient In tablePatient On TAdmissions.IPCODPACI.IPCODPACI Equals TPatient.IPCODPACI
                                     Join TEntity In tableEntity On TAdmissions.CODENTIDA.CODENTIDA Equals TEntity.CODENTIDA
            Where (TAdmissions.IESTADOIN = FACTURED Or TAdmissions.IESTADOIN = PARTIALSTATUS)
                                     Select New With {.AdmissionCode = TAdmissions.NUMINGRES, _
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
                                                      .EntityCode = TEntity.CODENTIDA, _
                                                      .EntityName = TEntity.NOMENTIDA, _
                                                      .AuthorizationNumber = TAdmissions.IAUTORIZA, _
                                                      .ResponsibleName = TAdmissions.IPRNOMBRE, _
                                                      .ResponsiblePhone = TAdmissions.IPTELEFON, _
                                                      .CareGroupId = TAdmissions.GENCAREGROUP, _
                                                      .HealthAdministratorId = TAdmissions.GENCONENTITY, _
                                                      .FunctionalUnitCode = TAdmissions.UFUCODIGO, _
                                                      .Status = TAdmissions.IESTADOIN, _
                                                      .StatusName = If(TAdmissions.IESTADOIN = "F", "Facturado", "Parcialmente Facturado"), _
                                                      .FullNameAdmission = String.Format(Infrastructure.CrossCutting.Resources.ResourceManager.GetString("AdmissionResume", "IndigoCrystalHis"), TAdmissions.NUMINGRES.ToString().Trim(), TPatient.IPCODPACI.ToString().Trim(), TPatient.IPNOMCOMP.ToString().Trim())}

            e.QueryableSource = TmpQueryableSource
            e.Tag = tableAdmissions
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

#End Region

#Region "AccountControl"
    Public Function ListMedicineSupplierByPatientCodeIngreso(patientCode As String, admissionNumber As String) As XPCollection(Of ViewMedicinesSupplies)
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("CodigoPaciente = '" & patientCode & "' AND Ingreso = '" & admissionNumber & "'")
        Dim collect As XPCollection(Of ViewMedicinesSupplies) = New XPCollection(Of ViewMedicinesSupplies)(sessionNew, criteria)
        Return collect
    End Function

    Public Function ListKardexMedicineSupplierByPatientCodeIngreso(patientCode As String, admissionNumber As String) As XPCollection(Of ViewKardexMedicineSupplier)
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("CodigoPaciente = '" & patientCode & "' AND Ingreso = '" & admissionNumber & "' AND Origen > 0")
        Dim collect As XPCollection(Of ViewKardexMedicineSupplier) = New XPCollection(Of ViewKardexMedicineSupplier)(sessionNew, criteria)
        Return collect
    End Function

    Public Function ListKardexMedicineSupplierByCodePatientCodeIngreso(code As String, patientCode As String, admissionNumber As String) As XPCollection(Of ViewKardexMedicineSupplier)
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Codigo = '" & code & "' AND CodigoPaciente = '" & patientCode & "' AND Ingreso = '" & admissionNumber & "' AND Origen > 0")
        Dim collect As XPCollection(Of ViewKardexMedicineSupplier) = New XPCollection(Of ViewKardexMedicineSupplier)(sessionNew, criteria)
        Return collect
    End Function

    Public Function ListLaboratories(patientCode As String, admissionNumber As String) As XPCollection(Of ViewLaboratories)
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("IPCODPACI = '" & patientCode & "' AND NUMINGRES = '" & admissionNumber & "'")
        Dim collect As XPCollection(Of ViewLaboratories) = New XPCollection(Of ViewLaboratories)(sessionNew, criteria)
        Return collect
    End Function

    Public Function ListPathologiesByPatientCodeIngreso(patientCode As String, admissionNumber As String) As XPCollection(Of ViewPathologies)
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("IPCODPACI = '" & patientCode & "' AND NUMINGRES = '" & admissionNumber & "'")
        Dim collect As XPCollection(Of ViewPathologies) = New XPCollection(Of ViewPathologies)(sessionNew, criteria)
        Return collect
    End Function

    Public Function ListImagesDxByPatientCodeIngreso(patientCode As String, admissionNumber As String) As XPCollection(Of ViewImagesDX)
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("IPCODPACI = '" & patientCode & "' AND NUMINGRES = '" & admissionNumber & "'")
        Dim collect As XPCollection(Of ViewImagesDX) = New XPCollection(Of ViewImagesDX)(sessionNew, criteria)
        Return collect
    End Function

    Public Function ListProceduresQxByPatientCodeIngreso(patientCode As String, admissionNumber As String) As XPCollection(Of ViewProceduresQx)
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("IPCODPACI = '" & patientCode & "' AND NUMINGRES = '" & admissionNumber & "'")
        Dim collect As XPCollection(Of ViewProceduresQx) = New XPCollection(Of ViewProceduresQx)(sessionNew, criteria)
        Return collect
    End Function

    Public Function ListProceduresNoQxByPatientCodeIngreso(patientCode As String, admissionNumber As String) As XPCollection(Of ViewProceduresNoQx)
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("IPCODPACI = '" & patientCode & "' AND NUMINGRES = '" & admissionNumber & "'")
        Dim collect As XPCollection(Of ViewProceduresNoQx) = New XPCollection(Of ViewProceduresNoQx)(sessionNew, criteria)
        Return collect
    End Function

    Public Function ListServicesProceduresLaboratoriesByadmissionCodeIngreso(patientCode As String, admissionNumber As String) As XPCollection(Of ViewServicesProceduresLaboratories)
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("IPCODPACI = '" & patientCode & "' AND NUMINGRES = '" & admissionNumber & "'")
        Dim collect As XPCollection(Of ViewServicesProceduresLaboratories) = New XPCollection(Of ViewServicesProceduresLaboratories)(sessionNew, criteria)
        Return collect
    End Function

    Function ListServicesProceduresPathologiesByadmissionCodeIngreso(patientCode As String, admissionNumber As String) As XPCollection(Of ViewServicesProceduresPathologies)
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("IPCODPACI = '" & patientCode & "' AND NUMINGRES = '" & admissionNumber & "'")
        Dim collect As XPCollection(Of ViewServicesProceduresPathologies) = New XPCollection(Of ViewServicesProceduresPathologies)(sessionNew, criteria)
        Return collect
    End Function

    Function ListServicesProceduresImagesDxByadmissionCodeIngreso(patientCode As String, admissionNumber As String) As XPCollection(Of ViewServicesProceduresImagesDx)
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("IPCODPACI = '" & patientCode & "' AND NUMINGRES = '" & admissionNumber & "'")
        Dim collect As XPCollection(Of ViewServicesProceduresImagesDx) = New XPCollection(Of ViewServicesProceduresImagesDx)(sessionNew, criteria)
        Return collect
    End Function

    Function ListServicesProceduresQxByadmissionCodeIngreso(patientCode As String, admissionNumber As String) As XPCollection(Of ViewServicesProceduresQx)
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("IPCODPACI = '" & patientCode & "' AND NUMINGRES = '" & admissionNumber & "'")
        Dim collect As XPCollection(Of ViewServicesProceduresQx) = New XPCollection(Of ViewServicesProceduresQx)(sessionNew, criteria)
        Return collect
    End Function

    Function ListServicesProceduresReportQxByadmissionCodeIngreso(patientCode As String, admissionNumber As String) As XPCollection(Of ViewServicesProceduresReportQx)
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("IPCODPACI = '" & patientCode & "' AND NUMINGRES = '" & admissionNumber & "'")
        Dim collect As XPCollection(Of ViewServicesProceduresReportQx) = New XPCollection(Of ViewServicesProceduresReportQx)(sessionNew, criteria)
        Return collect
    End Function

    Function ListServicesProceduresNoQxByadmissionCodeIngreso(patientCode As String, admissionNumber As String) As XPCollection(Of ViewServicesProceduresNoQx)
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("IPCODPACI = '" & patientCode & "' AND NUMINGRES = '" & admissionNumber & "'")
        Dim collect As XPCollection(Of ViewServicesProceduresNoQx) = New XPCollection(Of ViewServicesProceduresNoQx)(sessionNew, criteria)
        Return collect
    End Function

    Public Function ListConsultationByPatientCodeIngreso(patientCode As String, admissionNumber As String) As XPCollection(Of ViewConsultation)
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("IPCODPACI = '" & patientCode & "' AND NUMINGRES = '" & admissionNumber & "'")
        Dim collect As XPCollection(Of ViewConsultation) = New XPCollection(Of ViewConsultation)(sessionNew, criteria)
        Return collect
    End Function

    Function ListServicesProceduresConsultationByadmissionCodeIngreso(patientCode As String, admissionNumber As String) As XPCollection(Of ViewServicesProceduresConsultation)
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("IPCODPACI = '" & patientCode & "' AND NUMINGRES = '" & admissionNumber & "'")
        Dim collect As XPCollection(Of ViewServicesProceduresConsultation) = New XPCollection(Of ViewServicesProceduresConsultation)(sessionNew, criteria)
        Return collect
    End Function

    Public Function ListTherapyByPatientCodeIngreso(patientCode As String, admissionNumber As String) As XPCollection(Of ViewTherapy)
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("IPCODPACI = '" & patientCode & "' AND NUMINGRES = '" & admissionNumber & "'")
        Dim collect As XPCollection(Of ViewTherapy) = New XPCollection(Of ViewTherapy)(sessionNew, criteria)
        Return collect
    End Function

    Function ListServicesProceduresTherapyByadmissionCodeIngreso(patientCode As String, admissionNumber As String) As XPCollection(Of ViewServicesProceduresTherapy)
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("IPCODPACI = '" & patientCode & "' AND NUMINGRES = '" & admissionNumber & "'")
        Dim collect As XPCollection(Of ViewServicesProceduresTherapy) = New XPCollection(Of ViewServicesProceduresTherapy)(sessionNew, criteria)
        Return collect
    End Function

    Function ListOxygenConsumptionByadmissionCodeIngreso(patientCode As String, admissionNumber As String) As XPCollection(Of ViewOxygenConsumption)
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("IPCODPACI = '" & patientCode & "' AND NUMINGRES = '" & admissionNumber & "'")
        Dim collect As XPCollection(Of ViewOxygenConsumption) = New XPCollection(Of ViewOxygenConsumption)(sessionNew, criteria)
        Return collect
    End Function

    Function ListNursingProceduresByadmissionCodeIngresoCodCenAte(patientCode As String, admissionNumber As String, atentionCenter As String) As XPCollection(Of ViewNursingProcedures)
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("IPCODPACI = '" & patientCode & "' AND NUMINGRES = '" & admissionNumber & "' AND CODCENATE = '" & atentionCenter & "'")
        Dim collect As XPCollection(Of ViewNursingProcedures) = New XPCollection(Of ViewNursingProcedures)(sessionNew, criteria)
        Return collect
    End Function

    Function ListNursingProceduresDetailByadmissionCodeIngresoCodCenAte(patientCode As String, admissionNumber As String, atentionCenter As String) As XPCollection(Of ViewNursingProceduresDetail)
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("IPCODPACI = '" & patientCode & "' AND NUMINGRES = '" & admissionNumber & "' AND CODCENATE = '" & atentionCenter & "'")
        Dim collect As XPCollection(Of ViewNursingProceduresDetail) = New XPCollection(Of ViewNursingProceduresDetail)(sessionNew, criteria)
        Return collect
    End Function

    Function ListServicesNursingProceduresDetailByadmissionCodeIngresoCodCenAte(patientCode As String, admissionNumber As String, atentionCenter As String) As XPCollection(Of ViewServiceProceduresNursingProcedure)
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("IPCODPACI = '" & patientCode & "' AND NUMINGRES = '" & admissionNumber & "' AND CODCENATE = '" & atentionCenter & "'")
        Dim collect As XPCollection(Of ViewServiceProceduresNursingProcedure) = New XPCollection(Of ViewServiceProceduresNursingProcedure)(sessionNew, criteria)
        Return collect
    End Function

    Function ListReviewsByadmissionCodeIngreso(patientCode As String, admissionNumber As String) As XPCollection(Of ViewReviews)
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("IPCODPACI = '" & patientCode & "' AND NUMINGRES = '" & admissionNumber & "'")
        Dim collect As XPCollection(Of ViewReviews) = New XPCollection(Of ViewReviews)(sessionNew, criteria)
        Return collect
    End Function

#End Region

#Region "ListAdmissionsByPatientCode"

    ''' <summary>
    ''' Objeto de consulta
    ''' </summary>
    Private WithEvents linqListAdmissionsPatientCode As New LinqInstantFeedbackSource
    Private _patientCode As String
    ''' <summary>
    ''' Lista todos los ingresos que no estan anulados ni cancelados
    ''' </summary>
    Public Function ListAdmissionsPatientCode(PatientCode As String) As LinqInstantFeedbackSource
        linqListAdmissionsPatientCode.KeyExpression = "AdmissionCode"
        _patientCode = PatientCode
        Return linqListAdmissionsPatientCode
    End Function

    ''' <summary>
    ''' Ejecuta y obtiene un conjunto de datos
    ''' </summary>
    Private Sub plinqListAdmissionsPatientCode_GetQueryable(sender As Object, e As GetQueryableEventArgs) Handles linqListAdmissionsPatientCode.GetQueryable
        Try
            Dim sessionNew = New Session(XpoDefault.DataLayer)

            Dim tableAdmissions As XPQuery(Of AdmissionXpo) = New XPQuery(Of AdmissionXpo)(sessionNew)
            Dim tableProfessional As XPQuery(Of HealthCareProfessionalXpo) = New XPQuery(Of HealthCareProfessionalXpo)(sessionNew)
            Dim tablePatient As XPQuery(Of PatientXpo) = New XPQuery(Of PatientXpo)(sessionNew)
            Dim tableEntity As XPQuery(Of EntityXpo) = New XPQuery(Of EntityXpo)(sessionNew)

            Dim CLOSED As Char = "C" 'Cerrado
            Dim CANCELLED As Char = "A" 'Anulado

            Dim TmpQueryableSource = From TAdmissions In tableAdmissions
                                     Join TPatient In tablePatient On TAdmissions.IPCODPACI.IPCODPACI Equals TPatient.IPCODPACI
                                     Join TEntity In tableEntity On TAdmissions.CODENTIDA.CODENTIDA Equals TEntity.CODENTIDA
            Where (TAdmissions.IESTADOIN <> CLOSED And TAdmissions.IESTADOIN <> CANCELLED And TAdmissions.IESTADOIN = " " And TPatient.IPCODPACI = _patientCode)
                                     Select New With {.AdmissionCode = TAdmissions.NUMINGRES, _
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
                                                      .EntityCode = TEntity.CODENTIDA, _
                                                      .EntityName = TEntity.NOMENTIDA, _
                                                      .AuthorizationNumber = TAdmissions.IAUTORIZA, _
                                                      .ResponsibleName = TAdmissions.IPRNOMBRE, _
                                                      .ResponsiblePhone = TAdmissions.IPTELEFON, _
                                                      .CareGroupId = TAdmissions.GENCAREGROUP, _
                                                      .HealthAdministratorId = TAdmissions.GENCONENTITY, _
                                                      .Status = TAdmissions.IESTADOIN, _
                                                      .FullNameAdmission = String.Format(Infrastructure.CrossCutting.Resources.ResourceManager.GetString("AdmissionResume", "IndigoCrystalHis"), TAdmissions.NUMINGRES.ToString().Trim(), TPatient.IPCODPACI.ToString().Trim(), TPatient.IPNOMCOMP.ToString().Trim())}

            e.QueryableSource = TmpQueryableSource
            e.Tag = tableAdmissions
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

#End Region

#Region "ListAdmissionsByPatientCodeStatusAccidenteTransito"

    ''' <summary>
    ''' Objeto de consulta
    ''' </summary>
    Private WithEvents linqListAdmissionsPatientCodeStatus As New LinqInstantFeedbackSource
    Private _status As String
    ''' <summary>
    ''' Lista todos los ingresos que no estan anulados ni cancelados
    ''' </summary>
    Public Function ListAdmissionsPatientCodeStatus(PatientCode As String, status As String) As LinqInstantFeedbackSource
        linqListAdmissionsPatientCodeStatus.KeyExpression = "AdmissionCode"
        _patientCode = PatientCode
        _status = status
        Return linqListAdmissionsPatientCodeStatus
    End Function

    ''' <summary>
    ''' Ejecuta y obtiene un conjunto de datos
    ''' </summary>
    Private Sub plinqListAdmissionsPatientCodeStatus_GetQueryable(sender As Object, e As GetQueryableEventArgs) Handles linqListAdmissionsPatientCodeStatus.GetQueryable
        Try
            Dim sessionNew = New Session(XpoDefault.DataLayer)

            Dim tableAdmissions As XPQuery(Of AdmissionXpo) = New XPQuery(Of AdmissionXpo)(sessionNew)
            Dim tableProfessional As XPQuery(Of HealthCareProfessionalXpo) = New XPQuery(Of HealthCareProfessionalXpo)(sessionNew)
            Dim tablePatient As XPQuery(Of PatientXpo) = New XPQuery(Of PatientXpo)(sessionNew)
            Dim tableEntity As XPQuery(Of EntityXpo) = New XPQuery(Of EntityXpo)(sessionNew)

            Dim FACTURED As String = _status 'Facturado
            Dim Dos As Integer = 2

            Dim TmpQueryableSource = From TAdmissions In tableAdmissions
                                     Join TPatient In tablePatient On TAdmissions.IPCODPACI.IPCODPACI Equals TPatient.IPCODPACI
                                     Join TEntity In tableEntity On TAdmissions.CODENTIDA.CODENTIDA Equals TEntity.CODENTIDA
            Where (TAdmissions.IESTADOIN = FACTURED AndAlso TPatient.IPCODPACI = _patientCode AndAlso TAdmissions.ITIPORIES = Dos)
                                     Select New With {.AdmissionCode = TAdmissions.NUMINGRES, _
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
                                                      .EntityCode = TEntity.CODENTIDA, _
                                                      .EntityName = TEntity.NOMENTIDA, _
                                                      .AuthorizationNumber = TAdmissions.IAUTORIZA, _
                                                      .ResponsibleName = TAdmissions.IPRNOMBRE, _
                                                      .ResponsiblePhone = TAdmissions.IPTELEFON, _
                                                      .CareGroupId = TAdmissions.GENCAREGROUP, _
                                                      .HealthAdministratorId = TAdmissions.GENCONENTITY, _
                                                      .Status = TAdmissions.IESTADOIN, _
                                                      .FullNameAdmission = String.Format(Infrastructure.CrossCutting.Resources.ResourceManager.GetString("AdmissionResume", "IndigoCrystalHis"), TAdmissions.NUMINGRES.ToString().Trim(), TPatient.IPCODPACI.ToString().Trim(), TPatient.IPNOMCOMP.ToString().Trim())}

            e.QueryableSource = TmpQueryableSource
            e.Tag = tableAdmissions
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

#End Region

#Region "ListAdmissionsByPatientCodeStatus"

    ''' <summary>
    ''' Objeto de consulta
    ''' </summary>
    Private WithEvents linqListAdmissionsByPatientCodeStatus As New LinqInstantFeedbackSource
    ''' <summary>
    ''' Lista todos los ingresos que no estan anulados ni cancelados
    ''' </summary>
    Public Function ListAdmissionsByPatientCodeStatus(PatientCode As String, status As String) As LinqInstantFeedbackSource
        linqListAdmissionsByPatientCodeStatus.KeyExpression = "AdmissionCode"
        _patientCode = PatientCode
        _status = status
        Return linqListAdmissionsByPatientCodeStatus
    End Function

    ''' <summary>
    ''' Ejecuta y obtiene un conjunto de datos
    ''' </summary>
    Private Sub plinqListAdmissionsByPatientCodeStatus_GetQueryable(sender As Object, e As GetQueryableEventArgs) Handles linqListAdmissionsByPatientCodeStatus.GetQueryable
        Try
            Dim sessionNew = New Session(XpoDefault.DataLayer)

            Dim tableAdmissions As XPQuery(Of AdmissionXpo) = New XPQuery(Of AdmissionXpo)(sessionNew)
            Dim tableProfessional As XPQuery(Of HealthCareProfessionalXpo) = New XPQuery(Of HealthCareProfessionalXpo)(sessionNew)
            Dim tablePatient As XPQuery(Of PatientXpo) = New XPQuery(Of PatientXpo)(sessionNew)
            Dim tableEntity As XPQuery(Of EntityXpo) = New XPQuery(Of EntityXpo)(sessionNew)

            Dim TmpQueryableSource = From TAdmissions In tableAdmissions
                                     Join TPatient In tablePatient On TAdmissions.IPCODPACI.IPCODPACI Equals TPatient.IPCODPACI
                                     Join TEntity In tableEntity On TAdmissions.CODENTIDA.CODENTIDA Equals TEntity.CODENTIDA
            Where (TAdmissions.IESTADOIN = _status AndAlso TPatient.IPCODPACI = _patientCode)
                                     Select New With {.AdmissionCode = TAdmissions.NUMINGRES, _
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
                                                      .EntityCode = TEntity.CODENTIDA, _
                                                      .EntityName = TEntity.NOMENTIDA, _
                                                      .AuthorizationNumber = TAdmissions.IAUTORIZA, _
                                                      .ResponsibleName = TAdmissions.IPRNOMBRE, _
                                                      .ResponsiblePhone = TAdmissions.IPTELEFON, _
                                                      .CareGroupId = TAdmissions.GENCAREGROUP, _
                                                      .HealthAdministratorId = TAdmissions.GENCONENTITY, _
                                                      .Status = TAdmissions.IESTADOIN, _
                                                      .FullNameAdmission = String.Format(Infrastructure.CrossCutting.Resources.ResourceManager.GetString("AdmissionResume", "IndigoCrystalHis"), TAdmissions.NUMINGRES.ToString().Trim(), TPatient.IPCODPACI.ToString().Trim(), TPatient.IPNOMCOMP.ToString().Trim())}

            e.QueryableSource = TmpQueryableSource
            e.Tag = tableAdmissions
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

#End Region

#Region "ListAdmissions Liquidation Linq"

    ''' <summary>
    ''' Objeto de consulta
    ''' </summary>
    Private WithEvents linqListAdmissionsLiquidation As New LinqInstantFeedbackSource

    ''' <summary>
    ''' Lista todos los ingresos para el frontal de liquidacion
    ''' </summary>
    Public Function ListAdmissionsLiquidation() As LinqInstantFeedbackSource
        linqListAdmissionsLiquidation.KeyExpression = "AdmissionCode"
        Return linqListAdmissionsLiquidation
    End Function

    ''' <summary>
    ''' Ejecuta y obtiene un conjunto de datos
    ''' </summary>
    Private Sub plinqListAdmissionsLiquidation_GetQueryable(sender As Object, e As GetQueryableEventArgs) Handles linqListAdmissionsLiquidation.GetQueryable
        Try
            Dim sessionNew = New Session(XpoDefault.DataLayer)

            Dim tableAdmissions As XPQuery(Of AdmissionXpo) = New XPQuery(Of AdmissionXpo)(sessionNew)
            Dim tablePatient As XPQuery(Of PatientXpo) = New XPQuery(Of PatientXpo)(sessionNew)

            Dim TmpQueryableSource = From TAdmissions In tableAdmissions
                                     Join TPatient In tablePatient On TAdmissions.IPCODPACI.IPCODPACI Equals TPatient.IPCODPACI
                                     Select New With {.AdmissionCode = TAdmissions.NUMINGRES.Trim(), _
                                                      .PatientCode = TPatient.IPCODPACI, _
                                                      .PatientName = TPatient.IPNOMCOMP, _
                                                      .AdmissionType = TAdmissions.TIPOINGRE, _
                                                      .BedStay = TAdmissions.CODICAMHO, _
                                                      .LiquidationType = TAdmissions.ILIQUIDAC, _
                                                      .ResponsibleName = TAdmissions.IPRNOMBRE}

            e.QueryableSource = TmpQueryableSource
            e.Tag = tableAdmissions
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

#End Region

#Region "ListAdmissions Liquidation XPI"

    ''' <summary>
    ''' Lista todos los ingresos para el frontal de boleta de salida
    ''' </summary>
    Public Function ListAdmissionsToReportSlipOut() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(ViewAdmissionsToReportSlipOut))
        serverMode = New XPInstantFeedbackSource(classEntity, Nothing, Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los ingresos para el frontal de liquidacion
    ''' </summary>
    Public Function ListAdmissionsToReport() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(ViewAdmissionsToReport))
        serverMode = New XPInstantFeedbackSource(classEntity, Nothing, Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los ingresos para el frontal de listado de facturas
    ''' </summary>
    Public Function ListAdmissionsToLiquidation() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(ViewAdmissionsToLiquidation))
        serverMode = New XPInstantFeedbackSource(classEntity, Nothing, Nothing)
        Return serverMode
    End Function

    Public Function ListAdmissionsToLiquidationConfirm() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(ViewAdmissionsToLiquidationConfirm))
        serverMode = New XPInstantFeedbackSource(classEntity, Nothing, Nothing)
        Return serverMode
    End Function

    Public Function GetAdmissionObjectByNumIngres(admissionnumber As String) As XPCollection(Of ViewAdmissionsToLiquidation)
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("AdmissionCode='" & admissionnumber & "'")
        Dim collect As XPCollection(Of ViewAdmissionsToLiquidation) = New XPCollection(Of ViewAdmissionsToLiquidation)(sessionNew, criteria)
        Return collect
    End Function

#End Region

#Region "ViewListFuncionalUnitAuthorization Lista de Unidades Funcionales por permisos de usuario y grupos"

    ''' <summary>
    ''' Lista las unidades funcionales por permiso de usuario
    ''' </summary>
    Public Function ListViewListFuncionalUnitAuthorization(ByVal CodeGroup As String, ByVal CodeUsers As String) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Codeusers='" & CodeUsers & "' or CodeGroup='" & CodeGroup & "'")
        classEntity = sessionNew.GetClassInfo(GetType(ViewListFuncionalUnitAuthorization))
        serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function

#End Region

#Region "ListAdmissionByPatientCode"
    Public Function ListAdmissionsByPatientCode(patientCode As String) As XPInstantFeedbackSource
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("[PatientCode] = '" & patientCode & "'")
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(ViewAdmissionsToLiquidation))
        serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function
#End Region

#Region "ListHealthCareProfessional"
    ''' <summary>
    ''' lista Profesionales de la salud activos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListHealthCareProfessionalAll() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(HealthCareProfessionalXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "CODPROSAL;NOMMEDICO;CODESPEC1;CODESPEC1.DESESPECI;StatusName", Nothing)
        Return serverMode
    End Function
    ''' <summary>
    ''' lista Profesionales de la salud activos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListHealthCareProfessional() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("ESTADOMED=1")
        classEntity = sessionNew.GetClassInfo(GetType(HealthCareProfessionalXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function
    ''' <summary>
    ''' Lists the health care professional by profile.
    ''' </summary>
    ''' <param name="profile">The profile.</param>
    ''' <returns></returns>
    Public Function ListHealthCareProfessionalByProfile(profile As List(Of Integer)) As XPInstantFeedbackSource
        Dim InOperat = String.Join(",", profile.ToArray())
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("ESTADOMED=1 And MEDPERCIR In (" & InOperat & ")")
        classEntity = sessionNew.GetClassInfo(GetType(HealthCareProfessionalXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function
    ''' <summary>
    ''' lista todos los Profesionales de la salud con xpCollection
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListHealthCareProfessionalXpCollection() As XPCollection(Of HealthCareProfessionalXpo)
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("ESTADOMED=1")
        Dim collect As XPCollection(Of HealthCareProfessionalXpo) = New XPCollection(Of HealthCareProfessionalXpo)(sessionNew, criteria)
        Return collect
    End Function

    ''' <summary>
    ''' lista todos los Profesionales de la salud con xpCollection filtrado por la especialidad
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListHealthCareProfessionalSpecialtyXpCollection() As XPCollection(Of HealthCareProfessionalXpo)
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("CODESPEC1.CODESPECI='005' Or CODESPEC2.CODESPECI='002' Or CODESPEC3.CODESPECI='002'")
        Dim collect As XPCollection(Of HealthCareProfessionalXpo) = New XPCollection(Of HealthCareProfessionalXpo)(sessionNew, criteria)
        Return collect
    End Function

    ''' <summary>
    ''' lista todos los Profesionales de la salud
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCareProfessionalByCode(code As String) As XPCollection(Of HealthCareProfessionalXpo)
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("CODPROSAL='" & code & "'")
        Dim collect As XPCollection(Of HealthCareProfessionalXpo) = New XPCollection(Of HealthCareProfessionalXpo)(sessionNew, criteria)
        Return collect
    End Function

#End Region

#Region "ListSpecialtyByStatus"

    ''' <summary>
    ''' lista las especialidades por estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListSpecialty(ByVal status As Boolean) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("ESTADO=" & status)
        classEntity = sessionNew.GetClassInfo(GetType(SpecialtyXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "CODESPECI;DESESPECI;TIPATENCI;INDAUDFOR;ESTADO;CodeName", criteria)
        Return serverMode
    End Function

    Public Function ListSpecialtyXpCollection(ByVal status As Boolean) As XPCollection
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("ESTADO=" & status)
        Dim collect As XPCollection = New XPCollection(sessionNew, GetType(SpecialtyXpo), criteria)
        Return collect
    End Function

    ''' <summary>
    ''' lista las especialidades por estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListSpecialties(ByVal status As Boolean) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("ESTADO=" & status)
        classEntity = sessionNew.GetClassInfo(GetType(SpecialtyXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "CODESPECI;DESESPECI;TIPATENCI;INDAUDFOR;ESTADO;CodeName", criteria)
        Return serverMode
    End Function

#End Region

#Region "ListActivity"

    ''' <summary>
    ''' lista las Actividades
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListActivities() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(ActivitiesXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "codactivi;desactivi;INDAUDFOR", Nothing)
        Return serverMode
    End Function

#End Region

#Region "ListLocations"

    ''' <summary>
    ''' lista las ubicaciones
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListLocations() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(LocationXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "AUUBICACI;UBICODIGO;UBINOMBRE;DEPMUNCOD.DEPMUNCOD;DEPMUNCOD.MUNNOMBRE;DEPMUNCOD.DEPCODIGO.depcodigo;DEPMUNCOD.DEPCODIGO.nomdepart", Nothing)
        Return serverMode
    End Function

#End Region

#Region "ListCompany"

    ''' <summary>
    ''' lista las empresas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListCompany() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(CompanyXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "CODEMPRES;DESEMPRES", Nothing)
        Return serverMode
    End Function

#End Region

#Region "ListEthnicGroup"

    ''' <summary>
    ''' lista los grupos étnicos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListEthnicGroup() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(EthnicGroupXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "CODGRUPOE;DESGRUPET", Nothing)
        Return serverMode
    End Function

#End Region

#Region "ListEducationLevels"

    ''' <summary>
    ''' lista los niveles de educacion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListEducationLevels() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(EducationLevelsXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "NIVECODIGO;NIVEDESCRI", Nothing)
        Return serverMode
    End Function

#End Region

#Region "ListLevels"

    ''' <summary>
    ''' lista los niveles
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListLevels() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(LevelsXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "NIVCODIGO;NIVDESCRI", Nothing)
        Return serverMode
    End Function

#End Region

#Region "ListLanguage"

    ''' <summary>
    ''' lista los lenguajes
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListLanguage() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(LanguageXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "IDICODIGO;IDIDESCRI", Nothing)
        Return serverMode
    End Function

#End Region

#Region "ListBelief"

    ''' <summary>
    ''' lista las creencias
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListBelief() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(BeliefXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "CREDCODIGO;CREDDESCRI", Nothing)
        Return serverMode
    End Function

#End Region

#Region "ListSpecialGroups"

    ''' <summary>
    ''' lista los grupos especiales
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListSpecialGroups() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(SpecialGroupsXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "GRUPCODIGO;GRUPDESCRI", Nothing)
        Return serverMode
    End Function

#End Region

#Region "ListDisability"

    ''' <summary>
    ''' lista las incapacidades
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListDisability() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(DisabilidyXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "DISCCODIGO;DISCDESCRI", Nothing)
        Return serverMode
    End Function

#End Region

#Region "ListCenters"
    ''' <summary>
    ''' Lista de centros de atención
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListCenters() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(CentersXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "CODCENATE;NOMCENATE;CODIPSSEC;NIVATENCI;DIRCENATE;INDNUMTEL;DEPMUNCOD;CodeName", Nothing)
        Return serverMode
    End Function
#End Region

    ''' <summary>
    ''' Lista los medicos que tengan asociado el contrato
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListHealthProfessionalCodeByMedicalFeesContractId(medicalFeesContractId As Integer) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("GENCONTRA=" & medicalFeesContractId & "")
        classEntity = sessionNew.GetClassInfo(GetType(HealthCareProfessionalXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "CodeName;CODPROSAL;NOMMEDICO;GENCONTRA", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Gets the bed rate by bed identifier.
    ''' </summary>
    ''' <param name="bedId">The bed identifier.</param>
    ''' <returns></returns>
    Public Function GetBedRateByBedId(bedId As Integer) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("CODICAMAS.CODICAMAS=" & bedId)
        classEntity = sessionNew.GetClassInfo(GetType(CHGENTARI))
        serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista los tipos de estancia
    ''' </summary>
    ''' <returns></returns>
    Public Function GetAllStayType() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(CHTIPESTA))
        serverMode = New XPInstantFeedbackSource(classEntity, Nothing, Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los centros de atencion
    ''' </summary>
    ''' <returns></returns>
    Public Function GetAllCareCenter() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(ADCENATEN))
        serverMode = New XPInstantFeedbackSource(classEntity, Nothing, Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todas las unidades funcionales
    ''' </summary>
    ''' <returns></returns>
    Public Function GetAllFunctionalUnit() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(INUNIFUNC))
        serverMode = New XPInstantFeedbackSource(classEntity, Nothing, Nothing)
        Return serverMode
    End Function

#Region "ListDashBoardPharmacy"
    ''' <summary>
    ''' lista todos los Profesionales de la salud
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListDashBoardPharmacy(codeCareCenter As String) As XPCollection(Of ViewDashBoardPharmacy)
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("CodigoCentroAtencion='" & codeCareCenter & "'")
        Dim collect As XPCollection(Of ViewDashBoardPharmacy) = New XPCollection(Of ViewDashBoardPharmacy)(sessionNew, criteria)
        collect.Sorting.Add(New SortProperty("FechaOrden", DevExpress.Xpo.DB.SortingDirection.Ascending))
        Return collect
    End Function
#End Region

#Region "ListDashBoardPharmacyDevolution"
    ''' <summary>
    ''' lista todos los Profesionales de la salud
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListDashBoardPharmacyDevolution(codeCareCenter As String) As XPCollection(Of ViewDashBoardPharmacyDevolution)
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("CodigoCentroAtencion='" & codeCareCenter & "'")
        Dim collect As XPCollection(Of ViewDashBoardPharmacyDevolution) = New XPCollection(Of ViewDashBoardPharmacyDevolution)(sessionNew, criteria)
        collect.Sorting.Add(New SortProperty("FechaDevolucion", DevExpress.Xpo.DB.SortingDirection.Ascending))
        Return collect
    End Function
#End Region

#Region "GetAdmissionByCodeAdmission"

    ''' <summary>
    ''' lista todos un numero de admision por codigo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAdmissionsToReportSlipOutById(AdmissionCode As String) As XPCollection(Of ViewAdmissionsToReportSlipOut)
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("AdmissionCode ='" & AdmissionCode & "'")
        Dim collect As XPCollection(Of ViewAdmissionsToReportSlipOut) = New XPCollection(Of ViewAdmissionsToReportSlipOut)(sessionNew, criteria)
        Return collect
    End Function

    ''' <summary>
    ''' lista todos un numero de admision por codigo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAdmissionsToReportById(AdmissionCode As String) As XPCollection(Of ViewAdmissionsToReport)
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("AdmissionCode ='" & AdmissionCode & "'")
        Dim collect As XPCollection(Of ViewAdmissionsToReport) = New XPCollection(Of ViewAdmissionsToReport)(sessionNew, criteria)
        Return collect
    End Function

    '' <summary>
    ''' lista todos un numero de admision por codigo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAdmissionsToReport(AdmissionCode As String) As XPCollection(Of VAdmission)
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("NUMEROINGRESO ='" & AdmissionCode & "'")
        Dim collect As XPCollection(Of VAdmission) = New XPCollection(Of VAdmission)(sessionNew, criteria)
        collect.Sorting.Add(New SortProperty("FECHAHOSPITALIZACION", DevExpress.Xpo.DB.SortingDirection.Descending))
        Return collect
    End Function

#End Region

#Region "ListTown"
    ''' <summary>
    ''' Lista de centros de atención
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListTown() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(TownXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "DEPMUNCOD;MUNCODIGO;MUNNOMBRE;DEPCODIGO.depcodigo;DEPCODIGO.nomdepart;CodeName", Nothing)
        Return serverMode
    End Function
#End Region

#Region "ListIPS"
    ''' <summary>
    ''' Lista de centros de atención
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListIPS() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(ADCONTIPS))
        serverMode = New XPInstantFeedbackSource(classEntity, "CODIGOIPS;DSCRIPIPS;CodeName", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista las IPS por estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListIPSByStatus(status As Boolean) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("ESTADO=" & status)
        classEntity = sessionNew.GetClassInfo(GetType(ADCONTIPS))
        serverMode = New XPInstantFeedbackSource(classEntity, "CODIGOIPS;DSCRIPIPS;ESTADO;CodeName", criteria)
        Return serverMode
    End Function
#End Region

#Region "CUPS"
    ''' <summary>
    ''' Lista las IPS por estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListCUPSByStatus(status As Boolean) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("SIPSESTADO=" & status)
        classEntity = sessionNew.GetClassInfo(GetType(CUPSXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "CODSERIPS;DESSERIPS;SIPSESTADO;TIPSERIPS;CodeName", criteria)
        Return serverMode
    End Function

    Function ListCUPSCrystalByStatusType(status As Boolean, type As Integer) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("SIPSESTADO=" & status & " AND TIPSERIPS=" & type)
        classEntity = sessionNew.GetClassInfo(GetType(CUPSXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "CODSERIPS;DESSERIPS;SIPSESTADO;CodeName", criteria)
        Return serverMode
    End Function
#End Region

#Region "ListUF"
    ''' <summary>
    ''' Lista de centros de atención
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListUF() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(INUNIFUNC))
        serverMode = New XPInstantFeedbackSource(classEntity, "UFUCODIGO;UFUDESCRI;CodeName", Nothing)
        Return serverMode
    End Function
#End Region

#Region "ListMinWage"
    ''' <summary>
    ''' Lista de centros de atención
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListMinWage() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(INSALARIM))
        serverMode = New XPInstantFeedbackSource(classEntity, "ISALCODIG;ISALNOMBR;ISALVALOR", Nothing)
        Return serverMode
    End Function
#End Region

#Region "ListBeds"
    ''' <summary>
    ''' Lista de centros de atención
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListBeds(center As String, uf As String) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        'Dim criteria As CriteriaOperator = CriteriaOperator.Parse("CODCENATE='" & center & "' AND ESTADCAMA = 1")
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("CODCENATE='" & center & "' AND UFUCODIGO.UFUCODIGO= '" & uf & "' AND ESTADCAMA = 1")
        classEntity = sessionNew.GetClassInfo(GetType(CHCAMASHO))
        serverMode = New XPInstantFeedbackSource(classEntity, "CodeName;CODICAMAS;NUMCAMHOS;DESCCAMAS", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista de centros de atención
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAllBeds() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        'Dim criteria As CriteriaOperator = CriteriaOperator.Parse("CODCENATE='" & center & "' AND ESTADCAMA = 1")
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("ESTADCAMA = 1")
        classEntity = sessionNew.GetClassInfo(GetType(CHCAMASHO))
        serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function
#End Region


    ''' <summary>
    ''' Lista los ingresos para ordenes de servicio
    ''' </summary>
    ''' <returns></returns>
    Public Function GetViewAdmissionServiceOrder() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(ViewAdmissionServiceOrder))
        serverMode = New XPInstantFeedbackSource(classEntity, Nothing, Nothing)
        Return serverMode
    End Function
    
#End Region

#Region "Informes"

    ''' <summary>
    ''' Función para obtener una lista de datos de una entidad XPO
    ''' </summary>
    ''' <typeparam name="T">Objeto XPO</typeparam>
    ''' <param name="Fun">Objeto tipo Función opcional para filtrar los datos necesarios</param>
    ''' <returns>Lista tipo T</returns>
    Public Function GetCollection(Of T)(Optional Fun As Func(Of T, Boolean) = Nothing, Optional criteria As String = Nothing) As List(Of T)
        Dim result = Me.LoadCollection(Of T)(XpoDefault.DataLayer, Fun, criteria)
        result.Sort()
        Return result
    End Function

#End Region

End Class