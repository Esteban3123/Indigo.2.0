#Region "Librerias Improtadas"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.FixedAssetRepository
Imports Domain.Entities
Imports DevExpress.XtraReports.UI
Imports System.Drawing.Printing
Imports DevExpress.XtraReports.Parameters
Imports Presentation.Base

#End Region

Public Class rptSubFixedAssetRemissionEntranceDocument
    Implements IReport

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Try
            Dim filtroConsulta As String = "RemisionDate >= #" & Format(Me.ParametrosReporte(0), "yyyy-MM-dd") & "# And RemisionDate <= #" & Format(Me.ParametrosReporte(1), "yyyy-MM-dd") & "#"

            'filtro por Estado
            If ParametrosReporte(2) <> 4 Then
                filtroConsulta &= "AND Status = " & ParametrosReporte(2)
            End If

            'filtro por Ingreso
            If ParametrosReporte(3) IsNot Nothing And ParametrosReporte(4) IsNot Nothing Then
                filtroConsulta &= "AND Code >= '" & ParametrosReporte(3) & "' AND Code <= '" & ParametrosReporte(4) & "'"
            End If

            'filtro por Proveedor
            If ParametrosReporte(6) IsNot Nothing And ParametrosReporte(7) IsNot Nothing Then
                filtroConsulta &= "AND SupplierDistributionLineId.IdSupplier.Code >= " & ParametrosReporte(6) & " AND SupplierDistributionLineId.IdSupplier.Code <= " & ParametrosReporte(7)
            End If

            'filtro por tipo de adquisición
            If ParametrosReporte(8) <> 0 Then
                filtroConsulta &= " AND AdquisitionType = " & ParametrosReporte(8)
            End If

            Me.DataSource = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).BillingService.GetCollection(Of FixedAssetFixedAssetRemissionEntranceReportXpo)(Nothing, filtroConsulta)
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