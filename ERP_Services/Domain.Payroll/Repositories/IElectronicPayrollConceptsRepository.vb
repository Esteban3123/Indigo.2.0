'***********************************************************************
' Assembly         : DistributedServices.Common
' Author           : Andres Felipe Quintero Garcia
' Created          : 10-02-2025
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Base
Imports Domain.Payroll.Entities
#End Region

Public Interface IElectronicPayrollConceptsRepository
    Inherits IRepository(Of ElectronicPayrollConcepts)

    ''' <summary>
    ''' Obtiene el Conceptos de nómina electrónica
    ''' </summary>
    ''' <param name="code">Código de la Conceptos de nómina electrónica</param>
    ''' <param name="tracking">Tracking</param>
    ''' <returns>Conceptos de nómina electrónica</returns>
    ''' <remarks></remarks>
    Function GetElectronicPayrollConcepts(code As String, Optional tracking As Boolean = True) As ElectronicPayrollConcepts

    ''' <summary>
    ''' Lista todos los Conceptos de nómina electrónica
    ''' </summary>
    ''' <returns>Lista de Conceptos de nómina electrónica</returns>
    ''' <remarks></remarks>
    Function ListAllElectronicPayrollConcepts() As List(Of ElectronicPayrollConcepts)

End Interface
