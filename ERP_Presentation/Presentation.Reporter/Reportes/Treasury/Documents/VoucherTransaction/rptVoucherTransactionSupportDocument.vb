Imports System.Drawing.Printing
Imports System.IO
Imports DevExpress.XtraReports.Parameters
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.PaymentsRepository

Public Class rptVoucherTransactionSupportDocument
    Implements IReport
    Implements IReportAsync
    Implements IDocumentSupportReport

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    Public ReadOnly Property NameReport As String Implements IReport.NameReport

    Private DataList As List(Of ViewElectronicDocumentSupportRptXpo)

#Region "Properties"

    Public WriteOnly Property TitleText As String Implements IDocumentSupportReport.TitleText
        Set(value As String)
            Me.INDLblTitle.Text = value
        End Set
    End Property

    Public WriteOnly Property IdentificationLabel As String Implements IDocumentSupportReport.IdentificationLabel
        Set(value As String)
            Me.XrLabel1.Text = value
        End Set
    End Property

    Public WriteOnly Property ReceiverLabel As String Implements IDocumentSupportReport.ReceiverLabel
        Set(value As String)
            Me.XrLabel5.Text = value
        End Set
    End Property

    Public WriteOnly Property ShowIcaRetention As Boolean Implements IDocumentSupportReport.ShowIcaRetention
        Set(value As Boolean)
            Me.INDTxtReteIca.Visible = value
        End Set
    End Property

    Public WriteOnly Property ShowAuthorization As Boolean Implements IDocumentSupportReport.ShowAuthorization
        Set(value As Boolean)
            Me.INDLblAutorizacion.Visible = value
            Me.INDLblAutoriza.Visible = value
        End Set
    End Property

#End Region

    Public Sub New()
        InitializeComponent()

        Dim location = New SupportDocumentCustomReportByLocation(Me)
        location.ApplyLocalization()
    End Sub


    Public Sub CargarImagenes() Implements IReport.CargarImagenes
    End Sub

    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        DataList = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).TreasuryService.GetCollection(Of ViewElectronicDocumentSupportRptXpo)(Nothing, $"IdParentDocument = {ParametrosReporte(0)} AND EntityName ='VoucherTransaction'")
        If DataList?.Any() Then
            DataSource = DataList
            Visible = True
        Else
            Me.Visible = False
        End If
    End Sub

    Public Function CargarDataSourceAsync() As Task Implements IReportAsync.CargarDataSourceAsync
        Return Task.Factory.StartNew(AddressOf CargarDataSource)
    End Function

    Private Sub rpt_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        If Me.Parameters.Count > 0 And Me.Parameters(0).Value > 0 Then

            Dim ParametrosFilter As ParameterCollection = Me.Parameters
            ParametrosReporte = New Object() {ParametrosFilter("AccountPayableId").Value}
            CargarDataSource()

        End If

        LoadCompanyInfo()
    End Sub

    Private Sub LoadCompanyInfo()
        Dim firstRow As ViewElectronicDocumentSupportRptXpo = DataList?.FirstOrDefault

        If firstRow IsNot Nothing Then
            Me.INDLblCompany.Text = firstRow.NameCustomer
            Me.INDLblNitCompany.Text = $"Nit: {IndigoSessionValues.IndigoCompanyNit} - Dirección: {firstRow.AddresssCustomer} - Teléfono: {firstRow.PhoneCustomer}"
            Me.INDLblDocumentNumber.Text = $"N° {firstRow.DocumentNumber}"
            Me.INDLblValorEnLetras.Text = Utils.Num2Text(firstRow.Total).ToString & " PESOS"

            Dim retefuente = DataList.Where(Function(m) m.RetencionType = 1).Sum(Function(m) m.CreditValue)
            Dim reteIva = DataList.Where(Function(m) m.RetencionType = 2).Sum(Function(m) m.CreditValue)
            Dim reteIca = DataList.Where(Function(m) m.RetencionType = 3).Sum(Function(m) m.CreditValue)
            Dim otrasDeducciones = DataList.Where(Function(m) Not {1, 2, 3}.Contains(m.RetencionType)).Sum(Function(m) m.CreditValue)

            INDLblSubtotal.Text = DataList.Sum(Function(m) m.DebitValue).ToString("c2")
            INDLblRetentions.Text = (reteIca + reteIva + retefuente).ToString("c2")

            If reteIva > 0 Then
                XrTableRow17.Visible = True
                INDLblReteIVA.Text = reteIva.ToString("c2")
            End If

            If reteIca > 0 Then
                XrTableRow18.Visible = True
                INDLblReteIca.Text = reteIca.ToString("c2")
            End If

            If retefuente > 0 Then
                XrTableRow19.Visible = True
                INDLblRetefuente.Text = retefuente.ToString("c2")
            End If

            If String.IsNullOrEmpty(firstRow.QR) Then
                XrBarCode1.Visible = False
            End If

            INDLblOtrasDeducciones.Text = otrasDeducciones.ToString("c2")
            INDLblValorNetoPagar.Text = firstRow.Total.ToString("c2")
            INDLblAutorizacion.Text = $"Autorización de numeración de Documento Soporte Electrónico DIAN número {firstRow.ResolutionNumber} Fecha {firstRow.ResolutionDate}"
            INDLblAutoriza.Text = $"Autoriza del {firstRow.InvoicePrefix} {firstRow.InitialInvoice} al {firstRow.InvoicePrefix} {firstRow.FinalInvoice} desde el {firstRow.InitialDate} hasta el {firstRow.FinalDate}"
        End If
    End Sub
End Class