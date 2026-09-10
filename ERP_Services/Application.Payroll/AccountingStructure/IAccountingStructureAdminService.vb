'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 22-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IAccountingStructureAdminService
    Inherits IDisposable
    ''' <summary>
    ''' Obtiene la Estructura Contable de Nómina x Código
    ''' </summary>
    ''' <param name="code">Código Accounting Structure</param>
    ''' <param name="desatach"></param>
    ''' <returns>AccountingStructure</returns>
    ''' <remarks></remarks>
    Function GetAccountingStructure(code As String, Optional desatach As Boolean = True) As AccountingStructure

    ''' <summary>
    ''' Obtiene la Estructura Contable de Nómina x Id
    ''' </summary>
    ''' <param name="accountingStructureId">Id Accounting Structure</param>
    ''' <returns>AccountingStructure</returns>
    ''' <remarks></remarks>
    Function GetAccountingStructureById(accountingStructureId As Integer, Optional desatach As Boolean = True) As AccountingStructure

    ''' <summary>
    ''' Almacena una Estructura Contable de Nómina
    ''' </summary>
    ''' <param name="accountingStructure">accountingStructure</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function SaveAccountingStructure(ByVal accountingStructure As AccountingStructure, ByVal audit As AuditMessage) As Boolean

    ''' <summary>
    ''' Elimina una Estructura Contable de Nómina
    ''' </summary>
    ''' <param name="accountingStructure">accountingStructure</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function DeleteAccountingStructure(ByVal accountingStructure As AccountingStructure, ByVal audit As AuditMessage) As ActionMessageResult(Of AccountingStructure)

    ''' <summary>
    ''' Lista Toda las Estructuras Contables
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListAllAccountingStructure() As List(Of AccountingStructure)

End Interface
