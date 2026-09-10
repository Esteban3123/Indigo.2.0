'***********************************************************************
' Assembly         : Infrastructure.Data.MixinStationRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 02/12/2020
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Domain.Base

Public Class TransportationRepository
    Inherits GenericRepository(Of Transportation)
    Implements ITransportationRepository, Inject

    ''' <summary>
    ''' Contexto de Package
    ''' </summary>
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    ''' Inicia el contexto de Package
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    Public Function GetTransportation(code As String, Optional tracking As Boolean = True) As Transportation Implements ITransportationRepository.GetTransportation
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If

        Dim Transportation As Transportation = Nothing

        If tracking Then
            Transportation = (From e In _context.Transportation
                              Where e.Code = code
                              Select e).FirstOrDefault()
        Else
            Transportation = (From e In _context.Transportation.AsNoTracking()
                              Where e.Code = code
                              Select e).FirstOrDefault()
        End If

        If Transportation IsNot Nothing Then
            If Transportation.FixedAssetPhysicalAssetId IsNot Nothing Then
                Transportation.PhysicalAssetDescription = (From p In _context.FixedAssetPhysicalAsset.AsNoTracking
                                                           Join i In _context.FixedAssetItem.AsNoTracking On i.Id Equals p.ItemId
                                                           Where p.Id = Transportation.FixedAssetPhysicalAssetId
                                                           Select String.Concat(i.Code, " - ", i.Description)).FirstOrDefault()

                Transportation.ItemTypeId = (From p In _context.FixedAssetPhysicalAsset.AsNoTracking
                                             Join i In _context.FixedAssetItem.AsNoTracking On i.Id Equals p.ItemId
                                             Where p.Id = Transportation.FixedAssetPhysicalAssetId
                                             Select i.ItemTypeId).FirstOrDefault()

                Transportation.ItemTypeCodeName = (From p In _context.FixedAssetPhysicalAsset.AsNoTracking
                                                   Join i In _context.FixedAssetItem.AsNoTracking On i.Id Equals p.ItemId
                                                   Join it In _context.FixedAssetItemType.AsNoTracking On it.Id Equals i.ItemTypeId
                                                   Where p.Id = Transportation.FixedAssetPhysicalAssetId
                                                   Select String.Concat(it.Code, " - ", it.Name)).FirstOrDefault()
            End If

            If Transportation.SupplierId IsNot Nothing Then
                Transportation.SupplierCodeName = (From s In _context.Supplier.AsNoTracking Where s.Id = Transportation.SupplierId Select String.Concat(s.Code, " - ", s.Name)).FirstOrDefault()
            End If

            Transportation.TrademarkCodeName = (From t In _context.FixedAssetTrademark.AsNoTracking Where t.Id = Transportation.TrademarkId Select String.Concat(t.Code, " - ", t.Name)).FirstOrDefault()

            Return Transportation
        Else
            Return New Transportation()
        End If
    End Function

    Public Function GetTransportationById(id As String, Optional tracking As Boolean = True) As Transportation Implements ITransportationRepository.GetTransportationById
        Dim res = (From bg In _context.Transportation Where bg.Id = id Select bg).FirstOrDefault()
        If res IsNot Nothing Then
            res.OriginalValue = (From bg In _context.Transportation.AsNoTracking() Where bg.Id = id Select bg).FirstOrDefault()
            Return res
        Else
            Return New Transportation
        End If
    End Function
End Class