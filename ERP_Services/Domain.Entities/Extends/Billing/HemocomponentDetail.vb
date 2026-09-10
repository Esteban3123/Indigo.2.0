
Imports System.Runtime.Serialization

Partial Public Class HemocomponentDetail
    <DataMember()>
    Public Property Id As Integer

    <DataMember()>
    Public Property HemocomponentId As Integer

    <DataMember()>
    Public Property TypeServiceIPS As Short

    <DataMember()>
    Public Property kindLoad As Short

    <DataMember()>
    Public Property CodeServiceIPS As String

    <DataMember()>
    Public Property DescriptionServiceIPS As String

    <DataMember()>
    Public Property Activated As Boolean

    <DataMember()>
    Public Property SERRASANTI As Boolean

    <DataMember()>
    Public Property IdRelatedDescription As Integer?

End Class
