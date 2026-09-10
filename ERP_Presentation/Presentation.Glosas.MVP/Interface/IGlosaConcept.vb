Imports Domain.Entities
Imports Presentation.Base

Public Interface IGlosaConcept
    Inherits ICrudBase

    ''' <summary>
    ''' async loader
    ''' </summary>
    ''' <param name="State"></param>
    Sub AsyncLoader(State As Boolean)

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
    ''' Obtiene o asigna la secuencia numerica del formulario
    ''' </summary>
    ''' <value>Secuencia numerica del formulario</value>
    ''' <returns>La secuencia numerica del formulario</returns>
    Property Sequence As Domain.Entities.GlosaSequence

    ''' <summary>
    ''' Código
    ''' </summary>
    ''' <returns></returns>
    Property Code As String

    ''' <summary>
    ''' Nombre
    ''' </summary>
    ''' <returns></returns>
    Property Name As String

    ''' <summary>
    ''' Tipo
    ''' </summary>
    ''' <returns></returns>
    Property Type As String

    ''' <summary>
    ''' Concepto General
    ''' </summary>
    ''' <returns></returns>
    Property NameGeneral As String

    ''' <summary>
    ''' Application
    ''' </summary>
    ''' <returns></returns>
    Property Application As String

    ''' <summary>
    ''' Descuento Honorarios Médicos
    ''' </summary>
    ''' <returns></returns>
    Property DiscountedMedicalFees As Boolean

    ''' <summary>
    ''' Estado
    ''' </summary>
    ''' <returns></returns>
    Property Status As Boolean

    ''' <summary>
    ''' Lista de usuarios autorizados
    ''' </summary>
    ''' <returns></returns>
    Property ConceptGlosaList As List(Of ConceptGlosasUser)

    ''' <summary>
    ''' Lista de tipos de respuesta
    ''' </summary>
    ''' <returns></returns>
    ReadOnly Property ListTypes As List(Of Tuple(Of String, String))

End Interface
