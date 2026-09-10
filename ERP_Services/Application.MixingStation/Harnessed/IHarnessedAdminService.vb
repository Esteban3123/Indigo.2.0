'***********************************************************************
' Assembly         : Application.MixingStation
' Author           : Giovanny Plazas L
' Created          : 16-09-2021
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

Public Interface IHarnessedAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Guarda aprovechamientos
    ''' </summary>
    ''' <param name="ListHarnessed">The identifier.</param>
    ''' <param name="audit">The identifier.</param>
    Function SaveHarnessed(ByVal ListHarnessed As List(Of Harnessed), ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Funcion para registrar un aprovechamiento
    ''' </summary>
    ''' <param name="ListQuantityRemaining"></param>
    ''' <param name="CampaignDetailId"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Function RegisterHarnessed(ByVal ListQuantityRemaining As List(Of QuantityRemaining), ByVal CampaignDetailId As Integer, ByVal audit As AuditMessage) As ActionResult

End Interface