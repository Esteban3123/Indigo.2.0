'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 17-03-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Payroll.Entities
Imports Domain.Base.Entities
Imports System.Text
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Payroll
Imports Domain.Base
Imports Infrastructure.CrossCutting.Base

Public Class NationalSavingsFundAdminService
    Implements INationalSavingsFundAdminService

    ''' <summary>
    ''' Repositorio de Liquidación de Nómina
    ''' </summary>
    ''' <remarks></remarks>
    Private _LiquidationRepository As IPayrollLiquidationRepository

    ''' <summary>
    ''' Servicios de Fondo de Solidaridad
    ''' </summary>
    ''' <remarks></remarks>
    Private _NationalSavingsFundDomain As INationalSavingsFundDomain

    ''' <summary>
    ''' inicia el repositorio de Plano de Bancos
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(ByVal LiquidationRepository As IPayrollLiquidationRepository, ByVal NationalSavingsFundDomain As INationalSavingsFundDomain)
        If (LiquidationRepository Is Nothing) Then
            Throw New ArgumentNullException("Repositorio LiquidationRepository vacio")
        End If

        If (NationalSavingsFundDomain Is Nothing) Then
            Throw New ArgumentNullException("Repositorio NationalSavingsFundDomain vacio")
        End If

        _NationalSavingsFundDomain = NationalSavingsFundDomain
        _LiquidationRepository = LiquidationRepository
    End Sub

    ''' <summary>
    ''' Función en la cual se genera el archivo plano del Fondo Nacional del Ahorro por Fecha de Liquidación
    ''' </summary>
    ''' <param name="PayrollDateLiquidated">Fecha de Liquidación</param>
    ''' <param name="CompanyId">Id Empresa</param>
    ''' <param name="audit">Variable de Sesión</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GenerateNationalSavingsFundFile(PayrollDateLiquidated As Date, CompanyId As Integer, audit As Infrastructure.CrossCutting.Base.SessionValues) As ActionMessageResult(Of StringBuilder) Implements INationalSavingsFundAdminService.GenerateNationalSavingsFundFile
        Dim NationalSavingsFundFile As New ActionMessageResult(Of StringBuilder)

        Try
            Dim PayrollLiquidation As New List(Of Liquidation)
            Dim UnemployedLiquidationNationalSavingFund As New List(Of Liquidation)
            Dim PayrollLiquidationAcumulated As New List(Of Liquidation)
            Dim InitialDate As Date = New Date(Year(PayrollDateLiquidated), 1, 1)


            PayrollLiquidation = _LiquidationRepository.ListLiquitadionByDateLiquidatedNationalSavingsFund(PayrollDateLiquidated, CompanyId)
            PayrollLiquidationAcumulated = _LiquidationRepository.GetConfirmLiquidationByStarEndDate(InitialDate, PayrollDateLiquidated)


            If PayrollLiquidation.Any(Function(x) x.Contract.FundContract.Any(Function(y) y.FundType = "3" And y.Fund.ThirdParty.Nit = "899999284")) = True Then
                UnemployedLiquidationNationalSavingFund = PayrollLiquidation.Where(Function(x) x.Contract.FundContract.Any(Function(y) y.FundType = "3" And y.Fund.ThirdParty.Nit = "899999284")).ToList()
            End If

            If UnemployedLiquidationNationalSavingFund.Count > 0 Then
                NationalSavingsFundFile.ObjectEmbbeded = _NationalSavingsFundDomain.GenerateArchive(UnemployedLiquidationNationalSavingFund, PayrollLiquidationAcumulated)
                NationalSavingsFundFile.StateResult = True

                Return NationalSavingsFundFile
            Else
                NationalSavingsFundFile.StateResult = False
                Return Nothing
            End If
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", audit)
            Return New ActionMessageResult(Of StringBuilder)() With {.StateResult = False}
        End Try

    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                _NationalSavingsFundDomain.Dispose()
            End If
            _NationalSavingsFundDomain = Nothing
            _LiquidationRepository = Nothing
            IndigoGC.Execute()
        End If
        disposedValue = True
    End Sub

    ' Visual Basic agrega este código para implementar correctamente el patrón descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
