'***********************************************************************
' Assembly         : Domain.FixedAsset
' Author           : Jeisson Herrera Peña
' Created          : 30/09/2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Base
Imports Domain.Entities

#End Region

Public Interface IFixedAssetLocationTypeRepository
    Inherits IRepository(Of FixedAssetLocationType)

   ''' <summary>
    ''' funcion que lista todas los tipos de ubicacion
    ''' </summary>
    ''' <returns>Lista tipos de ubicacion</returns>
    Function ListAllLocationType() As List(Of FixedAssetLocationType)

End Interface
