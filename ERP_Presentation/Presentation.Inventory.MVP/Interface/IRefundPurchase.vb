'***********************************************************************
' Assembly         : Presentacion.Inventory
' Author           : Henry Alejandro Vargas Polania 
' Created          : 17/02/2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Librerias Importadas"
Imports Presentation.Base
Imports DevExpress.Xpo
Imports Presentation.Controls
#End Region

Public Interface IRefundPurchase
    Inherits ICrudBase

#Region "Properties"

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <returns>Tag del formulario</returns>
    ReadOnly Property MyTag As Object

    ''' <summary>
    ''' Esta propiedad establece el valor MyLayoutControl
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
    ''' Obtiene o asigna la secuencia numerica del formulario
    ''' </summary>
    ''' <value>Secuencia numerica del formulario</value>
    ''' <returns>La secuencia numerica del formulario</returns>
    Property Sequense As Domain.Entities.InventorySequence

    ''' <summary>
    ''' Obtiene o establece el consecutivo de la devolución de compra
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Code As String

    ''' <summary>
    ''' Obtiene o establece la fecha del documento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property DocumentDate As DateTime?

    ''' <summary>
    ''' Obtiene o establece una descripcion de la devolución de compra
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Description As String

    ''' <summary>
    ''' Obtiene o establece el almacen de la devolución de compra
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property WarehouseId As Integer?

    ''' <summary>
    ''' Obtiene o establece una descripcion de la devolución de compra
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property EntranceVoucherId As Integer?

    ''' <summary>
    ''' Nivel de redondeo usado en el comprobante de entrada
    ''' </summary>
    ''' <returns></returns>
    Property RoundingValue As Integer

    ''' <summary>
    ''' Indica si se esta realizando una devolución parcial o total del comprobante de entrada
    ''' </summary>
    ''' <returns></returns>
    Property DevolutionType As Boolean?

    ''' <summary>
    ''' Obtiene o establece el estado del registro
    ''' </summary>
    ''' <returns></returns>
    Property Status As Byte

#Region "Devolution Values"

    ''' <summary>
    ''' Valor Flete
    ''' </summary>
    ''' <returns></returns>
    Property FreightValue As Decimal

    ''' <summary>
    ''' Porcentaje de IVA del Flete
    ''' </summary>
    ''' <returns></returns>
    Property FreightIVAPercentage As Decimal

    ''' <summary>
    ''' Valor de Iva del Flete
    ''' </summary>
    ''' <returns></returns>
    Property FreightIVAValue As Decimal

    ''' <summary>
    ''' Obtiene o establece el subtotal del registro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Value As Decimal

    ''' <summary>
    ''' Obtiene o establece el valor del descuento del registro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ValueDiscount As Decimal

    ''' <summary>
    ''' Obtiene o establce el valor del iva del registro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ValueTax As Decimal

    ''' <summary>
    ''' Obtiene o establece el valor del porcentaje de retencion de iva del registro
    ''' </summary>
    ''' <remarks></remarks>
    Property WithholdingTaxPercentaje As Decimal

    ''' <summary>
    ''' Obtiene o establece el valor de la retencion de iva del registro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property WithholdingTax As Decimal

    ''' <summary>
    ''' Obtiene o establece el porcentaje de iva del comprobante de entrada
    ''' </summary>
    ''' <remarks></remarks>
    Property WithholdingIcaPercentage As Decimal?

    ''' <summary>
    ''' Obtiene o establece el valor de la retencion de ica del producto
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property WithholdingICA As Decimal

    ''' <summary>
    ''' Obtiene o establece el valor de la retencion en la fuente del registro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property RetentionSource As Decimal

    ''' <summary>
    ''' Obtiene o establece el valor de las otras retenciones del registro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property RetentionOther As Decimal

    ''' <summary>
    ''' Obtiene o establece el valor de las otras deducciones del registro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property DeductionOther As Decimal

    ''' <summary>
    ''' Obtiene o establece el valor de los descuentos distritales del registro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property DistrictTax As Decimal

    ''' <summary>
    ''' Obtiene o establece el valor total del registro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property TotalValue As Decimal

    ''' <summary>
    ''' Obtiene o establece el valor del descuetno de la orden de entrada
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property NetoValue As Decimal

#End Region

#End Region

#Region "Datasources"

    ''' <summary>
    ''' Datasource almacenes
    ''' </summary>
    Property ListWareHouse As XPInstantFeedbackSource

    ''' <summary>
    ''' Datasource comprobantes de entrada
    ''' </summary>
    Property ListEntranceVoucher As XPInstantFeedbackSource

    ''' <summary>
    ''' Datasource causas de devolución
    ''' </summary>
    Property DevolutionCauseXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' listado del detalle de la entrance voucher
    ''' </summary>
    ''' <remarks></remarks>
    Property ListEntranceVoucherDetail As List(Of Domain.Entities.EntranceVoucherDetailBatchSerial)

    ''' <summary>
    ''' Listado que contiene las retenciones que se le pueden aplican al comprobante
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ListRetention As List(Of Domain.Entities.OtherWithholdingDeduction)

    ''' <summary>
    ''' Listado que contiene las deducciones que se le pueden aplican al comprobante
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ListDeduction As List(Of Domain.Entities.OtherWithholdingDeduction)

    ''' <summary>
    ''' Listado que contiene las deducciones que se le pueden aplican al comprobante
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ListRetentionDevolution As List(Of Domain.Entities.EntranceVoucherDevolutionOtherDeduction)

    ''' <summary>
    ''' Listado que contiene las retenciones que se le pueden aplican al comprobante devolucion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ListDeductionDevolution As List(Of Domain.Entities.EntranceVoucherDevolutionOtherDeduction)

#End Region

End Interface
