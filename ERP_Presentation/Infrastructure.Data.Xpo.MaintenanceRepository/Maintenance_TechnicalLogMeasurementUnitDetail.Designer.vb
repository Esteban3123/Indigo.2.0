Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Maintenance.TechnicalLogMeasurementUnitDetail")>
Partial Public Class Maintenance_TechnicalLogMeasurementUnitDetail
        Inherits XPLiteObject
        Dim fId As Integer
        <Key(True)>
        Public Property Id() As Integer
            Get
                Return fId
            End Get
            Set(ByVal value As Integer)
                SetPropertyValue(Of Integer)("Id", fId, value)
            End Set
        End Property
        Dim fIdTechnicalLog As Maintenance_TechnicalLog
        <Association("Maintenance_TechnicalLogMeasurementUnitDetailReferencesMaintenance_TechnicalLog")>
        Public Property IdTechnicalLog() As Maintenance_TechnicalLog
            Get
                Return fIdTechnicalLog
            End Get
            Set(ByVal value As Maintenance_TechnicalLog)
                SetPropertyValue(Of Maintenance_TechnicalLog)("IdTechnicalLog", fIdTechnicalLog, value)
            End Set
        End Property
        Dim fIdMeasurementUnit As Maintenance_MeasurementUnit
        <Association("Maintenance_TechnicalLogMeasurementUnitDetailReferencesMaintenance_MeasurementUnit")>
        Public Property IdMeasurementUnit() As Maintenance_MeasurementUnit
            Get
                Return fIdMeasurementUnit
            End Get
            Set(ByVal value As Maintenance_MeasurementUnit)
                SetPropertyValue(Of Maintenance_MeasurementUnit)("IdMeasurementUnit", fIdMeasurementUnit, value)
            End Set
        End Property
End Class