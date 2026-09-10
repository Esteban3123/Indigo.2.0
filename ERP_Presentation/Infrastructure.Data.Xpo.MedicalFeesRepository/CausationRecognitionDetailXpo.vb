'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.MedicalFeesRepository
' Author           : Andres Alarcon
' Created          : 22/10/2025
'
' Copyright        : (c) . All rights reserved.
'*************************************************************

#Region "Imports"

Imports DevExpress.Xpo

#End Region

''' <summary>
''' conceptos de nota usado en los servicios Xpo
''' </summary>
<Persistent("MedicalFees.CausationRecognitionDetail")>
Public Class CausationRecognitionDetailXpo
    Inherits XPLiteObject

    Dim fId As Integer
    <Key(True)>
    <Persistent("Id")>
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
        End Set
    End Property

    Dim fMedicalFeesCausationId As Integer
    Public Property MedicalFeesCausationId() As Integer
        Get
            Return fMedicalFeesCausationId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("MedicalFeesCausationId", fMedicalFeesCausationId, value)
        End Set
    End Property

    Dim fCausationRecognitionId As CausationRecognitionXpo
    <Association("CausationRecognitionDetailReferencesCausationRecognition")>
    Public Property CausationRecognitionId() As CausationRecognitionXpo
        Get
            Return fCausationRecognitionId
        End Get
        Set(ByVal value As CausationRecognitionXpo)
            SetPropertyValue(Of CausationRecognitionXpo)("CausationRecognitionId", fCausationRecognitionId, value)
        End Set
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
