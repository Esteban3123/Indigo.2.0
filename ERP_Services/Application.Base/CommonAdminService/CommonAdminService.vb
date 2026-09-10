'***********************************************************************
' Assembly         : Application.Common
' Author           : Cristhian Mauricio Salazar
' Created          : 17-04-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.Data.SqlClient
Imports System.Configuration

Public Class CommonAdminService
    Implements ICommonAdminService



    ''' <summary>
    ''' Consulta los campos nulos para una tabla en un esquema especifico
    ''' </summary>
    ''' <param name="schema">Esquema</param>
    ''' <param name="nameTable">Nombre de la tabla</param>
    ''' <returns>DataSet con el conjunto de campos que son null</returns>
    ''' <remarks></remarks>
    Public Function GetFieldsNull(company As String, schema As String, nameTable As String) As DataSet Implements ICommonAdminService.GetFieldsNull
        Dim dtDatos As New DataTable("CamposNULL")
        Using conexion As New SqlConnection(String.Format(ConfigurationManager.ConnectionStrings("EFConnection").ConnectionString, company.Trim()))
            Dim query As String = "SELECT SC.NAME,SC.isnullable FROM sysobjects SO INNER JOIN syscolumns SC ON SO.ID = SC.ID WHERE so.name = '" & nameTable & "' and SCHEMA_NAME(uid) = '" & schema & "' and SC.isnullable='1'"
            Dim da As SqlDataAdapter = New SqlDataAdapter(query, conexion)
            'establezco tiempos
            da.SelectCommand.CommandTimeout = 90
            Dim ds As New DataSet
            da.Fill(ds, "NULL")
            dtDatos = ds.Tables("NULL")
            conexion.Close()
            Return ds
        End Using
    End Function

End Class
