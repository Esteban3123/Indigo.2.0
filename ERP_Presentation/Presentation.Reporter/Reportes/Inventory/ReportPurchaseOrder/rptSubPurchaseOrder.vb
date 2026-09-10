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

#End Region

Public Class rptSubPurchaseOrder
    Implements IReport
    Implements IReportAsync
    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Try
            Dim filter As String = Nothing

            'filtro por fechas
            If ParametrosReporte(0) IsNot Nothing And ParametrosReporte(1) IsNot Nothing Then
                filter = "GetDate(DocumentDate) >= #" & Format(ParametrosReporte(0), "yyyy-MM-dd") & "# AND GetDate(DocumentDate) <= #" & Format(ParametrosReporte(1), "yyyy-MM-dd") & "#"
            End If

            'si filtra por Estado
            If ParametrosReporte(5) <> 4 Then
                filter &= If(String.IsNullOrEmpty(filter), "", " AND ") & String.Format("Status IN ({0})", ParametrosReporte(5))
            End If

            'si filtra por Almacen
            If Not String.IsNullOrEmpty(ParametrosReporte(6)) Then
                filter &= If(String.IsNullOrEmpty(filter), "", " AND ") & String.Format(" WarehouseId.Id IN ({0})", ParametrosReporte(6))
            End If

            'si filtra por proveedor
            If Not String.IsNullOrEmpty(ParametrosReporte(3)) Then
                filter &= If(String.IsNullOrEmpty(filter), "", " AND ") & String.Format("SupplierId.IdThirdParty.Id IN ({0})", ParametrosReporte(3))
            End If

            'si filtra por documento
            If Not String.IsNullOrEmpty(ParametrosReporte(2)) Then
                filter &= If(String.IsNullOrEmpty(filter), "", " AND ") & String.Format("Id In ({0})", ParametrosReporte(2))
            End If

            Me.DataSource = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).TreasuryService.GetCollection(Of InventoryPurchaseOrderReportXpo)(Nothing, filter)
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

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte
End Class