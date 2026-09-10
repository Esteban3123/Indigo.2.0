'***********************************************************************
' Assembly         : Domain.Payroll.Entities
' Author           : Jhon Willian Corredor Araujo
' Created          : 03-09-2026
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities

''' <summary>
''' Construye los registros de auditoria de los cambios no contractuales
''' aplicados sobre un empleado, sus contratos y sus datos personales.
''' </summary>
Public Interface IEmployeeAuditDomain

    ''' <summary>
    ''' Compara la foto original del empleado contra el grafo modificado y devuelve
    ''' un registro de auditoria por cada campo cuyo valor cambio.
    ''' </summary>
    ''' <param name="originalEmployee">Empleado tal como esta almacenado antes del guardado</param>
    ''' <param name="modifiedEmployee">Empleado con los cambios propuestos</param>
    ''' <param name="userCode">Codigo del usuario que realiza la modificacion</param>
    ''' <param name="anchorContractId">Contrato al que se asocian los cambios de empleado y persona</param>
    ''' <param name="catalogResolver">Traductor de identificadores de catalogo a texto legible</param>
    ''' <returns>Lista de registros listos para persistir; vacia si no hubo cambios auditables</returns>
    Function BuildAuditEntries(originalEmployee As Employee,
                               modifiedEmployee As Employee,
                               userCode As String,
                               anchorContractId As Integer,
                               catalogResolver As IAuditCatalogResolver) As List(Of ContractAudit)

End Interface
