#Region "Imports"

Imports Presentation.Base

#End Region

''' <summary>
''' Define las propiedades y métodos usados en el frontal
''' de secuencias numericas
''' </summary>
Public Interface ISequences
    Inherits IcrudBase

#Region "Properties"

    Property [Module] As Integer

    Property [Form] As String

    Property IsManual As Boolean

    Property Scope As String

    Property IsSequential As Boolean

    Property Rate As Integer

    Property OperatingUnit As Integer

    Property SequencePattern As Integer

    Property NextNumber As Integer

    Property ValidLenght As Boolean

    Property MinimumChar As Byte

    Property MaximumChar As Byte

#End Region

End Interface
