Imports System.Runtime.Serialization
Partial Public Class GlosaObjectionsReceptionD
#Region "Manual Properties"


    Private _Image As Object
    Public Overridable Property Image As Object
        Get
            Return _Image
        End Get
        Set(value As Object)
            _Image = value
        End Set
    End Property

    Private _StateRecord As Boolean
    <DataMember()>
    Property StateRecord As Boolean
        Get
            Return _StateRecord
        End Get
        Set(value As Boolean)
            _StateRecord = value
        End Set
    End Property

    Private _StateSave As Boolean
    <DataMember()>
    Public Property StateSave As Boolean
        Get
            Return _StateSave
        End Get
        Set(value As Boolean)
            _StateSave = value
        End Set
    End Property

    Private _ListObjectionReceptionC As List(Of GlosaObjectionsReceptionC)
    <DataMember()>
    Public Property ListObjectionReceptionC() As List(Of GlosaObjectionsReceptionC)
        Get
            Return _ListObjectionReceptionC
        End Get
        Set(value As List(Of GlosaObjectionsReceptionC))
            _ListObjectionReceptionC = value
        End Set
    End Property

    Private _Reiterated As Boolean
    <DataMember()>
    Public Property Reiterated() As Boolean
        Get
            Return _Reiterated
        End Get
        Set(value As Boolean)
            _Reiterated = value
        End Set
    End Property

    Private _ReiterateId As Integer?
    <DataMember()>
    Public Property ReiterateId() As Integer?
        Get
            Return _ReiterateId
        End Get
        Set(value As Integer?)
            _ReiterateId = value
        End Set
    End Property

    Private _Invalidate As Boolean
    <DataMember()>
    Public Property Invalidate() As Boolean
        Get
            Return _Invalidate
        End Get
        Set(value As Boolean)
            _Invalidate = value
        End Set
    End Property

    Private _Processed As Boolean
    <DataMember()>
    Public Property Processed() As Boolean
        Get
            Return _Processed
        End Get
        Set(value As Boolean)
            _Processed = value
        End Set
    End Property

    Private _RadicateResponsibleCodeName As String

    <DataMember()>
    Public Property RadicateResponsibleCodeName As String
        Get
            Return _RadicateResponsibleCodeName
        End Get
        Set(value As String)
            _RadicateResponsibleCodeName = value
        End Set
    End Property

#End Region
End Class
