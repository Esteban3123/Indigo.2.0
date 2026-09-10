'***********************************************************************
' Assembly         : Infrastructure.Data.MaintenanceRepository
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 26-08-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Importar"
Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Domain.Base

#End Region

Public Class FixedAssetPartsAccesoriesConsumablesRepository
    Inherits GenericRepository(Of FixedAssetPartsAccesoriesConsumables)
    Implements IFixedAssetPartsAccesoriesConsumablesRepository


    'Devuelve el contexto en este repositorio 
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    '''inicializa la neva instancia d clase.
    ''' </summary>
    ''' <param name="context">el contexto.</param>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Función que obtiene una Parte, Accesorio por Código
    ''' </summary>
    ''' <param name="Code">Código</param>
    ''' <returns>PartsAccesoriesConsumables</returns>
    ''' <remarks></remarks>
    Public Function GetPartsAccesoriesConsumablesByCode(Code As String, Optional desatach As Boolean = True) As FixedAssetPartsAccesoriesConsumables Implements IFixedAssetPartsAccesoriesConsumablesRepository.GetPartsAccesoriesConsumablesByCode
        If Code Is Nothing OrElse Code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim res = (From d As FixedAssetPartsAccesoriesConsumables In Me._context.FixedAssetPartsAccesoriesConsumables.Include("FixedAssetPartsAccesoriesConsumablesDetail") Where d.Code.Equals(Code.Trim()) Select d).FirstOrDefault
        If res IsNot Nothing Then

            If res.FixedAssetPartsAccesoriesConsumablesDetail IsNot Nothing AndAlso res.FixedAssetPartsAccesoriesConsumablesDetail.Count > 0 Then
                For Each item In res.FixedAssetPartsAccesoriesConsumablesDetail
                    Dim LegalBook = (From l In _context.LegalBook.AsNoTracking Where l.Id = item.LegalBookId Select l).FirstOrDefault
                    item.CodeNameLegalBook = LegalBook.Code + " - " + LegalBook.Name
                Next
            End If

            res.OriginalValue = (From d As FixedAssetPartsAccesoriesConsumables In Me._context.FixedAssetPartsAccesoriesConsumables.AsNoTracking() Where d.Code.Equals(Code.Trim()) Select d).SingleOrDefault()
            Return res
        Else
            Return New FixedAssetPartsAccesoriesConsumables()
        End If
    End Function

    ''' <summary>
    ''' Función que obtiene todas las PartsAccesoriesConsumables
    ''' </summary>
    ''' <returns>Lista de PartsAccesoriesConsumables</returns>
    ''' <remarks></remarks>
    Public Function ListAllPartsAccesoriesConsumables() As List(Of FixedAssetPartsAccesoriesConsumables) Implements IFixedAssetPartsAccesoriesConsumablesRepository.ListAllPartsAccesoriesConsumables
        Dim ListPartsAccesoriesConsumables = From e In _context.FixedAssetPartsAccesoriesConsumables
                  Select e

        If ListPartsAccesoriesConsumables.Count() > 0 Then
            Return ListPartsAccesoriesConsumables.ToList()
        Else
            Return New List(Of FixedAssetPartsAccesoriesConsumables)
        End If
    End Function

    ''' <summary>
    ''' Función que obtiene todas las PartsAccesoriesConsumables
    ''' </summary>
    ''' <returns>Lista de PartsAccesoriesConsumables</returns>
    ''' <remarks></remarks>
    Public Function GetPartsAccesoriesConsumablesByEquipmentType(IdEquipmentType As Integer) As List(Of FixedAssetItemTypePartsAccesories) Implements IFixedAssetPartsAccesoriesConsumablesRepository.GetPartsAccesoriesConsumablesByEquipmentType
        Dim ListPartsAccesoriesConsumables = From e In _context.FixedAssetItemTypePartsAccesories.Include("FixedAssetPartsAccesoriesConsumables")
                  Select e

        If ListPartsAccesoriesConsumables.Count() > 0 Then
            Return ListPartsAccesoriesConsumables.ToList()
        Else
            Return Nothing
        End If
    End Function
End Class
