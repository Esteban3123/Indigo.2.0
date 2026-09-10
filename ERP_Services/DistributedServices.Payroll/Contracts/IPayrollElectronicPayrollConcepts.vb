'***********************************************************************
' Assembly         : DistributedServices.Common
' Author           : Andres Felipe Quintero Garcia
' Created          : 10-02-2025
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Base.Entities
Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
#End Region

<ServiceContract()>
Public Interface IPayrollElectronicPayrollConcepts

    ''' <summary>
    ''' Obtiene un Conceptos de nómina electrónica
    ''' </summary>
    ''' <param name="code">Codigo Conceptos de nómina electrónica</param>
    ''' <returns>Conceptos de nómina electrónica</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetElectronicPayrollConceptsByCode(code As String, ByVal session As SessionValues) As ElectronicPayrollConcepts

    ''' <summary>
    ''' Lista todos los Conceptos de nómina electrónica
    ''' </summary>
    ''' <returns>Lista de Conceptos de nómina electrónica</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ListAllElectronicPayrollConcepts(session As SessionValues) As List(Of ElectronicPayrollConcepts)

    ''' <summary>
    ''' Graba o actualiza los Conceptos de nómina electrónica
    ''' </summary>
    ''' <param name="electronicPayrollConcepts">Conceptos de nómina electrónica</param>
    ''' <param name="audit">Objeto auditoria</param>
    ''' <returns>True o False</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function SaveElectronicPayrollConcepts(ByVal electronicPayrollConcepts As ElectronicPayrollConcepts, session As SessionValues, idSequense As Int64, audit As AuditMessage) As ActionResult(Of ElectronicPayrollConcepts)

    ''' <summary>
    ''' Elimina Conceptos de nómina electrónica
    ''' </summary>
    ''' <param name="electronicPayrollConcepts">Conceptos de nómina electrónica</param>
    ''' <param name="audit">Objeto auditoria</param>
    ''' <returns>True o False</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function DeleteElectronicPayrollConcepts(ByVal electronicPayrollConcepts As ElectronicPayrollConcepts, ByVal session As SessionValues, ByVal audit As AuditMessage) As ActionResult
    <OperationContract()>
    Function ChangeStateElectronicPayrollConcepts(ByVal code As String, ByVal state As Boolean, session As SessionValues, audit As AuditMessage) As ActionResult(Of ElectronicPayrollConcepts)

End Interface
