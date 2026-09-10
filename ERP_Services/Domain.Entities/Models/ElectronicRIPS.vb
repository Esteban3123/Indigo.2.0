Imports System.Runtime.Serialization
''' <summary>
''' Modelo POCO para RIPS Electronico
''' </summary>
<DataContract>
Public Class ElectronicRIPS
    <DataMember>
    Public ListEntityCode As List(Of String)
    <DataMember>
    Public EntityName As String
End Class
