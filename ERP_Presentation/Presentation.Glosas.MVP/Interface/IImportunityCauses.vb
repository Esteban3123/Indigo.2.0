#Region "Imports"

Imports Presentation.Base
Imports Presentation.Controls

#End Region

Public Interface IImportunityCauses
    Inherits ICrudBase

#Region "Properties"

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <returns>Tag del formulario</returns>
    ReadOnly Property MyTag As Object

    '''' <summary>
    '''' Gets my layout control.
    '''' </summary>
    '''' <value>
    '''' My layout control.
    '''' </value>
    'ReadOnly Property MyLayoutControl As IndigoLayoutControl

    ''' <summary>PortfolioSequence
    ''' Obtiene o asigna la secuencia numerica del formulario
    ''' </summary>
    ''' <value>Secuencia numerica del formulario</value>
    ''' <returns>La secuencia numerica del formulario</returns>
    Property Sequense As Domain.Entities.GlosaSequence

    ''' <summary>
    ''' Obtiene o establece el consecutivo de la causa de devolución
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Code As String

    ''' <summary>
    ''' Obtiene o establece la nombre de la causa de devolución
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ImportunityCausesName As String

    ''' <summary>
    ''' Esta propiedad que contiene el estado del registro
    ''' </summary>
    Property Status As Boolean

#End Region

#Region "Methods"

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean

#End Region

End Interface
