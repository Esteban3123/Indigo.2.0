'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Daniel Eduardo Arévalo
' Created          : 20-11-2020
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Domain.Payroll.Entities
Imports Presentation.CloudAgent
Imports Presentation.Base
Imports Domain.Base.Entities
Imports Infrastructure.Data.Xpo
Imports DevExpress.Xpo
Imports DevExpress.Data.Linq

#End Region
''' <summary>
''' Realiza la conexion con los servicios de talento humano
''' </summary>

Public Class MEmployeeSchedule
    Inherits ModelBase
    Implements IDisposable

    Public Shared TAG As String = "2141"

    ''' <summary>
    ''' Contructor
    ''' </summary>
    ''' <param name="Tag">tag del form</param>
    ''' <remarks></remarks>
    Public Sub New(Tag As String)
        MyBase.New(Tag)
    End Sub

#Region "Methods"
    ''' <summary>
    ''' Lista todos las Unidades Funcionales
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAllFunctionalUnit() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).PayrollService.GetFunctionalUnit()
    End Function

    ''' <summary>
    ''' Lista todos los Cargos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAllPosition() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).PayrollService.GetPosition()
    End Function

    Public Function ListEmployee() As LinqInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).PayrollService.GetEmployee()
    End Function


    Public Function AnalisisEmployeeSchedule(ByVal InitialDate As Date, EndDate As Date, IdFunctionalUnit As Integer, IdPosition As Integer, IdEmployee As Integer) As List(Of SP_AnalisEmployeeSchedule_Result)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.AnalisisEmployeeSchedule(InitialDate, EndDate, IdFunctionalUnit, IdPosition, IdEmployee, Indigo)
    End Function

    Public Function SaveExtrahours(ByVal ListAnalisisEmployee As List(Of SP_AnalisEmployeeSchedule_Result)) As SP_SaveExtraHours_Result
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.SaveExtraHours(ListAnalisisEmployee, Indigo)
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region
End Class
