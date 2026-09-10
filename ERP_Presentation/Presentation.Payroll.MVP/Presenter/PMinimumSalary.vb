'***********************************************************************
' Assembly         : Presentation.Payroll.MVP
' Author           : Juan Pablo Daza Medina
' Created          : 28-10-2022
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Infrastructure.Data.Xpo.PayrollRepository
Imports Infrastructure.Data.Xpo
Imports DevExpress.Xpo
#End Region

Public Class PMinimumSalary
#Region "Fields"
    ''' <summary>
    ''' Variable para almacenar al formulario de Salario Mínimo
    ''' </summary>
    ''' <remarks></remarks>
    Private _view As IMinimumSalary
    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Private Indigo As SessionValues = SessionValues.Instance

    Private __sessionVales As SessionValues
#End Region
#Region "Builder"
    Public Sub New(ByRef view As IMinimumSalary)
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
    ''' <summary>
    ''' Obtiene los datos de la moneda oficial
    ''' </summary>
    ''' <returns></returns>
    Public Function LoadPayrollSettings() As PayrollSettingsXpo
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).PayrollService.GetCollection(Of PayrollSettingsXpo).FirstOrDefault()
    End Function

#End Region

End Class
