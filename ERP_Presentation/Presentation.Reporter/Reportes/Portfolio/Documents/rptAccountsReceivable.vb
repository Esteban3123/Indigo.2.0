Imports Infrastructure.Data.Xpo.GlosasRepository
Imports Infrastructure.Data.Xpo.PortfolioRepository
Imports System.IO
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraPrinting
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Domain.Entities
Imports DevExpress.Data.Filtering
Imports Presentation.Base.Extension
Imports DevExpress.XtraReports.Parameters
Imports DevExpress.Xpo
Imports Presentation.CloudAgent
Imports System.Globalization

Public Class rptAccountsReceivable
    Implements IReport

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Const CNameReport = "Glosas.FrmInvoiceRadicate"

    Public valueTotal As Decimal = 0

    Private OperatingUnit As XPCollection

    ''' <summary>
    ''' obtiene la moneda a la cual se realiza el reporte 
    ''' </summary>
    Dim CurrencyData As Currency

    ''' <summary>
    ''' abreviacion de la moneda 
    ''' </summary>
    Dim CurrencyAbbreviation As String
    ''' <summary>
    ''' tipo de documento - si es NIT O Cedula Juridica
    ''' </summary>
    Dim IdentificationType As String

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    ''' <summary>
    ''' Metodo para cargar imagenes
    ''' </summary>
    Public Sub CargarImagenes() Implements IReport.CargarImagenes

    End Sub

    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        valueTotal = 0
        Me.DataSource = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).GlosasService.GetCollection(Of PortfolioViewReportRadicateInvoiceDocumentXpo)(Nothing, "radicateInvoiceCId=" & Me.ParametrosReporte(0))
        Dim List As List(Of PortfolioViewReportRadicateInvoiceDocumentXpo) = CType(Me.DataSource, List(Of PortfolioViewReportRadicateInvoiceDocumentXpo))
        If List.Count > 0 Then
            XrLabelNumeroFactura.Text = List.Where(Function(x) x.radicateInvoiceStatus <> "4").Count
            For Each item In List.Where(Function(x) x.radicateInvoiceStatus <> "4")
                If item.BalanceInvoice > 0 Then
                    valueTotal += item.BalanceInvoice
                End If
            Next
        End If

        'A raiz de un error presentado con el Num2Text, se opta por usar la funcion propia de Visual Basic Utils.Num2Text
        If Me.ParametrosReporte(1) IsNot Nothing Then
            OperatingUnit = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).AccountingService.ListOperatingUnitByIdd(ParametrosReporte(1))
            Me.INDlblIPSCode.Text = If(Me.OperatingUnit(0).IPSCode IsNot Nothing, Me.OperatingUnit(0).IPSCode.Trim(), String.Empty)
            Me.INDlblAddress.Text = If(Me.OperatingUnit(0).Address IsNot Nothing, Me.OperatingUnit(0).Address.Trim() & If(Me.OperatingUnit(0).IdCity IsNot Nothing, If(Me.OperatingUnit(0).IdCity.Name IsNot Nothing, " " & Me.OperatingUnit(0).IdCity.Name.Trim() & If(Me.OperatingUnit(0).IdCity.DepartamentId IsNot Nothing, " - " & Me.OperatingUnit(0).IdCity.DepartamentId.Name.Trim(), String.Empty), String.Empty), String.Empty), String.Empty)
            Me.INDlblPhoneEmail.Text = If(Me.OperatingUnit(0).Phone IsNot Nothing, Me.OperatingUnit(0).Phone.Trim() & If(Me.OperatingUnit(0).EmailAudit IsNot Nothing, " - " & Me.OperatingUnit(0).EmailAudit.Trim(), String.Empty), String.Empty)

            If Me.OperatingUnit(0).IdCity IsNot Nothing Then
                Me.INDxtrLabelDadoEn.Text = "Dado En " & IIf(Me.OperatingUnit(0).IdCity.Name IsNot Nothing, Me.OperatingUnit(0).IdCity.Name, " ") & ", Fecha "
            End If
        End If

        Me.XrLabel19.Text = "POR CONCEPTO DE: PRESTACION DE SERVICIOS DE SALUD BRINDADOS A LOS USUARIOS DE SU ENTIDAD, SEGÚN ANEXO DE FACTURAS DE LA CUENTA DE COBRO No. " & List(0).RadicatedConsecutive & " ADJUNTA, LAS CUALES SE ANEXAN INTEGRALMENTE CON LOS SOPORTES  ESTABLECIDOS POR EL MINISTERIO DE SALUD Y PROTECCIÓN SOCIAL, Y  QUE HACEN PARTE COMPLEMENTARIA DEL PRESENTE DOCUMENTO."
    End Sub

    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return rptAccountsReceivable.CNameReport
        End Get
    End Property

    Private Sub rptAccountsReceivable_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint

        If Me.Parameters.Count > 0 And Me.Parameters(1).Value > 0 Then
            Dim ParametrosFilter As ParameterCollection = Me.Parameters
            ParametrosReporte = New Object() {ParametrosFilter("INDSubIdAccountReceivable").Value,
                                              ParametrosFilter("INDFooterReceivable").Value}
            CargarDataSource()
        End If

        Me.INDLblNombreEmpresaCliente.Text = IndigoSessionValues.IndigoCompanyName
        Me.INDLblSubTitle.Text = IndigoSessionValues.IndigoCompanyNit

        Me.INDLblTiTle2.Text = IndigoSessionValues.IndigoCompanyName
        Me.INDLblSubTitle2.Text = "NIT. : " & IndigoSessionValues.IndigoCompanyNit

        If Me.ParametrosReporte(1) IsNot Nothing AndAlso Me.OperatingUnit(0).IdCity IsNot Nothing Then
            Me.INDxtrLabelDadoEn.Text = "Dado En " & Me.OperatingUnit(0).IdCity.Name & ", Fecha "
        End If
        INDPrmNameCompany.Value = IndigoSessionValues.IndigoCompanyName
    End Sub

    Private Sub LblInvoiceValueEntity_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles LblInvoiceValueEntity.BeforePrint
        Dim row = GetCurrentRow()
        Dim CurrencyAbbreviation = DirectCast(row, PortfolioViewReportRadicateInvoiceDocumentXpo)?.CurrencyAbbreviation

        If Not String.IsNullOrEmpty(CurrencyAbbreviation) Then
            LblInvoiceValueEntity.Text = Utils.GetMoneyWithISO4217(LblInvoiceValueEntity.Text, CurrencyAbbreviation)
        End If
    End Sub

    Private Sub LblCreditNoteValue_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles LblCreditNoteValue.BeforePrint
        Dim row = GetCurrentRow()
        Dim CurrencyAbbreviation = DirectCast(row, PortfolioViewReportRadicateInvoiceDocumentXpo)?.CurrencyAbbreviation

        If Not String.IsNullOrEmpty(CurrencyAbbreviation) Then
            LblCreditNoteValue.Text = Utils.GetMoneyWithISO4217(LblCreditNoteValue.Text, CurrencyAbbreviation)
        End If
    End Sub

    Private Sub XrLabel6_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrLabel6.BeforePrint
        Dim row = GetCurrentRow()
        Dim CurrencyAbbreviation = DirectCast(row, PortfolioViewReportRadicateInvoiceDocumentXpo)?.CurrencyAbbreviation

        If Not String.IsNullOrEmpty(CurrencyAbbreviation) Then
            XrLabel6.Text = Utils.GetMoneyWithISO4217(XrLabel6.Text, CurrencyAbbreviation)
        End If
    End Sub

    Private Sub LblBalanceInvoice_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles LblBalanceInvoice.BeforePrint
        Dim row = GetCurrentRow()
        Dim CurrencyAbbreviation = DirectCast(row, PortfolioViewReportRadicateInvoiceDocumentXpo)?.CurrencyAbbreviation

        If Not String.IsNullOrEmpty(CurrencyAbbreviation) Then
            LblBalanceInvoice.Text = Utils.GetMoneyWithISO4217(LblBalanceInvoice.Text, CurrencyAbbreviation)
        End If
    End Sub



    Private Sub GroupHeader4_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles GroupHeader4.BeforePrint
        Dim sourceList As List(Of PortfolioViewReportRadicateInvoiceDocumentXpo) = CType(Me.DataSource, List(Of PortfolioViewReportRadicateInvoiceDocumentXpo))
        If sourceList Is Nothing Then Exit Sub
        Dim ParametrosFilter As ParameterCollection = Me.Parameters
        If Me.Parameters.Count > 0 And Me.Parameters(1).Value > 0 Then
            ParametrosReporte = New Object() {ParametrosFilter("INDSubIdAccountReceivable").Value,
                                              ParametrosFilter("INDFooterReceivable").Value}
        End If
        Dim GroupByCurrency = (From x In sourceList
                               Group By x.CurrencyAbbreviation, x.CurrencyId Into Group
                               Select CurrencyAbbreviation, CurrencyId,
                                        TotalSaldo = Group.Sum(Function(f)
                                                                   If ParametrosFilter("INDPrDevolution").Value = "2" Then
                                                                       If f.radicateInvoiceStatus = "4" Then
                                                                           Return 0
                                                                       Else
                                                                           Return f.BalanceInvoice
                                                                       End If
                                                                   Else
                                                                       Return f.BalanceInvoice
                                                                   End If
                                                               End Function))

        Dim row = 0
        For Each ObjItem In GroupByCurrency
            If row = 0 Then
                Dim CurrencyInfo = IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetCurrencyById(ObjItem.CurrencyId, IndigoSessionValues)

                XrTableCell4.Text = $"LA SUMA DE "
                Dim _integerPart As Int64 = Int(Convert.ToDecimal(ObjItem.TotalSaldo))
                Dim _decimalPart As Int64 = Strings.Right(Format(Convert.ToDecimal(ObjItem.TotalSaldo) - _integerPart, "0.00"), 2)

                XrTableCell5.Text = Utils.GetMoneyWithISO4217(ObjItem.TotalSaldo, ObjItem.CurrencyAbbreviation)
                XrTableCell6.Text = String.Format("{0} {1}{2}",
                                                          Utils.Num2Text(_integerPart).ToString,
                                                          CurrencyInfo.ISO4217.CurrencyName.ToUpper,
                                                          If(_decimalPart > 0, $", CON {Utils.Num2Text(_decimalPart).ToString } {Utils.ListDecimalCurrency(ObjItem.CurrencyAbbreviation)}", ""))
                row += 1
                Continue For
            End If

            XrTable2.InsertRowBelow(XrTable2.Rows.LastRow)
            Dim irow = XrTable2.Rows.LastRow.Index
            For Each Column As XRTableCell In XrTableRow2
                Dim cell = XrTable2.Rows(irow).Cells.Item(Column.Index)
                Select Case Column.Name
                    Case NameOf(XrTableCell4)
                        cell.Text = $" Y LA SUMA DE "
                    Case NameOf(XrTableCell5)
                        cell.Text = Utils.GetMoneyWithISO4217(ObjItem.TotalSaldo, ObjItem.CurrencyAbbreviation)
                    Case NameOf(XrTableCell6)
                        Dim CurrencyInfo = IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetCurrencyById(ObjItem.CurrencyId, IndigoSessionValues)

                        Dim _integerPart As Int64 = Int(Convert.ToDecimal(ObjItem.TotalSaldo))
                        Dim _decimalPart As Int64 = Strings.Right(Format(Convert.ToDecimal(ObjItem.TotalSaldo) - _integerPart, "0.00"), 2)

                        cell.Text = String.Format("{0} {1}{2}",
                                                          Utils.Num2Text(_integerPart).ToString,
                                                          CurrencyInfo.ISO4217.CurrencyName.ToUpper,
                                                          If(_decimalPart > 0, $", CON {Utils.Num2Text(_decimalPart).ToString } {Utils.ListDecimalCurrency(ObjItem.CurrencyAbbreviation)}", ""))
                End Select
            Next
        Next
    End Sub

    Private Sub GroupFooter2_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles GroupFooter2.BeforePrint
        Dim sourceList As List(Of PortfolioViewReportRadicateInvoiceDocumentXpo) = CType(Me.DataSource, List(Of PortfolioViewReportRadicateInvoiceDocumentXpo))
        If sourceList Is Nothing Then Exit Sub
        Dim ParametrosFilter As ParameterCollection = Me.Parameters
        If Me.Parameters.Count > 0 And Me.Parameters(1).Value > 0 Then
            ParametrosReporte = New Object() {ParametrosFilter("INDSubIdAccountReceivable").Value,
                                              ParametrosFilter("INDFooterReceivable").Value}
        End If
        Dim GroupByCurrency = (From x In sourceList
                               Group By x.CurrencyAbbreviation Into Group
                               Select CurrencyAbbreviation,
                                        TotalInvoiceValue = Group.Sum(Function(f)
                                                                          If ParametrosFilter("INDPrDevolution").Value = "2" Then
                                                                              If f.radicateInvoiceStatus = "4" Then
                                                                                  Return 0
                                                                              Else
                                                                                  Return f.InvoiceValueEntity + f.InvoiceValuePacient
                                                                              End If
                                                                          Else
                                                                              Return f.InvoiceValueEntity + f.InvoiceValuePacient
                                                                          End If
                                                                      End Function),
                                        TotalCredit = Group.Sum(Function(f)
                                                                    If ParametrosFilter("INDPrDevolution").Value = "2" Then
                                                                        If f.radicateInvoiceStatus = "4" Then
                                                                            Return 0
                                                                        Else
                                                                            Return f.CreditNoteValue
                                                                        End If
                                                                    Else
                                                                        Return f.CreditNoteValue
                                                                    End If
                                                                End Function),
                                        TotalAnticipo = Group.Sum(Function(f)
                                                                      If ParametrosFilter("INDPrDevolution").Value = "2" Then
                                                                          If f.radicateInvoiceStatus = "4" Then
                                                                              Return 0
                                                                          Else
                                                                              Return f.InvoiceValuePacient
                                                                          End If
                                                                      Else
                                                                          Return f.InvoiceValuePacient
                                                                      End If
                                                                  End Function),
                                        TotalSaldo = Group.Sum(Function(f)
                                                                   If ParametrosFilter("INDPrDevolution").Value = "2" Then
                                                                       If f.radicateInvoiceStatus = "4" Then
                                                                           Return 0
                                                                       Else
                                                                           Return f.BalanceInvoice
                                                                       End If
                                                                   Else
                                                                       Return f.BalanceInvoice
                                                                   End If
                                                               End Function))

        Dim row = 0
        For Each ObjItem In GroupByCurrency
            If row = 0 Then
                INDXrTotalName.Text = $"Valor Total {ObjItem.CurrencyAbbreviation}"
                XrTableCell3.Text = Utils.GetMoneyWithISO4217(ObjItem.TotalInvoiceValue, ObjItem.CurrencyAbbreviation)
                XrTableCell41.Text = Utils.GetMoneyWithISO4217(ObjItem.TotalCredit, ObjItem.CurrencyAbbreviation)
                XrTableCell42.Text = Utils.GetMoneyWithISO4217(ObjItem.TotalAnticipo, ObjItem.CurrencyAbbreviation)
                XrTableCell43.Text = Utils.GetMoneyWithISO4217(ObjItem.TotalSaldo, ObjItem.CurrencyAbbreviation)
                row += 1
                Continue For
            End If

            INDTblFooterReportAR.InsertRowBelow(INDTblFooterReportAR.Rows.LastRow)
            Dim irow = INDTblFooterReportAR.Rows.LastRow.Index
            For Each Column As XRTableCell In XrTableRow11
                Dim cell = INDTblFooterReportAR.Rows(irow).Cells.Item(Column.Index)
                Select Case Column.Name
                    Case NameOf(INDXrTotalName)
                        cell.Text = $"Valor Total {ObjItem.CurrencyAbbreviation}"
                    Case NameOf(XrTableCell3)
                        cell.Text = Utils.GetMoneyWithISO4217(ObjItem.TotalInvoiceValue, ObjItem.CurrencyAbbreviation)
                    Case NameOf(XrTableCell41)
                        cell.Text = Utils.GetMoneyWithISO4217(ObjItem.TotalCredit, ObjItem.CurrencyAbbreviation)
                    Case NameOf(XrTableCell42)
                        cell.Text = Utils.GetMoneyWithISO4217(ObjItem.TotalAnticipo, ObjItem.CurrencyAbbreviation)
                    Case NameOf(XrTableCell43)
                        cell.Text = Utils.GetMoneyWithISO4217(ObjItem.TotalSaldo, ObjItem.CurrencyAbbreviation)
                End Select
            Next
        Next
    End Sub
End Class