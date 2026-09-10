Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Maintenance.TechnicalLogDetail")>
Partial Public Class Maintenance_TechnicalLogDetail
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
        Dim fIdEquipmentRegistration As Maintenance_EquipmentRegistration
        <Association("Maintenance_TechnicalLogDetailReferencesMaintenance_EquipmentRegistration")>
        Public Property IdEquipmentRegistration() As Maintenance_EquipmentRegistration
            Get
                Return fIdEquipmentRegistration
            End Get
            Set(ByVal value As Maintenance_EquipmentRegistration)
                SetPropertyValue(Of Maintenance_EquipmentRegistration)("IdEquipmentRegistration", fIdEquipmentRegistration, value)
            End Set
        End Property
        Dim fIdTechnicalLog As Maintenance_TechnicalLog
        <Association("Maintenance_TechnicalLogDetailReferencesMaintenance_TechnicalLog")>
        Public Property IdTechnicalLog() As Maintenance_TechnicalLog
            Get
                Return fIdTechnicalLog
            End Get
            Set(ByVal value As Maintenance_TechnicalLog)
                SetPropertyValue(Of Maintenance_TechnicalLog)("IdTechnicalLog", fIdTechnicalLog, value)
            End Set
        End Property
        Dim fName As String
        <Size(50)>
        Public Property Name() As String
            Get
                Return fName
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)("Name", fName, value)
            End Set
        End Property
        Dim fValueMin As String
        <Size(10)>
        Public Property ValueMin() As String
            Get
                Return fValueMin
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)("ValueMin", fValueMin, value)
            End Set
        End Property
        Dim fValueMax As String
        <Size(10)>
        Public Property ValueMax() As String
            Get
                Return fValueMax
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)("ValueMax", fValueMax, value)
            End Set
        End Property
        Dim fIdMeasurementUnit As Maintenance_MeasurementUnit
        <Association("Maintenance_TechnicalLogDetailReferencesMaintenance_MeasurementUnit")>
        Public Property IdMeasurementUnit() As Maintenance_MeasurementUnit
            Get
                Return fIdMeasurementUnit
            End Get
            Set(ByVal value As Maintenance_MeasurementUnit)
                SetPropertyValue(Of Maintenance_MeasurementUnit)("IdMeasurementUnit", fIdMeasurementUnit, value)
            End Set
        End Property
        Dim fAbbreviation As String
        <Size(10)>
        Public Property Abbreviation() As String
            Get
                Return fAbbreviation
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)("Abbreviation", fAbbreviation, value)
            End Set
        End Property
End Class