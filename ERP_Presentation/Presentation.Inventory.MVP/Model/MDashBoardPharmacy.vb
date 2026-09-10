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
Imports Domain.Crystal.Entities
Imports Presentation.Base
Imports System.ServiceModel
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.InventoryRepository
Imports DevExpress.Xpo
Imports System.Dynamic
Imports Infrastructure.Data.Xpo.CrystalRepository
Imports Infrastructure.Data.Xpo.PayrollRepository
Imports Infrastructure.Data.Xpo.ContractRepository
Imports Infrastructure.Data.Xpo.InventoryRepository.View
Imports INUNIFUNC = Infrastructure.Data.Xpo.CrystalRepository.INUNIFUNC

#End Region

Public Class MDashBoardPharmacy
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

    Public Function GetATCXpo(id As Integer) As ATCXpo
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).InventoryService.GetATCXpo(id)
    End Function

    Public Function GetInventorySuppliedXpo(id As Integer) As InventorySupplieXpo
        Dim filter As String = "Id = " & id
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).InventoryService.GetCollection(Of InventorySupplieXpo)(Nothing, filter).FirstOrDefault()
    End Function

    Public Function GetInventoryProductXpo(id As Integer) As InventoryProductXpo
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).InventoryService.GetInventoryProductXpo(id)
    End Function

    Public Function GetHealthAdministratorXpo(id As Integer) As HealthAdministratorXpo
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).ContractService.GetHealthAdministratorXpo(id)
    End Function

    Public Function GetPatientXpo(code As String) As PatientXpo
        Return XpoServiceEx.Instance(_indigoSessionValues.HisContainer).CrystalService.GetPatientXpo(code)
    End Function

    Public Function GetContractCareGroupXpo(id As Integer) As ContractCareGroupXpo
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).ContractService.GetContractCareGroupXpo(id)
    End Function

    Public Function GetCommonThirdPartyXpo(nit As String) As Infrastructure.Data.Xpo.CommonRepository.CommonThirdPartyXpo
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).CommonService.GetCommonThirdPartyXpo(nit)
    End Function

    Public Function GetFunctionalUnitXpo(code As String) As PayrollFunctionalUnit
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).PayrollService.GetFunctionalUnitXpo(code)
    End Function

    Public Function GetAdmissionXpo(admissionNumber As String) As AdmissionXpo
        Return XpoServiceEx.Instance(_indigoSessionValues.HisContainer).CrystalService.GetAdmissionXpo(admissionNumber)
    End Function

    ''' <summary>
    ''' carga las unidades funcionales
    ''' </summary>
    Public Function LoadCareCenter() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.HisContainer).CrystalService.ListCenters()
    End Function

    ''' <summary>
    ''' Obtiene la secuencia numerica asignada al formulario
    ''' </summary>
    ''' <returns>Secuencia numerica</returns>
    Public Function GetSequense() As Domain.Entities.InventorySequence
        Return IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetSequenseByIdForm(Me._tagForm)
    End Function

    ''' <summary>
    ''' lista las solicitudes
    ''' </summary>
    ''' <param name="codeCareCenter"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListDashBoardPharmacy(codeCareCenter As String) As XPCollection(Of ViewDashBoardPharmacy)
        Return XpoServiceEx.Instance(_indigoSessionValues.HisContainer).CrystalService.ListDashBoardPharmacy(codeCareCenter)
    End Function

    Public Function ListDashBoardPharmacyXPInstant(codeCareCenter As String) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.HisContainer).CrystalService.ListDashBoardPharmacyXPInstant(codeCareCenter)
    End Function

    ''' <summary>
    ''' lista las solicitudes segun filtro de tipo
    ''' </summary>
    ''' <param name="codeCareCenter"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListDashBoardPharmacyByTypeXPInstant(codeCareCenter As String, medicamentos As Nullable(Of Integer), insumos As Nullable(Of Integer), medicamentos_insumos As Nullable(Of Integer)) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.HisContainer).CrystalService.ListDashBoardPharmacyByTypeXPInstant(codeCareCenter, medicamentos, insumos, medicamentos_insumos)
    End Function

    Public Function ListDashBoardPharmacyByPatientXPInstant(PatientCode As String) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.HisContainer).CrystalService.ListDashBoardPharmacyByPatientXPInstant(PatientCode)
    End Function

    Public Function ListDashBoardPharmacyByPatient(PatientCode As String, Optional AdmissionCode As String = Nothing, Optional TipoSolicitud As String = Nothing) As List(Of ViewDashBoardPharmacy)
        Dim Filter As String
        If AdmissionCode IsNot Nothing Then
            If TipoSolicitud IsNot Nothing Then
                Filter = "CodigoPaciente = '" & PatientCode & "'" & "AND Ingreso= '" & AdmissionCode & "'" & "AND TIPOSOLICITUD IN(" & TipoSolicitud & ")"
            Else
                Filter = "CodigoPaciente = '" & PatientCode & "'" & "AND Ingreso= '" & AdmissionCode & "'" & "AND TIPOSOLICITUD <> 2"
            End If
        Else
            If TipoSolicitud IsNot Nothing Then
                Filter = "CodigoPaciente = '" & PatientCode & "'" & "AND TIPOSOLICITUD IN(" & TipoSolicitud & ")"
            Else
                Filter = "CodigoPaciente = '" & PatientCode & "'"
            End If
        End If
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).InventoryService.GetCollection(Of ViewDashBoardPharmacy)(Nothing, Filter, withSort:=False)
    End Function
    ''' <summary>
    ''' lista los pacientes con solicitudes Central Mezclas
    ''' </summary>
    ''' <param name="codeCareCenter"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListDashBoardPharmacyMixingStationPatientXPInstant(codeCareCenter As String) As XPCollection(Of ViewDashBoardPharmacyMixingSationPatient)
        Return XpoServiceEx.Instance(_indigoSessionValues.HisContainer).CrystalService.ListDashBoardPharmacyMixingStationPatientXPInstant(codeCareCenter)
    End Function

    ''' <summary>
    ''' lista  los detalles de la solicitudes de los pacientes Central de Mezclas
    ''' </summary>
    ''' <param name="codeCareCenter"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListDashBoardPharmacyMixingStationXPCollection(codeCareCenter As String) As XPCollection(Of ViewDashBoardPharmacyMixingSation)
        Return XpoServiceEx.Instance(_indigoSessionValues.HisContainer).CrystalService.ListDashBoardPharmacyMixingStationXPCollection(codeCareCenter)
    End Function

    Public Function ListDashBoardPharmacyChemotherapyXPInstant(codeCareCenter As String) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.HisContainer).CrystalService.ListDashBoardPharmacyChemotherapyXPInstant(codeCareCenter)
    End Function

    Public Function ListDashBoardPharmacyChemotherapyByTypeXPInstant(codeCareCenter As String, medicamentos As Nullable(Of Integer), insumos As Nullable(Of Integer), medicamentos_insumos As Nullable(Of Integer)) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.HisContainer).CrystalService.ListDashBoardPharmacyChemotherapyByTypeXPInstant(codeCareCenter, medicamentos, insumos, medicamentos_insumos)
    End Function

    Public Function CitaCanceladaReprogramaXPInstant(cODAUTONU As Long) As ViewCitaCanceladaReprogramada
        Return XpoServiceEx.Instance(_indigoSessionValues.HisContainer).CrystalService.CitaCanceladaReprogramaXPInstant(cODAUTONU)
    End Function

    Public Function ListDashBoardPharmacyXPInstantExtramural(codeCareCenter As String) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.HisContainer).CrystalService.ListDashBoardPharmacyXPInstantExtramural(codeCareCenter)
    End Function

    Public Function ListDashBoardPharmacyByTypeXPInstantExtramural(codeCareCenter As String, medicamentos As Nullable(Of Integer), insumos As Nullable(Of Integer), medicamentos_insumos As Nullable(Of Integer)) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.HisContainer).CrystalService.ListDashBoardPharmacyByTypeXPInstantExtramural(codeCareCenter, medicamentos, insumos, medicamentos_insumos)
    End Function

    Public Function ListDashBoardCentralMixXPInstant(codeCareCenter As String) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.HisContainer).CrystalService.ListDashBoardCentralMixXPInstant(codeCareCenter)
    End Function

    Public Function ListDashBoardPharmacy_SurgicalPackageXPInstant(codeCareCenter As String) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.HisContainer).CrystalService.ListDashBoardPharmacy_SurgicalPackageXPInstant(codeCareCenter)
    End Function

    Public Function ListDashBoardPharmacy_SurgicalPackageByTypeXPInstant(codeCareCenter As String, medicamentos As Nullable(Of Integer), insumos As Nullable(Of Integer), medicamentos_insumos As Nullable(Of Integer)) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.HisContainer).CrystalService.ListDashBoardPharmacy_SurgicalPackageByTypeXPInstant(codeCareCenter, medicamentos, insumos, medicamentos_insumos)
    End Function

    ''' <summary>
    ''' lista las devoluciones
    ''' </summary>
    ''' <param name="codeCareCenter"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListDashBoardPharmacyDevolution(codeCareCenter As String) As XPCollection(Of CrystalRepository.ViewDashBoardPharmacyDevolution)
        Return XpoServiceEx.Instance(_indigoSessionValues.HisContainer).CrystalService.ListDashBoardPharmacyDevolution(codeCareCenter)
    End Function

    Public Function ListDashBoardPharmacyDevolutionXPInstant(codeCareCenter As String) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.HisContainer).CrystalService.ListDashBoardPharmacyDevolutionXPInstant(codeCareCenter)
    End Function
    Public Function ListDashBoardPharmacyDevolutionByTypeXPInstant(codeCareCenter As String, medicamentos As Nullable(Of Integer), insumos As Nullable(Of Integer), medicamentos_insumos As Nullable(Of Integer)) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.HisContainer).CrystalService.ListDashBoardPharmacyDevolutionByTypeXPInstant(codeCareCenter, medicamentos, insumos, medicamentos_insumos)
    End Function

    ''' <summary>
    ''' lista los almacenes
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListWarehouseUser(warehouseCode As String) As XPCollection(Of WarehouseXpo)
        Dim userCode = String.Empty
        If String.IsNullOrEmpty(warehouseCode) Then
            userCode = _indigoSessionValues.UserIndigo
        End If
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).InventoryService.ListCollectionNoTransitWarehouseByStatusAndWarehouseAndUser(True, warehouseCode, userCode)
    End Function

    ''' <summary>
    ''' retora el detalle del dashboard de farmacia
    ''' </summary>
    ''' <param name="consecutive"></param>
    ''' <param name="patienCode"></param>
    ''' <param name="admission"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListDashboardPharmacyDetail(consecutive As Decimal, patienCode As String, admission As String) As List(Of Domain.Crystal.Entities.ViewDashboardPharmacyDetail)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoCrystal.ListDashboardPharmacyDetail(consecutive, patienCode, admission)
    End Function

    Public Function ListDashboardPharmacyDetailSurgicalPackage(consecutive As Decimal, patienCode As String) As List(Of Domain.Crystal.Entities.ViewDashBoardPharmacy_SurgicalPackageDeatils)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoCrystal.ListDashboardPharmacyDetailSurgicalPackage(consecutive, patienCode)
    End Function

    Public Function ListDashBoardPharmacyDetailMixingStationCollection(consecutive As Decimal, patienCode As String, admission As String) As XPCollection(Of Infrastructure.Data.Xpo.CrystalRepository.ViewDashboardPharmacyDetailMixingSation)
        Return XpoServiceEx.Instance(_indigoSessionValues.HisContainer).CrystalService.ListDashBoardPharmacyDetailMixingStation(consecutive, patienCode, admission)
    End Function

    Public Function ListDashBoardPharmacyDetailMixingStationGrid(consecutive As Decimal, admission As String) As XPCollection(Of ViewDashboardPharmacyDetailMixingSationGrid)
        Return XpoServiceEx.Instance(_indigoSessionValues.HisContainer).CrystalService.ListDashBoardPharmacyDetailMixingStationGrid(consecutive, admission)
    End Function

    Public Function ListDashBoardPharmacyDetailCollection(consecutive As Decimal, patienCode As String, admission As String) As XPCollection(Of Infrastructure.Data.Xpo.CrystalRepository.ViewDashboardPharmacyDetail)
        Return XpoServiceEx.Instance(_indigoSessionValues.HisContainer).CrystalService.ListDashBoardPharmacyDetail(consecutive, patienCode, admission)
    End Function

    ''' <summary>
    ''' lista losdetalles segun el tipo de producto que sean por los filtros de la cabecera.
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListDashBoardPharmacyDetailByTypeFilter(consecutive As Decimal, patienCode As String, admission As String, typeFilters As String) As XPCollection(Of Infrastructure.Data.Xpo.CrystalRepository.ViewDashboardPharmacyDetail)
        Return XpoServiceEx.Instance(_indigoSessionValues.HisContainer).CrystalService.ListDashBoardPharmacyDetailByTypeFilter(consecutive, patienCode, admission, typeFilters)
    End Function

    Public Function ListDashBoardPharmacy_SurgicalPackageDetailsCollection(consecutive As Decimal, patienCode As String, admission As String) As XPCollection(Of Infrastructure.Data.Xpo.CrystalRepository.ViewDashBoardPharmacy_SurgicalPackageDeatils)
        Return XpoServiceEx.Instance(_indigoSessionValues.HisContainer).CrystalService.ListDashBoardPharmacy_SurgicalPackageDetails(consecutive, patienCode, admission)
    End Function

    Public Function ListDashBoardPharmacy_SurgicalPackageDetailsByTypeCollection(consecutive As Decimal, patienCode As String, admission As String, typeFilters As String) As XPCollection(Of Infrastructure.Data.Xpo.CrystalRepository.ViewDashBoardPharmacy_SurgicalPackageDeatils)
        Return XpoServiceEx.Instance(_indigoSessionValues.HisContainer).CrystalService.ListDashBoardPharmacy_SurgicalPackageByTypeDetails(consecutive, patienCode, admission, typeFilters)
    End Function

    Public Function ListDashBoardPharmacyDetailDevolutionCollection(consecutive As Decimal, patienCode As String, admission As String) As XPCollection(Of Infrastructure.Data.Xpo.CrystalRepository.ViewDashboardPharmacyDetailDevolution)
        Return XpoServiceEx.Instance(_indigoSessionValues.HisContainer).CrystalService.ListDashBoardPharmacyDetailDevolutionCollection(consecutive, patienCode, admission)
    End Function

    Public Function ListDashBoardPharmacyDetailDevolutionByTypeCollection(consecutive As Decimal, patienCode As String, admission As String, typeFilters As String) As XPCollection(Of Infrastructure.Data.Xpo.CrystalRepository.ViewDashboardPharmacyDetailDevolution)
        Return XpoServiceEx.Instance(_indigoSessionValues.HisContainer).CrystalService.ListDashBoardPharmacyDetailDevolutionByTypeCollection(consecutive, patienCode, admission, typeFilters)
    End Function

    ''' <summary>
    ''' retora el detalle del dashboard de farmacia para devoluciones
    ''' </summary>
    ''' <param name="consecutive"></param>
    ''' <param name="patienCode"></param>
    ''' <param name="admission"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListDashboardPharmacyDetailDevolution(consecutive As Decimal, patienCode As String, admission As String) As List(Of Domain.Crystal.Entities.ViewDashboardPharmacyDetailDevolution)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoCrystal.ListDashboardPharmacyDetailDevolution(consecutive, patienCode, admission)
    End Function

    ''' <summary>
    ''' obtiene un admision por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAdmissionByCode(code As String) As ActionResult(Of ADINGRESO)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoCrystal.GetAdmissionByCode(code)
    End Function

    ''' <summary>
    ''' actuliza la unidad funcional en crystal
    ''' </summary>
    ''' <param name="ConsecutivePharmacy"></param>
    ''' <param name="funcionalUnitCode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdateFunctionalUnitCrystal(ConsecutivePharmacy As Decimal, funcionalUnitCode As String) As ActionResult
        Return IndigoConecta.Instancia.CurrentCloud.IndigoInventory.UpdateFunctionalUnitCrystal(ConsecutivePharmacy, funcionalUnitCode)
    End Function

    ''' <summary>
    ''' metodo para validar la unidad funcional
    ''' </summary>
    ''' <param name="admissionNumber"></param>
    ''' <param name="patientCode"></param>
    ''' <param name="functionalUnitCode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidateFunctionalUnitDashBoard(admissionNumber As String, patientCode As String, functionalUnitCode As String, requestNumber As Integer) As ActionResult(Of String)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoInventory.ValidateFunctionalUnitDashBoard(admissionNumber, patientCode, functionalUnitCode, requestNumber)
    End Function

    ''' <summary>
    ''' lista el inventario fisico por el numero ATC del producto
    ''' </summary>
    ''' <param name="parameters"></param>
    ''' <param name="listCodes"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListPhysicalInventoryByCode(ByVal parameters As Dictionary(Of String, String), ByVal listCodes As List(Of String)) As List(Of PhysicalInventory)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoInventory.ListPhysicalInventoryByCode(parameters, listCodes)
    End Function

    ''' <summary>
    ''' lista el inventario fisico por el numero ATC del producto
    ''' </summary>
    ''' <param name="ATCNumber"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListPhysicalInventoryByATCNumber(ATCNumber As String, type As Integer) As List(Of PhysicalInventory)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoInventory.ListPhysicalInventoryByATCNumber(ATCNumber, type, _indigoSessionValues.UserIndigoId)
    End Function

    ''' <summary>
    ''' lista los inventarios fisicos por el numero ATC del producto con información adicional
    ''' </summary>
    ''' <param name="ATCNumber"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListPhysicalInventoryByATCNumberWithAdditionalInformation(ATCNumber As String, type As Integer, careGroupId As Integer, Optional TotalDose As Decimal? = Decimal.Zero) As List(Of PhysicalInventory)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoInventory.ListPhysicalInventoryByATCNumberWithAdditionalInformation(ATCNumber, type, _indigoSessionValues.UserIndigoId, careGroupId, TotalDose)
    End Function

    ''' <summary>
    ''' funcion para consultar CUM pestaña central de mezclas
    ''' </summary>
    ''' <param name="ATCCode"></param>
    ''' <param name="AdmissionNumber"></param>
    ''' <returns></returns>
    Public Async Function ListPhysicalInventoryByATCCodeToMS(ATCCode As String, AdmissionNumber As String, CodeSusceptibleMixingStation As String) As Task(Of ActionResult(Of List(Of PhysicalInventory)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.ListPhysicalInventoryByATCCodeToMSAsync(ATCCode, AdmissionNumber, CodeSusceptibleMixingStation)
    End Function

    ''' <summary>
    ''' lista el inventario fisico por el numero ATC del producto
    ''' </summary>
    ''' <param name="ATCNumber"></param>
    ''' <param name="type"></param>
    ''' <param name="admissionNumber"></param>
    ''' <returns></returns>
    ''' <remarks>HRR PBI3410</remarks>
    Public Function ListPhysicalInventoryCustodyByATCNumber(ATCNumber As String, type As Integer, admissionNumber As String) As List(Of PhysicalInventoryCustody)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoInventory.ListPhysicalInventoryCustodyByATCNumber(ATCNumber, type, _indigoSessionValues.UserIndigoId, admissionNumber)
    End Function

    ''' <summary>
    ''' metodo para obtener un inventario fisico cuando se hace por codigo de barras
    ''' </summary>
    ''' <param name="productCode"></param>
    ''' <param name="batchCode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPhysicalInventoryBarCode(productCode As String, batchCode As String) As ActionResult(Of List(Of PhysicalInventory))
        Return IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetPhysicalInventoryBarCode(productCode, batchCode, _indigoSessionValues.UserIndigoId)
    End Function

    ''' <summary>
    ''' metodo para obtener un inventario fisico cuando se hace por codigo de barras
    ''' </summary>
    ''' <param name="productCode"></param>
    ''' <param name="batchCode"></param>
    ''' <param name="admissionNumber"></param>
    ''' <returns></returns>
    ''' <remarks>HRR PBI3410</remarks>
    Public Function GetPhysicalInventoryCustodyBarCode(productCode As String, batchCode As String, admissionNumber As String) As ActionResult(Of List(Of PhysicalInventoryCustody))
        Return IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetPhysicalInventoryCustodyBarCode(productCode, batchCode, _indigoSessionValues.UserIndigoId, admissionNumber)
    End Function

    ''' <summary>
    ''' Consulta el inventario fisico de custodia por control de ingreso, almacen y usuario
    ''' </summary>
    ''' <param name="admissionNumber"></param>
    ''' <param name="wareHouseId"></param>
    ''' <returns></returns>
    ''' <remarks>HRR PBI3410</remarks>
    Public Function GetPhysicalInventoryCustodyByWareHouse(admissionNumber As String, wareHouseId As Integer) As List(Of Domain.Entities.PhysicalInventoryCustody)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetPhysicalInventoryCustodyByWareHouse(admissionNumber, wareHouseId, _indigoSessionValues.UserIndigoId)
    End Function

    ''' <summary>
    ''' Consulta el inventario fisico de custodia por control de ingreso, almacen y usuario
    ''' </summary>
    ''' <param name="patientCode"></param>
    ''' <param name="admissionNumber"></param>
    ''' <param name="wareHouseId"></param>
    ''' <returns></returns>
    ''' <remarks>HRR PBI3410</remarks>
    Public Function GetPhysicalInventoryCustodyByAdmissionWareHouse(patientCode As String, admissionNumber As String, wareHouseId As Integer) As List(Of Domain.Entities.PhysicalInventoryCustody)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetPhysicalInventoryCustodyByAdmissionWareHouse(patientCode, admissionNumber, wareHouseId, _indigoSessionValues.UserIndigoId)
    End Function
    ''' <summary>
    ''' lista los productos con cantidades para la devolucion
    ''' </summary>
    ''' <param name="admissionNumber"></param>
    ''' <param name="productCode"></param>
    ''' <param name="productType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListPharmaceuticalDispensingDetailBatchSerial(admissionNumber As String, functionalUnitCode As String, productCode As String, productType As Integer, Optional batchCode As String = "") As List(Of PharmaceuticalDispensingDetailBatchSerial)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoInventory.ListPharmaceuticalDispensingDetailBatchSerialDevolution(admissionNumber, functionalUnitCode, productCode, productType, _indigoSessionValues.UserIndigoId, batchCode)
    End Function

    ''' <summary>
    ''' lista los productos con cantidades para la devolucion por codigo de barras
    ''' </summary>
    ''' <param name="admissionNumber"></param>
    ''' <param name="productCode"></param>
    ''' <param name="batchCode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPharmaceuticalDispensingDetailBatchSerialDevolutionBarCode(admissionNumber As String, productCode As String, batchCode As String) As ActionResult(Of List(Of PharmaceuticalDispensingDetailBatchSerial))
        Return IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetPharmaceuticalDispensingDetailBatchSerialDevolutionBarCode(admissionNumber, productCode, batchCode, _indigoSessionValues.UserIndigoId)
    End Function

    ''' <summary>
    ''' Guarda una dispensación farmacéutica
    ''' </summary>
    ''' <param name="ListPharmaceuticalDispensing">The pharmaceutical dispensing.</param>
    ''' <param name="idSequense">The identifier sequense.</param>
    ''' <returns></returns>
    Public Async Function SaveDashBoardPharmacy(ByVal ListPharmaceuticalDispensing As List(Of PharmaceuticalDispensing), ListDetailAnnular As List(Of Domain.Crystal.Entities.ViewDashboardPharmacyDetail), ByVal idSequense As Int64) As Task(Of ActionResult(Of String))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.SaveDashboardPharmacyAsync(ListPharmaceuticalDispensing, ListDetailAnnular, idSequense, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    Public Async Function GetHighRiskDrugsByAtcCodeAsync(atcCodes As List(Of String)) As Task(Of List(Of WarningHighRiskDrugModel))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetHighRiskDrugsByAtcCodeAsync(atcCodes)
    End Function

    ''' <summary>
    ''' Guarda una dispensación farmacéutica
    ''' </summary>
    ''' <returns></returns>
    Public Async Function SaveDispensingByPatientMedilaser(ListCrystal As List(Of SP_ListHCPRESCRDByCODCONCEC_Result), ListPharmaceuticalDispensing As List(Of PharmaceuticalDispensing), ListDetailAnnular As List(Of Domain.Crystal.Entities.ViewDashboardPharmacyDetail), CareCenterCode As String, WareHouseId As Integer, CareGroupId As Integer, BillingAuthorizationId As Integer, DateItem As DateTime, Number As String, ListDeferred As List(Of Domain.Crystal.Entities.ViewDashboardPharmacyDetailDeferred), IsManual As Boolean) As Task(Of ActionResult(Of SP_SaveDispensingByPatientMedilaser_Result))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.SaveDispensingByPatientMedilaserAsync(ListCrystal, ListPharmaceuticalDispensing, ListDetailAnnular, CareCenterCode, WareHouseId, CareGroupId, BillingAuthorizationId, DateItem, Number, _indigoSessionValues, ListDeferred, IsManual)
    End Function

    Public Async Function SaveDashboardCentralMixAsync(ByVal ListPharmaceuticalDispensing As List(Of PharmaceuticalDispensing), ListDetailAnnular As List(Of Domain.Crystal.Entities.ViewDashboardPharmacyDetail), ByVal idSequense As Int64) As Task(Of ActionResult(Of String))
        '   Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.SaveDashboardCentralMixAsync(ListPharmaceuticalDispensing, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    Public Async Function SaveDashboardPharmacySurgicalPackage(ByVal ListPharmaceuticalDispensing As List(Of PharmaceuticalDispensing), ListDetailAnnular As List(Of Domain.Crystal.Entities.ViewDashBoardPharmacy_SurgicalPackageDeatils), ByVal idSequense As Int64) As Task(Of ActionResult(Of String))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.SaveDashboardPharmacySurgicalPackageAsync(ListPharmaceuticalDispensing, ListDetailAnnular, idSequense, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Devuelve una dispensacion
    ''' </summary>
    ''' <param name="ListPharmaceuticalDispensingDevolution"></param>
    ''' <param name="ListDetailAnnular"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveDashBoardPharmacyDevolution(ByVal ListPharmaceuticalDispensingDevolution As List(Of PharmaceuticalDispensingDevolution), ListDetailAnnular As List(Of Domain.Crystal.Entities.ViewDashboardPharmacyDetailDevolution), ByVal idSequense As Int64) As Task(Of ActionResult(Of String))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.SaveDashboardPharmacyDevolutionAsync(ListPharmaceuticalDispensingDevolution, ListDetailAnnular, idSequense, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' retorna el tipo de la estancia
    ''' </summary>
    ''' <param name="patient"></param>
    ''' <param name="admission"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetStayType(patient As String, admission As String) As String
        Return IndigoConecta.Instancia.CurrentCloud.IndigoCrystal.GetStayType(patient, admission)
    End Function

    ''' <summary>
    ''' retorna un paciente
    ''' </summary>
    ''' <param name="patient"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPatientByIdentification(patient As String) As ActionResult(Of INPACIENT)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoCrystal.GetPatientByIdentification(patient)
    End Function

    Public Function GetCareGroupByCareCenterXpo(careCenterCode As String) As Infrastructure.Data.Xpo.InventoryRepository.CareGroupByCareCenterXpo
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).InventoryService.GetCareGroupByCareCenterXpo(careCenterCode)
    End Function

    Public Async Function PostConfirmIntegration(args As Object, PharmaceuticalDispensing As PharmaceuticalDispensing) As Task(Of ActionResult)
        Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoInventory.InnerChannel)
            Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
            Dim mess As New MessageHeader(Of AuditMessage)(Me._indigoSessionValues.AuditMessageWcf)
            Dim header As System.ServiceModel.Channels.MessageHeader = mess.GetUntypedHeader(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            OperationContext.Current.OutgoingMessageHeaders.Add(header)

            Dim params As String = Utils.SerializeObjectToJson(args)
            Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.PostConfirmIntegrationAsync(params, PharmaceuticalDispensing)
        End Using
    End Function

    ''' <summary>
    ''' Consulta productos en custodia con saldo por paciente y numero de ingreso
    ''' </summary>
    ''' <param name="patientCode">codigo de paciente</param>
    ''' <param name="admissionNumber">numero de ingreso</param>
    ''' <returns></returns>
    ''' <remarks>HRR PBI3410</remarks>
    Public Async Function GetProductCustodyByPatientCodeAdmission(patientCode As String, admissionNumber As String) As Task(Of List(Of SP_ProductCustody_Result))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetProductCustodyByPatientCodeAdmissionAsync(patientCode, admissionNumber)
    End Function

    ''' <summary>
    ''' lista medicamentos LASA
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListViewLASAMedication(listATCs As List(Of String)) As XPCollection(Of ViewLASAMedication)
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).InventoryService.ListViewLASAMedication(listATCs)
    End Function

    ''' <summary>
    ''' funcion para realizar enrutamiento a farmacia
    ''' </summary>
    ''' <param name="ViewDashboardPharmacyDetail"></param>
    ''' <param name="RoutingLog"></param>
    ''' <returns></returns>
    Public Async Function RouteToPharmacy(ViewDashboardPharmacyDetail As List(Of Domain.Crystal.Entities.ViewDashboardPharmacyDetail), RoutingLog As RoutingLog) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCrystal.RouteToPharmacyAsync(ViewDashboardPharmacyDetail, RoutingLog, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene la unidad funcional por el tipo, devuelve la primera que encuentre
    ''' </summary>
    ''' <param name="Type"></param>
    ''' <returns></returns>
    Public Function GetFunctionalUnitByType(Type As String) As INUNIFUNC
        Return XpoServiceEx.Instance(_indigoSessionValues.HisContainer).CrystalService.GetXPOObject(Of INUNIFUNC)($"UFUTIPUNI='{Type}'")
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