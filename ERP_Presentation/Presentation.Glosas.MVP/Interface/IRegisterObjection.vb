'***********************************************************************
' Assembly         : Presentacion.Glosas.MVP
' Author           : Juan F. Tamayo
' Created          : 2013-05-07
'
' Last Modified By : Juan F. Tamayo
' Last Modified On : 2013-05-07
' Description      : Interface del frontal de registro de objeciones
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Presentation.Base
Imports Domain.Entities
Imports DevExpress.Xpo

#End Region

''' <summary>
''' Interface del frontal de registro de objeciones
''' </summary>
Public Interface IRegisterObjection
    Inherits ICrudBase

#Region "Properties"
    ''' <summary>
    ''' Obtiene o asigna el valor de la objecion principal
    ''' </summary>
    ''' <value>Valor de la objecion principal</value>
    ''' <returns>El valor de la objecion principal</returns>
    Property ValueMainObjection As Decimal
    ''' <summary>
    ''' Obtiene o asigna un valor que indica si el frontal ya se cargo inicialmente
    ''' </summary>
    ''' <value>Valor</value>
    ''' <returns>Un valor que indica si ya se cargo el formulario</returns>
    ''' <remarks></remarks>
    Property IsLoaded As Boolean
    ''' <summary>
    ''' Obtiene o asigna el id del detalle de factura
    ''' </summary>
    ''' <value>Id del detalle de factura</value>
    ''' <returns>El id del detalle de factura</returns>
    Property InvoiceDetaildId As Long
    ''' <summary>
    ''' obtiene el id detalle factura QX
    ''' </summary>
    ''' <value></value>
    ''' <returns>id detalle factura QX</returns>
    Property InvoiceDetaildQXId As Long
    ''' <summary>
    ''' Obtiene el valor de la factura
    ''' </summary>
    ''' <value>valor de la factura</value>
    ''' <returns>el valor de la factura</returns>
    Property InvocieValue As Decimal
    ''' <summary>
    ''' Obtiene el valor de la objecion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    Property ValueObjection As Decimal
    ''' <summary>
    ''' Propiedad para asignar el numero de factura del detalle
    ''' </summary>
    ''' <value>Nuemro de factura</value>
    ''' <returns>numero de factura</returns>
    Property InvoiceNumber As String
    ''' <summary>
    ''' Propiedad de control de glosa general o especifico
    ''' </summary>
    ''' <value></value>
    ''' <returns>si se va realizar una glosa general o especifica</returns>
    Property GeneralGlosa As Boolean
    ''' <summary>
    ''' propiedad para saber si el tipo de glosa a realizar es por seleccion multiple 
    ''' </summary>
    Property GeneralGlosaSelection As Boolean
    ''' <summary>
    ''' Obtiene o asigna el codigo del concepto general
    ''' </summary>
    ''' <value>Codigo del concepto</value>
    ''' <returns>El codigo del concepto</returns>
    Property CodeGeneralConcept As String
    ''' <summary>
    ''' Obtiene o asigna la fuente de datos de conceptos generales
    ''' </summary>
    Property GeneralConceptDataSource As XPInstantFeedbackSource
    ''' <summary>
    ''' Obtiene o asigna el codigo del responsable
    ''' </summary>
    ''' <value>Codigo del responsable</value>
    ''' <returns>El codigo del responsable</returns>
    Property CodeResponsible As String
    ''' <summary>
    ''' Obtiene o asigna la fuente de datos de responsables
    ''' </summary>
    Property ResponsibleDataSource As List(Of ResponsibleAll)
    ''' <summary>
    ''' Obtiene o asigna el valor de la objecion
    ''' </summary>
    ''' <value>Valor de la objecion</value>
    ''' <returns>El valor de la objecion</returns>
    ' Property Value As String
    ''' <summary>
    ''' Obtiene o asigna el comentario
    ''' </summary>
    ''' <value>Comentario</value>
    ''' <returns>El comentario</returns>
    Property Comment As String
    ''' <summary>
    ''' Obtiene o asigna las objeciones asociadas al detalle de factura
    ''' </summary>
    ''' <value>Objeciones</value>
    ''' <returns>Las objeciones asociadas</returns>
    Property MovementGlosaSource As List(Of GlosaMovementGlosa)
    ''' <summary>
    ''' Asigna el valor de activo o inactivo a los controles
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean
    ''' <summary>
    ''' Asigna el valor de activo o inactivo a la barra de tarea
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Property Actionstaskbar As Boolean
    ''' <summary>
    ''' propiedad para asignar la lista de detalles de factura al realizar una objecion general
    ''' </summary>
    ''' <value>lsita de detalle de factura</value>
    ''' <returns>una lista de detalle de factura</returns>
    ''' <remarks></remarks>
    Property ListInvoiceDetail As List(Of GlosaInvoiceDetail)

    ''' <summary>
    ''' propiedad para asignar la lista de detalles de factura qx al realizar una objecion por seleccion multiple
    ''' </summary>
    ''' <value>lsita de detalle de factura qx</value>
    ''' <returns>una lista de detalle de factura qx</returns>
    ''' <remarks></remarks>
    Property ListInvoiceDetailqx As List(Of GlosaInvoiceDetailQX)

    ''' <summary>
    ''' Porpiedad para saber si el tipo de glosa a realizar es por seleccion multiple y de tipo qx
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property GlosaSelectionqx As Boolean

    ''' <summary>
    ''' Propiedad Para Asignar la descripcion del servicio a registrar movimineto glosa
    ''' </summary>
    WriteOnly Property DescripTionService As String


#End Region

#Region "Methods"

    ''' <summary>
    ''' Cambia el estado del formulario para indicar que se esta llevando a cabo una operacion asincrona
    ''' </summary>
    ''' <param name="State">Valor que indica si se lleva a cabo la operacion</param>
    Sub AsyncLoader(ByVal State As Boolean)

#End Region

End Interface
