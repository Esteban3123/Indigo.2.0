'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 11-10-2018
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

Public Class FileForeclousureAdminService

    Implements IFileForeclousureAdminService

    ''' <summary>
    ''' Repositorio de Liquidación de Nómina
    ''' </summary>
    ''' <remarks></remarks>
    Private _LiquidationRepository As IPayrollLiquidationRepository

    ''' <summary>
    ''' Servicios de Company
    ''' </summary>
    ''' <remarks></remarks>
    Private _CompanyRepository As ICompanyRepository

    ''' <summary>
    ''' Servicios de Company
    ''' </summary>
    ''' <remarks></remarks>
    Private _foreclousureDomain As IForeclosureDomain

    ''' <summary>
    ''' Repositorio de Embargos
    ''' </summary>
    Private _foreclousureRepository As IForeclousureRepository

    ''' <summary>
    ''' inicia el repositorio de Plano de Bancos
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(ByVal LiquidationRepository As IPayrollLiquidationRepository, ByVal CompanyRepository As ICompanyRepository, ByVal foreclousureDomain As IForeclosureDomain, foreclousureRepository As IForeclousureRepository)
        If (LiquidationRepository Is Nothing) Then
            Throw New ArgumentNullException("Repositorio LiquidationRepository vacio")
        End If

        If (CompanyRepository Is Nothing) Then
            Throw New ArgumentNullException("Repositorio NationalSavingsFundDomain vacio")
        End If

        If (foreclousureDomain Is Nothing) Then
            Throw New ArgumentNullException("Repositorio foreclousureDomain vacio")
        End If

        _CompanyRepository = CompanyRepository
        _LiquidationRepository = LiquidationRepository
        _foreclousureDomain = foreclousureDomain
        _foreclousureRepository = foreclousureRepository
    End Sub

    ''' <summary>
    ''' Función en la cual se genera el archivo plano del Fondo Nacional del Ahorro por Fecha de Liquidación
    ''' </summary>
    ''' <param name="PayrollDateLiquidated">Fecha de Liquidación</param>
    ''' <param name="CompanyId">Id Empresa</param>
    ''' <param name="audit">Variable de Sesión</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GenerateFileForeclousure(PayrollDateLiquidated As Date, CompanyId As Integer, audit As Infrastructure.CrossCutting.Base.SessionValues) As ActionMessageResult(Of StringBuilder) Implements IFileForeclousureAdminService.GenerateFileForeclousure
        Dim FileForeclousure As New ActionMessageResult(Of StringBuilder)

        Try
            Dim PayrollStarDate As New Date(PayrollDateLiquidated.Year, PayrollDateLiquidated.Month, 1)

            Dim ObjCompany = _CompanyRepository.GetCompanyById(CompanyId)

            Dim ListLiquidationDetail As New List(Of LiquidationDetail)
            Dim ListForeclousure As New List(Of Foreclousure)

            ListLiquidationDetail = _LiquidationRepository.GetLastConceptClassBetweenDate("053", PayrollDateLiquidated, PayrollDateLiquidated)
            ListForeclousure = _foreclousureRepository.ListForeclousureByDateNoStatus(PayrollDateLiquidated)

            If ListLiquidationDetail.Count > 0 Then
                Return _foreclousureDomain.GenerateArchive(ListLiquidationDetail, ObjCompany, ListForeclousure)
            Else
                FileForeclousure.Message = "No existen datos para generar archivo"
                FileForeclousure.StateResult = False
            End If

            Return FileForeclousure

        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", audit)
            FileForeclousure.MessageResult.Add(New MessageResult("-999", ex.Message))
            FileForeclousure.StateResult = False
            Return FileForeclousure
        End Try

    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        ' TODO: uncomment the following line if Finalize() is overridden above.
        ' GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
