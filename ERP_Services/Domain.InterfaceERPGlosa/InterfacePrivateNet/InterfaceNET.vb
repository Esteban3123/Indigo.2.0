'***********************************************************************
' Assembly         : Domain.InterfaceERPGlosa
' Author           : RafaelPatiño
' Created          : 13-04-2014
'
' Last Modified By : 
' Last Modified On : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Interface
Imports System.Data.SqlClient
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Exceptions

Public Class InterfaceNET
    Implements IInterfaceNET

#Region "Variables"
    Dim con As ConectionSQL
    Dim Validation As IInterfaceValidate
#End Region

#Region "Contructor"
    Public Sub New(company As String)
        con = New ConectionSQL(company)
        Validation = New InterfaceValidate(company)
    End Sub
#End Region

#Region "Proceso de Radicacion de Objeciones"

    ''' <summary>
    ''' Funcion de interfaz en el proceso de radicar una objecion
    ''' </summary>
    ''' <param name="IndigoEmpresa"></param>
    ''' <param name="NumeroGlosa"></param>
    ''' <param name="factura"></param>
    ''' <param name="Tercero"></param>
    ''' <param name="NameContainer"></param>
    ''' <param name="ValorFac"></param>
    ''' <param name="FechaFactura"></param>
    ''' <param name="intOpcion"></param>
    ''' <param name="User"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function RadicateObjection(IndigoEmpresa As String, NumeroGlosa As String, factura As String, Tercero As String, NameContainer As String, ValorFac As Decimal, FechaFactura As Integer, intOpcion As String, User As String) As InterfaceResult Implements IInterfaceNET.RadicateObjection
        Dim Result As New InterfaceResult
        If IndigoEmpresa = String.Empty Then
            Throw New ArgumentNullException("codigo empresa indigo vacio")
        End If
        If NumeroGlosa = String.Empty Then
            Throw New ArgumentNullException("Numero Glosa vacio")
        End If
        If factura = String.Empty Then
            Throw New ArgumentNullException("factura vacio")
        End If
        If Tercero = String.Empty Then
            Throw New ArgumentNullException("Tercero vacio")
        End If
        If NameContainer = String.Empty Then
            Throw New ArgumentNullException("Empresa ERP vacio")
        End If
        If ValorFac = 0 Then
            Throw New ArgumentNullException("Valor Fac 0")
        End If
        If intOpcion = String.Empty Then
            Throw New ArgumentNullException("intOpcion vacio")
        End If
        If User = String.Empty Then
            Throw New ArgumentNullException("User vacio")
        End If

        Dim strSql As String = String.Empty
        Try

            'cargamos configuraciones de interface
            If con.sqlWebConection.State = ConnectionState.Closed Then
                con.sqlWebConection.Open()
            End If

            strSql = "SELECT top 1 id, CompanyName FROM " & IndigoEmpresa & ".Glosas.GlosasParametersInterface WHERE ContainerName= '" & NameContainer & "'  "
            Dim dtConfi As DataTable = con.ExecuteCommand_Data(strSql)
            Dim NombreEmpresa As String
            If dtConfi.Rows.Count = 0 Then
                Result = New InterfaceResult With {.Message = "No Existe configuracion de interface para el contenedor" & NameContainer, .Result = False}
                Return Result
            Else
                NombreEmpresa = dtConfi.Rows(0).Item("CompanyName").ToString
            End If

            'cargamos cuenta apartir de tercero y factura, Validamos Existencia de Cuenta

            ' Tercero = con.fncConcatenar("0", Tercero, 15, ISQL.Direccion.Izquierda)

            Dim cuentaCartera As String
            strSql = "SELECT top 1  C.CUECODIGO FROM " & NameContainer & "..CRNCXC F INNER JOIN " & NameContainer & "..GENTERCER T on F.GENTERCER = T.OID " & _
                             "INNER Join " & NameContainer & "..CTNCUENTA C on F.CTNCUENTA = C.OID WHERE  F.CXCDOCUME ='" & factura & "' AND T.TERNUMDOC = '" & Tercero & "'"
            Dim dtCuenta As DataTable = con.ExecuteCommand_Data(strSql)
            If dtCuenta.Rows.Count = 0 Then
                Result = New InterfaceResult With {.Message = "No existe la factura en ERP, favor avisar al administrador del sistema", .Result = False}
                Return Result
            Else
                If dtCuenta.Rows(0).Item("CUECODIGO").ToString <> String.Empty Then
                    cuentaCartera = dtCuenta.Rows(0).Item("CUECODIGO").ToString
                    Dim res As Integer = Validation.ValidateAccount(eTypeInterface.NETPrivate, NameContainer, cuentaCartera)
                    If res = 0 Then
                        Result = New InterfaceResult With {.Message = "No existe la cuenta " & cuentaCartera & " de la factura en DGH, favor avisar al administrador del sistema", .Result = False}
                        Return Result
                    End If
                Else
                    Result = New InterfaceResult With {.Message = "No existe la factura en ERP, favor avisar al administrador del sistema", .Result = False}
                    Return Result
                End If
            End If


            ' validar el saldo y el plan
            strSql = "SELECT   count(*) FROM " & NameContainer & "..CRNCXC A INNER JOIN  " & NameContainer & "..CRNCXCC B " & _
                    "on A.OID=B.CRNCXC INNER JOIN  " & NameContainer & "..GENTERCER C on A.GENTERCER=C.OID  " & _
                    "WHERE  A.CXCDOCUME='" & factura & "' AND C.TERNUMDOC='" & Tercero & "'  " & _
                    "AND (B.CCVALOR+B.CCVALDEB-B.CCVALCRE-B.CCVALABO-B.CCVALTRA) > 0"
            Dim resint As Integer = con.ExecuteCommand_Count(strSql)
            If resint = 0 Then
                Result = New InterfaceResult With {.Message = "  La factura " & factura & " no  tiene saldo, no se puede continuar!!", .Result = False}
                Return Result
            End If


            'consultar cuenta de radicacion segun tabla de configuracion
            strSql = "select InvoiceRadicate,RectifiableGlosa from  " & IndigoEmpresa & ".Glosas.AccountSettingsNET_PrivateMethod WHERE InvoiceRadicate = '" & cuentaCartera & "' "
            Dim dtConcept As DataTable = con.ExecuteCommand_Data(strSql)
            Dim concepto As String
            If dtConcept.Rows.Count = 0 Then
                Result = New InterfaceResult With {.Message = " No existe configuraciones contables glosas para metodo privado (Net) en el campo (Factura Radicada) - cuenta: " & cuentaCartera & ", no se puede continuar!!", .Result = False}
                Return Result
            Else
                concepto = dtConcept.Rows(0).Item("RectifiableGlosa").ToString  'concepto de radicacion nota credito
            End If
            'consultamos el codigo de cuenta para el concepto de "RectifiableGlosa"
            strSql = "SELECT cue.CUECODIGO  FROM " & NameContainer & "..crNConNOT concep INNER JOIN " & NameContainer & "..CTNCUENTA Cue on Concep.CTNCUENTA = cue.OID  WHERE concep.CONCODIGO= '" & concepto & "'"
            Dim dtCuentaConcepto As DataTable = con.ExecuteCommand_Data(strSql)
            Dim CuentaConcepto As String
            If dtCuentaConcepto.Rows.Count = 0 Then
                Result = New InterfaceResult With {.Message = " no existe cuenta para el concepto " & concepto & " , no se puede continuar!!", .Result = False}
                Return Result
            Else
                CuentaConcepto = dtCuentaConcepto.Rows(0).Item("CUECODIGO").ToString
            End If


            'validamos la cuenta maneje ce y  nivel 5
            strSql = "SELECT count(*) FROM " & NameContainer & "..CTNCUENTA WHERE CUECODIGO='" & CuentaConcepto & "' and /*CUEMANCEN=1 and*/ CTNNIVEL = '5'"
            resint = con.ExecuteCommand_Count(strSql)
            If resint = 0 Then
                Result = New InterfaceResult With {.Message = "La cuenta no existe, o no es de nivel 5'!!", .Result = False}
                Return Result
            End If


            Dim resultCreateCreditNOte As InterfaceResult = Me.CreateCreditNote(NameContainer, Tercero, factura, ValorFac, User, cuentaCartera, concepto, CuentaConcepto)
            If resultCreateCreditNOte.Result = False Then
                Return resultCreateCreditNOte
            End If


            Validation.AuditInterface(IndigoEmpresa, intOpcion, NumeroGlosa, factura, 2, concepto, resultCreateCreditNOte.Consecutive)

            Result = New InterfaceResult With {.Message = "Se Generaron los siguientes documentos: Nota Credito: " & resultCreateCreditNOte.Consecutive & ", Empresa: " & NombreEmpresa, .Result = True, .Consecutive = resultCreateCreditNOte.Consecutive, .Account = CuentaConcepto}
        Catch ex As Exception
            '  con.IndigoTransaction.Rollback()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Result = New InterfaceResult With {.Message = ex.Message, .Result = False}
        Finally
            con.sqlWebConection.Close()
        End Try
        Return Result
    End Function

    ''' <summary>
    ''' Funcion para crear NOTA CREDITO
    ''' </summary>
    ''' <param name="NameContainer"></param>
    ''' <param name="Tercero"></param>
    ''' <param name="factura"></param>
    ''' <param name="ValorFac"></param>
    ''' <param name="User"></param>
    ''' <param name="Cuenta"></param>
    ''' <param name="Concepto"></param>
    ''' <param name="CuentaConcepto"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CreateCreditNote(NameContainer As String, Tercero As String, factura As String, ValorFac As Decimal, User As String, Cuenta As String, Concepto As String, CuentaConcepto As String) As InterfaceResult Implements IInterfaceNET.CreateCreditNote
        Dim Result As New InterfaceResult
        Dim strSql As String = String.Empty
        Try

            If con.sqlWebConection.State = ConnectionState.Closed Then
                con.sqlWebConection.Open()
            End If

            con.InTransaction = True
            con.IndigoTransaction = con.sqlWebConection.BeginTransaction(IsolationLevel.ReadCommitted, "Interfax crear Nota Credito")


            Dim ParametroConsecGlosa As String
            strSql = "SELECT GENCONSECNOTCRE  FROM " & NameContainer & "..CRNPARSIS"
            Dim dtConCartera As DataTable = con.ExecuteCommand_Data(strSql)
            ParametroConsecGlosa = dtConCartera.Rows(0).Item("GENCONSECNOTCRE").ToString
            dtConCartera = Nothing

            'Dim parametroConsecTipDoc As String
            'strSql = "SELECT GENCONSEC FROM " & NameContainer & "..CTNTIPCOM WHERE OID = " & ParametroConsecGlosa
            'Dim dtConTipDoc As DataTable = con.ExecuteCommand_Data(strSql)
            'If dtConTipDoc.Rows.Count = 0 Then
            '    Result = New InterfaceResult With {.Message = " no existe Id de consecutivo en la tabla CTNTIPCOM, no se puede continuar!!", .Result = False}
            '    Return Result
            'Else
            '    parametroConsecTipDoc = dtConTipDoc.Rows(0).Item("GENCONSEC").ToString
            'End If
            'dtConTipDoc = Nothing


            'obtener numero consecutivo
            Dim ResultEje As Boolean
            Dim ConsecutiveNumber As Integer
            strSql = "UPDATE " & NameContainer & "..geNconsec SET gconumero=gconumero+1,gcoestado='1' WHERE OID = " & ParametroConsecGlosa
            ResultEje = con.ExecuteCommand(strSql)

            strSql = "SELECT  gconumero FROM " & NameContainer & "..geNconsec WHERE OID = " & ParametroConsecGlosa
            Dim dtNumConsecutive As DataTable = con.ExecuteCommand_Data(strSql)
            If dtNumConsecutive.Rows.Count = 0 Then
                Result = New InterfaceResult With {.Message = " no existe Id numero consecutivo en la tabla geNconsec, no se puede continuar!!", .Result = False}
                Return Result
            Else
                ConsecutiveNumber = CInt(dtNumConsecutive.Rows(0).Item("gconumero"))
            End If
            dtNumConsecutive = Nothing

            ConsecutiveNumber = con.fncConcatenar("0", ConsecutiveNumber.ToString, 10, ISQL.Direccion.Izquierda)
            Dim DateActual As Date = Date.Now
            Dim fecha As String
            fecha = Format(DateActual, "yyyyMMdd hh:mm:ss")
            'Dim TerceroNumeros As Integer = Val(Tercero)

            'consultamos el OID del tercero
            Dim OIDtercero As Integer
            strSql = "SELECT  OID FROM " & NameContainer & "..GENTERCER WHERE TERNUMDOC = '" & Tercero & "'"
            Dim dtTercero As DataTable = con.ExecuteCommand_Data(strSql)
            If dtTercero.Rows.Count = 0 Then
                Result = New InterfaceResult With {.Message = " No existe tercero con codigo " & Tercero & ", no se puede continuar!!", .Result = False}
                Return Result
            Else
                OIDtercero = CInt(dtTercero.Rows(0).Item("OID"))
            End If
            dtTercero = Nothing

            'con el codigo de usaurio traemos el OID del usuario
            Dim OIDusuario As Integer
            strSql = "SELECT top 1 OID  FROM " & NameContainer & "..GENUSUARIO  WHERE USUNOMBRE = '" & User & "'"
            Dim dtUsuario As DataTable = con.ExecuteCommand_Data(strSql)
            If dtUsuario.Rows.Count = 0 Then
                Result = New InterfaceResult With {.Message = " no existe Usuario con el codigo " & User & ", no se puede continuar!!", .Result = False}
                Return Result
            Else
                OIDusuario = CInt(dtUsuario.Rows(0).Item("OID"))
            End If
            dtUsuario = Nothing



            'consulto el ID del cliente 
            Dim OIDCliente As Integer
            strSql = "SELECT OID FROM " & NameContainer & "..GENTERCERC WHERE clicodigo = '" & Tercero & "'"
            Dim dtTerceroCliente As DataTable = con.ExecuteCommand_Data(strSql)
            If dtTerceroCliente.Rows.Count = 0 Then
                Result = New InterfaceResult With {.Message = " No existe tercero con codigo " & Tercero & ", no se puede continuar!!", .Result = False}
                Return Result
            Else
                OIDCliente = CInt(dtTerceroCliente.Rows(0).Item("OID"))
            End If
            dtTerceroCliente = Nothing

            ''cuenta de crcarter, cabecera
            strSql = "INSERT INTO " & NameContainer & "..CRNNOTA(NOTCONSEC, NOTFECHA, NOTESTADO, GENTERCER, GENTERCERC, NOTOBSERVA, NOTNATURA, NOTINTTIP, NOTINTCON," & _
            "NOTAPLIFAA, NOTCONSFOX, GENUSUARIO2, NOTFECCRE, GENUSUARIO3, NOTFECCON, NOTPARINV,OptimisticLockField , NOTREGDESCAR, NOTINTPRE)VALUES( " & _
            "'" & ConsecutiveNumber & "', " & _
            "convert(varchar(20),'" & (fecha) & "'),0," & OIDtercero & "," & OIDCliente & ",'Radicacion Glosa Subsanable - Fra. " & factura & " Mod. Glosas',2,-1, " & _
            "'" & factura & "',0,NULL," & OIDusuario & ", convert(varchar(20),'" & (fecha) & "'),null,null,0,0,NULL,NULL)"
            ResultEje = con.ExecuteCommand(strSql)


            Dim Auto As String
            Dim SQL As New SqlDataAdapter("SELECT @@IDENTITY", con.sqlWebConection)
            With SQL
                .SelectCommand.CommandTimeout = 90
                .SelectCommand.Transaction = con.IndigoTransaction
                .SelectCommand.CommandType = CommandType.Text
                Auto = .SelectCommand.ExecuteScalar.ToString
            End With

            'concepto 
            Dim OIDconcepto As Integer
            strSql = "SELECT  OID FROM " & NameContainer & "..CRNCONNOT  WHERE CONCODIGO = '" & Concepto & "'"
            Dim dtConcepto As DataTable = con.ExecuteCommand_Data(strSql)
            If dtConcepto.Rows.Count = 0 Then
                Result = New InterfaceResult With {.Message = " no existe Id del concepto: " & Concepto & ", no se puede continuar!!", .Result = False}
                Return Result
            Else
                OIDconcepto = CInt(dtConcepto.Rows(0).Item("OID"))
            End If
            dtConcepto = Nothing

            'cuenta
            Dim OIDcuenta As Integer
            strSql = "SELECT OID FROM " & NameContainer & "..CTNCUENTA  WHERE CUECODIGO = '" & CuentaConcepto & "'"
            Dim dtCuenta As DataTable = con.ExecuteCommand_Data(strSql)
            If dtCuenta.Rows.Count = 0 Then
                Result = New InterfaceResult With {.Message = " no existe Id de la cuenta: " & CuentaConcepto & ", no se puede continuar!!", .Result = False}
                Return Result
            Else
                OIDcuenta = CInt(dtCuenta.Rows(0).Item("OID"))
            End If
            dtCuenta = Nothing

            'detalle 
            strSql = "INSERT INTO  " & NameContainer & "..CRNNOTADETALLE(CRNNOTA, CRNCONNOT, CTNCUENTA, GENTERCER, CTNCENCOS, CCNNATURA, CCNVALOR, CTNCOMCONR, OptimisticLockField,CCNSOBCAM)VALUES(" & _
            "" & Auto & "," & OIDconcepto & "," & OIDcuenta & "," & OIDtercero & ",NULL,1,convert(varchar(50)," & ValorFac & "),NULL,0,NULL)"
            ResultEje = con.ExecuteCommand(strSql)


            'consult id de la factura
            Dim OIDFactura As Integer
            strSql = "SELECT OID FROM " & NameContainer & "..CRNCXC  WHERE CXCDOCUME = '" & factura & "'"
            Dim dtFactura As DataTable = con.ExecuteCommand_Data(strSql)
            If dtFactura.Rows.Count = 0 Then
                Result = New InterfaceResult With {.Message = " no existe factura: " & factura & ", no se puede continuar!!", .Result = False}
                Return Result
            Else
                OIDFactura = CInt(dtFactura.Rows(0).Item("OID"))
            End If
            dtFactura = Nothing


            'credito
            strSql = "INSERT INTO " & NameContainer & "..CRNNOTAFAC(CRNNOTA, CRNCXC,OptimisticLockField, PSNRECONOC, PSNRECONOMC)VALUES( " & _
            "" & Auto & "," & OIDFactura & ",0,NULL,NULL)"
            ResultEje = con.ExecuteCommand(strSql)

            Dim AutoFactura As String
            SQL = New SqlDataAdapter("SELECT @@IDENTITY", con.sqlWebConection)
            With SQL
                .SelectCommand.CommandTimeout = 90
                .SelectCommand.Transaction = con.IndigoTransaction
                .SelectCommand.CommandType = CommandType.Text
                AutoFactura = .SelectCommand.ExecuteScalar.ToString
            End With


            'consult id del detalle de movimineto de la factura
            strSql = "SELECT  OID FROM " & NameContainer & "..CRNCXCC  WHERE CRNCXC = '" & OIDFactura & "'"
            Dim dtFacturaDetalle As DataTable = con.ExecuteCommand_Data(strSql)

            'detalle de mov factura
            For Each item As DataRow In dtFacturaDetalle.Rows
                strSql = "INSERT INTO " & NameContainer & "..CRNNOTAFACC(CRNNOTAFAC, CRNCXCC,NOTVALOR,OptimisticLockField )VALUES(" & _
                    "" & AutoFactura & "," & item.Item("OID").ToString & ",convert(varchar(50)," & ValorFac & "),0)"
                con.ExecuteCommand(strSql)
            Next


            con.IndigoTransaction.Commit()


            Result = New InterfaceResult With {.Message = "OK", .Result = True, .Consecutive = ConsecutiveNumber}
        Catch ex As Exception
            con.IndigoTransaction.Rollback()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Result = New InterfaceResult With {.Message = ex.Message, .Result = False}
        Finally
            con.sqlWebConection.Close()
        End Try
        Return Result
    End Function

#End Region

#Region "Proceso de Aceptacion EAPB"

    Public Function AcceptanceEAPB(IndigoEmpresa As String, NumeroGlosa As String, factura As String, Tercero As String, NameContainer As String, ValorFac As Decimal, FechaFactura As Integer, intOpcion As String, User As String, Optional ByVal EjecutaAceptacionEAPBUnicaTransaccion As Boolean = False) As InterfaceResult Implements IInterfaceNET.AcceptanceEAPB
        Dim Result As New InterfaceResult
        If IndigoEmpresa = String.Empty Then
            Throw New ArgumentNullException("codigo empresa indigo vacio")
        End If
        If NumeroGlosa = String.Empty Then
            Throw New ArgumentNullException("Numero Glosa vacio")
        End If
        If factura = String.Empty Then
            Throw New ArgumentNullException("factura vacio")
        End If
        If Tercero = String.Empty Then
            Throw New ArgumentNullException("Tercero vacio")
        End If
        If NameContainer = String.Empty Then
            Throw New ArgumentNullException("Empresa ERP vacio")
        End If
        If ValorFac = 0 Then
            Throw New ArgumentNullException("Valor Fac 0")
        End If
        If intOpcion = String.Empty Then
            Throw New ArgumentNullException("intOpcion vacio")
        End If
        If User = String.Empty Then
            Throw New ArgumentNullException("User vacio")
        End If

        Dim strSql As String = String.Empty
        Try

            'cargamos configuraciones de interface
            If con.sqlWebConection.State = ConnectionState.Closed Then
                con.sqlWebConection.Open()
            End If

            strSql = "SELECT top 1 id, CompanyName FROM " & IndigoEmpresa & ".Glosas.GlosasParametersInterface WHERE ContainerName= '" & NameContainer & "'  "
            Dim dtConfi As DataTable = con.ExecuteCommand_Data(strSql)
            Dim NombreEmpresa As String
            If dtConfi.Rows.Count = 0 Then
                Result = New InterfaceResult With {.Message = "No Existe configuracion de interface para el contenedor" & NameContainer, .Result = False}
                Return Result
            Else
                NombreEmpresa = dtConfi.Rows(0).Item("CompanyName").ToString
            End If

            'cargamos cuenta apartir de tercero y factura, Validamos Existencia de Cuenta

            ' Tercero = con.fncConcatenar("0", Tercero, 15, ISQL.Direccion.Izquierda)

            Dim cuentaCartera As String
            strSql = "SELECT top 1  C.CUECODIGO FROM " & NameContainer & "..CRNCXC F INNER JOIN " & NameContainer & "..GENTERCER T on F.GENTERCER = T.OID " & _
                             "INNER Join " & NameContainer & "..CTNCUENTA C on F.CTNCUENTA = C.OID WHERE  F.CXCDOCUME ='" & factura & "' AND T.TERNUMDOC = '" & Tercero & "'"
            Dim dtCuenta As DataTable = con.ExecuteCommand_Data(strSql)
            If dtCuenta.Rows.Count = 0 Then
                Result = New InterfaceResult With {.Message = "No existe la factura en ERP, favor avisar al administrador del sistema", .Result = False}
                Return Result
            Else
                If dtCuenta.Rows(0).Item("CUECODIGO").ToString <> String.Empty Then
                    cuentaCartera = dtCuenta.Rows(0).Item("CUECODIGO").ToString
                    Dim res As Integer = Validation.ValidateAccount(eTypeInterface.NETPrivate, NameContainer, cuentaCartera)
                    If res = 0 Then
                        Result = New InterfaceResult With {.Message = "No existe la cuenta " & cuentaCartera & " de la factura en DGH, favor avisar al administrador del sistema", .Result = False}
                        Return Result
                    End If
                Else
                    Result = New InterfaceResult With {.Message = "No existe la factura en ERP, favor avisar al administrador del sistema", .Result = False}
                    Return Result
                End If
            End If


            ' validar el saldo y el plan
            'strSql = "SELECT   count(*) FROM " & NameContainer & "..CRNCXC A INNER JOIN  " & NameContainer & "..CRNCXCC B " & _
            '        "on A.OID=B.CRNCXC INNER JOIN  " & NameContainer & "..GENTERCER C on A.GENTERCER=C.OID  " & _
            '        "WHERE  A.CXCDOCUME='" & factura & "' AND C.TERNUMDOC='" & Tercero & "'  " & _
            '        "AND (B.CCVALOR+B.CCVALDEB-B.CCVALCRE-B.CCVALABO-B.CCVALTRA) > 0"
            'Dim resint As Integer = con.ExecuteCommand_Count(strSql)
            'If resint = 0 Then
            '    Result = New InterfaceResult With {.Message = "  La factura " & factura & " no  tiene saldo, no se puede continuar!!", .Result = False}
            '    Return Result
            'End If



            strSql = "select InvoiceRadicate,RectifiableGlosa,LegalProcess,Conciliation from  " & IndigoEmpresa & ".Glosas.AccountSettingsNET_PrivateMethod WHERE InvoiceRadicate = '" & cuentaCartera & "' "
            Dim dtConcept As DataTable = con.ExecuteCommand_Data(strSql)
            Dim concepto As String
            If dtConcept.Rows.Count = 0 Then
                Result = New InterfaceResult With {.Message = " No existe configuraciones contables glosas para metodo privado (Net) en el campo (Factura Radicada) - cuenta: " & cuentaCartera & ", no se puede continuar!!", .Result = False}
                Return Result
            Else
                concepto = dtConcept.Rows(0).Item("Conciliation").ToString  'concepto de Conciliation nota debito aceptacion EAPB
            End If
            'consultamos el codigo de cuenta para el concepto de "Conciliation"
            strSql = "SELECT cue.CUECODIGO  FROM " & NameContainer & "..crNConNOT concep INNER JOIN " & NameContainer & "..CTNCUENTA Cue on Concep.CTNCUENTA = cue.OID  WHERE concep.CONCODIGO= '" & concepto & "'"
            Dim dtCuentaConcepto As DataTable = con.ExecuteCommand_Data(strSql)
            Dim CuentaConcepto As String
            If dtCuentaConcepto.Rows.Count = 0 Then
                Result = New InterfaceResult With {.Message = " no existe cuenta para el concepto " & concepto & " , no se puede continuar!!", .Result = False}
                Return Result
            Else
                CuentaConcepto = dtCuentaConcepto.Rows(0).Item("CUECODIGO").ToString
            End If


            'validamos la cuenta maneje ce y  nivel 5
            Dim resint As Integer
            strSql = "SELECT count(*) FROM " & NameContainer & "..CTNCUENTA WHERE CUECODIGO='" & CuentaConcepto & "' and /*CUEMANCEN=1 and*/ CTNNIVEL = '5'"
            resint = con.ExecuteCommand_Count(strSql)
            If resint = 0 Then
                Result = New InterfaceResult With {.Message = "La cuenta no existe, o no es de nivel 5'!!", .Result = False}
                Return Result
            End If


            Dim resultCreateDebitNOte As InterfaceResult = Me.CreateDebitNote(NameContainer, Tercero, factura, ValorFac, User, cuentaCartera, concepto, CuentaConcepto, False, EjecutaAceptacionEAPBUnicaTransaccion)
            If resultCreateDebitNOte.Result = False Then
                Return resultCreateDebitNOte
            End If

            Validation.AuditInterface(IndigoEmpresa, intOpcion, NumeroGlosa, factura, 3, concepto, resultCreateDebitNOte.Consecutive)

            Result = New InterfaceResult With {.Message = "Se Generaron los siguientes documentos: Nota Debito: " & resultCreateDebitNOte.Consecutive & ", Empresa: " & NombreEmpresa, .Result = True, .Consecutive = resultCreateDebitNOte.Consecutive}
        Catch ex As Exception
            'con.IndigoTransaction.Rollback()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Result = New InterfaceResult With {.Message = ex.Message, .Result = False}
        Finally
            If EjecutaAceptacionEAPBUnicaTransaccion = False Then
                con.sqlWebConection.Close()
            End If
        End Try
        Return Result
    End Function

    Public Function CreateDebitNote(NameContainer As String, Tercero As String, factura As String, ValorFac As Decimal, User As String, Cuenta As String, Concepto As String, CuentaConcepto As String, ByVal Reiterated As Boolean, Optional ByVal EjecutaAceptacionEAPBUnicaTransaccion As Boolean = False) As InterfaceResult Implements IInterfaceNET.CreateDebitNote
        Dim Result As New InterfaceResult
        Dim strSql As String = String.Empty
        Try

            If con.sqlWebConection.State = ConnectionState.Closed Then
                con.sqlWebConection.Open()
            End If

            If EjecutaAceptacionEAPBUnicaTransaccion = False Then
                con.InTransaction = True
                con.IndigoTransaction = con.sqlWebConection.BeginTransaction(IsolationLevel.ReadCommitted, "Interfax crear Nota Debito")
            End If



            Dim ParametroConsecGlosa As String
            strSql = "SELECT GENCONSEC  FROM " & NameContainer & "..CRNPARSIS"
            Dim dtConCartera As DataTable = con.ExecuteCommand_Data(strSql)
            ParametroConsecGlosa = dtConCartera.Rows(0).Item("GENCONSEC").ToString
            dtConCartera = Nothing

            'Dim parametroConsecTipDoc As String
            'strSql = "SELECT GENCONSEC FROM " & NameContainer & "..CTNTIPCOM WHERE OID = " & ParametroConsecGlosa
            'Dim dtConTipDoc As DataTable = con.ExecuteCommand_Data(strSql)
            'If dtConTipDoc.Rows.Count = 0 Then
            '    Result = New InterfaceResult With {.Message = " no existe Id de consecutivo en la tabla CTNTIPCOM, no se puede continuar!!", .Result = False}
            '    Return Result
            'Else
            '    parametroConsecTipDoc = dtConTipDoc.Rows(0).Item("GENCONSEC").ToString
            'End If
            'dtConTipDoc = Nothing


            'obtener numero consecutivo
            Dim ResultEje As Boolean
            Dim ConsecutiveNumber As Integer
            strSql = "UPDATE " & NameContainer & "..geNconsec SET gconumero=gconumero+1,gcoestado='1' WHERE OID = " & ParametroConsecGlosa
            ResultEje = con.ExecuteCommand(strSql)

            strSql = "SELECT  gconumero FROM " & NameContainer & "..geNconsec WHERE OID = " & ParametroConsecGlosa
            Dim dtNumConsecutive As DataTable = con.ExecuteCommand_Data(strSql)
            If dtNumConsecutive.Rows.Count = 0 Then
                Result = New InterfaceResult With {.Message = " no existe Id numero consecutivo en la tabla geNconsec, no se puede continuar!!", .Result = False}
                Return Result
            Else
                ConsecutiveNumber = CInt(dtNumConsecutive.Rows(0).Item("gconumero"))
            End If
            dtNumConsecutive = Nothing

            ConsecutiveNumber = con.fncConcatenar("0", ConsecutiveNumber.ToString, 10, ISQL.Direccion.Izquierda)
            Dim DateActual As Date = Date.Now
            Dim fecha As String
            fecha = Format(DateActual, "yyyyMMdd hh:mm:ss")
            'Dim TerceroNumeros As Integer = Val(Tercero)

            'consultamos el OID del tercero
            Dim OIDtercero As Integer
            strSql = "SELECT  OID FROM " & NameContainer & "..GENTERCER WHERE TERNUMDOC = '" & Tercero & "'"
            Dim dtTercero As DataTable = con.ExecuteCommand_Data(strSql)
            If dtTercero.Rows.Count = 0 Then
                Result = New InterfaceResult With {.Message = " No existe tercero con codigo " & Tercero & ", no se puede continuar!!", .Result = False}
                Return Result
            Else
                OIDtercero = CInt(dtTercero.Rows(0).Item("OID"))
            End If
            dtTercero = Nothing

            'con el codigo de usaurio traemos el OID del usuario
            Dim OIDusuario As Integer
            strSql = "SELECT top 1 OID  FROM " & NameContainer & "..GENUSUARIO  WHERE USUNOMBRE = '" & User & "'"
            Dim dtUsuario As DataTable = con.ExecuteCommand_Data(strSql)
            If dtUsuario.Rows.Count = 0 Then
                Result = New InterfaceResult With {.Message = " no existe Usuario con el codigo " & User & ", no se puede continuar!!", .Result = False}
                Return Result
            Else
                OIDusuario = CInt(dtUsuario.Rows(0).Item("OID"))
            End If
            dtUsuario = Nothing


            'consulto el ID del cliente 
            Dim OIDCliente As Integer
            strSql = "SELECT OID FROM " & NameContainer & "..GENTERCERC WHERE clicodigo = '" & Tercero & "'"
            Dim dtTerceroCliente As DataTable = con.ExecuteCommand_Data(strSql)
            If dtTerceroCliente.Rows.Count = 0 Then
                Result = New InterfaceResult With {.Message = " No existe tercero con codigo " & Tercero & ", no se puede continuar!!", .Result = False}
                Return Result
            Else
                OIDCliente = CInt(dtTerceroCliente.Rows(0).Item("OID"))
            End If
            dtTerceroCliente = Nothing


            Dim ObservacionDetalle As String = String.Empty
            If Reiterated = True Then
                ObservacionDetalle = "Aceptacion EAPB por diferencia en reiteracion - Fra. " & factura & " Mod. Glosas"
            Else
                ObservacionDetalle = "Aceptacion EAPB Glosa - Fra. " & factura & " Mod. Glosas"
            End If

            ''cuenta de crcarter, cabecera
            strSql = "INSERT INTO " & NameContainer & "..CRNNOTA(NOTCONSEC, NOTFECHA, NOTESTADO, GENTERCER, GENTERCERC, NOTOBSERVA, NOTNATURA, NOTINTTIP, NOTINTCON," & _
            "NOTAPLIFAA, NOTCONSFOX, GENUSUARIO2, NOTFECCRE, GENUSUARIO3, NOTFECCON, NOTPARINV,OptimisticLockField , NOTREGDESCAR, NOTINTPRE)VALUES( " & _
            "'" & ConsecutiveNumber & "', " & _
            "convert(varchar(20),'" & (fecha) & "'),0," & OIDtercero & "," & OIDCliente & ",'" & ObservacionDetalle & "',1,-1, " & _
            "'" & factura & "',0,NULL," & OIDusuario & ", convert(varchar(20),'" & (fecha) & "'),null,null,0,0,NULL,NULL)"
            ResultEje = con.ExecuteCommand(strSql)


            Dim Auto As String
            Dim SQL As New SqlDataAdapter("SELECT @@IDENTITY", con.sqlWebConection)
            With SQL
                .SelectCommand.CommandTimeout = 90
                .SelectCommand.Transaction = con.IndigoTransaction
                .SelectCommand.CommandType = CommandType.Text
                Auto = .SelectCommand.ExecuteScalar.ToString
            End With

            'concepto 
            Dim OIDconcepto As Integer
            strSql = "SELECT  OID FROM " & NameContainer & "..CRNCONNOT  WHERE CONCODIGO = '" & Concepto & "'"
            Dim dtConcepto As DataTable = con.ExecuteCommand_Data(strSql)
            If dtConcepto.Rows.Count = 0 Then
                Result = New InterfaceResult With {.Message = " no existe Id del concepto: " & Concepto & ", no se puede continuar!!", .Result = False}
                Return Result
            Else
                OIDconcepto = CInt(dtConcepto.Rows(0).Item("OID"))
            End If
            dtConcepto = Nothing

            'cuenta
            Dim OIDcuenta As Integer
            strSql = "SELECT OID FROM " & NameContainer & "..CTNCUENTA  WHERE CUECODIGO = '" & CuentaConcepto & "'"
            Dim dtCuenta As DataTable = con.ExecuteCommand_Data(strSql)
            If dtCuenta.Rows.Count = 0 Then
                Result = New InterfaceResult With {.Message = " no existe Id de la cuenta: " & CuentaConcepto & ", no se puede continuar!!", .Result = False}
                Return Result
            Else
                OIDcuenta = CInt(dtCuenta.Rows(0).Item("OID"))
            End If
            dtCuenta = Nothing

            'detalle 
            strSql = "INSERT INTO  " & NameContainer & "..CRNNOTADETALLE(CRNNOTA, CRNCONNOT, CTNCUENTA, GENTERCER, CTNCENCOS, CCNNATURA, CCNVALOR, CTNCOMCONR, OptimisticLockField,CCNSOBCAM)VALUES(" & _
            "" & Auto & "," & OIDconcepto & "," & OIDcuenta & "," & OIDtercero & ",NULL,2,convert(varchar(50)," & ValorFac & "),NULL,0,NULL)"
            ResultEje = con.ExecuteCommand(strSql)


            'consult id de la factura
            Dim OIDFactura As Integer
            strSql = "SELECT OID FROM " & NameContainer & "..CRNCXC  WHERE CXCDOCUME = '" & factura & "'"
            Dim dtFactura As DataTable = con.ExecuteCommand_Data(strSql)
            If dtFactura.Rows.Count = 0 Then
                Result = New InterfaceResult With {.Message = " no existe factura: " & factura & ", no se puede continuar!!", .Result = False}
                Return Result
            Else
                OIDFactura = CInt(dtFactura.Rows(0).Item("OID"))
            End If
            dtFactura = Nothing


            'credito
            strSql = "INSERT INTO " & NameContainer & "..CRNNOTAFAC(CRNNOTA, CRNCXC,OptimisticLockField, PSNRECONOC, PSNRECONOMC)VALUES( " & _
            "" & Auto & "," & OIDFactura & ",0,NULL,NULL)"
            ResultEje = con.ExecuteCommand(strSql)

            Dim AutoFactura As String
            SQL = New SqlDataAdapter("SELECT @@IDENTITY", con.sqlWebConection)
            With SQL
                .SelectCommand.CommandTimeout = 90
                .SelectCommand.Transaction = con.IndigoTransaction
                .SelectCommand.CommandType = CommandType.Text
                AutoFactura = .SelectCommand.ExecuteScalar.ToString
            End With


            'consult id del detalle de movimineto de la factura
            strSql = "SELECT  OID FROM " & NameContainer & "..CRNCXCC  WHERE CRNCXC = '" & OIDFactura & "'"
            Dim dtFacturaDetalle As DataTable = con.ExecuteCommand_Data(strSql)

            'detalle de mov factura
            For Each item As DataRow In dtFacturaDetalle.Rows
                strSql = "INSERT INTO " & NameContainer & "..CRNNOTAFACC(CRNNOTAFAC, CRNCXCC,NOTVALOR,OptimisticLockField )VALUES(" & _
                    "" & AutoFactura & "," & item.Item("OID").ToString & ",convert(varchar(50)," & ValorFac & "),0)"
                con.ExecuteCommand(strSql)
            Next


            If EjecutaAceptacionEAPBUnicaTransaccion = False Then
                con.IndigoTransaction.Commit()
            End If


            Result = New InterfaceResult With {.Message = "OK", .Result = True, .Consecutive = ConsecutiveNumber}
        Catch ex As Exception
            con.IndigoTransaction.Rollback()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Result = New InterfaceResult With {.Message = ex.Message, .Result = False}
        Finally
            If EjecutaAceptacionEAPBUnicaTransaccion = False Then
                con.sqlWebConection.Close()
            End If
        End Try
        Return Result
    End Function

#End Region

#Region "Proceso Aceptacion IPS"

    Public Function AcceptanceIPS(IndigoEmpresa As String, NumeroGlosa As String, factura As String, Tercero As String, NameContainer As String, ValorFac As Decimal, FechaFactura As Integer, intOpcion As String, User As String, AfectaServicio As Boolean, Modulo As String, Optional BanderaEjecutaAceptacionEAPB As Boolean = False, Optional VAlorAceptadoEAPB As Decimal = 0) As InterfaceResult Implements IInterfaceNET.AcceptanceIPS
        Dim Result As New InterfaceResult
        If IndigoEmpresa = String.Empty Then
            Throw New ArgumentNullException("codigo empresa indigo vacio")
        End If
        If NumeroGlosa = String.Empty Then
            Throw New ArgumentNullException("Numero Glosa vacio")
        End If
        If factura = String.Empty Then
            Throw New ArgumentNullException("factura vacio")
        End If
        If Tercero = String.Empty Then
            Throw New ArgumentNullException("Tercero vacio")
        End If
        If NameContainer = String.Empty Then
            Throw New ArgumentNullException("Empresa ERP vacio")
        End If
        If ValorFac = 0 Then
            Throw New ArgumentNullException("Valor Fac 0")
        End If
        If intOpcion = String.Empty Then
            Throw New ArgumentNullException("intOpcion vacio")
        End If
        If User = String.Empty Then
            Throw New ArgumentNullException("User vacio")
        End If
        If Modulo = String.Empty Then
            Throw New ArgumentNullException("User vacio")
        End If

        Dim strSql As String = String.Empty
        Try

            'cargamos configuraciones de interface
            If con.sqlWebConection.State = ConnectionState.Closed Then
                con.sqlWebConection.Open()
            End If

            con.InTransaction = True
            con.IndigoTransaction = con.sqlWebConection.BeginTransaction(IsolationLevel.ReadCommitted, "Interfax crear Nota Contable")

            strSql = "SELECT top 1 id, CompanyName,AccountantAccountGeneralAcceptanceC,AccountantAccountPreviousAcceptanceC FROM " & IndigoEmpresa & ".Glosas.GlosasParametersInterface WHERE ContainerName= '" & NameContainer & "'  "
            Dim dtConfi As DataTable = con.ExecuteCommand_Data(strSql)
            Dim CuentaGeneralAceptacionActual As String = String.Empty
            Dim CuentaVigenciaAnteriores As String
            Dim NombreEmpresa As String
            If dtConfi.Rows.Count = 0 Then
                Result = New InterfaceResult With {.Message = "No Existe configuracion de interface para el contenedor" & NameContainer, .Result = False}
                Return Result
            Else
                NombreEmpresa = dtConfi.Rows(0).Item("CompanyName").ToString
                If AfectaServicio = False Then
                    If dtConfi.Rows(0).Item("AccountantAccountGeneralAcceptanceC").ToString = String.Empty Then
                        Result = New InterfaceResult With {.Message = "La cuenta para aceptaciones por parte de la IPS no existe" & NameContainer, .Result = False}
                        Return Result
                    Else
                        CuentaGeneralAceptacionActual = dtConfi.Rows(0).Item("AccountantAccountGeneralAcceptanceC").ToString
                    End If
                End If

                If dtConfi.Rows(0).Item("AccountantAccountPreviousAcceptanceC").ToString = String.Empty Then
                    Result = New InterfaceResult With {.Message = "La cuenta para aceptaciones vigencia anteriores por parte de la IPS no existe" & NameContainer, .Result = False}
                    Return Result
                Else
                    CuentaVigenciaAnteriores = dtConfi.Rows(0).Item("AccountantAccountPreviousAcceptanceC").ToString
                End If
            End If

            'Tercero = con.fncConcatenar("0", Tercero, 15, ISQL.Direccion.Izquierda)
            'valida mes actual este abierto 
            'Dim res As Boolean = Validation.ValidateMonthClose(eTypeInterface.NETPrivate, Date.Now, NameContainer)
            'If res = False Then
            '    Result = New InterfaceResult With {.Message = "El mes " & Month(Date.Now).ToString & " ya esta cerrado o no hay registro tabla Mes (CTNCIEMEN), no se puede continuar" & NameContainer, .Result = False}
            '    Return Result
            'End If


            'valida que la tabla de moviminetos contables por año y mes  (MM201404)  Exista
            'res = Validation.ValidateTableMov(Date.Now, NameContainer)
            'If res = False Then
            '    Result = New InterfaceResult With {.Message = "Tabla movimiento mes no existe, no se ha realizado cierre de mes" & NameContainer, .Result = False}
            '    Return Result
            'End If

            'Busco el consecutivo de comprobantes
            Dim ConsecutivoComprobante As String
            ' strSql = "SELECT top 1  CTNTIPCOM FROM " & NameContainer & "..CTNPARSIS"
            strSql = "SELECT top 1  CTNTIPCOM5 FROM " & NameContainer & "..CRNPARSIS"
            Dim dtConsecutivoComprobante As DataTable = con.ExecuteCommand_Data(strSql)
            If dtConsecutivoComprobante.Rows.Count = 0 Then
                Result = New InterfaceResult With {.Message = "No esta configurado los consecutivo para comprobantes o ha ocurrido un error, favor avisar al administrador del sistema", .Result = False}
                Return Result
            Else
                ConsecutivoComprobante = dtConsecutivoComprobante.Rows(0).Item("CTNTIPCOM5").ToString.TrimEnd
            End If




            Dim NumeroGenerado As Integer
            'busco consecutivo comprobante diario mediante el consecutivo de comprobantes
            Dim ConsecutivoNumcon As Integer
            Dim CodigoTipoDoc As String
            strSql = "SELECT top 1  GENCONSEC, tccodigo FROM " & NameContainer & "..CTNTIPCOM WHERE OID='" & ConsecutivoComprobante & "'"
            Dim dtConsecutivoNumcon As DataTable = con.ExecuteCommand_Data(strSql)
            If dtConsecutivoNumcon.Rows.Count = 0 Then
                Result = New InterfaceResult With {.Message = "No esta configurado los consecutivo para comprobantes diario o ha ocurrido un error, favor avisar al administrador del sistema", .Result = False}
                Return Result
            Else
                ConsecutivoNumcon = CInt(dtConsecutivoNumcon.Rows(0).Item("GENCONSEC").ToString.TrimEnd)
                CodigoTipoDoc = dtConsecutivoNumcon.Rows(0).Item("tccodigo").ToString.TrimEnd
                'Actualiza el consecutive del comprobante
                strSql = "UPDATE " & NameContainer & "..geNconsec SET GCONUMERO = GCONUMERO +1  WHERE OID='" & ConsecutivoNumcon & "'"
                Dim resActualizarConsecutivo As Boolean = con.ExecuteCommand(strSql)
                If resActualizarConsecutivo = True Then
                    'consultamo 
                    Dim dtConseIncrementa As DataTable
                    strSql = " SELECT top 1 GCONUMERO FROM " & NameContainer & "..geNconsec WHERE OID='" & ConsecutivoNumcon & "'"
                    dtConseIncrementa = con.ExecuteCommand_Data(strSql)
                    ' ConsecutivoNumcon = ConsecutivoNumcon + 1
                    NumeroGenerado = CInt(dtConseIncrementa.Rows(0).Item("GCONUMERO"))
                Else
                    Result = New InterfaceResult With {.Message = "ha ocurrido un error actualizando consecutivo, favor avisar al administrador del sistema", .Result = False}
                    Return Result
                End If
            End If





            Dim DateActual As Date = Date.Now
            Dim fecha As String
            fecha = Format(DateActual, "yyyyMMdd hh:mm:ss")
            Dim ResultEje As Boolean
            'Creacion del comprobante      Cabecera
            strSql = "INSERT INTO " & NameContainer & "..CTNCOM" & Year(DateActual) & "(COMCODIGO,CTNTIPCOM,COMFECCOM,COMESTADO,COMDETALLE,COMOIDDOCU,COMNUMDOCU,COMOIDTYPE,COMELIMCOMP,COMCIERANU,COMGECGBATCH,OptimisticLockField)" &
            "VALUES( '" & NumeroGenerado & "','" & ConsecutivoComprobante & "',convert(varchar(20),'" & fecha & "'),0,'Aceptacion Glosa - Fra. " & factura & " Mod. Glosas'," & ConsecutivoComprobante & ",'" & factura & "',0,NULL,0,NULL,0)"
            ResultEje = con.ExecuteCommand(strSql)

            Dim Auto As String
            Dim SQL As New SqlDataAdapter("SELECT @@IDENTITY", con.sqlWebConection)
            With SQL
                .SelectCommand.CommandTimeout = 90
                .SelectCommand.Transaction = con.IndigoTransaction
                .SelectCommand.CommandType = CommandType.Text
                Auto = .SelectCommand.ExecuteScalar.ToString
            End With


            'consultamos el OID del tercero
            Dim OIDtercero As Integer
            strSql = "SELECT  OID FROM " & NameContainer & "..GENTERCER WHERE TERNUMDOC = '" & Tercero & "'"
            Dim dtTercero As DataTable = con.ExecuteCommand_Data(strSql)
            If dtTercero.Rows.Count > 0 Then
                OIDtercero = CInt(dtTercero.Rows(0).Item("OID"))
            Else
                Result = New InterfaceResult With {.Message = "No se encontro ID del Tercero" & Tercero & ", favor avisar al administrador del sistema", .Result = False}
                Return Result
            End If
            dtTercero = Nothing



            'cuenta de cartera PortfoliGlosa
            Dim dtCuentaCartera As DataTable
            Dim cuentaCarteraGlosa As String = String.Empty
            strSql = "SELECT  AccountantAccountCustomers from " & IndigoEmpresa & ".Glosas.GlosaPortfolioGlosada where InvoiceNumber = '" & factura & "'"
            dtCuentaCartera = con.ExecuteCommand_Data(strSql)
            If dtCuentaCartera.Rows.Count > 0 Then
                cuentaCarteraGlosa = dtCuentaCartera.Rows(0).Item("AccountantAccountCustomers").ToString
            End If

            'cuentas de  tabla de configuracion siguiendo la secuencia de la tabla AccountSettingsNET_PrivateMethod
            Dim OIDcuentaConfiguraciones As String
            Dim dtCuenta As DataTable
            'strSql = "select InvoiceRadicate,RectifiableGlosa,LegalProcess,Conciliation from  GENESIS" & IndigoEmpresa & ".Glosas.AccountSettingsNET_PrivateMethod WHERE InvoiceRadicate = '" & cuentaCarteraGlosa & "' "
            'Dim dtAccoutConfi As DataTable = con.ExecuteCommand_Data(strSql)
            'Dim CuentaConfiguraciones As String = String.Empty
            'If dtAccoutConfi.Rows.Count = 0 Then
            '    Result = New InterfaceResult With {.Message = " No existe configuraciones contables glosas para metodo privado (Net) en el campo (Factura Radicada) - cuenta: " & cuentaCarteraGlosa & ", no se puede continuar!!", .Result = False}
            '    Return Result
            'Else
            strSql = "SELECT OID FROM " & NameContainer & "..CTNCUENTA  WHERE CUECODIGO = '" & cuentaCarteraGlosa & "'"
            dtCuenta = con.ExecuteCommand_Data(strSql)
            If dtCuenta.Rows.Count > 0 Then
                OIDcuentaConfiguraciones = CInt(dtCuenta.Rows(0).Item("OID"))
            Else
                Result = New InterfaceResult With {.Message = "No se encontro ID de la cuenta" & cuentaCarteraGlosa & ", favor avisar al administrador del sistema", .Result = False}
                Return Result
            End If
            'End If
            dtCuenta = Nothing

            Dim OIDcuenta As Integer
            If AfectaServicio = False Then
                'cuenta de aeptacion CuentaGeneralAceptacionActual
                strSql = "SELECT OID FROM " & NameContainer & "..CTNCUENTA  WHERE CUECODIGO = '" & CuentaGeneralAceptacionActual & "'"
                dtCuenta = con.ExecuteCommand_Data(strSql)
                If dtCuenta.Rows.Count > 0 Then
                    OIDcuenta = CInt(dtCuenta.Rows(0).Item("OID"))
                Else
                    Result = New InterfaceResult With {.Message = "No se encontro ID de la cuenta" & CuentaGeneralAceptacionActual & ", favor avisar al administrador del sistema", .Result = False}
                    Return Result
                End If
                dtCuenta = Nothing
            End If



            'Detalle
            strSql = "INSERT INTO " & NameContainer & "..CTNCOMD" & Year(DateActual) & "(CTNCOMCONC,CTNCUENTA,GENTERCER,CTNCENCOS,CMMVALDEB, CMMVALCRE, COMDETALLE, CTNCAURET, CMMESTCON, CMMFECCON,OptimisticLockField) " & _
            "VALUES( '" & Auto & "'," & OIDcuentaConfiguraciones & ", " & OIDtercero & ",NULL,0,convert(varchar(50)," & ValorFac & "),'Aceptacion Glosa - Fra. " & factura & " Mod. Glosas',NULL,0,NULL,0)"
            ResultEje = con.ExecuteCommand(strSql)


            Dim campoTmp As String = String.Empty
            If Modulo = "RA" Then
                campoTmp = "m.ValueAcceptedFirstInstance"
            ElseIf Modulo = "RE" Then
                campoTmp = "m.valueAcceptedSecondInstance"
            ElseIf Modulo = "CON" Then
                campoTmp = "m.ValueAcceptedIPSconciliation"
            End If


            '----------------------------------------------------------------------------------------------------------------------
            'si es factura es del año actual y afecta servicio, agrupamos por las cuentas de detalle de factura y CC detalle facturas
            '----------------------------------------------------------------------------------------------------------------------
            If FechaFactura = Year(DateActual) And AfectaServicio = True Then

                'averiguo si la factura registro movimientos por detalle o por detalle qx
                ' strSql = "select  count(*) from " & IndigoEmpresa & ".Glosas.GlosaInvoiceDetail d inner join " & IndigoEmpresa & ".Glosas.GlosaInvoiceDetailQX dqx on d.id = dqx.InvoiceDetailId where d.invoicenumber = '" & factura & "'"
                'strSql = " select count(*) from (" & _
                '            " select m.InvoiceNumber from " & IndigoEmpresa & ".Glosas.GlosaMovementGlosa m " & _
                '            " inner join  " & IndigoEmpresa & ".Glosas.GlosaInvoiceDetail d on d.Id = m.InvoiceDetailId " & _
                '            " where m.InvoiceNumber =  '" & factura & "' and m.ValueAcceptedFirstInstance > 0 	" & _
                '        " UNION" & _
                '            " select  m.InvoiceNumber from " & IndigoEmpresa & ".Glosas.GlosaMovementGlosa m " & _
                '            " inner join  " & IndigoEmpresa & ".Glosas.GlosaInvoiceDetailQX dqx on dqx.Id = m.InvoiceDetailIdQX " & _
                '            " where m.InvoiceNumber =  '" & factura & "'  and m.ValueAcceptedFirstInstance > 0 " & _
                '        " ) as tabletmp"

                'Dim cantQx As Integer = con.ExecuteCommand_Count(strSql)
                'Dim BanderaQx As Boolean
                'If cantQx > 0 Then
                '    BanderaQx = True
                'Else
                '    BanderaQx = False
                'End If

                strSql = "select  sum(" & campoTmp & ") as valor, dqx.AccountantAccountIncome as cuenta, dqx.CostCenterCode from " & IndigoEmpresa & ".Glosas.GlosaMovementGlosa m inner join " & _
                " " & IndigoEmpresa & ".Glosas.GlosaInvoiceDetailQX dqx on dqx.Id = m.InvoiceDetailIdQX " & _
                "where m.InvoiceNumber = '" & factura & "'  and " & campoTmp & " > 0 AND m.state <> 6 " & _
                "group by  dqx.AccountantAccountIncome,dqx.CostCenterCode" & _
                " UNION " & _
                " select sum(" & campoTmp & ") as valor, d.AccountantAccountIncome as cuenta, d.CostCenterCode from " & IndigoEmpresa & ".Glosas.GlosaMovementGlosa m inner join " & _
                " " & IndigoEmpresa & ".Glosas.GlosaInvoiceDetail d on d.Id = m.InvoiceDetailId and m.InvoiceDetailIdQX is null " & _
                "	where m.InvoiceNumber = '" & factura & "' and " & campoTmp & " > 0  AND m.InvoiceDetailIdQX is null AND m.state <> 6 " & _
                "	group by  d.AccountantAccountIncome, d.CostCenterCode"



                '----------------------------------------------------------------------------------------------------------------------
                'agrupamos por cuenta contable de parametros. aceptaciones generales y cc detalle de factura
                '----------------------------------------------------------------------------------------------------------------------
            ElseIf FechaFactura = Year(DateActual) And AfectaServicio = False Then

                'averiguo si la factura registro movimientos por detalle o por detalle qx
                'strSql = "select  count(*) from " & IndigoEmpresa & ".Glosas.GlosaInvoiceDetail d inner join " & IndigoEmpresa & ".Glosas.GlosaInvoiceDetailQX dqx on d.id = dqx.InvoiceDetailId where d.invoicenumber = '" & factura & "'"
                'Dim cantQx As Integer = con.ExecuteCommand_Count(strSql)
                'Dim BanderaQx As Boolean
                'If cantQx > 0 Then
                '    BanderaQx = True
                'Else
                '    BanderaQx = False
                'End If

                'If BanderaQx = True Then
                '    strSql = "select sum(" & campoTmp & ") as valor, p.AccountantAccountGeneralAcceptanceC as cuenta, dqx.CostCenterCode from " & IndigoEmpresa & ".Glosas.GlosaMovementGlosa m " & _
                '            "inner join " & IndigoEmpresa & ".Glosas.GlosaInvoiceDetailQX dqx on dqx.Id = m.InvoiceDetailIdQX " & _
                '            "inner join " & IndigoEmpresa & ".Glosas.GlosaInvoiceDetail d on d.id = dqx.InvoiceDetailId " & _
                '            "inner join " & IndigoEmpresa & ".Glosas.GlosaObjectionsReceptionD ObjD  on d.ObjectionsReceptionDId = ObjD.Id " & _
                '            "inner join " & IndigoEmpresa & ".Glosas.GlosasParametersInterface P on P.id = ObjD.GlosasParametersInterfaceId  " & _
                '            "where m.InvoiceNumber = '" & factura & "' and " & campoTmp & " > 0 " & _
                '            "group by  p.AccountantAccountGeneralAcceptanceC, dqx.CostCenterCode"
                'Else
                '    strSql = "select sum(" & campoTmp & ") as valor, p.AccountantAccountGeneralAcceptanceC as cuenta, d.CostCenterCode from " & IndigoEmpresa & ".Glosas.GlosaMovementGlosa m " & _
                '           "inner join " & IndigoEmpresa & ".Glosas.GlosaInvoiceDetail d on d.Id = m.InvoiceDetailId " & _
                '           "inner join " & IndigoEmpresa & ".Glosas.GlosaObjectionsReceptionD ObjD on d.ObjectionsReceptionDId = ObjD.Id " & _
                '           "inner join " & IndigoEmpresa & ".Glosas.GlosasParametersInterface P on P.id = ObjD.GlosasParametersInterfaceId  " & _
                '           "where m.InvoiceNumber = '" & factura & "' and " & campoTmp & " > 0 " & _
                '           "group by  p.AccountantAccountGeneralAcceptanceC, d.CostCenterCode "
                'End If

                strSql = "select sum(" & campoTmp & ") as valor, p.AccountantAccountGeneralAcceptanceC as cuenta, dqx.CostCenterCode from " & IndigoEmpresa & ".Glosas.GlosaMovementGlosa m " & _
                        "inner join " & IndigoEmpresa & ".Glosas.GlosaInvoiceDetailQX dqx on dqx.Id = m.InvoiceDetailIdQX " & _
                        "inner join " & IndigoEmpresa & ".Glosas.GlosaInvoiceDetail d on d.id = dqx.InvoiceDetailId " & _
                        "inner join " & IndigoEmpresa & ".Glosas.GlosaObjectionsReceptionD ObjD  on d.ObjectionsReceptionDId = ObjD.Id " & _
                        "inner join " & IndigoEmpresa & ".Glosas.GlosasParametersInterface P on P.id = ObjD.GlosasParametersInterfaceId  " & _
                        "where m.InvoiceNumber = '" & factura & "' and " & campoTmp & " > 0 AND m.state <> 6 " & _
                        "group by  p.AccountantAccountGeneralAcceptanceC, dqx.CostCenterCode" & _
                 " UNION " & _
                   " select sum(" & campoTmp & ") as valor, p.AccountantAccountGeneralAcceptanceC as cuenta, d.CostCenterCode from " & IndigoEmpresa & ".Glosas.GlosaMovementGlosa m " & _
                       "inner join " & IndigoEmpresa & ".Glosas.GlosaInvoiceDetail d on d.Id = m.InvoiceDetailId and m.InvoiceDetailIdQX is null " & _
                       "inner join " & IndigoEmpresa & ".Glosas.GlosaObjectionsReceptionD ObjD on d.ObjectionsReceptionDId = ObjD.Id " & _
                       "inner join " & IndigoEmpresa & ".Glosas.GlosasParametersInterface P on P.id = ObjD.GlosasParametersInterfaceId  " & _
                       "where m.InvoiceNumber = '" & factura & "' and " & campoTmp & " > 0 AND m.InvoiceDetailIdQX is null AND m.state <> 6 " & _
                       "group by  p.AccountantAccountGeneralAcceptanceC, d.CostCenterCode "




                '----------------------------------------------------------------------------------------------------------------------
                'vigencia anteriore: agrupamos por la cuenta contable de vigencia anteriores y cc
                '----------------------------------------------------------------------------------------------------------------------
            Else

                'averiguo si la factura registro movimientos por detalle o por detalle qx
                'strSql = "select  count(*) from " & IndigoEmpresa & ".Glosas.GlosaInvoiceDetail d inner join " & IndigoEmpresa & ".Glosas.GlosaInvoiceDetailQX dqx on d.id = dqx.InvoiceDetailId where d.invoicenumber = '" & factura & "'"
                'Dim cantQx As Integer = con.ExecuteCommand_Count(strSql)
                'Dim BanderaQx As Boolean
                'If cantQx > 0 Then
                '    BanderaQx = True
                'Else
                '    BanderaQx = False
                'End If


                'If BanderaQx = True Then
                '    strSql = "select sum(" & campoTmp & ") as valor, p.AccountantAccountPreviousAcceptanceC as cuenta, dqx.CostCenterCode from " & IndigoEmpresa & ".Glosas.GlosaMovementGlosa m " & _
                '            "inner join " & IndigoEmpresa & ".Glosas.GlosaInvoiceDetailQX dqx on dqx.Id = m.InvoiceDetailIdQX " & _
                '            "inner join " & IndigoEmpresa & ".Glosas.GlosaInvoiceDetail d on d.id = dqx.InvoiceDetailId " & _
                '            "inner join " & IndigoEmpresa & ".Glosas.GlosaObjectionsReceptionD ObjD  on d.ObjectionsReceptionDId = ObjD.Id " & _
                '            "inner join " & IndigoEmpresa & ".Glosas.GlosasParametersInterface P on P.id = ObjD.GlosasParametersInterfaceId  " & _
                '            "where m.InvoiceNumber = '" & factura & "' and " & campoTmp & " > 0 " & _
                '            "group by  p.AccountantAccountPreviousAcceptanceC, dqx.CostCenterCode"
                'Else
                '    strSql = "select sum(" & campoTmp & ") as valor, p.AccountantAccountPreviousAcceptanceC as cuenta, d.CostCenterCode from " & IndigoEmpresa & ".Glosas.GlosaMovementGlosa m " & _
                '           "inner join " & IndigoEmpresa & ".Glosas.GlosaInvoiceDetail d on d.Id = m.InvoiceDetailId " & _
                '           "inner join " & IndigoEmpresa & ".Glosas.GlosaObjectionsReceptionD ObjD on d.ObjectionsReceptionDId = ObjD.Id " & _
                '           "inner join " & IndigoEmpresa & ".Glosas.GlosasParametersInterface P on P.id = ObjD.GlosasParametersInterfaceId  " & _
                '           "where m.InvoiceNumber = '" & factura & "' and " & campoTmp & " > 0 " & _
                '           "group by  p.AccountantAccountPreviousAcceptanceC, d.CostCenterCode "
                'End If

                strSql = "select sum(" & campoTmp & ") as valor, p.AccountantAccountPreviousAcceptanceC as cuenta, dqx.CostCenterCode from " & IndigoEmpresa & ".Glosas.GlosaMovementGlosa m " & _
                        "inner join " & IndigoEmpresa & ".Glosas.GlosaInvoiceDetailQX dqx on dqx.Id = m.InvoiceDetailIdQX " & _
                        "inner join " & IndigoEmpresa & ".Glosas.GlosaInvoiceDetail d on d.id = dqx.InvoiceDetailId " & _
                        "inner join " & IndigoEmpresa & ".Glosas.GlosaObjectionsReceptionD ObjD  on d.ObjectionsReceptionDId = ObjD.Id " & _
                        "inner join " & IndigoEmpresa & ".Glosas.GlosasParametersInterface P on P.id = ObjD.GlosasParametersInterfaceId  " & _
                        "where m.InvoiceNumber = '" & factura & "' and " & campoTmp & " > 0 AND m.state <> 6 " & _
                        "group by  p.AccountantAccountPreviousAcceptanceC, dqx.CostCenterCode" & _
               " UNION " & _
                 " select sum(" & campoTmp & ") as valor, p.AccountantAccountPreviousAcceptanceC as cuenta, d.CostCenterCode from " & IndigoEmpresa & ".Glosas.GlosaMovementGlosa m " & _
                       "inner join " & IndigoEmpresa & ".Glosas.GlosaInvoiceDetail d on d.Id = m.InvoiceDetailId and m.InvoiceDetailIdQX is null " & _
                       "inner join " & IndigoEmpresa & ".Glosas.GlosaObjectionsReceptionD ObjD on d.ObjectionsReceptionDId = ObjD.Id " & _
                       "inner join " & IndigoEmpresa & ".Glosas.GlosasParametersInterface P on P.id = ObjD.GlosasParametersInterfaceId  " & _
                       "where m.InvoiceNumber = '" & factura & "' and " & campoTmp & " > 0 AND m.InvoiceDetailIdQX is null AND m.state <> 6 " & _
                       "group by  p.AccountantAccountPreviousAcceptanceC, d.CostCenterCode "



            End If


            'cargamos datatable de insercion de movimientos
            Dim dtCuentasAfectaServicio As DataTable = con.ExecuteCommand_Data(strSql)
            If dtCuentasAfectaServicio.Rows.Count = 0 Then
                Result = New InterfaceResult With {.Message = "Aceptacion IPS: No se encontró movimientos glosas para la factura:" & factura, .Result = False}
                Return Result
            End If



            For Each item As DataRow In dtCuentasAfectaServicio.Rows
                Dim cuentaDebito As String
                cuentaDebito = item.Item("cuenta").ToString

                Dim centroCosto As String = item.Item("CostCenterCode").ToString
                Dim ValorServicio As String = item.Item("valor")

                'cuenta
                If AfectaServicio = True And FechaFactura = Year(DateActual) Then
                    OIDcuenta = cuentaDebito ' si afecta servicio la tabla de detalles de factura o qx ya trae el OID de la cuenta, no se necesit consultarla
                Else
                    strSql = "SELECT OID FROM " & NameContainer & "..CTNCUENTA  WHERE CUECODIGO = '" & cuentaDebito & "'"
                    dtCuenta = con.ExecuteCommand_Data(strSql)
                    If dtCuenta.Rows.Count = 0 Then
                        Result = New InterfaceResult With {.Message = "No se encontro ID de la cuenta" & cuentaDebito & ", favor avisar al administrador del sistema", .Result = False}
                        Return Result
                    Else
                        OIDcuenta = CInt(dtCuenta.Rows(0).Item("OID"))
                    End If
                    dtCuenta = Nothing
                End If


                'OID centro de costo
                Dim OIDcentrocosto As Integer
                strSql = "SELECT OID FROM " & NameContainer & "..CTNCENCOS  WHERE CCCODIGO = '" & centroCosto & "'"
                Dim dtCentroCosto As DataTable = con.ExecuteCommand_Data(strSql)
                If dtCentroCosto.Rows.Count = 0 Then
                    Result = New InterfaceResult With {.Message = "No se encontro ID del centro de costo" & centroCosto & ", favor avisar al administrador del sistema", .Result = False}
                    Return Result
                Else
                    OIDcentrocosto = CInt(dtCentroCosto.Rows(0).Item("OID"))
                End If
                dtCentroCosto = Nothing


                'Detalle
                strSql = "INSERT INTO " & NameContainer & "..CTNCOMD" & Year(DateActual) & "(CTNCOMCONC,CTNCUENTA,GENTERCER,CTNCENCOS,CMMVALDEB, CMMVALCRE, COMDETALLE, CTNCAURET, CMMESTCON, CMMFECCON,OptimisticLockField) " & _
                "VALUES( '" & Auto & "'," & OIDcuenta & ", " & OIDtercero & "," & OIDcentrocosto & ",convert(varchar(50)," & ValorServicio & "),0,'Aceptacion Glosa - Fra. " & factura & " Mod. Glosas',NULL,0,NULL,0)"
                ResultEje = con.ExecuteCommand(strSql)



            Next




            Dim UnicaTransaccionResultado As New InterfaceResult
            If BanderaEjecutaAceptacionEAPB = True Then
                UnicaTransaccionResultado = Me.AcceptanceEAPB(IndigoEmpresa, NumeroGlosa, factura, Tercero, NameContainer, VAlorAceptadoEAPB, FechaFactura, intOpcion, User, True)
                If UnicaTransaccionResultado.Result = False Then 'si se genera un error
                    Result = UnicaTransaccionResultado  'New InterfaceResult With {.Message = "El mes " & Month(Date.Now).ToString & " ya esta cerrado, no se puede continuar" & NameContainer, .Result = False}
                    con.IndigoTransaction.Rollback()
                    Return Result
                End If
            End If


            con.IndigoTransaction.Commit()

            Validation.AuditInterface(IndigoEmpresa, intOpcion, NumeroGlosa, factura, 1, ConsecutivoComprobante, NumeroGenerado)

            If BanderaEjecutaAceptacionEAPB = True Then
                Result = New InterfaceResult With {.Message = "Se Generaron los siguientes documentos: Comprobante Contable: " & CodigoTipoDoc & " - " & NumeroGenerado & " Nota Debito: " & UnicaTransaccionResultado.Consecutive & " , Empresa: " & NombreEmpresa, .Result = True, .Consecutive = CodigoTipoDoc & " - " & NumeroGenerado}
            Else
                Result = New InterfaceResult With {.Message = "Se Generaron los siguientes documentos: Comprobante Contable: " & CodigoTipoDoc & " - " & NumeroGenerado & ", Empresa: " & NombreEmpresa, .Result = True, .Consecutive = CodigoTipoDoc & " - " & NumeroGenerado}
            End If

            'Result = New InterfaceResult With {.Message = "Se Generaron los siguientes documentos: Comprobante Contable: " & ConsecutivoComprobante & " - " & ConsecutivoNumcon & ", Empresa: " & NombreEmpresa, .Result = True, .Consecutive = ConsecutivoComprobante & " - " & ConsecutivoNumcon}
        Catch ex As Exception
            con.IndigoTransaction.Rollback()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Result = New InterfaceResult With {.Message = ex.Message, .Result = False}
        Finally
            con.sqlWebConection.Close()
        End Try
        Return Result
    End Function

#End Region

#Region "Proceso Traslado CObro Juridico"

    Public Function TransferJuridical(IndigoEmpresa As String, NumeroGlosa As String, factura As String, Tercero As String, NameContainer As String, ValorFac As Decimal, FechaFactura As Integer, intOpcion As String, User As String) As InterfaceResult Implements IInterfaceNET.TransferJuridical
        Dim Result As New InterfaceResult
        If IndigoEmpresa = String.Empty Then
            Throw New ArgumentNullException("codigo empresa indigo vacio")
        End If
        If NumeroGlosa = String.Empty Then
            Throw New ArgumentNullException("Numero Glosa vacio")
        End If
        If factura = String.Empty Then
            Throw New ArgumentNullException("factura vacio")
        End If
        If Tercero = String.Empty Then
            Throw New ArgumentNullException("Tercero vacio")
        End If
        If NameContainer = String.Empty Then
            Throw New ArgumentNullException("Empresa ERP vacio")
        End If
        If ValorFac = 0 Then
            Throw New ArgumentNullException("Valor Fac 0")
        End If
        If intOpcion = String.Empty Then
            Throw New ArgumentNullException("intOpcion vacio")
        End If
        If User = String.Empty Then
            Throw New ArgumentNullException("User vacio")
        End If


        Dim strSql As String = String.Empty
        Try

            'cargamos configuraciones de interface
            If con.sqlWebConection.State = ConnectionState.Closed Then
                con.sqlWebConection.Open()
            End If

            con.InTransaction = True
            con.IndigoTransaction = con.sqlWebConection.BeginTransaction(IsolationLevel.ReadCommitted, "Interfax crear Nota Contable")

            strSql = "SELECT top 1 id, CompanyName,AccountantAccountGeneralAcceptanceC,AccountantAccountPreviousAcceptanceC FROM " & IndigoEmpresa & ".Glosas.GlosasParametersInterface WHERE ContainerName= '" & NameContainer & "'  "
            Dim dtConfi As DataTable = con.ExecuteCommand_Data(strSql)
            Dim NombreEmpresa As String
            If dtConfi.Rows.Count = 0 Then
                Result = New InterfaceResult With {.Message = "No Existe configuracion de interface para el contenedor" & NameContainer, .Result = False}
                Return Result
            Else
                NombreEmpresa = dtConfi.Rows(0).Item("CompanyName").ToString
            End If

            'Tercero = con.fncConcatenar("0", Tercero, 15, ISQL.Direccion.Izquierda)
            'valida mes actual este abierto 
            'Dim res As Boolean = Validation.ValidateMonthClose(eTypeInterface.NETPrivate, Date.Now, NameContainer)
            'If res = False Then
            '    Result = New InterfaceResult With {.Message = "El mes " & Month(Date.Now).ToString & " ya esta cerrado, no se puede continuar" & NameContainer, .Result = False}
            '    Return Result
            'End If


            'valida que la tabla de moviminetos contables por año y mes  (MM201404)  Exista
            'res = Validation.ValidateTableMov(Date.Now, NameContainer)
            'If res = False Then
            '    Result = New InterfaceResult With {.Message = "Tabla movimiento mes no existe, no se ha realizado cierre de mes" & NameContainer, .Result = False}
            '    Return Result
            'End If

            'Busco el consecutivo de comprobantes
            Dim ConsecutivoComprobante As String
            'strSql = "SELECT top 1  CTNTIPCOM FROM " & NameContainer & "..CTNPARSIS"
            strSql = "SELECT top 1  CTNTIPCOM7 FROM " & NameContainer & "..CRNPARSIS "    'TIPO DE COMPROBANTE PARA DIFICIL RECAUDO
            Dim dtConsecutivoComprobante As DataTable = con.ExecuteCommand_Data(strSql)
            If dtConsecutivoComprobante.Rows.Count = 0 Then
                Result = New InterfaceResult With {.Message = "No esta configurado los consecutivo para comprobantes o ha ocurrido un error, favor avisar al administrador del sistema", .Result = False}
                Return Result
            Else
                ConsecutivoComprobante = dtConsecutivoComprobante.Rows(0).Item("CTNTIPCOM7").ToString.TrimEnd
            End If




            'busco consecutivo comprobante diario mediante el consecutivo de comprobantes
            Dim ConsecutivoNumcon As Integer
            Dim numerogenerado As Integer
            Dim CodigoTipoDoc As String
            strSql = "SELECT top 1  GENCONSEC,tccodigo FROM " & NameContainer & "..CTNTIPCOM WHERE OID='" & ConsecutivoComprobante & "'"
            Dim dtConsecutivoNumcon As DataTable = con.ExecuteCommand_Data(strSql)
            If dtConsecutivoNumcon.Rows.Count = 0 Then
                Result = New InterfaceResult With {.Message = "No esta configurado los consecutivo para comprobantes  o ha ocurrido un error, favor avisar al administrador del sistema", .Result = False}
                Return Result
            Else
                ConsecutivoNumcon = CInt(dtConsecutivoNumcon.Rows(0).Item("GENCONSEC").ToString.TrimEnd)
                CodigoTipoDoc = dtConsecutivoNumcon.Rows(0).Item("tccodigo").ToString.TrimEnd
                'Actualiza el consecutive del comprobante
                strSql = "UPDATE " & NameContainer & "..geNconsec SET GCONUMERO = GCONUMERO +1  WHERE OID='" & ConsecutivoNumcon & "'"
                Dim resActualizarConsecutivo As Boolean = con.ExecuteCommand(strSql)
                If resActualizarConsecutivo = True Then
                    'consultamo 
                    Dim dtConseIncrementa As DataTable
                    strSql = " SELECT top 1 GCONUMERO FROM " & NameContainer & "..geNconsec WHERE OID='" & ConsecutivoNumcon & "'"
                    dtConseIncrementa = con.ExecuteCommand_Data(strSql)
                    ' ConsecutivoNumcon = ConsecutivoNumcon + 1
                    numerogenerado = CInt((dtConseIncrementa.Rows(0).Item("GCONUMERO")))
                Else
                    Result = New InterfaceResult With {.Message = "ha ocurrido un error actualizando consecutivo, favor avisar al administrador del sistema", .Result = False}
                    Return Result
                End If
            End If


            Dim DateActual As Date = Date.Now
            'valido que el consecutivo generado no este ya registrado 
            strSql = "SELECT count(*) FROM " & NameContainer & "..CTNCOM" & Year(DateActual) & " WHERE COMCODIGO= '" & numerogenerado & "' AND CTNTIPCOM = '" & ConsecutivoNumcon & "'  "
            Dim dtCountConsecutive As Integer = con.ExecuteCommand_Count(strSql)
            If dtCountConsecutive > 0 Then
                Result = New InterfaceResult With {.Message = "El consecutivo " & numerogenerado & " del Tipo de comprobante difícil recaudo ya existe", .Result = False}
                Return Result
            End If



            Dim fecha As String
            fecha = Format(DateActual, "yyyyMMdd hh:mm:ss")
            Dim ResultEje As Boolean
            'Creacion del comprobante      Cabecera
            strSql = "INSERT INTO " & NameContainer & "..CTNCOM" & Year(DateActual) & "(COMCODIGO,CTNTIPCOM,COMFECCOM,COMESTADO,COMDETALLE,COMOIDDOCU,COMNUMDOCU,COMOIDTYPE,COMELIMCOMP,COMCIERANU,COMGECGBATCH,OptimisticLockField)" &
            "VALUES( '" & numerogenerado & "','" & ConsecutivoComprobante & "',convert(varchar(20),'" & fecha & "'),0,'Traslado cobro juridico Glosa - Fra. " & factura & " Mod. Glosas'," & ConsecutivoComprobante & ",'" & factura & "',0,NULL,0,NULL,0)"
            ResultEje = con.ExecuteCommand(strSql)

            Dim Auto As String
            Dim SQL As New SqlDataAdapter("SELECT @@IDENTITY", con.sqlWebConection)
            With SQL
                .SelectCommand.CommandTimeout = 90
                .SelectCommand.Transaction = con.IndigoTransaction
                .SelectCommand.CommandType = CommandType.Text
                Auto = .SelectCommand.ExecuteScalar.ToString
            End With


            'consultamos el OID del tercero
            Dim OIDtercero As Integer
            strSql = "SELECT  OID FROM " & NameContainer & "..GENTERCER WHERE TERNUMDOC = '" & Tercero & "'"
            Dim dtTercero As DataTable = con.ExecuteCommand_Data(strSql)
            If dtTercero.Rows.Count > 0 Then
                OIDtercero = CInt(dtTercero.Rows(0).Item("OID"))
            Else
                Result = New InterfaceResult With {.Message = "No se encontro ID del Tercero" & Tercero & ", favor avisar al administrador del sistema", .Result = False}
                Return Result
            End If

            dtTercero = Nothing



            'cuenta de cartera PortfoliGlosa
            Dim dtCuentaCartera As DataTable
            Dim dtOIDCuenta As DataTable
            Dim strOIDCuentaCartera As String = String.Empty
            Dim cuentaCarteraGlosa As String = String.Empty
            strSql = "SELECT  AccountantAccountCustomers from " & IndigoEmpresa & ".Glosas.GlosaPortfolioGlosada where InvoiceNumber = '" & factura & "'"
            dtCuentaCartera = con.ExecuteCommand_Data(strSql)
            If dtCuentaCartera.Rows.Count > 0 Then
                cuentaCarteraGlosa = dtCuentaCartera.Rows(0).Item("AccountantAccountCustomers").ToString
                strSql = "SELECT OID FROM " & NameContainer & "..CTNCUENTA  WHERE CUECODIGO = '" & cuentaCarteraGlosa & "'"
                dtOIDCuenta = con.ExecuteCommand_Data(strSql)
                If dtOIDCuenta.Rows.Count > 0 Then
                    strOIDCuentaCartera = CInt(dtOIDCuenta.Rows(0).Item("OID"))
                Else
                    Result = New InterfaceResult With {.Message = "No se encontro ID de la cuenta" & cuentaCarteraGlosa & ", favor avisar al administrador del sistema", .Result = False}
                    Return Result
                End If
            End If
            dtOIDCuenta = Nothing



            'Detalle
            strSql = "INSERT INTO " & NameContainer & "..CTNCOMD" & Year(DateActual) & "(CTNCOMCONC,CTNCUENTA,GENTERCER,CTNCENCOS,CMMVALDEB, CMMVALCRE, COMDETALLE, CTNCAURET, CMMESTCON, CMMFECCON,OptimisticLockField) " & _
            "VALUES( '" & Auto & "'," & strOIDCuentaCartera & ", " & OIDtercero & ",NULL,0,convert(varchar(50)," & ValorFac & "),'Aceptacion Glosa - Fra. " & factura & " Mod. Glosas',NULL,0,NULL,0)"
            ResultEje = con.ExecuteCommand(strSql)




            'consultamos el concepto de la cuentam, apartir de este concepto cargamos la cuenta de traslado a cobro juridio a Debitar
            Dim dtConcept As DataTable
            Dim strConcept As String
            strSql = "SELECT CONCODIGO FROM  " & NameContainer & "..crNConNOT con inner join  " & NameContainer & "..CTNCUENTA cue on con.CTNCUENTA = cue.oid where cue.cuecodigo = '" & cuentaCarteraGlosa & "'"
            dtConcept = con.ExecuteCommand_Data(strSql)
            If dtConcept.Rows.Count > 0 Then
                strConcept = dtConcept.Rows(0).Item("CONCODIGO").ToString
            Else
                Result = New InterfaceResult With {.Message = "Traslado Cobro Juridico: No se encontro concepto para la cuenta " & cuentaCarteraGlosa & ", no se puede continuar!!", .Result = False}
                Return Result
            End If


            'cuenta de traslado a cobro juridico
            Dim OIDLegalProceso As Integer
            strSql = "select InvoiceRadicate,RectifiableGlosa,LegalProcess,Conciliation from  " & IndigoEmpresa & ".Glosas.AccountSettingsNET_PrivateMethod WHERE RectifiableGlosa = '" & strConcept & "' "
            Dim dtAccoutConfi As DataTable = con.ExecuteCommand_Data(strSql)
            Dim CuentaConfiguraciones As String = String.Empty
            If dtAccoutConfi.Rows.Count = 0 Then
                Result = New InterfaceResult With {.Message = " No existe configuraciones contables glosas para metodo privado (Net) en el campo (Glosa Subsanada) - concepto: " & strConcept & ", no se puede continuar!!", .Result = False}
                Return Result
            Else
                Dim LegalProcess As String
                If dtAccoutConfi.Rows.Count > 0 AndAlso dtAccoutConfi.Rows(0).Item("LegalProcess") <> String.Empty Then
                    LegalProcess = dtAccoutConfi.Rows(0).Item("LegalProcess").ToString
                    strSql = "SELECT OID FROM " & NameContainer & "..CTNCUENTA  WHERE CUECODIGO = '" & LegalProcess & "'"
                    dtOIDCuenta = con.ExecuteCommand_Data(strSql)
                    If dtOIDCuenta.Rows.Count > 0 Then
                        OIDLegalProceso = CInt(dtOIDCuenta.Rows(0).Item("OID"))
                    Else
                        Result = New InterfaceResult With {.Message = "No se encontro ID de la cuenta" & LegalProcess & ", favor avisar al administrador del sistema", .Result = False}
                        Return Result
                    End If
                Else
                    Result = New InterfaceResult With {.Message = "Traslado Cobro Juridico:no se encontro cuenta de traslado a cobro juridico, favor avisar al administrador del sistema", .Result = False}
                    Return Result
                End If
            End If
            dtOIDCuenta = Nothing



            'Detalle
            strSql = "INSERT INTO " & NameContainer & "..CTNCOMD" & Year(DateActual) & "(CTNCOMCONC,CTNCUENTA,GENTERCER,CTNCENCOS,CMMVALDEB, CMMVALCRE, COMDETALLE, CTNCAURET, CMMESTCON, CMMFECCON,OptimisticLockField) " & _
            "VALUES( '" & Auto & "'," & OIDLegalProceso & ", " & OIDtercero & ",NULL,convert(varchar(50)," & ValorFac & "),0,'Traslado Cobro Juridico - Fra. " & factura & " Mod. Glosas',NULL,0,NULL,0)"
            ResultEje = con.ExecuteCommand(strSql)



            con.IndigoTransaction.Commit()

            Validation.AuditInterface(IndigoEmpresa, intOpcion, NumeroGlosa, factura, 1, ConsecutivoComprobante, numerogenerado)


            Result = New InterfaceResult With {.Message = "Se Generaron los siguientes documentos: Comprobante Contable: " & CodigoTipoDoc & " - " & numerogenerado & ", Empresa: " & NombreEmpresa, .Result = True, .Consecutive = CodigoTipoDoc & " - " & numerogenerado}


            'Result = New InterfaceResult With {.Message = "Se Generaron los siguientes documentos: Comprobante Contable: " & ConsecutivoComprobante & " - " & ConsecutivoNumcon & ", Empresa: " & NombreEmpresa, .Result = True, .Consecutive = ConsecutivoComprobante & " - " & ConsecutivoNumcon}
        Catch ex As Exception
            con.IndigoTransaction.Rollback()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Result = New InterfaceResult With {.Message = ex.Message, .Result = False}
        Finally
            con.sqlWebConection.Close()
        End Try
        Return Result
    End Function

#End Region

#Region "Cargar Saldo Actual de cartera "
    ''' <summary>
    ''' Funcion para cargar saldo de fatura de ERP
    ''' </summary>
    ''' <param name="NumberInvoice"></param>
    ''' <param name="NameContainer"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function LoadBalanceInvoice(NumberInvoice As String, NameContainer As String) As Decimal Implements IInterfaceNET.LoadBalanceInvoice

        If NameContainer = String.Empty Then
            Throw New ArgumentNullException("NameContainer vacio")
        End If
        If NumberInvoice = String.Empty Then
            Throw New ArgumentNullException("factura vacio")
        End If
        Dim balance As Decimal
        Dim strSql As String = String.Empty
        Try

            'cargamos configuraciones de interface
            If con.sqlWebConection.State = ConnectionState.Closed Then
                con.sqlWebConection.Open()
            End If

            '    strSql = "select crnsaldo  FROM " & NameContainer & "..CRNCXC  WHERE cxcdocume = '" & NumberInvoice & "'"
            strSql = "select (B.CCVALOR+B.CCVALDEB-B.CCVALCRE-B.CCVALABO-B.CCVALTRA) as crnsaldo  FROM " & NameContainer & "..CRNCXC car  INNER JOIN " & NameContainer & "..CRNCXCC B  ON  car.OID=B.CRNCXC  WHERE car.cxcdocume = '" & NumberInvoice & "'"
            Dim dtbalance As DataTable = con.ExecuteCommand_Data(strSql)
            If dtbalance.Rows.Count > 0 Then
                balance = dtbalance.Rows(0).Item("crnsaldo")
            End If


            Return balance

        Catch ex As Exception
            con.IndigoTransaction.Rollback()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            balance = 0
        Finally
            con.sqlWebConection.Close()
        End Try
        Return balance
    End Function


#End Region

#Region "Proceso de Reiteracion: NOTA DEBITO para el saldo que queda sin glosar y sin reiterar"

    Public Function BalanceReiterated(IndigoEmpresa As String, NumeroGlosa As String, factura As String, Tercero As String, NameContainer As String, ValorFac As Decimal, FechaFactura As Integer, intOpcion As String, User As String, Optional ByVal EjecutaAceptacionEAPBUnicaTransaccion As Boolean = False) As InterfaceResult Implements IInterfaceNET.BalanceReiterated
        Dim Result As New InterfaceResult
        If IndigoEmpresa = String.Empty Then
            Throw New ArgumentNullException("codigo empresa indigo vacio")
        End If
        If NumeroGlosa = String.Empty Then
            Throw New ArgumentNullException("Numero Glosa vacio")
        End If
        If factura = String.Empty Then
            Throw New ArgumentNullException("factura vacio")
        End If
        If Tercero = String.Empty Then
            Throw New ArgumentNullException("Tercero vacio")
        End If
        If NameContainer = String.Empty Then
            Throw New ArgumentNullException("Empresa ERP vacio")
        End If
        If ValorFac = 0 Then
            Throw New ArgumentNullException("Valor Fac 0")
        End If
        If intOpcion = String.Empty Then
            Throw New ArgumentNullException("intOpcion vacio")
        End If
        If User = String.Empty Then
            Throw New ArgumentNullException("User vacio")
        End If

        Dim strSql As String = String.Empty
        Try

            'cargamos configuraciones de interface
            If con.sqlWebConection.State = ConnectionState.Closed Then
                con.sqlWebConection.Open()
            End If

            strSql = "SELECT top 1 id, CompanyName FROM " & IndigoEmpresa & ".Glosas.GlosasParametersInterface WHERE ContainerName= '" & NameContainer & "'  "
            Dim dtConfi As DataTable = con.ExecuteCommand_Data(strSql)
            Dim NombreEmpresa As String
            If dtConfi.Rows.Count = 0 Then
                Result = New InterfaceResult With {.Message = "No Existe configuracion de interface para el contenedor" & NameContainer, .Result = False}
                Return Result
            Else
                NombreEmpresa = dtConfi.Rows(0).Item("CompanyName").ToString
            End If

            'cargamos cuenta apartir de tercero y factura, Validamos Existencia de Cuenta

            ' Tercero = con.fncConcatenar("0", Tercero, 15, ISQL.Direccion.Izquierda)

            Dim cuentaCartera As String
            strSql = "SELECT top 1  C.CUECODIGO FROM " & NameContainer & "..CRNCXC F INNER JOIN " & NameContainer & "..GENTERCER T on F.GENTERCER = T.OID " & _
                             "INNER Join " & NameContainer & "..CTNCUENTA C on F.CTNCUENTA = C.OID WHERE  F.CXCDOCUME ='" & factura & "' AND T.TERNUMDOC = '" & Tercero & "'"
            Dim dtCuenta As DataTable = con.ExecuteCommand_Data(strSql)
            If dtCuenta.Rows.Count = 0 Then
                Result = New InterfaceResult With {.Message = "No existe la factura en ERP, favor avisar al administrador del sistema", .Result = False}
                Return Result
            Else
                If dtCuenta.Rows(0).Item("CUECODIGO").ToString <> String.Empty Then
                    cuentaCartera = dtCuenta.Rows(0).Item("CUECODIGO").ToString
                    Dim res As Integer = Validation.ValidateAccount(eTypeInterface.NETPrivate, NameContainer, cuentaCartera)
                    If res = 0 Then
                        Result = New InterfaceResult With {.Message = "No existe la cuenta " & cuentaCartera & " de la factura en DGH, favor avisar al administrador del sistema", .Result = False}
                        Return Result
                    End If
                Else
                    Result = New InterfaceResult With {.Message = "No existe la factura en ERP, favor avisar al administrador del sistema", .Result = False}
                    Return Result
                End If
            End If


            ' validar el saldo y el plan
            'strSql = "SELECT   count(*) FROM " & NameContainer & "..CRNCXC A INNER JOIN  " & NameContainer & "..CRNCXCC B " & _
            '        "on A.OID=B.CRNCXC INNER JOIN  " & NameContainer & "..GENTERCER C on A.GENTERCER=C.OID  " & _
            '        "WHERE  A.CXCDOCUME='" & factura & "' AND C.TERNUMDOC='" & Tercero & "'  " & _
            '        "AND (B.CCVALOR+B.CCVALDEB-B.CCVALCRE-B.CCVALABO-B.CCVALTRA) > 0"
            'Dim resint As Integer = con.ExecuteCommand_Count(strSql)
            'If resint = 0 Then
            '    Result = New InterfaceResult With {.Message = "  La factura " & factura & " no  tiene saldo, no se puede continuar!!", .Result = False}
            '    Return Result
            'End If



            strSql = "select InvoiceRadicate,RectifiableGlosa,LegalProcess,Conciliation from  " & IndigoEmpresa & ".Glosas.AccountSettingsNET_PrivateMethod WHERE InvoiceRadicate = '" & cuentaCartera & "' "
            Dim dtConcept As DataTable = con.ExecuteCommand_Data(strSql)
            Dim concepto As String
            If dtConcept.Rows.Count = 0 Then
                Result = New InterfaceResult With {.Message = " No existe configuraciones contables glosas para metodo privado (Net) en el campo (Factura Radicada) - cuenta: " & cuentaCartera & ", no se puede continuar!!", .Result = False}
                Return Result
            Else
                concepto = dtConcept.Rows(0).Item("Conciliation").ToString  'concepto de Conciliation nota debito aceptacion EAPB
            End If
            'consultamos el codigo de cuenta para el concepto de "Conciliation"
            strSql = "SELECT cue.CUECODIGO  FROM " & NameContainer & "..crNConNOT concep INNER JOIN " & NameContainer & "..CTNCUENTA Cue on Concep.CTNCUENTA = cue.OID  WHERE concep.CONCODIGO= '" & concepto & "'"
            Dim dtCuentaConcepto As DataTable = con.ExecuteCommand_Data(strSql)
            Dim CuentaConcepto As String
            If dtCuentaConcepto.Rows.Count = 0 Then
                Result = New InterfaceResult With {.Message = " no existe cuenta para el concepto " & concepto & " , no se puede continuar!!", .Result = False}
                Return Result
            Else
                CuentaConcepto = dtCuentaConcepto.Rows(0).Item("CUECODIGO").ToString
            End If


            'validamos la cuenta maneje ce y  nivel 5
            Dim resint As Integer
            strSql = "SELECT count(*) FROM " & NameContainer & "..CTNCUENTA WHERE CUECODIGO='" & CuentaConcepto & "' and /*CUEMANCEN=1 and*/ CTNNIVEL = '5'"
            resint = con.ExecuteCommand_Count(strSql)
            If resint = 0 Then
                Result = New InterfaceResult With {.Message = "La cuenta no existe, o no es de nivel 5'!!", .Result = False}
                Return Result
            End If


            Dim resultCreateDebitNOte As InterfaceResult = Me.CreateDebitNote(NameContainer, Tercero, factura, ValorFac, User, cuentaCartera, concepto, CuentaConcepto, True, EjecutaAceptacionEAPBUnicaTransaccion)
            If resultCreateDebitNOte.Result = False Then
                Return resultCreateDebitNOte
            End If

            Validation.AuditInterface(IndigoEmpresa, intOpcion, NumeroGlosa, factura, 3, concepto, resultCreateDebitNOte.Consecutive)

            Result = New InterfaceResult With {.Message = "Se Generaron los siguientes documentos: Nota Debito: " & resultCreateDebitNOte.Consecutive & ", Empresa: " & NombreEmpresa, .Result = True, .Consecutive = resultCreateDebitNOte.Consecutive}
        Catch ex As Exception
            'con.IndigoTransaction.Rollback()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Result = New InterfaceResult With {.Message = ex.Message, .Result = False}
        Finally
            If EjecutaAceptacionEAPBUnicaTransaccion = False Then
                con.sqlWebConection.Close()
            End If
        End Try
        Return Result
    End Function
#End Region

#Region "Proceso de devoluciones de Factura, eliminacion de factura en radicados tanto ERP y genesis, Actualizacion de Cartera ERP"

    Public Function Devolucion(IndigoEmpresa As String, ByVal listRadicated As List(Of GlosaDevolutionsReceptionD), NameContainer As String, intOpcion As String, User As String) As List(Of InterfaceResult) Implements IInterfaceNET.Devolucion
        If IndigoEmpresa = String.Empty Then
            Throw New ArgumentNullException("empresa indigo vacio")
        End If
        If NameContainer = String.Empty Then
            Throw New ArgumentNullException("Empresa ERP vacio")
        End If
        If intOpcion = String.Empty Then
            Throw New ArgumentNullException("intOpcion vacio")
        End If
        If User = String.Empty Then
            Throw New ArgumentNullException("User vacio")
        End If

        Dim ListError As New List(Of InterfaceResult)
        Dim ListInfo As New List(Of InterfaceResult)
        Dim strSql As String = String.Empty
        Try

            'cargamos configuraciones de interface
            If con.sqlWebConection.State = ConnectionState.Closed Then
                con.sqlWebConection.Open()
            End If

            con.InTransaction = True
            con.IndigoTransaction = con.sqlWebConection.BeginTransaction(IsolationLevel.ReadCommitted, "Devoluciones de Facturas")

            Dim NumerodeNotaContable As String = String.Empty
            Dim ConsecutivoNumcon As Integer
            Dim NombreEmpresa As String = String.Empty
            Dim resultUpdate As Boolean


            Dim Tercero As String = String.Empty
            If listRadicated.Count > 0 AndAlso listRadicated(0).GlosaDevolutionsReceptionC IsNot Nothing AndAlso listRadicated(0).GlosaDevolutionsReceptionC.Customer IsNot Nothing Then
                Tercero = listRadicated(0).GlosaDevolutionsReceptionC.Customer.Nit.Trim
            Else
                ListError.Add(New InterfaceResult With {.Message = "Ocurrio un error, no existe se encontro NIT del tercero", .Result = False})
                con.IndigoTransaction.Rollback()
                Return ListError
            End If


            'ELIMINAMOS FACTURAS DE RADICADOS EN DINAMICA
            'GENERACION DE OFICIO EN DINAMICA


            'cosultamos vaalor de la nota 
            strSql = "SELECT top 1 id, CompanyName,DevolutionCodeNoteAccounting FROM " & IndigoEmpresa & ".Glosas.GlosasParametersInterface WHERE ContainerName= '" & NameContainer & "'  "
            Dim dtConfi As DataTable = con.ExecuteCommand_Data(strSql)

            If dtConfi.Rows.Count = 0 Then
                ListError.Add(New InterfaceResult With {.Message = "Devolución Factura: No Existe configuracion de interface para el contenedor" & NameContainer, .Result = False})
                con.IndigoTransaction.Rollback()
                Return ListError
            Else
                NombreEmpresa = dtConfi.Rows(0).Item("CompanyName").ToString
                If dtConfi.Rows(0).Item("DevolutionCodeNoteAccounting").ToString = String.Empty Then
                    ListError.Add(New InterfaceResult With {.Message = "Devolución Factura:no esta configurado el numero de comprobante para devoluciones " & NameContainer, .Result = False})
                    con.IndigoTransaction.Rollback()
                    Return ListError
                Else
                    NumerodeNotaContable = dtConfi.Rows(0).Item("DevolutionCodeNoteAccounting").ToString
                End If
            End If



            Dim ResultEje As Boolean
            Dim FlagContolGenerateAccountNote As Boolean
            Dim NumeroGlosa As String = listRadicated(0).GlosaDevolutionsReceptionC.RadicatedConsecutive  'en este caso el consecutivo de devoluciones de facturas 
            'Tercero = con.fncConcatenar("0", Tercero, 15, ISQL.Direccion.Izquierda)
            Dim Factura As String = String.Empty
            'Dim res As Boolean

            'valida mes actual este abierto 
            'res = Validation.ValidateMonthClose(eTypeInterface.FoxPrivate, Date.Now, NameContainer)
            'If res = False Then
            '    FlagContolGenerateAccountNote = False
            '    ListError.Add(New InterfaceResult With {.Message = "Devolución Factura: El mes " & Month(Date.Now).ToString & " ya esta cerrado, no se puede continuar" & NameContainer, .Result = False})
            'End If

            'valida que la tabla de moviminetos contables por año y mes  (MM201404)  Exista
            'res = Validation.ValidateTableMov(Date.Now, NameContainer)
            'If res = False Then
            '    FlagContolGenerateAccountNote = False
            '    ListError.Add(New InterfaceResult With {.Message = "Devolución Factura:Tabla movimiento mes no existe, no se ha realizado cierre de mes" & NameContainer, .Result = False})
            'End If


            'Busco el consecutivo de comprobantes
            strSql = "SELECT top 1  OID,GENCONSEC, TCCODIGO FROM " & NameContainer & "..CTNTIPCOM WHERE TCCODIGO='" & NumerodeNotaContable & "'"
            Dim OIDConsecutivoTipoDocumento As Integer
            Dim dtConsecutivoComprobante As DataTable = con.ExecuteCommand_Data(strSql)
            If dtConsecutivoComprobante.Rows.Count = 0 Then
                FlagContolGenerateAccountNote = False
                con.IndigoTransaction.Rollback()
                ListError.Add(New InterfaceResult With {.Message = "Devolución Factura:No esta configurado los consecutivo para comprobantes diario de Devolucion de facturas o ha ocurrido un error, favor avisar al administrador del sistema", .Result = False})
                Return ListError
            Else
                OIDConsecutivoTipoDocumento = dtConsecutivoComprobante.Rows(0).Item("OID")
                ConsecutivoNumcon = dtConsecutivoComprobante.Rows(0).Item("GENCONSEC")
                'Actualiza el consecutive del comprobante
                strSql = "UPDATE " & NameContainer & "..geNconsec SET GCONUMERO = GCONUMERO +1  WHERE OID='" & ConsecutivoNumcon & "'"
                Dim resActualizarConsecutivo As Boolean = con.ExecuteCommand(strSql)
                If resActualizarConsecutivo = True Then
                    'consultamo 
                    Dim dtConseIncrementa As DataTable
                    strSql = " SELECT top 1 GCONUMERO FROM " & NameContainer & "..geNconsec WHERE OID='" & ConsecutivoNumcon & "'"
                    dtConseIncrementa = con.ExecuteCommand_Data(strSql)
                    ConsecutivoNumcon = CInt(dtConseIncrementa.Rows(0).Item("GCONUMERO"))
                Else
                    FlagContolGenerateAccountNote = False
                    con.IndigoTransaction.Rollback()
                    ListError.Add(New InterfaceResult With {.Message = "Devolución Factura:ha ocurrido un error actualizando consecutivo, favor avisar al administrador del sistema", .Result = False})
                    Return ListError
                End If
            End If


            Dim InvoiceNotRadicate As String = String.Empty
            'GENERACION DE COMPROBANTES CONTABLE Y ACTUALIZACION DE CUENTA Y ESTADO CARTERA ERP
            For Each itemD As GlosaDevolutionsReceptionD In listRadicated
                FlagContolGenerateAccountNote = True
                Factura = itemD.InvoiceNumber


                'cuenta de cartera PortfoliGlosa
                Dim dtCuentaCartera As DataTable
                Dim OIDcuentaCarteraGlosa As String = String.Empty
                strSql = "select CTNCUENTA  from  " & NameContainer & "..CRNCXC where cxcDocume = '" & itemD.InvoiceNumber & "'"   'factura sin confirmar, traemos cuenta de cartera ERP
                dtCuentaCartera = con.ExecuteCommand_Data(strSql)
                Dim dtCuenta As DataTable
                Dim InvoiceRadicateAccount As String = String.Empty
                If dtCuentaCartera.Rows.Count > 0 Then
                    OIDcuentaCarteraGlosa = dtCuentaCartera.Rows(0).Item("CTNCUENTA").ToString
                    strSql = "SELECT CUECODIGO FROM " & NameContainer & "..CTNCUENTA  WHERE OID = '" & OIDcuentaCarteraGlosa & "'"
                    dtCuenta = con.ExecuteCommand_Data(strSql)
                    If dtCuenta.Rows.Count > 0 Then
                        InvoiceRadicateAccount = CInt(dtCuenta.Rows(0).Item("CUECODIGO"))
                        strSql = "select InvoiceNotRadicate,InvoiceRadicate,RectifiableGlosa,LegalProcess,Conciliation from  " & IndigoEmpresa & ".Glosas.AccountSettingsNET_PrivateMethod WHERE InvoiceRadicate = '" & InvoiceRadicateAccount & "' "
                        Dim dtAccoutConfi As DataTable = con.ExecuteCommand_Data(strSql)
                        If dtAccoutConfi.Rows.Count = 0 Then
                            FlagContolGenerateAccountNote = False
                            con.IndigoTransaction.Rollback()
                            ListError.Add(New InterfaceResult With {.Message = "Devolución Factura: No existe configuraciones contables glosas para metodo privado (NET) en el campo (Factura Radicada) - cuenta " & InvoiceRadicateAccount & ", no se puede continuar!!", .Result = False})
                            Return ListError
                        Else
                            If dtAccoutConfi.Rows(0).Item("InvoiceNotRadicate").ToString = String.Empty Then
                                ListError.Add(New InterfaceResult With {.Message = "Devolución Factura: No existe Cuenta Sin Radicar para la Cuenta Radicada " & InvoiceRadicateAccount & ", no se puede continuar!!", .Result = False})
                                Return ListError
                            Else
                                InvoiceNotRadicate = dtAccoutConfi.Rows(0).Item("InvoiceNotRadicate").ToString
                            End If
                        End If
                    Else
                        FlagContolGenerateAccountNote = False
                        con.IndigoTransaction.Rollback()
                        ListError.Add(New InterfaceResult With {.Message = "Devolución Factura: No se encontro ID de la cuenta" & InvoiceNotRadicate & ", favor avisar al administrador del sistema", .Result = False})
                        Return ListError
                    End If
                Else
                    FlagContolGenerateAccountNote = False
                    con.IndigoTransaction.Rollback()
                    ListError.Add(New InterfaceResult With {.Message = "Devolución Factura: no existe cuenta en cartera ERP para la factura " & Factura & ", no se puede continuar!!", .Result = False})
                    Return ListError
                End If
                dtCuenta = Nothing

                'id de numero de radicado
                Dim dtOIDRadicateOffice As DataTable
                strSql = "select C.OID from " & NameContainer & "..CRNDOCUME DOC INNER JOIN " & NameContainer & "..CRNRADFACC C on C.OID =DOC.OID where DOC.CDCONSEC = '" & itemD.RadicatedNumber & "'"
                dtOIDRadicateOffice = con.ExecuteCommand_Data(strSql)
                Dim OIDRadicate As Integer
                If dtOIDRadicateOffice IsNot Nothing AndAlso dtOIDRadicateOffice.Rows.Count > 0 AndAlso dtOIDRadicateOffice.Rows(0).Item("OID").ToString <> String.Empty Then
                    OIDRadicate = dtOIDRadicateOffice.Rows(0).Item("OID")
                Else
                    FlagContolGenerateAccountNote = False
                    con.IndigoTransaction.Rollback()
                    ListError.Add(New InterfaceResult With {.Message = "Devolución Factura: No se encontro radicado '" & itemD.RadicatedNumber & "'", .Result = False})
                    Return ListError
                End If


                'id de cartera
                Dim dtOIDcartera As DataTable
                Dim OIDcartera As Integer
                strSql = "Select OID from " & NameContainer & "..CRNCXC where cxcDocume = '" & itemD.InvoiceNumber & "'"
                dtOIDcartera = con.ExecuteCommand_Data(strSql)
                If dtOIDcartera IsNot Nothing AndAlso dtOIDcartera.Rows.Count > 0 Then
                    OIDcartera = dtOIDcartera.Rows(0).Item("OID")
                Else
                    FlagContolGenerateAccountNote = False
                    con.IndigoTransaction.Rollback()
                    ListError.Add(New InterfaceResult With {.Message = "Devolución Factura: No se encontro factura '" & itemD.InvoiceNumber & "'", .Result = False})
                    Return ListError
                End If


                strSql = "UPDATE " & NameContainer & "..CRNCXC SET CRNRADFACD = null WHERE OID = " & OIDcartera & ""
                ResultEje = con.ExecuteCommand(strSql)

                'eliminamos factura de radicado en dinamica
                strSql = " DELETE FROM  " & NameContainer & "..CRNRADFACD where CRNRADFACC = " & OIDRadicate & " AND CRNCXC = " & OIDcartera & " "
                ResultEje = con.ExecuteCommand(strSql)


                'cuenta de cartera PortfoliGlosa
                '  Dim dtCuentaCartera As DataTable
                '  Dim cuentaCarteraGlosa As String = String.Empty
                'strSql = "select CTNCUENTA  from  " & NameContainer & "..CRNCXC where cxcDocume =  '" & Factura & "'"   'factura sin confirmar, traemos cuenta de cartera ERP
                'dtCuentaCartera = con.ExecuteCommand_Data(strSql)
                'If dtCuentaCartera.Rows.Count > 0 Then
                '    cuentaCarteraGlosa = dtCuentaCartera.Rows(0).Item("CTNCUENTA").ToString
                'Else
                '    FlagContolGenerateAccountNote = False
                '    ListError.Add(New InterfaceResult With {.Message = "Devolución Factura: no existe cuenta en cartera ERP para la factura " & Factura & ", no se puede continuar!!", .Result = False})
                'End If




                Dim DateActual As Date = Date.Now
                Dim fecha As String
                fecha = Format(DateActual, "yyyyMMdd hh:mm:ss")
                'Creacion del comprobante      Cabecera
                strSql = "INSERT INTO " & NameContainer & "..CTNCOM" & Year(DateActual) & "(COMCODIGO,CTNTIPCOM,COMFECCOM,COMESTADO,COMDETALLE,COMOIDDOCU,COMNUMDOCU,COMOIDTYPE,COMELIMCOMP,COMCIERANU,COMGECGBATCH,OptimisticLockField)" &
                "VALUES( '" & ConsecutivoNumcon & "','" & OIDConsecutivoTipoDocumento & "',convert(varchar(20),'" & fecha & "'),0,'Devolución de Factura  - Fra. " & Factura & "  Glosas','" & OIDConsecutivoTipoDocumento & "','" & Factura & "',0,NULL,0,NULL,0)"
                ResultEje = con.ExecuteCommand(strSql)
                Dim Auto As String
                Dim SQL As New SqlDataAdapter("SELECT @@IDENTITY", con.sqlWebConection)
                If con.sqlWebConection.State = ConnectionState.Closed Then
                    Dim a = 0
                End If
                With SQL
                    .SelectCommand.CommandTimeout = 90
                    .SelectCommand.Transaction = con.IndigoTransaction
                    .SelectCommand.CommandType = CommandType.Text
                    Auto = .SelectCommand.ExecuteScalar.ToString
                End With


                'consultamos el OID del tercero
                Dim OIDtercero As Integer
                strSql = "SELECT  OID FROM " & NameContainer & "..GENTERCER WHERE TERNUMDOC = '" & Tercero & "'"
                Dim dtTercero As DataTable = con.ExecuteCommand_Data(strSql)
                If dtTercero.Rows.Count > 0 Then
                    OIDtercero = CInt(dtTercero.Rows(0).Item("OID"))
                Else
                    FlagContolGenerateAccountNote = False
                    con.IndigoTransaction.Rollback()
                    ListError.Add(New InterfaceResult With {.Message = "Devolución Factura: No se encontro ID del Tercero" & Tercero & ", favor avisar al administrador del sistema", .Result = False})
                    Return ListError
                End If
                dtTercero = Nothing


                Dim OIDInvoiceNotRadicate As String = String.Empty
                strSql = "SELECT OID FROM " & NameContainer & "..CTNCUENTA  WHERE CUECODIGO = '" & InvoiceNotRadicate & "'"
                dtCuenta = con.ExecuteCommand_Data(strSql)
                If dtCuenta.Rows.Count > 0 Then
                    OIDInvoiceNotRadicate = CInt(dtCuenta.Rows(0).Item("OID"))
                Else
                    FlagContolGenerateAccountNote = False
                    con.IndigoTransaction.Rollback()
                    ListError.Add(New InterfaceResult With {.Message = "Devolución Factura: No se encontro ID de la cuenta" & InvoiceNotRadicate & ", favor avisar al administrador del sistema", .Result = False})
                    Return ListError
                End If
                dtCuenta = Nothing

                'Detalle  	--movimiento credito
                strSql = "INSERT INTO " & NameContainer & "..CTNCOMD" & Year(DateActual) & "(CTNCOMCONC,CTNCUENTA,GENTERCER,CTNCENCOS,CMMVALDEB, CMMVALCRE, COMDETALLE, CTNCAURET, CMMESTCON, CMMFECCON,OptimisticLockField) " & _
                "VALUES( '" & Auto & "'," & OIDcuentaCarteraGlosa & ", " & OIDtercero & ",NULL,0,convert(varchar(50)," & itemD.BalanceInvoice & "),'Devolución de Factura - Fra. " & Factura & " Glosas',NULL,0,NULL,0)"
                ResultEje = con.ExecuteCommand(strSql)
                If ResultEje = False Then
                    FlagContolGenerateAccountNote = False
                    con.IndigoTransaction.Rollback()
                    ListError.Add(New InterfaceResult With {.Message = "Devolución Factura: ha ocurrido un error en la nota, creacion del detalle, favor avisar al administrador del sistema", .Result = False})
                    Return ListError
                End If



                'Detalle  	--movimiento debito
                strSql = "INSERT INTO " & NameContainer & "..CTNCOMD" & Year(DateActual) & "(CTNCOMCONC,CTNCUENTA,GENTERCER,CTNCENCOS,CMMVALDEB, CMMVALCRE, COMDETALLE, CTNCAURET, CMMESTCON, CMMFECCON,OptimisticLockField) " & _
                "VALUES( '" & Auto & "'," & OIDInvoiceNotRadicate & ", " & OIDtercero & ",NULL,convert(varchar(50)," & itemD.BalanceInvoice & "),0,'Devolución de Factura - Fra. " & Factura & " Glosas',NULL,0,NULL,0)"
                ResultEje = con.ExecuteCommand(strSql)
                If ResultEje = False Then
                    FlagContolGenerateAccountNote = False
                    con.IndigoTransaction.Rollback()
                    ListError.Add(New InterfaceResult With {.Message = "Devolución Factura:: ha ocurrido un error en la nota, creacion del detalle, favor avisar al administrador del sistema", .Result = False})
                    Return ListError
                End If



                'Una ves realizado el comprobante contable actualizamos la tabla de Cartera ERP para la factura
                strSql = "UPDATE " & NameContainer & "..CRNCXC SET CXCESTCAR = '0', CTNCUENTA = '" & OIDInvoiceNotRadicate & "' WHERE cxcDocume = '" & Factura & "'"   'Confirmado
                resultUpdate = con.ExecuteCommand(strSql)


                '  strSql = "DELETE FROM [" & IndigoEmpresa & "].[Glosas].[RadicateInvoiceD] WHERE  RadicatedNumber = '" & itemD.RadicatedNumber & "' AND invoicenumber= '" & itemD.InvoiceNumber & "' AND state = 2  "
                '  resultUpdate = con.ExecuteCommand(strSql)

                strSql = "UPDATE [" & IndigoEmpresa & "].[Portfolio].[RadicateInvoiceD] SET State = 4 WHERE  RadicatedNumber = '" & itemD.RadicatedNumber & "' AND invoicenumber= '" & itemD.InvoiceNumber & "' AND state = 2  "
                resultUpdate = con.ExecuteCommand(strSql)


                If FlagContolGenerateAccountNote = True Then
                    ListInfo.Add(New InterfaceResult With {.Message = "Se Generaron los siguientes documentos: Comprobante Contable: " & NumerodeNotaContable & " - " & ConsecutivoNumcon & " para la factura: " & Factura & ", Empresa: " & NombreEmpresa, .Result = True, .Consecutive = NumerodeNotaContable & " - " & ConsecutivoNumcon})
                    Validation.AuditInterface(IndigoEmpresa, intOpcion, NumeroGlosa, Factura, 1, ConsecutivoNumcon, ConsecutivoNumcon)
                End If


            Next 'FIN CICLO



            If ListError.Count > 0 Then
                con.IndigoTransaction.Rollback()
                Return ListError
            Else
                con.IndigoTransaction.Commit()
                Return ListInfo
            End If


        Catch ex As Exception
            con.IndigoTransaction.Rollback()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New List(Of InterfaceResult)({New InterfaceResult With {.Message = ex.Message, .Result = False}})
        Finally
            con.sqlWebConection.Close()
        End Try
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: elimine el estado administrado (objetos administrados).
            End If
            con = Nothing
            Validation = Nothing
            IndigoGC.Execute()
        End If
        disposedValue = True
    End Sub

    ' TODO: reemplace Finalize() solo si el anterior Dispose(disposing As Boolean) tiene código para liberar recursos no administrados.
    'Protected Overrides Sub Finalize()
    '    ' No cambie este código. Coloque el código de limpieza en el anterior Dispose(disposing As Boolean).
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' Visual Basic agrega este código para implementar correctamente el patrón descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' No cambie este código. Coloque el código de limpieza en el anterior Dispose(disposing As Boolean).
        Dispose(True)
        ' TODO: quite la marca de comentario de la siguiente línea si Finalize() se ha reemplazado antes.
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
