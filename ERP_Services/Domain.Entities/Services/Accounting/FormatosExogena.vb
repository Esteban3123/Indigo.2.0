#Region "Imports"

Imports Domain.Entities
Imports Domain.Base.Entities
Imports System.Text
Imports Infrastructure.CrossCutting.Base
Imports System.IO

#End Region

Public Class FormatosExogena
    Implements IFormatosExogena

#Region "Builder"

    Private _AccountingBalanceRepository As IAccountingBalanceRepository

    Public Sub New(radicateInvoiceDRepository As IAccountingBalanceRepository)
        _AccountingBalanceRepository = radicateInvoiceDRepository
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Genera el Formato Exogena 1001
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <returns></returns>
    Public Function GenerarExogenaXMLFormat1001(criterias As Dictionary(Of String, String)) As String Implements IFormatosExogena.GenerarExogenaXMLFormat1001
        Dim xmlCriterias = Utils.DictionaryToXML(criterias)
        Dim Data As List(Of SP_ExogenaFormat1001_Result) = _AccountingBalanceRepository.ExogenaFormat1001(xmlCriterias)

        Dim value As Decimal = Data.Sum(Function(x) x.PaymentDeductible)

        Dim builder As StringBuilder = New StringBuilder()
        builder.AppendLine("<?xml version=""1.0"" encoding=""ISO-8859-1""?>")
        builder.AppendLine("<mas xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xsi:noNamespaceSchemaLocation=""../xsd/1001.xsd"">")
        builder.AppendLine("<Cab>")
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "Ano", criterias("Year")))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "CodCpt", criterias("Concept")))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "Formato", criterias("Format")))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "Version", criterias("Version")))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "NumEnvio", criterias("SendingNumber")))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "FecEnvio", Date.Now.ToString("yyyy-MM-ddTHH:mm:ss").Trim))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "FecInicial", DateSerial(criterias("Year"), 1, 1).ToString("yyyy-MM-dd").Trim))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "FecFinal", DateSerial(criterias("Year"), 12, 31).ToString("yyyy-MM-dd").Trim))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "ValorTotal", CDec(value).ToString))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "CantReg", Data.Count()))
        builder.AppendLine("</Cab>")

        For Each a As SP_ExogenaFormat1001_Result In Data
            builder.Append("<pagos")
            builder.Append(" cpt=""" & a.Concept & """")
            builder.Append(" tdoc=""" & a.IdentificationType & """")
            builder.Append(" nid=""" & a.IdentificationNumber & """")

            If a.IdentificationType = 31 Then
                builder.Append(" dv=""" & a.DigitVerification & """")
                builder.Append(" raz=""" & a.BusinessName & """")
            Else
                builder.Append(" apl1=""" & a.FirstLastName & """")
                If Not String.IsNullOrEmpty(a.SecondLastName) Then
                    builder.Append(" apl2=""" & a.SecondLastName & """")
                End If
                builder.Append(" nom1=""" & a.FirstName & """")
                If Not String.IsNullOrEmpty(a.SecondLastName) Then
                    builder.Append(" nom2=""" & a.SecondName & """")
                End If
            End If

            If Not String.IsNullOrEmpty(a.Addresss) Then
                builder.Append(" dir=""" & a.Addresss & """")
                builder.Append(" dpto=""" & a.DepartmentCode & """")
                builder.Append(" mun=""" & a.CityCode.Replace(a.DepartmentCode, "") & """")
                builder.Append(" pais=""" & a.CodeCountry & """")
            End If

            builder.Append(" pago=""" & a.PaymentDeductible & """")
            builder.Append(" pnded=""" & a.PaymentNoDeductible & """")
            builder.Append(" ided=""" & a.IVADeductible & """")
            builder.Append(" inded=""" & a.IVANoDeductible & """")
            builder.Append(" retp=""" & a.ReteDeductible & """")
            builder.Append(" reta=""" & a.ReteNoDeductible & """")
            builder.Append(" comun=""" & a.ReteIVAComun & """")
            builder.Append(" ndom=""" & a.ReteIVAExt & """")
            builder.AppendLine(" />")
        Next
        builder.AppendLine("</mas>")
        Return builder.ToString()
    End Function

    ''' <summary>
    ''' Genera Formato 1003 de Exogena
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <returns></returns>
    Public Function GenerarExogenaXMLFormat1003(criterias As Dictionary(Of String, String)) As String Implements IFormatosExogena.GenerarExogenaXMLFormat1003
        Dim xmlCriterias = Utils.DictionaryToXML(criterias)
        Dim Data As List(Of SP_ExogenaFormat1003_Result) = _AccountingBalanceRepository.ExogenaFormat1003(xmlCriterias)

        Dim value As Decimal = Data.Sum(Function(x) x.RetentionValue)

        Dim builder As StringBuilder = New StringBuilder()
        builder.AppendLine("<?xml version=""1.0"" encoding=""ISO-8859-1""?>")
        builder.AppendLine("<mas xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xsi:noNamespaceSchemaLocation=""../xsd/1003.xsd"">")
        builder.AppendLine("<Cab>")
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "Ano", criterias("Year")))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "CodCpt", criterias("Concept")))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "Formato", criterias("Format")))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "Version", criterias("Version")))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "NumEnvio", criterias("SendingNumber")))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "FecEnvio", Date.Now.ToString("yyyy-MM-ddTHH:mm:ss").Trim))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "FecInicial", DateSerial(criterias("Year"), 1, 1).ToString("yyyy-MM-dd").Trim))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "FecFinal", DateSerial(criterias("Year"), 12, 31).ToString("yyyy-MM-dd").Trim))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "ValorTotal", CDec(value).ToString))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "CantReg", Data.Count()))
        builder.AppendLine("</Cab>")

        For Each a As SP_ExogenaFormat1003_Result In Data
            builder.Append("<rets")
            builder.Append(" cpt=""" & a.Concept & """")
            builder.Append(" tdoc=""" & a.IdentificationType & """")
            builder.Append(" nid=""" & a.IdentificationNumber & """")

            If a.IdentificationType = 31 Then
                builder.Append(" dv=""" & a.DigitVerification & """")
                builder.Append(" raz=""" & a.BusinessName & """")
            Else
                builder.Append(" apl1=""" & a.FirstLastName & """")
                If Not String.IsNullOrEmpty(a.SecondLastName) Then
                    builder.Append(" apl2=""" & a.SecondLastName & """")
                End If
                builder.Append(" nom1=""" & a.FirstName & """")
                If Not String.IsNullOrEmpty(a.SecondLastName) Then
                    builder.Append(" nom2=""" & a.SecondName & """")
                End If
            End If

            If Not String.IsNullOrEmpty(a.Addresss) Then
                builder.Append(" dir=""" & a.Addresss & """")
                builder.Append(" dpto=""" & a.DepartmentCode & """")
                builder.Append(" mun=""" & a.CityCode.Replace(a.DepartmentCode, "") & """")
            End If

            builder.Append(" valor=""" & a.BaseValue & """")
            builder.Append(" ret=""" & a.RetentionValue & """")

            builder.AppendLine(" />")
        Next
        builder.AppendLine("</mas>")
        Return builder.ToString()
    End Function

    ''' <summary>
    ''' Genera Formato 1004 de Exogena
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <returns></returns>
    Public Function GenerarExogenaXMLFormat1004(criterias As Dictionary(Of String, String)) As String Implements IFormatosExogena.GenerarExogenaXMLFormat1004
        Dim xmlCriterias = Utils.DictionaryToXML(criterias)
        Dim Data As List(Of SP_ExogenaFormat1004_Result) = _AccountingBalanceRepository.ExogenaFormat1004(xmlCriterias)

        Dim value As Decimal = Data.Sum(Function(x) x.DiscountValue)

        Dim builder As StringBuilder = New StringBuilder()
        builder.AppendLine("<?xml version=""1.0"" encoding=""ISO-8859-1""?>")
        builder.AppendLine("<mas xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xsi:noNamespaceSchemaLocation=""../xsd/1004.xsd"">")
        builder.AppendLine("<Cab>")
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "Ano", criterias("Year")))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "CodCpt", criterias("Concept")))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "Formato", criterias("Format")))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "Version", criterias("Version")))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "NumEnvio", criterias("SendingNumber")))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "FecEnvio", Date.Now.ToString("yyyy-MM-ddTHH:mm:ss").Trim))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "FecInicial", DateSerial(criterias("Year"), 1, 1).ToString("yyyy-MM-dd").Trim))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "FecFinal", DateSerial(criterias("Year"), 12, 31).ToString("yyyy-MM-dd").Trim))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "ValorTotal", CDec(value).ToString))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "CantReg", Data.Count()))
        builder.AppendLine("</Cab>")

        For Each a As SP_ExogenaFormat1004_Result In Data
            builder.Append("<descuentos")
            builder.Append(" cpt=""" & a.Concept & """")
            builder.Append(" tdoc=""" & a.IdentificationType & """")
            builder.Append(" nit=""" & a.IdentificationNumber & """")

            If a.IdentificationType = 31 Then
                builder.Append(" raz=""" & a.BusinessName & """")
            Else
                builder.Append(" pap=""" & a.FirstLastName & """")
                If Not String.IsNullOrEmpty(a.SecondLastName) Then
                    builder.Append(" sap=""" & a.SecondLastName & """")
                End If
                builder.Append(" pno=""" & a.FirstName & """")
                If Not String.IsNullOrEmpty(a.SecondLastName) Then
                    builder.Append(" ono=""" & a.SecondName & """")
                End If
            End If

            If Not String.IsNullOrEmpty(a.Addresss) Then
                builder.Append(" dir=""" & a.Addresss & """")
                builder.Append(" dpto=""" & a.DepartmentCode & """")
                builder.Append(" mun=""" & a.CityCode.Replace(a.DepartmentCode, "") & """")
                builder.Append(" pais=""" & a.CodeCountry & """")
            End If

            If Not String.IsNullOrEmpty(a.Email) Then
                builder.Append(" email=""" & a.Email & """")
            End If

            builder.Append(" vpag=""" & a.AmountPaid & """")
            builder.Append(" vdes=""" & a.DiscountValue & """")

            builder.AppendLine(" />")
        Next
        builder.AppendLine("</mas>")
        Return builder.ToString()
    End Function

    ''' <summary>
    ''' Genera el Formato Exogena 1005
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <returns></returns>
    Public Function GenerarExogenaXMLFormat1005(criterias As Dictionary(Of String, String)) As String Implements IFormatosExogena.GenerarExogenaXMLFormat1005
        Dim xmlCriterias = Utils.DictionaryToXML(criterias)
        Dim Data As List(Of SP_ExogenaFormat1005_Result) = _AccountingBalanceRepository.ExogenaFormat1005(xmlCriterias)

        Dim value As Decimal = Data.Sum(Function(x) x.DiscountableTax)

        Dim builder As StringBuilder = New StringBuilder()
        builder.AppendLine("<?xml version=""1.0"" encoding=""ISO-8859-1""?>")
        builder.AppendLine("<mas xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xsi:noNamespaceSchemaLocation=""../xsd/1005.xsd"">")
        builder.AppendLine("<Cab>")
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "Ano", criterias("Year")))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "CodCpt", criterias("Concept")))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "Formato", criterias("Format")))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "Version", criterias("Version")))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "NumEnvio", criterias("SendingNumber")))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "FecEnvio", Date.Now.ToString("yyyy-MM-ddTHH:mm:ss").Trim))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "FecInicial", DateSerial(criterias("Year"), 1, 1).ToString("yyyy-MM-dd").Trim))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "FecFinal", DateSerial(criterias("Year"), 12, 31).ToString("yyyy-MM-dd").Trim))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "ValorTotal", CDec(value).ToString))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "CantReg", Data.Count()))
        builder.AppendLine("</Cab>")

        For Each a As SP_ExogenaFormat1005_Result In Data
            builder.Append("<impventas")
            builder.Append(" tdoc=""" & a.IdentificationType & """")
            builder.Append(" nid=""" & a.IdentificationNumber & """")

            If a.IdentificationType = 31 Then
                builder.Append(" dv=""" & a.DigitVerification & """")
                builder.Append(" raz=""" & a.BusinessName & """")
            Else
                builder.Append(" apl1=""" & a.FirstLastName & """")
                If Not String.IsNullOrEmpty(a.SecondLastName) Then
                    builder.Append(" apl2=""" & a.SecondLastName & """")
                End If
                builder.Append(" nom1=""" & a.FirstName & """")
                If Not String.IsNullOrEmpty(a.SecondLastName) Then
                    builder.Append(" nom2=""" & a.SecondName & """")
                End If
            End If

            builder.Append(" vimp=""" & a.DiscountableTax & """")
            builder.Append(" ivade=""" & a.IvaValue & """")
            builder.AppendLine(" />")
        Next
        builder.AppendLine("</mas>")
        Return builder.ToString()
    End Function

    ''' <summary>
    ''' Genera el Formato Exogena 1006
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <returns></returns>
    Public Function GenerarExogenaXMLFormat1006(criterias As Dictionary(Of String, String)) As String Implements IFormatosExogena.GenerarExogenaXMLFormat1006
        Dim xmlCriterias = Utils.DictionaryToXML(criterias)
        Dim Data As List(Of SP_ExogenaFormat1006_Result) = _AccountingBalanceRepository.ExogenaFormat1006(xmlCriterias)

        Dim value As Decimal = Data.Sum(Function(x) x.IVA)

        Dim builder As StringBuilder = New StringBuilder()
        builder.AppendLine("<?xml version=""1.0"" encoding=""ISO-8859-1""?>")
        builder.AppendLine("<mas xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xsi:noNamespaceSchemaLocation=""../xsd/1006.xsd"">")
        builder.AppendLine("<Cab>")
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "Ano", criterias("Year")))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "CodCpt", criterias("Concept")))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "Formato", criterias("Format")))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "Version", criterias("Version")))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "NumEnvio", criterias("SendingNumber")))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "FecEnvio", Date.Now.ToString("yyyy-MM-ddTHH:mm:ss").Trim))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "FecInicial", DateSerial(criterias("Year"), 1, 1).ToString("yyyy-MM-dd").Trim))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "FecFinal", DateSerial(criterias("Year"), 12, 31).ToString("yyyy-MM-dd").Trim))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "ValorTotal", CDec(value).ToString))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "CantReg", Data.Count()))
        builder.AppendLine("</Cab>")

        For Each a As SP_ExogenaFormat1006_Result In Data
            builder.Append("<impoventas")
            builder.Append(" cpt=""" & a.Concept & """")
            builder.Append(" tdoc=""" & a.IdentificationType & """")
            builder.Append(" nid=""" & a.IdentificationNumber & """")

            If a.IdentificationType = 31 Then
                builder.Append(" dv=""" & a.DigitVerification & """")
                builder.Append(" raz=""" & a.BusinessName & """")
            Else
                builder.Append(" apl1=""" & a.FirstLastName & """")
                If Not String.IsNullOrEmpty(a.SecondLastName) Then
                    builder.Append(" apl2=""" & a.SecondLastName & """")
                End If
                builder.Append(" nom1=""" & a.FirstName & """")
                If Not String.IsNullOrEmpty(a.SecondLastName) Then
                    builder.Append(" nom2=""" & a.SecondName & """")
                End If
            End If

            builder.Append(" imp=""" & a.IVA & """")
            builder.Append(" iva=""" & a.IVARecovered & """")
            builder.Append(" icon=""" & a.ConsumptionTax & """")
            builder.AppendLine(" />")
        Next
        builder.AppendLine("</mas>")
        Return builder.ToString()
    End Function

    ''' <summary>
    ''' Genera Formato 1007 de Exogena
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <returns></returns>
    Public Function GenerarExogenaXMLFormat1007(criterias As Dictionary(Of String, String)) As String Implements IFormatosExogena.GenerarExogenaXMLFormat1007
        Dim xmlCriterias = Utils.DictionaryToXML(criterias)
        Dim Data As List(Of SP_ExogenaFormat1007_Result) = _AccountingBalanceRepository.ExogenaFormat1007(xmlCriterias)

        Dim value As Decimal = Data.Sum(Function(x) x.GrossIncome)

        Dim builder As StringBuilder = New StringBuilder()
        builder.AppendLine("<?xml version=""1.0"" encoding=""ISO-8859-1""?>")
        builder.AppendLine("<mas xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xsi:noNamespaceSchemaLocation=""../xsd/1007.xsd"">")
        builder.AppendLine("<Cab>")
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "Ano", criterias("Year")))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "CodCpt", criterias("Concept")))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "Formato", criterias("Format")))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "Version", criterias("Version")))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "NumEnvio", criterias("SendingNumber")))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "FecEnvio", Date.Now.ToString("yyyy-MM-ddTHH:mm:ss").Trim))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "FecInicial", DateSerial(criterias("Year"), 1, 1).ToString("yyyy-MM-dd").Trim))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "FecFinal", DateSerial(criterias("Year"), 12, 31).ToString("yyyy-MM-dd").Trim))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "ValorTotal", CDec(value).ToString))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "CantReg", Data.Count()))
        builder.AppendLine("</Cab>")

        For Each a As SP_ExogenaFormat1007_Result In Data
            builder.Append("<ingresos")
            builder.Append(" cpt=""" & a.Concept & """  ")
            builder.Append(" tdoc=""" & a.IdentificationType & """")
            builder.Append(" nid=""" & a.IdentificationNumber & """")

            If a.IdentificationType = 31 Then
                builder.Append(" dv=""" & a.DigitVerification & """")
                builder.Append(" raz=""" & a.BusinessName & """")
            Else
                builder.Append(" apl1=""" & a.FirstLastName & """")
                If Not String.IsNullOrEmpty(a.SecondLastName) Then
                    builder.Append(" apl2=""" & a.SecondLastName & """")
                End If
                builder.Append(" nom1=""" & a.FirstName & """")
                If Not String.IsNullOrEmpty(a.SecondLastName) Then
                    builder.Append(" nom2=""" & a.SecondName & """")
                End If
            End If

            If Not String.IsNullOrEmpty(a.CodeCountry) Then
                builder.Append(" pais=""" & a.CodeCountry & """")
            End If

            builder.Append(" ibru=""" & a.GrossIncome & """")
            builder.Append(" dred=""" & a.DeductedValue & """")
            builder.AppendLine(" />")
        Next
        builder.AppendLine("</mas>")
        Return builder.ToString()
    End Function

    ''' <summary>
    ''' Genera Formato 1008 de Exogena
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <returns></returns>
    Public Function GenerarExogenaXMLFormat1008(criterias As Dictionary(Of String, String)) As String Implements IFormatosExogena.GenerarExogenaXMLFormat1008
        Dim xmlCriterias = Utils.DictionaryToXML(criterias)
        Dim Data As List(Of SP_ExogenaFormat1008_Result) = _AccountingBalanceRepository.ExogenaFormat1008(xmlCriterias)

        Dim value As Decimal = Data.Sum(Function(x) x.Balance)

        Dim builder As StringBuilder = New StringBuilder()
        builder.AppendLine("<?xml version=""1.0"" encoding=""ISO-8859-1""?>")
        builder.AppendLine("<mas xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xsi:noNamespaceSchemaLocation=""../xsd/1008.xsd"">")
        builder.AppendLine("<Cab>")
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "Ano", criterias("Year")))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "CodCpt", criterias("Concept")))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "Formato", criterias("Format")))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "Version", criterias("Version")))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "NumEnvio", criterias("SendingNumber")))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "FecEnvio", Date.Now.ToString("yyyy-MM-ddTHH:mm:ss").Trim))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "FecInicial", DateSerial(criterias("Year"), 1, 1).ToString("yyyy-MM-dd").Trim))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "FecFinal", DateSerial(criterias("Year"), 12, 31).ToString("yyyy-MM-dd").Trim))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "ValorTotal", CDec(value).ToString))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "CantReg", Data.Count()))
        builder.AppendLine("</Cab>")

        For Each a As SP_ExogenaFormat1008_Result In Data
            builder.Append("<saldoscc")
            builder.Append(" cpt=""" & a.Concept & """")
            builder.Append(" tdoc=""" & a.IdentificationType & """")
            builder.Append(" nid=""" & a.IdentificationNumber & """")

            If a.IdentificationType = 31 Then
                builder.Append(" dv=""" & a.DigitVerification & """")
                builder.Append(" raz=""" & a.BusinessName & """")
            Else
                builder.Append(" apl1=""" & a.FirstLastName & """")
                If Not String.IsNullOrEmpty(a.SecondLastName) Then
                    builder.Append(" apl2=""" & a.SecondLastName & """")
                End If
                builder.Append(" nom1=""" & a.FirstName & """")
                If Not String.IsNullOrEmpty(a.SecondLastName) Then
                    builder.Append(" nom2=""" & a.SecondName & """")
                End If
            End If

            If Not String.IsNullOrEmpty(a.Addresss) Then
                builder.Append(" dir=""" & a.Addresss & """")
                builder.Append(" dpto=""" & a.DepartmentCode & """")
                builder.Append(" mun=""" & a.CityCode.Replace(a.DepartmentCode, "") & """")
                builder.Append(" pais=""" & a.CodeCountry & """")
            End If

            builder.Append(" sal=""" & a.Balance & """")

            builder.AppendLine(" />")
        Next
        builder.AppendLine("</mas>")
        Return builder.ToString()
    End Function

    ''' <summary>
    ''' Genera Formato 1009 de Exogena
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <returns></returns>
    Public Function GenerarExogenaXMLFormat1009(criterias As Dictionary(Of String, String)) As String Implements IFormatosExogena.GenerarExogenaXMLFormat1009
        Dim xmlCriterias = Utils.DictionaryToXML(criterias)
        Dim Data As List(Of SP_ExogenaFormat1009_Result) = _AccountingBalanceRepository.ExogenaFormat1009(xmlCriterias)

        Dim value As Decimal = Data.Sum(Function(x) x.Balance)

        Dim builder As StringBuilder = New StringBuilder()
        builder.AppendLine("<?xml version=""1.0"" encoding=""ISO-8859-1""?>")
        builder.AppendLine("<mas xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xsi:noNamespaceSchemaLocation=""../xsd/1009.xsd"">")
        builder.AppendLine("<Cab>")
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "Ano", criterias("Year")))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "CodCpt", criterias("Concept")))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "Formato", criterias("Format")))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "Version", criterias("Version")))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "NumEnvio", criterias("SendingNumber")))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "FecEnvio", Date.Now.ToString("yyyy-MM-ddTHH:mm:ss").Trim))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "FecInicial", DateSerial(criterias("Year"), 1, 1).ToString("yyyy-MM-dd").Trim))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "FecFinal", DateSerial(criterias("Year"), 12, 31).ToString("yyyy-MM-dd").Trim))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "ValorTotal", CDec(value).ToString))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "CantReg", Data.Count()))
        builder.AppendLine("</Cab>")

        For Each a As SP_ExogenaFormat1009_Result In Data
            builder.Append("<saldoscp")
            builder.Append(" cpt=""" & a.Concept & """")
            builder.Append(" tdoc=""" & a.IdentificationType & """")
            builder.Append(" nid=""" & a.IdentificationNumber & """")

            If a.IdentificationType = 31 Then
                builder.Append(" dv=""" & a.DigitVerification & """")
                builder.Append(" raz=""" & a.BusinessName & """")
            Else
                builder.Append(" apl1=""" & a.FirstLastName & """")
                If Not String.IsNullOrEmpty(a.SecondLastName) Then
                    builder.Append(" apl2=""" & a.SecondLastName & """")
                End If
                builder.Append(" nom1=""" & a.FirstName & """")
                If Not String.IsNullOrEmpty(a.SecondLastName) Then
                    builder.Append(" nom2=""" & a.SecondName & """")
                End If
            End If

            If Not String.IsNullOrEmpty(a.Addresss) Then
                builder.Append(" dir=""" & a.Addresss & """")
                builder.Append(" dpto=""" & a.DepartmentCode & """")
                builder.Append(" mun=""" & a.CityCode.Replace(a.DepartmentCode, "") & """")
                builder.Append(" pais=""" & a.CodeCountry & """")
            End If

            builder.Append(" sal=""" & a.Balance & """")

            builder.AppendLine(" />")
        Next
        builder.AppendLine("</mas>")
        Return builder.ToString()
    End Function

    ''' <summary>
    ''' Genera Formato 1010 de Exogena
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <returns></returns>
    Public Function GenerarExogenaXMLFormat1010(criterias As Dictionary(Of String, String)) As String Implements IFormatosExogena.GenerarExogenaXMLFormat1010
        Dim xmlCriterias = Utils.DictionaryToXML(criterias)
        Dim Data As List(Of SP_ExogenaFormat1010_Result) = _AccountingBalanceRepository.ExogenaFormat1010(xmlCriterias)

        Dim value As Decimal = Data.Sum(Function(x) x.PatrimonialValue)

        Dim builder As StringBuilder = New StringBuilder()
        builder.AppendLine("<?xml version=""1.0"" encoding=""ISO-8859-1""?>")
        builder.AppendLine("<mas xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xsi:noNamespaceSchemaLocation=""../xsd/1010.xsd"">")
        builder.AppendLine("<Cab>")
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "Ano", criterias("Year")))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "CodCpt", criterias("Concept")))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "Formato", criterias("Format")))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "Version", criterias("Version")))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "NumEnvio", criterias("SendingNumber")))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "FecEnvio", Date.Now.ToString("yyyy-MM-ddTHH:mm:ss").Trim))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "FecInicial", DateSerial(criterias("Year"), 1, 1).ToString("yyyy-MM-dd").Trim))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "FecFinal", DateSerial(criterias("Year"), 12, 31).ToString("yyyy-MM-dd").Trim))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "ValorTotal", CDec(value).ToString))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "CantReg", Data.Count()))
        builder.AppendLine("</Cab>")

        For Each a As SP_ExogenaFormat1010_Result In Data
            builder.Append("<socios")
            builder.Append(" tdoc=""" & a.IdentificationType & """")
            builder.Append(" nid=""" & a.IdentificationNumber & """")

            If a.IdentificationType = 31 Then
                builder.Append(" dv=""" & a.DigitVerification & """")
                builder.Append(" raz=""" & a.BusinessName & """")
            Else
                builder.Append(" apl1=""" & a.FirstLastName & """")
                If Not String.IsNullOrEmpty(a.SecondLastName) Then
                    builder.Append(" apl2=""" & a.SecondLastName & """")
                End If
                builder.Append(" nom1=""" & a.FirstName & """")
                If Not String.IsNullOrEmpty(a.SecondLastName) Then
                    builder.Append(" nom2=""" & a.SecondName & """")
                End If
            End If

            If Not String.IsNullOrEmpty(a.Addresss) Then
                builder.Append(" dir=""" & a.Addresss & """")
                builder.Append(" dpto=""" & a.DepartmentCode & """")
                builder.Append(" mun=""" & a.CityCode.Replace(a.DepartmentCode, "") & """")
                builder.Append(" pais=""" & a.CodeCountry & """")
            End If

            builder.Append(" val=""" & a.PatrimonialValue & """")
            builder.Append(" por=""" & a.ParticipationPercentage & """")
            builder.Append(" dec=""" & a.ParticipationPercentageDecimal & """")

            builder.AppendLine(" />")
        Next
        builder.AppendLine("</mas>")
        Return builder.ToString()
    End Function

    ''' <summary>
    ''' Genera Formato 1011 de Exogena
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <returns></returns>
    Public Function GenerarExogenaXMLFormat1011(criterias As Dictionary(Of String, String)) As String Implements IFormatosExogena.GenerarExogenaXMLFormat1011
        Dim xmlCriterias = Utils.DictionaryToXML(criterias)
        Dim Data As List(Of SP_ExogenaFormat1011_Result) = _AccountingBalanceRepository.ExogenaFormat1011(xmlCriterias)

        Dim value As Decimal = Data.Sum(Function(x) x.Balance)

        Dim builder As StringBuilder = New StringBuilder()
        builder.AppendLine("<?xml version=""1.0"" encoding=""ISO-8859-1""?>")
        builder.AppendLine("<mas xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xsi:noNamespaceSchemaLocation=""../xsd/101.xsd"">")
        builder.AppendLine("<Cab>")
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "Ano", criterias("Year")))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "CodCpt", criterias("Concept")))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "Formato", criterias("Format")))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "Version", criterias("Version")))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "NumEnvio", criterias("SendingNumber")))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "FecEnvio", Date.Now.ToString("yyyy-MM-ddTHH:mm:ss").Trim))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "FecInicial", DateSerial(criterias("Year"), 1, 1).ToString("yyyy-MM-dd").Trim))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "FecFinal", DateSerial(criterias("Year"), 12, 31).ToString("yyyy-MM-dd").Trim))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "ValorTotal", CDec(value).ToString))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "CantReg", Data.Count()))
        builder.AppendLine("</Cab>")

        For Each a As SP_ExogenaFormat1011_Result In Data
            builder.Append("<decl")
            builder.Append(" cpt=""" & a.Concept & """")
            builder.Append(" sal=""" & a.Balance & """")
            builder.AppendLine(" />")
        Next
        builder.AppendLine("</mas>")
        Return builder.ToString()
    End Function

    ''' <summary>
    ''' Genera Formato 1012 de Exogena
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <returns></returns>
    Public Function GenerarExogenaXMLFormat1012(criterias As Dictionary(Of String, String)) As String Implements IFormatosExogena.GenerarExogenaXMLFormat1012
        Dim xmlCriterias = Utils.DictionaryToXML(criterias)
        Dim Data As List(Of SP_ExogenaFormat1012_Result) = _AccountingBalanceRepository.ExogenaFormat1012(xmlCriterias)

        Dim value As Decimal = Data.Sum(Function(x) x.Balance)

        Dim builder As StringBuilder = New StringBuilder()
        builder.AppendLine("<?xml version=""1.0"" encoding=""ISO-8859-1""?>")
        builder.AppendLine("<mas xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xsi:noNamespaceSchemaLocation=""../xsd/1012.xsd"">")
        builder.AppendLine("<Cab>")
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "Ano", criterias("Year")))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "CodCpt", criterias("Concept")))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "Formato", criterias("Format")))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "Version", criterias("Version")))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "NumEnvio", criterias("SendingNumber")))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "FecEnvio", Date.Now.ToString("yyyy-MM-ddTHH:mm:ss").Trim))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "FecInicial", DateSerial(criterias("Year"), 1, 1).ToString("yyyy-MM-dd").Trim))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "FecFinal", DateSerial(criterias("Year"), 12, 31).ToString("yyyy-MM-dd").Trim))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "ValorTotal", CDec(value).ToString))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "CantReg", Data.Count()))
        builder.AppendLine("</Cab>")

        For Each a As SP_ExogenaFormat1012_Result In Data
            builder.Append("<dectri")
            builder.Append(" cpt=""" & a.Concept & """")
            builder.Append(" tdoc=""" & a.IdentificationType & """")
            builder.Append(" nid=""" & a.IdentificationNumber & """")

            If a.IdentificationType = 31 Then
                builder.Append(" dv=""" & a.DigitVerification & """")
                builder.Append(" raz=""" & a.BusinessName & """")
            Else
                builder.Append(" apl1=""" & a.FirstLastName & """")
                If Not String.IsNullOrEmpty(a.SecondLastName) Then
                    builder.Append(" apl2=""" & a.SecondLastName & """")
                End If
                builder.Append(" nom1=""" & a.FirstName & """")
                If Not String.IsNullOrEmpty(a.SecondLastName) Then
                    builder.Append(" nom2=""" & a.SecondName & """")
                End If
            End If

            builder.Append(" val=""" & a.Balance & """")

            builder.AppendLine(" />")
        Next
        builder.AppendLine("</mas>")
        Return builder.ToString()
    End Function

    ''' <summary>
    ''' Genera Formato 1056 de Exogena
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <returns></returns>
    Public Function GenerarExogenaXMLFormat1056(criterias As Dictionary(Of String, String)) As String Implements IFormatosExogena.GenerarExogenaXMLFormat1056
        Dim xmlCriterias = Utils.DictionaryToXML(criterias)
        Dim Data As List(Of SP_ExogenaFormat1056_Result) = _AccountingBalanceRepository.ExogenaFormat1056(xmlCriterias)

        Dim value As Decimal = Data.Sum(Function(x) x.PaymentValue)

        Dim builder As StringBuilder = New StringBuilder()
        builder.AppendLine("<?xml version=""1.0"" encoding=""ISO-8859-1""?>")
        builder.AppendLine("<mas xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xsi:noNamespaceSchemaLocation=""../xsd/1056.xsd"">")
        builder.AppendLine("<Cab>")
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "Ano", criterias("Year")))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "CodCpt", criterias("Concept")))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "Formato", criterias("Format")))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "Version", criterias("Version")))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "NumEnvio", criterias("SendingNumber")))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "FecEnvio", Date.Now.ToString("yyyy-MM-ddTHH:mm:ss").Trim))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "FecInicial", DateSerial(criterias("Year"), 1, 1).ToString("yyyy-MM-dd").Trim))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "FecFinal", DateSerial(criterias("Year"), 12, 31).ToString("yyyy-MM-dd").Trim))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "ValorTotal", CDec(value).ToString))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "CantReg", Data.Count()))
        builder.AppendLine("</Cab>")

        For Each a As SP_ExogenaFormat1056_Result In Data
            builder.Append("<abonos")
            builder.Append(" cpto=""" & a.Concept & """")
            builder.Append(" tdoc=""" & a.IdentificationType & """")
            builder.Append(" nid=""" & a.IdentificationNumber & """")

            If a.IdentificationType = 31 Then
                builder.Append(" dv=""" & a.DigitVerification & """")
                builder.Append(" raz=""" & a.BusinessName & """")
            Else
                builder.Append(" apl1=""" & a.FirstLastName & """")
                If Not String.IsNullOrEmpty(a.SecondLastName) Then
                    builder.Append(" apl2=""" & a.SecondLastName & """")
                End If
                builder.Append(" nom1=""" & a.FirstName & """")
                If Not String.IsNullOrEmpty(a.SecondLastName) Then
                    builder.Append(" nom2=""" & a.SecondName & """")
                End If
            End If

            If Not String.IsNullOrEmpty(a.Addresss) Then
                builder.Append(" dir=""" & a.Addresss & """")
                builder.Append(" dpto=""" & a.DepartmentCode & """")
                builder.Append(" mun=""" & a.CityCode.Replace(a.DepartmentCode, "") & """")
                builder.Append(" pais=""" & a.CodeCountry & """")
            End If

            builder.Append(" pag=""" & a.PaymentValue & """")
            builder.Append(" iva=""" & a.IVA & """")
            builder.Append(" rpren=""" & a.ReteDeductible & """")
            builder.Append(" raren=""" & a.ReteNoDeductible & """")
            builder.Append(" rpirc=""" & a.ReteIVAComun & """")
            builder.Append(" rpind=""" & a.ReteIVAExt & """")

            builder.AppendLine(" />")
        Next
        builder.AppendLine("</mas>")
        Return builder.ToString()
    End Function

    ''' <summary>
    ''' Genera Formato 1647 de Exogena
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <returns></returns>
    Public Function GenerarExogenaXMLFormat1647(criterias As Dictionary(Of String, String)) As String Implements IFormatosExogena.GenerarExogenaXMLFormat1647
        Dim xmlCriterias = Utils.DictionaryToXML(criterias)
        Dim Data As List(Of SP_ExogenaFormat1647_Result) = _AccountingBalanceRepository.ExogenaFormat1647(xmlCriterias)

        Dim value As Decimal = Data.Sum(Function(x) x.ValueOperation)

        Dim builder As StringBuilder = New StringBuilder()
        builder.AppendLine("<?xml version=""1.0"" encoding=""ISO-8859-1""?> ")
        builder.AppendLine("<mas xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xsi:noNamespaceSchemaLocation=""../xsd/1647.xsd""> ")
        builder.AppendLine("<Cab>")
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "Ano", criterias("Year")))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "CodCpt", criterias("Concept")))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "Formato", criterias("Format")))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "Version", criterias("Version")))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "NumEnvio", criterias("SendingNumber")))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "FecEnvio", Date.Now.ToString("yyyy-MM-ddTHH:mm:ss").Trim))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "FecInicial", DateSerial(criterias("Year"), 1, 1).ToString("yyyy-MM-dd").Trim))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "FecFinal", DateSerial(criterias("Year"), 12, 31).ToString("yyyy-MM-dd").Trim))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "ValorTotal", CDec(value).ToString))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "CantReg", Data.Count()))
        builder.AppendLine("</Cab>")

        For Each a As SP_ExogenaFormat1647_Result In Data
            builder.Append("<ingresos")
            builder.Append(" con=""" & a.Concept & """")
            builder.Append(" tdoc=""" & a.IdentificationTypeRecive & """")
            builder.Append(" nid=""" & a.IdentificationNumberRecive & """")

            If a.IdentificationTypeRecive = 31 Then
                builder.Append(" dv=""" & a.DigitVerificationRecive & """")
                builder.Append(" raz=""" & a.BusinessNameRecive & """")
            Else
                builder.Append(" apl1=""" & a.FirstLastNameRecive & """")
                If Not String.IsNullOrEmpty(a.SecondLastNameRecive) Then
                    builder.Append(" apl2=""" & a.SecondLastNameRecive & """")
                End If
                builder.Append(" nom1=""" & a.FirstNameRecive & """")
                If Not String.IsNullOrEmpty(a.SecondLastNameRecive) Then
                    builder.Append(" nom2=""" & a.SecondNameRecive & """")
                End If
            End If

            If Not String.IsNullOrEmpty(a.CodeCountryRecive) Then
                builder.Append(" pais=""" & a.CodeCountryRecive & """")
            End If

            builder.Append(" vtotal=""" & a.ValueOperation & """")
            builder.Append(" ving=""" & a.IngressValue & """")
            builder.Append(" vret=""" & a.RetentionValue & """")

            builder.Append(" tdoc2=""" & a.IdentificationTypeSend & """")
            builder.Append(" nid2i=""" & a.IdentificationNumberSend & """")

            If a.IdentificationTypeSend = 31 Then
                builder.Append(" dvi=""" & a.DigitVerificationSend & """")
                builder.Append(" razi=""" & a.BusinessNameSend & """")
            Else
                builder.Append(" apl1i=""" & a.FirstLastNameSend & """")
                If Not String.IsNullOrEmpty(a.SecondLastNameSend) Then
                    builder.Append(" apl2i=""" & a.SecondLastNameSend & """")
                End If
                builder.Append(" nom1i=""" & a.FirstNameSend & """")
                If Not String.IsNullOrEmpty(a.SecondLastNameSend) Then
                    builder.Append(" nom2i=""" & a.SecondNameSend & """")
                End If
            End If

            If Not String.IsNullOrEmpty(a.AddressSend) Then
                builder.Append(" dir=""" & a.AddressSend & """")
                builder.Append(" cdpt=""" & a.CodeDepartmentSend & """")
                builder.Append(" cmcp=""" & a.CodeCitySend.Replace(a.CodeDepartmentSend, "") & """")
                builder.Append(" paist=""" & a.CodeCountrySend & """")
            End If

            builder.AppendLine(" />")
        Next
        builder.AppendLine("</mas>")
        Return builder.ToString()
    End Function

    ''' <summary>
    ''' Genera Formato 2275 de Exogena
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <returns></returns>
    Public Function GenerarExogenaXMLFormat2275(criterias As Dictionary(Of String, String)) As String Implements IFormatosExogena.GenerarExogenaXMLFormat2275
        Dim xmlCriterias = Utils.DictionaryToXML(criterias)
        Dim Data As List(Of SP_ExogenaFormat2275_Result) = _AccountingBalanceRepository.ExogenaFormat2275(xmlCriterias)

        Dim value As Decimal = Data.Sum(Function(x) x.IncomeValue)

        Dim builder As StringBuilder = New StringBuilder()
        builder.AppendLine("<?xml version=""1.0"" encoding=""ISO-8859-1""?>")
        builder.AppendLine("<mas xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xsi:noNamespaceSchemaLocation=""../xsd/2275.xsd"">")
        builder.AppendLine("<Cab>")
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "Ano", criterias("Year")))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "CodCpt", criterias("Concept")))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "Formato", criterias("Format")))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "Version", criterias("Version")))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "NumEnvio", criterias("SendingNumber")))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "FecEnvio", Date.Now.ToString("yyyy-MM-ddTHH:mm:ss").Trim))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "FecInicial", DateSerial(criterias("Year"), 1, 1).ToString("yyyy-MM-dd").Trim))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "FecFinal", DateSerial(criterias("Year"), 12, 31).ToString("yyyy-MM-dd").Trim))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "ValorTotal", CDec(value).ToString))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "CantReg", Data.Count()))
        builder.AppendLine("</Cab>")

        For Each a As SP_ExogenaFormat2275_Result In Data
            builder.Append("<ingresos")
            builder.Append(" cpt=""" & a.Concept & """")
            builder.Append(" tdoc=""" & a.IdentificationType & """")
            builder.Append(" nit=""" & a.IdentificationNumber & """")

            If a.IdentificationType = 31 Then
                builder.Append(" raz=""" & a.BusinessName & """")
            Else
                builder.Append(" pap=""" & a.FirstLastName & """")
                If Not String.IsNullOrEmpty(a.SecondLastName) Then
                    builder.Append(" sap=""" & a.SecondLastName & """")
                End If
                builder.Append(" pno=""" & a.FirstName & """")
                If Not String.IsNullOrEmpty(a.SecondLastName) Then
                    builder.Append(" ono=""" & a.SecondName & """")
                End If
            End If

            If Not String.IsNullOrEmpty(a.Addresss) Then
                builder.Append(" dir=""" & a.Addresss & """")
                builder.Append(" dpto=""" & a.DepartmentCode & """")
                builder.Append(" mun=""" & a.CityCode.Replace(a.DepartmentCode, "") & """")
                builder.Append(" pais=""" & a.CodeCountry & """")
            End If

            If Not String.IsNullOrEmpty(a.Email) Then
                builder.Append(" email=""" & a.Email & """")
            End If

            builder.Append(" vtotali=""" & a.IncomeValue & """")
            builder.Append(" vrenta=""" & a.ExemptIncomeValue & """")

            builder.AppendLine(" />")
        Next
        builder.AppendLine("</mas>")
        Return builder.ToString()
    End Function

    ''' <summary>
    ''' Genera Formato 2276 de Exogena
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <returns></returns>
    Public Function GenerarExogenaXMLFormat2276(criterias As Dictionary(Of String, String)) As String Implements IFormatosExogena.GenerarExogenaXMLFormat2276
        Dim xmlCriterias = Utils.DictionaryToXML(criterias)
        Dim Data As List(Of SP_ExogenaFormat2276_Result) = _AccountingBalanceRepository.ExogenaFormat2276(xmlCriterias)

        Dim value As Decimal = Data.Sum(Function(x) x.PaymentsForWages)

        Dim builder As StringBuilder = New StringBuilder()
        builder.AppendLine("<?xml version=""1.0"" encoding=""ISO-8859-1""?> ")
        builder.AppendLine("<mas xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xsi:noNamespaceSchemaLocation=""../xsd/2276.xsd""> ")
        builder.AppendLine("<Cab>")
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "Ano", criterias("Year")))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "CodCpt", criterias("Concept")))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "Formato", criterias("Format")))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "Version", criterias("Version")))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "NumEnvio", criterias("SendingNumber")))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "FecEnvio", Date.Now.ToString("yyyy-MM-ddTHH:mm:ss").Trim))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "FecInicial", DateSerial(criterias("Year"), 1, 1).ToString("yyyy-MM-dd").Trim))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "FecFinal", DateSerial(criterias("Year"), 12, 31).ToString("yyyy-MM-dd").Trim))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "ValorTotal", CDec(value).ToString))
        builder.AppendLine(String.Format("<{0}>{1}</{0}", "CantReg", Data.Count()))
        builder.AppendLine("</Cab>")

        For Each a As SP_ExogenaFormat2276_Result In Data
            builder.Append("<rentra")
            builder.Append(" tdocb=""" & a.IdentificationType & """")
            builder.Append(" nitb=""" & a.IdentificationNumber & """")
            builder.Append(" pap=""" & a.FirstLastName & """")
            builder.Append(" sap=""" & a.SecondLastName & """")
            builder.Append(" pno=""" & a.FirstName & """")
            builder.Append(" ono=""" & a.SecondName & """")

            If Not String.IsNullOrEmpty(a.Addresss) Then
                builder.Append(" dir=""" & a.Addresss & """")
                builder.Append(" dpto=""" & a.DepartmentCode & """  ")
                builder.Append(" mun=""" & a.CityCode.Replace(a.DepartmentCode, "") & """")
                builder.Append(" pais=""" & a.CodeCountry & """")
            End If

            builder.Append(" pasa=""" & a.PaymentsForWages & """")
            builder.Append(" paec=""" & a.PaymentsForChurchEmoluments & """")
            builder.Append(" paho=""" & a.PaymentsForFees & """")
            builder.Append(" pase=""" & a.PaymentsForServices & """")
            builder.Append(" paco=""" & a.PaymentsForCommisions & """")
            builder.Append(" papre=""" & a.PaymentsForSocialBenefits & """")
            builder.Append(" pavia=""" & a.PaymentsForPerDiem & """")
            builder.Append(" paga=""" & a.PaymentsForRepresentationExpenses & """")
            builder.Append(" patra=""" & a.PaymentsForCooperativeAssociateWork & """")
            builder.Append(" potro=""" & a.OtherPayments & """")
            builder.Append(" pabo=""" & a.PaymentsWithBonds & """")
            builder.Append(" cein=""" & a.UnemploymentValue & """")
            builder.Append(" peju=""" & a.RetirementValue & """")
            builder.Append(" apos=""" & a.HealthValue & """")
            builder.Append(" apof=""" & a.PensionValue & """")
            builder.Append(" apov=""" & a.VoluntaryPensionValue & """")
            builder.Append(" apafc=""" & a.AFCValue & """")
            builder.Append(" vare=""" & a.RetentionValue & """")

            builder.AppendLine(" />")
        Next
        builder.AppendLine("</mas>")
        Return builder.ToString()
    End Function

#End Region

End Class
