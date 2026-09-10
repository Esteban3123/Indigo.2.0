Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

Partial Public Class Maintenance_Accessory

    <PersistentAlias("Code")>
    Public ReadOnly Property Codigo() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("Codigo"))
        End Get
    End Property
    <PersistentAlias("Name")>
    Public ReadOnly Property Descripcion() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("Descripcion"))
        End Get
    End Property

    <PersistentAlias("Concat(Code, ' - ', Name)")>
    Public ReadOnly Property CodeName As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("CodeName"))
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class