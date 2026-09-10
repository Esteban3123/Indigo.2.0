'***********************************************************************
' Assembly         : Infrastructure.Data.MixinStationRepository
' Author           : Diego A. Roldan
' Created          : 2021-11-08
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.Data.Entity
Imports Domain.Base
Imports Domain.Entities
Imports Infrastructure.Data.Base

Public Class ReleaseLineRepository
    Inherits GenericRepository(Of ReleaseLine)
    Implements IReleaseLineRepository, Inject

    ''' <summary>
    ''' Contexto de Tipo de dosis unitaria
    ''' </summary>
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    ''' Inicia el contexto de Tipo de Dosis Unitaria
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    Public Function GetReleaseLineByCampaignDetailId(campaignDetailId As Integer, Optional tracking As Boolean = True) As ReleaseLine Implements IReleaseLineRepository.GetReleaseLineByCampaignDetailId

        Dim query = _context.ReleaseLine.Where(Function(m) m.CampaignDetailId = campaignDetailId)

        If Not tracking Then query = query.AsNoTracking()

        Dim release = query.FirstOrDefault()

        If release IsNot Nothing Then
            release.WorkingAreaCodeName = _context.WorkingArea.AsNoTracking().Where(Function(m) m.Id = release.WorkingAreaId).Select(Function(m) String.Concat(m.Code, " - ", m.Description)).FirstOrDefault()
        End If

        Return release
    End Function

End Class