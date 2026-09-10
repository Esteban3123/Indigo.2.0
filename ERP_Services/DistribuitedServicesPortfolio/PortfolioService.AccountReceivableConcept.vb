'***********************************************************************
' Assembly         : DistributedServices.Portfolio
' Author           : Carlos Ernesto Cordoba
' Created          : 01-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.IOC
Imports Application.Portfolio
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports Microsoft.Practices.Unity
#End Region

Partial Class PortfolioService

#Region "Methods"
    ''' <summary>
    ''' metodo para eliminar un concepto de cuenta por cobrar
    ''' </summary>
    ''' <param name="accountReceivableConcept">The portfolio concept.</param>
    ''' <returns></returns>
    Public Function DeleteAccountReceivableConcept(accountReceivableConcept As AccountReceivableConcept, audit As AuditMessage) As ActionResult Implements IAccountReceivableConcept.DeleteAccountReceivableConcept
        Using service As IAccountReceivableConceptAdminService = Container.Current.Resolve(Of IAccountReceivableConceptAdminService)()
            Return service.DeleteAccountReceivableConcept(accountReceivableConcept, audit)
        End Using
        'Return Me._accountReceivableConceptAdminService.DeleteAccountReceivableConcept(accountReceivableConcept, audit)
    End Function

    ''' <summary>
    ''' metodo para obtener todos los conceptos de cuentas por cobrar
    ''' </summary>
    ''' <returns></returns>
    Public Function GetAllAccountReceivableConcept(audit As AuditMessage) As Object Implements IAccountReceivableConcept.GetAllAccountReceivableConcept
        Using service As IAccountReceivableConceptAdminService = Container.Current.Resolve(Of IAccountReceivableConceptAdminService)()
            Return service.GetAllAccountReceivableConcept(audit)
        End Using
        'Return Me._accountReceivableConceptAdminService.GetAllAccountReceivableConcept(audit)
    End Function

    ''' <summary>
    ''' Obtiene un documento de cuenta x cobrar por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAccountReceivableConceptById(id As Integer) As Domain.Entities.AccountReceivableConcept Implements IAccountReceivableConcept.GetAccountReceivableConceptById
        Using service As IAccountReceivableConceptAdminService = Container.Current.Resolve(Of IAccountReceivableConceptAdminService)()
            Return service.GetAccountReceivableConceptById(id)
        End Using
        'Return _accountReceivableConceptAdminService.GetAccountReceivableConceptById(id)
    End Function

    ''' <summary>
    ''' metodo para obtener un concepto de cuentas por cobrar
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Public Function GetAccountReceivableConceptByCode(code As String, audit As AuditMessage) As Object Implements IAccountReceivableConcept.GetAccountReceivableConceptByCode
        Using service As IAccountReceivableConceptAdminService = Container.Current.Resolve(Of IAccountReceivableConceptAdminService)()
            Return service.GetAccountReceivableConceptByCode(code, audit)
        End Using
        'Return Me._accountReceivableConceptAdminService.GetAccountReceivableConceptByCode(code, audit)
    End Function

    ''' <summary>
    ''' metodo para guardar un concepto de cuenta por cobrar
    ''' </summary>
    ''' <param name="accountReceivableConcept">The portfolio concept.</param>
    ''' <returns></returns>
    Public Function SaveAccountReceivableConcept(accountReceivableConcept As AccountReceivableConcept, idSequense As Int64, audit As AuditMessage) As ActionResult(Of AccountReceivableConcept) Implements IAccountReceivableConcept.SaveAccountReceivableConcept
        Using service As IAccountReceivableConceptAdminService = Container.Current.Resolve(Of IAccountReceivableConceptAdminService)()
            Return service.SaveAccountReceivableConcept(accountReceivableConcept, audit, idSequense)
        End Using
        'Return Me._accountReceivableConceptAdminService.SaveAccountReceivableConcept(accountReceivableConcept, audit, idSequense)
    End Function

    ''' <summary>
    ''' Metodo para cambiar el estado de la entidad
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <returns></returns>
    Public Function ChangeState(code As String, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.AccountReceivableConcept) Implements IAccountReceivableConcept.ChangeState
        Using service As IAccountReceivableConceptAdminService = Container.Current.Resolve(Of IAccountReceivableConceptAdminService)()
            Return service.ChangeState(code, state, audit)
        End Using
        'Return Me._accountReceivableConceptAdminService.ChangeState(code, state, audit)
    End Function
#End Region

End Class
