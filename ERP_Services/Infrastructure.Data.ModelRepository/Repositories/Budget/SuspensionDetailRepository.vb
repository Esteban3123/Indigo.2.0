'***********************************************************************
' Assembly         : Infrastructure.Data.ModelRepository
' Author           : Juan Carlos Bermudez
' Created          : 19-09-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Domain.Base.Entities

#End Region

Public Class SuspensionDetailRepository
    Inherits GenericRepository(Of SuspensionDetail)
    Implements ISuspensionDetailRepository

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

#Region "Methods"

    ''' <summary>
    ''' Obtiene un detalle por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSuspensionDetailById(id As Integer, Optional flagTracking As Boolean = True) As SuspensionDetail Implements ISuspensionDetailRepository.GetSuspensionDetailById
        If flagTracking = True Then
            Return (From sd In _context.SuspensionDetail Where sd.Id = id Select sd).FirstOrDefault()
        Else
            Return (From sd In _context.SuspensionDetail.AsNoTracking() Where sd.Id = id Select sd).FirstOrDefault()
        End If

    End Function

#End Region

End Class
