'***********************************************************************
' Assembly         : Presentacion.Accounting.MVP
' Author           : Diego Andrés Roldán
' Created          : 10-04-2014
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
#End Region

''' <summary>
''' Presentador del frontal Tarjetas
''' </summary>
Public Class PPatrimonialPart
    ''' <summary>
    ''' variable para comunicar con la interfaz
    ''' </summary>
    Dim View As IPatrimonialPart

    ''' <summary>
    ''' variable que obtiene los valores de la sesion
    ''' </summary>
    Dim Indigo As SessionValues

    ''' <summary>
    ''' Constructor que comunica con la interfaz
    ''' </summary>
    Public Sub New(ByRef iView As IPatrimonialPart)
        If iView Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        End If
        View = iView
        Indigo = SessionValues.Instance
    End Sub

    ''' <summary>
    ''' Obtiene la secuencia
    ''' </summary>
    Public Async Sub GetSequence()
        Using model As New MPatrimonialPart(View.MyTag)
            Me.View.Sequense = Await model.GetSequense()
        End Using
    End Sub

End Class
