'***********************************************************************
' Assembly         : Presentacion.Glosas.MVP
' Author           : Rafael Patiño
' Created          : 2014-10-10
'
' Last Modified By : Rafael Patiño
' Last Modified On : 2014-10-10
' Description      : Presentador del frontal de pago parciales
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.Runtime.CompilerServices
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base

#End Region

''' <summary>
''' Presentador del frontal de pago parciales
''' </summary>
Public Class PPartialPayments

#Region "Fields"

    ''' <summary>
    ''' Referencia a la interfaz del frontal de pago parciales
    ''' </summary>
    Private _view As IPartialPayments
    ''' <summary>
    ''' Objeto de la pago parciales
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
    Public Sub New(ByRef view As IPartialPayments)
        If view Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me._view = view
        End If
    End Sub

#End Region

End Class
