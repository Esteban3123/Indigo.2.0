'************************************************************
' Assembly         : Infrastructure.Data.FixedAssetRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 07/06/2016
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

Public Class FixedAssetReclassificationRepository
    Inherits GenericRepository(Of FixedAssetReclassification)
    Implements IFixedAssetReclassificationRepository

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
    ''' Obtiene el registro por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFixedAssetReclassificationById(Id As Integer, Optional tracking As Boolean = True) As FixedAssetReclassification Implements IFixedAssetReclassificationRepository.GetFixedAssetReclassificationById
        If Id = 0 Then
            Throw New ArgumentNullException("Id")
        End If

        Dim res As FixedAssetReclassification
        If tracking Then
            res = (From d In Me._context.FixedAssetReclassification Where d.Id = Id Select d).FirstOrDefault
        Else
            res = (From d In Me._context.FixedAssetReclassification.AsNoTracking.Include("FixedAssetReclassificationDetail").AsNoTracking.Include("FixedAssetReclassificationDetail.FixedAssetReclassificationDetailBook").AsNoTracking Where d.Id = Id Select d).FirstOrDefault
        End If

        If res IsNot Nothing Then
            res.OriginalValue = (From d In Me._context.FixedAssetReclassification.AsNoTracking() Where d.Id = Id Select d).SingleOrDefault()
            Return res
        Else
            Return New FixedAssetReclassification
        End If
    End Function

    ''' <summary>
    ''' Obtiene el registro por código
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFixedAssetReclassificationByCode(Code As String) As FixedAssetReclassification Implements IFixedAssetReclassificationRepository.GetFixedAssetReclassificationByCode
        If Code Is Nothing OrElse Code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim res = (From d In Me._context.FixedAssetReclassification.Include("FixedAssetReclassificationDetail").AsNoTracking.Include("FixedAssetReclassificationDetail.FixedAssetReclassificationDetailBook").AsNoTracking Where d.Code.Equals(Code.Trim()) Select d).FirstOrDefault
        If res IsNot Nothing Then
            Dim item As FixedAssetItem
            item = (From i In _context.FixedAssetItem.AsNoTracking.Include("FixedAssetItemCatalog").AsNoTracking Where i.Id = res.ItemIdPrevious).FirstOrDefault
            res.ItemPreviousCodeDescription = item.Code & " - " & item.Description
            res.ItemCatalogPreviousCodeDescription = item.FixedAssetItemCatalog.Code & " - " & item.FixedAssetItemCatalog.Description

            If res.ItemId IsNot Nothing Then
                item = (From i In _context.FixedAssetItem.AsNoTracking.Include("FixedAssetItemCatalog").AsNoTracking Where i.Id = res.ItemId).FirstOrDefault
                res.ItemCodeDescription = item.Code & " - " & item.Description
                res.ItemCatalogCodeDescription = item.FixedAssetItemCatalog.Code & " - " & item.FixedAssetItemCatalog.Description
            Else
                Dim catalog = (From i In _context.FixedAssetItemCatalog.AsNoTracking Where i.Id = res.ItemCatalogId).FirstOrDefault
                res.ItemCatalogCodeDescription = catalog.Code & " - " & catalog.Description
            End If

            If res.FixedAssetReclassificationDetail IsNot Nothing AndAlso res.FixedAssetReclassificationDetail.Count > 0 Then
                Dim dictionaryLegalBook As New Dictionary(Of Integer, String)
                For Each detail In res.FixedAssetReclassificationDetail
                    Dim fapa = (From i In _context.FixedAssetPhysicalAsset.AsNoTracking.Include("FixedAssetItem").AsNoTracking Where i.Id = detail.PhysicalAssetId).FirstOrDefault
                    detail.ItemCatalogId = fapa.FixedAssetItem.ItemCatalogId
                    detail.ItemId = fapa.ItemId
                    detail.ItemCodeDescription = fapa.FixedAssetItem.Code & " - " & fapa.FixedAssetItem.Description
                    detail.PhysicalAssetSerie = fapa.Serie
                    detail.PhysicalAssetPlate = fapa.Plate
                    detail.PhysicalAssetStatus = fapa.Status
                    detail.PhysicalAssetAdquisitionTypeReal = fapa.AdquisitionTypeReal
                    detail.PhysicalAssetHasReclassified = fapa.HasReclassified

                    If detail.FixedAssetReclassificationDetailBook IsNot Nothing AndAlso detail.FixedAssetReclassificationDetailBook.Count > 0 Then
                        For Each detailBook In detail.FixedAssetReclassificationDetailBook
                            If Not dictionaryLegalBook.ContainsKey(detailBook.LegalBookId) Then
                                Dim legalBook = (From l In Me._context.LegalBook.AsNoTracking Where l.Id = detailBook.LegalBookId).FirstOrDefault
                                dictionaryLegalBook.Add(detailBook.LegalBookId, legalBook.Code & " - " & legalBook.Name)
                            End If
                            Dim itemLegalBook = dictionaryLegalBook.Where(Function(l) l.Key = detailBook.LegalBookId).FirstOrDefault()
                            detailBook.LegalBookCodeName = itemLegalBook.Value
                        Next
                    End If
                Next
            End If

            res.OriginalValue = (From d In Me._context.FixedAssetReclassification.AsNoTracking() Where d.Code.Equals(Code.Trim()) Select d).SingleOrDefault()
            Return res
        Else
            Return New FixedAssetReclassification
        End If
    End Function

    ''' <summary>
    ''' Sp para guardar y confirmar la reclasificación
    ''' </summary>
    ''' <param name="Xml"></param>
    ''' <param name="CodeUser"></param>
    ''' <returns></returns>
    Public Function SP_GenerateJournalVoucherByFixedAssetReclassification(Xml As String, CodeUser As String) As List(Of SP_GenerateJournalVoucherByFixedAssetReclassification_Result) Implements IFixedAssetReclassificationRepository.SP_GenerateJournalVoucherByFixedAssetReclassification
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_GenerateJournalVoucherByFixedAssetReclassification(Xml, CodeUser).ToList()
    End Function

End Class
