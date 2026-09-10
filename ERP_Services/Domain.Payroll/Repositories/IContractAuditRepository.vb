'***********************************************************************
' Assembly         : Domain.Payroll
' Author           : Mariana Gonzalez Calderon
' Created          : 01/12/2025
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Payroll.Entities

''' <summary>
''' Interfaz del repositorio de auditoría de contratos
''' </summary>
Public Interface IContractAuditRepository
    Inherits IRepository(Of ContractAudit)

End Interface

