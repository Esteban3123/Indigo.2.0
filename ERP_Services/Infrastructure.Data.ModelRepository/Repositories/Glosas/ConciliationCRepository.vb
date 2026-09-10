'************************************************************
' Assembly         : Infraestructure.Data.GlosasRepository
' Author           : Juan Diego Diaz
' Created          : 22-05-2013
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Infrastructure.Data.Base
Imports Domain.Entities

#End Region

''' <summary>
''' Repositorio Cabeceras Conciliación
''' </summary>
Public Class ConciliationCRepository
    Inherits GenericRepository(Of ConciliationC)
    Implements IConciliationCRepository

    'Devuelve el contexto en este repositorio 
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    '''Inicializa la nueva instancia de clase.
    ''' </summary>
    ''' <param name="contex">El contexto.</param>
    Public Sub New(ByVal contex As IGlobalModelUnitOfWork)
        MyBase.New(contex)
        _context = contex
    End Sub

    ''' <summary>
    ''' Función que obtiene una cabecera de conciliación segun código.
    ''' </summary>
    ''' <param name="Id">Id Conciliación Cabecera</param>
    ''' <returns>Objeto Cabecera Conciliacion</returns>
    Public Function GetConciliationC(Id As String, Optional ByVal tracking As Boolean = True) As ConciliationC Implements IConciliationCRepository.GetConciliationC
        Dim Conciliation = From e In _context.ConciliationC.Include("ConciliationParticipants") Where e.Id = CInt(Id) Select e
        If Conciliation.Count > 0 Then
            Dim ConciliationData = Conciliation.SingleOrDefault
            ConciliationData.OriginalValue = (From e In _context.ConciliationC.AsNoTracking.Include("ConciliationParticipants").AsNoTracking Where e.Id = CInt(Id) Select e).SingleOrDefault
            Return ConciliationData
        End If
        Return New ConciliationC
    End Function

    ''' <summary>
    ''' Función que obtiene una cabecera de conciliación segun consecutivo.
    ''' </summary>
    ''' <param name="Consecutive">Consecutivo Conciliación Cabecera</param>
    ''' <returns>Objeto Cabecera Conciliacion</returns>
    Public Function GetConciliationConsecutive(Consecutive As String) As ConciliationC Implements IConciliationCRepository.GetConciliationCByConsecutive
        Dim Conciliation = From e In _context.ConciliationC.Include("ConciliationParticipants") Where e.ConciliationConsecutive = CDec(Consecutive) Select e
        If Conciliation.Count > 0 Then
            Dim ConciliationData = Conciliation.SingleOrDefault
            ConciliationData.OriginalValue = (From e In _context.ConciliationC.AsNoTracking.Include("ConciliationParticipants").AsNoTracking Where e.ConciliationConsecutive = CDec(Consecutive) Select e).SingleOrDefault
            Return ConciliationData
        Else
            Return New ConciliationC
        End If
    End Function

    ''' <summary>
    ''' Guarda y confirma una conciliación
    ''' </summary>
    ''' <param name="xml"></param>
    ''' <param name="UserCode"></param>
    ''' <returns></returns>
    Public Function SP_SaveConciliation(xml As String, UserCode As String) As SP_SaveConciliation_Result Implements IConciliationCRepository.SP_SaveConciliation
        DirectCast(_context, Entity.Infrastructure.IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_SaveConciliation(xml, UserCode).SingleOrDefault
    End Function

End Class
