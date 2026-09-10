'***********************************************************************
' Assembly         : DistributedServices.Budget
' Author           : Juan Carlos Bermudez  
' Created          : 28-08-2015
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
Public Interface IBudgetServiceRecognitionModification

    ''' <summary>
    ''' obtiene una modificacion de reconocimiento por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetRecognitionModificationByCode(code As String, budgetaryValidityId As Integer, audit As AuditMessage) As Domain.Entities.RecognitionModification
    ''' <summary>
    ''' obtiene una modificacion de reconocimiento por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetRecognitionModificationById(id As Integer) As RecognitionModification

    ''' <summary>
    ''' Guarda una modificacion de reconocimiento
    ''' </summary>
    ''' <param name="recognitionModification"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function SaveRecognitionModification(recognitionModification As RecognitionModification, listModificationDetailDelete As List(Of Integer), audit As AuditMessage) As ActionResult(Of RecognitionModification)

End Interface
