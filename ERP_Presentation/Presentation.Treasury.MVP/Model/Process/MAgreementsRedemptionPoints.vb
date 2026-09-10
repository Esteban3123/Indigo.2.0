
#Region "Imports"
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent
Imports Infrastructure.CrossCutting.Xpo.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.GlosasRepository
Imports Infrastructure.Data.Xpo.TreasuryRepository
Imports DevExpress.Data.Filtering
Imports DevExpress.Xpo
#End Region

Public Class MAgreementsRedemptionPoints
    Implements IDisposable

#Region "Fields"
    ''' <summary>
    ''' Variable que contiene la instancia de la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues

    ''' <summary>
    ''' Tag del formulario
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

    ''' <summary>
    ''' Lista todos los convenios
    ''' </summary>
    Public Async Function ListAllAgreementsRedemptionPoints() As Task(Of List(Of AgreementsRedemptionPoints))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.ListAllAgreementsRedemptionPointsAsync(Indigo, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene un convenio por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetAgreementsRedemptionPoints(ByVal code As String) As Task(Of Object)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.GetAgreementsRedemptionPointsByCodeAsync(code, Indigo)
    End Function

    ''' <summary>
    ''' Guarda o actualiza un convenio
    ''' </summary>
    ''' <param name="record"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveAgreementsRedemptionPoints(ByVal record As AgreementsRedemptionPoints, ByVal idSequense As Int64) As Task(Of ActionResult(Of AgreementsRedemptionPoints))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.SaveAgreementsRedemptionPointsAsync(record, idSequense, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Elimina un convenio
    ''' </summary>
    ''' <param name="record"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeleteAgreementsRedemptionPoints(ByVal record As AgreementsRedemptionPoints) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.DeleteAgreementsRedemptionPointsAsync(record, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Cambia el estado 
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ChangeState(ByVal code As String, ByVal state As Boolean) As Task(Of ActionResult(Of AgreementsRedemptionPoints))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.ChangeAgreementsRedemptionPointsStatusAsync(code, state, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene los detalles de un convenio
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Public Async Function ListAgreementsRedemptionPointsDetail(Id As Integer) As Task(Of List(Of AgreementsRedemptionPointsDetail))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.GetAgreementsRedemptionPointsDetailsAsync(Id)
    End Function

    ''' <summary>
    ''' Obtiene un convenio por id
    ''' </summary>
    ''' <param name="Id"></param>
    Public Function GetAgreementsRedemptionPointsById(AgreementsRedemptionPointsId As Integer) As XPCollection
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).TreasuryService.ListAgreementsRedemptionPointsById(AgreementsRedemptionPointsId)
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
