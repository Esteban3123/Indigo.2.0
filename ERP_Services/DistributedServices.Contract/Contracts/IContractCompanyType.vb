'***********************************************************************
' Assembly         : DistributedServices.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 26/11/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()>
Public Interface IContractCompanyType

    ''' <summary>
    ''' Guarda o Actualiza un grupo de atencion
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveCompanyType(CompanyType As Domain.Entities.CompanyType, idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CompanyType)

    ''' <summary>
    ''' Elimina un grupo de atencion
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteCompanyType(Company As Domain.Entities.CompanyType, audit As AuditMessage) As Domain.Base.Entities.ActionResult

    ''' <summary>
    ''' Obtiene un determinado grupo de atencion por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetCompanyTypeByCode(code As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CompanyType)

    ''' <summary>
    ''' Obtiene un determinado grupo de atencion por id
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetCompanyTypeById(id As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CompanyType)

    ''' <summary>
    ''' Obtiene todo el grupo de atencion
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetAllCompanyType(ByVal audit As AuditMessage) As List(Of Domain.Entities.CompanyType)

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ChangeStateCompanyType(code As String, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CompanyType)

End Interface
