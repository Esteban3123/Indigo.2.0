Imports Domain.Base.Entities
Imports Domain.Base.Entities.Core.Enums
Imports Presentation.Base
Imports Presentation.Controls

Public Interface IFormulario
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
    ''' Id Formulario
    ''' </summary>
    ''' <returns></returns>
    Property IdForm As Integer
    ''' <summary>
    ''' Nombre Formulario
    ''' </summary>
    ''' <returns></returns>
    Property FormName As String
    ''' <summary>
    ''' Evento
    ''' </summary>
    ''' <returns></returns>
    Property PrintEvents As String
    ''' <summary>
    ''' Maneja secuencia
    ''' </summary>
    ''' <returns></returns>
    Property HasSequence As Boolean

    ''' <summary>
    ''' Es Nativo secuencia
    ''' </summary>
    ''' <returns></returns>
    Property IsNativeForm As Boolean

    ''' <summary>
    ''' Maneja Formulario
    ''' </summary>
    ''' <returns></returns>
    Property HasForm As Boolean

    ''' <summary>
    ''' Maneja Confirmacion masiva
    ''' </summary>
    ''' <returns></returns>
    Property HandlesMassiveConfirm As Boolean

    ''' <summary>
    ''' Nombre de clase
    ''' </summary>
    ''' <returns></returns>
    Property ClassName As String

    ''' <summary>
    ''' Nombre de ensamblado
    ''' </summary>
    ''' <returns></returns>
    Property AssemblyName As String

    ''' <summary>
    ''' Nombre de secuencia
    ''' </summary>
    ''' <returns></returns>
    Property SequenceModule As String

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
