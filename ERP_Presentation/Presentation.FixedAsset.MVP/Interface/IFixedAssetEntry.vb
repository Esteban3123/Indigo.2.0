'***********************************************************************
' Assembly         : Presentacion.FixedAsset.MVP
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 19-01-2016
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Base
Imports DevExpress.Xpo
Imports Presentation.Controls
Imports Domain.Entities

#End Region

Public Interface IFixedAssetEntry
    Inherits IcrudBase

    ''' <summary>
    ''' Código del ingreso del activo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Code As String

    ''' <summary>
    ''' Fecha del ingreso
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property EntryDate As DateTime?

    ''' <summary>
    ''' Numero del ingreso
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property EntryNumber As String

    ''' <summary>
    ''' Tipo de adquisición
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property AdquisitionType As Integer?

    ''' <summary>
    '''Id del Proveedor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property SupplierId As Integer?

    ''' <summary>
    ''' id de la linea de distribucion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property SupplierDistributionLineId As Integer?

    ''' <summary>
    ''' Datasource del proveedor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property SupplierDistributionLineXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Id del tipo de proveedor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property SupplierTypeId As Integer?

    ''' <summary>
    ''' Datasource del tipo de proveedor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property SupplierTypeXpo As List(Of SupplierType)

    ''' <summary>
    ''' Descripción
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Description As String

    ''' <summary>
    ''' Ubicación y responsable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property GetLocationResponsible As Integer?

    ''' <summary>
    ''' Localización
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property LocationId As Integer?

    ''' <summary>
    ''' Datasource de la localización
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property LocationXpo As XPCollection

    ''' <summary>
    ''' Responsable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ResponsibleId As Integer?

    ''' <summary>
    ''' Datasource del responsable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ResponsibleXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Tipo de redondeo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property RoundService As Integer?

    ''' <summary>
    ''' No. de factura
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property InvoiceNumber As String

    ''' <summary>
    ''' Fecha de factura
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property InvoiceDate As DateTime?

    ''' <summary>
    ''' Dias de plazo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property DayPeriod As Integer

    ''' <summary>
    ''' % de ICA
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IcaPercentage As Decimal

    ''' <summary>
    ''' Valor del flete
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property FreightValue As Decimal

    ''' <summary>
    ''' % del IVA flete
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property FreightIVAPercentage As Decimal

    ''' <summary>
    ''' Valor IVA flete
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property FreightIVAValue As Decimal

    ''' <summary>
    ''' Obteniene el tag del frontal
    ''' </summary>
    ''' <value>
    ''' My tag.
    ''' </value>
    ReadOnly Property MyTag As Object

    ''' <summary>
    ''' Obtiene o establece el layout para customizacion
    ''' </summary>
    ''' <value>
    ''' My layout control.
    ''' </value>
    ReadOnly Property MyLayoutControl As IndigoLayoutControl

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [actions on controls]; otherwise, <c>false</c>.
    ''' </value>
    WriteOnly Property ActionsOnControls As Boolean

    ''' <summary>
    ''' Obtiene o establece la secuencia de cabecera
    ''' </summary>
    ''' <value>
    ''' The sequense.
    ''' </value>
    Property Sequense As Domain.Entities.FixedAssetSequence

    ''' <summary>
    ''' Parametros de activos fijos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property SettingsFixedAsset As SettingFixedAsset

    ''' <summary>
    ''' Id del centro de costo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property CostCenterId As Integer?

    ''' <summary>
    ''' Datasource del centro costo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property CostCenterXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Numero de contrato leasing
    ''' </summary>
    ''' <returns></returns>
    Property NumberContractLeasing As String

    ''' <summary>
    ''' Fecha inicial leasing
    ''' </summary>
    ''' <returns></returns>
    Property InitialDateLeasing As Date?

    ''' <summary>
    ''' Fecha final leasing
    ''' </summary>
    ''' <returns></returns>
    Property EndDateLeasing As Date?

    ''' <summary>
    ''' Registro tipo IVA
    ''' </summary>
    ''' <returns></returns>
    Property TaxRegistration As Integer?


    ''' <summary>
    ''' Datasource del compromiso
    ''' </summary>
    ''' <returns></returns>
    Property CommitmentDetailXpo As XPCollection

    ''' <summary>
    ''' Datasource de entidades de presupuesto
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property BudgetaryEntityXpo As XPCollection

    ''' <summary>
    ''' Datasource de las vigencias de presupuesto
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property BudgetaryValidityXpo As XPCollection
    ''' <summary>
    ''' Propiedad que contiene el listado de autorizaciones de documento soporte xpo
    ''' </summary>
    Property DocumentSupportXpo As XPInstantFeedbackSource
    ''' <summary>
    ''' Datasource de actividad Economica
    ''' </summary>
    ''' <returns></returns>
    Property EconomicActivityDatasource As XPInstantFeedbackSource

End Interface
