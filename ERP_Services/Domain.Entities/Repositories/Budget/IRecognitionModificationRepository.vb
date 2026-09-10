'***********************************************************************
' Assembly         : Domain.Inventory
' Author           : Juan Carlos Bermudez
' Created          : 26-08-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Base
Imports Domain.Entities
Imports Domain.Base.Entities

#End Region

Public Interface IRecognitionModificationRepository
    Inherits IRepository(Of RecognitionModification)

    ''' <summary>
    ''' obtiene un reconocimiento modificacion por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetRecognitionModificationByCode(code As String, budgetaryValidityId As Integer) As RecognitionModification
    ''' <summary>
    ''' obtiene un reconocimiento modificacion por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetRecognitionModificationById(id As Integer) As RecognitionModification

    ''' <summary>
    ''' Guarda la modificacion
    ''' </summary>
    ''' <param name="RecognitionModificationXml"></param>
    ''' <param name="RecognitionModificationDetailForDeleteXml"></param>
    ''' <param name="CodeUser"></param>
    ''' <returns></returns>
    Function SP_SaveRecognitionModification(RecognitionModificationXml As String, RecognitionModificationDetailForDeleteXml As String, CodeUser As String) As SP_SaveRecognitionModification_Result

End Interface
