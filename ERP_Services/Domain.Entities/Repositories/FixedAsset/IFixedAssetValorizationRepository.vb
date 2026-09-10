'************************************************************
' Assembly         : Domain.FixedAsset
' Author           : Carlos Mario Arias Rubiano
' Created          : 04/04/2016
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Importar"
Imports Domain.Base.Entities
Imports Domain.Base
Imports Domain.Entities
#End Region

''' <summary>
''' clase para definir cada una de la spropiedades y metodos que se van a persistir en la clase del repositorio
''' </summary>
''' <remarks></remarks>
Public Interface IFixedAssetValorizationRepository
    Inherits IRepository(Of ValorizationDevaluation)

    ''' <summary>
    ''' Consulta la Valorización/Desvalorización por id
    ''' </summary>
    Function GetValorizationDevaluationById(ByVal Id As Integer) As ValorizationDevaluation

    ''' <summary>
    ''' Consulta la Valorización/Desvalorización por código
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetTransactionByCode(Code As String) As FixedAssetTransaction

    ''' <summary>
    ''' Guarda un ingreso de activos
    ''' </summary>
    ''' <param name="ListDeleteString">Objeto xml de los listados de eliminados</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SP_SaveValorizationDevaluation(FixedAssetTransactionXml As String, ListDeleteString As List(Of String), codeUser As String) As SP_SaveFixedAssetTransaction_Result

    Function GetFixedAssetPhysicalAssetPartstById(Id As Integer) As FixedAssetPhysicalAssetParts

    Function GetFixedAssetPhysicalAssetById(Id As Integer) As FixedAssetPhysicalAsset

    ''' <summary>
    ''' Obtiene el libro oficial de contabilidad
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetLegalBook() As LegalBook

    ''' <summary>
    ''' Obtiene el inventario fisico con el agregado del catalogo de articulo
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPhysicalAssetWithFixedAssetCatalog(Id As Integer) As FixedAssetPhysicalAsset

End Interface
