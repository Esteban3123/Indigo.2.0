'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 16-01-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Payroll.Entities
Imports Presentation.CloudAgent
Imports Presentation.CloudAgent.IndigoReference.Payroll
Imports Presentation.Base
Imports System.Text

#End Region
Public Class MIncentivePayment
    Inherits ModelBase
    Implements IDisposable

#Region "Properties"

    Public Shared TAG As String = "597"

    ''' <summary>
    ''' Contructor
    ''' </summary>
    ''' <param name="Tag">tag del form</param>
    ''' <remarks></remarks>
    Public Sub New(Tag As String)
        MyBase.New(Tag)
    End Sub

    Dim Indigo As SessionValues = SessionValues.Instance
#End Region

#Region "methods"

    ''' <summary>
    ''' Calcular Primas
    ''' </summary>
    ''' <param name="strGroupId">Id del Grupo</param>
    ''' <param name="period">Periodo</param>
    ''' <param name="MaxPremiumByYear">Número Primas al Año</param>
    ''' <param name="VarYear">Año</param>
    ''' <param name="employeeNit">Nit del Empleado</param>
    ''' <param name="valueExtraIncentivePayment">Valor Primas Manual</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function CalculateIncentivePaymentAsync(strGroupId As String, period As Char, MaxPremiumByYear As Byte, VarYear As Integer, PaymentType As Char, Optional employeeNit As String = "", Optional valueExtraIncentivePayment As Double = 0, Optional offset As Integer = -1, Optional pageSize As Integer = 50) As Task(Of ActionMessageResult(Of List(Of IncentivePayment)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.CalculateIncentivePaymentAsync(strGroupId, period, MaxPremiumByYear, VarYear, PaymentType, Indigo, employeeNit, valueExtraIncentivePayment, offset, pageSize)
    End Function

    Public Async Function GetIncentivePaymentByDatesGroupId(groupId As String, MaxPremiumByYear As Byte, period As Char, VarYear As Integer) As Task(Of ActionMessageResult(Of List(Of IncentivePayment)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetIncentivePaymentByPeriodGroupIdAsync(groupId, MaxPremiumByYear, period, VarYear, Indigo, 1)
    End Function

    ''' <summary>
    ''' Almacenar Primas
    ''' </summary>
    ''' <param name="incentivePayment">Lista de Primas</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Async Function SaveIncentivePaymentAsync(incentivePayment As List(Of IncentivePayment)) As Task(Of ActionResult(Of List(Of IncentivePayment)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.SaveIncentivePaymentAsync(incentivePayment, Indigo)
    End Function

    ''' <summary>
    ''' Crear archivo plano de Primas
    ''' </summary>
    ''' <param name="ListIncentivePayment"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GenerateBankFileAsync(PeriodEndDate As Date, BankId As Integer, AccountNumber As String, AccountType As Integer, CompanyId As Integer) As Task(Of ActionMessageResult(Of StringBuilder))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GenerateBankFileIncentivePaymentAsync(PeriodEndDate, BankId, AccountNumber, AccountType, CompanyId, Indigo)
    End Function

    ''' <summary>
    ''' Funciona que retorna la Lista de Fechas de Liquidaciones de Primas Confirmadas y que se pagan por Periodo Independiente
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetConfirmIncentivenDates() As Task(Of List(Of Date))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetConfirmIncentivenDatesAsync(Indigo)
    End Function

    Public Async Function GetHeadIncentivePaymentAsync(Year As Integer, Period As Integer) As Task(Of List(Of IncentivePayment))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetHeadIncentivePaymentAsync(Year, Period, Indigo)
    End Function

    Public Async Function GetDetailIncentivePaymentAsync(GroupId As Integer, Year As Integer, Period As Integer) As Task(Of List(Of IncentivePayment))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetDetailIncentivePaymentAsync(GroupId, Year, Period, Indigo)
    End Function


    ''' <summary>
    ''' Obtiene el conteo de empleados para liquidar primas
    ''' </summary>
    Public Async Function GetEmployeeCountForIncentivePaymentAsync(strGroupId As String, period As Char) As Task(Of Integer)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetEmployeeCountForIncentivePaymentAsync(strGroupId, period, Indigo)
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
