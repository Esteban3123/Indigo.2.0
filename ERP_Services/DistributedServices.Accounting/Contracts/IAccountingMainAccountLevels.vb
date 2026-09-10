'***********************************************************************
' Assembly         : DistributedServices.Payments
' Author           : Pablo Alexander Salazar Sanchez
' Created          : 16/12/2022
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.ServiceModel
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Entities
#End Region

<ServiceContract()>
Public Interface IAccountingMainAccountLevels

#Region "Methods"

    ''' <summary>
    ''' Obtiene todo el grupo de atencion
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetAllMainAccountLevels(audit As AuditMessage) As List(Of MainAccountLevels)

    ''' <summary>
    ''' Guarda o Actualiza el Nivel de Cuenta Contable
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveMainAccountLevels(MainAccountLevels As MainAccountLevels, idSequense As Int64, audit As AuditMessage) As ActionResult(Of MainAccountLevels)

    ''' <summary>
    ''' Elimina el Nivel de Cuenta Contable
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteMainAccountLevels(MainAccountLevels As MainAccountLevels, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene el Nivel de Cuenta Contable por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetMainAccountLevelsByCode(code As String, audit As AuditMessage) As ActionResult(Of MainAccountLevels)

    ''' <summary>
    ''' Obtiene un determinado grupo de atencion por id
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetMainAccountLevelsById(id As Integer, audit As AuditMessage) As ActionResult(Of MainAccountLevels)

#End Region

End Interface
