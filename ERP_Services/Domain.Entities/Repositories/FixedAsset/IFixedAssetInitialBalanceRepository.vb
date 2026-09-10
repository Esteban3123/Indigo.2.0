'************************************************************
' Assembly         : Domain.FixedAsset
' Author           : Carlos Mario Arias Rubiano
' Created          : 12/04/2016
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

Public Interface IFixedAssetInitialBalanceRepository
    Inherits IRepository(Of FixedAssetInitialBalance)

    ''' <summary>
    ''' Obtiene un saldo inicial por código
    ''' </summary>
    ''' <param name="Code"></param>
    Function GetFixedAssetInitialBalance(ByVal Code As String) As FixedAssetInitialBalance

    ''' <summary>
    ''' Obtiene un saldo inicial por id
    ''' </summary>
    ''' <param name="Id"></param>
    Function GetFixedAssetInitialBalanceById(ByVal Id As Integer) As FixedAssetInitialBalance

    ''' <summary>
    ''' Elimina los detalles del saldo inicial
    ''' </summary>
    ''' <param name="ListString">Objeto xml armado con el listado que se envia desde presentation</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SP_SaveFixedAssetInitialBalance(ListString As List(Of String), codeUser As String) As SP_SaveFixedAssetInitialBalance_Result

    ''' <summary>
    ''' Inserta en las tablas de physicalAsset cuando se confirma el saldo inicial
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SP_ConfirmFixedAssetInitialBalance(FixedAssetInitialBalanceId As Integer) As SP_ConfirmFixedAssetInitialBalance_Result

    ''' <summary>
    ''' Copiar y pegar para el formulario de saldo inicial de activos fijos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SP_CopyAndPasteFixedAssetInitialBalance(XmlObject As String) As List(Of SP_CopyAndPasteFixedAssetInitialBalance_Result)

End Interface
