Imports System.Runtime.Serialization

'***********************************************************************
' Assembly         : Domain.Entities
' Author           : Rafael patiño
' Created          : 2014-11-13
'
' Last Modified By : Rafael E. Patiño
' Last Modified On : 2014-11-13
' Description      : Clase parcial y de servicios para la entidad GlosaInvoiceDetail
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

''' <summary>
''' Clase de servicios para la entidad <see cref="Domain.Entities.GlosaInvoiceDetailQXService " />
''' </summary>
Public Class GlosaInvoiceDetailqxService

End Class

''' <summary>
''' Clase parcial de la entidad <see cref="Domain.Entities.GlosaInvoiceDetailQXService" />
''' </summary>
''' <remarks></remarks>
Partial Public Class GlosaInvoiceDetailQX

#Region "Metodos y Funciones"

    ''' <summary>
    ''' calcula lo aceptado por la IPS
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CalculateAcceptedIPSConciliation() As Decimal
        Dim a As Decimal
        For Each mov As GlosaMovementGlosa In Me.GlosaMovementGlosa
            a += IIf(mov.ValueAcceptedIPSconciliation Is Nothing, 0, mov.ValueAcceptedIPSconciliation)
        Next
        Return a
    End Function

    ''' <summary>
    ''' calcula lo aceptado por la EPS
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CalculateAcceptedEAPBConciliation() As Decimal
        Dim a As Decimal
        For Each mov As GlosaMovementGlosa In Me.GlosaMovementGlosa
            a += IIf(mov.ValueAcceptedEAPBconciliation Is Nothing, 0, mov.ValueAcceptedEAPBconciliation)
        Next
        Return a
    End Function

    ''' <summary>
    ''' Funcion para calcular el valor pendiente
    ''' </summary>
    ''' <param name="conciliationStatus">
    ''' 0 - No se incluye
    ''' 1 - Sin confirmar
    ''' 2 - Confirmado
    ''' </param>
    ''' <returns></returns>
    Public Function CalculateValuePending(conciliationStatus As Byte) As Decimal
        Dim _valuePending As Decimal
        For Each mov As GlosaMovementGlosa In Me.GlosaMovementGlosa
            _valuePending += mov.CalculateValuePending(conciliationStatus)
        Next
        Return _valuePending
    End Function

#End Region


#Region "Manual Properties"

    Private _StateRecord As Byte
    Property StateRecord As Byte
        Get
            Return _StateRecord
        End Get
        Set(value As Byte)
            _StateRecord = value
        End Set
    End Property


    Private _ValueGlosadoFacade As Decimal
    <DataMember()>
    Public Property ValueGlosadoFacade() As Decimal
        Get
            Return Me._ValueGlosadoFacade
        End Get
        Set(value As Decimal)
            Me._ValueGlosadoFacade = value
        End Set
    End Property


    Private _ValueReiterated As Decimal
    <DataMember()>
    Public Property ValueReiteratedFacade() As Decimal
        Get
            Return Me._ValueReiterated
        End Get
        Set(value As Decimal)
            Me._ValueReiterated = value
        End Set
    End Property

    Private _ValueAcceptedFirstInstance As Decimal
    <DataMember()>
    Public Property ValueAcceptedFirstInstanceadoFacade() As Decimal
        Get
            Return Me._ValueAcceptedFirstInstance
        End Get
        Set(value As Decimal)
            Me._ValueAcceptedFirstInstance = value
        End Set
    End Property

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


    Private _ValueAcceptedIPSconciliation As Decimal
    <DataMember()>
    Public Property ValueAcceptedIPSconciliationFacade() As Decimal
        Get
            Return Me._ValueAcceptedIPSconciliation
        End Get
        Set(value As Decimal)
            Me._ValueAcceptedIPSconciliation = value
        End Set
    End Property


    Private _ValueAcceptedEAPBconciliation As Decimal
    <DataMember()>
    Public Property ValueAcceptedEAPBconciliationFacade() As Decimal
        Get
            Return Me._ValueAcceptedEAPBconciliation
        End Get
        Set(value As Decimal)
            Me._ValueAcceptedEAPBconciliation = value
        End Set
    End Property


    Private _ValuePendingConciliation As Decimal
    <DataMember()>
    Public Property ValuePendingConciliationFacade() As Decimal
        Get
            Return Me._ValuePendingConciliation
        End Get
        Set(value As Decimal)
            Me._ValuePendingConciliation = value
        End Set
    End Property

    Private _ValuePendingConciliationTmp As Decimal
    <DataMember()>
    Public Property ValuePendingConciliationTmpFacade() As Decimal
        Get
            Return Me._ValuePendingConciliationTmp
        End Get
        Set(value As Decimal)
            Me._ValuePendingConciliationTmp = value
        End Set
    End Property


    Private _ServiceCodeName As String
    <DataMember()>
    Public Property ServiceCodeName() As String
        Get
            Return Me._ServiceCodeName
        End Get
        Set(value As String)
            Me._ServiceCodeName = value
        End Set
    End Property


    Private _ServiceAreaCodeName As String
    <DataMember()>
    Public Property ServiceAreaCodeName() As String
        Get
            Return Me._ServiceAreaCodeName
        End Get
        Set(value As String)
            Me._ServiceAreaCodeName = value
        End Set
    End Property

    Private _MedicalCodeName As String
    <DataMember()>
    Public Property MedicalCodeName() As String
        Get
            Return Me._MedicalCodeName
        End Get
        Set(value As String)
            Me._MedicalCodeName = value
        End Set
    End Property

    Private _CostCenterCodeName As String
    <DataMember()>
    Public Property CostCenterCodeName() As String
        Get
            Return Me._CostCenterCodeName
        End Get
        Set(value As String)
            Me._CostCenterCodeName = value
        End Set
    End Property


    Private _movimientoAux As GlosaMovementGlosa
    <DataMember()>
    Property MovimientoAux As GlosaMovementGlosa
        Get
            Return _movimientoAux
        End Get
        Set(value As GlosaMovementGlosa)
            _movimientoAux = value
        End Set
    End Property


#End Region

End Class