'***********************************************************************
' Assembly         : Presentacion.Glosas.MVP
' Author           : Juan F. Tamayo
' Created          : 2013-04-20
'
' Last Modified By : Juan F. Tamayo
' Last Modified On : 2013-04-20
' Description      : Presentador del frontal de conciliación
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.Runtime.CompilerServices
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base

#End Region

''' <summary>
''' Presentador del frontal de conciliación
''' </summary>
Public Class PConciliation

#Region "Fields"

    ''' <summary>
    ''' Referencia a la interfaz del frontal de conciliación
    ''' </summary>
    Private _view As IConciliation
    ''' <summary>
    ''' Objeto de la conciliación
    ''' </summary>
    Private _conciliation As Object
    ''' <summary>
    ''' Referencia a los valores de sesion
    ''' </summary>
    Private _indigoSessionValues As SessionValues = SessionValues.Instance

#End Region

#Region "Builders"

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <param name="view">Referencia a la vista</param>
    Public Sub New(ByRef view As IConciliation)
        If view Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me._view = view
        End If
    End Sub

#End Region

#Region "Methods"

#End Region

End Class

''' <summary>
''' Metodos extendidos para las entidades de conciliacion
''' </summary>
Public Module ConciliationExtendedMethods

    ''' <summary>
    ''' Obtiene el valor total glosado en los movimientos del detalle
    ''' </summary>
    ''' <param name="obj">Entidad de tipo <see cref=" Domain.Entities.GlosaInvoiceDetail" /></param>
    ''' <returns>La sumatoria de los valores</returns>
    <Extension()>
    Public Function GetTotalObjections(ByVal obj As  Domain.Entities.GlosaInvoiceDetail) As Decimal
        Dim s As Decimal = 0
        If obj.GlosaInvoiceDetailQX IsNot Nothing AndAlso obj.GlosaInvoiceDetailQX.Count > 0 Then
            For Each m As  Domain.Entities.GlosaInvoiceDetailQX In obj.GlosaInvoiceDetailQX
                s += m.GetTotalObjections()
            Next
        Else
            For Each m As  Domain.Entities.GlosaMovementGlosa In (From mo As  Domain.Entities.GlosaMovementGlosa In obj.GlosaMovementGlosa Where mo.MainGlosa = True).ToList()
                s += m.ValueGlosado
            Next
        End If
        Return s
    End Function

    ''' <summary>
    ''' Obtiene el valor total glosado en los movimientos del detalle
    ''' </summary>
    ''' <param name="obj">Entidad de tipo <see cref=" Domain.Entities.GlosaInvoiceDetailQX" /></param>
    ''' <returns>La sumatoria de los valores</returns>
    <Extension()>
    Public Function GetTotalObjections(ByVal obj As  Domain.Entities.GlosaInvoiceDetailQX) As Decimal
        Dim s As Decimal = 0
        For Each m As  Domain.Entities.GlosaMovementGlosa In (From mv As  Domain.Entities.GlosaMovementGlosa In obj.GlosaInvoiceDetail.GlosaMovementGlosa Where mv.InvoiceDetailIdQX = obj.Id And mv.MainGlosa = True).ToList()
            s += m.ValueGlosado
        Next
        Return s
    End Function

    ''' <summary>
    ''' Obtiene el valor total reiterado en los movimientos del detalle
    ''' </summary>
    ''' <param name="obj">Entidad de tipo <see cref=" Domain.Entities.GlosaInvoiceDetail" /></param>
    ''' <returns>La sumatoria de los valores</returns>
    <Extension()>
    Public Function GetTotalReiterated(ByVal obj As  Domain.Entities.GlosaInvoiceDetail) As Decimal
        Dim s As Decimal = 0
        If obj.GlosaInvoiceDetailQX IsNot Nothing AndAlso obj.GlosaInvoiceDetailQX.Count > 0 Then
            For Each m As  Domain.Entities.GlosaInvoiceDetailQX In obj.GlosaInvoiceDetailQX
                s += m.GetTotalReiterated()
            Next
        Else
            For Each m As  Domain.Entities.GlosaMovementGlosa In (From mo As  Domain.Entities.GlosaMovementGlosa In obj.GlosaMovementGlosa Where mo.MainGlosa = True).ToList()
                s += IIf(m.ValueReiterated Is Nothing, 0, m.ValueReiterated)
            Next
        End If
        Return s
    End Function

    ''' <summary>
    ''' Obtiene el valor total reiterado en los movimientos del detalle
    ''' </summary>
    ''' <param name="obj">Entidad de tipo <see cref=" Domain.Entities.GlosaInvoiceDetailQX" /></param>
    ''' <returns>La sumatoria de los valores</returns>
    <Extension()>
    Public Function GetTotalReiterated(ByVal obj As  Domain.Entities.GlosaInvoiceDetailQX) As Decimal
        Dim s As Decimal = 0
        For Each m As  Domain.Entities.GlosaMovementGlosa In (From mv As  Domain.Entities.GlosaMovementGlosa In obj.GlosaInvoiceDetail.GlosaMovementGlosa Where mv.InvoiceDetailIdQX = obj.Id And mv.MainGlosa = True).ToList()
            s += IIf(m.ValueReiterated Is Nothing, 0, m.ValueReiterated)
        Next
        Return s
    End Function

    ''' <summary>
    ''' Obtiene el valor total aceptado en primera instancia en los movimientos del detalle
    ''' </summary>
    ''' <param name="obj">Entidad de tipo <see cref=" Domain.Entities.GlosaInvoiceDetail" /></param>
    ''' <returns>La sumatoria de los valores</returns>
    <Extension()>
    Public Function GetTotalAcceptedFirstInstance(ByVal obj As  Domain.Entities.GlosaInvoiceDetail) As Decimal
        Dim s As Decimal = 0
        If obj.GlosaInvoiceDetailQX IsNot Nothing AndAlso obj.GlosaInvoiceDetailQX.Count > 0 Then
            For Each m As  Domain.Entities.GlosaInvoiceDetailQX In obj.GlosaInvoiceDetailQX
                s += m.GetTotalAcceptedFirstInstance()
            Next
        Else
            For Each m As  Domain.Entities.GlosaMovementGlosa In (From mo As  Domain.Entities.GlosaMovementGlosa In obj.GlosaMovementGlosa).ToList()
                'For Each m As  Domain.Entities.GlosaMovementGlosa In (From mo As  Domain.Entities.GlosaMovementGlosa In obj.GlosaMovementGlosa Where mo.MainGlosa = True).ToList()
                s += IIf(m.ValueAcceptedFirstInstance Is Nothing, 0, m.ValueAcceptedFirstInstance)
            Next
        End If
        Return s
    End Function

    ''' <summary>
    ''' Obtiene el valor total aceptado en primera instancia en los movimientos del detalle
    ''' </summary>
    ''' <param name="obj">Entidad de tipo <see cref=" Domain.Entities.GlosaInvoiceDetailQX" /></param>
    ''' <returns>La sumatoria de los valores</returns>
    <Extension()>
    Public Function GetTotalAcceptedFirstInstance(ByVal obj As  Domain.Entities.GlosaInvoiceDetailQX) As Decimal
        Dim s As Decimal = 0
        ' For Each m As  Domain.Entities.GlosaMovementGlosa In (From mv As  Domain.Entities.GlosaMovementGlosa In obj.GlosaInvoiceDetail.GlosaMovementGlosa Where mv.InvoiceDetailIdQX = obj.Id And mv.MainGlosa = True).ToList()
        For Each m As  Domain.Entities.GlosaMovementGlosa In (From mv As  Domain.Entities.GlosaMovementGlosa In obj.GlosaInvoiceDetail.GlosaMovementGlosa Where mv.InvoiceDetailIdQX = obj.Id).ToList()
            s += IIf(m.ValueAcceptedFirstInstance Is Nothing, 0, m.ValueAcceptedFirstInstance)
        Next
        Return s
    End Function

    ''' <summary>
    ''' Obtiene el valor total aceptado en segunda instancia en los movimientos del detalle
    ''' </summary>
    ''' <param name="obj">Entidad de tipo <see cref=" Domain.Entities.GlosaInvoiceDetail" /></param>
    ''' <returns>La sumatoria de los valores</returns>
    <Extension()>
    Public Function GetTotalAcceptedSecondInstance(ByVal obj As  Domain.Entities.GlosaInvoiceDetail) As Decimal
        Dim s As Decimal = 0
        If obj.GlosaInvoiceDetailQX IsNot Nothing AndAlso obj.GlosaInvoiceDetailQX.Count > 0 Then
            For Each m As  Domain.Entities.GlosaInvoiceDetailQX In obj.GlosaInvoiceDetailQX
                s += m.GetTotalAcceptedSecondInstance()
            Next
        Else
            For Each m As  Domain.Entities.GlosaMovementGlosa In (From mo As  Domain.Entities.GlosaMovementGlosa In obj.GlosaMovementGlosa Where mo.MainGlosa = True).ToList()
                s += IIf(m.ValueAcceptedSecondInstance Is Nothing, 0, m.ValueAcceptedSecondInstance)
            Next
        End If
        Return s
    End Function

    ''' <summary>
    ''' Obtiene el valor total aceptado en segunda instancia en los movimientos del detalle
    ''' </summary>
    ''' <param name="obj">Entidad de tipo <see cref=" Domain.Entities.GlosaInvoiceDetailQX" /></param>
    ''' <returns>La sumatoria de los valores</returns>
    <Extension()>
    Public Function GetTotalAcceptedSecondInstance(ByVal obj As  Domain.Entities.GlosaInvoiceDetailQX) As Decimal
        Dim s As Decimal = 0
        For Each m As  Domain.Entities.GlosaMovementGlosa In (From mv As  Domain.Entities.GlosaMovementGlosa In obj.GlosaInvoiceDetail.GlosaMovementGlosa Where mv.InvoiceDetailIdQX = obj.Id And mv.MainGlosa = True).ToList()
            s += IIf(m.ValueAcceptedSecondInstance Is Nothing, 0, m.ValueAcceptedSecondInstance)
        Next
        Return s
    End Function

    ''' <summary>
    ''' Obtiene el valor total aceptado por la IPS en los movimientos del detalle
    ''' </summary>
    ''' <param name="obj">Entidad de tipo <see cref=" Domain.Entities.GlosaInvoiceDetail" /></param>
    ''' <returns>La sumatoria de los valores</returns>
    <Extension()>
    Public Function GetTotalAcceptedIPSconciliation(ByVal obj As  Domain.Entities.GlosaInvoiceDetail) As Decimal
        Dim s As Decimal = 0
        If obj.GlosaInvoiceDetailQX IsNot Nothing AndAlso obj.GlosaInvoiceDetailQX.Count > 0 Then
            For Each m As  Domain.Entities.GlosaInvoiceDetailQX In obj.GlosaInvoiceDetailQX
                s += m.GetTotalAcceptedIPSconciliation()
            Next
        Else
            'For Each m As  Domain.Entities.GlosaMovementGlosa In (From mo As  Domain.Entities.GlosaMovementGlosa In obj.GlosaMovementGlosa Where mo.MainGlosa = True).ToList()
            For Each m As  Domain.Entities.GlosaMovementGlosa In (From mo As  Domain.Entities.GlosaMovementGlosa In obj.GlosaMovementGlosa).ToList()
                s += IIf(m.ValueAcceptedIPSconciliation Is Nothing, 0, m.ValueAcceptedIPSconciliation)
            Next
        End If
        Return s
    End Function

    ''' <summary>
    ''' Obtiene el valor total aceptado por la IPS en los movimientos del detalle
    ''' </summary>
    ''' <param name="obj">Entidad de tipo <see cref=" Domain.Entities.GlosaInvoiceDetailQX" /></param>
    ''' <returns>La sumatoria de los valores</returns>
    <Extension()>
    Public Function GetTotalAcceptedIPSconciliation(ByVal obj As  Domain.Entities.GlosaInvoiceDetailQX) As Decimal
        Dim s As Decimal = 0
        'For Each m As  Domain.Entities.GlosaMovementGlosa In (From mv As  Domain.Entities.GlosaMovementGlosa In obj.GlosaInvoiceDetail.GlosaMovementGlosa Where mv.InvoiceDetailIdQX = obj.Id And mv.MainGlosa = True).ToList()
        For Each m As  Domain.Entities.GlosaMovementGlosa In (From mv As  Domain.Entities.GlosaMovementGlosa In obj.GlosaInvoiceDetail.GlosaMovementGlosa Where mv.InvoiceDetailIdQX = obj.Id).ToList()
            s += IIf(m.ValueAcceptedIPSconciliation Is Nothing, 0, m.ValueAcceptedIPSconciliation)
        Next
        Return s
    End Function

    ''' <summary>
    ''' Obtiene el valor total aceptado por la EAPB en los movimientos del detalle
    ''' </summary>
    ''' <param name="obj">Entidad de tipo <see cref=" Domain.Entities.GlosaInvoiceDetail" /></param>
    ''' <returns>La sumatoria de los valores</returns>
    <Extension()>
    Public Function GetTotalAcceptedEAPBconciliation(ByVal obj As  Domain.Entities.GlosaInvoiceDetail) As Decimal
        Dim s As Decimal = 0
        If obj.GlosaInvoiceDetailQX IsNot Nothing AndAlso obj.GlosaInvoiceDetailQX.Count > 0 Then
            For Each m As  Domain.Entities.GlosaInvoiceDetailQX In obj.GlosaInvoiceDetailQX
                s += m.GetTotalAcceptedEAPBconciliation()
            Next
        Else
            For Each m As  Domain.Entities.GlosaMovementGlosa In (From mo As  Domain.Entities.GlosaMovementGlosa In obj.GlosaMovementGlosa Where mo.MainGlosa = True).ToList()
                s += IIf(m.ValueAcceptedEAPBconciliation Is Nothing, 0, m.ValueAcceptedEAPBconciliation)
            Next
            'Dim balanceREalAcceptEAPB As Decimal = (GetTotalObjections(obj) - GetTotalAcceptedFirstInstance(obj) - GetTotalAcceptedSecondInstance(obj)) - getto
        End If
        Return s
    End Function

    ''' <summary>
    ''' Obtiene el valor total aceptado por la EAPB en los movimientos del detalle
    ''' </summary>
    ''' <param name="obj">Entidad de tipo <see cref=" Domain.Entities.GlosaInvoiceDetailQX" /></param>
    ''' <returns>La sumatoria de los valores</returns>
    <Extension()>
    Public Function GetTotalAcceptedEAPBconciliation(ByVal obj As  Domain.Entities.GlosaInvoiceDetailQX) As Decimal
        Dim s As Decimal = 0
        For Each m As  Domain.Entities.GlosaMovementGlosa In (From mv As  Domain.Entities.GlosaMovementGlosa In obj.GlosaInvoiceDetail.GlosaMovementGlosa Where mv.InvoiceDetailIdQX = obj.Id And mv.MainGlosa = True).ToList()
            s += IIf(m.ValueAcceptedEAPBconciliation Is Nothing, 0, m.ValueAcceptedEAPBconciliation)
        Next
        Return s
    End Function


    <Extension()>
    Public Function GetValuePendingConciliation(ByVal obj As  Domain.Entities.GlosaInvoiceDetail) As Decimal
        Dim s As Decimal = 0
        'For Each m As  Domain.Entities.GlosaMovementGlosa In (From mo As  Domain.Entities.GlosaMovementGlosa In obj.GlosaMovementGlosa Where mo.MainGlosa = True).ToList()
        For Each m As  Domain.Entities.GlosaMovementGlosa In (From mo As  Domain.Entities.GlosaMovementGlosa In obj.GlosaMovementGlosa).ToList()
            s += IIf(m.ValuePendingConciliation Is Nothing, 0, m.ValuePendingConciliation)
        Next
        Return s
    End Function


    <Extension()>
    Public Function GetValuePartialPayments(ByVal obj As  Domain.Entities.GlosaInvoiceDetail) As Decimal
        Dim s As Decimal = 0
        For Each m As  Domain.Entities.GlosaMovementGlosa In (From mo As  Domain.Entities.GlosaMovementGlosa In obj.GlosaMovementGlosa Where mo.MainGlosa = True).ToList()
            For Each p As  Domain.Entities.PartialPaymentsMovement In m.PartialPaymentsMovement
                s += IIf(p.ValueEAPB Is Nothing, 0, p.ValueEAPB)
            Next
        Next
        Return s
    End Function

End Module

''' <summary>
''' Encapsula los valores de la factura glosada
''' </summary>
Public Structure InvoiceValues

    ''' <summary>
    ''' Obtiene o asigna el valor de la glosa u objeción
    ''' </summary>
    ''' <value>Valor de la objeción</value>
    ''' <returns>El valor de la objeción</returns>
    Public Property ObjectionValue As Decimal
    ''' <summary>
    ''' Obtiene o asigna el valor de la reiteración
    ''' </summary>
    ''' <value>Valor de la reiteración</value>
    ''' <returns>El valor de la reiteración</returns>
    Public Property ReiteratedValue As Decimal
    ''' <summary>
    ''' Obtiene o asigna el valor aceptado en la primera instancia
    ''' </summary>
    ''' <value>Valor aceptado en la primera instancia</value>
    ''' <returns>El valor aceptado en la primera instancia</returns>
    Public Property FirstInstanceAceptedValue As Decimal
    ''' <summary>
    ''' Obtiene o asigna el valor aceptado en la segunda instancia
    ''' </summary>
    ''' <value>Valor aceptado en la segunda instancia</value>
    ''' <returns>El valor aceptado en la segunda instancia</returns>
    Public Property SecondInstanceAceptedValue As Decimal
    ''' <summary>
    ''' Obtiene o asigna el valor aceptado por la IPS en la conciliación
    ''' </summary>
    ''' <value>Valor aceptado por la IPS</value>
    ''' <returns>El valor aceptado por la IPS</returns>
    Public Property IPSConciliationAceptedValue As Decimal
    ''' <summary>
    ''' Obtiene o asigna los valores pendientes de conciliacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PendingValueConciliation As Decimal
    ''' <summary>
    ''' valor aceptado por EAPB en reiteracion 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ValueAccceptEAPBReiteration As Decimal

    ''' <summary>
    ''' valor de pago parcial
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ValuePartyalPayments As Decimal

End Structure