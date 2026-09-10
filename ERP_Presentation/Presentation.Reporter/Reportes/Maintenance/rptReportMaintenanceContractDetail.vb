#Region "Librerias Importadas"
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.MaintenanceRepository
Imports Domain.Entities
#End Region

Public Class rptReportMaintenanceContractDetail
    Implements IReport
    Implements IReportAsync

#Region "Properties"
    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

#End Region

#Region "LoadData"
    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Try
            Dim filtroConsulta As String = Nothing
            filtroConsulta = String.Format("MaintenanceContractId.InitialDate >= #" & Format(ParametrosReporte(0), "yyyy-MM-dd") & "# AND MaintenanceContractId.EndDate <= #" & Format(ParametrosReporte(1), "yyyy-MM-dd") & "#")
            'Filtrar por placa
            If Not String.IsNullOrEmpty(ParametrosReporte(2)) Then
                filtroConsulta &= String.Format(" AND PhysicalAssetId.Id IN ({0})", ParametrosReporte(2))
            End If
            'Filtrar por articulo
            If Not String.IsNullOrEmpty(ParametrosReporte(3)) Then
                filtroConsulta &= String.Format(" AND PhysicalAssetId.ItemId.Id IN ({0})", ParametrosReporte(3))
            End If
            'Filtrar por proveedor
            If Not String.IsNullOrEmpty(ParametrosReporte(4)) Then
                filtroConsulta &= String.Format(" AND MaintenanceContractId.SupplierId.Id IN ({0})", ParametrosReporte(4))
            End If
            'Filtrar por contrato
            If Not String.IsNullOrEmpty(ParametrosReporte(5)) Then
                filtroConsulta &= String.Format(" AND MaintenanceContractId.Id IN ({0})", ParametrosReporte(5))
            End If
            'Filtrar por catalogo de articulo
            If Not String.IsNullOrEmpty(ParametrosReporte(6)) Then
                filtroConsulta &= String.Format(" AND PhysicalAssetId.ItemId.ItemCatalogId.Id IN ({0})", ParametrosReporte(6))
            End If
            'Filtrar por estado
            If Not String.IsNullOrEmpty(ParametrosReporte(8)) And ParametrosReporte(8) <> 4 Then
                filtroConsulta &= " AND MaintenanceContractId.Status = " & ParametrosReporte(8)
            End If
            Me.DataSource = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).MaintenanceService.GetCollection(Of MaintenanceContractDetailXpo)(Nothing, filtroConsulta)
        Catch ex As Exception
            MessageIndigo.Show(GetExceptionDetails(ex), MessageType.Errores, Me.Text, Botones.Aceptar, "")
        End Try
    End Sub


    Public Function CargarDataSourceAsync() As Task Implements IReportAsync.CargarDataSourceAsync
        Return Task.Factory.StartNew(AddressOf CargarDataSource)
    End Function
#End Region

#Region "Methods"
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

    Private Sub rptReportMaintenanceContractDetailt_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        INDLblNitCompany.Text = "Nit:" & IndigoSessionValues.IndigoCompanyNit
        Me.INDLblUserPrint.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName
        If ParametrosReporte(0) IsNot Nothing And ParametrosReporte(1) IsNot Nothing Then
            Me.INDLblDate.Text = "Del " & CDate(Me.ParametrosReporte(0)).ToString("dd De MMMM Del yyyy") & " al " & CDate(Me.ParametrosReporte(1)).ToString("dd De MMMM Del yyyy")
        End If

    End Sub
#End Region

End Class