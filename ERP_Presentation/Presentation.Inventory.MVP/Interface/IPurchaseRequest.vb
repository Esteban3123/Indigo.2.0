'***********************************************************************
' Assembly         : Presentacion.Inventory
' Author           : Hector Rodriguez Rubiano
' Created          : 15/04/2019
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports DevExpress.Xpo
Imports Presentation.Base
Imports Presentation.Controls

#End Region
Public Interface IPurchaseRequest
    Inherits IcrudBase

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

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property MyLayoutControl As IndigoLayoutControl

    ''' <summary>
    ''' Obtiene o establece el consecutivo de la solicitud de inventario
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
    ''' Obtiene o establece el tipo de la solicitud
    ''' </summary>
    ''' <value>1 - Product 2 - Activo fijo 3 - Otro</value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property RequestType As Byte?

    ''' <summary>
    ''' Obtiene o establece el Id de la unidad funcional
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property FunctionalUnitId As Integer

    ''' <summary>
    ''' Obtiene y asigna el objeto de tipo xpo de unidad funcional
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property FunctionalUnitXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece una observacion de la solicitud
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Observation As String

    ''' <summary>
    ''' Obtiene o establece el estado de la unidad
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Status As Byte

    ''' <summary>
    ''' datasource de tipos de solicitudes
    ''' </summary>
    ''' <returns></returns>
    Property RequestTypeDataSource As XPInstantFeedbackSource

End Interface
