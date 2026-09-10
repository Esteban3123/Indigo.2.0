#Region "Librerias Importadas"
Imports DevExpress.Data.Helpers
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo.PortfolioRepository
Imports Infrastructure.Data.Xpo
Imports Presentation.Base

#End Region

Public Class rptSubReportDocumentNotePortfolio
    Implements IReport
    Implements IReportAsync

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Try
            Dim filtroConsulta As String = "GetDate(NoteDate) >= #" & Format(ParametrosReporte(0), "yyyy-MM-dd") & "# AND GetDate(NoteDate) <= #" & Format(ParametrosReporte(1), "yyyy-MM-dd") & "#"

            'filtro por terceros
            If ParametrosReporte(4) IsNot Nothing And ParametrosReporte(5) IsNot Nothing Then
                filtroConsulta &= "AND CustomerId.ThirdPartyId.Nit >= '" & ParametrosReporte(4) & "' AND CustomerId.ThirdPartyId.Nit <= '" & ParametrosReporte(5) & "'"
            End If
            ' si filtra por Notas
            If ParametrosReporte(6) IsNot Nothing And ParametrosReporte(7) IsNot Nothing Then
                filtroConsulta &= "AND Code >= '" & ParametrosReporte(6) & "' AND Code <= '" & ParametrosReporte(7) & "'"
            End If
            ' si filtra por Concepto de Notas
            If ParametrosReporte(8) IsNot Nothing And ParametrosReporte(9) IsNot Nothing Then
                filtroConsulta &= "AND Portfolio_PortfolioNoteDetails[PortfolioNoteConceptId.Code >= '" & ParametrosReporte(8) & "' AND PortfolioNoteConceptId.Code <= '" & ParametrosReporte(9) & "']"
            End If
            'si filtra por estado
            If ParametrosReporte(2) <> 4 Then
                filtroConsulta &= " AND Status = " & ParametrosReporte(2)
            End If
            'Si filtra por naturaleza
            If ParametrosReporte(3) <> 3 Then
                filtroConsulta &= " AND Nature = " & ParametrosReporte(3)
            End If

            Me.DataSource = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).PaymentsService.GetCollection(Of PortfolioNoteReportXpo)(Nothing, filtroConsulta)
        Catch ex As Exception
            MessageIndigo.Show(GetExceptionDetails(ex), MessageType.Errores, Me.Text, Botones.Aceptar, "")
        End Try
    End Sub

    ''' <summary>
    ''' Carga Asincrono para no bloquear la interfaz de usuario
    ''' </summary>
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
            Return ""
        End Get
    End Property

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte
End Class