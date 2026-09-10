'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Kevin Garay Rodriguez
' Created          : 18-04-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent
Imports Domain.Payroll.Entities
Imports Domain.Base.Entities
Imports Presentation.Base
Imports System.Text
Imports Infrastructure.Data.Xpo

Public Class MAutoliquidation
    Inherits ModelBase
    Implements IDisposable

    ''' <summary>
    ''' Constructor que recibe el tag del formulario
    ''' </summary>
    ''' <param name="tag"></param>
    ''' <remarks></remarks>
    Sub New(tag As String)
        MyBase.New(tag)
    End Sub

    ''' <summary>
    ''' Lista todas las fechas de liquidacion de una empresa
    ''' </summary>
    ''' <param name="companyId">Empresa</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetDateLiquidationCompany(companyId As Integer) As Task(Of List(Of String))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetDateLiquidationCompanyAsync(companyId, Indigo)
    End Function

    ''' <summary>
    ''' Funcion que lista las compañias
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetCompanyAsync(code As String) As Task(Of Company)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetCompanyAsync(code, Indigo)
    End Function

    ''' <summary>
    ''' Funcion que obtiene todos los centros de costos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ListAllWorkCenter() As Task(Of List(Of WorkCenter))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.ListAllWorkCenterAsync(Indigo)
    End Function

    ''' <summary>
    ''' Lista los campos nulos de la base de datos y permitir su customizacion
    ''' </summary>
    ''' <returns>Un conjuto de datos con los campos marcados como nulos en la base de datos</returns>
    Public Function GetFieldsNULL() As Object
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetFieldsNULL("Autoliquidation", Indigo)
    End Function

    ''' <summary>
    ''' Lista todas las compañias
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ListAllCompany() As Task(Of List(Of Company))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.ListAllCompanyAsync(Indigo)
    End Function

    ''' <summary>
    ''' Se obtiene la empresa que esta en la session (versión asíncrona)
    ''' </summary>
    ''' <returns></returns>
    Public Async Function ListCompanyBySessionAsync() As Task(Of Company)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetCompanyAsync(Indigo.IndigoCompanyNit, Indigo)
    End Function


    ''' <summary>
    ''' Genera todo el proceso de autoliquidación y el plano
    ''' </summary>
    ''' <param name="companyId">Id de la compañia</param>
    ''' <param name="periodLiquidation">Periodo de liquidación</param>
    ''' <param name="workCenterId">Id del centro de trabajo</param>
    ''' <param name="isCorrection">si es correccion</param>
    ''' <param name="dateLiquidation">Fecha de liquidacion</param>
    ''' <param name="numberTemplate">numero de plantilla</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GenerateAutoliquidationAsync(companyId As Integer, periodLiquidation As String, workCenterId As Integer, isCorrection As Boolean, dateLiquidation As Nullable(Of Date), numberTemplate As String) As Task(Of List(Of ActionMessageResult(Of StringBuilder)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GenerateAutoLiquidationAsync(companyId, periodLiquidation, workCenterId, isCorrection, dateLiquidation, numberTemplate, Indigo)
    End Function

    Public Async Function GenerateReportAutoliquidation(PeriodDate As String, WorkCenterId As Integer?) As Task(Of DataSet)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GenerateReportAutoliquidationAsync(PeriodDate, WorkCenterId, Indigo)
    End Function

    Public Async Function GenerateValidationAutoliquidation(WorkCenterId As Integer, PayrollDateLiquidated As Date) As Task(Of List(Of SP_AutoliquidationFile_Result))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GenerateValidationAutoliquidationAsync(WorkCenterId, PayrollDateLiquidated, Indigo)
    End Function

    Public Async Function GenerateValidationAutoliquidationCR(WorkCenterId As Integer, PayrollDateLiquidated As Date) As Task(Of List(Of SP_AutoliquidationFileCR))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GenerateValidationAutoliquidationCRAsync(WorkCenterId, PayrollDateLiquidated, Indigo)
    End Function

    Public Async Function ListVerifyAutoliquidation(WorkCenterId As Integer, PayrollDateLiquidated As Date) As Task(Of List(Of VerifyAutoliquidationFile))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.ListVerifyAutoliquidationAsync(WorkCenterId, PayrollDateLiquidated, Indigo)
    End Function

    Public Async Function ConfirmVerifyAutoliquidation(WorkCenterId As Integer, PayrollDateLiquidated As Date, FlagConfirm As Byte) As Task(Of ActionResult(Of List(Of VerifyAutoliquidationFile)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.ConfirmVerifyAutoliquidationAsync(WorkCenterId, PayrollDateLiquidated, FlagConfirm, Indigo)
    End Function

    Public Async Function SaveVerifyAutoliquidation(verifyAutoliquidation As VerifyAutoliquidationFile) As Task(Of ActionResult(Of VerifyAutoliquidationFile))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.SaveVerifyAutoliquidationAsync(verifyAutoliquidation, Indigo)
    End Function

    Public Function SaveMassiveVerifyAutoliquidation(ListImportFileRow As List(Of ImportFileRow)) As ActionResult(Of List(Of Tuple(Of String, Integer)))
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.SaveMassiveVerifyAutoliquidation(ListImportFileRow, Indigo)
    End Function
    ''' <summary>
    ''' Funcion que obtiene la informacion para crear el archivo plano CCSS
    ''' </summary>
    ''' <param name="companyId"></param>
    ''' <param name="workCenterId"></param>
    ''' <param name="periodLiquidation"></param>
    ''' <returns></returns>
    Public Async Function GenerateCCSS(companyId As Integer, workCenterId As Integer, periodLiquidation As String) As Task(Of ActionMessageResult(Of StringBuilder))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GenerateCCSSAsync(companyId, workCenterId, periodLiquidation, Indigo)
    End Function
    ''' <summary>
    ''' Funcion que obtiene la informacion para crear el archivo plano INS
    ''' </summary>
    ''' <param name="companyId"></param>
    ''' <param name="policyNumber"></param>
    ''' <param name="workCenterId"></param>
    ''' <param name="periodLiquidation"></param>
    ''' <returns></returns>
    Public Async Function GenerateINS(companyId As Integer, policyNumber As String, workCenterId As Integer, periodLiquidation As String) As Task(Of ActionMessageResult(Of StringBuilder))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GenerateINSAsync(companyId, policyNumber, workCenterId, periodLiquidation, Indigo)
    End Function

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
