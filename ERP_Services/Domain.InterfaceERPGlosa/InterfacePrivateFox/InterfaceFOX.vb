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
Imports Domain.Entities
Imports System.Text
Imports Infrastructure.CrossCutting.Exceptions

Public Class InterfaceFOX
    Implements IInterfaceFOX

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
    Public Function RadicateObjection(IndigoEmpresa As String, NumeroGlosa As String, factura As String, Tercero As String, NameContainer As String, ValorFac As Decimal, FechaFactura As Integer, intOpcion As String, User As String) As InterfaceResult Implements IInterfaceFOX.RadicateObjection
        Dim Result As New InterfaceResult
        If IndigoEmpresa = String.Empty Then
            Throw New ArgumentNullException("empresa indigo vacio")
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

            Tercero = con.fncConcatenar("0", Tercero, 15, ISQL.Direccion.Izquierda)

            Dim cuentaCartera As String
            strSql = "SELECT top 1  cpccodcue FROM " & NameContainer & "..crCarter WHERE cemNumFac= '" & factura & "' AND TerCodTer ='" & Tercero & "'"
            Dim dtCuenta As DataTable = con.ExecuteCommand_Data(strSql)
            If dtCuenta.Rows.Count = 0 Then
                Result = New InterfaceResult With {.Message = "No existe la factura en ERP, favor avisar al administrador del sistema", .Result = False}
                Return Result
            Else
                If dtCuenta.Rows(0).Item("cpccodcue").ToString <> String.Empty Then
                    cuentaCartera = dtCuenta.Rows(0).Item("cpccodcue").ToString
                    Dim res As Integer = Validation.ValidateAccount(eTypeInterface.FoxPrivate, NameContainer, cuentaCartera)
                    If res = 0 Then
                        Result = New InterfaceResult With {.Message = "No existe la cuenta " & cuentaCartera & " de la factura en ERP, favor avisar al administrador del sistema", .Result = False}
                        Return Result
                    End If
                Else
                    Result = New InterfaceResult With {.Message = "No existe la factura en ERP, favor avisar al administrador del sistema", .Result = False}
                    Return Result
                End If
            End If


            ' validar el saldo y el plan
            strSql = "SELECT  count(*) FROM " & NameContainer & "..crcarter WHERE cemNumFac= '" & factura & "' AND TerCodTer ='" & Tercero & "' AND cemsalfac > 0"
            Dim resint As Integer = con.ExecuteCommand_Count(strSql)
            If resint = 0 Then
                Result = New InterfaceResult With {.Message = "  La factura " & factura & " no  tiene saldo, no se puede continuar!!", .Result = False}
                Return Result
            End If

            'con.InTransaction = True
            'con.IndigoTransaction = con.sqlWebConection.BeginTransaction(IsolationLevel.ReadCommitted, "Actualizar cuenta cartera glosa")

            'consultar cuenta de radicacion segun tabla de configuracion
            strSql = "select InvoiceNotRadicate,InvoiceRadicate,RectifiableGlosa,LegalProcess,Conciliation from  " & IndigoEmpresa & ".Glosas.AccountSettingsFOX_PrivateMethod WHERE InvoiceRadicate = '" & cuentaCartera & "' "
            Dim dtConcept As DataTable = con.ExecuteCommand_Data(strSql)
            Dim concepto As String
            If dtConcept.Rows.Count = 0 Then
                Result = New InterfaceResult With {.Message = " No existe configuraciones contables glosas para metodo privado (Fox) en el campo (Factura Radicada) - cuenta: " & cuentaCartera & ", no se puede continuar!!", .Result = False}
                Return Result
            Else
                concepto = dtConcept.Rows(0).Item("RectifiableGlosa").ToString   'concepto de radicacion nota credito
            End If
            'consultamos el codigo del concepto de la cuenta 
            strSql = "SELECT cpccodcue   FROM " & NameContainer & "..crConcep WHERE CNOCODCON= '" & concepto & "'"
            Dim dtCuentaConcepto As DataTable = con.ExecuteCommand_Data(strSql)
            Dim CuentaConcepto As String
            If dtCuentaConcepto.Rows.Count = 0 Then
                Result = New InterfaceResult With {.Message = " no existe cuenta para el concepto " & concepto & " , no se puede continuar!!", .Result = False}
                Return Result
            Else
                CuentaConcepto = dtCuentaConcepto.Rows(0).Item("cpccodcue").ToString.Trim
                'If CuentaConcepto <> String.Empty Then
                '    Dim resultUpdate As Boolean
                '    'actualizamos cuenta de cartera glosa 
                '    strSql = "UPDATE " & IndigoEmpresa & ".Glosas.GlosaPortfolioGlosada SET AccountantAccountCustomers = '" & CuentaConcepto & "' WHERE InvoiceNumber = '" & factura & "'  "
                '    resultUpdate = con.ExecuteCommand(strSql)
                'End If
            End If


            'validamos la cuenta maneje ce y  nivel 5
            strSql = "SELECT count(*) FROM " & NameContainer & "..ctPlaCue WHERE cpcCodCue='" & CuentaConcepto & "' and cpcmancen=1 and cpctipcue = '5'"
            resint = con.ExecuteCommand_Count(strSql)
            If resint = 0 Then
                Result = New InterfaceResult With {.Message = "La cuenta no existe, o no maneja centros de costo'!!", .Result = False}
                Return Result
            End If


            Dim resultCreateCreditNOte As InterfaceResult = Me.CreateCreditNote(NameContainer, Tercero, factura, ValorFac, User, cuentaCartera, concepto, CuentaConcepto)


            Validation.AuditInterface(IndigoEmpresa, intOpcion, NumeroGlosa, factura, 2, concepto, resultCreateCreditNOte.Consecutive)

            Result = New InterfaceResult With {.Message = "Se Generaron los siguientes documentos: Nota Credito: " & resultCreateCreditNOte.Consecutive & ", Empresa: " & NombreEmpresa, .Result = True, .Consecutive = resultCreateCreditNOte.Consecutive, .Account = CuentaConcepto}
        Catch ex As Exception
            con.IndigoTransaction.Rollback()
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
    Public Function CreateCreditNote(NameContainer As String, Tercero As String, factura As String, ValorFac As Decimal, User As String, Cuenta As String, Concepto As String, CuentaConcepto As String) As InterfaceResult Implements IInterfaceFOX.CreateCreditNote
        Dim Result As New InterfaceResult
        Dim strSql As String = String.Empty
        Try

            If con.sqlWebConection.State = ConnectionState.Closed Then
                con.sqlWebConection.Open()
            End If

            con.InTransaction = True
            con.IndigoTransaction = con.sqlWebConection.BeginTransaction(IsolationLevel.ReadCommitted, "Interfax crear Nota Credito")

            'Dim resultUpdate As Boolean
            'strSql = "UPDATE GENESIS01.Glosas.GlosaPortfolioGlosada SET AccountantAccountCustomers = '" & CuentaConcepto & "' WHERE InvoiceNumber = '" & factura & "'  "
            'resultUpdate = con.ExecuteCommand(strSql)

            Dim ResultEje As Boolean
            Dim ConsecutiveNumber As String
            strSql = "UPDATE " & NameContainer & "..geconsec SET gconumero=gconumero+1,gcoestado='U' WHERE gcocodigo = '0502'"
            ResultEje = con.ExecuteCommand(strSql)

            strSql = "SELECT  gconumero FROM " & NameContainer & "..geconsec WHERE gcocodigo = '0502'"
            Dim dtNumConsecutive As DataTable = con.ExecuteCommand_Data(strSql)

            ConsecutiveNumber = dtNumConsecutive.Rows(0).Item("gconumero").ToString
            ConsecutiveNumber = con.fncConcatenar("0", ConsecutiveNumber, 10, ISQL.Direccion.Izquierda)
            Dim DateActual As Date = Date.Now
            Dim fecha As String
            fecha = Format(DateActual, "yyyyMMdd hh:mm:ss")
            Dim TerceroNumeros As Integer = Val(Tercero)


            ''cuenta de crcarter, cabecera
            strSql = "INSERT INTO " & NameContainer & "..crCNotas (CCNTIPNOT,CCNNUMNOT,tercodter,GCLCODIGO,ccnfechno,cemnumfac,gvecodigo,ccnvalnot,cpccodcue,ccccodcen,ccnestado,ccndetall,ccnusucre," & _
            "ccnfeccre,ccnusuanu,ccnfecanu,ccnusucon,ccnfeccon,ccncomdia,ccnnumcom,ccnrespon,ccndetpat,ccnrespo2,ccnrespo3,ccnvalres,ccnvalre2,ccnvalre3,ACACODIGO)VALUES( " & _
             " 'C'," & _
            "'" & ConsecutiveNumber & "'," & _
            "'" & Tercero & "','" & TerceroNumeros & "', convert(varchar(20),'" & (fecha) & "'),'" & factura & "','001',convert(varchar(50)," & ValorFac & "),'" & Cuenta & "','','','Radicacion Glosa Subsanable - Fra. " & factura & " Mod. Glosas'," & _
            "'" & User & "', convert(varchar(20),'" & (fecha) & "'),'',null,'',null,'','','',1,'','',0,0,0,'')"
            ResultEje = con.ExecuteCommand(strSql)

            'detalle 
            strSql = "INSERT INTO  " & NameContainer & "..crMNotas(CCNTIPNOT,CCNNUMNOT,cmnnumdet,cnocodcon,tercodter,cpccodcue,ccccodcen,cmnnatura,cmnvrconc,ccrcodcon,ccnnumfac,cmnvalbas,cmnporret," & _
            "CMNVALFAC, sipcodigo,gmecodigo,SLHCODIGO)VALUES( " & _
            "'C','" & ConsecutiveNumber & "', '01','" & Concepto & "','" & Tercero & "','" & CuentaConcepto & "','','D',convert(varchar(50)," & ValorFac & "),'','" & factura & "',0,0,0,'','','')"
            ResultEje = con.ExecuteCommand(strSql)

            'credito
            strSql = "INSERT INTO " & NameContainer & "..crfacNot(CCNTIPNOT,CCNNUMNOT,cemnumfac,CCFVALAJU)VALUES(" & _
            "'C','" & ConsecutiveNumber & "','" & factura & "',convert(varchar(50)," & ValorFac & "))"
            ResultEje = con.ExecuteCommand(strSql)

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
    Public Function AcceptanceEAPB(IndigoEmpresa As String, NumeroGlosa As String, factura As String, Tercero As String, NameContainer As String, ValorFac As Decimal, FechaFactura As Integer, intOpcion As String, User As String, Optional ByVal EjecutaAceptacionEAPBUnicaTransaccion As Boolean = False) As InterfaceResult Implements IInterfaceFOX.AcceptanceEAPB
        Dim Result As New InterfaceResult
        If IndigoEmpresa = String.Empty Then
            Throw New ArgumentNullException("empresa indigo vacio")
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
                Result = New InterfaceResult With {.Message = "Aceptacion EAPB: No Existe configuracion de interface para el contenedor" & NameContainer, .Result = False}
                Return Result
            Else
                NombreEmpresa = dtConfi.Rows(0).Item("CompanyName").ToString
            End If

            'cargamos cuenta apartir de tercero y factura, Validamos Existencia de Cuenta

            Tercero = con.fncConcatenar("0", Tercero, 15, ISQL.Direccion.Izquierda)

            Dim cuentaCartera As String
            strSql = "SELECT top 1  cpccodcue FROM " & NameContainer & "..crCarter WHERE cemNumFac= '" & factura & "' AND TerCodTer ='" & Tercero & "'"
            Dim dtCuenta As DataTable = con.ExecuteCommand_Data(strSql)
            If dtCuenta.Rows.Count = 0 Then
                Result = New InterfaceResult With {.Message = "Aceptacion EAPB: No existe la factura en ERP, favor avisar al administrador del sistema", .Result = False}
                Return Result
            Else
                If dtCuenta.Rows(0).Item("cpccodcue").ToString <> String.Empty Then
                    cuentaCartera = dtCuenta.Rows(0).Item("cpccodcue").ToString
                    Dim res As Integer = Validation.ValidateAccount(eTypeInterface.FoxPrivate, NameContainer, cuentaCartera)
                    If res = 0 Then
                        Result = New InterfaceResult With {.Message = "Aceptacion EAPB: No existe la cuenta " & cuentaCartera & " de la factura en DGH, favor avisar al administrador del sistema", .Result = False}
                        Return Result
                    End If
                Else
                    Result = New InterfaceResult With {.Message = "Aceptacion EAPB: No existe la factura en ERP, favor avisar al administrador del sistema", .Result = False}
                    Return Result
                End If
            End If


            ' validar el saldo y el plan  
            'strSql = "SELECT  count(*) FROM " & NameContainer & "..crcarter WHERE cemNumFac= '" & factura & "' AND TerCodTer ='" & Tercero & "' AND cemsalfac > 0"
            'Dim resint As Integer = con.ExecuteCommand_Count(strSql)
            'If resint = 0 Then
            '    Result = New InterfaceResult With {.Message = "  La factura " & factura & " no  tiene saldo, no se puede continuar!!", .Result = False}
            '    Return Result
            'End If



            'consultar cuenta de radicacion segun tabla de configuracion
            strSql = "select InvoiceNotRadicate,InvoiceRadicate,RectifiableGlosa,LegalProcess,Conciliation from  " & IndigoEmpresa & ".Glosas.AccountSettingsFOX_PrivateMethod WHERE InvoiceRadicate = '" & cuentaCartera & "' "
            Dim dtConcept As DataTable = con.ExecuteCommand_Data(strSql)
            Dim concepto As String
            If dtConcept.Rows.Count = 0 Then
                Result = New InterfaceResult With {.Message = "Aceptacion EAPB: No existe configuraciones contables glosas para metodo privado (Fox) en el campo (Factura Radicada) - cuenta: " & cuentaCartera & " , no se puede continuar!!", .Result = False}
                Return Result
            Else
                concepto = dtConcept.Rows(0).Item("Conciliation").ToString   'concepto de radicacion nota debito Conciliation
            End If
            'consultamos el codigo del concepto de la cuenta 
            strSql = "SELECT cpccodcue   FROM " & NameContainer & "..crConcep WHERE CNOCODCON= '" & concepto & "'"
            Dim dtCuentaConcepto As DataTable = con.ExecuteCommand_Data(strSql)
            Dim CuentaConcepto As String
            If dtCuentaConcepto.Rows.Count = 0 Then
                Result = New InterfaceResult With {.Message = "Aceptacion EAPB: no existe cuenta para el concepto " & concepto & " , no se puede continuar!!", .Result = False}
                Return Result
            Else
                CuentaConcepto = dtCuentaConcepto.Rows(0).Item("cpccodcue").ToString
            End If




            'validamos la cuenta maneje ce y  nivel 5
            Dim resint As Integer
            strSql = "SELECT count(*) FROM " & NameContainer & "..ctPlaCue WHERE cpcCodCue='" & CuentaConcepto & "' and cpcmancen=1 and cpctipcue = '5'"
            resint = con.ExecuteCommand_Count(strSql)
            If resint = 0 Then
                Result = New InterfaceResult With {.Message = "Aceptacion EAPB: La cuenta " & CuentaConcepto & " del concepto " & concepto & " no existe, o no maneja centros de costo'!!", .Result = False}
                Return Result
            End If


            Dim resultCreateDebitNOte As InterfaceResult = Me.CreateDebitNote(NameContainer, Tercero, factura, ValorFac, User, cuentaCartera, concepto, CuentaConcepto, False, EjecutaAceptacionEAPBUnicaTransaccion)


            Validation.AuditInterface(IndigoEmpresa, intOpcion, NumeroGlosa, factura, 3, concepto, resultCreateDebitNOte.Consecutive)

            Result = New InterfaceResult With {.Message = "Se Generaron los siguientes documentos: Nota Debito: " & resultCreateDebitNOte.Consecutive & ", Empresa: " & NombreEmpresa, .Result = True, .Consecutive = resultCreateDebitNOte.Consecutive}
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

    Public Function CreateDebitNote(NameContainer As String, Tercero As String, factura As String, ValorFac As Decimal, User As String, Cuenta As String, Concepto As String, CuentaConcepto As String, ByVal Reiterated As Boolean, Optional ByVal EjecutaAceptacionEAPBUnicaTransaccion As Boolean = False) As InterfaceResult Implements IInterfaceFOX.CreateDebitNote
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


            Dim ResultEje As Boolean
            Dim ConsecutiveNumber As String
            strSql = "UPDATE " & NameContainer & "..geconsec SET gconumero=gconumero+1,gcoestado='U' WHERE gcocodigo = '0501'"
            ResultEje = con.ExecuteCommand(strSql)

            strSql = "SELECT  gconumero FROM " & NameContainer & "..geconsec WHERE gcocodigo = '0501'"
            Dim dtNumConsecutive As DataTable = con.ExecuteCommand_Data(strSql)

            ConsecutiveNumber = dtNumConsecutive.Rows(0).Item("gconumero").ToString
            ConsecutiveNumber = con.fncConcatenar("0", ConsecutiveNumber, 10, ISQL.Direccion.Izquierda)
            Dim DateActual As Date = Date.Now
            Dim fecha As String
            fecha = Format(DateActual, "yyyyMMdd hh:mm:ss")
            Dim TerceroNumeros As Integer = Val(Tercero)

            Dim ObservacionDetalle As String = String.Empty
            If Reiterated = True Then
                ObservacionDetalle = "Aceptacion EAPB por diferencia en reiteracion - Fra. " & factura & " Mod. Glosas"
            Else
                ObservacionDetalle = "Aceptacion EAPB Glosa - Fra. " & factura & " Mod. Glosas2"
            End If
            ''cuenta de crcarter, cabecera
            strSql = "INSERT INTO " & NameContainer & "..crCNotas (CCNTIPNOT,CCNNUMNOT,tercodter,GCLCODIGO,ccnfechno,cemnumfac,gvecodigo,ccnvalnot,cpccodcue,ccccodcen,ccnestado,ccndetall,ccnusucre," & _
            "ccnfeccre,ccnusuanu,ccnfecanu,ccnusucon,ccnfeccon,ccncomdia,ccnnumcom,ccnrespon,ccndetpat,ccnrespo2,ccnrespo3,ccnvalres,ccnvalre2,ccnvalre3,ACACODIGO)VALUES( " & _
             "'D'," & _
            "'" & ConsecutiveNumber & "'," & _
            "'" & Tercero & "','" & TerceroNumeros & "', convert(varchar(20),'" & (fecha) & "'),'" & factura & "','001',convert(varchar(50)," & ValorFac & "),'" & Cuenta & "','','','" & ObservacionDetalle & "'," & _
            "'" & User & "', convert(varchar(20),'" & (fecha) & "'),'',null,'',null,'','','',1,'','',0,0,0,'')"
            ResultEje = con.ExecuteCommand(strSql)

            'detalle 
            strSql = "INSERT INTO  " & NameContainer & "..crMNotas(CCNTIPNOT,CCNNUMNOT,cmnnumdet,cnocodcon,tercodter,cpccodcue,ccccodcen,cmnnatura,cmnvrconc,ccrcodcon,ccnnumfac,cmnvalbas,cmnporret," & _
            "CMNVALFAC, sipcodigo,gmecodigo,SLHCODIGO)VALUES( " & _
            "'D','" & ConsecutiveNumber & "', '01','" & Concepto & "','" & Tercero & "','" & CuentaConcepto & "','','C',convert(varchar(50)," & ValorFac & "),'','" & factura & "',0,0,0,'','','')"
            ResultEje = con.ExecuteCommand(strSql)

            'credito
            strSql = "INSERT INTO " & NameContainer & "..crfacNot(CCNTIPNOT,CCNNUMNOT,cemnumfac,CCFVALAJU)VALUES(" & _
            "'D','" & ConsecutiveNumber & "','" & factura & "',convert(varchar(50)," & ValorFac & "))"
            ResultEje = con.ExecuteCommand(strSql)

            If EjecutaAceptacionEAPBUnicaTransaccion = False Then
                con.IndigoTransaction.Commit()
            End If


            Result = New InterfaceResult With {.Message = "Aceptacion EAPB: OK", .Result = True, .Consecutive = ConsecutiveNumber}
        Catch ex As Exception
            con.IndigoTransaction.Rollback()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Result = New InterfaceResult With {.Message = "Aceptacion EAPB: " & ex.Message, .Result = False}
        Finally
            If EjecutaAceptacionEAPBUnicaTransaccion = False Then
                con.sqlWebConection.Close()
            End If
        End Try
        Return Result
    End Function
#End Region

#Region "Proceso Aceptacion IPS"
    Public Function AcceptanceIPS(IndigoEmpresa As String, NumeroGlosa As String, factura As String, Tercero As String, NameContainer As String, ValorFac As Decimal, FechaFactura As Integer, intOpcion As String, User As String, AfectaServicio As Boolean, Modulo As String, Optional BanderaEjecutaAceptacionEAPB As Boolean = False, Optional VAlorAceptadoEAPB As Decimal = 0) As InterfaceResult Implements IInterfaceFOX.AcceptanceIPS
        Dim Result As New InterfaceResult
        If IndigoEmpresa = String.Empty Then
            Throw New ArgumentNullException("empresa indigo vacio")
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

            Dim DateActual As Date = Date.Now
            strSql = "SELECT top 1 id, CompanyName,AccountantAccountGeneralAcceptanceC,AccountantAccountPreviousAcceptanceC FROM " & IndigoEmpresa & ".Glosas.GlosasParametersInterface WHERE ContainerName= '" & NameContainer & "'  "
            Dim dtConfi As DataTable = con.ExecuteCommand_Data(strSql)
            Dim CuentaGeneralAceptacionActual As String
            Dim CuentaVigenciaAnteriores As String
            Dim NombreEmpresa As String
            If dtConfi.Rows.Count = 0 Then
                Result = New InterfaceResult With {.Message = "Aceptacion IPS: No Existe configuracion de interface para el contenedor" & NameContainer, .Result = False}
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

            Tercero = con.fncConcatenar("0", Tercero, 15, ISQL.Direccion.Izquierda)
            'valida mes actual este abierto 
            Dim res As Boolean = Validation.ValidateMonthClose(eTypeInterface.FoxPrivate, Date.Now, NameContainer)
            If res = False Then
                Result = New InterfaceResult With {.Message = "Aceptacion IPS: El mes " & Month(Date.Now).ToString & " ya esta cerrado, no se puede continuar" & NameContainer, .Result = False}
                Return Result
            End If


            'valida que la tabla de moviminetos contables por año y mes  (MM201404)  Exista
            res = Validation.ValidateTableMov(Date.Now, NameContainer)
            If res = False Then
                Result = New InterfaceResult With {.Message = "Aceptacion IPS:Tabla movimiento mes no existe, no se ha realizado cierre de mes" & NameContainer, .Result = False}
                Return Result
            End If

            'Busco el consecutivo de comprobantes
            Dim ConsecutivoComprobante As String
            strSql = "SELECT top 1  cpscomcog FROM " & NameContainer & "..crParSiS"
            Dim dtConsecutivoComprobante As DataTable = con.ExecuteCommand_Data(strSql)
            If dtConsecutivoComprobante.Rows.Count = 0 Then
                Result = New InterfaceResult With {.Message = "Aceptacion IPS:No esta configurado los consecutivo para comprobantes o ha ocurrido un error, favor avisar al administrador del sistema", .Result = False}
                Return Result
            Else
                ConsecutivoComprobante = dtConsecutivoComprobante.Rows(0).Item("cpscomcog").ToString.TrimEnd
            End If

            'busco consecutivo comprobante diario mediante el consecutivo de comprobantes
            Dim ConsecutivoNumcon As String
            strSql = "SELECT top 1  ccdnumcom FROM " & NameContainer & "..ctcomdia WHERE ccdcodcom='" & ConsecutivoComprobante & "'"
            Dim dtConsecutivoNumcon As DataTable = con.ExecuteCommand_Data(strSql)
            If dtConsecutivoNumcon.Rows.Count = 0 Then
                Result = New InterfaceResult With {.Message = "Aceptacion IPS:No esta configurado los consecutivo para comprobantes diario o ha ocurrido un error, favor avisar al administrador del sistema", .Result = False}
                Return Result
            Else
                ConsecutivoNumcon = dtConsecutivoNumcon.Rows(0).Item("ccdnumcom").ToString.TrimEnd
                'Actualiza el consecutive del comprobante
                strSql = "UPDATE " & NameContainer & "..ctcomdia SET ccdnumcom = ccdnumcom +1  WHERE ccdcodcom='" & ConsecutivoComprobante & "'"
                Dim resActualizarConsecutivo As Boolean = con.ExecuteCommand(strSql)
                If resActualizarConsecutivo = True Then
                    ConsecutivoNumcon = ConsecutivoNumcon + 1
                Else
                    Result = New InterfaceResult With {.Message = "Aceptacion IPS:ha ocurrido un error actualizando consecutivo, favor avisar al administrador del sistema", .Result = False}
                    Return Result
                End If
            End If


            Dim fecha As String
            fecha = Format(DateActual, "yyyyMMdd hh:mm:ss")
            Dim ResultEje As Boolean
            ConsecutivoNumcon = con.fncConcatenar("0", ConsecutivoNumcon, 10, ISQL.Direccion.Izquierda)
            'Creacion del comprobante      Cabecera
            strSql = "INSERT INTO " & NameContainer & "..ctconmov(ccdcodcom,ccmnumcom,ccmfeccom,ccmasunto,ccmnumreg,ccmestado,ccmdocume)" &
            "VALUES( '" & ConsecutivoComprobante & "','" & ConsecutivoNumcon & "',convert(varchar(20),'" & fecha & "'),'Aceptacion Glosa - Fra. " & factura & " Mod. Glosas',0,'','" & factura & "' )"
            ResultEje = con.ExecuteCommand(strSql)


            'cuenta de cartera PortfoliGlosa
            Dim dtCuentaCartera As DataTable
            Dim cuentaCarteraGlosa As String = String.Empty
            strSql = "SELECT  AccountantAccountCustomers from " & IndigoEmpresa & ".Glosas.GlosaPortfolioGlosada where InvoiceNumber = '" & factura & "'"  'Cartera genesis
            dtCuentaCartera = con.ExecuteCommand_Data(strSql)
            If dtCuentaCartera.Rows.Count > 0 Then
                cuentaCarteraGlosa = dtCuentaCartera.Rows(0).Item("AccountantAccountCustomers").ToString
            End If


            'Detalle  Acredito la cuenta de GlosaPortfolioGlosada
            Dim Contador As Integer = 1
            Dim MesConcatenado As String = Month(DateActual).ToString
            MesConcatenado = con.fncConcatenar("0", MesConcatenado, 2, ISQL.Direccion.Izquierda)
            Dim NombreTabla As String = NameContainer & "..MM" & Year(DateActual) & MesConcatenado
            strSql = "INSERT INTO " & NombreTabla & " (ccdcodcom,ccmnumcom,cpccodcue,tercodter,ccccodcen,ccmfeccom,cmmvaldeb,cmmvalcre,cmmdetmov,cmmcontem,cmmfeccon,ccrcodcon,cmmporret,cmmvalbas,cmmvalfac,cmmnumreg) " & _
            "VALUES( '" & ConsecutivoComprobante & "','" & ConsecutivoNumcon & "', '" & cuentaCarteraGlosa & "', '" & Tercero & "','',convert(varchar(20),'" & fecha & "'),0, " & _
            "convert(varchar(50)," & ValorFac & "),'Aceptacion Glosa - Fra. " & factura & " Mod. Glosas','',null,'',0,0,0,'" & Contador & "')"
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

                ''averiguo si la factura registro movimientos por detalle o por detalle qx
                'strSql = "select  count(*) from " & IndigoEmpresa & ".Glosas.GlosaInvoiceDetail d inner join " & IndigoEmpresa & ".Glosas.GlosaInvoiceDetailQX dqx on d.id = dqx.InvoiceDetailId inner join " & IndigoEmpresa & ".Glosas.GlosaMovementGlosa m on m.InvoiceDetailIdQX = dqx.id  where d.invoicenumber = '" & factura & "'"
                'Dim cantQx As Integer = con.ExecuteCommand_Count(strSql)
                'Dim BanderaQx As Boolean
                'If cantQx > 0 Then
                '    BanderaQx = True
                'Else
                '    BanderaQx = False
                'End If


                'If BanderaQx = True Then
                '    strSql = "select  sum(" & campoTmp & ") as valor, dqx.AccountantAccountIncome as cuenta, dqx.CostCenterCode from " & IndigoEmpresa & ".Glosas.GlosaMovementGlosa m inner join " & _
                '    " " & IndigoEmpresa & ".Glosas.GlosaInvoiceDetailQX dqx on dqx.Id = m.InvoiceDetailIdQX " & _
                '    "where m.InvoiceNumber = '" & factura & "'  and " & campoTmp & " > 0 " & _
                '    "group by  dqx.AccountantAccountIncome,dqx.CostCenterCode"
                'Else
                '    strSql = "select sum(" & campoTmp & ") as valor, d.AccountantAccountIncome as cuenta, d.CostCenterCode from " & IndigoEmpresa & ".Glosas.GlosaMovementGlosa m inner join " & _
                '     " " & IndigoEmpresa & ".Glosas.GlosaInvoiceDetail d on d.Id = m.InvoiceDetailId " & _
                '    "	where m.InvoiceNumber = '" & factura & "' and " & campoTmp & " > 0 " & _
                '    "	group by  d.AccountantAccountIncome, d.CostCenterCode"
                'End If





                strSql = "select  sum(" & campoTmp & ") as valor, dqx.AccountantAccountIncome as cuenta, dqx.CostCenterCode from " & IndigoEmpresa & ".Glosas.GlosaMovementGlosa m inner join " & _
                " " & IndigoEmpresa & ".Glosas.GlosaInvoiceDetailQX dqx on dqx.Id = m.InvoiceDetailIdQX " & _
                "where m.InvoiceNumber = '" & factura & "'  and " & campoTmp & " > 0 AND m.state <> 6 " & _
                "group by  dqx.AccountantAccountIncome,dqx.CostCenterCode" & _
                " UNION " & _
                " select sum(" & campoTmp & ") as valor, d.AccountantAccountIncome as cuenta, d.CostCenterCode from " & IndigoEmpresa & ".Glosas.GlosaMovementGlosa m inner join " & _
                " " & IndigoEmpresa & ".Glosas.GlosaInvoiceDetail d on d.Id = m.InvoiceDetailId " & _
                "	where m.InvoiceNumber = '" & factura & "' and " & campoTmp & " > 0  and m.InvoiceDetailIdQX is null AND m.state <> 6 " & _
                "	group by  d.AccountantAccountIncome, d.CostCenterCode"


                '----------------------------------------------------------------------------------------------------------------------
                'agrupamos por cuenta contable de parametros. aceptaciones generales y cc detalle de factura
                '----------------------------------------------------------------------------------------------------------------------
            ElseIf FechaFactura = Year(DateActual) And AfectaServicio = False Then

                'averiguo si la factura registro movimientos por detalle o por detalle qx
                'strSql = "select  count(*) from " & IndigoEmpresa & ".Glosas.GlosaInvoiceDetail d inner join " & IndigoEmpresa & ".Glosas.GlosaInvoiceDetailQX dqx on d.id = dqx.InvoiceDetailId inner join " & IndigoEmpresa & ".Glosas.GlosaMovementGlosa m on m.InvoiceDetailIdQX = dqx.id  where d.invoicenumber = '" & factura & "'"
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
                       "inner join " & IndigoEmpresa & ".Glosas.GlosaInvoiceDetail d on d.Id = m.InvoiceDetailId " & _
                       "inner join " & IndigoEmpresa & ".Glosas.GlosaObjectionsReceptionD ObjD on d.ObjectionsReceptionDId = ObjD.Id " & _
                       "inner join " & IndigoEmpresa & ".Glosas.GlosasParametersInterface P on P.id = ObjD.GlosasParametersInterfaceId  " & _
                       "where m.InvoiceNumber = '" & factura & "' and " & campoTmp & " > 0 and m.InvoiceDetailIdQX is null AND m.state <> 6 " & _
                       "group by  p.AccountantAccountGeneralAcceptanceC, d.CostCenterCode "



                '----------------------------------------------------------------------------------------------------------------------
                'vigencia anteriore: agrupamos por la cuenta contable de vigencia anteriores y cc
                '----------------------------------------------------------------------------------------------------------------------
            Else

                ''averiguo si la factura registro movimientos por detalle o por detalle qx
                'strSql = "select  count(*) from " & IndigoEmpresa & ".Glosas.GlosaInvoiceDetail d inner join " & IndigoEmpresa & ".Glosas.GlosaInvoiceDetailQX dqx on d.id = dqx.InvoiceDetailId inner join " & IndigoEmpresa & ".Glosas.GlosaMovementGlosa m on m.InvoiceDetailIdQX = dqx.id where d.invoicenumber = '" & factura & "'"
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
                  "select sum(" & campoTmp & ") as valor, p.AccountantAccountPreviousAcceptanceC as cuenta, d.CostCenterCode from " & IndigoEmpresa & ".Glosas.GlosaMovementGlosa m " & _
                       "inner join " & IndigoEmpresa & ".Glosas.GlosaInvoiceDetail d on d.Id = m.InvoiceDetailId " & _
                       "inner join " & IndigoEmpresa & ".Glosas.GlosaObjectionsReceptionD ObjD on d.ObjectionsReceptionDId = ObjD.Id " & _
                       "inner join " & IndigoEmpresa & ".Glosas.GlosasParametersInterface P on P.id = ObjD.GlosasParametersInterfaceId  " & _
                       "where m.InvoiceNumber = '" & factura & "' and " & campoTmp & " > 0 and m.InvoiceDetailIdQX is null AND m.state <> 6 " & _
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
                Contador = Contador + 1
                cuentaDebito = item.Item("cuenta").ToString

                Dim centroCosto As String = item.Item("CostCenterCode").ToString
                Dim ValorServicio As String = item.Item("valor")
                'Detalle
                strSql = "INSERT INTO " & NombreTabla & " (ccdcodcom,ccmnumcom,cpccodcue,tercodter,ccccodcen,ccmfeccom,cmmvaldeb,cmmvalcre,cmmdetmov,cmmcontem,cmmfeccon,ccrcodcon,cmmporret,cmmvalbas,cmmvalfac,cmmnumreg) " & _
                "VALUES( '" & ConsecutivoComprobante & "','" & ConsecutivoNumcon & "', '" & cuentaDebito & "', '" & Tercero & "','" & centroCosto & "',convert(varchar(20),'" & fecha & "')," & _
                "convert(varchar(50)," & ValorServicio & "),0,'Aceptacion Glosa - Fra. " & factura & " Mod. Glosas','',null,'',0,0,0,'" & Contador & "')"
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

            Validation.AuditInterface(IndigoEmpresa, intOpcion, NumeroGlosa, factura, 1, ConsecutivoComprobante, ConsecutivoNumcon)

            If BanderaEjecutaAceptacionEAPB = True Then
                Result = New InterfaceResult With {.Message = "Se Generaron los siguientes documentos: Comprobante Contable: " & ConsecutivoComprobante & " - " & ConsecutivoNumcon & " Nota Debito: " & UnicaTransaccionResultado.Consecutive & " , Empresa: " & NombreEmpresa, .Result = True, .Consecutive = ConsecutivoComprobante & " - " & ConsecutivoNumcon}
            Else
                Result = New InterfaceResult With {.Message = "Se Generaron los siguientes documentos: Comprobante Contable: " & ConsecutivoComprobante & " - " & ConsecutivoNumcon & ", Empresa: " & NombreEmpresa, .Result = True, .Consecutive = ConsecutivoComprobante & " - " & ConsecutivoNumcon}
            End If

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

#Region "Proceso de Radicacion de Factura y Actualizacion de Cartera ERP"

    Public Function RadicateInvoice(RadicateInvoiceC As RadicateInvoiceC, ByVal listRadicated As List(Of RadicateInvoiceD), IndigoEmpresa As String, NameContainer As String, intOpcion As String, User As String, Comment As String) As List(Of InterfaceResult) Implements IInterfaceFOX.RadicateInvoice

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
            con.IndigoTransaction = con.sqlWebConection.BeginTransaction(IsolationLevel.ReadCommitted, "Nota Contable Rad.deFactura")

            Dim NumerodeNotaContable As String = String.Empty
            Dim ConsecutivoNumcon As String = String.Empty
            Dim NombreEmpresa As String = String.Empty
            Dim Tercero As String = String.Empty
            Dim ConsecutiveNumberRadicate As String
            Dim DateConfirmationsystem As Date = RadicateInvoiceC.ConfirmDateSystem
            Dim DateConfirmation As Date = RadicateInvoiceC.ConfirmDate


            If RadicateInvoiceC IsNot Nothing AndAlso RadicateInvoiceC.Customer IsNot Nothing Then
                Tercero = RadicateInvoiceC.Customer.Nit.Trim
            Else
                con.IndigoTransaction.Rollback()
                ListError.Add(New InterfaceResult With {.Message = "Ocurrio un error, no existe NIT del tercero", .Result = False})
                Return ListError
            End If


            'GENERACION DE OFICIO EN DINAMICA
            'GENERACION DE OFICIO EN DINAMICA
            ConsecutiveNumberRadicate = con.fncConcatenar("0", RadicateInvoiceC.RadicatedConsecutive, 10, ISQL.Direccion.Izquierda)
            strSql = "select count(*)  from  " & NameContainer & "..[CRCRACTS] where [CCRNUMRAD] = '" & ConsecutiveNumberRadicate & "'"
            Dim intCount As Integer = con.ExecuteCommand_Count(strSql)
            If intCount > 0 Then
                ListError.Add(New InterfaceResult With {.Message = "ya existe un número de radicado con el consecutivo: " & ConsecutiveNumberRadicate & ", Actualice al último consecutivo, 9 - RADICACION FACTURAS", .Result = False})
                con.IndigoTransaction.Rollback()
                Return ListError
            End If

            'GENERACION DE OFICIO EN DINAMICA
            Dim stringBuilderRadicate As New StringBuilder
            Dim stringBuilderheaderVoucher As New StringBuilder
            Dim stringBuilderDetailsVoucher As New StringBuilder
            Dim stringBuilderUpdateState As New StringBuilder

            ' Dim fechaCreacion As String
            Dim fechaConfirmacion As String
            Dim fechaDateConfirmationsystem As String
            fechaDateConfirmationsystem = Format(DateConfirmationsystem, "yyyyMMdd hh:mm:ss")
            fechaConfirmacion = Format(DateConfirmation, "yyyyMMdd hh:mm:ss")

            Tercero = con.fncConcatenar("0", Tercero, 15, ISQL.Direccion.Izquierda)
            'cabecera de radciacion de cuentas ERP
            strSql = "INSERT INTO " & NameContainer & "..[CRCRACTS]([TERCODTER],[CCRNUMRAD],[CCRFECRAD],[CCRESTADO],[CCRUSUCRE],[CCRUSUANU],[CCRFECANU],[CCRUSUCON],[CCRFECCON]" & _
                ",[ENTCODIGO],[GECCODIGO],[CCRRADENT],[PLACODIGO],[CCRUSUMOD],[CCRESTCUE],[CCRCONOBJ],[ACACODIGO]) " & _
                "VALUES('" & Tercero & "','" & ConsecutiveNumberRadicate & "',convert(varchar(20),'" & (fechaConfirmacion) & "'),'C','" & User & "',NULL,NULL,'" & User & "',convert(varchar(20),'" & (fechaConfirmacion) & "'),'','','','','" & User & "','',0,'') "
            stringBuilderRadicate.AppendLine(strSql)
            For Each item As RadicateInvoiceD In listRadicated
                'Detalle radicacion de cuentas ERP
                strSql = "INSERT INTO " & NameContainer & "..[CRMRACTS]([CCRNUMRAD],[CMRNUMFAC],[CMRRADENT],[CMROBSERV],[GECCODIGO],[PLACODIGO],[ENTCODIGO],[CMRENTCUE],[CMRFECCUE] " & _
               ",[CMREJECUE],[CMRVALRAD],[ACACODIGO],[PLACODIG1])" & _
                "VALUES('" & ConsecutiveNumberRadicate & "','" & item.InvoiceNumber & "','','" & Comment & "','" & item.ContractCode.Trim & "','" & item.PlanCode.Trim & "','" & item.ContractEntity.Trim & "','',GETDATE(),'',convert(varchar(50)," & item.BalanceInvoice & "),NULL,'" & item.PlanCode & "')"
                stringBuilderRadicate.AppendLine(strSql)
            Next
            'GENERACION DE OFICIO EN DINAMICA
            'FIN GENERACION DE OFICIO EN DINAMICA


            strSql = "SELECT top 1 id, CompanyName,RadicateCodeNoteAccounting FROM " & IndigoEmpresa & ".Glosas.GlosasParametersInterface WHERE ContainerName= '" & NameContainer & "'  "
            Dim dtConfi As DataTable = con.ExecuteCommand_Data(strSql)
            If dtConfi.Rows.Count = 0 Then
                ListError.Add(New InterfaceResult With {.Message = "Radicacion Factura: No Existe configuracion de interface para el contenedor" & NameContainer, .Result = False})
                con.IndigoTransaction.Rollback()
                Return ListError
            Else
                NombreEmpresa = dtConfi.Rows(0).Item("CompanyName").ToString
                If dtConfi.Rows(0).Item("RadicateCodeNoteAccounting").ToString = String.Empty Then
                    ListError.Add(New InterfaceResult With {.Message = "Radicacion Factura:no esta configurado el numero de comprobante para radicacion de cuentas " & NameContainer, .Result = False})
                    con.IndigoTransaction.Rollback()
                    Return ListError
                Else
                    NumerodeNotaContable = dtConfi.Rows(0).Item("RadicateCodeNoteAccounting").ToString
                End If
            End If

            'valida mes actual este abierto 
            Dim res As Boolean = Validation.ValidateMonthClose(eTypeInterface.FoxPrivate, Date.Now, NameContainer)
            If res = False Then
                ListError.Add(New InterfaceResult With {.Message = "Radicacion Factura: El mes " & Month(Date.Now).ToString & " ya esta cerrado, no se puede continuar" & NameContainer, .Result = False})
                con.IndigoTransaction.Rollback()
                Return ListError
            End If

            'valida que la tabla de moviminetos contables por año y mes  (MM201404)  Exista
            res = Validation.ValidateTableMov(Date.Now, NameContainer)
            If res = False Then
                ListError.Add(New InterfaceResult With {.Message = "Radicacion Factura:Tabla movimiento mes no existe, no se ha realizado cierre de mes" & NameContainer, .Result = False})
                con.IndigoTransaction.Rollback()
                Return ListError
            End If

            Dim FlagContolGenerateAccountNote As Boolean
            Dim ValorFac As Decimal
            Dim NumeroGlosa As String = RadicateInvoiceC.RadicatedConsecutive  'en este caso el consecutivo de radicacion de facturas 
            Tercero = con.fncConcatenar("0", Tercero, 15, ISQL.Direccion.Izquierda)


            'un solo update
            strSql = "UPDATE " & NameContainer & "..ctcomdia SET ccdnumcom = ccdnumcom   WHERE ccdcodcom='" & NumerodeNotaContable & "'"
            Dim resActualizarConsecutivo As Boolean = con.ExecuteCommand(strSql)
            If resActualizarConsecutivo = False Then
                ListError.Add(New InterfaceResult With {.Message = "Radicacion Factura:ha ocurrido un error actualizando consecutivo, favor avisar al administrador del sistema", .Result = False})
                Return ListError
            End If

            Dim CountVoucher As Integer
            'Busco el consecutivo de comprobantes
            strSql = "SELECT top 1  CCDNUMCOM  FROM " & NameContainer & "..CTCOMDIA where CCDCODCOM = '" & NumerodeNotaContable & "'"
            Dim dtConsecutivoComprobante As DataTable = con.ExecuteCommand_Data(strSql)
            If dtConsecutivoComprobante.Rows.Count = 0 Then
                ListError.Add(New InterfaceResult With {.Message = "Radicacion Factura:No esta configurado los consecutivo para comprobantes diario de radicacion de facturas o ha ocurrido un error, favor avisar al administrador del sistema", .Result = False})
                Return ListError
            Else
                ConsecutivoNumcon = dtConsecutivoComprobante.Rows(0).Item("CCDNUMCOM").ToString.TrimEnd
                CountVoucher = dtConsecutivoComprobante.Rows(0).Item("CCDNUMCOM").ToString.TrimEnd
            End If

            Dim DateActual As Date = Date.Now
            Dim fecha As String
            fecha = Format(DateActual, "yyyyMMdd hh:mm:ss")
            Dim MesConcatenado As String = Month(DateActual).ToString
            MesConcatenado = con.fncConcatenar("0", MesConcatenado, 2, ISQL.Direccion.Izquierda)
            Dim NombreTabla As String = NameContainer & "..MM" & Year(DateActual) & MesConcatenado

            'GENERACION DE COMPROBANTES CONTABLE Y ACTUALIZACION DE CUENTA Y ESTADO CARTERA ERP
            For Each itemD As RadicateInvoiceD In listRadicated
                FlagContolGenerateAccountNote = True
                Dim Factura As String = itemD.InvoiceNumber
                ValorFac = itemD.BalanceInvoice

                CountVoucher = CountVoucher + 1
                ConsecutivoNumcon = ConsecutivoNumcon + 1
                ConsecutivoNumcon = con.fncConcatenar("0", ConsecutivoNumcon, 10, ISQL.Direccion.Izquierda)

                '  Dim ResultEje As Boolean
                'Creacion del comprobante      Cabecera
                strSql = "INSERT INTO " & NameContainer & "..ctconmov(ccdcodcom,ccmnumcom,ccmfeccom,ccmasunto,ccmnumreg,ccmestado,ccmdocume)" &
                "VALUES( '" & NumerodeNotaContable & "','" & ConsecutivoNumcon & "',convert(varchar(20),'" & fecha & "'),'Radicación de Factura  - Fra. " & Factura & " Mod. Glosas',0,'','" & Factura & "' )"
                stringBuilderheaderVoucher.AppendLine(strSql)


                Dim InvoiceNotRadicate As String = itemD.AccountantAccountCustomers

                'cuenta de cartera PortfoliGlosa
                'Dim dtCuentaCartera As DataTable
                'Dim cuentaCarteraGlosa As String = String.Empty

                'strSql = "select CPCCODCUE  from  " & NameContainer & "..crcarter where CEMNUMFAC =  '" & Factura & "'"   'factura sin confirmar, traemos cuenta de cartera ERP
                'dtCuentaCartera = con.ExecuteCommand_Data(strSql)
                'Dim InvoiceNotRadicate As String = String.Empty
                Dim InvoiceRadicate As String = String.Empty
                'If dtCuentaCartera.Rows.Count > 0 Then
                '    cuentaCarteraGlosa = dtCuentaCartera.Rows(0).Item("CPCCODCUE").ToString
                strSql = "select InvoiceNotRadicate,InvoiceRadicate,RectifiableGlosa,LegalProcess,Conciliation from  " & IndigoEmpresa & ".Glosas.AccountSettingsFOX_PrivateMethod WHERE InvoiceNotRadicate = '" & InvoiceNotRadicate & "' "
                Dim dtAccoutConfi As DataTable = con.ExecuteCommand_Data(strSql)
                If dtAccoutConfi.Rows.Count = 0 Then
                    FlagContolGenerateAccountNote = False
                    ListError.Add(New InterfaceResult With {.Message = "Radicacion Factura: No existe configuraciones contables glosas para metodo privado (Fox) en el campo (Factura No Radicada) - cuenta " & InvoiceNotRadicate & ", no se puede continuar!!", .Result = False})
                Else
                    ' InvoiceNotRadicate = dtAccoutConfi.Rows(0).Item("InvoiceNotRadicate").ToString
                    InvoiceRadicate = dtAccoutConfi.Rows(0).Item("InvoiceRadicate").ToString
                End If
                'Else
                '    FlagContolGenerateAccountNote = False
                '    ListError.Add(New InterfaceResult With {.Message = "Radicacion Factura: no existe cuenta en cartera ERP para la factura " & Factura & ", no se puede continuar!!", .Result = False})
                'End If


                'Detalle A credito
                Dim Contador As Integer = 1

                strSql = "INSERT INTO " & NombreTabla & " (ccdcodcom,ccmnumcom,cpccodcue,tercodter,ccccodcen,ccmfeccom,cmmvaldeb,cmmvalcre,cmmdetmov,cmmcontem,cmmfeccon,ccrcodcon,cmmporret,cmmvalbas,cmmvalfac,cmmnumreg) " & _
                "VALUES( '" & NumerodeNotaContable & "','" & ConsecutivoNumcon & "', '" & InvoiceNotRadicate & "', '" & Tercero & "','',convert(varchar(20),'" & fecha & "'),0, " & _
                "convert(varchar(50)," & ValorFac & "),'Radicación de Factura - Fra. " & Factura & " Mod. Glosas','',null,'',0,0,0,'" & Contador & "')"
                stringBuilderDetailsVoucher.AppendLine(strSql)


                Contador = Contador + 1
                'Detalle Debito
                strSql = "INSERT INTO " & NombreTabla & " (ccdcodcom,ccmnumcom,cpccodcue,tercodter,ccccodcen,ccmfeccom,cmmvaldeb,cmmvalcre,cmmdetmov,cmmcontem,cmmfeccon,ccrcodcon,cmmporret,cmmvalbas,cmmvalfac,cmmnumreg) " & _
                "VALUES( '" & NumerodeNotaContable & "','" & ConsecutivoNumcon & "', '" & InvoiceRadicate & "', '" & Tercero & "','',convert(varchar(20),'" & fecha & "')," & _
                "convert(varchar(50)," & ValorFac & "),0,'Radicación de Factura - Fra. " & Factura & " Mod. Glosas','',null,'',0,0,0,'" & Contador & "')"
                stringBuilderDetailsVoucher.AppendLine(strSql)


                'Una ves realizado el comprobante contable actualizamos la tabla de Cartera ERP para la factura

                strSql = "UPDATE " & NameContainer & "..crcarter SET cemestado = '2', CPCCODCUE = '" & InvoiceRadicate & "' WHERE cemnumfac = '" & Factura & "'"   'Confirmado
                stringBuilderUpdateState.AppendLine(strSql)
                'If resultUpdate = False Then
                '    FlagContolGenerateAccountNote = False
                '    ListError.Add(New InterfaceResult With {.Message = "Radicacion Factura: Error Actualizando Cartera ERP", .Result = False})
                'End If


                'strSql = "UPDATE [" & IndigoEmpresa & "].[Glosas].[RadicateInvoiceD] set RadicatedNumber = '" & ConsecutiveNumberRadicate & "',RadicatedDate = convert(varchar(20),'" & fecha & "'), state = 2  where invoicenumber = '" & Factura & "' AND RadicateInvoiceCId = '" & itemD.RadicateInvoiceCId & "' "
                'resultUpdate = con.ExecuteCommand(strSql)
                'If resultUpdate = False Then
                '    FlagContolGenerateAccountNote = False
                '    ListError.Add(New InterfaceResult With {.Message = "Radicacion Factura: Error Actualizando Tabla detalle de radicado", .Result = False})
                'End If

                If FlagContolGenerateAccountNote = True Then
                    ListInfo.Add(New InterfaceResult With {.Message = "Se Generaron los siguientes documentos: Comprobante Contable: " & NumerodeNotaContable & " - " & ConsecutivoNumcon & " para la factura: " & Factura & ", Empresa: " & NombreEmpresa, .Result = True, .Consecutive = NumerodeNotaContable & " - " & ConsecutivoNumcon})
                    Validation.AuditInterface(IndigoEmpresa, intOpcion, NumeroGlosa, Factura, 1, NumerodeNotaContable, ConsecutivoNumcon)
                End If
            Next 'FIN CICLO

            Dim command As New System.Data.SqlClient.SqlCommand("", con.sqlWebConection)
            command.CommandTimeout = 30000
            command.CommandType = CommandType.Text
            Dim IntResult As Integer

            'generacion de cabecera y detalle en ERP de radicados
            command.CommandText = stringBuilderRadicate.ToString
            IntResult = command.ExecuteNonQuery()
            If IntResult <= 0 Then
                ListError.Add(New InterfaceResult With {.Message = "Ocurrio un error al cabecera y detalle de radicación de cuentas ERP", .Result = False})
            End If

            'segundo update actualizando consecutivos de comprobantes
            command.CommandText = "UPDATE " & NameContainer & "..ctcomdia SET ccdnumcom = " & CountVoucher & "  WHERE ccdcodcom='" & NumerodeNotaContable & "'"
            IntResult = command.ExecuteNonQuery()
            If IntResult <= 0 Then
                ListError.Add(New InterfaceResult With {.Message = "Radicacion Factura:ha ocurrido un error actualizando consecutivo, favor avisar al administrador del sistema", .Result = False})
            End If

            'ejecuto creacion de cabeceras de comprobantes contables
            command.CommandText = stringBuilderheaderVoucher.ToString()
            IntResult = command.ExecuteNonQuery()
            If IntResult <= 0 Then
                ListError.Add(New InterfaceResult With {.Message = "Radicacion Factura: Error creando cabecera de comprobante contables", .Result = False})
            End If

            'ejecuto creacion de detalle de comprobantes contables
            command.CommandText = stringBuilderDetailsVoucher.ToString()
            IntResult = command.ExecuteNonQuery()
            If IntResult <= 0 Then
                ListError.Add(New InterfaceResult With {.Message = "Radicacion Factura: Error creando Detalle de comprobante contables", .Result = False})
            End If

            'ejecuto actualizacion de estado en ERP 
            command.CommandText = stringBuilderUpdateState.ToString()
            IntResult = command.ExecuteNonQuery()
            If IntResult <= 0 Then
                ListError.Add(New InterfaceResult With {.Message = "Radicacion Factura: Error actualizando estado en cartera ERP", .Result = False})
            End If

            command.CommandText = "UPDATE [" & IndigoEmpresa & "].[Portfolio].[RadicateInvoiceC] set  state = 2, ConfirmDateSystem = convert(varchar(20),'" & (fechaDateConfirmationsystem) & "'), ConfirmDate = convert(varchar(20),'" & (fechaConfirmacion) & "'), ConfirmUser =" & RadicateInvoiceC.ConfirmUser & ", ConfirmComment = '" & RadicateInvoiceC.ConfirmComment & "'   " & _
            "where id = '" & RadicateInvoiceC.Id & "' "
            IntResult = command.ExecuteNonQuery()
            If IntResult <= 0 Then
                ListError.Add(New InterfaceResult With {.Message = "Radicacion Factura: Error Actualizando Tabla cabecera de radicado", .Result = False})
            End If

            command.CommandText = "UPDATE [" & IndigoEmpresa & "].[Portfolio].[RadicateInvoiceD] set RadicatedNumber = '" & ConsecutiveNumberRadicate & "',RadicatedDate = convert(varchar(20),'" & fecha & "'), state = 2  where  RadicateInvoiceCId = '" & RadicateInvoiceC.Id & "' "
            IntResult = command.ExecuteNonQuery()
            If IntResult <= 0 Then
                ListError.Add(New InterfaceResult With {.Message = "Radicacion Factura: Error Actualizando Estado detalle de radicacion", .Result = False})
            End If

            'control de errores y/o mensaje de informacion
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

#Region "Traslado cobro juridico"

    Public Function TransferJuridical(IndigoEmpresa As String, NumeroGlosa As String, factura As String, Tercero As String, NameContainer As String, ValorFac As Decimal, FechaFactura As Integer, intOpcion As String, User As String) As InterfaceResult Implements IInterfaceFOX.TransferJuridical
        Dim Result As New InterfaceResult
        If IndigoEmpresa = String.Empty Then
            Throw New ArgumentNullException("empresa indigo vacio")
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
                Result = New InterfaceResult With {.Message = "Traslado Cobro Juridico: No Existe configuracion de interface para el contenedor" & NameContainer, .Result = False}
                Return Result
            Else
                NombreEmpresa = dtConfi.Rows(0).Item("CompanyName").ToString
            End If

            Tercero = con.fncConcatenar("0", Tercero, 15, ISQL.Direccion.Izquierda)
            'valida mes actual este abierto 
            Dim res As Boolean = Validation.ValidateMonthClose(eTypeInterface.FoxPrivate, Date.Now, NameContainer)
            If res = False Then
                Result = New InterfaceResult With {.Message = "Traslado Cobro Juridico: El mes " & Month(Date.Now).ToString & " ya esta cerrado, no se puede continuar" & NameContainer, .Result = False}
                Return Result
            End If


            'valida que la tabla de moviminetos contables por año y mes  (MM201404)  Exista
            res = Validation.ValidateTableMov(Date.Now, NameContainer)
            If res = False Then
                Result = New InterfaceResult With {.Message = "Traslado Cobro Juridico:Tabla movimiento mes no existe, no se ha realizado cierre de mes" & NameContainer, .Result = False}
                Return Result
            End If

            'Busco el consecutivo de comprobantes
            Dim ConsecutivoComprobante As String
            strSql = "SELECT top 1  COMDIDIRE FROM " & NameContainer & "..crParSiS"   'comprobante de difícil Diario recaudo
            Dim dtConsecutivoComprobante As DataTable = con.ExecuteCommand_Data(strSql)
            If dtConsecutivoComprobante.Rows.Count = 0 Then
                Result = New InterfaceResult With {.Message = "Traslado Cobro Juridico:No esta configurado los consecutivo para comprobantes o ha ocurrido un error, favor avisar al administrador del sistema", .Result = False}
                Return Result
            Else
                ConsecutivoComprobante = dtConsecutivoComprobante.Rows(0).Item("COMDIDIRE").ToString.TrimEnd
            End If

            'busco consecutivo comprobante diario mediante el consecutivo de comprobantes
            Dim ConsecutivoNumcon As String
            strSql = "SELECT top 1  ccdnumcom FROM " & NameContainer & "..ctcomdia WHERE ccdcodcom='" & ConsecutivoComprobante & "'"
            Dim dtConsecutivoNumcon As DataTable = con.ExecuteCommand_Data(strSql)
            If dtConsecutivoNumcon.Rows.Count = 0 Then
                Result = New InterfaceResult With {.Message = "Traslado Cobro Juridico:No esta configurado los consecutivo para comprobantes diario o ha ocurrido un error, favor avisar al administrador del sistema", .Result = False}
                Return Result
            Else
                ConsecutivoNumcon = dtConsecutivoNumcon.Rows(0).Item("ccdnumcom").ToString.TrimEnd
                'Actualiza el consecutive del comprobante
                strSql = "UPDATE " & NameContainer & "..ctcomdia SET ccdnumcom = ccdnumcom +1  WHERE ccdcodcom='" & ConsecutivoComprobante & "'"
                Dim resActualizarConsecutivo As Boolean = con.ExecuteCommand(strSql)
                If resActualizarConsecutivo = True Then
                    ConsecutivoNumcon = ConsecutivoNumcon + 1
                Else
                    Result = New InterfaceResult With {.Message = "Traslado Cobro Juridico:ha ocurrido un error actualizando consecutivo, favor avisar al administrador del sistema", .Result = False}
                    Return Result
                End If
            End If

            Dim DateActual As Date = Date.Now
            Dim fecha As String
            fecha = Format(DateActual, "yyyyMMdd hh:mm:ss")
            ConsecutivoNumcon = con.fncConcatenar("0", ConsecutivoNumcon, 10, ISQL.Direccion.Izquierda)
            Dim ResultEje As Boolean
            'Creacion del comprobante      Cabecera
            strSql = "INSERT INTO " & NameContainer & "..ctconmov(ccdcodcom,ccmnumcom,ccmfeccom,ccmasunto,ccmnumreg,ccmestado,ccmdocume)" &
            "VALUES( '" & ConsecutivoComprobante & "','" & ConsecutivoNumcon & "',convert(varchar(20),'" & fecha & "'),'Traslado Cobro Juridico - Fra. " & factura & " Mod. Glosas',0,'','" & factura & "' )"
            ResultEje = con.ExecuteCommand(strSql)


            'cuenta de cartera PortfoliGlosa
            Dim dtCuentaCartera As DataTable
            Dim cuentaCarteraGlosa As String = String.Empty
            strSql = "SELECT  AccountantAccountCustomers from " & IndigoEmpresa & ".Glosas.GlosaPortfolioGlosada where InvoiceNumber = '" & factura & "'"  'Cartera genesis
            dtCuentaCartera = con.ExecuteCommand_Data(strSql)
            If dtCuentaCartera.Rows.Count > 0 Then
                cuentaCarteraGlosa = dtCuentaCartera.Rows(0).Item("AccountantAccountCustomers").ToString
            End If


            'Detalle  Acredito la cuenta de GlosaPortfolioGlosada
            Dim Contador As Integer = 1
            Dim MesConcatenado As String = Month(DateActual).ToString
            MesConcatenado = con.fncConcatenar("0", MesConcatenado, 2, ISQL.Direccion.Izquierda)
            Dim NombreTabla As String = NameContainer & "..MM" & Year(DateActual) & MesConcatenado
            strSql = "INSERT INTO " & NombreTabla & " (ccdcodcom,ccmnumcom,cpccodcue,tercodter,ccccodcen,ccmfeccom,cmmvaldeb,cmmvalcre,cmmdetmov,cmmcontem,cmmfeccon,ccrcodcon,cmmporret,cmmvalbas,cmmvalfac,cmmnumreg) " & _
            "VALUES( '" & ConsecutivoComprobante & "','" & ConsecutivoNumcon & "', '" & cuentaCarteraGlosa & "', '" & Tercero & "','',convert(varchar(20),'" & fecha & "'),0, " & _
            "convert(varchar(50)," & ValorFac & "),'Traslado Cobro Juridico - Fra. " & factura & " Mod. Glosas','',null,'',0,0,0,'" & Contador & "')"
            ResultEje = con.ExecuteCommand(strSql)



            'consultamos el concepto de la cuentam, apartir de este concepto cargamos la cuenta de traslado a cobro juridio a Debitar
            Dim dtConcept As DataTable
            Dim strconcept As String
            strSql = "Select RTRIM(LTRIM(cnocodcon)) as cnocodcon from  " & NameContainer & "..crConcep where CPCCODCUE = '" & cuentaCarteraGlosa & "' "
            dtConcept = con.ExecuteCommand_Data(strSql)
            If dtConcept.Rows.Count > 0 Then
                strconcept = dtConcept.Rows(0).Item("cnocodcon").ToString
            Else
                Result = New InterfaceResult With {.Message = "Traslado Cobro Juridico: No se encontro concepto para la cuenta " & cuentaCarteraGlosa & ", no se puede continuar!!", .Result = False}
                Return Result
            End If


            'cuenta de traslado a cobro juridico
            Dim LegalProcess As String
            strSql = "select InvoiceNotRadicate,InvoiceRadicate,RectifiableGlosa,LegalProcess,Conciliation from  " & IndigoEmpresa & ".Glosas.AccountSettingsFOX_PrivateMethod WHERE RectifiableGlosa = '" & strconcept & "' "
            Dim dtAccoutConfi As DataTable = con.ExecuteCommand_Data(strSql)
            If dtAccoutConfi.Rows.Count = 0 Then
                Result = New InterfaceResult With {.Message = "Traslado Cobro Juridico: No existe configuraciones contables glosas para metodo privado (Fox) en el campo (Glosa Subsanada) - concepto  " & strconcept & ", no se puede continuar!!", .Result = False}
                Return Result
            Else
                If dtAccoutConfi.Rows.Count > 0 AndAlso dtAccoutConfi.Rows(0).Item("LegalProcess") <> String.Empty Then
                    LegalProcess = dtAccoutConfi.Rows(0).Item("LegalProcess").ToString
                Else
                    Result = New InterfaceResult With {.Message = "Traslado Cobro Juridico:no se encontro cuenta de traslado a cobro juridico, favor avisar al administrador del sistema", .Result = False}
                    Return Result
                End If
            End If




            Contador = Contador + 1
            'Detalle Debito
            strSql = "INSERT INTO " & NombreTabla & " (ccdcodcom,ccmnumcom,cpccodcue,tercodter,ccccodcen,ccmfeccom,cmmvaldeb,cmmvalcre,cmmdetmov,cmmcontem,cmmfeccon,ccrcodcon,cmmporret,cmmvalbas,cmmvalfac,cmmnumreg) " & _
            "VALUES( '" & ConsecutivoComprobante & "','" & ConsecutivoNumcon & "', '" & LegalProcess & "', '" & Tercero & "','',convert(varchar(20),'" & fecha & "')," & _
            "convert(varchar(50)," & ValorFac & "),0,'Traslado Cobro Juridico - Fra. " & factura & " Mod. Glosas','',null,'',0,0,0,'" & Contador & "')"
            ResultEje = con.ExecuteCommand(strSql)



            con.IndigoTransaction.Commit()

            Validation.AuditInterface(IndigoEmpresa, intOpcion, NumeroGlosa, factura, 1, ConsecutivoComprobante, ConsecutivoNumcon)



            Result = New InterfaceResult With {.Message = "Se Generaron los siguientes documentos: Comprobante Contable: " & ConsecutivoComprobante & " - " & ConsecutivoNumcon & ", Empresa: " & NombreEmpresa, .Result = True, .Consecutive = ConsecutivoComprobante & " - " & ConsecutivoNumcon}


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
    Public Function LoadBalanceInvoice(NumberInvoice As String, NameContainer As String) As Decimal Implements IInterfaceFOX.LoadBalanceInvoice

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

            strSql = "select CEMSALFAC  FROM " & NameContainer & "..CRCARTER  WHERE CEMNUMFAC = '" & NumberInvoice & "'"
            Dim dtbalance As DataTable = con.ExecuteCommand_Data(strSql)
            If dtbalance.Rows.Count > 0 Then
                balance = dtbalance.Rows(0).Item("CEMSALFAC")
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

    Public Function BalanceReiterated(IndigoEmpresa As String, NumeroGlosa As String, factura As String, Tercero As String, NameContainer As String, ValorFac As Decimal, FechaFactura As Integer, intOpcion As String, User As String, Optional ByVal EjecutaAceptacionEAPBUnicaTransaccion As Boolean = False) As InterfaceResult Implements IInterfaceFOX.BalanceReiterated
        Dim Result As New InterfaceResult
        If IndigoEmpresa = String.Empty Then
            Throw New ArgumentNullException("empresa indigo vacio")
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
                Result = New InterfaceResult With {.Message = "Aceptacion EAPB: No Existe configuracion de interface para el contenedor" & NameContainer, .Result = False}
                Return Result
            Else
                NombreEmpresa = dtConfi.Rows(0).Item("CompanyName").ToString
            End If

            'cargamos cuenta apartir de tercero y factura, Validamos Existencia de Cuenta

            Tercero = con.fncConcatenar("0", Tercero, 15, ISQL.Direccion.Izquierda)

            Dim cuentaCartera As String
            strSql = "SELECT top 1  cpccodcue FROM " & NameContainer & "..crCarter WHERE cemNumFac= '" & factura & "' AND TerCodTer ='" & Tercero & "'"
            Dim dtCuenta As DataTable = con.ExecuteCommand_Data(strSql)
            If dtCuenta.Rows.Count = 0 Then
                Result = New InterfaceResult With {.Message = "Aceptacion EAPB: No existe la factura en ERP, favor avisar al administrador del sistema", .Result = False}
                Return Result
            Else
                If dtCuenta.Rows(0).Item("cpccodcue").ToString <> String.Empty Then
                    cuentaCartera = dtCuenta.Rows(0).Item("cpccodcue").ToString
                    Dim res As Integer = Validation.ValidateAccount(eTypeInterface.FoxPrivate, NameContainer, cuentaCartera)
                    If res = 0 Then
                        Result = New InterfaceResult With {.Message = "Aceptacion EAPB: No existe la cuenta " & cuentaCartera & " de la factura en DGH, favor avisar al administrador del sistema", .Result = False}
                        Return Result
                    End If
                Else
                    Result = New InterfaceResult With {.Message = "Aceptacion EAPB: No existe la factura en ERP, favor avisar al administrador del sistema", .Result = False}
                    Return Result
                End If
            End If


            'consultar cuenta de radicacion segun tabla de configuracion
            strSql = "select InvoiceNotRadicate,InvoiceRadicate,RectifiableGlosa,LegalProcess,Conciliation from  " & IndigoEmpresa & ".Glosas.AccountSettingsFOX_PrivateMethod WHERE InvoiceRadicate = '" & cuentaCartera & "' "
            Dim dtConcept As DataTable = con.ExecuteCommand_Data(strSql)
            Dim concepto As String
            If dtConcept.Rows.Count = 0 Then
                Result = New InterfaceResult With {.Message = "Aceptacion EAPB: No existe configuraciones contables glosas para metodo privado (Fox) en el campo (Factura Radicada) - cuenta: " & cuentaCartera & " , no se puede continuar!!", .Result = False}
                Return Result
            Else
                concepto = dtConcept.Rows(0).Item("Conciliation").ToString   'concepto de radicacion nota debito Conciliation
            End If
            'consultamos el codigo del concepto de la cuenta 
            strSql = "SELECT cpccodcue   FROM " & NameContainer & "..crConcep WHERE CNOCODCON= '" & concepto & "'"
            Dim dtCuentaConcepto As DataTable = con.ExecuteCommand_Data(strSql)
            Dim CuentaConcepto As String
            If dtCuentaConcepto.Rows.Count = 0 Then
                Result = New InterfaceResult With {.Message = "Aceptacion EAPB: no existe cuenta para el concepto " & concepto & " , no se puede continuar!!", .Result = False}
                Return Result
            Else
                CuentaConcepto = dtCuentaConcepto.Rows(0).Item("cpccodcue").ToString
            End If



            'validamos la cuenta maneje ce y  nivel 5
            Dim resint As Integer
            strSql = "SELECT count(*) FROM " & NameContainer & "..ctPlaCue WHERE cpcCodCue='" & CuentaConcepto & "' and cpcmancen=1 and cpctipcue = '5'"
            resint = con.ExecuteCommand_Count(strSql)
            If resint = 0 Then
                Result = New InterfaceResult With {.Message = "Aceptacion EAPB: La cuenta " & CuentaConcepto & " del concepto " & concepto & " no existe, o no maneja centros de costo'!!", .Result = False}
                Return Result
            End If


            Dim resultCreateDebitNOte As InterfaceResult = Me.CreateDebitNote(NameContainer, Tercero, factura, ValorFac, User, cuentaCartera, concepto, CuentaConcepto, True, EjecutaAceptacionEAPBUnicaTransaccion)


            Validation.AuditInterface(IndigoEmpresa, intOpcion, NumeroGlosa, factura, 3, concepto, resultCreateDebitNOte.Consecutive)

            Result = New InterfaceResult With {.Message = "Se Generaron los siguientes documentos: Nota Debito: " & resultCreateDebitNOte.Consecutive & ", Empresa: " & NombreEmpresa, .Result = True, .Consecutive = resultCreateDebitNOte.Consecutive}
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

#Region "Proceso de devoluciones de Factura, eliminacion de factura en radicados tanto ERP y genesis, Actualizacion de Cartera ERP"

    Public Function Devolucion(IndigoEmpresa As String, ByVal listRadicated As List(Of GlosaDevolutionsReceptionD), NameContainer As String, intOpcion As String, User As String) As List(Of InterfaceResult) Implements IInterfaceFOX.Devolucion

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

            ' con.InTransaction = True
            ' con.IndigoTransaction = con.sqlWebConection.BeginTransaction(IsolationLevel.ReadCommitted, "Devoluciones de Facturas")

            Dim NumerodeNotaContable As String = String.Empty
            Dim ConsecutivoNumcon As String = String.Empty
            Dim NombreEmpresa As String = String.Empty
            Dim resultUpdate As Boolean


            Dim Tercero As String = String.Empty
            If listRadicated.Count > 0 AndAlso listRadicated(0).GlosaDevolutionsReceptionC IsNot Nothing AndAlso listRadicated(0).GlosaDevolutionsReceptionC.Customer IsNot Nothing Then
                Tercero = listRadicated(0).GlosaDevolutionsReceptionC.Customer.Nit.Trim
            Else
                '   con.IndigoTransaction.Rollback()
                ListError.Add(New InterfaceResult With {.Message = "Ocurrio un error, no existe NIT del tercero", .Result = False})
                '   con.IndigoTransaction.Rollback()
                Return ListError
            End If


            'ELIMINAMOS FACTURAS DE RADICADOS EN DINAMICA
            'GENERACION DE OFICIO EN DINAMICA


            'cosultamos vaalor de la nota 
            strSql = "SELECT top 1 id, CompanyName,DevolutionCodeNoteAccounting FROM " & IndigoEmpresa & ".Glosas.GlosasParametersInterface WHERE ContainerName= '" & NameContainer & "'  "
            Dim dtConfi As DataTable = con.ExecuteCommand_Data(strSql)

            If dtConfi.Rows.Count = 0 Then
                ListError.Add(New InterfaceResult With {.Message = "Devolución Factura: No Existe configuracion de interface para el contenedor" & NameContainer, .Result = False})
            Else
                NombreEmpresa = dtConfi.Rows(0).Item("CompanyName").ToString
                If dtConfi.Rows(0).Item("DevolutionCodeNoteAccounting").ToString = String.Empty Then
                    ListError.Add(New InterfaceResult With {.Message = "Devolución Factura:no esta configurado el numero de comprobante para devoluciones " & NameContainer, .Result = False})
                Else
                    NumerodeNotaContable = dtConfi.Rows(0).Item("DevolutionCodeNoteAccounting").ToString
                End If
            End If



            Dim ResultEje As Boolean
            Dim FlagContolGenerateAccountNote As Boolean
            Dim NumeroGlosa As String = listRadicated(0).GlosaDevolutionsReceptionC.RadicatedConsecutive  'en este caso el consecutivo de devoluciones de facturas 
            Tercero = con.fncConcatenar("0", Tercero, 15, ISQL.Direccion.Izquierda)
            Dim Factura As String = String.Empty

            'valida mes actual este abierto 
            Dim res As Boolean = Validation.ValidateMonthClose(eTypeInterface.FoxPrivate, Date.Now, NameContainer)
            If res = False Then
                FlagContolGenerateAccountNote = False
                ListError.Add(New InterfaceResult With {.Message = "Devolución Factura: El mes " & Month(Date.Now).ToString & " ya esta cerrado, no se puede continuar" & NameContainer, .Result = False})
            End If

            'valida que la tabla de moviminetos contables por año y mes  (MM201404)  Exista
            res = Validation.ValidateTableMov(Date.Now, NameContainer)
            If res = False Then
                FlagContolGenerateAccountNote = False
                ListError.Add(New InterfaceResult With {.Message = "Devolución Factura:Tabla movimiento mes no existe, no se ha realizado cierre de mes" & NameContainer, .Result = False})
            End If


            'Busco el consecutivo de comprobantes
            'strSql = "SELECT top 1  cpscomcog FROM " & NameContainer & "..crParSiS"
            strSql = "SELECT top 1  CCDNUMCOM  FROM " & NameContainer & "..CTCOMDIA where CCDCODCOM = '" & NumerodeNotaContable & "'"
            Dim dtConsecutivoComprobante As DataTable = con.ExecuteCommand_Data(strSql)
            If dtConsecutivoComprobante.Rows.Count = 0 Then
                FlagContolGenerateAccountNote = False
                ListError.Add(New InterfaceResult With {.Message = "Devolución Factura:No esta configurado los consecutivo para comprobantes diario de radicacion de facturas o ha ocurrido un error, favor avisar al administrador del sistema", .Result = False})
            Else
                ConsecutivoNumcon = dtConsecutivoComprobante.Rows(0).Item("CCDNUMCOM").ToString.TrimEnd
                strSql = "UPDATE " & NameContainer & "..ctcomdia SET ccdnumcom = ccdnumcom +1  WHERE ccdcodcom='" & NumerodeNotaContable & "'"
                Dim resActualizarConsecutivo As Boolean = con.ExecuteCommand(strSql)
                If resActualizarConsecutivo = True Then
                    ConsecutivoNumcon = ConsecutivoNumcon + 1
                Else
                    FlagContolGenerateAccountNote = False
                    ListError.Add(New InterfaceResult With {.Message = "Devolución Factura:ha ocurrido un error actualizando consecutivo, favor avisar al administrador del sistema", .Result = False})
                End If
            End If



            'GENERACION DE COMPROBANTES CONTABLE Y ACTUALIZACION DE CUENTA Y ESTADO CARTERA ERP
            For Each itemD As GlosaDevolutionsReceptionD In listRadicated
                FlagContolGenerateAccountNote = True

                Factura = itemD.InvoiceNumber

                'eliminamos factura de radicado en dinamica
                strSql = " DELETE FROM  " & NameContainer & "..[CRMRACTS] WHERE CMRNUMFAC = '" & itemD.InvoiceNumber & "'  AND  CCRNUMRAD = '" & itemD.RadicatedNumber & "' "
                ResultEje = con.ExecuteCommand(strSql)

                'cuenta de cartera PortfoliGlosa
                Dim dtCuentaCartera As DataTable
                Dim cuentaCarteraGlosa As String = String.Empty

                strSql = "select CPCCODCUE  from  " & NameContainer & "..crcarter where CEMNUMFAC =  '" & Factura & "'"   'factura sin confirmar, traemos cuenta de cartera ERP
                dtCuentaCartera = con.ExecuteCommand_Data(strSql)
                Dim InvoiceNotRadicate As String = String.Empty
                Dim InvoiceRadicate As String = String.Empty
                If dtCuentaCartera.Rows.Count > 0 Then
                    cuentaCarteraGlosa = dtCuentaCartera.Rows(0).Item("CPCCODCUE").ToString
                    strSql = "select InvoiceNotRadicate,InvoiceRadicate,RectifiableGlosa,LegalProcess,Conciliation from  " & IndigoEmpresa & ".Glosas.AccountSettingsFOX_PrivateMethod WHERE InvoiceRadicate = '" & cuentaCarteraGlosa & "' "
                    Dim dtAccoutConfi As DataTable = con.ExecuteCommand_Data(strSql)
                    If dtAccoutConfi.Rows.Count = 0 Then
                        FlagContolGenerateAccountNote = False
                        ListError.Add(New InterfaceResult With {.Message = "Devolución Factura: No existe configuraciones contables glosas para metodo privado (Fox) en el campo (Factura Radicada) - cuenta " & cuentaCarteraGlosa & ", no se puede continuar!!", .Result = False})
                    Else
                        InvoiceNotRadicate = dtAccoutConfi.Rows(0).Item("InvoiceNotRadicate").ToString
                        InvoiceRadicate = dtAccoutConfi.Rows(0).Item("InvoiceRadicate").ToString
                    End If
                Else
                    FlagContolGenerateAccountNote = False
                    ListError.Add(New InterfaceResult With {.Message = "Devolución Factura: no existe cuenta en cartera ERP para la factura " & Factura & ", no se puede continuar!!", .Result = False})
                End If



                Dim DateActual As Date = Date.Now
                Dim fecha As String
                fecha = Format(DateActual, "yyyyMMdd hh:mm:ss")
                ConsecutivoNumcon = con.fncConcatenar("0", ConsecutivoNumcon, 10, ISQL.Direccion.Izquierda)
                'Creacion del comprobante      Cabecera
                strSql = "INSERT INTO " & NameContainer & "..ctconmov(ccdcodcom,ccmnumcom,ccmfeccom,ccmasunto,ccmnumreg,ccmestado,ccmdocume)" &
                "VALUES( '" & NumerodeNotaContable & "','" & ConsecutivoNumcon & "',convert(varchar(20),'" & fecha & "'),'Devolución de Factura  - Fra. " & Factura & " Mod. Glosas',0,'','" & Factura & "' )"
                ResultEje = con.ExecuteCommand(strSql)


                'Detalle A credito
                Dim Contador As Integer = 1
                Dim MesConcatenado As String = Month(DateActual).ToString
                MesConcatenado = con.fncConcatenar("0", MesConcatenado, 2, ISQL.Direccion.Izquierda)
                Dim NombreTabla As String = NameContainer & "..MM" & Year(DateActual) & MesConcatenado
                strSql = "INSERT INTO " & NombreTabla & " (ccdcodcom,ccmnumcom,cpccodcue,tercodter,ccccodcen,ccmfeccom,cmmvaldeb,cmmvalcre,cmmdetmov,cmmcontem,cmmfeccon,ccrcodcon,cmmporret,cmmvalbas,cmmvalfac,cmmnumreg) " & _
                "VALUES( '" & NumerodeNotaContable & "','" & ConsecutivoNumcon & "', '" & InvoiceRadicate & "', '" & Tercero & "','',convert(varchar(20),'" & fecha & "'),0, " & _
                "convert(varchar(50)," & itemD.BalanceInvoice & "),'Devolución de Factura - Fra. " & Factura & " Mod. Glosas','',null,'',0,0,0,'" & Contador & "')"
                ResultEje = con.ExecuteCommand(strSql)


                Contador = Contador + 1
                'Detalle Debito
                strSql = "INSERT INTO " & NombreTabla & " (ccdcodcom,ccmnumcom,cpccodcue,tercodter,ccccodcen,ccmfeccom,cmmvaldeb,cmmvalcre,cmmdetmov,cmmcontem,cmmfeccon,ccrcodcon,cmmporret,cmmvalbas,cmmvalfac,cmmnumreg) " & _
                "VALUES( '" & NumerodeNotaContable & "','" & ConsecutivoNumcon & "', '" & InvoiceNotRadicate & "', '" & Tercero & "','',convert(varchar(20),'" & fecha & "')," & _
                "convert(varchar(50)," & itemD.BalanceInvoice & "),0,'Devolución de Factura - Fra. " & Factura & " Mod. Glosas','',null,'',0,0,0,'" & Contador & "')"
                ResultEje = con.ExecuteCommand(strSql)


                'Una ves realizado el comprobante contable actualizamos la tabla de Cartera ERP para la factura

                strSql = "UPDATE " & NameContainer & "..crcarter SET cemestado = '1', CPCCODCUE = '" & InvoiceNotRadicate & "' WHERE cemnumfac = '" & Factura & "'"   'Confirmado
                resultUpdate = con.ExecuteCommand(strSql)


                ' strSql = "DELETE FROM [" & IndigoEmpresa & "].[Glosas].[RadicateInvoiceD] WHERE  RadicatedNumber = '" & itemD.RadicatedNumber & "' AND invoicenumber= '" & itemD.InvoiceNumber & "' AND state = 2  "
                ' resultUpdate = con.ExecuteCommand(strSql)

                strSql = "UPDATE  [" & IndigoEmpresa & "].[Portfolio].[RadicateInvoiceD] SET State = 4 WHERE  RadicatedNumber = '" & itemD.RadicatedNumber & "' AND invoicenumber= '" & itemD.InvoiceNumber & "' AND state = 2  "
                resultUpdate = con.ExecuteCommand(strSql)



                If FlagContolGenerateAccountNote = True Then
                    ListInfo.Add(New InterfaceResult With {.Message = "Se Generaron los siguientes documentos: Comprobante Contable: " & NumerodeNotaContable & " - " & ConsecutivoNumcon & " para la factura: " & Factura & ", Empresa: " & NombreEmpresa, .Result = True, .Consecutive = NumerodeNotaContable & " - " & ConsecutivoNumcon})
                    Validation.AuditInterface(IndigoEmpresa, intOpcion, NumeroGlosa, Factura, 1, NumerodeNotaContable, ConsecutivoNumcon)
                End If


            Next 'FIN CICLO



            If ListError.Count > 0 Then
                '   con.IndigoTransaction.Rollback()
                Return ListError
            Else
                '  con.IndigoTransaction.Commit()
                Return ListInfo
            End If


        Catch ex As Exception
            ' con.IndigoTransaction.Rollback()
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
        ' GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
