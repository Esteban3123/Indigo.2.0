'***********************************************************************
' Assembly         : Presentacion.Inventory
' Author           : Rafael Eduardo Patiño Cabrera
' Created          : 20/04/2015
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

Public Interface ILendingMerchandising
    Inherits IcrudBase
    ''' <summary>
    ''' consecutivo del oficio de prestamo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Code As String
    ''' <summary>
    ''' Fecha del documento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property DateDocument As Date
    ''' <summary>
    ''' Lista de tipo de prestamo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property DataSourceTypeOfLoan As List(Of Tuple(Of Integer, String))
    ''' <summary>
    ''' Lista de almacenes
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property DatasourceStock As DevExpress.Xpo.XPInstantFeedbackSource
    ''' <summary>
    ''' lista de terceros
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property DataSourceThird As DevExpress.Xpo.XPInstantFeedbackSource
    ''' <summary>
    ''' Observacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Observation As String
    ''' <summary>
    ''' Lista de productos a prestar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property DataSourceProducts As DevExpress.Xpo.XPCollection
    ''' <summary>
    ''' Obtiene o asigna la secuencia numerica del formulario
    ''' </summary>
    ''' <value>Secuencia numerica del formulario</value>
    ''' <returns>La secuencia numerica del formulario</returns>
    Property Sequense As Domain.Entities.InventorySequence
    ''' <summary>
    ''' Esta propiedad que contiene el estado del registro
    ''' </summary>
    Property Status As Boolean
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
    ''' Id del tercero
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IdThirdParty As Integer?
    ''' <summary>
    ''' Id del almacen
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IdStores As Integer?
    ''' <summary>
    ''' Id tipo de prestamo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IdLeanType As Integer?
End Interface
