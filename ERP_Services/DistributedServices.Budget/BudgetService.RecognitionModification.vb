'***********************************************************************
' Assembly         : Application.Budget
' Author           : Juan Carlos Bermudez
' Created          : 28-08-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Application.Budget
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity

#End Region

Partial Public Class BudgetService

    ''' <summary>
    ''' obtiene un traslado del pac por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    Public Function GetRecognitionModificationByCode(code As String, budgetaryValidityId As Integer, audit As AuditMessage) As Domain.Entities.RecognitionModification Implements IBudgetServiceRecognitionModification.GetRecognitionModificationByCode
        Using service As IRecognitionModificationAdminService = Container.Current.Resolve(Of IRecognitionModificationAdminService)()
            Return service.GetRecognitionModificationByCode(code, budgetaryValidityId, audit)
        End Using
    End Function

    ''' <summary>
    ''' obtiene un traslado del pac por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetRecognitionModificationById(id As Integer) As Domain.Entities.RecognitionModification Implements IBudgetServiceRecognitionModification.GetRecognitionModificationById
        Using service As IRecognitionModificationAdminService = Container.Current.Resolve(Of IRecognitionModificationAdminService)()
            Return service.GetRecognitionModificationById(id)
        End Using
    End Function

    ''' <summary>
    ''' Guarda una modificacion de presupuesto
    ''' </summary>
    ''' <param name="recognitionModification"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveRecognitionModification(recognitionModification As RecognitionModification, listModificationDetailDelete As List(Of Integer), audit As AuditMessage) As ActionResult(Of RecognitionModification) Implements IBudgetServiceRecognitionModification.SaveRecognitionModification
        Using service As IRecognitionModificationAdminService = Container.Current.Resolve(Of IRecognitionModificationAdminService)()
            Return service.SaveRecognitionModification(recognitionModification, listModificationDetailDelete, audit)
        End Using
    End Function

End Class