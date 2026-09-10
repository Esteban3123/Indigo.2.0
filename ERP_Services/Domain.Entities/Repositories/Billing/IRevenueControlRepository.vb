'************************************************************
' Assembly         : Domain.Billing
' Author           : Juan F. Tamayo
' Created          : 2014-11-27
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Domain.Base
Imports Domain.Entities
Imports System.Dynamic
Imports Domain.Base.Entities

#End Region

Public Interface IRevenueControlRepository
    Inherits IRepository(Of RevenueControl)

    ''' <summary>
    ''' Obtiene los datos de control de ingreso con sus folios y detalles de liquidación
    ''' </summary>
    ''' <param name="admissionCode">Número del ingreso</param>
    ''' <returns>Objeto con información del ingreso, folios y tipo de liquidación</returns>
    Function GetControlPOCOByCode(ByVal admissionCode As String) As FolioDataHeader

    ''' <summary>
    ''' Obtiene un control de ingreso por su número de admisión
    ''' </summary>
    ''' <param name="AdmissionNumber">Número de ingreso</param>
    ''' <returns>Control de ingreso encontrado o Nothing</returns>
    Function GetRevenueControlByAdmissionNumber(AdmissionNumber As String) As RevenueControl

    ''' <summary>
    ''' Obtiene un control de ingreso por su Id
    ''' </summary>
    ''' <param name="Id">Id del control de ingreso</param>
    ''' <returns>Control de ingreso encontrado o Nothing</returns>
    Function GetRevenueControlById(Id As Integer) As RevenueControl

    ''' <summary>
    ''' Obtiene un control de ingreso con todas sus entidades relacionadas (detalles, distribuciones, órdenes de servicio y tarifas)
    ''' </summary>
    ''' <param name="id">Id del control de ingreso</param>
    ''' <returns>Control de ingreso con todas sus relaciones cargadas</returns>
    Function GetRevenueControlWithAggregatesById(ByVal id As Integer) As RevenueControl

    ''' <summary>
    ''' Verifica si los folios seleccionados representan la totalidad de folios pendientes por liquidar del ingreso
    ''' </summary>
    ''' <param name="listRevenueControlDetailId">Lista de Ids de los folios a liquidar</param>
    ''' <param name="RevenueControlId">Id del control de ingreso</param>
    ''' <returns>Resultado con estado de verificación y folio sin distribuir (si existe)</returns>
    Function VerifyIsTotalFolioToLiquidate(listRevenueControlDetailId As List(Of Integer), RevenueControlId As Integer) As ActionResult(Of RevenueControlDetail)

    ''' <summary>
    ''' Cierra un ingreso cambiando su estado a 'Cerrado'
    ''' </summary>
    ''' <param name="admissionNumber">Número de ingreso a cerrar</param>
    ''' <param name="containerCrystal">Contenedor de Crystal</param>
    ''' <param name="UserCode">Código del usuario que cierra el ingreso</param>
    ''' <returns>Resultado del procedimiento almacenado</returns>
    Function CloseAdmission(admissionNumber As String, containerCrystal As String, UserCode As String) As SP_CloseAdmission_Result

    ''' <summary>
    ''' Ejecuta la retarificación de servicios de un folio con base en grupo de atención, homologaciones y tarifas
    ''' </summary>
    ''' <param name="revenuecontrolDetailId">Id del detalle del control de ingreso (folio)</param>
    ''' <param name="careGroupId">Id del grupo de atención</param>
    ''' <param name="patientGenus">Género del paciente</param>
    ''' <param name="patientBirth">Fecha de nacimiento del paciente</param>
    ''' <param name="listHomologationsXml">XML con homologaciones</param>
    ''' <param name="onlyRateChange">Indica si solo se cambia tarifa sin recalcular</param>
    ''' <param name="thirdPartyPatientId">Id del paciente tercero</param>
    ''' <param name="healthAdministratorId">Id de la administradora de salud</param>
    ''' <param name="listServiceOrderDetailWithQxXml">XML con detalles de orden de servicio quirúrgicos</param>
    ''' <returns>Resultado del procedimiento almacenado</returns>
    Function SP_ChangeRateServices(revenuecontrolDetailId As Integer?,
                                          careGroupId As Integer?,
                                          patientGenus As Integer?,
                                          patientBirth As Date?,
                                          listHomologationsXml As String,
                                          onlyRateChange As Boolean?,
                                          thirdPartyPatientId As Integer?,
                                          healthAdministratorId As Integer?,
                                          listServiceOrderDetailWithQxXml As String) As SP_ChangeRateServices_Result

    ''' <summary>
    ''' Ejecuta la distribución de servicios y productos de un folio origen a un folio destino
    ''' </summary>
    ''' <param name="revenueControlId">Id del control de ingreso</param>
    ''' <param name="sourceFolioId">Id del folio origen</param>
    ''' <param name="targetFolioId">Id del folio destino</param>
    ''' <param name="distribType">Tipo de distribución</param>
    ''' <param name="distributeQuantity">Cantidad de ítems a distribuir</param>
    ''' <param name="caregroupId">Id del grupo de atención origen</param>
    ''' <param name="careGroupIdTarget">Id del grupo de atención destino</param>
    ''' <param name="changeRateServicesNeccesary">Indica si es necesario cambiar tarifas</param>
    ''' <param name="user">Código del usuario que ejecuta la distribución</param>
    ''' <param name="patientGenus">Género del paciente</param>
    ''' <param name="patientBirth">Fecha de nacimiento del paciente</param>
    ''' <param name="onlyRateChange">Indica si solo se cambia tarifa</param>
    ''' <param name="thirdPartyPatientId">Id del paciente tercero</param>
    ''' <param name="healthAdministratorId">Id de la administradora de salud</param>
    ''' <param name="folioType">Tipo de folio</param>
    ''' <param name="contractEntityId">Id de la entidad del contrato</param>
    ''' <param name="thirdPartyId">Id del tercero</param>
    ''' <param name="productsAndServicesXml">XML con productos y servicios a distribuir</param>
    ''' <param name="homologationsXml">XML con homologaciones</param>
    ''' <param name="listServiceOrderDetailXml">XML con detalles de orden de servicio</param>
    ''' <returns>Resultado del procedimiento almacenado</returns>
    Function SP_DistributeFolio(revenueControlId As Integer?,
                                       sourceFolioId As Integer?,
                                       targetFolioId As Integer?,
                                       distribType As Byte?,
                                       distributeQuantity As Integer?,
                                       caregroupId As Integer?,
                                       careGroupIdTarget As Integer?,
                                       changeRateServicesNeccesary As Boolean?,
                                       user As String,
                                       patientGenus As Integer?,
                                       patientBirth As Date?,
                                       onlyRateChange As Boolean?,
                                       thirdPartyPatientId As Integer?,
                                       healthAdministratorId As Integer?,
                                       folioType As Byte?,
                                       contractEntityId As Integer?,
                                       thirdPartyId As Integer?,
                                       productsAndServicesXml As String,
                                       homologationsXml As String,
                                       listServiceOrderDetailXml As String) As SP_DistributeFolio_Result

    ''' <summary>
    ''' Ejecuta el proceso de liquidación de uno o varios folios de un ingreso
    ''' </summary>
    ''' <param name="patientCode">Código del paciente</param>
    ''' <param name="admissionNumber">Número de ingreso</param>
    ''' <param name="containerCrystal">Contenedor de Crystal</param>
    ''' <param name="billingAuthorizationId">Id de la autorización de facturación</param>
    ''' <param name="operativeUnitId">Id de la unidad operativa</param>
    ''' <param name="thirdPartyPatientId">Id del paciente tercero</param>
    ''' <param name="userCode">Código del usuario que liquida</param>
    ''' <param name="companyType">Tipo de empresa</param>
    ''' <param name="revenueControlDetailCrossingListXml">XML con lista de folios a liquidar</param>
    ''' <param name="skipAccountControlValidations">Indica si se omiten validaciones de control de cuenta</param>
    ''' <returns>Lista de resultados de liquidación</returns>
    Function LiquidateFolio(patientCode As String, admissionNumber As String, containerCrystal As String, billingAuthorizationId As Integer?, operativeUnitId As Integer?, thirdPartyPatientId As Integer?, userCode As String, companyType As Byte?, revenueControlDetailCrossingListXml As String, skipAccountControlValidations As Boolean) As List(Of LiquidateFolio_Result)

    ''' <summary>
    ''' Asocia una factura a un folio (detalle de control de ingreso)
    ''' </summary>
    ''' <param name="RevenueControlDetailId">Id del folio a asociar</param>
    ''' <param name="InvoiceId">Id de la factura</param>
    ''' <param name="UserCode">Código del usuario que realiza la asociación</param>
    ''' <returns>Resultado del procedimiento almacenado</returns>
    Function SP_AssociateInvoice(RevenueControlDetailId As Integer, InvoiceId As Integer, ByVal UserCode As String) As SP_AssociateInvoice_Result

    ''' <summary>
    ''' Obtiene una lista de ingresos con base en la lista de ids de folios y el número de ingreso cabecera
    ''' </summary>
    ''' <param name="listFoliosIds">Lista de ids de folios</param>
    ''' <param name="admissionNumber">Número de ingreso cabecera</param>
    ''' <returns>Lista de números de ingresos unificados</returns>
    Function UnifiedAdmissionListByFolioIds(listFoliosIds As List(Of Integer), admissionNumber As String) As List(Of String)

End Interface
