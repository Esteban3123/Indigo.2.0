'***********************************************************************
' Assembly         : Application.InteropCost
' Author           : Diego Andrés Roldán Lozano
' Created          : 30-12-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Data.Entity.Infrastructure
Imports System.Data.Entity.Core
Imports Infrastructure.CrossCutting.Resources
Imports Domain.Entities.Service
Imports System.Text
Imports Domain.Payroll
Imports Domain.Crystal.Entities
Imports Domain.Crystal
Imports System.Transactions

Public Class ControlOutPatientServicesAdminService
    Implements IControlOutPatientServicesAdminService

#Region "Fields"
    Private _serviceorderAdminService As IServiceOrderAdminService
    Private _admissionRepository As IAdmissionRepository
    Private _inCONSECURepository As IINCONSECURepository
    Private _billingSequenceAdminService As IBillingSequenseAdminService
    Private _careGroupRepository As ICareGroupRepository
    Private _aGASICITARepository As IAGASICITARepository
    Private _aDCONCOEXrepository As IADCONCOEXrepository
    Private _aMBORDLABRepository As IAMBORDLABRepository
    Private _aMBORDIMARepository As IAMBORDIMARepository
    Private _aMBORDPATRepository As IAMBORDPATRepository
    Private _billingServices As IBillingServices
    Private _healthAdministratorRepository As IHealthAdministratorRepository
    Private _iNCUPSIPSRepository As IINCUPSIPSRepository
    Private _iAGACTMDDDRepository As IAGACTMDDDRepository
    Private _inventoryProductRepository As IInventoryProductRepository
    Private _productGroupRepository As IProductGroupsRepository
    Private _settingInventoryRepository As ISettingInventoryRepository
    Private _functionalUnitRepository As IFunctionalUnitRepository
    Private _productRateDetailRepository As IProductRateDetailRepository
    Private _iNPROFSALRepository As IHealthProfessionalRepository
    Private _hCFARMEPCRepository As IHCFARMEPCRepository
    Private _hCINTESERRepository As IHCINTESERRepository
    Private _hCPARPACSRepository As IHCPARPACSRepository
    Private _serviceOrderRepository As IServiceOrderRepository
    Private _inentidadRepository As ICrystalEntityRepository
    Private _fomagValidator As IValidationFomagAdminService
#End Region

#Region "Methods"

    Public Sub New(ByVal serviceorderAdminService As IServiceOrderAdminService, admissionRepository As IAdmissionRepository,
                   inCONSECURepository As IINCONSECURepository, billingSequenceAdminService As IBillingSequenseAdminService,
                   careGroupRepository As ICareGroupRepository, aGASICITARepository As IAGASICITARepository, aDCONCOEXrepository As IADCONCOEXrepository,
                   aMBORDLABRepository As IAMBORDLABRepository, aMBORDIMARepository As IAMBORDIMARepository, aMBORDPATRepository As IAMBORDPATRepository,
                   billingServices As IBillingServices, healthAdministratorRepository As IHealthAdministratorRepository,
                   iNCUPSIPSRepository As IINCUPSIPSRepository, iAGACTMDDDRepository As IAGACTMDDDRepository,
                   inventoryProductRepository As IInventoryProductRepository, productGroupRepository As IProductGroupsRepository,
                   settingInventoryRepository As ISettingInventoryRepository, functionalUnitRepository As IFunctionalUnitRepository,
                   productRateDetailRepository As IProductRateDetailRepository, iNPROFSALRepository As IHealthProfessionalRepository,
                   hCFARMEPCRepository As IHCFARMEPCRepository, hCINTESERRepository As IHCINTESERRepository, hCPARPACSRepository As IHCPARPACSRepository,
                   serviceOrderRepository As IServiceOrderRepository, inentidadRepository As ICrystalEntityRepository, fomagValidator As IValidationFomagAdminService)
        _serviceorderAdminService = serviceorderAdminService
        _admissionRepository = admissionRepository
        _inCONSECURepository = inCONSECURepository
        _billingSequenceAdminService = billingSequenceAdminService
        _careGroupRepository = careGroupRepository
        _aGASICITARepository = aGASICITARepository
        _aDCONCOEXrepository = aDCONCOEXrepository
        _aMBORDLABRepository = aMBORDLABRepository
        _aMBORDIMARepository = aMBORDIMARepository
        _aMBORDPATRepository = aMBORDPATRepository
        _billingServices = billingServices
        _healthAdministratorRepository = healthAdministratorRepository
        _iNCUPSIPSRepository = iNCUPSIPSRepository
        _iAGACTMDDDRepository = iAGACTMDDDRepository
        _inventoryProductRepository = inventoryProductRepository
        _productGroupRepository = productGroupRepository
        _settingInventoryRepository = settingInventoryRepository
        _functionalUnitRepository = functionalUnitRepository
        _productRateDetailRepository = productRateDetailRepository
        _iNPROFSALRepository = iNPROFSALRepository
        _hCFARMEPCRepository = hCFARMEPCRepository
        _hCINTESERRepository = hCINTESERRepository
        _hCPARPACSRepository = hCPARPACSRepository
        _serviceOrderRepository = serviceOrderRepository
        _inentidadRepository = inentidadRepository
        _fomagValidator = fomagValidator
    End Sub

    Public Function GetProductsByActmedicaAndCups(listActmedicaCups As List(Of ACTMEDCUPS), IPFECNACI As Date) As ActionResult(Of List(Of ProductCita)) Implements IControlOutPatientServicesAdminService.GetProductsByActmedicaAndCups
        Try
            Dim ProductoCita As List(Of ProductCita) = Nothing
            Dim prodActMed As List(Of AGACTMDDD) = _iAGACTMDDDRepository.GetAGACTMDDDByCODACTMEDListAndCupsCodeList(listActmedicaCups, IPFECNACI)
            If prodActMed IsNot Nothing AndAlso prodActMed.Count > 0 Then

                Dim products As List(Of InventoryProduct) = _inventoryProductRepository.GetInventoryProductByCodeList(prodActMed.Select(Function(x) x.CODPRODUC.Trim()).Distinct().ToList())
                If products IsNot Nothing AndAlso products.Count > 0 Then
                    ProductoCita = New List(Of ProductCita)()
                    For Each p In products
                        Dim pCita As New ProductCita()
                        Dim pActmed As AGACTMDDD = prodActMed.Where(Function(x) x.CODPRODUC.Trim().Equals(p.Code)).FirstOrDefault()
                        With pCita
                            .Product = p
                            .CANTRECUR = pActmed.CANTRECUR
                            .MLPRODUCT = pActmed.MLPRODUCT
                            .KGPRODUCT = pActmed.KGPRODUCT
                            .EDADESDE = pActmed.EDADESDE
                            .EDAHASTA = pActmed.EDAHASTA
                            .REACALAUT = pActmed.REACALAUT
                        End With
                        ProductoCita.Add(pCita)
                    Next
                    Return New ActionResult(Of List(Of ProductCita)) With {.StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = ProductoCita}
                Else
                    Return New ActionResult(Of List(Of ProductCita)) With {.StatusCode = eStatusResult.WARNING, .Message = "No se encontraron productos"}
                End If
            Else
                Return New ActionResult(Of List(Of ProductCita)) With {.StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = Nothing}
            End If
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of ProductCita)) With {.StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    Public Function GenerateServiceOrderProducts(params As String, audit As AuditMessage) As ActionResult(Of String) Implements IControlOutPatientServicesAdminService.GenerateServiceOrderProducts
        Try
            Dim args As Object = Utils.DeserializeJsonToObject(params)

            Dim Products As List(Of Object) = DirectCast(args.Products, List(Of Object))
            Dim serverDate = Date.Now


            If args.OptionMedios = 1 Then

                Dim iNPROFSAL As INPROFSAL = _iNPROFSALRepository.GetHealthProfessionalByCode("999")
                If iNPROFSAL Is Nothing OrElse String.IsNullOrEmpty(iNPROFSAL.CODPROSAL) Then
                    Return New ActionResult(Of String) With {.StatusCode = eStatusResult.WARNING, .Message = "El profesional 999 debe estar creado"}
                End If

                Dim unitOfWork As IUnitWork = _hCFARMEPCRepository.UnitWork

                Dim hCFARMEPC As New Domain.Crystal.Entities.HCFARMEPC()
                With hCFARMEPC
                    .IDETIPHIS = Nothing
                    .NUMEFOLIO = Nothing
                    .FECHAORDE = serverDate
                    .CODPROSAL = iNPROFSAL.CODPROSAL
                    .IPCODPACI = args.PatientCode.ToString()
                    .NUMINGRES = args.AdmissionNumber.ToString()
                    .CODCENATE = args.CodCentroAtencion.ToString()
                    .UFUCODIGO = args.CodUnifunc.ToString()
                    .CODCONCEP = Nothing
                    .CODCONCES = Nothing
                    .ORDESTADO = 1
                    .CODBODEGA = args.CODBODEGA 'Consultar en el servidor por unidad funcional select top 1 CODBODEGA,CODCENCOS from HCUNITHIS where UFUCODIGO = '001' Para sacar CODBODEGA, CODCENCOS
                    .CODCENCOS = args.CODCENCOS
                    .ORDTRANUE = 1
                    .JUSANULAC = Nothing
                End With

                For Each product As Object In Products
                    Dim hCFARMEPD As New Domain.Crystal.Entities.HCFARMEPD()
                    With hCFARMEPD
                        .CODCONCEC = hCFARMEPC.CODCONCEC
                        .CODPRODUC = product.ProductCode.ToString()

                        .NUMINGRES = hCFARMEPC.NUMINGRES
                        .CODPROSAL = hCFARMEPC.CODPROSAL
                        .IPCODPACI = hCFARMEPC.IPCODPACI
                        .CODCENATE = hCFARMEPC.CODCENATE
                        .UFUCODIGO = hCFARMEPC.UFUCODIGO

                        .CANPEDPRO = CInt(product.Quantity)
                        .CANENTPRO = 0
                        .CANPENPRO = .CANPEDPRO
                        .PROESTADO = 1
                        .TIPOREGIS = product.TipoRegist.ToString()
                    End With
                    hCFARMEPC.HCFARMEPD.Add(hCFARMEPD)
                Next

                Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                    _hCFARMEPCRepository.SaveEntity(hCFARMEPC)
                    unitOfWork.Commit()
                    scope.Complete()
                    Return New ActionResult(Of String) With {.StatusCode = eStatusResult.SUCCESS, .Message = "Se generó una solicitud de Dispensación"}
                End Using

            ElseIf args.OptionMedios = 2 Then
                Dim errors As New StringBuilder()
                Dim settingInventory As Domain.Entities.SettingInventory = _settingInventoryRepository.GetSettingInventory(CInt(args.OperatingUnitId))
                If settingInventory Is Nothing OrElse settingInventory.Id = 0 Then
                    Return New ActionResult(Of String) With {.StatusCode = eStatusResult.WARNING, .Message = "No se encontraron parámetros de Inventario para la unidad operativa seleccionada"}
                End If

                Dim so As New ServiceOrder()
                With so
                    .PatientCode = args.PatientCode.ToString().Trim()
                    .AdmissionNumber = args.AdmissionNumber.ToString().Trim()
                    .OrderDate = serverDate
                    .EntityName = "ControlServiciosAmbulatorios"
                    .OperatingUnitId = CInt(args.OperatingUnitId)
                    .Status = 1
                End With

                For Each product As Object In Products
                    If Not CType(product, IDictionary(Of String, Object)).ContainsKey("ProductGroupId") OrElse CInt(product.ProductGroupId) = 0 Then
                        errors.AppendLine("El producto " + product.ProductCode + " - " + product.ProductName + " no tiene un grupo asociado")
                        Continue For
                    End If

                    'Obtenemos el valor del producto
                    Dim productRateDetail As ProductRateDetail = _productRateDetailRepository.GetProductRateDetailByCareGroupIdProductIdServiceDate(CInt(args.CareGroupId), CInt(product.ProductId), serverDate)
                    If productRateDetail Is Nothing OrElse productRateDetail.Id = 0 Then
                        Dim caregroup As CareGroup = _careGroupRepository.GetCareGroupById(CInt(args.CareGroupId))
                        errors.AppendLine("No se encontró tarifa para el producto " + product.ProductCode + " - " + product.ProductName + " con grupo de atención (" + String.Concat(caregroup.Code, " - ", caregroup.Name) + ") y fecha " + serverDate.ToShortDateString())
                        Continue For
                    End If

                    Dim productGroup As ProductGroup = _productGroupRepository.GetProductGroupByProductId(CInt(product.ProductId))
                    Dim functionalUnit As Domain.Payroll.Entities.FunctionalUnit = _functionalUnitRepository.GetFunctionalUnitById(CInt(args.FunctionalUnitId))

                    Dim sod As New ServiceOrderDetail()
                    With sod
                        .CareGroupId = args.CareGroupId
                        .HealthAdministratorId = CInt(args.HealthAdministratorId)
                        .ThirdPartyId = CInt(args.ThirdPartyId)
                        .RecordType = 2
                        .CUPSAssociateService = False
                        .LiquidationType = 5 'Standard
                        .ProductId = CInt(product.ProductId)
                        .InvoicedQuantity = CInt(product.Quantity)
                        .SupplyQuantity = .InvoicedQuantity
                        .DevolutionQuantity = 0
                        .RateManualSalePrice = CDec(productRateDetail.SalesValue)
                        .CostValue = CDec(product.ProductCost)
                        .ServiceDate = serverDate
                        .AuthorizationNumber = ""
                        .PerformsFunctionalUnitId = CInt(args.FunctionalUnitId)
                        .SettlementType = 1 'Manula de tarifas
                        .RecoveryRatio = 0D 'Descuento 0
                        .SubTotalSalesPrice = CDec(productRateDetail.SalesValue)
                        .ThirdPartyDiscount = 0D
                        .ThirdPartyDiscountPercentage = 0D
                        .TotalSalesPrice = CDec(productRateDetail.SalesValue)
                        .GrandTotalSalesPrice = CDec(productRateDetail.SalesValue * .InvoicedQuantity)
                        .SurchargeApply = False
                        .SurgeryNumber = 0
                        .IsFirstEvent = True
                        .IncomeMainAccountId = productGroup.IncomeAccountId

                        If settingInventory.AssociateCostCenter = 2 Then
                            'Grupo de producto
                            .CostCenterId = productGroup.CostCenterId
                        Else
                            'Unidad funcional por defecto
                            .CostCenterId = functionalUnit.CostCenterId
                        End If
                    End With
                    so.ServiceOrderDetail.Add(sod)
                Next
                If errors.Length > 0 Then
                    Return New ActionResult(Of String) With {.StatusCode = eStatusResult.WARNING, .Message = errors.ToString()}
                End If
                'Consultamos el id de la secuencia
                Dim _idCurrentSequence As Integer = 0 ' CType(resSequence.ObjectEmbbeded, Integer)
                Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                    Dim result As ActionResult(Of ServiceOrder) = _serviceorderAdminService.SaveServiceOrder(so, audit, _idCurrentSequence)
                    If result.StateResult Then
                        scope.Complete()
                        Return New ActionResult(Of String) With {.StatusCode = eStatusResult.SUCCESS, .Message = "Se generó una órden de servicio con código " + result.ObjectEmbbeded.Code}
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of String) With {.StatusCode = eStatusResult.WARNING, .Message = result.Message}
                    End If
                End Using
            Else
                Return New ActionResult(Of String) With {.StatusCode = eStatusResult.WARNING, .Message = ""}
            End If
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of String) With {.StatusCode = eStatusResult.EXCEPTION, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    Private Function GetBillingSequenceByTagForm(tag As String, Optional idOperativeUnitId As Integer = 0) As ActionResult(Of String)
        Try
            'Consultar el id de la secuencia
            Dim _idCurrentSequence As Integer = 0
            Dim _sequence As BillingSequence = _billingSequenceAdminService.GetSequenseByIdForm(tag)
            If _sequence IsNot Nothing AndAlso _sequence.Id > 0 Then
                If _sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                    _idCurrentSequence = _sequence.BillingSequenceDetail(0).Id
                ElseIf _sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                    Dim res = (From ou As BillingSequenceDetail In _sequence.BillingSequenceDetail Where ou.OperatingUnit.Id = idOperativeUnitId Select ou).ToList()
                    If res IsNot Nothing AndAlso res.Count > 0 Then
                        _idCurrentSequence = res(0).Id
                    End If
                End If
                Return New ActionResult(Of String) With {.StateResult = True, .ObjectEmbbeded = _idCurrentSequence}
            Else
                Throw New Exception()
            End If
        Catch ex As Exception
            Return New ActionResult(Of String) With {.StateResult = False, .Message = String.Format("No se encontró secuencia numérica para el Tag {0} de Facturación", tag)}
        End Try
    End Function

    Public Function GenerateDocuments(controlOutPatientServices As ControlOutPatientServices, companyNit As String, audit As AuditMessage) As ActionResult(Of String) Implements IControlOutPatientServicesAdminService.GenerateDocuments
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() _
                                                With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})

                If controlOutPatientServices.Ingreso Is Nothing Then
                    Throw New Exception("No hay datos de ingreso")
                End If

                'Limpiar el CompanyNit antes de ser usado
                If String.IsNullOrWhiteSpace(companyNit) Then
                    companyNit = ""
                Else
                    companyNit = companyNit.Trim()
                End If

                'Validación FOMAG para el cliente JERSALUD
                Dim fomagValidation = _fomagValidator.ValidatePatientAsync(controlOutPatientServices.Ingreso.IPCODPACI, companyNit).GetAwaiter().GetResult()
                If Not fomagValidation.StateResult Then Return New ActionResult(Of String) With {.StateResult = False, .Message = fomagValidation.Message}

                Dim xmlDocuments As New StringBuilder()
                xmlDocuments.AppendLine("<ControlOutPatientServices>")
                xmlDocuments.AppendLine($"<UnitTypeFunctionalUnit>{controlOutPatientServices.UnitTypeFunctionalUnit}</UnitTypeFunctionalUnit>")
                xmlDocuments.AppendLine($"<OperatingUnitId>{controlOutPatientServices.OperatingUnitId}</OperatingUnitId>")
                xmlDocuments.AppendLine($"<CareGroupId>{CInt(controlOutPatientServices.CareGroupId)}</CareGroupId>")
                xmlDocuments.AppendLine($"<IngresoExistente>{controlOutPatientServices.IngresoExistente}</IngresoExistente>")
                'Datos del Ingreso

                xmlDocuments.AppendLine("<IngresoOrdenServicio>")
                xmlDocuments.AppendLine($"<NUMINGRES>{controlOutPatientServices.Ingreso.NUMINGRES}</NUMINGRES>")
                xmlDocuments.AppendLine($"<IPCODPACI>{controlOutPatientServices.Ingreso.IPCODPACI}</IPCODPACI>")
                xmlDocuments.AppendLine($"<TIPOINGRE>{controlOutPatientServices.Ingreso.TIPOINGRE}</TIPOINGRE>")
                xmlDocuments.AppendLine($"<IINGREPOR>{controlOutPatientServices.Ingreso.IINGREPOR}</IINGREPOR>")
                xmlDocuments.AppendLine($"<CODENTIDA>{controlOutPatientServices.Ingreso.CODENTIDA}</CODENTIDA>")
                xmlDocuments.AppendLine($"<ITIPORIES>{controlOutPatientServices.Ingreso.ITIPORIES}</ITIPORIES>")
                xmlDocuments.AppendLine($"<ICAUSAING>{controlOutPatientServices.Ingreso.ICAUSAING}</ICAUSAING>")
                xmlDocuments.AppendLine($"<IFECHAING>{controlOutPatientServices.Ingreso.IFECHAING.ToString("dd/MM/yyyy HH:mm:ss")}</IFECHAING>")
                xmlDocuments.AppendLine($"<ILIQUIDAC>{controlOutPatientServices.Ingreso.ILIQUIDAC}</ILIQUIDAC>")
                xmlDocuments.AppendLine($"<ICONTROLI>{controlOutPatientServices.Ingreso.ICONTROLI}</ICONTROLI>")
                xmlDocuments.AppendLine($"<CODCENATE>{controlOutPatientServices.Ingreso.CODCENATE}</CODCENATE>")
                xmlDocuments.AppendLine($"<UFUCODIGO>{controlOutPatientServices.Ingreso.UFUCODIGO}</UFUCODIGO>")
                If Not String.IsNullOrEmpty(controlOutPatientServices.Ingreso.IAUTORIZA) Then
                    xmlDocuments.AppendLine($"<IAUTORIZA>{controlOutPatientServices.Ingreso.IAUTORIZA}</IAUTORIZA>")
                End If
                xmlDocuments.AppendLine($"<IESTADOIN>{controlOutPatientServices.Ingreso.IESTADOIN}</IESTADOIN>")
                If Not String.IsNullOrEmpty(controlOutPatientServices.Ingreso.IINGRESOA) Then
                    xmlDocuments.AppendLine($"<IINGRESOA>{controlOutPatientServices.Ingreso.IINGRESOA}</IINGRESOA>")
                End If
                If controlOutPatientServices.Ingreso.ISOATVALO IsNot Nothing Then
                    xmlDocuments.AppendLine($"<ISOATVALO>{controlOutPatientServices.Ingreso.ISOATVALO}</ISOATVALO>")
                End If
                If Not String.IsNullOrEmpty(controlOutPatientServices.Ingreso.ISALCODIG) Then
                    xmlDocuments.AppendLine($"<ISALCODIG>{controlOutPatientServices.Ingreso.ISALCODIG}</ISALCODIG>")
                End If
                If Not String.IsNullOrEmpty(controlOutPatientServices.Ingreso.INUMERORE) Then
                    xmlDocuments.AppendLine($"<INUMERORE>{controlOutPatientServices.Ingreso.INUMERORE}</INUMERORE>")
                End If
                If controlOutPatientServices.Ingreso.IFECHAREM IsNot Nothing Then
                    xmlDocuments.AppendLine($"<IFECHAREM>{controlOutPatientServices.Ingreso.IFECHAREM.Value.ToString("dd/MM/yyyy HH:mm:ss")}</IFECHAREM>")
                End If
                If Not String.IsNullOrEmpty(controlOutPatientServices.Ingreso.IAUTORREM) Then
                    xmlDocuments.AppendLine($"<IAUTORREM>{controlOutPatientServices.Ingreso.IAUTORREM}</IAUTORREM>")
                End If
                If Not String.IsNullOrEmpty(controlOutPatientServices.Ingreso.DEPMUNCOD) Then
                    xmlDocuments.AppendLine($"<DEPMUNCOD>{controlOutPatientServices.Ingreso.DEPMUNCOD}</DEPMUNCOD>")
                End If
                If Not String.IsNullOrEmpty(controlOutPatientServices.Ingreso.AIPSREMIS) Then
                    xmlDocuments.AppendLine($"<AIPSREMIS>{controlOutPatientServices.Ingreso.AIPSREMIS}</AIPSREMIS>")
                End If
                If controlOutPatientServices.Ingreso.IDUBICACION IsNot Nothing Then
                    xmlDocuments.AppendLine($"<IDUBICACION>{controlOutPatientServices.Ingreso.IDUBICACION}</IDUBICACION>")
                End If
                If Not String.IsNullOrEmpty(controlOutPatientServices.Ingreso.IOBSERVAC) Then
                    xmlDocuments.AppendLine($"<IOBSERVAC>{controlOutPatientServices.Ingreso.IOBSERVAC}</IOBSERVAC>")
                End If
                If Not String.IsNullOrEmpty(controlOutPatientServices.Ingreso.IJUSTIFIC) Then
                    xmlDocuments.AppendLine($"<IJUSTIFIC>{controlOutPatientServices.Ingreso.IJUSTIFIC}</IJUSTIFIC>")
                End If
                xmlDocuments.AppendLine($"<IREINGRES>{controlOutPatientServices.Ingreso.IREINGRES}</IREINGRES>")
                If Not String.IsNullOrEmpty(controlOutPatientServices.Ingreso.UFUINGMED) Then
                    xmlDocuments.AppendLine($"<UFUINGMED>{controlOutPatientServices.Ingreso.UFUINGMED}</UFUINGMED>")
                End If
                If Not String.IsNullOrEmpty(controlOutPatientServices.Ingreso.CODPROING) Then
                    xmlDocuments.AppendLine($"<CODPROING>{controlOutPatientServices.Ingreso.CODPROING}</CODPROING>")
                End If
                If Not String.IsNullOrEmpty(controlOutPatientServices.Ingreso.UFUEGRMED) Then
                    xmlDocuments.AppendLine($"<UFUEGRMED>{controlOutPatientServices.Ingreso.UFUEGRMED}</UFUEGRMED>")
                End If
                If Not String.IsNullOrEmpty(controlOutPatientServices.Ingreso.CODPROEGR) Then
                    xmlDocuments.AppendLine($"<CODPROEGR>{controlOutPatientServices.Ingreso.CODPROEGR}</CODPROEGR>")
                End If
                If Not String.IsNullOrEmpty(controlOutPatientServices.Ingreso.UFUINGHOS) Then
                    xmlDocuments.AppendLine($"<UFUINGHOS>{controlOutPatientServices.Ingreso.UFUINGHOS}</UFUINGHOS>")
                End If
                If Not String.IsNullOrEmpty(controlOutPatientServices.Ingreso.UFUEGRHOS) Then
                    xmlDocuments.AppendLine($"<UFUEGRHOS>{controlOutPatientServices.Ingreso.UFUEGRHOS}</UFUEGRHOS>")
                End If
                If Not String.IsNullOrEmpty(controlOutPatientServices.Ingreso.CODESPTRA) Then
                    xmlDocuments.AppendLine($"<CODESPTRA>{controlOutPatientServices.Ingreso.CODESPTRA}</CODESPTRA>")
                End If
                If Not String.IsNullOrEmpty(controlOutPatientServices.Ingreso.TIPOPROFE) Then
                    xmlDocuments.AppendLine($"<TIPOPROFE>{controlOutPatientServices.Ingreso.TIPOPROFE}</TIPOPROFE>")
                End If
                If Not String.IsNullOrEmpty(controlOutPatientServices.Ingreso.UFUAACTMED) Then
                    xmlDocuments.AppendLine($"<UFUAACTMED>{controlOutPatientServices.Ingreso.UFUAACTMED}</UFUAACTMED>")
                End If
                If Not String.IsNullOrEmpty(controlOutPatientServices.Ingreso.UFUAACTHOS) Then
                    xmlDocuments.AppendLine($"<UFUAACTHOS>{controlOutPatientServices.Ingreso.UFUAACTHOS}</UFUAACTHOS>")
                End If
                If controlOutPatientServices.Ingreso.CODCAMACT IsNot Nothing Then
                    xmlDocuments.AppendLine($"<CODCAMACT>{controlOutPatientServices.Ingreso.CODCAMACT}</CODCAMACT>")
                End If
                If Not String.IsNullOrEmpty(controlOutPatientServices.Ingreso.CODDIAING) Then
                    xmlDocuments.AppendLine($"<CODDIAING>{controlOutPatientServices.Ingreso.CODDIAING}</CODDIAING>")
                End If
                If Not String.IsNullOrEmpty(controlOutPatientServices.Ingreso.CODDIAEGR) Then
                    xmlDocuments.AppendLine($"<CODDIAEGR>{controlOutPatientServices.Ingreso.CODDIAEGR}</CODDIAEGR>")
                End If
                If Not String.IsNullOrEmpty(controlOutPatientServices.Ingreso.UFUACTPAC) Then
                    xmlDocuments.AppendLine($"<UFUACTPAC>{controlOutPatientServices.Ingreso.UFUACTPAC}</UFUACTPAC>")
                End If
                If Not String.IsNullOrEmpty(controlOutPatientServices.Ingreso.CODUSUCRE) Then
                    xmlDocuments.AppendLine($"<CODUSUCRE>{controlOutPatientServices.Ingreso.CODUSUCRE}</CODUSUCRE>")
                End If
                If controlOutPatientServices.Ingreso.FECREGCRE IsNot Nothing Then
                    xmlDocuments.AppendLine($"<FECREGCRE>{controlOutPatientServices.Ingreso.FECREGCRE.Value.ToString("dd/MM/yyyy HH:mm:ss")}</FECREGCRE>")
                End If
                If Not String.IsNullOrEmpty(controlOutPatientServices.Ingreso.CODUSUMOD) Then
                    xmlDocuments.AppendLine($"<CODUSUMOD>{controlOutPatientServices.Ingreso.CODUSUMOD}</CODUSUMOD>")
                End If
                If controlOutPatientServices.Ingreso.FECREGMOD IsNot Nothing Then
                    xmlDocuments.AppendLine($"<FECREGMOD>{controlOutPatientServices.Ingreso.FECREGMOD.Value.ToString("dd/MM/yyyy HH:mm:ss")}</FECREGMOD>")
                End If
                If Not String.IsNullOrEmpty(controlOutPatientServices.Ingreso.CODUSUANU) Then
                    xmlDocuments.AppendLine($"<CODUSUANU>{controlOutPatientServices.Ingreso.CODUSUANU}</CODUSUANU>")
                End If
                If controlOutPatientServices.Ingreso.FECREGANU IsNot Nothing Then
                    xmlDocuments.AppendLine($"<FECREGANU>{controlOutPatientServices.Ingreso.FECREGANU.Value.ToString("dd/MM/yyyy HH:mm:ss")}</FECREGANU>")
                End If
                If Not String.IsNullOrEmpty(controlOutPatientServices.Ingreso.NUMINGREI) Then
                    xmlDocuments.AppendLine($"<NUMINGREI>{controlOutPatientServices.Ingreso.NUMINGREI}</NUMINGREI>")
                End If
                If Not String.IsNullOrEmpty(controlOutPatientServices.Ingreso.CODICAMHO) Then
                    xmlDocuments.AppendLine($"<CODICAMHO>{controlOutPatientServices.Ingreso.CODICAMHO}</CODICAMHO>")
                End If
                If controlOutPatientServices.Ingreso.FECHOSPIT IsNot Nothing Then
                    xmlDocuments.AppendLine($"<FECHOSPIT>{controlOutPatientServices.Ingreso.FECHOSPIT.Value.ToString("dd/MM/yyyy HH:mm:ss")}</FECHOSPIT>")
                End If
                xmlDocuments.AppendLine($"<INDAUDFOR>{controlOutPatientServices.Ingreso.INDAUDFOR}</INDAUDFOR>")
                If Not String.IsNullOrEmpty(controlOutPatientServices.Ingreso.IPRNOMBRE) Then
                    xmlDocuments.AppendLine($"<IPRNOMBRE>{controlOutPatientServices.Ingreso.IPRNOMBRE}</IPRNOMBRE>")
                End If
                If Not String.IsNullOrEmpty(controlOutPatientServices.Ingreso.IPCODACTR) Then
                    xmlDocuments.AppendLine($"<IPCODACTR>{controlOutPatientServices.Ingreso.IPCODACTR}</IPCODACTR>")
                End If
                If Not String.IsNullOrEmpty(controlOutPatientServices.Ingreso.IPEXPEDIC) Then
                    xmlDocuments.AppendLine($"<IPEXPEDIC>{controlOutPatientServices.Ingreso.IPEXPEDIC}</IPEXPEDIC>")
                End If
                If controlOutPatientServices.Ingreso.FECACTRAN IsNot Nothing Then
                    xmlDocuments.AppendLine($"<FECACTRAN>{controlOutPatientServices.Ingreso.FECACTRAN.Value.ToString("dd/MM/yyyy HH:mm:ss")}</FECACTRAN>")
                End If
                If Not String.IsNullOrEmpty(controlOutPatientServices.Ingreso.HORACIDEN) Then
                    xmlDocuments.AppendLine($"<HORACIDEN>{controlOutPatientServices.Ingreso.HORACIDEN}</HORACIDEN>")
                End If
                If Not String.IsNullOrEmpty(controlOutPatientServices.Ingreso.IPTELEFON) Then
                    xmlDocuments.AppendLine($"<IPTELEFON>{controlOutPatientServices.Ingreso.IPTELEFON}</IPTELEFON>")
                End If
                If Not String.IsNullOrEmpty(controlOutPatientServices.Ingreso.OBSERACIT) Then
                    xmlDocuments.AppendLine($"<OBSERACIT>{controlOutPatientServices.Ingreso.OBSERACIT}</OBSERACIT>")
                End If
                If Not String.IsNullOrEmpty(controlOutPatientServices.Ingreso.OBSERAREM) Then
                    xmlDocuments.AppendLine($"<OBSERAREM>{controlOutPatientServices.Ingreso.OBSERAREM}</OBSERAREM>")
                End If
                If controlOutPatientServices.Ingreso.INGRECEXT IsNot Nothing Then
                    xmlDocuments.AppendLine($"<INGRECEXT>{controlOutPatientServices.Ingreso.INGRECEXT}</INGRECEXT>")
                End If
                If controlOutPatientServices.Ingreso.PACATENDI IsNot Nothing Then
                    xmlDocuments.AppendLine($"<PACATENDI>{controlOutPatientServices.Ingreso.PACATENDI}</PACATENDI>")
                End If
                If Not String.IsNullOrEmpty(controlOutPatientServices.Ingreso.ESCADOWNT) Then
                    xmlDocuments.AppendLine($"<ESCADOWNT>{controlOutPatientServices.Ingreso.ESCADOWNT}</ESCADOWNT>")
                End If
                If Not String.IsNullOrEmpty(controlOutPatientServices.Ingreso.ESCABIERI) Then
                    xmlDocuments.AppendLine($"<ESCABIERI>{controlOutPatientServices.Ingreso.ESCABIERI}</ESCABIERI>")
                End If
                If Not String.IsNullOrEmpty(controlOutPatientServices.Ingreso.ESCARASS) Then
                    xmlDocuments.AppendLine($"<ESCARASS>{controlOutPatientServices.Ingreso.ESCARASS}</ESCARASS>")
                End If
                If Not String.IsNullOrEmpty(controlOutPatientServices.Ingreso.ESCNORPAC) Then
                    xmlDocuments.AppendLine($"<ESCNORPAC>{controlOutPatientServices.Ingreso.ESCNORPAC}</ESCNORPAC>")
                End If
                If controlOutPatientServices.Ingreso.SERSUSCEP IsNot Nothing Then
                    xmlDocuments.AppendLine($"<SERSUSCEP>{controlOutPatientServices.Ingreso.SERSUSCEP}</SERSUSCEP>")
                End If
                If Not String.IsNullOrEmpty(controlOutPatientServices.Ingreso.ESCVASPAC) Then
                    xmlDocuments.AppendLine($"<ESCVASPAC>{controlOutPatientServices.Ingreso.ESCVASPAC}</ESCVASPAC>")
                End If
                If Not String.IsNullOrEmpty(controlOutPatientServices.Ingreso.ESCAPAPAC) Then
                    xmlDocuments.AppendLine($"<ESCAPAPAC>{controlOutPatientServices.Ingreso.ESCAPAPAC}</ESCAPAPAC>")
                End If
                If controlOutPatientServices.Ingreso.VIVESOLO IsNot Nothing Then
                    xmlDocuments.AppendLine($"<VIVESOLO>{controlOutPatientServices.Ingreso.VIVESOLO}</VIVESOLO>")
                End If
                If controlOutPatientServices.Ingreso.GENCAREGROUP IsNot Nothing Then
                    xmlDocuments.AppendLine($"<GENCAREGROUP>{controlOutPatientServices.Ingreso.GENCAREGROUP}</GENCAREGROUP>")
                End If
                If controlOutPatientServices.Ingreso.GENULTLIQUI IsNot Nothing Then
                    xmlDocuments.AppendLine($"<GENULTLIQUI>{controlOutPatientServices.Ingreso.GENULTLIQUI}</GENULTLIQUI>")
                End If
                If controlOutPatientServices.Ingreso.GENCONENTITY IsNot Nothing Then
                    xmlDocuments.AppendLine($"<GENCONENTITY>{controlOutPatientServices.Ingreso.GENCONENTITY}</GENCONENTITY>")
                End If
                If Not String.IsNullOrEmpty(controlOutPatientServices.Ingreso.NUMTRIAGEI) Then
                    xmlDocuments.AppendLine($"<NUMTRIAGEI>{controlOutPatientServices.Ingreso.NUMTRIAGEI}</NUMTRIAGEI>")
                End If
                xmlDocuments.AppendLine($"<SOLRESHEMO>{IIf(controlOutPatientServices.Ingreso.SOLRESHEMO, 1, 0)}</SOLRESHEMO>")
                If controlOutPatientServices.Ingreso.TRATAESPECIA <> Nothing AndAlso controlOutPatientServices.Ingreso.TRATAESPECIA > 0 Then
                    xmlDocuments.AppendLine($"<TRATAESPECIA>{controlOutPatientServices.Ingreso.TRATAESPECIA}</TRATAESPECIA>")
                End If

                If controlOutPatientServices?.Ingreso?.IdAdmissionType IsNot Nothing Then
                    xmlDocuments.AppendLine($"<IdAdmissionType>{controlOutPatientServices.Ingreso.IdAdmissionType}</IdAdmissionType>")
                End If

                If controlOutPatientServices?.Ingreso?.IdEntryRoutesHealthServices IsNot Nothing Then
                    xmlDocuments.AppendLine($"<IdEntryRoutesHealthServices>{controlOutPatientServices.Ingreso.IdEntryRoutesHealthServices}</IdEntryRoutesHealthServices>")
                End If


                If controlOutPatientServices?.Ingreso?.IdHealthPurposes IsNot Nothing Then
                    xmlDocuments.AppendLine($"<IdHealthPurposes>{controlOutPatientServices.Ingreso.IdHealthPurposes}</IdHealthPurposes>")
                End If


                If controlOutPatientServices?.Ingreso?.IdAdmissionModalities IsNot Nothing Then
                    xmlDocuments.AppendLine($"<IdAdmissionModalities>{controlOutPatientServices.Ingreso.IdAdmissionModalities}</IdAdmissionModalities>")
                End If

                xmlDocuments.AppendLine("</IngresoOrdenServicio>")

                'Citas
                If controlOutPatientServices.ListCitasMedicas IsNot Nothing AndAlso controlOutPatientServices.ListCitasMedicas.Any() Then

                    controlOutPatientServices.ListCitasMedicas.
                        ForEach(Sub(item)
                                    xmlDocuments.AppendLine("<CitaMedica>")
                                    xmlDocuments.AppendLine($"<OrigenCirugia>{item.OrigenCirugia}</OrigenCirugia>")
                                    xmlDocuments.AppendLine($"<Codigo>{item.Codigo}</Codigo>")
                                    xmlDocuments.AppendLine($"<CodeRelated>{item.CodeRelated}</CodeRelated>")
                                    xmlDocuments.AppendLine($"<FechaCita>{item.FechaCita.ToString("dd/MM/yyyy HH:mm:ss")}</FechaCita>")
                                    xmlDocuments.AppendLine($"<CodigoProfesional>{item.CodigoProfesional}</CodigoProfesional>")
                                    xmlDocuments.AppendLine($"<Profesional>{item.Profesional}</Profesional>")
                                    xmlDocuments.AppendLine($"<NitMedico>{item.NitMedico}</NitMedico>")
                                    xmlDocuments.AppendLine($"<Consultorio>{item.Consultorio}</Consultorio>")
                                    If item.TipoCita IsNot Nothing Then
                                        xmlDocuments.AppendLine($"<TipoCita>{item.TipoCita}</TipoCita>")
                                    End If
                                    xmlDocuments.AppendLine($"<ActividadMedica>{item.ActividadMedica}</ActividadMedica>")
                                    xmlDocuments.AppendLine($"<CodigoServicio>{item.CodigoServicio}</CodigoServicio>")
                                    xmlDocuments.AppendLine($"<Especialidad>{item.Especialidad}</Especialidad>")
                                    xmlDocuments.AppendLine($"<Servicio>{item.Servicio}</Servicio>")
                                    xmlDocuments.AppendLine($"<CantidadServicio>{item.CantidadServicio}</CantidadServicio>")
                                    xmlDocuments.AppendLine($"<CodigoEspecialidad>{item.CodigoEspecialidad}</CodigoEspecialidad>")
                                    xmlDocuments.AppendLine($"<AreaServicio>{item.AreaServicio}</AreaServicio>")
                                    xmlDocuments.AppendLine($"<CentroCosto>{item.CentroCosto}</CentroCosto>")
                                    If item.TipoSolicitud IsNot Nothing Then
                                        xmlDocuments.AppendLine($"<TipoSolicitud>{item.TipoSolicitud}</TipoSolicitud>")
                                    End If
                                    xmlDocuments.AppendLine($"<RequiresConfirmAppointment>{item.RequiresConfirmAppointment}</RequiresConfirmAppointment>")
                                    If item.InvoiceId IsNot Nothing Then
                                        xmlDocuments.AppendLine($"<InvoiceId>{item.InvoiceId}</InvoiceId>")
                                    End If
                                    xmlDocuments.AppendLine($"<InvoiceNumber>{item.InvoiceNumber}</InvoiceNumber>")
                                    xmlDocuments.AppendLine($"<CodigoPaciente>{item.CodigoPaciente}</CodigoPaciente>")
                                    xmlDocuments.AppendLine($"<CodigoCentroAtencion>{item.CodigoCentroAtencion}</CodigoCentroAtencion>")
                                    xmlDocuments.AppendLine($"<CodigoUnidadFuncional>{item.CodigoUnidadFuncional}</CodigoUnidadFuncional>")
                                    xmlDocuments.AppendLine($"<AdmissionNumberInvoice>{item.AdmissionNumberInvoice}</AdmissionNumberInvoice>")
                                    xmlDocuments.AppendLine($"<IsFalseId>{item.IsFalseId}</IsFalseId>")
                                    xmlDocuments.AppendLine($"<GeneratedId>{item.GeneratedId}</GeneratedId>")
                                    If item.HealthAdministratorIdInvoice IsNot Nothing Then
                                        xmlDocuments.AppendLine($"<HealthAdministratorIdInvoice>{item.HealthAdministratorIdInvoice}</HealthAdministratorIdInvoice>")
                                    End If
                                    If item.CareGroupIdInvoice IsNot Nothing Then
                                        xmlDocuments.AppendLine($"<CareGroupIdInvoice>{item.CareGroupIdInvoice}</CareGroupIdInvoice>")
                                    End If
                                    xmlDocuments.AppendLine($"<NombreCompletoPaciente>{item.NombreCompletoPaciente}</NombreCompletoPaciente>")
                                    xmlDocuments.AppendLine($"<CUPSEntityContractDescriptionId>{item.CUPSEntityContractDescriptionId}</CUPSEntityContractDescriptionId>")
                                    xmlDocuments.AppendLine($"<ActivityType>{item.ActivityType}</ActivityType>")
                                    xmlDocuments.AppendLine($"<IdSala>{item.IDSALA}</IdSala>")
                                    xmlDocuments.AppendLine($"<CODACTMED>{item.CODACTMED}</CODACTMED>")
                                    xmlDocuments.AppendLine($"<TypeOfScheduleActivity>{item.TypeOfScheduleActivity}</TypeOfScheduleActivity>")
                                    xmlDocuments.AppendLine("</CitaMedica>")
                                End Sub)

                End If

                'Detalles de Órdenes de Servicio
                If controlOutPatientServices.ServiceOrderDetail IsNot Nothing AndAlso controlOutPatientServices.ServiceOrderDetail.Any() Then

                    Dim RowXml As Integer = 1

                    controlOutPatientServices.ServiceOrderDetail.
                        ForEach(Sub(item)
                                    xmlDocuments.AppendLine("<ServiceOrderDetail>")
                                    xmlDocuments.AppendLine($"<RowXml>{RowXml}</RowXml>")
                                    xmlDocuments.AppendLine($"<Id>{item.Id}</Id>")
                                    xmlDocuments.AppendLine($"<ServiceOrderId>{item.ServiceOrderId}</ServiceOrderId>")
                                    xmlDocuments.AppendLine($"<CareGroupId>{item.CareGroupId}</CareGroupId>")
                                    If item.HealthAdministratorId IsNot Nothing Then
                                        xmlDocuments.AppendLine($"<HealthAdministratorId>{item.HealthAdministratorId}</HealthAdministratorId>")
                                    End If
                                    If item.ThirdPartyId IsNot Nothing Then
                                        xmlDocuments.AppendLine($"<ThirdPartyId>{item.ThirdPartyId}</ThirdPartyId>")
                                    End If
                                    xmlDocuments.AppendLine($"<ServiceType>{item.ServiceType}</ServiceType>")
                                    xmlDocuments.AppendLine($"<RecordType>{item.RecordType}</RecordType>")
                                    If item.CUPSEntityId IsNot Nothing Then
                                        xmlDocuments.AppendLine($"<CUPSEntityId>{item.CUPSEntityId}</CUPSEntityId>")
                                    End If
                                    If item.IPSServiceId IsNot Nothing Then
                                        xmlDocuments.AppendLine($"<IPSServiceId>{item.IPSServiceId}</IPSServiceId>")
                                    End If
                                    If item.HospitalStayId IsNot Nothing Then
                                        xmlDocuments.AppendLine($"<HospitalStayId>{item.HospitalStayId}</HospitalStayId>")
                                    End If
                                    If item.HospitalStayDetailId IsNot Nothing Then
                                        xmlDocuments.AppendLine($"<HospitalStayDetailId>{item.HospitalStayDetailId}</HospitalStayDetailId>")
                                    End If
                                    If item.ControlExternalConsultation IsNot Nothing Then
                                        xmlDocuments.AppendLine($"<ControlExternalConsultation>{item.ControlExternalConsultation}</ControlExternalConsultation>")
                                    End If
                                    If item.ControlExternalConsultationCode IsNot Nothing Then
                                        xmlDocuments.AppendLine($"<ControlExternalConsultationCode>{item.ControlExternalConsultationCode}</ControlExternalConsultationCode>")
                                    End If
                                    xmlDocuments.AppendLine($"<CUPSAssociateService>{item.CUPSAssociateService}</CUPSAssociateService>")
                                    If Not String.IsNullOrEmpty(item.CodeAssociateService) Then
                                        xmlDocuments.AppendLine($"<CodeAssociateService>{item.CodeAssociateService}</CodeAssociateService>")
                                    End If
                                    xmlDocuments.AppendLine($"<IsPackage>{item.IsPackage}</IsPackage>")
                                    xmlDocuments.AppendLine($"<Packaging>{item.Packaging}</Packaging>")
                                    If item.PackageServiceOrderDetailId IsNot Nothing Then
                                        xmlDocuments.AppendLine($"<PackageServiceOrderDetailId>{item.PackageServiceOrderDetailId}</PackageServiceOrderDetailId>")
                                    End If
                                    xmlDocuments.AppendLine($"<LiquidationType>{item.LiquidationType}</LiquidationType>")
                                    If item.Presentation IsNot Nothing Then
                                        xmlDocuments.AppendLine($"<Presentation>{item.Presentation}</Presentation>")
                                    End If
                                    If item.ProductId IsNot Nothing Then
                                        xmlDocuments.AppendLine($"<ProductId>{item.ProductId}</ProductId>")
                                    End If
                                    xmlDocuments.AppendLine($"<InvoicedQuantity>{item.InvoicedQuantity}</InvoicedQuantity>")
                                    xmlDocuments.AppendLine($"<SupplyQuantity>{item.SupplyQuantity}</SupplyQuantity>")
                                    xmlDocuments.AppendLine($"<DevolutionQuantity>{item.DevolutionQuantity}</DevolutionQuantity>")
                                    xmlDocuments.AppendLine($"<RateManualSalePrice>{item.RateManualSalePrice}</RateManualSalePrice>")
                                    xmlDocuments.AppendLine($"<CostValue>{item.CostValue}</CostValue>")
                                    xmlDocuments.AppendLine($"<ServiceDate>{item.ServiceDate.ToString("dd/MM/yyyy HH:mm:ss")}</ServiceDate>")
                                    If Not String.IsNullOrEmpty(item.AuthorizationNumber) Then
                                        xmlDocuments.AppendLine($"<AuthorizationNumber>{item.AuthorizationNumber}</AuthorizationNumber>")
                                    End If
                                    xmlDocuments.AppendLine($"<PerformsFunctionalUnitId>{item.PerformsFunctionalUnitId}</PerformsFunctionalUnitId>")
                                    If Not String.IsNullOrEmpty(item.PerformsHealthProfessionalCode) Then
                                        xmlDocuments.AppendLine($"<PerformsHealthProfessionalCode>{item.PerformsHealthProfessionalCode}</PerformsHealthProfessionalCode>")
                                    End If
                                    If Not String.IsNullOrEmpty(item.PerformsProfessionalSpecialty) Then
                                        xmlDocuments.AppendLine($"<PerformsProfessionalSpecialty>{item.PerformsProfessionalSpecialty}</PerformsProfessionalSpecialty>")
                                    End If
                                    If item.PerformsHealthProfessionalThirdPartyId IsNot Nothing Then
                                        xmlDocuments.AppendLine($"<PerformsHealthProfessionalThirdPartyId>{item.PerformsHealthProfessionalThirdPartyId}</PerformsHealthProfessionalThirdPartyId>")
                                    End If
                                    If item.BillingConceptId IsNot Nothing Then
                                        xmlDocuments.AppendLine($"<BillingConceptId>{item.BillingConceptId}</BillingConceptId>")
                                    End If
                                    xmlDocuments.AppendLine($"<CostCenterId>{item.CostCenterId}</CostCenterId>")
                                    xmlDocuments.AppendLine($"<SettlementType>{item.SettlementType}</SettlementType>")
                                    If item.IncludeServiceOrderDetailId IsNot Nothing Then
                                        xmlDocuments.AppendLine($"<IncludeServiceOrderDetailId>{item.IncludeServiceOrderDetailId}</IncludeServiceOrderDetailId>")
                                    End If
                                    If item.RecoveryRatio IsNot Nothing Then
                                        xmlDocuments.AppendLine($"<RecoveryRatio>{item.RecoveryRatio}</RecoveryRatio>")
                                    End If
                                    If item.RateManualId IsNot Nothing Then
                                        xmlDocuments.AppendLine($"<RateManualId>{item.RateManualId}</RateManualId>")
                                    End If
                                    If item.RateManualType IsNot Nothing Then
                                        xmlDocuments.AppendLine($"<RateManualType>{item.RateManualType}</RateManualType>")
                                    End If
                                    If item.RateManualDetailId IsNot Nothing Then
                                        xmlDocuments.AppendLine($"<RateManualDetailId>{item.RateManualDetailId}</RateManualDetailId>")
                                    End If
                                    If item.DefinitionRateDetailId IsNot Nothing Then
                                        xmlDocuments.AppendLine($"<DefinitionRateDetailId>{item.DefinitionRateDetailId}</DefinitionRateDetailId>")
                                    End If
                                    If item.DefinitionRateDetailConditionId IsNot Nothing Then
                                        xmlDocuments.AppendLine($"<DefinitionRateDetailConditionId>{item.DefinitionRateDetailConditionId}</DefinitionRateDetailConditionId>")
                                    End If
                                    xmlDocuments.AppendLine($"<SubTotalSalesPrice>{item.SubTotalSalesPrice}</SubTotalSalesPrice>")
                                    xmlDocuments.AppendLine($"<ThirdPartyDiscount>{item.ThirdPartyDiscount}</ThirdPartyDiscount>")
                                    xmlDocuments.AppendLine($"<ThirdPartyDiscountPercentage>{item.ThirdPartyDiscountPercentage}</ThirdPartyDiscountPercentage>")
                                    xmlDocuments.AppendLine($"<TotalSalesPrice>{item.TotalSalesPrice}</TotalSalesPrice>")
                                    xmlDocuments.AppendLine($"<GrandTotalSalesPrice>{item.GrandTotalSalesPrice}</GrandTotalSalesPrice>")
                                    xmlDocuments.AppendLine($"<SurchargeApply>{item.SurchargeApply}</SurchargeApply>")
                                    If item.SurgicalInterventionType IsNot Nothing Then
                                        xmlDocuments.AppendLine($"<SurgicalInterventionType>{item.SurgicalInterventionType}</SurgicalInterventionType>")
                                    End If
                                    xmlDocuments.AppendLine($"<SurgeryNumber>{item.SurgeryNumber}</SurgeryNumber>")
                                    xmlDocuments.AppendLine($"<IsFirstEvent>{item.IsFirstEvent}</IsFirstEvent>")
                                    xmlDocuments.AppendLine($"<IsAnnulled>{item.IsAnnulled}</IsAnnulled>")
                                    xmlDocuments.AppendLine($"<IsDelete>{item.IsDelete}</IsDelete>")
                                    xmlDocuments.AppendLine($"<IncomeMainAccountId>{item.IncomeMainAccountId}</IncomeMainAccountId>")

                                    xmlDocuments.AppendLine($"<IdCita>{item.IdCita}</IdCita>")
                                    xmlDocuments.AppendLine($"<HemocomponentId>{item.HemocomponentId}</HemocomponentId>")
                                    xmlDocuments.AppendLine($"<CODSERIPS>{item.CODSERIPS}</CODSERIPS>")

                                    If item.ApplyRIAS IsNot Nothing Then
                                        xmlDocuments.AppendLine("<ApplyRIAS>" & item.ApplyRIAS.ToString() & "</ApplyRIAS>")
                                    Else
                                        xmlDocuments.AppendLine("<ApplyRIAS>-</ApplyRIAS>")
                                    End If
                                    xmlDocuments.AppendLine("<RIASCupsId>" & item.RIASCupsId & "</RIASCupsId>")
                                    xmlDocuments.AppendLine("<RealizedQuantity>" & item.RealizedQuantity & "</RealizedQuantity>")
                                    xmlDocuments.AppendLine("<CUPSEntityContractDescriptionId>" & item.CUPSEntityContractDescriptionId & "</CUPSEntityContractDescriptionId>")
                                    xmlDocuments.AppendLine("<QuotationServiceOrderDetailId>" & item.QuotationServiceOrderDetailId & "</QuotationServiceOrderDetailId>")
                                    xmlDocuments.AppendLine("<TraceabilityPaperworkEventsId>" & item.TraceabilityPaperworkEventsId & "</TraceabilityPaperworkEventsId>")
                                    xmlDocuments.AppendLine(String.Format("<IsServiceOrderDetailControlJustify>{0}</IsServiceOrderDetailControlJustify>", item.IsServiceOrderDetailControlJustify))
                                    xmlDocuments.AppendLine(String.Format("<ServiceOrderDetailControlJustification>{0}</ServiceOrderDetailControlJustification>", item.ServiceOrderDetailControlJustification))
                                    xmlDocuments.AppendLine("<FinalProductCost>" & item.FinalProductCost & "</FinalProductCost>")
                                    xmlDocuments.AppendLine("<GrossValue>" & item.GrossValue & "</GrossValue>")
                                    xmlDocuments.AppendLine("<TaxValue>" & item.TaxValue & "</TaxValue>")

                                    If item.ServiceOrderDetailSurgical IsNot Nothing AndAlso item.ServiceOrderDetailSurgical.Count > 0 Then
                                        item.ServiceOrderDetailSurgical.ToList().
                                            ForEach(Sub(itemSurgical)
                                                        xmlDocuments.AppendLine("<ServiceOrderDetailSurgical>")
                                                        xmlDocuments.AppendLine($"<Id>{itemSurgical.Id}</Id>")
                                                        xmlDocuments.AppendLine($"<ServiceOrderDetailId>{itemSurgical.ServiceOrderDetailId}</ServiceOrderDetailId>")
                                                        xmlDocuments.AppendLine($"<ServiceOrderDetailIdRow>{RowXml}</ServiceOrderDetailIdRow>")
                                                        xmlDocuments.AppendLine($"<IPSServiceId>{itemSurgical.IPSServiceId}</IPSServiceId>")
                                                        xmlDocuments.AppendLine($"<InvoicedQuantity>{itemSurgical.InvoicedQuantity}</InvoicedQuantity>")
                                                        xmlDocuments.AppendLine($"<LiquidationPercentage>{itemSurgical.LiquidationPercentage}</LiquidationPercentage>")
                                                        xmlDocuments.AppendLine($"<RateManualSalePrice>{itemSurgical.RateManualSalePrice}</RateManualSalePrice>")
                                                        xmlDocuments.AppendLine($"<TotalSalesPrice>{itemSurgical.TotalSalesPrice}</TotalSalesPrice>")

                                                        If itemSurgical.PerformsHealthProfessionalCode IsNot Nothing AndAlso itemSurgical.PerformsHealthProfessionalCode IsNot String.Empty Then
                                                            xmlDocuments.AppendLine($"<PerformsHealthProfessionalCode>{itemSurgical.PerformsHealthProfessionalCode}</PerformsHealthProfessionalCode>")
                                                        End If

                                                        If itemSurgical.PerformsHealthProfessionalThirdPartyId IsNot Nothing Then
                                                            xmlDocuments.AppendLine($"<PerformsHealthProfessionalThirdPartyId>{itemSurgical.PerformsHealthProfessionalThirdPartyId}</PerformsHealthProfessionalThirdPartyId>")
                                                        End If

                                                        xmlDocuments.AppendLine($"<CostValue>{itemSurgical.CostValue}</CostValue>")
                                                        xmlDocuments.AppendLine($"<BillingConceptId>{itemSurgical.BillingConceptId}</BillingConceptId>")
                                                        xmlDocuments.AppendLine($"<CostCenterId>{itemSurgical.CostCenterId}</CostCenterId>")

                                                        If itemSurgical.RateManualDetailSurgicalId IsNot Nothing Then
                                                            xmlDocuments.AppendLine($"<RateManualDetailSurgicalId>{itemSurgical.RateManualDetailSurgicalId}</RateManualDetailSurgicalId>")
                                                        End If

                                                        xmlDocuments.AppendLine($"<SurchargeApply>{itemSurgical.SurchargeApply}</SurchargeApply>")
                                                        xmlDocuments.AppendLine($"<OnlyMedicalFees>{itemSurgical.OnlyMedicalFees}</OnlyMedicalFees>")
                                                        xmlDocuments.AppendLine($"<IncomeMainAccountId>{itemSurgical.IncomeMainAccountId}</IncomeMainAccountId>")
                                                        xmlDocuments.AppendLine($"<EntityState>{itemSurgical.ChangeTracker.State}</EntityState>")
                                                        xmlDocuments.AppendLine("</ServiceOrderDetailSurgical>")
                                                    End Sub)
                                    End If

                                    xmlDocuments.AppendLine("</ServiceOrderDetail>")

                                    RowXml += 1
                                End Sub)
                End If
                If controlOutPatientServices.ListHemocomponent IsNot Nothing AndAlso controlOutPatientServices.ListHemocomponent.Any() Then
                    Dim IdAuto As Integer = 1
                    controlOutPatientServices.ListHemocomponent.
                        ForEach(Sub(item)
                                    xmlDocuments.AppendLine("<Hemocomponent>")
                                    xmlDocuments.AppendLine(String.Format("<{0}>{1}</{0}>", "IdAuto", IdAuto))
                                    xmlDocuments.AppendLine(String.Format("<{0}>{1}</{0}>", "Id", item.Id))
                                    xmlDocuments.AppendLine(String.Format("<{0}>{1}</{0}>", "Code", item.Code))
                                    xmlDocuments.AppendLine(String.Format("<{0}>{1}</{0}>", "Description", item.Description))
                                    xmlDocuments.AppendLine(String.Format("<{0}>{1}</{0}>", "ProfessionalId", item.ProfessionalId))
                                    xmlDocuments.AppendLine(String.Format("<{0}>{1}</{0}>", "Quantity", item.Quantity))
                                    xmlDocuments.AppendLine(String.Format("<{0}>{1}</{0}>", "SpecialityCode", item.Quantity))
                                    xmlDocuments.AppendLine(String.Format("<{0}>{1}</{0}>", "IdAGASICITA", item.IdAGASICITA))
                                    xmlDocuments.AppendLine(String.Format("<{0}>{1}</{0}>", "VolumenComponent", item.VolumenComponent))
                                    item.Details.ForEach(Sub(detail)
                                                             xmlDocuments.AppendLine("<HemocomponentDetail>")
                                                             xmlDocuments.AppendLine(String.Format("<{0}>{1}</{0}>", "Id", detail.Id))
                                                             xmlDocuments.AppendLine(String.Format("<{0}>{1}</{0}>", "HemocomponentId", detail.HemocomponentId))
                                                             xmlDocuments.AppendLine(String.Format("<{0}>{1}</{0}>", "TypeServiceIPS", detail.TypeServiceIPS))
                                                             xmlDocuments.AppendLine(String.Format("<{0}>{1}</{0}>", "kindLoad", detail.kindLoad))
                                                             xmlDocuments.AppendLine(String.Format("<{0}>{1}</{0}>", "CodeServiceIPS", detail.CodeServiceIPS))
                                                             xmlDocuments.AppendLine(String.Format("<{0}>{1}</{0}>", "DescriptionServiceIPS", detail.DescriptionServiceIPS))
                                                             xmlDocuments.AppendLine(String.Format("<{0}>{1}</{0}>", "SERRASANTI", IIf(detail.SERRASANTI, 1, 0)))
                                                             xmlDocuments.AppendLine(String.Format("<{0}>{1}</{0}>", "IdAutoC", IdAuto))
                                                             xmlDocuments.AppendLine("</HemocomponentDetail>")
                                                         End Sub)
                                    xmlDocuments.AppendLine("</Hemocomponent>")
                                    IdAuto += 1
                                End Sub)
                End If
                xmlDocuments.AppendLine("</ControlOutPatientServices>")

                Dim results As SP_GenerateDocuments_Result = _serviceOrderRepository.SP_GenerateDocuments(xmlDocuments.ToString(), audit.CodeUser)
                If results Is Nothing OrElse results.CodeResult = "999" Then
                    scope.Dispose()
                    Return New ActionResult(Of String) With {.StateResult = False, .Message = results.MessageResult}
                End If

                scope.Complete()
                Return New ActionResult(Of String) With {.StateResult = True, .Message = results.MessageResult, .MessageResult = {results.NumIngres}.ToList(), .MessageResultAux = {results.NotificationContract.ToString()}.ToList()}
            End Using
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of String) With {.StateResult = False, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)} 'ResourceManager.GetString("ErrorUnknown")}
        End Try
    End Function

    Private Function CreateItem(cita As CitasMedicas, clase As eClaseCita, numingres As String, careGroupId As Integer, healthAdministratorId As Integer, Optional codentida As String = "") As ActionResult
        Dim result As New ActionResult()
        result.StateResult = False

        Select Case clase
            Case eClaseCita.ConsultaExterna

                Dim admissionNumber As String = IIf(cita.InvoiceId IsNot Nothing AndAlso cita.InvoiceId <> 0, cita.AdmissionNumberInvoice, numingres)
                Dim adconcoexx As ADCONCOEX = _aDCONCOEXrepository.GetADCONCOEXByAdmissionAndIPCODPACIAndESTADO(admissionNumber, cita.CodigoPaciente, 1)
                If adconcoexx IsNot Nothing AndAlso adconcoexx.CODCONCEC > 0 Then
                    result.Message = String.Format("Ya existe un registro de consulta externa para el ingreso ({0}) y paciente ({1})", admissionNumber, cita.CodigoPaciente.Trim())
                    Return result
                End If

                Dim aDCONCOEX As New ADCONCOEX()
                With aDCONCOEX
                    .IPCODPACI = cita.CodigoPaciente
                    .IPFECHACO = cita.FechaCita
                    .IPFECHCIT = cita.FechaCita
                    .CONESTADO = 1
                    .IPNOMCOMP = cita.NombreCompletoPaciente
                    .CODENTIDA = codentida.Trim()
                    .CODCENATE = cita.CodigoCentroAtencion
                    .UFUCODIGO = cita.CodigoUnidadFuncional
                    .CODTIPCON = cita.TipoCita
                    .CODPROSAL = cita.CodigoProfesional
                    .NUMINGRES = admissionNumber
                    .NUMCONCIT = IIf(cita.IsFalseId = False, cita.Codigo, Nothing)
                    .INDAUDFOR = 0
                    .AUTESTADO = Nothing
                    .CODESPECI = cita.CodigoEspecialidad
                    .LIQUIDAR = IIf(cita.InvoiceId IsNot Nothing AndAlso cita.InvoiceId <> 0, False, True)
                    .PRIMERLLA = False
                    .SEGUNDLLA = False
                    .TERCERLLA = False
                    .GENCAREGROUP = IIf(cita.InvoiceId IsNot Nothing AndAlso cita.InvoiceId <> 0, cita.CareGroupIdInvoice, careGroupId)
                    .GENCONENTITY = IIf(cita.InvoiceId IsNot Nothing AndAlso cita.InvoiceId <> 0, cita.HealthAdministratorIdInvoice, healthAdministratorId)
                    .GENINVOICE = cita.InvoiceNumber
                    .GENINVOICEID = cita.InvoiceId
                End With

                If Not cita.IsFalseId Then
                    Dim aDCONCOED As New ADCONCOED()
                    aDCONCOED.NUMCONCIT = aDCONCOEX.NUMCONCIT
                    aDCONCOEX.ADCONCOED.Add(aDCONCOED)
                End If

                _aDCONCOEXrepository.SaveEntity(aDCONCOEX)
                _aDCONCOEXrepository.UnitWork.Commit()

                cita.GeneratedId = aDCONCOEX.CODCONCEC
            Case eClaseCita.Laboratorios

                Dim admissionNumber As String = IIf(cita.InvoiceId IsNot Nothing AndAlso cita.InvoiceId <> 0, cita.AdmissionNumberInvoice, numingres)
                Dim ambordlabx As AMBORDLAB = _aMBORDLABRepository.GetAMBORDLABByIngresoPacienteCodigoServicio(admissionNumber, cita.CodigoPaciente, cita.CodigoServicio)
                If ambordlabx IsNot Nothing AndAlso ambordlabx.AUTO > 0 Then
                    result.Message = String.Format("Ya existe un registro de laboratorio para el ingreso ({0}), paciente ({1}) y servicio ({2})", admissionNumber, cita.CodigoPaciente.Trim(), cita.CodigoServicio)
                    Return result
                End If

                Dim aMBORDLAB As New AMBORDLAB()
                With aMBORDLAB
                    .IPCODPACI = cita.CodigoPaciente
                    .NUMINGRES = admissionNumber
                    .CODCENATE = cita.CodigoCentroAtencion
                    .UFUCODIGO = cita.CodigoUnidadFuncional
                    .CODPROSAL = cita.CodigoProfesional
                    .FECORDMED = cita.FechaCita
                    .CODSERIPS = cita.CodigoServicio
                    .CANSERIPS = IIf(cita.CantidadServicio <> 0, cita.CantidadServicio, 1)
                    .ESTSERIPS = 1
                    .INDAUDFOR = 0
                    .ESTALELAB = False
                    .IPFECHACO = cita.FechaCita
                    .NUMCONCIT = IIf(cita.IsFalseId = False, cita.Codigo, Nothing)
                    .GENCAREGROUP = IIf(cita.InvoiceId IsNot Nothing AndAlso cita.InvoiceId <> 0, cita.CareGroupIdInvoice, careGroupId)
                    .GENCONENTITY = IIf(cita.InvoiceId IsNot Nothing AndAlso cita.InvoiceId <> 0, cita.HealthAdministratorIdInvoice, healthAdministratorId)
                    .GENINVOICE = cita.InvoiceNumber
                    .GENINVOICEID = cita.InvoiceId
                End With

                'Se envia a guardar en la tabla de ADCONCOEX
                Dim resultADCONCOEX = SaveADCONCOEX(cita, clase, numingres, careGroupId, healthAdministratorId, codentida)
                If resultADCONCOEX.StateResult = False Then
                    result.Message = resultADCONCOEX.Message
                    Return result
                End If

                _aMBORDLABRepository.SaveEntity(aMBORDLAB)
                _aMBORDLABRepository.UnitWork.Commit()

                cita.GeneratedId = aMBORDLAB.AUTO
            Case eClaseCita.Imagenes

                Dim hCPARPACS As HCPARPACS = _hCPARPACSRepository.GetHCPARPACSByCentAten(cita.CodigoCentroAtencion) 'Centro Atencion parametro
                Dim hCINTESER As HCINTESER = Nothing
                If hCPARPACS IsNot Nothing AndAlso hCPARPACS.INTPACSAC Then
                    hCINTESER = _hCINTESERRepository.GetHCINTESERByCodserIpsAndCentAten(cita.CodigoServicio, cita.CodigoCentroAtencion) 'por codserips y cent atenc
                End If

                Dim aMBORDIMA As New AMBORDIMA()
                With aMBORDIMA
                    .IPCODPACI = cita.CodigoPaciente
                    .NUMINGRES = IIf(cita.InvoiceId IsNot Nothing AndAlso cita.InvoiceId <> 0, cita.AdmissionNumberInvoice, numingres)
                    .CODCENATE = cita.CodigoCentroAtencion
                    .UFUCODIGO = cita.CodigoUnidadFuncional
                    .CODPROSAL = cita.CodigoProfesional
                    .FECORDMED = cita.FechaCita
                    .CODSERIPS = cita.CodigoServicio
                    .CANSERIPS = IIf(cita.CantidadServicio <> 0, cita.CantidadServicio, 1)
                    .ESTSERIPS = 1
                    .INDAUDFOR = 0

                    .SERREAINT = hCPARPACS IsNot Nothing AndAlso hCPARPACS.INTPACSAC AndAlso hCINTESER IsNot Nothing AndAlso Not String.IsNullOrEmpty(hCINTESER.CODSERIPS)

                    .SERTRANSC = False
                    .SERVALMED = False
                    .ESTALEIMG = False
                    .REALINOTIF = False
                    .IPFECHACO = cita.FechaCita
                    .NUMCONCIT = IIf(cita.IsFalseId = False, cita.Codigo, Nothing)
                    .GENCAREGROUP = IIf(cita.InvoiceId IsNot Nothing AndAlso cita.InvoiceId <> 0, cita.CareGroupIdInvoice, careGroupId)
                    .GENCONENTITY = IIf(cita.InvoiceId IsNot Nothing AndAlso cita.InvoiceId <> 0, cita.HealthAdministratorIdInvoice, healthAdministratorId)
                    .GENINVOICE = cita.InvoiceNumber
                    .GENINVOICEID = cita.InvoiceId

                End With

                'Se envia a guardar en la tabla de ADCONCOEX
                Dim resultADCONCOEX = SaveADCONCOEX(cita, clase, numingres, careGroupId, healthAdministratorId, codentida)
                If resultADCONCOEX.StateResult = False Then
                    result.Message = resultADCONCOEX.Message
                    Return result
                End If

                _aMBORDIMARepository.SaveEntity(aMBORDIMA)
                _aMBORDIMARepository.UnitWork.Commit()

                cita.GeneratedId = aMBORDIMA.AUTO
            Case eClaseCita.Patologias

                Dim admissionNumber As String = IIf(cita.InvoiceId IsNot Nothing AndAlso cita.InvoiceId <> 0, cita.AdmissionNumberInvoice, numingres)
                Dim ambordpatx As AMBORDPAT = _aMBORDPATRepository.GetAMBORDPATByIngresoPacienteCodigoServicio(admissionNumber, cita.CodigoPaciente, cita.CodigoServicio)
                If ambordpatx IsNot Nothing AndAlso ambordpatx.AUTO > 0 Then
                    result.Message = String.Format("Ya existe un registro de patologías para el ingreso ({0}), paciente ({1}) y servicio ({2})", admissionNumber, cita.CodigoPaciente.Trim(), cita.CodigoServicio)
                    Return result
                End If

                Dim aMBORDPAT As New AMBORDPAT()
                With aMBORDPAT
                    .IPCODPACI = cita.CodigoPaciente
                    .NUMINGRES = IIf(cita.InvoiceId IsNot Nothing AndAlso cita.InvoiceId <> 0, cita.AdmissionNumberInvoice, numingres)
                    .CODCENATE = cita.CodigoCentroAtencion
                    .UFUCODIGO = cita.CodigoUnidadFuncional
                    .CODPROSAL = cita.CodigoProfesional
                    .FECORDMED = cita.FechaCita
                    .CODSERIPS = cita.CodigoServicio
                    .CANSERIPS = IIf(cita.CantidadServicio <> 0, cita.CantidadServicio, 1)
                    .ESTSERIPS = 1
                    .INDAUDFOR = 0
                    .ESTALEPAT = False
                    .IPFECHACO = cita.FechaCita
                    .NUMCONCIT = IIf(cita.IsFalseId = False, cita.Codigo, Nothing)
                    .GENCAREGROUP = IIf(cita.InvoiceId IsNot Nothing AndAlso cita.InvoiceId <> 0, cita.CareGroupIdInvoice, careGroupId)
                    .GENCONENTITY = IIf(cita.InvoiceId IsNot Nothing AndAlso cita.InvoiceId <> 0, cita.HealthAdministratorIdInvoice, healthAdministratorId)
                    .GENINVOICE = cita.InvoiceNumber
                    .GENINVOICEID = cita.InvoiceId
                End With
                _aMBORDPATRepository.SaveEntity(aMBORDPAT)
                _aMBORDPATRepository.UnitWork.Commit()

                cita.GeneratedId = aMBORDPAT.AUTO
        End Select

        result.StateResult = True
        Return result
    End Function

    ''' <summary>
    ''' Metodo que guarda en la tabla ADCONCOEX
    ''' </summary>
    ''' <returns></returns>
    Private Function SaveADCONCOEX(cita As CitasMedicas, clase As eClaseCita, numingres As String, careGroupId As Integer, healthAdministratorId As Integer, Optional codentida As String = "") As ActionResult(Of ADCONCOEX)
        'Se obtiene el numero de ingreso
        Dim admissionNumber As String = IIf(cita.InvoiceId IsNot Nothing AndAlso cita.InvoiceId <> 0, cita.AdmissionNumberInvoice, numingres)

        'Se obtiene el mensaje en caso de error
        Dim message As String = IIf(clase = eClaseCita.Laboratorios, "laboratorios", "imagenes")

        ''Se valida que no exista un adconcoex con el numero de admision, codigo paciente y el estado
        'Dim adconcoexx As ADCONCOEX = _aDCONCOEXrepository.GetADCONCOEXByAdmissionAndIPCODPACIAndESTADO(admissionNumber, cita.CodigoPaciente, 1)
        'If adconcoexx IsNot Nothing AndAlso adconcoexx.CODCONCEC > 0 Then
        '    Return New ActionResult(Of ADCONCOEX) With {.StateResult = False, .Message = String.Format("Ya existe un registro de " + message + " para el ingreso ({0}) y paciente ({1})", admissionNumber, cita.CodigoPaciente.Trim())}
        'End If

        'Se genera la entidad cabecera
        Dim aDCONCOEX As New ADCONCOEX()
        With aDCONCOEX
            .IPCODPACI = cita.CodigoPaciente
            .IPFECHACO = cita.FechaCita
            .IPFECHCIT = cita.FechaCita
            .CONESTADO = 1
            .IPNOMCOMP = cita.NombreCompletoPaciente
            .CODENTIDA = codentida.Trim()
            .CODCENATE = cita.CodigoCentroAtencion
            .UFUCODIGO = cita.CodigoUnidadFuncional
            .CODTIPCON = cita.TipoCita
            .CODPROSAL = cita.CodigoProfesional
            .NUMINGRES = admissionNumber
            .NUMCONCIT = IIf(cita.IsFalseId = False, cita.Codigo, Nothing)
            .INDAUDFOR = 0
            .AUTESTADO = Nothing
            .CODESPECI = cita.CodigoEspecialidad
            .LIQUIDAR = IIf(cita.InvoiceId IsNot Nothing AndAlso cita.InvoiceId <> 0, False, True)
            .PRIMERLLA = False
            .SEGUNDLLA = False
            .TERCERLLA = False
            .GENCAREGROUP = IIf(cita.InvoiceId IsNot Nothing AndAlso cita.InvoiceId <> 0, cita.CareGroupIdInvoice, careGroupId)
            .GENCONENTITY = IIf(cita.InvoiceId IsNot Nothing AndAlso cita.InvoiceId <> 0, cita.HealthAdministratorIdInvoice, healthAdministratorId)
            .GENINVOICE = cita.InvoiceNumber
            .GENINVOICEID = cita.InvoiceId
        End With

        'Se genera el detalle
        If Not cita.IsFalseId Then
            Dim aDCONCOED As New ADCONCOED()
            aDCONCOED.NUMCONCIT = aDCONCOEX.NUMCONCIT
            aDCONCOEX.ADCONCOED.Add(aDCONCOED)
        End If

        'Se envia a guardar
        _aDCONCOEXrepository.SaveEntity(aDCONCOEX)
        _aDCONCOEXrepository.UnitWork.Commit()

        'Se devuelve la entidad
        Return New ActionResult(Of ADCONCOEX) With {.StateResult = True, .ObjectEmbbeded = aDCONCOEX}
    End Function

#End Region

#Region "Enums"
    '<DataContract()>
    Public Enum eClaseCita
        '<EnumMember()> _
        ConsultaExterna
        '<EnumMember()> _
        Laboratorios
        '<EnumMember()> _
        Imagenes
        '<EnumMember()> _
        Patologias
    End Enum
#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                _serviceorderAdminService.Dispose()
                _billingSequenceAdminService.Dispose()
                _billingServices.Dispose()
            End If
            _serviceorderAdminService = Nothing
            _admissionRepository = Nothing
            _inCONSECURepository = Nothing
            _billingSequenceAdminService = Nothing
            _careGroupRepository = Nothing
            _aGASICITARepository = Nothing
            _aDCONCOEXrepository = Nothing
            _aMBORDLABRepository = Nothing
            _aMBORDIMARepository = Nothing
            _aMBORDPATRepository = Nothing
            _billingServices = Nothing
            _healthAdministratorRepository = Nothing
            _iNCUPSIPSRepository = Nothing
            _iAGACTMDDDRepository = Nothing
            _inventoryProductRepository = Nothing
            _productGroupRepository = Nothing
            _settingInventoryRepository = Nothing
            _functionalUnitRepository = Nothing
            _productRateDetailRepository = Nothing
            _iNPROFSALRepository = Nothing
            _hCFARMEPCRepository = Nothing
            _hCINTESERRepository = Nothing
            _hCPARPACSRepository = Nothing
            _serviceOrderRepository = Nothing
            _inentidadRepository = Nothing
            IndigoGC.Execute()
        End If
        disposedValue = True
    End Sub

    ' Visual Basic agrega este código para implementar correctamente el patrón descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class