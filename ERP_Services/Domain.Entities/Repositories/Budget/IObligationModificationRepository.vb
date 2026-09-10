'***********************************************************************
' Assembly         : Domain.Budget
' Author           : Carlos Mario Arias Rubiano
' Created          : 07/09/2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities

Public Interface IObligationModificationRepository
    Inherits IRepository(Of ObligationModification)

    ''' <summary>
    ''' Obtiene una modificacion de obligacion por codigo
    ''' </summary>
    '''<param name="Code"></param>
    ''' <returns></returns>
    Function GetObligationModification(Code As String, validityId As Integer) As ObligationModification

    ''' <summary>
    ''' Obtiene una modificacion de obligacion por id
    ''' </summary>
    '''<param name="Id"></param>
    ''' <returns></returns>
    Function GetObligationModificationById(Id As Integer) As ObligationModification

    ''' <summary>
    ''' Guarda la modificacion
    ''' </summary>
    ''' <param name="ObligationModificationXml"></param>
    ''' <param name="ObligationModificationDetailForDeleteXml"></param>
    ''' <param name="CodeUser"></param>
    ''' <returns></returns>
    Function SP_SaveObligationModification(ObligationModificationXml As String, ObligationModificationDetailForDeleteXml As String, CodeUser As String) As SP_SaveObligationModification_Result

End Interface
