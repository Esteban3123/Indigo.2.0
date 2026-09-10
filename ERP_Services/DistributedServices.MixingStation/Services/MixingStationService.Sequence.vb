'***********************************************************************
' Assembly         : DistributedService.MixingStation
' Author           : Yoe Andres Cardenas
' Created          : 25-04-2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Application.MixingStation
Imports Microsoft.Practices.Unity

#End Region

Partial Class MixingStationService

    ''' <summary>
    ''' Obtiene la configuración de secuencia numerica asignada al frontal
    ''' </summary>
    ''' <param name="idForm">Id del frontal a consultar</param>
    ''' <returns>Secuencia numerica asignada al frontal</returns>
    Public Function GetSequenceByIdForm(idForm As String) As Domain.Entities.MixingStationSequence Implements IMixingStationServiceSequence.GetSequenceByIdForm
        Using service As IMixingStationSequenceAdminService = Container.Current.Resolve(Of IMixingStationSequenceAdminService)()
            Return service.GetSequenceByIdForm(idForm)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene un grupo de secuencias numericas por su id de configuración
    ''' </summary>
    ''' <param name="id">Id de la configuración de la secuencia</param>
    ''' <returns>Grupo de secuencias numericas</returns>
    Public Function GetNumericSequenceGroupById(id As Integer) As List(Of String) Implements IMixingStationServiceSequence.GetNumericSequenceGroupById
        Using service As IMixingStationSequenceAdminService = Container.Current.Resolve(Of IMixingStationSequenceAdminService)()
            Return service.GetNumericSequenceGroupById(id)
        End Using
    End Function

    Public Function SaveSequence(seq As Domain.Entities.MixingStationSequence) As Domain.Base.Entities.ActionResult Implements IMixingStationServiceSequence.SaveSequence
        Using service As IMixingStationSequenceAdminService = Container.Current.Resolve(Of IMixingStationSequenceAdminService)()
            Return service.SaveSequence(seq)
        End Using
    End Function

End Class
