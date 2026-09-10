'***********************************************************************
' Assembly         : Domain.FixedAsset
' Author           : Jeisson Herrera Peña
' Created          : 27/01/2016
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Base
Imports Domain.Entities

#End Region

Public Interface ISettingFixedAssetRepository

    Inherits IRepository(Of SettingFixedAsset)

    ''' <summary>
    ''' funcion que lista todas los tipos de poliza
    ''' </summary>
    ''' <returns>Lista de tipos de poliza</returns>
    Function GetSettingFixedAssetByOperatingUnitId(OperatingUnitId As Integer, Optional tracking As Boolean = True) As SettingFixedAsset

End Interface
