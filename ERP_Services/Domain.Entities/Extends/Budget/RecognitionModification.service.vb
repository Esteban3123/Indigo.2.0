#Region "imports"
Imports System.Runtime.Serialization
#End Region

Partial Public Class RecognitionModification

#Region "Properties"

    ''' <summary>
    ''' Obtiene o establece la unidad operativa
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property OperatingUnitId As Integer

    ''' <summary>
    ''' Obtiene o establece el codigo y nombre de la categoria
    ''' </summary>
    <DataMember()>
    Public Property CodeRecognition As String

#End Region

End Class
