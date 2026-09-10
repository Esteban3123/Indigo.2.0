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

Public Class FixedAssetChangePlateRepository
    Inherits GenericRepository(Of FixedAssetChangePlate)
    Implements IFixedAssetChangePlateRepository

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
    Public Function GetFixedAssetChangePlate(code As String) As FixedAssetChangePlate Implements IFixedAssetChangePlateRepository.GetFixedAssetChangePlate
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim res = (From d In Me._context.FixedAssetChangePlate.Include("FixedAssetChangePlateDetail") Where d.Code.Equals(code.Trim()) Select d).FirstOrDefault
        If res IsNot Nothing Then

            If res.FixedAssetChangePlateDetail IsNot Nothing AndAlso res.FixedAssetChangePlateDetail.Count > 0 Then
                For Each item In res.FixedAssetChangePlateDetail
                    item.PhysicalAssetDescription = (From i In _context.FixedAssetPhysicalAsset.AsNoTracking.Include("FixedAssetItem").AsNoTracking Where i.Id = item.FixedAssetPhysicalAssetId Select String.Concat(item.OldPlate, " - ", String.Concat(i.FixedAssetItem.Code, " - ", i.FixedAssetItem.Description))).FirstOrDefault
                Next
            End If

            res.OriginalValue = (From d In Me._context.FixedAssetChangePlate.AsNoTracking() Where d.Code.Equals(code.Trim()) Select d).SingleOrDefault()
            Return res
        Else
            Return New FixedAssetChangePlate
        End If
    End Function

    ''' <summary>
    ''' Obtiene el registro por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFixedAssetChangePlateById(Id As Integer) As FixedAssetChangePlate Implements IFixedAssetChangePlateRepository.GetFixedAssetChangePlateById
        If Id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Dim res = (From d In Me._context.FixedAssetChangePlate Where d.Id = Id Select d).FirstOrDefault
        If res IsNot Nothing Then
            res.OriginalValue = (From d In Me._context.FixedAssetChangePlate.AsNoTracking() Where d.Id = Id Select d).SingleOrDefault()
            Return res
        Else
            Return New FixedAssetChangePlate
        End If
    End Function

    ''' <summary>
    ''' Valida si ya existe la placa
    ''' </summary>
    ''' <param name="Plate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPhysicalByPlate(Plate As String) As FixedAssetPhysicalAsset Implements IFixedAssetChangePlateRepository.GetPhysicalByPlate
        Return (From p In _context.FixedAssetPhysicalAsset.AsNoTracking.Include("FixedAssetItem").AsNoTracking Where p.Plate = Plate Select p).FirstOrDefault
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPhysicalById(Id As Integer) As FixedAssetPhysicalAsset Implements IFixedAssetChangePlateRepository.GetPhysicalById
        Return (From p In _context.FixedAssetPhysicalAsset Where p.Id = Id Select p).FirstOrDefault
    End Function

    ''' <summary>
    ''' Sp para cambiar la placa en los procesos
    ''' </summary>
    ''' <param name="Xml"></param>
    ''' <param name="CodeUser"></param>
    ''' <returns></returns>
    Public Function SP_FixedAssetChangePlate(Xml As String, CodeUser As String) As List(Of SP_FixedAssetChangePlate_Result) Implements IFixedAssetChangePlateRepository.SP_FixedAssetChangePlate
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_FixedAssetChangePlate(Xml, CodeUser).ToList()
    End Function

End Class
