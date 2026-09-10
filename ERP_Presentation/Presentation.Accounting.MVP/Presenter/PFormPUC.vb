'***********************************************************************
' Assembly         : Presentacion.Accounting.MVP
' Author           : Sergio Abraham Fernandez Cruz
' Created          : 05-05-2014
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
Public Class PFormPUC

#Region "Variables"

    ''' <summary>
    ''' Variable para instanciar los valores de la sesion
    ''' </summary>
    Dim _indigo As SessionValues

    ''' <summary>
    ''' variable utilizada para comunicarse con la interfaz
    ''' </summary>
    Dim _view As IFormPUC

#End Region

#Region "Constructor"

    ''' <summary>
    ''' Comunica el presentador con la interfaz e inicia la instancia de la singleton
    ''' </summary>
    Public Sub New(ByRef iview As IFormPUC)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        End If
        _view = iview
        _indigo = SessionValues.Instance
    End Sub

#End Region

End Class
