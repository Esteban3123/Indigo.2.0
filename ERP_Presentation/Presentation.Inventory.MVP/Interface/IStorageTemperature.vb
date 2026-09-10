'***********************************************************************
' Assembly         : Presentacion.Inventory.MVP
' Author           : Andres Alarcon
' Created          : 27-11-2024
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Presentation.Base
Imports Presentation.Controls

#End Region

Public Interface IStorageTemperature
    Inherits ICrudBase

#Region "Properties"

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <returns>Tag del formulario</returns>
    ReadOnly Property MyTag As Object

    ''' <summary>
    ''' Gets my layout control.
    ''' </summary>
    ''' <value>
    ''' My layout control.
    ''' </value>
    ReadOnly Property MyLayoutControl As IndigoLayoutControl

    ''' <summary>PortfolioSequence
    ''' Obtiene o asigna la secuencia numerica del formulario
    ''' </summary>
    ''' <value>Secuencia numerica del formulario</value>
    ''' <returns>La secuencia numerica del formulario</returns>
    Property Sequense As Domain.Entities.InventorySequence

    ''' <summary>
    ''' Obtiene o establece el consecutivo 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Code As String

    ''' <summary>
    ''' Obtiene o establece el nombre del rango de temperatura
    ''' </summary>
    Property Name As String

    ''' <summary>
    ''' Variable que refleja el valor "Desde"
    ''' </summary>
    Property From As Integer

    ''' <summary>
    ''' Variable que refleja el valor "Hasta"
    ''' </summary>
    Property Until As Integer

    ''' <summary>
    ''' Obtiene o establece la unidad de temperatura
    ''' </summary>
    Property TemperatureUnit As Byte

    ''' <summary>
    ''' Obtiene o establece la descripcion del registro, que consta de las variables concatenadas
    ''' </summary>
    Property Description As String

    ''' <summary>
    ''' Esta propiedad establece el valor del control de acciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean
#End Region
End Interface
