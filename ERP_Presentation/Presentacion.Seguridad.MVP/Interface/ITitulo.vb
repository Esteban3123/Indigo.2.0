Imports Domain.Base.Entities
Imports Domain.Base.Entities.Core.Enums
Imports Presentation.Base
Imports Presentation.Controls

Public Interface ITitulo
    Inherits ICrudBase

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean

    ''' <summary>
    ''' Id Titulo
    ''' </summary>
    ''' <returns></returns>
    Property IdTitle As Integer

    ''' <summary>
    ''' Nombre Titulo
    ''' </summary>
    ''' <returns></returns>
    Property TitleName As String

    ''' <summary>
    ''' Estado Titulo
    ''' </summary>
    ''' <returns></returns>
    Property State As Boolean

    ''' <summary>
    ''' Estado crud del registro
    ''' </summary>
    ''' <returns></returns>
    Property Crud As ECrud

End Interface
