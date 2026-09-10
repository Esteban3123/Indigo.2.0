Imports Domain.Base.Entities
Imports Domain.Base.Entities.Core.Enums
Imports Presentation.Base
Imports Presentation.Controls

Public Interface IModule
    Inherits ICrudBase

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    ReadOnly Property MyLayoutControl As IndigoLayoutControl

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean

    ''' <summary>
    ''' Id Modulo
    ''' </summary>
    ''' <returns></returns>
    Property IdModule As Integer
    ''' <summary>
    ''' Id Producto
    ''' </summary>
    ''' <returns></returns>
    Property IdProduct As Integer
    ''' <summary>
    ''' Nombre Modulo
    ''' </summary>
    ''' <returns></returns>
    Property ModuleName As String
    ''' <summary>
    ''' Descripcion Modulo
    ''' </summary>
    ''' <returns></returns>
    Property ModuleDescription As String
    ''' <summary>
    ''' Estado Modulo
    ''' </summary>
    ''' <returns></returns>
    Property State As Boolean

    ''' <summary>
    ''' Estado crud del registro
    ''' </summary>
    ''' <returns></returns>
    Property Crud As ECrud


End Interface
