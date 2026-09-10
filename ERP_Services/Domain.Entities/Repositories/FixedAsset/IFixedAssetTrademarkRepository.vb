'************************************************************
' Assembly         : Domain.Maintenance
' Author           : Daniel Eduardo Arévalo
' Created          : 15-09-2014
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Importar"
Imports Domain.Base.Entities
Imports Domain.Base
#End Region

Public Interface IFixedAssetTrademarkRepository

    Inherits IRepository(Of FixedAssetTrademark)

    ''' <summary>
    ''' Función que obtiene todas las Marcas par alos Equipos
    ''' </summary>
    ''' <returns>Lista de Marcas</returns>
    ''' <remarks></remarks>
    Function ListAllTrademark() As List(Of FixedAssetTrademark)

    ''' <summary>
    ''' Función que obtiene una marca por Código
    ''' </summary>
    ''' <param name="Code">Código de la Marca</param>
    ''' <returns>Trademark</returns>
    ''' <remarks></remarks>
    Function GetTrademarkByCode(Code As String, Optional desatach As Boolean = True) As FixedAssetTrademark

End Interface
