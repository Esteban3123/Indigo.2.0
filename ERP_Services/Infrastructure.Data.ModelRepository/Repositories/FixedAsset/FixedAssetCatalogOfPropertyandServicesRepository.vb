'************************************************************
' Assembly         : Infrastructure.Data.FixedAssetRepository
' Author           : Andres Alarcon
' Created          : 10-06-2024
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Infrastructure.Data.Base
Imports Domain.Entities

#End Region

Public Class FixedAssetCatalogOfPropertyandServicesRepository
    Inherits GenericRepository(Of FixedAssetCatalogOfPropertyandServices)
    Implements IFixedAssetCatalogOfPropertyandServicesRepository

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

    Public Function GetCatalogOfPropertyandServicesByCode(Code As String) As FixedAssetCatalogOfPropertyandServices Implements IFixedAssetCatalogOfPropertyandServicesRepository.GetCatalogOfPropertyandServicesByCode
        If Code Is Nothing OrElse Code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("Code")
        End If

        Return (From d As FixedAssetCatalogOfPropertyandServices In Me._context.FixedAssetCatalogOfPropertyandServices
                Where d.Code.Equals(Code.Trim())
                Select d).FirstOrDefault
    End Function

End Class
