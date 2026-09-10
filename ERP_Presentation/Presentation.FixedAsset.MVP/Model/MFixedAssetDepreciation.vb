'***********************************************************************
' Assembly         : Presentacion.FixedAsset.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 03/06/2016
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
Imports Presentation.CloudAgent
Imports Domain.Base.Entities

#End Region

Public Class MFixedAssetDepreciation
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

#Region "Methods"

    ''' <summary>
    ''' Obtiene un concepto de pago
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetFixedAssetDepreciationById(ByVal id As Integer) As Task(Of ActionResult(Of FixedAssetDepreciation))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.GetFixedAssetDepreciationByIdAsync(id, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene un concepto de pago
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetFixedAssetDepreciation(ByVal code As String) As Task(Of ActionResult(Of FixedAssetDepreciation))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.GetFixedAssetDepreciationAsync(code, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Guarda o actualiza un concepto de pago
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveDepreciation(DepreciationMonth As Integer, DepreciationYear As Integer, OperatingUnitId As Integer, ModeConfirm As Boolean) As Task(Of ActionResult(Of FixedAssetDepreciation))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.SaveDepreciationAsync(DepreciationMonth, DepreciationYear, OperatingUnitId, ModeConfirm, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Guarda o actualiza un concepto de pago
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ConfirmDepreciation(DepreciationMonth As Integer, DepreciationYear As Integer, OperatingUnitId As Integer) As Task(Of ActionResult(Of FixedAssetDepreciation))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.ConfirmDepreciationAsync(DepreciationMonth, DepreciationYear, OperatingUnitId, Me._indigoSessionValues.AuditMessageWcf)
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
