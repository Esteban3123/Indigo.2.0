'************************************************************
' Assembly         : Domain.Entities.Service
' Author           : Carlos Mario Arias Rubiano
' Created          : 09/06/2017
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Domain.Entities
Imports Domain.Base

#End Region

''' <summary>
''' Contrato de repositorio para la entidad clase contable
''' </summary>
Public Interface IMassiveReplicationRepository
    Inherits IRepository(Of JournalVouchers)

    ''' <summary>
    ''' Consume el sp de replicación masiva
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SP_MassiveReplication(InitialDate As Date?, EndDate As Date?, BookOriginId As Integer, BookDestinationId As Integer, CodeInitialJournalVoucherType As String, CodeEndJournalVoucherType As String, CodeUser As String) As SP_MassiveReplication_Result

End Interface