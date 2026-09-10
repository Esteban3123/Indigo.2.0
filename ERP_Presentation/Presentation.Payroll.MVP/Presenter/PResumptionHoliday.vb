'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 29/06/2016
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
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.PayrollRepository
Imports DevExpress.Data.Linq

#End Region

''' <summary>
''' Esta presentador captura toda la logica aplicada en el frontal 
''' </summary>
Public Class PResumptionHoliday

#Region "Variables"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IResumptionHoliday


    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues

#End Region

#Region "Builder"

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz
    ''' </summary>
    ''' <param name="iview">Iview</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As IResumptionHoliday)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
        Indigo = SessionValues.Instance
    End Sub

    Public Sub New()
        Indigo = SessionValues.Instance
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Lista los empleados
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeEmployee()
        View.EmployeeXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).PayrollService.GetEmployee()
    End Sub

    ''' <summary>
    ''' Lista las remisiones de entrada
    ''' </summary>
    ''' <remarks></remarks>
    Public Function ListResumptionHoliday(EmployeeId As Integer) As List(Of ResumptionHolidayXpo)
        Dim filtroConsulta As String = "EmployeeId = " & EmployeeId
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).PayrollService.GetCollection(Of ResumptionHolidayXpo)(Nothing, filtroConsulta)
    End Function

    ''' <summary>
    ''' Lista las remisiones de entrada
    ''' </summary>
    ''' <remarks></remarks>
    Public Function ListVacationsByEmployeeId(EmployeeId As Integer) As List(Of PayrollVacation)
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).PayrollService.ListVacationsByEmployeeId(EmployeeId)
    End Function

#End Region

End Class
