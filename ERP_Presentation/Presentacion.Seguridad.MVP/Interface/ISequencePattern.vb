#Region "Imports"

Imports Presentation.Base

#End Region

''' <summary>
''' Define las propiedades y métodos usados en el frontal
''' de patrones de secuencias numericas
''' </summary>
Public Interface ISequencePattern
    Inherits IcrudBase

#Region "Properties"

    Property PatternName As String

    Property Pattern As String

#End Region

End Interface