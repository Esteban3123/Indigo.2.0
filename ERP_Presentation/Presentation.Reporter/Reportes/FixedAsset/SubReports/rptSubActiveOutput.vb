#Region "Librerias Importadas"
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.FixedAssetRepository
Imports Domain.Entities
#End Region

Public Class rptSubActiveOutput
    Implements IReport

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Try
            Dim filtroConsulta As String = Nothing

            'filtro por fechas
            If ParametrosReporte(0) IsNot Nothing And ParametrosReporte(1) IsNot Nothing Then
                filtroConsulta &= "FixedAssetActiveOutputId.DocumentDate >= #" & Format(ParametrosReporte(0), "yyyy-MM-dd") & "# AND FixedAssetActiveOutputId.DocumentDate <= #" & Format(ParametrosReporte(1), "yyyy-MM-dd") & "#"
            End If

            'filtro por Tipo de Activo
            If ParametrosReporte(2) <> 3 Then
                filtroConsulta &= " AND ActiveType = " & ParametrosReporte(2)
            End If

            'filtro por Estado
            If ParametrosReporte(3) <> 4 Then
                filtroConsulta &= " AND FixedAssetActiveOutputId.Status = " & ParametrosReporte(3)
            End If

            'filtro por Documento
            If ParametrosReporte(4) IsNot Nothing And ParametrosReporte(5) IsNot Nothing Then
                filtroConsulta &= " AND FixedAssetActiveOutputId.Code >= '" & ParametrosReporte(4) & "' AND FixedAssetActiveOutputId.Code <= '" & ParametrosReporte(5) & "'"
            End If

            'filtro por Placa
            If ParametrosReporte(6) IsNot Nothing And ParametrosReporte(7) IsNot Nothing Then
                filtroConsulta &= " AND PhysicalAssetId.Plate >= '" & ParametrosReporte(6) & "' AND PhysicalAssetId.Plate <= '" & ParametrosReporte(7) & "'"
            End If

            'filtro por Artículo
            If ParametrosReporte(8) IsNot Nothing And ParametrosReporte(9) IsNot Nothing Then
                filtroConsulta &= " AND PhysicalAssetId.ItemId.Code >= '" & ParametrosReporte(8) & "' AND PhysicalAssetId.ItemId.Code <= '" & ParametrosReporte(9) & "'"
            End If

            Me.DataSource = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).FixedAsset.GetCollection(Of FixedAssetFixedAssetActiveOutputDetailReportXpo)(Nothing, filtroConsulta)
            Me.INDOperatingUnit.Value = ParametrosReporte(10)
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