'***********************************************************************
' Assembly         : Presentacion.Billing.MVP
' Author           : Diego Andrés Roldán Lozano
' Created          : 2022-08-09
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent

#End Region

Public Class MElectronicSupportDocumentAdjustmentNote
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
    '''
    ''' </summary>
    ''' <param name="Code">The reversal reason.</param>
    ''' <returns></returns>
    Public Async Function GetElectronicSupportDocumentAdjustmentNoteByCodeAsync(Code As String) As Task(Of Domain.Base.Entities.ActionResult(Of ElectronicSupportDocumentAdjustmentNote))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.GetElectronicSupportDocumentAdjustmentNoteByCodeAsync(Code, Me._indigoSessionValues.AuditMessageWcf)
    End Function


    Public Async Function SaveElectronicSupportDocumentAdjusmentNoteAsync(electronicSupportDocument As ElectronicSupportDocumentAdjustmentNote, idSequense As Int64) As Task(Of Domain.Base.Entities.ActionResult(Of ElectronicSupportDocumentAdjustmentNote))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.SaveElectronicSupportDocumentAdjusmentNoteAsync(electronicSupportDocument, Me._indigoSessionValues.AuditMessageWcf, idSequense)
    End Function

    ''' <summary>
    ''' Actualizar el estado de documentos para ser procesados
    ''' </summary>
    ''' <param name="Ids"></param>
    ''' <returns></returns>
    Public Async Function UpdateStateElectronicSupportDocumentsAdjusmentNoteAsync(Ids As List(Of Integer)) As Task(Of Domain.Base.Entities.ActionResult(Of ElectronicSupportDocumentAdjustmentNote))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.UpdateStateElectronicSupportDocumentsAdjusmentNoteAsync(Ids, Me._indigoSessionValues.AuditMessageWcf)
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: desechar estado administrado (objetos administrados).
            End If

            ' TODO: liberar recursos no administrados (objetos no administrados) e invalidar Finalize() below.
            ' TODO: Establecer campos grandes como Null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: invalidar Finalize() sólo si la instrucción Dispose(ByVal disposing As Boolean) anterior tiene código para liberar recursos no administrados.
    'Protected Overrides Sub Finalize()
    '    ' No cambie este código. Ponga el código de limpieza en la instrucción Dispose(ByVal disposing As Boolean) anterior.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' Visual Basic agregó este código para implementar correctamente el patrón descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' No cambie este código. Coloque el código de limpieza en Dispose(disposing As Boolean).
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
