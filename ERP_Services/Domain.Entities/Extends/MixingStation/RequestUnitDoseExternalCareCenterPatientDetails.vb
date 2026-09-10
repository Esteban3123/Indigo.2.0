Imports System.Runtime.Serialization

Partial Public Class RequestUnitDoseExternalCareCenterPatientDetails

    <DataMember()>
    Public Property ATCCodeName As String

    <DataMember()>
    Public Property MeasurementUnitCodeName As String

    <DataMember()>
    Public Property AdministrationRouteCodeName As String

    ''' <summary>
    ''' Crea una copia de la entidad
    ''' </summary>
    ''' <returns>Copia de la entidad</returns>
    Public Function CloneEntity() As RequestUnitDoseExternalCareCenterPatientDetails
        Dim entity As RequestUnitDoseExternalCareCenterPatientDetails = DirectCast(MemberwiseClone(), RequestUnitDoseExternalCareCenterPatientDetails)
        Return entity
    End Function

End Class
