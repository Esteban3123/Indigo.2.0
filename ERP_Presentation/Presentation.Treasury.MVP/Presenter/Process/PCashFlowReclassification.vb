'***********************************************************************
' Assembly         : Presentacion.Treasury.MVP
' Author           : HECTOR RODRIGUEZ
' Created          : 21/11/2019
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.Data
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Presentation.Base
Imports Presentation.CloudAgent
Imports Presentation.Controls.MVP
#End Region

Public Class PCashFlowReclassification
    ''' <summary>
    ''' variable para comunicar con la interfaz
    ''' </summary>
    Dim View As ICashFlowReclassification

    ''' <summary>
    ''' variable que obtiene los valores de la sesion
    ''' </summary>
    Dim Indigo As SessionValues

    ''' <summary>
    ''' Constructor que comunica con la interfaz
    ''' </summary>
    Public Sub New(ByRef iView As ICashFlowReclassification)
        If iView Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        End If
        View = iView
        Indigo = SessionValues.Instance
    End Sub

    ''' <summary>
    ''' Loads the definition layout.
    ''' </summary>
    Public Async Sub LoadDefinitionLayout()
        Await Me.View.MyLayoutControl.LoadDefinitionAsync()
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="parametros"></param>
    Public Async Sub ListCashFlowStatus(ByVal parametros As String)
        Dim _ds = Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.ListCashFlowStatusAsync(parametros, Me.Indigo)
        If _ds IsNot Nothing Then
            Me.View.DetailsDatasource = _ds.Tables(0)
        Else
            Me.View.DetailsDatasource = Nothing
        End If

    End Sub

End Class
