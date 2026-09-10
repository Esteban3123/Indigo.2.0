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
#End Region

Public Class rptSaleInvoiceReducedByDispensing
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

                Dim statusName As String = ""
                If CType(INDList(0), DetailInvoicesXpo).InvoiceId.Status = 1 Then
                    statusName = "Facturado"
                Else
                    statusName = "Anulado"
                End If
                XrTableCell3.Text = statusName
            End If

            'Se obtiene el Nit y Name del Tercero
            Dim INDIdThirdParty = CType(INDList(0), DetailInvoicesXpo).InvoiceId.ThirdPartyId.Id
            Dim INDList9 = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).CrystalService.GetCollection(Of ThirdsPartyXpo)(Nothing, "Id = " & INDIdThirdParty)
            If INDList9.Count > 0 Then
                XrTableCell2.Text = CType(INDList9(0), ThirdsPartyXpo).Nit
                XrTableCell5.Text = CType(INDList9(0), ThirdsPartyXpo).Name
            End If

            'Se obtiene la autorización
            Dim BillingAuthorizationId As Integer = CType(INDList(0), DetailInvoicesXpo).InvoiceId.BillingAuthorizationId
            If BillingAuthorizationId <> Nothing AndAlso BillingAuthorizationId > 0 Then
                Dim billingAuthorizationXpo As BillingAuthorizationXpo = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).CrystalService.GetCollection(Of BillingAuthorizationXpo)(Nothing, "Id = " & BillingAuthorizationId).FirstOrDefault()
                If billingAuthorizationXpo IsNot Nothing Then
                    XrLabel3.Text = "AGENTE RETENEDOR IMPUESTO SOBRE LAS VENTAS AL REG COMÚN / RESOLUCIÓN " + billingAuthorizationXpo.ResolutionNumber + " DEL " + billingAuthorizationXpo.ResolutionDate.ToString("MM/dd/yyyy") + " HABILI-AUTORIZA " + billingAuthorizationXpo.InvoicePrefix + " " + billingAuthorizationXpo.InitialInvoice.ToString() + " AL " + billingAuthorizationXpo.InvoicePrefix + " " + billingAuthorizationXpo.FinalInvoice.ToString() + " - FACTURACIÓN POR COMPUTADOR - EFECTUAR RETENCIÓN DEL 2% SERVICIOS DE SA"
                End If
            End If
        End If

        Me.DataSource = INDList

    End Sub

    Public Sub CargarImagenes() Implements IReport.CargarImagenes

    End Sub

    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return rptSaleInvoiceReducedByDispensing.CNameReport
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
        Me.XrTableCell45.Text = Utils.Num2Text(Convert.ToDouble(GetCurrentColumnValue("INDTotalInvoice"))).ToString & " PESOS M/Cte."

        'Dim filtroConsulta As String = "Id = " & ParametrosReporte(0)
        If Me.Parameters.Count > 0 And Me.Parameters(0).Value > 0 Then
            Dim ParametrosFilter As ParameterCollection = Me.Parameters
            ParametrosReporte = New Object() {ParametrosFilter("INDSubPrInvoiceId").Value}
            CargarDataSource()
        End If
    End Sub

    Private Sub XrTable2_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTable2.BeforePrint
        INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        INDLblNitCompany.Text = "Nit:" & IndigoSessionValues.IndigoCompanyNit
        INDlblAdressTel.Text = "Dirección: " + IndigoSessionValues.IndigoCompanyAddress + "  " + "Tel: " + IndigoSessionValues.IndigoCompanyPhoneNumber.ToString()
        INDUserImp.Text = IndigoSessionValues.UserIndigoName
        INDDateImp.Text = Date.Now()
    End Sub

    Private Sub XrTable1_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs)

    End Sub
End Class