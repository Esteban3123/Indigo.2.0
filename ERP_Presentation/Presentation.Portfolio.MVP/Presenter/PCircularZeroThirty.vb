'***********************************************************************
' Assembly         : Presentacion.Inventory.MVP
' Author           : Faiber Julian Mora Dussan
' Created          : 06-10-2016
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports Presentation.Controls.MVP
Imports Infrastructure.Data.Xpo
Imports DevExpress.Xpo
Imports Presentation.Base

#End Region

Public Class PCircularZeroThirty

#Region "Variables"

    ''' <summary>
    ''' Referencia a la vista de la interfaz
    ''' </summary>
    Private View As ICircularZeroThirty

    ''' <summary>
    ''' Instancia a los valores de sesión
    ''' </summary>
    Private Indigo As SessionValues

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="iView"></param>
    Public Sub New(ByVal iView As ICircularZeroThirty)
        If iView Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        End If
        Me.View = iView
        Me.Indigo = SessionValues.Instance
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Carga los trimestres que ya han sido procesados
    ''' </summary>
    Public Async Sub LoadTrimesters()
        Await View.LoadTrimestersAsync()
    End Sub

#End Region

End Class
