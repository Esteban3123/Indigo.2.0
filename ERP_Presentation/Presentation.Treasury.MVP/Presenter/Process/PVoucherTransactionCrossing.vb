'***********************************************************************
' Assembly         : Presentacion.Treasury.MVP
' Author           : Diego Andrés Roldán
' Created          : 23-09-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.TreasuryRepository
Imports Presentation.Accounting.MVP
Imports Presentation.Base
Imports Presentation.Common.MVP
Imports Presentation.Controls.MVP

#End Region

Public Class PVoucherTransactionCrossing
    ''' <summary>
    ''' variable para comunicar con la interfaz
    ''' </summary>
    Dim View As IVoucherTransactionCrossing

    ''' <summary>
    ''' variable que obtiene los valores de la sesion
    ''' </summary>
    Dim Indigo As SessionValues

    ''' <summary>
    ''' Constructor que comunica con la interfaz
    ''' </summary>
    Public Sub New(ByRef iView As IVoucherTransactionCrossing)
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
    ''' Lista todos los terceros y lo asigna al datasource de terceros
    ''' </summary>
    Public Sub InitializeThirdParty()
        Using Model As New MBusqueda
            Me.View.ThirdPartyDatasource = Model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ThirdParty)
        End Using
    End Sub

    ''' <summary>
    ''' Obtiene una secuencia
    ''' </summary>
    Public Async Sub GetSequence()
        Using modelCommmonTreasury As New MCommonTreasury(View.MyTag)
            Me.View.Sequense = Await modelCommmonTreasury.GetSequense()
        End Using
    End Sub

    Public Sub LoadInvoiceCxCBySupplierId(thirdId As Integer)
        Using Model As New MBusqueda
            Dim filter As Object() = {thirdId, eAccountPayableState.Confirm}
            Me.View.AccountRecivableDatasource = Model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAccountRecivableAccountByThirdIdState, filter)
        End Using
    End Sub

    ''' <summary>
    ''' carga las facturas de cuentas por pagar por tercero
    ''' </summary>
    Public Sub LoadInvoiceCxPBySupplierId(SupplierId As Integer)
        Me.View.AccountPayableDatasource = XpoServiceEx.Instance(Indigo.TransactionalContainer).PaymentsService.ListAccountPayableByIdSupplierAndState(CInt(SupplierId), CByte(eAccountPayableState.Confirm), CByte(1))
    End Sub

    ''' <summary>
    ''' Lee la fecha del servidor
    ''' </summary>
    Public Async Sub LoadDateServer(dateServer As Date)
        Using Model As New MDocumentAccount(Me.View.MyTag)
            If Await Model.ValidatePeriod(dateServer.Month, dateServer.Year) Then
                View.DocumentDate = dateServer
            Else
                View.DocumentDate = Nothing
                View.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("MonthSelectedClose", "Accounting")
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Loads the invoice cx p.
    ''' </summary>
    Public Sub LoadInvoiceCxP()
        Me.View.AccountPayableDatasource = XpoServiceEx.Instance(Indigo.TransactionalContainer).PaymentsService.ListAccountPayableByStateAndNature(eAccountPayableState.Confirm, 1) 'Debito
    End Sub

    ''' <summary>
    ''' Loads the invoice cx c.
    ''' </summary>
    Public Sub LoadInvoiceCxC()
        Me.View.AccountRecivableDatasource = XpoServiceEx.Instance(Indigo.TransactionalContainer).PortfolioService.ListAccountRecivableAccountByState(eAccountPayableState.Confirm)
    End Sub

    ''' <summary>
    ''' Obtiene el listado de detalles de cxc
    ''' </summary>
    ''' <remarks></remarks>
    Public Function ListDetailsCxC(crossingAccountId As Integer) As List(Of ViewListCrossingAccountDetailCxCXpo)
        Dim filter As String = "CrossingAccountId = " & crossingAccountId
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).TreasuryService.GetCollection(Of ViewListCrossingAccountDetailCxCXpo)(Nothing, filter).ToList()
    End Function

    ''' <summary>
    ''' Obtiene el listado de detalles de cxp
    ''' </summary>
    ''' <remarks></remarks>
    Public Function ListDetailsCxP(crossingAccountId As Integer) As List(Of ViewListCrossingAccountDetailCxPXpo)
        Dim filter As String = "CrossingAccountId = " & crossingAccountId
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).TreasuryService.GetCollection(Of ViewListCrossingAccountDetailCxPXpo)(Nothing, filter).ToList()
    End Function

End Class

Public Enum eAccountPayableState
    Confirm = 2
End Enum