'************************************************************
' Assembly         : Infrastructure.Data.FixedAssetRepository
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 05-02-2016
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Importar"
Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Domain.Base

#End Region

Public Class FixedAssetFixedAssetRemissionEntranceRepository

    Inherits GenericRepository(Of FixedAssetRemissionEntrance)
    Implements IFixedAssetFixedAssetRemissionEntranceRepository

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

    Public Function GetFixedAssetFixedAssetRemissionEntranceByCode(code As String, Optional tracking As Boolean = True) As FixedAssetRemissionEntrance Implements IFixedAssetFixedAssetRemissionEntranceRepository.GetFixedAssetFixedAssetRemissionEntranceByCode
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim res = (From d As FixedAssetRemissionEntrance In Me._context.FixedAssetRemissionEntrance.Include("FixedAssetRemissionEntranceItem").Include("FixedAssetRemissionEntranceItem.FixedAssetRemissionEntranceItemDetail") _
                   .Include("FixedAssetRemissionEntranceItem.FixedAssetRemissionEntranceItemDetail.FixedAssetRemissionEntranceItemDetailBook").Include("FixedAssetRemissionEntranceItem.FixedAssetRemissionEntranceItemDetail.FixedAssetRemissionEntranceItemDetailPart")
                   Where d.Code.Equals(code.Trim()) Select d).FirstOrDefault
        If res IsNot Nothing Then

            Dim IdSuplier = res.SupplierDistributionLineId
            Dim ResponsibleId = res.ResponsibleId
            Dim LocationId = res.LocationId

            Dim ObjSuplier = (From a In _context.SuppliersDistributionLines.Include("Supplier").Include("Supplier.ThirdParty").AsNoTracking Where a.Id = IdSuplier Select a).FirstOrDefault()

            If ResponsibleId IsNot Nothing Then
                Dim ObjResponsible = (From a In _context.FixedAssetResponsible.Include("ThirdParty").AsNoTracking Where a.Id = ResponsibleId Select a).FirstOrDefault()
                res.NameResponsible = ObjResponsible.ThirdParty.Name
            End If

            If LocationId IsNot Nothing Then
                Dim ObjLocation = (From c In _context.FixedAssetLocation.AsNoTracking Where c.Id = LocationId Select c).FirstOrDefault()
                res.NameLocation = ObjLocation.Name
            End If


            res.NameSuplier = ObjSuplier.Supplier.ThirdParty.Name

            If res.FixedAssetRemissionEntranceItem.Count > 0 Then
                For Each ObjFixedAssetRemissionEntranceEquipment As FixedAssetRemissionEntranceItem In res.FixedAssetRemissionEntranceItem
                    Dim IdEquipment = ObjFixedAssetRemissionEntranceEquipment.ItemId
                    Dim IdIVA = ObjFixedAssetRemissionEntranceEquipment.IVAId
                    Dim IdTrademark = ObjFixedAssetRemissionEntranceEquipment.TrademarkId
                    Dim IdPoliza = ObjFixedAssetRemissionEntranceEquipment.PolicyId
                    Dim ObjEquipment = (From c In _context.FixedAssetItem.AsNoTracking Where c.Id = IdEquipment Select c).FirstOrDefault()
                    Dim ObjIVA = (From d In _context.GeneralLedgerIVA.AsNoTracking Where d.Id = IdIVA Select d).FirstOrDefault()
                    Dim ObjTrademark = (From e In _context.FixedAssetTrademark.AsNoTracking Where e.Id = IdTrademark Select e).FirstOrDefault()
                    Dim ObjPoliza = (From f In _context.FixedAssetPolicy.AsNoTracking Where f.Id = IdPoliza Select f).FirstOrDefault()

                    If ObjEquipment IsNot Nothing Then
                        ObjFixedAssetRemissionEntranceEquipment.NameEquipment = ObjEquipment.Description
                    End If

                    If ObjIVA IsNot Nothing Then
                        ObjFixedAssetRemissionEntranceEquipment.NameIva = ObjIVA.Name
                    End If

                    If ObjTrademark IsNot Nothing Then
                        ObjFixedAssetRemissionEntranceEquipment.NameTrademark = ObjTrademark.Name
                    End If

                    If ObjPoliza IsNot Nothing Then
                        ObjFixedAssetRemissionEntranceEquipment.NamePoliza = ObjPoliza.Name
                    End If


                    If ObjFixedAssetRemissionEntranceEquipment.FixedAssetRemissionEntranceItemDetail IsNot Nothing Then
                        For Each ObjFixedAssetRemissionEntranceItemDetail As FixedAssetRemissionEntranceItemDetail In ObjFixedAssetRemissionEntranceEquipment.FixedAssetRemissionEntranceItemDetail
                            Dim IdResponsible = ObjFixedAssetRemissionEntranceItemDetail.ResponsibleId
                            Dim IdLocation = ObjFixedAssetRemissionEntranceItemDetail.LocationId
                            Dim IdStatus = ObjFixedAssetRemissionEntranceItemDetail.StatusAssetId

                            Dim ObjResponsible = (From a In _context.FixedAssetResponsible.Include("ThirdParty").AsNoTracking Where a.Id = IdResponsible Select a).FirstOrDefault()
                            Dim ObjLocation = (From c In _context.FixedAssetLocation.AsNoTracking Where c.Id = IdLocation Select c).FirstOrDefault()
                            Dim ObjStatus = (From c In _context.FixedAssetStatusAsset.AsNoTracking Where c.Id = IdStatus Select c).FirstOrDefault()

                            ObjFixedAssetRemissionEntranceItemDetail.NameResponsible = ObjResponsible.ThirdParty.Name
                            ObjFixedAssetRemissionEntranceItemDetail.NameLocation = ObjLocation.Name
                            ObjFixedAssetRemissionEntranceItemDetail.NameStatus = ObjStatus.Name

                            If ObjFixedAssetRemissionEntranceItemDetail.FixedAssetRemissionEntranceItemDetailBook IsNot Nothing Then
                                For Each ObjFixedAssetRemissionEntranceItemDetailBook As FixedAssetRemissionEntranceItemDetailBook In ObjFixedAssetRemissionEntranceItemDetail.FixedAssetRemissionEntranceItemDetailBook

                                    Dim IdLegalBook = ObjFixedAssetRemissionEntranceItemDetailBook.LegalBookId

                                    Dim ObjLegalBook = (From c In _context.LegalBook.AsNoTracking Where c.Id = IdLegalBook Select c).FirstOrDefault()

                                    ObjFixedAssetRemissionEntranceItemDetailBook.LegalBookCodeName = ObjLegalBook.Code + " - " + ObjLegalBook.Name

                                Next
                            End If


                            If ObjFixedAssetRemissionEntranceItemDetail.FixedAssetRemissionEntranceItemDetailPart IsNot Nothing Then
                                For Each ObjParts As FixedAssetRemissionEntranceItemDetailPart In ObjFixedAssetRemissionEntranceItemDetail.FixedAssetRemissionEntranceItemDetailPart
                                    Dim IdPartsAccesories = ObjParts.PartAccesoriesConsumiblesId
                                    Dim ObjPartsAccesories = (From a In _context.FixedAssetPartsAccesoriesConsumables.AsNoTracking Where a.Id = IdPartsAccesories Select a).FirstOrDefault()

                                    ObjParts.NamePartsAccesoriesConsumibles = ObjPartsAccesories.Name

                                Next
                            End If


                        Next
                    End If

                Next
            End If

            res.OriginalValue = (From d As FixedAssetRemissionEntrance In Me._context.FixedAssetRemissionEntrance.AsNoTracking() Where d.Code.Equals(code.Trim()) Select d).SingleOrDefault()
            Return res
        Else
            Return New FixedAssetRemissionEntrance()
        End If
    End Function

    Public Function GetFixedAssetFixedAssetRemissionEntranceById(Id As Integer, Optional tracking As Boolean = True) As FixedAssetRemissionEntrance Implements IFixedAssetFixedAssetRemissionEntranceRepository.GetFixedAssetFixedAssetRemissionEntranceById
        Dim res = (From d As FixedAssetRemissionEntrance In Me._context.FixedAssetRemissionEntrance.Include("FixedAssetRemissionEntranceEquipment").Include("FixedAssetRemissionEntranceEquipment.FixedAssetRemissionEntranceItemDetail").Include("FixedAssetRemissionEntranceEquipment.FixedAssetRemissionEntranceItemDetail.FixedAssetPartAccesoriesConsumiblesFixedAssetRemissionEntrance") Where d.Id.Equals(Id) Select d).FirstOrDefault
        If res IsNot Nothing Then
            Return res
        Else
            Return Nothing
        End If
    End Function

    Public Function GetSupplierBySupplierDistributionLineId(SupplierDistributionLineId As Integer) As Integer Implements IFixedAssetFixedAssetRemissionEntranceRepository.GetSupplierBySupplierDistributionLineId
        Return (From s In _context.SuppliersDistributionLines.AsNoTracking Where s.Id = SupplierDistributionLineId Select s.IdSupplier).FirstOrDefault
    End Function

    Public Function GetMainAccountWithItemId(ItemId As Integer, AdquisitionType As Integer) As Integer Implements IFixedAssetFixedAssetRemissionEntranceRepository.GetMainAccountWithItemId
        If AdquisitionType = 3 Then 'Si el tipo de adquisición es comodato
            Return (From i In _context.FixedAssetItem.AsNoTracking
                    Join c In _context.FixedAssetItemCatalog.AsNoTracking On c.Id Equals i.ItemCatalogId
                    Where i.Id = ItemId
                    Select c.DebitLoanAccountId).FirstOrDefault
        ElseIf AdquisitionType = 7 Then 'Si el tipo de adquisición es leasing financiero
            Return (From i In _context.FixedAssetItem.AsNoTracking
                    Join c In _context.FixedAssetItemCatalog.AsNoTracking On c.Id Equals i.ItemCatalogId
                    Where i.Id = ItemId
                    Select c.IncomeLeasingAccountId).FirstOrDefault
        Else 'Si es de otro tipo
            Return (From i In _context.FixedAssetItem.AsNoTracking
                    Join c In _context.FixedAssetItemCatalog.AsNoTracking On c.Id Equals i.ItemCatalogId
                    Where i.Id = ItemId
                    Select c.IncomeAccountId).FirstOrDefault
        End If
    End Function

End Class
