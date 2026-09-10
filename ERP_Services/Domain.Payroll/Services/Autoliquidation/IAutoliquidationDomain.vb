'***********************************************************************
' Assembly         : Domain.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 17-03-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.Text
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Domain.Payroll.Entities


Public Interface IAutoliquidationDomain
    Inherits IDisposable

    Function GenerateArchive(company As Domain.Payroll.Entities.Company, workCenter As WorkCenter, periodLiquidation As String, isCorrection As Boolean,
                                   dateLiquidation As Nullable(Of Date), numberTemplate As String, listLiquidation As List(Of Liquidation), SpreadsheetType As String) As ActionMessageResult(Of StringBuilder)


    Function ValidateData(ListImportFileRow As List(Of ImportFileRow), ListVerifyAutoliquidationFile As List(Of VerifyAutoliquidationFile)) As ActionResult(Of List(Of VerifyAutoliquidationFile))

    ''' <summary>
    ''' Funcion para el archivo plano CCSS
    ''' </summary>
    ''' <param name="company"></param>
    ''' <param name="workCenterId"></param>
    ''' <param name="periodLiquidation"></param>
    ''' <param name="listLiquidation"></param>
    ''' <returns></returns>
    Function GenerateCCSS(company As Domain.Payroll.Entities.Company, workCenterId As Integer, periodLiquidation As String, listLiquidation As List(Of Liquidation)) As ActionMessageResult(Of StringBuilder)

    ''' <summary>
    ''' Funcion para el archivo plano INS
    ''' </summary>
    ''' <param name="company"></param>
    ''' <param name="policyNumber"></param>
    ''' <param name="workCenter"></param>
    ''' <param name="periodLiquidation"></param>
    ''' <param name="listLiquidation"></param>
    ''' <returns></returns>
    Function GenerateINS(company As Domain.Payroll.Entities.Company, policyNumber As String, workCenter As WorkCenter, periodLiquidation As String, listLiquidation As List(Of Liquidation)) As ActionMessageResult(Of StringBuilder)
End Interface
