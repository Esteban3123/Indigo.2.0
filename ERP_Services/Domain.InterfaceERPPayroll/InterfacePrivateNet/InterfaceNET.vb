'***********************************************************************
' Assembly         : Domain.InterfaceERPGlosa
' Author           : Daniel Eduardo Arévalo
' Created          : 29-07-2014
'
' Last Modified By : 
' Last Modified On : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Interface
Imports System.Data.SqlClient
Imports Domain.Payroll.Entities
Imports Domain.Base.Entities

Public Class InterfaceNET

    Implements IInterfaceNET


#Region "Variables"
    Dim con As ConectionSQL
    'Dim Validation As IInterfaceValidate
#End Region

#Region "Contructor"
    Public Sub New(company As String)
        con = New ConectionSQL(company)
        'Validation = New InterfaceValidate(company)
    End Sub
#End Region


    Public Function CreatePayrollMovementAccount(ByVal ListCostDistribution As List(Of CostDistributions), LiquidationConfirm As List(Of Liquidation), ListEmployee As List(Of Employee), Group As Group) As ActionResult(Of List(Of InterfaceResult)) Implements IInterfaceNET.CreatePayrollMovementAccount
        Dim Result As New InterfaceResult
        If ListCostDistribution Is Nothing Then
            Throw New ArgumentNullException("ListCostDistribution vacio")
        End If

        Dim strSql As String = String.Empty

        Dim ListError As New List(Of InterfaceResult)
        Dim ListInfo As New List(Of InterfaceResult)

        Dim actionResult As New ActionResult(Of List(Of InterfaceResult))
        Dim indexError As Integer = 0
        Try
            'cargamos configuraciones de interface
            If con.sqlWebConection.State = ConnectionState.Closed Then
                con.sqlWebConection.Open()
            End If

            con.InTransaction = True
            con.IndigoTransaction = con.sqlWebConection.BeginTransaction(IsolationLevel.ReadCommitted, "Creacion Comprobante Nomina")

            ' Dim Group = ListCostDistribution.Item(0).Group

            Dim NameContainer = Group.PayrollParameter.InterfaceName

            Dim PayrollDateLiquidated = ListCostDistribution.Item(0).PayrollDateLiquidated

            Dim ConsecutivoComprobantePayroll As String = Group.PayrollParameter.PayrollVoucherCode
            Dim ConsecutivoComprobanteProvisiones As String = Group.PayrollParameter.ProvisionVoucherCode
            Dim ConsecutivoComprobantePrestaciones As String = Group.PayrollParameter.PrestacionVoucherCode
            Dim FlagContolGenerateAccountNote As Boolean

            Dim SumTotalAcrruedPayroll = ListCostDistribution.Sum(Function(x) x.AccruedValue)
            Dim SumTotalDeductedPayroll = ListCostDistribution.Sum(Function(x) x.DeductedValue)
            Dim TotalPayrollValue = SumTotalAcrruedPayroll - SumTotalDeductedPayroll


            Dim ListPayrollVourcherComprobante = ListCostDistribution.Where(Function(x) x.AccountingStatementNumber = ConsecutivoComprobantePayroll).ToList()

            If ListPayrollVourcherComprobante IsNot Nothing And ListPayrollVourcherComprobante.Count() > 0 Then

                '' COMPROBANTE CONTABLE DE NÓMINA
                'busco consecutivo comprobante diario mediante el consecutivo de comprobantes
                Dim ConsecutivoNumcon As Integer
                Dim numerogenerado As Integer
                Dim CodigoTipoDoc As String
                Dim OIDTipoDoc As Integer
                strSql = "SELECT top 1 OID,GENCONSEC,tccodigo FROM " & NameContainer & "..CTNTIPCOM WHERE TCCODIGO='" & ConsecutivoComprobantePayroll & "'"
                Dim dtConsecutivoNumcon As DataTable = con.ExecuteCommand_Data(strSql)
                If dtConsecutivoNumcon.Rows.Count = 0 Then
                    ListError.Add(New InterfaceResult With {.Message = "No esta configurado los consecutivo para comprobantes  o ha ocurrido un error, favor avisar al administrador del sistema", .Result = False})
                    FlagContolGenerateAccountNote = False
                Else
                    ConsecutivoNumcon = CInt(dtConsecutivoNumcon.Rows(0).Item("GENCONSEC").ToString.TrimEnd)
                    CodigoTipoDoc = dtConsecutivoNumcon.Rows(0).Item("tccodigo").ToString.TrimEnd
                    OIDTipoDoc = dtConsecutivoNumcon.Rows(0).Item("OID").ToString.TrimEnd
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
                        ListError.Add(New InterfaceResult With {.Message = "ha ocurrido un error actualizando consecutivo, favor avisar al administrador del sistema", .Result = False})
                        FlagContolGenerateAccountNote = False
                    End If
                End If

                Dim DateActual As Date = Date.Now
                'valido que el consecutivo generado no este ya registrado 
                strSql = "SELECT count(*) FROM " & NameContainer & "..CTNCOM" & Year(DateActual) & " WHERE COMCODIGO= '" & numerogenerado & "' AND CTNTIPCOM = '" & ConsecutivoNumcon & "'  "
                Dim dtCountConsecutive As Integer = con.ExecuteCommand_Count(strSql)
                If dtCountConsecutive > 0 Then
                    ListError.Add(New InterfaceResult With {.Message = "El consecutivo " & numerogenerado & " del Tipo de comprobante difícil recaudo ya existe", .Result = False})
                    FlagContolGenerateAccountNote = False
                End If


                If ListError.Count > 0 Then
                    con.IndigoTransaction.Rollback()
                    actionResult.ObjectEmbbeded = ListError
                    actionResult.StateResult = False
                    Return actionResult
                End If


                Dim fecha As String
                fecha = Format(DateActual, "yyyyMMdd hh:mm:ss")
                Dim ResultEje As Boolean
                'Creacion del comprobante      Cabecera
                strSql = "INSERT INTO " & NameContainer & "..CTNCOM" & Year(DateActual) & "(COMCODIGO,CTNTIPCOM,COMFECCOM,COMESTADO,COMDETALLE,COMOIDDOCU,COMNUMDOCU,COMOIDTYPE,COMELIMCOMP,COMCIERANU,COMGECGBATCH,OptimisticLockField)" &
                "VALUES( '" & numerogenerado & "','" & OIDTipoDoc & "',convert(varchar(20),'" & fecha & "'),0,'Nomina Contable Indigo VIE - Grupo " & Group.Code & " ','" & numerogenerado & "','" & numerogenerado & "',0,NULL,0,NULL,0)"
                ResultEje = con.ExecuteCommand(strSql)

                Dim Auto As String
                Dim SQL As New SqlDataAdapter("SELECT @@IDENTITY", con.sqlWebConection)
                With SQL
                    .SelectCommand.CommandTimeout = 90
                    .SelectCommand.Transaction = con.IndigoTransaction
                    .SelectCommand.CommandType = CommandType.Text
                    Auto = .SelectCommand.ExecuteScalar.ToString
                End With

                Dim listaConceptosPrestacionales = (From e In ListPayrollVourcherComprobante Where e.Concept.ConceptClass = "017" Or e.Concept.ConceptClass = "014" Or e.Concept.ConceptClass = "038" Or e.Concept.ConceptClass = "016" Or e.Concept.ConceptClass = "041" Select e)
                If listaConceptosPrestacionales IsNot Nothing And listaConceptosPrestacionales.Count > 0 Then
                    For Each nit As CostDistributions In (From e In listaConceptosPrestacionales Select e).ToList().Distinct()
                        Dim suma As Double = (From e In listaConceptosPrestacionales Where e.CompanyNit = nit.CompanyNit Select e.TotalConceptValue).Sum()


                        'consultamos el OID del tercero
                        Dim OIDtercero As Integer
                        strSql = "SELECT  OID FROM " & NameContainer & "..GENTERCER WHERE TERNUMDOC = '" & nit.CompanyNit & "'"
                        Dim dtTercero As DataTable = con.ExecuteCommand_Data(strSql)
                        If dtTercero.Rows.Count > 0 Then
                            OIDtercero = CInt(dtTercero.Rows(0).Item("OID"))
                        Else
                            ListError.Add(New InterfaceResult With {.Message = "No se encontro ID del Tercero" & nit.CompanyNit & ", favor avisar al administrador del sistema", .Result = False})
                            FlagContolGenerateAccountNote = False
                        End If
                        dtTercero = Nothing

                        'buscamos el OID de la cuenta acreditar y debitar
                        Dim strOIDCuentaCREDIT As String = nit.DeductedAccount
                        If strOIDCuentaCREDIT = String.Empty Then
                            ListError.Add(New InterfaceResult With {.Message = "No se encontro cuenta de nomina, favor avisar al administrador del sistema", .Result = False})
                        End If

                        Dim OIDCuentaCredito As Integer
                        Dim ManejaCentroCosto As Boolean

                        strSql = "SELECT OID, CUEMANCEN FROM " & NameContainer & "..CTNCUENTA  WHERE CUECODIGO = '" & strOIDCuentaCREDIT & "'"
                        Dim dtCuentaCredito As DataTable = con.ExecuteCommand_Data(strSql)
                        If dtCuentaCredito.Rows.Count = 0 Then
                            ListError.Add(New InterfaceResult With {.Message = "No se encontro ID de la Cuenta Contable" & strOIDCuentaCREDIT & ", favor avisar al administrador del sistema", .Result = False})
                            FlagContolGenerateAccountNote = False
                        Else
                            OIDCuentaCredito = CInt(dtCuentaCredito.Rows(0).Item("OID"))
                            ManejaCentroCosto = CInt(dtCuentaCredito.Rows(0).Item("CUEMANCEN"))
                        End If

                        Dim strOIDCuentadebit As String = nit.AccruedAccount
                        If strOIDCuentadebit = String.Empty Then
                            ListError.Add(New InterfaceResult With {.Message = "No se encontro cuenta de nomina del concepto, favor avisar al administrador del sistema", .Result = False})
                            FlagContolGenerateAccountNote = False
                        End If

                        Dim OIDCuentaDebito As Integer
                        strSql = "SELECT OID, CUEMANCEN FROM " & NameContainer & "..CTNCUENTA  WHERE CUECODIGO = '" & strOIDCuentadebit & "'"
                        Dim dtCuentaDebito As DataTable = con.ExecuteCommand_Data(strSql)
                        If dtCuentaDebito.Rows.Count = 0 Then
                            ListError.Add(New InterfaceResult With {.Message = "No se encontro ID de la Cuenta Contable" & strOIDCuentadebit & ", favor avisar al administrador del sistema", .Result = False})
                            FlagContolGenerateAccountNote = False
                        Else
                            OIDCuentaDebito = CInt(dtCuentaDebito.Rows(0).Item("OID"))
                            ManejaCentroCosto = CInt(dtCuentaDebito.Rows(0).Item("CUEMANCEN"))
                        End If

                        Dim OIDcentrocosto As String = ""
                        If ManejaCentroCosto = True Then
                            Dim centroCosto As String = nit.CostCenter.Code
                            'OID centro de costo

                            strSql = "SELECT OID FROM " & NameContainer & "..CTNCENCOS  WHERE CCCODIGO = '" & centroCosto & "'"
                            Dim dtCentroCosto As DataTable = con.ExecuteCommand_Data(strSql)
                            If dtCentroCosto.Rows.Count = 0 Then
                                ListError.Add(New InterfaceResult With {.Message = "No se encontro ID del centro de costo" & centroCosto & ", favor avisar al administrador del sistema", .Result = False})
                                FlagContolGenerateAccountNote = False
                            Else
                                OIDcentrocosto = CInt(dtCentroCosto.Rows(0).Item("OID"))
                            End If
                            dtCentroCosto = Nothing
                        Else
                            OIDcentrocosto = "NULL"
                        End If

                        If nit.Concept Is Nothing Then
                            Dim Var = "Prueba"
                        End If

                        If OIDcentrocosto Is Nothing Or OIDcentrocosto = "" Then
                            Dim Var = "Prueba"
                        End If

                        If ListError.Count > 0 Then
                            con.IndigoTransaction.Rollback()
                            actionResult.ObjectEmbbeded = ListError
                            actionResult.StateResult = False
                            Return actionResult
                        End If

                        If nit.Concept.ConceptType = "1" Then

                            'Detalle
                            strSql = "INSERT INTO " & NameContainer & "..CTNCOMD" & Year(DateActual) & "(CTNCOMCONC,CTNCUENTA,GENTERCER,CTNCENCOS,CMMVALDEB, CMMVALCRE, COMDETALLE, CTNCAURET, CMMESTCON, CMMFECCON,OptimisticLockField) " & _
                            "VALUES( '" & Auto & "'," & OIDCuentaDebito & ", " & OIDtercero & "," & OIDcentrocosto & ",convert(varchar(50),'" & nit.AccruedValue & "'),0,'Nomina Contable Indigo Vie " & nit.PayrollDateLiquidated.ToString("yyyyMM") & " ',NULL,0,NULL,0)"
                            ResultEje = con.ExecuteCommand(strSql)
                        ElseIf nit.Concept.ConceptType = "2" Then

                            'Detalle
                            strSql = "INSERT INTO " & NameContainer & "..CTNCOMD" & Year(DateActual) & "(CTNCOMCONC,CTNCUENTA,GENTERCER,CTNCENCOS,CMMVALDEB, CMMVALCRE, COMDETALLE, CTNCAURET, CMMESTCON, CMMFECCON,OptimisticLockField) " & _
                            "VALUES( '" & Auto & "'," & OIDCuentaCredito & ", " & OIDtercero & "," & OIDcentrocosto & ",0,convert(varchar(50),'" & nit.DeductedValue & "'),'Nomina Contable Indigo Vie " & nit.PayrollDateLiquidated.ToString("yyyyMM") & " ',NULL,0,NULL,0)"
                            ResultEje = con.ExecuteCommand(strSql)
                        End If

                    Next
                End If

                Dim ObjListPayrollVourcherComprobante = ListPayrollVourcherComprobante.Where(Function(X) X.Concept.ConceptClass <> "014" And X.Concept.ConceptClass <> "038" And X.Concept.ConceptClass <> "016" And X.Concept.ConceptClass <> "041")

                If ObjListPayrollVourcherComprobante IsNot Nothing And ObjListPayrollVourcherComprobante.Count > 0 Then

                    For Each item As CostDistributions In ObjListPayrollVourcherComprobante
                        Dim Tercero As String = ""

                        Tercero = item.EmployeeNit

                        'consultamos el OID del tercero
                        Dim OIDtercero As Integer
                        strSql = "SELECT  OID FROM " & NameContainer & "..GENTERCER WHERE TERNUMDOC = '" & Tercero & "'"
                        Dim dtTercero As DataTable = con.ExecuteCommand_Data(strSql)
                        If dtTercero.Rows.Count > 0 Then
                            OIDtercero = CInt(dtTercero.Rows(0).Item("OID"))
                        Else
                            ListError.Add(New InterfaceResult With {.Message = "No se encontro ID del Tercero" & Tercero & ", favor avisar al administrador del sistema", .Result = False})
                            FlagContolGenerateAccountNote = False
                        End If
                        dtTercero = Nothing

                        'buscamos el OID de la cuenta acreditar y debitar
                        Dim strOIDCuentaCREDIT As String = item.DeductedAccount
                        If strOIDCuentaCREDIT = String.Empty Then
                            ListError.Add(New InterfaceResult With {.Message = "No se encontro cuenta de nomina, favor avisar al administrador del sistema", .Result = False})
                        End If

                        Dim OIDCuentaCredito As Integer
                        Dim ManejaCentroCosto As Boolean

                        strSql = "SELECT OID, CUEMANCEN FROM " & NameContainer & "..CTNCUENTA  WHERE CUECODIGO = '" & strOIDCuentaCREDIT & "'"
                        Dim dtCuentaCredito As DataTable = con.ExecuteCommand_Data(strSql)
                        If dtCuentaCredito.Rows.Count = 0 Then
                            ListError.Add(New InterfaceResult With {.Message = "No se encontro ID de la Cuenta Contable" & strOIDCuentaCREDIT & ", favor avisar al administrador del sistema", .Result = False})
                            FlagContolGenerateAccountNote = False
                        Else
                            OIDCuentaCredito = CInt(dtCuentaCredito.Rows(0).Item("OID"))
                            ManejaCentroCosto = CInt(dtCuentaCredito.Rows(0).Item("CUEMANCEN"))
                        End If

                        Dim strOIDCuentadebit As String = item.AccruedAccount
                        If strOIDCuentadebit = String.Empty Then
                            ListError.Add(New InterfaceResult With {.Message = "No se encontro cuenta de nomina del concepto, favor avisar al administrador del sistema", .Result = False})
                            FlagContolGenerateAccountNote = False
                        End If

                        Dim OIDCuentaDebito As Integer
                        strSql = "SELECT OID, CUEMANCEN FROM " & NameContainer & "..CTNCUENTA  WHERE CUECODIGO = '" & strOIDCuentadebit & "'"
                        Dim dtCuentaDebito As DataTable = con.ExecuteCommand_Data(strSql)
                        If dtCuentaDebito.Rows.Count = 0 Then
                            ListError.Add(New InterfaceResult With {.Message = "No se encontro ID de la Cuenta Contable" & strOIDCuentadebit & ", favor avisar al administrador del sistema", .Result = False})
                            FlagContolGenerateAccountNote = False
                        Else
                            OIDCuentaDebito = CInt(dtCuentaDebito.Rows(0).Item("OID"))
                            ManejaCentroCosto = CInt(dtCuentaDebito.Rows(0).Item("CUEMANCEN"))
                        End If

                        Dim OIDcentrocosto As String = ""
                        If ManejaCentroCosto = True Then
                            Dim centroCosto As String = item.CostCenter.Code
                            'OID centro de costo

                            strSql = "SELECT OID FROM " & NameContainer & "..CTNCENCOS  WHERE CCCODIGO = '" & centroCosto & "'"
                            Dim dtCentroCosto As DataTable = con.ExecuteCommand_Data(strSql)
                            If dtCentroCosto.Rows.Count = 0 Then
                                ListError.Add(New InterfaceResult With {.Message = "No se encontro ID del centro de costo" & centroCosto & ", favor avisar al administrador del sistema", .Result = False})
                                FlagContolGenerateAccountNote = False
                            Else
                                OIDcentrocosto = CInt(dtCentroCosto.Rows(0).Item("OID"))
                            End If
                            dtCentroCosto = Nothing
                        Else
                            OIDcentrocosto = "NULL"
                        End If

                        If item.Concept Is Nothing Then
                            Dim Var = "Prueba"
                        End If

                        If OIDcentrocosto Is Nothing Or OIDcentrocosto = "" Then
                            Dim Var = "Prueba"
                        End If

                        If ListError.Count > 0 Then
                            con.IndigoTransaction.Rollback()
                            actionResult.ObjectEmbbeded = ListError
                            actionResult.StateResult = False
                            Return actionResult
                        End If

                        If item.Concept.ConceptType = "1" Then

                            'Detalle
                            strSql = "INSERT INTO " & NameContainer & "..CTNCOMD" & Year(DateActual) & "(CTNCOMCONC,CTNCUENTA,GENTERCER,CTNCENCOS,CMMVALDEB, CMMVALCRE, COMDETALLE, CTNCAURET, CMMESTCON, CMMFECCON,OptimisticLockField) " & _
                            "VALUES( '" & Auto & "'," & OIDCuentaDebito & ", " & OIDtercero & "," & OIDcentrocosto & ",convert(varchar(50),'" & item.AccruedValue & "'),0,'Nomina Contable Indigo Vie " & item.PayrollDateLiquidated.ToString("yyyyMM") & " ',NULL,0,NULL,0)"
                            ResultEje = con.ExecuteCommand(strSql)
                        ElseIf item.Concept.ConceptType = "2" Then

                            'Detalle
                            strSql = "INSERT INTO " & NameContainer & "..CTNCOMD" & Year(DateActual) & "(CTNCOMCONC,CTNCUENTA,GENTERCER,CTNCENCOS,CMMVALDEB, CMMVALCRE, COMDETALLE, CTNCAURET, CMMESTCON, CMMFECCON,OptimisticLockField) " & _
                            "VALUES( '" & Auto & "'," & OIDCuentaCredito & ", " & OIDtercero & "," & OIDcentrocosto & ",0,convert(varchar(50),'" & item.DeductedValue & "'),'Nomina Contable Indigo Vie " & item.PayrollDateLiquidated.ToString("yyyyMM") & " ',NULL,0,NULL,0)"
                            ResultEje = con.ExecuteCommand(strSql)
                        End If

                    Next
                End If


            End If


            ' '' COMPROBANTE CONTABLE DE NÓMINA
            ''busco consecutivo comprobante diario mediante el consecutivo de comprobantes
            'Dim ConsecutivoNumcon As Integer
            'Dim numerogenerado As Integer
            'Dim CodigoTipoDoc As String
            'Dim OIDTipoDoc As Integer
            'strSql = "SELECT top 1 OID,GENCONSEC,tccodigo FROM " & NameContainer & "..CTNTIPCOM WHERE TCCODIGO='" & ConsecutivoComprobantePayroll & "'"
            'Dim dtConsecutivoNumcon As DataTable = con.ExecuteCommand_Data(strSql)
            'If dtConsecutivoNumcon.Rows.Count = 0 Then
            '    ListError.Add(New InterfaceResult With {.Message = "No esta configurado los consecutivo para comprobantes  o ha ocurrido un error, favor avisar al administrador del sistema", .Result = False})
            '    FlagContolGenerateAccountNote = False
            'Else
            '    ConsecutivoNumcon = CInt(dtConsecutivoNumcon.Rows(0).Item("GENCONSEC").ToString.TrimEnd)
            '    CodigoTipoDoc = dtConsecutivoNumcon.Rows(0).Item("tccodigo").ToString.TrimEnd
            '    OIDTipoDoc = dtConsecutivoNumcon.Rows(0).Item("OID").ToString.TrimEnd
            '    'Actualiza el consecutive del comprobante
            '    strSql = "UPDATE " & NameContainer & "..geNconsec SET GCONUMERO = GCONUMERO +1  WHERE OID='" & ConsecutivoNumcon & "'"
            '    Dim resActualizarConsecutivo As Boolean = con.ExecuteCommand(strSql)
            '    If resActualizarConsecutivo = True Then
            '        'consultamo 
            '        Dim dtConseIncrementa As DataTable
            '        strSql = " SELECT top 1 GCONUMERO FROM " & NameContainer & "..geNconsec WHERE OID='" & ConsecutivoNumcon & "'"
            '        dtConseIncrementa = con.ExecuteCommand_Data(strSql)
            '        ' ConsecutivoNumcon = ConsecutivoNumcon + 1
            '        numerogenerado = CInt((dtConseIncrementa.Rows(0).Item("GCONUMERO")))
            '    Else
            '        ListError.Add(New InterfaceResult With {.Message = "ha ocurrido un error actualizando consecutivo, favor avisar al administrador del sistema", .Result = False})
            '        FlagContolGenerateAccountNote = False
            '    End If
            'End If

            'Dim DateActual As Date = Date.Now
            ''valido que el consecutivo generado no este ya registrado 
            'strSql = "SELECT count(*) FROM " & NameContainer & "..CTNCOM" & Year(DateActual) & " WHERE COMCODIGO= '" & numerogenerado & "' AND CTNTIPCOM = '" & ConsecutivoNumcon & "'  "
            'Dim dtCountConsecutive As Integer = con.ExecuteCommand_Count(strSql)
            'If dtCountConsecutive > 0 Then
            '    ListError.Add(New InterfaceResult With {.Message = "El consecutivo " & numerogenerado & " del Tipo de comprobante difícil recaudo ya existe", .Result = False})
            '    FlagContolGenerateAccountNote = False
            'End If


            'If ListError.Count > 0 Then
            '    con.IndigoTransaction.Rollback()
            '    actionResult.ObjectEmbbeded = ListError
            '    actionResult.StateResult = False
            '    Return actionResult
            'End If


            'Dim fecha As String
            'fecha = Format(DateActual, "yyyyMMdd hh:mm:ss")
            'Dim ResultEje As Boolean
            ''Creacion del comprobante      Cabecera
            'strSql = "INSERT INTO " & NameContainer & "..CTNCOM" & Year(DateActual) & "(COMCODIGO,CTNTIPCOM,COMFECCOM,COMESTADO,COMDETALLE,COMOIDDOCU,COMNUMDOCU,COMOIDTYPE,COMELIMCOMP,COMCIERANU,COMGECGBATCH,OptimisticLockField)" &
            '"VALUES( '" & numerogenerado & "','" & OIDTipoDoc & "',convert(varchar(20),'" & fecha & "'),0,'Nomina Contable Indigo VIE - Grupo " & Group.Code & " ','" & numerogenerado & "','" & numerogenerado & "',0,NULL,0,NULL,0)"
            'ResultEje = con.ExecuteCommand(strSql)

            'Dim Auto As String
            'Dim SQL As New SqlDataAdapter("SELECT @@IDENTITY", con.sqlWebConection)
            'With SQL
            '    .SelectCommand.CommandTimeout = 90
            '    .SelectCommand.Transaction = con.IndigoTransaction
            '    .SelectCommand.CommandType = CommandType.Text
            '    Auto = .SelectCommand.ExecuteScalar.ToString
            'End With


            'Dim TotalFund As New Dictionary(Of String, Decimal)
            ''Diccionario para cuentas contables
            'Dim dictionaryAccounts As New Dictionary(Of String, String)
            'Dim Tercero As String = ""

            'For Each item As CostDistributions In ListCostDistribution

            '    'indexError += 1

            '    'If indexError = 468 Then
            '    '    Dim VarPryueba = ""
            '    'End If
            '    Dim Employee = ListEmployee.Where(Function(x) x.Id = item.EmployeeId).FirstOrDefault()

            '    Dim ConceptAccountingStructure As ConceptAccountingStructure = item.Concept.ConceptAccountingStructure.Where(Function(x) x.ConceptId = item.ConceptId And x.InterfazName = NameContainer And x.AccountingStructureId = Employee.Contract.Where(Function(y) y.Id = item.ContractId).SingleOrDefault().FunctionalUnit.AccountingStructureId).SingleOrDefault()
            '    If ConceptAccountingStructure IsNot Nothing Then

            '        FlagContolGenerateAccountNote = True
            '        Dim AccountFund As String

            '        Dim TotalConcept As Double

            '        If item.Concept.ConceptType <> "3" Then

            '            If item.Concept.ConceptClass <> "031" Or item.Concept.ConceptClass <> "033" Or item.Concept.ConceptClass <> "034" Or item.Concept.ConceptClass <> "008" _
            '                Or item.Concept.ConceptClass <> "007" Or item.Concept.ConceptClass <> "035" Or item.Concept.ConceptClass <> "036" Or item.Concept.ConceptClass <> "037" _
            '                Or item.Concept.ConceptClass <> "015" Or item.Concept.ConceptClass <> "018" Or item.Concept.ConceptClass <> "009" Then

            '                If item.Concept.ConceptClass = "017" Or item.Concept.ConceptClass = "014" Or item.Concept.ConceptClass = "038" Then
            '                    'Salud
            '                    If item.Concept.ConceptClass = "017" Then
            '                        Tercero = Employee.Contract.Where(Function(x) x.Id = item.ContractId).FirstOrDefault().FundContract.Where(Function(x) x.FundType = 1 And x.State = True).FirstOrDefault().Fund.ThirdParty.Nit
            '                        'Pensión
            '                    ElseIf item.Concept.ConceptClass = "014" Or item.Concept.ConceptClass = "038" Then
            '                        Tercero = Employee.Contract.Where(Function(x) x.Id = item.ContractId).FirstOrDefault().FundContract.Where(Function(x) x.FundType = 2 And x.State = True).FirstOrDefault().Fund.ThirdParty.Nit
            '                    End If

            '                    If Tercero <> Nothing Or Tercero <> "" Then
            '                        If Not dictionaryAccounts.ContainsKey(Tercero) Then
            '                            If item.Concept.ConceptType = "1" Then
            '                                dictionaryAccounts.Add(Tercero, ConceptAccountingStructure.AccruedAccount)
            '                                'dictionaryAccounts.Add(Tercero, item.Concept.ConceptAccountingStructure.Where(Function(x) x.InterfazName = NameContainer And x.AccountingStructureId = Employee.Contract.FunctionalUnit.AccountingStructureId).Single().AccruedAccount)
            '                            Else
            '                                'dictionaryAccounts.Add(Tercero, item.Concept.ConceptAccountingStructure.Where(Function(x) x.InterfazName = NameContainer And x.AccountingStructureId = item.Contract.FunctionalUnit.AccountingStructureId).Single().DeductedAccount)
            '                                dictionaryAccounts.Add(Tercero, ConceptAccountingStructure.DeductedAccount)
            '                            End If

            '                        End If

            '                        If TotalFund.ContainsKey(Tercero) = True Then
            '                            TotalFund(Tercero) += item.TotalConceptValue
            '                        Else
            '                            TotalFund.Add(Tercero, item.TotalConceptValue)
            '                        End If
            '                    End If

            '                Else
            '                    Tercero = Employee.ThirdParty.Nit

            '                    'consultamos el OID del tercero
            '                    Dim OIDtercero As Integer
            '                    strSql = "SELECT  OID FROM " & NameContainer & "..GENTERCER WHERE TERNUMDOC = '" & Tercero & "'"
            '                    Dim dtTercero As DataTable = con.ExecuteCommand_Data(strSql)
            '                    If dtTercero.Rows.Count > 0 Then
            '                        OIDtercero = CInt(dtTercero.Rows(0).Item("OID"))
            '                    Else
            '                        ListError.Add(New InterfaceResult With {.Message = "No se encontro ID del Tercero" & Tercero & ", favor avisar al administrador del sistema", .Result = False})
            '                        FlagContolGenerateAccountNote = False
            '                    End If
            '                    dtTercero = Nothing

            '                    'buscamos el OID de la cuenta acreditar y debitar
            '                    Dim strOIDCuentaCREDIT As String = item.DeductedAccount
            '                    If strOIDCuentaCREDIT = String.Empty Then
            '                        ListError.Add(New InterfaceResult With {.Message = "No se encontro cuenta de nomina, favor avisar al administrador del sistema", .Result = False})
            '                    End If

            '                    Dim OIDCuentaCredito As Integer
            '                    Dim ManejaCentroCosto As Boolean

            '                    strSql = "SELECT OID, CUEMANCEN FROM " & NameContainer & "..CTNCUENTA  WHERE CUECODIGO = '" & strOIDCuentaCREDIT & "'"
            '                    Dim dtCuentaCredito As DataTable = con.ExecuteCommand_Data(strSql)
            '                    If dtCuentaCredito.Rows.Count = 0 Then
            '                        ListError.Add(New InterfaceResult With {.Message = "No se encontro ID de la Cuenta Contable" & strOIDCuentaCREDIT & ", favor avisar al administrador del sistema", .Result = False})
            '                        FlagContolGenerateAccountNote = False
            '                    Else
            '                        OIDCuentaCredito = CInt(dtCuentaCredito.Rows(0).Item("OID"))
            '                        ManejaCentroCosto = CInt(dtCuentaCredito.Rows(0).Item("CUEMANCEN"))
            '                    End If

            '                    Dim strOIDCuentadebit As String = item.AccruedAccount
            '                    If strOIDCuentadebit = String.Empty Then
            '                        ListError.Add(New InterfaceResult With {.Message = "No se encontro cuenta de nomina del concepto, favor avisar al administrador del sistema", .Result = False})
            '                        FlagContolGenerateAccountNote = False
            '                    End If

            '                    Dim OIDCuentaDebito As Integer
            '                    strSql = "SELECT OID, CUEMANCEN FROM " & NameContainer & "..CTNCUENTA  WHERE CUECODIGO = '" & strOIDCuentadebit & "'"
            '                    Dim dtCuentaDebito As DataTable = con.ExecuteCommand_Data(strSql)
            '                    If dtCuentaDebito.Rows.Count = 0 Then
            '                        ListError.Add(New InterfaceResult With {.Message = "No se encontro ID de la Cuenta Contable" & strOIDCuentadebit & ", favor avisar al administrador del sistema", .Result = False})
            '                        FlagContolGenerateAccountNote = False
            '                    Else
            '                        OIDCuentaDebito = CInt(dtCuentaDebito.Rows(0).Item("OID"))
            '                        ManejaCentroCosto = CInt(dtCuentaDebito.Rows(0).Item("CUEMANCEN"))
            '                    End If

            '                    Dim OIDcentrocosto As String = ""
            '                    If ManejaCentroCosto = True Then
            '                        Dim centroCosto As String = Employee.CostCenter.Code
            '                        'OID centro de costo

            '                        strSql = "SELECT OID FROM " & NameContainer & "..CTNCENCOS  WHERE CCCODIGO = '" & centroCosto & "'"
            '                        Dim dtCentroCosto As DataTable = con.ExecuteCommand_Data(strSql)
            '                        If dtCentroCosto.Rows.Count = 0 Then
            '                            ListError.Add(New InterfaceResult With {.Message = "No se encontro ID del centro de costo" & centroCosto & ", favor avisar al administrador del sistema", .Result = False})
            '                            FlagContolGenerateAccountNote = False
            '                        Else
            '                            OIDcentrocosto = CInt(dtCentroCosto.Rows(0).Item("OID"))
            '                        End If
            '                        dtCentroCosto = Nothing
            '                    Else
            '                        OIDcentrocosto = "NULL"
            '                    End If

            '                    If item.Concept Is Nothing Then
            '                        Dim Var = "Prueba"
            '                    End If

            '                    If OIDcentrocosto Is Nothing Or OIDcentrocosto = "" Then
            '                        Dim Var = "Prueba"
            '                    End If

            '                    If ListError.Count > 0 Then
            '                        con.IndigoTransaction.Rollback()
            '                        actionResult.ObjectEmbbeded = ListError
            '                        actionResult.StateResult = False
            '                        Return actionResult
            '                    End If

            '                    If item.Concept.ConceptType = "1" Then

            '                        If item Is Nothing Or item.AccruedValue Is Nothing Then
            '                            Dim Var = "Prueba"
            '                        End If

            '                        'Detalle
            '                        strSql = "INSERT INTO " & NameContainer & "..CTNCOMD" & Year(DateActual) & "(CTNCOMCONC,CTNCUENTA,GENTERCER,CTNCENCOS,CMMVALDEB, CMMVALCRE, COMDETALLE, CTNCAURET, CMMESTCON, CMMFECCON,OptimisticLockField) " & _
            '                        "VALUES( '" & Auto & "'," & OIDCuentaDebito & ", " & OIDtercero & "," & OIDcentrocosto & ",convert(varchar(50),'" & item.AccruedValue & "'),0,'Nomina Contable Indigo Vie Cloud Platform Genesis " & item.PayrollDateLiquidated.ToString("yyyyMM") & " ',NULL,0,NULL,0)"
            '                        ResultEje = con.ExecuteCommand(strSql)
            '                    ElseIf item.Concept.ConceptType = "2" Then

            '                        If item Is Nothing Or item.DeductedValue Is Nothing Then
            '                            Dim Var = "Prueba"
            '                        End If

            '                        'Detalle
            '                        strSql = "INSERT INTO " & NameContainer & "..CTNCOMD" & Year(DateActual) & "(CTNCOMCONC,CTNCUENTA,GENTERCER,CTNCENCOS,CMMVALDEB, CMMVALCRE, COMDETALLE, CTNCAURET, CMMESTCON, CMMFECCON,OptimisticLockField) " & _
            '                        "VALUES( '" & Auto & "'," & OIDCuentaCredito & ", " & OIDtercero & "," & OIDcentrocosto & ",0,convert(varchar(50),'" & item.DeductedValue & "'),'Nomina Contable Indigo Vie Cloud Platform Genesis " & item.PayrollDateLiquidated.ToString("yyyyMM") & " ',NULL,0,NULL,0)"
            '                        ResultEje = con.ExecuteCommand(strSql)
            '                    End If

            '                End If
            '            End If
            '        End If

            '    End If
            'Next


            ''Recorro el Diccionario para Insertar los valores de Salud y Pensión
            'For Each Fund As KeyValuePair(Of String, Decimal) In TotalFund

            '    Tercero = Fund.Key
            '    Dim ValueConceptFund = Fund.Value
            '    Dim Account As String

            '    If dictionaryAccounts.ContainsKey(Fund.Key) = True Then
            '        For Each Accounts As KeyValuePair(Of String, String) In dictionaryAccounts
            '            If Tercero = Accounts.Key Then
            '                Account = Accounts.Value
            '            End If

            '        Next
            '    End If

            '    'consultamos el OID del tercero
            '    Dim OIDtercero As Integer
            '    strSql = "SELECT  OID FROM " & NameContainer & "..GENTERCER WHERE TERNUMDOC = '" & Tercero & "'"
            '    Dim dtTercero As DataTable = con.ExecuteCommand_Data(strSql)
            '    If dtTercero.Rows.Count > 0 Then
            '        OIDtercero = CInt(dtTercero.Rows(0).Item("OID"))
            '    Else
            '        ListError.Add(New InterfaceResult With {.Message = "No se encontro ID del Tercero" & Tercero & ", favor avisar al administrador del sistema", .Result = False})
            '        FlagContolGenerateAccountNote = False
            '    End If
            '    dtTercero = Nothing

            '    'buscamos el OID de la cuenta acreditar y debitar
            '    Dim strOIDCuentaCREDIT As String = Account
            '    If strOIDCuentaCREDIT = String.Empty Then
            '        ListError.Add(New InterfaceResult With {.Message = "No se encontro cuenta de nomina, favor avisar al administrador del sistema", .Result = False})
            '    End If

            '    Dim OIDCuentaCredito As Integer
            '    strSql = "SELECT OID FROM " & NameContainer & "..CTNCUENTA  WHERE CUECODIGO = '" & strOIDCuentaCREDIT & "'"
            '    Dim dtCuentaCredito As DataTable = con.ExecuteCommand_Data(strSql)
            '    If dtCuentaCredito.Rows.Count = 0 Then
            '        ListError.Add(New InterfaceResult With {.Message = "No se encontro ID de la Cuenta Contable" & strOIDCuentaCREDIT & ", favor avisar al administrador del sistema", .Result = False})
            '        FlagContolGenerateAccountNote = False
            '    Else
            '        OIDCuentaCredito = CInt(dtCuentaCredito.Rows(0).Item("OID"))
            '    End If

            '    If ListError.Count > 0 Then
            '        con.IndigoTransaction.Rollback()
            '        actionResult.ObjectEmbbeded = ListError
            '        actionResult.StateResult = False
            '        Return actionResult
            '    End If


            '    'Detalle
            '    strSql = "INSERT INTO " & NameContainer & "..CTNCOMD" & Year(DateActual) & "(CTNCOMCONC,CTNCUENTA,GENTERCER,CTNCENCOS,CMMVALDEB, CMMVALCRE, COMDETALLE, CTNCAURET, CMMESTCON, CMMFECCON,OptimisticLockField) " & _
            '    "VALUES( '" & Auto & "'," & OIDCuentaCredito & ", " & OIDtercero & ",NULL,0,convert(varchar(50),'" & ValueConceptFund & "'),'Nomina Contable Indigo Vie Cloud Platform Genesis " & PayrollDateLiquidated.ToString("yyyyMM") & " ',NULL,0,NULL,0)"
            '    ResultEje = con.ExecuteCommand(strSql)

            'Next


            ''Recorro la Liquidación para Sacar la Cuenta Principal de Nómina y su costo
            'For Each Liquidation As Liquidation In LiquidationConfirm

            '    Tercero = Liquidation.Employee.ThirdParty.Nit

            '    'consultamos el OID del tercero
            '    Dim OIDtercero As Integer
            '    strSql = "SELECT  OID FROM " & NameContainer & "..GENTERCER WHERE TERNUMDOC = '" & Tercero & "'"
            '    Dim dtTercero As DataTable = con.ExecuteCommand_Data(strSql)
            '    If dtTercero.Rows.Count > 0 Then
            '        OIDtercero = CInt(dtTercero.Rows(0).Item("OID"))
            '    Else
            '        ListError.Add(New InterfaceResult With {.Message = "No se encontro ID del Tercero" & Tercero & ", favor avisar al administrador del sistema", .Result = False})
            '        FlagContolGenerateAccountNote = False
            '    End If
            '    dtTercero = Nothing

            '    'buscamos el OID de la cuenta acreditar y debitar
            '    Dim strOIDCuentaCREDIT As String = Group.PayrollParameter.PayrollAccount
            '    If strOIDCuentaCREDIT = String.Empty Then
            '        ListError.Add(New InterfaceResult With {.Message = "No se encontro cuenta de nomina, favor avisar al administrador del sistema", .Result = False})
            '    End If

            '    Dim OIDCuentaCredito As Integer
            '    strSql = "SELECT OID FROM " & NameContainer & "..CTNCUENTA  WHERE CUECODIGO = '" & strOIDCuentaCREDIT & "'"
            '    Dim dtCuentaCredito As DataTable = con.ExecuteCommand_Data(strSql)
            '    If dtCuentaCredito.Rows.Count = 0 Then
            '        ListError.Add(New InterfaceResult With {.Message = "No se encontro ID de la Cuenta Contable" & strOIDCuentaCREDIT & ", favor avisar al administrador del sistema", .Result = False})
            '        FlagContolGenerateAccountNote = False
            '    Else
            '        OIDCuentaCredito = CInt(dtCuentaCredito.Rows(0).Item("OID"))
            '    End If

            '    If ListError.Count > 0 Then
            '        con.IndigoTransaction.Rollback()
            '        actionResult.ObjectEmbbeded = ListError
            '        actionResult.StateResult = False
            '        Return actionResult
            '    End If

            '    'Detalle
            '    strSql = "INSERT INTO " & NameContainer & "..CTNCOMD" & Year(DateActual) & "(CTNCOMCONC,CTNCUENTA,GENTERCER,CTNCENCOS,CMMVALDEB, CMMVALCRE, COMDETALLE, CTNCAURET, CMMESTCON, CMMFECCON,OptimisticLockField) " & _
            '    "VALUES( '" & Auto & "'," & OIDCuentaCredito & ", " & OIDtercero & ",NULL,0,convert(varchar(50),'" & Liquidation.TotalPaid & "'),'Nomina Contable Indigo Vie Cloud Platform Genesis " & PayrollDateLiquidated.ToString("yyyyMM") & " ',NULL,0,NULL,0)"
            '    ResultEje = con.ExecuteCommand(strSql)

            'Next

            'If FlagContolGenerateAccountNote = True Then
            '    ListInfo.Add(New InterfaceResult With {.Message = "Se Generaron los siguientes documentos: Comprobante Contable: " & numerogenerado & " - " & ConsecutivoNumcon & " para el grupo: " & Group.Code & ", Empresa : " & Group.PayrollParameter.InterfaceName, .Result = True, .Consecutive = numerogenerado & " - " & ConsecutivoNumcon})
            'End If



            ' '' COMPROBANTE CONTABLE PROVISIONES
            ''busco consecutivo comprobante diario mediante el consecutivo de comprobantes

            'ConsecutivoNumcon = 0
            'numerogenerado = 0
            'CodigoTipoDoc = String.Empty
            'OIDTipoDoc = 0
            'strSql = "SELECT top 1 OID,GENCONSEC,tccodigo FROM " & NameContainer & "..CTNTIPCOM WHERE TCCODIGO='" & ConsecutivoComprobanteProvisiones & "'"
            'dtConsecutivoNumcon = con.ExecuteCommand_Data(strSql)
            'If dtConsecutivoNumcon.Rows.Count = 0 Then
            '    ListError.Add(New InterfaceResult With {.Message = "No esta configurado los consecutivo para comprobantes  o ha ocurrido un error, favor avisar al administrador del sistema", .Result = False})
            '    FlagContolGenerateAccountNote = False
            'Else
            '    ConsecutivoNumcon = CInt(dtConsecutivoNumcon.Rows(0).Item("GENCONSEC").ToString.TrimEnd)
            '    CodigoTipoDoc = dtConsecutivoNumcon.Rows(0).Item("tccodigo").ToString.TrimEnd
            '    OIDTipoDoc = dtConsecutivoNumcon.Rows(0).Item("OID").ToString.TrimEnd
            '    'Actualiza el consecutive del comprobante
            '    strSql = "UPDATE " & NameContainer & "..geNconsec SET GCONUMERO = GCONUMERO +1  WHERE OID='" & ConsecutivoNumcon & "'"
            '    Dim resActualizarConsecutivo As Boolean = con.ExecuteCommand(strSql)
            '    If resActualizarConsecutivo = True Then
            '        'consultamo 
            '        Dim dtConseIncrementa As DataTable
            '        strSql = " SELECT top 1 GCONUMERO FROM " & NameContainer & "..geNconsec WHERE OID='" & ConsecutivoNumcon & "'"
            '        dtConseIncrementa = con.ExecuteCommand_Data(strSql)
            '        ' ConsecutivoNumcon = ConsecutivoNumcon + 1
            '        numerogenerado = CInt((dtConseIncrementa.Rows(0).Item("GCONUMERO")))
            '    Else
            '        ListError.Add(New InterfaceResult With {.Message = "ha ocurrido un error actualizando consecutivo, favor avisar al administrador del sistema", .Result = False})
            '        FlagContolGenerateAccountNote = False
            '    End If
            'End If

            'DateActual = Date.Now
            ''valido que el consecutivo generado no este ya registrado 
            'strSql = "SELECT count(*) FROM " & NameContainer & "..CTNCOM" & Year(DateActual) & " WHERE COMCODIGO= '" & numerogenerado & "' AND CTNTIPCOM = '" & ConsecutivoNumcon & "'  "
            'dtCountConsecutive = con.ExecuteCommand_Count(strSql)
            'If dtCountConsecutive > 0 Then
            '    ListError.Add(New InterfaceResult With {.Message = "El consecutivo " & numerogenerado & " del Tipo de comprobante difícil recaudo ya existe", .Result = False})
            '    FlagContolGenerateAccountNote = False
            'End If

            'If ListError.Count > 0 Then
            '    con.IndigoTransaction.Rollback()
            '    actionResult.ObjectEmbbeded = ListError
            '    actionResult.StateResult = False
            '    Return actionResult
            'End If

            'fecha = String.Empty
            'fecha = Format(DateActual, "yyyyMMdd hh:mm:ss")
            ''Dim ResultEje As Boolean
            ''Creacion del comprobante      Cabecera
            'strSql = "INSERT INTO " & NameContainer & "..CTNCOM" & Year(DateActual) & "(COMCODIGO,CTNTIPCOM,COMFECCOM,COMESTADO,COMDETALLE,COMOIDDOCU,COMNUMDOCU,COMOIDTYPE,COMELIMCOMP,COMCIERANU,COMGECGBATCH,OptimisticLockField)" &
            '"VALUES( '" & numerogenerado & "','" & OIDTipoDoc & "',convert(varchar(20),'" & fecha & "'),0,'Pago Aportes Contable Indigo Vie Cloud Platform Genesis - Grupo " & Group.Code & " Nómina " & PayrollDateLiquidated & " ','" & numerogenerado & "','" & numerogenerado & "',0,NULL,0,NULL,0)"
            'ResultEje = con.ExecuteCommand(strSql)


            'SQL = New SqlDataAdapter("SELECT @@IDENTITY", con.sqlWebConection)
            'With SQL
            '    .SelectCommand.CommandTimeout = 90
            '    .SelectCommand.Transaction = con.IndigoTransaction
            '    .SelectCommand.CommandType = CommandType.Text
            '    Auto = .SelectCommand.ExecuteScalar.ToString
            'End With

            'Dim TotalFundProvisions As New Dictionary(Of String, Decimal)

            'Dim ListCostDistributionParafiscal As New List(Of CostDistributions)

            ''Diccionario para Cuentas Debito
            'Dim dictionaryAccountsDebitoProvision As New Dictionary(Of String, String)

            ''Diccionario para Cuentas Credito
            'Dim dictionaryAccountsCreditoProvision As New Dictionary(Of String, String)

            ' ''Diccionario para Cuentas Débito y Centros de Costo
            'Dim TotalCostCenterProvision As New Dictionary(Of String, Dictionary(Of String, Decimal))

            'For Each item As CostDistributions In ListCostDistribution
            '    If item.Concept.ConceptClass = "035" Or item.Concept.ConceptClass = "036" Or item.Concept.ConceptClass = "037" _
            '      Or item.Concept.ConceptClass = "015" Or item.Concept.ConceptClass = "018" Or item.Concept.ConceptClass = "009" Then
            '        ListCostDistributionParafiscal.Add(item)
            '    End If
            'Next

            Dim FechaLiquidation As Date

            'For Each item As CostDistributions In ListCostDistributionParafiscal
            '    FechaLiquidation = item.PayrollDateLiquidated

            '    Dim Employee = ListEmployee.Where(Function(x) x.Id = item.EmployeeId).SingleOrDefault()

            '    If item.Concept.ConceptClass = "035" Or item.Concept.ConceptClass = "036" Or item.Concept.ConceptClass = "037" _
            '        Or item.Concept.ConceptClass = "015" Or item.Concept.ConceptClass = "018" Or item.Concept.ConceptClass = "009" _
            '          Then

            '        'Caja de Compensación
            '        If item.Concept.ConceptClass = "036" Then
            '            Tercero = Employee.Contract.Where(Function(x) x.Id = item.ContractId).SingleOrDefault().FundContract.Where(Function(x) x.FundType = 5 And x.State = True).SingleOrDefault().Fund.ThirdParty.Nit
            '            'Pension 
            '        ElseIf item.Concept.ConceptClass = "015" Then
            '            Tercero = Employee.Contract.Where(Function(x) x.Id = item.ContractId).SingleOrDefault().FundContract.Where(Function(x) x.FundType = 2 And x.State = True).SingleOrDefault().Fund.ThirdParty.Nit

            '            'Salud 
            '        ElseIf item.Concept.ConceptClass = "018" Then
            '            Tercero = Employee.Contract.Where(Function(x) x.Id = item.ContractId).SingleOrDefault().FundContract.Where(Function(x) x.FundType = 1 And x.State = True).SingleOrDefault().Fund.ThirdParty.Nit

            '            'ARL
            '        ElseIf item.Concept.ConceptClass = "009" Then
            '            Tercero = Employee.Contract.Where(Function(x) x.Id = item.ContractId).SingleOrDefault().FundContract.Where(Function(x) x.FundType = 4 And x.State = True).SingleOrDefault().Fund.ThirdParty.Nit
            '            'Tercero = "899999034"
            '            'SENA
            '        ElseIf item.Concept.ConceptClass = "035" Then
            '            Tercero = "899999034"
            '        ElseIf item.Concept.ConceptClass = "037" Then
            '            'ICBF
            '            Tercero = "899999239"

            '        End If


            '        Dim ConceptAccountingStructure As ConceptAccountingStructure = item.Concept.ConceptAccountingStructure.Where(Function(x) x.ConceptId = item.ConceptId And x.InterfazName = NameContainer And x.AccountingStructureId = Employee.Contract.Where(Function(y) y.Id = item.ContractId).SingleOrDefault().FunctionalUnit.AccountingStructureId).SingleOrDefault()


            '        If Not dictionaryAccountsDebitoProvision.ContainsKey(Tercero) Then
            '            dictionaryAccountsDebitoProvision.Add(Tercero, ConceptAccountingStructure.AccruedAccount)
            '        End If

            '        If Not dictionaryAccountsCreditoProvision.ContainsKey(Tercero) Then
            '            dictionaryAccountsCreditoProvision.Add(Tercero, ConceptAccountingStructure.DeductedAccount)
            '        End If


            '        If TotalFundProvisions.ContainsKey(Tercero) = True Then
            '            TotalFundProvisions(Tercero) += item.TotalConceptValue
            '        Else
            '            TotalFundProvisions.Add(Tercero, item.TotalConceptValue)
            '        End If

            '        If TotalCostCenterProvision.ContainsKey(Tercero) Then
            '            If TotalCostCenterProvision(Tercero).ContainsKey(Employee.CostCenter.Code) Then
            '                TotalCostCenterProvision(Tercero)(Employee.CostCenter.Code) += item.TotalConceptValue
            '            Else
            '                TotalCostCenterProvision(Tercero).Add(Employee.CostCenter.Code, item.TotalConceptValue)
            '            End If
            '        Else
            '            TotalCostCenterProvision.Add(Tercero, New Dictionary(Of String, Decimal))
            '            TotalCostCenterProvision(Tercero).Add(Employee.CostCenter.Code, item.TotalConceptValue)
            '        End If

            '        'Tercero = Employee.ThirdParty.Nit


            '    End If
            'Next


            'For Each CostCenterProvision As KeyValuePair(Of String, Dictionary(Of String, Decimal)) In TotalCostCenterProvision
            '    For Each TerceroProvision As KeyValuePair(Of String, Decimal) In CostCenterProvision.Value
            '        Dim CostCenterCode = TerceroProvision.Key
            '        Dim ValueConcept = TerceroProvision.Value
            '        Dim TerceroNit = CostCenterProvision.Key
            '        Dim DebitAccount As String

            '        For Each CuentaDebito As KeyValuePair(Of String, String) In dictionaryAccountsDebitoProvision
            '            If CuentaDebito.Key = TerceroNit Then
            '                DebitAccount = CuentaDebito.Value
            '            End If
            '        Next


            '        'consultamos el OID del tercero
            '        Dim OIDtercero As Integer
            '        strSql = "SELECT  OID FROM " & NameContainer & "..GENTERCER WHERE TERNUMDOC = '" & TerceroNit & "'"
            '        Dim dtTercero As DataTable = con.ExecuteCommand_Data(strSql)
            '        If dtTercero.Rows.Count > 0 Then
            '            OIDtercero = CInt(dtTercero.Rows(0).Item("OID"))
            '        Else
            '            ListError.Add(New InterfaceResult With {.Message = "No se encontro ID del Tercero" & Tercero & ", favor avisar al administrador del sistema", .Result = False})
            '            FlagContolGenerateAccountNote = False
            '        End If
            '        dtTercero = Nothing

            '        Dim ManejaCentroCosto As Boolean

            '        Dim strOIDCuentadebit As String = DebitAccount
            '        If strOIDCuentadebit = String.Empty Then
            '            ListError.Add(New InterfaceResult With {.Message = "No se encontro cuenta de nomina del concepto, favor avisar al administrador del sistema", .Result = False})
            '            FlagContolGenerateAccountNote = False
            '        End If

            '        Dim OIDCuentaDebito As Integer
            '        strSql = "SELECT OID, CUEMANCEN FROM " & NameContainer & "..CTNCUENTA  WHERE CUECODIGO = '" & strOIDCuentadebit & "'"
            '        Dim dtCuentaDebito As DataTable = con.ExecuteCommand_Data(strSql)
            '        If dtCuentaDebito.Rows.Count = 0 Then
            '            ListError.Add(New InterfaceResult With {.Message = "No se encontro ID de la Cuenta Contable" & strOIDCuentadebit & ", favor avisar al administrador del sistema", .Result = False})
            '            FlagContolGenerateAccountNote = False
            '        Else
            '            OIDCuentaDebito = CInt(dtCuentaDebito.Rows(0).Item("OID"))
            '            ManejaCentroCosto = CInt(dtCuentaDebito.Rows(0).Item("CUEMANCEN"))
            '        End If

            '        Dim OIDcentrocosto As String
            '        If ManejaCentroCosto = True Then
            '            Dim centroCosto As String = CostCenterCode
            '            'OID centro de costo

            '            strSql = "SELECT OID FROM " & NameContainer & "..CTNCENCOS  WHERE CCCODIGO = '" & centroCosto & "'"
            '            Dim dtCentroCosto As DataTable = con.ExecuteCommand_Data(strSql)
            '            If dtCentroCosto.Rows.Count = 0 Then
            '                ListError.Add(New InterfaceResult With {.Message = "No se encontro ID del centro de costo" & centroCosto & ", favor avisar al administrador del sistema", .Result = False})
            '                FlagContolGenerateAccountNote = False
            '            Else
            '                OIDcentrocosto = CInt(dtCentroCosto.Rows(0).Item("OID"))
            '            End If
            '            dtCentroCosto = Nothing
            '        Else
            '            OIDcentrocosto = "NULL"
            '        End If

            '        If ListError.Count > 0 Then
            '            con.IndigoTransaction.Rollback()
            '            actionResult.ObjectEmbbeded = ListError
            '            actionResult.StateResult = False
            '            Return actionResult
            '        End If

            '        'Detalle
            '        strSql = "INSERT INTO " & NameContainer & "..CTNCOMD" & Year(DateActual) & "(CTNCOMCONC,CTNCUENTA,GENTERCER,CTNCENCOS,CMMVALDEB, CMMVALCRE, COMDETALLE, CTNCAURET, CMMESTCON, CMMFECCON,OptimisticLockField) " & _
            '        "VALUES( '" & Auto & "'," & OIDCuentaDebito & ", " & OIDtercero & "," & OIDcentrocosto & ",convert(varchar(50),'" & ValueConcept & "'),0,'Nomina Contable Indigo Vie Cloud Platform Genesis " & FechaLiquidation.ToString("yyyyMM") & " ',NULL,0,NULL,0)"
            '        ResultEje = con.ExecuteCommand(strSql)

            '    Next

            'Next

            ''Recorro el Diccionario para Insertar los valores de Salud y Pensión
            'For Each FundProvision As KeyValuePair(Of String, Decimal) In TotalFundProvisions
            '    Tercero = FundProvision.Key
            '    Dim ValueConceptFund = FundProvision.Value
            '    Dim DebitoAccount As String
            '    Dim CreditoAccount As String

            '    'Cargamos las Cuentas Debito 
            '    If dictionaryAccountsDebitoProvision.ContainsKey(FundProvision.Key) = True Then
            '        For Each Accounts As KeyValuePair(Of String, String) In dictionaryAccountsDebitoProvision
            '            If Tercero = Accounts.Key Then
            '                DebitoAccount = Accounts.Value
            '            End If
            '        Next
            '    End If

            '    'Cargamos las Cuentas Crédito
            '    If dictionaryAccountsCreditoProvision.ContainsKey(FundProvision.Key) = True Then
            '        For Each Accounts As KeyValuePair(Of String, String) In dictionaryAccountsCreditoProvision
            '            If Tercero = Accounts.Key Then
            '                CreditoAccount = Accounts.Value
            '            End If
            '        Next
            '    End If

            '    'consultamos el OID del tercero
            '    Dim OIDtercero As Integer
            '    strSql = "SELECT  OID FROM " & NameContainer & "..GENTERCER WHERE TERNUMDOC = '" & Tercero & "'"
            '    Dim dtTercero As DataTable = con.ExecuteCommand_Data(strSql)
            '    If dtTercero.Rows.Count > 0 Then
            '        OIDtercero = CInt(dtTercero.Rows(0).Item("OID"))
            '    Else
            '        ListError.Add(New InterfaceResult With {.Message = "No se encontro ID del Tercero" & Tercero & ", favor avisar al administrador del sistema", .Result = False})
            '        FlagContolGenerateAccountNote = False
            '    End If
            '    dtTercero = Nothing

            '    'buscamos el OID de la cuenta acreditar y debitar
            '    Dim strOIDCuentaCREDIT As String = CreditoAccount
            '    If strOIDCuentaCREDIT = String.Empty Then
            '        ListError.Add(New InterfaceResult With {.Message = "No se encontro cuenta de nomina, favor avisar al administrador del sistema", .Result = False})
            '    End If

            '    Dim OIDCuentaCredito As Integer
            '    strSql = "SELECT OID FROM " & NameContainer & "..CTNCUENTA  WHERE CUECODIGO = '" & strOIDCuentaCREDIT & "'"
            '    Dim dtCuentaCredito As DataTable = con.ExecuteCommand_Data(strSql)
            '    If dtCuentaCredito.Rows.Count = 0 Then
            '        ListError.Add(New InterfaceResult With {.Message = "No se encontro ID de la Cuenta Contable" & strOIDCuentaCREDIT & ", favor avisar al administrador del sistema", .Result = False})
            '        FlagContolGenerateAccountNote = False
            '    Else
            '        OIDCuentaCredito = CInt(dtCuentaCredito.Rows(0).Item("OID"))
            '    End If

            '    If ListError.Count > 0 Then
            '        con.IndigoTransaction.Rollback()
            '        actionResult.ObjectEmbbeded = ListError
            '        actionResult.StateResult = False
            '        Return actionResult
            '    End If

            '    'Detalle
            '    strSql = "INSERT INTO " & NameContainer & "..CTNCOMD" & Year(DateActual) & "(CTNCOMCONC,CTNCUENTA,GENTERCER,CTNCENCOS,CMMVALDEB, CMMVALCRE, COMDETALLE, CTNCAURET, CMMESTCON, CMMFECCON,OptimisticLockField) " & _
            '    "VALUES( '" & Auto & "'," & OIDCuentaCredito & ", " & OIDtercero & ",NULL,0,convert(varchar(50),'" & ValueConceptFund & "'),'Nomina Contable Indigo Vie Cloud Platform Genesis " & PayrollDateLiquidated.ToString("yyyyMM") & " ',NULL,0,NULL,0)"
            '    ResultEje = con.ExecuteCommand(strSql)

            'Next

            'If FlagContolGenerateAccountNote = True Then
            '    ListInfo.Add(New InterfaceResult With {.Message = "Se Generaron los siguientes documentos: Comprobante Contable: " & numerogenerado & " - " & ConsecutivoNumcon & " para el grupo: " & Group.Code & ", Empresa : " & Group.PayrollParameter.InterfaceName, .Result = True, .Consecutive = numerogenerado & " - " & ConsecutivoNumcon})
            'End If

            ' '' COMPROBANTE CONTABLE PRESTACIONES SOCIALES
            'ConsecutivoNumcon = 0
            'numerogenerado = 0
            'CodigoTipoDoc = String.Empty
            'OIDTipoDoc = 0
            'strSql = "SELECT top 1 OID,GENCONSEC,tccodigo FROM " & NameContainer & "..CTNTIPCOM WHERE TCCODIGO='" & ConsecutivoComprobantePrestaciones & "'"
            'dtConsecutivoNumcon = con.ExecuteCommand_Data(strSql)
            'If dtConsecutivoNumcon.Rows.Count = 0 Then
            '    ListError.Add(New InterfaceResult With {.Message = "No esta configurado los consecutivo para comprobantes de Prestaciones  o ha ocurrido un error, favor avisar al administrador del sistema", .Result = False})
            '    FlagContolGenerateAccountNote = False
            'Else
            '    ConsecutivoNumcon = CInt(dtConsecutivoNumcon.Rows(0).Item("GENCONSEC").ToString.TrimEnd)
            '    CodigoTipoDoc = dtConsecutivoNumcon.Rows(0).Item("tccodigo").ToString.TrimEnd
            '    OIDTipoDoc = dtConsecutivoNumcon.Rows(0).Item("OID").ToString.TrimEnd
            '    'Actualiza el consecutive del comprobante
            '    strSql = "UPDATE " & NameContainer & "..geNconsec SET GCONUMERO = GCONUMERO +1  WHERE OID='" & ConsecutivoNumcon & "'"
            '    Dim resActualizarConsecutivo As Boolean = con.ExecuteCommand(strSql)
            '    If resActualizarConsecutivo = True Then
            '        'consultamo 
            '        Dim dtConseIncrementa As DataTable
            '        strSql = " SELECT top 1 GCONUMERO FROM " & NameContainer & "..geNconsec WHERE OID='" & ConsecutivoNumcon & "'"
            '        dtConseIncrementa = con.ExecuteCommand_Data(strSql)
            '        ' ConsecutivoNumcon = ConsecutivoNumcon + 1
            '        numerogenerado = CInt((dtConseIncrementa.Rows(0).Item("GCONUMERO")))
            '    Else
            '        ListError.Add(New InterfaceResult With {.Message = "ha ocurrido un error actualizando consecutivo - Prestaciones -, favor avisar al administrador del sistema", .Result = False})
            '        FlagContolGenerateAccountNote = False
            '    End If
            'End If

            'DateActual = Date.Now
            ''valido que el consecutivo generado no este ya registrado 
            'strSql = "SELECT count(*) FROM " & NameContainer & "..CTNCOM" & Year(DateActual) & " WHERE COMCODIGO= '" & numerogenerado & "' AND CTNTIPCOM = '" & ConsecutivoNumcon & "'  "
            'dtCountConsecutive = con.ExecuteCommand_Count(strSql)
            'If dtCountConsecutive > 0 Then
            '    ListError.Add(New InterfaceResult With {.Message = "El consecutivo " & numerogenerado & " del Tipo de comprobante difícil recaudo ya existe", .Result = False})
            '    FlagContolGenerateAccountNote = False
            'End If

            'If ListError.Count > 0 Then
            '    con.IndigoTransaction.Rollback()
            '    actionResult.ObjectEmbbeded = ListError
            '    actionResult.StateResult = False
            '    Return actionResult
            'End If

            'fecha = String.Empty
            'fecha = Format(DateActual, "yyyyMMdd hh:mm:ss")
            ''Dim ResultEje As Boolean
            ''Creacion del comprobante      Cabecera
            'strSql = "INSERT INTO " & NameContainer & "..CTNCOM" & Year(DateActual) & "(COMCODIGO,CTNTIPCOM,COMFECCOM,COMESTADO,COMDETALLE,COMOIDDOCU,COMNUMDOCU,COMOIDTYPE,COMELIMCOMP,COMCIERANU,COMGECGBATCH,OptimisticLockField)" &
            '"VALUES( '" & numerogenerado & "','" & OIDTipoDoc & "',convert(varchar(20),'" & fecha & "'),0,'Pago Nómina - Prestaciones - Contable Indigo Vie Cloud Platform Genesis - Grupo " & Group.Code & " Nómina " & PayrollDateLiquidated & " ','" & numerogenerado & "','" & numerogenerado & "',0,NULL,0,NULL,0)"
            'ResultEje = con.ExecuteCommand(strSql)


            'SQL = New SqlDataAdapter("SELECT @@IDENTITY", con.sqlWebConection)
            'With SQL
            '    .SelectCommand.CommandTimeout = 90
            '    .SelectCommand.Transaction = con.IndigoTransaction
            '    .SelectCommand.CommandType = CommandType.Text
            '    Auto = .SelectCommand.ExecuteScalar.ToString
            'End With

            'Dim ListCostDistributionPrestaciones As New List(Of CostDistributions)

            'For Each item As CostDistributions In ListCostDistribution

            '    If item.Concept.ConceptClass = "031" Or item.Concept.ConceptClass = "033" Or item.Concept.ConceptClass = "034" Or item.Concept.ConceptClass = "008" Then
            '        ListCostDistributionPrestaciones.Add(item)
            '    End If
            'Next

            'For Each item As CostDistributions In ListCostDistributionPrestaciones

            '    Dim Employee = ListEmployee.Where(Function(x) x.Id = item.EmployeeId).SingleOrDefault()

            '    Dim ConceptAccountingStructure As ConceptAccountingStructure = item.Concept.ConceptAccountingStructure.Where(Function(x) x.ConceptId = item.ConceptId And x.InterfazName = NameContainer And x.AccountingStructureId = Employee.Contract.Where(Function(y) y.Id = item.ContractId).SingleOrDefault().FunctionalUnit.AccountingStructureId).SingleOrDefault()


            '    If ConceptAccountingStructure IsNot Nothing Then

            '        Tercero = Employee.ThirdParty.Nit

            '        'consultamos el OID del tercero
            '        Dim OIDtercero As Integer
            '        strSql = "SELECT  OID FROM " & NameContainer & "..GENTERCER WHERE TERNUMDOC = '" & Tercero & "'"
            '        Dim dtTercero As DataTable = con.ExecuteCommand_Data(strSql)
            '        If dtTercero.Rows.Count > 0 Then
            '            OIDtercero = CInt(dtTercero.Rows(0).Item("OID"))
            '        Else
            '            ListError.Add(New InterfaceResult With {.Message = "No se encontro ID del Tercero" & Tercero & ", favor avisar al administrador del sistema", .Result = False})
            '            FlagContolGenerateAccountNote = False
            '        End If
            '        dtTercero = Nothing

            '        Dim ManejaCentroCosto As Boolean

            '        Dim strOIDCuentadebit As String = ConceptAccountingStructure.AccruedAccount
            '        If strOIDCuentadebit = String.Empty Then
            '            ListError.Add(New InterfaceResult With {.Message = "No se encontro cuenta de nomina del concepto, favor avisar al administrador del sistema", .Result = False})
            '            FlagContolGenerateAccountNote = False
            '        End If

            '        Dim OIDCuentaDebito As Integer
            '        strSql = "SELECT OID, CUEMANCEN FROM " & NameContainer & "..CTNCUENTA  WHERE CUECODIGO = '" & strOIDCuentadebit & "'"
            '        Dim dtCuentaDebito As DataTable = con.ExecuteCommand_Data(strSql)
            '        If dtCuentaDebito.Rows.Count = 0 Then
            '            ListError.Add(New InterfaceResult With {.Message = "No se encontro ID de la Cuenta Contable" & strOIDCuentadebit & ", favor avisar al administrador del sistema", .Result = False})
            '            FlagContolGenerateAccountNote = False
            '        Else
            '            OIDCuentaDebito = CInt(dtCuentaDebito.Rows(0).Item("OID"))
            '            ManejaCentroCosto = CInt(dtCuentaDebito.Rows(0).Item("CUEMANCEN"))
            '        End If

            '        Dim OIDcentrocosto As String
            '        If ManejaCentroCosto = True Then
            '            Dim centroCosto As String = Employee.CostCenter.Code
            '            'OID centro de costo

            '            strSql = "SELECT OID FROM " & NameContainer & "..CTNCENCOS  WHERE CCCODIGO = '" & centroCosto & "'"
            '            Dim dtCentroCosto As DataTable = con.ExecuteCommand_Data(strSql)
            '            If dtCentroCosto.Rows.Count = 0 Then
            '                ListError.Add(New InterfaceResult With {.Message = "No se encontro ID del centro de costo" & centroCosto & ", favor avisar al administrador del sistema", .Result = False})
            '                FlagContolGenerateAccountNote = False
            '            Else
            '                OIDcentrocosto = CInt(dtCentroCosto.Rows(0).Item("OID"))
            '            End If
            '            dtCentroCosto = Nothing
            '        Else
            '            OIDcentrocosto = "NULL"
            '        End If

            '        If ListError.Count > 0 Then
            '            con.IndigoTransaction.Rollback()
            '            actionResult.ObjectEmbbeded = ListError
            '            actionResult.StateResult = False
            '            Return actionResult
            '        End If

            '        'Detalle
            '        strSql = "INSERT INTO " & NameContainer & "..CTNCOMD" & Year(DateActual) & "(CTNCOMCONC,CTNCUENTA,GENTERCER,CTNCENCOS,CMMVALDEB, CMMVALCRE, COMDETALLE, CTNCAURET, CMMESTCON, CMMFECCON,OptimisticLockField) " & _
            '        "VALUES( '" & Auto & "'," & OIDCuentaDebito & ", " & OIDtercero & "," & OIDcentrocosto & ",convert(varchar(50),'" & item.TotalConceptValue & "'),0,'Nomina Contable Indigo Vie Cloud Platform Genesis " & item.PayrollDateLiquidated.ToString("yyyyMM") & " ',NULL,0,NULL,0)"
            '        ResultEje = con.ExecuteCommand(strSql)

            '        'buscamos el OID de la cuenta acreditar y debitar
            '        Dim strOIDCuentaCREDIT As String = ConceptAccountingStructure.DeductedAccount
            '        If strOIDCuentaCREDIT = String.Empty Then
            '            ListError.Add(New InterfaceResult With {.Message = "No se encontro cuenta de nomina, favor avisar al administrador del sistema", .Result = False})
            '        End If

            '        Dim OIDCuentaCredito As Integer
            '        strSql = "SELECT OID, CUEMANCEN FROM " & NameContainer & "..CTNCUENTA  WHERE CUECODIGO = '" & strOIDCuentaCREDIT & "'"
            '        Dim dtCuentaCredito As DataTable = con.ExecuteCommand_Data(strSql)
            '        If dtCuentaCredito.Rows.Count = 0 Then
            '            ListError.Add(New InterfaceResult With {.Message = "No se encontro ID de la Cuenta Contable" & strOIDCuentaCREDIT & ", favor avisar al administrador del sistema", .Result = False})
            '            FlagContolGenerateAccountNote = False
            '        Else
            '            OIDCuentaCredito = CInt(dtCuentaCredito.Rows(0).Item("OID"))
            '            ManejaCentroCosto = CInt(dtCuentaCredito.Rows(0).Item("CUEMANCEN"))
            '        End If

            '        'Detalle
            '        strSql = "INSERT INTO " & NameContainer & "..CTNCOMD" & Year(DateActual) & "(CTNCOMCONC,CTNCUENTA,GENTERCER,CTNCENCOS,CMMVALDEB, CMMVALCRE, COMDETALLE, CTNCAURET, CMMESTCON, CMMFECCON,OptimisticLockField) " & _
            '        "VALUES( '" & Auto & "'," & OIDCuentaCredito & ", " & OIDtercero & ",NULL,0,convert(varchar(50),'" & item.TotalConceptValue & "'),'Nomina Contable Indigo Vie Cloud Platform Genesis " & PayrollDateLiquidated.ToString("yyyyMM") & " ',NULL,0,NULL,0)"
            '        ResultEje = con.ExecuteCommand(strSql)
            '    End If

            'Next

            'If FlagContolGenerateAccountNote = True Then
            '    ListInfo.Add(New InterfaceResult With {.Message = "Se Generaron los siguientes documentos: Comprobante Contable: " & numerogenerado & " - " & ConsecutivoNumcon & " para el grupo: " & Group.Code & ", Empresa : " & Group.PayrollParameter.InterfaceName, .Result = True, .Consecutive = numerogenerado & " - " & ConsecutivoNumcon})
            'End If

            Dim Flag As Boolean

            ' Con Errores
            If ListError.Count > 0 Then
                con.IndigoTransaction.Rollback()
                Flag = False
                'Return ListError
            Else
                'Sin Errores
                con.IndigoTransaction.Commit()
                Flag = True
                'Return ListInfo
            End If

            If Flag = False Then
                actionResult.ObjectEmbbeded = ListError
            Else
                actionResult.ObjectEmbbeded = ListInfo
            End If

            actionResult.StateResult = Flag
            Return actionResult


        Catch ex As Exception
            Console.WriteLine(indexError)
            con.IndigoTransaction.Rollback()
            actionResult.StateResult = False
            actionResult.ObjectEmbbeded = New List(Of InterfaceResult)({New InterfaceResult With {.Message = ex.Message, .Result = False}})
            Return actionResult
        Finally
            con.sqlWebConection.Close()
        End Try
    End Function


End Class
