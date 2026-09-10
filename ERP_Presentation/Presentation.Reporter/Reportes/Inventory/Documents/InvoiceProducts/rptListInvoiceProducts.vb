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

#End Region

Public Class rptListInvoiceProducts
    Implements IReport

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Try
            Dim filtroConsulta As String = Nothing

            filtroConsulta = "DocumentType = 7"

            If ParametrosReporte(0) IsNot Nothing And ParametrosReporte(1) IsNot Nothing Then
                filtroConsulta &= "AND InvoiceDate >= #" & Format(ParametrosReporte(0), "yyyy-MM-dd HH:mm:ss") & "# AND InvoiceDate <= #" & Format(ParametrosReporte(1), "yyyy-MM-dd HH:mm:ss") & "# "
            End If

            If ParametrosReporte(2) IsNot Nothing AndAlso ParametrosReporte(2) <> "" AndAlso ParametrosReporte(3) IsNot Nothing AndAlso ParametrosReporte(3) <> "" Then
                filtroConsulta &= " AND InvoiceNumber >= '" & ParametrosReporte(2) & "' AND InvoiceNumber <= '" & ParametrosReporte(3) & "'"
            End If

            If ParametrosReporte(4) IsNot Nothing Then
                filtroConsulta &= " AND ThirdPartyId = " & ParametrosReporte(4)
            End If

            If ParametrosReporte(5) IsNot Nothing Then
                'Sucursal
                filtroConsulta &= " AND BranchOfficeId = " & ParametrosReporte(5)
            End If

            If ParametrosReporte(6) IsNot Nothing Then
                filtroConsulta &= " AND InvoicedUser = '" & ParametrosReporte(6) & "'"
            End If

            If ParametrosReporte(7) <> 3 Then
                filtroConsulta &= " AND Status = " & ParametrosReporte(7)
            End If

            Dim INDList = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).BillingService.GetCollection(Of BillingViewRadicatedInvoicReportXpo)(Nothing, filtroConsulta)

            Me.DataSource = INDList

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

End Class