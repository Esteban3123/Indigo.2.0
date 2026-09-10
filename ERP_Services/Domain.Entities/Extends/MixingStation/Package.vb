Imports System.Runtime.Serialization
Imports System.Text

Partial Public Class Package

    ''' <summary>
    ''' Obtiene o establece la descripcion de la unidad de medida de la concentración
    ''' </summary>
    <DataMember()>
    Public Property ConcentrationMeasurementUnitCodeName As String

    ''' <summary>
    ''' Obtiene o establece la descripcion del paquete asociado
    ''' </summary>
    <DataMember()>
    Public Property AssociatedPackageCodeName As String

    ''' <summary>
    ''' Obtiene o establece la descripcion de la unidad de medida
    ''' </summary>
    <DataMember()>
    Public Property VolumeTotalOrderMeasurementUnitCodeName As String

    ''' <summary>
    ''' Obtiene o establece la descripcion de la unidad de medida del preparado
    ''' </summary>
    <DataMember()>
    Public Property MeasurementPreparedUnitCodeName As String

    ''' <summary>
    ''' Obtiene o establece la descripcion de la temperautra de almacenamiento
    ''' </summary>
    <DataMember()>
    Public Property StorageTemperatureDescription As String

    ''' <summary>
    ''' Id de la unidad operativa
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property OperatingUnitId As Integer

    ''' <summary>
    ''' Permite saber si es un paquete personalizado
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property IsPackagePersonalized As Boolean = False

    ''' <summary>
    ''' Permite establecer el código y nombre del nivel de riesgo
    ''' </summary>
    <DataMember>
    Public Property RiskLevelCodeName As String

    <DataMember>
    Public Property UnitDoseTypeCodeName As String

    ''' <summary>
    ''' Obtiene o establece el código y nombre del medicamento principal
    ''' Medicamento de referencia marcado como principal para procesos de dispensación y facturación.
    ''' </summary>
    <DataMember()>
    Public Property MainDrugCodeName As String

    ''' <summary>
    ''' Obtiene o establece el nombre del producto asociado
    ''' </summary>
    <DataMember()>
    Public Property ProductName As String

End Class
