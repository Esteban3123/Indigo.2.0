'***********************************************************************
' Assembly         : Domain.Inventory
' Author           : Juan Carlos Bermudez
' Created          : 02-09-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Base
Imports Domain.Entities
Imports Domain.Base.Entities

#End Region

Public Interface ICommitmentRepository
    Inherits IRepository(Of Commitment)

    ''' <summary>
    ''' obtiene un compromiso por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetCommitmentByCode(code As String, BudgetaryValidityId As Integer) As Commitment

    ''' <summary>
    ''' obtiene un compromiso por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetCommitmentById(id As Integer) As Commitment

    ''' <summary>
    ''' Guarda la obligacion
    ''' </summary>
    ''' <param name="CommitmentXml"></param>
    ''' <param name="CommitmentDetailForDeleteXml"></param>
    ''' <param name="CodeUser"></param>
    ''' <returns></returns>
    Function SP_SaveCommitment(CommitmentXml As String, CommitmentDetailForDeleteXml As String, CodeUser As String) As SP_SaveCommitment_Result

End Interface
