#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo.InventoryRepository
Imports Infrastructure.Data.Xpo
Imports Presentation.Base

#End Region

Public Class rptKardexVsPhysicalByGroup
    Implements IReport
    Implements IReportAsync

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Try
            Dim filtroConsulta As String = Nothing

            ' si filtra por Producto
            'If ParametrosReporte(2) IsNot Nothing And ParametrosReporte(3) IsNot Nothing Then
            If ParametrosReporte(2) <> "" And ParametrosReporte(3) <> "" Then
                If filtroConsulta Is Nothing Then
                    filtroConsulta = "ProductCode >= '" & ParametrosReporte(2) & "' AND ProductCode <= '" & ParametrosReporte(3) & "'"
                Else
                    filtroConsulta &= " AND ProductCode >= '" & ParametrosReporte(2) & "' AND ProductCode <= '" & ParametrosReporte(3) & "'"
                End If
            End If

            'si filtra por Almacen
            If ParametrosReporte(4) IsNot Nothing Then
                If filtroConsulta Is Nothing Then
                    filtroConsulta &= " WarehouseId IN (" & String.Join(",", CType(ParametrosReporte(4), List(Of Integer)).ToArray()) & ")"
                Else
                    filtroConsulta &= " AND WarehouseId IN (" & String.Join(",", CType(ParametrosReporte(4), List(Of Integer)).ToArray()) & ")"
                End If
            End If

            Me.DataSource = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).TreasuryService.GetCollection(Of InventoryViewReportKardexVsPhysicalReportXpo)(Nothing, filtroConsulta)
        Catch ex As Exception
            MessageIndigo.Show(GetExceptionDetails(ex), MessageType.Errores, Me.Text, Botones.Aceptar, "")
        End Try
    End Sub

    Public Function CargarDataSourceAsync() As Task Implements IReportAsync.CargarDataSourceAsync
        Return Task.Factory.StartNew(AddressOf CargarDataSource)
    End Function

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
            Return Nothing
        End Get
    End Property

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    Private Sub rptKardexVsPhysical_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        Me.INDLblNameCompany.Text = IndigoSessionValues.IndigoCompanyName
        Me.INDLblNitCompany.Text = "Nit : " & IndigoSessionValues.IndigoCompanyNit
        Me.INDUserImp.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName
    End Sub

End Class