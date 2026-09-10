Imports DevExpress.XtraEditors
Imports System.Data
Imports System.Data.SqlClient

Public Class frmConfiguracionSincronizador

    'Private Sub CheckEdit1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles INDchkMismaConfiguracion.CheckedChanged
    '    If INDchkMismaConfiguracion.Checked = True Then
    '        INDtxtContraseña2.Text = INDtxtContraseña1.Text
    '        INDtxtServidor2.Text = INDtxtServidor1.Text
    '        INDtxtUsuario2.Text = INDtxtUsuario1.Text
    '    End If
    'End Sub

    'Private Sub INDbtnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles INDbtnAceptar.Click

    '    If INDtxtContraseña2.Text = String.Empty Or INDtxtServidor2.Text = String.Empty Or INDtxtNombreDB2.Text = String.Empty Or INDtxtUsuario2.Text = String.Empty Or INDtxtContraseña1.Text = String.Empty Or INDtxtServidor1.Text = String.Empty Or INDtxtNombreDB1.Text = String.Empty Or INDtxtUsuario1.Text = String.Empty Then

    '        XtraMessageBox.Show("Debe Diligenciar todos los Campos Para la Correcta Configuracion", "Genesis.NET", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '        Exit Sub
    '    Else
    '        Dim ConfiguracionConexion As New DataTable

    '        ConfiguracionConexion.ReadXmlSchema("d:\dtConexion.xsd")
    '        ConfiguracionConexion.ReadXml("d:\dtConexion.xml")



    '        Dim DataFile1 As DataRow


    '        DataFile1 = ConfiguracionConexion.NewRow

    '        DataFile1(0) = "Data Source=" & "" & INDtxtServidor1.Text.Trim & "; User ID=" & INDtxtUsuario1.Text.Trim & ";Password=" & INDtxtContraseña1.Text.Trim
    '        DataFile1(1) = INDtxtNombreDB1.Text.Trim


    '        ConfiguracionConexion.Rows.Add(DataFile1)


    '        Dim DataFile2 As DataRow


    '        DataFile2 = ConfiguracionConexion.NewRow

    '        DataFile2(0) = "Data Source=" & "" & INDtxtServidor2.Text.Trim & "; User ID=" & INDtxtUsuario2.Text.Trim & ";Password=" & INDtxtContraseña2.Text.Trim
    '        DataFile2(1) = INDtxtNombreDB2.Text.Trim


    '        ConfiguracionConexion.Rows.Add(DataFile2)


    '        ConfiguracionConexion.Rows.RemoveAt(0)
    '        ConfiguracionConexion.Rows.RemoveAt(1)
    '        ConfiguracionConexion.Rows.RemoveAt(0)
    '        ConfiguracionConexion.Rows.RemoveAt(1)
    '        ConfiguracionConexion.Rows.RemoveAt(0)
    '        ConfiguracionConexion.Rows.RemoveAt(1)

    '        ConfiguracionConexion.WriteXmlSchema("d:\dtConexion.xsd")
    '        ConfiguracionConexion.WriteXml("d:\dtConexion.xml")

    '    End If

    'End Sub

    'Private Sub frmConfiguracionSincronizador_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    '    Dim INDConexion As String = "192.168.50.10"
    '    Dim INDUser As String = "ivan"
    '    Dim INDPaswoord As String = "sierra"

    '    Dim dtConexion As New DataTable
    '    dtConexion.TableName = "dtConexion"

    '    dtConexion.Columns.Add("Conexion")
    '    dtConexion.Columns.Add("DB")

    '    Dim dtRow As DataRow

    '    dtRow = dtConexion.NewRow

    '    dtRow(0) = "Data Source=" & INDConexion & ";User ID=" & INDUser & ";Password=" & INDPaswoord & ""
    '    dtRow(1) = "GENESIS"
    '    dtConexion.Rows.Add(dtRow)

    '    Dim dtRow1 As DataRow

    '    dtRow1 = dtConexion.NewRow

    '    dtRow1(0) = "Data Source=" & INDConexion & ";User ID=" & INDUser & ";Password=" & INDPaswoord & ""
    '    dtRow1(1) = "GENESISHISTO"
    '    dtConexion.Rows.Add(dtRow1)




    '    dtConexion.WriteXml("d:\dtConexion.xml")
    '    dtConexion.WriteXmlSchema("d:\dtConexion.xsd")


    '    'Dim A As New DataTable
    '    'A.ReadXmlSchema("d:\dtSincronizacion.xsd")
    '    'A.ReadXml("d:\dtSincronizacion.xml")


    '    'Dim data As DataRow

    '    'data = A.NewRow


    '    'data(0) = "2"
    '    'data(1) = "SEGPERUSU"
    '    'data(2) = "11/04/2011"


    '    'A.Rows.Add(data)

    '    'A.WriteXmlSchema("d:\dtSincronizacion.xsd")
    '    'A.WriteXml("d:\dtSincronizacion.xml")


    'End Sub



    Dim _sqlConexion As SqlConnection
    ''' <summary>
    ''' Propiedad que contiene la Conexion Web
    ''' </summary>
    ''' <value>Conexion Web</value>
    ''' <returns>Devuelve la Conexion Web</returns>
    ''' <remarks></remarks>
    Private Property sqlWebConexion() As SqlConnection
        Get
            Return _sqlConexion
        End Get
        Set(ByVal value As SqlConnection)
            _sqlConexion = value
        End Set
    End Property


    Function ConsultaNumeroRegistros(ByVal TablaDB As String) As String

        sqlWebConexion = New SqlConnection("Data Source=" & INDtxtServidor1.Text & ";Initial Catalog=GENESIS;User ID=" & INDtxtUsuario1.Text & ";Password=" & INDtxtContraseña1.Text & "  ")
        Dim dtNumeroRegistros As DataTable
        Dim SQL As String
        SQL = "SELECT COUNT(*) FROM " & TablaDB & ""
        Dim da As SqlDataAdapter = New SqlDataAdapter(SQL, sqlWebConexion)
        'establezco tiempos
        da.SelectCommand.CommandTimeout = 90
        Dim ds As New DataSet
        da.Fill(ds, "NumeroRegistros")
        dtNumeroRegistros = ds.Tables("NumeroRegistros")
        da = Nothing
        ds = Nothing
        sqlWebConexion.Close()

        Return dtNumeroRegistros.Rows(0).Item(0).ToString
    End Function


    Private Sub INDtxtNombreDB1_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles INDtxtNombreDB1.KeyDown
        If e.KeyCode = Keys.Enter Then
            sqlWebConexion = New SqlConnection("Data Source=" & INDtxtServidor1.Text & ";Initial Catalog=GENESIS;User ID=" & INDtxtUsuario1.Text & ";Password=" & INDtxtContraseña1.Text & "  ")


            Dim dtTablasDB As DataTable
            Dim SQL As String
            SQL = "SELECT name FROM sysobjects WHERE xtype='u'"
            Dim da As SqlDataAdapter = New SqlDataAdapter(SQL, sqlWebConexion)
            'establezco tiempos
            da.SelectCommand.CommandTimeout = 90
            Dim ds As New DataSet
            da.Fill(ds, "RegistrosInsertados")
            dtTablasDB = ds.Tables("RegistrosInsertados")
            da = Nothing
            ds = Nothing


            Dim dtTablasCount As New DataTable

            dtTablasCount.Columns.Add("NombreTabla")
            dtTablasCount.Columns.Add("NumeroDeRegistros")


            For i As Integer = 0 To dtTablasDB.Rows.Count - 1
                Dim Fila As DataRow
                Fila = dtTablasCount.NewRow
                Fila(0) = dtTablasDB.Rows(i).Item(0)
                Fila(1) = ""
                dtTablasCount.Rows.Add(Fila)
            Next


            For i As Integer = 0 To dtTablasCount.Rows.Count - 1
                dtTablasCount.Rows(i).Item(1) = ConsultaNumeroRegistros(dtTablasCount.Rows(i).Item(0).ToString)
            Next


            INDTablasDBGrid.Properties.DataSource = dtTablasCount
            sqlWebConexion.Close()
        End If
    End Sub
End Class