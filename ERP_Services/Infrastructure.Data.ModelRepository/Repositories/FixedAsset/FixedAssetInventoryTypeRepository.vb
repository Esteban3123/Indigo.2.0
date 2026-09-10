'************************************************************
' Assembly         : Infrastructure.Data.GlosasRepository
' Author           : Oscar Ivan Sierra
' Created          : 08-08-2013
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Importar"
Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Domain.Base

#End Region


''' <summary>
''' clase para hacer todas las operaciones de persistencia para la entidad aseguradora
''' </summary>
''' <remarks></remarks>
Public Class FixedAssetInventoryTypeRepository
    Inherits GenericRepository(Of FixedAssetInventoryType)
    Implements IFixedAssetInventoryTypeRepository

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


    Public Function GetInventoryType(codeInventarioType As String) As FixedAssetInventoryType Implements IFixedAssetInventoryTypeRepository.GetInventoryType
        If codeInventarioType Is Nothing OrElse codeInventarioType.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim res = (From d As FixedAssetInventoryType In Me._context.FixedAssetInventoryType Where d.Code.Equals(codeInventarioType.Trim()) Select d).FirstOrDefault
        If res IsNot Nothing Then

            res.OriginalValue = (From d As FixedAssetInventoryType In Me._context.FixedAssetInventoryType.AsNoTracking() Where d.Code.Equals(codeInventarioType.Trim()) Select d).SingleOrDefault()
            Return res
        Else
            Return New FixedAssetInventoryType()
        End If

    End Function

    Public Function ListAllInventoryType() As List(Of FixedAssetInventoryType) Implements IFixedAssetInventoryTypeRepository.ListAllInventoryType
        Dim Busqueda = From e In _context.FixedAssetInventoryType
                       Select e

        Return Busqueda.ToList
    End Function

End Class
