'***********************************************************************
' Assembly         : Presentacion.Inventory.MVP
' Author           : Diego Andrés Roldán Lozano
' Created          : 23/06/2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel
Imports Presentation.CloudAgent
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Domain.Crystal.Entities

#End Region

Public Class MBedRate
    Implements IDisposable


#Region "Fields"

    '' <summary>
    ''' Variable que contiene la instancia de la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues
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

    Public Async Function DeleteBedRateAsync(bedRate As CHGENTARI) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCrystal.DeleteBedRateAsync(bedRate)
    End Function

    Public Async Function GetBedRatebyBedCodeAsync(bedCode As Integer) As Task(Of ActionResult(Of List(Of CHGENTARI)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCrystal.GetBedRatebyBedCodeAsync(bedCode)
    End Function

    Public Async Function SaveBedRateAsync(bedRate As CHGENTARI) As Task(Of ActionResult(Of CHGENTARI))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCrystal.SaveBedRateAsync(bedRate)
    End Function

    Public Async Function GetBedRatebyCodeAsync(code As Integer) As Task(Of CHGENTARI)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCrystal.GetBedRatebyCodeAsync(code)
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