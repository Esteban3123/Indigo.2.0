'************************************************************
' Assembly         : Infrastructure.Data.Billing
' Author           : Juan F. Tamayo
' Created          : 2014-11-27
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Base

#End Region

Public Class RevenueControlRepository
    Inherits GenericRepository(Of RevenueControl)
    Implements IRevenueControlRepository

    ''' <summary>
    ''' Prefijo del tipo de paciente en los recursos
    ''' </summary>
    Private Const LIQUIDATIONTYPE_PREFIX As String = "LiquidationType_"

    ''' <summary>
    ''' Nombre del modulo o archivo en los recursos
    ''' </summary>
    Private Const MODULENAME As String = "Billing"

    ' Contexto del repositorio
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    ''' Constructor del repositorio de control de ingresos
    ''' </summary>
    ''' <param name="context">Contexto del repositorio</param>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtiene los datos de control de ingreso con sus folios y detalles de liquidación
    ''' </summary>
    ''' <param name="admissionCode">Número del ingreso</param>
    ''' <returns>Objeto con información del ingreso, folios y tipo de liquidación</returns>
    Public Function GetControlPOCOByCode(admissionCode As String) As FolioDataHeader Implements IRevenueControlRepository.GetControlPOCOByCode

        Dim ressp = _context.SP_GetControlPOCOByCode(admissionCode).ToList()


        Dim admission As New FolioDataHeader()
        admission.Id = 0
        admission.FolioQuantity = 0
        'Aqui se carga la cantidad de folios y sus Ids
        If ressp IsNot Nothing AndAlso ressp.Any() Then
            Dim list As New List(Of FolioDataDetail)()
            admission.Id = ressp(0).Id
            admission.FolioQuantity = ressp(0).FolioQuantity
            admission.LiquidationType = ressp(0).LiquidationType
            admission.LiquidationTypeName = ResourceManager.GetString(LIQUIDATIONTYPE_PREFIX & ressp(0).LiquidationType, MODULENAME)
            admission.ContractCodeName = String.Empty
            Dim objLockSurgical As New Object()
            Parallel.ForEach(ressp, Sub(d)
                                        SyncLock objLockSurgical
                                            list.Add(New FolioDataDetail())
                                            list(list.Count - 1).Id = d.RevenueControlDetailId
                                            list(list.Count - 1).Status = d.RevenueControlDetailStatus
                                            If d.RevenueControlDetailStatus = 2 Then
                                                'Facturado
                                                list(list.Count - 1).FolioName = $"Factura # {d.InvoiceNumber}"
                                            Else
                                                list(list.Count - 1).FolioName = $"Folio # {d.FolioOrder}"
                                            End If
                                        End SyncLock
                                    End Sub)
            admission.ListRevenueControlDetails = list
            If ressp(0).ContractId IsNot Nothing Then
                admission.ContractCodeName = ressp(0).ContractCodeName
            End If
        End If

        Return admission
    End Function


    ''' <summary>
    ''' Obtiene un control de ingreso por su número de admisión
    ''' </summary>
    ''' <param name="AdmissionNumber">Número de ingreso</param>
    ''' <returns>Control de ingreso encontrado o Nothing</returns>
    Public Function GetRevenueControlByAdmissionNumber(AdmissionNumber As String) As RevenueControl Implements IRevenueControlRepository.GetRevenueControlByAdmissionNumber
        Return (From rc In _context.RevenueControl Where rc.AdmissionNumber = AdmissionNumber Select rc).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Obtiene un control de ingreso por su Id
    ''' </summary>
    ''' <param name="Id">Id del control de ingreso</param>
    ''' <returns>Control de ingreso encontrado o Nothing</returns>
    Public Function GetRevenueControlById(Id As Integer) As RevenueControl Implements IRevenueControlRepository.GetRevenueControlById
        Return (From rc In _context.RevenueControl Where rc.Id = Id Select rc).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Obtiene un control de ingreso con todas sus entidades relacionadas (detalles, distribuciones, órdenes de servicio y tarifas)
    ''' </summary>
    ''' <param name="id">Id del control de ingreso</param>
    ''' <returns>Control de ingreso con todas sus relaciones cargadas</returns>
    Public Function GetRevenueControlWithAggregatesById(id As Integer) As RevenueControl Implements IRevenueControlRepository.GetRevenueControlWithAggregatesById
        Return (From rc In Me._context.RevenueControl.Include("RevenueControlDetail").Include("RevenueControlDetail.ServiceOrderDetailDistribution").Include("RevenueControlDetail.ServiceOrderDetailDistribution.ServiceOrderDetail").Include("RevenueControlDetail.ServiceOrderDetailDistribution.ServiceOrderDetail.RateManualDetail") Where rc.Id = id Select rc).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Verifica si los folios seleccionados representan la totalidad de folios pendientes por liquidar del ingreso
    ''' </summary>
    ''' <param name="listRevenueControlDetailId">Lista de Ids de los folios a liquidar</param>
    ''' <param name="RevenueControlId">Id del control de ingreso</param>
    ''' <returns>Resultado con estado de verificación y folio sin distribuir (si existe)</returns>
    Public Function VerifyIsTotalFolioToLiquidate(listRevenueControlDetailId As List(Of Integer), RevenueControlId As Integer) As ActionResult(Of RevenueControlDetail) Implements IRevenueControlRepository.VerifyIsTotalFolioToLiquidate
        Dim one As Byte = 1
        Dim three As Byte = 3
        Dim query = (From rcd In _context.RevenueControlDetail.AsNoTracking().Include("ServiceOrderDetailDistribution").AsNoTracking()
                     Where rcd.RevenueControlId = RevenueControlId AndAlso (rcd.Status = one OrElse rcd.Status = three)
                     Select rcd).ToList()

        If query IsNot Nothing AndAlso query.Any() Then
            Dim FolioWithOutDistributeditems As RevenueControlDetail = query.Where(Function(x) Not x.ServiceOrderDetailDistribution _
                                                                        .Any(Function(o) o.DistributionType <> one)) _
                                                                        .OrderByDescending(Function(x) x.FolioOrder) _
                                                                        .FirstOrDefault()

            Dim IsTotal As Boolean = query.Count > 0 AndAlso query.Count = listRevenueControlDetailId.Count
            Return New ActionResult(Of RevenueControlDetail) With {.StateResult = IsTotal, .ObjectEmbbeded = FolioWithOutDistributeditems}
        Else
            Return New ActionResult(Of RevenueControlDetail) With {.StateResult = False, .ObjectEmbbeded = Nothing}
        End If
    End Function

    ''' <summary>
    ''' Cierra un ingreso cambiando su estado a 'Cerrado'
    ''' </summary>
    ''' <param name="admissionNumber">Número de ingreso a cerrar</param>
    ''' <param name="containerCrystal">Contenedor de Crystal</param>
    ''' <param name="UserCode">Código del usuario que cierra el ingreso</param>
    ''' <returns>Resultado del procedimiento almacenado</returns>
    Public Function CloseAdmission(admissionNumber As String, containerCrystal As String, UserCode As String) As SP_CloseAdmission_Result Implements IRevenueControlRepository.CloseAdmission
        Return _context.SP_CloseAdmission(admissionNumber, containerCrystal, UserCode).FirstOrDefault()
    End Function

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
    Public Function SP_ChangeRateServices(revenuecontrolDetailId As Integer?,
                                          careGroupId As Integer?,
                                          patientGenus As Integer?,
                                          patientBirth As Date?,
                                          listHomologationsXml As String,
                                          onlyRateChange As Boolean?,
                                          thirdPartyPatientId As Integer?,
                                          healthAdministratorId As Integer?,
                                          listServiceOrderDetailWithQxXml As String) As SP_ChangeRateServices_Result Implements IRevenueControlRepository.SP_ChangeRateServices
        CType(_context, System.Data.Entity.Infrastructure.IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_ChangeRateServices(revenuecontrolDetailId, careGroupId,
                                              patientGenus,
                                              patientBirth,
                                              listHomologationsXml,
                                              onlyRateChange,
                                              thirdPartyPatientId,
                                              healthAdministratorId,
                                              listServiceOrderDetailWithQxXml).FirstOrDefault()
    End Function

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
    Public Function SP_DistributeFolio(revenueControlId As Integer?,
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
                                       listServiceOrderDetailXml As String) As SP_DistributeFolio_Result Implements IRevenueControlRepository.SP_DistributeFolio
        CType(_context, System.Data.Entity.Infrastructure.IObjectContextAdapter).ObjectContext.CommandTimeout = 3600

        Return _context.SP_DistributeFolio(revenueControlId,
                                           sourceFolioId,
                                           targetFolioId,
                                           distribType,
                                           distributeQuantity,
                                           caregroupId,
                                           careGroupIdTarget,
                                           changeRateServicesNeccesary,
                                           user,
                                           patientGenus,
                                           patientBirth,
                                           onlyRateChange,
                                           thirdPartyPatientId,
                                           healthAdministratorId,
                                           folioType,
                                           contractEntityId,
                                           thirdPartyId,
                                           productsAndServicesXml,
                                           homologationsXml,
                                           listServiceOrderDetailXml).FirstOrDefault()
    End Function

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
    Public Function LiquidateFolio(patientCode As String, admissionNumber As String, containerCrystal As String, billingAuthorizationId As Integer?, operativeUnitId As Integer?, thirdPartyPatientId As Integer?, userCode As String, companyType As Byte?, revenueControlDetailCrossingListXml As String, skipAccountControlValidations As Boolean) As List(Of LiquidateFolio_Result) Implements IRevenueControlRepository.LiquidateFolio
        CType(_context, System.Data.Entity.Infrastructure.IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.LiquidateFolio(patientCode, admissionNumber, containerCrystal, billingAuthorizationId, operativeUnitId, thirdPartyPatientId, userCode, companyType, revenueControlDetailCrossingListXml, skipAccountControlValidations).ToList()
    End Function

    ''' <summary>
    ''' Asocia una factura a un folio (detalle de control de ingreso)
    ''' </summary>
    ''' <param name="RevenueControlDetailId">Id del folio a asociar</param>
    ''' <param name="InvoiceId">Id de la factura</param>
    ''' <param name="UserCode">Código del usuario que realiza la asociación</param>
    ''' <returns>Resultado del procedimiento almacenado</returns>
    Public Function SP_AssociateInvoice(RevenueControlDetailId As Integer, InvoiceId As Integer, ByVal UserCode As String) As SP_AssociateInvoice_Result Implements IRevenueControlRepository.SP_AssociateInvoice
        CType(_context, System.Data.Entity.Infrastructure.IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_AssociateInvoice(RevenueControlDetailId, InvoiceId, UserCode).FirstOrDefault()
    End Function


    ''' <summary>
    ''' Obtiene una lista de ingresos con base en la lista de ids de folios y el numero de ingreso cabecera
    ''' La lista obedece a los ingresos relacionados/unificados de uno o varios folios que se liquidarán.
    ''' </summary>
    ''' <param name="listFoliosIds">Lista de ids de folios</param>
    ''' <param name="admissionNumber">Numero de ingreso cabecera</param>
    ''' <returns>Lista de ingresos unificados</returns>
    Public Function UnifiedAdmissionListByFolioIds(listFoliosIds As List(Of Integer), admissionNumber As String) As List(Of String) Implements IRevenueControlRepository.UnifiedAdmissionListByFolioIds
        If listFoliosIds Is Nothing OrElse listFoliosIds.Count = 0 OrElse String.IsNullOrEmpty(admissionNumber) Then
            Return New List(Of String)()
        End If

        Dim query = From rc In _context.RevenueControl.AsNoTracking()
                    Join rcd In _context.RevenueControlDetail.AsNoTracking() On rc.Id Equals rcd.RevenueControlId
                    Join sodd In _context.ServiceOrderDetailDistribution.AsNoTracking() On rcd.Id Equals sodd.RevenueControlDetailId
                    Join sod In _context.ServiceOrderDetail.AsNoTracking() On sodd.ServiceOrderDetailId Equals sod.Id
                    Join so In _context.ServiceOrder.AsNoTracking() On sod.ServiceOrderId Equals so.Id
                    Where listFoliosIds.Contains(rcd.Id) AndAlso rc.AdmissionNumber = admissionNumber AndAlso rc.AdmissionNumber <> so.AdmissionNumber
                    Select rcAdmission = rc.AdmissionNumber, soAdmission = so.AdmissionNumber

        Dim pairs = query.ToList()

        If pairs.Count = 0 Then
            Return New List(Of String)
        End If

        Dim result As New List(Of String)()
        result.AddRange(pairs.Select(Function(p) p.rcAdmission.Trim()).Distinct())
        result.AddRange(pairs.Select(Function(p) p.soAdmission.Trim()).Distinct())
        Return result.Distinct().ToList()

    End Function

End Class
