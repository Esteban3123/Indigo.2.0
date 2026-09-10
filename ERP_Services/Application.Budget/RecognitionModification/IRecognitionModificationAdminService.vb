'***********************************************************************
' Assembly         : Application.Budget
' Author           : Juan Carlos Bermudez
' Created          : 28-08-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region

Public Interface IRecognitionModificationAdminService
    Inherits IDisposable

    ''' <summary>
    ''' obtiene un traslado del pac por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetRecognitionModificationByCode(code As String, budgetaryValidityId As Integer, audit As AuditMessage) As RecognitionModification
    ''' <summary>
    ''' obtiene un traslado del pac por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetRecognitionModificationById(id As Integer) As RecognitionModification
    ''' <summary>
    ''' Guarda un traslado del pac
    ''' </summary>
    ''' <param name="recognitionModification"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveRecognitionModification(recognitionModification As RecognitionModification, listModificationDetailDelete As List(Of Integer), audit As AuditMessage) As ActionResult(Of RecognitionModification)

End Interface
