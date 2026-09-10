'************************************************************
' Assembly         : Domain.Contract
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 11/07/2017
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Domain.Base
#End Region


Public Interface IGroupersRepository
    Inherits IRepository(Of Groupers)

    ''' <summary>
    ''' Obtiene un rango de uvr por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetGroupers(code As String) As Groupers

    Function GetGroupersPOCO(code As String) As Groupers

    ''' <summary>
    ''' Obtiene un rango de uvr por id
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetGroupersById(id As Integer) As Groupers

    ''' <summary>
    ''' Verifica si el id enviado corresponde con un grouper padre
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Function ValidateIfGrouperIsParent(id As Integer) As Boolean

    Function GetListGroupersPOCO(listGrouperCode As List(Of String)) As List(Of Groupers)

End Interface
