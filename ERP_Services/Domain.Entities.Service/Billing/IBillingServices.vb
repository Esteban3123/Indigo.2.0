'************************************************************
' Assembly         : Domain.Entities.Service
' Author           : Carlos Ernesto Cordoba
' Created          : 24-11-2014
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Domain.Base.Entities
Imports Domain.Crystal.Entities
Imports Infrastructure.CrossCutting.Base

#End Region

Public Interface IBillingServices
    Inherits IDisposable

#Region "Methods"

    Function ValidationsContract(contract As Contract) As ActionResult

    ''' <summary>
    ''' Loads the invoice object.
    ''' </summary>
    ''' <param name="Folio">The identifier folio.</param>
    ''' <returns></returns>
    Function CreateInvoiceObject(Folio As RevenueControlDetail, OperativeUnitId As Integer, settingBilling As SettingsBilling, billingAuthorization As BillingAuthorization, ListPortfolioAdvanceCrossing As List(Of PortfolioAdvance)) As ActionResult(Of Invoice)

    ''' <summary>
    ''' Gets the service value.
    ''' </summary>
    ''' <param name="CupsEntityId">Id del CUPS</param>
    ''' <param name="IPSServiceId">Id del servicio IPS</param>
    ''' <param name="CareGroupId">Id del grupo de atencion</param>
    ''' <param name="FunctionalUnitId">Id de la unidad funcional</param>
    ''' <param name="Specialty">Especialidad del medico</param>
    ''' <param name="ServiceDate">Fecha del servicio</param>
    ''' <param name="PatientGenus">Genero del paciente</param>
    ''' <param name="PatientDateBirth">Fecha de nacimiento del paciente</param>
    ''' <param name="InvoicedQuantity">Cantidad del servicio</param>
    ''' <param name="ProfessionalHealthCode">Codigo del medico</param>
    ''' <param name="ProfessionalHealthThirdPartyId">Id del tercero asignado al medico</param>
    ''' <returns></returns>
    Function GetServiceValue(AdmissionNumber As String, CenterAttentionCode As String, CupsEntityId As Integer, IPSServiceId As Integer, CareGroupId As Integer, FunctionalUnitId As Integer?, Specialty As String, ServiceDate As Date, PatientGenus As Integer, PatientDateBirth As Date, InvoicedQuantity As Integer, ProfessionalHealthCode As String, ProfessionalHealthThirdPartyId As Integer?, Optional RiasId As Integer? = Nothing, Optional ContractDescriptionId As Integer? = Nothing) As ActionResult(Of ServiceOrderDetail)

    Function GetServiceValueByManualId(ServiceDetail As ServiceOrderDetail, IPSServiceId As Integer, CareGroupId As Integer, FunctionalUnitId As Integer?, Specialty As String, ServiceDate As Date, PatientGenus As Integer, PatientDateBirth As Date, InvoicedQuantity As Integer, ProfessionalHealthCode As String, ProfessionalHealthThirdPartyId As Integer?) As ActionResult(Of ServiceOrderDetail)

    ''' <summary>
    ''' Calcula el valor del servicio con materiales de sutura
    ''' </summary>
    ''' <param name="CupsEntityId">The cups entity identifier.</param>
    ''' <param name="IPSServiceId">The ips service identifier.</param>
    ''' <param name="CareGroupId">The care group identifier.</param>
    ''' <param name="FunctionalUnitId">The functional unit identifier.</param>
    ''' <param name="Specialty">The specialty.</param>
    ''' <param name="ServiceDate">The service date.</param>
    ''' <param name="PatientGenus">The patient genus.</param>
    ''' <param name="PatientDateBirth">The patient date birth.</param>
    ''' <param name="InvoicedQuantity">The invoiced quantity.</param>
    ''' <param name="ProfessionalHealthCode">The professional health code.</param>
    ''' <param name="ProfessionalHealthThirdPartyId">The professional health third party identifier.</param>
    ''' <returns></returns>
    Function GetValueService(AdmissionNumber As String, CenterAttentionCode As String, CupsEntityId As Integer, IPSServiceId As Integer, CareGroupId As Integer, FunctionalUnitId As Integer?, Specialty As String, ServiceDate As Date, PatientGenus As Integer, PatientDateBirth As Date, InvoicedQuantity As Integer, ProfessionalHealthCode As String, ProfessionalHealthThirdPartyId As Integer?, Optional RiasId As Integer? = Nothing, Optional ContractDescriptionId As Integer? = Nothing) As ActionResult(Of ServiceOrderDetail)

    ''' <summary>
    ''' Gets the service value.
    ''' </summary>
    ''' <param name="careGroupId">The care group identifier.</param>
    ''' <param name="ProductId">The product identifier.</param>
    ''' <returns></returns>
    Function GetProductRateDetail(careGroupId As Integer, ProductId As Integer, serviceDate As DateTime) As ActionResult(Of ProductRateDetail)

    ''' <summary>
    ''' obtiene el valor con recargo
    ''' </summary>
    ''' <param name="serviceOrderDetail"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetServiceValueSurcharge(serviceOrderDetail As ServiceOrderDetail) As ServiceOrderDetail

    ''' <summary>
    ''' obtener el valor de los detalles del ips quirurgico cuando el usuario cambia los valores por defecto en el formulario
    ''' </summary>
    ''' <param name="serviceOrderDetail"></param>
    ''' <param name="listSurgicalProcedureServiceDefault"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetServiceValueBySurgicalProcedureService(serviceOrderDetail As ServiceOrderDetail, listSurgicalProcedureServiceDefault As List(Of SurgicalProcedureService)) As ActionResult(Of ServiceOrderDetail)
    ''' <summary>
    ''' metodo para recalcular los eventos cuando se cambie el item que es primer evento
    ''' </summary>
    ''' <param name="serviceOrderDetail"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function RecalculateSurgicalEvents(serviceOrderDetail As ServiceOrderDetail) As ServiceOrderDetail

    ''' <summary>
    ''' Obtiene el precio del servicio en el manual tarifario para cada una de las estancias
    ''' </summary>
    ''' <param name="caregroupId">Id del grupo de atención</param>
    ''' <param name="staysList">Lista de estancias a calcular. Las estancias deben ir con agregados</param>
    ''' <param name="collectStay">Valor que indica si se usa el CUPS de estancia o de observación</param>
    ''' <returns>La lista de estancias con el precio de cada servicio</returns>
    Function GetPriceToStaysList(ByVal caregroupId As Integer, ByVal staysList As List(Of CHREGESTA), stayOption As eLiquidateStayOption, Optional ByVal collectStay As Boolean = True, Optional endDate As Date? = Nothing) As ActionResult(Of List(Of CHREGESTA))

    ''' <summary>
    ''' funcion para calcular el valor de un servicio cuando se hace desde el modulo de central de mezclas especificamente desde Contratos de centros de atencion externos
    ''' </summary>
    ''' <param name="ContractExternalClientId"></param>
    ''' <param name="CupsId"></param>
    ''' <param name="FunctionalUnitId"></param>
    ''' <param name="Specialty"></param>
    ''' <param name="ServiceDate"></param>
    ''' <param name="IPSServiceId"></param>
    ''' <param name="ManualType"></param>
    ''' <param name="RiasId"></param>
    ''' <param name="ContractDescriptionId"></param>
    ''' <returns></returns>
    Function GetServiceValueToMS(ContractExternalClientId As Integer, CupsId As Integer, FunctionalUnitId As Integer, Specialty As String, ServiceDate As Date, IPSServiceId As Integer, Optional ManualType As Integer = 0, Optional RiasId As Integer? = Nothing, Optional ContractDescriptionId As Integer? = Nothing) As ActionResult(Of List(Of CupsHomologation))
#End Region

#Region "Liquidation Methods"


    ''' <summary>
    ''' Liquidates the recovery fee.
    ''' </summary>
    ''' <param name="admission">The admission.</param>
    ''' <param name="idFolio">The identifier folio.</param>
    ''' <param name="liquidate">solo aparece si el tipo de liquidacion es de multiseleccion y su es true quiere decir que se esta tratando de liquidar, si es falso esta tratando de remover</param>
    ''' <returns></returns>
    Function LiquidateRecoveryFee(ByVal admission As Object, ByVal idFolio As Integer, serviceOrderDetailDistributionDetail As Integer, ServiceDistributionList As List(Of Object), LiquidationType As eLiquidateRecoveryType, Optional liquidate As Boolean = False, Optional args As Object = Nothing) As ActionResult(Of RevenueControlDetail)

    ''' <summary>
    ''' Loads the journa voucher liquidation.
    ''' </summary>
    ''' <returns></returns>
    Function GenerateJournaVoucherLiquidation(revenueControlDetail As RevenueControlDetail, OperativeUnitId As Integer, invoiceNumber As String, reverse As Boolean) As ActionResult(Of JournalVouchers)

    ''' <summary>
    ''' Actualiza los valores del folio
    ''' </summary>
    ''' <param name="revenueControlDetailId">The revenue control detail identifier.</param>
    ''' <returns></returns>
    Function UpdateRevenueControlDetailValues(revenueControlDetailId As Integer, Optional folio As RevenueControlDetail = Nothing, Optional operativeUnitId As Integer? = Nothing) As ActionResult

    ''' <summary>
    ''' Actualiza los valores del folio
    ''' </summary>
    ''' <param name="revenueControlDetailId">The revenue control detail identifier.</param>
    ''' <returns></returns>
    Function UpdateRevenueControlDetailValuesNew(revenueControlDetailId As Integer, Optional folio As RevenueControlDetail = Nothing, Optional operativeUnitId As Integer? = Nothing) As ActionResult

    ''' <summary>
    ''' Sets the service value.
    ''' </summary>
    ''' <param name="sodd">The sodd.</param>
    ''' <param name="sod">The sod.</param>
    ''' <param name="GuidHomologation">The unique identifier homologation.</param>
    ''' <param name="careGroupId">The care group identifier.</param>
    Sub setServiceValue(sodd As ServiceOrderDetailDistribution, sod As ServiceOrderDetail, GuidHomologation As String, careGroupId As Integer)

    ''' <summary>
    ''' Crea un nuevo detalle de orden de servicio para distribucion por unidades
    ''' </summary>
    ''' <param name="serviceOrderDetail">The service order detail.</param>
    ''' <returns></returns>
    Function CreateNewServiceOrderDetail(serviceOrderDetail As ServiceOrderDetail, invoiceQuantity As Integer) As ServiceOrderDetail

    ''' <summary>
    ''' Gets the income main account.
    ''' </summary>
    ''' <param name="serviceOrderDetail">The service order detail.</param>
    ''' <param name="CareGroup">The care group.</param>
    ''' <returns></returns>
    Function GetIncomeMainAccount(serviceOrderDetail As ServiceOrderDetail, CareGroupId As Integer, Optional careGroup As CareGroup = Nothing) As ActionResult

#End Region

End Interface
