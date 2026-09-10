'***********************************************************************
' Assembly         : DistributedService.MixingStation
' Author           : Ruben Dario Castañeda Giraldo
' Created          : 06-05-2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Application.MixingStation
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.IOC
Imports Microsoft.Practices.Unity
#End Region
Partial Class MixingStationService
    Implements IMixingStationCenterAttention
    ''' <summary>
    ''' Trae los parámetros de central de mezclas
    ''' </summary>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function ListAllCMCenterAttention(audit As AuditMessage) As List(Of CMCenterAttention) Implements IMixingStationCenterAttention.ListAllCMCenterAttention
        'Using service As ICMConfigAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ICMConfigAdminService)()
        Using service As ICMCenterAttention = Container.Current.Resolve(Of ICMCenterAttention)()
            Return service.ListAllCMCenterAttention(audit)
        End Using
    End Function
    ''' <summary>
    ''' Guarda la asociacion entre el centro de atencion y la central de mezclas
    ''' </summary>
    Public Function SaveCMCenterAttention(cmCenterAttention As CMCenterAttention) As ActionResult(Of CMCenterAttention) Implements IMixingStationCenterAttention.SaveCMCenterAttention
        Using service As ICMCenterAttention = Container.Current.Resolve(Of ICMCenterAttention)()
            Return service.SaveCMCenterAttention(cmCenterAttention)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene un turno por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetCMCenterAttention(ByVal id As String) As ActionResult(Of CMCenterAttention) Implements IMixingStationCenterAttention.GetCMCenterAttention
        Using service As ICMCenterAttention = Container.Current.Resolve(Of ICMCenterAttention)()
            Return service.GetCMCenterAttention(id)
        End Using
    End Function
    Public Function UpdateStateCMCenterAttention(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of CMCenterAttention) Implements IMixingStationCenterAttention.UpdateStateCMCenterAttention
        Using service As ICMCenterAttention = Container.Current.Resolve(Of ICMCenterAttention)()
            Return service.UpdateStateCMCenterAttention(code, state, audit)
        End Using
    End Function
    ''' <summary>
    ''' Obtiene un tipo de dosis unitaria por id
    ''' </summary>
    ''' <param name="Id_MixingStation">The identifier.</param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function ListAllCMMixingProducitonLine(Id_MixingStation As Integer, audit As AuditMessage) As List(Of Tuple(Of Integer, String)) Implements IMixingStationCenterAttention.ListAllCMMixingProducitonLine
        Using service As ICMCenterAttention = Container.Current.Resolve(Of ICMCenterAttention)()
            Return service.ListAllCMMixingProducitonLine(Id_MixingStation, audit)
        End Using
    End Function
End Class
