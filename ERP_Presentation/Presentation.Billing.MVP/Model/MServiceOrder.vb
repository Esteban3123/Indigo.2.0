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
Imports Infrastructure.Data.Xpo.BillingRepository
Imports DevExpress.Data.PLinq
Imports DevExpress.Data.Linq
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo.CrystalRepository
Imports System.Dynamic
Imports Domain.Crystal.Entities
Imports Infrastructure.Data.Xpo.AccountingRepository

#End Region

Public Class MServiceOrder
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
    ''' Metodo que valida si la rias se puede agregar
    ''' </summary>
    ''' <param name="ListParameters"></param>
    ''' <returns></returns>
    Public Function SP_RIAS_ValidacionCUPSRIAS(ListParameters As List(Of Tuple(Of String, Integer, String, Integer, DateTime))) As Task(Of ActionResult(Of List(Of SP_RIAS_ValidacionCUPSRIAS_Result)))
        Return IndigoConecta.Instancia.CurrentCloud.IndigoCrystal.SP_RIAS_ValidacionCUPSRIASAsync(ListParameters)
    End Function

    ''' <summary>
    ''' Metodo que valida si la rias se puede agregar
    ''' </summary>
    ''' <param name="ListParameters"></param>
    ''' <returns></returns>
    Public Function SP_RIAS_ValidacionCUPSRIASDontAsync(ListParameters As List(Of Tuple(Of String, Integer, String, Integer, DateTime))) As ActionResult(Of List(Of SP_RIAS_ValidacionCUPSRIAS_Result))
        Return IndigoConecta.Instancia.CurrentCloud.IndigoCrystal.SP_RIAS_ValidacionCUPSRIAS(ListParameters)
    End Function

    ''' <summary>
    ''' obtiene los detalles de la distribucion por el id del detalle de la orden
    ''' </summary>
    ''' <param name="ServiceOrderDetailId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetInvoiceDetailByServiceOrderDetailId(ServiceOrderDetailId As Integer) As InvoiceDetail
        Return IndigoConecta.Instancia.CurrentCloud.IndigoBilling.GetInvoiceDetailByServiceOrderDetailId(ServiceOrderDetailId)
    End Function
    ''' <summary>
    ''' obtiene los detalles de la distribucion por el id del detalle de la orden
    ''' </summary>
    ''' <param name="ServiceOrderDetailId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetServiceOrderDetailDistributionByServideOrderDetailId(ServiceOrderDetailId As Integer) As List(Of ServiceOrderDetailDistribution)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoBilling.GetServiceOrderDetailDistributionByServideOrderDetailId(ServiceOrderDetailId)
    End Function
    ''' <summary>
    ''' obtiene un folio por id del detalle de la orden de servicio
    ''' </summary>
    ''' <param name="serviceOrderDetailId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetRevenueControlDetailByServiceOrderDetailId(serviceOrderDetailId As Integer) As RevenueControlDetail
        Return IndigoConecta.Instancia.CurrentCloud.IndigoBilling.GetRevenueControlDetailByServiceOrderDetailId(serviceOrderDetailId)
    End Function
    ''' <summary>
    ''' metodo para obtener las tarifas para los eventos en la orden de servicio
    ''' </summary>
    ''' <param name="RateManualId"></param>
    ''' <param name="InterventionType"></param>
    ''' <returns></returns>
    Public Function GetSurgeriesPercetageManualByRateManualIdInterventionType(RateManualId As Integer, InterventionType As Integer) As Domain.Entities.SurgeriesPercentageManual
        Return IndigoConecta.Instancia.CurrentCloud.IndigoContract.GetSurgeriesPercentageManualByRateManualIdInterventionType(RateManualId, InterventionType)
    End Function
    Public Async Function GetSurgeriesPercetageManualByRateManualIdInterventionTypeAsync(RateManualId As Integer, InterventionType As Integer) As Task(Of Domain.Entities.SurgeriesPercentageManual)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoContract.GetSurgeriesPercentageManualByRateManualIdInterventionTypeAsync(RateManualId, InterventionType)
    End Function
    ''' <summary>
    ''' Obtiene un ingresos plano por su numero
    ''' </summary>
    Public Function GetAdmissionByServiceOrder(ByVal admissionNumber As String) As Object
        Dim res = IndigoConecta.Instancia.CurrentCloud.IndigoBilling.GetAdmissionByServiceOrder(admissionNumber)
        If res IsNot Nothing AndAlso Not res.Trim().Equals(String.Empty) Then
            Return Utils.DeserializeJsonToObject(res)
        Else
            Return Nothing
        End If
    End Function

    Public Function GetAdmissionByServiceOrderCollection(admissionNumber As String, Optional PatientCode As String = Nothing) As XPCollection(Of ViewAdmissionOpenAndPartial)
        Return XpoServiceEx.Instance(_indigoSessionValues.HisContainer).CrystalService.GetViewAdmissionOpenAndPartialCollection(admissionNumber, PatientCode)
    End Function
    ''' <summary>
    ''' lista los detalles quirurgicos del detalle de la orden de servicio
    ''' </summary>
    ''' <param name="ServiceOrderDetailId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListSurgicalDetailByIdServiceOrderDetail(ServiceOrderDetailId As Integer) As List(Of ServiceOrderDetailSurgical)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoBilling.ListServiceOrderDetailSurgicalByIdDerviceOrderDetail(ServiceOrderDetailId)
    End Function
    ''' <summary>
    ''' obtiene procedimiento quirurgicos del servcio IPS
    ''' </summary>
    ''' <param name="idIPSService"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSurgicalProcedureServiceByIPSServiceId(idIPSService As Integer) As List(Of SurgicalProcedureService)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoContract.GetSurgicalProcedureServiceByIPSServiceId(idIPSService)
    End Function
    ''' <summary>
    ''' metodo el valor del servicio
    ''' </summary>
    ''' <param name="listCupsHomologation"></param>
    ''' <param name="CareGroupId"></param>
    ''' <param name="FunctionalUnitId"></param>
    ''' <param name="Specialty"></param>
    ''' <param name="ServiceDate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetServiceValue(AdmissionNumber As String, ByVal CenterAttentionCode As String, listCupsHomologation As List(Of CupsHomologation), CareGroupId As Integer, FunctionalUnitId As Integer, Specialty As String, ServiceDate As Date, PatientGenus As Integer, PatientDateBirth As Date, InvoicedQuantity As Integer, ProfessionalHealthCode As String, ProfessionalHealthThirdPartyId As Integer, Optional RiasId As Integer? = Nothing, Optional ContractDescriptionId As Integer? = Nothing) As ActionResult(Of List(Of ServiceOrderDetail))
        Return IndigoConecta.Instancia.CurrentCloud.IndigoBilling.GetServiceValue(AdmissionNumber, CenterAttentionCode, listCupsHomologation, CareGroupId, FunctionalUnitId, Specialty, ServiceDate, PatientGenus, PatientDateBirth, InvoicedQuantity, ProfessionalHealthCode, ProfessionalHealthThirdPartyId, RiasId, ContractDescriptionId)
    End Function

    ''' <summary>
    ''' Metodo que calcula el valor del servicio que se utiliza cuando el usuario cambie el valor en el control de rias en ordenes de servicio
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetServiceValueByRIAS(AdmissionNumber As String, ByVal CenterAttentionCode As String, CupsEntityId As Integer, IPSServiceId As Integer, CareGroupId As Integer, FunctionalUnitId As Integer, Specialty As String, ServiceDate As Date, PatientGenus As Integer, PatientDateBirth As Date, InvoicedQuantity As Integer, ProfessionalHealthCode As String, ProfessionalHealthThirdPartyId As Integer, Optional RiasId As Integer? = Nothing) As ActionResult(Of ServiceOrderDetail)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoBilling.GetServiceValueByRIAS(AdmissionNumber, CenterAttentionCode, CupsEntityId, IPSServiceId, CareGroupId, FunctionalUnitId, Specialty, ServiceDate, PatientGenus, PatientDateBirth, InvoicedQuantity, ProfessionalHealthCode, ProfessionalHealthThirdPartyId, RiasId)
    End Function

    Public Function GetServiceValueByManual(ServiceDetail As ServiceOrderDetail, IPSServiceId As Integer, CareGroupId As Integer, FunctionalUnitId As Integer, Specialty As String, ServiceDate As Date, PatientGenus As Integer, PatientDateBirth As Date, InvoicedQuantity As Integer, ProfessionalHealthCode As String, ProfessionalHealthThirdPartyId As Integer) As ActionResult(Of ServiceOrderDetail)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoBilling.GetServiceValueByManual(ServiceDetail, IPSServiceId, CareGroupId, FunctionalUnitId, Specialty, ServiceDate, PatientGenus, PatientDateBirth, InvoicedQuantity, ProfessionalHealthCode, ProfessionalHealthThirdPartyId)
    End Function

    ''' <summary>
    ''' metodo para obtener el valor con recargo
    ''' </summary>
    ''' <param name="serviceOrderDetail"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetServiceValueSurcharge(serviceOrderDetail As ServiceOrderDetail) As ServiceOrderDetail
        Return IndigoConecta.Instancia.CurrentCloud.IndigoBilling.GetServiceValueSurcharge(serviceOrderDetail)
    End Function
    ''' <summary>
    ''' metodo para recalcular los eventos cuando se cambie el item que es primer evento
    ''' </summary>
    ''' <param name="serviceOrderDetail"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function RecalculateSurgicalEvents(serviceOrderDetail As ServiceOrderDetail) As ServiceOrderDetail
        Return IndigoConecta.Instancia.CurrentCloud.IndigoBilling.RecalculateSurgicalEvents(serviceOrderDetail)
    End Function

    Public Async Function RecalculateSurgicalEventsAsync(serviceOrderDetail As ServiceOrderDetail) As Task(Of ServiceOrderDetail)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.RecalculateSurgicalEventsAsync(serviceOrderDetail)
    End Function

    ''' <summary>
    ''' obtener el valor de los detalles del ips quirurgico cuando el usuario cambia los valores por defecto en el formulario
    ''' </summary>
    ''' <param name="serviceOrderDetail"></param>
    ''' <param name="listSurgicalProcedureServiceDefault"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetServiceValueBySurgicalProcedureService(serviceOrderDetail As Domain.Entities.ServiceOrderDetail, listSurgicalProcedureServiceDefault As List(Of Domain.Entities.SurgicalProcedureService)) As ActionResult(Of Domain.Entities.ServiceOrderDetail)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoBilling.GetServiceValueBySurgicalProcedureService(serviceOrderDetail, listSurgicalProcedureServiceDefault)
    End Function
    ''' <summary>
    ''' metodo para obtener un detalla del manual de tarifas quirurgico
    ''' </summary>
    Public Async Function GetSurgicalDetailServiceOrder(rateManualId As Integer, ipsService As Integer, surgicalGrouopId As Integer?, UVRNumber As Integer?, serviceManual As Integer) As Task(Of RateManualDetailSurgical)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoContract.GetSurgicalDetailServiceOrderAsync(rateManualId, ipsService, surgicalGrouopId, UVRNumber, serviceManual)
    End Function
    ''' <summary>
    ''' lista los detalles de la orden de servico que se podran incluir en otro
    ''' </summary>
    ''' <param name="adminssionNumber"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListServiceOrderDetailsByAdmissionNumber(adminssionNumber As String) As List(Of ServiceOrderDetail)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoBilling.ListServicesOrderDetailByAdminssionNumber(adminssionNumber)
    End Function

    Public Async Function ListServiceOrderDetailsByAdmissionNumberAsync(adminssionNumber As String) As Task(Of List(Of ServiceOrderDetail))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.ListServicesOrderDetailByAdminssionNumberAsync(adminssionNumber)
    End Function
    ''' <summary>
    ''' metodo para obtener la homologacion del cups
    ''' </summary>
    ''' <param name="CareGroupId"></param>
    ''' <param name="CupsId"></param>
    ''' <param name="FunctionalUnitId"></param>
    ''' <param name="Specialty"></param>
    ''' <param name="ServiceDate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetHomologationCups(CareGroupId As Integer, CupsId As Integer, FunctionalUnitId As Integer, Specialty As String, ServiceDate As Date, Optional IPSServiceId As Integer = 0, Optional ManualType As Integer = 0, Optional RiasId As Integer? = Nothing, Optional ContractDescriptionId As Integer? = Nothing) As ActionResult(Of List(Of CupsHomologation))
        Return IndigoConecta.Instancia.CurrentCloud.IndigoContract.GetHomologationCups(CareGroupId, CupsId, FunctionalUnitId, Specialty, ServiceDate, IPSServiceId, ManualType, RiasId, ContractDescriptionId)
    End Function

    Public Function GetHomologationCupsByListCUPS(ParamArray parameters() As Object) As Domain.Base.Entities.ActionResult(Of List(Of Domain.Entities.CupsHomologation))
        Return IndigoConecta.Instancia.CurrentCloud.IndigoContract.GetHomologationCupsByListCUPS(parameters(0), parameters(1), parameters(2), parameters(3), parameters(4), parameters(5), parameters(6))
    End Function

    ''' <summary>
    ''' lista las homologaciones de los servicios ips
    ''' </summary>
    ''' <param name="IpsServiceId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListCupsHomologationByIpsServiceId(IpsServiceId As Integer) As List(Of CupsHomologation)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoContract.ListCupsHomologationByIpsServiceId(IpsServiceId)
    End Function

    ''' <summary>
    ''' obtiene una orden por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetServiceOrderAsync(code As String) As Task(Of ServiceOrder)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.GetServiceOrderAsync(code, Me._indigoSessionValues.AuditMessageWcf)
    End Function
    ''' <summary>
    ''' obtuiene una orden por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetServiceOrderById(id As Integer) As ServiceOrder
        Return IndigoConecta.Instancia.CurrentCloud.IndigoBilling.GetServiceOrderById(id)
    End Function

    ''' <summary>
    ''' odtiene el detalla de la orden
    ''' </summary>
    ''' <param name="serviceOrderId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetServiceOrderDetailByOrderServiceId(serviceOrderId As Integer) As List(Of ServiceOrderDetail)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoBilling.GetServiceOrderDetailByServiceOrderId(serviceOrderId)
    End Function

    Public Async Function GetServiceOrderDetailByOrderServiceIdAsync(serviceOrderId As Integer) As Task(Of List(Of ServiceOrderDetail))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.GetServiceOrderDetailByServiceOrderIdAsync(serviceOrderId)
    End Function

    Public Async Function GetServiceOrderDetailById(Id As Integer) As Task(Of ServiceOrderDetail)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.GetServiceOrderDetailByIdAsync(Id)
    End Function

    ''' <summary>
    ''' Guarda o Actualiza una orden de sercivio
    ''' </summary>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">entidad Vacio</exception>
    Async Function SaveServiceOrder(ServiceOrder As ServiceOrder, ByVal idSequense As Int64) As Task(Of Domain.Base.Entities.ActionResult(Of Domain.Entities.ServiceOrder))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.SaveServiceOrderAsync(ServiceOrder, idSequense, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Guarda el detalle quirurgico
    ''' </summary>
    ''' <param name="ServiceOrderDetailSurgical"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveServiceOrderDetailSurgical(ServiceOrderDetailSurgical As ServiceOrderDetailSurgical) As Task(Of Domain.Base.Entities.ActionResult(Of Domain.Entities.ServiceOrderDetailSurgical))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.SaveServiceOrderDetailSurgicalAsync(ServiceOrderDetailSurgical, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Elimina un detalle quirurgico
    ''' </summary>
    ''' <param name="ServiceOrderDetailSurgicalId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeleteServiceOrderDetailSurgical(ByVal ServiceOrderDetailSurgicalId As Integer) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.DeleteServiceOrderDetailSurgicalAsync(ServiceOrderDetailSurgicalId, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Actualiza los campos de médico y tercero del detalle de la orden de servicio
    ''' </summary>
    ''' <param name="ServiceOrderDetailId"></param>
    ''' <param name="HealthProfessionalCode"></param>
    ''' <param name="ThirdPartyId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function UpdateFieldsServiceOrderDetail(ServiceOrderDetailId As Integer, HealthProfessionalCode As String, ThirdPartyId As Integer) As Task(Of Domain.Base.Entities.ActionResult(Of ServiceOrderDetail))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.UpdateFieldsServiceOrderDetailAsync(ServiceOrderDetailId, HealthProfessionalCode, ThirdPartyId)
    End Function

    ''' <summary>
    ''' Actualiza los campos de médico y tercero del detalle quirurgico de la orden de servicio
    ''' </summary>
    ''' <param name="ServiceOrderDetailSurgicalId"></param>
    ''' <param name="HealthProfessionalCode"></param>
    ''' <param name="ThirdPartyId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function UpdateFieldsServiceOrderDetailSurgical(ServiceOrderDetailSurgicalId As Integer, HealthProfessionalCode As String, ThirdPartyId As Integer) As Task(Of ActionResult(Of ServiceOrderDetailSurgical))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.UpdateFieldsServiceOrderDetailSurgicalAsync(ServiceOrderDetailSurgicalId, HealthProfessionalCode, ThirdPartyId)
    End Function

    ''' <summary>
    ''' Actualiza los campos de médico y tercero del detalle de la orden de servicio
    ''' </summary>
    ''' <param name="ListTuple">Este listado contiene los id para poder consultar y actualizar</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function UpdateHealthProfessionalForMedicalFeesCausation(ListTuple As List(Of Tuple(Of Integer, Integer, String, Integer)), SelectionSurgical As Boolean, Company As String) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.UpdateHealthProfessionalForMedicalFeesCausationAsync(ListTuple, SelectionSurgical, Company)
    End Function

    Public Async Function UpdateServiceOrderDetailList(listServiceOrderDetail As List(Of ServiceOrderDetail)) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.UpdateServiceOrderDetailListAsync(listServiceOrderDetail)
    End Function

    ''' <summary>
    ''' Valida que la causacion no exista en ninguna liquidacion
    ''' </summary>
    ''' <param name="MedicalFeesCausationId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ValidateDeleteServiceOrderDetailSurgical(MedicalFeesCausationId As Integer) As Task(Of ActionResult(Of String))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.ValidateDeleteServiceOrderDetailSurgicalAsync(MedicalFeesCausationId)
    End Function

    ''' <summary>
    ''' lista los ingresos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetListAdmissions() As LinqInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.HisContainer).CrystalService.ListAdmissions()
    End Function

    ''' <summary>
    ''' lista los detalles de la orden
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListServiceOrderDetailByServiceOrderId(serviceOrderId As Integer) As XPCollection(Of ServiceOrderDetailXpo)
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).BillingService.ListServiceOrderDetailByServiceOrderId(serviceOrderId)
    End Function

    ''' <summary>
    ''' Lista el Grupo de atencion
    ''' </summary>
    ''' <param name="careGroupId"></param>
    ''' <returns></returns>
    Public Function CareGroupById(careGroupId As Integer) As CareGroupXpo
        Dim filtroConsulta As String = "Id =" & careGroupId
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).BillingService.GetCollection(Of CareGroupXpo)(Nothing, filtroConsulta).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Obtiene los cups que necesiten una cotización
    ''' </summary>
    ''' <returns></returns>
    Public Function GetCUPSWithQuoted(listServiceOrderDetail As List(Of ServiceOrderDetail)) As String
        Dim messageReturn As String = String.Empty
        Dim listProcedures As New List(Of Infrastructure.Data.Xpo.ContractRepository.ProcedureCupsXpo)

        For Each item In listServiceOrderDetail
            Dim filterCareGroup = "Id = " & item.CareGroupId
            Dim careGroup = XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).ContractService.GetCollection(Of CareGroupXpo)(Nothing, filterCareGroup).FirstOrDefault()
            Dim filterProcedures As String
            If item.CUPSEntityContractDescriptionId IsNot Nothing Then
                filterProcedures = " CupsId.Id = " & item.CUPSEntityId & " and ProceduresTemplateId = " & careGroup.ProcedureTemplateId.Id & " and CUPSEntityContractDescriptionId = " & item.CUPSEntityContractDescriptionId
            Else
                filterProcedures = " CupsId.Id = " & item.CUPSEntityId & " and ProceduresTemplateId = " & careGroup.ProcedureTemplateId.Id
            End If

            Dim procedure = XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).ContractService.GetCollection(Of Infrastructure.Data.Xpo.ContractRepository.ProcedureCupsXpo)(Nothing, filterProcedures).FirstOrDefault()

            If procedure IsNot Nothing Then
                listProcedures.Add(procedure)
            End If
        Next

        If listProcedures IsNot Nothing AndAlso listProcedures.Count > 0 Then
            For Each item In listProcedures
                If item.Quoted Then
                    messageReturn = "Los servicios " + String.Join(",", (From x In listProcedures Select x.CupsId.Code + " - " + x.CupsId.Description).ToArray()) + " requieren de una cotización, desea agregar una cotización?"
                End If
            Next
        End If

        Return messageReturn
    End Function

    ''' <summary>
    ''' lista los ingresos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetViewAdmissionServiceOrder() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.HisContainer).CrystalService.GetViewAdmissionServiceOrder()
    End Function

    ''' <summary>
    ''' lista los ingresos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetViewAdmissionServiceOrderByPatient(patientCode As String) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.HisContainer).CrystalService.GetViewAdmissionServiceOrderByPatient(patientCode)
    End Function

    ''' <summary>
    ''' lista los ingresos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetViewAdmissionOpenAndPartial() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.HisContainer).CrystalService.GetViewAdmissionOpenAndPartial()
    End Function


    ''' <summary>
    ''' lista los ingresos segun el tipo de bloqueo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetViewAdmissionOpenAndPartialServiceOrder() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.HisContainer).CrystalService.GetViewAdmissionOpenAndPartialServiceOrder()
    End Function

    ''' <summary>
    ''' Gets the list admissions filter.
    ''' </summary>
    ''' <returns></returns>
    Function GetListAdmissionsPatientCodeStatus(patientCode As String, status As String) As LinqInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.HisContainer).CrystalService.ListAdmissionsPatientCodeStatus(patientCode, status)
    End Function

    Function GetListAdmissionsByPatientCodeStatus(patientCode As String, status As String) As LinqInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.HisContainer).CrystalService.ListAdmissionsByPatientCodeStatus(patientCode, status)
    End Function

    ''' <summary>
    ''' lista los ingresos por el tercero
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetListAdmissionsPatientCode(patientCode As String) As LinqInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.HisContainer).CrystalService.ListAdmissionsPatientCode(patientCode)
    End Function

    ''' <summary>
    ''' lista los ingresos por el tercero con el estado abierto y parciales
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetListAdmissionsOpenAndPartialPatientCode(patientCode As String) As LinqInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.HisContainer).CrystalService.ListAdmissionsOpenAndPartialPatientCode(patientCode)
    End Function

    ''' <summary>
    ''' lista los ingresos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCareProfessionalByCode(code As String) As XPCollection(Of HealthCareProfessionalXpo)
        Return XpoServiceEx.Instance(_indigoSessionValues.HisContainer).CrystalService.GetCareProfessionalByCode(code)
    End Function

    ''' <summary>
    ''' lista los ingresos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCostCenter() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).PayrollService.GetCostCenterByState(True)
    End Function
    ''' <summary>
    ''' lista los servicios SOAT
    ''' </summary>
    ''' <param name="idCareGroup"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListSoatByCareGroup(idCareGroup As Integer) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).ContractService.ListSoatByCareGroup(idCareGroup)
    End Function
    Function ListSoatByCareGroupNoQx(idCareGroup As Integer) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).ContractService.ListSoatByCareGroupNotQx(idCareGroup)
    End Function
    ''' <summary>
    ''' lista los servicios SOAT por presentacion
    ''' </summary>
    Public Function ListSoatByCareGroupAndPresentation(caregroupId As Object, presentation As Integer) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).ContractService.ListSoatByCareGroupAndPresentation(caregroupId, presentation)
    End Function
    ''' <summary>
    ''' lista los servicios ISS
    ''' </summary>
    ''' <param name="idCareGroup"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListIssServicesByCareGroup(idCareGroup As Integer) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).ContractService.ListIssServicesByCareGroup(idCareGroup)
    End Function
    Function ListIssServicesByCareGroupNotQx(idCareGroup As Integer) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).ContractService.ListIssServicesByCareGroupNotQx(idCareGroup)
    End Function
    ''' <summary>
    ''' Lists the iss services by care group by presentation.
    ''' </summary>
    ''' <param name="idCareGroup">The identifier care group.</param>
    ''' <param name="presentation">The presentation.</param>
    ''' <returns></returns>
    Function ListIssServicesByCareGroupAndPresentation(idCareGroup As Integer, presentation As Integer) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).ContractService.ListIssServicesByCareGroupAndPresentation(idCareGroup, presentation)
    End Function
    ''' <summary>
    ''' lista los cups
    ''' </summary>
    ''' <param name="idCareGroup"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListCUPS(idCareGroup As Integer, Optional CupsId As List(Of Integer) = Nothing) As LinqInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).ContractService.ListCUPS(idCareGroup, CupsId)
    End Function
    ''' <summary>
    ''' lista las unidades funcionales
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListFunctionalUnit() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).PayrollService.ListFunctionalUnitUserAuthorized(_indigoSessionValues.UserIndigo)
    End Function
    ''' <summary>
    ''' lista los profesionales de la salud
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListHealthCareProfessional() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.HisContainer).CrystalService.ListHealthCareProfessional()
    End Function
    Public Function ListHealthCareProfessionalByProfile(profile As List(Of Integer)) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.HisContainer).CrystalService.ListHealthCareProfessionalByProfile(profile)
    End Function
    ''' <summary>
    ''' lista las entidades prestadoras de salud
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListHealthAdministratorByStatus() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).ContractService.ListHealthAdministratorByStatus(True)
    End Function

    ''' <summary>
    ''' lista los grupos de atencion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListCareGroup() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).ContractService.ListCareGroupByStatus(True)
    End Function

    ''' <summary>
    ''' lista los grupos de atencion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListRIASCups(cupsCode As String) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).BillingService.ListRIASCupsByCupsCodeAndStatus(cupsCode, 1)
    End Function

    ''' <summary>
    ''' Lista las descripciones asociadas al cups
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListContractDescriptionsByCupsEntityId(CupsEntityId As Integer) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).ContractService.ListContractDescriptionsByCupsEntityId(CupsEntityId)
    End Function

    ''' <summary>
    ''' Obtiene el cups por código
    ''' </summary>
    ''' <returns></returns>
    Public Function GetRiasCupsByRiasCupsId(riasCupsId As Integer) As ViewRIASCupsXpo
        Dim filter As String = "RiasCupsId = " & riasCupsId
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).BillingService.GetCollection(Of ViewRIASCupsXpo)(Nothing, filter).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Obtiene la relación entre el cups y la descripción
    ''' </summary>
    ''' <returns></returns>
    Public Function GetCUPSEntityContractDescriptionById(Id As Integer) As Infrastructure.Data.Xpo.ContractRepository.CUPSEntityContractDescriptionsXpo
        Dim filter As String = $"Id = {Id} and IsDelete = 0"
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).ContractService.GetCollection(Of Infrastructure.Data.Xpo.ContractRepository.CUPSEntityContractDescriptionsXpo)(Nothing, filter).FirstOrDefault()
    End Function

    ''' <summary>
    ''' lista los terceros
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListThirdParty() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).CommonService.GetThirdParty()
    End Function

    ''' <summary>
    ''' Obtiene el cups por código
    ''' </summary>
    ''' <returns></returns>
    Public Function GetCupsEntityByCode(cupsCode As String) As CUPSEntityXpo
        Dim filter As String = "Code = '" & cupsCode & "'"
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).ContractService.GetCollection(Of CUPSEntityXpo)(Nothing, filter).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Obtiene el cups por código
    ''' </summary>
    ''' <returns></returns>
    Public Function GetCupsEntityById(CupsEntityId As Integer) As Infrastructure.Data.Xpo.ContractRepository.CupsEntityXpo
        Dim filter As String = $"Id = {CupsEntityId} and CUPSEntityContractDescriptionsXpo[IsDelete=0]"
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).ContractService.GetCollection(Of Infrastructure.Data.Xpo.ContractRepository.CupsEntityXpo)(Nothing, filter).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Se obtiene si el numero de autorización es obligatorio
    ''' </summary>
    ''' <returns></returns>
    Public Function GetControlByTypeFunctionalUnit(careGroupId As Integer, unitType As Integer) As Infrastructure.Data.Xpo.ContractRepository.ControlByTypeFunctionalUnitXpo
        Dim filter As String = "CareGroupId = " & careGroupId & " and UnitType = " & unitType
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).ContractService.GetCollection(Of Infrastructure.Data.Xpo.ContractRepository.ControlByTypeFunctionalUnitXpo)(Nothing, filter).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Se obtiene si el numero de autorización es obligatorio
    ''' </summary>
    ''' <returns></returns>
    Public Function GetFunctionalUnitById(functionalUnitId As Integer) As FunctionalUnitXpo
        Dim filter As String = "Id = " & functionalUnitId
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).ContractService.GetCollection(Of FunctionalUnitXpo)(Nothing, filter).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Método que me permite saber si se abre el form modal del numero de autorización
    ''' </summary>
    ''' <returns></returns>
    Public Function ValidateAuthorizationNumber(careGroupId As Integer, functionalUnitId As Integer, functionalUnitCode As String) As Boolean
        'Valor por defecto
        ValidateAuthorizationNumber = False

        'Filtro para la unidad funcional
        Dim filterFuntionalUnit As String = ""

        If functionalUnitId > 0 Then 'Si se consulta con el id
            filterFuntionalUnit = "Id = " & functionalUnitId
        Else 'Si se consulta con el código
            filterFuntionalUnit = "Code = '" & functionalUnitCode & "'"
        End If

        'Se obtiene la unidad funcional para saber el tipo de unidad
        Dim functionalUnit = XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).ContractService.GetCollection(Of FunctionalUnitXpo)(Nothing, filterFuntionalUnit).FirstOrDefault()

        If functionalUnit IsNot Nothing Then 'Si encuentra la unidad funcional
            'Se obtiene el tipo de unidad
            Dim unitTypeFunctionalUnit = Utils.GetFunctionalUnitType(functionalUnit.UnitType)

            'Se obtiene la parametrización del grupo de atención de los tipos de unidad para saber si el número de autorización es obligatorio
            Dim filterCareGroup As String = "CareGroupId = " & careGroupId & " and UnitType = " & unitTypeFunctionalUnit
            Dim controlByTypeFunctionalUnit = XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).ContractService.GetCollection(Of Infrastructure.Data.Xpo.ContractRepository.ControlByTypeFunctionalUnitXpo)(Nothing, filterCareGroup).FirstOrDefault()

            If controlByTypeFunctionalUnit IsNot Nothing Then 'Si encuentra la parametrización
                'Se asigna el valor
                ValidateAuthorizationNumber = controlByTypeFunctionalUnit.MandatoryAuthorization
            End If
        End If

        'Se retorna
        Return ValidateAuthorizationNumber
    End Function

    ''' <summary>
    ''' Validar un detalle de la orden de servicio
    ''' </summary>
    ''' <param name="listServiceOrderDetail"></param>
    ''' <returns></returns>
    Public Async Function ValidateServiceOrderDetail(listServiceOrderDetail As List(Of ServiceOrderDetail)) As Task(Of ActionResult(Of ServiceOrderDetail))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.ValidateServiceOrderDetailAsync(listServiceOrderDetail, _indigoSessionValues.AuditMessageWcf)
    End Function

    Public Function GetViewAdmissionOpenAndPartialByPatientCode(PatientCode As String) As List(Of ViewAdmissionOpenAndPartial)
        Dim filter As String = "PatientCode = '" & PatientCode & "'"
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).CrystalService.GetCollection(Of ViewAdmissionOpenAndPartial)(Nothing, filter).ToList()
    End Function

    ''' <summary>
    ''' consulta los parametro de empresa
    ''' </summary>
    ''' <returns></returns>
    Public Async Function CompanySettings() As Task(Of GeneralLedgerCompanySettingsXpo)
        Return Await Task.Factory.StartNew(Function() As GeneralLedgerCompanySettingsXpo
                                               Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).AccountingService.GetXPOObject(Of GeneralLedgerCompanySettingsXpo)(Nothing)
                                           End Function)
    End Function

    ''' <summary>
    ''' lista los grupos de atencion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListRIASCups(cupsCode As List(Of String)) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).BillingService.ListRIASCupsByCupsCodeAndStatus(cupsCode, 1)
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
