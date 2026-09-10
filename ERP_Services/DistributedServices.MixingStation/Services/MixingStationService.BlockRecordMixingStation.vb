'***********************************************************************
' Assembly         : DistributedServices.MixingStation
' Author           : Yoe Andres Cardenas
' Created          : 26-04-2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Application.MixingStation
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports Microsoft.Practices.Unity
Partial Class MixingStationService

    ''' <summary>
    ''' Elimina una registro bloqueado
    ''' </summary>
    ''' <returns>
    ''' ActionResult
    ''' </returns>
    Public Function DeleteBlockRecordMixingStation(blockRecordMixingStation As BlockRecordMixingStation) As ActionResult Implements IMixingStationBlockRecordMixingStation.DeleteBlockRecordMixingStation
        Using service As IBlockRecordMixingStationAdminService = Container.Current.Resolve(Of IBlockRecordMixingStationAdminService)()
            Return service.DeleteBlockRecordMixingStation(blockRecordMixingStation)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene el bloque del registro de central de mezclas por IdForm y registro del identificador
    ''' </summary>
    ''' <param name="IdForm">The identifier form.</param>
    ''' <param name="IdRecord">Consecutive.</param>
    ''' <returns></returns>
    Public Function GetBlockRecordMixingStationByIdformAndIdRecord(IdForm As String, IdRecord As String) As BlockRecordMixingStation Implements IMixingStationBlockRecordMixingStation.GetBlockRecordMixingStationByIdformAndIdRecord
        Using service As IBlockRecordMixingStationAdminService = Container.Current.Resolve(Of IBlockRecordMixingStationAdminService)()
            Return service.GetBlockRecordMixingStationByIdformAndIdRecord(IdForm, IdRecord)
        End Using
    End Function

    ''' <summary>
    ''' Almacena o Actualiza registro bloqueado
    ''' </summary>
    ''' <returns>
    ''' ActionResult
    ''' </returns>
    Public Function SaveBlockRecordMixingStation(blockRecordMixingStation As BlockRecordMixingStation) As ActionResult(Of BlockRecordMixingStation) Implements IMixingStationBlockRecordMixingStation.SaveBlockRecordMixingStation
        Using service As IBlockRecordMixingStationAdminService = Container.Current.Resolve(Of IBlockRecordMixingStationAdminService)()
            Return service.SaveBlockRecordMixingStation(blockRecordMixingStation)
        End Using
    End Function

End Class
