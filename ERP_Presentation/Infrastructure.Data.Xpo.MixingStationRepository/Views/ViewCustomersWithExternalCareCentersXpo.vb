'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.MixingStation
' Author           : andrea Coqueco
' Created          : 29-07-2024
'
' Copyright        : (c) . All rights reserved.
'*************************************************************
Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("MixingStation.ViewCustomersWithExternalCareCenters")>
Partial Public Class ViewCustomersWithExternalCareCentersXpo
    Inherits XPLiteObject

    Dim fViewKey As String
    <Key(True)>
    Public Property ViewKey() As String
        Get
            Return fViewKey
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ViewKey", fViewKey, value)
        End Set
    End Property

    Dim fIdCustomer As Integer
    Public Property IdCustomer() As Integer
        Get
            Return fIdCustomer
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdCustomer", fIdCustomer, value)
        End Set
    End Property

    Dim fNitCustomer As String
    Public Property NitCustomer() As String
        Get
            Return fNitCustomer
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NitCustomer", fNitCustomer, value)
        End Set
    End Property

    Dim fNameCustomer As String
    Public Property NameCustomer() As String
        Get
            Return fNameCustomer
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NameCustomer", fNameCustomer, value)
        End Set
    End Property

    Dim fNitName As String
    'columna que devuelve el nit y el nombre concatenado
    <Size(50)>
    <PersistentAlias("concat(NitCustomer,' - ',NameCustomer)")>
    Public ReadOnly Property NitName() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("NitName"))
        End Get
    End Property

#Region "Builders"
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

    Public Sub New()
        MyBase.New(Session.DefaultSession)
    End Sub

    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

#End Region

End Class