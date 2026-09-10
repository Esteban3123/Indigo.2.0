Imports System.Configuration
Imports Application.Common
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.IOC

Partial Class CommonERPService

    Public Function GetOperatingUnitByCode(code As String, session As SessionValues) As ActionResult(Of OperatingUnit) Implements ICommonERPOperatingUnit.GetOperatingUnitByCode
        Using UnitService As IOperatingUnitAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IOperatingUnitAdminService)()
            Return UnitService.GetOperatingUnitByCode(code, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' obtiene una unidad operativa por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetOperatingUnitById(id As Integer, session As SessionValues) As ActionResult(Of OperatingUnit) Implements ICommonERPOperatingUnit.GetOperatingUnitById
        Using UnitService As IOperatingUnitAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IOperatingUnitAdminService)()
            Return UnitService.GetOperatingUnitById(id, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Guarda o actualiza una unidad operativa
    ''' </summary>
    ''' <param name="operatingUnit"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveOperatingUnit(operatingUnit As OperatingUnit, session As SessionValues) As ActionResult(Of OperatingUnit) Implements ICommonERPOperatingUnit.SaveOperatingUnit
        Using UnitService As IOperatingUnitAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IOperatingUnitAdminService)()
            Return UnitService.SaveOperatingUnit(operatingUnit, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Elimina una unidad operativa
    ''' </summary>
    ''' <param name="operatingUnit"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteOperatingUnit(operatingUnit As OperatingUnit, session As SessionValues) As ActionResult Implements ICommonERPOperatingUnit.DeleteOperatingUnit
        Using UnitService As IOperatingUnitAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IOperatingUnitAdminService)()
            Return UnitService.DeleteOperatingUnit(operatingUnit, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Lista todas las unidades operativas
    ''' </summary>
    ''' <returns>Lista de unidades operativas</returns>
    ''' <remarks></remarks>
    Public Function ListAllOperatingUnit(session As Infrastructure.CrossCutting.Base.SessionValues, transactionContainer As String) As List(Of Domain.Entities.OperatingUnit) Implements ICommonERPOperatingUnit.ListAllOperatingUnit
        Using UnitService As IOperatingUnitAdminService = IocFactory.Instance(transactionContainer).CurrentContainer.Resolve(Of IOperatingUnitAdminService)()
            Return UnitService.ListAllOperatingUnit()
        End Using
    End Function

    Public Function ListAllOperatingUnitCommand(session As Infrastructure.CrossCutting.Base.SessionValues, transactionContainer As String) As List(Of Domain.Entities.OperatingUnit) Implements ICommonERPOperatingUnit.ListAllOperatingUnitCommand

        Dim sql As New Text.StringBuilder()

        sql.AppendLine(" SELECT OP.Id, OP.IdUnit, OP.UnitCode, OP.UnitName, OP.IPSCode, OP.Address, OP.Phone, OP.Email, OP.IdCity FROM Common.OperatingUnit OP WITH(NOLOCK) ")

        Dim dtDatos As New DataTable("OperatingUnit")
        Dim conx = String.Format(ConfigurationManager.ConnectionStrings(Infrastructure.CrossCutting.Base.ConfigurationFile.CONX_GENESIS).ConnectionString, "", transactionContainer)
        Dim conexion As New SqlClient.SqlConnection(conx)
        Dim da As SqlClient.SqlDataAdapter = New SqlClient.SqlDataAdapter(sql.ToString(), conexion)
        da.SelectCommand.CommandTimeout = 90
        Dim ds As New DataSet
        da.Fill(ds, "OperatingUnit")
        dtDatos = ds.Tables("OperatingUnit")
        conexion.Close()

        If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
            Dim operationUnits As New List(Of OperatingUnit)()
            For Each op As DataRow In ds.Tables(0).Rows
                Dim ou = New OperatingUnit()
                With ou
                    .Id = Convert.ToInt32(op("Id"))
                    .IdUnit = If(op("IdUnit").Equals(DBNull.Value), CType(Nothing, Integer?), Convert.ToInt32(op("IdUnit")))
                    .UnitCode = op("UnitCode").ToString()
                    .UnitName = op("UnitName").ToString()
                    .IPSCode = op("IPSCode").ToString()
                    .Address = op("Address").ToString()
                    .Phone = op("Phone").ToString()
                    .Email = op("Email").ToString()
                    .IdCity = Convert.ToInt32(op("IdCity"))
                    .OperatingUnitDescription = String.Format("{0} {1}", op("UnitCode").ToString(), op("UnitName").ToString())
                End With
                operationUnits.Add(ou)
            Next
            Return operationUnits
        Else
            Return New List(Of OperatingUnit)()
        End If

    End Function

End Class
