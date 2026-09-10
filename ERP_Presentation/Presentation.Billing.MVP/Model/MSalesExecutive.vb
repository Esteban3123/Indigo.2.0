#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent
Imports Domain.Entities
Imports Domain.Base.Entities

#End Region

Public Class MSalesExecutive
    Implements IDisposable

#Region "Fields"

    ''' <summary>
    ''' Variable que contiene la instancia de la clase singleton
    ''' </summary>
    Private Indigo As SessionValues

    ''' <summary>
    ''' Tago del formulario
    ''' </summary>
    Private _tagForm As String

#End Region

#Region "Builder"

    ''' <summary>
    ''' Contructor
    ''' </summary>
    ''' <param name="Tag">tag del form</param>
    ''' <remarks></remarks>
    Public Sub New(Tag As String)
        Me._tagForm = Tag
        Indigo = SessionValues.Instance
        Me.Indigo.AuditMessageWcf.Functional = Me._tagForm
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Consulta  por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetSalesExecutiveById(ByVal id As Integer) As Task(Of ActionResult(Of SalesExecutive))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.GetSalesExecutiveByIdAsync(id, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Consulta  por code
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetSalesExecutiveByCode(ByVal Code As String) As Task(Of ActionResult(Of SalesExecutive))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.GetSalesExecutiveByCodeAsync(Code, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Guarda o actualiza 
    ''' </summary>
    ''' <param name="SalesExecutive"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveSalesExecutive(ByVal SalesExecutive As SalesExecutive, ByVal sequenseId As Int64) As Task(Of ActionResult(Of SalesExecutive))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.SaveSalesExecutiveAsync(SalesExecutive, Me.Indigo.AuditMessageWcf, sequenseId)
    End Function

    ''' <summary>
    ''' Cambia el estado
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ChangeStateSalesExecutive(ByVal code As String, ByVal state As Boolean) As Task(Of ActionResult(Of SalesExecutive))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.ChangeStateSalesExecutiveAsync(code, state, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Elimina 
    ''' </summary>
    ''' <param name="SalesExecutive"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeleteSalesExecutive(ByVal SalesExecutive As SalesExecutive) As Task(Of ActionResult(Of SalesExecutive))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.DeleteSalesExecutiveAsync(SalesExecutive, Me.Indigo.AuditMessageWcf)
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
