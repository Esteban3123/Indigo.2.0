#Region "Librerias Improtadas"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.InventoryRepository
Imports Infrastructure.Data.Xpo.SecurityRepository
Imports Infrastructure.Data.Xpo.CrystalRepository
Imports Domain.Entities
Imports DevExpress.XtraReports.UI
Imports System.Drawing.Printing

Imports System.Drawing
Imports DevExpress.XtraPrinting.Drawing
Imports Presentation.Base
Imports DevExpress.XtraReports.Parameters
Imports System.Text

#End Region

Public Class rptDashboardPharmacyDevolution
    Implements IReport
    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Try
            Dim patientCodes As String = String.Empty
            Dim rowIds As String = String.Empty
            Dim admissionNumbers As String = String.Empty

            Dim position As Integer = 0
            For Each item As Object In ParametrosReporte(0)
                If position = 0 Then
                    patientCodes = "'" & item.CodigoPaciente & "'"
                    rowIds = item.Row
                    admissionNumbers = "'" & item.Ingreso & "'"
                Else
                    patientCodes = patientCodes & ", " & "'" & item.CodigoPaciente & "'"
                    rowIds = rowIds & ", " & item.Row
                    admissionNumbers = admissionNumbers & ", " & "'" & item.Ingreso & "'"
                End If
                position += 1
            Next

            Dim filter As String = "PatientCode in (" & patientCodes & ") and Row in (" & rowIds & ") and AdmissionNumber in (" & admissionNumbers & ")"
            If ParametrosReporte(2) = False Then
                filter = String.Format("PendingQuantity > 0 AND ({0})", filter)
            End If

            Dim ListDetail = XpoServiceEx.Instance(IndigoSessionValues.HisContainer).InventoryService.ListViewDashboardPharmacyDevolutionFilters(filter)
            Me.DataSource = ListDetail
        Catch ex As Exception
            MessageIndigo.Show(GetExceptionDetails(ex), MessageType.Errores, Me.Text, Botones.Aceptar, "")
        End Try
    End Sub

    Public Function GetExceptionDetails(exception As Exception) As String
        Dim properties = exception.[GetType]().GetProperties()
        Dim fields = properties.[Select](Function([property]) New With {
            Key .Name = [property].Name,
            Key .Value = [property].GetValue(exception, Nothing)
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

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    Private Sub rptDashboardPharmacy_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        If ParametrosReporte(1) = 1 Then
            INDGroup.Value = 1
            XrTable10.Visible = False
            XrTable3.Visible = False
        Else
            INDGroup.Value = 2
        End If
        If ParametrosReporte(3) = True Then
            GroupFooter1.PageBreak = DevExpress.XtraReports.UI.PageBreak.AfterBand
        Else
            GroupFooter1.PageBreak = DevExpress.XtraReports.UI.PageBreak.None
        End If

        INDRequests.Value = ParametrosReporte(2)
        INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        INDLblNitCompany.Text = "Nit:" & IndigoSessionValues.IndigoCompanyNit
        INDUserImp.Text = "Usuario Impresión: " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName
        INDUserCreate.Text = IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName
    End Sub
End Class