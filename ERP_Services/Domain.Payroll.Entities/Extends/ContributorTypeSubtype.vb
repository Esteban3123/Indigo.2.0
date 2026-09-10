Imports System.Runtime.Serialization

Partial Public Class ContributorTypeSubtype

    ''' <summary>
    ''' Nombre del Tipo de Cotizante (desnormalizado, solo para transporte)
    ''' </summary>
    <DataMember()>
    Public Property ContributorTypeName As String

    ''' <summary>
    ''' Nombre del Subtipo de Cotizante (desnormalizado, solo para transporte)
    ''' </summary>
    <DataMember()>
    Public Property ContributorSubtypeName As String


    ''' <summary>
    ''' Codigo del Subtipo de Cotizante (desnormalizado, solo para transporte)
    ''' </summary>
    <DataMember()>
    Public Property ContributorSubtypeCode As String

End Class
