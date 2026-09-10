'***********************************************************************
' Assembly         : Presentacion.Inventory
' Author           : Henry Alejandro Vargas Polania 
' Created          : 26/12/2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports DevExpress.Xpo
Imports Domain.Entities
Imports Presentation.Base
Imports Presentation.Controls

#End Region

Public Interface IInventoryContract
    Inherits IcrudBase

#Region "Variables"

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property MyLayoutControl As IndigoLayoutControl

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <returns>Tag del formulario</returns>
    ReadOnly Property MyTag As Object

    ''' <summary>
    ''' Obtiene o asigna la secuencia numerica del formulario
    ''' </summary>
    ''' <value>Secuencia numerica del formulario</value>
    ''' <returns>La secuencia numerica del formulario</returns>
    Property Sequense As Domain.Entities.InventorySequence

#End Region

#Region "Properties"

    Property Code As String

    Property ContractTypeId As Integer

    Property DocumentDate As DateTime?

    Property InitialDate As DateTime?

    Property EndDate As DateTime?

    Property SupplierId As Integer

    Property SupplierDistributionLineId As Integer

    Property ContractNumber As String

    Property Description As String

    Property PaymentMethod As String

    Property DeliveryMethod As String

    Property DeliveryPlace As String

    Property SourceOrder As Integer

    Property PurchaseProcess As Integer

    Property Exclusivity As Boolean?

    Property ManageProducts As Boolean?

    Property OnlyGuarantee As Boolean?

    Property TechnicalSupervicion As String

    Property SupervisionExecution As String

    Property Clauses As String

    Property Attachments As String

    Property Availability As String

    Property Resolution As String

    Property ResolutionDate As DateTime?

    Property QuoteNumber As String

    Property QuoteDate As DateTime?

    Property RecordNumber As String

    Property RecordDate As DateTime?

    Property NegotiationType As String

    Property Approved As String

    Property Deadline As DateTime?

    Property ValidityDate As DateTime?

    Property Value As Decimal

    Property DiscountValue As Decimal

    Property IvaValue As Decimal

    Property TotalValue As Decimal

    Property Status As Byte

    ''' <summary>
    ''' obtiene o establece el Id de la moneda del documento
    ''' </summary>
    ''' <returns></returns>
    Property CurrencyId(Optional CurrencyAbbreviation As String = Nothing) As Integer

    ''' <summary>
    ''' Obtiene o establece el datasource del combo de moneda
    ''' </summary>
    ''' <returns></returns>
    Property CurrencyDatasource As XPInstantFeedbackSource

    Property ListSupplier As XPInstantFeedbackSource

    Property ListContractType As XPInstantFeedbackSource

#End Region

#Region "XPO"

    Property SuppliersDistributionLinesXpo As XPInstantFeedbackSource

#Region "Budget Interface"

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
    ''' Establece el datasource de las disponibilidades
    ''' </summary>
    ''' <returns></returns>
    Property AvailabilityXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Disponibilidades asociadas al contrato 
    ''' </summary>
    ''' <returns></returns>
    Property ListContractAvailability As List(Of InventoryContractAvailability)

#End Region

#End Region

End Interface
