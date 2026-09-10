'***********************************************************************
' Assembly         : Presentacion.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 30/07/2014
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
Imports DevExpress.Data.Linq

#End Region

''' <summary>
''' esta interfaz contiene las propiedades y metodos que va implementar nuestra vista y va a controlar nuestro presenter
''' </summary>
''' <remarks></remarks>
Public Interface ITransfers
    Inherits IcrudBase

#Region "Properties"

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Status As Integer

    ''' <summary>
    ''' Consecutivo del traslado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Consecutive As String

    ''' <summary>
    ''' Fecha del documento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property DocumentDate As DateTime?

    ''' <summary>
    ''' Id del proveedor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IdSupplier As Integer?

    ''' <summary>
    ''' Listado los proveedores
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property SupplierXpo As XPInstantFeedbackSource

    ' ''' <summary>
    ' ''' Propiedad que contiene el listado de proveedores con las lineas de distribucion
    ' ''' </summary>
    'Property SuppliersDistributionLinesXpo As XPInstantFeedbackSource

    ' ''' <summary>
    ' ''' Propiedad que contiene el id de la asociacion del proveedor con la linea de distribucion
    ' ''' </summary>
    ' ''' <value></value>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Property IdSupplierDistributionLine As Integer

    ''' <summary>
    ''' Obtiene o establece el listado de facturas por id proveedor y estado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property AccountPayableSharesDatasource As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el id de la cuota
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IdAccountPayableShare As Integer

    ''' <summary>
    ''' Propiedad que contiene el id de la cuenta contable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IdAccount As Integer?

    ''' <summary>
    ''' Propiedad que contiene el listado de centros de costo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property CostCenterXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Propiedad que contiene el id del centro de costo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IdCostCenter As Integer?

    ''' <summary>
    ''' Propiedad que contiene las observaciones del traslado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Comments As String

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
    Property Sequense As Domain.Entities.PaymentsSecuence

    ''' <summary>
    ''' Propiedad que contiene el listado de anticipos a proveedores
    ''' </summary>
    Property AdvancePaymentsXpo As LinqInstantFeedbackSource

    ''' <summary>
    ''' Establece el id del anticipo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IdAdvancePayments As Integer?

    ''' <summary>
    ''' Obtiene o establece la fecha de la factura
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property BillDate As DateTime

    ''' <summary>
    ''' Obtiene o establece la fecha de expiracion de la cuota
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ExpiratedDateShare As DateTime

    ''' <summary>
    ''' Obtiene o establece el valor de la cuota
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ValueShare As Decimal

    ''' <summary>
    ''' Obtiene o establece el saldo de la cuota
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property BalanceShare As Decimal

    ''' <summary>
    ''' Obtiene o establece el valor a cruzar de la cuota
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property CrossValueShare As Decimal

    ''' <summary>
    ''' Obtiene o establece el tipo de traslado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property TransferType As Integer?

    ''' <summary>
    ''' Obtiene o establece el id de la nota de pago
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property AccountPayableConceptNoteId As Integer?

    ''' <summary>
    ''' Establece el datasource de los conceptos de pago
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property AccountPayableConceptNoteXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el id de la cuenta contable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property MainAccountOtherConceptId As Integer?

    ''' <summary>
    ''' Establece el datasource de la cuenta contable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property MainAccountOtherConceptXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el id del tercero
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ThirdPartyOtherConceptId As Integer?

    ''' <summary>
    ''' Establece el datasource del tercero
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ThirdPartyOtherConceptXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el id del centro costo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property CostCenterOtherConceptId As Integer?

    ''' <summary>
    ''' Establece el datasource del centro costo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property CostCenterOtherConceptXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece la naturaleza
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Nature As Integer?

    ''' <summary>
    ''' Obtiene o establece el valor de los otros conceptos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ValueOtherConcept As Decimal

    ''' <summary>
    ''' Obtiene el id de la moneda
    ''' </summary>
    ''' <returns></returns>
    Property CurrencyId As Integer?

    ''' <summary>
    ''' Abreviación del tipo de moneda
    ''' </summary>
    ''' <returns></returns>
    Property CurrencyAbbreviation As String

#End Region

End Interface
