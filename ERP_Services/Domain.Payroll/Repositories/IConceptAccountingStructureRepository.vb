'***********************************************************************
' Assembly         : Domain.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 24-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Payroll.Entities

Public Interface IConceptAccountingStructureRepository
    Inherits IRepository(Of ConceptAccountingStructure)

    ''' <summary>
    ''' Función para Cargar los Conceptos con Estructura Contable por Interfaz y Id del Código
    ''' </summary>
    ''' <param name="InterfaceName">Nombre Interfaz</param>
    ''' <param name="ConceptId">Id Concepto</param>
    ''' <returns>Lista de ConceptAccountingStructure</returns>
    ''' <remarks></remarks>
    Function GetAccountingStructureByInterfaceConceptId(ByVal InterfaceName As String, ConceptId As Integer) As List(Of ConceptAccountingStructure)

    ''' <summary>
    ''' Función para Cargar los Conceptos con Estructura Contable por Interfaz
    ''' </summary>
    ''' <param name="InterfaceName">Nombre Interfaz</param>
    ''' <returns>Lista de ConceptAccountingStructure</returns>
    ''' <remarks></remarks>
    Function GetAccountingStructureByInterfaceName(InterfaceName As String, FunctionalUnitId As Integer) As List(Of ConceptAccountingStructure)

    Function GetAccountingStructureIntegrated(FunctionalUnitId As Integer) As List(Of ConceptAccountingStructure)

End Interface
