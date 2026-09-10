#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.BudgetRepository
Imports Presentation.Base
Imports Presentation.CloudAgent
#End Region

Public Class rptReportExpenseRecordBook
    Implements IReport
    Implements IReportAsync

#Region "Properties"

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Dim criterias As Dictionary(Of String, String)

    Dim dtReportExpenseRecordBook As DataTable

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return ""
        End Get
    End Property

#End Region

#Region "Load Data"

    Public Sub CargarDataSource() Implements IReport.CargarDataSource

    End Sub

    Public Async Function CargarDataSourceAsync() As Task Implements IReportAsync.CargarDataSourceAsync
        Try
            criterias = ParametrosReporte(0)

            Dim ds As DataSet = Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.GetReportExpenseRecordBookAsync(Me.criterias, Me.IndigoSessionValues)
            If ds IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
                dtReportExpenseRecordBook = ds.Tables("ReportExpenseRecordBook")
                Me.DataSource = dtReportExpenseRecordBook
                Me.DataMember = "ReportExpenseRecordBook"

                Dim listEntity = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).BudgetService.GetCollection(Of BudgetBudgetaryEntityReportXpo)(Nothing, "Id = " & criterias("BudgetaryEntityId"))
                If listEntity IsNot Nothing Then
                    Dim entity = listEntity.FirstOrDefault()

                    INDTcSection.Text = entity.Section
                    INDTcUnit.Text = entity.Unit
                    INDTcRegion.Text = entity.Region
                    INDTcName.Text = entity.Name
                    INDTcYear.Text = CDate(criterias("CutoffDate")).Year
                End If

                Dim listBudget = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).BudgetService.GetCollection(Of BudgetReportXpo)(Nothing, "Id = " & criterias("BudgetId"))
                If listBudget IsNot Nothing Then
                    Dim budget = listBudget.FirstOrDefault()

                    INDTcCategory.Text = String.Format("{0} {1}", If(criterias("CodeToUse") = 1, budget.CategoryId.Code, budget.CategoryId.AlternativeCode), budget.CategoryId.Name)
                    INDTcFinancialSource.Text = String.Format("{0} {1}", budget.CategoryId.FinancialSourceId.Code, budget.CategoryId.FinancialSourceId.Name)
                    INDTcRevenueType.Text = String.Format("{0} {1}", budget.RevenueTypeId.Code, budget.RevenueTypeId.Name)
                End If
            Else
                Me.DataSource = Nothing
            End If
        Catch ex As Exception
            MessageIndigo.Show(GetExceptionDetails(ex), MessageType.Errores, Me.Text, Botones.Aceptar, "")
            Me.DataSource = Nothing
        End Try
    End Function

#End Region

#Region "Methods"

    Private Sub rptReportExpenseRecordBook_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        Me.INDPrmCodeToUse.Value = Me.criterias("CodeToUse")

        INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        INDLblNitCompany.Text = "Nit:" & IndigoSessionValues.IndigoCompanyNit
        Me.INDUserImp.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName
    End Sub

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