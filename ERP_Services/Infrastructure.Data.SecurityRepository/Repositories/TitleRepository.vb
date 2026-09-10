'***********************************************************************
' Assembly         : Infrastructure.Data.SecurityRepository
' Author           : Jhon Tovar
' Created          : 05-04-2022
'
' Last Modified By :
' Last Modified On :
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports System.Configuration
Imports Domain.Security
Imports Domain.Security.Entities
Imports Infrastructure.CrossCutting.Interface

''' <summary>
''' Esta clase contiene cada uno de los metodos y funciones del repositorio
''' en Infraestructura.Base	 ademas implementa de la interfaz ubicada en la capa de Dominio.Seguridad. Esta clase es la encargada 
''' realizar las respectivas consultas y operaciones del CRUD para la entidad de Title.
''' </summary>
Public Class TitleRepository
    Implements ITitleRepository

    ''' <summary>
    ''' Guardar titulo
    ''' </summary>
    ''' <param name="title"></param>
    ''' <returns></returns>
    Public Function SaveTitle(title As Title) As Boolean Implements ITitleRepository.SaveTitle
        Using conx As New ConectionSQL(ConfigurationManager.AppSettings(Infrastructure.CrossCutting.Base.ConfigurationFile.SECURITY_CONTAINER_PARAMETER_NAME))
            Dim query = "INSERT INTO [Security].Title (Id,[Name],[State]) VALUES (@Id,@Name,1)"

            conx.AddParam("Id", SqlDbType.Int, title.IdTitle)
            conx.AddParam("Name", SqlDbType.VarChar, title.TitleName)

            Return conx.ExecuteCommandParams(query)
        End Using
    End Function

    ''' <summary>
    ''' Actualizar titulo
    ''' </summary>
    ''' <param name="title"></param>
    ''' <returns></returns>
    Public Function UpdateTitle(title As Title) As Boolean Implements ITitleRepository.UpdateTitle
        Using conx As New ConectionSQL(ConfigurationManager.AppSettings(Infrastructure.CrossCutting.Base.ConfigurationFile.SECURITY_CONTAINER_PARAMETER_NAME))
            Dim query = "UPDATE [Security].Title SET 
                        [Name] = @Name
                        WHERE Id = @Id"

            conx.AddParam("Id", SqlDbType.Int, title.IdTitle)
            conx.AddParam("Name", SqlDbType.VarChar, title.TitleName)

            Return conx.ExecuteCommandParams(query)
        End Using
    End Function

    ''' <summary>
    ''' Consultar titulo
    ''' </summary>
    ''' <param name="IdTitle"></param>
    ''' <returns></returns>
    Public Function GetTitle(IdTitle As Integer) As Title Implements ITitleRepository.GetTitle
        Using conx As New ConectionSQL(ConfigurationManager.AppSettings(Infrastructure.CrossCutting.Base.ConfigurationFile.SECURITY_CONTAINER_PARAMETER_NAME))
            Dim res As Title = New Title()

            Dim query = "SELECT Id,[Name],[State] FROM [Security].Title WHERE Id = @Id"

            conx.AddParam("Id", SqlDbType.Int, IdTitle)
            Dim dt As DataTable = conx.ExecuteCommandParams_Data(query)

            If dt.Rows.Count > 0 Then
                res.IdTitle = CInt(dt.Rows(0)("Id"))
                res.TitleName = dt.Rows(0)("Name").ToString()
                res.State = Convert.ToByte(dt.Rows(0)("State"))
            End If

            Return res
        End Using
    End Function

    ''' <summary>
    ''' eliminar titulo
    ''' </summary>
    ''' <param name="IdTitle"></param>
    ''' <returns></returns>
    Public Function DeleteTitle(IdTitle As Integer) As Boolean Implements ITitleRepository.DeleteTitle
        Dim Result As Boolean
        Dim query As String
        Using conx As New ConectionSQL(ConfigurationManager.AppSettings(Infrastructure.CrossCutting.Base.ConfigurationFile.SECURITY_CONTAINER_PARAMETER_NAME))
            Try
                If conx.sqlWebConection.State = ConnectionState.Closed Then
                    conx.sqlWebConection.Open()
                End If

                conx.InTransaction = True
                conx.IndigoTransaction = conx.sqlWebConection.BeginTransaction(IsolationLevel.ReadCommitted, "Eliminar Titulo")
                conx.AddParam("Id", SqlDbType.Int, IdTitle)

                query = "DELETE FROM Security.Title WHERE Id = @Id"
                conx.ExecuteCommandParams(query, False)

                conx.IndigoTransaction.Commit()
                Result = True
            Catch ex As Exception
                conx.IndigoTransaction.Rollback()
                Result = False
            Finally
                conx.sqlWebConection.Close()
            End Try

            Return Result
        End Using
    End Function

    ''' <summary>
    ''' Cambiar estado de titulo
    ''' </summary>
    ''' <param name="IdTitle"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    Public Function ChangeStateTitle(IdTitle As Integer, state As Byte) As Boolean Implements ITitleRepository.ChangeStateTitle
        Using conx As New ConectionSQL(ConfigurationManager.AppSettings(Infrastructure.CrossCutting.Base.ConfigurationFile.SECURITY_CONTAINER_PARAMETER_NAME))
            Dim query = "UPDATE Security.Title SET State = @State WHERE Id = @Id"

            conx.AddParam("Id", SqlDbType.Int, IdTitle)
            conx.AddParam("State", SqlDbType.Bit, state)

            Return conx.ExecuteCommandParams(query)
        End Using
    End Function
End Class
