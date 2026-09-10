'***********************************************************************
' Assembly         : Infrastructure.Data.FixedAssetRepository
' Author           : Jeisson Herrera Peña
' Created          : 24/09/2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.Data.Base
Imports Domain.Entities

#End Region

Public Class FixedAssetLocationRepository
    Inherits GenericRepository(Of FixedAssetLocation)
    Implements IFixedAssetLocationRepository

#Region "Fields"

    ''' <summary>
    ''' Contexto de FixedAsset
    ''' </summary>
    ''' <remarks></remarks>
    Private _context As IGlobalModelUnitOfWork

#End Region

#Region "Constuctor"

    ''' <summary>
    ''' Inicia el contexto de FixedAsset
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

#End Region

#Region "Methods"

    Public Function GetLocation(codeLocation As String) As FixedAssetLocation Implements IFixedAssetLocationRepository.GetLocation
        Dim Busqueda = From e In _context.FixedAssetLocation
                 Where e.Code = codeLocation
                 Select e
        If Busqueda.Count = 0 Then
            Return New FixedAssetLocation
        Else
            Return Busqueda.Single
        End If
    End Function

    Public Function ListAllLocation() As List(Of FixedAssetLocation) Implements IFixedAssetLocationRepository.ListAllLocation

        Dim res = (From d As FixedAssetLocation In Me._context.FixedAssetLocation Select d).ToList()

        If res IsNot Nothing Then

            For Each ObjLocation As FixedAssetLocation In res
                Dim FunctionalUnit = (From a In _context.FunctionalUnit.AsNoTracking Where a.Id = ObjLocation.FunctionalUnitId Select a).FirstOrDefault()
                Dim LocationType = (From b In _context.FixedAssetLocationType.AsNoTracking Where b.Id = ObjLocation.LocationTypeId Select b).FirstOrDefault()

                ObjLocation.NameFunctionalUnit = FunctionalUnit.Name

                ObjLocation.NameLocationType = LocationType.Name

            Next

            Return res.ToList
        Else
            Return Nothing
        End If

    End Function

    Public Function ListLocation() As List(Of FixedAssetLocation) Implements IFixedAssetLocationRepository.ListLocation
        Dim Busqueda = From e In _context.FixedAssetLocation
                       Select e


        Return Busqueda.ToList
    End Function

    Public Function SaveLocation(Location As FixedAssetLocation) As Boolean Implements IFixedAssetLocationRepository.SaveLocation
        _context.FixedAssetLocation.ApplyChanges(Location)
        Return True
    End Function

 
#End Region

End Class
