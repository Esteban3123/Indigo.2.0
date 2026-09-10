

#Region "Imports"
Imports Presentation.Base
Imports Presentation.Controls
Imports Domain.Entities
Imports DevExpress.Xpo
#End Region

Public Interface INPTConfiguration
    Inherits ICrudBase

    ''' <summary>
    ''' Layout del formulario
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
    ReadOnly Property MyTag As Object

    ''' <summary>
    ''' carga los datos de la tabla
    ''' </summary>
    ''' <returns></returns>
    Property ListNPTConfiguration As List(Of NPTConfiguration)


End Interface
