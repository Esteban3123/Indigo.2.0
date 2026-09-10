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

Public Class rptDashboardPharmacyReduced
    Implements IReport
    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance
    Dim count As Integer
    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Try
            Dim filtroConsulta As New List(Of String)
            For Each item As Object In ParametrosReporte(0)
                filtroConsulta.Add("(CodigoPaciente = '" & item.CodigoPaciente & "' AND ConsecutivoFarmacia = '" & item.ConsecutivoFarmacia & "' AND Ingreso = '" & item.Ingreso & "')")
            Next

            Dim filter As String = String.Join(" OR ", filtroConsulta)
            If ParametrosReporte(2) = False Then
                filter = String.Format("CantidadPendiente > 0 AND ({0})", filter)
            End If

            Dim ListDetail = XpoServiceEx.Instance(IndigoSessionValues.HisContainer).InventoryService.ListViewDashboardPharmacyFilters(filter)

            count = ListDetail.Count * 70

            Me.DataSource = ListDetail
        Catch ex As Exception
            MessageIndigo.Show(GetExceptionDetails(ex), MessageType.Errores, Me.Text, Botones.Aceptar, "")
        End Try
    End Sub

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

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte


    Private Sub rptDashboardPharmacyReduced_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        Dim table As XRTable = CType(XrTable10, XRTable)
        Dim table2 As XRTable = CType(XrTable1, XRTable)
        Dim table3 As XRTable = CType(XrTable2, XRTable)

        'altura del reporte calculado
        Dim page = count + 360
        Dim reporte As XtraReport = (CType(sender, XtraReport))
        reporte.PageHeight = page

        If ParametrosReporte(1) = 1 Then
            INDGroup.Value = 1
            table.Rows.Remove(XrTableRow21)
            table2.Rows.Remove(XrTableRow1)
            table3.Rows.Remove(XrTableRow2)
            XrTable11.Rows.Remove(XrTableRow8)
        Else
            INDGroup.Value = 2
        End If

        INDRequests.Value = ParametrosReporte(2)
        INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        INDLblNitCompany.Text = "Nit:" & IndigoSessionValues.IndigoCompanyNit
        INDUserImp.Text = "Usuario Impresión: " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName
        INDUserCreate.Text = "Usuario Creación: " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName
    End Sub
End Class