#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Infrastructure.Data.Xpo
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo.AccountingRepository

#End Region

Public Class PAccountingVoucher

#Region "Fields"

    ''' <summary>
    ''' Instancia de la interface
    ''' </summary>
    Dim _view As IAccountBalance

    ''' <summary>
    ''' Referencia a los valores de sesión
    ''' </summary>
    Dim _indigo As SessionValues

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="view">Referencia a la vista del frontal</param>
    Public Sub New(ByRef view As IAccountBalance)
        If view Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me._indigo = SessionValues.Instance
            Me._view = view
        End If
    End Sub

#End Region

#Region "Methods"
    ''' <summary>
    ''' Permite consultar los comprobantes contables asociados al movimiento
    ''' </summary>
    ''' <param name="accountingMovementId"></param>
    ''' <returns></returns>
    Public Function GetJournalVouchersAssociated(accountingMovementId As Integer) As List(Of JournalVouchersXpo)
        Dim filter As String = String.Format("AccountingMovementId = {0}", accountingMovementId)
        Return XpoServiceEx.Instance(_indigo.TransactionalContainer).AccountingService.GetCollection(Of JournalVouchersXpo)(Nothing, filter).ToList()
    End Function

#End Region

End Class
