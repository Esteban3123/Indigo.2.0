'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Kevin Garay Rodriguez
' Created          : 03-03-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent
Imports Presentation.CloudAgent.IndigoReference.Glosas
Imports Domain.Payroll.Entities
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Presentation.Base
Imports System.Text
Imports Domain.Payroll

#End Region
''' <summary>
''' Modelo que se comunica con los servicios corresporndientes al funcional
''' </summary>
Public Class MBankFile
    Implements IDisposable

#Region "Construct"

    ''' <summary>
    ''' Referencia a los valores de session
    ''' </summary>
    Private _indigo As SessionValues

    ''' <summary>
    ''' Id del frontal
    ''' </summary>
    Private _tagForm As String

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Sub New(ByVal tag As String)
        Me._tagForm = tag
        Me._indigo = SessionValues.Instance
    End Sub

#End Region

#Region "Methods"

    Public Async Function ListAllCompany() As Task(Of List(Of Domain.Payroll.Entities.Company))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.ListAllCompanyAsync(Me._indigo)
    End Function

    Public Async Function GetBankFileByCode(code As String) As Task(Of BankFile)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetBankFileByCodeAsync(code, Me._indigo)
    End Function

    Public Function GetBankFileDetailByBankFileId(id As Integer) As List(Of BankFileDetail)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetBankFileDetailByBankFileId(id, Me._indigo)
    End Function

    Public Async Function SaveBankFile(bankFile As BankFile, listBankFileDetailDelete As List(Of Integer)) As Task(Of ActionResult(Of BankFile))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.SaveBankFileAsync(bankFile, listBankFileDetailDelete, Me._indigo)
    End Function

    Public Async Function GenerateBankFileAsync(bankFileId As Integer) As Task(Of ActionMessageResult(Of StringBuilder))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GenerateBankFileAsync(bankFileId, Me._indigo)
    End Function

    Public Async Function SaveAndConfirmBankFile(bankFile As BankFile, listBankFileDetailDelete As List(Of Integer)) As Task(Of ActionResult(Of BankFile))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.SaveAndConfirmBankFileAsync(bankFile, listBankFileDetailDelete, Me._indigo)
    End Function

#Region "Method IncentivePayment"
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
    Public Async Function CalculateIncentivePaymentAsync(strGroupId As String, period As Char, MaxPremiumByYear As Byte, VarYear As Integer, PaymentType As Char, Optional employeeNit As String = "", Optional valueExtraIncentivePayment As Double = 0) As Task(Of ActionMessageResult(Of List(Of IncentivePayment)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.CalculateIncentivePaymentAsync(strGroupId, period, MaxPremiumByYear, VarYear, PaymentType, _indigo, employeeNit, valueExtraIncentivePayment, 0, 0)
    End Function

    ''' <summary>
    ''' Se obtiene la prima x fecha, grupoId
    ''' </summary>
    ''' <param name="groupId"></param>
    ''' <param name="MaxPremiumByYear"></param>
    ''' <param name="period"></param>
    ''' <param name="VarYear"></param>
    ''' <returns></returns>
    Public Async Function GetIncentivePaymentByDatesGroupId(groupId As String, MaxPremiumByYear As Byte, period As Char, VarYear As Integer) As Task(Of ActionMessageResult(Of List(Of IncentivePayment)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetIncentivePaymentByPeriodGroupIdAsync(groupId, MaxPremiumByYear, period, VarYear, _indigo, 1)
    End Function

    ''' <summary>
    ''' Almacenar Primas
    ''' </summary>
    ''' <param name="incentivePayment">Lista de Primas</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Async Function SaveIncentivePaymentAsync(incentivePayment As List(Of IncentivePayment)) As Task(Of ActionResult(Of List(Of IncentivePayment)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.SaveIncentivePaymentAsync(incentivePayment, _indigo)
    End Function

    ''' <summary>
    ''' Crear archivo plano de Primas
    ''' </summary>
    ''' <param name="ListIncentivePayment"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GenerateBankFileAsync(PeriodEndDate As Date, BankId As Integer, AccountNumber As String, AccountType As Integer, CompanyId As Integer) As Task(Of ActionMessageResult(Of StringBuilder))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GenerateBankFileIncentivePaymentAsync(PeriodEndDate, BankId, AccountNumber, AccountType, CompanyId, _indigo)
    End Function

    ''' <summary>
    ''' Funciona que retorna la Lista de Fechas de Liquidaciones de Primas Confirmadas y que se pagan por Periodo Independiente
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetConfirmIncentivenDates() As Task(Of List(Of Date))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetConfirmIncentivenDatesAsync(_indigo)
    End Function

    ''' <summary>
    ''' Funcion que retorna año, periodo
    ''' </summary>
    ''' <param name="Year"></param>
    ''' <param name="Period"></param>
    ''' <returns></returns>
    Public Async Function GetHeadIncentivePaymentAsync(Year As Integer, Period As Integer) As Task(Of List(Of IncentivePayment))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetHeadIncentivePaymentAsync(Year, Period, _indigo)
    End Function

    ''' <summary>
    ''' Funcion que retorna año, periodo y grupo
    ''' </summary>
    ''' <param name="GroupId"></param>
    ''' <param name="Year"></param>
    ''' <param name="Period"></param>
    ''' <returns></returns>
    Public Async Function GetDetailIncentivePaymentAsync(GroupId As Integer, Year As Integer, Period As Integer) As Task(Of List(Of IncentivePayment))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetDetailIncentivePaymentAsync(GroupId, Year, Period, _indigo)
    End Function
    ''' <summary>
    ''' Trae la lista de las primas liquidadas pero no confirmadas en AP
    ''' </summary>
    ''' <param name="Period"></param>
    ''' <param name="DateLiquidate"></param>
    ''' <returns></returns>
    Public Async Function ShowIncentivePaymentWithoutConfirm(Period As Integer, DateLiquidate As DateTime) As Task(Of List(Of SP_BankFileIncentivePaymentWithoutConfirm_Result))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.ShowIncentivePaymentAsync(Period, DateLiquidate, Me._indigo)
    End Function
#End Region

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: eliminar estado administrado (objetos administrados).
            End If

            ' TODO: liberar recursos no administrados (objetos no administrados) e invalidar Finalize() below.
            ' TODO: Establecer campos grandes como Null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: invalidar Finalize() sólo si la instrucción Dispose(ByVal disposing As Boolean) anterior tiene código para liberar recursos no administrados.
    'Protected Overrides Sub Finalize()
    '    ' No cambie este código. Ponga el código de limpieza en la instrucción Dispose(ByVal disposing As Boolean) anterior.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' Visual Basic agregó este código para implementar correctamente el modelo descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' No cambie este código. Coloque el código de limpieza en Dispose(disposing As Boolean).
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class