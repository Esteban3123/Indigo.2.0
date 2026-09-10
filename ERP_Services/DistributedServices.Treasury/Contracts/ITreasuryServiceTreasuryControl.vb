'***********************************************************************
' Assembly         : DistributedServices.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 13-08-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()>
Public Interface ITreasuryServiceTreasuryControl

    ''' <summary>
    ''' Guarda un registro de control de los documentos de tesoreria
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveTreasuryControl(treasuryControl As TreasuryControl, audit As AuditMessage) As ActionResult(Of TreasuryControl)

    ''' <summary>
    ''' Elimina un registro de control de los documentos de tesoreria
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteTreasuryControl(treasuryControl As TreasuryControl, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene un registro de control de los documentos de tesoreria por id
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetTreasuryControlById(Id As Integer) As TreasuryControl

End Interface
