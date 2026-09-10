'***********************************************************************
' Assembly         : Infrastructure.Data.MixingStationRepository
' Author           : Yoe Andres Cardenas
' Created          : 24-04-2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Domain.Base.Entities
Imports System.Configuration
Imports Infrastructure.CrossCutting.Base
Imports System.Data.SqlClient
Imports Domain.Base

#End Region

''' <summary>
''' Repositorio de la entidad secuencia numerica
''' </summary>
Public Class MixingStationSequenceDetailRepository
    Inherits GenericRepository(Of MixingStationSequenceDetail)
    Implements IMixingStationSequenceDetailRepository, Inject

#Region "Fields"

    ''' <summary>
    ''' Contexto de contabilidad
    ''' </summary>
    Private _context As IGlobalModelUnitOfWork

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="acountingContext">Contexto de cintabilidad</param>
    Public Sub New(ByVal acountingContext As IGlobalModelUnitOfWork)
        MyBase.New(acountingContext)
        Me._context = acountingContext
    End Sub

#End Region

#Region "IMixingStationSequenceRepository"
    ''' <summary>
    ''' Obtiene un detalle de secuencia numerica por su id
    ''' </summary>
    ''' <param name="id">Id del detalle</param>
    ''' <returns>Detalle de la secuencia numerica</returns>
    Public Function GetSequenceDById1(id As Integer) As MixingStationSequenceDetail Implements IMixingStationSequenceDetailRepository.GetSequenceDById
        Dim result = (From s As MixingStationSequenceDetail In Me._context.MixingStationSequenceDetail.Include("MixingStationSequence").Include("Sequense") Where s.Id = id).ToList()
        If result IsNot Nothing AndAlso result.Count > 0 Then
            Return result(0)
        Else
            Return New MixingStationSequenceDetail()
        End If
    End Function
    ''' <summary>
    ''' Obtiene la secuencia numerica primero haciendo el update para bloquear la tabla y no hayan problemas de concurrencia
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Public Function GetSequenceDetailUpdatedById(id As Int32) As MixingStationSequenceDetail Implements IMixingStationSequenceDetailRepository.GetSequenceDetailUpdatedById
        Dim connectionString As String = String.Format(ConfigurationManager.ConnectionStrings(Infrastructure.CrossCutting.Base.ConfigurationFile.CONX_GENESIS).ConnectionString, "",
            ServerSessionValues.Current.CurrentContainer)
        Dim queryString As String = " UPDATE MixingStation.MixingStationSequenceDetail SET Next = Next + 1 WHERE Id = " & id & " "
        Using connection As New SqlConnection(connectionString)
            Dim Command As New SqlCommand(queryString, connection)
            Command.Connection.Open()
            Command.ExecuteNonQuery()
        End Using

        Dim result = (From s As MixingStationSequenceDetail In Me._context.MixingStationSequenceDetail.AsNoTracking().Include("MixingStationSequence").AsNoTracking().Include("Sequense").AsNoTracking() Where s.Id = id).ToList()
        If result IsNot Nothing AndAlso result.Count > 0 Then
            result(0).Next -= 1
            result(0).MarkAsUnchanged()
            Return result(0)
        Else
            Return New MixingStationSequenceDetail()
        End If
    End Function
#End Region

End Class
