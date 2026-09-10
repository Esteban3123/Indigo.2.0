#Region "Imports"

Imports Presentation.Base
Imports DevExpress.Xpo
Imports Presentation.Controls

#End Region

Public Interface ITransferOrder
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
    ''' Obtiene o establece el consecutivo de la orden de traslado
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
    Property DocumentDate As Date

    ''' <summary>
    ''' Obtiene o establece el el tipo de orden de traslado
    ''' </summary>
    ''' <value>1  - Traslado 2 - Consumo</value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property OrderType As Byte

    ''' <summary>
    ''' Obtiene o establece una descripcion de la orden de traslado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Description As String

    ''' <summary>
    ''' Obtiene o establece el Id del almacen de origen
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property SourceWarehouseId As Integer?

    ''' <summary>
    ''' Obtiene o establece hacia donde se va a despachar los items
    ''' </summary>
    ''' <value>1 - Almacen 2 - Unidad Funcional</value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property DispatchTo As Byte?

    ''' <summary>
    ''' Obtiene o establece el Id del Almacen de destino
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property TransitWarehouseId As Integer?

    ''' <summary>
    ''' Obtiene o establece el Id del Almacen de destino
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property TargetWarehouseId As Integer?

    ''' <summary>
    ''' Obtiene o establece el Id de la unidad funcional
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property TargetFunctionalUnitId As Integer?

    ''' <summary>
    ''' Obtiene o establece el Id del concepto de movimiento de inventarios
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property AdjustmentConceptId As Integer?

    ''' <summary>
    ''' Obtiene o establece el Id del tercero
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ThirdPartyId As Integer?

    ''' <summary>
    ''' Obtiene o establece el estado de la unidad
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Status As Byte

#End Region

#Region "Datasources"

    ''' <summary>
    ''' Obtiene y asigna el objeto de tipo xpo de almacen
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property SourceWarehouseXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene y asigna el objeto de tipo xpo de almacen
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property TransitWarehouseXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene y asigna el objeto de tipo xpo de almacen
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property TargetWarehouseXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene y asigna el objeto de tipo xpo de unidad funcional
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property TargetFunctionalUnitXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene y asigna el objeto de tipo xpo de concepto de movimiento de inventarios
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property AdjustmentConceptXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene y asigna el objeto de tipo xpo de tercero
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ThirdPartyXpo As XPInstantFeedbackSource

#End Region

End Interface
