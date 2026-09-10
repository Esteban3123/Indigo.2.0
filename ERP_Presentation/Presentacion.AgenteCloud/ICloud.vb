#Region "Librerias Importadas"

Imports Presentation.CloudAgent.IndigoReference.Security
Imports Presentation.CloudAgent.IndigoReference.Common
Imports Presentation.CloudAgent.IndigoReference.Glosas
Imports Presentation.CloudAgent.IndigoReference.Payroll
Imports Presentation.CloudAgent.IndigoReference.CommonERP
Imports Presentation.CloudAgent.IndigoReference.Maintenance
Imports Presentation.CloudAgent.IndigoReference.Accounting
Imports Presentation.CloudAgent.IndigoReference.Treasury
Imports Presentation.CloudAgent.IndigoReference.Payments
Imports Presentation.CloudAgent.IndigoReference.DocumentalSystem
Imports Presentation.CloudAgent.IndigoReference.Indexing
Imports Presentation.CloudAgent.IndigoReference.FixedAsset
Imports Presentation.CloudAgent.IndigoReference.Portfolio
Imports Presentation.CloudAgent.IndigoReference.Budget
Imports Presentation.CloudAgent.IndigoReference.Inventory
Imports Presentation.CloudAgent.IndigoReference.Contract
Imports Presentation.CloudAgent.IndigoReference.Billing
Imports Presentation.CloudAgent.IndigoReference.InteropCost
Imports Presentation.CloudAgent.IndigoReference.MedicalFees
Imports Presentation.CloudAgent.IndigoReference.Crystal
Imports Presentation.CloudAgent.IndigoReference.FileManager
Imports Presentation.CloudAgent.IndigoReference.Cost
Imports Presentation.CloudAgent.IndigoReference.Taxes
Imports Presentation.CloudAgent.IndigoReference.MixingStation
Imports Presentation.CloudAgent.IndigoReference.Authorization
Imports Presentation.CloudAgent.IndigoReference.ElectronicDocuments
Imports Presentation.CloudAgent.IndigoReference.Admissions
Imports Presentation.CloudAgent.IndigoReference.AccountManagement

#End Region

Public Interface ICloud

#Region "Eventos"

    ''' <summary>
    ''' Se activa cuando un canal de algún servicio ha fallado
    ''' </summary>
    ''' <param name="sender">Objeto quien genero el evento</param>
    ''' <param name="e">Datos adicionales acerca del evento</param>
    Event FaultedChannel(ByVal sender As Object, ByVal e As FaultedChannelEventArgs)

#End Region

#Region "Propiedades"
    ''' <summary>
    ''' Obtiene el Objeto Indigo portfolio
    ''' </summary>
    ''' <value>Indigo Services.</value>
    ReadOnly Property IndigoPortfolio() As PortfolioServiceClient

    ''' <summary>
    ''' Obtiene el Objeto Indigo crystal
    ''' </summary>
    ''' <value>Indigo Services.</value>
    ReadOnly Property IndigoCrystal As CrystalServiceClient

    ''' <summary>
    ''' Obtiene el Objeto Indigo payments
    ''' </summary>
    ''' <value>Indigo Services.</value>
    ReadOnly Property IndigoPayments() As PaymentsServiceClient

    ''' <summary>
    ''' Obtiene el Objeto Indigo Treasury
    ''' </summary>
    ''' <value>Indigo Services.</value>
    ReadOnly Property IndigoTreasury() As TreasuryServiceClient

    ''' <summary>
    ''' Obtiene el Objeto Indigo MedicalFees
    ''' </summary>
    ''' <value>Indigo Services.</value>
    ReadOnly Property IndigoMedicalFees() As MedicalFeesServiceClient

    ''' <summary>
    ''' Obtiene el Objeto Indigo Billing
    ''' </summary>
    ''' <value>Indigo Services.</value>
    ReadOnly Property IndigoBilling() As BillingServiceClient

    ''' <summary>
    ''' Obtiene el Objeto Indigo Accounting
    ''' </summary>
    ''' <value>Indigo Services.</value>
    ReadOnly Property IndigoAccounting() As AccountingServiceClient

    ''' <summary>
    ''' Obtiene el objeto de Indigo AccountManagement
    ''' </summary>
    ''' <returns></returns>
    ReadOnly Property IndigoAccountManagement() As AccountManagementServiceClient

    ''' <summary>
    ''' Obtiene el Objeto Indigo Comunes
    ''' </summary>
    ''' <value>Indigo Services.</value>
    ReadOnly Property IndigoComunes() As CommonServiceClient

    ''' <summary>
    ''' Obtiene el Objeto Indigo Seguridad Default
    ''' </summary>
    ''' <value>Indigo Services.</value>
    ReadOnly Property IndigoSeguridadDefault() As Presentation.CloudAgent.IndigoReference.SecurityDefault.SecurityServiceClient

    ''' <summary>
    ''' Obtiene el Objeto Indigo Comunes
    ''' </summary>
    ''' <value>Indigo Services.</value>
    ReadOnly Property IndigoSeguridad() As SecurityServiceClient

    ''' <summary>
    ''' Obtiene el Objeto Indigo Glosas
    ''' </summary> 
    ''' <value>Indigo Services.</value>
    ReadOnly Property IndigoGlosas() As GlosasServiceClient

    ''' <summary>
    '''  Obtiene el objeto Indigo Payroll
    ''' </summary>
    ''' <value>Indigo Services</value>
    ReadOnly Property IndigoPayroll() As PayrollServiceClient

    ''' <summary>
    '''  Obtiene el objeto Indigo Payroll
    ''' </summary>
    ''' <value>Indigo Services</value>
    ReadOnly Property IndigoFixedAssets() As FixedAssetServiceClient

    ''' <summary>
    '''  Obtiene el objeto Indigo Payroll
    ''' </summary>
    ''' <value>Indigo Services</value>
    ReadOnly Property IndigoBudget() As BudgetServiceClient

    ''' <summary>
    '''  Obtiene el objeto Indigo Payroll
    ''' </summary>
    ''' <value>Indigo Services</value>
    ReadOnly Property IndigoCommonERP() As CommonERPServiceClient
    ''' <summary>
    '''  Obtiene el objeto Indigo Maintenance
    ''' </summary>
    ''' <value>Indigo Services</value>
    ReadOnly Property IndigoMaintenance() As MaintenanceServiceClient
    ''' <summary>
    '''  Obtiene el objeto Indigo DocumentalSystem
    ''' </summary>
    ''' <value>Indigo Services</value>
    ReadOnly Property IndigoDocumentalSystem() As DocumentalSystemServiceClient

    ''' <summary>
    ''' Obtiene la instancia al cliente del servicio de indexación
    ''' </summary>
    ReadOnly Property IndigoIndexing() As IndexingClient

    ''' <summary>
    ''' Obtiene la instancia al cliente del servicio de inventarios
    ''' </summary>
    ReadOnly Property IndigoInventory() As InventoryServiceClient

    ''' <summary>
    ''' Obtiene la instancia al cliente del servicio de costos
    ''' </summary>
    ReadOnly Property IndigoInteropCost() As InteropCostServiceClient

    ''' <summary>
    ''' Obtiene la instancia al cliente del servicio de costos
    ''' </summary>
    ReadOnly Property IndigoCost() As CostServiceClient

    ''' <summary>
    ''' Obtiene la instancia al cliente del servicio de inventarios
    ''' </summary>
    ReadOnly Property IndigoContract() As ContractServiceClient

    ''' <summary>
    ''' Obtiene la instancia al cliente del servicio de inventarios
    ''' </summary>
    ReadOnly Property IndigoTaxes() As TaxesServiceClient

    ''' <summary>
    ''' Obtiene la instancia al cliente del servicio de administración de archivos
    ''' </summary>
    ReadOnly Property IndigoFileManager() As FileManagerServiceClient

    ''' <summary>
    ''' Obtiene el objeto Indigo MixingStation
    ''' </summary>
    ReadOnly Property IndigoMixingStation() As MixingStationServiceClient

    ''' <summary>
    ''' Obtiene el objeto Indigo Authorization
    ''' </summary>
    ReadOnly Property IndigoAuthorization() As AuthorizationServiceClient

    ''' <summary>
    ''' Obtiene el objeto Indigo ElectronicDocument
    ''' </summary>
    ReadOnly Property IndigoElectronicDocument() As ElectronicDocumentsServiceClient

    ''' <summary>
    ''' Obtiene el objeto Indigo Admissions
    ''' </summary>
    ReadOnly Property IndigoAdmissions() As AdmissionsServiceClient

#End Region

End Interface

''' <summary>
''' Encapsula la información del canal que ha fallado
''' </summary>
Public Class FaultedChannelEventArgs
    Inherits EventArgs

    ''' <summary>
    ''' Obtiene o asigna el servicio al que pertenece el canal que ha fallado
    ''' </summary>
    ''' <value>Servicio al que pertenece el canal</value>
    ''' <returns>El servicio al que pertenece el canal</returns>
    Public Property Service As eServicios

    ''' <summary>
    ''' Genera una nueva instancia de la clase
    ''' </summary>
    Public Sub New(ByVal service As eServicios)
        Me.Service = service
    End Sub

End Class