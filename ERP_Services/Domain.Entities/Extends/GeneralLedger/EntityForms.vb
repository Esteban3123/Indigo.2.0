Imports System.Runtime.Serialization

Partial Public Class EntityForms

    <DataMember>
    Property ModuleName As String

    <DataMember>
    Property FormName As String

    <DataMember>
    Property Description As String

    <DataMember>
    Property ListVieBot As List(Of VieBot)

End Class
