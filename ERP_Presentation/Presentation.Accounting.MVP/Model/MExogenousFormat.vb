#Region "Imports"

Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.CloudAgent

#End Region

Public Class MExogenousFormat
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
    ''' Obtiene la secuencia numerica asignada al formulario
    ''' </summary>
    ''' <returns>Secuencia numerica</returns>
    Public Async Function GetSequense() As Task(Of Domain.Entities.GeneralLedgerSequence)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetSequenseByIdFormAsync(Me._tagForm)
    End Function

    ''' <summary>
    ''' Obtiene un formato por id
    ''' </summary>
    ''' <returns>Ciudad</returns>
    Public Function GetExogenousFormatById(ByVal id As Integer) As ActionResult(Of Domain.Entities.ExogenousFormat)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetExogenousFormatById(id, _indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene un formato por código
    ''' </summary>
    ''' <returns>Ciudad</returns>
    Public Async Function GetExogenousFormatByCode(ByVal code As String) As Task(Of ActionResult(Of Domain.Entities.ExogenousFormat))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetExogenousFormatByCodeAsync(code, _indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Guarda o actualiza un formato de exógena
    ''' </summary>
    ''' <param name="ExogenousFormat"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveExogenousFormat(ByVal ExogenousFormat As Domain.Entities.ExogenousFormat, Optional ByVal idSequense As Int64 = 0) As Task(Of Domain.Base.Entities.ActionResult(Of Domain.Entities.ExogenousFormat))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.SaveExogenousFormatAsync(ExogenousFormat, _indigoSessionValues.AuditMessageWcf, idSequense)
    End Function

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ChangeState(ByVal code As String, ByVal state As Boolean) As Task(Of ActionResult(Of Domain.Entities.ExogenousFormat))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.ChangeStateExogenousFormatAsync(code, state, _indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Gets the block record accounting.
    ''' </summary>
    ''' <param name="idForm">The identifier form.</param>
    ''' <param name="idRecord">The identifier record.</param>
    ''' <returns></returns>
    Public Async Function GetBlockRecordAccounting(ByVal idForm As String, ByVal idRecord As String) As Task(Of Domain.Entities.BlockRecordGeneralLedger)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetBlockRecordAccountingByIdformAndIdRecordAsync(idForm, idRecord)
    End Function

    ''' <summary>
    ''' Saves the block record accounting.
    ''' </summary>
    ''' <param name="record">The record.</param>
    ''' <returns></returns>
    Public Async Function SaveBlockRecordAccounting(ByVal record As Domain.Entities.BlockRecordGeneralLedger) As Task(Of ActionResult(Of Domain.Entities.BlockRecordGeneralLedger))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.SaveBlockRecordAccountingAsync(record)
    End Function

    ''' <summary>
    ''' Deletes the block record accounting.
    ''' </summary>
    ''' <param name="record">The record.</param>
    ''' <returns></returns>
    Public Async Function DeleteBlockRecordAccounting(ByVal record As Domain.Entities.BlockRecordGeneralLedger) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.DeleteBlockRecordAccountingAsync(record)
    End Function

    Public Async Function GenerateFormatExogena(criterias As Dictionary(Of String, String)) As Task(Of ActionResult(Of String))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GenerateExogenaFormatsAsync(criterias, _indigoSessionValues)
    End Function

    Public Async Function GetReportExogenousFormat(criterias As Dictionary(Of String, String)) As Task(Of System.Data.DataSet)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetReportExogenousFormatAsync(criterias, Me._indigoSessionValues)
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
