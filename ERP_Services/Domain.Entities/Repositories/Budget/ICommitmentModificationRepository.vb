'***********************************************************************
' Assembly         : Domain.Inventory
' Author           : Juan Carlos Bermudez
' Created          : 05-09-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Base
Imports Domain.Entities
Imports Domain.Base.Entities

#End Region

Public Interface ICommitmentModificationRepository
    Inherits IRepository(Of CommitmentModification)

    ''' <summary>
    ''' obtiene una modificacion de compromiso por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetCommitmentModificationByCode(code As String, BudgetaryValidityId As Integer) As CommitmentModification

    ''' <summary>
    ''' obtiene una modificacion de compromiso por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetCommitmentModificationById(id As Integer) As CommitmentModification

    ''' <summary>
    ''' Guarda la modificacion
    ''' </summary>
    ''' <param name="CommitmentModificationXml"></param>
    ''' <param name="CommitmentModificationDetailForDeleteXml"></param>
    ''' <param name="CodeUser"></param>
    ''' <returns></returns>
    Function SP_SaveCommitmentModification(CommitmentModificationXml As String, CommitmentModificationDetailForDeleteXml As String, CodeUser As String) As SP_SaveCommitmentModification_Result

End Interface
