Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Maintenance.EquipmentHistory")>
Partial Public Class Maintenance_EquipmentHistory
        Inherits XPLiteObject
        Dim fId As Short
        <Key()>
        Public Property Id() As Short
            Get
                Return fId
            End Get
            Set(ByVal value As Short)
                SetPropertyValue(Of Short)("Id", fId, value)
            End Set
        End Property
        Dim fName As String
        Public Property Name() As String
            Get
                Return fName
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)("Name", fName, value)
            End Set
        End Property
        Dim fScore As Short
        Public Property Score() As Short
            Get
                Return fScore
            End Get
            Set(ByVal value As Short)
                SetPropertyValue(Of Short)("Score", fScore, value)
            End Set
        End Property
        Dim fState As Boolean
        Public Property State() As Boolean
            Get
                Return fState
            End Get
            Set(ByVal value As Boolean)
                SetPropertyValue(Of Boolean)("State", fState, value)
            End Set
        End Property
        <Association("Maintenance_EquipmentRegistrationReferencesMaintenance_EquipmentHistory")>
        Public ReadOnly Property Maintenance_EquipmentRegistrations() As XPCollection(Of Maintenance_EquipmentRegistration)
            Get
                Return GetCollection(Of Maintenance_EquipmentRegistration)("Maintenance_EquipmentRegistrations")
            End Get
        End Property
End Class