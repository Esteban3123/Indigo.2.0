Imports System.Runtime.Serialization
''' <summary>
''' Enumeracion para determinar si es glosa o reiteracion
''' </summary>
''' <remarks></remarks>
<DataContract>
Public Enum EtypeDocumentGloss As Integer
    ''' <summary>
    ''' Glosa
    ''' </summary>
    ''' <remarks></remarks>
    Gloss = 1
    ''' <summary>
    ''' Reiteracion
    ''' </summary>
    ''' <remarks></remarks>
    Reiteration = 2
End Enum