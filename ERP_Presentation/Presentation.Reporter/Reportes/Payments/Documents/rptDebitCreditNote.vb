#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.PaymentsRepository
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraReports.Parameters
Imports System.Globalization
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo.AccountingRepository

#End Region

Public Class rptDebitCreditNote
    Implements IReport

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Private ValueTotal As Double
    Private INDUser As Object
    Private NoteType As Byte
    Private INDList As List(Of PaymentsPaymentNotes)
    Private CurrencyAbbreviation As String = SessionValues.Instance.CurrencyISO4217

    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Dim filtroConsulta As String = "Id = " & ParametrosReporte(0)
        INDList = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).TreasuryService.GetCollection(Of PaymentsPaymentNotes)(Nothing, filtroConsulta)
        Dim INDCodeUser = CType(INDList(0), PaymentsPaymentNotes).CreationUser.Trim()
        Dim INDTypeNote = CType(INDList(0), PaymentsPaymentNotes).IndicatesBillAdvance
        ValueTotal = 0

        'ocultar los Totales
        Dim table1 As XRTable = CType(XrTable16, XRTable)
        Dim table2 As XRTable = CType(XrTable12, XRTable)
        Dim ivadetail = New List(Of PaymentsNoteDetailsAccountInfo)
        If INDTypeNote = 1 Then
            table1.Rows.Remove(XrTableRow22)
        Else
            table2.Rows.Remove(XrTableRow20)
        End If
        For Each item In INDList
            If item.IndicatesBillAdvance = 2 Then
                Me.XrTableCell50.Text = "REVERSION CxP"
                DetailReport3.Visible = False
                DetailReport4.Visible = False
                DetailReport5.Visible = False
                ''se construye los conceptos en base a la cuenta por pagar
                Dim accountPayable As PaymentsAccountPayable = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).PaymentsService.GetCollection(Of Infrastructure.Data.Xpo.PaymentsRepository.PaymentsAccountPayable)(Nothing, "Id = '" & item.IdAccountPayable.Id & "'").FirstOrDefault()
                If accountPayable IsNot Nothing Then
                    ''se agrega item del total de la factura al debito
                    ivadetail.Add(New PaymentsNoteDetailsAccountInfo With {
                                                                            .MainAccountCodeName = $"{accountPayable?.IdAccount?.Number} - { accountPayable?.IdAccount?.Name}",
                                                                            .CostCenterCodeName = "",
                                                                            .NatureName = "Débito",
                                                                            .DebitValue = accountPayable?.Value
                                                                        })
                    ''se agregan items de iva
                    If accountPayable.TaxRegistration = 2 Then
                        For Each detailAccount In accountPayable.PaymentsAccountPayableDetailConceptXpoP.Where(Function(s) s.IvaValue.HasValue AndAlso s.IvaValue > 0)?.ToList()
                            ''se agrega item del total de la factura
                            ivadetail.Add(New PaymentsNoteDetailsAccountInfo With {
                                                                            .MainAccountCodeName = $"{detailAccount?.IdConceptAccountPayable?.IdAccount?.Number} - { detailAccount?.IdConceptAccountPayable?.IdAccount?.Name}",
                                                                            .CostCenterCodeName = "",
                                                                            .NatureName = "Crédito",
                                                                            .CreditValue = detailAccount?.BaseValue
                                                                        })
                            ivadetail.Add(New PaymentsNoteDetailsAccountInfo With {
                                                                                .MainAccountCodeName = $"{detailAccount?.RateIva?.AccountPurchaseService?.Number} - { detailAccount?.RateIva?.AccountPurchaseService?.Name}",
                                                                                .CostCenterCodeName = "",
                                                                                .NatureName = If(detailAccount?.Nature = 1, "Crédito", "Débito"),
                                                                                .CreditValue = If(detailAccount?.Nature = 1, detailAccount?.IvaValue, 0),
                                                                                .DebitValue = If(detailAccount?.Nature = 2, detailAccount?.IvaValue, 0)
                                                                            })

                        Next
                    Else
                        For Each detailAccount In accountPayable.PaymentsAccountPayableDetailConceptXpoP.Where(Function(s) s.IvaValue.HasValue AndAlso s.IvaValue > 0)?.ToList()

                            ''se valida si es al costo o al costo fical
                            If accountPayable?.TaxRegistration = 4 Then
                                If detailAccount?.Nature = 1 Then
                                    ''se agrega item del total de la factura
                                    ivadetail.Add(New PaymentsNoteDetailsAccountInfo With {
                                                                            .MainAccountCodeName = $"{detailAccount?.IdConceptAccountPayable?.IdAccount?.Number} - { detailAccount?.IdConceptAccountPayable?.IdAccount?.Name}",
                                                                            .CostCenterCodeName = "",
                                                                            .NatureName = "Crédito",
                                                                            .CreditValue = detailAccount?.BaseValue
                                                                        })

                                    ivadetail.Add(New PaymentsNoteDetailsAccountInfo With {
                                                                         .MainAccountCodeName = $"{detailAccount?.IdConceptAccountPayable?.IdAccount?.Number} - { detailAccount?.IdConceptAccountPayable?.IdAccount?.Name}",
                                                                         .CostCenterCodeName = "",
                                                                         .NatureName = If(detailAccount?.Nature = 1, "Crédito", "Débito"),
                                                                         .CreditValue = If(detailAccount?.Nature = 1, detailAccount?.IvaValue, 0),
                                                                         .DebitValue = If(detailAccount?.Nature = 2, detailAccount?.IvaValue, 0)
                                                                      })
                                End If
                            ElseIf accountPayable?.TaxRegistration = 1 Then
                                ''se agrega item del total de la factura
                                ivadetail.Add(New PaymentsNoteDetailsAccountInfo With {
                                                                            .MainAccountCodeName = $"{detailAccount?.IdConceptAccountPayable?.IdAccount?.Number} - { detailAccount?.IdConceptAccountPayable?.IdAccount?.Name}",
                                                                            .CostCenterCodeName = "",
                                                                            .NatureName = "Crédito",
                                                                            .CreditValue = detailAccount?.TotalConcept
                                                                        })

                                ivadetail.Add(New PaymentsNoteDetailsAccountInfo With {
                                                                                .MainAccountCodeName = $"{detailAccount?.RateIva?.AccountDebitControlFiscal?.Number} - { detailAccount?.RateIva?.AccountDebitControlFiscal?.Name}",
                                                                                .CostCenterCodeName = "",
                                                                                .NatureName = ResourceManager.GetString("AccountNatureDebit"),
                                                                                .CreditValue = If(detailAccount?.IvaValue > 0, detailAccount?.IvaValue, 0),
                                                                                .Nature = 1
                                                                                })

                                ivadetail.Add(New PaymentsNoteDetailsAccountInfo With {
                                                                         .MainAccountCodeName = $"{detailAccount?.RateIva?.AccountCreditControlFiscal?.Number} - { detailAccount?.RateIva?.AccountCreditControlFiscal?.Name}",
                                                                         .CostCenterCodeName = "",
                                                                         .NatureName = ResourceManager.GetString("AccountNatureCredit"),
                                                                         .DebitValue = If(detailAccount?.IvaValue > 0, detailAccount?.IvaValue, 0),
                                                                         .Nature = 2
                                                                      })

                            End If
                        Next
                    End If
                    ''se agrega item de retencion
                    For Each detailAccount In accountPayable.PaymentsAccountPayableDetailConceptXpoP.Where(Function(s) s.IdRetentionConcept?.Id IsNot Nothing AndAlso s.IdRetentionConcept?.Id > 0)?.ToList()
                        ''se agrega item del total de la factura
                        ivadetail.Add(New PaymentsNoteDetailsAccountInfo With {
                                                                                .MainAccountCodeName = $"{detailAccount?.IdRetentionConcept?.Code} - { detailAccount?.IdRetentionConcept?.Name}",
                                                                                .CostCenterCodeName = "",
                                                                                .NatureName = If(detailAccount?.Nature = 1, "Crédito", "Débito"),
                                                                                .CreditValue = If(detailAccount?.Nature = 1, detailAccount?.Value, 0),
                                                                                .DebitValue = If(detailAccount?.Nature = 2, detailAccount?.Value, 0)
                                                                            })
                    Next
                End If
            Else
                DetailReport3.Visible = True
                DetailReport4.Visible = True
                DetailReport5.Visible = True
                Me.XrTableCell50.Text = "CONCEPTO"
            End If
            NoteType = item.IndicatesBillAdvance
            If NoteType = 2 Then
                ValueTotal += item.IdAccountPayable.Value
            Else
                ValueTotal = 0
            End If
        Next
        INDUser = XpoServiceEx.Instance(IndigoSessionValues.SecurityContainer).PaymentsService.GetCollection(Of Infrastructure.Data.Xpo.SecurityRepository.UserXpo)(Nothing, "UserCode = '" & INDCodeUser & "'")
        Me.DataSource = INDList

        For Each Itemc In INDList
            Itemc?.PaymentsPaymentsNoteDetailsXpo?.Where(Function(s) s.DiscountableIVA.HasValue AndAlso s.IVAValue.HasValue)?.ToList()?.ForEach(Sub(item)
                                                                                                                                                    If item?.DiscountableIVA IsNot Nothing AndAlso item?.IVAValue > 0 Then
                                                                                                                                                        If item?.TaxRegistration = 2 Then

                                                                                                                                                            ivadetail.Add(New PaymentsNoteDetailsAccountInfo With {
                                                                                                                                                                                                              .MainAccountCodeName = $"{item?.IdGeneralLedgerIVA?.AccountPurchaseService?.Number} - { item?.IdGeneralLedgerIVA?.AccountPurchaseService?.Name}",
                                                                                                                                                                                                              .CostCenterCodeName = "",
                                                                                                                                                                                                              .NatureName = If(item?.Nature = 1, "Débito", "Crédito"),
                                                                                                                                                                                                              .DebitValue = If(item?.Nature = 1, item?.IVAValue, 0),
                                                                                                                                                                                                              .CreditValue = If(item?.Nature = 2, item?.IVAValue, 0)
                                                                                                                                                                                                          })

                                                                                                                                                        ElseIf item?.TaxRegistration = 1 Then
                                                                                                                                                            ivadetail.Add(New PaymentsNoteDetailsAccountInfo With {
                                                                                                                                                                                                             .MainAccountCodeName = $"{item?.IdGeneralLedgerIVA?.AccountDebitControlFiscal?.Number} - { item?.IdGeneralLedgerIVA?.AccountDebitControlFiscal?.Name}",
                                                                                                                                                                                                             .CostCenterCodeName = "",
                                                                                                                                                                                                             .NatureName = ResourceManager.GetString("AccountNatureDebit"),
                                                                                                                                                                                                             .DebitValue = If(item?.IVAValue > 0, item?.IVAValue, 0),
                                                                                                                                                                                                             .Nature = 1
                                                                                                                                                                                                          })

                                                                                                                                                            ivadetail.Add(New PaymentsNoteDetailsAccountInfo With {
                                                                                                                                                                                                             .MainAccountCodeName = $"{item?.IdGeneralLedgerIVA?.AccountCreditControlFiscal?.Number} - { item?.IdGeneralLedgerIVA?.AccountCreditControlFiscal?.Name}",
                                                                                                                                                                                                             .CostCenterCodeName = "",
                                                                                                                                                                                                             .NatureName = ResourceManager.GetString("AccountNatureCredit"),
                                                                                                                                                                                                             .CreditValue = If(item?.IVAValue > 0, item?.IVAValue, 0),
                                                                                                                                                                                                             .Nature = 2
                                                                                                                                                                                                          })
                                                                                                                                                        ElseIf item?.TaxRegistration = 4 Then
                                                                                                                                                            ivadetail.Add(New PaymentsNoteDetailsAccountInfo With {
                                                                                                                                                                                                              .MainAccountCodeName = $"{item?.IdAccount?.Number} - { item?.IdAccount?.Name}",
                                                                                                                                                                                                              .CostCenterCodeName = "",
                                                                                                                                                                                                              .NatureName = If(item?.Nature = 1, "Débito", "Crédito"),
                                                                                                                                                                                                              .DebitValue = If(item?.Nature = 1, item?.IVAValue, 0),
                                                                                                                                                                                                              .CreditValue = If(item?.Nature = 2, item?.IVAValue, 0)
                                                                                                                                                                                                          })
                                                                                                                                                        End If

                                                                                                                                                    End If
                                                                                                                                                End Sub)
        Next
        Me.DetailReport8.DataSource = ivadetail
        If Not ivadetail.Count > 0 Then
            Me.DetailReport8.Visible = False
        End If
        If NoteType <> 2 Then
            Dim filtroConsulta2 As String = "IdPaymentsNote.Id = " & ParametrosReporte(0)
            Dim List As List(Of PaymentsPaymentsNoteDetailsXpo) = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).PaymentsService.GetCollection(Of PaymentsPaymentsNoteDetailsXpo)(Nothing, filtroConsulta2)
            For Each item In List
                If item.IdPaymentsNote.Nature = 1 Then
                    ValueTotal += IIf(item.Nature = 2, item.Value, 0)
                Else
                    ValueTotal += IIf(item.Nature = 1, item.Value, 0)
                End If
            Next
        End If
    End Sub

    Public Sub CargarImagenes() Implements IReport.CargarImagenes

    End Sub

    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return ""
        End Get
    End Property

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    Private Sub rptDebitCreditNote_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        If Me.Parameters.Count > 0 And Me.Parameters(0).Value > 0 Then
            Dim ParametrosFilter As ParameterCollection = Me.Parameters
            ParametrosReporte = New Object() {ParametrosFilter("INDSubIdNote").Value}
            CargarDataSource()
        End If

        Dim _culture As CultureInfo = CultureInfo.CurrentCulture.Clone()
        Dim currencyName As String = TryCast(Me.DataSource, List(Of PaymentsPaymentNotes))?.FirstOrDefault?.Currency?.ISO4217Xpo?.CurrencyName
        Dim CurrencyAbbreviation As String = TryCast(Me.DataSource, List(Of PaymentsPaymentNotes))?.FirstOrDefault?.CurrencyAbbreviation
        Dim CurrencyDecimal = TryCast(Me.DataSource, List(Of PaymentsPaymentNotes))?.FirstOrDefault?.Currency?.ISO4217Xpo?.CodeAbbreviation
        If String.IsNullOrEmpty(CurrencyAbbreviation) Then
            CurrencyAbbreviation = SessionValues.Instance.CurrencyISO4217
            currencyName = CurrencyAbbreviation
        End If

        _culture = CultureInfo.CurrentCulture.Clone()
        _culture.NumberFormat = CurrencyAbbreviation.GetNumberFormat
        ApplyLocalization(_culture)

        Dim _integerPart As Int64 = Int(Convert.ToDecimal(ValueTotal))
        Dim _decimalPart As Integer = Strings.Right(Format(Convert.ToDecimal(ValueTotal) - _integerPart, "0.00"), 2)
        Me.XrTableCell14.Text = String.Format("{0}  {1}{2}",
                                                 Utils.Num2Text(_integerPart).ToString,
                                                 currencyName.ToUpper,
                                                 If(_decimalPart > 0, $", CON {Utils.Num2Text(_decimalPart).ToString}  {Utils.ListDecimalCurrency(CurrencyDecimal)}", ""))

        INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        INDLblNitCompany.Text = "Nit:" & IndigoSessionValues.IndigoCompanyNit
        INDUserImp.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName
        If INDUser.count() > 0 Then
            INDLblCreationUser.Text = INDUser(0).CodeName
        End If
        Me.XrTableCell49.Text = String.Format(_culture.NumberFormat, "{0:c2}", ValueTotal)
        If NoteType = 2 Then
            XrTableCell52.Text = ""
        End If
    End Sub
End Class