
#Region "Librerias Importadas"

Imports SecurityDefault = Presentation.CloudAgent.IndigoReference.SecurityDefault
Imports Presentation.CloudAgent.IndigoReference.Security
Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent.IndigoReference.Common
Imports Presentation.CloudAgent.IndigoReference.Glosas
Imports Presentation.CloudAgent.IndigoReference.Payroll
Imports Presentation.CloudAgent.IndigoReference.CommonERP
Imports Presentation.CloudAgent.IndigoReference.Maintenance
Imports Presentation.CloudAgent.IndigoReference.DocumentalSystem
Imports Presentation.CloudAgent.IndigoReference.Indexing
Imports Presentation.CloudAgent.IndigoReference.Accounting
Imports Presentation.CloudAgent.IndigoReference.Treasury
Imports Presentation.CloudAgent.IndigoReference.Payments
Imports System.IO
Imports Presentation.CloudAgent.IndigoReference.Portfolio
Imports Presentation.CloudAgent.IndigoReference.Inventory
Imports Presentation.CloudAgent.IndigoReference.Contract
Imports Presentation.CloudAgent.IndigoReference.Billing
Imports Presentation.CloudAgent.IndigoReference.InteropCost
Imports Presentation.CloudAgent.IndigoReference.MedicalFees
Imports Presentation.CloudAgent.IndigoReference.Crystal
Imports Presentation.CloudAgent.IndigoReference.FileManager
Imports Presentation.CloudAgent.IndigoReference.Cost
Imports Presentation.CloudAgent.IndigoReference.Taxes
Imports Presentation.CloudAgent
Imports Presentation.CloudAgent.IndigoReference.MixingStation
Imports Presentation.CloudAgent.IndigoReference.Authorization
Imports Presentation.CloudAgent.IndigoReference.ElectronicDocuments
Imports Presentation.CloudAgent.IndigoReference.Admissions
Imports Presentation.CloudAgent.IndigoReference.AccountManagement

#End Region

Public Class IndigoConect
    Implements IDisposable, ICloud

#Region "Variables"

    Dim Indigo As SessionValues = SessionValues.Instance

#End Region

#Region "Eventos"

    ''' <summary>
    ''' Se activa cuando un canal de algún servicio ha fallado
    ''' </summary>
    ''' <param name="sender">Objeto quien genero el evento</param>
    ''' <param name="e">Datos adicionales acerca del evento</param>
    Public Event FaultedChannel(sender As Object, e As FaultedChannelEventArgs) Implements ICloud.FaultedChannel

#End Region

#Region "Propiedades"

    Private _IndigoAccountManagement As AccountManagementServiceClient
    ''' <summary>
    ''' Obtiene el Objeto Indigo Accounting
    ''' </summary>
    ''' <value>Objeto Indigo Accounting</value>
    ''' <returns>Devuelve el Objeto Indigo Accounting</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property IndigoAccountManagement As AccountManagementServiceClient Implements ICloud.IndigoAccountManagement
        Get
            Return Me._IndigoAccountManagement
        End Get
    End Property

    Private _IndigoAccounting As AccountingServiceClient
    ''' <summary>
    ''' Obtiene el Objeto Indigo Accounting
    ''' </summary>
    ''' <value>Objeto Indigo Accounting</value>
    ''' <returns>Devuelve el Objeto Indigo Accounting</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property IndigoAccounting As IndigoReference.Accounting.AccountingServiceClient Implements ICloud.IndigoAccounting
        Get
            Return Me._IndigoAccounting
        End Get
    End Property

    Private _IndigoTreasury As TreasuryServiceClient

    ''' <summary>
    ''' Obtiene el Objeto Indigo Treasury
    ''' </summary>
    ''' <value>
    ''' Indigo Services.
    ''' </value>
    Public ReadOnly Property IndigoTreasury As IndigoReference.Treasury.TreasuryServiceClient Implements ICloud.IndigoTreasury
        Get
            Return _IndigoTreasury
        End Get
    End Property

    Private _IndigoMedicalFees As MedicalFeesServiceClient

    ''' <summary>
    ''' Obtiene el Objeto Indigo Treasury
    ''' </summary>
    ''' <value>
    ''' Indigo Services.
    ''' </value>
    Public ReadOnly Property IndigoMedicalFees As IndigoReference.MedicalFees.MedicalFeesServiceClient Implements ICloud.IndigoMedicalFees
        Get
            Return _IndigoMedicalFees
        End Get
    End Property

    Private _IndigoInteropCost As InteropCostServiceClient

    ''' <summary>
    ''' Obtiene el Objeto Indigo InteropCost
    ''' </summary>
    ''' <value>
    ''' Indigo Services.
    ''' </value>
    Public ReadOnly Property IndigoInteropCost As IndigoReference.InteropCost.InteropCostServiceClient Implements ICloud.IndigoInteropCost
        Get
            Return _IndigoInteropCost
        End Get
    End Property

    Private _IndigoCost As CostServiceClient

    ''' <summary>
    ''' Obtiene el Objeto Indigo InteropCost
    ''' </summary>
    ''' <value>
    ''' Indigo Services.
    ''' </value>
    Public ReadOnly Property IndigoCost As IndigoReference.Cost.CostServiceClient Implements ICloud.IndigoCost
        Get
            Return _IndigoCost
        End Get
    End Property

    Private _IndigoBilling As BillingServiceClient

    ''' <summary>
    ''' Obtiene el Objeto Indigo Billing
    ''' </summary>
    ''' <value>
    ''' Indigo Services.
    ''' </value>
    Public ReadOnly Property IndigoBilling As IndigoReference.Billing.BillingServiceClient Implements ICloud.IndigoBilling
        Get
            Return _IndigoBilling
        End Get
    End Property

    Private _IndigoCrystal As CrystalServiceClient
    ''' <summary>
    ''' Obtiene el Objeto Indigo Crystal
    ''' </summary>
    ''' <value>
    ''' Indigo Services.
    ''' </value>
    Public ReadOnly Property IndigoCrystal As IndigoReference.Crystal.CrystalServiceClient Implements ICloud.IndigoCrystal
        Get
            Return _IndigoCrystal
        End Get
    End Property

    Private _IndigoPayments As PaymentsServiceClient
    Public ReadOnly Property IndigoPayments As IndigoReference.Payments.PaymentsServiceClient Implements ICloud.IndigoPayments
        Get
            Return Me._IndigoPayments
        End Get
    End Property

    Private _IndigoComunes As CommonServiceClient
    ''' <summary>
    ''' Obtiene el Objeto Indigo Comunes
    ''' </summary>
    ''' <value>Objeto Indigo Comunes</value>
    ''' <returns>Devuelve el Objeto Indigo Comunes</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property IndigoComunes As CommonServiceClient Implements ICloud.IndigoComunes
        Get
            Return _IndigoComunes
        End Get
    End Property

    Private _IndigoSeguridadDefault As SecurityDefault.SecurityServiceClient
    ''' <summary>
    ''' Obtiene el Objeto Indigo Seguridad
    ''' </summary>
    ''' <value>Objeto Indigo Seguridad</value>
    ''' <returns>Devuelve el Objeto Indigo Seguridad</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property IndigoSeguridadDefault As SecurityDefault.SecurityServiceClient Implements ICloud.IndigoSeguridadDefault
        Get
            Return _IndigoSeguridadDefault
        End Get
    End Property

    Private _IndigoSeguridad As SecurityServiceClient
    ''' <summary>
    ''' Obtiene el Objeto Indigo Seguridad
    ''' </summary>
    ''' <value>Objeto Indigo Seguridad</value>
    ''' <returns>Devuelve el Objeto Indigo Seguridad</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property IndigoSeguridad As IndigoReference.Security.SecurityServiceClient Implements ICloud.IndigoSeguridad
        Get
            Return _IndigoSeguridad
        End Get
    End Property
    Private _IndigoGlosas As IndigoReference.Glosas.GlosasServiceClient
    ''' <summary>
    ''' Obtiene el Objeto Indigo Glosas
    ''' </summary>
    ''' <value>Objeto Indigo Seguridad</value>
    ''' <returns>Devuelve el Objeto Indigo Glosas</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property IndigoGlosas As IndigoReference.Glosas.GlosasServiceClient Implements ICloud.IndigoGlosas
        Get
            Return _IndigoGlosas
        End Get
    End Property

    Private _IndigoPayroll As IndigoReference.Payroll.PayrollServiceClient
    ''' <summary>
    ''' Obtiene el objeto indigo Payroll
    ''' </summary>
    ''' <value>Objeto indigo Payroll</value>
    ''' <returns>Devuelve el objeto indigo Payroll</returns>
    Public ReadOnly Property IndigoPayroll As IndigoReference.Payroll.PayrollServiceClient Implements ICloud.IndigoPayroll
        Get
            Return _IndigoPayroll
        End Get
    End Property

    Private _IndigoCommonERP As CommonERPServiceClient
    ''' <summary>
    ''' Obtiene el objeto indigo Payroll
    ''' </summary>
    ''' <value>Objeto indigo Payroll</value>
    ''' <returns>Devuelve el objeto indigo Payroll</returns>
    Public ReadOnly Property IndigoCommonERP As CommonERPServiceClient Implements ICloud.IndigoCommonERP
        Get
            Return _IndigoCommonERP
        End Get
    End Property

    Private _IndigoMaintenance As MaintenanceServiceClient
    ''' <summary>
    ''' Obtiene el objeto indigo Maintenance
    ''' </summary>
    ''' <value>Objeto indigo Maintenance</value>
    ''' <returns>Devuelve el objeto indigo Maintenance</returns>
    Public ReadOnly Property IndigoMaintenance As MaintenanceServiceClient Implements ICloud.IndigoMaintenance
        Get
            Return _IndigoMaintenance
        End Get
    End Property

    Private _IndigoFixedAssets As IndigoReference.FixedAsset.FixedAssetServiceClient
    ''' <summary>
    ''' Obtiene el objeto Indigo activos fijos
    ''' </summary>
    ''' <value>
    ''' Indigo Services
    ''' </value>
    Public ReadOnly Property IndigoFixedAssets As IndigoReference.FixedAsset.FixedAssetServiceClient Implements ICloud.IndigoFixedAssets
        Get
            Return _IndigoFixedAssets
        End Get
    End Property

    Private _IndigoPortfolio As IndigoReference.Portfolio.PortfolioServiceClient
    ''' <summary>
    ''' Obtiene el Objeto Indigo portfolio
    ''' </summary>
    ''' <value>
    ''' Indigo Services.
    ''' </value>
    Public ReadOnly Property IndigoPortfolio As IndigoReference.Portfolio.PortfolioServiceClient Implements ICloud.IndigoPortfolio
        Get
            Return _IndigoPortfolio
        End Get
    End Property

    Private _IndigoBudget As IndigoReference.Budget.BudgetServiceClient
    ''' <summary>
    ''' Obtiene el Objeto Indigo presupuesto
    ''' </summary>
    ''' <value>
    ''' Indigo Services.
    ''' </value>
    Public ReadOnly Property IndigoBudget As IndigoReference.Budget.BudgetServiceClient Implements ICloud.IndigoBudget
        Get
            Return Me._IndigoBudget
        End Get
    End Property

    Private _IndigoDocumentalSystem As DocumentalSystemServiceClient
    ''' <summary>
    ''' Obtiene el objeto indigo DocumentalSystem
    ''' </summary>
    ''' <value>Objeto indigo DocumentalSystem</value>
    ''' <returns>Devuelve el objeto indigo DocumentalSystem</returns>
    Public ReadOnly Property IndigoDocumentalSystem As DocumentalSystemServiceClient Implements ICloud.IndigoDocumentalSystem
        Get
            Return _IndigoDocumentalSystem
        End Get
    End Property

    Private _IndigoIndexing As IndexingClient
    ''' <summary>
    ''' Obtiene la instancia al cliente del servicio de indexación
    ''' </summary>
    Public ReadOnly Property IndigoIndexing As IndigoReference.Indexing.IndexingClient Implements ICloud.IndigoIndexing
        Get
            Return _IndigoIndexing
        End Get
    End Property

    Private _IndigoInventory As InventoryServiceClient
    ''' <summary>
    ''' Obtiene el objeto indigo inventory
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property IndigoInventory As IndigoReference.Inventory.InventoryServiceClient Implements ICloud.IndigoInventory
        Get
            Return _IndigoInventory
        End Get
    End Property

    Private _IndigoContract As ContractServiceClient
    ''' <summary>
    ''' Obtiene el objeto indigo contract
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property IndigoContract As IndigoReference.Contract.ContractServiceClient Implements ICloud.IndigoContract
        Get
            Return _IndigoContract
        End Get
    End Property

    Private _IndigoTaxes As TaxesServiceClient
    ''' <summary>
    ''' Obtiene el objeto indigo contract
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property IndigoTaxes As IndigoReference.Taxes.TaxesServiceClient Implements ICloud.IndigoTaxes
        Get
            Return _IndigoTaxes
        End Get
    End Property

    Private _IndigoFileManager As FileManagerServiceClient
    ''' <summary>
    ''' Obtiene la unica instancia al cliente del servicio de administración de archivos
    ''' </summary>
    Public ReadOnly Property IndigoFileManager As IndigoReference.FileManager.FileManagerServiceClient Implements ICloud.IndigoFileManager
        Get
            Return Me._IndigoFileManager
        End Get
    End Property
    Private _IndigoMixingStation As MixingStationServiceClient

    Public ReadOnly Property IndigoMixingStation As IndigoReference.MixingStation.MixingStationServiceClient Implements ICloud.IndigoMixingStation
        Get
            Return Me._IndigoMixingStation
        End Get
    End Property

    Private _IndigoAuthorization As AuthorizationServiceClient
    ''' <summary>
    ''' Obtiene el Objeto Indigo Autorizaciones
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property IndigoAuthorization As AuthorizationServiceClient Implements ICloud.IndigoAuthorization
        Get
            Return Me._IndigoAuthorization
        End Get
    End Property

    Private _IndigoElectronicDocument As ElectronicDocumentsServiceClient
    ''' <summary>
    ''' Obtiene el Objeto Indigo Documentos Electronicos
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property IndigoElectronicDocument As ElectronicDocumentsServiceClient Implements ICloud.IndigoElectronicDocument
        Get
            Return Me._IndigoElectronicDocument
        End Get
    End Property

    Private _IndigoAdmissions As AdmissionsServiceClient
    ''' <summary>
    ''' Obtiene el Objeto Indigo Documentos Electronicos
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property IndigoAdmissions As AdmissionsServiceClient Implements ICloud.IndigoAdmissions
        Get
            Return Me._IndigoAdmissions
        End Get
    End Property

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                _IndigoComunes.Close()
                _IndigoSeguridad.Close()
                If _IndigoSeguridadDefault IsNot Nothing Then
                    _IndigoSeguridadDefault.Close()
                End If
                _IndigoGlosas.Close()
                _IndigoPayroll.Close()
                _IndigoCommonERP.Close()
                _IndigoDocumentalSystem.Close()
                _IndigoInventory.Close()
                _IndigoContract.Close()
                _IndigoInteropCost.Close()
                _IndigoMedicalFees.Close()
                _IndigoCost.Close()
                _IndigoAuthorization.Close()
                _IndigoElectronicDocument.Close()
                _IndigoAccountManagement.Close()
            End If
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

#Region "Constructores"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase <see cref="Cloud" />.
    ''' </summary>
    Public Sub New()
        'inicializo instancias de cada uno de los servicios
        InicializeServices()
    End Sub

    Public Sub New(ByVal _serviceDefault As Boolean)
        _IndigoSeguridadDefault = New SecurityDefault.SecurityServiceClient(GetEndPointSecurity(Protocol.basicHttp, eServicios.SecurityDefault), GetRemoteAddressSecurity(Protocol.basicHttp, eServicios.SecurityDefault))
        _IndigoSeguridadDefault.Endpoint.EndpointBehaviors.Add(New UnityMessageBehavior())
        AddHandler _IndigoSeguridadDefault.InnerChannel.Faulted, AddressOf FaultedConectionSeguridadDefault
    End Sub

    ''' <summary>
    ''' Inicializa los services de acuerdo a un protocolo selccionado previamente.
    ''' </summary>
    Private Sub InicializeServices()

        System.Net.ServicePointManager.SecurityProtocol = Net.SecurityProtocolType.Tls13 Or Net.SecurityProtocolType.Tls12
        _IndigoPayments = New PaymentsServiceClient(GetEndPoint(CType(Indigo.WebServiceProtocol, Protocol), eServicios.Payments), GetRemoteAddress(CType(Indigo.WebServiceProtocol, Protocol), eServicios.Payments))
        _IndigoPayments.Endpoint.EndpointBehaviors.Add(New UnityMessageBehavior())
        _IndigoAccounting = New AccountingServiceClient(GetEndPoint(CType(Indigo.WebServiceProtocol, Protocol), eServicios.Accounting), GetRemoteAddress(CType(Indigo.WebServiceProtocol, Protocol), eServicios.Accounting))
        _IndigoAccounting.Endpoint.EndpointBehaviors.Add(New UnityMessageBehavior())
        _IndigoSeguridad = New SecurityServiceClient(GetEndPoint(CType(Indigo.WebServiceProtocol, Protocol), eServicios.Security), GetRemoteAddress(CType(Indigo.WebServiceProtocol, Protocol), eServicios.Security))
        _IndigoSeguridad.Endpoint.EndpointBehaviors.Add(New UnityMessageBehavior())
        _IndigoComunes = New CommonServiceClient(GetEndPoint(CType(Indigo.WebServiceProtocol, Protocol), eServicios.Common), GetRemoteAddress(CType(Indigo.WebServiceProtocol, Protocol), eServicios.Common))
        _IndigoGlosas = New GlosasServiceClient(GetEndPoint(CType(Indigo.WebServiceProtocol, Protocol), eServicios.Glosas), GetRemoteAddress(CType(Indigo.WebServiceProtocol, Protocol), eServicios.Glosas))
        _IndigoGlosas.Endpoint.EndpointBehaviors.Add(New UnityMessageBehavior)
        _IndigoPayroll = New PayrollServiceClient(GetEndPoint(CType(Indigo.WebServiceProtocol, Protocol), eServicios.Payroll), GetRemoteAddress(CType(Indigo.WebServiceProtocol, Protocol), eServicios.Payroll))
        _IndigoPayroll.Endpoint.EndpointBehaviors.Add(New UnityMessageBehavior)
        _IndigoTreasury = New TreasuryServiceClient(GetEndPoint(CType(Indigo.WebServiceProtocol, Protocol), eServicios.Treasury), GetRemoteAddress(CType(Indigo.WebServiceProtocol, Protocol), eServicios.Treasury))
        _IndigoTreasury.Endpoint.EndpointBehaviors.Add(New UnityMessageBehavior())

        _IndigoInteropCost = New InteropCostServiceClient(GetEndPoint(CType(Indigo.WebServiceProtocol, Protocol), eServicios.InteropCost), GetRemoteAddress(CType(Indigo.WebServiceProtocol, Protocol), eServicios.InteropCost))
        _IndigoInteropCost.Endpoint.EndpointBehaviors.Add(New UnityMessageBehavior())
        _IndigoCost = New CostServiceClient(GetEndPoint(CType(Indigo.WebServiceProtocol, Protocol), eServicios.Cost), GetRemoteAddress(CType(Indigo.WebServiceProtocol, Protocol), eServicios.Cost))
        _IndigoCost.Endpoint.EndpointBehaviors.Add(New UnityMessageBehavior())

        _IndigoMedicalFees = New MedicalFeesServiceClient(GetEndPoint(CType(Indigo.WebServiceProtocol, Protocol), eServicios.MedicalFees), GetRemoteAddress(CType(Indigo.WebServiceProtocol, Protocol), eServicios.MedicalFees))
        _IndigoMedicalFees.Endpoint.EndpointBehaviors.Add(New UnityMessageBehavior())

        _IndigoBilling = New BillingServiceClient(GetEndPoint(CType(Indigo.WebServiceProtocol, Protocol), eServicios.Billing), GetRemoteAddress(CType(Indigo.WebServiceProtocol, Protocol), eServicios.Billing))
        _IndigoBilling.Endpoint.EndpointBehaviors.Add(New UnityMessageBehavior())

        _IndigoCrystal = New CrystalServiceClient(GetEndPoint(CType(Indigo.WebServiceProtocol, Protocol), eServicios.Crystal), GetRemoteAddress(CType(Indigo.WebServiceProtocol, Protocol), eServicios.Crystal))
        _IndigoCrystal.Endpoint.EndpointBehaviors.Add(New UnityMessageBehavior())

        _IndigoPortfolio = New PortfolioServiceClient(GetEndPoint(CType(Indigo.WebServiceProtocol, Protocol), eServicios.Portfolio), GetRemoteAddress(CType(Indigo.WebServiceProtocol, Protocol), eServicios.Portfolio))
        _IndigoPortfolio.Endpoint.EndpointBehaviors.Add(New UnityMessageBehavior())
        _IndigoFixedAssets = New IndigoReference.FixedAsset.FixedAssetServiceClient(GetEndPoint(CType(Indigo.WebServiceProtocol, Protocol), eServicios.FixedAssets), GetRemoteAddress(CType(Indigo.WebServiceProtocol, Protocol), eServicios.FixedAssets))
        _IndigoFixedAssets.Endpoint.EndpointBehaviors.Add(New UnityMessageBehavior())
        _IndigoBudget = New IndigoReference.Budget.BudgetServiceClient(GetEndPoint(CType(Indigo.WebServiceProtocol, Protocol), eServicios.Budget), GetRemoteAddress(CType(Indigo.WebServiceProtocol, Protocol), eServicios.Budget))
        _IndigoBudget.Endpoint.EndpointBehaviors.Add(New UnityMessageBehavior())
        _IndigoCommonERP = New CommonERPServiceClient(GetEndPoint(CType(Indigo.WebServiceProtocol, Protocol), eServicios.CommonERP), GetRemoteAddress(CType(Indigo.WebServiceProtocol, Protocol), eServicios.CommonERP))
        _IndigoCommonERP.Endpoint.EndpointBehaviors.Add(New UnityMessageBehavior())
        _IndigoMaintenance = New MaintenanceServiceClient(GetEndPoint(CType(Indigo.WebServiceProtocol, Protocol), eServicios.Maintenance), GetRemoteAddress(CType(Indigo.WebServiceProtocol, Protocol), eServicios.Maintenance))
        _IndigoMaintenance.Endpoint.EndpointBehaviors.Add(New UnityMessageBehavior())

        _IndigoDocumentalSystem = New DocumentalSystemServiceClient(GetEndPoint(CType(Indigo.DocumentalSystemWebServiceProtocol, Protocol), eServicios.DocumentalSystem), GetRemoteAddress(CType(Indigo.DocumentalSystemWebServiceProtocol, Protocol), eServicios.DocumentalSystem, Indigo.UriServerDocumentalSystem))
        _IndigoIndexing = New IndexingClient(GetEndPoint(CType(Indigo.IndexingWebServiceProtocol, Protocol), eServicios.Indexing), GetRemoteAddress(CType(Indigo.IndexingWebServiceProtocol, Protocol), eServicios.Indexing, Indigo.UriIndexingWebService))
        _IndigoInventory = New InventoryServiceClient(GetEndPoint(CType(Indigo.WebServiceProtocol, Protocol), eServicios.Inventory), GetRemoteAddress(CType(Indigo.WebServiceProtocol, Protocol), eServicios.Inventory))
        _IndigoInventory.Endpoint.EndpointBehaviors.Add(New UnityMessageBehavior())
        _IndigoContract = New ContractServiceClient(GetEndPoint(CType(Indigo.WebServiceProtocol, Protocol), eServicios.Contract), GetRemoteAddress(CType(Indigo.WebServiceProtocol, Protocol), eServicios.Contract))
        _IndigoContract.Endpoint.EndpointBehaviors.Add(New UnityMessageBehavior())

        _IndigoFileManager = New FileManagerServiceClient(GetEndPoint(CType(Indigo.WebServiceProtocol, Protocol), eServicios.FileManager), GetRemoteAddress(CType(Indigo.WebServiceProtocol, Protocol), eServicios.FileManager))
        _IndigoFileManager.Endpoint.EndpointBehaviors.Add(New UnityMessageBehavior())

        _IndigoTaxes = New TaxesServiceClient(GetEndPoint(CType(Indigo.WebServiceProtocol, Protocol), eServicios.Taxes), GetRemoteAddress(CType(Indigo.WebServiceProtocol, Protocol), eServicios.Taxes))
        _IndigoTaxes.Endpoint.EndpointBehaviors.Add(New UnityMessageBehavior())

        _IndigoMixingStation = New MixingStationServiceClient(GetEndPoint(CType(Indigo.WebServiceProtocol, Protocol), eServicios.MixingStation), GetRemoteAddress(CType(Indigo.WebServiceProtocol, Protocol), eServicios.MixingStation))
        _IndigoMixingStation.Endpoint.EndpointBehaviors.Add(New UnityMessageBehavior())

        _IndigoAuthorization = New AuthorizationServiceClient(GetEndPoint(CType(Indigo.WebServiceProtocol, Protocol), eServicios.Authorization), GetRemoteAddress(CType(Indigo.WebServiceProtocol, Protocol), eServicios.Authorization))
        _IndigoAuthorization.Endpoint.EndpointBehaviors.Add(New UnityMessageBehavior())

        _IndigoElectronicDocument = New ElectronicDocumentsServiceClient(GetEndPoint(CType(Indigo.WebServiceProtocol, Protocol), eServicios.ElectronicDocuments), GetRemoteAddress(CType(Indigo.WebServiceProtocol, Protocol), eServicios.ElectronicDocuments))
        _IndigoElectronicDocument.Endpoint.EndpointBehaviors.Add(New UnityMessageBehavior())

        _IndigoAdmissions = New AdmissionsServiceClient(GetEndPoint(CType(Indigo.WebServiceProtocol, Protocol), eServicios.Admissions), GetRemoteAddress(CType(Indigo.WebServiceProtocol, Protocol), eServicios.Admissions))
        _IndigoAdmissions.Endpoint.EndpointBehaviors.Add(New UnityMessageBehavior())

        _IndigoAccountManagement = New AccountManagementServiceClient(GetEndPoint(CType(Indigo.WebServiceProtocol, Protocol), eServicios.AccountManagement), GetRemoteAddress(CType(Indigo.WebServiceProtocol, Protocol), eServicios.AccountManagement))
        _IndigoAccountManagement.Endpoint.EndpointBehaviors.Add(New UnityMessageBehavior())

        'manejadores
        AddHandler _IndigoPayments.InnerChannel.Faulted, AddressOf FaultedConectionPayments
        AddHandler _IndigoAccounting.InnerChannel.Faulted, AddressOf FaultedConectionAccounting
        AddHandler _IndigoTreasury.InnerChannel.Faulted, AddressOf FaultedConectionTreasury

        AddHandler _IndigoInteropCost.InnerChannel.Faulted, AddressOf FaultedConectionInteropCost
        AddHandler _IndigoCost.InnerChannel.Faulted, AddressOf FaultedConectionCost
        AddHandler _IndigoMedicalFees.InnerChannel.Faulted, AddressOf FaultedConectionMedicalFees

        AddHandler _IndigoBilling.InnerChannel.Faulted, AddressOf FaultedConectionBilling
        AddHandler _IndigoCrystal.InnerChannel.Faulted, AddressOf FaultedConectionCrystal
        AddHandler _IndigoPortfolio.InnerChannel.Faulted, AddressOf FaultedConectionPortfolio
        AddHandler _IndigoFixedAssets.InnerChannel.Faulted, AddressOf FaultedConectionFixedAsset
        AddHandler _IndigoSeguridad.InnerChannel.Faulted, AddressOf FaultedConectionSeguridad
        AddHandler _IndigoComunes.InnerChannel.Faulted, AddressOf FaultedConectioneComunes
        AddHandler _IndigoCommonERP.InnerChannel.Faulted, AddressOf FaultedConectioneCommonERP
        AddHandler _IndigoGlosas.InnerChannel.Faulted, AddressOf FaultedConectioneGlosas
        AddHandler _IndigoPayroll.InnerChannel.Faulted, AddressOf FaultedConectionePayroll
        AddHandler _IndigoMaintenance.InnerChannel.Faulted, AddressOf FaultedConectionMaintenance
        AddHandler _IndigoDocumentalSystem.InnerChannel.Faulted, AddressOf FaultedConectionDocumentalSystem
        AddHandler _IndigoIndexing.InnerChannel.Faulted, AddressOf FaultedConectionIndexingService
        AddHandler _IndigoBudget.InnerChannel.Faulted, AddressOf FaultedConectionBudget
        AddHandler _IndigoInventory.InnerChannel.Faulted, AddressOf FaultedConectionInventory
        AddHandler _IndigoContract.InnerChannel.Faulted, AddressOf FaultedConectionContract
        AddHandler _IndigoFileManager.InnerChannel.Faulted, AddressOf FaultedConectionFileManager
        AddHandler _IndigoTaxes.InnerChannel.Faulted, AddressOf FaultedConectionTaxes
        AddHandler _IndigoMixingStation.InnerChannel.Faulted, AddressOf FaultedConectionMixingStation
        AddHandler _IndigoAuthorization.InnerChannel.Faulted, AddressOf FaultedConectionAuthorization
        AddHandler _IndigoElectronicDocument.InnerChannel.Faulted, AddressOf FaultedConectionElectronicDocument
        AddHandler _IndigoAdmissions.InnerChannel.Faulted, AddressOf FaultedConectionAdmissionsDocument
        AddHandler _IndigoAccountManagement.InnerChannel.Faulted, AddressOf FaultedConectionAccountManagement
    End Sub

#End Region

#Region "Metodos y Funciones"

    ''' <summary>
    ''' funcion para concatenar el nombre del endpoint por cada protocolo y servicio correspondiente
    ''' </summary>
    ''' <param name="Protocolo">el protocolo.</param>
    ''' <param name="Servicio">El servicio a utilizar</param>
    ''' <returns>El Nombre de la configuracion del Endpoint Correspondiente</returns>
    Private Function GetEndPoint(ByVal Protocolo As Protocol, ByVal Servicio As eServicios) As String
        Return String.Format("{0}_Endpoint_{1}", [Enum].GetName(GetType(Protocol), Protocolo), [Enum].GetName(GetType(eServicios), Servicio))
    End Function

    ''' <summary>
    ''' funcion para concatenar el nombre del endpoint por cada protocolo y servicio correspondiente
    ''' </summary>
    ''' <param name="Protocolo">el protocolo.</param>
    ''' <param name="Servicio">El servicio a utilizar</param>
    ''' <returns>El Nombre de la configuracion del Endpoint Correspondiente</returns>
    Private Function GetEndPointSecurity(ByVal Protocolo As Protocol, ByVal Servicio As eServicios) As String
        Dim _protocoloName As String
        Protocolo = Protocol.basicHttp
        If Indigo.UriWebSecurityServices.ToUpper.Contains("HTTPS") Then
            Protocolo = Protocol.wsHttp
        ElseIf Indigo.UriWebSecurityServices.ToUpper.Contains("NET.TCP") Then
            Protocolo = Protocol.netTcp
        End If
        _protocoloName = [Enum].GetName(GetType(Protocol), Protocolo)
        If Protocolo = Protocol.wsHttp Then
            _protocoloName = String.Format("{0}s", _protocoloName)
        End If
        Return String.Format("{0}_Endpoint_{1}", _protocoloName, [Enum].GetName(GetType(eServicios), Servicio))
    End Function

    ''' <summary>
    ''' funcion para contatenar el remoteaddress por cada protocolo y servicio correspondiente
    ''' </summary>
    ''' <param name="Protocolo">el protocolo.</param>
    ''' <param name="Servicio">El servicio a utilizar</param>
    ''' <returns>El Nombre del remoteaddress del Endpoint Correspondiente</returns>
    Private Function GetRemoteAddress(ByVal Protocolo As Protocol, ByVal Servicio As eServicios, Optional ByVal Uri As String = "") As String
        If Uri Is Nothing OrElse Uri.Trim().Equals(String.Empty) Then
            Return String.Format("{0}{1}.svc/{2}{3}", Indigo.UriWebServices, [Enum].GetName(GetType(eServicios), Servicio), [Enum].GetName(GetType(Protocol), Protocolo), [Enum].GetName(GetType(eServicios), Servicio))
        Else
            Return String.Format("{0}{1}.svc/{2}{3}", Uri.Trim(), [Enum].GetName(GetType(eServicios), Servicio), [Enum].GetName(GetType(Protocol), Protocolo), [Enum].GetName(GetType(eServicios), Servicio))
        End If
    End Function

    ''' <summary>
    ''' funcion para contatenar el remoteaddress por cada protocolo y servicio correspondiente
    ''' </summary>
    ''' <param name="Protocolo">el protocolo.</param>
    ''' <param name="Servicio">El servicio a utilizar</param>
    ''' <returns>El Nombre del remoteaddress del Endpoint Correspondiente</returns>
    Private Function GetRemoteAddressSecurity(ByVal Protocolo As Protocol, ByVal Servicio As eServicios, Optional ByVal Uri As String = "") As String
        Dim _protocoloName As String

        If Indigo.UriWebSecurityServices.ToUpper.Contains("HTTPS") Then
            Protocolo = Protocol.wsHttp
        ElseIf Indigo.UriWebSecurityServices.ToUpper.Contains("NET.TCP") Then
            Protocolo = Protocol.netTcp
        End If
        _protocoloName = [Enum].GetName(GetType(Protocol), Protocolo)
        If Protocolo = Protocol.wsHttp Then
            _protocoloName = String.Format("{0}s", _protocoloName)
        End If
        If Uri Is Nothing OrElse Uri.Trim().Equals(String.Empty) Then
            Return String.Format("{0}{1}.svc/{2}{3}", Indigo.UriWebSecurityServices, [Enum].GetName(GetType(eServicios), Servicio), _protocoloName, [Enum].GetName(GetType(eServicios), Servicio))
        Else
            Return String.Format("{0}{1}.svc/{2}{3}", Uri.Trim(), [Enum].GetName(GetType(eServicios), Servicio), _protocoloName, [Enum].GetName(GetType(eServicios), Servicio))
        End If
    End Function

#End Region

#Region "Faulted Conection"
    Private Sub FaultedConectionAccountManagement(ByVal sender As Object, ByVal e As EventArgs)
        'aborto
        _IndigoAccountManagement.InnerChannel.Abort()
        'Recreo el canal
        _IndigoAccountManagement = New AccountManagementServiceClient(GetEndPoint(CType(Indigo.WebServiceProtocol, Protocol), eServicios.Accounting), GetRemoteAddress(CType(Indigo.WebServiceProtocol, Protocol), eServicios.AccountManagement))
        'Agrego el manejador
        AddHandler _IndigoAccounting.InnerChannel.Faulted, AddressOf FaultedConectionAccountManagement
        RaiseEvent FaultedChannel(Me, New FaultedChannelEventArgs(eServicios.Accounting))
    End Sub

    Private Sub FaultedConectionAccounting(ByVal sender As Object, ByVal e As EventArgs)
        'aborto
        _IndigoAccounting.InnerChannel.Abort()
        'Recreo el canal
        _IndigoAccounting = New AccountingServiceClient(GetEndPoint(CType(Indigo.WebServiceProtocol, Protocol), eServicios.Accounting), GetRemoteAddress(CType(Indigo.WebServiceProtocol, Protocol), eServicios.Accounting))
        'Agrego el manejador
        AddHandler _IndigoAccounting.InnerChannel.Faulted, AddressOf FaultedConectionAccounting
        RaiseEvent FaultedChannel(Me, New FaultedChannelEventArgs(eServicios.Accounting))
    End Sub

    Private Sub FaultedConectionPayments(ByVal sender As Object, ByVal e As EventArgs)
        'aborto
        _IndigoPayments.InnerChannel.Abort()
        'Recreo el canal
        _IndigoPayments = New PaymentsServiceClient(GetEndPoint(CType(Indigo.WebServiceProtocol, Protocol), eServicios.Payments), GetRemoteAddress(CType(Indigo.WebServiceProtocol, Protocol), eServicios.Payments))
        'Agrego el manejador
        AddHandler _IndigoPayments.InnerChannel.Faulted, AddressOf FaultedConectionPayments
        RaiseEvent FaultedChannel(Me, New FaultedChannelEventArgs(eServicios.Payments))
    End Sub

    Private Sub FaultedConectionPortfolio(ByVal sender As Object, ByVal e As EventArgs)
        'aborto
        _IndigoPortfolio.InnerChannel.Abort()
        'Recreo el canal
        _IndigoPortfolio = New PortfolioServiceClient(GetEndPoint(CType(Indigo.WebServiceProtocol, Protocol), eServicios.Portfolio), GetRemoteAddress(CType(Indigo.WebServiceProtocol, Protocol), eServicios.Portfolio))
        'Agrego el manejador
        AddHandler _IndigoPortfolio.InnerChannel.Faulted, AddressOf FaultedConectionPortfolio
        RaiseEvent FaultedChannel(Me, New FaultedChannelEventArgs(eServicios.Portfolio))
    End Sub

    Private Sub FaultedConectionTreasury(ByVal sender As Object, ByVal e As EventArgs)
        'aborto
        _IndigoTreasury.InnerChannel.Abort()
        'Recreo el canal
        _IndigoTreasury = New TreasuryServiceClient(GetEndPoint(CType(Indigo.WebServiceProtocol, Protocol), eServicios.Treasury), GetRemoteAddress(CType(Indigo.WebServiceProtocol, Protocol), eServicios.Treasury))
        'Agrego el manejador
        AddHandler _IndigoTreasury.InnerChannel.Faulted, AddressOf FaultedConectionTreasury
        RaiseEvent FaultedChannel(Me, New FaultedChannelEventArgs(eServicios.Treasury))
    End Sub

    Private Sub FaultedConectionInteropCost(ByVal sender As Object, ByVal e As EventArgs)
        'aborto
        _IndigoInteropCost.InnerChannel.Abort()
        'Recreo el canal
        _IndigoInteropCost = New InteropCostServiceClient(GetEndPoint(CType(Indigo.WebServiceProtocol, Protocol), eServicios.InteropCost), GetRemoteAddress(CType(Indigo.WebServiceProtocol, Protocol), eServicios.InteropCost))
        'Agrego el manejador
        AddHandler _IndigoInteropCost.InnerChannel.Faulted, AddressOf FaultedConectionInteropCost
        RaiseEvent FaultedChannel(Me, New FaultedChannelEventArgs(eServicios.InteropCost))
    End Sub

    Private Sub FaultedConectionCost(ByVal sender As Object, ByVal e As EventArgs)
        'aborto
        _IndigoCost.InnerChannel.Abort()
        'Recreo el canal
        _IndigoCost = New CostServiceClient(GetEndPoint(CType(Indigo.WebServiceProtocol, Protocol), eServicios.Cost), GetRemoteAddress(CType(Indigo.WebServiceProtocol, Protocol), eServicios.Cost))
        'Agrego el manejador
        AddHandler _IndigoCost.InnerChannel.Faulted, AddressOf FaultedConectionCost
        RaiseEvent FaultedChannel(Me, New FaultedChannelEventArgs(eServicios.Cost))
    End Sub

    Private Sub FaultedConectionMedicalFees(ByVal sender As Object, ByVal e As EventArgs)
        'aborto
        _IndigoMedicalFees.InnerChannel.Abort()
        'Recreo el canal
        _IndigoMedicalFees = New MedicalFeesServiceClient(GetEndPoint(CType(Indigo.WebServiceProtocol, Protocol), eServicios.MedicalFees), GetRemoteAddress(CType(Indigo.WebServiceProtocol, Protocol), eServicios.MedicalFees))
        'Agrego el manejador
        AddHandler _IndigoMedicalFees.InnerChannel.Faulted, AddressOf FaultedConectionMedicalFees
        RaiseEvent FaultedChannel(Me, New FaultedChannelEventArgs(eServicios.MedicalFees))
    End Sub

    Private Sub FaultedConectionBilling(sender As Object, e As EventArgs)
        'aborto
        _IndigoBilling.InnerChannel.Abort()
        'Recreo el canal
        _IndigoBilling = New BillingServiceClient(GetEndPoint(CType(Indigo.WebServiceProtocol, Protocol), eServicios.Billing), GetRemoteAddress(CType(Indigo.WebServiceProtocol, Protocol), eServicios.Billing))
        'Agrego el manejador
        AddHandler _IndigoBilling.InnerChannel.Faulted, AddressOf FaultedConectionBilling
        RaiseEvent FaultedChannel(Me, New FaultedChannelEventArgs(eServicios.Billing))
    End Sub

    Private Sub FaultedConectionFixedAsset(ByVal sender As Object, ByVal e As EventArgs)
        'aborto
        _IndigoFixedAssets.Abort()
        'Recreo el canal
        _IndigoFixedAssets = New IndigoReference.FixedAsset.FixedAssetServiceClient(GetEndPoint(CType(Indigo.WebServiceProtocol, Protocol), eServicios.FixedAssets), GetRemoteAddress(CType(Indigo.WebServiceProtocol, Protocol), eServicios.FixedAssets))
        'Agrego el manejador
        AddHandler _IndigoFixedAssets.InnerChannel.Faulted, AddressOf FaultedConectionFixedAsset
        RaiseEvent FaultedChannel(Me, New FaultedChannelEventArgs(eServicios.FixedAssets))
    End Sub

    Private Sub FaultedConectionSeguridad(ByVal sender As Object, ByVal e As EventArgs)
        'aborto
        _IndigoSeguridad.InnerChannel.Abort()
        'Recreo el canal
        _IndigoSeguridad = New SecurityServiceClient(GetEndPoint(CType(Indigo.WebServiceProtocol, Protocol), eServicios.Security), GetRemoteAddress(CType(Indigo.WebServiceProtocol, Protocol), eServicios.Security))
        'Agrego el manejador
        AddHandler _IndigoSeguridad.InnerChannel.Faulted, AddressOf FaultedConectionSeguridad
        RaiseEvent FaultedChannel(Me, New FaultedChannelEventArgs(eServicios.Security))
    End Sub

    Private Sub FaultedConectionSeguridadDefault(ByVal sender As Object, ByVal e As EventArgs)
        'aborto
        _IndigoSeguridadDefault.InnerChannel.Abort()
        'Recreo el canal
        _IndigoSeguridadDefault = New SecurityDefault.SecurityServiceClient(GetEndPointSecurity(Protocol.basicHttp, eServicios.SecurityDefault), GetRemoteAddressSecurity(Protocol.basicHttp, eServicios.SecurityDefault))
        'Agrego el manejador
        AddHandler _IndigoSeguridadDefault.InnerChannel.Faulted, AddressOf FaultedConectionSeguridadDefault
        RaiseEvent FaultedChannel(Me, New FaultedChannelEventArgs(eServicios.SecurityDefault))
    End Sub

    Private Sub FaultedConectioneComunes(ByVal sender As Object, ByVal e As EventArgs)
        'aborto
        _IndigoComunes.InnerChannel.Abort()
        'creo el nuevo canal
        _IndigoComunes = New CommonServiceClient(GetEndPoint(CType(Indigo.WebServiceProtocol, Protocol), eServicios.Common), GetRemoteAddress(CType(Indigo.WebServiceProtocol, Protocol), eServicios.Common))
        'creo el manejador
        AddHandler _IndigoComunes.InnerChannel.Faulted, AddressOf FaultedConectioneComunes
        RaiseEvent FaultedChannel(Me, New FaultedChannelEventArgs(eServicios.Common))
    End Sub

    Private Sub FaultedConectioneGlosas(ByVal sender As Object, ByVal e As EventArgs)
        'aborto
        _IndigoGlosas.InnerChannel.Abort()
        'creo el nuevo canal
        _IndigoGlosas = New GlosasServiceClient(GetEndPoint(CType(Indigo.WebServiceProtocol, Protocol), eServicios.Glosas), GetRemoteAddress(CType(Indigo.WebServiceProtocol, Protocol), eServicios.Glosas))
        'creo el manejador
        AddHandler _IndigoGlosas.InnerChannel.Faulted, AddressOf FaultedConectioneComunes
        RaiseEvent FaultedChannel(Me, New FaultedChannelEventArgs(eServicios.Glosas))
    End Sub

    Private Sub FaultedConectionePayroll(ByVal sender As Object, ByVal e As EventArgs)
        'aborto
        _IndigoPayroll.InnerChannel.Abort()
        'creo el nuevo canal
        _IndigoPayroll = New PayrollServiceClient(GetEndPoint(CType(Indigo.WebServiceProtocol, Protocol), eServicios.Payroll), GetRemoteAddress(CType(Indigo.WebServiceProtocol, Protocol), eServicios.Payroll))
        'creo el manejador
        AddHandler _IndigoPayroll.InnerChannel.Faulted, AddressOf FaultedConectionePayroll
        RaiseEvent FaultedChannel(Me, New FaultedChannelEventArgs(eServicios.Payroll))
    End Sub

    Private Sub FaultedConectioneCommonERP(ByVal sender As Object, ByVal e As EventArgs)
        'aborto
        _IndigoCommonERP.InnerChannel.Abort()
        'creo el nuevo canal
        _IndigoCommonERP = New CommonERPServiceClient(GetEndPoint(CType(Indigo.WebServiceProtocol, Protocol), eServicios.CommonERP), GetRemoteAddress(CType(Indigo.WebServiceProtocol, Protocol), eServicios.CommonERP))
        'creo el manejador
        AddHandler _IndigoCommonERP.InnerChannel.Faulted, AddressOf FaultedConectioneCommonERP
        RaiseEvent FaultedChannel(Me, New FaultedChannelEventArgs(eServicios.CommonERP))
    End Sub

    Private Sub FaultedConectionMaintenance(sender As Object, e As EventArgs)
        'aborto
        _IndigoMaintenance.InnerChannel.Abort()
        'creo el nuevo canal
        _IndigoMaintenance = New MaintenanceServiceClient(GetEndPoint(CType(Indigo.WebServiceProtocol, Protocol), eServicios.Maintenance), GetRemoteAddress(CType(Indigo.WebServiceProtocol, Protocol), eServicios.Maintenance))
        'creo el manejador
        AddHandler _IndigoMaintenance.InnerChannel.Faulted, AddressOf FaultedConectionMaintenance
        RaiseEvent FaultedChannel(Me, New FaultedChannelEventArgs(eServicios.Maintenance))
    End Sub

    Private Sub FaultedConectionBudget(sender As Object, e As EventArgs)
        'aborto
        _IndigoBudget.InnerChannel.Abort()
        'creo el nuevo canal
        _IndigoBudget = New IndigoReference.Budget.BudgetServiceClient(GetEndPoint(CType(Indigo.WebServiceProtocol, Protocol), eServicios.Budget), GetRemoteAddress(CType(Indigo.WebServiceProtocol, Protocol), eServicios.Budget))
        'creo el manejador
        AddHandler _IndigoBudget.InnerChannel.Faulted, AddressOf FaultedConectionBudget
        RaiseEvent FaultedChannel(Me, New FaultedChannelEventArgs(eServicios.Budget))
    End Sub

    Private Sub FaultedConectionInventory(sender As Object, e As EventArgs)
        'aborto
        _IndigoInventory.InnerChannel.Abort()
        'creo el nuevo canal
        _IndigoInventory = New IndigoReference.Inventory.InventoryServiceClient(GetEndPoint(CType(Indigo.WebServiceProtocol, Protocol), eServicios.Inventory), GetRemoteAddress(CType(Indigo.WebServiceProtocol, Protocol), eServicios.Inventory))
        'creo el manejador
        AddHandler _IndigoInventory.InnerChannel.Faulted, AddressOf FaultedConectionInventory
        RaiseEvent FaultedChannel(Me, New FaultedChannelEventArgs(eServicios.Inventory))
    End Sub

    Private Sub FaultedConectionContract(sender As Object, e As EventArgs)
        'aborto
        _IndigoContract.InnerChannel.Abort()
        'creo el nuevo canal
        _IndigoContract = New IndigoReference.Contract.ContractServiceClient(GetEndPoint(CType(Indigo.WebServiceProtocol, Protocol), eServicios.Contract), GetRemoteAddress(CType(Indigo.WebServiceProtocol, Protocol), eServicios.Contract))
        'creo el manejador
        AddHandler _IndigoContract.InnerChannel.Faulted, AddressOf FaultedConectionContract
        RaiseEvent FaultedChannel(Me, New FaultedChannelEventArgs(eServicios.Contract))
    End Sub

    Private Sub FaultedConectionFileManager(sender As Object, e As EventArgs)
        'aborto
        _IndigoFileManager.InnerChannel.Abort()
        'creo el nuevo canal
        _IndigoFileManager = New IndigoReference.FileManager.FileManagerServiceClient(GetEndPoint(CType(Indigo.WebServiceProtocol, Protocol), eServicios.FileManager), GetRemoteAddress(CType(Indigo.WebServiceProtocol, Protocol), eServicios.FileManager))
        'creo el manejador
        AddHandler _IndigoContract.InnerChannel.Faulted, AddressOf FaultedConectionContract
        RaiseEvent FaultedChannel(Me, New FaultedChannelEventArgs(eServicios.Contract))
    End Sub

    Private Sub FaultedConectionTaxes(sender As Object, e As EventArgs)
        'aborto
        _IndigoTaxes.InnerChannel.Abort()
        'creo el nuevo canal
        _IndigoTaxes = New IndigoReference.Taxes.TaxesServiceClient(GetEndPoint(CType(Indigo.WebServiceProtocol, Protocol), eServicios.Taxes), GetRemoteAddress(CType(Indigo.WebServiceProtocol, Protocol), eServicios.Taxes))
        'creo el manejador
        AddHandler _IndigoTaxes.InnerChannel.Faulted, AddressOf FaultedConectionTaxes
        RaiseEvent FaultedChannel(Me, New FaultedChannelEventArgs(eServicios.Taxes))
    End Sub

    Private Sub FaultedConectionDocumentalSystem(sender As Object, e As EventArgs)
        'aborto
        _IndigoDocumentalSystem.InnerChannel.Abort()
        'creo el nuevo canal
        _IndigoDocumentalSystem = New DocumentalSystemServiceClient(GetEndPoint(CType(Indigo.DocumentalSystemWebServiceProtocol, Protocol), eServicios.DocumentalSystem), GetRemoteAddress(CType(Indigo.DocumentalSystemWebServiceProtocol, Protocol), eServicios.DocumentalSystem, Indigo.UriServerDocumentalSystem))
        'creo el manejador
        AddHandler _IndigoDocumentalSystem.InnerChannel.Faulted, AddressOf FaultedConectionDocumentalSystem
        RaiseEvent FaultedChannel(Me, New FaultedChannelEventArgs(eServicios.DocumentalSystem))
    End Sub

    Private Sub FaultedConectionIndexingService(sender As Object, e As EventArgs)
        'aborto
        _IndigoIndexing.InnerChannel.Abort()
        'creo el nuevo canal
        _IndigoIndexing = New IndexingClient(GetEndPoint(CType(Indigo.IndexingWebServiceProtocol, Protocol), eServicios.Indexing), GetRemoteAddress(CType(Indigo.IndexingWebServiceProtocol, Protocol), eServicios.Indexing, Indigo.UriIndexingWebService))
        'creo el manejador
        AddHandler _IndigoIndexing.InnerChannel.Faulted, AddressOf FaultedConectionIndexingService
        RaiseEvent FaultedChannel(Me, New FaultedChannelEventArgs(eServicios.Indexing))
    End Sub

    Private Sub FaultedConectionCrystal(sender As Object, e As EventArgs)
        'aborto
        _IndigoCrystal.InnerChannel.Abort()
        'Recreo el canal
        _IndigoCrystal = New CrystalServiceClient(GetEndPoint(CType(Indigo.WebServiceProtocol, Protocol), eServicios.Crystal), GetRemoteAddress(CType(Indigo.WebServiceProtocol, Protocol), eServicios.Crystal))
        'Agrego el manejador
        AddHandler _IndigoCrystal.InnerChannel.Faulted, AddressOf FaultedConectionCrystal
        RaiseEvent FaultedChannel(Me, New FaultedChannelEventArgs(eServicios.Crystal))
    End Sub

    Private Sub FaultedConectionMixingStation(sender As Object, e As EventArgs)
        'aborto
        _IndigoMixingStation.InnerChannel.Abort()
        'creo el nuevo canal
        _IndigoMixingStation = New IndigoReference.MixingStation.MixingStationServiceClient(GetEndPoint(CType(Indigo.WebServiceProtocol, Protocol), eServicios.MixingStation), GetRemoteAddress(CType(Indigo.WebServiceProtocol, Protocol), eServicios.MixingStation))
        'creo el manejador
        AddHandler _IndigoMixingStation.InnerChannel.Faulted, AddressOf FaultedConectionMixingStation
        RaiseEvent FaultedChannel(Me, New FaultedChannelEventArgs(eServicios.MixingStation))
    End Sub

    Private Sub FaultedConectionAuthorization(sender As Object, e As EventArgs)
        'aborto
        _IndigoAuthorization.InnerChannel.Abort()
        'creo el nuevo canal
        _IndigoAuthorization = New IndigoReference.Authorization.AuthorizationServiceClient(GetEndPoint(CType(Indigo.WebServiceProtocol, Protocol), eServicios.Authorization), GetRemoteAddress(CType(Indigo.WebServiceProtocol, Protocol), eServicios.Authorization))
        'creo el manejador
        AddHandler _IndigoAuthorization.InnerChannel.Faulted, AddressOf FaultedConectionAuthorization
        RaiseEvent FaultedChannel(Me, New FaultedChannelEventArgs(eServicios.Authorization))
    End Sub

    Private Sub FaultedConectionElectronicDocument(sender As Object, e As EventArgs)
        'aborto
        _IndigoElectronicDocument.InnerChannel.Abort()
        'creo el nuevo canal
        _IndigoElectronicDocument = New IndigoReference.ElectronicDocuments.ElectronicDocumentsServiceClient(GetEndPoint(CType(Indigo.WebServiceProtocol, Protocol), eServicios.ElectronicDocuments), GetRemoteAddress(CType(Indigo.WebServiceProtocol, Protocol), eServicios.ElectronicDocuments))
        'creo el manejador
        AddHandler _IndigoElectronicDocument.InnerChannel.Faulted, AddressOf FaultedConectionElectronicDocument
        RaiseEvent FaultedChannel(Me, New FaultedChannelEventArgs(eServicios.ElectronicDocuments))
    End Sub

    Private Sub FaultedConectionAdmissionsDocument(sender As Object, e As EventArgs)
        'aborto
        _IndigoAdmissions.InnerChannel.Abort()
        'creo el nuevo canal
        _IndigoAdmissions = New AdmissionsServiceClient(GetEndPoint(CType(Indigo.WebServiceProtocol, Protocol), eServicios.Admissions), GetRemoteAddress(CType(Indigo.WebServiceProtocol, Protocol), eServicios.Admissions))
        'creo el manejador
        AddHandler _IndigoAdmissions.InnerChannel.Faulted, AddressOf FaultedConectionAdmissionsDocument
        RaiseEvent FaultedChannel(Me, New FaultedChannelEventArgs(eServicios.Admissions))
    End Sub

#End Region



End Class
