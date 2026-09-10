'************************************************************
' Assembly         : Infrastructure.Data.FixedAssetRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 17/05/2016
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Importar"
Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Domain.Base
Imports System.Data.Entity.Infrastructure

#End Region

Public Class FixedAssetTransferRepository
    Inherits GenericRepository(Of FixedAssetTransfer)
    Implements IFixedAssetTransferRepository

    'Devuelve el contexto en este repositorio 
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    '''inicializa la neva instancia d clase.
    ''' </summary>
    ''' <param name="contex">el contexto.</param>
    Public Sub New(ByVal contex As IGlobalModelUnitOfWork)
        MyBase.New(contex)
        _context = contex
    End Sub

    ''' <summary>
    ''' Obtiene el registro por código
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFixedAssetTransfer(code As String) As FixedAssetTransfer Implements IFixedAssetTransferRepository.GetFixedAssetTransfer
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim res = (From d In Me._context.FixedAssetTransfer.Include("FixedAssetTransferDetail") Where d.Code.Equals(code.Trim()) Select d).FirstOrDefault
        If res IsNot Nothing Then

            If res.SourceLocationId IsNot Nothing Then
                Dim location = (From l In _context.FixedAssetLocation.AsNoTracking Where l.Id = res.SourceLocationId Select l).FirstOrDefault
                res.SourceLocationCodeName = location.Code + " - " + location.Name
            End If

            If res.TargetLocationId IsNot Nothing Then
                Dim location = (From l In _context.FixedAssetLocation.AsNoTracking Where l.Id = res.TargetLocationId Select l).FirstOrDefault
                res.TargetLocationCodeName = location.Code + " - " + location.Name
            End If

            If res.SourceResponsibleId IsNot Nothing Then
                Dim responsible = (From r In _context.FixedAssetResponsible.AsNoTracking.Include("ThirdParty").AsNoTracking Where r.Id = res.SourceResponsibleId Select r).FirstOrDefault
                res.SourceResponsibleCodeName = responsible.Code + " - " + responsible.ThirdParty.Nit + " - " + responsible.ThirdParty.Name
            End If

            If res.TargetResponsibleId IsNot Nothing Then
                Dim responsible = (From r In _context.FixedAssetResponsible.AsNoTracking.Include("ThirdParty").AsNoTracking Where r.Id = res.TargetResponsibleId Select r).FirstOrDefault
                res.TargetResponsibleCodeName = responsible.Code + " - " + responsible.ThirdParty.Nit + " - " + responsible.ThirdParty.Name
            End If

            If res.FixedAssetTransferDetail IsNot Nothing AndAlso res.FixedAssetTransferDetail.Count > 0 Then
                For Each item In res.FixedAssetTransferDetail
                    Dim physical = (From p In _context.FixedAssetPhysicalAsset.AsNoTracking.Include("FixedAssetItem").AsNoTracking Where p.Id = item.PhysicalAssetId Select p).FirstOrDefault
                    item.ItemCodeName = physical.FixedAssetItem.Code + " - " + physical.FixedAssetItem.Description
                    item.Serie = physical.Serie
                    item.Plate = physical.Plate
                Next
            End If

            res.OriginalValue = (From d In Me._context.FixedAssetTransfer.AsNoTracking() Where d.Code.Equals(code.Trim()) Select d).SingleOrDefault()
            Return res
        Else
            Return New FixedAssetTransfer()
        End If
    End Function

    ''' <summary>
    ''' Obtiene el registro por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFixedAssetTransferById(Id As Integer) As FixedAssetTransfer Implements IFixedAssetTransferRepository.GetFixedAssetTransferById
        If Id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Dim res = (From d In Me._context.FixedAssetTransfer Where d.Id = Id Select d).FirstOrDefault
        If res IsNot Nothing Then
            res.OriginalValue = (From d In Me._context.FixedAssetTransfer.AsNoTracking() Where d.Id = Id Select d).SingleOrDefault()
            Return res
        Else
            Return New FixedAssetTransfer()
        End If
    End Function

    ''' <summary>
    ''' Obtiene el registro por id
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetListFixedAssetPhysicalAsset(ListInteger As List(Of Integer), Optional tracking As Boolean = True) As List(Of FixedAssetPhysicalAsset) Implements IFixedAssetTransferRepository.GetListFixedAssetPhysicalAsset
        If tracking Then
            Return (From p In _context.FixedAssetPhysicalAsset Where ListInteger.Contains(p.Id) Select p).ToList
        Else
            Return (From p In _context.FixedAssetPhysicalAsset.AsNoTracking().Include("MainAccounts").Include("FixedAssetItem.FixedAssetItemCatalog").AsNoTracking() Where ListInteger.Contains(p.Id) Select p).ToList
        End If
    End Function

    ''' <summary>
    ''' Obtiene el id del tercero por el id del responsable
    ''' </summary>
    ''' <param name="ResponsibleId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetThirdPartyIdByResponsibleId(ResponsibleId As Integer) As Integer Implements IFixedAssetTransferRepository.GetThirdPartyIdByResponsibleId
        Return (From r In _context.FixedAssetResponsible.AsNoTracking Where r.Id = ResponsibleId Select r.ThirdPartyId).FirstOrDefault
    End Function

    ''' <summary>
    ''' Obtiene la clase de la localización
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetClassOfLocation(Id As Integer) As Integer Implements IFixedAssetTransferRepository.GetClassOfLocation
        Return (From c In _context.FixedAssetLocation.AsNoTracking Where c.Id = Id Select c.Class).FirstOrDefault
    End Function

End Class
