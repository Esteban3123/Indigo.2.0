Imports DevExpress.Xpo

<Persistent("dbo.ViewPatientDeparture")>
Public Class ViewPatientDeparture
    Inherits XPLiteObject


    Dim fNumIng As String
    <Key()>
    Public Property NumIng() As String
        Get
            Return fNumIng
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NumIng", fNumIng, value)
        End Set
    End Property

    Dim fUnitFunctional As String

    Public Property UnitFunctional() As String
        Get
            Return fUnitFunctional
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("UnitFunctional", fUnitFunctional, value)
        End Set
    End Property

    Dim fUnitFunctionalDescr As String

    Public Property UnitFunctionalDescr() As String
        Get
            Return fUnitFunctionalDescr
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("UnitFunctionalDescr", fUnitFunctionalDescr, value)
        End Set
    End Property

    Dim fCenterCare As String
    Public Property CenterCare() As String
        Get
            Return fCenterCare
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CenterCare", fCenterCare, value)
        End Set
    End Property

    Dim fNomCama As String
    Public Property NomCama() As String
        Get
            Return fNomCama
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NomCama", fNomCama, value)
        End Set
    End Property

    Dim fIdenUsua As String
    Public Property IdenUsua() As String
        Get
            Return fIdenUsua
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("IdenUsua", fIdenUsua, value)
        End Set
    End Property


    Dim fNomUsua As String
    Public Property NomUsua() As String
        Get
            Return fNomUsua
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("fNomUsua", fNomUsua, value)
        End Set
    End Property

    Dim fNomMed As String
    Public Property NomMed() As String
        Get
            Return fNomMed
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NomMed", fNomMed, value)
        End Set
    End Property

    Dim fFechaEgresoM As DateTime
    Public Property FechaEgresoM() As DateTime
        Get
            Return fFechaEgresoM
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("FechaEgresoM", fFechaEgresoM, value)
        End Set
    End Property


    Dim fTiempoEgreso As Integer
    Public Property TiempoEgreso() As Integer
        Get
            Return fTiempoEgreso
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("TiempoEgreso", fTiempoEgreso, value)
        End Set
    End Property

    Dim fNomEnf As String
    Public Property NomEnf() As String
        Get
            Return fNomEnf
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NomEnf", fNomEnf, value)
        End Set
    End Property

    Dim fFechaEgresoE As DateTime
    Public Property FechaEgresoE() As DateTime
        Get
            Return fFechaEgresoE
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("FechaEgresoE", fFechaEgresoE, value)
        End Set
    End Property




    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class
