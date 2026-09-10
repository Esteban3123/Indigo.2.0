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
<Persistent("MedicalFees.CausationRecognition")>
Public Class CausationRecognitionXpo
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

    Dim fSupplierId As Integer
    Public Property SupplierId() As Integer
        Get
            Return fSupplierId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("SupplierId", fSupplierId, value)
        End Set
    End Property

    Dim fOperativeUnitId As Integer
    Public Property OperativeUnitId() As Integer
        Get
            Return fOperativeUnitId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("OperativeUnitId", fOperativeUnitId, value)
        End Set
    End Property

    Dim fTotalSupplier As Decimal
    Public Property TotalSupplier() As Decimal
        Get
            Return fTotalSupplier
        End Get
        Set(value As Decimal)
            SetPropertyValue("TotalSupplier", fTotalSupplier, value)
        End Set
    End Property

    Dim fCreationDate As DateTime
    Public Property CreationDate() As DateTime
        Get
            Return fCreationDate
        End Get
        Set(value As DateTime)
            SetPropertyValue("CreationDate", fCreationDate, value)
        End Set
    End Property

    <Association("CausationRecognitionDetailReferencesCausationRecognition", GetType(CausationRecognitionDetailXpo))>
    Public ReadOnly Property CausationRecognitionDetailXpo() As XPCollection(Of CausationRecognitionDetailXpo)
        Get
            Return GetCollection(Of CausationRecognitionDetailXpo)("CausationRecognitionDetailXpo")
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
