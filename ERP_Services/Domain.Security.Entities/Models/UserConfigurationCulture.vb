Imports System.Runtime.Serialization

<DataContract>
Public Class UserConfigurationCulture
    <DataMember()>
    Public Property IdUser As Integer
    <DataMember()>
    Public Property Idtimezone As Integer?
    <DataMember()>
    Public Property dateFormat As Integer?
    <DataMember()>
    Public Property timeFormat As Integer?
    <DataMember()>
    Public Property LanguageCulture As String
End Class
