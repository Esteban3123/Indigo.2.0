Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Billing.VSlipOutAndAdmission")> _
Public Class BillingVSlipOutAndAdmission
    Inherits XPLiteObject
    Dim fidSlipOut As Integer
    <Key(True)> _
    Public Property idSlipOut() As Integer
        Get
            Return fidSlipOut
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("idSlipOut", fidSlipOut, value)
        End Set
    End Property
    Dim fcodeSlipOut As String
    <Size(20)> _
    Public Property codeSlipOut() As String
        Get
            Return fcodeSlipOut
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("codeSlipOut", fcodeSlipOut, value)
        End Set
    End Property
    Dim fdateSlipOut As DateTime
    Public Property dateSlipOut() As DateTime
        Get
            Return fdateSlipOut
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("dateSlipOut", fdateSlipOut, value)
        End Set
    End Property
    Dim fAdmissionNumber As String
    <Size(10)> _
    Public Property AdmissionNumber() As String
        Get
            Return fAdmissionNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AdmissionNumber", fAdmissionNumber, value)
        End Set
    End Property
    Dim fdocumentPacient As String
    <Size(15)> _
    Public Property documentPacient() As String
        Get
            Return fdocumentPacient
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("documentPacient", fdocumentPacient, value)
        End Set
    End Property
    Dim fnamePacient As String
    <Size(250)> _
    Public Property namePacient() As String
        Get
            Return fnamePacient
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("namePacient", fnamePacient, value)
        End Set
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
