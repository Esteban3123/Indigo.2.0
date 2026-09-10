'***********************************************************************
' Assembly         : Infrastructure.Data.ModelRepository
' Author           : Juan Carlos Bermudez
' Created          : 15-09-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Domain.Base.Entities

#End Region

Public Class AvailabilityDetailRepository
    Inherits GenericRepository(Of AvailabilityDetail)
    Implements IAvailabilityDetailRepository

    'Contexto de Budget
    Private _context As IGlobalModelUnitOfWork

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
    Public Function GetAvailabilityDetailById(id As Integer, Optional FlagTracking As Boolean = False) As AvailabilityDetail Implements IAvailabilityDetailRepository.GetAvailabilityDetailById
        If FlagTracking = True Then
            Return (From ad In _context.AvailabilityDetail.AsNoTracking() Where ad.Id = id Select ad).FirstOrDefault()
        Else
            Return (From ad In _context.AvailabilityDetail Where ad.Id = id Select ad).FirstOrDefault()
        End If
    End Function

#End Region

End Class
