Imports System.Runtime.Serialization

Partial Public Class ExternalPatientPreparation

    ''' <summary>
    ''' Obtiene o establece codigo y el nombre de la unidad de medida del total del preparado
    ''' </summary>
    <DataMember()>
    Public Property TotalPreparedUnitMeasurementCodeName As String

    ''' <summary>
    ''' Obtiene o establece la abreviacion de la unidad de medida del total del preparado
    ''' </summary>
    <DataMember()>
    Public Property TotalPreparedUnitMeasurementAbreviation As String

    ''' <summary>
    ''' Obtiene o establece codigo y el nombre de la undiad de medida del total del preparado
    ''' </summary>
    <DataMember()>
    Public Property TotalPreparedVolumeWithUnit As String

    ''' <summary>
    ''' Obtiene o establece codigo y el nombre de la vía de administración
    ''' </summary>
    <DataMember()>
    Public Property AdministrationRouteCodeName As String

    '==================================================================

    ''' <summary>
    ''' Obtiene o establece codigo y el nombre del medicamento principal
    ''' </summary>
    <DataMember()>
    Public Property MainMedicineCodeName As String

    ''' <summary >
    ''' Obtiene o establece codigo y el nombre de la unidad de medida del medicamento principal
    ''' </summary>
    <DataMember()>
    Public Property MainMedicineMeasureUnitCodeName As String


    ''' <summary >
    ''' Obtiene o establece la cantidad y la unidad de medida de la unidad de medida del medicamento principal
    ''' </summary>
    <DataMember()>
    Public Property MainMedicineQuantityWithUnit As String

    ''' <summary >
    ''' Obtiene o establece codigo y el nombre del volumen del medicamento principal
    ''' </summary>
    <DataMember()>
    Public Property MainMedicineVolumeCodeName As String

    ''' <summary >
    ''' Obtiene o establece la cantidad y la unidad de medida del volumen del medicamento principal
    ''' </summary>
    <DataMember()>
    Public Property MainMedicineVolumeQuantityWithUnit As String

    '---------------------------------------------------------------------------------------------

    ''' <summary>
    ''' Obtiene o establece codigo y el nombre del reconstituyente
    ''' </summary>
    <DataMember()>
    Public Property ReconstituentCodeName As String

    ''' <summary>
    ''' Obtiene o establece codigo y el nombre de la unidad de medida  del reconstituyente
    ''' </summary>
    <DataMember()>
    Public Property ReconstituentMeasureUnitCodeName As String

    ''' <summary >
    ''' Obtiene o establece la cantidad y la unidad de medida del reconstituyente
    ''' </summary>
    <DataMember()>
    Public Property ReconstituentQuantityWithUnit As String

    '---------------------------------------------------------------------------------------------

    ''' <summary>
    ''' Obtiene o establece codigo y el nombre del vehículo
    ''' </summary>
    <DataMember()>
    Public Property VehicleCodeName As String

    ''' <summary>
    ''' Obtiene o establece codigo y el nombre de la unidad de medida del vehículo
    ''' </summary>
    <DataMember()>
    Public Property VehicleMeasureUnitCodeName As String

    ''' <summary >
    ''' Obtiene o establece la cantidad y la unidad de medida del reconstituyente
    ''' </summary>
    <DataMember()>
    Public Property VehicleQuantityWithUnit As String
    '==================================================================  

    Public Sub LoadDescriptions()
        For Each detail In Me.ExternalPatientPreparationDetail
            If detail.ComponentType = 1 Then
                MainMedicineCodeName = detail.CodeName
                MainMedicineQuantityWithUnit = detail.QuantityWithUnit
            ElseIf detail.ComponentType = 2 Then
                ReconstituentCodeName = detail.CodeName
                ReconstituentQuantityWithUnit = detail.QuantityWithUnit
            ElseIf detail.ComponentType = 3 Then
                VehicleCodeName = detail.CodeName
                VehicleQuantityWithUnit = detail.QuantityWithUnit
            End If
        Next

        Me.TotalPreparedVolumeWithUnit = $"{VolumeTotalOrder} {TotalPreparedUnitMeasurementAbreviation}"
    End Sub

End Class
