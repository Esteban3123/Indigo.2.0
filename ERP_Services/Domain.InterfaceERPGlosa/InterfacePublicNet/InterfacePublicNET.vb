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
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Entities
Imports System.Data.SqlClient

Public Class InterfacePublicNET
    Implements IInterfacePublicNET


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

#Region "Radicacion de Objeciones   NOTA CONTABLE"

    ''' <summary>
    ''' Funcion para generar nota contable en la readicacion de objeciones METODO PUBLICO
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
    Public Function RadicateObjection(IndigoEmpresa As String, NumeroGlosa As String, factura As String, Tercero As String, NameContainer As String, ValorFac As Decimal, FechaFactura As Integer, intOpcion As String, User As String, plancodigo As String) As InterfaceResult Implements IInterfacePublicNET.RadicateObjection
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
        If plancodigo = String.Empty Then
            Throw New Exception("Plan Codigo Vacio")
        End If


        Dim strSql As String = String.Empty
        Try

            'cargamos configuraciones de interface
            If con.sqlWebConection.State = ConnectionState.Closed Then
                con.sqlWebConection.Open()
            End If

            con.InTransaction = True
            con.IndigoTransaction = con.sqlWebConection.BeginTransaction(IsolationLevel.ReadCommitted, "Interfax crear Nota Contable")

            'leer parametro NIIF de DGH - esto solo aplica para las versiones NET de DGH sector publico
            strSql = "select top 1 IFPNIIFACT from " & NameContainer & ".dbo.IFNPARAME "
            Dim AplicaNiif As String
            Dim dtConfiDGH As DataTable = con.ExecuteCommand_Data(strSql)
            If dtConfiDGH.Rows.Count = 0 Then
                Result = New InterfaceResult With {.Message = "Radicacion Objeciones: No existe configuración de parametros contables en DGH" & NameContainer, .Result = False}
                Return Result
            Else
                If dtConfiDGH.Rows(0).Item("IFPNIIFACT") = "1" Then
                    AplicaNiif = "1"
                Else
                    AplicaNiif = "0"
                End If
            End If

            strSql = "SELECT top 1 id, CompanyName,DebitAccount FROM " & IndigoEmpresa & ".Glosas.GlosasParametersInterface WHERE ContainerName= '" & NameContainer & "'  "
            Dim dtConfi As DataTable = con.ExecuteCommand_Data(strSql)
            Dim DebitAccount As String
            Dim CreditAccount As String
            Dim NombreEmpresa As String
            If dtConfi.Rows.Count = 0 Then
                Result = New InterfaceResult With {.Message = "Radicacion Objeciones: No Existe configuracion de interface para el contenedor" & NameContainer, .Result = False}
                Return Result
            Else
                NombreEmpresa = dtConfi.Rows(0).Item("CompanyName").ToString
                If dtConfi.Rows(0).Item("DebitAccount").ToString = String.Empty Then
                    Result = New InterfaceResult With {.Message = "Radicacion Objeciones: La cuenta Debito para radicación no existe en las configuraciones de interfaces" & NameContainer, .Result = False}
                    Return Result
                Else
                    Dim ParametersInterfaceId As String
                    DebitAccount = dtConfi.Rows(0).Item("DebitAccount").ToString
                    ParametersInterfaceId = dtConfi.Rows(0).Item("Id").ToString
                    Dim dtAccountCreditPlan As DataTable
                    strSql = "select InvoiceRadicate as CreditAccount,denomination,plancode from " & IndigoEmpresa & ".Glosas.AccountSettingsNET_PublicMethod where ParametersInterfaceId = " & ParametersInterfaceId & " and plancode = '" & plancodigo & " '"
                    dtAccountCreditPlan = con.ExecuteCommand_Data(strSql)
                    If dtAccountCreditPlan IsNot Nothing AndAlso dtAccountCreditPlan.Rows.Count > 0 AndAlso dtAccountCreditPlan.Rows(0).Item("CreditAccount").ToString <> String.Empty Then
                        CreditAccount = dtAccountCreditPlan.Rows(0).Item("CreditAccount").ToString
                    Else
                        If dtAccountCreditPlan IsNot Nothing AndAlso dtAccountCreditPlan.Rows.Count > 0 Then
                            Result = New InterfaceResult With {.Message = "Radicacion Objeciones: La cuenta Credito para radicación no existe en las configuraciones de interfaces para el plan:" & dtAccountCreditPlan.Rows(0).Item("plancode").ToString & " - " & dtAccountCreditPlan.Rows(0).Item("denomination").ToString, .Result = False}
                            Return Result
                        Else
                            Result = New InterfaceResult With {.Message = "Radicacion Objeciones: No se encontraron cuenta con el plan: " & plancodigo, .Result = False}
                            Return Result
                        End If

                    End If
                End If
            End If

            Dim res As Boolean

            ' Tercero = con.fncConcatenar("0", Tercero, 15, ISQL.Direccion.Izquierda)
            'valida mes actual este abierto 
            'res = Validation.ValidateMonthClose(eTypeInterface.NETPublic, Date.Now, NameContainer)
            'If res = False Then
            '    Result = New InterfaceResult With {.Message = "Radicacion Objeciones: El mes " & Month(Date.Now).ToString & " ya esta cerrado, no se puede continuar" & NameContainer, .Result = False}
            '    Return Result
            'End If

            'validacion de la cuenta debito
            res = Validation.ValidateAccount(eTypeInterface.NETPublic, NameContainer, DebitAccount)
            If res = False Then
                Result = New InterfaceResult With {.Message = "Radicacion Objeciones: La cuenta " & DebitAccount & "No Existe", .Result = False}
                Return Result
            End If


            'validacion de la cuenta credito
            res = Validation.ValidateAccount(eTypeInterface.NETPublic, NameContainer, CreditAccount)
            If res = False Then
                Result = New InterfaceResult With {.Message = "Radicacion Objeciones: La cuenta " & CreditAccount & "No Existe", .Result = False}
                Return Result
            End If

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


            Dim DateActual As Date = Date.Now
            Dim fecha As String
            fecha = Format(DateActual, "yyyyMMdd hh:mm:ss")
            Dim ResultEje As Boolean
            'Creacion de historico de estados
            Dim AutoCRNCXCEST As Integer
            strSql = "INSERT INTO " & NameContainer & "..CRNCXCEST([CRNCXC],[CCEFECHA],[CCEESTADO],[CRNDOCUME],[GENUSUARIO],[CCEOBSERV],[CCEFECEST],[OptimisticLockField],[CCEDESCON])" &
            "VALUES('" & OIDFactura & "',convert(varchar(20),'" & fecha & "'),'3',NULL,'" & OIDusuario & "','Mod. genesis: La CxC paso de estado RadicadaEntidad al estado Objetada',convert(varchar(20),'" & fecha & "'),0,0)"
            ResultEje = con.ExecuteCommand(strSql)
            Dim SQLEstados As New SqlDataAdapter("SELECT @@IDENTITY", con.sqlWebConection)
            With SQLEstados
                .SelectCommand.CommandTimeout = 90
                .SelectCommand.Transaction = con.IndigoTransaction
                .SelectCommand.CommandType = CommandType.Text
                AutoCRNCXCEST = .SelectCommand.ExecuteScalar.ToString
            End With

            'Actualizo estado de factura en cartera
            strSql = "UPDATE " & NameContainer & "..CRNCXC SET CXCESTCAR = '3',CXCESTCACT = '" & AutoCRNCXCEST & "' WHERE CXCDOCUME= '" & factura & "' "
            res = con.ExecuteCommand(strSql)
            If res = False Then
                Result = New InterfaceResult With {.Message = "Radicacion Objeciones: ha ocurrido un error actualizando estado de cartera", .Result = False}
                Return Result
            End If

            'Busco el consecutivo de comprobantes
            Dim ConsecutivoComprobante As String
            strSql = "SELECT top 1   CTNTIPCOM5 FROM " & NameContainer & "..CRNPARSIS"
            Dim dtConsecutivoComprobante As DataTable = con.ExecuteCommand_Data(strSql)
            If dtConsecutivoComprobante.Rows.Count = 0 Then
                Result = New InterfaceResult With {.Message = "Radicacion Objeciones:No esta configurado los consecutivo para comprobantes o ha ocurrido un error, favor avisar al administrador del sistema", .Result = False}
                Return Result
            Else
                ConsecutivoComprobante = dtConsecutivoComprobante.Rows(0).Item("CTNTIPCOM5").ToString.TrimEnd
            End If

            Dim NumeroGenerado As Integer
            'busco consecutivo comprobante diario mediante el consecutivo de comprobantes
            Dim OIDComprobante As String
            Dim CodigoTipoDocumento As String
            strSql = "SELECT top 1  GENCONSEC, tccodigo FROM " & NameContainer & "..CTNTIPCOM WHERE OID='" & ConsecutivoComprobante & "'"
            Dim dtConsecutivoNumcon As DataTable = con.ExecuteCommand_Data(strSql)
            If dtConsecutivoNumcon.Rows.Count = 0 Then
                Result = New InterfaceResult With {.Message = "Radicacion Objeciones:No esta configurado los consecutivo para comprobantes diario o ha ocurrido un error, favor avisar al administrador del sistema", .Result = False}
                Return Result
            Else
                OIDComprobante = dtConsecutivoNumcon.Rows(0).Item("GENCONSEC").ToString.TrimEnd
                CodigoTipoDocumento = dtConsecutivoNumcon.Rows(0).Item("tccodigo").ToString.TrimEnd
                'Actualiza el consecutive del comprobante
                strSql = "UPDATE " & NameContainer & "..geNconsec SET GCONUMERO = GCONUMERO +1  WHERE OID='" & OIDComprobante & "'"
                Dim resActualizarConsecutivo As Boolean = con.ExecuteCommand(strSql)
                If resActualizarConsecutivo = True Then
                    'consultamo 
                    Dim dtConseIncrementa As DataTable
                    strSql = " SELECT top 1 GCONUMERO FROM " & NameContainer & "..geNconsec WHERE OID='" & OIDComprobante & "'"
                    dtConseIncrementa = con.ExecuteCommand_Data(strSql)
                    ' ConsecutivoNumcon = ConsecutivoNumcon + 1
                    NumeroGenerado = CInt(dtConseIncrementa.Rows(0).Item("GCONUMERO"))
                Else
                    Result = New InterfaceResult With {.Message = "Radicacion Objeciones:ha ocurrido un error actualizando consecutivo, favor avisar al administrador del sistema", .Result = False}
                    Return Result
                End If
            End If




            'Creacion del comprobante      Cabecera
            strSql = "INSERT INTO " & NameContainer & "..CTNCOM" & Year(DateActual) & "(COMCODIGO,CTNTIPCOM,COMFECCOM,COMESTADO,COMDETALLE,COMOIDDOCU,COMNUMDOCU,COMOIDTYPE,COMELIMCOMP,COMCIERANU,COMGECGBATCH,OptimisticLockField,COMHOMNIIF,IFNCOMFIN,COMFFECHA,COMDETDOCORI)" &
            "VALUES( '" & NumeroGenerado & "','" & ConsecutivoComprobante & "',convert(varchar(20),'" & fecha & "'),0,'Radicacion Glosa Subsanable-Fra. " & factura & "  Glosas'," & ConsecutivoComprobante & ",'" & factura & "',0,NULL,0,NULL,0," & AplicaNiif & ",0,NULL,NULL)"
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


            Dim OIDDebitAccount As String
            Dim dtCuenta As DataTable
            strSql = "SELECT OID FROM " & NameContainer & "..CTNCUENTA  WHERE CUECODIGO = '" & DebitAccount & "'"
            dtCuenta = con.ExecuteCommand_Data(strSql)
            If dtCuenta.Rows.Count > 0 Then
                OIDDebitAccount = CInt(dtCuenta.Rows(0).Item("OID"))
            Else
                Result = New InterfaceResult With {.Message = "No se encontro ID de la cuenta" & DebitAccount & ", favor avisar al administrador del sistema", .Result = False}
                Return Result
            End If
            'End If
            dtCuenta = Nothing


            Dim OIDCreditAccount As String
            strSql = "SELECT OID FROM " & NameContainer & "..CTNCUENTA  WHERE CUECODIGO = '" & CreditAccount & "'"
            dtCuenta = con.ExecuteCommand_Data(strSql)
            If dtCuenta.Rows.Count > 0 Then
                OIDCreditAccount = CInt(dtCuenta.Rows(0).Item("OID"))
            Else
                Result = New InterfaceResult With {.Message = "No se encontro ID de la cuenta" & CreditAccount & ", favor avisar al administrador del sistema", .Result = False}
                Return Result
            End If
            'End If
            dtCuenta = Nothing


            'Detalle --movimiento credito
            strSql = "INSERT INTO " & NameContainer & "..CTNCOMD" & Year(DateActual) & "(CTNCOMCONC,CTNCUENTA,GENTERCER,CTNCENCOS,CMMVALDEB, CMMVALCRE, COMDETALLE, CTNCAURET, CMMESTCON, CMMFECCON,OptimisticLockField) " & _
            "VALUES( '" & Auto & "'," & OIDDebitAccount & ", " & OIDtercero & ",NULL,0,convert(varchar(50)," & ValorFac & "),'Radicacion Glosa Subsanable - Fra. " & factura & " Glosas',NULL,0,NULL,0)"
            ResultEje = con.ExecuteCommand(strSql)
            If ResultEje = False Then
                Result = New InterfaceResult With {.Message = "Radicacion Objeciones:ha ocurrido un error en la nota, creacion del detalle, favor avisar al administrador del sistema", .Result = False}
                Return Result
            End If

            strSql = "INSERT INTO " & NameContainer & "..CTNCOMD" & Year(DateActual) & "(CTNCOMCONC,CTNCUENTA,GENTERCER,CTNCENCOS,CMMVALDEB, CMMVALCRE, COMDETALLE, CTNCAURET, CMMESTCON, CMMFECCON,OptimisticLockField) " & _
            "VALUES( '" & Auto & "'," & OIDCreditAccount & ", " & OIDtercero & ",NULL,convert(varchar(50)," & ValorFac & "),0,'Radicacion Glosa Subsanable - Fra. " & factura & " Glosas',NULL,0,NULL,0)"
            ResultEje = con.ExecuteCommand(strSql)
            If ResultEje = False Then
                Result = New InterfaceResult With {.Message = "Radicacion Objeciones:ha ocurrido un error en la nota, creacion del detalle, favor avisar al administrador del sistema", .Result = False}
                Return Result
            End If


            con.IndigoTransaction.Commit()

            Validation.AuditInterface(IndigoEmpresa, intOpcion, NumeroGlosa, factura, 1, CodigoTipoDocumento, NumeroGenerado)

            Result = New InterfaceResult With {.Message = "Se Generaron los siguientes documentos: Comprobante Contable: " & CodigoTipoDocumento & " - " & NumeroGenerado & ", Empresa: " & NombreEmpresa, .Result = True, .Consecutive = CodigoTipoDocumento & " - " & NumeroGenerado, .Account = DebitAccount}

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

#Region "Aceptacion EAPB  NOTA CONTABLE"

    ''' <summary>
    ''' Aceptacion total por parte de la EAPB, generamos nota contable, (se reversa la nota contable de la radicacion) 
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
    Public Function AceptacionEAPBTotal(IndigoEmpresa As String, NumeroGlosa As String, factura As String, Tercero As String, NameContainer As String, ValorFac As Decimal, FechaFactura As Integer, intOpcion As String, User As String, plancodigo As String) As InterfaceResult Implements IInterfacePublicNET.AceptacionEAPBTotal
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
        If plancodigo = String.Empty Then
            Throw New Exception("Plan Codigo Vacio")
        End If


        Dim strSql As String = String.Empty
        Try

            'cargamos configuraciones de interface
            If con.sqlWebConection.State = ConnectionState.Closed Then
                con.sqlWebConection.Open()
            End If

            con.InTransaction = True
            con.IndigoTransaction = con.sqlWebConection.BeginTransaction(IsolationLevel.ReadCommitted, "Interfax crear Nota Contable")

            'leer parametro NIIF de DGH - esto solo aplica para las versiones NET de DGH sector publico
            strSql = "select top 1 IFPNIIFACT from " & NameContainer & ".dbo.IFNPARAME "
            Dim AplicaNiif As String
            Dim dtConfiDGH As DataTable = con.ExecuteCommand_Data(strSql)
            If dtConfiDGH.Rows.Count = 0 Then
                Result = New InterfaceResult With {.Message = "Radicacion Objeciones: No existe configuración de parametros contables en DGH" & NameContainer, .Result = False}
                Return Result
            Else
                If dtConfiDGH.Rows(0).Item("IFPNIIFACT") = "1" Then
                    AplicaNiif = "1"
                Else
                    AplicaNiif = "0"
                End If
            End If


            strSql = "SELECT top 1 id, CompanyName,DebitAccount FROM " & IndigoEmpresa & ".Glosas.GlosasParametersInterface WHERE ContainerName= '" & NameContainer & "'  "
            Dim dtConfi As DataTable = con.ExecuteCommand_Data(strSql)
            Dim DebitAccount As String
            Dim CreditAccount As String
            Dim NombreEmpresa As String
            If dtConfi.Rows.Count = 0 Then
                Result = New InterfaceResult With {.Message = "Tramite Glosa: No Existe configuracion de interface para el contenedor" & NameContainer, .Result = False}
                Return Result
            Else
                NombreEmpresa = dtConfi.Rows(0).Item("CompanyName").ToString
                If dtConfi.Rows(0).Item("DebitAccount").ToString = String.Empty Then
                    Result = New InterfaceResult With {.Message = "Radicacion Objeciones: La cuenta Debito para radicación no existe en las configuraciones de interfaces" & NameContainer, .Result = False}
                    Return Result
                Else
                    Dim ParametersInterfaceId As String
                    DebitAccount = dtConfi.Rows(0).Item("DebitAccount").ToString
                    ParametersInterfaceId = dtConfi.Rows(0).Item("Id").ToString
                    Dim dtAccountCreditPlan As DataTable
                    strSql = "select InvoiceRadicate as CreditAccount,denomination,plancode from " & IndigoEmpresa & ".Glosas.AccountSettingsNET_PublicMethod where ParametersInterfaceId = " & ParametersInterfaceId & " and plancode = '" & plancodigo & " '"
                    dtAccountCreditPlan = con.ExecuteCommand_Data(strSql)
                    If dtAccountCreditPlan IsNot Nothing AndAlso dtAccountCreditPlan.Rows.Count > 0 AndAlso dtAccountCreditPlan.Rows(0).Item("CreditAccount").ToString <> String.Empty Then
                        CreditAccount = dtAccountCreditPlan.Rows(0).Item("CreditAccount").ToString
                    Else
                        If dtAccountCreditPlan IsNot Nothing AndAlso dtAccountCreditPlan.Rows.Count > 0 Then
                            Result = New InterfaceResult With {.Message = "Tramite Glosa: La cuenta Credito para radicación no existe en las configuraciones de interfaces para el plan:" & dtAccountCreditPlan.Rows(0).Item("plancode").ToString & " - " & dtAccountCreditPlan.Rows(0).Item("denomination").ToString, .Result = False}
                            Return Result
                        Else
                            Result = New InterfaceResult With {.Message = "Tramite Glosa: No se encontraron cuenta con el plan: " & plancodigo, .Result = False}
                            Return Result
                        End If

                    End If
                End If
            End If

            Dim res As Boolean


            '  Tercero = con.fncConcatenar("0", Tercero, 15, ISQL.Direccion.Izquierda)
            'valida mes actual este abierto 
            'res  = Validation.ValidateMonthClose(eTypeInterface.NETPublic, Date.Now, NameContainer)
            'If res = False Then
            '    Result = New InterfaceResult With {.Message = "Tramite Glosa: El mes " & Month(Date.Now).ToString & " ya esta cerrado, no se puede continuar" & NameContainer, .Result = False}
            '    Return Result
            'End If



            'validacion de la cuenta debito
            res = Validation.ValidateAccount(eTypeInterface.NETPublic, NameContainer, DebitAccount)
            If res = False Then
                Result = New InterfaceResult With {.Message = "Tramite Glosa: La cuenta " & DebitAccount & "No Existe", .Result = False}
                Return Result
            End If


            'validacion de la cuenta credito
            res = Validation.ValidateAccount(eTypeInterface.NETPublic, NameContainer, CreditAccount)
            If res = False Then
                Result = New InterfaceResult With {.Message = "Tramite Glosa: La cuenta " & CreditAccount & "No Existe", .Result = False}
                Return Result
            End If


            'Actualizo estado de factura en cartera
            'strSql = "UPDATE " & NameContainer & "..CRNCXC SET CXCESTCAR = '4' WHERE CXCDOCUME= '" & factura & "' "
            'res = con.ExecuteCommand(strSql)
            'If res = False Then
            '    Result = New InterfaceResult With {.Message = "Tramite Glosa: ha ocurrido un error actualizando estado de cartera", .Result = False}
            '    Return Result
            'End If

            'Busco el consecutivo de comprobantes
            Dim OIDComprobante As String
            Dim CodigoTipoDocumento As String
            strSql = "SELECT top 1   CTNTIPCOM5 FROM " & NameContainer & "..CRNPARSIS"
            Dim dtConsecutivoComprobante As DataTable = con.ExecuteCommand_Data(strSql)
            If dtConsecutivoComprobante.Rows.Count = 0 Then
                Result = New InterfaceResult With {.Message = "Tramite Glosa:No esta configurado los consecutivo para comprobantes o ha ocurrido un error, favor avisar al administrador del sistema", .Result = False}
                Return Result
            Else
                OIDComprobante = dtConsecutivoComprobante.Rows(0).Item("CTNTIPCOM5").ToString.TrimEnd
            End If

            Dim NumeroGenerado As Integer
            'busco consecutivo comprobante diario mediante el consecutivo de comprobantes
            Dim ConsecutivoNumcon As String
            strSql = "SELECT top 1  GENCONSEC, tccodigo FROM " & NameContainer & "..CTNTIPCOM WHERE OID='" & OIDComprobante & "'"
            Dim dtConsecutivoNumcon As DataTable = con.ExecuteCommand_Data(strSql)
            If dtConsecutivoNumcon.Rows.Count = 0 Then
                Result = New InterfaceResult With {.Message = "Tramite Glosa:No esta configurado los consecutivo para comprobantes diario o ha ocurrido un error, favor avisar al administrador del sistema", .Result = False}
                Return Result
            Else
                ConsecutivoNumcon = dtConsecutivoNumcon.Rows(0).Item("GENCONSEC").ToString.TrimEnd
                CodigoTipoDocumento = dtConsecutivoNumcon.Rows(0).Item("tccodigo").ToString.TrimEnd
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
                    Result = New InterfaceResult With {.Message = "Tramite Glosa:ha ocurrido un error actualizando consecutivo, favor avisar al administrador del sistema", .Result = False}
                    Return Result
                End If
            End If



            Dim DateActual As Date = Date.Now
            Dim fecha As String
            fecha = Format(DateActual, "yyyyMMdd hh:mm:ss")
            Dim ResultEje As Boolean
            'Creacion del comprobante      Cabecera
            strSql = "INSERT INTO " & NameContainer & "..CTNCOM" & Year(DateActual) & "(COMCODIGO,CTNTIPCOM,COMFECCOM,COMESTADO,COMDETALLE,COMOIDDOCU,COMNUMDOCU,COMOIDTYPE,COMELIMCOMP,COMCIERANU,COMGECGBATCH,OptimisticLockField,COMHOMNIIF,IFNCOMFIN,COMFFECHA,COMDETDOCORI)" &
            "VALUES( '" & NumeroGenerado & "','" & OIDComprobante & "',convert(varchar(20),'" & fecha & "'),0,'Radicacion Glosa Subsanable-Fra. " & factura & "  Glosas'," & OIDComprobante & ",'" & factura & "',0,NULL,0,NULL,0," & AplicaNiif & ",0,NULL,NULL)"
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
                Result = New InterfaceResult With {.Message = "Tramite Glosa: No se encontro ID del Tercero" & Tercero & ", favor avisar al administrador del sistema", .Result = False}
                Return Result
            End If
            dtTercero = Nothing


            Dim OIDDebitAccount As String
            Dim dtCuenta As DataTable
            strSql = "SELECT OID FROM " & NameContainer & "..CTNCUENTA  WHERE CUECODIGO = '" & DebitAccount & "'"
            dtCuenta = con.ExecuteCommand_Data(strSql)
            If dtCuenta.Rows.Count > 0 Then
                OIDDebitAccount = CInt(dtCuenta.Rows(0).Item("OID"))
            Else
                Result = New InterfaceResult With {.Message = "Tramite Glosa: No se encontro ID de la cuenta" & DebitAccount & ", favor avisar al administrador del sistema", .Result = False}
                Return Result
            End If
            'End If
            dtCuenta = Nothing


            Dim OIDCreditAccount As String
            strSql = "SELECT OID FROM " & NameContainer & "..CTNCUENTA  WHERE CUECODIGO = '" & CreditAccount & "'"
            dtCuenta = con.ExecuteCommand_Data(strSql)
            If dtCuenta.Rows.Count > 0 Then
                OIDCreditAccount = CInt(dtCuenta.Rows(0).Item("OID"))
            Else
                Result = New InterfaceResult With {.Message = "Tramite Glosa: No se encontro ID de la cuenta" & CreditAccount & ", favor avisar al administrador del sistema", .Result = False}
                Return Result
            End If
            'End If
            dtCuenta = Nothing


            'Detalle  	--movimiento debito
            strSql = "INSERT INTO " & NameContainer & "..CTNCOMD" & Year(DateActual) & "(CTNCOMCONC,CTNCUENTA,GENTERCER,CTNCENCOS,CMMVALDEB, CMMVALCRE, COMDETALLE, CTNCAURET, CMMESTCON, CMMFECCON,OptimisticLockField) " & _
            "VALUES( '" & Auto & "'," & OIDCreditAccount & ", " & OIDtercero & ",NULL,0,convert(varchar(50)," & ValorFac & "),'Aceptacion Glosa - Fra. " & factura & " Glosas',NULL,0,NULL,0)"
            ResultEje = con.ExecuteCommand(strSql)
            If ResultEje = False Then
                Result = New InterfaceResult With {.Message = "Tramite Glosa: ha ocurrido un error en la nota, creacion del detalle, favor avisar al administrador del sistema", .Result = False}
                Return Result
            End If


            'Detalle  	--movimiento credito
            strSql = "INSERT INTO " & NameContainer & "..CTNCOMD" & Year(DateActual) & "(CTNCOMCONC,CTNCUENTA,GENTERCER,CTNCENCOS,CMMVALDEB, CMMVALCRE, COMDETALLE, CTNCAURET, CMMESTCON, CMMFECCON,OptimisticLockField) " & _
            "VALUES( '" & Auto & "'," & OIDDebitAccount & ", " & OIDtercero & ",NULL,convert(varchar(50)," & ValorFac & "),0,'Aceptacion Glosa - Fra. " & factura & " Glosas',NULL,0,NULL,0)"
            ResultEje = con.ExecuteCommand(strSql)
            If ResultEje = False Then
                Result = New InterfaceResult With {.Message = "Tramite Glosa: ha ocurrido un error en la nota, creacion del detalle, favor avisar al administrador del sistema", .Result = False}
                Return Result
            End If


            con.IndigoTransaction.Commit()

            Validation.AuditInterface(IndigoEmpresa, intOpcion, NumeroGlosa, factura, 1, OIDComprobante, NumeroGenerado)

            Result = New InterfaceResult With {.Message = "Se Generaron los siguientes documentos: Comprobante Contable: " & CodigoTipoDocumento & " - " & NumeroGenerado & ", Empresa: " & NombreEmpresa, .Result = True, .Consecutive = CodigoTipoDocumento & " - " & NumeroGenerado, .Account = DebitAccount}

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

#Region "Aceptacion EAPB - IPS   (por el valor aceptado por la IPS = NOTA CREDITO   -   por el valor aceptado por la EAPB = NOTA CONTABLE)"


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
    Public Function AcceptanceIPS(IndigoEmpresa As String, NumeroGlosa As String, factura As String, Tercero As String, NameContainer As String, ValorFac As Decimal, FechaFactura As Integer, intOpcion As String, User As String, AfectaServicio As Boolean, plancodigo As String, Optional VAlorAceptadoEAPB As Decimal = 0, Optional StatePortfolioDGH As String = "") As InterfaceResult Implements IInterfacePublicNET.AcceptanceIPS
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
        If plancodigo = String.Empty Then
            Throw New ArgumentNullException("Plan codigo vacio")
        End If

        Dim strSql As String = String.Empty
        Try

            'cargamos configuraciones de interface
            If con.sqlWebConection.State = ConnectionState.Closed Then
                con.sqlWebConection.Open()
            End If

            con.InTransaction = True
            con.IndigoTransaction = con.sqlWebConection.BeginTransaction(IsolationLevel.ReadCommitted, "Interfax crear Nota credito")

            Dim AcceptanceObjectionsConcept As String
            Dim AcceptanceObjectionsConceptPast As String
            Dim DebitAccount As String
            Dim CreditAccount As String
            strSql = "SELECT top 1 id, CompanyName,AcceptanceObjectionsConcept,AcceptanceObjectionsConceptPast,DebitAccount FROM " & IndigoEmpresa & ".Glosas.GlosasParametersInterface WHERE ContainerName= '" & NameContainer & "'  "
            Dim dtConfi As DataTable = con.ExecuteCommand_Data(strSql)
            Dim NombreEmpresa As String
            If dtConfi.Rows.Count = 0 Then
                Result = New InterfaceResult With {.Message = "No Existe configuracion de interface para el contenedor" & NameContainer, .Result = False}
                Return Result
            Else
                NombreEmpresa = dtConfi.Rows(0).Item("CompanyName").ToString
                If dtConfi.Rows(0).Item("AcceptanceObjectionsConcept").ToString = String.Empty Or dtConfi.Rows(0).Item("AcceptanceObjectionsConceptPast").ToString = String.Empty Then
                    Result = New InterfaceResult With {.Message = "Aceptacion IPS: no existe en las configuraciones de interfaces los conceptos para aceptacion (Concepto Aceptacion general - Vigencia Anteriores)" & NameContainer, .Result = False}
                    Return Result
                Else
                    AcceptanceObjectionsConcept = dtConfi.Rows(0).Item("AcceptanceObjectionsConcept").ToString
                    AcceptanceObjectionsConceptPast = dtConfi.Rows(0).Item("AcceptanceObjectionsConceptPast").ToString
                End If

                If dtConfi.Rows(0).Item("DebitAccount").ToString = String.Empty Then
                    Result = New InterfaceResult With {.Message = "Aceptacion IPS: no existe en las cuentas credito o debito" & NameContainer, .Result = False}
                    Return Result
                Else
                    Dim ParametersInterfaceId As String
                    DebitAccount = dtConfi.Rows(0).Item("DebitAccount").ToString
                    ParametersInterfaceId = dtConfi.Rows(0).Item("Id").ToString
                    Dim dtAccountCreditPlan As DataTable
                    strSql = "select InvoiceRadicate as CreditAccount,denomination,plancode from " & IndigoEmpresa & ".Glosas.AccountSettingsNET_PublicMethod where ParametersInterfaceId = " & ParametersInterfaceId & " and plancode = '" & plancodigo & " '"
                    dtAccountCreditPlan = con.ExecuteCommand_Data(strSql)
                    If dtAccountCreditPlan IsNot Nothing AndAlso dtAccountCreditPlan.Rows.Count > 0 AndAlso dtAccountCreditPlan.Rows(0).Item("CreditAccount").ToString <> String.Empty Then
                        CreditAccount = dtAccountCreditPlan.Rows(0).Item("CreditAccount").ToString
                    Else
                        Result = New InterfaceResult With {.Message = "Aceptacion IPS: La cuenta Credito para radicación no existe en las configuraciones de interfaces para el plan:" & dtAccountCreditPlan.Rows(0).Item("plancode").ToString & " - " & dtAccountCreditPlan.Rows(0).Item("denomination").ToString, .Result = False}
                        Return Result
                    End If
                End If
            End If

            'cargamos cuenta apartir de tercero y factura, Validamos Existencia de Cuenta

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
                    Dim resInteger As Integer = Validation.ValidateAccount(eTypeInterface.NETPrivate, NameContainer, cuentaCartera)
                    If resInteger = 0 Then
                        Result = New InterfaceResult With {.Message = "No existe la cuenta " & cuentaCartera & " de la factura en DGH, favor avisar al administrador del sistema", .Result = False}
                        Return Result
                    End If
                Else
                    Result = New InterfaceResult With {.Message = "No existe la factura en ERP, favor avisar al administrador del sistema", .Result = False}
                    Return Result
                End If
            End If
            dtCuenta = Nothing

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


            'de acuerdo a la fecha de la factura tomamos concepto de aceptacion general o de vigencia anteriores
            Dim Concept As String = String.Empty
            If FechaFactura = Year(Date.Now) Then
                Concept = AcceptanceObjectionsConcept
            Else
                Concept = AcceptanceObjectionsConceptPast
            End If

            Dim resultBool As Boolean
            Dim dtAccountConcept As DataTable
            Dim AccountConcept As String
            'validamos que los concepto de aceptacion general exista
            resultBool = Validation.ValidateConcept(eTypeInterface.NETPublic, NameContainer, Concept, "")
            If resultBool = False Then
                Result = New InterfaceResult With {.Message = "Aceptacion IPS: El concepto para aceptaciones ya no existe, o no cumple con las medidas establecidas en los parametos contables, favor avisar al administrador del sistema", .Result = False}
                Return Result
            Else
                'consultamos el codigo de cuenta para el concepto de "RectifiableGlosa"
                strSql = "SELECT cue.CUECODIGO  FROM " & NameContainer & "..crNConNOT concep INNER JOIN " & NameContainer & "..CTNCUENTA Cue on Concep.CTNCUENTA = cue.OID  WHERE concep.CONCODIGO= '" & Concept & "'"
                Dim dtCuentaConcepto As DataTable = con.ExecuteCommand_Data(strSql)
                If dtCuentaConcepto.Rows.Count = 0 Then
                    Result = New InterfaceResult With {.Message = " no existe cuenta para el concepto " & Concept & " , no se puede continuar!!", .Result = False}
                    Return Result
                Else
                    AccountConcept = dtCuentaConcepto.Rows(0).Item("CUECODIGO").ToString
                End If
            End If
            dtAccountConcept = Nothing


            'validacion de la cuenta del concepto exista
            resultBool = Validation.ValidateAccount(eTypeInterface.NETPublic, NameContainer, AccountConcept)
            If resultBool = False Then
                Result = New InterfaceResult With {.Message = "Aceptacion IPS: La cuenta " & AccountConcept & "No Existe", .Result = False}
                Return Result
            End If

            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            'CREAMOS LA NOTA CREDITO DE ACEPTACION 
            '
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim resultCreateCreditNOte As InterfaceResult = Me.CreateCreditNote(NameContainer, Tercero, factura, ValorFac, User, cuentaCartera, AcceptanceObjectionsConcept, AccountConcept, True)
            If resultCreateCreditNOte.Result = False Then
                Return resultCreateCreditNOte
            End If

           

           

            Dim DateActual As Date = Date.Now
            Dim fecha As String
            fecha = Format(DateActual, "yyyyMMdd hh:mm:ss")
            Dim ResultEje As Boolean
            Dim Comment As String = String.Empty

            If StatePortfolioDGH <> String.Empty Then

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

                If StatePortfolioDGH = "3" Then
                    Comment = "Mod. Genesis: La CxC paso de estado RadicadaEntidad al estado Objetada"
                ElseIf StatePortfolioDGH = "4" Then
                    Comment = "Mod. Genesis: La CxC paso de estado Objetada al estado Contestada_Radicada"
                End If

                'Creacion de historico de estados
                Dim AutoCRNCXCEST As Integer
                strSql = "INSERT INTO " & NameContainer & "..CRNCXCEST([CRNCXC],[CCEFECHA],[CCEESTADO],[CRNDOCUME],[GENUSUARIO],[CCEOBSERV],[CCEFECEST],[OptimisticLockField],[CCEDESCON])" &
                "VALUES('" & OIDFactura & "',convert(varchar(20),'" & fecha & "'),'" & StatePortfolioDGH & "',NULL,'" & OIDusuario & "','" & Comment & "',convert(varchar(20),'" & fecha & "'),0,0)"
                ResultEje = con.ExecuteCommand(strSql)
                Dim SQL As New SqlDataAdapter("SELECT @@IDENTITY", con.sqlWebConection)
                With SQL
                    .SelectCommand.CommandTimeout = 90
                    .SelectCommand.Transaction = con.IndigoTransaction
                    .SelectCommand.CommandType = CommandType.Text
                    AutoCRNCXCEST = .SelectCommand.ExecuteScalar.ToString
                End With


                Dim res As Boolean
                'Actualizo estado de factura en cartera
                strSql = "UPDATE " & NameContainer & "..CRNCXC SET CXCESTCAR = '" & StatePortfolioDGH & "', CXCESTCACT = '" & AutoCRNCXCEST & "' WHERE CXCDOCUME= '" & factura & "' "
                res = con.ExecuteCommand(strSql)
                If res = False Then
                    Result = New InterfaceResult With {.Message = "Aceptacion IPS: ha ocurrido un error actualizando estado de cartera", .Result = False}
                    Return Result
                End If
            End If


            'si la EAPB no ACEPTA NADA 
            If VAlorAceptadoEAPB = 0 Then
                con.IndigoTransaction.Commit()
                Validation.AuditInterface(IndigoEmpresa, intOpcion, NumeroGlosa, factura, 2, AccountConcept, resultCreateCreditNOte.Consecutive)
                con.sqlWebConection.Close()
                Result = New InterfaceResult With {.Message = "Se Generaron los siguientes documentos: Nota Credito: " & resultCreateCreditNOte.Consecutive & " , Empresa: " & NombreEmpresa, .Result = True}
                Return Result
            End If

            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            'CREACION DE LA NOTA CONTABLE
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            'valida mes actual este abierto 
            'resultBool = Validation.ValidateMonthClose(eTypeInterface.NETPublic, Date.Now, NameContainer)
            'If resultBool = False Then
            '    Result = New InterfaceResult With {.Message = "Aceptacion IPS: El mes " & Month(Date.Now).ToString & " ya esta cerrado, no se puede continuar" & NameContainer, .Result = False}
            '    Return Result
            'End If

            'leer parametro NIIF de DGH - esto solo aplica para las versiones NET de DGH sector publico
            strSql = "select top 1 IFPNIIFACT from " & NameContainer & ".dbo.IFNPARAME "
            Dim AplicaNiif As String
            Dim dtConfiDGH As DataTable = con.ExecuteCommand_Data(strSql)
            If dtConfiDGH.Rows.Count = 0 Then
                Result = New InterfaceResult With {.Message = "Radicacion Objeciones: No existe configuración de parametros contables en DGH" & NameContainer, .Result = False}
                Return Result
            Else
                If dtConfiDGH.Rows(0).Item("IFPNIIFACT") = "1" Then
                    AplicaNiif = "1"
                Else
                    AplicaNiif = "0"
                End If
            End If

            'validacion de la cuenta debito
            resultBool = Validation.ValidateAccount(eTypeInterface.NETPublic, NameContainer, DebitAccount)
            If resultBool = False Then
                Result = New InterfaceResult With {.Message = "Aceptacion IPS: La cuenta " & DebitAccount & "No Existe", .Result = False}
                Return Result
            End If


            'validacion de la cuenta credito
            resultBool = Validation.ValidateAccount(eTypeInterface.NETPublic, NameContainer, CreditAccount)
            If resultBool = False Then
                Result = New InterfaceResult With {.Message = "Aceptacion IPS: La cuenta " & CreditAccount & "No Existe", .Result = False}
                Return Result
            End If


            'Busco el consecutivo de comprobantes
            Dim ConsecutivoComprobante As String
            strSql = "SELECT top 1   CTNTIPCOM5 FROM " & NameContainer & "..CRNPARSIS"
            Dim dtConsecutivoComprobante As DataTable = con.ExecuteCommand_Data(strSql)
            If dtConsecutivoComprobante.Rows.Count = 0 Then
                Result = New InterfaceResult With {.Message = "Aceptacion IPS: No esta configurado los consecutivo para comprobantes o ha ocurrido un error, favor avisar al administrador del sistema", .Result = False}
                Return Result
            Else
                ConsecutivoComprobante = dtConsecutivoComprobante.Rows(0).Item("CTNTIPCOM5").ToString.TrimEnd
            End If

            Dim NumeroGenerado As Integer
            'busco consecutivo comprobante diario mediante el consecutivo de comprobantes
            Dim OIDComprobante As String
            Dim CodigoTipoDocumento As String
            strSql = "SELECT top 1  GENCONSEC, tccodigo FROM " & NameContainer & "..CTNTIPCOM WHERE OID='" & ConsecutivoComprobante & "'"
            Dim dtConsecutivoNumcon As DataTable = con.ExecuteCommand_Data(strSql)
            If dtConsecutivoNumcon.Rows.Count = 0 Then
                Result = New InterfaceResult With {.Message = "Aceptacion IPS:No esta configurado los consecutivo para comprobantes diario o ha ocurrido un error, favor avisar al administrador del sistema", .Result = False}
                Return Result
            Else
                OIDComprobante = dtConsecutivoNumcon.Rows(0).Item("GENCONSEC").ToString.TrimEnd
                CodigoTipoDocumento = dtConsecutivoNumcon.Rows(0).Item("tccodigo").ToString.TrimEnd
                'Actualiza el consecutive del comprobante
                strSql = "UPDATE " & NameContainer & "..geNconsec SET GCONUMERO = GCONUMERO +1  WHERE OID='" & OIDComprobante & "'"
                Dim resActualizarConsecutivo As Boolean = con.ExecuteCommand(strSql)
                If resActualizarConsecutivo = True Then
                    'consultamo 
                    Dim dtConseIncrementa As DataTable
                    strSql = " SELECT top 1 GCONUMERO FROM " & NameContainer & "..geNconsec WHERE OID='" & OIDComprobante & "'"
                    dtConseIncrementa = con.ExecuteCommand_Data(strSql)
                    ' ConsecutivoNumcon = ConsecutivoNumcon + 1
                    NumeroGenerado = CInt(dtConseIncrementa.Rows(0).Item("GCONUMERO"))
                Else
                    Result = New InterfaceResult With {.Message = "Aceptacion IPS:ha ocurrido un error actualizando consecutivo, favor avisar al administrador del sistema", .Result = False}
                    Return Result
                End If
            End If



            'Creacion del comprobante      Cabecera
            strSql = "INSERT INTO " & NameContainer & "..CTNCOM" & Year(DateActual) & "(COMCODIGO,CTNTIPCOM,COMFECCOM,COMESTADO,COMDETALLE,COMOIDDOCU,COMNUMDOCU,COMOIDTYPE,COMELIMCOMP,COMCIERANU,COMGECGBATCH,OptimisticLockField,COMHOMNIIF,IFNCOMFIN,COMFFECHA,COMDETDOCORI)" &
            "VALUES( '" & NumeroGenerado & "','" & ConsecutivoComprobante & "',convert(varchar(20),'" & fecha & "'),0,'Radicacion Glosa Subsanable-Fra. " & factura & "  Glosas'," & ConsecutivoComprobante & ",'" & factura & "',0,NULL,0,NULL,0," & AplicaNiif & ",0,NULL,NULL)"
            ResultEje = con.ExecuteCommand(strSql)
            Dim Auto As String
            Dim SQLComprobantes As New SqlDataAdapter("SELECT @@IDENTITY", con.sqlWebConection)
            With SQLComprobantes
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
                Result = New InterfaceResult With {.Message = "Aceptacion IPS: No se encontro ID del Tercero" & Tercero & ", favor avisar al administrador del sistema", .Result = False}
                Return Result
            End If
            dtTercero = Nothing


            Dim OIDDebitAccount As String
            strSql = "SELECT OID FROM " & NameContainer & "..CTNCUENTA  WHERE CUECODIGO = '" & DebitAccount & "'"
            dtCuenta = con.ExecuteCommand_Data(strSql)
            If dtCuenta.Rows.Count > 0 Then
                OIDDebitAccount = CInt(dtCuenta.Rows(0).Item("OID"))
            Else
                Result = New InterfaceResult With {.Message = "Aceptacion IPS: No se encontro ID de la cuenta" & DebitAccount & ", favor avisar al administrador del sistema", .Result = False}
                Return Result
            End If
            'End If
            dtCuenta = Nothing


            Dim OIDCreditAccount As String
            strSql = "SELECT OID FROM " & NameContainer & "..CTNCUENTA  WHERE CUECODIGO = '" & CreditAccount & "'"
            dtCuenta = con.ExecuteCommand_Data(strSql)
            If dtCuenta.Rows.Count > 0 Then
                OIDCreditAccount = CInt(dtCuenta.Rows(0).Item("OID"))
            Else
                Result = New InterfaceResult With {.Message = "Aceptacion IPS: No se encontro ID de la cuenta" & CreditAccount & ", favor avisar al administrador del sistema", .Result = False}
                Return Result
            End If
            'End If
            dtCuenta = Nothing


            'Detalle  	--movimiento debito
            strSql = "INSERT INTO " & NameContainer & "..CTNCOMD" & Year(DateActual) & "(CTNCOMCONC,CTNCUENTA,GENTERCER,CTNCENCOS,CMMVALDEB, CMMVALCRE, COMDETALLE, CTNCAURET, CMMESTCON, CMMFECCON,OptimisticLockField) " & _
            "VALUES( '" & Auto & "'," & OIDCreditAccount & ", " & OIDtercero & ",NULL,0,convert(varchar(50)," & VAlorAceptadoEAPB & "),'Aceptacion Glosa - Fra. " & factura & " Glosas',NULL,0,NULL,0)"
            ResultEje = con.ExecuteCommand(strSql)
            If ResultEje = False Then
                Result = New InterfaceResult With {.Message = "Aceptacion IPS: ha ocurrido un error en la nota, creacion del detalle, favor avisar al administrador del sistema", .Result = False}
                Return Result
            End If


            'Detalle  	--movimiento credito
            strSql = "INSERT INTO " & NameContainer & "..CTNCOMD" & Year(DateActual) & "(CTNCOMCONC,CTNCUENTA,GENTERCER,CTNCENCOS,CMMVALDEB, CMMVALCRE, COMDETALLE, CTNCAURET, CMMESTCON, CMMFECCON,OptimisticLockField) " & _
            "VALUES( '" & Auto & "'," & OIDDebitAccount & ", " & OIDtercero & ",NULL,convert(varchar(50)," & VAlorAceptadoEAPB & "),0,'Aceptacion Glosa - Fra. " & factura & " Glosas',NULL,0,NULL,0)"
            ResultEje = con.ExecuteCommand(strSql)
            If ResultEje = False Then
                Result = New InterfaceResult With {.Message = "Aceptacion IPS: ha ocurrido un error en la nota, creacion del detalle, favor avisar al administrador del sistema", .Result = False}
                Return Result
            End If


            con.IndigoTransaction.Commit()

            Validation.AuditInterface(IndigoEmpresa, intOpcion, NumeroGlosa, factura, 1, ConsecutivoComprobante, NumeroGenerado)

            Result = New InterfaceResult With {.Message = "Se Generaron los siguientes documentos: Comprobante Contable: " & CodigoTipoDocumento & " - " & NumeroGenerado & " Nota Credito: " & resultCreateCreditNOte.Consecutive & " , Empresa: " & NombreEmpresa, .Result = True, .Consecutive = CodigoTipoDocumento & " - " & NumeroGenerado}
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
    Public Function CreateCreditNote(NameContainer As String, Tercero As String, factura As String, ValorFac As Decimal, User As String, Cuenta As String, Concepto As String, CuentaConcepto As String, Optional ByVal EjecutaAceptacionEAPBUnicaTransaccion As Boolean = False) As InterfaceResult Implements IInterfacePublicNET.CreateCreditNote
        Dim Result As New InterfaceResult
        Dim strSql As String = String.Empty
        Try

            If con.sqlWebConection.State = ConnectionState.Closed Then
                con.sqlWebConection.Open()
            End If

            If EjecutaAceptacionEAPBUnicaTransaccion = False Then
                con.InTransaction = True
                con.IndigoTransaction = con.sqlWebConection.BeginTransaction(IsolationLevel.ReadCommitted, "Interfax crear Nota Credito")
            End If
        

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

            If EjecutaAceptacionEAPBUnicaTransaccion = False Then
                con.IndigoTransaction.Commit()
            End If


            Result = New InterfaceResult With {.Message = "OK", .Result = True, .Consecutive = ConsecutiveNumber}
        Catch ex As Exception
            con.IndigoTransaction.Rollback()
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

    Public Function Devolucion(IndigoEmpresa As String, ByVal listRadicated As List(Of GlosaDevolutionsReceptionD), NameContainer As String, intOpcion As String, User As String) As List(Of InterfaceResult) Implements IInterfacePublicNET.Devolucion

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


            'leer parametro NIIF de DGH - esto solo aplica para las versiones NET de DGH sector publico
            strSql = "select top 1 IFPNIIFACT from " & NameContainer & ".dbo.IFNPARAME "
            Dim AplicaNiif As String
            Dim dtConfiDGH As DataTable = con.ExecuteCommand_Data(strSql)
            If dtConfiDGH.Rows.Count = 0 Then
                ListError.Add(New InterfaceResult With {.Message = "Radicacion Objeciones: No existe configuración de parametros contables en DGH" & NameContainer, .Result = False})
                con.IndigoTransaction.Rollback()
                Return ListError
            Else
                If dtConfiDGH.Rows(0).Item("IFPNIIFACT") = "1" Then
                    AplicaNiif = "1"
                Else
                    AplicaNiif = "0"
                End If
            End If

            Dim Tercero As String = String.Empty
            If listRadicated.Count > 0 AndAlso listRadicated(0).GlosaDevolutionsReceptionC IsNot Nothing AndAlso listRadicated(0).GlosaDevolutionsReceptionC.Customer IsNot Nothing Then
                Tercero = listRadicated(0).GlosaDevolutionsReceptionC.Customer.Nit.Trim
            Else
                con.IndigoTransaction.Rollback()
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
            Dim OIDTipComprobante As Integer
            strSql = "SELECT top 1  OID,GENCONSEC, TCCODIGO FROM " & NameContainer & "..CTNTIPCOM WHERE TCCODIGO='" & NumerodeNotaContable & "'"
            Dim dtConsecutivoComprobante As DataTable = con.ExecuteCommand_Data(strSql)
            If dtConsecutivoComprobante.Rows.Count = 0 Then
                FlagContolGenerateAccountNote = False
                ListError.Add(New InterfaceResult With {.Message = "Devolución Factura:No esta configurado los consecutivo para comprobantes diario de radicacion de facturas o ha ocurrido un error, favor avisar al administrador del sistema", .Result = False})
            Else
                ConsecutivoNumcon = dtConsecutivoComprobante.Rows(0).Item("GENCONSEC")
                OIDTipComprobante = dtConsecutivoComprobante.Rows(0).Item("OID")
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
                    ListError.Add(New InterfaceResult With {.Message = "Devolución Factura:ha ocurrido un error actualizando consecutivo, favor avisar al administrador del sistema", .Result = False})
                End If
            End If


            Dim InvoiceNotRadicate As String = String.Empty
            'GENERACION DE COMPROBANTES CONTABLE Y ACTUALIZACION DE CUENTA Y ESTADO CARTERA ERP
            For Each itemD As GlosaDevolutionsReceptionD In listRadicated
                FlagContolGenerateAccountNote = True

                Factura = itemD.InvoiceNumber

                Dim dtAccountCreditContractPlan As DataTable
                strSql = "select AccountNotRadicate,AccountRadicate, plancode,ContractCode from " & IndigoEmpresa & ".Glosas.Contract_Public where ContractCode = '" & itemD.ContractCode & "' and plancode = '" & itemD.PlanCode & " '"
                dtAccountCreditContractPlan = con.ExecuteCommand_Data(strSql)
                If dtAccountCreditContractPlan IsNot Nothing AndAlso dtAccountCreditContractPlan.Rows.Count > 0 AndAlso dtAccountCreditContractPlan.Rows(0).Item("AccountNotRadicate").ToString <> String.Empty Then
                    InvoiceNotRadicate = dtAccountCreditContractPlan.Rows(0).Item("AccountNotRadicate").ToString
                Else
                    FlagContolGenerateAccountNote = False
                    ListError.Add(New InterfaceResult With {.Message = "Devolución Factura: La cuenta Credito para radicación no existe en las configuraciones de interfaces para el plan:" & dtAccountCreditContractPlan.Rows(0).Item("plancode").ToString & " - " & dtAccountCreditContractPlan.Rows(0).Item("ContractCode").ToString, .Result = False})
                End If


                'id de numero de radicado
                Dim dtOIDRadicateOffice As DataTable
                strSql = "select C.OID from " & NameContainer & "..CRNDOCUME DOC INNER JOIN " & NameContainer & "..CRNRADFACC C on C.OID =DOC.OID where DOC.CDCONSEC = '" & itemD.RadicatedNumber & "'"
                dtOIDRadicateOffice = con.ExecuteCommand_Data(strSql)
                Dim OIDRadicate As Integer
                If dtOIDRadicateOffice IsNot Nothing AndAlso dtOIDRadicateOffice.Rows.Count > 0 AndAlso dtOIDRadicateOffice.Rows(0).Item("OID").ToString <> String.Empty Then
                    OIDRadicate = dtOIDRadicateOffice.Rows(0).Item("OID")
                Else
                    FlagContolGenerateAccountNote = False
                    ListError.Add(New InterfaceResult With {.Message = "Devolución Factura: No se encontro radicado '" & itemD.RadicatedNumber & "'", .Result = False})
                End If


                'id de cartera
                Dim dtOIDcartera As DataTable
                Dim OIDcartera As Integer
                strSql = "Select OID from " & NameContainer & "..CRNCXC where cxcDocume = '" & itemD.InvoiceNumber & "'"
                dtOIDcartera = con.ExecuteCommand_Data(strSql)
                If dtOIDcartera IsNot Nothing AndAlso dtOIDcartera.Rows.Count > 0 AndAlso dtAccountCreditContractPlan.Rows.Count > 0 Then
                    OIDcartera = dtOIDcartera.Rows(0).Item("OID")
                Else
                    FlagContolGenerateAccountNote = False
                    ListError.Add(New InterfaceResult With {.Message = "Devolución Factura: No se encontro factura '" & itemD.InvoiceNumber & "'", .Result = False})
                End If

                'LIMPIAMOS LA RELACION EN CARTERA PARA PROCEDER A ELIMIBNAR FACTURA DEL RADICADO
                strSql = "UPDATE " & NameContainer & "..CRNCXC SET CRNRADFACD = NULL WHERE OID =  " & OIDcartera & "  "
                ResultEje = con.ExecuteCommand(strSql)


                'eliminamos factura de radicado en dinamica
                strSql = " DELETE FROM  " & NameContainer & "..CRNRADFACD where CRNRADFACC = " & OIDRadicate & " AND CRNCXC = " & OIDcartera & " "
                ResultEje = con.ExecuteCommand(strSql)


                'cuenta de cartera PortfoliGlosa
                Dim dtCuentaCartera As DataTable
                Dim cuentaCarteraGlosa As String = String.Empty
                strSql = "select CTNCUENTA  from  " & NameContainer & "..CRNCXC where cxcDocume =  '" & Factura & "'"   'factura sin confirmar, traemos cuenta de cartera ERP
                dtCuentaCartera = con.ExecuteCommand_Data(strSql)
                If dtCuentaCartera.Rows.Count > 0 Then
                    cuentaCarteraGlosa = dtCuentaCartera.Rows(0).Item("CTNCUENTA").ToString
                Else
                    FlagContolGenerateAccountNote = False
                    ListError.Add(New InterfaceResult With {.Message = "Devolución Factura: no existe cuenta en cartera ERP para la factura " & Factura & ", no se puede continuar!!", .Result = False})
                End If




                Dim DateActual As Date = Date.Now
                Dim fecha As String
                fecha = Format(DateActual, "yyyyMMdd hh:mm:ss")
                'Creacion del comprobante      Cabecera
                strSql = "INSERT INTO " & NameContainer & "..CTNCOM" & Year(DateActual) & "(COMCODIGO,CTNTIPCOM,COMFECCOM,COMESTADO,COMDETALLE,COMOIDDOCU,COMNUMDOCU,COMOIDTYPE,COMELIMCOMP,COMCIERANU,COMGECGBATCH,OptimisticLockField,COMHOMNIIF,IFNCOMFIN,COMFFECHA,COMDETDOCORI)" &
                "VALUES( '" & ConsecutivoNumcon & "','" & OIDTipComprobante & "',convert(varchar(20),'" & fecha & "'),0,'Devolución de Factura  - Fra. " & Factura & "  Glosas','" & OIDTipComprobante & "','" & Factura & "',0,NULL,0,NULL,0," & AplicaNiif & ",0,NULL,NULL)"
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
                    FlagContolGenerateAccountNote = False
                    ListError.Add(New InterfaceResult With {.Message = "Devolución Factura: No se encontro ID del Tercero" & Tercero & ", favor avisar al administrador del sistema", .Result = False})
                End If
                dtTercero = Nothing


                Dim OIDInvoiceNotRadicate As String = String.Empty
                Dim dtCuenta As DataTable
                strSql = "SELECT OID FROM " & NameContainer & "..CTNCUENTA  WHERE CUECODIGO = '" & InvoiceNotRadicate & "'"
                dtCuenta = con.ExecuteCommand_Data(strSql)
                If dtCuenta.Rows.Count > 0 Then
                    OIDInvoiceNotRadicate = CInt(dtCuenta.Rows(0).Item("OID"))
                Else
                    FlagContolGenerateAccountNote = False
                    ListError.Add(New InterfaceResult With {.Message = "Devolución Factura: No se encontro ID de la cuenta" & InvoiceNotRadicate & ", favor avisar al administrador del sistema", .Result = False})
                End If
                'End If
                dtCuenta = Nothing



                'Detalle  	--movimiento credito
                strSql = "INSERT INTO " & NameContainer & "..CTNCOMD" & Year(DateActual) & "(CTNCOMCONC,CTNCUENTA,GENTERCER,CTNCENCOS,CMMVALDEB, CMMVALCRE, COMDETALLE, CTNCAURET, CMMESTCON, CMMFECCON,OptimisticLockField) " & _
                "VALUES( '" & Auto & "'," & cuentaCarteraGlosa & ", " & OIDtercero & ",NULL,0,convert(varchar(50)," & itemD.BalanceInvoice & "),'Devolución de Factura - Fra. " & Factura & " Glosas',NULL,0,NULL,0)"
                ResultEje = con.ExecuteCommand(strSql)
                If ResultEje = False Then
                    FlagContolGenerateAccountNote = False
                    ListError.Add(New InterfaceResult With {.Message = "Devolución Factura: ha ocurrido un error en la nota, creacion del detalle, favor avisar al administrador del sistema", .Result = False})
                End If



                'Detalle  	--movimiento debito
                strSql = "INSERT INTO " & NameContainer & "..CTNCOMD" & Year(DateActual) & "(CTNCOMCONC,CTNCUENTA,GENTERCER,CTNCENCOS,CMMVALDEB, CMMVALCRE, COMDETALLE, CTNCAURET, CMMESTCON, CMMFECCON,OptimisticLockField) " & _
                "VALUES( '" & Auto & "'," & OIDInvoiceNotRadicate & ", " & OIDtercero & ",NULL,convert(varchar(50)," & itemD.BalanceInvoice & "),0,'Devolución de Factura - Fra. " & Factura & " Glosas',NULL,0,NULL,0)"
                ResultEje = con.ExecuteCommand(strSql)
                If ResultEje = False Then
                    FlagContolGenerateAccountNote = False
                    ListError.Add(New InterfaceResult With {.Message = "Devolución Factura:: ha ocurrido un error en la nota, creacion del detalle, favor avisar al administrador del sistema", .Result = False})
                End If

                'con el codigo de usaurio traemos el OID del usuario
                Dim OIDusuario As Integer
                strSql = "SELECT top 1 OID  FROM " & NameContainer & "..GENUSUARIO  WHERE USUNOMBRE = '" & User & "'"
                Dim dtUsuario As DataTable = con.ExecuteCommand_Data(strSql)
                If dtUsuario.Rows.Count = 0 Then
                    ListError.Add(New InterfaceResult With {.Message = "Devolución Factura: no existe Usuario con el codigo " & User & " no se puede continuar!!", .Result = False})
                Else
                    OIDusuario = CInt(dtUsuario.Rows(0).Item("OID"))
                End If
                dtUsuario = Nothing

                'Creacion de historico de estados
                Dim AutoCRNCXCEST As Integer
                strSql = "INSERT INTO " & NameContainer & "..CRNCXCEST([CRNCXC],[CCEFECHA],[CCEESTADO],[CRNDOCUME],[GENUSUARIO],[CCEOBSERV],[CCEFECEST],[OptimisticLockField],[CCEDESCON])" &
                "VALUES('" & OIDcartera & "',convert(varchar(20),'" & fecha & "'),0,NULL,'" & OIDusuario & "','Mod. Genesis: La CxC paso de estado Radicada al estado SinRadicar',convert(varchar(20),'" & fecha & "'),0,0)"
                ResultEje = con.ExecuteCommand(strSql)
                Dim SQLEstados As New SqlDataAdapter("SELECT @@IDENTITY", con.sqlWebConection)
                With SQLEstados
                    .SelectCommand.CommandTimeout = 90
                    .SelectCommand.Transaction = con.IndigoTransaction
                    .SelectCommand.CommandType = CommandType.Text
                    AutoCRNCXCEST = .SelectCommand.ExecuteScalar.ToString
                End With


                'Una ves realizado el comprobante contable actualizamos la tabla de Cartera ERP para la factura
                strSql = "UPDATE " & NameContainer & "..CRNCXC SET CXCESTCAR = '0', CTNCUENTA = '" & OIDInvoiceNotRadicate & "', CXCESTCACT = '" & AutoCRNCXCEST & "'  WHERE cxcDocume = '" & Factura & "'"   'Confirmado
                resultUpdate = con.ExecuteCommand(strSql)


                '  strSql = "DELETE FROM [" & IndigoEmpresa & "].[Glosas].[RadicateInvoiceD] WHERE  RadicatedNumber = '" & itemD.RadicatedNumber & "' AND invoicenumber= '" & itemD.InvoiceNumber & "' AND state = 2  "
                '  resultUpdate = con.ExecuteCommand(strSql)

                strSql = "UPDATE [" & IndigoEmpresa & "].[Portfolio].[RadicateInvoiceD] SET State = 4 WHERE  RadicatedNumber = '" & itemD.RadicatedNumber & "' AND invoicenumber= '" & itemD.InvoiceNumber & "' AND state = 2  "
                resultUpdate = con.ExecuteCommand(strSql)


                If FlagContolGenerateAccountNote = True Then
                    ListInfo.Add(New InterfaceResult With {.Message = "Se Generaron los siguientes documentos: Comprobante Contable: " & NumerodeNotaContable & " - " & ConsecutivoNumcon & " para la factura: " & Factura & ", Empresa: " & NombreEmpresa, .Result = True, .Consecutive = NumerodeNotaContable & " - " & ConsecutivoNumcon})
                    Validation.AuditInterface(IndigoEmpresa, intOpcion, NumeroGlosa, Factura, 1, NumerodeNotaContable, ConsecutivoNumcon)
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
