#Region "Librerias Improtadas"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.InventoryRepository
'Imports Infrastructure.Data.Xpo.SecurityRepository
Imports DevExpress.XtraReports.UI
Imports System.Drawing.Printing

Imports System.Drawing
Imports DevExpress.XtraPrinting.Drawing
Imports Presentation.Base
Imports DevExpress.XtraReports.Parameters
Imports System.Text
Imports Infrastructure.Data.Xpo.SecurityRepository

#End Region

Public Class rptPharmaceuticalDispensingDevolutionReduced
    Implements IReport
    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance
    Dim count As Integer
    Dim filtroConsulta As String
    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Try
            'If ParametrosReporte.Length > 1 Then
            '    filtroConsulta = "DevolutionCode = '" & ParametrosReporte(0) & "'"
            'Else
            '    filtroConsulta = "PharmaceuticalDispensingDevolutionId = " & ParametrosReporte(0)
            'End If
            'Dim ListDetail = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).InventoryService.ListViewPharmaceuticalDispensingDevolutionFilters(filtroConsulta)
            ''count = ListDetail.Count * 81
            'If ListDetail.Count > 0 Then
            '    Dim INDNameUser = CType(ListDetail(0), InventoryPharmaceuticalViewDispensingDevolutionReportXpo).CreationUser.Trim()

            '    Dim INDListUser = XpoServiceEx.Instance(IndigoSessionValues.SecurityContainer).SecurityService.GetCollection(Of UserXpo)(Nothing, "UserCode = '" & INDNameUser & "'")

            '    If INDListUser IsNot Nothing Then
            '        Dim INDCodName = CType(INDListUser(0), UserXpo).CodeName.Trim
            '        Me.INDUserCreate.Text = "Usuario Creación: " & INDCodName
            '    End If
            'End If
            'Me.DataSource = ListDetail
            Dim devolution = CType(ParametrosReporte(0), Domain.Entities.PharmaceuticalDispensingDevolution)
            XrTableCell7.Text = devolution.CodePatient
            XrTableCell5.Text = devolution.NamePatient
            XrTableCell2.Text = devolution.DocumentDate
            XrTableCell4.Text = devolution.CodeNameWarehouse
            XrTableCell19.Text = devolution.AdmissionNumber
            XrTableCell17.Text = devolution.Code
            XrTableCell15.Text = devolution.Observation
            INDUserImp.Text = "Usuario Impresión: " & devolution.CreationUser
            INDUserCreate.Text = "Usuario Creación: " & devolution.CodeNameUser

            Me.DataSource = devolution.PharmaceuticalDispensingDevolutionDetail.ToList()
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


    Private Sub rptPharmaceuticalDispensingDevolutionReduced_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        'Se condiciona con el parametro 1 por que el parametro 0 es el de la agrupación
        If Me.Parameters.Count > 0 And Me.Parameters(1).Value > 0 Then
            Dim ParametrosFilter As ParameterCollection = Me.Parameters
            ParametrosReporte = New Object() {ParametrosFilter("INDSubIdDisDevolution").Value}
            CargarDataSource()
        End If

        INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        INDLblNitCompany.Text = "Nit:" & IndigoSessionValues.IndigoCompanyNit
        'INDUserImp.Text = "Usuario Impresión: " & IndigoSessionValues.UserIndigoName

        Dim reporte As XtraReport = (CType(sender, XtraReport))
        reporte.Watermark.PageRange = "1"

        'Dim page = count + 340
        'Dim reporte As XtraReport = (CType(sender, XtraReport))
        'reporte.PageHeight = page
    End Sub
End Class