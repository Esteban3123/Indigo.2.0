'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 22-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

<ServiceContract()> _
Public Interface IPayrollAccountingStructure

    ''' <summary>
    ''' Elimina una Estructura Contable de Nómina
    ''' </summary>
    ''' <param name="accountingStructure">accountingStructure</param>
    ''' <param name="session">Objeto session</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function DeleteAccountingStructure(accountingStructure As AccountingStructure, session As SessionValues) As ActionMessageResult(Of AccountingStructure)

    ''' <summary>
    ''' Obtiene la Estructura Contable de Nómina x Código
    ''' </summary>
    ''' <param name="code">Código Accounting Structure</param>
    ''' <param name="session">Objeto Session</param>
    ''' <param name="desatach"></param>
    ''' <returns>AccountingStructure</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetAccountingStructure(code As String, session As SessionValues) As AccountingStructure

    ''' <summary>
    ''' Obtiene la Estructura Contable de Nómina x Id
    ''' </summary>
    ''' <param name="accountingStructureId">Id Accounting Structure</param>
    ''' <param name="session">Objeto session</param>
    ''' <returns>AccountingStructure</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetAccountingStructureById(accountingStructureId As Integer, session As SessionValues) As AccountingStructure

    ''' <summary>
    ''' Almacena una Estructura Contable de Nómina
    ''' </summary>
    ''' <param name="accountingStructure">accountingStructure</param>
    ''' <param name="session">Objeto session</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function SaveAccountingStructure(accountingStructure As AccountingStructure, session As SessionValues) As Boolean

    ''' <summary>
    ''' Lista Toda las Estructuras Contables
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function ListAllAccountingStructure(session As SessionValues) As List(Of AccountingStructure)

End Interface
