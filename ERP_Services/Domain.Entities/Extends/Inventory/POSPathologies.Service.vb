Imports System.Runtime.Serialization

Partial Public Class POSPathologies

#Region "Properties"
    ''' <summary>
    ''' Obtiene o establece el codigo del diagnostico
    ''' </summary>
    <DataMember()>
    Public Property DiagnosticCode As String

    ''' <summary>
    ''' Obtiene o establece el nombre del diagnostico
    ''' </summary>
    <DataMember()>
    Public Property DiagnosticName As String

    ''' <summary>
    ''' Obtiene o establece el nombre del diagnostico
    ''' </summary>
    <DataMember()>
    Public Property BillingGroupName As String

    ''' <summary>
    ''' Obtiene o establece el nombre de la unidad de edad
    ''' </summary>
    <DataMember()>
    Public Property AgeMeasureName As String

#End Region

End Class
