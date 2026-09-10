'***********************************************************************
' Assembly         : Presentacion.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 01/04/2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Librerias Importadas"
Imports Presentation.Base
Imports Domain.Entities
Imports DevExpress.Xpo
Imports Presentation.Controls

#End Region

''' <summary>
''' esta interfaz contiene las propiedades y metodos que va implementar nuestra vista y va a controlar nuestro presenter
''' </summary>
''' <remarks></remarks>
Public Interface IOpeningBalance
    Inherits IcrudBase

#Region "Properties"

    ''' <summary>
    ''' Propiedad que contiene el listado de proveedores con las lineas de distribucion de anticipo
    ''' </summary>
    Property SuppliersDistributionLinesAdvanceXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Propiedad que contiene el id de la linea del proveedor de anticipo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IdSupplierDistributionLinesAdvance As Integer

    ''' <summary>
    ''' Obtiene o establece el id del proveedor de anticipo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IdSupplierAdvance As Integer

    ''' <summary>
    ''' Obtiene o establece el id del centro de costo de anticipo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IdCostCenterAdvance As Integer?

    ''' <summary>
    ''' Obtiene el listado del centro de costo de anticipo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property CostCenterAdvanceXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Propiedad que contiene el listado de proveedores con las lineas de distribucion de facturas
    ''' </summary>
    Property SuppliersDistributionLinesBillXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Propiedad que contiene el id de la linea del proveedor de facturas
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IdSupplierDistributionLinesBill As Integer

    ''' <summary>
    ''' Obtiene o establece el id del proveedor de facturas
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IdSupplierBill As Integer

    ''' <summary>
    ''' Obtiene o establece el id del centro de costo de facturas
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IdCostCenterBill As Integer?

    ''' <summary>
    ''' Obtiene el listado del centro de costo de facturas
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property CostCenterBillXpo As XPInstantFeedbackSource





    

    ''' <summary>
    ''' Esta propiedad contiene el codigo de la dependencia
    ''' </summary>
    Property CodeOpeningBalance As String

    
    ''' <summary>
    ''' Obtiene o establece el id de la cuenta contable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IdAccountAdvance As Integer

    ''' <summary>
    ''' Establece el datasource de la cuenta contable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property AccountXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el id de la cuenta contable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IdAccountBill As Integer

    ''' <summary>
    ''' Obtiene o establece el numero de la factura
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property BillNumber As String

    ''' <summary>
    ''' Obtiene o establece el plazo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Term As Integer

    ' ''' <summary>
    ' ''' Obtiene o establece la fecha del vencimiento
    ' ''' </summary>
    ' ''' <value></value>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Property ExpiredDate As DateTime

    ''' <summary>
    ''' Layout
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property MyLayoutControl As IndigoLayoutControl

    ''' <summary>
    ''' Esta propiedad que contiene el estado del registro
    ''' </summary>
    Property Status As Integer

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
    Property Sequense As Domain.Entities.PaymentsSecuence

    ''' <summary>
    ''' Fecha del anticipo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property AdvanceDate As DateTime?

    ''' <summary>
    ''' Observacion del anticipo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property AdvanceComments As String

    ''' <summary>
    ''' Valor del anticipo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ValueAdvance As Decimal

    ''' <summary>
    ''' Valor de la factura
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ValueBill As Decimal

    ''' <summary>
    ''' Saldo de la factura
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property BalanceBill As Decimal

    ''' <summary>
    ''' Observacion del saldo inicial
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Observations As String

    ''' <summary>
    ''' Obtiene o establece el id del tipo de proveedor 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property SupplierTypeId As Integer?

    ''' <summary>
    ''' Establece el datasource del tipo de proveedor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property SupplierTypeXpo As List(Of Domain.Entities.SupplierType)

    ''' <summary>
    ''' Obtiene o establece el id de la unidad de radicacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property FilingUnitId As Integer?

    ''' <summary>
    ''' Establece el datasource de la unidad de radicacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property FilingUnitXpo As List(Of Domain.Entities.FilingUnit)

    ''' <summary>
    ''' Obtiene o establece la fecha del periodo del servicio
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ServicePeriodDate As DateTime?

#End Region

End Interface
