Imports DevExpress.Xpo

<Persistent("dbo.ViewLiquidationGetAdmission")>
Partial Public Class ViewLiquidationGetAdmission
    Inherits XPLiteObject

    Dim fAdmissionCode As String
    <Key(True)>
    Public Property AdmissionCode() As String
        Get
            Return fAdmissionCode
        End Get
        Set(value As String)
            SetPropertyValue("AdmissionCode", fAdmissionCode, value)
        End Set
    End Property

    Dim fAdmissionDate As DateTime
    Public Property AdmissionDate() As DateTime
        Get
            Return fAdmissionDate
        End Get
        Set(value As DateTime)
            SetPropertyValue(Of DateTime)("AdmissionDate", fAdmissionDate, value)
        End Set
    End Property

    Dim fResponsibleName As String
    Public Property ResponsibleName() As String
        Get
            Return fResponsibleName
        End Get
        Set(value As String)
            SetPropertyValue("ResponsibleName", fResponsibleName, value)
        End Set
    End Property

    Dim fPatientCode As String
    Public Property PatientCode() As String
        Get
            Return fPatientCode
        End Get
        Set(value As String)
            SetPropertyValue("PatientCode", fPatientCode, value)
        End Set
    End Property

    Dim fPatientName As String
    Public Property PatientName() As String
        Get
            Return fPatientName
        End Get
        Set(value As String)
            SetPropertyValue("PatientName", fPatientName, value)
        End Set
    End Property

    Dim fAdmissionTypeName As String
    Public Property AdmissionTypeName() As String
        Get
            Return fAdmissionTypeName
        End Get
        Set(value As String)
            SetPropertyValue("AdmissionTypeName", fAdmissionTypeName, value)
        End Set
    End Property

    Dim fBedStay As String
    Public Property BedStay() As String
        Get
            Return fBedStay
        End Get
        Set(value As String)
            SetPropertyValue("BedStay", fBedStay, value)
        End Set
    End Property

    Dim fLiquidationType As Byte
    Public Property LiquidationType() As Byte
        Get
            Return fLiquidationType
        End Get
        Set(value As Byte)
            SetPropertyValue("LiquidationType", fLiquidationType, value)
        End Set
    End Property

    Dim fStatusName As String
    Public Property StatusName() As String
        Get
            Return fStatusName
        End Get
        Set(value As String)
            SetPropertyValue("StatusName", fStatusName, value)
        End Set
    End Property

    Dim fStatus As String
    Public Property Status() As String
        Get
            Return fStatus
        End Get
        Set(value As String)
            SetPropertyValue("Status", fStatus, value)
        End Set
    End Property

    Dim fFunctionalUnitCodeName As String
    Public Property FunctionalUnitCodeName() As String
        Get
            Return fFunctionalUnitCodeName
        End Get
        Set(value As String)
            SetPropertyValue("FunctionalUnitCodeName", fFunctionalUnitCodeName, value)
        End Set
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
End Class