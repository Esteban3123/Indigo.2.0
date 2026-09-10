#Region "Librerias Improtadas"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.AccountingRepository
Imports Infrastructure.Data.Xpo.BillingRepository
Imports Infrastructure.Data.Xpo.CrystalRepository
Imports Domain.Entities
Imports DevExpress.XtraReports.UI
Imports System.Drawing.Printing
Imports DevExpress.XtraReports.Parameters
Imports System.Globalization
#End Region

Public Class rptSaleInvoiceReduced
    Implements IReport

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance
    Dim INDDocumentType
    Dim INDAdmissionNumber As String

    Const CNameReport = "Facturacion.CtrFolio"

    Public Sub CargarDataSource() Implements IReport.CargarDataSource

        Dim filtroConsulta As String = "InvoiceId.Id = " & ParametrosReporte(0)

        'Se obtiene el Código del Paciente, el Grupo de Atención y el Id de la Entidad
        Dim INDList = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).BillingService.GetCollection(Of DetailInvoicesXpo)(Nothing, filtroConsulta)

        If INDList.Count > 0 Then
            Dim INDPatientCode = CType(INDList(0), DetailInvoicesXpo).InvoiceId.PatientCode
            INDPatientCode = Trim(INDPatientCode)
            Dim INDCareGroup = CType(INDList(0), DetailInvoicesXpo).InvoiceId.CareGroupId.Id
            INDDocumentType = CType(INDList(0), DetailInvoicesXpo).InvoiceId.DocumentType
            INDAdmissionNumber = CType(INDList(0), DetailInvoicesXpo).InvoiceId.AdmissionNumber

            'Se obtiene el Grupo de Atención del Paciente, la Fecha de Nacimiento, el Tipo de Cobertura en Salud, el Sexo, el Nombre y su Código
            Dim INDList2 = XpoServiceEx.Instance(IndigoSessionValues.HisContainer).CrystalService.GetCollection(Of PatientXpo)(Nothing, "IPCODPACI = '" & INDPatientCode & "'")
            If INDList2.Count > 0 Then
                XrTableCell9.Text = CType(INDList2(0), PatientXpo).IPCODPACI
                XrTableCell18.Text = CType(INDList2(0), PatientXpo).IPNOMCOMP

                Dim INDEdadPac As Date = CType(INDList2(0), PatientXpo).IPFECNACI
                XrTableCell10.Text = CalcularEdad(INDEdadPac)

                Dim INDNivCod = CType(INDList2(0), PatientXpo).NIVCODIGO

                'Se obtiene el nivel
                Dim INDList3 = XpoServiceEx.Instance(IndigoSessionValues.HisContainer).CrystalService.GetCollection(Of LevelsXpo)(Nothing, "NIVCODIGO = '" & INDNivCod & "'")
                If INDList3.Count > 0 Then
                    XrTableCell16.Text = CType(INDList3(0), LevelsXpo).NIVCODIGO.Trim
                End If
            End If

            'Se obtiene la Fecha de Ingreso del Paciente
            Dim INDList4 = XpoServiceEx.Instance(IndigoSessionValues.HisContainer).CrystalService.GetCollection(Of AdmissionXpo)(Nothing, "IPCODPACI = '" & INDPatientCode & "' And NUMINGRES = '" & INDAdmissionNumber & "'")
            If INDList4.Count > 0 Then
                XrTableCell14.Text = CType(INDList4(0), AdmissionXpo).IFECHAING
            End If

            'Se obtiene el Contrato y la Entidad
            Dim INDList5 = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).CrystalService.GetCollection(Of CareGroupReportXpo)(Nothing, "Id = " & INDCareGroup)
            If INDList5.Count > 0 Then
                If CType(INDList5(0), CareGroupReportXpo).ContractId IsNot Nothing Then
                    Dim INDContractId = CType(INDList5(0), CareGroupReportXpo).ContractId.Id
                    Dim INDList6 = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).CrystalService.GetCollection(Of ContractsXpo)(Nothing, "Id = " & INDContractId)
                    If INDList6.Count > 0 Then
                        XrTableCell3.Text = CType(INDList6(0), ContractsXpo).Code
                        XrTableCell8.Text = CType(INDList6(0), ContractsXpo).ContractEntityId.Code
                    End If
                End If
            End If

            'Se obtiene el Nit y Name del Tercero
            Dim INDIdThirdParty = CType(INDList(0), DetailInvoicesXpo).InvoiceId.ThirdPartyId.Id
            Dim INDList9 = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).CrystalService.GetCollection(Of ThirdsPartyXpo)(Nothing, "Id = " & INDIdThirdParty)
            If INDList9.Count > 0 Then
                XrTableCell1.Text = CType(INDList9(0), ThirdsPartyXpo).Nit
                XrTableCell5.Text = CType(INDList9(0), ThirdsPartyXpo).Name
            End If

            'Se hace cambio de moneda segun validación
            For Each row In INDList
                If INDList(0).InvoiceId.CurrencyId IsNot Nothing AndAlso (INDList(0).InvoiceId.TRMValue IsNot Nothing) Then
                    row.ServiceOrderDetailId.ThirdPartyDiscount = INDList(0).ServiceOrderDetailId.ThirdPartyDiscount / INDList(0).InvoiceId.TRMValue
                    For Each item In row.ServiceOrderDetailId.Billing_ServiceOrderDetailDistributions
                        item.ThirdPartySalesPrice = item.ThirdPartySalesPrice / INDList(0).InvoiceId.TRMValue
                        item.SubTotalPatientSalesPrice = item.SubTotalPatientSalesPrice / INDList(0).InvoiceId.TRMValue
                    Next
                End If
            Next
            If INDList(0).InvoiceId.CurrencyId IsNot Nothing Then
                Me.CurrencyCell.Text = INDList(0).InvoiceId.CurrencyId.CurrencyName
            Else
                Dim GeneralLedgerCompany = XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer).AccountingService.GetCollection(Of GeneralLedgerCompanySettingsXpo)(Nothing, Nothing).ToList().FirstOrDefault
                Me.CurrencyCell.Text = GeneralLedgerCompany.OfficialCurrency.CurrencyName
            End If

        End If

        Me.DataSource = INDList

    End Sub

    Public Sub CargarImagenes() Implements IReport.CargarImagenes

    End Sub

    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return rptSaleInvoiceReduced.CNameReport
        End Get
    End Property

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    Public Function CalcularEdad(INDDateN As Date) As String
        Dim INDEdadY, INDEdadM, INDEdadD, INDYearR, INDMonthsR, INDDaysR, INDDaysMonthR, INDEdadYN, INDEdadMN, INDEdadDN As Integer

        INDDaysR = Now.Day
        INDMonthsR = Now.Month
        INDDaysMonthR = DateTime.DaysInMonth(Now.Year, Now.Month)
        INDYearR = Now.Year
        INDEdadD = Format(INDDateN, "dd")
        INDEdadM = Format(INDDateN, "MM")
        INDEdadY = Format(INDDateN, "yyyy")

        INDEdadDN = INDDaysR - INDEdadD

        If (INDEdadDN < 0) Then
            INDEdadMN = INDMonthsR - INDEdadM - 1
            INDEdadDN += INDDaysMonthR
        Else
            INDEdadMN = INDMonthsR - INDEdadM
        End If

        If (INDEdadMN < 0) Then
            INDEdadMN += 12
            INDEdadYN = INDYearR - INDEdadY - 1
        Else
            INDEdadYN = INDYearR - INDEdadY
        End If

        Return INDEdadYN & " Años / " & INDEdadMN & " Meses / " & INDEdadDN & " Días"
    End Function

    Private Sub rptSaleInvoiceReduced_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        Dim CurrencyAbbreviation As String = IndigoSessionValues.CurrencyISO4217

        If Me.DataSource IsNot Nothing AndAlso Not String.IsNullOrEmpty(Me.DataSource(0).InvoiceId.CurrencyId.Abbreviation) Then
            Dim _culture As CultureInfo = CultureInfo.CurrentCulture.Clone()
            CurrencyAbbreviation = Me.DataSource?(0).InvoiceId.CurrencyId.Abbreviation
            _culture.NumberFormat = CurrencyAbbreviation.GetNumberFormat
            ApplyLocalization(_culture)
        End If

        Dim _integerPart As Integer = Int(Convert.ToDecimal(GetCurrentColumnValue("INDTotalInvoice")))
        Dim _decimalPart As Integer = Strings.Right(Format(Convert.ToDecimal(GetCurrentColumnValue("INDTotalInvoice")) - _integerPart, "0.00"), 2)
        Me.XrTableCell45.Text = String.Format("{0} {1}{2}",
                                                  Utils.Num2Text(_integerPart).ToString, IndigoSessionValues.CurrencyISO4217,
                                                    If(_decimalPart > 0, $", CON {Utils.Num2Text(_decimalPart).ToString} {Utils.ListDecimalCurrency(CurrencyAbbreviation)}", ""))

        'Dim filtroConsulta As String = "Id = " & ParametrosReporte(0)
        If Me.Parameters.Count > 0 And Me.Parameters(0).Value > 0 Then
            Dim ParametrosFilter As ParameterCollection = Me.Parameters
            ParametrosReporte = New Object() {ParametrosFilter("INDSubPrInvoiceId").Value}
            CargarDataSource()
        End If
    End Sub

    Private Sub XrTable1_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTable1.BeforePrint

        INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        INDLblNitCompany.Text = "Nit:" & IndigoSessionValues.IndigoCompanyNit
        INDUserImp.Text = IndigoSessionValues.UserIndigoName
        Dim table As XRTable = CType(sender, XRTable)
        Dim row As XRTableRow = table.Rows(0)

        If INDDocumentType <> 1 And INDDocumentType <> 2 Then
            If row.Cells("XrTableCell2") IsNot Nothing And row.Cells("XrTableCell3") IsNot Nothing And table.Rows("XrTableRow3") IsNot Nothing Then
                row.Cells.Remove(XrTableCell2)
                row.Cells.Remove(XrTableCell3)
                table.Rows.Remove(XrTableRow3)
            End If
        End If
    End Sub
End Class