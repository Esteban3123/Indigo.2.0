'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.CrystalRepository
' Author           : Juan F. Tamayo
' Created          : 2016-02-08
'
' Copyright        : (c) . All rights reserved.
'*************************************************************

#Region "Imports"

Imports Infrastructure.CrossCutting.Xpo.Base
Imports DevExpress.Xpo
Imports Infrastructure.CrossCutting.Base
Imports DevExpress.Xpo.Metadata
Imports DevExpress.Xpo.DB
Imports DevExpress.Xpo.DB.Helpers
Imports DevExpress.Xpo.Helpers
Imports DevExpress.Data.PLinq
Imports DevExpress.Data.Linq
Imports Infrastructure.Data.Xpo.CrystalRepository.INDIGO001
Imports DevExpress.Data.Filtering
Imports Infrastructure.Data.Xpo.CrystalRepository

#End Region

Public Class CrystalServiceXpoEx
    Inherits XpoBaseService
    Implements IDisposable

#Region "Public Methods"



    Public Function GetPatientXpo(code As String) As PatientXpo
        Dim session As New IndigoXPOSession(Of PatientXpo)()
        Return session.GetObjectByKey(Of PatientXpo)(code)
    End Function

    Public Function GetAdmissionXpo(admissionNumber As String) As AdmissionXpo
        Dim session As New IndigoXPOSession(Of AdmissionXpo)()
        Return session.GetObjectByKey(Of AdmissionXpo)(admissionNumber)
    End Function

    Public Function GetHCREGEGREByAdmissionNumber(admissionNumber As String) As HCREGEGRE
        Dim session As New IndigoXPOSession(Of HCREGEGRE)()
        Return session.GetObjectByKey(Of HCREGEGRE)(admissionNumber)
    End Function

    Public Function GetCHREGEGREByAdmissionNumber(admissionNumber As String) As CHREGEGRE
        Dim session As New IndigoXPOSession(Of CHREGEGRE)()
        Return session.GetObjectByKey(Of CHREGEGRE)(admissionNumber)
    End Function

    Public Function GetRiskFactorById(RiskFactorId As Integer) As RiskFactorXpo
        Dim session As New IndigoXPOSession(Of RiskFactorXpo)()
        Return session.GetObjectByKey(Of RiskFactorXpo)(RiskFactorId)
    End Function

    Public Function ListAllRiskFactor() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of RiskFactorXpo)()
        Dim _classEntity = session.GetClassInfo(GetType(RiskFactorXpo))
        Return New XPInstantFeedbackSource(_classEntity)
    End Function

    Public Function ListAllINDIAGNOS() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of INDIAGNOS)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("ESTADO=1")
        Dim _classEntity = session.GetClassInfo(GetType(INDIAGNOS))
        Return New XPInstantFeedbackSource(_classEntity, Nothing, criteria)
    End Function

    Public Function ListINDIAGNOSPerFilter(PatientSex As Integer, PatientAge As Integer, PatientAgeInMonths As Integer, PatientAgeInDays As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of INDIAGNOS)()

        ' Determinar el criterio de sexo
        Dim sexFilterStr As String
        If PatientSex = 1 Then ' Masculino
            sexFilterStr = "APLICAMAS = 1"
        Else ' Femenino
            sexFilterStr = "APLICAFEM = 1"
        End If

        ' Construir criterio para edad
        Dim ageMinFilter As String =
        "(EDADMINIM IS NULL OR EDADMINIM = 0 OR " &
        "(UNIEDADMI = 1 AND EDADMINIM <= " & PatientAge & ") OR " &
        "(UNIEDADMI = 2 AND EDADMINIM <= " & PatientAgeInMonths & ") OR " &
        "(UNIEDADMI = 3 AND EDADMINIM <= " & PatientAgeInDays & "))"

        Dim ageMaxFilter As String =
        "(EDADMAXIM IS NULL OR EDADMAXIM = 0 OR " &
        "(UNIEDADMA = 1 AND EDADMAXIM >= " & PatientAge & ") OR " &
        "(UNIEDADMA = 2 AND EDADMAXIM >= " & PatientAgeInMonths & ") OR " &
        "(UNIEDADMA = 3 AND EDADMAXIM >= " & PatientAgeInDays & "))"

        ' Combinar todos los criterios
        Dim strCriteria As String = sexFilterStr & " AND " & ageMinFilter & " AND " & ageMaxFilter & " AND ESTADO = 1"
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(strCriteria)
        Dim _classEntity = session.GetClassInfo(GetType(INDIAGNOS))
        Dim res = New XPInstantFeedbackSource(_classEntity, Nothing, criteria)
        Return res
    End Function

    Public Function ListAllINUNIMEDI() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of INUNIMEDI)()
        Dim _classEntity = session.GetClassInfo(GetType(INUNIMEDI))
        Return New XPInstantFeedbackSource(_classEntity)
    End Function

    Public Function ListAllHCVIAADMI() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of HCVIAADMI)()
        Dim _classEntity = session.GetClassInfo(GetType(HCVIAADMI))
        Return New XPInstantFeedbackSource(_classEntity)
    End Function

    Public Function ListAllIHFORMEDI() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of IHFORMEDI)()
        Dim _classEntity = session.GetClassInfo(GetType(IHFORMEDI))
        Return New XPInstantFeedbackSource(_classEntity)
    End Function

    Public Function ListAllDCI() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of IHDCIMEDIXpo)()
        Dim _classEntity = session.GetClassInfo(GetType(IHDCIMEDIXpo))
        Return New XPInstantFeedbackSource(_classEntity)
    End Function

    Public Function ListAllIHGRUFARM() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of IHGRUFARM)()
        Dim _classEntity = session.GetClassInfo(GetType(IHGRUFARM))
        Return New XPInstantFeedbackSource(_classEntity)
    End Function

    ''' <summary>
    ''' funcion que lista los ingresos
    ''' </summary>
    ''' <returns></returns>
    Public Function ListGetAdmissionXpo() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ViewGetAdmissionXpo)()
        Dim values() As String = {"F", "A", "C"}
        Dim criteria As New InOperator("Status", values)
        Dim notIncriteria As New UnaryOperator(UnaryOperatorType.Not, criteria)
        Dim _classEntity = session.GetClassInfo(GetType(ViewGetAdmissionXpo))
        Return New XPInstantFeedbackSource(_classEntity, Nothing, notIncriteria)
    End Function

    ''' <summary>
    ''' metodo que consulta los tipos de identificacion del ehr
    ''' </summary>
    ''' <returns></returns>
    Public Function ListADTIPOIDENTIFICAxpoActive() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ADTIPOIDENTIFICAXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("ESTADO=1")
        Dim _classEntity = session.GetClassInfo(GetType(ADTIPOIDENTIFICAXpo))
        Return New XPInstantFeedbackSource(_classEntity, Nothing, criteria)
    End Function

#Region "Liquidation"
    ''' <summary>
    ''' Lista todos los ingresos para el frontal de listado de facturas
    ''' </summary>
    Public Function Liquidation_GetAdmission(Optional filter As CriteriaOperator = Nothing, Optional properties As String = Nothing) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ViewLiquidationGetAdmission)
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(ViewLiquidationGetAdmission)), properties, filter)
        'End Using
    End Function

    Public Function Liquidation_GetAdmission(numIngres As String) As ViewLiquidationGetAdmissionAll
        Dim session As New IndigoXPOSession(Of ViewLiquidationGetAdmissionAll)
        Return session.GetObjectByKey(Of ViewLiquidationGetAdmissionAll)(numIngres)
        'End Using
    End Function
#End Region

#Region "ListPatients"

    ''' <summary>
    ''' Funcion para listar todos los pacientes
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAllPatients() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PatientXpo)()
        Dim _classEntity = session.GetClassInfo(GetType(PatientXpo))
        Return New XPInstantFeedbackSource(_classEntity)
    End Function

#End Region

#Region "ListAdmissions"

    ''' <summary>
    ''' Lista todos los ingresos que no estan anulados ni cancelados
    ''' </summary>
    Public Function ListAdmissions() As LinqInstantFeedbackSource
        Dim linqListAdmissions As New LinqInstantFeedbackSource
        AddHandler linqListAdmissions.GetQueryable, AddressOf plinqListAdmissions_GetQueryable
        linqListAdmissions.KeyExpression = "AdmissionCode"
        Return linqListAdmissions
    End Function

    ''' <summary>
    ''' Ejecuta y obtiene un conjunto de datos
    ''' </summary>
    Private Sub plinqListAdmissions_GetQueryable(sender As Object, e As GetQueryableEventArgs)
        Try
            Dim session As New Session(XpoDefault.DataLayer)
            Dim tableAdmissions As XPQuery(Of AdmissionXpo) = New XPQuery(Of AdmissionXpo)(session)
            Dim tableProfessional As XPQuery(Of HealthCareProfessionalXpo) = New XPQuery(Of HealthCareProfessionalXpo)(session)
            Dim tablePatient As XPQuery(Of PatientXpo) = New XPQuery(Of PatientXpo)(session)
            Dim tableEntity As XPQuery(Of EntityXpo) = New XPQuery(Of EntityXpo)(session)

            'Dim CLOSED As Char = "C" 'Cerrado
            'Dim CANCELLED As Char = "A" 'Anulado

            Dim TmpQueryableSource = From TAdmissions In tableAdmissions
                                     Join TPatient In tablePatient On TAdmissions.IPCODPACI Equals TPatient.IPCODPACI
                                     Join TEntity In tableEntity On TAdmissions.CODENTIDA.CODENTIDA Equals TEntity.CODENTIDA
                                     Where (TAdmissions.IESTADOIN = " " Or TAdmissions.IESTADOIN = "P")
                                     Select New With {.AdmissionCode = TAdmissions.NUMINGRES,
                                                      .PatientCode = TPatient.IPCODPACI,
                                                      .PatientName = TPatient.IPNOMCOMP,
                                                      .PatientDateBirth = TPatient.IPFECNACI,
                                                      .PatientGenus = TPatient.IPSEXOPAC,
                                                      .AdmissionDate = TAdmissions.IFECHAING,
                                                      .AdmissionType = TAdmissions.TIPOINGRE,
                                                      .BedStay = TAdmissions.CODICAMHO,
                                                      .PlaceEntry = TAdmissions.IINGREPOR,
                                                      .LiquidationType = TAdmissions.ILIQUIDAC,
                                                      .BenefitPlan = TAdmissions.CODPANATE,
                                                      .EntityCode = TEntity.CODENTIDA,
                                                      .EntityName = TEntity.NOMENTIDA,
                                                      .AuthorizationNumber = TAdmissions.IAUTORIZA,
                                                      .ResponsibleName = TAdmissions.IPRNOMBRE,
                                                      .ResponsiblePhone = TAdmissions.IPTELEFON,
                                                      .CareGroupId = TAdmissions.GENCAREGROUP,
                                                      .HealthAdministratorId = TAdmissions.GENCONENTITY,
                                                      .FunctionalUnitCode = TAdmissions.UFUCODIGO,
                                                      .Status = TAdmissions.IESTADOIN,
                                                      .CenterAttentionCode = TAdmissions.CODCENATE,
                                                      .FullNameAdmission = String.Format(Infrastructure.CrossCutting.Resources.ResourceManager.GetString("AdmissionResume", "IndigoCrystalHis"), TAdmissions.NUMINGRES.ToString().Trim(), TPatient.IPCODPACI.ToString().Trim(), TPatient.IPNOMCOMP.ToString().Trim())}

            e.QueryableSource = TmpQueryableSource
            e.Tag = tableAdmissions
            'End Using
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub
#End Region

#Region "ListAllAdmissions"

    Private _admissionCode As String
    ''' <summary>
    ''' Lista todos los ingresos que no estan anulados ni cancelados
    ''' </summary>
    Public Function ListAdmissionsByCode(admissionCode) As LinqInstantFeedbackSource
        _admissionCode = admissionCode
        Dim linqListAllAdmissions As New LinqInstantFeedbackSource
        AddHandler linqListAllAdmissions.GetQueryable, AddressOf plinqListAllAdmissions_GetQueryable
        linqListAllAdmissions.KeyExpression = "AdmissionCode"
        Return linqListAllAdmissions
    End Function



    ''' <summary>
    ''' Lista todos los ingresos que no estan anulados ni cancelados
    ''' </summary>
    Public Function ListAllAdmissions() As LinqInstantFeedbackSource
        Dim linqListAllAdmissions As New LinqInstantFeedbackSource
        AddHandler linqListAllAdmissions.GetQueryable, AddressOf plinqListAllAdmissions_GetQueryable
        linqListAllAdmissions.KeyExpression = "AdmissionCode"
        Return linqListAllAdmissions
    End Function

    ''' <summary>
    ''' Ejecuta y obtiene un conjunto de datos
    ''' </summary>
    Private Sub plinqListAllAdmissions_GetQueryable(sender As Object, e As GetQueryableEventArgs)
        Try
            Dim session As New Session(XpoDefault.DataLayer)
            Dim tableAdmissions As XPQuery(Of AdmissionXpo) = New XPQuery(Of AdmissionXpo)(session)
            Dim tableProfessional As XPQuery(Of HealthCareProfessionalXpo) = New XPQuery(Of HealthCareProfessionalXpo)(session)
            Dim tablePatient As XPQuery(Of PatientXpo) = New XPQuery(Of PatientXpo)(session)
            Dim tableEntity As XPQuery(Of EntityXpo) = New XPQuery(Of EntityXpo)(session)

            Dim CLOSED As Char = "C" 'Cerrado
            Dim CANCELLED As Char = "A" 'Anulado
            Dim FACTURED As Char = "F" 'Facturado
            Dim PARTIALSTATUS As Char = "P" 'Parcial

            Dim TmpQueryableSource = From TAdmissions In tableAdmissions
                                     Join TPatient In tablePatient On TAdmissions.IPCODPACI Equals TPatient.IPCODPACI
                                     Join TEntity In tableEntity On TAdmissions.CODENTIDA.CODENTIDA Equals TEntity.CODENTIDA
                                     Where (TAdmissions.IESTADOIN = FACTURED Or TAdmissions.IESTADOIN = PARTIALSTATUS) AndAlso (_admissionCode Is Nothing OrElse TAdmissions.NUMINGRES = _admissionCode)
                                     Select New With {.AdmissionCode = TAdmissions.NUMINGRES,
                                                      .PatientCode = TPatient.IPCODPACI,
                                                      .PatientName = TPatient.IPNOMCOMP,
                                                      .PatientDateBirth = TPatient.IPFECNACI,
                                                      .PatientGenus = TPatient.IPSEXOPAC,
                                                      .AdmissionDate = TAdmissions.IFECHAING,
                                                      .AdmissionType = TAdmissions.TIPOINGRE,
                                                      .BedStay = TAdmissions.CODICAMHO,
                                                      .PlaceEntry = TAdmissions.IINGREPOR,
                                                      .LiquidationType = TAdmissions.ILIQUIDAC,
                                                      .BenefitPlan = TAdmissions.CODPANATE,
                                                      .EntityCode = TEntity.CODENTIDA,
                                                      .EntityName = TEntity.NOMENTIDA,
                                                      .AuthorizationNumber = TAdmissions.IAUTORIZA,
                                                      .ResponsibleName = TAdmissions.IPRNOMBRE,
                                                      .ResponsiblePhone = TAdmissions.IPTELEFON,
                                                      .CareGroupId = TAdmissions.GENCAREGROUP,
                                                      .HealthAdministratorId = TAdmissions.GENCONENTITY,
                                                      .FunctionalUnitCode = TAdmissions.UFUCODIGO,
                                                      .Status = TAdmissions.IESTADOIN,
                                                      .StatusName = If(TAdmissions.IESTADOIN = "F", "Facturado", "Parcialmente Facturado"),
                                                      .FullNameAdmission = String.Format(Infrastructure.CrossCutting.Resources.ResourceManager.GetString("AdmissionResume", "IndigoCrystalHis"), TAdmissions.NUMINGRES.ToString().Trim(), TPatient.IPCODPACI.ToString().Trim(), TPatient.IPNOMCOMP.ToString().Trim())}

            e.QueryableSource = TmpQueryableSource
            e.Tag = tableAdmissions
            'End Using
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub
#End Region

#Region "AccountControl"

    Public Function ListMedicineSupplierAggregatesByPatientCodeIngreso(patientCode As String, admissionNumber As String, Optional ItemProductionClass As Boolean = False) As XPCollection(Of ViewMedicinesSupplies)
        Dim listMedicineSupplier As XPCollection(Of ViewMedicinesSupplies) = ListMedicineSupplierByPatientCodeIngreso(patientCode, admissionNumber, ItemProductionClass)
        'Dim listKardex As XPCollection(Of ViewKardexMedicineSupplier) = ListKardexMedicineSupplierByPatientCodeIngreso(patientCode, admissionNumber)

        'Dim listxxx = listKardex.ToList()
        'Dim list2 = listMedicineSupplier.ToList()
        'If list2.Count > 0 AndAlso listxxx.Count > 0 Then
        '    For Each medicine In listMedicineSupplier
        '        'Dim kardex As XPCollection(Of ViewKardexMedicineSupplier) = service.ListKardexMedicineSupplierByCodePatientCodeIngreso(medicine.Codigo, patientCode, admissionNumber)
        '        Dim kardexList As List(Of ViewKardexMedicineSupplier) = (From e In listxxx Where e.Codigo = medicine.Codigo Select e).ToList()
        '        medicine.KardexMedicineSupplier = kardexList
        '        medicine.Entregada = kardexList.Where(Function(o) o.Origen > 0 AndAlso o.Origen < 4).Sum(Function(o) o.Cantidad)
        '        medicine.Aplicada = kardexList.Where(Function(o) o.Origen > 10 AndAlso o.Origen < 16).Sum(Function(o) o.Cantidad)
        '        'medicine.Prestamo = kardexList.Where(Function(o) o.Origen = 4).Sum(Function(o) o.Cantidad)
        '        'medicine.Devolutivo = kardexList.Where(Function(o) o.Origen = 16).Sum(Function(o) o.Cantidad)
        '        'medicine.Alerta = IIf(medicine.Prestamo - medicine.Devolutivo = 0, 0, 1)
        '        Dim trasladosSalida As Integer = kardexList.Where(Function(o) o.Origen = 18).Sum(Function(o) o.Cantidad)
        '        Dim trasladosEntradas As Integer = kardexList.Where(Function(o) o.Origen = 5).Sum(Function(o) o.Cantidad)
        '        medicine.Fisico = medicine.Entregada + trasladosEntradas - medicine.Aplicada - trasladosSalida
        '    Next
        'End If
        Return listMedicineSupplier
    End Function


    Public Function ListMedicineSupplierByPatientCodeIngreso(patientCode As String, admissionNumber As String, Optional ItemProductionClass As Boolean = False) As XPCollection(Of ViewMedicinesSupplies)
        Dim session As New IndigoXPOSession(Of ViewMedicinesSupplies)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(String.Format("Ingreso = '{0}' AND ClaseItemProduccion= {1}", admissionNumber, ItemProductionClass))
        Return New XPCollection(Of ViewMedicinesSupplies)(session, criteria)
        'End Using
    End Function
    Function GetAdmissionByNumIngres(admissionNumber As String) As XPCollection(Of ViewAdmissionsToLiquidation)
        Dim session As New IndigoXPOSession(Of ViewAdmissionsToLiquidation)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("AdmissionCode='" & admissionNumber & "'")
        Return New XPCollection(Of ViewAdmissionsToLiquidation)(session, criteria)
        'End Using
    End Function
    Function GetAdmissionConfirmByNumIngres(admissionNumber As String) As XPCollection(Of ViewAdmissionsToLiquidationConfirm)
        Dim session As New IndigoXPOSession(Of ViewAdmissionsToLiquidationConfirm)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("AdmissionCode='" & admissionNumber & "'")
        Return New XPCollection(Of ViewAdmissionsToLiquidationConfirm)(session, criteria)
        'End Using
    End Function

    Function GetAdmissionConfirmByNumIngresAndIINGREPOR(admissionNumber As String) As XPCollection(Of ViewAdmissionsToLiquidationConfirm)
        Dim session As New IndigoXPOSession(Of ViewAdmissionsToLiquidationConfirm)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("AdmissionCode='" & admissionNumber & "' And (PlaceEntry = 2 or PlaceEntry = 3)")
        Return New XPCollection(Of ViewAdmissionsToLiquidationConfirm)(session, criteria)
        'End Using
    End Function

    Public Function ListKardexMedicineSupplierByPatientCodeIngreso(patientCode As String, admissionNumber As String) As XPCollection(Of ViewKardexMedicineSupplier)
        Dim session As New IndigoXPOSession(Of ViewKardexMedicineSupplier)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Ingreso = '" & admissionNumber & "' AND Origen > 0")
        Return New XPCollection(Of ViewKardexMedicineSupplier)(session, criteria)
        'End Using
    End Function

    Public Function ListKardexMedicineSupplierByCodePatientCodeIngreso(code As String, patientCode As String, admissionNumber As String) As XPCollection(Of ViewKardexMedicineSupplier)
        Dim session As New IndigoXPOSession(Of ViewKardexMedicineSupplier)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Codigo = '" & code & "' AND CodigoPaciente = '" & patientCode & "' AND Ingreso = '" & admissionNumber & "' AND Origen > 0")
        Return New XPCollection(Of ViewKardexMedicineSupplier)(session, criteria)
        'End Using
    End Function

    Public Function ListLaboratories(patientCode As String, admissionNumber As String) As XPCollection(Of ViewLaboratories)
        Dim session As New IndigoXPOSession(Of ViewLaboratories)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("NUMINGRES = '" & admissionNumber & "'")
        Return New XPCollection(Of ViewLaboratories)(session, criteria)
        'End Using
    End Function

    Public Function ListPathologiesByPatientCodeIngreso(patientCode As String, admissionNumber As String) As XPCollection(Of ViewPathologies)
        Dim session As New IndigoXPOSession(Of ViewPathologies)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("NUMINGRES = '" & admissionNumber & "'")
        Return New XPCollection(Of ViewPathologies)(session, criteria)
        'End Using
    End Function

    Public Function ListImagesDxByPatientCodeIngreso(patientCode As String, admissionNumber As String) As XPCollection(Of ViewImagesDX)
        Dim session As New IndigoXPOSession(Of ViewImagesDX)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("NUMINGRES = '" & admissionNumber & "'")
        Return New XPCollection(Of ViewImagesDX)(session, criteria)
        'End Using
    End Function

    Public Function ListProceduresQxByPatientCodeIngreso(patientCode As String, admissionNumber As String) As XPCollection(Of ViewProceduresQx)
        Dim session As New IndigoXPOSession(Of ViewProceduresQx)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("NUMINGRES = '" & admissionNumber & "'")
        Return New XPCollection(Of ViewProceduresQx)(session, criteria)
        'End Using
    End Function

    Public Function ListProceduresNoQxByPatientCodeIngreso(patientCode As String, admissionNumber As String) As XPCollection(Of ViewProceduresNoQx)
        Dim session As New IndigoXPOSession(Of ViewProceduresNoQx)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("NUMINGRES = '" & admissionNumber & "'")
        Return New XPCollection(Of ViewProceduresNoQx)(session, criteria)
        'End Using
    End Function

    Public Function ListServicesProceduresLaboratoriesByadmissionCodeIngreso(patientCode As String, admissionNumber As String) As XPCollection(Of ViewServicesProceduresLaboratories)
        Dim session As New IndigoXPOSession(Of ViewServicesProceduresLaboratories)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("NUMINGRES = '" & admissionNumber & "'")
        Return New XPCollection(Of ViewServicesProceduresLaboratories)(session, criteria)
        'End Using
    End Function

    'Public Function ListServicesProceduresReviewsByadmissionCodeIngreso(admissionNumber As String) As XPCollection(Of ViewServicesProceduresReviews)
    '    Dim session = New Session(XpoDefault.DataLayer)
    '    Dim criteria As CriteriaOperator = CriteriaOperator.Parse("NUMINGRES = '" & admissionNumber & "'")
    '    Dim collect As XPCollection(Of ViewServicesProceduresReviews) = New XPCollection(Of ViewServicesProceduresReviews)(session, criteria)
    '    Return collect
    'End Function

    Function ListServicesProceduresPathologiesByadmissionCodeIngreso(patientCode As String, admissionNumber As String) As XPCollection(Of ViewServicesProceduresPathologies)
        Dim session As New IndigoXPOSession(Of ViewServicesProceduresPathologies)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("NUMINGRES = '" & admissionNumber & "'")
        Return New XPCollection(Of ViewServicesProceduresPathologies)(session, criteria)
        'End Using
    End Function

    Function ListServicesProceduresImagesDxByadmissionCodeIngreso(patientCode As String, admissionNumber As String) As XPCollection(Of ViewServicesProceduresImagesDx)
        Dim session As New IndigoXPOSession(Of ViewServicesProceduresImagesDx)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("NUMINGRES = '" & admissionNumber & "'")
        Return New XPCollection(Of ViewServicesProceduresImagesDx)(session, criteria)
        'End Using
    End Function

    Function ListServicesProceduresQxByadmissionCodeIngreso(patientCode As String, admissionNumber As String) As XPCollection(Of ViewServicesProceduresQx)
        Dim session As New IndigoXPOSession(Of ViewServicesProceduresQx)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("NUMINGRES = '" & admissionNumber & "'")
        Return New XPCollection(Of ViewServicesProceduresQx)(session, criteria)
        'End Using
    End Function

    Function ListServicesProceduresReportQxByadmissionCodeIngreso(patientCode As String, admissionNumber As String) As XPCollection(Of ViewServicesProceduresReportQx)
        Dim session As New IndigoXPOSession(Of ViewServicesProceduresReportQx)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("NUMINGRES = '" & admissionNumber & "'")
        Return New XPCollection(Of ViewServicesProceduresReportQx)(session, criteria)
        'End Using
    End Function

    Function ListServicesProceduresNoQxByadmissionCodeIngreso(patientCode As String, admissionNumber As String) As XPCollection(Of ViewServicesProceduresNoQx)
        Dim session As New IndigoXPOSession(Of ViewServicesProceduresNoQx)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("NUMINGRES = '" & admissionNumber & "'")
        Return New XPCollection(Of ViewServicesProceduresNoQx)(session, criteria)
        'End Using
    End Function

    Public Function ListConsultationByPatientCodeIngreso(patientCode As String, admissionNumber As String) As XPCollection(Of ViewConsultation)
        Dim session As New IndigoXPOSession(Of ViewConsultation)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("NUMINGRES = '" & admissionNumber & "'")
        Return New XPCollection(Of ViewConsultation)(session, criteria)
        'End Using
    End Function

    Function ListServicesProceduresConsultationByadmissionCodeIngreso(patientCode As String, admissionNumber As String) As XPCollection(Of ViewServicesProceduresConsultation)
        Dim session As New IndigoXPOSession(Of ViewServicesProceduresConsultation)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("NUMINGRES = '" & admissionNumber & "'")
        Return New XPCollection(Of ViewServicesProceduresConsultation)(session, criteria)
        'End Using
    End Function

    Public Function ListTherapyByPatientCodeIngreso(patientCode As String, admissionNumber As String) As XPCollection(Of ViewTherapy)
        Dim session As New IndigoXPOSession(Of ViewTherapy)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("NUMINGRES = '" & admissionNumber & "'")
        Return New XPCollection(Of ViewTherapy)(session, criteria)
        'End Using
    End Function

    Function ListServicesProceduresTherapyByadmissionCodeIngreso(patientCode As String, admissionNumber As String) As XPCollection(Of ViewServicesProceduresTherapy)
        Dim session As New IndigoXPOSession(Of ViewServicesProceduresTherapy)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("NUMINGRES = '" & admissionNumber & "'")
        Return New XPCollection(Of ViewServicesProceduresTherapy)(session, criteria)
        'End Using
    End Function

    Function ListOxygenConsumptionByadmissionCodeIngreso(patientCode As String, admissionNumber As String) As XPCollection(Of ViewOxygenConsumption)
        Dim session As New IndigoXPOSession(Of ViewOxygenConsumption)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("NUMINGRES = '" & admissionNumber & "'")
        Return New XPCollection(Of ViewOxygenConsumption)(session, criteria)
        'End Using
    End Function

    Function ListNursingProceduresByadmissionCodeIngresoCodCenAte(patientCode As String, admissionNumber As String, atentionCenter As String) As XPCollection(Of ViewNursingProcedures)
        Dim session As New IndigoXPOSession(Of ViewNursingProcedures)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("NUMINGRES = '" & admissionNumber & "' AND CODCENATE = '" & atentionCenter & "'")
        Return New XPCollection(Of ViewNursingProcedures)(session, criteria)
        'End Using
    End Function

    Function ListNursingProceduresDetailByadmissionCodeIngresoCodCenAte(patientCode As String, admissionNumber As String, atentionCenter As String) As XPCollection(Of ViewNursingProceduresDetail)
        Dim session As New IndigoXPOSession(Of ViewNursingProceduresDetail)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("IPCODPACI = '" & patientCode & "' AND NUMINGRES = '" & admissionNumber & "' AND CODCENATE = '" & atentionCenter & "'")
        Return New XPCollection(Of ViewNursingProceduresDetail)(session, criteria)
        'End Using
    End Function

    Function ListServicesNursingProceduresDetailByadmissionCodeIngresoCodCenAte(patientCode As String, admissionNumber As String, atentionCenter As String) As XPCollection(Of ViewServiceProceduresNursingProcedure)
        Dim session As New IndigoXPOSession(Of ViewServiceProceduresNursingProcedure)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("NUMINGRES = '" & admissionNumber & "' AND CODCENATE = '" & atentionCenter & "'")
        Return New XPCollection(Of ViewServiceProceduresNursingProcedure)(session, criteria)
        'End Using
    End Function

    Function ListReviewsByadmissionCodeIngreso(patientCode As String, admissionNumber As String) As XPCollection(Of ViewReviews)
        Dim session As New IndigoXPOSession(Of ViewReviews)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("NUMINGRES = '" & admissionNumber & "'")
        Return New XPCollection(Of ViewReviews)(session, criteria)
        'End Using
    End Function

    Function ListQxRealizadosByadmissionCodeIngreso(patientCode As String, admissionNumber As String) As XPCollection(Of ViewSurgeriesPerformed)
        Dim session As New IndigoXPOSession(Of ViewSurgeriesPerformed)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("NUMINGRES = '" & admissionNumber & "'")
        Return New XPCollection(Of ViewSurgeriesPerformed)(session, criteria)
        'End Using
    End Function

    Function ListQxEquipeByadmissionCodeIngreso(patientCode As String, admissionNumber As String) As XPCollection(Of ViewSurgicalEquipment)
        Dim session As New IndigoXPOSession(Of ViewSurgicalEquipment)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("NUMINGRES = '" & admissionNumber & "'")
        Return New XPCollection(Of ViewSurgicalEquipment)(session, criteria)
        'End Using
    End Function

    Function ListQxInformByadmissionCodeIngreso(patientCode As String, admissionNumber As String) As XPCollection(Of ViewSurgicalReport)
        Dim session As New IndigoXPOSession(Of ViewSurgicalReport)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("NUMINGRES = '" & admissionNumber & "'")
        Return New XPCollection(Of ViewSurgicalReport)(session, criteria)
        'End Using
    End Function

    Function ListServicesHemocomponentByadmissionIngreso(admissionNumber As String) As XPCollection(Of ViewServicesHemocomponent)
        Dim session As New IndigoXPOSession(Of ViewServicesHemocomponent)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("NUMINGRES = '" & admissionNumber & "'")
        Return New XPCollection(Of ViewServicesHemocomponent)(session, criteria)
        'End Using
    End Function

#End Region

#Region "ListAdmissionsByPatientCode"

    Private _patientCode As String
    ''' <summary>
    ''' Lista todos los ingresos que no estan anulados ni cancelados
    ''' </summary>
    Public Function ListAdmissionsPatientCode(PatientCode As String) As LinqInstantFeedbackSource
        Dim linqListAdmissionsPatientCode As New LinqInstantFeedbackSource
        AddHandler linqListAdmissionsPatientCode.GetQueryable, AddressOf plinqListAdmissionsPatientCode_GetQueryable
        linqListAdmissionsPatientCode.KeyExpression = "AdmissionCode"
        _patientCode = PatientCode
        Return linqListAdmissionsPatientCode
    End Function

    ''' <summary>
    ''' Ejecuta y obtiene un conjunto de datos
    ''' </summary>
    Private Sub plinqListAdmissionsPatientCode_GetQueryable(sender As Object, e As GetQueryableEventArgs)
        Try
            Dim session As New Session(XpoDefault.DataLayer)
            Dim tableAdmissions As New XPQuery(Of AdmissionXpo)(session)
            Dim tableProfessional As New XPQuery(Of HealthCareProfessionalXpo)(session)
            Dim tablePatient As New XPQuery(Of PatientXpo)(session)
            Dim tableEntity As New XPQuery(Of EntityXpo)(session)

            Dim CLOSED As Char = "C" 'Cerrado
            Dim CANCELLED As Char = "A" 'Anulado

            Dim TmpQueryableSource = From TAdmissions In tableAdmissions
                                     Join TPatient In tablePatient On TAdmissions.IPCODPACI Equals TPatient.IPCODPACI
                                     Join TEntity In tableEntity On TAdmissions.CODENTIDA.CODENTIDA Equals TEntity.CODENTIDA
                                     Where (TAdmissions.IESTADOIN <> CLOSED And TAdmissions.IESTADOIN <> CANCELLED And TPatient.IPCODPACI = _patientCode)
                                     Select New With {.AdmissionCode = TAdmissions.NUMINGRES,
                                                      .PatientCode = TPatient.IPCODPACI,
                                                      .PatientName = TPatient.IPNOMCOMP,
                                                      .PatientDateBirth = TPatient.IPFECNACI,
                                                      .PatientGenus = TPatient.IPSEXOPAC,
                                                      .AdmissionDate = TAdmissions.IFECHAING,
                                                      .AdmissionType = TAdmissions.TIPOINGRE,
                                                      .BedStay = TAdmissions.CODICAMHO,
                                                      .PlaceEntry = TAdmissions.IINGREPOR,
                                                      .LiquidationType = TAdmissions.ILIQUIDAC,
                                                      .BenefitPlan = TAdmissions.CODPANATE,
                                                      .EntityCode = TEntity.CODENTIDA,
                                                      .EntityName = TEntity.NOMENTIDA,
                                                      .AuthorizationNumber = TAdmissions.IAUTORIZA,
                                                      .ResponsibleName = TAdmissions.IPRNOMBRE,
                                                      .ResponsiblePhone = TAdmissions.IPTELEFON,
                                                      .CareGroupId = TAdmissions.GENCAREGROUP,
                                                      .HealthAdministratorId = TAdmissions.GENCONENTITY,
                                                      .Status = TAdmissions.IESTADOIN,
                                                      .FullNameAdmission = String.Format(Infrastructure.CrossCutting.Resources.ResourceManager.GetString("AdmissionResume", "IndigoCrystalHis"), TAdmissions.NUMINGRES.ToString().Trim(), TPatient.IPCODPACI.ToString().Trim(), TPatient.IPNOMCOMP.ToString().Trim())}

            e.QueryableSource = TmpQueryableSource
            e.Tag = tableAdmissions
            'End Using
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

#End Region

#Region "ListAdmissionsOpenAndPartialByPatientCode"

    Private _patientCodeOpenAndPartial As String
    ''' <summary>
    ''' Lista todos los ingresos que no estan anulados ni cancelados
    ''' </summary>
    Public Function ListAdmissionsOpenAndPartialPatientCode(PatientCode As String) As LinqInstantFeedbackSource
        Dim linqListAdmissionsOpenAndPartialPatientCode As New LinqInstantFeedbackSource
        AddHandler linqListAdmissionsOpenAndPartialPatientCode.GetQueryable, AddressOf plinqListAdmissionsOpenAndPartialPatientCode_GetQueryable
        linqListAdmissionsOpenAndPartialPatientCode.KeyExpression = "AdmissionCode"
        _patientCodeOpenAndPartial = PatientCode
        Return linqListAdmissionsOpenAndPartialPatientCode
    End Function

    ''' <summary>
    ''' Ejecuta y obtiene un conjunto de datos
    ''' </summary>
    Private Sub plinqListAdmissionsOpenAndPartialPatientCode_GetQueryable(sender As Object, e As GetQueryableEventArgs)
        Try
            Dim session As New Session(XpoDefault.DataLayer)
            Dim tableAdmissions As New XPQuery(Of AdmissionXpo)(session)
            Dim tableProfessional As New XPQuery(Of HealthCareProfessionalXpo)(session)
            Dim tablePatient As New XPQuery(Of PatientXpo)(session)
            Dim tableEntity As New XPQuery(Of EntityXpo)(session)

            Dim AdmissionOpen As Char = " " 'Abierto
            Dim AdmissionPartial As Char = "P" 'Parciales

            Dim TmpQueryableSource = From TAdmissions In tableAdmissions
                                     Join TPatient In tablePatient On TAdmissions.IPCODPACI Equals TPatient.IPCODPACI
                                     Join TEntity In tableEntity On TAdmissions.CODENTIDA.CODENTIDA Equals TEntity.CODENTIDA
                                     Where (TAdmissions.IESTADOIN = AdmissionOpen Or TAdmissions.IESTADOIN = AdmissionPartial) And TPatient.IPCODPACI = _patientCodeOpenAndPartial
                                     Select New With {.AdmissionCode = TAdmissions.NUMINGRES,
                                                      .PatientCode = TPatient.IPCODPACI,
                                                      .PatientName = TPatient.IPNOMCOMP,
                                                      .PatientDateBirth = TPatient.IPFECNACI,
                                                      .PatientGenus = TPatient.IPSEXOPAC,
                                                      .AdmissionDate = TAdmissions.IFECHAING,
                                                      .AdmissionType = TAdmissions.TIPOINGRE,
                                                      .BedStay = TAdmissions.CODICAMHO,
                                                      .PlaceEntry = TAdmissions.IINGREPOR,
                                                      .LiquidationType = TAdmissions.ILIQUIDAC,
                                                      .BenefitPlan = TAdmissions.CODPANATE,
                                                      .EntityCode = TEntity.CODENTIDA,
                                                      .EntityName = TEntity.NOMENTIDA,
                                                      .AuthorizationNumber = TAdmissions.IAUTORIZA,
                                                      .ResponsibleName = TAdmissions.IPRNOMBRE,
                                                      .ResponsiblePhone = TAdmissions.IPTELEFON,
                                                      .CareGroupId = TAdmissions.GENCAREGROUP,
                                                      .HealthAdministratorId = TAdmissions.GENCONENTITY,
                                                      .Status = TAdmissions.IESTADOIN,
                                                      .FullNameAdmission = String.Format(Infrastructure.CrossCutting.Resources.ResourceManager.GetString("AdmissionResume", "IndigoCrystalHis"), TAdmissions.NUMINGRES.ToString().Trim(), TPatient.IPCODPACI.ToString().Trim(), TPatient.IPNOMCOMP.ToString().Trim())}

            e.QueryableSource = TmpQueryableSource
            e.Tag = tableAdmissions
            'End Using
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

#End Region

#Region "ListAdmissionsByPatientCodeStatusAccidenteTransito"

    Private _status As String
    ''' <summary>
    ''' Lista todos los ingresos que no estan anulados ni cancelados
    ''' </summary>
    Public Function ListAdmissionsPatientCodeStatus(PatientCode As String, status As String) As LinqInstantFeedbackSource
        Dim linqListAdmissionsPatientCodeStatus As New LinqInstantFeedbackSource
        AddHandler linqListAdmissionsPatientCodeStatus.GetQueryable, AddressOf plinqListAdmissionsPatientCodeStatus_GetQueryable
        linqListAdmissionsPatientCodeStatus.KeyExpression = "AdmissionCode"
        _patientCode = PatientCode
        _status = status
        Return linqListAdmissionsPatientCodeStatus
    End Function

    ''' <summary>
    ''' Ejecuta y obtiene un conjunto de datos
    ''' </summary>
    Private Sub plinqListAdmissionsPatientCodeStatus_GetQueryable(sender As Object, e As GetQueryableEventArgs)
        Try
            Dim session As New Session(XpoDefault.DataLayer)
            Dim tableAdmissions As New XPQuery(Of AdmissionXpo)(session)
            Dim tableProfessional As New XPQuery(Of HealthCareProfessionalXpo)(session)
            Dim tablePatient As New XPQuery(Of PatientXpo)(session)
            Dim tableEntity As New XPQuery(Of EntityXpo)(session)

            Dim FACTURED As String = _status 'Facturado
            Dim Dos As Integer = 2

            Dim TmpQueryableSource = From TAdmissions In tableAdmissions
                                     Join TPatient In tablePatient On TAdmissions.IPCODPACI Equals TPatient.IPCODPACI
                                     Join TEntity In tableEntity On TAdmissions.CODENTIDA.CODENTIDA Equals TEntity.CODENTIDA
                                     Where (TAdmissions.IESTADOIN = FACTURED AndAlso TPatient.IPCODPACI = _patientCode AndAlso TAdmissions.ITIPORIES = Dos)
                                     Select New With {.AdmissionCode = TAdmissions.NUMINGRES,
                                                      .PatientCode = TPatient.IPCODPACI,
                                                      .PatientName = TPatient.IPNOMCOMP,
                                                      .PatientDateBirth = TPatient.IPFECNACI,
                                                      .PatientGenus = TPatient.IPSEXOPAC,
                                                      .AdmissionDate = TAdmissions.IFECHAING,
                                                      .AdmissionType = TAdmissions.TIPOINGRE,
                                                      .BedStay = TAdmissions.CODICAMHO,
                                                      .PlaceEntry = TAdmissions.IINGREPOR,
                                                      .LiquidationType = TAdmissions.ILIQUIDAC,
                                                      .BenefitPlan = TAdmissions.CODPANATE,
                                                      .EntityCode = TEntity.CODENTIDA,
                                                      .EntityName = TEntity.NOMENTIDA,
                                                      .AuthorizationNumber = TAdmissions.IAUTORIZA,
                                                      .ResponsibleName = TAdmissions.IPRNOMBRE,
                                                      .ResponsiblePhone = TAdmissions.IPTELEFON,
                                                      .CareGroupId = TAdmissions.GENCAREGROUP,
                                                      .HealthAdministratorId = TAdmissions.GENCONENTITY,
                                                      .Status = TAdmissions.IESTADOIN,
                                                      .TRATAESPECIA = TAdmissions.TRATAESPECIA,
                                                      .FullNameAdmission = String.Format(Infrastructure.CrossCutting.Resources.ResourceManager.GetString("AdmissionResume", "IndigoCrystalHis"), TAdmissions.NUMINGRES.ToString().Trim(), TPatient.IPCODPACI.ToString().Trim(), TPatient.IPNOMCOMP.ToString().Trim())}

            e.QueryableSource = TmpQueryableSource
            e.Tag = tableAdmissions
            'End Using
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

#End Region

#Region "ListAdmissionsByPatientCodeStatus"

    ''' <summary>
    ''' Lista todos los ingresos que no estan anulados ni cancelados
    ''' </summary>
    Public Function ListAdmissionsByPatientCodeStatus(PatientCode As String, status As String) As LinqInstantFeedbackSource
        Dim linqListAdmissionsByPatientCodeStatus As New LinqInstantFeedbackSource
        AddHandler linqListAdmissionsByPatientCodeStatus.GetQueryable, AddressOf plinqListAdmissionsByPatientCodeStatus_GetQueryable
        linqListAdmissionsByPatientCodeStatus.KeyExpression = "AdmissionCode"
        _patientCode = PatientCode
        _status = status
        Return linqListAdmissionsByPatientCodeStatus
    End Function

    ''' <summary>
    ''' Ejecuta y obtiene un conjunto de datos
    ''' </summary>
    Private Sub plinqListAdmissionsByPatientCodeStatus_GetQueryable(sender As Object, e As GetQueryableEventArgs)
        Try
            Dim session As New Session(XpoDefault.DataLayer)
            Dim tableAdmissions As New XPQuery(Of AdmissionXpo)(session)
            Dim tableProfessional As New XPQuery(Of HealthCareProfessionalXpo)(session)
            Dim tablePatient As New XPQuery(Of PatientXpo)(session)
            Dim tableEntity As New XPQuery(Of EntityXpo)(session)
            Dim tableAdmissionType As New XPQuery(Of AdmissionTypeXpo)(session)

            Dim TmpQueryableSource = From TAdmissions In tableAdmissions
                                     Join TPatient In tablePatient On TAdmissions.IPCODPACI Equals TPatient.IPCODPACI
                                     Join TEntity In tableEntity On TAdmissions.CODENTIDA.CODENTIDA Equals TEntity.CODENTIDA
                                     Join TAdmissionType In tableAdmissionType On TAdmissionType.Id Equals TAdmissions.IdAdmissionType
                                     Where (TAdmissions.IESTADOIN = _status AndAlso TPatient.IPCODPACI = _patientCode)
                                     Select New With {.AdmissionCode = TAdmissions.NUMINGRES,
                                                      .PatientCode = TPatient.IPCODPACI,
                                                      .PatientName = TPatient.IPNOMCOMP,
                                                      .PatientDateBirth = TPatient.IPFECNACI,
                                                      .PatientGenus = TPatient.IPSEXOPAC,
                                                      .AdmissionDate = TAdmissions.IFECHAING,
                                                      .AdmissionType = TAdmissions.TIPOINGRE,
                                                      .BedStay = TAdmissions.CODICAMHO,
                                                      .PlaceEntry = TAdmissions.IINGREPOR,
                                                      .LiquidationType = TAdmissions.ILIQUIDAC,
                                                      .BenefitPlan = TAdmissions.CODPANATE,
                                                      .EntityCode = TEntity.CODENTIDA,
                                                      .EntityName = TEntity.NOMENTIDA,
                                                      .AuthorizationNumber = TAdmissions.IAUTORIZA,
                                                      .ResponsibleName = TAdmissions.IPRNOMBRE,
                                                      .ResponsiblePhone = TAdmissions.IPTELEFON,
                                                      .CareGroupId = TAdmissions.GENCAREGROUP,
                                                      .HealthAdministratorId = TAdmissions.GENCONENTITY,
                                                      .Status = TAdmissions.IESTADOIN,
                                                      .TRATAESPECIA = TAdmissions.TRATAESPECIA,
                                                      .AdmissionTypeCodeName = TAdmissionType.CodeName,
                                                       .Color = TAdmissionType.Color,
                                                      .FullNameAdmission = String.Format(Infrastructure.CrossCutting.Resources.ResourceManager.GetString("AdmissionResume", "IndigoCrystalHis"), TAdmissions.NUMINGRES.ToString().Trim(), TPatient.IPCODPACI.ToString().Trim(), TPatient.IPNOMCOMP.ToString().Trim())}

            e.QueryableSource = TmpQueryableSource
            e.Tag = tableAdmissions
            'End Using
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

#End Region

#Region "ListAdmissions Liquidation Linq"

    ''' <summary>
    ''' Lista todos los ingresos para el frontal de liquidacion
    ''' </summary>
    Public Function ListAdmissionsLiquidation() As LinqInstantFeedbackSource
        Dim linqListAdmissionsLiquidation As New LinqInstantFeedbackSource
        AddHandler linqListAdmissionsLiquidation.GetQueryable, AddressOf plinqListAdmissionsLiquidation_GetQueryable
        linqListAdmissionsLiquidation.KeyExpression = "AdmissionCode"
        Return linqListAdmissionsLiquidation
    End Function

    ''' <summary>
    ''' Ejecuta y obtiene un conjunto de datos
    ''' </summary>
    Private Sub plinqListAdmissionsLiquidation_GetQueryable(sender As Object, e As GetQueryableEventArgs)
        Try
            Dim session As New Session(XpoDefault.DataLayer)
            Dim tableAdmissions As New XPQuery(Of AdmissionXpo)(session)
            Dim tablePatient As New XPQuery(Of PatientXpo)(session)

            Dim TmpQueryableSource = From TAdmissions In tableAdmissions
                                     Join TPatient In tablePatient On TAdmissions.IPCODPACI Equals TPatient.IPCODPACI
                                     Select New With {.AdmissionCode = TAdmissions.NUMINGRES.Trim(),
                                                      .PatientCode = TPatient.IPCODPACI,
                                                      .PatientName = TPatient.IPNOMCOMP,
                                                      .AdmissionType = TAdmissions.TIPOINGRE,
                                                      .BedStay = TAdmissions.CODICAMHO,
                                                      .LiquidationType = TAdmissions.ILIQUIDAC,
                                                      .ResponsibleName = TAdmissions.IPRNOMBRE}

            e.QueryableSource = TmpQueryableSource
            e.Tag = tableAdmissions
            'End Using
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
        Dim session As New IndigoXPOSession(Of ViewAdmissionsToReportSlipOut)
        Dim _classEntity = session.GetClassInfo(GetType(ViewAdmissionsToReportSlipOut))
        Return New XPInstantFeedbackSource(_classEntity, Nothing, Nothing)
    End Function

    ''' <summary>
    ''' Lista todos los ingresos para el frontal de liquidacion
    ''' </summary>
    Public Function ListAdmissionsToReport() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ViewAdmissionsToReport)
        Dim _classEntity = session.GetClassInfo(GetType(ViewAdmissionsToReport))
        Return New XPInstantFeedbackSource(_classEntity, Nothing, Nothing)
    End Function

    ''' <summary>
    ''' Lista todos los ingresos para el frontal de listado de facturas
    ''' </summary>
    Public Function ListAdmissionsToLiquidation() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ViewAdmissionsToLiquidation)()
        Dim _classEntity = session.GetClassInfo(GetType(ViewAdmissionsToLiquidation))
        Return New XPInstantFeedbackSource(_classEntity, Nothing, Nothing)
    End Function

    ''' <summary>
    ''' Lista todos los ingresos de tipo consulta externa para control cuentas ambulatorios
    ''' </summary>
    Public Function ListAdmissionsToLiquidationByIINGREPOR() As XPInstantFeedbackSource
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("PlaceEntry = 2 Or PlaceEntry = 3")
        Dim session As New IndigoXPOSession(Of ViewAdmissionsToLiquidation)()
        Dim _classEntity = session.GetClassInfo(GetType(ViewAdmissionsToLiquidation))
        Return New XPInstantFeedbackSource(_classEntity, Nothing, criteria)
    End Function

    Public Function ListAdmissionsToLiquidationDashboard(codcenate As String) As XPCollection(Of ViewAdmissionsToLiquidationDashboard)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse($"CODCENATE='{codcenate}'")
        Dim session As New IndigoXPOSession(Of ViewAdmissionsToLiquidationDashboard)()
        Return New XPCollection(Of ViewAdmissionsToLiquidationDashboard)(session, criteria)
    End Function

    Public Function ListAdmissionsToLiquidationOncologycal(admissionCode As String, patientCode As String) As XPInstantFeedbackSource
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse($"PatientCode = '{patientCode}' And AdmissionCode <> '{admissionCode}'")
        Dim session As New IndigoXPOSession(Of ViewAdmissionToLiquidationOncologycal)()
        Dim _classEntity = session.GetClassInfo(GetType(ViewAdmissionToLiquidationOncologycal))
        Return New XPInstantFeedbackSource(_classEntity, Nothing, criteria)
    End Function

    Public Function ListAdmissionsToLiquidationConfirm() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ViewAdmissionsToLiquidationConfirm)()
        Dim _classEntity = session.GetClassInfo(GetType(ViewAdmissionsToLiquidationConfirm))
        Return New XPInstantFeedbackSource(_classEntity, Nothing, Nothing)
    End Function

    ''' <summary>
    ''' Lista los ingresos de tipo consulta externa para control cuentas ambulatorios
    ''' </summary>
    ''' <returns></returns>
    Public Function ListAdmissionsToLiquidationConfirmByIINGREPOR() As XPInstantFeedbackSource
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("PlaceEntry = 2 or PlaceEntry = 3")
        Dim session As New IndigoXPOSession(Of ViewAdmissionsToLiquidationConfirm)()
        Dim _classEntity = session.GetClassInfo(GetType(ViewAdmissionsToLiquidationConfirm))
        Return New XPInstantFeedbackSource(_classEntity, Nothing, criteria)
    End Function

    Public Function ListViewAdmissionsToLiquidationConfirmOnlyLiquidation() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ViewAdmissionsToLiquidationConfirmOnlyLiquidation)()
        Dim _classEntity = session.GetClassInfo(GetType(ViewAdmissionsToLiquidationConfirmOnlyLiquidation))
        Return New XPInstantFeedbackSource(_classEntity, Nothing, Nothing)
    End Function

    Public Function GetAdmissionObjectByNumIngres(admissionnumber As String) As XPCollection(Of ViewAdmissionsToLiquidation)
        Dim session As New IndigoXPOSession(Of ViewAdmissionsToLiquidation)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("AdmissionCode='" & admissionnumber & "'")
        Return New XPCollection(Of ViewAdmissionsToLiquidation)(session, criteria)
        'Using session As New Session(XpoDefault.DataLayer)
        '    Dim criteria As CriteriaOperator = CriteriaOperator.Parse("AdmissionCode='" & admissionnumber & "'")
        '    Return New XPCollection(Of ViewAdmissionsToLiquidation)(session, criteria)
        'End Using
    End Function

    Public Function GetAdmissionObjectByNumIngresAndIINGREPOR(admissionnumber As String) As XPCollection(Of ViewAdmissionsToLiquidation)
        Dim session As New IndigoXPOSession(Of ViewAdmissionsToLiquidation)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("AdmissionCode='" & admissionnumber & "' And (PlaceEntry = 2 or PlaceEntry = 3)")
        Return New XPCollection(Of ViewAdmissionsToLiquidation)(session, criteria)
        'Using session As New Session(XpoDefault.DataLayer)
        '    Dim criteria As CriteriaOperator = CriteriaOperator.Parse("AdmissionCode='" & admissionnumber & "'")
        '    Return New XPCollection(Of ViewAdmissionsToLiquidation)(session, criteria)
        'End Using
    End Function

#End Region

#Region "ViewListFuncionalUnitAuthorization Lista de Unidades Funcionales por permisos de usuario y grupos"

    ''' <summary>
    ''' Lista las unidades funcionales por permiso de usuario
    ''' </summary>
    Public Function ListViewListFuncionalUnitAuthorization(ByVal CodeGroup As String, ByVal CodeUsers As String) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ViewListFuncionalUnitAuthorization)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Codeusers='" & CodeUsers & "' or CodeGroup='" & CodeGroup & "'")
        Dim _classEntity = session.GetClassInfo(GetType(ViewListFuncionalUnitAuthorization))
        Return New XPInstantFeedbackSource(_classEntity, Nothing, criteria)
    End Function

#End Region

#Region "ListAdmissionByPatientCode"
    Public Function ListAdmissionsByPatientCode(patientCode As String) As XPInstantFeedbackSource
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("[PatientCode] = '" & patientCode & "'")
        Dim session As New IndigoXPOSession(Of ViewAdmissionsToLiquidation)()
        Dim _classEntity = session.GetClassInfo(GetType(ViewAdmissionsToLiquidation))
        Return New XPInstantFeedbackSource(_classEntity, Nothing, criteria)
    End Function
#End Region

#Region "ListHealthCareProfessional"
    ''' <summary>
    ''' lista Profesionales de la salud activos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListHealthCareProfessionalAll() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of HealthCareProfessionalXpo)()
        Dim _classEntity = session.GetClassInfo(GetType(HealthCareProfessionalXpo))
        Return New XPInstantFeedbackSource(_classEntity, "CodeName;CODPROSAL;NOMMEDICO;CODESPEC1;CODESPEC1.DESESPECI;StatusName", Nothing)
    End Function
    ''' <summary>
    ''' lista Profesionales de la salud activos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListHealthCareProfessional() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of HealthCareProfessionalXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("ESTADOMED=1")
        Dim _classEntity = session.GetClassInfo(GetType(HealthCareProfessionalXpo))
        Return New XPInstantFeedbackSource(_classEntity, Nothing, criteria)
    End Function
    ''' <summary>
    ''' Lists the health care professional by profile.
    ''' </summary>
    ''' <param name="profile">The profile.</param>
    ''' <returns></returns>
    Public Function ListHealthCareProfessionalByProfile(profile As List(Of Integer)) As XPInstantFeedbackSource
        Dim InOperat = String.Join(",", profile.ToArray())
        Dim session As New IndigoXPOSession(Of HealthCareProfessionalXpo)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("ESTADOMED=1 And MEDPERCIR In (" & InOperat & ")")
        Dim _classEntity = session.GetClassInfo(GetType(HealthCareProfessionalXpo))
        Return New XPInstantFeedbackSource(_classEntity, Nothing, criteria)
    End Function
    ''' <summary>
    ''' lista todos los Profesionales de la salud con xpCollection
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListHealthCareProfessionalXpCollection() As XPCollection(Of HealthCareProfessionalXpo)
        Dim session As New IndigoXPOSession(Of HealthCareProfessionalXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("ESTADOMED=1")
        Return New XPCollection(Of HealthCareProfessionalXpo)(session, criteria)
        'End Using
    End Function

    ''' <summary>
    ''' lista todos los Profesionales de la salud con xpCollection filtrado por la especialidad
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListHealthCareProfessionalSpecialtyXpCollection() As XPCollection(Of HealthCareProfessionalXpo)
        Dim session As New IndigoXPOSession(Of HealthCareProfessionalXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("CODESPEC1.CODESPECI='005' Or CODESPEC2.CODESPECI='002' Or CODESPEC3.CODESPECI='002'")
        Return New XPCollection(Of HealthCareProfessionalXpo)(session, criteria)
        'End Using
    End Function

    ''' <summary>
    ''' lista todos los Profesionales de la salud
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCareProfessionalByCode(code As String) As XPCollection(Of HealthCareProfessionalXpo)
        Dim session As New IndigoXPOSession(Of HealthCareProfessionalXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("CODPROSAL='" & code & "'")
        Return New XPCollection(Of HealthCareProfessionalXpo)(session, criteria)
        'End Using
    End Function

#End Region

#Region "ListSpecialtyByStatus"

    ''' <summary>
    ''' lista las especialidades por estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListSpecialty(ByVal status As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of SpecialtyXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("ESTADO=" & status)
        Dim _classEntity = session.GetClassInfo(GetType(SpecialtyXpo))
        Return New XPInstantFeedbackSource(_classEntity, "CODESPECI;DESESPECI;TIPATENCI;INDAUDFOR;ESTADO;CodeName", criteria)
    End Function

    Public Function ListSpecialtyXpCollection(ByVal status As Boolean) As XPCollection
        Dim session As New IndigoXPOSession(Of SpecialtyXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("ESTADO=" & status)
        Return New XPCollection(session, GetType(SpecialtyXpo), criteria)
        'End Using
    End Function

    ''' <summary>
    ''' lista las especialidades por estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListSpecialties(ByVal status As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of SpecialtyXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("ESTADO=" & status)
        Dim _classEntity = session.GetClassInfo(GetType(SpecialtyXpo))
        Return New XPInstantFeedbackSource(_classEntity, "CODESPECI;DESESPECI;TIPATENCI;INDAUDFOR;ESTADO;CodeName", criteria)
    End Function

#End Region

#Region "ListActivity"

    ''' <summary>
    ''' lista las Actividades
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListActivities() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ActivitiesXpo)()
        Dim _classEntity = session.GetClassInfo(GetType(ActivitiesXpo))
        Return New XPInstantFeedbackSource(_classEntity, "codactivi;desactivi;INDAUDFOR", Nothing)
    End Function

#End Region

#Region "ListLocations"

    ''' <summary>
    ''' lista las ubicaciones
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListLocations() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of LocationXpo)()
        Dim _classEntity = session.GetClassInfo(GetType(LocationXpo))
        Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "AUUBICACI;UBICODIGO;UBINOMBRE;DEPMUNCOD.DEPMUNCOD;DEPMUNCOD.MUNNOMBRE;DEPMUNCOD.DEPCODIGO.depcodigo;DEPMUNCOD.DEPCODIGO.nomdepart", Nothing)
        Return _serverMode
    End Function

#End Region

#Region "ListCompany"

    ''' <summary>
    ''' lista las empresas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListCompany() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CompanyXpo)
        Dim _classEntity = session.GetClassInfo(GetType(CompanyXpo))
        Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "CODEMPRES;DESEMPRES", Nothing)
        Return _serverMode
    End Function

#End Region

#Region "ListEthnicGroup"

    ''' <summary>
    ''' lista los grupos étnicos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListEthnicGroup() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of EthnicGroupXpo)()
        Dim _classEntity = session.GetClassInfo(GetType(EthnicGroupXpo))
        Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "CODGRUPOE;DESGRUPET", Nothing)
        Return _serverMode
    End Function

#End Region

#Region "ListEducationLevels"

    ''' <summary>
    ''' lista los niveles de educacion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListEducationLevels() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of EducationLevelsXpo)()

        Dim _classEntity = session.GetClassInfo(GetType(EducationLevelsXpo))
        Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "NIVECODIGO;NIVEDESCRI", Nothing)
        Return _serverMode
    End Function

#End Region

#Region "ListLevels"

    ''' <summary>
    ''' lista los niveles
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListLevels() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of LevelsXpo)()

        Dim _classEntity = session.GetClassInfo(GetType(LevelsXpo))
        Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "NIVCODIGO;NIVDESCRI", Nothing)
        Return _serverMode
    End Function

#End Region

#Region "ListLanguage"

    ''' <summary>
    ''' lista los lenguajes
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListLanguage() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of LanguageXpo)()

        Dim _classEntity = session.GetClassInfo(GetType(LanguageXpo))
        Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "IDICODIGO;IDIDESCRI", Nothing)
        Return _serverMode
    End Function

#End Region

#Region "ListBelief"

    ''' <summary>
    ''' lista las creencias
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListBelief() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of BeliefXpo)()

        Dim _classEntity = session.GetClassInfo(GetType(BeliefXpo))
        Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "CREDCODIGO;CREDDESCRI", Nothing)
        Return _serverMode
    End Function

#End Region

#Region "ListSpecialGroups"

    ''' <summary>
    ''' lista los grupos especiales
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListSpecialGroups() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of SpecialGroupsXpo)()

        Dim _classEntity = session.GetClassInfo(GetType(SpecialGroupsXpo))
        Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "GRUPCODIGO;GRUPDESCRI", Nothing)
        Return _serverMode
    End Function

#End Region

#Region "ListDisability"

    ''' <summary>
    ''' lista las incapacidades
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListDisability() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of DisabilidyXpo)()

        Dim _classEntity = session.GetClassInfo(GetType(DisabilidyXpo))
        Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "DISCCODIGO;DISCDESCRI", Nothing)
        Return _serverMode
    End Function

#End Region

#Region "ListCenters"
    ''' <summary>
    ''' Lista de centros de atención
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListCenters() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CentersXpo)()

        Dim _classEntity = session.GetClassInfo(GetType(CentersXpo))
        Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "CODCENATE;NOMCENATE;CODIPSSEC;NIVATENCI;DIRCENATE;INDNUMTEL;DEPMUNCOD;CodeName", Nothing)
        Return _serverMode
    End Function

#End Region

#Region "ListCentersPatientDeparture"
    ''' <summary>
    ''' Lista de centros de atención
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListCentersPatientDepartureHIS(CodigoUsuario As String) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ViewListCenterHis)()

        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("CodigoUsuario='" & CodigoUsuario & "'")
        Dim _classEntity = session.GetClassInfo(GetType(ViewListCenterHis))
        Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "Codigo;CentroAtencion;CodigoDescripcion;CodigoUsuario", criteria)
        Return _serverMode
    End Function

    ''' <summary>
    ''' Lista de centros de atención
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListPatientDepartureHIS(CenterCareCode As String, UnitFunctionalCode As String) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ViewPatientDeparture)()

        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("UnitFunctional IN (" & UnitFunctionalCode & ")" & " AND CenterCare = '" & CenterCareCode & "'")
        Dim _classEntity = session.GetClassInfo(GetType(ViewPatientDeparture))
        Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "UnitFunctional;UnitFunctionalDescr;CenterCare;NomCama;IdenUsua;NomUsua;NomMed;FechaEgresoM;TiempoEgreso;NomEnf;FechaEgresoE;NumIng", criteria)
        Return _serverMode
    End Function

    ''' <summary>
    ''' lista unidadesfuncionales
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListUnitFunctionalPatientDepartureHIS(CodigoUsuario As String, CareCenter As String) As XPCollection(Of ViewUnitFunctionalHis)
        Dim session As New IndigoXPOSession(Of ViewUnitFunctionalHis)()

        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("CodigoUsuario='" & CodigoUsuario & "'" & " AND " & " CenterCode='" & CareCenter & "'")
        Dim collect As XPCollection(Of ViewUnitFunctionalHis) = New XPCollection(Of ViewUnitFunctionalHis)(session, criteria)
        'collect.Sorting.Add(New SortProperty("FechaOrden", DevExpress.Xpo.DB.SortingDirection.Ascending))
        Return collect
        'End Using
    End Function



#End Region

    ''' <summary>
    ''' Lista las RIAS por estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListRIAS(status As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of RIASXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("ESTADO = " & status & "")
        Dim _classEntity = session.GetClassInfo(GetType(RIASXpo))
        Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "ID;CODPRO;NOMBRE;EDADMINIMA;EDADMAXIMA;TIPOEDAD;SEXO;ESTADO;FORMUCARATE;MENPACNOINS;MENPACINASI;APLICARESTRICCION;CodeName", criteria)
        Return _serverMode
    End Function

    Public Function ListRIASXpCollection(ByVal status As Integer) As XPCollection
        Dim session As New IndigoXPOSession(Of RIASXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("ESTADO = " & status & "")
        Return New XPCollection(session, GetType(RIASXpo), criteria)
        'End Using
    End Function

    ''' <summary>
    ''' Lista los medicos que tengan asociado el contrato
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListHealthProfessionalCodeByMedicalFeesContractId(medicalFeesContractId As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of HealthCareProfessionalXpo)()

        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("GENCONTRA=" & medicalFeesContractId & "")
        Dim _classEntity = session.GetClassInfo(GetType(HealthCareProfessionalXpo))
        Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "CodeName;CODPROSAL;NOMMEDICO;GENCONTRA", criteria)
        Return _serverMode
    End Function

    ''' <summary>
    ''' Gets the bed rate by bed identifier.
    ''' </summary>
    ''' <param name="bedId">The bed identifier.</param>
    ''' <returns></returns>
    Public Function GetBedRateByBedId(bedId As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CHGENTARI)()

        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("CODICAMAS.CODICAMAS=" & bedId)
        Dim _classEntity = session.GetClassInfo(GetType(CHGENTARI))
        Dim _serverMode = New XPInstantFeedbackSource(_classEntity, Nothing, criteria)
        Return _serverMode
    End Function

    ''' <summary>
    ''' Lista los tipos de estancia
    ''' </summary>
    ''' <returns></returns>
    Public Function GetAllStayType() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CHTIPESTA)()

        Dim _classEntity = session.GetClassInfo(GetType(CHTIPESTA))
        Dim _serverMode = New XPInstantFeedbackSource(_classEntity, Nothing, Nothing)
        Return _serverMode
    End Function

    ''' <summary>
    ''' Lista los tipos de estancia asociados a una unidad funcional en la
    ''' tarifa de camas. Se navega por la colección CHGENTARIs de CHTIPESTA
    ''' hasta la unidad funcional (INUNIFUNC.UFUCODIGO).
    ''' </summary>
    ''' <param name="FunctionalUnitCode">Código de la unidad funcional (INUNIFUNC.UFUCODIGO)</param>
    Public Function GetAllStayTypeActiveByFunctionalUnit(FunctionalUnitCode As String) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CHTIPESTA)()

        ' Se usa la sintaxis de colección de XPO: CHGENTARIs[UFUCODIGO.UFUCODIGO = ?]
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(
            "CHGENTARIs[UFUCODIGO.UFUCODIGO = ?]",
            FunctionalUnitCode)

        Dim _classEntity = session.GetClassInfo(GetType(CHTIPESTA))
        Return New XPInstantFeedbackSource(_classEntity, Nothing, criteria)
    End Function

    ''' <summary>
    ''' Lista todos los centros de atencion
    ''' </summary>
    ''' <returns></returns>
    Public Function GetAllCareCenter() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ADCENATEN)()

        Dim _classEntity = session.GetClassInfo(GetType(ADCENATEN))
        Dim _serverMode = New XPInstantFeedbackSource(_classEntity, Nothing, Nothing)
        Return _serverMode
    End Function

    ''' <summary>
    ''' Lista todas las unidades funcionales
    ''' </summary>
    ''' <returns></returns>
    Public Function GetAllFunctionalUnit() As XPInstantFeedbackSource
        Dim session = New IndigoXPOSession(Of INUNIFUNC)()
        Dim _classEntity = session.GetClassInfo(GetType(INUNIFUNC))
        Dim _serverMode = New XPInstantFeedbackSource(_classEntity, Nothing, Nothing)
        Return _serverMode
    End Function

    Public Function GetAllAGACTIMED() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of AGACTIMED)()

        Dim _classEntity = session.GetClassInfo(GetType(AGACTIMED))
        Dim _serverMode = New XPInstantFeedbackSource(_classEntity, Nothing, Nothing)
        Return _serverMode
    End Function

#Region "ListDashBoardPharmacy"
    ''' <summary>
    ''' lista todos los Profesionales de la salud
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListDashBoardPharmacy(codeCareCenter As String) As XPCollection(Of ViewDashBoardPharmacy)
        Dim session As New IndigoXPOSession(Of ViewDashBoardPharmacy)()

        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("CodigoCentroAtencion='" & codeCareCenter & "'")
        Dim collect As XPCollection(Of ViewDashBoardPharmacy) = New XPCollection(Of ViewDashBoardPharmacy)(session, criteria)
        collect.Sorting.Add(New SortProperty("FechaOrden", DevExpress.Xpo.DB.SortingDirection.Ascending))
        Return collect
        'End Using
    End Function

    Public Function ListDashBoardPharmacyDetail(consecutive As Decimal, patienCode As String, admission As String) As XPCollection(Of ViewDashboardPharmacyDetail)
        Dim session As New IndigoXPOSession(Of ViewDashboardPharmacyDetail)()

        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Consecutivo='" & consecutive & "' and CodigoPaciente ='" & patienCode & "' and Ingreso='" & admission & "' and Estado = 1")
        Dim collect As XPCollection(Of ViewDashboardPharmacyDetail) = New XPCollection(Of ViewDashboardPharmacyDetail)(session, criteria)
        Return collect
        'End Using
    End Function


    ''' <summary>
    ''' lista las solicitudes intrahospitalarias segun el tipo de sus detalles
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListDashBoardPharmacyDetailByTypeFilter(consecutive As Decimal, patienCode As String, admission As String, typeFilters As String) As XPCollection(Of ViewDashboardPharmacyDetail)
        Dim session As New IndigoXPOSession(Of ViewDashboardPharmacyDetail)()

        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Consecutivo='" & consecutive & "' and CodigoPaciente ='" & patienCode & "' and Ingreso='" & admission & "' and Estado = 1 AND TIPPRODUC IN(" & typeFilters & ")")
        Dim collect As XPCollection(Of ViewDashboardPharmacyDetail) = New XPCollection(Of ViewDashboardPharmacyDetail)(session, criteria)
        Return collect
        'End Using
    End Function

    Public Function ListDashBoardPharmacyDetailMixingStation(consecutive As Decimal, patienCode As String, admission As String) As XPCollection(Of ViewDashboardPharmacyDetailMixingSation)
        Dim session As New IndigoXPOSession(Of ViewDashboardPharmacyDetailMixingSation)()

        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Consecutivo='" & consecutive & "' and CodigoPaciente ='" & patienCode & "' and Ingreso='" & admission & "'")
        Dim collect As XPCollection(Of ViewDashboardPharmacyDetailMixingSation) = New XPCollection(Of ViewDashboardPharmacyDetailMixingSation)(session, criteria)
        Return collect
        'End Using
    End Function

    Public Function ListDashBoardPharmacy_SurgicalPackageDetails(consecutive As Decimal, patienCode As String, admission As String) As XPCollection(Of ViewDashBoardPharmacy_SurgicalPackageDeatils)
        Dim session As New IndigoXPOSession(Of ViewDashBoardPharmacy_SurgicalPackageDeatils)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Consecutivo='" & consecutive & "' and CodigoPaciente ='" & patienCode & "' and Ingreso='" & admission & "' and Estado = 1")
        Dim collect As XPCollection(Of ViewDashBoardPharmacy_SurgicalPackageDeatils) = New XPCollection(Of ViewDashBoardPharmacy_SurgicalPackageDeatils)(session, criteria)
        Return collect
        'End Using
    End Function

    Public Function ListDashBoardPharmacy_SurgicalPackageByTypeDetails(consecutive As Decimal, patienCode As String, admission As String, typeFilters As String) As XPCollection(Of ViewDashBoardPharmacy_SurgicalPackageDeatils)
        Dim session As New IndigoXPOSession(Of ViewDashBoardPharmacy_SurgicalPackageDeatils)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Consecutivo='" & consecutive & "' and CodigoPaciente ='" & patienCode & "' and Ingreso='" & admission & "' and Estado = 1 AND TIPPRODUC IN(" & typeFilters & ")")
        Dim collect As XPCollection(Of ViewDashBoardPharmacy_SurgicalPackageDeatils) = New XPCollection(Of ViewDashBoardPharmacy_SurgicalPackageDeatils)(session, criteria)
        Return collect
        'End Using
    End Function

    ''' <summary>
    '''  lista las solicitudes intrah.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListDashBoardPharmacyXPInstant(codeCareCenter As String) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ViewDashBoardPharmacy)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("CodigoCentroAtencion in (" & codeCareCenter & ") AND (TIPOSOLICITUD = 0 OR TIPOSOLICITUD = 1 OR TIPOSOLICITUD = 3) AND PatientDischarge =0 ")
        Dim _classEntity = session.GetClassInfo(GetType(ViewDashBoardPharmacy))
        Dim _serverMode = New XPInstantFeedbackSource(_classEntity, Nothing, criteria)
        Return _serverMode
    End Function

    ''' <summary>
    ''' lista las solicitudes intrah. con el filtro de tipo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListDashBoardPharmacyByTypeXPInstant(codeCareCenter As String, medicamentos As Nullable(Of Integer), insumos As Nullable(Of Integer), medicamentos_insumos As Nullable(Of Integer)) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ViewDashBoardPharmacy)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("CodigoCentroAtencion in (" & codeCareCenter & ") 
                                                            AND (TIPOSOLICITUD = 0 OR TIPOSOLICITUD = 1 OR TIPOSOLICITUD = 3) AND 
                                                            PatientDischarge =0 
                                                            AND( MEDICAMENTO " & If(medicamentos Is Nothing, " IS NULL", $" = {medicamentos}") &
                                                           " OR INSUMOS " & If(insumos Is Nothing, "IS NULL", $" = {insumos}") &
                                                            " OR MEDICAMENTO_INSUMO " & If(medicamentos_insumos Is Nothing, "IS NULL", $" = {medicamentos_insumos}") & ")")
        Dim _classEntity = session.GetClassInfo(GetType(ViewDashBoardPharmacy))
        Dim _serverMode = New XPInstantFeedbackSource(_classEntity, Nothing, criteria)
        Return _serverMode
    End Function

    ''' <summary>
    ''' lista las solicitudes de central de mezclas por paciente
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListDashBoardPharmacyMixingStationPatientXPInstant(codeCareCenter As String) As XPCollection(Of ViewDashBoardPharmacyMixingSationPatient)
        Dim session As New IndigoXPOSession(Of ViewDashBoardPharmacyMixingSationPatient)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("CodigoCentroAtencion in (" & codeCareCenter & ")")
        Dim collect As XPCollection(Of ViewDashBoardPharmacyMixingSationPatient) = New XPCollection(Of ViewDashBoardPharmacyMixingSationPatient)(session, criteria)
        Return collect
    End Function

    ''' <summary>
    ''' lista las solicitud de central de mezclas del paciente
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListDashBoardPharmacyMixingStationXPCollection(codeCareCenter As String) As XPCollection(Of ViewDashBoardPharmacyMixingSation)
        Dim session As New IndigoXPOSession(Of ViewDashBoardPharmacyMixingSation)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("CodigoCentroAtencion in (" & codeCareCenter & ")")
        Dim collect As XPCollection(Of ViewDashBoardPharmacyMixingSation) = New XPCollection(Of ViewDashBoardPharmacyMixingSation)(session, criteria)
        Return collect
    End Function

    ''' <summary>
    ''' Consulta los datos de cancelacion o reprogramacion de una cita
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CitaCanceladaReprogramaXPInstant(cODAUTONU As Long) As ViewCitaCanceladaReprogramada
        Dim session As New IndigoXPOSession(Of ViewCitaCanceladaReprogramada)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("CODAUTONU = " & cODAUTONU & "")
        Dim collect As XPCollection(Of ViewCitaCanceladaReprogramada) = New XPCollection(Of ViewCitaCanceladaReprogramada)(session, criteria)
        Return collect.FirstOrDefault
    End Function

    ''' <summary>
    ''' lista quimioterapias
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListDashBoardPharmacyChemotherapyXPInstant(codeCareCenter As String) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ViewDashBoardPharmacyChemotherapy)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("CodigoCentroAtencion in (" & codeCareCenter & ")")
        Dim _classEntity = session.GetClassInfo(GetType(ViewDashBoardPharmacyChemotherapy))
        Dim _serverMode = New XPInstantFeedbackSource(_classEntity, Nothing, criteria)
        Return _serverMode
    End Function

    ''' <summary>
    ''' lista quimioterapias por tipo 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListDashBoardPharmacyChemotherapyByTypeXPInstant(codeCareCenter As String, medicamentos As Nullable(Of Integer), insumos As Nullable(Of Integer), medicamentos_insumos As Nullable(Of Integer)) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ViewDashBoardPharmacyChemotherapy)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("CodigoCentroAtencion in (" & codeCareCenter & ") 
                                                                   AND  TIPOSOLICITUD = 2 AND (PERMITEEXTRA IS NULL OR PERMITEEXTRA = 1)
                                                                   AND( MEDICAMENTO=" & If(medicamentos Is Nothing, "NULL", medicamentos) &
                                                                   " OR INSUMOS=" & If(insumos Is Nothing, "NULL", insumos) &
                                                                   " OR MEDICAMENTO_INSUMO=" & If(medicamentos_insumos Is Nothing, "NULL", medicamentos_insumos) & ")")
        Dim _classEntity = session.GetClassInfo(GetType(ViewDashBoardPharmacyChemotherapy))
        Dim _serverMode = New XPInstantFeedbackSource(_classEntity, Nothing, criteria)
        Return _serverMode
    End Function

    Public Function ListDashBoardPharmacyXPInstantExtramural(codeCareCenter As String) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ViewDashBoardPharmacy)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("CodigoCentroAtencion in (" & codeCareCenter & ") AND  TIPOSOLICITUD = 2 AND (PERMITEEXTRA IS NULL OR PERMITEEXTRA = 1)")
        Dim _classEntity = session.GetClassInfo(GetType(ViewDashBoardPharmacy))
        Dim _serverMode = New XPInstantFeedbackSource(_classEntity, Nothing, criteria)
        Return _serverMode
    End Function

    Public Function ListDashBoardPharmacyByTypeXPInstantExtramural(codeCareCenter As String, medicamentos As Nullable(Of Integer), insumos As Nullable(Of Integer), medicamentos_insumos As Nullable(Of Integer)) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ViewDashBoardPharmacy)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("CodigoCentroAtencion in (" & codeCareCenter & ") 
                                                                   AND  TIPOSOLICITUD = 2 AND (PERMITEEXTRA IS NULL OR PERMITEEXTRA = 1)
                                                                   AND( MEDICAMENTO=" & If(medicamentos Is Nothing, "NULL", medicamentos) &
                                                                   " OR INSUMOS=" & If(insumos Is Nothing, "NULL", insumos) &
                                                                   " OR MEDICAMENTO_INSUMO=" & If(medicamentos_insumos Is Nothing, "NULL", medicamentos_insumos) & ")")
        Dim _classEntity = session.GetClassInfo(GetType(ViewDashBoardPharmacy))
        Dim _serverMode = New XPInstantFeedbackSource(_classEntity, Nothing, criteria)
        Return _serverMode
    End Function

    ''' <summary>
    ''' Lista las solicitudes de Farmacia por el código del paciente
    ''' </summary>
    ''' <param name="PatientCode">Código del paciente a consultar</param>
    ''' <returns></returns>
    Public Function ListDashBoardPharmacyByPatientXPInstant(PatientCode As String) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ViewDashBoardPharmacy)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("CodigoPaciente = '" & PatientCode & "'")
        Dim _classEntity = session.GetClassInfo(GetType(ViewDashBoardPharmacy))
        Dim _serverMode = New XPInstantFeedbackSource(_classEntity, Nothing, criteria)
        Return _serverMode
    End Function

    ''' <summary>
    ''' Lista los items para central de mezclas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListDashBoardCentralMixXPInstant(codeCareCenter As String) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ViewDashBoardPharmacy)()
        Dim _classEntity = session.GetClassInfo(GetType(ViewDashBoardPharmacy))
        Dim _serverMode = New XPInstantFeedbackSource(_classEntity, Nothing, Nothing)
        Return _serverMode
    End Function

    ''' <summary>
    ''' lista los paciente para cirugia - paquetes QX
    ''' </summary>
    ''' <param name="codeCareCenter"></param>
    ''' <returns></returns>
    Public Function ListDashBoardPharmacy_SurgicalPackageXPInstant(codeCareCenter As String) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ViewDashBoardPharmacy_SurgicalPackage)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("CodigoCentroAtencion in (" & codeCareCenter.Trim & ")")
        Dim _classEntity = session.GetClassInfo(GetType(ViewDashBoardPharmacy_SurgicalPackage))
        Dim _serverMode = New XPInstantFeedbackSource(_classEntity, Nothing, criteria)
        Return _serverMode
    End Function

    ''' <summary>
    ''' lista los paciente para cirugia - paquetes QX ccon el filtro de tipo
    ''' </summary>
    ''' <param name="codeCareCenter"></param>
    ''' <returns></returns>
    Public Function ListDashBoardPharmacy_SurgicalPackageByTypeXPInstant(codeCareCenter As String, medicamentos As Nullable(Of Integer), insumos As Nullable(Of Integer), medicamentos_insumos As Nullable(Of Integer)) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ViewDashBoardPharmacy_SurgicalPackage)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("CodigoCentroAtencion in (" & codeCareCenter.Trim & ")
                                                                   AND( MEDICAMENTO=" & If(medicamentos Is Nothing, "NULL", medicamentos) &
                                                                   " OR INSUMOS=" & If(insumos Is Nothing, "NULL", insumos) &
                                                                   " OR MEDICAMENTO_INSUMO=" & If(medicamentos_insumos Is Nothing, "NULL", medicamentos_insumos) & ")")
        Dim _classEntity = session.GetClassInfo(GetType(ViewDashBoardPharmacy_SurgicalPackage))
        Dim _serverMode = New XPInstantFeedbackSource(_classEntity, Nothing, criteria)
        Return _serverMode
    End Function

    Public Function ListDashBoardPharmacyDetailMixingStationGrid(consecutive As Decimal, admission As String) As XPCollection(Of ViewDashboardPharmacyDetailMixingSationGrid)
        Dim session As New IndigoXPOSession(Of ViewDashboardPharmacyDetailMixingSationGrid)()

        Dim criteria As CriteriaOperator = CriteriaOperator.Parse($"Consecutivo={consecutive} and Ingreso='{admission}'")
        Dim collect As XPCollection(Of ViewDashboardPharmacyDetailMixingSationGrid) = New XPCollection(Of ViewDashboardPharmacyDetailMixingSationGrid)(session, criteria)
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
        Dim session As New IndigoXPOSession(Of ViewDashBoardPharmacyDevolution)()

        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("CodigoCentroAtencion='" & codeCareCenter & "'")
        Dim collect As XPCollection(Of ViewDashBoardPharmacyDevolution) = New XPCollection(Of ViewDashBoardPharmacyDevolution)(session, criteria)
        collect.Sorting.Add(New SortProperty("FechaDevolucion", DevExpress.Xpo.DB.SortingDirection.Ascending))
        Return collect
        'End Using
    End Function

    Public Function ListDashBoardPharmacyDetailDevolutionCollection(consecutive As Integer, patientCode As String, admission As String) As XPCollection(Of ViewDashboardPharmacyDetailDevolution)
        Dim session As New IndigoXPOSession(Of ViewDashboardPharmacyDetailDevolution)()

        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Consecutivo=" & consecutive & " and CodigoPacienteDevolucion ='" & patientCode & "' and Ingreso='" & admission & "'")
        Dim collect As XPCollection(Of ViewDashboardPharmacyDetailDevolution) = New XPCollection(Of ViewDashboardPharmacyDetailDevolution)(session, criteria)
        Return collect
        'End Using
    End Function

    Public Function ListDashBoardPharmacyDetailDevolutionByTypeCollection(consecutive As Integer, patientCode As String, admission As String, typeFilters As String) As XPCollection(Of ViewDashboardPharmacyDetailDevolution)
        Dim session As New IndigoXPOSession(Of ViewDashboardPharmacyDetailDevolution)()

        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Consecutivo=" & consecutive & " and CodigoPacienteDevolucion ='" & patientCode & "' and Ingreso='" & admission & "' AND TIPPRODUC IN(" & typeFilters & ")")
        Dim collect As XPCollection(Of ViewDashboardPharmacyDetailDevolution) = New XPCollection(Of ViewDashboardPharmacyDetailDevolution)(session, criteria)
        Return collect
        'End Using
    End Function

    Public Function ListDashBoardPharmacyDevolutionXPInstant(codeCareCenter As String) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ViewDashBoardPharmacyDevolution)()

        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("CodigoCentroAtencion in (" & codeCareCenter & ")")
        Dim _classEntity = session.GetClassInfo(GetType(ViewDashBoardPharmacyDevolution))
        Dim _serverMode = New XPInstantFeedbackSource(_classEntity, Nothing, criteria)
        Return _serverMode
    End Function

    Public Function ListDashBoardPharmacyDevolutionByTypeXPInstant(codeCareCenter As String, medicamentos As Nullable(Of Integer), insumos As Nullable(Of Integer), medicamentos_insumos As Nullable(Of Integer)) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ViewDashBoardPharmacyDevolution)()

        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("CodigoCentroAtencion in (" & codeCareCenter & ")
                                                                   AND( MEDICAMENTO=" & If(medicamentos Is Nothing, "NULL", medicamentos) &
                                                                   " OR INSUMOS=" & If(insumos Is Nothing, "NULL", insumos) &
                                                                   " OR MEDICAMENTO_INSUMO=" & If(medicamentos_insumos Is Nothing, "NULL", medicamentos_insumos) & ")")
        Dim _classEntity = session.GetClassInfo(GetType(ViewDashBoardPharmacyDevolution))
        Dim _serverMode = New XPInstantFeedbackSource(_classEntity, Nothing, criteria)
        Return _serverMode
    End Function
#End Region

#Region "GetAdmissionByCodeAdmission"

    ''' <summary>
    ''' lista todos un numero de admision por codigo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAdmissionsToReportSlipOutById(AdmissionCode As String) As XPCollection(Of ViewAdmissionsToReportSlipOut)
        Dim session As New IndigoXPOSession(Of ViewAdmissionsToReportSlipOut)()

        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("AdmissionCode ='" & AdmissionCode & "'")
        Dim collect As XPCollection(Of ViewAdmissionsToReportSlipOut) = New XPCollection(Of ViewAdmissionsToReportSlipOut)(session, criteria)
        Return collect
        'End Using
    End Function

    ''' <summary>
    ''' lista todos un numero de admision por codigo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListByCriteriaReport(CriteriaReport As String) As XPCollection(Of ViewAdmissionsToReportSlipOutReport)
        Dim session As New IndigoXPOSession(Of ViewAdmissionsToReportSlipOutReport)()

        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(CriteriaReport)
        Dim collect As XPCollection(Of ViewAdmissionsToReportSlipOutReport) = New XPCollection(Of ViewAdmissionsToReportSlipOutReport)(session, criteria)
        Return collect
        'End Using
    End Function

    ''' <summary>
    ''' lista todos un numero de admision por codigo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAdmissionsToReportById(AdmissionCode As String) As XPCollection(Of ViewAdmissionsToReport)
        Dim session As New IndigoXPOSession(Of ViewAdmissionsToReport)()

        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("AdmissionCode ='" & AdmissionCode & "'")
        Dim collect As XPCollection(Of ViewAdmissionsToReport) = New XPCollection(Of ViewAdmissionsToReport)(session, criteria)
        Return collect
        'End Using
    End Function

    '' <summary>
    ''' lista todos un numero de admision por codigo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAdmissionsToReport(AdmissionCode As String) As XPCollection(Of VAdmission)
        Dim session As New IndigoXPOSession(Of VAdmission)()

        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("NUMEROINGRESO ='" & AdmissionCode & "'")
        Dim collect As XPCollection(Of VAdmission) = New XPCollection(Of VAdmission)(session, criteria)
        collect.Sorting.Add(New SortProperty("FECHAHOSPITALIZACION", DevExpress.Xpo.DB.SortingDirection.Descending))
        Return collect
        'End Using
    End Function

#End Region

#Region "ListTown"
    ''' <summary>
    ''' Lista de centros de atención
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListTown() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of TownXpo)()

        Dim _classEntity = session.GetClassInfo(GetType(TownXpo))
        Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "DEPMUNCOD;MUNCODIGO;MUNNOMBRE;DEPCODIGO.depcodigo;DEPCODIGO.nomdepart;CodeName", Nothing)
        Return _serverMode
    End Function
#End Region

#Region "ListIPS"
    ''' <summary>
    ''' Lista de centros de atención
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListIPS() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ADCONTIPS)()

        Dim _classEntity = session.GetClassInfo(GetType(ADCONTIPS))
        Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "CODIGOIPS;DSCRIPIPS;CodeName", Nothing)
        Return _serverMode
    End Function

    ''' <summary>
    ''' Lista las IPS por estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListIPSByStatus(status As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ADCONTIPS)()

        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("ESTADO=" & status)
        Dim _classEntity = session.GetClassInfo(GetType(ADCONTIPS))
        Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "CODIGOIPS;DSCRIPIPS;ESTADO;CodeName", criteria)
        Return _serverMode
    End Function
#End Region

#Region "CUPS"
    ''' <summary>
    ''' Lista las IPS por estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListCUPSByStatus(status As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CUPSXpo)()

        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("SIPSESTADO=" & status)
        Dim _classEntity = session.GetClassInfo(GetType(CUPSXpo))
        Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "CODSERIPS;DESSERIPS;SIPSESTADO;TIPSERIPS;CodeName", criteria)
        Return _serverMode
    End Function

    Function ListCUPSCrystalByStatusType(status As Boolean, type As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CUPSXpo)()

        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("SIPSESTADO=" & status & " AND TIPSERIPS=" & type)
        Dim _classEntity = session.GetClassInfo(GetType(CUPSXpo))
        Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "CODSERIPS;DESSERIPS;SIPSESTADO;CodeName", criteria)
        Return _serverMode
    End Function
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    Function GetCUPSCrystalByCode(code As String) As CUPSXpo
        Dim session As New IndigoXPOSession(Of CUPSXpo)()

        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(String.Format("CODSERIPS='{0}'", code))
        Dim collect As XPCollection(Of CUPSXpo) = New XPCollection(Of CUPSXpo)(session, criteria)
        Return collect.FirstOrDefault
    End Function
#End Region

#Region "AGENSALAC"

    ''' <summary>
    ''' DataSource de la Sala
    ''' </summary>
    ''' <param name="ServiceType"></param>
    ''' <param name="AttetionCenter"></param>
    ''' <returns></returns>
    Function GetRoomWithFilters(ServiceType As Byte, AttetionCenter As String) As List(Of AGENSALACXpo)
        Dim session As New IndigoXPOSession(Of AGENSALACXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(String.Format("TIPOSALA='D' AND TIPOSERVIC = {0} AND CODCENATE = '{1}' AND ESTADO = 1", ServiceType, AttetionCenter))

        Dim collect As XPCollection(Of AGENSALACXpo) = New XPCollection(Of AGENSALACXpo)(session, criteria)
        Return collect.ToList()
    End Function

#End Region
#Region "AGENSALAACT"
    ''' <summary>
    ''' DataSource de actividades de agendamiento
    ''' </summary>
    ''' <param name="RoomCode"></param>
    ''' <returns></returns>
    Function GetScheduleActivities(RoomCode As Integer) As List(Of AGENSALAACTXpo)
        Dim session As New IndigoXPOSession(Of AGENSALAACTXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(String.Format(" CODCONCEC = {0} AND AGACTIMED.ACTIVICON = 2 AND AGACTIMED.ESTADOACT = 1", RoomCode))

        Dim collect As XPCollection(Of AGENSALAACTXpo) = New XPCollection(Of AGENSALAACTXpo)(session, criteria)
        Return collect.ToList()
    End Function
#End Region
#Region "ViewListCupsByAGACTIMED"

    Public Function ListCUPSByCODACTMED(CODACTMED As String) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ViewListCupsByAGACTIMEDXpo)()

        Dim criteria As CriteriaOperator = CriteriaOperator.Parse($"CODACTMED='{CODACTMED}'")
        Dim _classEntity = session.GetClassInfo(GetType(ViewListCupsByAGACTIMEDXpo))
        Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "CODSERIPS;DESSERIPS;CODACTMED;CodeName;TIPSERIPS;HaveDescription", criteria)
        Return _serverMode
    End Function

    ''' <summary>
    ''' funcion que consulta las actividades de agendamiento por especialidad para los tipos de actividad otros
    ''' </summary>
    ''' <param name="especialityCode"></param>
    ''' <returns></returns>
    Public Function GetScheduleActivityOther(especialityCode As String) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ViewScheduleActivityByEspecialityXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse($"EspecialityCode={especialityCode}")
        Dim _classEntity = session.GetClassInfo(GetType(ViewScheduleActivityByEspecialityXpo))
        Dim _serverMode = New XPInstantFeedbackSource(_classEntity, Nothing, criteria)
        Return _serverMode
    End Function

    ''' <summary>
    ''' funcion que consulta los consultorios 
    ''' </summary>
    ''' <returns></returns>
    Public Function GetConsultinRoom(centerAttentionCode As String, Optional dateConsulting As Date? = Nothing) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ViewConsultinRoomgByProfessionalXpo)()
        Dim filterDate = String.Empty
        If dateConsulting IsNot Nothing Then
            filterDate = $"AND AppointmentDate = #{Format(dateConsulting, "yyyy-MM-dd")}#"
        End If
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse($"ConsultingRoomCenterAttentionCode = '{centerAttentionCode}' AND AppointmentCenterAttentionCode = '{centerAttentionCode}' {filterDate} ")
        Dim _classEntity = session.GetClassInfo(GetType(ViewConsultinRoomgByProfessionalXpo))
        Dim _serverMode = New XPInstantFeedbackSource(_classEntity, Nothing, criteria)
        Return _serverMode
    End Function

    ''' <summary>
    ''' metodo que lista todos los tipos de ingreso
    ''' </summary>
    ''' <returns></returns>
    Public Function GetAdmissiontypes() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of AdmissionTypeXpo)()
        Dim _classEntity = session.GetClassInfo(GetType(AdmissionTypeXpo))
        Dim _serverMode = New XPInstantFeedbackSource(_classEntity, Nothing, Nothing)
        Return _serverMode
    End Function
#End Region

    ''' <summary>
    ''' metodo que lista todos las Vías Ingreso Servicios de Salud
    ''' </summary>
    ''' <returns></returns>
    Public Function GetEntryRoutesHealthServices() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of EntryRoutesHealthServicesXpo)()
        Dim _classEntity = session.GetClassInfo(GetType(EntryRoutesHealthServicesXpo))
        Dim _serverMode = New XPInstantFeedbackSource(_classEntity, Nothing, Nothing)
        Return _serverMode
    End Function

    ''' <summary>
    ''' metodo que lista las Finalidades tecnologías de la salud activas
    ''' </summary>
    ''' <returns></returns>
    Public Function GetHealthPurposes() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of HealthPurposesXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status = 1")
        Dim _classEntity = session.GetClassInfo(GetType(HealthPurposesXpo))
        Dim _serverMode = New XPInstantFeedbackSource(_classEntity, Nothing, criteria)
        Return _serverMode
    End Function

    ''' <summary>
    ''' metodo que lista todos las Modalidades de Atención
    ''' </summary>
    ''' <returns></returns>
    Public Function GetAdmissionModalities() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of AdmissionModalitiesXpo)()
        Dim _classEntity = session.GetClassInfo(GetType(AdmissionModalitiesXpo))
        Dim _serverMode = New XPInstantFeedbackSource(_classEntity, Nothing, Nothing)
        Return _serverMode
    End Function


#Region "AGDISPONSALA"
    Function GetCodProfesional(IDSALA As Integer, RoomDate As DateTime) As AGDISPONSALAXpo
        Dim session As New IndigoXPOSession(Of AGDISPONSALAXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(String.Format(" IDSALA = {0} AND FECHORAIN >= #{1}# AND FECHORAFI <= #{1}# ", IDSALA, Format(RoomDate, "yyyy-MM-dd HH:mm:ss")))

        Dim collect As XPCollection(Of AGDISPONSALAXpo) = New XPCollection(Of AGDISPONSALAXpo)(session, criteria)
        Return collect.FirstOrDefault()
    End Function
#End Region

#Region "ListUF"
    ''' <summary>
    ''' Lista de centros de atención
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListUF() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of INUNIFUNC)()

        Dim _classEntity = session.GetClassInfo(GetType(INUNIFUNC))
        Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "UFUCODIGO;UFUDESCRI;CodeName;UFUTIPUNI", Nothing)
        Return _serverMode
    End Function
#End Region

#Region "ListMinWage"
    ''' <summary>
    ''' Lista de centros de atención
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListMinWage() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of INSALARIM)()

        Dim _classEntity = session.GetClassInfo(GetType(INSALARIM))
        Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "ISALCODIG;ISALNOMBR;ISALVALOR", Nothing)
        Return _serverMode
    End Function
#End Region

#Region "ListBeds"
    ''' <summary>
    ''' Lista de centros de atención
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListBeds(center As String, uf As String) As XPInstantFeedbackSource
        Using session As New Session(XpoDefault.DataLayer)

            'Dim criteria As CriteriaOperator = CriteriaOperator.Parse("CODCENATE='" & center & "' AND ESTADCAMA = 1")
            Dim criteria As CriteriaOperator = CriteriaOperator.Parse("CODCENATE='" & center & "' AND UFUCODIGO.UFUCODIGO= '" & uf & "' AND ESTADCAMA = 1")
            Dim _classEntity = session.GetClassInfo(GetType(CHCAMASHO))
            Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "CodeName;CODICAMAS;NUMCAMHOS;DESCCAMAS", criteria)
            Return _serverMode
        End Using
    End Function

    ''' <summary>
    ''' Lista de centros de atención
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAllBeds() As XPInstantFeedbackSource
        Using session As New Session(XpoDefault.DataLayer)

            'Dim criteria As CriteriaOperator = CriteriaOperator.Parse("CODCENATE='" & center & "' AND ESTADCAMA = 1")
            Dim criteria As CriteriaOperator = CriteriaOperator.Parse("ESTADCAMA = 1")
            Dim _classEntity = session.GetClassInfo(GetType(CHCAMASHO))
            Dim _serverMode = New XPInstantFeedbackSource(_classEntity, Nothing, criteria)
            Return _serverMode
        End Using
    End Function
#End Region

#Region "ListHCMOANULB"
    ''' <summary>
    ''' funcion para listar las causas generales, para el motivo de nuevo enrutamiento en farmacia
    ''' </summary>
    ''' <returns></returns>
    Public Function ListHCMOANULBXPInstant(Optional Type As Integer? = Nothing) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of HCMOANULBXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse($"TIPSERIPS={If(Type Is Nothing, 24, Type)} and ESTADO =1 ")
        Dim _classEntity = session.GetClassInfo(GetType(HCMOANULBXpo))
        Dim _serverMode = New XPInstantFeedbackSource(_classEntity, Nothing, criteria)
        Return _serverMode
    End Function
#End Region

    ''' <summary>
    ''' Lista los ingresos con estado abierto, parcialmente, facturado y cerrados
    ''' </summary>
    ''' <returns></returns>
    Public Function GetViewAdmissionOpenPartialInvoicedClosed() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ViewAdmissionOpenPartialInvoicedClosedXpo)()

        Dim _classEntity = session.GetClassInfo(GetType(ViewAdmissionOpenPartialInvoicedClosedXpo))
        Dim _serverMode = New XPInstantFeedbackSource(_classEntity, Nothing, Nothing)
        Return _serverMode
    End Function

    ''' <summary>
    ''' Lista los ingresos para ordenes de servicio
    ''' </summary>
    ''' <returns></returns>
    Public Function GetViewAdmissionServiceOrder() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ViewAdmissionServiceOrder)()

        Dim _classEntity = session.GetClassInfo(GetType(ViewAdmissionServiceOrder))
        Dim _serverMode = New XPInstantFeedbackSource(_classEntity, Nothing, Nothing)
        Return _serverMode
    End Function

    ''' <summary>
    ''' Lista los ingresos para ordenes de servicio
    ''' </summary>
    ''' <returns></returns>
    Public Function GetViewAdmissionServiceOrderByPatient(patientCode As String) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ViewAdmissionServiceOrder)()

        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("PatientCode='" & patientCode & "'")
        Dim _classEntity = session.GetClassInfo(GetType(ViewAdmissionServiceOrder))
        Dim _serverMode = New XPInstantFeedbackSource(_classEntity, Nothing, criteria)
        Return _serverMode
    End Function

    ''' <summary>
    ''' Lista los ingresos para ordenes de servicio con la propiedad IESTADOIN = ' ' o IESTADOIN = 'P'
    ''' </summary>
    ''' <returns></returns>
    Public Function GetViewAdmissionOpenAndPartial() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ViewAdmissionOpenAndPartial) '(XpoDefault.DataLayer)

        Dim _classEntity = session.GetClassInfo(GetType(ViewAdmissionOpenAndPartial))
        Dim _serverMode = New XPInstantFeedbackSource(_classEntity, Nothing, Nothing)
        Return _serverMode
    End Function

    ''' <summary>
    ''' Lista los ingresos para ordenes de servicio con la propiedad IESTADOIN = ' ' o IESTADOIN = 'P'
    ''' </summary>
    ''' <returns></returns>
    Public Function GetViewAdmissionOpenAndPartialServiceOrder() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ViewAdmissionOpenAndPartialServiceOrder) '(XpoDefault.DataLayer)

        Dim _classEntity = session.GetClassInfo(GetType(ViewAdmissionOpenAndPartialServiceOrder))
        Dim _serverMode = New XPInstantFeedbackSource(_classEntity, Nothing, Nothing)
        Return _serverMode
    End Function

    Public Function GetViewAdmissionOpenAndPartialCollection(admissionNumber As String, Optional PatientCode As String = Nothing) As XPCollection(Of ViewAdmissionOpenAndPartial)
        Dim session As New IndigoXPOSession(Of ViewAdmissionOpenAndPartial)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse($"{IIf(String.IsNullOrEmpty(PatientCode), $"AdmissionCode='{admissionNumber}'", $"PatientCode= '{PatientCode}'")}")
        Dim collect As XPCollection(Of ViewAdmissionOpenAndPartial) = New XPCollection(Of ViewAdmissionOpenAndPartial)(session, criteria)
        Return collect
        'End Using
    End Function

#End Region

#Region "ListPopulationGroup"
    ''' <summary>
    ''' Lista de grupos poblacionales
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListPopulationGroup() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ADPOBESPEXpo)()

        Dim _classEntity = session.GetClassInfo(GetType(ADPOBESPEXpo))
        Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "ID;CODIGO;DESCRIPCION;ESTADO;CodigoDescripcion", Nothing)
        Return _serverMode
    End Function
#End Region



#Region "ListImagingGroupActive"
    ''' <summary>
    ''' Lista de grupos de imagenologia activos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListImagingGroupActive() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of RISGRIMAGEXpo)()

        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("[ESTADO]=1")
        Dim _classEntity = session.GetClassInfo(GetType(RISGRIMAGEXpo))
        Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "ID;CODIGO;NOMBRE;ESTADO;CodigoNombre;EstadoName", criteria)
        Return _serverMode
    End Function
#End Region

#Region "ListHemocomponentes"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Public Function ListHemocomponent() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of HCCOMSANXpo)()
        Dim _classEntity = session.GetClassInfo(GetType(HCCOMSANXpo))
        Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "ID;CODCOMSAM;DESCOMSAM;VALIDAHEMOCLA;CodigoDescripcion", Nothing)
        Return _serverMode
    End Function
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="hemocomponentId"></param>
    ''' <returns></returns>
    Public Function GetHemocomponentById(hemocomponentId As Integer) As HCCOMSANXpo
        Dim session As New IndigoXPOSession(Of HCCOMSANXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(String.Format("ID = {0}", hemocomponentId))
        Dim collect As XPCollection(Of HCCOMSANXpo) = New XPCollection(Of HCCOMSANXpo)(session, criteria)
        Return collect.FirstOrDefault
    End Function

    ''' <summary>
    ''' Funcion para traer las citas de hemocomponentes
    ''' </summary>
    ''' <param name="IPCODPACI"></param>
    ''' <returns></returns>
    Public Function GetMedicalAppointmentHemo(IPCODPACI As String) As List(Of AGASICITAXpo)
        Dim session As New IndigoXPOSession(Of AGASICITAXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(String.Format("TIPSOLICITU = 3 AND TIPTRATAMIENTO = 5 AND (GENGENERATESO IS NULL OR GENGENERATESO <> 1) AND IPCODPACI ='{0}'", IPCODPACI))
        Dim collect As XPCollection(Of AGASICITAXpo) = New XPCollection(Of AGASICITAXpo)(session, criteria)
        Return collect.ToList()
    End Function

    ''' <summary>
    ''' Funcion para traer las lista de hemocomponentes de ordenes extramural
    ''' </summary>
    ''' <param name="IPCODPACI"></param>
    ''' <returns></returns>
    Public Function GetOrdersExtramuralHemo(IPCODPACI As String) As List(Of HCORHEMCOXpo)
        Dim session As New IndigoXPOSession(Of HCORHEMCOXpo)()
        ' Obtener la fecha de tres meses atrás desde hoy
        Dim DateThreeMonthsAfter As DateTime = DateTime.Today.AddMonths(-3)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(String.Format("MANEXTPRO = 1 AND idAGASICITA IS NULL  AND IPCODPACI ='{0}' AND FECORDMED >= #{1:M/d/yyyy}#", IPCODPACI, DateThreeMonthsAfter))
        Dim collect As XPCollection(Of HCORHEMCOXpo) = New XPCollection(Of HCORHEMCOXpo)(session, criteria)
        Return collect.ToList()
    End Function

    ''' <summary>
    ''' Obtiene el tipo de actividad de agendamiento definido desde el EHR
    ''' </summary>
    ''' <param name="ScheduleActivityCode"></param>
    ''' <returns></returns>
    Public Function GetScheduleActivityType(ScheduleActivityCode As String) As Integer
        Dim session As New IndigoXPOSession(Of AGACTIMED)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse($"CODACTMED='{ScheduleActivityCode}'")
        Dim result As AGACTIMED = session.FindObject(Of AGACTIMED)(criteria)
        Return CInt(result.ACTIVICON)
    End Function


    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="hemocomponentId"></param>
    ''' <returns></returns>
    Public Function ListReserveHemocomponentDetail(hemocomponentId As Integer) As XPCollection(Of HCCOMSANDXpo)
        Dim session As New IndigoXPOSession(Of HCCOMSANDXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(String.Format("COMSAMID = {0} AND TIPSERIPS = 1", hemocomponentId))
        'Dim _classEntity = session.GetClassInfo(GetType(HCCOMSANDXpo))
        'Dim _serverMode = New XPInstantFeedbackSource(_classEntity, "ID;COMSAMID;CODSERIPS;TIPSERIPS;TIPOCARGUE", Nothing)
        'Return _serverMode
        Dim collect As XPCollection(Of HCCOMSANDXpo) = New XPCollection(Of HCCOMSANDXpo)(session, criteria)
        Return collect
    End Function
#End Region

#Region "ADPARAMET"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="careCenterCode"></param>
    ''' <returns></returns>
    Public Function GetADPARAMETByCareCenterCode(careCenterCode As String) As ADPARAMETXpo
        Dim session As New IndigoXPOSession(Of ADPARAMETXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(String.Format("CODCENATE = '{0}'", careCenterCode))
        Dim collect As XPCollection(Of ADPARAMETXpo) = New XPCollection(Of ADPARAMETXpo)(session, criteria)
        Return collect.FirstOrDefault
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

#Region "HCORHEMBOL"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="_HCORHEMBOLID"></param>
    ''' <returns></returns>
    Public Function GetHCORHEMBOLByID(_HCORHEMBOLID As Integer) As HCORHEMBOLXpo
        Dim session As New IndigoXPOSession(Of HCORHEMBOLXpo)()
        Return session.GetObjectByKey(Of HCORHEMBOLXpo)(_HCORHEMBOLID)
    End Function
#End Region

#Region "HealthCareProfessionalXpo"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="_CODPROSAL"></param>
    ''' <returns></returns>
    Public Function GetHCORHEMBOLByCode(_CODPROSAL As String) As HealthCareProfessionalXpo
        Dim session As New IndigoXPOSession(Of HealthCareProfessionalXpo)()
        'Dim criteria As CriteriaOperator = CriteriaOperator.Parse(String.Format("CODPROSAL = '{0}'", _CODPROSAL))
        'Dim collect As XPCollection(Of HealthCareProfessionalXpo) = New XPCollection(Of HealthCareProfessionalXpo)(session, criteria)
        'Return collect.FirstOrDefault
        Return session.GetObjectByKey(Of HealthCareProfessionalXpo)(_CODPROSAL)
    End Function
#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

#End Region

End Class