'***********************************************************************
' Assembly         : Presentacion.Inventory
' Author           : Oscar Stiven Astudillo
' Created          : 2024-11-09
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Presentation.Base
Imports Presentation.Controls
#End Region


Public Interface IMedicationType
    Inherits ICrudBase


#Region "Properties"
    ''' <summary>
    ''' Obtiene o establece el codigo
    ''' </summary>
    ''' <value>Una cadena que representa el código del tipo de medicamento.</value>
    Property Code As String

    ''' <summary>
    ''' Obtiene o establece el nombre
    ''' </summary>
    ''' <value>Una cadena que representa el nombre del tipo de medicamento.</value>
    Property Name As String


    ''' <summary>
    ''' Indica el estado actual del registro.
    ''' </summary>
    ''' <value>Un valor booleano que especifica si el registro está activo o inactivo.</value>
    Property Status As Boolean

    ''' <summary>
    ''' Obtiene el control de diseño asociado con esta interfaz.
    ''' </summary>
    ''' <value>Una instancia de IndigoLayoutControl utilizada para gestionar el diseño.</value>
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
    Property Sequence As InventorySequence

    ''' <summary>
    ''' establece el estado de los controles
    ''' </summary>
    ''' <value>
    ''' </value>
    WriteOnly Property ActionsOnControls As Boolean
#End Region

End Interface
