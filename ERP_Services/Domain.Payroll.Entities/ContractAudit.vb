'***********************************************************************
' Assembly         : Domain.Payroll
' Author           : Mariana Gonzalez Calderon
' Created          : 01/12/2025
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Collections.Specialized
Imports System.ComponentModel
Imports System.Globalization
Imports System.IO
Imports System.Reflection
Imports System.Runtime.Serialization.Formatters.Binary
Imports System.Runtime.Serialization
Imports System.Runtime.CompilerServices
Imports Domain.Base.Entities
Imports System.Data.Entity.ModelConfiguration

<DataContract(IsReference:=True), Serializable(), KnownType(GetType(ContractAudit))>
Partial Public Class ContractAudit
	Inherits Entity(Of ContractAudit)
    Implements IObjectWithChangeTracker
    Implements INotifyPropertyChanged

#Region "Aux Property"

	Private _ContractAuditAux As ContractAudit

	<DataMember()>
    Public Property ContractAuditAux As ContractAudit
        Get
            Return Me._ContractAuditAux
        End Get
        Set(value As ContractAudit)
            Me._ContractAuditAux = value
        End Set
    End Property


#End Region

#Region "Simple Properties"

	Private _id As Integer
	<DataMember()>
	Public Property Id() As Integer
        Get
            Return _id
        End Get
        Set(ByVal value As Integer)
            If Not Equals(_id, value) Then
                If ChangeTracker.ChangeTrackingEnabled AndAlso ChangeTracker.State <> ObjectState.Added Then
                    Throw New InvalidOperationException("The property 'Id' is part of the object's key and cannot be changed. Changes to key properties can only be made when the object is not being tracked or is in the Added state.")
                End If
                _id = value
                OnPropertyChanged("Id")
            End If
        End Set
    End Property

	Private _contractId As Integer
	<DataMember()>
	Public Property ContractId() As Integer
        Get
            Return _contractId
        End Get
        Set(ByVal value As Integer)
            If Not Equals(_contractId, value) Then
                _contractId = value
                OnPropertyChanged("ContractId")
            End If
        End Set
    End Property

	Private _type As String
	<DataMember()>
	Public Property Type() As String
        Get
            Return _type
        End Get
        Set(ByVal value As String)
            If Not Equals(_type, value) Then
                _type = value
                OnPropertyChanged("Type")
            End If
        End Set
    End Property

	Private _fieldName As String
	<DataMember()>
	Public Property FieldName() As String
        Get
            Return _fieldName
        End Get
        Set(ByVal value As String)
            If Not Equals(_fieldName, value) Then
                _fieldName = value
                OnPropertyChanged("FieldName")
            End If
        End Set
    End Property

	Private _valueOld As String
	<DataMember()>
	Public Property ValueOld() As String
        Get
            Return _valueOld
        End Get
        Set(ByVal value As String)
            If Not Equals(_valueOld, value) Then
                _valueOld = value
                OnPropertyChanged("ValueOld")
            End If
        End Set
    End Property

	Private _valueNew As String
	<DataMember()>
	Public Property ValueNew() As String
        Get
            Return _valueNew
        End Get
        Set(ByVal value As String)
            If Not Equals(_valueNew, value) Then
                _valueNew = value
                OnPropertyChanged("ValueNew")
            End If
        End Set
    End Property

	Private _userCode As String
	<DataMember()>
	Public Property UserCode() As String
        Get
            Return _userCode
        End Get
        Set(ByVal value As String)
            If Not Equals(_userCode, value) Then
                _userCode = value
                OnPropertyChanged("UserCode")
            End If
        End Set
    End Property

	Private _date As Date
	<DataMember()>
	Public Property [Date]() As Date
        Get
            Return _date
        End Get
        Set(ByVal value As Date)
            If Not Equals(_date, value) Then
                _date = value
                OnPropertyChanged("Date")
            End If
        End Set
    End Property

#End Region

#Region "ChangeTracking"

    Protected Overridable Sub OnPropertyChanged(ByVal propertyName As String)
        If ChangeTracker.State <> ObjectState.Added AndAlso ChangeTracker.State <> ObjectState.Deleted Then
            ChangeTracker.State = ObjectState.Modified
        End If
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
    End Sub

    Protected Overridable Sub OnNavigationPropertyChanged(ByVal propertyName As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
    End Sub

    Private Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
    Private _changeTracker As ObjectChangeTracker

    <DataMember()>
    Public Property ChangeTracker() As ObjectChangeTracker Implements IObjectWithChangeTracker.ChangeTracker
        Get
            If _changeTracker Is Nothing Then
                _changeTracker = New ObjectChangeTracker()
                AddHandler _changeTracker.ObjectStateChanging, AddressOf HandleObjectStateChanging
            End If
            Return _changeTracker
        End Get
        Set(ByVal value As ObjectChangeTracker)
            If _changeTracker IsNot Nothing Then
                RemoveHandler _changeTracker.ObjectStateChanging, AddressOf HandleObjectStateChanging
            End If
            _changeTracker = value
            If _changeTracker IsNot Nothing Then
                AddHandler _changeTracker.ObjectStateChanging, AddressOf HandleObjectStateChanging
            End If
        End Set
    End Property

    Private Sub HandleObjectStateChanging(ByVal sender As Object, ByVal e As ObjectStateChangingEventArgs)
        If e.NewState = ObjectState.Deleted Then
            Me.ClearNavigationProperties()
        End If
    End Sub

    Private _isDeserializing As Boolean
    Protected Property IsDeserializing() As Boolean
        Get
            Return _isDeserializing
        End Get
        Private Set(ByVal value As Boolean)
            _isDeserializing = value
        End Set
    End Property

    <OnDeserializing()>
    Public Sub OnDeserializingMethod(ByVal context As StreamingContext)
        IsDeserializing = True
    End Sub

    <OnDeserialized()>
    Public Sub OnDeserializedMethod(ByVal context As StreamingContext)
        IsDeserializing = False
        ChangeTracker.ChangeTrackingEnabled = True
    End Sub

    Protected Overridable Sub ClearNavigationProperties()
    End Sub

#End Region

End Class

