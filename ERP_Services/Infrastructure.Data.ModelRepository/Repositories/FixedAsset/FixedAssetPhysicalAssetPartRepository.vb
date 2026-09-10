#Region "Importar"
Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Domain.Base
Imports System.Data.Entity.Infrastructure

#End Region

Public Class FixedAssetPhysicalAssetPartRepository
    Inherits GenericRepository(Of FixedAssetPhysicalAssetParts)
    Implements IFixedAssetPhysicalAssetPartRepository

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

    Public Function GetFixedAssetPhysicalAssetPartById(Id As Integer) As FixedAssetPhysicalAssetParts Implements IFixedAssetPhysicalAssetPartRepository.GetFixedAssetPhysicalAssetPartById
        Dim Busqueda = From e In _context.FixedAssetPhysicalAssetParts
                       Where e.Id = Id
                       Select e

        Return Busqueda.FirstOrDefault()
    End Function

End Class
