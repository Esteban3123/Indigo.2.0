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

Public Class rptSaleLisInvoice
    Implements IReport
    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Const CNameReport = "Facturacion.CtrFolio"

    Dim dictionarySum As New Dictionary(Of Integer, Integer)

    'Dim totalSum As Decimal = 0

    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Try
            Dim filtroConsulta As String = Nothing

            If ParametrosReporte(10) = 3 Then
                filtroConsulta = "DocumentType = 5"
            ElseIf ParametrosReporte(10) = 4 Then
                filtroConsulta = "DocumentType = 3"
            ElseIf ParametrosReporte(10) = 6 Then
                filtroConsulta = "DocumentType = 7"
            Else
                filtroConsulta = "DocumentType in(1,2)"
            End If

            If ParametrosReporte(0) IsNot Nothing And ParametrosReporte(1) IsNot Nothing Then
                filtroConsulta &= " AND InvoiceDate >= #" & Format(ParametrosReporte(0), "yyyy-MM-dd HH:mm:ss") & "# AND InvoiceDate <= #" & Format(ParametrosReporte(1), "yyyy-MM-dd HH:mm:ss") & "# "
            End If

            If ParametrosReporte(3) IsNot Nothing AndAlso ParametrosReporte(3) <> "" Then
                If ParametrosReporte.Count >= 14 AndAlso ParametrosReporte(14) IsNot Nothing AndAlso ParametrosReporte(14) <> "" Then
                    filtroConsulta &= " AND InvoiceNumber >= '" & ParametrosReporte(3) & "' AND InvoiceNumber <= '" & ParametrosReporte(14) & "'"
                Else
                    filtroConsulta &= " AND InvoiceNumber = '" & ParametrosReporte(3) & "'"
                End If
            End If

            If ParametrosReporte(4) IsNot Nothing Then
                filtroConsulta &= " AND HealthAdministratorId = " & ParametrosReporte(4)
            End If

            If ParametrosReporte(5) IsNot Nothing Then
                filtroConsulta &= " AND PatientCode = '" & Trim(ParametrosReporte(5)) & "'"
            End If

            If ParametrosReporte(6) IsNot Nothing Then
                filtroConsulta &= " AND CareGroupId = " & ParametrosReporte(6)
            End If

            If ParametrosReporte(7) IsNot Nothing Then
                filtroConsulta &= " AND InvoiceCategoryId = " & ParametrosReporte(7)
            End If

            If ParametrosReporte(8) IsNot Nothing Then
                filtroConsulta &= " AND ThirdPartyId = " & ParametrosReporte(8)
            End If

            If ParametrosReporte(9) IsNot Nothing Then
                If ParametrosReporte(9).ToString().Contains(",") Then
                    filtroConsulta &= $" AND AdmissionNumber In ({ParametrosReporte(9)})"
                Else
                    filtroConsulta &= " AND AdmissionNumber = '" & ParametrosReporte(9) & "'"
                End If
            End If

            If ParametrosReporte(11) IsNot Nothing Then
                filtroConsulta &= " AND InvoicedUser = '" & ParametrosReporte(11) & "'"
            End If

            If ParametrosReporte(12) <> 3 Then
                filtroConsulta &= " AND Status = " & ParametrosReporte(12)
            End If

            If ParametrosReporte(13) IsNot Nothing Then
                filtroConsulta &= " AND RadicateInvoiceIdC = " & ParametrosReporte(13)
            End If

            Dim INDList = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).BillingService.GetCollection(Of BillingViewRadicatedInvoicReportXpo)(Nothing, filtroConsulta)
            
            Me.DataSource = INDList

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
End Class