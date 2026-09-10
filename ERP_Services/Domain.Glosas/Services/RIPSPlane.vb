'***********************************************************************
' Assembly         : Domain.Glosas
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 19-02-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base.Entities
Imports Domain.Common.Entities
Imports Domain.Entities
Imports System.Text
Imports Infrastructure.CrossCutting.Base
Imports System.IO

Public Class RIPSPlane
    Implements IRIPSPlane

#Region "Variables Públicas"
    Dim CountUSFile As Integer
    Dim CountACFile As Integer
    Dim CountADFile As Integer
    Dim CountAPFile As Integer
    Dim CountATFile As Integer
    Dim CountANFile As Integer
    Dim CountAFFFile As Integer
    Dim CountAUFile As Integer
    Dim CountAHFile As Integer
    Dim CountAMFile As Integer
    Dim GlobalCodigoEntidadAdministradora As String
#End Region

    ''' <summary>
    ''' Función para Crear los RIPS
    ''' </summary>
    ''' <param name="IdRadicateInvoice">IdRadicateInvoice</param>
    ''' <param name="IndigoSessionValues">Variable de Sesión</param>
    ''' <returns>List(Of ActionMessageResult(Of StringBuilder))</returns>
    ''' <remarks></remarks>
    Public Function GenerateRIPS(IdRadicateInvoice As Integer, ByVal IndigoSessionValues As SessionValues) As List(Of ActionMessageResult(Of StringBuilder)) Implements IRIPSPlane.GenerateRIPS

        CountUSFile = 0
        CountACFile = 0
        CountADFile = 0
        CountAPFile = 0
        CountATFile = 0
        CountANFile = 0
        CountAFFFile = 0
        CountAUFile = 0
        CountAHFile = 0
        CountAMFile = 0

        Dim result As New StringBuilder()

        Dim ListPlaneRIPS As New List(Of ActionMessageResult(Of StringBuilder))

        Dim VarAFFFile = AFFile(IdRadicateInvoice, IndigoSessionValues)
        Dim VarUSFile = USFile(IdRadicateInvoice, IndigoSessionValues)
        Dim VarACFile = ACFile(IdRadicateInvoice, IndigoSessionValues)
        Dim VarADFile = ADFile(IdRadicateInvoice, IndigoSessionValues)
        Dim VarAPFile = APFile(IdRadicateInvoice, IndigoSessionValues)
        Dim VarATFile = ATFile(IdRadicateInvoice, IndigoSessionValues)
        Dim VarANFile = ANFile(IdRadicateInvoice, IndigoSessionValues)
        Dim VarAUFile = AUFile(IdRadicateInvoice, IndigoSessionValues)
        Dim VarAHFile = AHFile(IdRadicateInvoice, IndigoSessionValues)
        'Dim VarAMFile = AMFile(IdRadicateInvoice, IndigoSessionValues)

        If VarUSFile.StateResult = True Then
            ListPlaneRIPS.Add(VarUSFile)
        End If

        If VarACFile.StateResult = True Then
            ListPlaneRIPS.Add(VarACFile)
        End If

        If VarADFile.StateResult = True Then
            ListPlaneRIPS.Add(VarADFile)
        End If

        If VarAPFile.StateResult = True Then
            ListPlaneRIPS.Add(VarAPFile)
        End If

        If VarATFile.StateResult = True Then
            ListPlaneRIPS.Add(VarATFile)
        End If

        If VarANFile.StateResult = True Then
            ListPlaneRIPS.Add(VarANFile)
        End If

        If VarAFFFile.StateResult = True Then
            ListPlaneRIPS.Add(VarAFFFile)
        End If

        If VarAUFile.StateResult = True Then
            ListPlaneRIPS.Add(VarAUFile)
        End If

        If VarAHFile.StateResult = True Then
            ListPlaneRIPS.Add(VarAHFile)
        End If

        'If VarAMFile.StateResult = True Then
        '    ListPlaneRIPS.Add(VarAMFile)
        'End If

        Dim VarCTFile = CTFile(ListPlaneRIPS)

        If VarCTFile.StateResult = True Then
            ListPlaneRIPS.Add(VarCTFile)
        End If

        Return ListPlaneRIPS

    End Function

    ''' <summary>
    ''' Función pra calcular el tamaño máximo del campo que utilizará el plano
    ''' </summary>
    ''' <param name="VarField">Campo</param>
    ''' <param name="MaxLength">Tamaño Máximo</param>
    ''' <returns>Integer</returns>
    ''' <remarks></remarks>
    Public Function MaxSize(VarField As String, MaxLength As Integer) As Integer
        Dim Size As Integer = 0

        If Len(VarField) < MaxLength Then
            Size = Len(VarField)
        Else
            Size = MaxLength
        End If

        Return Size

    End Function

    Public Function CTFile(ListPlaneRIPS As List(Of ActionMessageResult(Of StringBuilder))) As ActionMessageResult(Of StringBuilder)
        Dim ActualMonth = Month(Date.Now).ToString()
        Dim ActualYear = Year(Date.Now).ToString()
        Dim result As New ActionMessageResult(Of StringBuilder)
        Dim resultPlane As New StringBuilder()
        Dim MonthValid As String

        Try

            If Len(ActualMonth) = 1 Then
                MonthValid = "0" & ActualMonth
            Else
                MonthValid = ActualMonth
            End If

            If ListPlaneRIPS.Count() > 0 Then
                For i As Integer = 0 To ListPlaneRIPS.Count() - 1

                    Dim lineHead As String = ""
                    Dim RegisterCount As Integer = 0

                    If ListPlaneRIPS.Item(i).Message = "US" + ActualMonth + ActualYear Then
                        RegisterCount = CountUSFile
                    End If

                    If ListPlaneRIPS.Item(i).Message = "AC" + ActualMonth + ActualYear Then
                        RegisterCount = CountACFile
                    End If

                    If ListPlaneRIPS.Item(i).Message = "AP" + ActualMonth + ActualYear Then
                        RegisterCount = CountAPFile
                    End If

                    If ListPlaneRIPS.Item(i).Message = "AD" + ActualMonth + ActualYear Then
                        RegisterCount = CountADFile
                    End If

                    If ListPlaneRIPS.Item(i).Message = "AT" + ActualMonth + ActualYear Then
                        RegisterCount = CountATFile
                    End If

                    If ListPlaneRIPS.Item(i).Message = "AN" + ActualMonth + ActualYear Then
                        RegisterCount = CountANFile
                    End If

                    If ListPlaneRIPS.Item(i).Message = "AF" + ActualMonth + ActualYear Then
                        RegisterCount = CountAFFFile
                    End If

                    If ListPlaneRIPS.Item(i).Message = "AU" + ActualMonth + ActualYear Then
                        RegisterCount = CountAUFile
                    End If

                    If ListPlaneRIPS.Item(i).Message = "AH" + ActualMonth + ActualYear Then
                        RegisterCount = CountAHFile
                    End If

                    If ListPlaneRIPS.Item(i).Message = "AM" + ActualMonth + ActualYear Then
                        RegisterCount = CountAMFile
                    End If

                    If i > 0 Then
                        lineHead = vbCrLf
                    End If

                    lineHead &= Utils.StringPad(GlobalCodigoEntidadAdministradora, MaxSize(GlobalCodigoEntidadAdministradora, 10), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(Date.Now.ToString("dd/MM/yyyy"), MaxSize((Date.Now.ToString("dd/MM/yyyy")), 20), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(ListPlaneRIPS.Item(i).Message & ActualYear.ToString() & MonthValid, MaxSize(ListPlaneRIPS.Item(i).Message & ActualYear.ToString() & MonthValid, 8), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(RegisterCount, MaxSize(RegisterCount, 10), " ", Utils.PadType.STR_PAD_RIGHT)
                    resultPlane.Append(lineHead)
                Next

                result.Message = "CT" + ActualMonth + ActualYear
                result.ObjectEmbbeded = resultPlane
                result.StateResult = True
            Else
                result.StateResult = False
            End If

        Catch ex As Exception
            result.StateResult = False
        End Try

        Return result
    End Function

    ''' <summary>
    ''' Archivo RIP AF (Archivo de Transacciones)
    ''' </summary>
    ''' <param name="IdRadicateInvoice">Id Radicación</param>
    ''' <param name="IndigoSessionValues">Variables de Sesión</param>
    ''' <returns>ActionMessageResult(Of StringBuilder)</returns>
    ''' <remarks></remarks>
    Public Function AFFile(IdRadicateInvoice As Integer, ByVal IndigoSessionValues As SessionValues) As ActionMessageResult(Of StringBuilder)
        Dim resultPlane As New StringBuilder()
        Dim result As New ActionMessageResult(Of StringBuilder)
        Dim ContainerGenesis = IndigoSessionValues.TransactionalContainer
        Dim HISContainer = IndigoSessionValues.HisContainer

        Dim ActualMonth = Month(Date.Now).ToString()
        Dim ActualYear = Year(Date.Now).ToString()

        Dim cnx As New System.Data.SqlClient.SqlConnection(Infrastructure.CrossCutting.Base.Utils.GetEntityConnectionString(Infrastructure.CrossCutting.Base.ConfigurationFile.CONX_GENESIS, String.Empty, ContainerGenesis, False))

        Try
            cnx.Open()

            Dim command As New System.Data.SqlClient.SqlCommand("", cnx)

            command.CommandTimeout = 30000

            command.CommandType = CommandType.Text

            command.CommandText = "SELECT LTRIM(RTRIM(IPS.CODIGOIPS)) AS CodigoIps, LTRIM(RTRIM(IPS.DSCRIPIPS)) as RazonSocial,'NI' as TipoIdentificacion, ISNULL(LTRIM(RTRIM(IPS.CODIGONIT)),0) AS NitIPS, I.InvoiceNumber as NumeroFactura, CONVERT(VARCHAR(10),I.InvoiceDate,103) as FechaFactura, HA.HealthEntityCode AS CodigoEntidadAdministradora, " +
                "TP.Name as NombreEntidadAdministradora, CONT.ContractNumber as NumeroContrato, CG.Name as PlanBeneficios,'' AS NumeroPoliza, I.TotalPatientSalesPrice as TotalPagoCompartido, 0 as ValorComision,I.PatientDiscount as TotalDescuentos, I.TotalPatientWithDiscount as NetoPagar " +
                "FROM " & ContainerGenesis & ".Billing.Invoice I, " & ContainerGenesis & ".Billing.RevenueControlDetail RCD, " & ContainerGenesis & ".[Contract].HealthAdministrator HA, " & ContainerGenesis & ".Common.ThirdParty TP, " & HISContainer & ".[dbo].[ADCENATEN] AS CA, " & HISContainer & ".[dbo].[ADCONTIPS] IPS, " & HISContainer & ".[dbo].[ADINGRESO] as Ingreso, " & ContainerGenesis & ".[Contract].CareGroup CG, " & ContainerGenesis & ".[Contract].[Contract] CONT, " +
                "" & ContainerGenesis & ".Portfolio.RadicateInvoiceC RC, " & ContainerGenesis & ".Portfolio.RadicateInvoiceD RD WHERE i.RevenueControlDetailId = RCD.Id AND RCD.HealthAdministratorId = HA.Id AND HA.ThirdPartyId = TP.Id AND I.AdmissionNumber = Ingreso.NUMINGRES AND Ingreso.CODCENATE = CA.CODCENATE AND CA.CODCENATE = IPS.CODCENATE  " +
                "AND I.CareGroupId = CG.Id AND CG.ContractId = CONT.Id AND RC.Id = RD.RadicateInvoiceCId AND RD.InvoiceNumber = I.InvoiceNumber AND RC.Id = '" & IdRadicateInvoice & "'"

            Dim da As New System.Data.SqlClient.SqlDataAdapter(command)

            cnx.Close()

            Dim dt As New Data.DataTable()

            da.Fill(dt)

            If dt.Rows.Count > 0 Then

                CountAFFFile = dt.Rows.Count()

                For Each r As DataRow In dt.Rows

                    Dim CodigoIps As String = r("CodigoIps")
                    Dim RazonSocial As String = r("RazonSocial")
                    Dim TipoIdentificacion As String = r("TipoIdentificacion")
                    Dim NitIPS As String = r("NitIPS")
                    Dim NumeroFactura As String = r("NumeroFactura")
                    Dim FechaFactura As String = r("FechaFactura")
                    Dim CodigoEntidadAdministradora As String = r("CodigoEntidadAdministradora")
                    GlobalCodigoEntidadAdministradora = CodigoEntidadAdministradora
                    Dim NombreEntidadAdministradora As String = r("NombreEntidadAdministradora")
                    Dim NumeroContrato As String = r("NumeroContrato")
                    Dim PlanBeneficios As String = r("PlanBeneficios")
                    Dim NumeroPoliza As String = r("NumeroPoliza")
                    Dim TotalPagoCompartido As String = r("TotalPagoCompartido")
                    Dim ValorComision As String = r("ValorComision")
                    Dim TotalDescuentos As String = r("TotalDescuentos")
                    Dim NetoPagar As String = r("NetoPagar")

                    Dim FechaInicio As Date = New Date(Year(Date.Now), Month(Date.Now), 1)
                    Dim FechaFin As Date = DateSerial(Year(FechaInicio), Month(FechaInicio) + 1, 0)

                    Dim lineHead As String = ""

                    If dt.Rows.IndexOf(r) > 0 Then
                        lineHead = vbCrLf
                    End If

                    lineHead &= Utils.StringPad(CodigoIps, MaxSize(CodigoIps, 10), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(RazonSocial, MaxSize(RazonSocial, 60), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(TipoIdentificacion, MaxSize(TipoIdentificacion, 2), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(NitIPS, MaxSize(NitIPS, 20), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(NitIPS, MaxSize(NitIPS, 20), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(NumeroFactura, MaxSize(NumeroFactura, 20), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(FechaFactura, MaxSize(FechaFactura, 10), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(FechaInicio, MaxSize(FechaInicio, 10), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(FechaFin, MaxSize(FechaFin, 10), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(CodigoEntidadAdministradora, MaxSize(CodigoEntidadAdministradora, 6), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(NombreEntidadAdministradora, MaxSize(NombreEntidadAdministradora, 30), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(NumeroContrato, MaxSize(NumeroContrato, 15), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(PlanBeneficios, MaxSize(PlanBeneficios, 30), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(NumeroPoliza, MaxSize(NumeroPoliza, 10), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(TotalPagoCompartido, MaxSize(TotalPagoCompartido, 15), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(ValorComision, MaxSize(ValorComision, 15), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(TotalDescuentos, MaxSize(TotalDescuentos, 15), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(NetoPagar, MaxSize(NetoPagar, 15), " ", Utils.PadType.STR_PAD_RIGHT)

                    resultPlane.Append(lineHead)
                Next

                result.Message = "AF" + ActualMonth + ActualYear
                result.ObjectEmbbeded = resultPlane
                result.StateResult = True

            Else
                result.StateResult = False
            End If

        Catch ex As Exception
            result.StateResult = False
        End Try

        Return result

    End Function

    ''' <summary>
    ''' Archivo RIP US (Archivo de Usuarios de los Servicios de Salud)
    ''' </summary>
    ''' <param name="IdRadicateInvoice">Id Radicación</param>
    ''' <param name="IndigoSessionValues">Variables de Sesión</param>
    ''' <returns>ActionMessageResult(Of StringBuilder)</returns>
    ''' <remarks></remarks>
    Public Function USFile(IdRadicateInvoice As Integer, ByVal IndigoSessionValues As SessionValues) As ActionMessageResult(Of StringBuilder)
        Dim resultPlane As New StringBuilder()
        Dim result As New ActionMessageResult(Of StringBuilder)
        Dim ContainerGenesis = IndigoSessionValues.TransactionalContainer
        Dim HISContainer = IndigoSessionValues.HisContainer

        Dim ActualMonth = Month(Date.Now).ToString()
        Dim ActualYear = Year(Date.Now).ToString()

        Dim cnx As New System.Data.SqlClient.SqlConnection(Infrastructure.CrossCutting.Base.Utils.GetEntityConnectionString(Infrastructure.CrossCutting.Base.ConfigurationFile.CONX_GENESIS, String.Empty, ContainerGenesis, False))

        Try
            cnx.Open()

            Dim command As New System.Data.SqlClient.SqlCommand("", cnx)

            command.CommandTimeout = 30000

            command.CommandType = CommandType.Text

            command.CommandText = "select CASE IPTIPODOC WHEN 1 THEN 'CC' WHEN 2 THEN 'CE' WHEN 5 THEN 'PA' WHEN 4 THEN 'RC' WHEN 3 THEN 'TI' WHEN 6 THEN 'AS' WHEN 7 THEN 'MS' WHEN 8 THEN 'NU' END as TipoIdenficacion, " +
                "LTRIM(RTRIM(IPCODPACI)) as NumeroIdentificacion, HA.HealthEntityCode AS CodigoEntidadAdministradora, " +
                "CASE IPTIPOPAC WHEN 0 THEN 1 WHEN 1 THEN 2 WHEN 2 THEN 3 WHEN 3 THEN 4 ELSE 5 END AS TipoUsuario, " +
                "LTRIM(RTRIM(INP.IPPRIAPEL)) AS PrimerApellido, LTRIM(RTRIM(INP.IPSEGAPEL)) as SegundoApellido, LTRIM(RTRIM(INP.IPPRINOMB)) as PrimerNombre,  LTRIM(RTRIM(INP.IPSEGNOMB)) as SegundoNombre, DATEDIFF(YEAR,INP.IPFECNACI,GETDATE()) as Edad, 1 as UnidadMedidaEdad, " +
                "CASE IPSEXOPAC WHEN 1 THEN 'M' WHEN 2 THEN 'F' END as Sexo, SubString(UBI.DEPMUNCOD,0,3) as Departamento, SubString(UBI.DEPMUNCOD,3,5) as Ciudad, CASE TIPOUBICA WHEN 0 THEN 'U' WHEN 1 THEN 'R' END AS ZonaResidencial " +
                "FROM " & HISContainer & ".[dbo].[INPACIENT] INP, " & ContainerGenesis & ".Billing.Invoice I, " & ContainerGenesis & ".Billing.RevenueControlDetail RCD, " & ContainerGenesis & ".[Contract].HealthAdministrator HA, " & HISContainer & ".[dbo].[INUBICACI] UBI,  " & ContainerGenesis & ".Portfolio.RadicateInvoiceC RC,  " & ContainerGenesis & ".Portfolio.RadicateInvoiceD RCDD " +
                "WHERE INP.IPCODPACI = I.PatientCode AND I.RevenueControlDetailId = RCD.Id AND RCD.HealthAdministratorId = HA.Id AND INP.AUUBICACI = UBI.AUUBICACI AND RC.Id = RCDD.RadicateInvoiceCId AND I.InvoiceNumber = RCDD.InvoiceNumber AND RC.Id = '" & IdRadicateInvoice & "'"


            Dim da As New System.Data.SqlClient.SqlDataAdapter(command)

            cnx.Close()

            Dim dt As New Data.DataTable()

            da.Fill(dt)

            If dt.Rows.Count > 0 Then

                CountUSFile = dt.Rows.Count()

                For Each r As DataRow In dt.Rows

                    Dim TipoIdenficacion As String = r("TipoIdenficacion")
                    Dim NumeroIdentificacion As String = r("NumeroIdentificacion")
                    Dim CodigoEntidadAdministradora As String = r("CodigoEntidadAdministradora")
                    GlobalCodigoEntidadAdministradora = CodigoEntidadAdministradora
                    Dim TipoUsuario As String = r("TipoUsuario")
                    Dim PrimerApellido As String = r("PrimerApellido")
                    Dim SegundoApellido As String = r("SegundoApellido")
                    Dim PrimerNombre As String = r("PrimerNombre")
                    Dim SegundoNombre As String = r("SegundoNombre")
                    Dim Edad As String = r("Edad")
                    Dim UnidadMedidaEdad As String = r("UnidadMedidaEdad")
                    Dim Sexo As String = r("Sexo")
                    Dim Departamento As String = r("Departamento")
                    Dim Ciudad As String = r("Ciudad")
                    Dim ZonaResidencial As String = r("ZonaResidencial")

                    Dim lineHead As String = ""

                    If dt.Rows.IndexOf(r) > 0 Then
                        lineHead = vbCrLf
                    End If

                    lineHead &= Utils.StringPad(TipoIdenficacion, 2, " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(NumeroIdentificacion, MaxSize(NumeroIdentificacion, 20), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(CodigoEntidadAdministradora, MaxSize(CodigoEntidadAdministradora, 6), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(TipoUsuario, 1, " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(PrimerApellido, MaxSize(PrimerApellido, 30), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(SegundoApellido, MaxSize(SegundoApellido, 30), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(PrimerNombre, MaxSize(PrimerNombre, 20), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(SegundoNombre, MaxSize(SegundoNombre, 20), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(Edad, MaxSize(Edad, 3), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(UnidadMedidaEdad, 1, " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(Sexo, 1, " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(Departamento, 2, " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(Ciudad, 3, " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(ZonaResidencial, 1, " ", Utils.PadType.STR_PAD_RIGHT)
                    resultPlane.Append(lineHead)
                Next
                result.Message = "US" + ActualMonth + ActualYear
                result.ObjectEmbbeded = resultPlane
                result.StateResult = True

            Else
                result.StateResult = False
            End If

        Catch ex As Exception
            cnx.Close()
            result.StateResult = False
        End Try

        Return result

    End Function

    Public Function AMFile(IdRadicateInvoice As Integer, ByVal IndigoSessionValues As SessionValues) As ActionMessageResult(Of StringBuilder)
        Dim resultPlane As New StringBuilder()
        Dim result As New ActionMessageResult(Of StringBuilder)
        Dim ContainerGenesis = IndigoSessionValues.TransactionalContainer
        Dim HISContainer = IndigoSessionValues.HisContainer

        Dim ActualMonth = Month(Date.Now).ToString()
        Dim ActualYear = Year(Date.Now).ToString()

        Dim cnx As New System.Data.SqlClient.SqlConnection(Infrastructure.CrossCutting.Base.Utils.GetEntityConnectionString(Infrastructure.CrossCutting.Base.ConfigurationFile.CONX_GENESIS, String.Empty, ContainerGenesis, False))

        Try

            cnx.Open()

            Dim command As New System.Data.SqlClient.SqlCommand("", cnx)

            command.CommandTimeout = 30000

            command.CommandType = CommandType.Text

            command.CommandText = "SELECT HA.HealthEntityCode AS CodigoEntidadAdministradora, IPS.CODIGOIPS AS CodigoIps, CASE IPTIPODOC WHEN 1 THEN 'CC' WHEN 2 THEN 'CE' WHEN 5 THEN 'PA' WHEN 4 THEN 'RC' WHEN 3 THEN 'TI' WHEN 6 THEN 'AS' WHEN 7 THEN 'MS' WHEN 8 THEN 'NU' END as TipoIdentificacion, " +
            "INP.IPCODPACI as NumeroIdentificacion, SOD.AuthorizationNumber as NumeroAutorizacion, IP.CodeCUM as CodigoMedicamento, SOD.ProductId, CASE IP.POSProduct WHEN 1 THEN 1 ELSE 2 END as TipoMedicamento, IP.Name as NombreMedicamento, IP.Presentation as FormaFarmaceutica, ATC.Concentration as Concentracion, " +
            "IMU.Name as UnidadMedida, SOD.InvoicedQuantity as NumeroUnidades, SOD.TotalSalesPrice as ValorUnitarioMedicamento, SOD.GrandTotalSalesPrice as ValorTotalMedicamento " +
            "FROM " & ContainerGenesis & ".Billing.Invoice I, " & ContainerGenesis & ".Billing.RevenueControlDetail RCD, " & ContainerGenesis & ".[Contract].HealthAdministrator HA, " & HISContainer & ".[dbo].[ADCONTIPS] IPS,  " & HISContainer & ".[dbo].[ADCENATEN] AS CA,  " & HISContainer & ".[dbo].[ADINGRESO] as Ingreso, " & HISContainer & ".[dbo].[INPACIENT] INP, " +
            "" & ContainerGenesis & ".Billing.ServiceOrderDetail SOD, " & ContainerGenesis & ".Billing.InvoiceDetail ID, " & ContainerGenesis & ".[Contract].IPSService IPSSer, " & ContainerGenesis & ".Inventory.InventoryProduct IP, " & ContainerGenesis & ".Inventory.ATC as ATC, " & ContainerGenesis & ".Inventory.InventoryMeasurementUnit IMU, " & ContainerGenesis & ".Inventory.ProductType PT, " +
            "" & ContainerGenesis & ".Portfolio.RadicateInvoiceC RC, " & ContainerGenesis & ".Portfolio.RadicateInvoiceD RD " +
            "WHERE I.RevenueControlDetailId = RCD.Id AND RCD.HealthAdministratorId = HA.Id And I.AdmissionNumber = Ingreso.NUMINGRES AND Ingreso.CODCENATE = CA.CODCENATE AND CA.CODIPSSEC = IPS.CODIGOIPS AND I.PatientCode = INP.IPCODPACI AND ID.InvoiceId = I.Id AND ID.ServiceOrderDetailId = SOD.Id " +
            "AND SOd.IPSServiceId = IPSSer.Id AND IPSSer.Presentation = 1 AND IPSser.SubattentionCode in (21,22) AND ID.ServiceOrderDetailId = SOD.Id AND SOD.ProductId = IP.Id AND IP.ATCId = ATC.Id AND ATC.WeightMeasureUnit = IMU.Id AND IP.ProductTypeId = PT.Id AND RC.Id = RD.RadicateInvoiceCId AND RD.InvoiceNumber = I.InvoiceNumber " +
            "AND RC.Id = '" & IdRadicateInvoice & "'"""

            Dim da As New System.Data.SqlClient.SqlDataAdapter(command)

            cnx.Close()

            Dim dt As New Data.DataTable()

            da.Fill(dt)

            If dt.Rows.Count > 0 Then

                For Each r As DataRow In dt.Rows

                    Dim TipoIdenficacion = r("TipoIdenficacion")
                    Dim NumeroIdentificacion = r("NumeroIdentificacion")
                    Dim CodigoEntidadAdministradora = r("CodigoEntidadAdministradora")
                    Dim TipoUsuario = r("TipoUsuario")
                    Dim PrimerApellido = r("PrimerApellido")
                    Dim SegundoApellido = r("SegundoApellido")
                    Dim PrimerNombre = r("PrimerNombre")
                    Dim SegundoNombre = r("SegundoNombre")
                    Dim Edad = r("Edad")
                    Dim UnidadMedidaEdad = r("UnidadMedidaEdad")
                    Dim Sexo = r("Sexo")
                    Dim Departamento = r("Departamento")
                    Dim Ciudad = r("Ciudad")
                    Dim ZonaResidencial = r("ZonaResidencial")

                    Dim lineHead As String = ""

                    If dt.Rows.IndexOf(r) > 0 Then
                        lineHead = vbCrLf
                    End If

                    lineHead &= Utils.StringPad(TipoIdenficacion, 2, " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(NumeroIdentificacion, 20, " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(CodigoEntidadAdministradora, 6, " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(TipoUsuario, 1, " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(PrimerApellido, 30, " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(SegundoApellido, 30, " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(PrimerNombre, 20, " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(SegundoNombre, 20, " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(Edad, 3, " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(UnidadMedidaEdad, 1, " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(Sexo, 1, " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(Departamento, 2, " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(Ciudad, 3, " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(ZonaResidencial, 1, " ", Utils.PadType.STR_PAD_RIGHT)
                    resultPlane.Append(lineHead)
                Next

                result.Message = "AM" + ActualMonth + ActualYear
                result.ObjectEmbbeded = resultPlane
                result.StateResult = True

            Else
                result.StateResult = False
            End If

        Catch ex As Exception
            cnx.Close()
            result.StateResult = False
        End Try

        Return result

    End Function

    ''' <summary>
    ''' Archivo RIP AC (Archivo de Consulta)
    ''' </summary>
    ''' <param name="IdRadicateInvoice">Id Radicación</param>
    ''' <param name="IndigoSessionValues">Variables de Sesión</param>
    ''' <returns>ActionMessageResult(Of StringBuilder)</returns>
    ''' <remarks></remarks>
    Public Function ACFile(IdRadicateInvoice As Integer, ByVal IndigoSessionValues As SessionValues) As ActionMessageResult(Of StringBuilder)

        Dim resultPlane As New StringBuilder()
        Dim result As New ActionMessageResult(Of StringBuilder)
        Dim ContainerGenesis = IndigoSessionValues.TransactionalContainer
        Dim HISContainer = IndigoSessionValues.HisContainer

        Dim ActualMonth = Month(Date.Now).ToString()
        Dim ActualYear = Year(Date.Now).ToString()

        Dim cnx As New System.Data.SqlClient.SqlConnection(Infrastructure.CrossCutting.Base.Utils.GetEntityConnectionString(Infrastructure.CrossCutting.Base.ConfigurationFile.CONX_GENESIS, String.Empty, ContainerGenesis, False))

        Try
            cnx.Open()

            Dim command As New System.Data.SqlClient.SqlCommand("", cnx)

            command.CommandTimeout = 30000

            command.CommandType = CommandType.Text

            command.CommandText = "SELECT I.InvoiceNumber as NumeroFactura, HA.HealthEntityCode AS CodigoEntidadAdministradora, CASE IPTIPODOC WHEN 1 THEN 'CC' WHEN 2 THEN 'CE' WHEN 5 THEN 'PA' WHEN 4 THEN 'RC' WHEN 3 THEN 'TI' " +
                "WHEN 6 THEN 'AS' WHEN 7 THEN 'MS' WHEN 8 THEN 'NU' END as TipoIdenficacion, LTRIM(RTRIM(INP.IPCODPACI)) as NumeroIdentificacion, CONVERT(VARCHAR(10),Ingreso.IFECHAING,103) AS FechaConsulta, LTRIM(RTRIM(Ingreso.IAUTORIZA)) as NumeroAutorizacion, " +
                "LTRIM(RTRIM(Ingreso.NUMINGRES)) as CodigoConsulta, 10 as FinalidadConsulta, CASE ITIPORIES WHEN 10 THEN 01 WHEN 6 THEN 02 WHEN 7 THEN 06 WHEN 3 THEN 13 WHEN 4 THEN 13 WHEN 2 THEN 14 ELSE 15 END as CausaExterna, Diag.CODDIAGNO as CodigoDiagnosticoPrincipal, " +
                "CASE Diag.TIPDIAGNO WHEN 'I' THEN 1 WHEN 'C' THEN 2 WHEN 'R' THEN 3 END as TipoDiagnostico FROM " & HISContainer & ".[dbo].[INPACIENT] INP, " & ContainerGenesis & ".Billing.Invoice I, " & ContainerGenesis & ".Billing.RevenueControlDetail RCD, " & ContainerGenesis & ".[Contract].HealthAdministrator HA, " +
                " " & HISContainer & ".dbo.ADINGRESO Ingreso, " & HISContainer & ".dbo.INDIAGNOP Diag, " & ContainerGenesis & ".Portfolio.RadicateInvoiceC RC, " & ContainerGenesis & ".Portfolio.RadicateInvoiceD RCDD WHERE INP.IPCODPACI = I.PatientCode AND I.RevenueControlDetailId = RCD.Id AND RCD.HealthAdministratorId = HA.Id " +
                "AND Ingreso.IPCODPACI = INP.IPCODPACI AND Diag.NUMINGRES = ingreso.NUMINGRES AND RC.Id = RCDD.RadicateInvoiceCId AND I.InvoiceNumber = RCDD.InvoiceNumber AND RC.Id = '" & IdRadicateInvoice & "'"

            Dim da As New System.Data.SqlClient.SqlDataAdapter(command)

            cnx.Close()

            Dim dt As New Data.DataTable()

            da.Fill(dt)

            If dt.Rows.Count > 0 Then

                CountACFile = dt.Rows.Count()

                For Each r As DataRow In dt.Rows
                    Dim NumeroFactura As String = r("NumeroFactura")
                    Dim CodigoEntidadAdministradora As String = r("CodigoEntidadAdministradora")
                    Dim TipoIdenficacion As String = r("TipoIdenficacion")
                    Dim NumeroIdentificacion As String = r("NumeroIdentificacion")
                    Dim FechaConsulta As String = r("FechaConsulta")
                    Dim NumeroAutorizacion As String = r("NumeroAutorizacion")
                    Dim CodigoConsulta As String = r("CodigoConsulta")
                    Dim FinalidadConsulta As String = r("FinalidadConsulta")
                    Dim CausaExterna As String = r("CausaExterna")
                    Dim CodigoDiagnosticoPrincipal As String = r("CodigoDiagnosticoPrincipal")
                    Dim TipoDiagnostico As String = r("TipoDiagnostico")

                    Dim lineHead As String = ""

                    If dt.Rows.IndexOf(r) > 0 Then
                        lineHead = vbCrLf
                    End If

                    lineHead &= Utils.StringPad(NumeroFactura, MaxSize(NumeroFactura, 20), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(CodigoEntidadAdministradora, MaxSize(CodigoEntidadAdministradora, 10), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(TipoIdenficacion, 2, " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(NumeroIdentificacion, MaxSize(NumeroIdentificacion, 20), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(FechaConsulta, MaxSize(FechaConsulta, 10), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(NumeroAutorizacion, MaxSize(NumeroAutorizacion, 15), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(CodigoConsulta, MaxSize(CodigoConsulta, 8), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(FinalidadConsulta, 2, " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(CausaExterna, MaxSize(CausaExterna, 2), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(CodigoDiagnosticoPrincipal, MaxSize(CodigoDiagnosticoPrincipal, 4), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(CodigoDiagnosticoPrincipal, MaxSize(CodigoDiagnosticoPrincipal, 4), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(CodigoDiagnosticoPrincipal, MaxSize(CodigoDiagnosticoPrincipal, 4), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(CodigoDiagnosticoPrincipal, MaxSize(CodigoDiagnosticoPrincipal, 4), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(TipoDiagnostico, 1, " ", Utils.PadType.STR_PAD_RIGHT)
                    'lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)

                    'Falta los Valores Totales
                    resultPlane.Append(lineHead)

                Next
                result.Message = "AC" + ActualMonth + ActualYear
                result.ObjectEmbbeded = resultPlane
                result.StateResult = True

            Else
                result.StateResult = False
            End If

        Catch ex As Exception
            cnx.Close()
            result.StateResult = False
        End Try

        Return result

    End Function

    ''' <summary>
    ''' Archivo RIP AD (Archivo de Descripción Agrupada de los servicios de Salud Prestados)
    ''' </summary>
    ''' <param name="IdRadicateInvoice">Id Radicación</param>
    ''' <param name="IndigoSessionValues">Variables de Sesión</param>
    ''' <returns>ActionMessageResult(Of StringBuilder)</returns>
    ''' <remarks></remarks>
    Public Function ADFile(IdRadicateInvoice As Integer, ByVal IndigoSessionValues As SessionValues) As ActionMessageResult(Of StringBuilder)

        Dim resultPlane As New StringBuilder()
        Dim result As New ActionMessageResult(Of StringBuilder)
        Dim ContainerGenesis = IndigoSessionValues.TransactionalContainer
        Dim HISContainer = IndigoSessionValues.HisContainer

        Dim ActualMonth = Month(Date.Now).ToString()
        Dim ActualYear = Year(Date.Now).ToString()

        Dim cnx As New System.Data.SqlClient.SqlConnection(Infrastructure.CrossCutting.Base.Utils.GetEntityConnectionString(Infrastructure.CrossCutting.Base.ConfigurationFile.CONX_GENESIS, String.Empty, ContainerGenesis, False))
        Try

            cnx.Open()

            Dim command As New System.Data.SqlClient.SqlCommand("", cnx)

            command.CommandTimeout = 30000

            command.CommandType = CommandType.Text

            command.CommandText = "SELECT I.InvoiceNumber as NumeroFactura, HA.HealthEntityCode AS CodigoEntidadAdministradora, CUS.RIPSConcept as CodigoConcepto, SOD.InvoicedQuantity as Cantidad, SOD.TotalSalesPrice as ValorUnitario, " +
                "SOD.GrandTotalSalesPrice as ValorTotalConcepto " +
                "FROM " & ContainerGenesis & ".Billing.Invoice I inner join " & ContainerGenesis & ".Billing.InvoiceDetail ID on I.Id = Id.InvoiceId inner join " & ContainerGenesis & ".Billing.RevenueControlDetail RCD on I.RevenueControlDetailId = RCD.Id inner join " +
                "" & ContainerGenesis & ".[Contract].HealthAdministrator HA on RCD.HealthAdministratorId = HA.Id inner join " & ContainerGenesis & ".Billing.ServiceOrderDetail SOD on ID.ServiceOrderDetailId = SOD.Id inner join " & ContainerGenesis & ".[Contract].CUPSEntity CUS on SOD.CUPSEntityId = CUS.Id inner join " +
                "" & ContainerGenesis & ".Billing.BillingGroup BG on CUS.BillingGroupId = BG.Id inner join " & ContainerGenesis & ".Portfolio.RadicateInvoiceD RD on RD.InvoiceNumber = I.InvoiceNumber inner join " & ContainerGenesis & ".Portfolio.RadicateInvoiceC RC on RC.Id = RD.RadicateInvoiceCId " +
                "WHERE SOD.SettlementType <> 3 AND RC.Id = '" & IdRadicateInvoice & "'"

            Dim da As New System.Data.SqlClient.SqlDataAdapter(command)

            cnx.Close()

            Dim dt As New Data.DataTable()

            da.Fill(dt)

            If dt.Rows.Count > 0 Then

                CountACFile = dt.Rows.Count()

                For Each r As DataRow In dt.Rows
                    Dim NumeroFactura As String = r("NumeroFactura")
                    Dim CodigoEntidadAdministradora As String = r("CodigoEntidadAdministradora")
                    Dim CodigoConcepto As String = r("CodigoConcepto")
                    Dim Cantidad As String = r("Cantidad")
                    Dim ValorUnitario As String = r("ValorUnitario")
                    Dim ValorTotalConcepto As String = r("ValorTotalConcepto")

                    Dim lineHead As String = ""

                    If dt.Rows.IndexOf(r) > 0 Then
                        lineHead = vbCrLf
                    End If

                    lineHead &= Utils.StringPad(NumeroFactura, MaxSize(NumeroFactura, 20), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(CodigoEntidadAdministradora, MaxSize(CodigoEntidadAdministradora, 10), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(CodigoConcepto, MaxSize(CodigoConcepto, 2), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(Cantidad, MaxSize(Cantidad, 15), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(ValorUnitario, MaxSize(ValorUnitario, 15), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(ValorTotalConcepto, MaxSize(ValorTotalConcepto, 15), " ", Utils.PadType.STR_PAD_RIGHT)

                    resultPlane.Append(lineHead)

                Next
                result.Message = "AD" + ActualMonth + ActualYear
                result.ObjectEmbbeded = resultPlane
                result.StateResult = True
            Else
                result.StateResult = False

            End If

        Catch ex As Exception
            cnx.Close()
            result.StateResult = False
        End Try

        Return result
    End Function


    ''' <summary>
    ''' Archivo RIP AP (Archivo de Procedimientos)
    ''' </summary>
    ''' <param name="IdRadicateInvoice">Id Radicación</param>
    ''' <param name="IndigoSessionValues">Variables de Sesión</param>
    ''' <returns>ActionMessageResult(Of StringBuilder)</returns>
    ''' <remarks></remarks>
    Public Function APFile(IdRadicateInvoice As Integer, ByVal IndigoSessionValues As SessionValues) As ActionMessageResult(Of StringBuilder)

        Dim resultPlane As New StringBuilder()
        Dim result As New ActionMessageResult(Of StringBuilder)
        Dim ContainerGenesis = IndigoSessionValues.TransactionalContainer
        Dim HISContainer = IndigoSessionValues.HisContainer

        Dim ActualMonth = Month(Date.Now).ToString()
        Dim ActualYear = Year(Date.Now).ToString()

        Dim cnx As New System.Data.SqlClient.SqlConnection(Infrastructure.CrossCutting.Base.Utils.GetEntityConnectionString(Infrastructure.CrossCutting.Base.ConfigurationFile.CONX_GENESIS, String.Empty, ContainerGenesis, False))
        Try

            cnx.Open()

            Dim command As New System.Data.SqlClient.SqlCommand("", cnx)

            command.CommandTimeout = 30000

            command.CommandType = CommandType.Text

            command.CommandText = "SELECT I.InvoiceNumber as NumeroFactura, HA.HealthEntityCode AS CodigoEntidadAdministradora, CASE IPTIPODOC WHEN 1 THEN 'CC' WHEN 2 THEN 'CE' WHEN 5 THEN 'PA' WHEN 4 THEN 'RC' WHEN 3 THEN 'TI' WHEN 6 THEN 'AS' WHEN 7 THEN 'MS' WHEN 8 THEN 'NU' END as TipoIdenficacion, " +
                "LTRIM(RTRIM(INP.IPCODPACI)) as NumeroIdentificacion, CONVERT(VARCHAR(10),SOD.ServiceDate,103) as FechaProcedimiento, ISNULL(SOD.AuthorizationNumber,'') as NumeroAutorizacion, CUPS.RIPSCode as CodigoProcedimiento, 1 as AmbitoRealizacion, CASE IPS.ServiceType WHEN 2 THEN 1 WHEN 3 THEN 2 WHEN 4 THEN 3 WHEN 5 THEN 4 WHEN 6 THEN 5 ELSE '' END as FinalidadProcedimiento, " +
            "0 as PersonalAtiende, 'I500' as Diagnostico, 'I500' as DiagnosticoRelacionado,'' as Complicacion, CASE SOD.SurgicalInterventionType WHEN 1 THEN 1 WHEN 6 THEN 2 WHEN 4 THEN 3 WHEN 7 THEN 4 WHEN 5 THEN 5 ELSE '' END as FormaActoQuirurgico, SOD.GrandTotalSalesPrice AS ValorProcedimiento " +
            "FROM " & HISContainer & ".[dbo].[INPACIENT] INP, " & ContainerGenesis & ".Billing.Invoice I, " & ContainerGenesis & ".Billing.InvoiceDetail ID, " & ContainerGenesis & ".Billing.RevenueControlDetail RCD,  " & ContainerGenesis & ".[Contract].HealthAdministrator HA, " & ContainerGenesis & ".Billing.ServiceOrderDetail SOD, " & ContainerGenesis & ".[Contract].IPSService IPS, " & ContainerGenesis & ".[Contract].CUPSEntity CUPS, " & ContainerGenesis & ".Portfolio.RadicateInvoiceC RC," +
            "" & ContainerGenesis & ".Portfolio.RadicateInvoiceD RD WHERE INP.IPCODPACI = I.PatientCode AND I.RevenueControlDetailId = RCD.Id AND RCD.HealthAdministratorId = HA.Id AND ID.InvoiceId = I.Id AND ID.ServiceOrderDetailId = SOD.Id AND SOD.IPSServiceId = IPS.Id AND CUPS.Id = SOD.CUPSEntityId AND RC.Id = RD.RadicateInvoiceCId AND RD.InvoiceNumber = I.InvoiceNumber " +
            "AND IPS.SubattentionCode in (1,16,17,18,23,24,25,26,27,28,29,30,31,32,34) AND RC.Id = '" & IdRadicateInvoice & "'"

            Dim da As New System.Data.SqlClient.SqlDataAdapter(command)

            cnx.Close()

            Dim dt As New Data.DataTable()

            da.Fill(dt)

            If dt.Rows.Count > 0 Then

                CountAPFile = dt.Rows.Count()

                For Each r As DataRow In dt.Rows
                    Dim NumeroFactura As String = r("NumeroFactura")
                    Dim CodigoEntidadAdministradora As String = r("CodigoEntidadAdministradora")
                    Dim TipoIdenficacion As String = r("TipoIdenficacion")
                    Dim NumeroIdentificacion As String = r("NumeroIdentificacion")
                    Dim FechaProcedimiento As String = r("FechaProcedimiento")
                    Dim NumeroAutorizacion As String = r("NumeroAutorizacion")
                    Dim CodigoProcedimiento As String = r("CodigoProcedimiento")
                    Dim AmbitoRealizacion As String = r("AmbitoRealizacion")
                    Dim FinalidadProcedimiento As String = r("FinalidadProcedimiento")
                    Dim PersonalAtiende As String = r("PersonalAtiende")
                    Dim Diagnostico As String = r("Diagnostico")
                    Dim DiagnosticoRelacionado As String = r("DiagnosticoRelacionado")
                    Dim Complicacion As String = r("Complicacion")
                    Dim FormaActoQuirurgico As String = r("FormaActoQuirurgico")
                    Dim ValorProcedimiento As String = r("ValorProcedimiento")

                    Dim lineHead As String = ""

                    If dt.Rows.IndexOf(r) > 0 Then
                        lineHead = vbCrLf
                    End If

                    lineHead &= Utils.StringPad(NumeroFactura, MaxSize(NumeroFactura, 20), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(CodigoEntidadAdministradora, MaxSize(CodigoEntidadAdministradora, 10), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(TipoIdenficacion, MaxSize(TipoIdenficacion, 2), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(NumeroIdentificacion, MaxSize(NumeroIdentificacion, 20), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(FechaProcedimiento, MaxSize(FechaProcedimiento, 10), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(NumeroAutorizacion, MaxSize(NumeroAutorizacion, 15), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(CodigoProcedimiento, MaxSize(CodigoProcedimiento, 8), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(AmbitoRealizacion, MaxSize(AmbitoRealizacion, 1), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(FinalidadProcedimiento, MaxSize(FinalidadProcedimiento, 1), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(PersonalAtiende, MaxSize(PersonalAtiende, 1), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(Diagnostico, MaxSize(Diagnostico, 4), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(DiagnosticoRelacionado, MaxSize(DiagnosticoRelacionado, 4), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(Complicacion, MaxSize(Complicacion, 4), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(FormaActoQuirurgico, MaxSize(FormaActoQuirurgico, 1), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(ValorProcedimiento, MaxSize(ValorProcedimiento, 15), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)

                    resultPlane.Append(lineHead)

                Next

                result.Message = "AP" + ActualMonth + ActualYear
                result.ObjectEmbbeded = resultPlane
                result.StateResult = True
            Else
                result.StateResult = False
            End If

        Catch ex As Exception
            cnx.Close()
            result.StateResult = False
        End Try

        Return result

    End Function

    ''' <summary>
    ''' Archivo RIP AT (Archivo de Otros Servicios)
    ''' </summary>
    ''' <param name="IdRadicateInvoice">Id Radicación</param>
    ''' <param name="IndigoSessionValues">Variables de Sesión</param>
    ''' <returns>ActionMessageResult(Of StringBuilder)</returns>
    ''' <remarks></remarks>
    Public Function ATFile(IdRadicateInvoice As Integer, ByVal IndigoSessionValues As SessionValues) As ActionMessageResult(Of StringBuilder)
        Dim resultPlane As New StringBuilder()
        Dim result As New ActionMessageResult(Of StringBuilder)
        Dim ContainerGenesis = IndigoSessionValues.TransactionalContainer
        Dim HISContainer = IndigoSessionValues.HisContainer

        Dim ActualMonth = Month(Date.Now).ToString()
        Dim ActualYear = Year(Date.Now).ToString()

        Dim cnx As New System.Data.SqlClient.SqlConnection(Infrastructure.CrossCutting.Base.Utils.GetEntityConnectionString(Infrastructure.CrossCutting.Base.ConfigurationFile.CONX_GENESIS, String.Empty, ContainerGenesis, False))
        Try

            cnx.Open()

            Dim command As New System.Data.SqlClient.SqlCommand("", cnx)

            command.CommandTimeout = 30000

            command.CommandType = CommandType.Text

            command.CommandText = "SELECT I.InvoiceNumber as NumeroFactura, HA.HealthEntityCode AS CodigoEntidadAdministradora, CASE IPTIPODOC WHEN 1 THEN 'CC' WHEN 2 THEN 'CE' WHEN 5 THEN 'PA' WHEN 4 THEN 'RC' WHEN 3 THEN 'TI' WHEN 6 THEN 'AS' WHEN 7 THEN 'MS' WHEN 8 THEN 'NU' END as TipoIdenficacion, " +
                "LTRIM(RTRIM(INP.IPCODPACI)) as NumeroIdentificacion, ISNULL(SOD.AuthorizationNumber,'') as NumeroAutorizacion, CASE IPS.SubattentionCode WHEN 19 THEN 1 WHEN 20 THEN 1 WHEN 33 THEN 2 WHEN 2 THEN 3 WHEN 3 THEN 3 WHEN 4 THEN 3 WHEN 5 THEN 3 WHEN 6 THEN 3 WHEN 7 THEN 3 WHEN 12 THEN 4 WHEN 13 THEN 4 WHEN 14 THEN 4 WHEN 15 THEN 4 END AS TipoServicio, " +
                "CUPS.RIPSCode as CodigoServicio, IPS.Name as NombreServicio, SOD.InvoicedQuantity as Cantidad, SOD.TotalSalesPrice as ValorUnitario, SOD.GrandTotalSalesPrice AS ValorProcedimiento " +
                "FROM " & HISContainer & ".[dbo].[INPACIENT] INP, " & ContainerGenesis & ".Billing.Invoice I, " & ContainerGenesis & ".Billing.InvoiceDetail ID, " & ContainerGenesis & ".Billing.RevenueControlDetail RCD, " & ContainerGenesis & ".[Contract].HealthAdministrator HA, " & ContainerGenesis & ".Billing.ServiceOrderDetail SOD, " & ContainerGenesis & ".[Contract].IPSService IPS, " & ContainerGenesis & ".[Contract].CUPSEntity CUPS, " & ContainerGenesis & ".Portfolio.RadicateInvoiceC RC, " +
                "" & ContainerGenesis & ".Portfolio.RadicateInvoiceD RD WHERE INP.IPCODPACI = I.PatientCode AND I.RevenueControlDetailId = RCD.Id AND RCD.HealthAdministratorId = HA.Id AND ID.InvoiceId = I.Id AND ID.ServiceOrderDetailId = SOD.Id AND SOD.IPSServiceId = IPS.Id AND CUPS.Id = SOD.CUPSEntityId AND RC.Id = RD.RadicateInvoiceCId AND RD.InvoiceNumber = I.InvoiceNumber " +
                "AND IPS.SubattentionCode in (2,3,4,5,6,7,12,13,14,15,19,20,33) AND RC.Id = '" & IdRadicateInvoice & "'"

            Dim da As New System.Data.SqlClient.SqlDataAdapter(command)

            cnx.Close()

            Dim dt As New Data.DataTable()

            da.Fill(dt)

            If dt.Rows.Count > 0 Then

                CountAPFile = dt.Rows.Count()

                For Each r As DataRow In dt.Rows
                    Dim NumeroFactura As String = r("NumeroFactura")
                    Dim CodigoEntidadAdministradora As String = r("CodigoEntidadAdministradora")
                    Dim TipoIdenficacion As String = r("TipoIdenficacion")
                    Dim NumeroIdentificacion As String = r("NumeroIdentificacion")
                    Dim NumeroAutorizacion As String = r("NumeroAutorizacion")
                    Dim TipoServicio As String = r("TipoServicio")
                    Dim CodigoServicio As String = r("CodigoServicio")
                    Dim NombreServicio As String = r("NombreServicio")
                    Dim Cantidad As String = r("Cantidad")
                    Dim ValorUnitario As String = r("ValorUnitario")
                    Dim ValorProcedimiento As String = r("ValorProcedimiento")

                    Dim lineHead As String = ""

                    If dt.Rows.IndexOf(r) > 0 Then
                        lineHead = vbCrLf
                    End If

                    lineHead &= Utils.StringPad(NumeroFactura, MaxSize(NumeroFactura, 20), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(CodigoEntidadAdministradora, MaxSize(CodigoEntidadAdministradora, 10), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(TipoIdenficacion, MaxSize(TipoIdenficacion, 2), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(NumeroIdentificacion, MaxSize(NumeroIdentificacion, 20), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(NumeroAutorizacion, MaxSize(NumeroAutorizacion, 10), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(TipoServicio, MaxSize(TipoServicio, 1), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(CodigoServicio, MaxSize(CodigoServicio, 20), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(NombreServicio, MaxSize(NombreServicio, 60), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(Cantidad, MaxSize(Cantidad, 5), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(ValorUnitario, MaxSize(ValorUnitario, 15), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(ValorProcedimiento, MaxSize(ValorProcedimiento, 15), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                   

                    resultPlane.Append(lineHead)

                Next

                result.Message = "AT" + ActualMonth + ActualYear
                result.ObjectEmbbeded = resultPlane
                result.StateResult = True
            Else
                result.StateResult = False
            End If

        Catch ex As Exception
            cnx.Close()
            result.StateResult = False
        End Try

        Return result

    End Function

    ''' <summary>
    ''' Archivo RIP AN (Archivo de Recién Nacidos)
    ''' </summary>
    ''' <param name="IdRadicateInvoice">Id Radicación</param>
    ''' <param name="IndigoSessionValues">Variables de Sesión</param>
    ''' <returns>ActionMessageResult(Of StringBuilder)</returns>
    ''' <remarks></remarks>
    Public Function ANFile(IdRadicateInvoice As Integer, ByVal IndigoSessionValues As SessionValues) As ActionMessageResult(Of StringBuilder)
        Dim resultPlane As New StringBuilder()
        Dim result As New ActionMessageResult(Of StringBuilder)
        Dim ContainerGenesis = IndigoSessionValues.TransactionalContainer
        Dim HISContainer = IndigoSessionValues.HisContainer

        Dim ActualMonth = Month(Date.Now).ToString()
        Dim ActualYear = Year(Date.Now).ToString()

        Dim cnx As New System.Data.SqlClient.SqlConnection(Infrastructure.CrossCutting.Base.Utils.GetEntityConnectionString(Infrastructure.CrossCutting.Base.ConfigurationFile.CONX_GENESIS, String.Empty, ContainerGenesis, False))
        Try

            cnx.Open()

            Dim command As New System.Data.SqlClient.SqlCommand("", cnx)

            command.CommandTimeout = 30000

            command.CommandType = CommandType.Text

            command.CommandText = "SELECT I.InvoiceNumber as NumeroFactura, HA.HealthEntityCode AS CodigoEntidadAdministradora, CASE IPTIPODOC WHEN 1 THEN 'CC' WHEN 2 THEN 'CE' WHEN 5 THEN 'PA' WHEN 4 THEN 'RC' WHEN 3 THEN 'TI' WHEN 6 THEN 'AS' WHEN 7 THEN 'MS' WHEN 8 THEN 'NU' END as TipoIdenficacion, " +
                "LTRIM(RTRIM(INP.IPCODPACI)) as NumeroIdentificacion, CONVERT(VARCHAR(10),RECINAC.FECHANACIM,103) as FechaNacimiento, CONVERT(VARCHAR(5),RECINAC.FECHANACIM,114) as HoraNacimiento, GINE.NOMSEMGES as EdadGestacional, 1 as ControlPrenatal, RECINAC.SEXRECNAC as Sexo, RECINAC.PESORECNA as Peso, " +
                "RECNADI.CODDIAGNO as CodigoDiagnostico, CASE VITANACIM WHEN 'MUERTO' THEN RECNADI.CODDIAGNO ELSE '' END as CausaMuerte, CASE VITANACIM WHEN 'MUERTO' THEN CONVERT(VARCHAR(10),RECINAC.FECHANACIM,103) ELSE '' END as FechaMuerte, CASE VITANACIM WHEN 'MUERTO' THEN CONVERT(VARCHAR(5),RECINAC.FECHANACIM,114) ELSE '' END as HoraMuerte " +
                "FROM " & HISContainer & ".[dbo].[INPACIENT] INP, " & ContainerGenesis & ".Billing.Invoice I, " & ContainerGenesis & ".Billing.RevenueControlDetail RCD, " & ContainerGenesis & ".[Contract].HealthAdministrator HA, " & ContainerGenesis & ".Portfolio.RadicateInvoiceC RC, " & ContainerGenesis & ".Portfolio.RadicateInvoiceD RD," & HISContainer & ".dbo.HCANTGINE GINE," & HISContainer & ".dbo.HCRECINAC RECINAC, " +
                "" & HISContainer & ".dbo.HCRECNADI RECNADI WHERE INP.IPCODPACI = I.PatientCode AND I.RevenueControlDetailId = RCD.Id AND RCD.HealthAdministratorId = HA.Id AND RC.Id = RD.RadicateInvoiceCId AND RD.InvoiceNumber = I.InvoiceNumber AND I.PatientCode = GINE.IPCODPACI AND RECINAC.NUMINGRES = GINE.NUMINGRES " +
                "AND RECINAC.IPCODPACI = I.PatientCode AND RECNADI.CONSECREC = RECINAC.NUMCONSEC AND RC.Id = '" & IdRadicateInvoice & "'"

            Dim da As New System.Data.SqlClient.SqlDataAdapter(command)

            cnx.Close()

            Dim dt As New Data.DataTable()

            da.Fill(dt)

            If dt.Rows.Count > 0 Then

                CountAPFile = dt.Rows.Count()

                For Each r As DataRow In dt.Rows
                    Dim NumeroFactura As String = r("NumeroFactura")
                    Dim CodigoEntidadAdministradora As String = r("CodigoEntidadAdministradora")
                    Dim TipoIdenficacion As String = r("TipoIdenficacion")
                    Dim NumeroIdentificacion As String = r("NumeroIdentificacion")
                    Dim FechaNacimiento As String = r("FechaNacimiento")
                    Dim HoraNacimiento As String = r("HoraNacimiento")
                    Dim EdadGestacional As String = r("EdadGestacional")
                    Dim ControlPrenatal As String = r("ControlPrenatal")
                    Dim Sexo As String = r("Sexo")
                    Dim Peso As String = r("Peso")
                    Dim CodigoDiagnostico As String = r("CodigoDiagnostico")
                    Dim CausaMuerte As String = r("CausaMuerte")
                    Dim FechaMuerte As String = r("FechaMuerte")
                    Dim HoraMuerte As String = r("HoraMuerte")


                    Dim lineHead As String = ""

                    If dt.Rows.IndexOf(r) > 0 Then
                        lineHead = vbCrLf
                    End If

                    lineHead &= Utils.StringPad(NumeroFactura, MaxSize(NumeroFactura, 20), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(CodigoEntidadAdministradora, MaxSize(CodigoEntidadAdministradora, 10), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(TipoIdenficacion, MaxSize(TipoIdenficacion, 2), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(NumeroIdentificacion, MaxSize(NumeroIdentificacion, 20), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(FechaNacimiento, MaxSize(FechaNacimiento, 10), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(HoraNacimiento, MaxSize(HoraNacimiento, 5), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(EdadGestacional, MaxSize(EdadGestacional, 2), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(ControlPrenatal, MaxSize(ControlPrenatal, 1), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(Sexo, MaxSize(Sexo, 1), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(Peso, MaxSize(Peso, 4), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(CodigoDiagnostico, MaxSize(CodigoDiagnostico, 4), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(CausaMuerte, MaxSize(CausaMuerte, 4), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(FechaMuerte, MaxSize(FechaMuerte, 10), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(HoraMuerte, MaxSize(HoraMuerte, 5), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)

                    resultPlane.Append(lineHead)
                Next

                result.Message = "AN" + ActualMonth + ActualYear
                result.ObjectEmbbeded = resultPlane
                result.StateResult = True
            Else
                result.StateResult = False
            End If

        Catch ex As Exception
            cnx.Close()
            result.StateResult = False
        End Try

        Return result
    End Function

    Public Function AUFile(IdRadicateInvoice As Integer, ByVal IndigoSessionValues As SessionValues) As ActionMessageResult(Of StringBuilder)
        Dim resultPlane As New StringBuilder()
        Dim result As New ActionMessageResult(Of StringBuilder)
        Dim ContainerGenesis = IndigoSessionValues.TransactionalContainer
        Dim HISContainer = IndigoSessionValues.HisContainer

        Dim ActualMonth = Month(Date.Now).ToString()
        Dim ActualYear = Year(Date.Now).ToString()

        Dim cnx As New System.Data.SqlClient.SqlConnection(Infrastructure.CrossCutting.Base.Utils.GetEntityConnectionString(Infrastructure.CrossCutting.Base.ConfigurationFile.CONX_GENESIS, String.Empty, ContainerGenesis, False))
        Try

            cnx.Open()

            Dim command As New System.Data.SqlClient.SqlCommand("", cnx)

            command.CommandTimeout = 30000

            command.CommandType = CommandType.Text

            command.CommandText = "SELECT DISTINCT I.InvoiceNumber as NumeroFactura, HA.HealthEntityCode AS CodigoEntidadAdministradora, CASE IPTIPODOC WHEN 1 THEN 'CC' WHEN 2 THEN 'CE' WHEN 5 THEN 'PA' WHEN 4 THEN 'RC' WHEN 3 THEN 'TI' WHEN 6 THEN 'AS' WHEN 7 THEN 'MS' WHEN 8 THEN 'NU' END as TipoIdenficacion, " +
                "INP.IPCODPACI as NumeroIdentificacion, CONVERT(VARCHAR(10),URG.IPFECLLEGA ,103) as FechaIngreso, CONVERT(VARCHAR(5),URG.IPFECLLEGA,114) as HoraIngreso, LTRIM(RTRIM(TRI.NUMINGRES)) as NumeroAutorizacion, CASE ITIPORIES WHEN 10 THEN 01 WHEN 6 THEN 02 WHEN 7 THEN 06 WHEN 3 THEN 13 WHEN 4 THEN 13 WHEN 2 THEN 14 ELSE 15 END as CausaExterna, " +
                "Ingreso.CODDIAEGR as Diagnostico, CASE HIS.INDAUDFOR WHEN 12 THEN '1' WHEN 10 THEN '2' WHEN 3 THEN '3' WHEN 4 THEN '3' WHEN 5 THEN '3' WHEN 6 THEN '3' ELSE '' END as DestinoUsuario, CASE Egreso.ESTPACEGR WHEN 3 THEN 2 ELSE 1 END as EstadoSalida, ISNULL(Egreso.CODCAUMUE,'') as CausaBasicaMuerte, CONVERT(VARCHAR(10),Egreso.FECALTPAC,103) as FechaSalida, " +
                "CONVERT(VARCHAR(5),Egreso.FECALTPAC,114) as HoraSalida FROM " & HISContainer & ".[dbo].[INPACIENT] INP, " & ContainerGenesis & ".Billing.Invoice I, " & ContainerGenesis & ".Billing.RevenueControlDetail RCD, " & ContainerGenesis & ".[Contract].HealthAdministrator HA, " & ContainerGenesis & ".Portfolio.RadicateInvoiceC RC, " & ContainerGenesis & ".Portfolio.RadicateInvoiceD RCDD, " & HISContainer & ".dbo.ADCONTURG URG, " & HISContainer & ".dbo.ADTRIAGEU TRI, " +
                "" & HISContainer & ".dbo.HCHISPACA HIS, " & HISContainer & ".dbo.ADINGRESO Ingreso, " & HISContainer & ".dbo.HCREGEGRE Egreso WHERE INP.IPCODPACI = I.PatientCode AND I.RevenueControlDetailId = RCD.Id AND RCD.HealthAdministratorId = HA.Id AND TRI.NUMINGRES = ingreso.NUMINGRES AND RC.Id = RCDD.RadicateInvoiceCId AND I.InvoiceNumber = RCDD.InvoiceNumber AND ltrim(RTRIM(URG.IPCODPACI)) = i.PatientCode " +
                "AND TRI.CODCONCEC = URG.CODCONCEC AND HIS.NUMINGRES = TRI.NUMINGRES and ltrim(rtrim(Ingreso.IPCODPACI )) = i.PatientCode and Ingreso.NUMINGRES = TRI.NUMINGRES and Egreso.IPCODPACI = Ingreso.IPCODPACI and Egreso.NUMINGRES = Ingreso.NUMINGRES AND RC.Id = '" & IdRadicateInvoice & "'"

            Dim da As New System.Data.SqlClient.SqlDataAdapter(command)

            cnx.Close()

            Dim dt As New Data.DataTable()

            da.Fill(dt)

            If dt.Rows.Count > 0 Then

                CountAUFile = dt.Rows.Count()

                For Each r As DataRow In dt.Rows
                    Dim NumeroFactura As String = r("NumeroFactura")
                    Dim CodigoEntidadAdministradora As String = r("CodigoEntidadAdministradora")
                    Dim TipoIdenficacion As String = r("TipoIdenficacion")
                    Dim NumeroIdentificacion As String = r("NumeroIdentificacion")
                    Dim FechaIngreso As String = r("FechaIngreso")
                    Dim HoraIngreso As String = r("HoraIngreso")
                    Dim NumeroAutorizacion As String = r("NumeroAutorizacion")
                    Dim CausaExterna As String = r("CausaExterna")
                    Dim Diagnostico As String = r("Diagnostico")
                    Dim DestinoUsuario As String = r("DestinoUsuario")
                    Dim EstadoSalida As String = r("EstadoSalida")
                    Dim CausaBasicaMuerte As String = r("CausaBasicaMuerte")
                    Dim FechaSalida As String = r("FechaSalida")
                    Dim HoraSalida As String = r("HoraSalida")

                    Dim lineHead As String = ""

                    If dt.Rows.IndexOf(r) > 0 Then
                        lineHead = vbCrLf
                    End If

                    lineHead &= Utils.StringPad(NumeroFactura, MaxSize(NumeroFactura, 20), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(CodigoEntidadAdministradora, MaxSize(CodigoEntidadAdministradora, 10), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(TipoIdenficacion, MaxSize(TipoIdenficacion, 2), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(NumeroIdentificacion, MaxSize(NumeroIdentificacion, 20), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(FechaIngreso, MaxSize(FechaIngreso, 10), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(HoraIngreso, MaxSize(HoraIngreso, 5), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(NumeroAutorizacion, MaxSize(NumeroAutorizacion, 15), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(CausaExterna, MaxSize(CausaExterna, 2), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(Diagnostico, MaxSize(Diagnostico, 4), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(Diagnostico, MaxSize(Diagnostico, 4), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(Diagnostico, MaxSize(Diagnostico, 4), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(Diagnostico, MaxSize(Diagnostico, 4), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(DestinoUsuario, MaxSize(DestinoUsuario, 1), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(EstadoSalida, MaxSize(EstadoSalida, 1), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(CausaBasicaMuerte, MaxSize(CausaBasicaMuerte, 4), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(FechaSalida, MaxSize(FechaSalida, 10), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(HoraSalida, MaxSize(HoraSalida, 5), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)

                    resultPlane.Append(lineHead)
                Next

                result.Message = "AU" + ActualMonth + ActualYear
                result.ObjectEmbbeded = resultPlane
                result.StateResult = True
            Else
                result.StateResult = False
            End If

        Catch ex As Exception
            cnx.Close()
            result.StateResult = False
        End Try

        Return result
    End Function

    Public Function AHFile(IdRadicateInvoice As Integer, ByVal IndigoSessionValues As SessionValues) As ActionMessageResult(Of StringBuilder)
        Dim resultPlane As New StringBuilder()
        Dim result As New ActionMessageResult(Of StringBuilder)
        Dim ContainerGenesis = IndigoSessionValues.TransactionalContainer
        Dim HISContainer = IndigoSessionValues.HisContainer

        Dim ActualMonth = Month(Date.Now).ToString()
        Dim ActualYear = Year(Date.Now).ToString()

        Dim cnx As New System.Data.SqlClient.SqlConnection(Infrastructure.CrossCutting.Base.Utils.GetEntityConnectionString(Infrastructure.CrossCutting.Base.ConfigurationFile.CONX_GENESIS, String.Empty, ContainerGenesis, False))
        Try

            cnx.Open()

            Dim command As New System.Data.SqlClient.SqlCommand("", cnx)

            command.CommandTimeout = 30000

            command.CommandType = CommandType.Text

            command.CommandText = "SELECT DISTINCT I.InvoiceNumber as NumeroFactura, HA.HealthEntityCode AS CodigoEntidadAdministradora, CASE IPTIPODOC WHEN 1 THEN 'CC' WHEN 2 THEN 'CE' WHEN 5 THEN 'PA' WHEN 4 THEN 'RC' WHEN 3 THEN 'TI' WHEN 6 THEN 'AS' WHEN 7 THEN 'MS' WHEN 8 THEN 'NU' END as TipoIdenficacion, " +
                "INP.IPCODPACI as NumeroIdentificacion, Ingreso.IINGREPOR as ViaIngreso, CONVERT(VARCHAR(10),Ingreso.IFECHAING,103) as FechaIngreso, CONVERT(VARCHAR(5),Ingreso.IFECHAING,114) as HoraIngreso, HIS.NUMINGRES as NumeroAutorizacion, CASE ITIPORIES WHEN 10 THEN 01 WHEN 6 THEN 02 WHEN 7 THEN 06 WHEN 3 THEN 13 WHEN 4 THEN 13 " +
                "WHEN 2 THEN 14 ELSE 15 END as CausaExterna, Ingreso.CODDIAEGR as DiagnosticoIngreso, HIS.CODDIAGNO as DiagnosticoEgreso, '' as DiagnosticoComplicacion, CASE Egreso.ESTPACEGR WHEN 3 THEN 2 ELSE 1 END as EstadoSalida, ISNULL(Egreso.CODCAUMUE,'') as CausaBasicaMuerte, CONVERT(VARCHAR(10),Egreso.FECALTPAC,103) as FechaSalida, " +
                "CONVERT(VARCHAR(5),Egreso.FECALTPAC,114) as HoraSalida FROM " & HISContainer & ".[dbo].[INPACIENT] INP, " & ContainerGenesis & ".Billing.Invoice I, " & ContainerGenesis & ".Billing.RevenueControlDetail RCD, " & ContainerGenesis & ".[Contract].HealthAdministrator HA, " & ContainerGenesis & ".Portfolio.RadicateInvoiceC RC, " & ContainerGenesis & ".Portfolio.RadicateInvoiceD RCDD, " & HISContainer & ".dbo.HCHISPACA HIS, " +
                " " & HISContainer & ".dbo.ADINGRESO Ingreso,  " & HISContainer & ".dbo.HCREGEGRE Egreso WHERE INP.IPCODPACI = I.PatientCode AND I.RevenueControlDetailId = RCD.Id AND RCD.HealthAdministratorId = HA.Id AND RC.Id = RCDD.RadicateInvoiceCId AND I.InvoiceNumber = RCDD.InvoiceNumber and ltrim(rtrim(Ingreso.IPCODPACI )) = i.PatientCode " +
                "and Egreso.IPCODPACI = Ingreso.IPCODPACI and Egreso.NUMINGRES = Ingreso.NUMINGRES and LTRIM(rtrim(Egreso.IPCODPACI)) = I.PatientCode and HIS.IPCODPACI = Ingreso.IPCODPACI and HIS.NUMINGRES = Ingreso.NUMINGRES AND RC.Id = '" & IdRadicateInvoice & "'"

            Dim da As New System.Data.SqlClient.SqlDataAdapter(command)

            cnx.Close()

            Dim dt As New Data.DataTable()

            da.Fill(dt)

            If dt.Rows.Count > 0 Then

                CountAHFile = dt.Rows.Count()

                For Each r As DataRow In dt.Rows
                    Dim NumeroFactura As String = r("NumeroFactura")
                    Dim CodigoEntidadAdministradora As String = r("CodigoEntidadAdministradora")
                    Dim TipoIdenficacion As String = r("TipoIdenficacion")
                    Dim NumeroIdentificacion As String = r("NumeroIdentificacion")
                    Dim ViaIngreso As String = r("ViaIngreso")
                    Dim FechaIngreso As String = r("FechaIngreso")
                    Dim HoraIngreso As String = r("HoraIngreso")
                    Dim NumeroAutorizacion As String = r("NumeroAutorizacion")
                    Dim CausaExterna As String = r("CausaExterna")
                    Dim DiagnosticoIngreso As String = r("DiagnosticoIngreso")
                    Dim DiagnosticoEgreso As String = r("DiagnosticoEgreso")
                    Dim DiagnosticoComplicacion As String = r("DiagnosticoComplicacion")
                    Dim EstadoSalida As String = r("EstadoSalida")
                    Dim CausaBasicaMuerte As String = r("CausaBasicaMuerte")
                    Dim FechaSalida As String = r("FechaSalida")
                    Dim HoraSalida As String = r("HoraSalida")

                    

                    Dim lineHead As String = ""

                    If dt.Rows.IndexOf(r) > 0 Then
                        lineHead = vbCrLf
                    End If

                    lineHead &= Utils.StringPad(NumeroFactura, MaxSize(NumeroFactura, 20), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(CodigoEntidadAdministradora, MaxSize(CodigoEntidadAdministradora, 10), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(TipoIdenficacion, MaxSize(TipoIdenficacion, 2), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(NumeroIdentificacion, MaxSize(NumeroIdentificacion, 20), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(ViaIngreso, MaxSize(ViaIngreso, 1), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(FechaIngreso, MaxSize(FechaIngreso, 10), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(HoraIngreso, MaxSize(HoraIngreso, 5), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(NumeroAutorizacion, MaxSize(NumeroAutorizacion, 15), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(CausaExterna, MaxSize(CausaExterna, 2), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(DiagnosticoIngreso, MaxSize(DiagnosticoIngreso, 4), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(DiagnosticoEgreso, MaxSize(DiagnosticoEgreso, 4), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(DiagnosticoEgreso, MaxSize(DiagnosticoEgreso, 4), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(DiagnosticoEgreso, MaxSize(DiagnosticoEgreso, 4), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(DiagnosticoEgreso, MaxSize(DiagnosticoEgreso, 4), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(DiagnosticoComplicacion, MaxSize(DiagnosticoComplicacion, 4), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(EstadoSalida, MaxSize(EstadoSalida, 1), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(CausaBasicaMuerte, MaxSize(CausaBasicaMuerte, 4), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(FechaSalida, MaxSize(FechaSalida, 10), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)
                    lineHead &= Utils.StringPad(HoraSalida, MaxSize(HoraSalida, 5), " ", Utils.PadType.STR_PAD_RIGHT)
                    lineHead &= Utils.StringPad(",", 1, 0, Utils.PadType.STR_PAD_LEFT)

                    resultPlane.Append(lineHead)
                Next

                result.Message = "AH" + ActualMonth + ActualYear
                result.ObjectEmbbeded = resultPlane
                result.StateResult = True
            Else
                result.StateResult = False
            End If

        Catch ex As Exception
            cnx.Close()
            result.StateResult = False
        End Try

        Return result

    End Function


End Class
