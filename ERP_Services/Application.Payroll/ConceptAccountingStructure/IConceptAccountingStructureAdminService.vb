'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 24-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IConceptAccountingStructureAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Función para Cargar los Conceptos con Estructura Contable por Interfaz y Id del Código
    ''' </summary>
    ''' <param name="InterfaceName">Nombre Interfaz</param>
    ''' <param name="ConceptId">Id Concepto</param>
    ''' <returns>Lista de ConceptAccountingStructure</returns>
    ''' <remarks></remarks>
    Function GetAccountingStructureByInterfaceConceptId(ByVal InterfaceName As String, ConceptId As Integer) As List(Of ConceptAccountingStructure)

End Interface
