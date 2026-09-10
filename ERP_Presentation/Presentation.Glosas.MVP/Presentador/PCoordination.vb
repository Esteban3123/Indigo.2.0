'***********************************************************************
' Assembly         : Presentacion.Glosas.MVP
' Author           : Juan F. Tamayo
' Created          : 2013-07-22
'
' Last Modified By : Juan F. Tamayo
' Last Modified On : 2013-07-22
' Description      : Presentador del frontal de coordinación
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.Runtime.CompilerServices
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base

#End Region

''' <summary>
''' Presentador del frontal de coordinación
''' </summary>
Public Class PCoordination

#Region "Fields"

    ''' <summary>
    ''' Referencia a la interfaz del frontal de coordinación
    ''' </summary>
    Private _view As ICoordination
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
    Public Sub New(ByRef view As ICoordination)
        If view Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me._view = view
        End If
    End Sub

#End Region


End Class
