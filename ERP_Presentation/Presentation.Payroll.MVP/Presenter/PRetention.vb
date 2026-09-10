'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Kevin Garay Rodriguez
' Created          : 08-07-2013
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
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Common.MVP

#End Region
''' <summary>
''' Esta presentador captura toda la logica aplicada en el frontal de plantilla de contrato
''' </summary>
Public Class PRetention

#Region "Fields"

    ''' <summary>
    ''' Variable utilizada para instanciar la interfaz IRetention
    ''' </summary>
    Private _view As IRetention
    ''' <summary>
    ''' Variable que se utilizapa para tratar las retenciones como un Objeto
    ''' </summary>
    Private _retention As Object
    ''' <summary>
    ''' Variable que se utiliza para instanciar la clase singlenton
    ''' </summary>
    Private _indigo As SessionValues = SessionValues.Instance

#End Region

#Region "Builders"

    ''' <summary>
    ''' Constructor de la clase presentador en el frontal de plantillas de contrato
    ''' </summary>
    ''' <param name="view">Vista de plantilla de contrato</param>
    Public Sub New(ByRef view As IRetention)
        If view Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me._view = view
        End If
    End Sub

    ''' <summary>
    ''' Metodo para inicializar el consecutivo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function initialize() As task
        Using model As New MRetention
            _view.Consecutive = Await model.GetConsecutive()
        End Using
    End Function

#End Region
End Class
