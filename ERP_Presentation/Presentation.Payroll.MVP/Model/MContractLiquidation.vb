Imports Presentation.Base
Imports Presentation.CloudAgent
Imports Domain.Payroll.Entities
Imports Domain.Base.Entities

'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Jose Luis Rojas
' Created          : 10-01-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Public Class MContractLiquidation
    Inherits ModelBase
    Implements IDisposable

    Public Shared TAG As String = "596"

    ''' <summary>
    ''' Contructor
    ''' </summary>
    ''' <param name="Tag">tag del form</param>
    ''' <remarks></remarks>
    Public Sub New(Tag As String)
        MyBase.New(Tag)
    End Sub

#Region "methods"

    ''' <summary>
    ''' Metodo que consulta el listado de nominas liquidadas para el contrato seleccionado
    ''' </summary>
    ''' <param name="contractId">id del contrato</param>
    ''' <returns>listado de liquidaciones de nomina</returns>
    Public Function GetPaymentsByContractId(contractId As Integer) As List(Of Liquidation)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetPaymentsByContractId(contractId, Indigo)
    End Function


    Public Async Function LiquidateContracts(employeesToLiquidate As Dictionary(Of Integer, Tuple(Of Date, Integer))) As Task(Of List(Of ActionMessageResult(Of ContractLiquidation)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.LiquidateContractsAsync(employeesToLiquidate, Indigo)
    End Function

    ''' <summary>
    ''' Metodo que consulta las Liquidaciones de Contrato por Mes y Año
    ''' </summary>
    ''' <param name="Month">Mes</param>
    ''' <param name="Year">Año</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetContractLiquidationByMonthAndYear(Month As Integer, Year As Integer) As Task(Of List(Of ContractLiquidation))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetContractLiquidationByMonthAndYearAsync(Month, Year, Indigo)
    End Function

    Public Async Function GetLiquidationsPaidByBaseContractId(baseContractId As Integer) As Task(Of List(Of Liquidation))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetLiquidationsPaidByBaseContractIdAsync(baseContractId, Indigo)
    End Function


    ''' <summary>
    ''' Lista los campos nulos de la base de datos y permitir su customizacion
    ''' </summary>
    ''' <returns>Un conjuto de datos con los campos marcados como nulos en la base de datos</returns>
    Public Function GetNullFields() As DataSet
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetFieldsNULL("ContractLiquidation", Indigo)
    End Function

    ''' <summary>
    ''' Metodo para confirmar una liquidacion de contrato
    ''' </summary>
    ''' <param name="contractLiquidation">liquidacion contrato</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ConfirmLiquidationContract(contractLiquidation As ContractLiquidation) As Task(Of Domain.Base.Entities.ActionMessageResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.ConfirmLiquidationContractAsync(contractLiquidation, Indigo)
    End Function

    ''' <summary>
    ''' Metodo para confirmar una lista de liquidacion de contratos
    ''' </summary>
    ''' <param name="ListContractLiquidation">Lista de liquidacion contratos</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Async Function ConfirmListLiquidationContract(ListContractLiquidation As List(Of ContractLiquidation)) As Task(Of Domain.Base.Entities.ActionMessageResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.ConfirmListLiquidationContractAsync(ListContractLiquidation, Indigo)
    End Function

    Public Async Function ListAllOperatingUnit() As Task(Of List(Of Domain.Entities.OperatingUnit))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.ListAllOperatingUnitAsync(Indigo, Indigo.IndigoCompany)
    End Function

    ''' <summary>
    ''' Obtiene el reporte de detalle de liquidación de contratos con conceptos dinámicos
    ''' </summary>
    ''' <param name="initialDate">Fecha inicial del período</param>
    ''' <param name="endDate">Fecha final del período</param>
    ''' <param name="employeeId">Id del empleado (opcional)</param>
    ''' <returns>DataTable con el reporte de liquidación de contratos detallado</returns>
    Public Async Function GetContractLiquidationDetailReportAsync(initialDate As Date, endDate As Date, Optional employeeId As Integer? = Nothing) As Task(Of System.Data.DataTable)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetContractLiquidationDetailReportAsync(initialDate, endDate, employeeId, Indigo)
    End Function




#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
