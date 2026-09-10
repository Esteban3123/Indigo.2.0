'***********************************************************************
' Assembly         : Presentacion.Inventory
' Author           : Diego Andrés Roldán Lozano
' Created          : 04-12-2014
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

Public Interface IPackage
    Inherits IcrudBase

    ''' <summary>
    ''' Obtiene el layout del frontal
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property MyLayoutControl As IndigoLayoutControl

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <returns>Tag del formulario</returns>
    ReadOnly Property MyTag As Object

    ''' <summary>
    ''' Obtiene o establece el código
    ''' </summary>
    Property Code As String

    ''' <summary>
    ''' Obtiene o establece el estado del registro
    ''' </summary>
    Property Status As Boolean

    ''' <summary>
    ''' Obtiene o asigna la secuencia numerica del formulario
    ''' </summary>
    Property Sequence As Domain.Entities.InventorySequence

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean

End Interface