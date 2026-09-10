#Region "Imports"

Imports System.Runtime.Serialization

#End Region

Partial Public Class ViewListRequestDetailImport

    ''' <summary>
    ''' Bandera para saber si el item esta activo
    ''' </summary>
    ''' <returns></returns>
    Property Activated As Boolean

    ''' <summary>
    ''' bandera para saber que el item que voy a importar cumpla con las validaciones que se hicieron (se usa en remision de entrada)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ItemInvalid As Boolean

    ''' <summary>
    ''' Fabricante
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property ManufacturerName As String

    ''' <summary>
    ''' Registro Sanitario
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property HealthRegistration As String

    ''' <summary>
    ''' Presentación
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property Presentation As String

    ''' <summary>
    ''' propiedad que contiene la cantidad a importar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember>
    Property QuantityImport As Integer?


End Class
