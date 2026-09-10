'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 24-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.IOC
Imports Application.Payroll
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Partial Class PayrollService

    Implements IPayrollConceptAccountingStructure

    ''' <summary>
    ''' Función para Cargar los Conceptos con Estructura Contable por Interfaz y Id del Código
    ''' </summary>
    ''' <param name="InterfaceName">Nombre Interfaz</param>
    ''' <param name="ConceptId">Id Concepto</param>
    ''' <returns>Lista de ConceptAccountingStructure</returns>
    ''' <remarks></remarks>
    Public Function GetAccountingStructureByInterfaceConceptId(InterfaceName As String, ConceptId As Integer, session As SessionValues) As List(Of ConceptAccountingStructure) Implements IPayrollConceptAccountingStructure.GetAccountingStructureByInterfaceConceptId
        Using ConceptAccountingStructureAdmin As IConceptAccountingStructureAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IConceptAccountingStructureAdminService)()
            Return ConceptAccountingStructureAdmin.GetAccountingStructureByInterfaceConceptId(InterfaceName, ConceptId)
        End Using
    End Function
End Class
