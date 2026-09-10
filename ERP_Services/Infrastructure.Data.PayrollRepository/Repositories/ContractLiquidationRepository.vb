'***********************************************************************
' Assembly         : Infrastructure.Data.PayrollRepository
' Author           : Jose Luis Rojas
' Created          : 16-01-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Infrastructure.Data.Base
Imports Domain.Payroll.Entities
Imports Domain.Payroll

Public Class ContractLiquidationRepository
    Inherits GenericRepository(Of ContractLiquidation)
    Implements IContractLiquidationRepository

    'Contexto de payroll
    Private _context As IPayrollUnitOfWork

    Public Sub New(ByVal context As IPayrollUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtiene la lista de nominas por contrato
    ''' </summary>
    ''' <param name="contractId">Id del contrato</param>
    ''' <returns>Lista de nominas</returns>
    Public Function GetPaymentsByContractId(contractId As Integer) As List(Of Liquidation) Implements IContractLiquidationRepository.GetPaymentsByContractId
        Dim payedPayrolls = From c In _context.Liquidation
                       Where c.ContractId = contractId And c.RegisterStatus <> ""
                       Select c


        Return payedPayrolls.ToList()

    End Function


    Public Function GetLiquidationsPaidByBaseContractId(baseContractId As Integer) As List(Of Liquidation) Implements IContractLiquidationRepository.GetLiquidationsPaidByBaseContractId

        Dim payedPayrolls = From l In _context.Liquidation
                            Join c In _context.Contract On l.ContractId Equals c.Id
                            Where c.InitialContractNumber = baseContractId And l.RegisterStatus <> ""
                            Select l

        Dim r = payedPayrolls.ToList

        Return r

    End Function

    ''' <summary>
    ''' Funcion que obtiene los contratos liquidados de x empleado
    ''' </summary>
    ''' <param name="employeeId">id empleado</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetContractLiquidationByEmployee(employeeId As Integer) As List(Of ContractLiquidation) Implements IContractLiquidationRepository.GetContractLiquidationByEmployee
        Dim contractLiquidation = From e In _context.ContractLiquidation
                                  Where e.EmployeeId = employeeId
                                  Select e
        Return contractLiquidation.ToList()
    End Function

    ''' <summary>
    ''' Función que obtiene una Lista de Liquidaciones por Mes y Año
    ''' </summary>
    ''' <param name="Month">Mes</param>
    ''' <param name="Year">Año</param>
    ''' <returns>Lista de Liquidaciones de Contrato</returns>
    ''' <remarks></remarks>
    Public Function GetContractLiquidationByMonthAndYear(Month As Integer, Year As Integer) As List(Of ContractLiquidation) Implements IContractLiquidationRepository.GetContractLiquidationByMonthAndYear
        Dim contractLiquidation = From e In _context.ContractLiquidation.Include("Contract").Include("Employee.ThirdParty.Person").Include("RetirementReason") _
                                  .Include("Contract.Position").Include("Contract.Group").Include("ContractLiquidationDetail").Include("Contract.FundContract.Fund.ThirdParty").Include("Contract.FunctionalUnit.BranchOffice.City.Department")
                                  Where e.RetirementDate.Month = Month And e.RetirementDate.Year = Year
                                  Select e

        If contractLiquidation.Count() > 0 Then

            Dim ListContractLiquidation = contractLiquidation.ToList()

            For Each varContractLiquidation As ContractLiquidation In ListContractLiquidation

                If varContractLiquidation.ContractLiquidationDetail.Any(Function(x) x.ConceptType Is Nothing) Then
                    For Each varContractLiquidationDetail As ContractLiquidationDetail In varContractLiquidation.ContractLiquidationDetail

                        Dim conceptId = (From ge In _context.Concept Where ge.Id = varContractLiquidationDetail.IdConcept Select ge).FirstOrDefault()
                        varContractLiquidationDetail.ConceptType = conceptId.ConceptType
                    Next
                End If

            Next

            Return ListContractLiquidation
        Else
            Return Nothing
        End If

    End Function

    ''' <summary>
    ''' Funcion para calcular las liquidacion por id de empleado en cierto rango de fecha
    ''' </summary>
    ''' <param name="employeeId">Id del empleado</param>
    ''' <param name="initialDate">Fecha Inicial</param>
    ''' <param name="endDate">Fecha Final</param>
    ''' <returns>Lista de liquidaciones</returns>
    ''' <remarks></remarks>
    Public Function LiquidationEmployeeByDate(employeeId As Integer, initialDate As Date, endDate As Date) As List(Of Liquidation) Implements IContractLiquidationRepository.LiquidationEmployeeByDate
        Dim payrollLiquidated = From e In _context.Liquidation.Include("LiquidationDetail").Include("LiquidationDetail.Concept")
                                Where e.EmployeeId = employeeId And e.PayrollDateLiquidated >= initialDate And e.PayrollDateLiquidated <= endDate And e.RegisterStatus <> ""
        If payrollLiquidated.Count > 0 Then
            Return payrollLiquidated.ToList()
        Else
            Return New List(Of Liquidation)
        End If
    End Function

    Public Function GetContractLiquidationByContractId(ByVal IdContract As Integer) As ContractLiquidation Implements IContractLiquidationRepository.GetContractLiquidationByContractId
        Dim contractLiquidation = From e In _context.ContractLiquidation
                                  Where e.ContractId = IdContract
                                  Select e

        Return contractLiquidation.FirstOrDefault()
    End Function

    ''' <summary>
    ''' Obtiene el reporte de detalle de liquidación de contratos con conceptos dinámicos como columnas
    ''' </summary>
    ''' <param name="initialDate">Fecha inicial del período</param>
    ''' <param name="endDate">Fecha final del período</param>
    ''' <param name="employeeId">Id del empleado (opcional)</param>
    ''' <param name="session">Valores de sesión para obtener la conexión</param>
    ''' <returns>DataTable con el reporte de liquidación de contratos detallado</returns>
    Public Function GetContractLiquidationDetailReport(initialDate As Date, endDate As Date, Optional employeeId As Integer? = Nothing, Optional session As Infrastructure.CrossCutting.Base.SessionValues = Nothing) As System.Data.DataTable Implements IContractLiquidationRepository.GetContractLiquidationDetailReport
        Dim dtContractLiquidationReport As New System.Data.DataTable("ContractLiquidationDetailReport")

        Using sqlCnn As New System.Data.SqlClient.SqlConnection(Infrastructure.CrossCutting.Base.Utils.GetEntityConnectionString(
            Infrastructure.CrossCutting.Base.ConfigurationFile.CONX_GENESIS_REPORTS,
            String.Empty,
            session.TransactionalContainer,
            False))

            Using sqlCmd As New System.Data.SqlClient.SqlCommand("Payroll.SP_ContractLiquidationDetailReport", sqlCnn)
                sqlCmd.CommandType = System.Data.CommandType.StoredProcedure
                sqlCmd.CommandTimeout = 3600

                Dim sqlPrm As System.Data.SqlClient.SqlParameter

                sqlPrm = New System.Data.SqlClient.SqlParameter
                sqlPrm.ParameterName = "@InitialDate"
                sqlPrm.Value = initialDate
                sqlCmd.Parameters.Add(sqlPrm)

                sqlPrm = New System.Data.SqlClient.SqlParameter
                sqlPrm.ParameterName = "@EndDate"
                sqlPrm.Value = endDate
                sqlCmd.Parameters.Add(sqlPrm)

                sqlPrm = New System.Data.SqlClient.SqlParameter
                sqlPrm.ParameterName = "@EmployeeId"
                If employeeId.HasValue Then
                    sqlPrm.Value = employeeId.Value
                Else
                    sqlPrm.Value = DBNull.Value
                End If
                sqlCmd.Parameters.Add(sqlPrm)

                sqlCnn.Open()

                Using sqlDR As System.Data.SqlClient.SqlDataReader = sqlCmd.ExecuteReader
                    dtContractLiquidationReport.Load(sqlDR)
                End Using
            End Using
        End Using

        Return dtContractLiquidationReport
    End Function

End Class