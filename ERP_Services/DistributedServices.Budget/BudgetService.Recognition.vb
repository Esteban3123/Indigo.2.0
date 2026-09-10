'***********************************************************************
' Assembly         : DistributedServices.Budget
' Author           : Jeisson Herrera Peña
' Created          : 25-08-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Application.Budget
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity

#End Region

Partial Class BudgetService

#Region "Methods"

    ''' <summary>
    ''' Elimina un reconocimiento
    ''' </summary>
    ''' <param name="recognition"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteRecognition(recognition As Domain.Entities.Recognition, audit As AuditMessage) As Domain.Base.Entities.ActionResult Implements IBudgetServiceRecognition.DeleteRecognition
        Using service As IRecognitionAdminService = Container.Current.Resolve(Of IRecognitionAdminService)()
            Return service.DeleteRecognition(recognition, audit)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene el reconocimiento por codigo
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <param name="ItemType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetRecognition(Code As String, ItemType As Byte, budgetaryValidityId As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.Recognition) Implements IBudgetServiceRecognition.GetRecognition
        Using service As IRecognitionAdminService = Container.Current.Resolve(Of IRecognitionAdminService)()
            Return service.GetRecognition(Code.Trim(), ItemType, budgetaryValidityId, audit)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene el reconocimiento por Id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetRecognitionById(Id As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.Recognition) Implements IBudgetServiceRecognition.GetRecognitionById
        Using service As IRecognitionAdminService = Container.Current.Resolve(Of IRecognitionAdminService)()
            Return service.GetRecognitionById(Id, audit)
        End Using
    End Function

    ''' <summary>
    ''' Guarda o actualiza el reconocimiento
    ''' </summary>
    ''' <param name="recognition"></param>
    ''' <param name="listDetailsForDelete"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveRecognition(recognition As Domain.Entities.Recognition, listDetailsForDelete As List(Of Integer), audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.Recognition) Implements IBudgetServiceRecognition.SaveRecognition
        Using service As IRecognitionAdminService = Container.Current.Resolve(Of IRecognitionAdminService)()
            Return service.SaveRecognition(recognition, listDetailsForDelete, audit)
        End Using
    End Function

#End Region

End Class