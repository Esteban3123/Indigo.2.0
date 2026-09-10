'***********************************************************************
' Assembly         : Presentacion.Billing.MVP
' Author           : Carlos Ernesto Cordoba
' Created          : 28-10-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities
Imports Presentation.CloudAgent
Imports Domain.Base.Entities
Imports Presentation.Base
Imports System.ServiceModel
Imports Infrastructure.Data.Xpo
Imports DevExpress.Data.PLinq
Imports DevExpress.Data.Linq
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo.CrystalRepository
Imports System.Dynamic
Imports Domain.Crystal.Entities

#End Region

Public Class MControlOutpatientServices
    Implements IDisposable


#Region "Fields"

    ''' <summary>
    ''' Referencia a los valores de session
    ''' </summary>
    Private _indigoSessionValues As SessionValues

    ''' <summary>
    ''' Id del frontal
    ''' </summary>
    Private _tagForm As String

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    Public Sub New(ByVal tag As String)
        Me._tagForm = tag
        Me._indigoSessionValues = SessionValues.Instance
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Gets the homologations cups.
    ''' </summary>
    ''' <param name="careGroupId">The care group identifier.</param>
    ''' <returns></returns>
    Public Async Function GetHomologationsCups(parameters As Object, careGroupId As Integer) As Task(Of ActionResult(Of List(Of List(Of CupsHomologation))))
        Dim args As String = Utils.SerializeObjectToJson(parameters)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.GetHomologationsCupsAsync(args, careGroupId)
    End Function

    Public Async Function GetHCUNITHISByUFUCODIGOWithFACMECONINS(ufucodigo As String) As Task(Of HCUNITHIS)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCrystal.GetHCUNITHISByUFUCODIGOWithFACMECONINSAsync(ufucodigo)
    End Function


    ''' <summary>
    ''' lista los centros de atencion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListCentersHIS() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.HisContainer).CrystalService.ListCenters()
    End Function

    Public Function GetAdmissionObjectByNumIngres(admissionnumber As String) As XPCollection(Of ViewAdmissionsToLiquidation)
        Return XpoServiceEx.Instance(_indigoSessionValues.HisContainer).CrystalService.GetAdmissionObjectByNumIngres(admissionnumber)
    End Function

    Public Function GetAdmissionObjectAllByNumIngres(admissionnumber As String) As ViewLiquidationGetAdmissionAll
        Return XpoServiceEx.Instance(_indigoSessionValues.HisContainer).CrystalService.Liquidation_GetAdmission(admissionnumber)
    End Function

    Function GetAllProductsByClasses(ParamArray elements() As String) As XPInstantFeedbackSource
        'Dim strElements As New List(Of String)
        Dim strElements = (From e In elements Select e).ToList()
        'For Each element As Object In elements
        '    If element.GetType Is GetType(System.String) Then
        '        strElements.Add(element)
        '    ElseIf element.GetType Is GetType(System.Collections.Generic.List(Of String)) Then
        '        For Each s As String In element
        '            strElements.Add(s)
        '        Next
        '    End If
        'Next
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).InventoryService.ListInventoryProductByProductTypeClasses(strElements)
    End Function

    ''' <summary>
    ''' Listars the citas medicas.
    ''' </summary>
    ''' <param name="patientCode">The patient code.</param>
    ''' <param name="atentionCenterCode">The atention center code.</param>
    ''' <returns></returns>
    Public Async Function ListarCitasMedicas(patientCode As String, atentionCenterCode As String) As Task(Of List(Of SP_AD_ListarCitasMedicasNativo_Result))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCrystal.ListarCitasMedicasAsync(patientCode, atentionCenterCode)
    End Function

    Public Async Function GenerateServiceOrderProductsAsync(args As Object) As Task(Of ActionResult(Of String))
        Dim params As String = Utils.SerializeObjectToJson(args)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.GenerateServiceOrderProductsAsync(params, _indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' lista las unidades funcionales por tipo de unidad (3-Apoyo Dx, 20-Laboratorio, 21-Cardiologia No Invasiva, 24-Consulta Externa - Gineco-Obstetricia)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListFunctionalUnit() As XPInstantFeedbackSource
        Dim unitTypes = "'3','20','21','24','15','22'"
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).PayrollService.ListFunctionalUnitByUnitTypeAndUserAuthorized(unitTypes, _indigoSessionValues.UserIndigo)
    End Function

    Public Function ListFunctionalUnitCareCenter(careCenterCode As String) As XPInstantFeedbackSource
        Dim unitTypes = "'3','20','21','24','15','22','19','1','23','13'"
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).PayrollService.ListFunctionalUnitByUnitTypeCareCenterUserAuthorized(careCenterCode.Trim(), unitTypes, _indigoSessionValues.UserIndigo)
    End Function

    Public Function ListAllFunctionalUnitCareCenter(careCenterCode As String) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).PayrollService.ListAllFunctionalUnitByUnitTypeCareCenterUserAuthorized(careCenterCode.Trim(), _indigoSessionValues.UserIndigo)
    End Function

    Public Function ListContractXpo() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).ContractService.ListContractByStatus(1)
    End Function
    ''' <summary>
    ''' lista los municipios
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListTown() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.HisContainer).CrystalService.ListTown()
    End Function
    ''' <summary>
    ''' lista las ips por estado
    ''' </summary>
    ''' <param name="status"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListIPSCrystalByStatus(status As Boolean) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.HisContainer).CrystalService.ListIPSByStatus(status)
    End Function
    ''' <summary>
    ''' lista los CUPS por estado
    ''' </summary>
    ''' <param name="status"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListCUPSCrystalByStatus(status As Boolean) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.HisContainer).CrystalService.ListCUPSByStatus(status)
    End Function

    Public Function ListCUPSCrystalByStatusType(status As Boolean, type As Integer) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.HisContainer).CrystalService.ListCUPSCrystalByStatusType(status, type)
    End Function

    Public Async Function GenerateDocuments(controlOutPatientServices As ControlOutPatientServices) As Task(Of ActionResult(Of String))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.GenerateDocumentsAsync(controlOutPatientServices, SessionValues.Instance.IndigoCompanyNit, _indigoSessionValues.AuditMessageWcf)
    End Function

    Public Async Function GetProductsByActmedicaAndCups(listActmedicaCups As List(Of ACTMEDCUPS), IPFECNACI As Date) As Task(Of ActionResult(Of List(Of ProductCita)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.GetProductsByActmedicaAndCupsAsync(listActmedicaCups, IPFECNACI)
    End Function

    Public Function GetAuthorizationParameterByTUF(CareCenterCode As String, FunctionalUnit As String) As Boolean
        Dim Result As ActionResult = IndigoConecta.Instancia.CurrentCloud.IndigoBilling.GetAuthorizationParameterByTUF(CareCenterCode, FunctionalUnit, _indigoSessionValues.TransactionalContainer)
        Return Result.StateResult
    End Function

    ''' <summary>
    ''' funcion que retorna el datasource de actividades de agendamiento
    ''' </summary>
    ''' <param name="especialityCode"></param>
    ''' <returns></returns>
    Public Function GetScheduleActivityOther(especialityCode As String) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.HisContainer).CrystalService.GetScheduleActivityOther(especialityCode)
    End Function

    ''' <summary>
    ''' funcion  que retorna el datasource de los consultorios 
    ''' </summary>
    ''' <returns></returns>
    Public Function GetConsultinRoom(centerAttentionCode As String) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.HisContainer).CrystalService.GetConsultinRoom(centerAttentionCode)
    End Function

    ''' <summary>
    ''' funcion que obtiene el cups por codigo de la vista ViewListCupsByAGACTIMEDXpo 
    ''' </summary>
    ''' <returns></returns>
    Public Function GetAdmissiontypes() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.HisContainer).CrystalService.GetAdmissiontypes()
    End Function

    ''' <summary>
    ''' funcion que obtiene las Vías Ingreso Servicios de Salud
    ''' </summary>
    ''' <returns></returns>
    Public Function GetEntryRoutesHealthServices() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.HisContainer).CrystalService.GetEntryRoutesHealthServices()
    End Function

    ''' <summary>
    ''' funcion que obtiene las Finalidades tecnologías de la salud
    ''' </summary>
    ''' <returns></returns>
    Public Function GetHealthPurposes() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.HisContainer).CrystalService.GetHealthPurposes()
    End Function

    ''' <summary>
    ''' funcion que obtiene la Modalidades de Atención
    ''' </summary>
    ''' <returns></returns>
    Public Function GetAdmissionModalities() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.HisContainer).CrystalService.GetAdmissionModalities()
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: desechar estado administrado (objetos administrados).
            End If

            ' TODO: liberar recursos no administrados (objetos no administrados) e invalidar Finalize() below.
            ' TODO: Establecer campos grandes como Null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: invalidar Finalize() sólo si la instrucción Dispose(ByVal disposing As Boolean) anterior tiene código para liberar recursos no administrados.
    'Protected Overrides Sub Finalize()
    '    ' No cambie este código. Ponga el código de limpieza en la instrucción Dispose(ByVal disposing As Boolean) anterior.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' Visual Basic agregó este código para implementar correctamente el patrón descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' No cambie este código. Coloque el código de limpieza en Dispose(disposing As Boolean).
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
