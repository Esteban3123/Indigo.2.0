'***********************************************************************
' Assembly         : Presentation.Cost.MVP
' Author           : Juan David Capera
' Created          : 2023-03-24
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent
#End Region

Public Class MAverageStandardCost
    Implements IDisposable

    ''' <summary>
    ''' Contructor
    ''' </summary>
    ''' <param name="Tag">tag del form</param>
    ''' <remarks></remarks>
    Public Sub New(Tag As String)
        Me._tagForm = Tag
        _session = SessionValues.Instance
        _session.AuditMessageWcf.Functional = Me._tagForm
    End Sub

#Region "Fields"

    ''' <summary>
    ''' Variable que contiene la instancia de la clase singleton
    ''' </summary>
    Private _session As SessionValues
    ''' <summary>
    ''' Tago del formulario
    ''' </summary>
    Private _tagForm As String

#End Region

#Region "Methods"

    ''' <summary>
    ''' Save entity
    ''' </summary>
    ''' <param name="standarCost"></param>
    ''' <returns></returns>
    Public Async Function SaveAverageStandardCostAsync(standarCost As StandarCost) As Task(Of ActionResult(Of StandarCost))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.SaveAverageStandardCostAsync(standarCost, _session.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Get Entity By Code
    ''' </summary>
    ''' <param name="standarCostCode"></param>
    ''' <returns></returns>
    Public Async Function GetAverageStandardCostByCodeAsync(standarCostCode As String) As Task(Of ActionResult(Of StandarCost))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.GetAverageStandardCostByCodeAsync(standarCostCode)
    End Function

    ''' <summary>
    ''' Get Entity By Id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Public Async Function GetAverageStandardCostByIdAsync(Id As Integer) As Task(Of ActionResult(Of StandarCost))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.GetAverageStandardCostByIdAsync(Id)
    End Function

    ''' <summary>
    ''' load data
    ''' </summary>
    ''' <param name="dataImportFile"></param>
    ''' <param name="dataCopyPaste"></param>
    ''' <returns></returns>
    Public Function ImportOrCopyAndPasteDetails(dataImportFile As List(Of ImportFileRow), dataCopyPaste As List(Of List(Of String))) As ActionResult(Of List(Of StandarCostDetails))
        Return IndigoConecta.Instancia.CurrentCloud.IndigoCost.ImportOrCopyAndPasteDetails(dataImportFile, dataCopyPaste)
    End Function

    ''' <summary>
    ''' load data
    ''' </summary>
    ''' <param name="dataImportFile"></param>
    ''' <param name="dataCopyPaste"></param>
    ''' <returns></returns>
    Public Async Function ImportOrCopyAndPasteDetailsAsync(dataImportFile As List(Of ImportFileRow), dataCopyPaste As List(Of List(Of String))) As Task(Of ActionResult(Of List(Of StandarCostDetails)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCost.ImportOrCopyAndPasteDetailsAsync(dataImportFile, dataCopyPaste)
    End Function

#End Region

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
