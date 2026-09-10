Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Runtime.CompilerServices

Public Class ConectionSQL
    Implements ISQL, IDisposable

#Region "Configuracion General"



#Region "Constructor"

    ''' <summary>
    ''' incializa una instancia de la clase/>.
    ''' </summary>
    Public Sub New(company As String)
        Connect(company)
        _InTransaction = False
    End Sub

#End Region

    ''' <summary>
    ''' Lista parametros para SQL
    ''' </summary>
    Private Params As List(Of Tuple(Of String, SqlDbType, Object)) = New List(Of Tuple(Of String, SqlDbType, Object))

    ''' <summary>
    ''' Conectar a SQL
    ''' </summary>
    ''' 
    Public Sub Connect(company As String) Implements ISQL.Connect
        'cargo la configuracion
        'Dim Server As String
        'Server = "192.168.0.212"
        'Dim User As String
        'User = "genesis"
        'Dim Pass As String
        'Pass = "123"

        sqlWebConection = New SqlConnection(Infrastructure.CrossCutting.Base.Utils.GetEntityConnectionString(Infrastructure.CrossCutting.Base.ConfigurationFile.CONX_GENESIS, String.Empty, company, False))
    End Sub



    Private _InTransaction As Boolean
    ''' <summary>
    ''' define si la conexion esta bajo una transaccion
    ''' </summary>
    ''' <value><c>true</c> if [en transaccion]; otherwise, <c>false</c>.</value>
    Public Property InTransaction As Boolean Implements ISQL.InTransaction
        Get
            Return _InTransaction
        End Get
        Set(ByVal value As Boolean)
            _InTransaction = value
        End Set
    End Property

    Private _Transaction As System.Data.SqlClient.SqlTransaction
    ''' <summary>
    ''' Propiedad de Tipo SQlTransacion para Manejo de Transaciones en SQL
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IndigoTransaction As System.Data.SqlClient.SqlTransaction Implements ISQL.IndigoTransaction
        Get
            Return _Transaction
        End Get
        Set(ByVal value As System.Data.SqlClient.SqlTransaction)
            _Transaction = value
        End Set
    End Property

    Private _sqlWebConection As System.Data.SqlClient.SqlConnection
    Private disposedValue As Boolean

    ''' <summary>
    ''' Propiedad que contiene la Conexion Web
    ''' </summary>
    ''' <value>Conexion Web</value>
    ''' <returns>Devuelve la Conexion Web</returns>
    ''' <remarks></remarks>
    Public Property sqlWebConection As System.Data.SqlClient.SqlConnection Implements ISQL.sqlWebConection
        Get
            Return _sqlWebConection
        End Get
        Set(ByVal value As System.Data.SqlClient.SqlConnection)
            _sqlWebConection = value
        End Set
    End Property


#End Region

#Region "SQL"

    ''' <summary>
    ''' Funcion Para ejecutar Una Sentencia SQL
    ''' </summary>
    ''' <param name="Command">Sentencia SQL</param>
    ''' <returns>True -&gt; Correcto</returns>
    ''' <remarks></remarks>
    Public Function ExecuteCommand(ByVal Command As String) As Boolean Implements ISQL.ExecuteCommand
        If sqlWebConection.State = ConnectionState.Closed Then
            sqlWebConection.Open()
        End If
        Using SQL As New SqlCommand(Command, sqlWebConection)
            'aumento el tiempo
            SQL.CommandTimeout = 30000
            'tipo de comando
            SQL.CommandType = CommandType.Text
            If InTransaction = True Then
                SQL.Transaction = IndigoTransaction
            End If
            Try
                Dim IntResult As Integer
                'Ejecutar
                IntResult = SQL.ExecuteNonQuery()
                If IntResult > 0 Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                Throw ex
            Finally
                If InTransaction = False Then
                    sqlWebConection.Close()
                End If
            End Try
        End Using
    End Function


    ''' <summary>
    ''' Funcion Para ejecutar Una Sentencia SQL, y Devover una Tabla
    ''' </summary>
    ''' <param name="Comando">Sentencia SQL</param>
    ''' <returns>True -&gt; Correcto</returns>
    ''' <remarks></remarks>
    Public Function ExecuteCommand_Data(ByVal Comando As String) As System.Data.DataTable Implements ISQL.ExecuteCommand_Data

        If sqlWebConection.State = ConnectionState.Closed Then
            sqlWebConection.Open()
        End If

        Dim da As SqlDataAdapter = New SqlDataAdapter(Comando, sqlWebConection)
        'establesco tiempos
        If InTransaction = True Then
            da.SelectCommand.Transaction = IndigoTransaction
        End If
        da.SelectCommand.CommandTimeout = 90

        Dim ds As New DataSet
        da.Fill(ds, "Datos")

        ExecuteCommand_Data = ds.Tables("Datos")
        da = Nothing
        ds = Nothing
        If InTransaction = False Then
            sqlWebConection.Close()
        End If

        Return ExecuteCommand_Data
    End Function

    ''' <summary>
    ''' Funcion Para ejecutar Una Sentencia SQL con sql command, y Devolver una Tabla
    ''' </summary>
    ''' <param name="Comando">Sentencia SQL</param>
    ''' <returns>True -&gt; Correcto</returns>
    ''' <remarks></remarks>
    Public Function ExecuteSqlCommand_Data(ByVal Comando As String, ByVal NameParameter As String, ByVal ValueParameter As Object) As System.Data.DataTable
        Try
            If sqlWebConection.State = ConnectionState.Closed Then
                sqlWebConection.Open()
            End If
            Using cmd As System.Data.SqlClient.SqlCommand = New System.Data.SqlClient.SqlCommand()
                cmd.Connection = sqlWebConection
                cmd.CommandText = Comando
                cmd.CommandTimeout = 80

                If Not String.IsNullOrEmpty(NameParameter) Then
                    Dim param As System.Data.SqlClient.SqlParameter = New System.Data.SqlClient.SqlParameter()
                    param.ParameterName = "@" & NameParameter
                    param.SqlDbType = SqlDbType.VarChar
                    param.Size = 20
                    param.Value = ValueParameter
                    cmd.Parameters.Add(param)
                End If

                cmd.ExecuteNonQuery()
                sqlWebConection.Close()
                Dim dataAdapt = New SqlDataAdapter()
                dataAdapt.SelectCommand = cmd
                Dim DataTable = New DataTable()
                DataTable.TableName = NameParameter
                dataAdapt.Fill(DataTable)
                If InTransaction = False Then
                    sqlWebConection.Close()
                End If
                If DataTable.Rows.Count > 0 Then
                    Return DataTable
                End If
            End Using
            Return New DataTable()
        Catch ex As Exception
            sqlWebConection.Close()
            Return New DataTable()
        End Try
    End Function
    ''' <summary>
    ''' Funcion para ejecutar un a sentencia SQl y devolver un count
    ''' </summary>
    ''' <param name="Command">Sentencia SQL</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ExecuteCommand_Count(ByVal Command As String) As Integer Implements ISQL.ExecuteCommand_Count
        If sqlWebConection.State = ConnectionState.Closed Then
            sqlWebConection.Open()
        End If
        Dim IntCount As Integer
        Using SQL As New SqlCommand(Command, sqlWebConection)
            'aumento el tiempo
            SQL.CommandTimeout = 90
            SQL.Transaction = IndigoTransaction
            'tipo de comando
            SQL.CommandType = CommandType.Text
            IntCount = SQL.ExecuteScalar()
        End Using
        Return IntCount
    End Function

    Public Function SecurityConnection(ByVal Comando As String, ByVal NameParameter As String, ByVal ValueParameter As String, ContainerSecurity As String) As DataTable
        Using sqlConexionSecurity As New SqlConnection
            Try
                If sqlWebConection.State = ConnectionState.Closed Then
                    sqlWebConection.Open()
                End If

                Using cmd As New System.Data.SqlClient.SqlCommand()
                    cmd.Connection = sqlWebConection
                    cmd.CommandText = Comando

                    Dim param As New System.Data.SqlClient.SqlParameter()

                    param.ParameterName = String.Format("{0}{1}", "@", NameParameter)
                    param.SqlDbType = SqlDbType.VarChar
                    param.Value = ValueParameter
                    cmd.Parameters.Add(param)

                    cmd.ExecuteNonQuery()

                    Dim dataAdapt = New SqlDataAdapter()
                    dataAdapt.SelectCommand = cmd
                    Dim DataTable = New DataTable()
                    DataTable.TableName = "Datos"
                    dataAdapt.Fill(DataTable)
                    If InTransaction = False Then
                        sqlConexionSecurity.Close()
                    End If
                    If DataTable.Rows.Count > 0 Then
                        Return DataTable
                    End If
                End Using
                Return New DataTable()
            Catch ex As SqlException
                sqlConexionSecurity.Close()
                Return New DataTable()
            End Try
        End Using
    End Function

    ''' <summary>
    ''' Funcion para ejecutar una sentencia SQL NO RESULT con parametros, para adicionar parametros se debe usar el metodo AddParam.
    ''' </summary>
    ''' <param name="Command">Enviar comando sql, los parametros adicionar con (@) @State.</param>
    ''' <param name="ClearParams">Limpiar parametros</param>
    ''' <returns></returns>
    Public Function ExecuteCommandParams(ByVal Command As String, Optional ByVal ClearParams As Boolean = True) As Boolean Implements ISQL.ExecuteCommandParams
        If sqlWebConection.State = ConnectionState.Closed Then
            sqlWebConection.Open()
        End If

        Using cmd As SqlCommand = New SqlCommand()

            Dim param As SqlParameter
            Dim vkey As Tuple(Of String, SqlDbType, Object)
            cmd.Connection = sqlWebConection
            cmd.CommandText = Command
            cmd.CommandTimeout = 30000
            cmd.CommandType = CommandType.Text

            For i As Integer = 0 To Params.Count - 1
                param = New SqlParameter()
                vkey = Params(i)
                param.ParameterName = String.Format("{0}{1}", "@", vkey.Item1.ToString)
                param.SqlDbType = vkey.Item2
                param.Value = vkey.Item3
                cmd.Parameters.Add(param)
            Next i

            If InTransaction = True Then
                cmd.Transaction = IndigoTransaction
            End If
            Try
                Dim IntResult As Integer
                'Ejecutar
                IntResult = cmd.ExecuteNonQuery()
                If ClearParams Then
                    Params.Clear()
                End If
                If IntResult > 0 Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                Throw ex
            Finally
                If InTransaction = False Then
                    sqlWebConection.Close()
                End If
            End Try
        End Using
    End Function

    ''' <summary>
    ''' Funcion Para ejecutar Una Sentencia SQL, y Devover una Tabla. Para adicionar parametros se debe usar el metodo AddParam.
    ''' </summary>
    ''' <param name="Comando">Sentencia SQL, los parametros adicionar con (@) @State.</param>
    ''' <returns>True -&gt; Correcto</returns>
    ''' <remarks></remarks>
    Public Function ExecuteCommandParams_Data(ByVal Comando As String) As System.Data.DataTable Implements ISQL.ExecuteCommandParams_Data
        If sqlWebConection.State = ConnectionState.Closed Then
            sqlWebConection.Open()
        End If

        Dim adapter As SqlDataAdapter = New SqlDataAdapter()
        Using command = New SqlCommand(Comando, sqlWebConection)

            command.CommandTimeout = 90
            Dim param As SqlParameter
            Dim vkey As Tuple(Of String, SqlDbType, Object)

            For i As Integer = 0 To Params.Count - 1
                param = New SqlParameter()
                vkey = Params(i)
                param.ParameterName = String.Format("{0}{1}", "@", vkey.Item1.ToString)
                param.SqlDbType = vkey.Item2
                param.Value = vkey.Item3
                command.Parameters.Add(param)
            Next i

            adapter.SelectCommand = command
            If InTransaction = True Then
                adapter.SelectCommand.Transaction = IndigoTransaction
            End If
        End Using

        Dim ds As New DataSet
        adapter.Fill(ds, "Datos")

        ExecuteCommandParams_Data = ds.Tables("Datos")
        adapter = Nothing
        ds = Nothing
        Params.Clear()
        If InTransaction = False Then
            sqlWebConection.Close()
        End If
        Return ExecuteCommandParams_Data
    End Function

#End Region

#Region "Funcion o Metodos de Utilidades"

    ''' <summary>
    ''' funcion para concatenar cadenas.
    ''' </summary>
    ''' <param name="strCaracter">caracter a concatenar.</param>
    ''' <param name="strCadena">cadena a concatenar.</param>
    ''' <param name="intNumero">numero de caracteres.</param>
    ''' <param name="Dir">la Direccion.</param>
    ''' <param name="strPrefijo">el prefijo.</param>
    ''' <returns></returns>
    Public Function fncConcatenar(ByVal strCaracter As String, ByVal strCadena As String, ByVal intNumero As Integer, ByVal Dir As ISQL.Direccion, Optional ByVal strPrefijo As String = "") As String Implements ISQL.fncConcatenar
        Dim intCant As Integer
        intCant = strCadena.Length
        For i = intCant To intNumero - 1
            If Dir = ISQL.Direccion.Izquierda Then
                strCadena = strCaracter & strCadena
            Else
                strCadena = strCadena & strCaracter
            End If
        Next
        fncConcatenar = strPrefijo.Trim & strCadena
        Return fncConcatenar
    End Function

    ''' <summary>
    ''' Funcion para crear un parametro de sql
    ''' </summary>
    ''' <param name="key"></param>
    ''' <param name="type"></param>
    ''' <param name="value"></param>
    Public Sub AddParam(key As String, type As SqlDbType, value As Object) Implements ISQL.AddParam
        Params.Add(New Tuple(Of String, SqlDbType, Object)(key, type, value))
    End Sub


    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: eliminar el estado administrado (objetos administrados)
            End If

            ' TODO: liberar los recursos no administrados (objetos no administrados) y reemplazar el finalizador
            ' TODO: establecer los campos grandes como NULL
            disposedValue = True
        End If
    End Sub

    ' ' TODO: reemplazar el finalizador solo si "Dispose(disposing As Boolean)" tiene código para liberar los recursos no administrados
    ' Protected Overrides Sub Finalize()
    '     ' No cambie este código. Coloque el código de limpieza en el método "Dispose(disposing As Boolean)".
    '     Dispose(disposing:=False)
    '     MyBase.Finalize()
    ' End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        ' No cambie este código. Coloque el código de limpieza en el método "Dispose(disposing As Boolean)".
        Dispose(disposing:=True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
