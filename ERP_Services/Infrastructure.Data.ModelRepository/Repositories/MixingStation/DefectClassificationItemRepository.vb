'***********************************************************************
' Assembly         : Infrastructure.Data.MixinStationRepository
' Author           : Cristian Camilo Bahamon Castaño
' Created          : 20/09/2022
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Domain.Base
#End Region

Public Class DefectClassificationItemRepository
    Inherits GenericRepository(Of DefectClassificationItem)
    Implements IDefectClassificationItemRepository, Inject

    ''' <summary>
    ''' Contexto de payments
    ''' </summary>
    ''' <remarks></remarks>
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    ''' Inicia el contexto de payments
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    Public Function ListAllDefectClassificationItem() As List(Of DefectClassificationItem) Implements IDefectClassificationItemRepository.ListAllDefectClassificationItem
        Dim DefectClassificationItem = From e In _context.DefectClassificationItem
                                       Select e
        Return DefectClassificationItem.ToList()
    End Function

    Public Function GetDefectClassificationItemByCode(code As String, Optional tracking As Boolean = True) As DefectClassificationItem Implements IDefectClassificationItemRepository.GetDefectClassificationItemByCode
        Dim DefectClassificationItem As DefectClassificationItem = Nothing
        If tracking Then
            DefectClassificationItem = (From e In _context.DefectClassificationItem.Include("DefectsUnitDoseType")
                                        Where e.Code = code
                                        Select e).FirstOrDefault
        Else
            DefectClassificationItem = (From e In _context.DefectClassificationItem.AsNoTracking.Include("DefectsUnitDoseType")
                                        Where e.Code = code
                                        Select e).FirstOrDefault
        End If

        If DefectClassificationItem IsNot Nothing Then

            If DefectClassificationItem.DefectsUnitDoseType IsNot Nothing AndAlso DefectClassificationItem.DefectsUnitDoseType.Count > 0 Then
                For Each item In DefectClassificationItem.DefectsUnitDoseType
                    Dim unitDoseType = (From x In _context.UnitDoseType.AsNoTracking Where x.Id = item.Id_UnitDoseType Select x).FirstOrDefault()

                    item.UnitDoseTypeCode = unitDoseType.Code
                    item.UnitDoseTypeName = unitDoseType.Description
                Next
            End If

            Dim CategoryDefects = (From x In _context.DefectClassificationGroup.AsNoTracking Where x.Id = DefectClassificationItem.DefectClassificationGroupId Select x).FirstOrDefault()
            DefectClassificationItem.CategoryDefectsName = CategoryDefects.Description

            Return DefectClassificationItem
        Else
            Return New DefectClassificationItem()
        End If
    End Function

    Public Function GetDefectClassificationItemById(id As Integer, Optional tracking As Boolean = True) As DefectClassificationItem Implements IDefectClassificationItemRepository.GetDefectClassificationItemById
        Dim DefectClassificationItem = From e In _context.DefectClassificationItem
                                       Where e.Id = id
                                       Select e
        If DefectClassificationItem.Count > 0 Then
            Dim Objdefects = Nothing
            If tracking = False Then
                Objdefects = (From e In _context.DefectClassificationItem.AsNoTracking
                              Where e.Id = id
                              Select e).SingleOrDefault
            Else
                Objdefects = DefectClassificationItem.SingleOrDefault
            End If
            Return Objdefects
        Else
            Return New DefectClassificationItem()
        End If
    End Function
End Class
