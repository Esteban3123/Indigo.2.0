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

        If res IsNot Nothing AndAlso res.Any() Then

            Dim functionalUnitIds = res.Where(Function(x) x.FunctionalUnitId > 0).Select(Function(x) x.FunctionalUnitId).Distinct().ToList()
            Dim locationTypeIds = res.Where(Function(x) x.LocationTypeId > 0).Select(Function(x) x.LocationTypeId).Distinct().ToList()

            Dim functionalUnits = (From a In _context.FunctionalUnit.AsNoTracking()
                                   Where functionalUnitIds.Contains(a.Id)
                                   Select a).ToDictionary(Function(x) x.Id)

            Dim locationTypes = (From b In _context.FixedAssetLocationType.AsNoTracking()
                                 Where locationTypeIds.Contains(b.Id)
                                 Select b).ToDictionary(Function(x) x.Id)

            For Each ObjLocation As FixedAssetLocation In res
                If ObjLocation.FunctionalUnitId > 0 AndAlso functionalUnits.ContainsKey(ObjLocation.FunctionalUnitId) Then
                    ObjLocation.NameFunctionalUnit = functionalUnits(ObjLocation.FunctionalUnitId).Name
                End If

                If ObjLocation.LocationTypeId > 0 AndAlso locationTypes.ContainsKey(ObjLocation.LocationTypeId) Then
                    ObjLocation.NameLocationType = locationTypes(ObjLocation.LocationTypeId).Name
                End If
            Next

            Return res
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
