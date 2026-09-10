'***********************************************************************
' Assembly         : Presentacion.Inventory.MVP
' Author           : Faiber Julian Mora Dussan
' Created          : 26-09-2016
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"

Imports Presentation.Base
Imports Domain.Entities
Imports DevExpress.Xpo
Imports Presentation.Controls

#End Region

Public Interface IInventoryRequestDevolution
    Inherits IcrudBase

#Region "Properties"

    ''' <summary>
    ''' Esta Propiedad establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean

    ''' <summary>
    ''' Obtiene o asigna el tag del funcional
    ''' </summary>
    ''' <value>Tag del fucnional</value>
    ''' <returns></returns>
    ReadOnly Property MyTag As String

    ''' <summary>
    ''' Obtiene o establece la secuencia de cabecera
    ''' </summary>
    ''' <value>
    ''' The sequense.
    ''' </value>
    Property Sequense As Domain.Entities.InventorySequence

    ''' <summary>
    ''' Obtiene o establece el layout para customizacion
    ''' </summary>
    ''' <value>
    ''' My layout control.
    ''' </value>
    ReadOnly Property MyLayoutControl As IndigoLayoutControl

    ''' <summary>
    ''' Codigo de solicitud de devolución
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Code As String

    ''' <summary>
    ''' Fecha de la devolución
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property DocumentDate As DateTime

    ''' <summary>
    ''' Descripción de la devolución de solicitud
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Description As String

    ''' <summary>
    ''' Obtiene o establece el tipo de la solicitud a devolver
    ''' </summary>
    ''' <value>1: Unidad Funcional - 2: Almacen</value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property RequestDevolutionType As Integer

    ''' <summary>
    ''' Obtiene o establece el estado de la unidad
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Status As Byte

#End Region

End Interface