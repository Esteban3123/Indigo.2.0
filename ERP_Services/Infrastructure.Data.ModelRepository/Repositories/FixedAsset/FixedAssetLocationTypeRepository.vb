'***********************************************************************
' Assembly         : Infrastructure.Data.FixedAssetRepository
' Author           : Jeisson Herrera Peña
' Created          : 30/09/2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.Data.Base
Imports Domain.Entities

#End Region

Public Class FixedAssetLocationTypeRepository
    Inherits GenericRepository(Of FixedAssetLocationType)
    Implements IFixedAssetLocationTypeRepository

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

    Public Function ListAllLocationType() As List(Of FixedAssetLocationType) Implements IFixedAssetLocationTypeRepository.ListAllLocationType
        Dim Busqueda = From e In _context.FixedAssetLocationType
                   Where e.State = True
                    Select e

        Return Busqueda.ToList
    End Function

#End Region

End Class
