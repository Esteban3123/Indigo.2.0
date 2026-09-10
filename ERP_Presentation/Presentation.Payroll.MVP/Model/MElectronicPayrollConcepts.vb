'***********************************************************************
' Assembly         : Presentacion.Payroll.File.MVP
' Author           : Andres Felipe Quintero Garcia
' Created          : 10-02-2025
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Base.Entities
Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.CloudAgent
#End Region
Public Class MElectronicPayrollConcepts
    Inherits ModelBase
    Implements IDisposable

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="tag"></param>
    ''' <remarks></remarks>
    Sub New(tag As String)
        MyBase.New(tag)
    End Sub

    ''' <summary>
    ''' Función que obtiene el conceptos de nómina electrónica por Code
    ''' </summary>
    ''' <param name="code">Id del Grupo</param>
    ''' <returns>Conceptos de nómina electrónica</returns>
    ''' <remarks></remarks>
    Public Async Function GetElectronicPayrollConceptsByCode(code As String) As Task(Of ElectronicPayrollConcepts)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetElectronicPayrollConceptsByCodeAsync(code, Indigo)
    End Function

    ''' <summary>
    ''' Lista los Conceptos de nómina electrónica
    ''' </summary>
    Public Async Function ListAllElectronicPayrollConcepts() As Task(Of List(Of ElectronicPayrollConcepts))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.ListAllElectronicPayrollConceptsAsync(Indigo)
    End Function

    ''' <summary>
    ''' Almacena los conceptos de nómina electrónica
    ''' </summary>
    ''' <param name="electronicPayrollConcepts"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveElectronicPayrollConcepts(ByVal electronicPayrollConcepts As Object, ByVal idSequense As Int64) As Task(Of ActionResult(Of ElectronicPayrollConcepts))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.SaveElectronicPayrollConceptsAsync(electronicPayrollConcepts, Indigo, idSequense, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Función Para Eliminar conceptos de nómina electrónica
    ''' </summary>
    ''' <param name="electronicPayrollConcepts"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeleteElectronicPayrollConcepts(electronicPayrollConcepts As ElectronicPayrollConcepts) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.DeleteElectronicPayrollConceptsAsync(electronicPayrollConcepts, Indigo, Me.Indigo.AuditMessageWcf)
    End Function


    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ChangeState(ByVal code As String, ByVal state As Boolean) As Task(Of ActionResult(Of ElectronicPayrollConcepts))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.ChangeStateElectronicPayrollConceptsAsync(code, state, Indigo, Me.Indigo.AuditMessageWcf)
    End Function
#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        ' TODO: uncomment the following line if Finalize() is overridden above.
        ' GC.SuppressFinalize(Me)
    End Sub
#End Region
End Class
