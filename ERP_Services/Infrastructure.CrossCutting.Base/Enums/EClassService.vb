Imports System.Runtime.Serialization
''' <summary>
''' Enumeracion que determina la clase del servicio
''' </summary>
''' <remarks></remarks>
<DataContract()>
Public Enum EClassService As Integer

    ''' <summary>
    ''' ninguno
    ''' </summary>
    <EnumMember()>
    None = 1

    ''' <summary>
    ''' Cirujano
    ''' </summary>
    <EnumMember()>
    Surgeon = 2

    ''' <summary>
    ''' Anesteciologo
    ''' </summary>
    <EnumMember()>
    Anesthesiologist = 3

    ''' <summary>
    ''' Ayudante
    ''' </summary>
    <EnumMember()>
    Assistant = 4

    ''' <summary>
    ''' Derecho de sala
    ''' </summary>
    <EnumMember()>
    RightRoom = 5

    ''' <summary>
    ''' Materiales Sutura
    ''' </summary>
    <EnumMember()>
    SutureMaterials = 6

    ''' <summary>
    ''' Instrumentacion Quirurgica
    ''' </summary>
    <EnumMember()>
    SurgicalInstrumentation = 7
End Enum