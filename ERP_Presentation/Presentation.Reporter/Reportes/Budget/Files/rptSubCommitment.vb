#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.BudgetRepository
Imports Presentation.Base

#End Region

Public Class rptSubCommitment
    Implements IReport
    Implements IReportAsync

#Region "Properties"

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Dim criterias As Dictionary(Of String, String)

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return ""
        End Get
    End Property

#End Region

#Region "Load Data"

    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Try
            criterias = ParametrosReporte(0)

            Dim filtroConsulta As String = "GetDate(DocumentDate) >= #" & Format(CDate(Me.criterias("DateStart")), "yyyy-MM-dd") & "# AND GetDate(DocumentDate) <= #" & Format(CDate(Me.criterias("DateEnd")), "yyyy-MM-dd") & "# AND BudgetaryValidityId.Id = " & Me.criterias("BudgetValidityId")

            If Not String.IsNullOrEmpty(Me.criterias("CommitmentCode")) Then
                filtroConsulta &= " AND Code = '" & Me.criterias("CommitmentCode") & "'"
            End If

            If Not String.IsNullOrEmpty(Me.criterias("ThirdParties")) Then
                filtroConsulta &= String.Format(" AND ThirdPartyId.Id IN ({0})", Me.criterias("ThirdParties"))
            End If

            Me.DataSource = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).TreasuryService.GetCollection(Of BudgetCommitmentReportXpo)(Nothing, filtroConsulta)
        Catch ex As Exception
            MessageIndigo.Show(GetExceptionDetails(ex), MessageType.Errores, Me.Text, Botones.Aceptar, "")
            Me.DataSource = Nothing
        End Try
    End Sub

    Public Function CargarDataSourceAsync() As Task Implements IReportAsync.CargarDataSourceAsync
        Return Task.Factory.StartNew(AddressOf CargarDataSource)
    End Function

#End Region

#Region "Methods"

    Public Sub CargarImagenes() Implements IReport.CargarImagenes

    End Sub

    Public Function GetExceptionDetails(exception As Exception) As String
        Dim properties = exception.[GetType]().GetProperties()
        Dim fields = properties.[Select](Function([property]) New With {
            Key .Name = [property].Name,
            Key .Value = [property].GetValue(exception, Nothing)
        }).[Select](Function(x) [String].Format("{0} : {1}", x.Name, If(x.Value IsNot Nothing, x.Value.ToString(), [String].Empty)))
        Return [String].Join(vbLf, fields)
    End Function

#End Region

End Class