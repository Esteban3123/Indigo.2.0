#Region "Imports"
Imports DevExpress.Xpo
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Presentation.Base
#End Region
Public Class PReportMainAccountsWithOutParameterization

#Region "Variables"
    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance
#End Region

    ''' <summary>
    ''' metodo para Cargar el data source del gridcontrol
    ''' </summary>
    ''' <remarks></remarks>
    Public Function LoadDatasourceMainAccountWithOutParameterization(GetMonthPeriod As Integer, GetYearPeriod As Integer) As XPCollection(Of CostRepository.CostViewMainAccountsWithoutParameterization)
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).CostService.LoadDatasourceMainAccountWithOutParameterization(GetMonthPeriod, GetYearPeriod)
    End Function

End Class
