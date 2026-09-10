'************************************************************
' Assembly         : Infrastructure.Data.FixedAssetRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 09/06/2016
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

Public Class FixedAssetDepreciationRepository
    Inherits GenericRepository(Of FixedAssetDepreciation)
    Implements IFixedAssetDepreciationRepository

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
    Public Function GetFixedAssetDepreciation(code As String) As FixedAssetDepreciation Implements IFixedAssetDepreciationRepository.GetFixedAssetDepreciation
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim res = (From d In Me._context.FixedAssetDepreciation Where d.Code.Equals(code.Trim()) Select d).FirstOrDefault
        If res IsNot Nothing Then

            res.OriginalValue = (From d In Me._context.FixedAssetDepreciation.AsNoTracking() Where d.Code.Equals(code.Trim()) Select d).SingleOrDefault()
            Return res
        Else
            Return New FixedAssetDepreciation
        End If
    End Function

    ''' <summary>
    ''' Obtiene el registro por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFixedAssetDepreciationById(Id As Integer) As FixedAssetDepreciation Implements IFixedAssetDepreciationRepository.GetFixedAssetDepreciationById
        If Id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Dim res = (From d In Me._context.FixedAssetDepreciation Where d.Id = Id Select d).FirstOrDefault
        If res IsNot Nothing Then
            res.OriginalValue = (From d In Me._context.FixedAssetDepreciation.AsNoTracking() Where d.Id = Id Select d).SingleOrDefault()
            Return res
        Else
            Return New FixedAssetDepreciation
        End If
    End Function

    ''' <summary>
    ''' Genera y guarda la depreciación
    ''' </summary>
    ''' <param name="depreciateMonth"></param>
    ''' <param name="depreciateYear"></param>
    ''' <param name="codeUser"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SP_SaveDepreciation(depreciateMonth As Integer, depreciateYear As Integer, codeUser As String, operatingUnitId As Integer, ModeConfirm As Boolean) As SP_SaveDepreciation_Result Implements IFixedAssetDepreciationRepository.SP_SaveDepreciation
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_SaveDepreciation(depreciateMonth, depreciateYear, codeUser, operatingUnitId, ModeConfirm).SingleOrDefault
    End Function

End Class
