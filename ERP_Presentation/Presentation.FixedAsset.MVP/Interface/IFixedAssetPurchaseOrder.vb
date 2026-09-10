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

Imports DevExpress.Xpo
Imports Domain.Entities
Imports Presentation.Base
Imports Presentation.Controls

#End Region

Public Interface IFixedAssetPurchaseOrder
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
    ''' Obtiene o establece la secuencia de cabecera
    ''' </summary>
    ''' <value>
    ''' The sequense.
    ''' </value>
    Property Sequense As Domain.Entities.FixedAssetSequence

#End Region

#Region "Properties"

    ''' <summary>
    ''' codigo de la remision
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Code As String

    ''' <summary>
    ''' fecha de la remision
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property PurchaseOrderDate As DateTime?

    ''' <summary>
    ''' id de la linea de distribucion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property SupplierDistributionLineId As Integer?

    ''' <summary>
    ''' obtiene o establece el Id de la moneda del documento
    ''' </summary>
    ''' <returns></returns>
    Property CurrencyId(Optional CurrencyAbbreviation As String = Nothing) As Integer

    ''' <summary>
    ''' detalle de la remision
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Description As String

    ''' <summary>
    ''' numero de la cotizacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property QuotationNumber As String

    ''' <summary>
    ''' Tipo de orden
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property OrderType As Integer?

    ''' <summary>
    ''' otro tipo de orden
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property OtherOrderType As String

    ''' <summary>
    ''' Forma de pago
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property WayPay As String

    ''' <summary>
    ''' Garantia
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Warranty As String

    ''' <summary>
    ''' Lugar de entrega
    ''' </summary>
    Property DeliveryPlace As String

    ''' <summary>
    ''' Dias de plazo
    ''' </summary>
    Property DeadlineDays As String

    ''' <summary>
    ''' Fecha de entrega
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property DeliverDate As DateTime?

    ''' <summary>
    ''' unidad funcional que solicita
    ''' </summary>
    ''' <returns></returns>
    Property RequestedFunctionalUnitId As Integer?

#End Region

#Region "XPO"

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
    ''' Establece el datasource de las unidad funcional que solicita
    ''' </summary>
    ''' <returns></returns>
    Property RequestedFunctionalUnitXPO As XPInstantFeedbackSource

    ''' <summary>
    ''' Disponibilidades asociadas al contrato 
    ''' </summary>
    ''' <returns></returns>
    Property ListPurchaseOrderAvailability As List(Of FixedAssetPurchaseOrderAvailability)

    ''' <summary>
    ''' Obtiene o establece el datasource del combo de moneda
    ''' </summary>
    ''' <returns></returns>
    Property CurrencyDatasource As XPInstantFeedbackSource

#End Region

#End Region

End Interface
