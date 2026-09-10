'***********************************************************************
' Assembly         : Presentacion.MixingStation.MVP
' Author           : Giovanny Plazas Lozano
' Created          : 29/08/2022
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Base
Imports Presentation.Controls
Imports Domain.Entities
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo.MixingStationRepository
#End Region

Public Interface IReadjustments

#Region "Properties"
    ' Property ListViewReadjustments As List(Of ViewReadjustmentsXpo)
    ''' <summary>
    ''' Establece el Id de la central de mezclas para mostrar las readecuaciones
    ''' </summary>
    ''' <returns></returns>
    Property CMConfigurationId As Integer?

    ''' <summary>
    ''' Establece el datasource de la rejilla
    ''' </summary>
    ''' <returns></returns>
    Property ListViewReadjustmentsXpo As List(Of ViewReadjustmentsXpo)

    ''' <summary>
    ''' lista de la centrales de mezclas
    ''' </summary>
    ''' <returns></returns>
    Property ListCMConfiguration As List(Of MixinStationCMConfigXpo)

    ''' <summary>
    ''' obtiene el tag del formulario
    ''' </summary>
    ''' <returns></returns>
    ReadOnly Property MyTag As Object
#End Region

#Region "Methods"
    Sub SendToMixingStation(ByVal _selectedItems As List(Of ViewReadjustmentsXpo))
#End Region
End Interface
