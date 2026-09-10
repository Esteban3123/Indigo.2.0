#Region "Librerias Improtadas"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.AccountingRepository
Imports Infrastructure.Data.Xpo.BillingRepository
Imports Infrastructure.Data.Xpo.SecurityRepository
Imports Infrastructure.Data.Xpo.CrystalRepository
Imports Domain.Entities
Imports DevExpress.XtraReports.UI
Imports System.Drawing.Printing
Imports System.Drawing
Imports DevExpress.XtraPrinting.Drawing
Imports Presentation.Base
Imports DevExpress.XtraReports.Parameters

#End Region

Public Class rptSlipOutDetailed
    Implements IReport
    Implements IReportAsync

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Private INDUser As Object


    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Try


            Dim filtroConsulta As String = "DateRequest >= #" & Format(ParametrosReporte(0), "yyyy-MM-dd HH:mm:ss") & "# AND DateRequest <= #" & Format(ParametrosReporte(1), "yyyy-MM-dd HH:mm:ss") & "# "

            'Se filtra por Entidad
            If ParametrosReporte(2) <> "" Then
                'filtroConsulta &= " AND HealthAdministratorId = " & ParametrosReporte(4)
                filtroConsulta &= " AND EntityCode = '" & ParametrosReporte(2) & "'"
            End If

            'Se filtra por Paciente
            If ParametrosReporte(3) <> "" Then
                'filtroConsulta &= " AND PatientCode = '" & Trim(ParametrosReporte(5)) & "'"
                filtroConsulta &= " AND PatientCode = '" & Trim(ParametrosReporte(3)) & "'"
            End If

            'Se filtra por Numero de ingreso
            If ParametrosReporte(4) <> "" Then
                'filtroConsulta &= " AND AdmissionNumber = '" & ParametrosReporte(9) & "'"
                filtroConsulta &= " AND AdmissionCode = '" & ParametrosReporte(4) & "'"
            End If

            'Se filtra por Usuario
            If ParametrosReporte(5) <> "" Then
                'filtroConsulta &= " AND InvoicedUser = '" & ParametrosReporte(11) & "'"
                filtroConsulta &= " AND UserCreate = '" & ParametrosReporte(5) & "'"
            End If

            Dim INDlist = XpoServiceEx.Instance(IndigoSessionValues.HisContainer).CrystalService.ListByCriteriaReport(filtroConsulta)

            'Dim INDlist = XpoServiceEx.Instance(IndigoSessionValues.HisContainer).CrystalService.ListAdmissionsToReportSlipOutById(ParametrosReporte(0))
            If INDlist.Count > 0 Then
                Dim INDCodeUser = CType(INDlist(0), ViewAdmissionsToReportSlipOutReport).UserCreate.Trim()
                INDUser = XpoServiceEx.Instance(IndigoSessionValues.SecurityContainer).SecurityService.GetCollection(Of Infrastructure.Data.Xpo.SecurityRepository.UserXpo)(Nothing, "UserCode = '" & INDCodeUser & "'")
            End If            
            Me.DataSource = INDlist
        Catch ex As Exception
            MessageIndigo.Show(GetExceptionDetails(ex), MessageType.Errores, Me.Text, Botones.Aceptar, "")
        End Try
    End Sub

    Public Function CargarDataSourceAsync() As Task Implements IReportAsync.CargarDataSourceAsync
        Return Task.Factory.StartNew(AddressOf CargarDataSource)
    End Function

    Public Function GetExceptionDetails(exception As Exception) As String
        Dim properties = exception.[GetType]().GetProperties()
        Dim fields = properties.[Select](Function([property]) New With { _
            Key .Name = [property].Name, _
            Key .Value = [property].GetValue(exception, Nothing) _
        }).[Select](Function(x) [String].Format("{0} : {1}", x.Name, If(x.Value IsNot Nothing, x.Value.ToString(), [String].Empty)))
        Return [String].Join(vbLf, fields)
    End Function

    Public Sub CargarImagenes() Implements IReport.CargarImagenes

    End Sub

    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return ""
        End Get
    End Property


    Public Function CalcularEdad(INDDateN As Date) As Integer
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

        Return INDEdadYN
    End Function

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    Private Sub rptSlipOut_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        INDUserImp.Text = "Usuario Impresión: " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName
        If INDUser.count() > 0 Then
            INDUserCreate.Text = INDUser(0).CodeName
        End If
        INDLblNitCompany.Text = "Nit:" & IndigoSessionValues.IndigoCompanyNit
        XrTableCell2.Text = Date.Now
        INDPrCompany.Value = IndigoSessionValues.IndigoCompanyName
    End Sub
End Class