'***********************************************************************
' Assembly         : Infrastructure.Data.ModelRepository
' Author           : Juan Carlos Bermudez
' Created          : 02-09-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Domain.Base.Entities

#End Region

Public Class CommitmentDetailRepository
    Inherits GenericRepository(Of CommitmentDetail)
    Implements ICommitmentDetailRepository

    
#Region "Properties"

    'Contexto de Budget
    Private _context As IGlobalModelUnitOfWork

#End Region

#Region "Buider"
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub
#End Region

    ''' <summary>
    ''' obtiene un detalle por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetCommitmentDetailById(id As Integer) As CommitmentDetail Implements ICommitmentDetailRepository.GetCommitmentDetailById
        Return (From cd In _context.CommitmentDetail Where cd.Id = id Select cd).FirstOrDefault()
    End Function
End Class
