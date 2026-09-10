'***********************************************************************
' Assembly         : Presentacion.Portfolio.MVP
' Author           : Hector Rodriguez Rubiano
' Created          : 08-08-2019
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
#End Region
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent

Public Class MDemandStatus
    Implements IDisposable

    ''' <summary>
    ''' Variable que contiene la instancia de la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues
    ''' <summary>
    ''' Tag del formulario
    ''' </summary>
    Private _tagForm As String


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
    ''' Obtener un estado de demanda por su codigo, metodo asincrono
    ''' </summary>
    ''' <param name="code">El codigo del estado de demanda</param>
    ''' <returns>objeto accesorio</returns>
    Public Async Function GetDemandStatusAsync(ByVal code As String) As Task(Of Object)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.GetDemandStatusByCodeAsync(code)
    End Function
    ''' <summary>
    ''' Graba el estado de demanda en modo asincrono
    ''' </summary>
    ''' <param name="ObjDemandStatus">DemandStatus</param>
    ''' <returns>Un valor que indica si se grabo el estado de demanda</returns>
    ''' 
    Public Async Function SaveDemandStatusAsync(ByVal ObjDemandStatus As DemandStatus, ByVal idSequense As Int64) As Task(Of ActionResult(Of DemandStatus))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.SaveDemandStatusAsync(ObjDemandStatus, idSequense, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Elimina un estado de demanda modo asincrono
    ''' </summary>
    ''' <param name="ObjDemandStatus">DemandStatus</param>
    ''' <returns>Un valor que indica si se elimino con exito el estado de demanda</returns>
    ''' 
    Public Async Function DeleteDemandStatusAsync(ByVal ObjDemandStatus As DemandStatus) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.DeleteDemandStatusAsync(ObjDemandStatus, Me.Indigo.AuditMessageWcf)
    End Function
    ''' <summary>
    ''' Lista todos los estados de demanda
    ''' </summary>
    Public Async Function ListAllDemandStatusAsync() As Task(Of List(Of DemandStatus))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.ListAllDemandStatusAsync(Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Metodo para cambiar de estado la entidad
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <param name="State"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ChangeStateStatus(ByVal code As String, ByVal state As Boolean) As Task(Of ActionResult(Of DemandStatus))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.ChangeDemandStatusAsync(code, state, Me.Indigo.AuditMessageWcf)
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

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region
End Class
