'***********************************************************************
' Assembly         : Presentacion.Portfolio.MVP
' Author           : Juan Carlos Bermudez
' Created          : 27-07-2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.Controls.MVP

#End Region

Public Class PPortfolioReclassification

#Region "Fields"

    ''' <summary>
    ''' Instancia de la interface
    ''' </summary>
    Dim View As IPortfolioReclassification

    ''' <summary>
    ''' Referencia a los valores de sesión
    ''' </summary>
    Dim Indigo As SessionValues

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="view">Referencia a la vista del frontal</param>
    Public Sub New(ByRef view As IPortfolioReclassification)
        If view Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.Indigo = SessionValues.Instance
            Me.View = view
        End If
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Loads the definition layout.
    ''' </summary>
    Public Async Sub LoadDefinitionLayout()
        Await Me.View.MyLayoutControl.LoadDefinitionAsync()
    End Sub

#End Region

End Class
