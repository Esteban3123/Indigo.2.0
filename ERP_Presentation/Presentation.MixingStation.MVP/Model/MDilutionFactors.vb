'***********************************************************************
' Assembly         : Presentacion.MixingStation.MVP
' Author           : 
' Created          : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent
Imports Domain.Entities
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports Infrastructure.Data.Xpo
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo.InventoryRepository

#End Region

Public Class MDilutionFactors
    Implements IDisposable

#Region "Fields"


    ''' <summary>
    ''' Variable que contiene la instancia de la clase singleton
    ''' </summary>
    Dim _sessionValues As SessionValues

    ''' <summary>
    ''' Tag del formulario
    ''' </summary>
    Private _tagForm As String

#End Region

#Region "Builder"

    ''' <summary>
    ''' Contructor
    ''' </summary>
    ''' <param name="tag">tag del form</param>
    ''' <remarks></remarks>
    Public Sub New(tag As String)
        Me._tagForm = tag
        _sessionValues = SessionValues.Instance
        Me._sessionValues.AuditMessageWcf.Functional = Me._tagForm
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene un paquete por código asincrono
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetDilutionFactorsAsync(ByVal code As String) As Task(Of ActionResult(Of DilutionFactors))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.GetDilutionFactorsByCodeAsync(code)
    End Function


    Public Async Function SaveDilutionFactorsAsync(ListDilutionFactors As List(Of DilutionFactors), Optional ByVal idSequence As Int64 = 0) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.SaveDilutionFactorsRepositoryAsync(ListDilutionFactors, Me._sessionValues.AuditMessageWcf, idSequence)
    End Function

    Public Function DefaultMeasureUnit(filter As String) As MeasureUnitXpo
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).InventoryService.GetCollection(Of MeasureUnitXpo)(Nothing, filter).FirstOrDefault
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
