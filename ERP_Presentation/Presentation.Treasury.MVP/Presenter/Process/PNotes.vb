'***********************************************************************
' Assembly         : Presentacion.Treasury.MVP
' Author           : Diego Andrés Roldán
' Created          : 18-03-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.Accounting.MVP
Imports Presentation.Controls.MVP
Imports Infrastructure.Data.Xpo
Imports Presentation.Common.MVP

#End Region

''' <summary>
''' Presentador del frontal Notas de Tesoreria
''' </summary>
Public Class PNotes

    ''' <summary>
    ''' variable para comunicar con la interfaz
    ''' </summary>
    Dim View As INotes

    ''' <summary>
    ''' variable que obtiene los valores de la sesion
    ''' </summary>
    Dim Indigo As SessionValues

    ''' <summary>
    ''' Constructor que comunica con la interfaz
    ''' </summary>
    Public Sub New(ByRef iView As INotes)
        If iView Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        End If
        View = iView
        Indigo = SessionValues.Instance
    End Sub

    ''' <summary>
    ''' Loads the definition layout.
    ''' </summary>
    Public Async Sub LoadDefinitionLayout()
        Await Me.View.MyLayoutControl.LoadDefinitionAsync()
    End Sub

    ''' <summary>
    ''' Obtiene la secuencia
    ''' </summary>
    Public Async Sub GetSequence()
        Using modelCommmonTreasury As New MCommonTreasury(View.MyTag)
            Me.View.Sequense = Await modelCommmonTreasury.GetSequense()
        End Using
    End Sub

    ''' <summary>
    ''' Lista todas cuentas bancarias de entidades y lo asigna al datasource de cuentas bancarias
    ''' </summary>
    Public Sub InitializeEntityBankAccount()
        Me.View.EntityAccountDatasource = XpoServiceEx.Instance(Indigo.TransactionalContainer).TreasuryService.ListEntityBankAccount(True)
    End Sub

    ''' <summary>
    ''' Lista todas las cajas
    ''' </summary>
    Public Sub InitializeCash()
        Me.View.CashDatasource = XpoServiceEx.Instance(Indigo.TransactionalContainer).TreasuryService.ListCashRegisterByUser(Indigo.UserIndigoId, 0, True)
    End Sub

    ''' <summary>
    ''' Lista todos los centros de costo
    ''' </summary>
    Public Sub InitializeCostCenter()
        Me.View.CostCenterDatasource = XpoServiceEx.Instance(Indigo.TransactionalContainer).PayrollService.GetCostCenterByState(True)
    End Sub

    ''' <summary>
    ''' Lista todos los centros de costo
    ''' </summary>
    Public Sub InitializeCrossAccount()
        Me.View.CrossAccountDatasource = XpoServiceEx.Instance(Indigo.TransactionalContainer).TreasuryService.ListCrossingAccountConfirmed()
    End Sub


    ''' <summary>
    ''' Carga la fecha del servidor
    ''' </summary>
    Public Async Sub LoadDateServer()
        Dim DateServer As Date
        Using Model As New MVoucherTransaction(View.MyTag)
            DateServer = Await Model.GetServerDate()
        End Using
        Using Model As New MDocumentAccount(Me.View.MyTag)
            If Await Model.ValidatePeriod(DateServer.Month, DateServer.Year) Then
                View.DateNote = DateServer
            Else
                View.DateNote = Nothing
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Initializes the voucer transaction.
    ''' </summary>
    Public Sub InitializeVoucerTransaction()
        Me.View.VoucherTransactionDatasource = XpoServiceEx.Instance(Indigo.TransactionalContainer).TreasuryService.ListAllVoucherTransactionByStatus(2)
    End Sub

    ''' <summary>
    ''' Obtiene un comprobante de egreso por Id
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetVoucherTransactionById(Id As Integer) As TreasuryRepository.VoucherTransactionXpo
        Dim filter As String = "Id = " & Id
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).CommonService.GetCollection(Of TreasuryRepository.VoucherTransactionXpo)(Nothing, filter).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Inicializa el datasource con todos los recibos de caja confirmados
    ''' </summary>
    ''' <remarks>
    ''' Carga todos los recibos de caja en estado confirmado (Status = 2) sin aplicar filtros adicionales.
    ''' Este método proporciona un listado completo para selección manual de recibos.
    ''' </remarks>
    Public Sub InitializeCashReceipts()
        Me.View.CashReceiptDatasource = XpoServiceEx.Instance(Indigo.TransactionalContainer).TreasuryService.ListAllCashReceiptByStatus(2)
    End Sub

    ''' <summary>
    ''' Inicializa el datasource de recibos de caja filtrados por cuenta bancaria y mes
    ''' </summary>
    ''' <param name="entityBankAccountId">ID de la cuenta bancaria</param>
    ''' <param name="noteDate">Fecha que determina el mes de filtrado</param>
    ''' <remarks>
    ''' Carga los recibos de caja del mes completo correspondiente a la fecha proporcionada,
    ''' filtrados por la cuenta bancaria especificada.
    ''' Actualmente para uso exclusivo de las notas de tipo gastos conciliación de tarjetas (NoteType = 8)
    ''' </remarks>
    Public Sub InitializeCashReceiptsForCardReconciliation(entityBankAccountId As Integer, noteDate As Date)
        Dim selectedMonth As Integer = noteDate.Month
        Dim selectedYear As Integer = noteDate.Year
        Dim startDate As New Date(selectedYear, selectedMonth, 1)
        Dim endDate As Date = startDate.AddMonths(1).AddDays(-1)

        Me.View.CashReceiptsDatasource = XpoServiceEx.Instance(Indigo.TransactionalContainer).TreasuryService.ListCashReceiptsToPost(entityBankAccountId, startDate, endDate)
    End Sub

    Public Sub InitializeConsignment()
        Me.View.ConsignmentDatasource = XpoServiceEx.Instance(Indigo.TransactionalContainer).TreasuryService.ListConsignmentByStatus(2)
    End Sub

End Class
