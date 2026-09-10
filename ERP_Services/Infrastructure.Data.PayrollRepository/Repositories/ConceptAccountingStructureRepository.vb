'***********************************************************************
' Assembly         : Infrastructure.Data.PayrollRepository
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 24-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Payroll.Entities
Imports Domain.Payroll

Public Class ConceptAccountingStructureRepository

    Inherits GenericRepository(Of ConceptAccountingStructure)
    Implements IConceptAccountingStructureRepository


    'Contexto de payroll
    Private _context As IPayrollUnitOfWork

    Public Sub New(ByVal context As IPayrollUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Función para Cargar los Conceptos con Estructura Contable por Interfaz y Id del Código
    ''' </summary>
    ''' <param name="InterfaceName">Nombre Interfaz</param>
    ''' <param name="ConceptId">Id Concepto</param>
    ''' <returns>Lista de ConceptAccountingStructure</returns>
    ''' <remarks></remarks>
    Public Function GetAccountingStructureByInterfaceConceptId(InterfaceName As String, ConceptId As Integer) As List(Of ConceptAccountingStructure) Implements IConceptAccountingStructureRepository.GetAccountingStructureByInterfaceConceptId
        Dim ConceptAccountingStructure = From e In _context.ConceptAccountingStructure.Include("AccountingStructure")
                                         Where e.InterfazName = InterfaceName And e.ConceptId = ConceptId
                       Select e
        Return ConceptAccountingStructure.ToList()
    End Function

    ''' <summary>
    ''' Función para Cargar los Conceptos con Estructura Contable por Interfaz y Id del Código
    ''' </summary>
    ''' <param name="InterfaceName">Nombre Interfaz</param>
    ''' <returns>Lista de ConceptAccountingStructure</returns>
    ''' <remarks></remarks>
    Public Function GetAccountingStructureByInterfaceName(InterfaceName As String, FunctionalUnitId As Integer) As List(Of ConceptAccountingStructure) Implements IConceptAccountingStructureRepository.GetAccountingStructureByInterfaceName
        'Dim ConceptAccountingStructure = From e In _context.ConceptAccountingStructure.Include("AccountingStructure").Include("AccountingStructure.FunctionalUnit")
        '                                 Where e.InterfazName = InterfaceName And e.AccountingStructure.FunctionalUnit.Where(Function(x) x.Id = FunctionalUnitId).FirstOrDefault().Id = FunctionalUnitId
        '               Select e

        'Return ConceptAccountingStructure.ToList()

        Dim ConceptAccountingStructure = From e In _context.ConceptAccountingStructure.Include("AccountingStructure").Include("AccountingStructure.FunctionalUnit")
                                         Where e.InterfazName = InterfaceName And e.AccountingStructure.FunctionalUnit.Any(Function(x) x.Id = FunctionalUnitId)
                                         Select e

        Return ConceptAccountingStructure.ToList()
    End Function

    Public Function GetAccountingStructureIntegrated(FunctionalUnitId As Integer) As List(Of ConceptAccountingStructure) Implements IConceptAccountingStructureRepository.GetAccountingStructureIntegrated

        Dim ConceptAccountingStructure = From e In _context.ConceptAccountingStructure.Include("AccountingStructure").Include("AccountingStructure.FunctionalUnit")
                                         Where e.AccountingStructure.FunctionalUnit.Any(Function(x) x.Id = FunctionalUnitId)
                                         Select e

        Return ConceptAccountingStructure.ToList()
    End Function


End Class
