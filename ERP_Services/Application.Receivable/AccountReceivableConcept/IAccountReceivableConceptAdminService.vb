'***********************************************************************
' Assembly         : Application.Portfolio
' Author           : Carlos Ernesto Cordoba
' Created          : 01-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
#End Region

Public Interface IAccountReceivableConceptAdminService
    Inherits IDisposable
#Region "Methods"
    ''' <summary>
    ''' metodo para obtener todos los conceptos de cuentas por pagar
    ''' </summary>
    ''' <returns></returns>
    Function GetAllAccountReceivableConcept(ByVal audit As AuditMessage)

    ''' <summary>
    ''' metodo para obtener un concepto de cuenta por pagar
    ''' </summary>
    ''' <param name="code">codigo</param>
    ''' <returns></returns>
    Function GetAccountReceivableConceptByCode(ByVal code As String, ByVal audit As AuditMessage)

    ''' <summary>
    ''' Obtiene un concepto de cuenta x cobrar por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAccountReceivableConceptById(id As Integer) As AccountReceivableConcept

    ''' <summary>
    ''' metodo para guardar un concepto de cuenta por cobrar
    ''' </summary>    
    Function SaveAccountReceivableConcept(ByVal accountReceivableConcept As AccountReceivableConcept, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of AccountReceivableConcept)

    ''' <summary>
    ''' metodo para eliminar un concepto de cuenta por cobrar
    ''' </summary>    
    Function DeleteAccountReceivableConcept(ByVal accountReceivableConcept As AccountReceivableConcept, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Metodo para cambiar el estado de la entidad
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <returns></returns>
    Function ChangeState(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of AccountReceivableConcept)
#End Region
End Interface
