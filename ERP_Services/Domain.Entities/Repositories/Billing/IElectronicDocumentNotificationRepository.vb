'************************************************************
' Assembly         : Domain.Billing
' Author           : Miguel Angel Fonseca Castro
' Created          : 2020-01-31
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Domain.Base
Imports Domain.Entities

#End Region

Public Interface IElectronicDocumentNotificationRepository
    Inherits IRepository(Of ElectronicDocumentNotification)

    ''' <summary>
    ''' Obtiene una notificación de un documento electronico usado para la facturación electronica por el id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    Function GetElectronicDocumentNotificationById(ByVal Id As Integer) As ElectronicDocumentNotification

    ''' <summary>
    ''' Obtiene una notificación de un listado de documento electronico de acuerdo con el estado en que se encuentren
    ''' </summary>
    ''' <param name="status">The identifier.</param>
    ''' <returns></returns>
    Function GetElectronicDocumentNotificationIdsByStatus(ByVal status As Boolean) As List(Of Integer)

    ''' <summary>
    ''' Obtiene el tipo de nota asociado al documento electrónico
    ''' </summary>
    ''' <param name="electronicDocumentId"></param>
    ''' <returns></returns>
    Function GetElectronicNoteType(electronicDocumentId As Integer) As Byte

End Interface
