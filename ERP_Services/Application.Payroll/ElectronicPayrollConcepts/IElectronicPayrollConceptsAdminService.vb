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

Public Interface IElectronicPayrollConceptsAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Lista todos los Conceptos de nómina electrónica
    ''' </summary>
    ''' <returns>Lista de Conceptos de nómina electrónica</returns>
    ''' <remarks></remarks>
    Function ListAllElectronicPayrollConcepts() As List(Of ElectronicPayrollConcepts)

    ''' <summary>
    ''' Obtiene un Conceptos de nómina electrónica
    ''' </summary>
    ''' <param name="code">Codigo Conceptos de nómina electrónica</param>
    ''' <returns>Conceptos de nómina electrónica</returns>
    ''' <remarks></remarks>
    Function GetElectronicPayrollConceptsByCode(code As String) As ElectronicPayrollConcepts

    ''' <summary>
    ''' Graba o actualiza los Conceptos de nómina electrónica
    ''' </summary>
    ''' <param name="electronicPayrollConcepts">Conceptos de nómina electrónica</param>
    ''' <param name="audit">Objeto auditoria</param>
    ''' <returns>True o False</returns>
    ''' <remarks></remarks>
    Function SaveElectronicPayrollConcepts(ByVal electronicPayrollConcepts As ElectronicPayrollConcepts, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of ElectronicPayrollConcepts)

    ''' <summary>
    ''' Elimina Conceptos de nómina electrónica
    ''' </summary>
    ''' <param name="electronicPayrollConcepts">Conceptos de nómina electrónica</param>
    ''' <param name="audit">Objeto auditoria</param>
    ''' <returns>True o False</returns>
    ''' <remarks></remarks>
    Function DeleteElectronicPayrollConcepts(ByVal electronicPayrollConcepts As ElectronicPayrollConcepts, ByVal audit As AuditMessage) As ActionResult
    Function ChangeStateElectronicPayrollConcepts(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of ElectronicPayrollConcepts)
End Interface
