Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Interface

Public Class InterfaceValidate
    Implements IInterfaceValidate

    Dim con As ConectionSQL

    Public Sub New(company As String)
        con = New ConectionSQL(company)
    End Sub

    Public Function ValidateAccount(enumeration As eTypeInterface, ContainerName As String, account As String) As Integer Implements IInterfaceValidate.ValidateAccount
        Dim strSql As String = String.Empty
        If enumeration = eTypeInterface.FoxPrivate Or enumeration = eTypeInterface.FoxPublic Then
            strSql = "SELECT count(*) FROM " & ContainerName & "..ctPlaCue WHERE cpcCodCue='" & account & "' and  cpctipcue = '5'"
        ElseIf enumeration = eTypeInterface.NETPrivate Or enumeration = eTypeInterface.NETPublic Then
            strSql = "SELECT  count(*) FROM " & ContainerName & "..CTNCUENTA WHERE CUECODIGO='" & account & "' and CTNNIVEL = '5'  "
        End If
        Dim intCount As Integer = con.ExecuteCommand_Count(strSql)
        Return intCount
    End Function

    Public Function ValidateConcept(enumeration As eTypeInterface, ContainerName As String, CodeConcept As String, typeconcept As String) As Integer Implements IInterfaceValidate.ValidateConcept
        Dim strSql As String = String.Empty
        If enumeration = eTypeInterface.FoxPrivate Or enumeration = eTypeInterface.FoxPublic Then
            strSql = "SELECT  count(*)  FROM " & ContainerName & "..crConcep WHERE cnoCodCon= '" & CodeConcept & "'  /*and cnomanter=2 and cnoafecar=2 and cnoconglo=1 and cnoafeban=1  and cnotipnot='" & typeconcept & "'*/"
        ElseIf enumeration = eTypeInterface.NETPrivate Or enumeration = eTypeInterface.NETPublic Then
            strSql = "SELECT count(*)  FROM " & ContainerName & "..crNConNOT WHERE CONCODIGO= '" & CodeConcept & "' /*and CONAFECTA=1*/"
        End If
        Dim intCount As Integer = con.ExecuteCommand_Count(strSql)
        Return intCount
    End Function

    Public Function ValidateMonthClose(enumeration As eTypeInterface, DateActual As Date, ContainerName As String) As Boolean Implements IInterfaceValidate.ValidateMonthClose
        Dim strSql As String = String.Empty
        Dim anio As Integer = Year(Date.Now)
        Dim mes As Integer = Month(Date.Now)
        Dim result As Boolean
        If enumeration = eTypeInterface.FoxPrivate Or enumeration = eTypeInterface.FoxPublic Then
            strSql = "SELECT CPSFECINI, rtrim(cpscierre) as cpscierre FROM " & ContainerName & "..ctparsis"
            Dim dtMes As DataTable = con.ExecuteCommand_Data(strSql)
            Dim mesCerrado As Integer
            Dim anioIni As Integer
            Dim mesIni As Integer
            Dim difAnio As Integer
            If dtMes.Rows.Count > 0 Then
                If dtMes.Rows(0).Item("CPSFECINI").ToString <> String.Empty Then
                    mesIni = Month(dtMes.Rows(0).Item("CPSFECINI"))
                    anioIni = Year(dtMes.Rows(0).Item("CPSFECINI"))
                End If
                If dtMes.Rows(0).Item("cpscierre").ToString <> String.Empty Then
                    mesCerrado = Len(dtMes.Rows(0).Item("cpscierre"))
                End If
            End If
            If mesCerrado <= 13 And mesCerrado <= mes Then
                result = True
            ElseIf mesCerrado > 13 Then
                mesCerrado = mesCerrado - 12
                If mesCerrado <= mes Then
                    result = True
                Else
                    result = False
                End If
            Else
                difAnio = anio - anioIni
                If difAnio > 1 Then
                    result = True
                Else
                    result = False
                End If
            End If
            Return result
        ElseIf enumeration = eTypeInterface.NETPrivate Or enumeration = eTypeInterface.NETPublic Then
            strSql = "SELECT CTCMESTADO  from " & ContainerName & "..CTNCIEMEN WHERE CTCMAÑO = " & anio & " AND CTCMMES = " & mes
            Dim dtMes As DataTable = con.ExecuteCommand_Data(strSql)
            If dtMes.Rows.Count > 0 Then
                If dtMes.Rows(0).Item("CTCMESTADO") = 0 Then
                    result = True
                Else
                    result = False
                End If
            End If
        End If
        Return result
    End Function

    Public Function ValidateTableMov(DateActual As Date, ContainerName As String) As Boolean Implements IInterfaceValidate.ValidateTableMov
        Dim strSql As String = String.Empty
        Dim result As Boolean
        Dim MesConcatenado As String = Month(DateActual).ToString
        MesConcatenado = con.fncConcatenar("0", MesConcatenado, 2, ISQL.Direccion.Izquierda)
        strSql = "SELECT count (*) FROM " & ContainerName & "..sysobjects A WHERE name = 'MM" & Year(DateActual) & MesConcatenado & "'"
        Dim intCount As Integer = con.ExecuteCommand_Count(strSql)
        If intCount = 0 Then
            result = False
        Else
            result = True
        End If
        Return result
    End Function

    ''' <summary>
    ''' Funcion para crear una auditoria basica de las interface
    ''' </summary>
    ''' <param name="IndigoCompany"></param>
    ''' <param name="intOpcion"></param>
    ''' <param name="NumeroGlosa"></param>
    ''' <param name="factura"></param>
    ''' <param name="accion"></param>
    ''' <param name="Concepto"></param>
    ''' <param name="NumeroConsecutivo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function AuditInterface(ByVal IndigoCompany As String, ByVal intOpcion As String, ByVal NumeroGlosa As String, ByVal factura As String, ByVal accion As String, ByVal Concepto As String, ByVal NumeroConsecutivo As String) As Boolean Implements IInterfaceValidate.AuditInterface
        Dim Result As New Boolean
        Dim strSql As String = String.Empty
        Try

            If con.sqlWebConection.State = ConnectionState.Closed Then
                con.sqlWebConection.Open()
            End If

            strSql = "INSERT INTO [" & IndigoCompany & "].[Glosas].[AuditInterface] ([OptionRegister],[NumberGlosa],[InvoiceNumber],[ActionCode],[Concept],[NewConsecutive]) " & _
            "VALUES('" & intOpcion & "'," & NumeroGlosa & ",'" & factura & "','" & accion & "','" & Concepto & "'," & NumeroConsecutivo & ")"
            con.ExecuteCommand(strSql)

            Result = True
        Catch ex As Exception
            con.IndigoTransaction.Rollback()
            Result = False
        Finally
            con.sqlWebConection.Close()
        End Try
        Return Result
    End Function

End Class
