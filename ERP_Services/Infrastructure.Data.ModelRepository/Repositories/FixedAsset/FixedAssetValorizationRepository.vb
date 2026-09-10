'************************************************************
' Assembly         : Infrastructure.Data.FixedAsset
' Author           : Carlos Mario Arias Rubiano
' Created          : 04/04/2016
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


''' <summary>
''' clase para hacer todas las operaciones de persistencia para la fabricante
''' </summary>
''' <remarks></remarks>
Public Class FixedAssetValorizationRepository
    Inherits GenericRepository(Of ValorizationDevaluation)
    Implements IFixedAssetValorizationRepository

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
    ''' Obtiene una Valorización/Desvalorización por código
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetTransactionByCode(Code As String) As FixedAssetTransaction Implements IFixedAssetValorizationRepository.GetTransactionByCode
        If Code Is Nothing OrElse Code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim res = (From d As FixedAssetTransaction In Me._context.FixedAssetTransaction.Include("FixedAssetTransactionDetail").Include("FixedAssetTransactionDetail.FixedAssetTransactionDetailBook").Include("FixedAssetTransactionDetail.FixedAssetTransactionDetailBook.LegalBook").Include("Currency")
                   Where d.Code.Equals(Code.Trim()) Select d).FirstOrDefault

        If res IsNot Nothing Then

            Dim ThirdPartyId = res.ThirdPartyId
            Dim SupplierId = res.SupplierId
            Dim SupplierDistributionLineId = res.SupplierDistributionLineId
            Dim SupplierTypeId = res.SupplierTypeId
            Dim CreditMainAccountId = res.CreditMainAccountId
            Dim CostCenterId = res.CostCenterId

            Dim ObjThirdParty = (From a In _context.ThirdParty.AsNoTracking Where a.Id = ThirdPartyId Select a).FirstOrDefault()

            res.NameThirdParty = ObjThirdParty.Name

            Dim ObjSupplier As Supplier
            Dim ObjSupplierDistributionLine As SuppliersDistributionLines
            Dim ObjSupplierType As SupplierType

            If SupplierId IsNot Nothing Then
                ObjSupplier = (From a In _context.Supplier.AsNoTracking Where a.Id = SupplierId Select a).FirstOrDefault()
                res.NameSupplier = ObjSupplier.Name
            End If

            If SupplierDistributionLineId Then
                ObjSupplierDistributionLine = (From a In _context.SuppliersDistributionLines.Include("DistributionLines").AsNoTracking Where a.Id = SupplierDistributionLineId Select a).FirstOrDefault()
                res.NameSupplierDistributionLines = ObjSupplierDistributionLine.DistributionLines.Name
            End If

            If SupplierDistributionLineId Then
                ObjSupplierType = (From a In _context.SupplierType.AsNoTracking Where a.Id = SupplierTypeId Select a).FirstOrDefault()
                res.NameSupplierType = ObjSupplierType.Name
            End If

            Dim ObjCreditAccount = (From a In _context.MainAccounts.AsNoTracking Where a.Id = CreditMainAccountId Select a).FirstOrDefault()

            res.NameCreditMainAccount = ObjCreditAccount.Number + " - " + ObjCreditAccount.Name

            Dim ObjCostCenter = (From a In _context.CostCenter.AsNoTracking Where a.Id = CostCenterId Select a).FirstOrDefault()

            res.NameCostCenter = ObjCostCenter.Name

            If res.FixedAssetTransactionDetail IsNot Nothing AndAlso res.FixedAssetTransactionDetail.Count > 0 Then
                For Each ObjFixedAssetTransactionDetail As FixedAssetTransactionDetail In res.FixedAssetTransactionDetail
                    Dim PhysicalAssetId = ObjFixedAssetTransactionDetail.PhysicalAssetId
                    Dim PhysicalAssetPartsId = ObjFixedAssetTransactionDetail.PhysicalAssetPartsId
                    Dim AssetMainAccountId = ObjFixedAssetTransactionDetail.AssetMainAccountId

                    Dim ObjPhysicalAsset As FixedAssetPhysicalAsset

                    If PhysicalAssetId IsNot Nothing Then
                        ObjPhysicalAsset = (From a In _context.FixedAssetPhysicalAsset.Include("FixedAssetItem").AsNoTracking Where a.Id = PhysicalAssetId Select a).FirstOrDefault()
                        ObjFixedAssetTransactionDetail.NameItem = ObjPhysicalAsset.FixedAssetItem.Description
                    End If

                    Dim ObjAssetMainAccount = (From a In _context.MainAccounts.AsNoTracking Where a.Id = AssetMainAccountId Select a).FirstOrDefault()

                    ObjFixedAssetTransactionDetail.NameAssetMainAccount = ObjAssetMainAccount.Number + " - " + ObjAssetMainAccount.Name

                    If ObjFixedAssetTransactionDetail.FixedAssetTransactionDetailBook IsNot Nothing AndAlso ObjFixedAssetTransactionDetail.FixedAssetTransactionDetailBook.Count > 0 Then
                        For Each ObjFixedAssetTransactionDetailBook As FixedAssetTransactionDetailBook In ObjFixedAssetTransactionDetail.FixedAssetTransactionDetailBook
                            Dim LegalBookIdId = ObjFixedAssetTransactionDetailBook.LegalBookId

                            Dim ObjLegalBook = (From a In _context.LegalBook.Include("Currency").AsNoTracking Where a.Id = LegalBookIdId Select a).FirstOrDefault()

                            ObjFixedAssetTransactionDetailBook.NameLegalBook = ObjLegalBook.Name
                            ObjFixedAssetTransactionDetailBook.CurrencyAbbreviationLegalBook = ObjLegalBook.Currency?.Abbreviation
                        Next
                    End If
                Next
            End If

            res.OriginalValue = (From d As FixedAssetTransaction In Me._context.FixedAssetTransaction.AsNoTracking() Where d.Code.Equals(Code.Trim()) Select d).SingleOrDefault()
            Return res
        Else
            Return New FixedAssetTransaction()
        End If
    End Function

    ''' <summary>
    ''' Obtiene una Valorización/Desvalorización por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetValorizationDevaluationById(Id As Integer) As ValorizationDevaluation Implements IFixedAssetValorizationRepository.GetValorizationDevaluationById
        If Id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Dim res = (From d In Me._context.ValorizationDevaluation Where d.Id = Id Select d).FirstOrDefault
        If res IsNot Nothing Then
            res.OriginalValue = (From d In Me._context.ValorizationDevaluation.AsNoTracking() Where d.Id = Id Select d).SingleOrDefault()
            Return res
        Else
            Return New ValorizationDevaluation()
        End If
    End Function

    ''' <summary>
    ''' Guarda un ingreso de activos
    ''' </summary>
    ''' <param name="FixedAssetEntryXml"></param>
    ''' <param name="ListDeleteString"></param>
    ''' <param name="codeUser"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SP_SaveValorizationDevaluation(FixedAssetTransactionXml As String, ListDeleteString As List(Of String), codeUser As String) As SP_SaveFixedAssetTransaction_Result Implements IFixedAssetValorizationRepository.SP_SaveValorizationDevaluation
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        FixedAssetTransactionXml = Replace(FixedAssetTransactionXml, ", ", "-")
        Return _context.SP_SaveFixedAssetTransaction(FixedAssetTransactionXml, ListDeleteString(0), ListDeleteString(1), codeUser).SingleOrDefault
    End Function

    ''' <summary>
    ''' Obtiene el articulo por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFixedAssetPhysicalAssetById(Id As Integer) As FixedAssetPhysicalAsset Implements IFixedAssetValorizationRepository.GetFixedAssetPhysicalAssetById
        If Id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Return (From i In _context.FixedAssetPhysicalAsset.AsNoTracking Where i.Id = Id Select i).FirstOrDefault
    End Function

    ''' <summary>
    ''' Obtiene el articulo por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFixedAssetPhysicalAssetPartstById(Id As Integer) As FixedAssetPhysicalAssetParts Implements IFixedAssetValorizationRepository.GetFixedAssetPhysicalAssetPartstById
        If Id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Return (From i In _context.FixedAssetPhysicalAssetParts.Include("FixedAssetPhysicalAsset").AsNoTracking Where i.Id = Id Select i).FirstOrDefault
    End Function

    ''' <summary>
    ''' Obtiene el libro oficial
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetLegalBook() As LegalBook Implements IFixedAssetValorizationRepository.GetLegalBook
        Return (From l In _context.LegalBook.AsNoTracking Where l.OfficialBook = True Select l).FirstOrDefault
    End Function

    ''' <summary>
    ''' Obtiene el inventario fisico con el agregado del catalogo de articulo
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPhysicalAssetWithFixedAssetCatalog(Id As Integer) As FixedAssetPhysicalAsset Implements IFixedAssetValorizationRepository.GetPhysicalAssetWithFixedAssetCatalog
        Return (From f In _context.FixedAssetPhysicalAsset.AsNoTracking.Include("FixedAssetItem").AsNoTracking.Include("FixedAssetItem.FixedAssetItemCatalog").AsNoTracking Where f.Id = Id Select f).FirstOrDefault
    End Function

End Class
