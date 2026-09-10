#Region "Librerias Improtadas"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.BillingRepository
Imports Infrastructure.Data.Xpo.CrystalRepository
Imports Domain.Entities
Imports DevExpress.XtraReports.UI
Imports System.Drawing.Printing
#End Region

Public Class rptServiceOrder
    Implements IReport

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Const CNameReport = "Facturacion.FrmOrdersService"

    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Dim filtroConsulta As String = "ServiceOrderId.Id = " & ParametrosReporte(0)

        'Se obtiene el Código del Paciente
        Dim INDList = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).BillingService.GetCollection(Of OrderServiceDetailXpo)(Nothing, filtroConsulta)
        Dim INDPatientCode = CType(INDList(0), OrderServiceDetailXpo).ServiceOrderId.PatientCode

        'Se obtiene el Grupo de Atención del Paciente, la Fecha de Nacimiento, el Sexo, el Nombre y su Código
        Dim INDList2 = XpoServiceEx.Instance(IndigoSessionValues.HisContainer).CrystalService.GetCollection(Of PatientXpo)(Nothing, "IPCODPACI = '" & INDPatientCode & "'")
        Dim INDGENCAREGROUP = CType(INDList2(0), PatientXpo).GENCAREGROUP

        XrTableCell2.Text = CType(INDList2(0), PatientXpo).IPCODPACI & " - " & CType(INDList2(0), PatientXpo).IPNOMCOMP
        Dim INDSexoPac = CType(INDList2(0), PatientXpo).IPSEXOPAC
        If (INDSexoPac = 1) Then
            XrTableCell8.Text = "Masculino"
        Else
            XrTableCell8.Text = "Femenino"
        End If

        Dim INDEdadPac As Date = CType(INDList2(0), PatientXpo).IPFECNACI
        XrTableCell4.Text = CalcularEdad(INDEdadPac)

        'Se obtiene el Código de Cama del Paciente
        Dim INDList3 = XpoServiceEx.Instance(IndigoSessionValues.HisContainer).CrystalService.GetCollection(Of AdmissionXpo)(Nothing, "IPCODPACI = '" & INDPatientCode & "'")
        XrTableCell12.Text = CType(INDList3(0), AdmissionXpo).CODICAMHO

        Dim INDGenCareGroupId = CType(INDList3(0), AdmissionXpo).GENCAREGROUP
        Dim INDList4 = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).BillingService.GetCollection(Of CareGroupReportXpo)(Nothing, "Id = " & INDGenCareGroupId)
        If (INDList4.Count > 0) Then
            XrTableCell14.Text = CType(INDList4(0), CareGroupReportXpo).CodName_GroupCare
        Else
            XrTableCell14.Text = ""
        End If

        Me.DataSource = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).BillingService.GetCollection(Of OrderServiceDetailXpo)(Nothing, filtroConsulta)

    End Sub

    Public Sub CargarImagenes() Implements IReport.CargarImagenes

    End Sub

    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return rptServiceOrder.CNameReport
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

    Private Sub XrTable3_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTable3.BeforePrint
        Dim table As XRTable = CType(sender, XRTable)
        Dim row As XRTableRow = table.Rows(0)

        Dim filtroConsulta As String = "ServiceOrderId.Id = " & ParametrosReporte(0)
        Dim INDList = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).BillingService.GetCollection(Of OrderServiceDetailXpo)(Nothing, filtroConsulta)

        If (CType(INDList(0), OrderServiceDetailXpo).Presentation <> 1) Then
            If row.Cells("XrTableCell34") IsNot Nothing Then
                row.Cells.Remove(XrTableCell34)
                XrTableCell22.WidthF = 86.21
                XrTableCell23.WidthF = 523.05
                XrTableCell27.WidthF = 65.29
                XrTableCell24.WidthF = 83.33
                XrTableCell25.WidthF = 40.62
            End If
        End If
    End Sub

    Private Sub rptServiceOrder_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        INDLblNitCompany.Text = "Nit:" & IndigoSessionValues.IndigoCompanyNit
        INDUserImp.Text = "Usuario Impresión: " & IndigoSessionValues.UserIndigoName
    End Sub
End Class