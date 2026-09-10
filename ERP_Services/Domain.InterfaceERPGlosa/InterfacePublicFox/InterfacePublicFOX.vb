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
Imports System.Text

Public Class InterfacePublicFOX
    Implements IInterfacePublicFOX


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
    Public Function RadicateObjection(IndigoEmpresa As String, NumeroGlosa As String, factura As String, Tercero As String, NameContainer As String, ValorFac As Decimal, FechaFactura As Integer, intOpcion As String, User As String, plancodigo As String) As InterfaceResult Implements IInterfacePublicFOX.RadicateObjection
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
                    strSql = "select InvoiceRadicate as CreditAccount,denomination,plancode from " & IndigoEmpresa & ".Glosas.AccountSettingsFOX_PublicMethod where ParametersInterfaceId = " & ParametersInterfaceId & " and plancode = '" & plancodigo & " '"
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

            Tercero = con.fncConcatenar("0", Tercero, 15, ISQL.Direccion.Izquierda)
            'valida mes actual este abierto 
            Dim res As Boolean = Validation.ValidateMonthClose(eTypeInterface.FoxPublic, Date.Now, NameContainer)
            If res = False Then
                Result = New InterfaceResult With {.Message = "Radicacion Objeciones: El mes " & Month(Date.Now).ToString & " ya esta cerrado, no se puede continuar" & NameContainer, .Result = False}
                Return Result
            End If


            'valida que la tabla de moviminetos contables por año y mes  (MM201404)  Exista
            res = Validation.ValidateTableMov(Date.Now, NameContainer)
            If res = False Then
                Result = New InterfaceResult With {.Message = "Radicacion Objeciones:Tabla movimiento mes no existe, no se ha realizado cierre de mes" & NameContainer, .Result = False}
                Return Result
            End If


            'validacion de la cuenta debito
            res = Validation.ValidateAccount(eTypeInterface.FoxPublic, NameContainer, DebitAccount)
            If res = False Then
                Result = New InterfaceResult With {.Message = "Radicacion Objeciones: La cuenta " & DebitAccount & "No Existe", .Result = False}
                Return Result
            End If


            'validacion de la cuenta credito
            res = Validation.ValidateAccount(eTypeInterface.FoxPublic, NameContainer, CreditAccount)
            If res = False Then
                Result = New InterfaceResult With {.Message = "Radicacion Objeciones: La cuenta " & CreditAccount & "No Existe", .Result = False}
                Return Result
            End If


            'Actualizo estado de factura en cartera
            strSql = "UPDATE " & NameContainer & "..CRCARTER SET CEMESTADO = '3' WHERE CEMNUMFAC= '" & factura & "' AND TercodTer =  '" & Tercero & "'"
            res = con.ExecuteCommand(strSql)
            If res = False Then
                Result = New InterfaceResult With {.Message = "Radicacion Objeciones: ha ocurrido un error actualizando estado de cartera", .Result = False}
                Return Result
            End If

            'Busco el consecutivo de comprobantes
            Dim ConsecutivoComprobante As String
            strSql = "SELECT top 1   CPSCOMGLO FROM " & NameContainer & "..crParSiS"
            Dim dtConsecutivoComprobante As DataTable = con.ExecuteCommand_Data(strSql)
            If dtConsecutivoComprobante.Rows.Count = 0 Then
                Result = New InterfaceResult With {.Message = "Radicacion Objeciones:No esta configurado los consecutivo para comprobantes o ha ocurrido un error, favor avisar al administrador del sistema", .Result = False}
                Return Result
            Else
                ConsecutivoComprobante = dtConsecutivoComprobante.Rows(0).Item("CPSCOMGLO").ToString.TrimEnd
            End If

            'busco consecutivo comprobante diario mediante el consecutivo de comprobantes
            Dim ConsecutivoNumcon As String
            strSql = "SELECT top 1  ccdnumcom FROM " & NameContainer & "..ctcomdia WHERE ccdcodcom='" & ConsecutivoComprobante & "'"
            Dim dtConsecutivoNumcon As DataTable = con.ExecuteCommand_Data(strSql)
            If dtConsecutivoNumcon.Rows.Count = 0 Then
                Result = New InterfaceResult With {.Message = "Radicacion Objeciones:No esta configurado los consecutivo para comprobantes diario o ha ocurrido un error, favor avisar al administrador del sistema", .Result = False}
                Return Result
            Else
                ConsecutivoNumcon = dtConsecutivoNumcon.Rows(0).Item("ccdnumcom").ToString.TrimEnd
                'Actualiza el consecutive del comprobante
                strSql = "UPDATE " & NameContainer & "..ctcomdia SET ccdnumcom = ccdnumcom +1  WHERE ccdcodcom='" & ConsecutivoComprobante & "'"
                Dim resActualizarConsecutivo As Boolean = con.ExecuteCommand(strSql)
                If resActualizarConsecutivo = True Then
                    ConsecutivoNumcon = ConsecutivoNumcon + 1
                Else
                    Result = New InterfaceResult With {.Message = "Radicacion Objeciones:ha ocurrido un error actualizando consecutivo, favor avisar al administrador del sistema", .Result = False}
                    Return Result
                End If
            End If


            Dim DateActual As Date = Date.Now
            Dim fecha As String
            fecha = Format(DateActual, "yyyyMMdd hh:mm:ss")
            Dim ResultEje As Boolean
            ConsecutivoNumcon = con.fncConcatenar("0", ConsecutivoNumcon, 10, ISQL.Direccion.Izquierda)
            'Creacion del comprobante      Cabecera
            strSql = "INSERT INTO " & NameContainer & "..ctconmov(ccdcodcom,ccmnumcom,ccmfeccom,ccmasunto,ccmnumreg,ccmestado,ccmdocume)" &
            "VALUES( '" & ConsecutivoComprobante & "','" & ConsecutivoNumcon & "',GETDATE(),'Radicacion Glosa Subsanable-Fra." & factura & " Glosas',0,'','" & factura & "' )"
            ResultEje = con.ExecuteCommand(strSql)
            If ResultEje = False Then
                Result = New InterfaceResult With {.Message = "Radicacion Objeciones:ha ocurrido un error en la nota, creacion de la cabecera, favor avisar al administrador del sistema", .Result = False}
                Return Result
            End If

            Dim Contador As Integer = 1
            Dim MesConcatenado As String = Month(DateActual).ToString
            MesConcatenado = con.fncConcatenar("0", MesConcatenado, 2, ISQL.Direccion.Izquierda)
            Dim NombreTabla As String = NameContainer & "..MM" & Year(DateActual) & MesConcatenado

            'Detalle  	--movimiento credito
            strSql = "INSERT INTO " & NombreTabla & " (ccdcodcom,ccmnumcom,cpccodcue,tercodter,ccccodcen,ccmfeccom,cmmvaldeb,cmmvalcre,cmmdetmov,cmmcontem,cmmfeccon,ccrcodcon,cmmporret,cmmvalbas,cmmvalfac,cmmnumreg) " & _
            "VALUES( '" & ConsecutivoComprobante & "','" & ConsecutivoNumcon & "', '" & DebitAccount & "', '" & Tercero & "','',GETDATE(), " & _
            " 0, convert(varchar(50)," & ValorFac & "),'Radicacion Glosa Subsanable - Fra. " & factura & " Glosas','',null,'',0,0,0,'" & Contador & "')"
            ResultEje = con.ExecuteCommand(strSql)
            If ResultEje = False Then
                Result = New InterfaceResult With {.Message = "Radicacion Objeciones:ha ocurrido un error en la nota, creacion del detalle, favor avisar al administrador del sistema", .Result = False}
                Return Result
            End If

            'Detalle  	--movimiento debito
            Contador = Contador + 1
            strSql = "INSERT INTO " & NombreTabla & " (ccdcodcom,ccmnumcom,cpccodcue,tercodter,ccccodcen,ccmfeccom,cmmvaldeb,cmmvalcre,cmmdetmov,cmmcontem,cmmfeccon,ccrcodcon,cmmporret,cmmvalbas,cmmvalfac,cmmnumreg) " & _
            "VALUES( '" & ConsecutivoComprobante & "','" & ConsecutivoNumcon & "', '" & CreditAccount & "', '" & Tercero & "','',GETDATE(), " & _
            "convert(varchar(50)," & ValorFac & "),0,'Radicacion Glosa Subsanable - Fra. " & factura & " Glosas','',null,'',0,0,0,'" & Contador & "')"
            ResultEje = con.ExecuteCommand(strSql)
            If ResultEje = False Then
                Result = New InterfaceResult With {.Message = "Radicacion Objeciones:ha ocurrido un error en la nota, creacion del detalle, favor avisar al administrador del sistema", .Result = False}
                Return Result
            End If




            con.IndigoTransaction.Commit()

            Validation.AuditInterface(IndigoEmpresa, intOpcion, NumeroGlosa, factura, 1, ConsecutivoComprobante, ConsecutivoNumcon)

            Result = New InterfaceResult With {.Message = "Se Generaron los siguientes documentos: Comprobante Contable: " & ConsecutivoComprobante & " - " & ConsecutivoNumcon & ", Empresa: " & NombreEmpresa, .Result = True, .Consecutive = ConsecutivoComprobante & " - " & ConsecutivoNumcon, .Account = DebitAccount}

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
    Public Function AceptacionEAPBTotal(IndigoEmpresa As String, NumeroGlosa As String, factura As String, Tercero As String, NameContainer As String, ValorFac As Decimal, FechaFactura As Integer, intOpcion As String, User As String, plancodigo As String) As InterfaceResult Implements IInterfacePublicFOX.AceptacionEAPBTotal
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
            con.IndigoTransaction = con.sqlWebConection.BeginTransaction(IsolationLevel.ReadCommitted, "Interfax crear Nota Contable")

            strSql = "SELECT top 1 id, CompanyName,DebitAccount FROM " & IndigoEmpresa & ".Glosas.GlosasParametersInterface WHERE ContainerName= '" & NameContainer & "'  "
            Dim dtConfi As DataTable = con.ExecuteCommand_Data(strSql)
            Dim DebitAccount As String
            Dim CreditAccount As String
            Dim NombreEmpresa As String
            If dtConfi.Rows.Count = 0 Then
                Result = New InterfaceResult With {.Message = "Aceptacion EAPB Total: No Existe configuracion de interface para el contenedor" & NameContainer, .Result = False}
                Return Result
            Else
                NombreEmpresa = dtConfi.Rows(0).Item("CompanyName").ToString
                If dtConfi.Rows(0).Item("DebitAccount").ToString = String.Empty Then
                    Result = New InterfaceResult With {.Message = "Aceptacion EAPB Total: La cuenta Debito o Credito para radicación no existe en las configuraciones de interfaces" & NameContainer, .Result = False}
                    Return Result
                Else
                    Dim ParametersInterfaceId As String
                    DebitAccount = dtConfi.Rows(0).Item("DebitAccount").ToString
                    ParametersInterfaceId = dtConfi.Rows(0).Item("Id").ToString
                    Dim dtAccountCreditPlan As DataTable
                    strSql = "select InvoiceRadicate as CreditAccount,denomination,plancode from " & IndigoEmpresa & ".Glosas.AccountSettingsFOX_PublicMethod where ParametersInterfaceId = " & ParametersInterfaceId & " and plancode = '" & plancodigo & " '"
                    dtAccountCreditPlan = con.ExecuteCommand_Data(strSql)
                    If dtAccountCreditPlan IsNot Nothing AndAlso dtAccountCreditPlan.Rows.Count > 0 AndAlso dtAccountCreditPlan.Rows(0).Item("CreditAccount").ToString <> String.Empty Then
                        CreditAccount = dtAccountCreditPlan.Rows(0).Item("CreditAccount").ToString
                    Else
                        Result = New InterfaceResult With {.Message = "Aceptacion EAPB Total: La cuenta Credito para radicación no existe en las configuraciones de interfaces para el plan:" & dtAccountCreditPlan.Rows(0).Item("plancode").ToString & " - " & dtAccountCreditPlan.Rows(0).Item("denomination").ToString, .Result = False}
                        Return Result
                    End If
                End If
            End If

            Tercero = con.fncConcatenar("0", Tercero, 15, ISQL.Direccion.Izquierda)

            'valida mes actual este abierto 
            Dim res As Boolean = Validation.ValidateMonthClose(eTypeInterface.FoxPublic, Date.Now, NameContainer)
            If res = False Then
                Result = New InterfaceResult With {.Message = "Aceptacion EAPB Total: El mes " & Month(Date.Now).ToString & " ya esta cerrado, no se puede continuar" & NameContainer, .Result = False}
                Return Result
            End If


            'valida que la tabla de moviminetos contables por año y mes  (MM201404)  Exista
            res = Validation.ValidateTableMov(Date.Now, NameContainer)
            If res = False Then
                Result = New InterfaceResult With {.Message = "Aceptacion EAPB Total:Tabla movimiento mes no existe, no se ha realizado cierre de mes" & NameContainer, .Result = False}
                Return Result
            End If


            'validacion de la cuenta debito
            res = Validation.ValidateAccount(eTypeInterface.FoxPublic, NameContainer, DebitAccount)
            If res = False Then
                Result = New InterfaceResult With {.Message = "Aceptacion EAPB Total: La cuenta " & DebitAccount & "No Existe", .Result = False}
                Return Result
            End If


            'validacion de la cuenta credito
            res = Validation.ValidateAccount(eTypeInterface.FoxPublic, NameContainer, CreditAccount)
            If res = False Then
                Result = New InterfaceResult With {.Message = "Aceptacion EAPB Total: La cuenta " & CreditAccount & "No Existe", .Result = False}
                Return Result
            End If


            'Actualizo estado de factura en cartera
            'strSql = "UPDATE " & NameContainer & "..CRCARTER SET CEMESTADO = '4' WHERE CEMNUMFAC= '" & factura & "' AND TercodTer =  '" & Tercero & "'"
            'res = con.ExecuteCommand(strSql)
            'If res = False Then
            '    Result = New InterfaceResult With {.Message = "Aceptacion EAPB Total: ha ocurrido un error actualizando estado de cartera", .Result = False}
            '    Return Result
            'End If

            'Busco el consecutivo de comprobantes
            Dim ConsecutivoComprobante As String
            strSql = "SELECT top 1  cpscomcog FROM " & NameContainer & "..crParSiS"
            Dim dtConsecutivoComprobante As DataTable = con.ExecuteCommand_Data(strSql)
            If dtConsecutivoComprobante.Rows.Count = 0 Then
                Result = New InterfaceResult With {.Message = "Aceptacion EAPB Total:No esta configurado los consecutivo para comprobantes o ha ocurrido un error, favor avisar al administrador del sistema", .Result = False}
                Return Result
            Else
                ConsecutivoComprobante = dtConsecutivoComprobante.Rows(0).Item("cpscomcog").ToString.TrimEnd
            End If

            'busco consecutivo comprobante diario mediante el consecutivo de comprobantes
            Dim ConsecutivoNumcon As String
            strSql = "SELECT top 1  ccdnumcom FROM " & NameContainer & "..ctcomdia WHERE ccdcodcom='" & ConsecutivoComprobante & "'"
            Dim dtConsecutivoNumcon As DataTable = con.ExecuteCommand_Data(strSql)
            If dtConsecutivoNumcon.Rows.Count = 0 Then
                Result = New InterfaceResult With {.Message = "Aceptacion EAPB Total:No esta configurado los consecutivo para comprobantes diario o ha ocurrido un error, favor avisar al administrador del sistema", .Result = False}
                Return Result
            Else
                ConsecutivoNumcon = dtConsecutivoNumcon.Rows(0).Item("ccdnumcom").ToString.TrimEnd
                'Actualiza el consecutive del comprobante
                strSql = "UPDATE " & NameContainer & "..ctcomdia SET ccdnumcom = ccdnumcom +1  WHERE ccdcodcom='" & ConsecutivoComprobante & "'"
                Dim resActualizarConsecutivo As Boolean = con.ExecuteCommand(strSql)
                If resActualizarConsecutivo = True Then
                    ConsecutivoNumcon = ConsecutivoNumcon + 1
                Else
                    Result = New InterfaceResult With {.Message = "Aceptacion EAPB Total:ha ocurrido un error actualizando consecutivo, favor avisar al administrador del sistema", .Result = False}
                    Return Result
                End If
            End If


            Dim DateActual As Date = Date.Now
            Dim fecha As String
            fecha = Format(DateActual, "yyyyMMdd hh:mm:ss")
            Dim ResultEje As Boolean
            ConsecutivoNumcon = con.fncConcatenar("0", ConsecutivoNumcon, 10, ISQL.Direccion.Izquierda)
            'Creacion del comprobante      Cabecera
            strSql = "INSERT INTO " & NameContainer & "..ctconmov(ccdcodcom,ccmnumcom,ccmfeccom,ccmasunto,ccmnumreg,ccmestado,ccmdocume)" &
            "VALUES( '" & ConsecutivoComprobante & "','" & ConsecutivoNumcon & "',GETDATE(),'Aceptacion Glosa EAPB-Fra. " & factura & " Mod.Glosas',0,'','" & factura & "' )"
            ResultEje = con.ExecuteCommand(strSql)
            If ResultEje = False Then
                Result = New InterfaceResult With {.Message = "Aceptacion EAPB Total:ha ocurrido un error en la nota, creacion de la cabecera, favor avisar al administrador del sistema", .Result = False}
                Return Result
            End If


            'Detalle  	--movimiento debito
            Dim Contador As Integer = 1
            Dim MesConcatenado As String = Month(DateActual).ToString
            MesConcatenado = con.fncConcatenar("0", MesConcatenado, 2, ISQL.Direccion.Izquierda)
            Dim NombreTabla As String = NameContainer & "..MM" & Year(DateActual) & MesConcatenado
            strSql = "INSERT INTO " & NombreTabla & " (ccdcodcom,ccmnumcom,cpccodcue,tercodter,ccccodcen,ccmfeccom,cmmvaldeb,cmmvalcre,cmmdetmov,cmmcontem,cmmfeccon,ccrcodcon,cmmporret,cmmvalbas,cmmvalfac,cmmnumreg) " & _
            "VALUES( '" & ConsecutivoComprobante & "','" & ConsecutivoNumcon & "', '" & CreditAccount & "', '" & Tercero & "','',GETDATE(), " & _
            " 0, convert(varchar(50)," & ValorFac & "),'Aceptacion Glosa EAPB - Fra. " & factura & " Mod. Glosas','',null,'',0,0,0,'" & Contador & "')"
            ResultEje = con.ExecuteCommand(strSql)
            If ResultEje = False Then
                Result = New InterfaceResult With {.Message = "Aceptacion EAPB Total:ha ocurrido un error en la nota, creacion del detalle, favor avisar al administrador del sistema", .Result = False}
                Return Result
            End If


            'Detalle  	--movimiento credito
            Contador = Contador + 1
            strSql = "INSERT INTO " & NombreTabla & " (ccdcodcom,ccmnumcom,cpccodcue,tercodter,ccccodcen,ccmfeccom,cmmvaldeb,cmmvalcre,cmmdetmov,cmmcontem,cmmfeccon,ccrcodcon,cmmporret,cmmvalbas,cmmvalfac,cmmnumreg) " & _
            "VALUES( '" & ConsecutivoComprobante & "','" & ConsecutivoNumcon & "', '" & DebitAccount & "', '" & Tercero & "','',GETDATE(), " & _
            " convert(varchar(50)," & ValorFac & "), 0 ,  'Aceptacion Glosa EAPB - Fra. " & factura & " Mod. Glosas','',null,'',0,0,0,'" & Contador & "')"
            ResultEje = con.ExecuteCommand(strSql)
            If ResultEje = False Then
                Result = New InterfaceResult With {.Message = "Aceptacion EAPB Total:ha ocurrido un error en la nota, creacion del detalle, favor avisar al administrador del sistema", .Result = False}
                Return Result
            End If


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
    Public Function AcceptanceIPS(IndigoEmpresa As String, NumeroGlosa As String, factura As String, Tercero As String, NameContainer As String, ValorFac As Decimal, FechaFactura As Integer, intOpcion As String, User As String, AfectaServicio As Boolean, plancodigo As String, Optional VAlorAceptadoEAPB As Decimal = 0, Optional StatePortfolioDGH As String = "") As InterfaceResult Implements IInterfacePublicFOX.AcceptanceIPS
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
                    strSql = "select InvoiceRadicate as CreditAccount,denomination,plancode from " & IndigoEmpresa & ".Glosas.AccountSettingsFOX_PublicMethod where ParametersInterfaceId = " & ParametersInterfaceId & " and plancode = '" & plancodigo & " '"
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

            Tercero = con.fncConcatenar("0", Tercero, 15, ISQL.Direccion.Izquierda)

            Dim cuentaCartera As String
            strSql = "SELECT top 1  cpccodcue FROM " & NameContainer & "..crCarter WHERE cemNumFac= '" & factura & "' AND TerCodTer ='" & Tercero & "'"
            Dim dtCuenta As DataTable = con.ExecuteCommand_Data(strSql)
            If dtCuenta.Rows.Count = 0 Then
                Result = New InterfaceResult With {.Message = "Aceptacion IPS: No existe la factura en ERP, favor avisar al administrador del sistema", .Result = False}
                Return Result
            Else
                If dtCuenta.Rows(0).Item("cpccodcue").ToString <> String.Empty Then
                    cuentaCartera = dtCuenta.Rows(0).Item("cpccodcue").ToString
                    Dim res As Integer = Validation.ValidateAccount(eTypeInterface.FoxPrivate, NameContainer, cuentaCartera)
                    If res = 0 Then
                        Result = New InterfaceResult With {.Message = "Aceptacion IPS :No existe la cuenta " & cuentaCartera & " de la factura en ERP, favor avisar al administrador del sistema", .Result = False}
                        Return Result
                    End If
                Else
                    Result = New InterfaceResult With {.Message = "Aceptacion IPS: No existe la factura en ERP, favor avisar al administrador del sistema", .Result = False}
                    Return Result
                End If
            End If


            'validar el saldo y el plan
            strSql = "SELECT  count(*) FROM " & NameContainer & "..crcarter WHERE cemNumFac= '" & factura & "' AND TerCodTer ='" & Tercero & "' AND cemsalfac > 0"
            Dim resint As Integer = con.ExecuteCommand_Count(strSql)
            If resint = 0 Then
                Result = New InterfaceResult With {.Message = " Aceptacion IPS: La factura " & factura & " no  tiene saldo, no se puede continuar!!", .Result = False}
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
            resultBool = Validation.ValidateConcept(eTypeInterface.FoxPublic, NameContainer, Concept, "2")
            If resultBool = False Then
                Result = New InterfaceResult With {.Message = "Aceptacion IPS: El concepto para aceptaciones ya no existe, o no cumple con las medidas establecidas en los parametos contables, favor avisar al administrador del sistema", .Result = False}
                Return Result
            Else
                strSql = "SELECT cpccodcue FROM " & NameContainer & "..crConcep WHERE cnoCodCon= '" & Concept & "'"
                dtAccountConcept = con.ExecuteCommand_Data(strSql)
                If dtAccountConcept.Rows.Count > 0 AndAlso dtAccountConcept.Rows(0).Item("cpccodcue").ToString <> String.Empty Then
                    AccountConcept = dtAccountConcept.Rows(0).Item("cpccodcue").ToString
                Else
                    Result = New InterfaceResult With {.Message = "Aceptacion IPS: la cuenta del concepto " & Concept & " no existe, favor avisar al administrador del sistema", .Result = False}
                    Return Result
                End If
            End If
            dtAccountConcept = Nothing


            'validacion de la cuenta del concepto exista
            resultBool = Validation.ValidateAccount(eTypeInterface.FoxPublic, NameContainer, AccountConcept)
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


            'Actualizo estado de factura en cartera
            If StatePortfolioDGH <> String.Empty Then
                strSql = "UPDATE " & NameContainer & "..CRCARTER SET CEMESTADO = '" & StatePortfolioDGH & "' WHERE CEMNUMFAC= '" & factura & "' AND TercodTer =  '" & Tercero & "'"
                resultBool = con.ExecuteCommand(strSql)
                If resultBool = False Then
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
            resultBool = Validation.ValidateMonthClose(eTypeInterface.FoxPublic, Date.Now, NameContainer)
            If resultBool = False Then
                Result = New InterfaceResult With {.Message = "Aceptacion IPS: El mes " & Month(Date.Now).ToString & " ya esta cerrado, no se puede continuar" & NameContainer, .Result = False}
                Return Result
            End If

            'valida que la tabla de moviminetos contables por año y mes  (MM201404)  Exista
            resultBool = Validation.ValidateTableMov(Date.Now, NameContainer)
            If resultBool = False Then
                Result = New InterfaceResult With {.Message = "Aceptacion IPS:Tabla movimiento mes no existe, no se ha realizado cierre de mes" & NameContainer, .Result = False}
                Return Result
            End If

            'validacion de la cuenta debito
            resultBool = Validation.ValidateAccount(eTypeInterface.FoxPublic, NameContainer, DebitAccount)
            If resultBool = False Then
                Result = New InterfaceResult With {.Message = "Aceptacion IPS: La cuenta " & DebitAccount & "No Existe", .Result = False}
                Return Result
            End If


            'validacion de la cuenta credito
            resultBool = Validation.ValidateAccount(eTypeInterface.FoxPublic, NameContainer, CreditAccount)
            If resultBool = False Then
                Result = New InterfaceResult With {.Message = "Aceptacion IPS: La cuenta " & CreditAccount & "No Existe", .Result = False}
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
                    Result = New InterfaceResult With {.Message = "Aceptacion IPS: ha ocurrido un error actualizando consecutivo, favor avisar al administrador del sistema", .Result = False}
                    Return Result
                End If
            End If


            Dim DateActual As Date = Date.Now
            Dim fecha As String
            fecha = Format(DateActual, "yyyyMMdd hh:mm:ss")
            Dim ResultEje As Boolean
            ConsecutivoNumcon = con.fncConcatenar("0", ConsecutivoNumcon, 10, ISQL.Direccion.Izquierda)
            'Creacion del comprobante      Cabecera
            strSql = "INSERT INTO " & NameContainer & "..ctconmov(ccdcodcom,ccmnumcom,ccmfeccom,ccmasunto,ccmnumreg,ccmestado,ccmdocume)" &
            "VALUES( '" & ConsecutivoComprobante & "','" & ConsecutivoNumcon & "',GETDATE(),'Aceptacion Glosa EAPB-Fra. " & factura & "Mod.Glosas',0,'','" & factura & "' )"
            ResultEje = con.ExecuteCommand(strSql)
            If ResultEje = False Then
                Result = New InterfaceResult With {.Message = "Aceptacion IPS:ha ocurrido un error en la nota, creacion de la cabecera, favor avisar al administrador del sistema", .Result = False}
                Return Result
            End If


            'Detalle  	--movimiento credito
            Dim Contador As Integer = 1
            Dim MesConcatenado As String = Month(DateActual).ToString
            MesConcatenado = con.fncConcatenar("0", MesConcatenado, 2, ISQL.Direccion.Izquierda)
            Dim NombreTabla As String = NameContainer & "..MM" & Year(DateActual) & MesConcatenado
            'ValorFac = VAlorAceptadoEAPB  'lo no aceptado por la IPS va para la EPS
            strSql = "INSERT INTO " & NombreTabla & " (ccdcodcom,ccmnumcom,cpccodcue,tercodter,ccccodcen,ccmfeccom,cmmvaldeb,cmmvalcre,cmmdetmov,cmmcontem,cmmfeccon,ccrcodcon,cmmporret,cmmvalbas,cmmvalfac,cmmnumreg) " & _
            "VALUES( '" & ConsecutivoComprobante & "','" & ConsecutivoNumcon & "', '" & CreditAccount & "', '" & Tercero & "','',GETDATE(), " & _
            " 0, convert(varchar(50)," & VAlorAceptadoEAPB & "),'Aceptacion Glosa EAPB - Fra. " & factura & " Mod. Glosas','',null,'',0,0,0,'" & Contador & "')"
            ResultEje = con.ExecuteCommand(strSql)
            If ResultEje = False Then
                Result = New InterfaceResult With {.Message = "Aceptacion IPS:ha ocurrido un error en la nota, creacion del detalle, favor avisar al administrador del sistema", .Result = False}
                Return Result
            End If


            'Detalle  	--movimiento debito
            Contador = Contador + 1
            strSql = "INSERT INTO " & NombreTabla & " (ccdcodcom,ccmnumcom,cpccodcue,tercodter,ccccodcen,ccmfeccom,cmmvaldeb,cmmvalcre,cmmdetmov,cmmcontem,cmmfeccon,ccrcodcon,cmmporret,cmmvalbas,cmmvalfac,cmmnumreg) " & _
            "VALUES( '" & ConsecutivoComprobante & "','" & ConsecutivoNumcon & "', '" & DebitAccount & "', '" & Tercero & "','',GETDATE(), " & _
            " convert(varchar(50)," & VAlorAceptadoEAPB & "), 0 ,  'Aceptacion Glosa EAPB - Fra. " & factura & " Mod. Glosas','',null,'',0,0,0,'" & Contador & "')"
            ResultEje = con.ExecuteCommand(strSql)
            If ResultEje = False Then
                Result = New InterfaceResult With {.Message = "Aceptacion IPS:ha ocurrido un error en la nota, creacion del detalle, favor avisar al administrador del sistema", .Result = False}
                Return Result
            End If


            con.IndigoTransaction.Commit()

            Validation.AuditInterface(IndigoEmpresa, intOpcion, NumeroGlosa, factura, 1, ConsecutivoComprobante, ConsecutivoNumcon)

            Result = New InterfaceResult With {.Message = "Se Generaron los siguientes documentos: Comprobante Contable: " & ConsecutivoComprobante & " - " & ConsecutivoNumcon & " Nota Credito: " & resultCreateCreditNOte.Consecutive & " , Empresa: " & NombreEmpresa, .Result = True, .Consecutive = ConsecutivoComprobante & " - " & ConsecutivoNumcon}
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
    Public Function CreateCreditNote(NameContainer As String, Tercero As String, factura As String, ValorFac As Decimal, User As String, Cuenta As String, Concepto As String, CuentaConcepto As String, Optional ByVal EjecutaAceptacionEAPBUnicaTransaccion As Boolean = False) As InterfaceResult Implements IInterfacePublicFOX.CreateCreditNote
        Dim Result As New InterfaceResult
        Dim strSql As String = String.Empty
        Try

            If con.sqlWebConection.State = ConnectionState.Closed Then
                con.sqlWebConection.Open()
            End If

            If EjecutaAceptacionEAPBUnicaTransaccion = False Then
                con.InTransaction = True
                con.IndigoTransaction = con.sqlWebConection.BeginTransaction(IsolationLevel.ReadCommitted, "Interfax crear Nota credito")
            End If


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
            "'" & Tercero & "','" & TerceroNumeros & "', GETDATE(),'" & factura & "','001',convert(varchar(50)," & ValorFac & "),'" & Cuenta & "','','','Total Aceptado de la FRA.  " & factura & " Mod. Glosas'," & _
            "'" & User & "', GETDATE(),'',null,'',null,'','','',1,'','',0,0,0,'')"
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

#Region "actualizacion de estados en cartera Dinamica"

    ''' <summary>
    ''' Funcion para actualizar el estado de la cartera en ERP
    ''' </summary>
    ''' <param name="factura"></param>
    ''' <param name="NameContainer"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdtaeStateReiteration(factura As String, NameContainer As String, newstate As String) As Boolean Implements IInterfacePublicFOX.UpdtaeStateReiteration
        If NameContainer = String.Empty Then
            Throw New ArgumentNullException("NameContainer vacio")
        End If
        If factura = String.Empty Then
            Throw New ArgumentNullException("factura vacio")
        End If
        Dim resultUpdate As Boolean
        Dim strSql As String = String.Empty
        Try
            'cargamos configuraciones de interface
            If con.sqlWebConection.State = ConnectionState.Closed Then
                con.sqlWebConection.Open()
            End If
            'Una ves realizado el comprobante contable actualizamos la tabla de Cartera ERP para la factura
            strSql = "UPDATE " & NameContainer & "..crcarter SET cemestado = '" & newstate & "' WHERE cemnumfac = '" & factura & "'"   'Confirmado
            resultUpdate = con.ExecuteCommand(strSql)
            Return resultUpdate
        Catch ex As Exception
            con.IndigoTransaction.Rollback()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            resultUpdate = False
        Finally
            con.sqlWebConection.Close()
        End Try
        Return resultUpdate
    End Function


#End Region

#Region "Proceso de Radicacion de Factura y Actualizacion de Cartera ERP  NOTA CONTABLE"

    Public Function RadicateInvoice(RadicateInvoiceC As RadicateInvoiceC, ByVal listRadicated As List(Of RadicateInvoiceD), IndigoEmpresa As String, NameContainer As String, intOpcion As String, User As String, Comment As String) As List(Of InterfaceResult) Implements IInterfacePublicFOX.RadicateInvoice
        Dim Result As New InterfaceResult
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

            'GENERACION DE OFICIO EN DINAMICA
            Dim NumerodeNotaContable As String = String.Empty
            Dim ConsecutivoNumcon As String = String.Empty
            Dim NombreEmpresa As String = String.Empty
            '   Dim resultUpdate As Boolean
            Dim ConsecutiveNumberRadicate As String
            Dim DateConfirmationsystem As Date = RadicateInvoiceC.ConfirmDateSystem
            Dim DateConfirmation As Date = RadicateInvoiceC.ConfirmDate
            ' Dim ResultEje As Boolean
            Dim fechaConfirmacion As String
            Dim fechaDateConfirmationsystem As String
            fechaDateConfirmationsystem = Format(DateConfirmationsystem, "yyyyMMdd hh:mm:ss")
            fechaConfirmacion = Format(DateConfirmation, "yyyyMMdd hh:mm:ss")
            Dim Tercero As String = String.Empty
            If RadicateInvoiceC IsNot Nothing AndAlso RadicateInvoiceC.Customer IsNot Nothing Then
                Tercero = RadicateInvoiceC.Customer.Nit.Trim
            Else
                con.IndigoTransaction.Rollback()
                ListError.Add(New InterfaceResult With {.Message = "Ocurrio un error, no existe se encontro NIT del tercero", .Result = False})
                Return ListError
            End If
            Tercero = con.fncConcatenar("0", Tercero, 15, ISQL.Direccion.Izquierda)
            ConsecutiveNumberRadicate = con.fncConcatenar("0", RadicateInvoiceC.RadicatedConsecutive, 10, ISQL.Direccion.Izquierda)

            'valida mes actual este abierto 
            Dim res As Boolean = Validation.ValidateMonthClose(eTypeInterface.FoxPrivate, Date.Now, NameContainer)
            If res = False Then
                ListError.Add(New InterfaceResult With {.Message = "Radicacion Factura: El mes " & Month(Date.Now).ToString & " ya esta cerrado, no se puede continuar" & NameContainer, .Result = False})
                Return ListError
            End If
            'valida que la tabla de moviminetos contables por año y mes  (MM201404)  Exista
            res = Validation.ValidateTableMov(Date.Now, NameContainer)
            If res = False Then
                ListError.Add(New InterfaceResult With {.Message = "Radicacion Factura:Tabla movimiento mes no existe, no se ha realizado cierre de mes" & NameContainer, .Result = False})
                Return ListError
            End If

            'valido el numero de radicado 
            strSql = "select count(*)  from  " & NameContainer & "..[CRCRACTS] where [CCRNUMRAD] = '" & ConsecutiveNumberRadicate & "'"
            Dim intCount As Integer = con.ExecuteCommand_Count(strSql)
            If intCount > 0 Then
                ListError.Add(New InterfaceResult With {.Message = "ya existe un número de radicado con el consecutivo: " & ConsecutiveNumberRadicate & ", Actualice al último consecutivo, 9 - RADICACION FACTURAS", .Result = False})
                Return ListError
            End If

            'GENERACION DE OFICIO EN DINAMICA
            Dim stringBuilderRadicate As New StringBuilder
            Dim stringBuilderheaderVoucher As New StringBuilder
            Dim stringBuilderDetailsVoucher As New StringBuilder
            Dim stringBuilderUpdateState As New StringBuilder

            'cabecera de radicacion de cuentas ERP
            strSql = "INSERT INTO " & NameContainer & "..[CRCRACTS]([TERCODTER],[CCRNUMRAD],[CCRFECRAD],[CCRESTADO],[CCRUSUCRE],[CCRUSUANU],[CCRFECANU],[CCRUSUCON],[CCRFECCON]" & _
                ",[ENTCODIGO],[GECCODIGO],[CCRRADENT],[PLACODIGO],[CCRUSUMOD],[CCRESTCUE],[CCRCONOBJ],[ACACODIGO]) " & _
                "VALUES('" & Tercero & "','" & ConsecutiveNumberRadicate & "',convert(varchar(20),'" & (fechaConfirmacion) & "'),'C','" & User & "',NULL,NULL,'" & User & "',convert(varchar(20),'" & (fechaConfirmacion) & "'),'','','','','" & User & "','',0,'') "
            stringBuilderRadicate.AppendLine(strSql)
            For Each item As RadicateInvoiceD In listRadicated
                stringBuilderRadicate.AppendLine("INSERT INTO " & NameContainer & "..[CRMRACTS]([CCRNUMRAD],[CMRNUMFAC],[CMRRADENT],[CMROBSERV],[GECCODIGO],[PLACODIGO],[ENTCODIGO],[CMRENTCUE],[CMRFECCUE] " & _
               ",[CMREJECUE],[CMRVALRAD],[ACACODIGO],[PLACODIG1])" & _
                "VALUES('" & ConsecutiveNumberRadicate & "','" & item.InvoiceNumber & "','','" & Comment & "','" & item.ContractCode.Trim & "','" & item.PlanCode.Trim & "','" & item.ContractEntity.Trim & "','',GETDATE(),'',convert(varchar(50)," & item.BalanceInvoice & "),NULL,'" & item.PlanCode & "')")
            Next
            'FIN GENERACION DE OFICIO EN DINAMICA


            'cargo configuraciones de interfacez
            strSql = "SELECT top 1 id, CompanyName,RadicateCodeNoteAccounting FROM " & IndigoEmpresa & ".Glosas.GlosasParametersInterface WHERE ContainerName= '" & NameContainer & "'  "
            Dim dtConfi As DataTable = con.ExecuteCommand_Data(strSql)
            If dtConfi.Rows.Count = 0 Then
                ListError.Add(New InterfaceResult With {.Message = "Radicacion Factura: No Existe configuracion de interface para el contenedor" & NameContainer, .Result = False})
                Return ListError
            Else
                NombreEmpresa = dtConfi.Rows(0).Item("CompanyName").ToString
                If dtConfi.Rows(0).Item("RadicateCodeNoteAccounting").ToString = String.Empty Then
                    ListError.Add(New InterfaceResult With {.Message = "Radicacion Factura:no esta configurado el numero de comprobante para radicacion de cuentas " & NameContainer, .Result = False})
                    Return ListError
                Else
                    NumerodeNotaContable = dtConfi.Rows(0).Item("RadicateCodeNoteAccounting").ToString
                End If
            End If


            'un solo update
            strSql = "UPDATE " & NameContainer & "..ctcomdia SET ccdnumcom = ccdnumcom  WHERE ccdcodcom='" & NumerodeNotaContable & "'"
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
            Dim fecha As String = Format(DateActual, "yyyyMMdd hh:mm:ss")
            Dim FlagContolGenerateAccountNote As Boolean
            Dim NumeroGlosa As String = RadicateInvoiceC.RadicatedConsecutive  'en este caso el consecutivo de radicacion de facturas 
            Dim MesConcatenado As String = Month(DateActual).ToString
            MesConcatenado = con.fncConcatenar("0", MesConcatenado, 2, ISQL.Direccion.Izquierda)


            'GENERACION DE COMPROBANTES CONTABLE Y ACTUALIZACION DE CUENTA Y ESTADO CARTERA ERP
            For Each itemD As RadicateInvoiceD In listRadicated
                FlagContolGenerateAccountNote = True
                Dim Factura As String = itemD.InvoiceNumber

                Dim AccountRadicate As String
                Dim dtAccountCreditContractPlan As DataTable
                strSql = "select AccountRadicate from " & IndigoEmpresa & ".Glosas.Contract_Public where ContractCode = '" & itemD.ContractCode & "' and plancode = '" & itemD.PlanCode & " '"
                dtAccountCreditContractPlan = con.ExecuteCommand_Data(strSql)
                If dtAccountCreditContractPlan IsNot Nothing AndAlso dtAccountCreditContractPlan.Rows.Count > 0 AndAlso dtAccountCreditContractPlan.Rows(0).Item("AccountRadicate").ToString <> String.Empty Then
                    AccountRadicate = dtAccountCreditContractPlan.Rows(0).Item("AccountRadicate").ToString
                Else
                    FlagContolGenerateAccountNote = False
                    ListError.Add(New InterfaceResult With {.Message = "Radicacion de factura: La cuenta Credito para radicación no existe en las configuraciones de interfaces para el plan:" & itemD.PlanCode & " - " & itemD.ContractCode, .Result = False})
                    Return ListError
                End If

                CountVoucher = CountVoucher + 1
                ConsecutivoNumcon = ConsecutivoNumcon + 1
                'strSql = "UPDATE " & NameContainer & "..ctcomdia SET ccdnumcom = ccdnumcom +1  WHERE ccdcodcom='" & NumerodeNotaContable & "'"
                'Dim resActualizarConsecutivo As Boolean = con.ExecuteCommand(strSql)
                'If resActualizarConsecutivo = True Then
                '    ConsecutivoNumcon = ConsecutivoNumcon + 1
                'Else
                '    FlagContolGenerateAccountNote = False
                '    ListError.Add(New InterfaceResult With {.Message = "Radicacion Factura:ha ocurrido un error actualizando consecutivo, favor avisar al administrador del sistema", .Result = False})
                'End If
               

                ConsecutivoNumcon = con.fncConcatenar("0", ConsecutivoNumcon, 10, ISQL.Direccion.Izquierda)
                'Creacion del comprobante      Cabecera
                strSql = "INSERT INTO " & NameContainer & "..ctconmov(ccdcodcom,ccmnumcom,ccmfeccom,ccmasunto,ccmnumreg,ccmestado,ccmdocume)" &
                "VALUES( '" & NumerodeNotaContable & "','" & ConsecutivoNumcon & "',GETDATE(),'Radicación de Factura-Fra. " & Factura & " Glosas',0,'','" & Factura & "' )"
                stringBuilderheaderVoucher.AppendLine(strSql)
                'ResultEje = con.ExecuteCommand(strSql)

                'cuenta de cartera PortfoliGlosa
                Dim InvoiceNotRadicate As String = itemD.AccountantAccountCustomers 'es la misma cuenta SIn radicar
                ' Dim dtCuentaCartera As DataTable
                ' Dim cuentaCarteraGlosa As String = String.Empty

                ' strSql = "select CPCCODCUE  from  " & NameContainer & "..crcarter where CEMNUMFAC =  '" & Factura & "'"   'factura sin confirmar, traemos cuenta de cartera ERP
                ' dtCuentaCartera = con.ExecuteCommand_Data(strSql)

                'Dim InvoiceRadicate As String
                'If dtCuentaCartera.Rows.Count > 0 Then
                '    InvoiceNotRadicate = dtCuentaCartera.Rows(0).Item("CPCCODCUE").ToString
                'Else
                '    FlagContolGenerateAccountNote = False
                '    ListError.Add(New InterfaceResult With {.Message = "Radicacion Factura: no existe cuenta en cartera ERP para la factura " & Factura & ", no se puede continuar!!", .Result = False})
                'End If


                'Detalle A credito
                Dim Contador As Integer = 1
                Dim NombreTabla As String = NameContainer & "..MM" & Year(DateActual) & MesConcatenado
                strSql = "INSERT INTO " & NombreTabla & " (ccdcodcom,ccmnumcom,cpccodcue,tercodter,ccccodcen,ccmfeccom,cmmvaldeb,cmmvalcre,cmmdetmov,cmmcontem,cmmfeccon,ccrcodcon,cmmporret,cmmvalbas,cmmvalfac,cmmnumreg) " & _
                "VALUES( '" & NumerodeNotaContable & "','" & ConsecutivoNumcon & "', '" & InvoiceNotRadicate & "', '" & Tercero & "','',GETDATE(),0, " & _
                "convert(varchar(50)," & itemD.BalanceInvoice & "),'Radicación de Factura - Fra. " & Factura & " Mod. Glosas','',null,'',0,0,0,'" & Contador & "')"
                stringBuilderDetailsVoucher.AppendLine(strSql)
                ' ResultEje = con.ExecuteCommand(strSql)


                Contador = Contador + 1
                'Detalle Debito
                strSql = "INSERT INTO " & NombreTabla & " (ccdcodcom,ccmnumcom,cpccodcue,tercodter,ccccodcen,ccmfeccom,cmmvaldeb,cmmvalcre,cmmdetmov,cmmcontem,cmmfeccon,ccrcodcon,cmmporret,cmmvalbas,cmmvalfac,cmmnumreg) " & _
                "VALUES( '" & NumerodeNotaContable & "','" & ConsecutivoNumcon & "', '" & AccountRadicate.Trim & "', '" & Tercero & "','',GETDATE()," & _
                "convert(varchar(50)," & itemD.BalanceInvoice & "),0,'Radicación de Factura - Fra. " & Factura & " Mod. Glosas','',null,'',0,0,0,'" & Contador & "')"
                stringBuilderDetailsVoucher.AppendLine(strSql)
                ' ResultEje = con.ExecuteCommand(strSql)


                'Una ves realizado el comprobante contable actualizamos la tabla de Cartera ERP para la factura
                strSql = "UPDATE " & NameContainer & "..crcarter SET cemestado = '2', CPCCODCUE = '" & AccountRadicate.Trim & "' WHERE cemnumfac = '" & Factura & "'"   'Confirmado
                stringBuilderUpdateState.AppendLine(strSql)
                ' resultUpdate = con.ExecuteCommand(strSql)
                'If resultUpdate = False Then
                '    FlagContolGenerateAccountNote = False
                '    ListError.Add(New InterfaceResult With {.Message = "Radicacion Factura: Error Actualizando Cartera ERP", .Result = False})
                'End If


                'strSql = "UPDATE [" & IndigoEmpresa & "].[Glosas].[RadicateInvoiceD] set RadicatedNumber = '" & ConsecutiveNumberRadicate & "',RadicatedDate = convert(varchar(20),'" & fecha & "'), state = 2  where invoicenumber = '" & Factura & "' AND RadicateInvoiceCId = '" & itemD.RadicateInvoiceCId & "' "
                'stringBuilderUpdateState.AppendLine(strSql)
                ' resultUpdate = con.ExecuteCommand(strSql)
                'If resultUpdate = False Then
                '    FlagContolGenerateAccountNote = False
                '    ListError.Add(New InterfaceResult With {.Message = "Radicacion Factura: Error Actualizando Tabla detalle de radicado", .Result = False})
                'End If

                If FlagContolGenerateAccountNote = True Then
                    ListInfo.Add(New InterfaceResult With {.Message = "Se Generaron los siguientes documentos: Comprobante Contable: " & NumerodeNotaContable & " - " & ConsecutivoNumcon & " para la factura: " & Factura & ", Empresa: " & NombreEmpresa, .Result = True, .Consecutive = NumerodeNotaContable & " - " & ConsecutivoNumcon})
                    ' Validation.AuditInterface(IndigoEmpresa, intOpcion, NumeroGlosa, Factura, 1, NumerodeNotaContable, ConsecutivoNumcon)
                End If
            Next

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
          
            'ejecuto confirmacion de radicado
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




#Region "Actualizar fecha de confirmacion interface publico   NOTA: desarrollo solicitado por hospital pitalito "
    ''' <summary>
    ''' Funcion para actualizar el estado de la cartera en ERP
    ''' </summary>
    ''' <param name="NumberRadicate"></param>
    ''' <param name="NameContainer"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdateDateConfirm(NumberRadicate As String, NameContainer As String, NewDate As Date) As Boolean Implements IInterfacePublicFOX.UpdateDateConfirm

        If NameContainer = String.Empty Then
            Throw New ArgumentNullException("NameContainer vacio")
        End If
        If NumberRadicate = String.Empty Then
            Throw New ArgumentNullException("factura vacio")
        End If

        Dim resultUpdate As Boolean
        Dim strSql As String = String.Empty
        Try

            'cargamos configuraciones de interface
            If con.sqlWebConection.State = ConnectionState.Closed Then
                con.sqlWebConection.Open()
            End If

            Dim fechaConfirmacion As String
            fechaConfirmacion = Format(NewDate, "yyyyMMdd hh:mm:ss")
            'Una ves realizado el comprobante contable actualizamos la tabla de Cartera ERP para la factura

            strSql = "UPDATE " & NameContainer & "..CRCRACTS SET CCRFECCON = '" & fechaConfirmacion & "' WHERE CCRNUMRAD = '" & NumberRadicate & "'"   'Confirmado
            resultUpdate = con.ExecuteCommand(strSql)


            Return resultUpdate

        Catch ex As Exception
            con.IndigoTransaction.Rollback()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            resultUpdate = False
        Finally
            con.sqlWebConection.Close()
        End Try
        Return resultUpdate
    End Function


#End Region
#End Region

#Region "Proceso de devoluciones de Factura, eliminacion de factura en radicados tanto ERP y genesis, Actualizacion de Cartera ERP"

    Public Function Devolucion(IndigoEmpresa As String, ByVal listRadicated As List(Of GlosaDevolutionsReceptionD), NameContainer As String, intOpcion As String, User As String) As List(Of InterfaceResult) Implements IInterfacePublicFOX.Devolucion

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
                ListError.Add(New InterfaceResult With {.Message = "Ocurrio un error, no existe se encontro NIT del tercero", .Result = False})
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
                    ListError.Add(New InterfaceResult With {.Message = "Aceptacion IPS: La cuenta Credito para radicación no existe en las configuraciones de interfaces para el plan:" & dtAccountCreditContractPlan.Rows(0).Item("plancode").ToString & " - " & dtAccountCreditContractPlan.Rows(0).Item("ContractCode").ToString, .Result = False})
                End If


                'eliminamos factura de radicado en dinamica
                strSql = " DELETE FROM  " & NameContainer & "..[CRMRACTS] WHERE CMRNUMFAC = '" & itemD.InvoiceNumber & "'  AND  CCRNUMRAD = '" & itemD.RadicatedNumber & "' "
                ResultEje = con.ExecuteCommand(strSql)

                'cuenta de cartera PortfoliGlosa
                Dim dtCuentaCartera As DataTable
                Dim cuentaCarteraGlosa As String = String.Empty

                strSql = "select CPCCODCUE  from  " & NameContainer & "..crcarter where CEMNUMFAC =  '" & Factura & "'"   'factura sin confirmar, traemos cuenta de cartera ERP
                dtCuentaCartera = con.ExecuteCommand_Data(strSql)


                If dtCuentaCartera.Rows.Count > 0 Then
                    cuentaCarteraGlosa = dtCuentaCartera.Rows(0).Item("CPCCODCUE").ToString
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
                "VALUES( '" & NumerodeNotaContable & "','" & ConsecutivoNumcon & "', '" & cuentaCarteraGlosa & "', '" & Tercero & "','',convert(varchar(20),'" & fecha & "'),0, " & _
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


                '  strSql = "DELETE FROM [" & IndigoEmpresa & "].[Glosas].[RadicateInvoiceD] WHERE  RadicatedNumber = '" & itemD.RadicatedNumber & "' AND invoicenumber= '" & itemD.InvoiceNumber & "' AND state = 2  "
                '  resultUpdate = con.ExecuteCommand(strSql)

                strSql = "UPDATE [" & IndigoEmpresa & "].[Portfolio].[RadicateInvoiceD] SET State = 4 WHERE  RadicatedNumber = '" & itemD.RadicatedNumber & "' AND invoicenumber= '" & itemD.InvoiceNumber & "' AND state = 2  "
                resultUpdate = con.ExecuteCommand(strSql)
                'If resultUpdate = False Then
                '    FlagContolGenerateAccountNote = False
                '    ListError.Add(New InterfaceResult With {.Message = "Devolución Factura:ha ocurrido un error actualizando estado de factura, no se encontro factura en el radicado " & itemD.RadicatedNumber & ", favor avisar al administrador del sistema", .Result = False})
                'End If

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
