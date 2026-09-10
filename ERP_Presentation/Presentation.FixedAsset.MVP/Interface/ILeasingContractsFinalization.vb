'***********************************************************************
' Assembly         : Presentacion.FixedAsset.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 23/03/2017
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Base
Imports DevExpress.Xpo
Imports Presentation.Controls
Imports Domain.Entities
Imports Infrastructure.Data.Xpo.FixedAssetRepository

#End Region

Public Interface ILeasingContractsFinalization
    Inherits IcrudBase

    ''' <summary>
    ''' Año de los parametros
    ''' </summary>
    ''' <returns></returns>
    Property Year As Integer

    ''' <summary>
    ''' Mes de los parametros
    ''' </summary>
    ''' <returns></returns>
    Property Month As Integer

    ''' <summary>
    ''' Xpo de parametros de activos
    ''' </summary>
    ''' <returns></returns>
    Property SettingsFixedAssetXpo As SettingFixedAssetXpo

End Interface
