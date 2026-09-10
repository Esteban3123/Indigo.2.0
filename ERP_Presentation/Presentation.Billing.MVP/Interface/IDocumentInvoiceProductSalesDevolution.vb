'***********************************************************************
' Assembly         : Presentacion.Billing.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 14/01/2020
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

#End Region

Public Interface IDocumentInvoiceProductSalesDevolution
    Inherits IcrudBase

    ''' <summary>
    ''' Layout del formulario
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
    Property Sequense As Domain.Entities.BillingSequence

    ''' <summary>
    ''' Obtiene o establece el código del registro
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
    ''' Id del almacen
    ''' </summary>
    ''' <returns></returns>
    Property WarehouseId As Integer

    ''' <summary>
    ''' Datasource del almacen
    ''' </summary>
    ''' <returns></returns>
    Property WarehouseXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Detalle
    ''' </summary>
    ''' <returns></returns>
    Property Detail As String

    ''' <summary>
    ''' Id de la factura de productos
    ''' </summary>
    ''' <returns></returns>
    Property DocumentInvoiceProductSalesId As Integer

    ''' <summary>
    ''' Datasource de las facturas de productos
    ''' </summary>
    ''' <returns></returns>
    Property DocumentInvoiceProductSalesDatasourceXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Fecha de la factura
    ''' </summary>
    ''' <returns></returns>
    Property BillDate As DateTime?

    ''' <summary>
    ''' Descripción del cliente
    ''' </summary>
    ''' <returns></returns>
    Property ThirdPartyDescription As String

End Interface
