Imports System.Runtime.Serialization

Partial Public Class ExternalPatientPreparationDetail

    ''' <summary>
    ''' Obtiene o establece codigo y el nombre del medicamento
    ''' </summary>
    <DataMember()>
    Public Property AtcCodeName As String

    ''' <summary>
    ''' Obtiene o establece codigo y el nombre del suministro
    ''' </summary>
    <DataMember()>
    Public Property SupplieCodeName As String

    ''' <summary>
    ''' Obtiene o establece codigo y el nombre del producto
    ''' </summary>
    <DataMember()>
    Public Property ProductCodeName As String

    ''' <summary>
    ''' Obtiene o establece codigo y el nombre del componente
    ''' </summary>
    <DataMember()>
    Public Property CodeName As String

    ''' <summary>
    ''' Obtiene o establece codigo y el nombre de la unidad de medida en peso
    ''' </summary>
    <DataMember()>
    Public Property MeasurementUnitCodeName As String

    ''' <summary>
    ''' Obtiene o establece la abreviacion de la unidad de medida en peso
    ''' </summary>
    <DataMember()>
    Public Property MeasurementUnitAbreviation As String

    ''' <summary>
    ''' Obtiene o establece codigo y el nombre de la unidad de medida en volumen
    ''' </summary>
    <DataMember()>
    Public Property VolumeMeasureUnitCodeName As String

    ''' <summary>
    ''' Obtiene o establece la abreviacion de la unidad de medida en volumen
    ''' </summary>
    <DataMember()>
    Public Property VolumeMeasureUnitAbreviation As String

    ''' <summary >
    ''' Obtiene o establece la cantidad y la unidad de medida del componente
    ''' </summary>
    <DataMember()>
    Public Property QuantityWithUnit As String

    Public Sub LoadDescriptions()
        If Me.AtcId IsNot Nothing Then
            Me.CodeName = Me.AtcCodeName
        ElseIf Me.SupplieId IsNot Nothing Then
            Me.CodeName = Me.SupplieCodeName
        ElseIf Me.ProductId IsNot Nothing Then
            Me.CodeName = Me.ProductCodeName
        End If

        If Me.ComponentType = 1 AndAlso (Me.Quantity IsNot Nothing AndAlso Me.Quantity > 0) Then
            QuantityWithUnit = $"{ Me.Quantity} {Me.MeasurementUnitAbreviation}"
        Else
            QuantityWithUnit = $"{ Me.Volume} {Me.VolumeMeasureUnitAbreviation}"
        End If
    End Sub

End Class

