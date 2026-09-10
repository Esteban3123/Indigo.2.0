'***********************************************************************
' Assembly         : Infrastructure.Data.PayrollRepository
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 20-12-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Payroll.Entities
Imports Domain.Payroll

Public Class LiquidationDetailRepository
    Inherits GenericRepository(Of LiquidationDetail)
    Implements ILiquidationDetailRepository

    'Devuelve el contexto en este repositorio 
    Private _context As IPayrollUnitOfWork

    ''' <summary>
    '''inicializa la neva instancia d clase.
    ''' </summary>
    ''' <param name="contex">el contexto.</param>
    Public Sub New(ByVal contex As IPayrollUnitOfWork)
        MyBase.New(contex)
        _context = contex
    End Sub

    Public Function LiquidationDetailBaseVacationByEmployeeDate(EmployeeId As Integer, initialDate As Date, endDate As Date) As List(Of LiquidationDetail) Implements ILiquidationDetailRepository.LiquidationDetailBaseVacationByEmployeeDate
        Dim payrollLiquidated = From e In _context.LiquidationDetail.AsNoTracking().Include("Concept").AsNoTracking().Include("Liquidation").AsNoTracking().Include("Liquidation.Contract").AsNoTracking()
                                Where e.Liquidation.EmployeeId = EmployeeId And e.Liquidation.PayrollDateLiquidated >= initialDate And e.Liquidation.PayrollDateLiquidated <= endDate And e.Concept.AffectIBCVacation = True And e.ConceptClass <> "005" And e.ConceptClass <> "021" And e.Liquidation.RegisterStatus <> ""
        If payrollLiquidated.Count > 0 Then
            Return payrollLiquidated.ToList()
        Else
            Return New List(Of LiquidationDetail)
        End If
    End Function

    Public Function LiquidationDetailBaseVacation(ContractId As Integer, ListConceptClass As List(Of String), initialDate As Date, endDate As Date) As List(Of LiquidationDetail) Implements ILiquidationDetailRepository.LiquidationDetailBaseVacation
        Dim payrollLiquidated = From e In _context.LiquidationDetail.Include("Concept").Include("Liquidation")
                                Where ListConceptClass.Contains(e.ConceptClass) And e.Liquidation.InitialContractNumber = ContractId And e.Liquidation.PayrollDateLiquidated >= initialDate And e.Liquidation.PayrollDateLiquidated <= endDate _
                                        And e.Concept.AffectIBCVacation = True
        If payrollLiquidated.Count > 0 Then
            Return payrollLiquidated.ToList()
        Else
            Return New List(Of LiquidationDetail)
        End If
    End Function
End Class
