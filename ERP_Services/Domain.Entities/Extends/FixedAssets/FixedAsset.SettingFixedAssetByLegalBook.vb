#Region "Imports"
Imports System.Runtime.Serialization
#End Region

Partial Public Class SettingFixedAssetByLegalBook

    ''' <summary>
    ''' Código y nombre del libro oficial
    ''' </summary>
    <DataMember()>
    Public Property LegalBookCodeName As String

    ''' <summary>
    ''' Permite saber si el libro es oficial
    ''' </summary>
    <DataMember()>
    Public Property OfficialBook As Boolean

    ''' <summary>
    ''' Permite saber si el estado del libro
    ''' </summary>
    <DataMember()>
    Public Property StatusBook As Boolean

End Class
