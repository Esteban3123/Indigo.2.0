#Region "Imports"
Imports System.Runtime.Serialization
#End Region

Partial Public Class FixedAssetEntryItemDetail

    ''' <summary>
    ''' Código nombre unidad funcional
    ''' </summary>
    <DataMember()>
    Public Property FuntionalUnitCodeName As String

    ''' <summary>
    ''' Código nombre responsable
    ''' </summary>
    <DataMember()>
    Public Property ResponsibleCodeName As String

    ''' <summary>
    ''' Código nombre localización
    ''' </summary>
    <DataMember()>
    Public Property LocationCodeName As String

    ''' <summary>
    ''' Código nombre estado del activo
    ''' </summary>
    <DataMember()>
    Public Property StatusAssetCodeName As String

    ''' <summary>
    ''' Permite saber si es la primera vez que se asignan los valores
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property IsFirstSetValues As Boolean


    <DataMember>
    Public Property OrderItem As Integer
End Class
