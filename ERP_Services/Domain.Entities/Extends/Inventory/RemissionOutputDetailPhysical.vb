Imports System.Runtime.Serialization

Partial Public Class RemissionOutputDetailPhysical
    <DataMember>
    Property CodeNameProduct As String

    <DataMember>
    Property CodeNameWarehouse As String

    <DataMember>
    Property CodeBatchSerial As String

    <DataMember>
    Property QuantityDeliver As Integer

    <DataMember>
    Property ProductId As Integer

    <DataMember>
    Property DevolutionCauseId As Integer?

    ''' <summary>
    ''' Bandera para saber si el item esta activo
    ''' </summary>
    ''' <returns></returns>
    <DataMember>
    Property Activated As Boolean

    ''' <summary>
    ''' bandera para saber que el item que voy a importar cumpla con las validaciones que se hicieron (se usa en remision de entrada)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember>
    Property ItemInvalid As Boolean

    <DataMember>
    Property RemissionOutputCode As String

    <DataMember>
    Property RemissionDate As DateTime

End Class
