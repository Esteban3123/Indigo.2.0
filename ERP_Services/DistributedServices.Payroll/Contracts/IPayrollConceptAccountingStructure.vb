'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 24-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

<ServiceContract()> _
Public Interface IPayrollConceptAccountingStructure

    ''' <summary>
    ''' Función para Cargar los Conceptos con Estructura Contable por Interfaz y Id del Código
    ''' </summary>
    ''' <param name="InterfaceName">Nombre Interfaz</param>
    ''' <param name="ConceptId">Id Concepto</param>
    ''' <returns>Lista de ConceptAccountingStructure</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetAccountingStructureByInterfaceConceptId(ByVal InterfaceName As String, ConceptId As Integer, session As SessionValues) As List(Of ConceptAccountingStructure)

End Interface
