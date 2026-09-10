'***********************************************************************
' Assembly         : Presentacion.Inventory.MVP
' Author           : Juan F. Tamayo
' Created          : 2014-10-23
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
Imports System.ServiceModel
Imports Presentation.CloudAgent
Imports Domain.Base.Entities

#End Region

Public Class MInventoryRiskLevel
    Implements IDisposable

#Region "Fields"

    ''' <summary>
    ''' Instancia a los valores de sesión
    ''' </summary>
    Private Indigo As SessionValues

    ''' <summary>
    ''' Tag del formulario
    ''' </summary>
    Private _tagForm As String

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="tag">Tag del funcional</param>
    Public Sub New(ByVal tag As String)
        Me._tagForm = tag
        Me.Indigo = SessionValues.Instance
        Me.Indigo.AuditMessageWcf.Functional = Me._tagForm
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene un nivel de riesgo por Codigo
    ''' </summary>
    ''' <param name="code">Codigo del nivel de riesgo</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetRiskLevelByCodeAsync(ByVal code As String) As Task(Of Domain.Base.Entities.ActionResult(Of InventoryRiskLevel))
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetRiskLevelByCodeAsync(code, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Guarda o actualiza un nivel de riesgo
    ''' </summary>
    ''' <param name="record">Nivel de riesgo</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveRiskLevel(ByVal record As InventoryRiskLevel, ByVal idSequense As Int64) As Task(Of ActionResult(Of InventoryRiskLevel))
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.SaveRiskLevelAsync(record, idSequense, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Elimina un nivel de riesgo
    ''' </summary>
    ''' <param name="record">Nivel de riesgo</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeleteRiskLevel(ByVal record As InventoryRiskLevel) As Task(Of ActionResult)
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.DeleteRiskLevelAsync(record, Me.Indigo.AuditMessageWcf)
    End Function


    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function UpdateStateInventoryRiskLevelAsync(ByVal code As String, ByVal state As Boolean) As Task(Of ActionResult(Of InventoryRiskLevel))
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.UpdateStateInventoryRiskLevelAsync(code, state, Me.Indigo.AuditMessageWcf)
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
