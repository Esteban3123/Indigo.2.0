Imports System.Runtime.Serialization

Partial Public Class CMConfigurationUsers

#Region "Properties"

    ''' <summary>
    ''' Obtiene o establece el nombre del usuario
    ''' </summary>
    <DataMember()>
    Public Property FullNameUser As String

    ''' <summary>
    ''' Obtiene o establece el nombre del Director 
    ''' </summary>
    <DataMember()>
    Public Property CodeNameUser As String

    ''' <summary>
    ''' Obtiene o establece el nombre de la posicion del empleado/usuario
    ''' </summary>
    <DataMember()>
    Public Property PositionName As String

#End Region

End Class
