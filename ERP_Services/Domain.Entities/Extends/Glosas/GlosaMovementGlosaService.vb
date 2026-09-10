Imports System.Runtime.Serialization

'***********************************************************************
' Assembly         : Domain.Entities
' Author           : Rafael patiño
' Created          : 2014-11-13
'
' Last Modified By : Rafael E. Patiño
' Last Modified On : 2014-11-13
' Description      : Clase parcial y de servicios para la entidad GlosaMovementGlosa
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

''' <summary>
''' Clase parcial de la entidad <see cref="Domain.Entities.GlosaMovementGlosa" />
''' </summary>
''' <remarks></remarks>
Partial Public Class GlosaMovementGlosa

#Region "Manual Properties"
    <DataMember()>
    Public Property ResponsibleReiterationName As String
    <DataMember()>
    Public Property ResponsibleName As String
    <DataMember()>
    Public Property ConceptGlosasCodeName As String

    ''' <summary>
    ''' Valor aceptado por la entidad en coordinación
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property ValueAcceptedCoordination As Decimal


    Private _listMovementAux As List(Of GlosaMovementGlosa)
    <DataMember()>
    Property ListMovimientoAux As List(Of GlosaMovementGlosa)
        Get
            Return _listMovementAux
        End Get
        Set(value As List(Of GlosaMovementGlosa))
            _listMovementAux = value
        End Set
    End Property


    Private _TypeGlosa As Integer
    'Para Saber Si Es Una Glosa General o Manual
    <DataMember()>
    Public Property TypeGlosa As Integer
        Get
            Return Me._TypeGlosa
        End Get
        Set(value As Integer)
            Me._TypeGlosa = value
        End Set
    End Property

    Private _codeServiceQX As String
    <DataMember()>
    Public Property CodeServiceQX As String
        Get
            Return Me._codeServiceQX
        End Get
        Set(value As String)
            Me._codeServiceQX = value
        End Set
    End Property

    Private _nameServiceQX As String
    <DataMember()>
    Public Property NameServiceQX As String
        Get
            Return Me._nameServiceQX
        End Get
        Set(value As String)
            Me._nameServiceQX = value
        End Set
    End Property

    Private _serviceCodeName As String

    <DataMember()>
    Public Property ServiceCodeName As String
        Get
            Return Me._serviceCodeName
        End Get
        Set(value As String)
            Me._serviceCodeName = value
        End Set
    End Property


    Private _otherMovements As List(Of GlosaMovementGlosa)
    <DataMember()>
    Public Property OtherMovements As List(Of GlosaMovementGlosa)
        Get
            Return Me._otherMovements
        End Get
        Set(value As List(Of GlosaMovementGlosa))
            Me._otherMovements = value
        End Set
    End Property

    Private _responsibleCodeNameGlosa As String
    <DataMember()>
    Public Property ResponsibleCodeNameGlosa As String
        Get
            Return Me._responsibleCodeNameGlosa
        End Get
        Set(value As String)
            Me._responsibleCodeNameGlosa = value
        End Set
    End Property


    Private _responsibleCodeNameReiteration As String
    <DataMember()>
    Public Property ResponsibleCodeNameReiteration As String
        Get
            Return Me._responsibleCodeNameReiteration
        End Get
        Set(value As String)
            Me._responsibleCodeNameReiteration = value
        End Set
    End Property

    Private _maxValueAccepted As Nullable(Of Decimal)
    <DataMember()>
    Public Property MaxValueAccepted() As Nullable(Of Decimal)
        Get
            Return _maxValueAccepted
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            _maxValueAccepted = value
        End Set
    End Property

    Private _maxValueAcceptedGeneral As Nullable(Of Decimal)
    <DataMember()>
    Public Property MaxValueAcceptedGeneral() As Nullable(Of Decimal)
        Get
            Return _maxValueAcceptedGeneral
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            _maxValueAcceptedGeneral = value
        End Set
    End Property

    Private _valueGlosaMaxAccepted As String
    <DataMember()>
    Public Property ValueGlosaMaxAccepted As String
        Get
            Return Me._valueGlosaMaxAccepted
        End Get
        Set(value As String)
            Me._valueGlosaMaxAccepted = value
        End Set
    End Property

    Private _valueReiteratedMaxAccepted As String
    <DataMember()>
    Public Property ValueReiteratedMaxAccepted As String
        Get
            Return Me._valueReiteratedMaxAccepted
        End Get
        Set(value As String)
            Me._valueReiteratedMaxAccepted = value
        End Set
    End Property


    Private _valueAux As Nullable(Of Decimal)
    <DataMember()>
    Public Property ValueAux() As Nullable(Of Decimal)
        Get
            Return _valueAux
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            _valueAux = value
        End Set
    End Property

    <DataMember()>
    Public Property StateEvaluation() As Byte

    <DataMember()>
    Property ValPendingIPSconciliation As Decimal

    <DataMember()>
    Property ValPendingEAPBconciliation As Decimal

    <DataMember()>
    Property ValPendingConciliation As Decimal

    ''' <summary>
    ''' Obtiene o establece el Nit y el nombre del tercero
    ''' </summary>
    <DataMember()>
    Public Property ResponsibleThirdPartyNitName As String

    ''' <summary>
    ''' tipo del concepto de glosa de respuesta
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property GlosaEvaluationType As String

#End Region

#Region "Metodos y Funciones"

    ''' <summary>
    ''' Funcion para calcular el valor pendiente del movimiento
    ''' </summary>
    ''' <param name="conciliationStatus">
    ''' 0 - No se incluye
    ''' 1 - Sin confirmar
    ''' 2 - Confirmado
    ''' </param>
    ''' <returns></returns>
    Public Function CalculateValuePending(conciliationStatus As Byte) As Decimal
        Dim _valueGlosado As Decimal
        Dim _valueAcceptedIPSfirts As Decimal
        Dim _valueAcceptedIPSSecond As Decimal
        Dim _valueAcceptedEAPBReiterationas As Decimal
        Dim _valueAcceptedIPSConciliation As Decimal = 0
        Dim _valueAcceptedEAPBConciliation As Decimal = 0
        Dim _valuePartyalPayments As Decimal

        If Me.MainGlosa = True Then
            _valueGlosado = Me.ValueGlosado
        End If
        _valueAcceptedIPSfirts = IIf(Me.ValueAcceptedFirstInstance Is Nothing, 0, Me.ValueAcceptedFirstInstance)
        _valueAcceptedIPSSecond = IIf(Me.ValueAcceptedSecondInstance Is Nothing, 0, Me.ValueAcceptedSecondInstance)
        _valueAcceptedEAPBReiterationas = IIf(Me.ValueReiterationBalance Is Nothing, 0, Me.ValueReiterationBalance)
        If conciliationStatus > 0 Then
            If conciliationStatus = 1 Then
                _valueAcceptedIPSConciliation = IIf(Me.ValueAcceptedIPSconciliation Is Nothing, 0, Me.ValueAcceptedIPSconciliation)
                _valueAcceptedEAPBConciliation = IIf(Me.ValueAcceptedEAPBconciliation Is Nothing, 0, Me.ValueAcceptedEAPBconciliation)
            ElseIf conciliationStatus = 2 Then
                If Me.State = "6" Then
                    _valueAcceptedIPSConciliation = IIf(Me.ValueAcceptedIPSconciliation Is Nothing, 0, Me.ValueAcceptedIPSconciliation)
                    _valueAcceptedEAPBConciliation = IIf(Me.ValueAcceptedEAPBconciliation Is Nothing, 0, Me.ValueAcceptedEAPBconciliation)
                ElseIf Me.GlosaMovementGlosaConciliation IsNot Nothing Then
                    For Each c As GlosaMovementGlosaConciliation In Me.GlosaMovementGlosaConciliation.Where(Function(d) d.State = 2)
                        _valueAcceptedIPSConciliation += c.ValueAcceptedIPSconciliation
                        _valueAcceptedEAPBConciliation += c.ValueAcceptedEAPBconciliation
                    Next
                End If
            End If
        End If
        For Each p As PartialPaymentsMovement In Me.PartialPaymentsMovement
            _valuePartyalPayments = _valuePartyalPayments + p.ValueEAPB
        Next

        Return _valueGlosado - (_valueAcceptedIPSfirts + _valueAcceptedIPSSecond + _valueAcceptedEAPBReiterationas + _valueAcceptedIPSConciliation + _valueAcceptedEAPBConciliation + _valuePartyalPayments)
    End Function

#End Region

End Class