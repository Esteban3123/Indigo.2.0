'***********************************************************************
' Assembly         : Infrastructure.Data.Xpo
' Author           : juan F. Tamayo
' Created          : 2016-02-08
'
' Last Modified By : Juan F. Tamayo
' Last Modified On : 2016-02-08
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System
Imports Infrastructure.CrossCutting.Base
Imports DevExpress.Xpo
Imports Infrastructure.CrossCutting.Xpo.Base
Imports System.ServiceModel
Imports System.ServiceModel.Description
Imports System.Configuration
Imports DevExpress.Xpo.Metadata

#End Region

''' <summary>
''' Provee servicios de acceso a datos usando un almacén de datos
''' </summary>
Public Class XpoServiceEx
    Implements IDisposable

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    Public Sub New(company As String)
        ReadConfiguration()
        RefreshDataLayer(company)
    End Sub

    Public Sub New()
    End Sub

#End Region

#Region "Singleton"

    ''' <summary>
    ''' uri donde estan localizado los servicios xpo
    ''' </summary>
    Public Shared uriServiceEntitiesXpo As String

    ''' <summary>
    ''' protocolo utilizado para los servicios xpo
    ''' </summary>
    Public Shared protocolServicesXpo As Protocol

    ''' <summary>
    ''' Punto de entrada del servicio
    ''' </summary>
    Public Shared endpointConfiguration As String

    ''' <summary>
    ''' Dirección remota del servicio
    ''' </summary>
    Public Shared remoteAddress As String

    ''' <summary>
    ''' Unica instancia del servicio
    ''' </summary>
    Private Shared _instance As XpoServiceEx 'Dictionary(Of String, XpoServiceEx) = New Dictionary(Of String, XpoServiceEx)()

    ''' <summary>
    ''' Número de endpoint asignado al cliente
    ''' </summary>
    Private Shared _endpointNumber As Integer

    Public Property DataLayer As IDataLayer

    ''' <summary>
    ''' Obtiene la unica instancia del servicio
    ''' </summary>
    ''' <param name="company">Empresa a consultar</param>
    ''' <returns>Unica instancia del servicio</returns>
    Public Shared ReadOnly Property Instance(ByVal company As String) As XpoServiceEx
        Get
            XpoDefault.DataLayer = Nothing
            'Dim newServices As String = ConfigurationManager.AppSettings.Get("NewServices")
            'If Not String.IsNullOrEmpty(newServices) AndAlso newServices.ToLower().Equals("true") Then
            If ApplicationSetting.Instance.NewServices Then
                _instance = New XpoServiceCached(company)
            Else
                _instance = New XpoServiceEx(company)
            End If

            'If Not _instance.ContainsKey(company) Then
            '    Dim newServices As String = ConfigurationManager.AppSettings.Get("NewServices")
            '    If Not String.IsNullOrEmpty(newServices) AndAlso newServices.ToLower().Equals("true") Then
            '        _instance.Add(company, New XpoServiceCached(company))
            '    Else
            '        _instance.Add(company, New XpoServiceEx(company))
            '    End If
            'End If
            'XpoDefault.DataLayer = _instance(company).DataLayer
            'Return _instance(company)

            Return _instance
        End Get
    End Property

    ''' <summary>
    ''' Destruye la instancia
    ''' </summary>
    Public Shared Sub DisposeInstance()
        If _instance IsNot Nothing Then
            _instance.Dispose()
            'For Each i As XpoServiceEx In _instance.Select(Function(o) o.Value)
            '    i.Dispose()
            'Next
            _instance = Nothing
            uriServiceEntitiesXpo = Nothing
            endpointConfiguration = Nothing
            remoteAddress = Nothing
            _endpointNumber = -1
        End If
    End Sub

    ''' <summary>
    ''' Forza la destrucción e instanciación del servicio con
    ''' la empresa pasada como parametro
    ''' </summary>
    Public Shared Sub RenewInstance(ByVal company As String)
        If _instance IsNot Nothing Then
            DisposeInstance()
        End If
        Dim r = XpoServiceEx.Instance(company)
        '_instance = New XpoServiceEx(company)
    End Sub

    ''' <summary>
    ''' Referesca la configuración de la capa de datos
    ''' </summary>
    ''' <param name="company">Empresa a consultar</param>
    Public Sub RefreshDataLayer(ByVal company As String)
        DevExpress.Xpo.SimpleDataLayer.SuppressReentrancyAndThreadSafetyCheck = True
        'Dim dict As XPDictionary = New ReflectionDictionary()
        'dict.GetDataStoreSchema(System.Reflection.Assembly.GetExecutingAssembly())
        'DataLayer = New ThreadSafeDataLayer(dict, New WCFServiceDataStoreEx(endpointConfiguration, remoteAddress, company))
        DataLayer = New SimpleDataLayer(New WCFServiceDataStoreEx(endpointConfiguration, remoteAddress, company))
        XpoDefault.DataLayer = DataLayer 'New SimpleDataLayer(New WCFServiceDataStoreEx(endpointConfiguration, remoteAddress, company))
    End Sub

    ''' <summary>
    ''' metodo necesario para leer la configuracion xml de la aplicacion
    ''' </summary>
    Private Shared Sub ReadConfiguration()
        uriServiceEntitiesXpo = ConfigurationFile.Instance.UrlXpoWebServer
        protocolServicesXpo = ConfigurationFile.Instance.ProtocolUrlXpoWebServer
        _endpointNumber = GetEndPointNumber()
        remoteAddress = GetRemoteAddress()
        endpointConfiguration = GetEndPoint()
    End Sub

    ''' <summary>
    ''' Obtiene el numero de endpoint asignado al cliente
    ''' </summary>
    ''' <returns>Numero de endpoint</returns>
    Private Shared Function GetEndPointNumber() As Integer
        Dim srv As New WCFServiceDataStoreDefault($"{[Enum].GetName(GetType(Protocol), protocolServicesXpo)}_Endpoint_DefaultGate", System.String.Format("{0}XpoDefaultGate.svc", uriServiceEntitiesXpo))
        Return srv.GetEndpointNumber()
    End Function
    Private Shared Function CreateEndPoint() As EndpointAddress
        Dim address As New EndpointAddress(remoteAddress)
        Return address
    End Function
    Private Shared Function GetEndPoint() As String
        Return System.String.Format("{0}_Endpoint_XpoGateEx", [Enum].GetName(GetType(Protocol), protocolServicesXpo))
    End Function

    ''' <summary>
    ''' funcion para contatenar el remoteaddress por cada protocolo
    ''' </summary>
    ''' <returns>El Nombre del remoteaddress del Endpoint Correspondiente</returns>
    Private Shared Function GetRemoteAddress() As String
        Return System.String.Format("{0}XpoGateEx{1}.svc", uriServiceEntitiesXpo, _endpointNumber)
    End Function

#End Region

#Region "Fields"

    ''' <summary>
    ''' Numero maximo de metodos de servicio invocables antes
    ''' de ejecutar la función que libera memoria
    ''' </summary>
    Private Const MAX_REQUESTS As Int32 = 5

    ''' <summary>
    ''' Cuenta total de peticiones a servicios
    ''' </summary>
    Private _countRequests As Int32 = 0

#End Region

#Region "Members"

    Private _auditService As AuditRepository.AuditServicesXpoEx
    ''' <summary>
    ''' Obtiene acceso a los servicios de auditoria
    ''' </summary>
    Public ReadOnly Property AuditService As AuditRepository.AuditServicesXpoEx
        Get
            If Me._auditService Is Nothing Then
                _auditService = New AuditRepository.AuditServicesXpoEx()
            End If
            Return Me._auditService
        End Get
    End Property

    Private _securityService As SecurityRepository.SecurityServicesXpoEx
    ''' <summary>
    ''' Obtiene acceso a los servicios de seguridad
    ''' </summary>
    Public ReadOnly Property SecurityService As SecurityRepository.SecurityServicesXpoEx
        Get
            If Me._securityService Is Nothing Then
                _securityService = New SecurityRepository.SecurityServicesXpoEx()
            End If
            Return Me._securityService
        End Get
    End Property

    ''' <summary>
    ''' Obtiene acceso a los sevicios de gestion de cuentas
    ''' </summary>
    Private _accountManagementService As AccountManagementRespository.AccountManagementServiceXpoEx
    Public ReadOnly Property AccountManagementService As AccountManagementRespository.AccountManagementServiceXpoEx
        Get
            If Me._accountManagementService Is Nothing Then
                _accountManagementService = New AccountManagementRespository.AccountManagementServiceXpoEx()
            End If
            Return Me._accountManagementService
        End Get
    End Property

    ''' <summary>
    ''' Obtiene acceso a los servicios de contabilidad
    ''' </summary>
    Private _accountingService As AccountingRepository.AccountingServiceXpoEx
    Public ReadOnly Property AccountingService As AccountingRepository.AccountingServiceXpoEx
        Get
            If Me._accountingService Is Nothing Then
                _accountingService = New AccountingRepository.AccountingServiceXpoEx()
            End If
            Return Me._accountingService
        End Get
    End Property

    Private _billingService As BillingRepository.BillingServiceXpoEx
    ''' <summary>
    ''' Obtiene acceso a los servicios del modulo de facturación
    ''' </summary>
    Public ReadOnly Property BillingService As BillingRepository.BillingServiceXpoEx
        Get
            If Me._billingService Is Nothing Then
                _billingService = New BillingRepository.BillingServiceXpoEx()
            End If
            Return Me._billingService
        End Get
    End Property

    Private _budgetService As BudgetRepository.BudgetServicesXpoEx
    ''' <summary>
    ''' Obtiene acceso a los servicios del modulo de presupuesto
    ''' </summary>
    Public ReadOnly Property BudgetService As BudgetRepository.BudgetServicesXpoEx
        Get
            If Me._budgetService Is Nothing Then
                _budgetService = New BudgetRepository.BudgetServicesXpoEx()
            End If
            Return Me._budgetService
        End Get
    End Property
    Private _commonService As CommonRepository.CommonServicesXpoEx

    ''' <summary>
    ''' Obtiene acceso a los servicios del modulo comunes
    ''' </summary>
    Public ReadOnly Property CommonService As CommonRepository.CommonServicesXpoEx
        Get
            If Me._commonService Is Nothing Then
                _commonService = New CommonRepository.CommonServicesXpoEx()
            End If
            Return Me._commonService
        End Get
    End Property

    Private _contractService As ContractRepository.ContractServiceXpoEx
    ''' <summary>
    ''' Obtiene acceso a los servicios del modulo de contratos
    ''' </summary>
    Public ReadOnly Property ContractService As ContractRepository.ContractServiceXpoEx
        Get
            If Me._contractService Is Nothing Then
                _contractService = New ContractRepository.ContractServiceXpoEx()
            End If
            Return Me._contractService
        End Get
    End Property

    Private _crystalService As CrystalRepository.CrystalServiceXpoEx
    ''' <summary>
    ''' Obtiene acceso a los servicios de Indigo Crystal HIS
    ''' </summary>
    Public ReadOnly Property CrystalService As CrystalRepository.CrystalServiceXpoEx
        Get
            If Me._crystalService Is Nothing Then
                _crystalService = New CrystalRepository.CrystalServiceXpoEx()
            End If
            Return Me._crystalService
        End Get
    End Property

    Private _fixedAssetService As FixedAssetRepository.FixedAssetServiceXpoEx
    ''' <summary>
    ''' Obtiene acceso a los servicios del modulo de activos fijos
    ''' </summary>
    Public ReadOnly Property FixedAsset As FixedAssetRepository.FixedAssetServiceXpoEx
        Get
            If Me._fixedAssetService Is Nothing Then
                _fixedAssetService = New FixedAssetRepository.FixedAssetServiceXpoEx()
            End If
            Return Me._fixedAssetService
        End Get
    End Property

    Private _glosasService As GlosasRepository.GlosasServicesXpoEx
    ''' <summary>
    ''' Obtiene acceso a los servicios del modulo de glosas
    ''' </summary>
    Public ReadOnly Property GlosasService As GlosasRepository.GlosasServicesXpoEx
        Get
            If Me._glosasService Is Nothing Then
                _glosasService = New GlosasRepository.GlosasServicesXpoEx()
            End If
            Return Me._glosasService
        End Get
    End Property

    Private _interopCostService As InteropCostRepository.InteropCostServiceXpoEx
    ''' <summary>
    ''' Obtiene acceso a los servicios del modulo de costos
    ''' </summary>
    Public ReadOnly Property InteropCostService As InteropCostRepository.InteropCostServiceXpoEx
        Get
            If Me._interopCostService Is Nothing Then
                _interopCostService = New InteropCostRepository.InteropCostServiceXpoEx()
            End If
            Return Me._interopCostService
        End Get
    End Property

    Private _costService As CostRepository.CostServiceXpoEx
    ''' <summary>
    ''' Obtiene acceso a los servicios del modulo de costos
    ''' </summary>
    Public ReadOnly Property CostService As CostRepository.CostServiceXpoEx
        Get
            If Me._costService Is Nothing Then
                _costService = New CostRepository.CostServiceXpoEx()
            End If
            Return Me._costService
        End Get
    End Property

    Private _inventoryService As InventoryRepository.InventoryServiceXpoEx
    ''' <summary>
    ''' Obtiene acceso a los servicios del modulo de inventario
    ''' </summary>
    Public ReadOnly Property InventoryService As InventoryRepository.InventoryServiceXpoEx
        Get
            If Me._inventoryService Is Nothing Then
                _inventoryService = New InventoryRepository.InventoryServiceXpoEx()
            End If
            Return Me._inventoryService
        End Get
    End Property

    Private _medicalFeesService As MedicalFeesRepository.MedicalFeesServiceXpoEx
    ''' <summary>
    ''' Obtiene acceso a los servicios del modulo de honorarios medicos
    ''' </summary>
    Public ReadOnly Property MedicalFeesService As MedicalFeesRepository.MedicalFeesServiceXpoEx
        Get
            If Me._medicalFeesService Is Nothing Then
                _medicalFeesService = New MedicalFeesRepository.MedicalFeesServiceXpoEx()
            End If
            Return Me._medicalFeesService
        End Get
    End Property

    Private _paymentsService As PaymentsRepository.PaymentsServiceXpoEx
    ''' <summary>
    ''' Obtiene acceso a los servicios del modulo de pagos
    ''' </summary>
    Public ReadOnly Property PaymentsService As PaymentsRepository.PaymentsServiceXpoEx
        Get
            If Me._paymentsService Is Nothing Then
                _paymentsService = New PaymentsRepository.PaymentsServiceXpoEx()
            End If
            Return Me._paymentsService
        End Get
    End Property

    Private _taxesService As TaxesRepository.TaxesServiceXpoEx
    ''' <summary>
    ''' Obtiene acceso a los servicios del modulo de pagos
    ''' </summary>
    Public ReadOnly Property TaxesService As TaxesRepository.TaxesServiceXpoEx
        Get
            If Me._taxesService Is Nothing Then
                _taxesService = New TaxesRepository.TaxesServiceXpoEx()
            End If
            Return Me._taxesService
        End Get
    End Property

    Private _payrollService As PayrollRepository.PayrollServicesXpoEx
    ''' <summary>
    ''' Obtiene acceso a los servicios del modulo de nomina
    ''' </summary>
    Public ReadOnly Property PayrollService As PayrollRepository.PayrollServicesXpoEx
        Get
            If Me._payrollService Is Nothing Then
                _payrollService = New PayrollRepository.PayrollServicesXpoEx()
            End If
            Return Me._payrollService
        End Get
    End Property

    Private _portfolioService As PortfolioRepository.PortfolioServicesXpoEx
    ''' <summary>
    ''' Obtiene acceso a los servicios del modulo de cartera
    ''' </summary>
    Public ReadOnly Property PortfolioService As PortfolioRepository.PortfolioServicesXpoEx
        Get
            If Me._portfolioService Is Nothing Then
                _portfolioService = New PortfolioRepository.PortfolioServicesXpoEx()
            End If
            Return Me._portfolioService
        End Get
    End Property

    Private _treasuryService As TreasuryRepository.TreasuryServiceXpoEx
    ''' <summary>
    ''' Obtiene acceso a los servicios del modulo de tesoreria
    ''' </summary>
    Public ReadOnly Property TreasuryService As TreasuryRepository.TreasuryServiceXpoEx
        Get
            If Me._treasuryService Is Nothing Then
                _treasuryService = New TreasuryRepository.TreasuryServiceXpoEx()
            End If
            Return Me._treasuryService
        End Get
    End Property

    Private _maintenanceService As MaintenanceRepository.MaintenanceServicesXpoEx
    ''' <summary>
    ''' Obtiene acceso a los servicios del modulo de mantenimiento
    ''' </summary>
    Public ReadOnly Property MaintenanceService As MaintenanceRepository.MaintenanceServicesXpoEx
        Get
            If Me._maintenanceService Is Nothing Then
                _maintenanceService = New MaintenanceRepository.MaintenanceServicesXpoEx()
            End If
            Return Me._maintenanceService
        End Get
    End Property

    Private _documentalSystemService As DocumentalSystemRepository.DocumentalSystemServicesXpoEx
    ''' <summary>
    ''' Obtiene acceso a los servicios del modulo de gestion documental
    ''' </summary>
    Public ReadOnly Property DocumentalSystem As DocumentalSystemRepository.DocumentalSystemServicesXpoEx
        Get
            If Me._documentalSystemService Is Nothing Then
                _documentalSystemService = New DocumentalSystemRepository.DocumentalSystemServicesXpoEx()
            End If
            Return Me._documentalSystemService
        End Get
    End Property

    Private _mixingStationService As MixingStationRepository.MixingStationServiceXpoEx
    ''' <summary>
    ''' Obtiene acceso a los servicios del modulo de central de mezclas
    ''' </summary>
    Public ReadOnly Property MixingStationService As MixingStationRepository.MixingStationServiceXpoEx
        Get
            If Me._mixingStationService Is Nothing Then
                _mixingStationService = New MixingStationRepository.MixingStationServiceXpoEx()
            End If
            Return Me._mixingStationService
        End Get
    End Property

    Private _authorizationService As AuthorizationRepository.AuthorizationServiceXpoEx
    ''' <summary>
    ''' Obtiene acceso a los servicios del modulo de central de mezclas
    ''' </summary>
    Public ReadOnly Property AuthorizationService As AuthorizationRepository.AuthorizationServiceXpoEx
        Get
            If Me._authorizationService Is Nothing Then
                _authorizationService = New AuthorizationRepository.AuthorizationServiceXpoEx()
            End If
            Return Me._authorizationService
        End Get
    End Property

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            If XpoDefault.DataLayer.Connection IsNot Nothing AndAlso XpoDefault.DataLayer.Connection.State = ConnectionState.Open Then
                XpoDefault.DataLayer.Connection.Close()
            End If
            If _billingService IsNot Nothing Then
                _billingService.Dispose()
            End If
            If _commonService IsNot Nothing Then
                _commonService.Dispose()
            End If
            If _contractService IsNot Nothing Then
                _contractService.Dispose()
            End If
            If _crystalService IsNot Nothing Then
                _crystalService.Dispose()
            End If
            If _payrollService IsNot Nothing Then
                _payrollService.Dispose()
            End If
            If _accountingService IsNot Nothing Then
                _accountingService.Dispose()
            End If
            If _accountManagementService IsNot Nothing Then
                _accountManagementService.Dispose()
            End If
            If _auditService IsNot Nothing Then
                _auditService.Dispose()
            End If
            If _budgetService IsNot Nothing Then
                _budgetService.Dispose()
            End If
            If _documentalSystemService IsNot Nothing Then
                _documentalSystemService.Dispose()
            End If
            If _fixedAssetService IsNot Nothing Then
                _fixedAssetService.Dispose()
            End If
            If _glosasService IsNot Nothing Then
                _glosasService.Dispose()
            End If
            If _interopCostService IsNot Nothing Then
                _interopCostService.Dispose()
            End If
            If _inventoryService IsNot Nothing Then
                _inventoryService.Dispose()
            End If
            If _maintenanceService IsNot Nothing Then
                _maintenanceService.Dispose()
            End If
            If _medicalFeesService IsNot Nothing Then
                _medicalFeesService.Dispose()
            End If
            If _paymentsService IsNot Nothing Then
                _paymentsService.Dispose()
            End If
            If _portfolioService IsNot Nothing Then
                _portfolioService.Dispose()
            End If
            If _securityService IsNot Nothing Then
                _securityService.Dispose()
            End If
            If _treasuryService IsNot Nothing Then
                _treasuryService.Dispose()
            End If
            If _costService IsNot Nothing Then
                _costService.Dispose()
            End If
            ' TODO: set large fields to null.
            _billingService = Nothing
            _commonService = Nothing
            _contractService = Nothing
            _crystalService = Nothing
            _payrollService = Nothing
            _accountingService = Nothing
            _accountManagementService = Nothing
            _auditService = Nothing
            _budgetService = Nothing
            _documentalSystemService = Nothing
            _fixedAssetService = Nothing
            _glosasService = Nothing
            _interopCostService = Nothing
            _inventoryService = Nothing
            _maintenanceService = Nothing
            _medicalFeesService = Nothing
            _paymentsService = Nothing
            _portfolioService = Nothing
            _securityService = Nothing
            _treasuryService = Nothing
            _costService = Nothing
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