#Region "Imports"

Imports System
Imports DevExpress.Xpo

#End Region


<Persistent("GeneralLedger.HealthSuperParameters")>
Public Class HealthSuperParametersXpo
    Inherits XPLiteObject

#Region "Members"

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

    Dim fCode As String
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property

    Dim fFormat As Integer
    Public Property Format() As Integer
        Get
            Return fFormat
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Format", fFormat, value)
        End Set
    End Property

    Dim fStatus As Boolean
    Public Property Status As Boolean
        Get
            Return fStatus
        End Get
        Set(value As Boolean)
            SetPropertyValue(Of Boolean)("Status", fStatus, value)
        End Set
    End Property

#End Region

#Region "Custom Members"

    <PersistentAlias("Iif(Status = 1, 'Activo', 'Inactivo')")>
    Public ReadOnly Property StatusName As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("StatusName"))
        End Get
    End Property

    <PersistentAlias("Iif(  Format = 1, 'FT001 - Catalogo Información Financiera',
                            Format = 3,'FT003 - Cuentas Por Cobrar – Deudores',
                            Format = 4,'FT004 - Cuentas por Pagar – Acreedores',
                            Format = 6,'FT006 - Bancos y Carteras Colectivas',
                            Format = 7,'FT007 - Control de Inversiones Inscritas en el Mercado de Valores de Colombia',
                            Format = 8,'FT008 - Inversiones – Otros Títulos',
                            Format = 9,'FT009 - Activos y Pasivos en Moneda Extranjera',
                            Format = 10,'FT010 - Activos No Monetarios',
                            Format = 25,'FT025 - Facturación Radicada','N/A')")>
    Public ReadOnly Property FormatName As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("FormatName"))
        End Get
    End Property

#End Region

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
