'***********************************************************************
' Assembly         : Presentacion.Inventory.MVP
' Author           : Diego A. Roldán Lozano
' Created          : 2023-03-23
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Librerias Importadas"
Imports DevExpress.Xpo
Imports Presentation.Base
Imports Presentation.Controls

#End Region

Public Interface IRequestParam
    Inherits ICrudBase

    ''' <summary>
    ''' Esta propiedad que contiene el estado del registro
    ''' </summary>
    Property Status As Boolean
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
    ReadOnly Property MyTag As String

    ''' <summary>
    ''' Obtiene o establece el consecutivo del grupo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Code As String

    ''' <summary>
    ''' Requiere autorización
    ''' </summary>
    ''' <returns></returns>
    Property RequiredAuthorization As Boolean

    ''' <summary>
    ''' Fecha inicio
    ''' </summary>
    ''' <returns></returns>
    Property InitialDate As Date?

    ''' <summary>
    ''' Unidad de tiempo
    ''' </summary>
    ''' <returns></returns>
    Property UnitTime As Integer?
    ''' <summary>
    ''' Frecuencia
    ''' </summary>
    ''' <returns></returns>
    Property Frecuency As Integer

    ''' <summary>
    ''' Mes
    ''' </summary>
    ''' <returns></returns>
    Property Month As Integer?

    ''' <summary>
    ''' Contiene una lista de unidades funcionales
    ''' </summary>
    ''' <returns></returns>
    Property RequestParamFunctionalUnits As XPCollection

    ''' <summary>
    ''' Contiene una lista de almacenes
    ''' </summary>
    ''' <returns></returns>
    Property RequestParamWarehouse As XPCollection

    ''' <summary>
    ''' Obtiene o asigna la secuencia numerica del formulario
    ''' </summary>
    ''' <value>Secuencia numerica del formulario</value>
    ''' <returns>La secuencia numerica del formulario</returns>
    Property Sequence As Domain.Entities.InventorySequence

End Interface
