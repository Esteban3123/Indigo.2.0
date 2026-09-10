#Region "Imports"

Imports System.Runtime.Serialization

#End Region

Partial Public Class OperatingUnit

#Region "Properties"

    ''' <summary>
    ''' Obtiene o establece la descripcion de la unidad operativa
    ''' </summary>
    <DataMember()>
    Public Property OperatingUnitDescription As String

    ''' <summary>
    ''' Obtiene o establece la descripcion de ciudad
    ''' </summary>
    <DataMember()>
    Public Property CityDescription As String

#End Region

End Class
