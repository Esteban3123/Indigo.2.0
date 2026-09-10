'***********************************************************************
' Assembly         : Presentacion.Inventory
' Author           : Carlos Mario Arias Rubiano
' Created          : 08/09/2014
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

Public Interface IWarehouse
    Inherits ICrudBase

#Region "Properties"

    ''' <summary>
    ''' Esta propiedad que contiene el estado del registro
    ''' </summary>
    Property Status As Boolean

    ''' <summary>
    ''' Esta propiedad que contiene el layout del formulario
    ''' </summary>
    ReadOnly Property MyLayoutControl As IndigoLayoutControl

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

    ''' <summary>
    ''' Obtiene o establece el consecutivo del grupo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Code As String

    ''' <summary>
    ''' Obtiene o establece la descripcion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Description As String

    ''' <summary>
    ''' Obtiene o establece el prefijo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Prefix As String

    ''' <summary>
    ''' Obtiene o establece el id del proveedor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property SupplierId As Integer

    ''' <summary>
    ''' Obtiene o establece el código del centro de atención
    ''' </summary>
    ''' <returns></returns>
    Property CenterAttentionCode As String

    ''' <summary>
    ''' Obtiene o establece el id del centro de costo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IdCostCenter As Integer

    ''' <summary>
    ''' Obtiene o establece el id de la cuenta del tercero para debito
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IdThirdPartyAccountDebit As Integer?

    ''' <summary>
    ''' Obtiene o establece el id de la cuenta del tercero para credito
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IdThirdPartyAccountCredit As Integer?

    ''' <summary>
    ''' Obtiene o establece el id del usuario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IdUser As Integer?
    ''' <summary>
    ''' obtiene o establece el ide del usuario del tercerro
    ''' </summary>
    ''' <returns></returns>
    Property IdUserRequest As Integer?

    ''' <summary>
    ''' Obtiene o establece si el almacen maneja restricciones
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property HandleRestrictedProducts As Boolean

#End Region

#Region "Datasources"

    Property ViewListConsignmentWarehouseProductsBatchSerialXpo As List(Of Infrastructure.Data.Xpo.InventoryRepository.View.ViewListConsignmentWarehouseProductsBatchSerialXpo)

    ''' <summary>
    ''' Propiedad que contiene el listado de proveedores
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property SupplierXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Propiedad que contiene el listado de centros de atención
    ''' </summary>
    ''' <returns></returns>
    Property CenterAttentions As XPInstantFeedbackSource

    ''' <summary>
    ''' Propiedad que contiene el listado del centro costo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property CostCenterXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Propiedad que contiene el listado cuentas contables
    ''' </summary>
    Property ThirdPartyAccountDebitXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Propiedad que contiene el listado de cuentas contables
    ''' </summary>
    Property ThirdPartyAccountCreditXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Establece el datasource de usuarios
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property UserXpo As DevExpress.Data.Linq.LinqInstantFeedbackSource

    ''' <summary>
    ''' Establece el datasource de usuarios Terceros
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property UserRequestXpo As DevExpress.Data.Linq.LinqInstantFeedbackSource


    ''' <summary>
    ''' Lista los productos de almacenes en consignación 
    ''' </summary>
    ''' <returns></returns>
    Property ListConsignimentwarehouseProducts As List(Of Infrastructure.Data.Xpo.InventoryRepository.View.ViewListConsignmentWarehouseProductsXpo)

#End Region

#Region "Methods"

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean

#End Region

End Interface
