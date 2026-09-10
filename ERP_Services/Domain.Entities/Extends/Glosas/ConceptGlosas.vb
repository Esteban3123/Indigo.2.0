Imports System.Runtime.Serialization
Partial Public Class ConceptGlosas

#Region "Manual Properties"

    Private _conceptCodeName As String

    <DataMember()>
    Public Property ConceptCodeName() As String
        Get
            Return _conceptCodeName
        End Get
        Set(ByVal value As String)
            Me._conceptCodeName = value
        End Set
    End Property

    Private _tupleHomologation As List(Of Tuple(Of String, String, String))
    Private ReadOnly Property ListTupleHomologation As List(Of Tuple(Of String, String, String))
        Get
            If _tupleHomologation Is Nothing Then
                _tupleHomologation = New List(Of Tuple(Of String, String, String))
                _tupleHomologation.Add(New Tuple(Of String, String, String)("2", "996", "5"))
                _tupleHomologation.Add(New Tuple(Of String, String, String)("2", "998", "6"))
                _tupleHomologation.Add(New Tuple(Of String, String, String)("2", "999", "7"))
                _tupleHomologation.Add(New Tuple(Of String, String, String)("2", "997", "8"))
                _tupleHomologation.Add(New Tuple(Of String, String, String)("2", "995", "9"))
            End If
            Return _tupleHomologation
        End Get
    End Property

#End Region

#Region "Methods"
    ''' <summary>
    ''' funcion que homologa dependiendo el codigo y el tipo 2 (respuesta) a que nuevo tipo pertence
    ''' </summary>
    ''' <returns></returns>
    Public Function HomologateTypeByCodeAndResponse() As String
        If Me.Type <> 2 OrElse Not ListTupleHomologation.Exists(Function(x) x.Item1 = Me.Type And x.Item2 = Me.Code) Then
            Return Me.Type
        End If
        Return ListTupleHomologation.Find(Function(x) x.Item1 = Me.Type And x.Item2 = Me.Code).Item3
    End Function
#End Region
End Class
