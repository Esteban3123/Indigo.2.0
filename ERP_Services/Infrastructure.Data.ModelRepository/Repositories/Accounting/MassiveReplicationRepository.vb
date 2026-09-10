'***********************************************************************
' Assembly         : Infrastructure.Data.AccountingRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 09/06/2017
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Data.Entity.Infrastructure
#End Region

Public Class MassiveReplicationRepository
    Inherits GenericRepository(Of JournalVouchers)
    Implements IMassiveReplicationRepository

    'Devuelve el contexto en este repositorio 
    Private _context As IGlobalModelUnitOfWork

#Region "Builder"
    ''' <summary>
    '''Inicializa la nueva instancia de clase.
    ''' </summary>
    ''' <param name="contex">El contexto.</param>
    Public Sub New(ByVal contex As IGlobalModelUnitOfWork)
        MyBase.New(contex)
        _context = contex
    End Sub
#End Region

#Region "Functions"

    ''' <summary>
    ''' Consume el sp de replicación masiva
    ''' </summary>
    ''' <param name="InitialDate"></param>
    ''' <param name="EndDate"></param>
    ''' <param name="BookOriginId"></param>
    ''' <param name="BookDestinationId"></param>
    ''' <param name="CodeInitialJournalVoucherType"></param>
    ''' <param name="CodeEndJournalVoucherType"></param>
    ''' <param name="CodeUser"></param>
    ''' <returns></returns>
    Public Function SP_MassiveReplication(InitialDate As Date?, EndDate As Date?, BookOriginId As Integer, BookDestinationId As Integer, CodeInitialJournalVoucherType As String, CodeEndJournalVoucherType As String, CodeUser As String) As SP_MassiveReplication_Result Implements IMassiveReplicationRepository.SP_MassiveReplication
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_MassiveReplication(InitialDate, EndDate, BookOriginId, BookDestinationId, CodeInitialJournalVoucherType, CodeEndJournalVoucherType, CodeUser).SingleOrDefault
    End Function

#End Region

End Class
