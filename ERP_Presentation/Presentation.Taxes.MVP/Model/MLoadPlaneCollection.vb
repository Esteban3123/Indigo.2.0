'***********************************************************************
' Assembly         : Presentacion.Treasury.MVP
' Author           : Diego Andrés Roldán Lozano
' Created          : 01-04-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Presentation.Base
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo
Imports Presentation.CloudAgent
Imports System.ServiceModel
Imports Infrastructure.Data.Xpo.TaxesRepository

#End Region

''' <summary>
''' Realiza la conexion con los servicios del grupo
''' </summary>
''' 
Public Class MLoadPlaneCollection
    Implements IDisposable

#Region "Fields"

    ''' <summary>
    ''' Referencia a los valores de session
    ''' </summary>
    Private _indigoSessionValues As SessionValues

    ''' <summary>
    ''' Id del frontal
    ''' </summary>
    Private _tagForm As String

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    Public Sub New(ByVal tag As String)
        Me._tagForm = tag
        Me._indigoSessionValues = SessionValues.Instance
    End Sub

#End Region

    Public Async Function ValidateLoadPlaneCollection(data As List(Of String)) As Task(Of ActionResult(Of List(Of Tuple(Of Integer, String, String, String))))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTaxes.ValidateLoadPlaneCollectionAsync(data)
    End Function

    Public Async Function SaveLoadPlaneCollection(data As List(Of String)) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTaxes.SaveLoadPlaneCollectionAsync(data, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    Public Function GetCountResultLoadPlaneCollection() As Integer
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).TaxesService.GetCountResultLoadPlaneCollection()
    End Function

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
