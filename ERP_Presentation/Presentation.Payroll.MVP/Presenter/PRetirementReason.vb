'***********************************************************************
' Assembly         : Presentation.Payroll.MVP
' Author           : Cristhian Mauricio Salazar
' Created          : 1-04-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base

Public Class PRetirementReason

    ''' <summary>
    ''' Variable para almacenar al formulario de RetirementReason
    ''' </summary>
    ''' <remarks></remarks>
    Private _view As IRetirementReason

    Public Sub New(ByRef view As IRetirementReason)
        If view Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        End If
        _view = view
    End Sub

    Public Async Sub GetSequence()
        Using model As New MBlockRecordAndSequensePayroll(_view.MyTag)
            Me._view.Sequence = Await model.GetSequense()
        End Using
    End Sub
End Class
