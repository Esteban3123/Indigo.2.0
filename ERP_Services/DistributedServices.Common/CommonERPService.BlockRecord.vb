'***********************************************************************
' Assembly         : DistributedServices.Common
' Author           : Juan Diego Diaz
' Created          : 03-10-2013
'
' Copyright        : (c) . All rights reserved.

Imports System.Configuration
Imports System.Data.SqlClient
Imports Application.Common
Imports Infrastructure.CrossCutting.IOC

Partial Class CommonERPService
    Implements ICommonERPBlockRecord

    ''' <summary>
    ''' Elimina una registro bloqueado
    ''' </summary>
    ''' <param name="blockRecord">Registro bloqueado</param>
    ''' <param name="session">Objeto session</param>
    ''' <returns>ActionResult</returns>
    Public Function DeleteBlockRecord(blockRecord As Domain.Entities.BlockRecord, session As Infrastructure.CrossCutting.Base.SessionValues) As Domain.Base.Entities.ActionResult Implements ICommonERPBlockRecord.DeleteBlockRecord
        Using blockRecordAdminService As IBlockRecordAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IBlockRecordAdminService)()
            Return blockRecordAdminService.DeleteBlockRecord(blockRecord, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene una registro de bloqueo según parametros
    ''' </summary>
    ''' <param name="IdForm">Id del formulario</param>
    ''' <param name="IdRecord">Id del registro</param>
    ''' <param name="session">Objeto session</param>
    ''' <returns>Registro bloqueado</returns>
    Public Function GetBlockRecordByIdformAndIdRecord(IdForm As String, IdRecord As String, session As Infrastructure.CrossCutting.Base.SessionValues) As Domain.Entities.BlockRecord Implements ICommonERPBlockRecord.GetBlockRecordByIdformAndIdRecord
        Using blockRecordAdminService As IBlockRecordAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IBlockRecordAdminService)()
            Return blockRecordAdminService.GetBlockRecordByIdformAndIdRecord(IdForm, IdRecord)
        End Using
    End Function

    ''' <summary>
    ''' Lista Todos los registros bloqueados
    ''' </summary>
    ''' <param name="session">Objeto session</param>
    ''' <returns>Registros bloqueados</returns>
    Public Function ListAllBlockRecord(session As Infrastructure.CrossCutting.Base.SessionValues) As List(Of Domain.Entities.BlockRecord) Implements ICommonERPBlockRecord.ListAllBlockRecord
        Using blockRecordAdminService As IBlockRecordAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IBlockRecordAdminService)()
            Return blockRecordAdminService.ListAllBlockRecord()
        End Using
    End Function

    ''' <summary>
    ''' Almacena o Actualiza registro bloqueado
    ''' </summary>
    ''' <param name="blockRecord">Registro bloqueado</param>
    ''' <param name="session">Objeto session</param>
    ''' <returns>ActionResult</returns>
    Public Function SaveBlockRecord(blockRecord As Domain.Entities.BlockRecord, session As Infrastructure.CrossCutting.Base.SessionValues) As Domain.Base.Entities.ActionResult(Of Domain.Entities.BlockRecord) Implements ICommonERPBlockRecord.SaveBlockRecord
        Using blockRecordAdminService As IBlockRecordAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IBlockRecordAdminService)()
            Return blockRecordAdminService.SaveBlockRecord(blockRecord, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Desbloquear un registro
    ''' </summary>
    ''' <param name="CodUser"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SP_UnlockBlockRecord(CodUser As String, session As Infrastructure.CrossCutting.Base.SessionValues) As Boolean Implements ICommonERPBlockRecord.SP_UnlockBlockRecord
        'Using blockRecordAdminService As IBlockRecordAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IBlockRecordAdminService)()
        'Return blockRecordAdminService.SP_UnlockBlockRecord(CodUser, session)
        Try
            Dim dtResult As New DataTable()
            Dim conn As New SqlConnection(String.Format(ConfigurationManager.ConnectionStrings(Infrastructure.CrossCutting.Base.ConfigurationFile.CONX_GENESIS).ConnectionString, "", session.TransactionalContainer))
            conn.Open()
            Dim command As New SqlCommand("[Common].[SP_UnlockBlockRecord]", conn)
            command.CommandType = CommandType.StoredProcedure
            command.Parameters.AddWithValue("CodUser", CodUser)
            Using resultado As SqlDataReader = command.ExecuteReader(CommandBehavior.CloseConnection)
                dtResult.Load(resultado)
                conn.Close()
            End Using
            Return dtResult.Rows.Count > 0 AndAlso dtResult.Rows(0)("Resultado").ToString().Equals("1")
        Catch ex As Exception
            Return False
        End Try
    End Function
End Class
