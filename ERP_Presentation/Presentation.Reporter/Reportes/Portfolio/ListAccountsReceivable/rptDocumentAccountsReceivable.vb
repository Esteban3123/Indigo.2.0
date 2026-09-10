#Region "Librerias Importadas"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo.PortfolioRepository
Imports Infrastructure.Data.Xpo
Imports DevExpress.XtraReports.UI

#End Region

Public Class rptDocumentAccountsReceivable
    Implements IReport

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Private _INDDateStart As Date
    Public Property INDDateStart As Date
        Get
            Return Me._INDDateStart
        End Get
        Set(value As Date)
            Me._INDDateStart = value
        End Set
    End Property

    Private _INDDateEnd As Date
    Public Property INDDateEnd As Date
        Get
            Return Me._INDDateEnd
        End Get
        Set(value As Date)
            Me._INDDateEnd = value
        End Set
    End Property

    Private _INDStatuss As Integer
    Public Property INDStatuss As Integer
        Get
            Return Me._INDStatuss
        End Get
        Set(value As Integer)
            Me._INDStatuss = value
        End Set
    End Property

    Private _INDListClients As List(Of Integer)
    Public Property INDListClients As List(Of Integer)
        Get
            Return Me._INDListClients
        End Get
        Set(value As List(Of Integer))
            Me._INDListClients = value
        End Set
    End Property

    Private _INDListSellers As List(Of Integer)
    Public Property INDListSellers As List(Of Integer)
        Get
            Return Me._INDListSellers
        End Get
        Set(value As List(Of Integer))
            Me._INDListSellers = value
        End Set
    End Property

    Private _INDListInvoices As List(Of Integer)
    Public Property INDListInvoices As List(Of Integer)
        Get
            Return Me._INDListInvoices
        End Get
        Set(value As List(Of Integer))
            Me._INDListInvoices = value
        End Set
    End Property

    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Dim filtroConsulta As String = "GetDate(AccountReceivableDate) >= #" & Format(INDDateStart, "yyyy-MM-dd") & "# AND GetDate(AccountReceivableDate) <= #" & Format(INDDateEnd, "yyyy-MM-dd") & "#"

        ' si filtra por Clientes
        If INDListClients.Count > 0 Then
            Dim ClientsIds As String
            For Each ItemClients As String In INDListClients
                If ClientsIds IsNot Nothing Then
                    ClientsIds &= ","
                End If
                ClientsIds &= ItemClients
            Next
            filtroConsulta &= " AND CustomerId.Id in (" & ClientsIds & ")"
        End If

        ' si filtra por Vendedores
        If INDListSellers.Count > 0 Then
            Dim SellersIds As String
            For Each ItemSellers As String In INDListSellers
                If SellersIds IsNot Nothing Then
                    SellersIds &= ","
                End If
                SellersIds &= ItemSellers
            Next
            filtroConsulta &= " AND SellerId.Id in (" & SellersIds & ")"
        End If

        ' si filtra por Facturas
        If INDListInvoices.Count > 0 Then
            Dim InvoicesIds As String
            For Each ItemInvoices As String In INDListInvoices
                If InvoicesIds IsNot Nothing Then
                    InvoicesIds &= ","
                End If
                InvoicesIds &= ItemInvoices
            Next
            filtroConsulta &= " AND Id in (" & InvoicesIds & ")"
        End If

        If Me.INDStatuss <> 4 Then
            filtroConsulta &= " AND Status = " & INDStatuss
        End If

        Dim listReport As List(Of PortfolioAccountReceivableReportXpo) = XpoServiceex.Instance(IndigoSessionValues.TransactionalContainer).PaymentsService.GetCollection(Of PortfolioAccountReceivableReportXpo)(Nothing, filtroConsulta)
        Me.DataSource = listReport
    End Sub

    Public Sub CargarImagenes() Implements IReport.CargarImagenes

    End Sub

    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return ""
        End Get
    End Property

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    Private Sub rptDocumentAccountsReceivable_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        INDLblNitCompany.Text = "Nit:" & IndigoSessionValues.IndigoCompanyNit
        INDLblUserPrint.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName
    End Sub
End Class