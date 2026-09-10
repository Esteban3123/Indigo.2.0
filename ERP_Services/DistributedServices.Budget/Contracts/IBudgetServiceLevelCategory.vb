'***********************************************************************
' Assembly         : DistributedServices.Budget
' Author           : Jhossept Kevin Garay
' Created          : 21-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel
Imports Domain.Entities
Imports Domain.Base.Entities

#End Region

<ServiceContract()>
Public Interface IBudgetServiceLevelCategory
    ''' <summary>
    ''' Obtiene un nivel de categoria
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetLevelCategory(ByVal level As String, audit As AuditMessage) As LevelCategory

    ''' <summary>
    ''' Obtiene un nivel de categoria
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function ListLevelsCategory(audit As AuditMessage) As List(Of LevelCategory)

    ''' <summary>
    ''' Elimina un nivel de categoria
    ''' </summary>
    ''' <param name="LevelCategory">La entidad</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">company Vacio</exception>
    <OperationContract()>
    Function DeleteLevelCategory(LevelCategory As LevelCategory, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Guarda o Actualiza un nivel de categoria
    ''' </summary>
    ''' <param name="LevelCategory">la entidad</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">entidad Vacio</exception>
    <OperationContract()>
    Function SaveLevelCategory(LevelsCategory As List(Of LevelCategory), audit As AuditMessage) As ActionResult(Of List(Of LevelCategory))

End Interface
