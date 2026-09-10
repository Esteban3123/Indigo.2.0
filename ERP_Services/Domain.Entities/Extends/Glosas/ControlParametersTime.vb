'************************************************************
' Assembly         : Domain.Entities
' Author           : Juan Diego Diaz
' Created          : 02-07-2013
'
' Copyright        : (c) . All rights reserved.
'************************************************************


Public Class ControlParametersTime

    Private _code As String
    Property Code As String
        Get
            Return _code
        End Get
        Set(value As String)
            _code = value
        End Set
    End Property

    Private _operationDate As DateTime

    Property OperationDate As DateTime
        Get
            Return _operationDate
        End Get
        Set(value As DateTime)
            _operationDate = value
        End Set
    End Property

    Private _limitDate As DateTime

    Property LimitDate As DateTime
        Get
            Return _limitDate
        End Get
        Set(value As DateTime)
            _limitDate = value
        End Set
    End Property

    Private _timeParameters As Integer

    Property TimeParameters As Integer
        Get
            Return _timeParameters
        End Get
        Set(value As Integer)
            _timeParameters = value
        End Set
    End Property


    Private _remainingTime As Integer

    Property RemainingTime As Integer
        Get
            Return _remainingTime
        End Get
        Set(value As Integer)
            _remainingTime = value
        End Set
    End Property

    Private _responsibleNameCode As String
    Property ResponsibleNameCode As String
        Get
            Return _responsibleNameCode
        End Get
        Set(value As String)
            _responsibleNameCode = value
        End Set
    End Property

    Private _completeDate As Nullable(Of DateTime)

    Property CompleteDate As Nullable(Of DateTime)
        Get
            Return _completeDate
        End Get
        Set(value As Nullable(Of DateTime))
            _completeDate = value
        End Set
    End Property

    Private _spendTime As Integer

    Property SpendTime As Integer
        Get
            Return _spendTime
        End Get
        Set(value As Integer)
            _spendTime = value
        End Set
    End Property

End Class
