'***********************************************************************
' Assembly         : Presentation.Payroll.MVP
' Author           : Andrés Steven Rojas 
' Created          : 03-10-2023
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
#End Region
Public Class PLicensingConcepts
#Region "Fields"
    ''' <summary>
    ''' Variable para almacenar al formulario de Salario Mínimo
    ''' </summary>
    ''' <remarks></remarks>
    Private _view As ILicensingConcepts

    Private __sessionVales As SessionValues
#End Region
#Region "Builder"
    Public Sub New(ByRef view As ILicensingConcepts)
        If view Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        End If
        _view = view
    End Sub

#End Region

#Region "Metodos"
    Public Async Sub GetSequence()
        Using model As New MBlockRecordAndSequensePayroll(Me._view.MyTag)
            Me._view.Sequence = Await model.GetSequense
        End Using
    End Sub

#End Region
End Class
