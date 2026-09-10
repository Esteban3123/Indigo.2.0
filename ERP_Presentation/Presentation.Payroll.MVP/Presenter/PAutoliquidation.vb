'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Jose Luis Rojas
' Created          : 08-04-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.Common.MVP

Public Class PAutoliquidation

    ''' <summary>
    ''' Variable para almacenar el frontal de auto liquidacion
    ''' </summary>
    ''' <remarks></remarks>
    Private _view As IAutoliquidation

    ''' <summary>
    ''' Contructor del presentador
    ''' </summary>
    ''' <param name="view">Vista de la autoliquidacion</param>
    ''' <remarks></remarks>
    Public Sub New(view As IAutoliquidation)
        If view Is Nothing Then
            Throw New ArgumentNullException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        End If
        _view = view
    End Sub


End Class
