Imports System.Runtime.Serialization
Imports System.Collections.Generic

''' <summary>
''' Resultado del procesamiento de causaciones no reconocidas
''' </summary>
<DataContract(IsReference:=True), Serializable()>
Public Class UnrecognizedProcessingResult

    <DataMember()>
    Public Property TotalCandidates As Integer

    <DataMember()>
    Public Property SuccessCount As Integer

    <DataMember()>
    Public Property FailedCount As Integer

    <DataMember()>
    Public Property ExcludedByLiquidation As Integer

    <DataMember()>
    Public Property Details As List(Of ProcessingDetail)

    Public Sub New()
        Details = New List(Of ProcessingDetail)()
    End Sub

End Class

''' <summary>
''' Detalle individual del resultado de procesamiento
''' </summary>
<DataContract(IsReference:=True), Serializable()>
Public Class ProcessingDetail

    <DataMember()>
    Public Property ServiceOrderDetailId As Integer

    ''' <summary>
    ''' OK / Failed / ExcludedByLiquidation / AlreadyCaused
    ''' </summary>
    <DataMember()>
    Public Property Status As String

    <DataMember()>
    Public Property Message As String

End Class
