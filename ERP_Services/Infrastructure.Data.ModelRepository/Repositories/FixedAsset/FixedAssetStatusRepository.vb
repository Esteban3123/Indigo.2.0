'***********************************************************************
' Assembly         : Infrastructure.Data.MaintenanceRepository
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 09-06-2016
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Importar"
Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Domain.Base.Entities
#End Region

Public Class FixedAssetStatusRepository

    Inherits GenericRepository(Of FixedAssetStatusAsset)
    Implements IFixedAssetStatusRepository

    'Devuelve el contexto en este repositorio 
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    '''inicializa la neva instancia d clase.
    ''' </summary>
    ''' <param name="context">el contexto.</param>
    ''' 
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    Public Function GetFixedAssetStatusByCode(Code As String) As FixedAssetStatusAsset Implements IFixedAssetStatusRepository.GetFixedAssetStatusByCode
        If Code Is Nothing OrElse Code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("Code")
        End If
        Dim res = (From d As FixedAssetStatusAsset In _context.FixedAssetStatusAsset Where d.Code.Equals(Code.Trim()) Select d).FirstOrDefault
        If res IsNot Nothing Then
            res.OriginalValue = (From d As FixedAssetStatusAsset In Me._context.FixedAssetStatusAsset.AsNoTracking() Where d.Code.Equals(Code.Trim()) Select d).FirstOrDefault()
            Return res
        Else
            Return New FixedAssetStatusAsset()
        End If
    End Function

    Public Function ListAllFixedAssetStatus() As List(Of FixedAssetStatusAsset) Implements IFixedAssetStatusRepository.ListAllFixedAssetStatus
        Dim ListFixedAssetStatus = From e In _context.FixedAssetStatusAsset
                Select e

        If ListFixedAssetStatus.Count() > 0 Then
            Return ListFixedAssetStatus.ToList()
        Else
            Return New List(Of FixedAssetStatusAsset)
        End If
    End Function
End Class
