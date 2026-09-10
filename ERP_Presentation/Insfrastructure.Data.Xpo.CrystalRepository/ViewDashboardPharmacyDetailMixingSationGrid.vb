Imports DevExpress.Xpo

<Persistent("dbo.ViewDashboardPharmacyDetailMixingSationGrid")>
Partial Public Class ViewDashboardPharmacyDetailMixingSationGrid
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

    Dim fOpcion As String
    Public Property Opcion() As String
        Get
            Return fOpcion
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Opcion", fOpcion, value)
        End Set
    End Property

    Dim fProcedimiento As String
    Public Property Procedimiento() As String
        Get
            Return fProcedimiento
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Procedimiento", fProcedimiento, value)
        End Set
    End Property

    Dim fConsecutivo As Decimal
    Public Property Consecutivo() As Decimal
        Get
            Return fConsecutivo
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Consecutivo", fConsecutivo, value)
        End Set
    End Property

    Dim fCodeSusceptibleMixingStation As String
    Public Property CodeSusceptibleMixingStation() As String
        Get
            Return fCodeSusceptibleMixingStation
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CodeSusceptibleMixingStation", fCodeSusceptibleMixingStation, value)
        End Set
    End Property

    Dim fIdOrigin As Integer
    Public Property IdOrigin() As Integer
        Get
            Return fIdOrigin
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdOrigin", fIdOrigin, value)
        End Set
    End Property

    Dim fProducto As String
    Public Property Producto() As String
        Get
            Return fProducto
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Producto", fProducto, value)
        End Set
    End Property

    Dim fCodProducto As String
    Public Property CodProducto() As String
        Get
            Return fCodProducto
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CodProducto", fCodProducto, value)
        End Set
    End Property

    Dim fMainDrugCode As String
    Public Property MainDrugCode() As String
        Get
            Return fMainDrugCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("MainDrugCode", fMainDrugCode, value)
        End Set
    End Property

    Dim fCantidadSolicitada As Integer
    Public Property CantidadSolicitada() As Integer
        Get
            Return fCantidadSolicitada
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CantidadSolicitada", fCantidadSolicitada, value)
        End Set
    End Property

    Dim fCantidadEntregada As Integer
    Public Property CantidadEntregada() As Integer
        Get
            Return fCantidadEntregada
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CantidadEntregada", fCantidadEntregada, value)
        End Set
    End Property

    Dim fCantidadPendiente As Integer
    Public Property CantidadPendiente() As Integer
        Get
            Return fCantidadPendiente
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CantidadPendiente", fCantidadPendiente, value)
        End Set
    End Property

    Dim fMSClass As Integer
    Public Property MSClass() As Integer
        Get
            Return fMSClass
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("MSClass", fMSClass, value)
        End Set
    End Property

    Dim fIngreso As String
    Public Property Ingreso() As String
        Get
            Return fIngreso
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Ingreso", fIngreso, value)
        End Set
    End Property

    Dim fCodigoPaciente As String
    Public Property CodigoPaciente() As String
        Get
            Return fCodigoPaciente
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CodigoPaciente", fCodigoPaciente, value)
        End Set
    End Property

    <NonPersistent()>
    Public Property CUMSource As Integer

    <NonPersistent()>
    Public Property Action As Integer?

    <NonPersistent()>
    Public Property ListPhysicalInventory As List(Of Domain.Entities.PhysicalInventory)

    <NonPersistent()>
    Public Property ApplyProcedureId As Integer?

    <NonPersistent()>
    Public Property CodeNameApplyProcedure As String

    <NonPersistent()>
    Public Property ListPhysicalInventoryCustody As List(Of Domain.Entities.PhysicalInventoryCustody)
#End Region

#Region "Builder"

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

#End Region

End Class