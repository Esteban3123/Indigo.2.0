#Region "Imports"

Imports System.Runtime.Serialization

#End Region

Partial Public Class FixedAssetTransfer

    ''' <summary>
    ''' Código y nombre del responsable origen
    ''' </summary>
    <DataMember()>
    Public Property SourceResponsibleCodeName As String

    ''' <summary>
    ''' Código y nombre del responsable destino
    ''' </summary>
    <DataMember()>
    Public Property TargetResponsibleCodeName As String

    ''' <summary>
    ''' Código y nombre del localización origen
    ''' </summary>
    <DataMember()>
    Public Property SourceLocationCodeName As String

    ''' <summary>
    ''' Código y nombre del localización destino
    ''' </summary>
    <DataMember()>
    Public Property TargetLocationCodeName As String

End Class
