Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Contract.GroupersCareGroup")>
Public Class GroupersCareGroupXpo
    Inherits XPLiteObject

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

    Dim fCareGroupId As CareGroupReportXpo
    <Association("Contract_GroupersCareGroupReferencesContract_CareGroup")>
    Public Property CareGroupId() As CareGroupReportXpo
        Get
            Return fCareGroupId
        End Get
        Set(ByVal value As CareGroupReportXpo)
            SetPropertyValue(Of CareGroupReportXpo)("CareGroupId", fCareGroupId, value)
        End Set
    End Property

    Dim fGroupersId As GroupersXpo
    <Association("Contract_GroupersCareGroupReferencesContract_Group")>
    Public Property GroupersId() As GroupersXpo
        Get
            Return fGroupersId
        End Get
        Set(ByVal value As GroupersXpo)
            SetPropertyValue(Of GroupersXpo)("GroupersId", fGroupersId, value)
        End Set
    End Property

    Dim fEjectEvent As Integer
    Public Property EjectEvent() As Integer
        Get
            Return fEjectEvent
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("EjectEvent", fEjectEvent, value)
        End Set
    End Property

    Dim fRealCME As Decimal
    Public Property RealCME() As Decimal
        Get
            Return fRealCME
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("RealCME", fRealCME, value)
        End Set
    End Property

    Dim fTotalEject As Decimal
    Public Property TotalEject() As Decimal
        Get
            Return fTotalEject
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TotalEject", fTotalEject, value)
        End Set
    End Property

    Dim fDocumentDate As DateTime
    Public Property DocumentDate() As DateTime
        Get
            Return fDocumentDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("DocumentDate", fDocumentDate, value)
        End Set
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
