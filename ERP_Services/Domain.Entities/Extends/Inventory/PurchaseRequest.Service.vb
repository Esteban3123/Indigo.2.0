#Region "imports"

Imports System.Runtime.Serialization

#End Region

Partial Public Class PurchaseRequest

    ''' <summary>
    ''' propiedad que contiene el codigo y el nombre de la unidad funcional
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property DescriptionFunctionalUnit As String

    <DataMember()>
    Public Property Prefix As String

End Class
