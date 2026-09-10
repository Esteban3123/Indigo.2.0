Imports System.Runtime.Serialization

''' <summary>
''' Clase para trasportar los datos del clima
''' </summary>
''' <remarks></remarks>
<DataContract(IsReference:=True)>
Public Class WeatherDocument

    <DataMember()> _
    Public Property ConditionCode As String

    <DataMember()> _
    Public Property Humidity As String

    <DataMember()> _
    Public Property WindSpeed As String

    <DataMember()> _
    Public Property Visibility As String

    <DataMember()> _
    Public Property Pressure As String

    <DataMember()> _
    Public Property Temperature As String

    <DataMember()>
    Public Property NameCity As String

    <DataMember()>
    Public Property TempHigh As String
    <DataMember()>
    Public Property TempLow As String
    <DataMember()>
    Public Property CondText As String

    <DataMember()>
    Public Property UrlImage As String

    ''' <summary>
    ''' Hora en que se toma la lectura
    ''' </summary>
    <DataMember()>
    Public Property DateReaded As DateTime

End Class
