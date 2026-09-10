'***********************************************************************
' Assembly         : Presentacion.Treasury.MVP
' Author           : Diego Andrés Roldán Lozano
' Created          : 07-04-2014
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
Imports Presentation.Base
Imports System.ServiceModel
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo

#End Region

''' <summary>
''' Realiza la conexion con los servicios de conceptos de nota
''' </summary>
Public Class MNoteConcepts
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
    ''' Saves the note concept.
    ''' </summary>
    ''' <param name="noteConcept">The note concept.</param>
    ''' <returns></returns>
    Public Async Function SaveNoteConcept(ByVal noteConcept As NoteConcepts, ByVal idSequence As Int64) As Task(Of ActionResult(Of NoteConcepts))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.SaveNoteConceptAsync(noteConcept, Me._indigoSessionValues.AuditMessageWcf, idSequence)
    End Function

    ''' <summary>
    ''' Actualiza el estado del registro
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <returns></returns>
    Public Async Function UpdateStateNoteConcept(ByVal code As String, ByVal state As Boolean) As Task(Of ActionResult(Of NoteConcepts))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.UpdateStateNoteConceptAsync(code, state, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    Public Function ListAllNoteConceptXpo() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Me._indigoSessionValues.TransactionalContainer).TreasuryService.ListNoteConcept(True)
    End Function

    Function ListMainAccountXpo() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Me._indigoSessionValues.TransactionalContainer).AccountingService.ListAccountsByLevel(5, True, 0)
    End Function

    Function ListThirdPartyXpo() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Me._indigoSessionValues.TransactionalContainer).CommonService.GetThirdParty()
    End Function

    Function ListCostCenterXpo() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Me._indigoSessionValues.TransactionalContainer).PayrollService.GetCostCenterByState(True)
    End Function

    ''' <summary>
    ''' Deletes the note concept.
    ''' </summary>
    ''' <param name="noteConcept">The note concept.</param>
    ''' <returns></returns>
    Public Async Function DeleteNoteConcept(ByVal noteConcept As NoteConcepts) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.DeleteNoteConceptAsync(noteConcept, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene un concepto de nota por el id
    ''' </summary>
    ''' <returns></returns>
    Public Async Function GetNoteConceptById(ByVal Id As Integer) As Task(Of NoteConcepts)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.GetNoteConceptByIdAsync(Id, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Gets the note concept.
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Public Async Function GetNoteConcept(ByVal code As String) As Task(Of ActionResult(Of NoteConcepts))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.GetNoteConceptAsync(code, Me._indigoSessionValues.AuditMessageWcf)
    End Function
    ''' <summary>
    ''' Valida y genera el import de las notas 
    ''' </summary>
    ''' <param name="DataImport"></param>
    ''' <returns></returns>
    Public Function ValidateNoteConcept(DataImport As List(Of ImportFileRow)) As ActionResult(Of List(Of TreasuryNoteDetail))
        Return IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.ValidateNoteConcept(DataImport)
    End Function
    ''' <summary>
    ''' Funcion para copiar y pegar 
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    Public Function CopyPasteNoteConceptDetail(data As List(Of List(Of String))) As Task(Of ActionResult(Of List(Of TreasuryNoteDetail)))
        Return IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.CopyPasteNoteConceptDetailAsync(data)
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
