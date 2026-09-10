'***********************************************************************
' Assembly         : Infrastructure.Data.Xpo.SecurityRepository
' Author           : WalterSierra
' Created          : 11-03-2011
'
' Last Modified By : Juan F. Tamayo
' Last Modified On : 2013-02-28
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports DevExpress.Xpo.DB
Imports DevExpress.Xpo
Imports DevExpress.Xpo.Metadata
Imports System.ServiceModel
Imports Infrastructure.CrossCutting.Xpo.Base
Imports System.IO
Imports Infrastructure.CrossCutting.Base
Imports DevExpress.Data.Filtering
Imports DevExpress.Data.Linq
Imports System.Configuration
Imports DevExpress.Data.PLinq
#End Region

''' <summary>
''' Clase que expone los servicios de los repositorios
''' </summary>
Public Class GlosasServicesXpo
    Inherits XpoBaseService

#Region "Fields"

    ''' <summary>
    ''' uri donde estan localizado los servicios xpo
    ''' </summary>
    Dim uriServiceEntitiesXpo As String

    ''' <summary>
    ''' protocolo utilizado para los servicios xpo
    ''' </summary>
    Dim protocolServicesXpo As Protocol

    ''' <summary>
    ''' Variable de Tipo Consultas asincronas de xpo
    ''' </summary>
    Dim serverMode As XPInstantFeedbackSource

    ''' <summary>
    ''' variable que contiene el mapeo especifo por entidad para realizar la consulta mediante xpo
    ''' </summary>
    Dim classEntity As XPClassInfo

#End Region

#Region "Builders"

    ''' <summary>
    ''' Incializa una nueva instancia de la clase <see cref="GlosasServicesXpo" />.
    ''' </summary>
    Public Sub New(Company As String)
        'verifico que exista el archivo
        ReadConfiguration()
        'establezclo la capa de datos para XPO
        XpoDefault.DataLayer = New SimpleDataLayer(New WCFServiceDataStore(GetEndPoint, GetRemoteAddress, Company))

    End Sub

#End Region

#Region "Private Methods"

    ''' <summary>
    ''' metodo necesario para leer la configuracion xml de la aplicacion
    ''' </summary>
    Private Sub ReadConfiguration()
        'verifico que exista el archivo
        'If File.Exists(String.Concat(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "\", "Indigo Technologies", "\", "Indigo Crystal", "\", FileName)) = False Then
        '    Throw New FileNotFoundException("El Archivo de Configuracion no existe!", FileName)
        'End If
        ''leo el archivo
        'Dim xmlConfiguration As New DataTable("ConfiguracionGenesis")
        'With xmlConfiguration
        '    .Columns.Add("UrlServidorWeb")
        '    .Columns.Add("UrlServidorEntidades")
        '    .Columns.Add("ProtocoloURLServidorWeb")
        '    .Columns.Add("ProtocoloURlServidorEntidades")
        '    .Columns.Add("RutaActualizacion")
        '    .Columns.Add("BuscarActulizaciones")
        '    .ReadXml(String.Concat(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "\", "Indigo Technologies", "\", "Indigo Crystal", "\", FileName))
        'End With
        'cargo la uri de servicios
        'uriServiceEntitiesXpo = xmlConfiguration.Rows(0).Item("UrlServidorEntidades").ToString
        uriServiceEntitiesXpo = ConfigurationFile.Instance.UrlXpoWebServer

        'cargo el protocolo
        protocolServicesXpo = ConfigurationFile.Instance.ProtocolUrlXpoWebServer
    End Sub

    ''' <summary>
    ''' funcion para contatenar el nombre del endpoint por cada protocolo
    ''' </summary>
    ''' <returns>El Nombre de la configuracion del Endpoint Correspondiente</returns>
    Private Function GetEndPoint() As String
        Return System.String.Format("{0}_Endpoint", [Enum].GetName(GetType(Protocol), protocolServicesXpo))
    End Function

    ''' <summary>
    ''' funcion para contatenar el remoteaddress por cada protocolo
    ''' </summary>
    ''' <returns>El Nombre del remoteaddress del Endpoint Correspondiente</returns>
    Private Function GetRemoteAddress() As String
        Return System.String.Format("{0}XpoGate.svc/{1}", uriServiceEntitiesXpo, [Enum].GetName(GetType(Protocol), protocolServicesXpo))
    End Function

#End Region

#Region "Collections Reports"

    ''' <summary>
    ''' Función para obtener una lista de datos de una entidad XPO
    ''' </summary>
    ''' <typeparam name="T">Objeto XPO</typeparam>
    ''' <param name="Fun">Objeto tipo Función opcional para filtrar los datos necesarios</param>
    ''' <returns>Lista tipo T</returns>
    Public Function GetCollection(Of T)(Optional Fun As Func(Of T, Boolean) = Nothing, Optional criteria As String = Nothing) As List(Of T)
        Dim result = Me.LoadCollection(Of T)(XpoDefault.DataLayer, Fun, criteria)
        Return result
    End Function


#Region "Listar ObjectionReceptionD con PLinqServerModeSource"

    ''' <summary>
    ''' Obtiene todos los empleados
    ''' </summary>
    Public Function ListGetD(ByVal IdReceptionC As String) As PLinqServerModeSource

        Dim sessionNew = New Session(XpoDefault.DataLayer)

        Dim tableConcept As XPQuery(Of Common_ConceptGlosas) = New XPQuery(Of Common_ConceptGlosas)(sessionNew)
        Dim tableMov As XPQuery(Of GlosaMovementGlosaXpo) = New XPQuery(Of GlosaMovementGlosaXpo)(sessionNew)
        Dim tableObjD As XPQuery(Of GlosasObjectionDXpo) = New XPQuery(Of GlosasObjectionDXpo)(sessionNew)
        Dim tableObjC As XPQuery(Of GlosasObjectionCXpo) = New XPQuery(Of GlosasObjectionCXpo)(sessionNew)
        Dim tableCustomer As XPQuery(Of Common_CustomerXpo) = New XPQuery(Of Common_CustomerXpo)(sessionNew)

        Dim TmpDocumentype As String = 1
        Dim TmpQueryableSource = From T1 In tableObjC
                                 Join T2 In tableObjD On T2.GlosaObjectionsReceptionCId.Id Equals T1.Id
                                 Join T3 In tableMov On T3.InvoiceNumber Equals T2.InvoiceNumber
                                 Join T4 In tableConcept On T4.Id Equals T3.CodeGlosaId
                                 Join t5 In tableCustomer On t5.Id Equals T1.CustomerId.Id
                          Where T1.Id = IdReceptionC
          Select T1.RadicatedConsecutive, T1.DocumentDate, T1.ReceivesTheSettled, t5.Name, t5.Nit, T4.Code, T4.NameSpecific, T2.InvoiceNumber, T2.DocumentType, T3.JustificationGlosa, T3.ValueGlosado, T3.ValueAcceptedFirstInstance, T3.ValueReiterated, T3.ValueAcceptedSecondInstance
        Dim b As New PLinqServerModeSource
        b.Source = TmpQueryableSource.ToList()
        Return b
    End Function

#End Region


    ''' <summary>
    ''' Función para obtener una lista de datos de una entidad XPO
    ''' </summary>
    ''' <typeparam name="T">Objeto XPO</typeparam>
    ''' <param name="Fun">Objeto tipo Función opcional para filtrar los datos necesarios</param>
    ''' <returns>Lista tipo T</returns>
    'Public Function GetCollectionMovements(Of T)(Optional Fun As Func(Of T, Boolean) = Nothing, Optional criteria As String = Nothing) As IList
    '    Dim idObjC = Nothing
    '    If criteria IsNot Nothing Then
    '        idObjC = criteria.Split("=").LastOrDefault
    '    End If
    '    Dim session As New Session(XpoDefault.DataLayer)
    '    Dim listObjD = Me.LoadCollection(Of Glosas_GlosaObjectionsReceptionD)(XpoDefault.DataLayer, Nothing, "GlosaObjectionsReceptionCId.Id=" & idObjC, session).ToList
    '    Dim listMovDetail As New List(Of Glosas_GlosaMovementGlosa)

    '    For Each item In listObjD
    '        Dim ListMovDetailAux = Me.LoadCollection(Of Glosas_GlosaMovementGlosa)(XpoDefault.DataLayer, Nothing, "MainGlosa = True AND InvoiceDetailId.InvoiceNumber='" & item.InvoiceNumber & "'", session).ToList
    '        If ListMovDetailAux IsNot Nothing AndAlso ListMovDetailAux.Count > 0 Then
    '            listMovDetail.AddRange(ListMovDetailAux)
    '        End If
    '    Next
    '    Dim ListMovements = Me.LoadCollection(Of T)(XpoDefault.DataLayer, Fun, criteria, session)
    '    Dim aux = ListMovements.OfType(Of Glosas_GlosaMovementGlosa)().FirstOrDefault
    '    Dim customerId As Integer
    '    If aux IsNot Nothing Then
    '        customerId = aux.InvoiceDetailId.ObjectionsReceptionDId.GlosaObjectionsReceptionCId.CustomerId
    '    Else
    '        If listObjD IsNot Nothing AndAlso listObjD.Count > 0 Then
    '            customerId = listObjD.FirstOrDefault.GlosaObjectionsReceptionCId.CustomerId
    '        End If
    '    End If
    '    Dim Customer = Me.LoadCollection(Of GlosasCustomerXpo)(XpoDefault.DataLayer, Nothing, "Id=" & customerId, session).FirstOrDefault
    '    listMovDetail.AddRange(ListMovements)
    '    'Dim Result = (From a In listMovDetail.OfType(Of T)()
    '    '             Select Movimientos = a, CustomerAux = Customer.Name).Distinct().ToList()
    '    Dim ListMov = listMovDetail.OfType(Of Glosas_GlosaMovementGlosa)().Distinct().ToList
    '    Dim groupExample = From a In ListMov
    '    Group a By idAux = a.InvoiceDetailId.Id, invoiceAux = a.InvoiceNumber, serviceAux = a.InvoiceDetailId.ServiceName, idQxAux = a.InvoiceDetailIdQX Into Group
    '    Select Group.ToList
    '    Dim listresult As List(Of Glosas_GlosaMovementGlosa) = New List(Of Glosas_GlosaMovementGlosa)
    '    Dim sumGlosa = 0
    '    Dim sumAccepted = 0


    '    For Each item In groupExample
    '        For Each itemAux In item
    '            sumGlosa = item.Max(Function(x) x.ValueGlosado)
    '            sumAccepted = sumAccepted + itemAux.ValueAcceptedFirstInstance
    '        Next
    '        Dim glosa = item.Where(Function(x) x.MainGlosa = True).FirstOrDefault
    '        glosa.ValueGlosado = sumGlosa
    '        If glosa.JustificationGlosa IsNot Nothing Then
    '            glosa.JustificationGlosa = StripHTML(glosa.JustificationGlosa)
    '        End If
    '        glosa.ValueAcceptedFirstInstance = sumAccepted
    '        listresult.Add(glosa)
    '        sumGlosa = 0
    '        sumAccepted = 0
    '    Next
    '    Dim dataResult = (From e In listresult
    '                     Select Movimientos = e, CustomerAux = Customer.Name).Distinct().ToList
    '    Return dataResult
    'End Function

    Private Function StripHTML(source As String) As String
        Try
            Dim result As String

            ' Remove HTML Development formatting
            ' Replace line breaks with space
            ' because browsers inserts space
            result = source.Replace(vbCr, " ")
            ' Replace line breaks with space
            ' because browsers inserts space
            result = result.Replace(vbLf, " ")
            ' Remove step-formatting
            result = result.Replace(vbTab, String.Empty)
            ' Remove repeating spaces because browsers ignore them
            result = System.Text.RegularExpressions.Regex.Replace(result, "( )+", " ")

            ' Remove the header (prepare first by clearing attributes)
            result = System.Text.RegularExpressions.Regex.Replace(result, "<( )*head([^>])*>", "<head>", System.Text.RegularExpressions.RegexOptions.IgnoreCase)
            result = System.Text.RegularExpressions.Regex.Replace(result, "(<( )*(/)( )*head( )*>)", "</head>", System.Text.RegularExpressions.RegexOptions.IgnoreCase)
            result = System.Text.RegularExpressions.Regex.Replace(result, "(<head>).*(</head>)", String.Empty, System.Text.RegularExpressions.RegexOptions.IgnoreCase)

            ' remove all scripts (prepare first by clearing attributes)
            result = System.Text.RegularExpressions.Regex.Replace(result, "<( )*script([^>])*>", "<script>", System.Text.RegularExpressions.RegexOptions.IgnoreCase)
            result = System.Text.RegularExpressions.Regex.Replace(result, "(<( )*(/)( )*script( )*>)", "</script>", System.Text.RegularExpressions.RegexOptions.IgnoreCase)
            'result = System.Text.RegularExpressions.Regex.Replace(result,
            '         @"(<script>)([^(<script>\.</script>)])*(</script>)",
            '         string.Empty,
            '         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            result = System.Text.RegularExpressions.Regex.Replace(result, "(<script>).*(</script>)", String.Empty, System.Text.RegularExpressions.RegexOptions.IgnoreCase)

            ' remove all styles (prepare first by clearing attributes)
            result = System.Text.RegularExpressions.Regex.Replace(result, "<( )*style([^>])*>", "<style>", System.Text.RegularExpressions.RegexOptions.IgnoreCase)
            result = System.Text.RegularExpressions.Regex.Replace(result, "(<( )*(/)( )*style( )*>)", "</style>", System.Text.RegularExpressions.RegexOptions.IgnoreCase)
            result = System.Text.RegularExpressions.Regex.Replace(result, "(<style>).*(</style>)", String.Empty, System.Text.RegularExpressions.RegexOptions.IgnoreCase)

            ' insert tabs in spaces of <td> tags
            result = System.Text.RegularExpressions.Regex.Replace(result, "<( )*td([^>])*>", vbTab, System.Text.RegularExpressions.RegexOptions.IgnoreCase)

            ' insert line breaks in places of <BR> and <LI> tags
            result = System.Text.RegularExpressions.Regex.Replace(result, "<( )*br( )*>", vbCr, System.Text.RegularExpressions.RegexOptions.IgnoreCase)
            result = System.Text.RegularExpressions.Regex.Replace(result, "<( )*li( )*>", vbCr, System.Text.RegularExpressions.RegexOptions.IgnoreCase)

            ' insert line paragraphs (double line breaks) in place
            ' if <P>, <DIV> and <TR> tags
            result = System.Text.RegularExpressions.Regex.Replace(result, "<( )*div([^>])*>", vbCr & vbCr, System.Text.RegularExpressions.RegexOptions.IgnoreCase)
            result = System.Text.RegularExpressions.Regex.Replace(result, "<( )*tr([^>])*>", vbCr & vbCr, System.Text.RegularExpressions.RegexOptions.IgnoreCase)
            result = System.Text.RegularExpressions.Regex.Replace(result, "<( )*p([^>])*>", vbCr & vbCr, System.Text.RegularExpressions.RegexOptions.IgnoreCase)

            ' Remove remaining tags like <a>, links, images,
            ' comments etc - anything that's enclosed inside < >
            result = System.Text.RegularExpressions.Regex.Replace(result, "<[^>]*>", String.Empty, System.Text.RegularExpressions.RegexOptions.IgnoreCase)

            ' replace special characters:
            result = System.Text.RegularExpressions.Regex.Replace(result, " ", " ", System.Text.RegularExpressions.RegexOptions.IgnoreCase)

            result = System.Text.RegularExpressions.Regex.Replace(result, "&bull;", " * ", System.Text.RegularExpressions.RegexOptions.IgnoreCase)
            result = System.Text.RegularExpressions.Regex.Replace(result, "&lsaquo;", "<", System.Text.RegularExpressions.RegexOptions.IgnoreCase)
            result = System.Text.RegularExpressions.Regex.Replace(result, "&rsaquo;", ">", System.Text.RegularExpressions.RegexOptions.IgnoreCase)
            result = System.Text.RegularExpressions.Regex.Replace(result, "&trade;", "(tm)", System.Text.RegularExpressions.RegexOptions.IgnoreCase)
            result = System.Text.RegularExpressions.Regex.Replace(result, "&frasl;", "/", System.Text.RegularExpressions.RegexOptions.IgnoreCase)
            result = System.Text.RegularExpressions.Regex.Replace(result, "&lt;", "<", System.Text.RegularExpressions.RegexOptions.IgnoreCase)
            result = System.Text.RegularExpressions.Regex.Replace(result, "&gt;", ">", System.Text.RegularExpressions.RegexOptions.IgnoreCase)
            result = System.Text.RegularExpressions.Regex.Replace(result, "&copy;", "(c)", System.Text.RegularExpressions.RegexOptions.IgnoreCase)
            result = System.Text.RegularExpressions.Regex.Replace(result, "&reg;", "(r)", System.Text.RegularExpressions.RegexOptions.IgnoreCase)
            ' Remove all others. More can be added, see
            result = System.Text.RegularExpressions.Regex.Replace(result, "&(.{2,6});", String.Empty, System.Text.RegularExpressions.RegexOptions.IgnoreCase)

            ' for testing
            'System.Text.RegularExpressions.Regex.Replace(result,
            '       this.txtRegex.Text,string.Empty,
            '       System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            ' make line breaking consistent
            result = result.Replace(vbLf, vbCr)

            ' Remove extra line breaks and tabs:
            ' replace over 2 breaks with 2 and over 4 tabs with 4.
            ' Prepare first to remove any whitespaces in between
            ' the escaped characters and remove redundant tabs in between line breaks
            result = System.Text.RegularExpressions.Regex.Replace(result, "(" & vbCr & ")( )+(" & vbCr & ")", vbCr & vbCr, System.Text.RegularExpressions.RegexOptions.IgnoreCase)
            result = System.Text.RegularExpressions.Regex.Replace(result, "(" & vbTab & ")( )+(" & vbTab & ")", vbTab & vbTab, System.Text.RegularExpressions.RegexOptions.IgnoreCase)
            result = System.Text.RegularExpressions.Regex.Replace(result, "(" & vbTab & ")( )+(" & vbCr & ")", vbTab & vbCr, System.Text.RegularExpressions.RegexOptions.IgnoreCase)
            result = System.Text.RegularExpressions.Regex.Replace(result, "(" & vbCr & ")( )+(" & vbTab & ")", vbCr & vbTab, System.Text.RegularExpressions.RegexOptions.IgnoreCase)
            ' Remove redundant tabs
            result = System.Text.RegularExpressions.Regex.Replace(result, "(" & vbCr & ")(" & vbTab & ")+(" & vbCr & ")", vbCr & vbCr, System.Text.RegularExpressions.RegexOptions.IgnoreCase)
            ' Remove multiple tabs following a line break with just one tab
            result = System.Text.RegularExpressions.Regex.Replace(result, "(" & vbCr & ")(" & vbTab & ")+", vbCr & vbTab, System.Text.RegularExpressions.RegexOptions.IgnoreCase)
            ' Initial replacement target string for line breaks
            Dim breaks As String = vbCr & vbCr & vbCr
            ' Initial replacement target string for tabs
            Dim tabs As String = vbTab & vbTab & vbTab & vbTab & vbTab
            For index As Integer = 0 To result.Length - 1
                result = result.Replace(breaks, vbCr & vbCr)
                result = result.Replace(tabs, vbTab & vbTab & vbTab & vbTab)
                breaks = breaks & Convert.ToString(vbCr)
                tabs = tabs & Convert.ToString(vbTab)
            Next

            ' That's it.
            Return result
        Catch
            Return source
        End Try
    End Function

    ' ''' <summary>
    ' ''' Función para obtener una lista de datos de una entidad XPO
    ' ''' </summary>
    ' ''' <typeparam name="T">Objeto XPO</typeparam>
    ' ''' <param name="Fun">Objeto tipo Función opcional para filtrar los datos necesarios</param>
    ' ''' <returns>Lista tipo T</returns>
    Public Function GetCollectionConciliation(Of T)(Optional Fun As Func(Of T, Boolean) = Nothing, Optional criteria As String = Nothing, Optional Company As String = "") As IList
        Dim ListConciliation = Me.LoadCollection(Of T)(XpoDefault.DataLayer, Fun, criteria)
        ' Dim ListCompanyIndigo = Me.LoadCollection(Of Common_CompanyIndigo)(XpoDefault.DataLayer, Nothing, "CompanyCode=" & Company)

        'Dim Result = (From a In ListConciliation.OfType(Of T)()
        '          Select Movimientos = a, CompanyIndigo = (From e In ListCompanyIndigo Select e).ToList).ToList

        Dim Result = (From a In ListConciliation.OfType(Of T)()
                     Select Movimientos = a).ToList
        Return Result
    End Function

    ''' <summary>
    ''' Funcion para obtener un XPView
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <returns></returns>
    Public Function GetCollectionView(Of T)() As XPView
        Return Me.LoadView(Of T)(XpoDefault.DataLayer)
    End Function

    ''' <summary>
    ''' Function para obtener un XPDataView
    ''' </summary>
    ''' <returns></returns>
    Public Function GetDataView() As XPDataView
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim xpDataView As New XPDataView()
        Dim sql As String = "SELECT Id,Code,Name from Glosas.Responsible"
        Dim selectedData As SelectedData = sessionNew.ExecuteQuery(sql)
        'xpDataView.PopulateProperties(sessionNew.GetClassInfo(Of CustomQueryData)())
        xpDataView.AddProperty("Id", GetType(Integer))
        xpDataView.AddProperty("Code", GetType(String))
        xpDataView.AddProperty("Name", GetType(String))
        xpDataView.LoadData(selectedData)
        Return xpDataView
    End Function

#End Region

#Region "Public Methods"

    ''' <summary>
    ''' Consulta el listado de conceptos de detalles
    ''' </summary>
    ''' <returns></returns>
    Public Function GetTemplateJustification() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(GlosasJustificationTemplateXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Code;IdConcept.NameCode", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta el listado de conceptos de detalles
    ''' </summary>
    ''' <returns></returns>
    Public Function GetDetailedConcepts(Optional ByVal IdSpecificConcepts As Integer = 0) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = Nothing
        If IdSpecificConcepts <> 0 Then
            criteria = CriteriaOperator.Parse("IdSpecificConcept='" & CStr(IdSpecificConcepts) & "'")
        End If
        classEntity = sessionNew.GetClassInfo(GetType(GlosasDetailedConceptXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Codigo;Descripcion", criteria)
        Return serverMode
    End Function
    ''' <summary>
    ''' Consulta el listado de conceptos de detalles
    ''' </summary>
    ''' <returns></returns>
    Public Function GetGeneralConcepts() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(GlosasGeneralConceptsXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Codigo;Descripcion", Nothing)
        Return serverMode
    End Function
    ''' <summary>
    ''' Consulta el listado de responsables
    ''' </summary>
    ''' <returns></returns>
    Public Function GetResponsibles() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(GlosasResponsibleXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Codigo;Descripcion", Nothing)
        Return serverMode
    End Function
    ''' <summary>
    ''' Consulta el listado de responsables
    ''' </summary>
    ''' <returns></returns>
    Public Function ListResponseHierarchy() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(Glosas_GlosasResponseHierarchy))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name", Nothing)
        Return serverMode
    End Function
    ''' <summary>
    ''' Consulta el listado de Conceptos especificos
    ''' </summary>
    ''' <returns></returns>
    Public Function GetSpecificConcepts(Optional ByVal IdGeneralConcepts As Integer = 0) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = Nothing
        If IdGeneralConcepts <> 0 Then
            criteria = CriteriaOperator.Parse("IdGeneralConcept='" & CStr(IdGeneralConcepts) & "'")
        End If
        classEntity = sessionNew.GetClassInfo(GetType(GlosasSpecificConceptsXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Codigo;Descripcion", criteria)
        Return serverMode
    End Function
    ''' <summary>
    ''' Consulta el listado customers
    ''' </summary>
    ''' <returns></returns>
    Public Function GetCustomers() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("State = '1'") 'clientes activos
        classEntity = sessionNew.GetClassInfo(GetType(GlosasCustomerXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;NitName;Nit;Name;State", criteria)
        Return serverMode
    End Function

    Public Function GetObjectionsReception() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(GlosasObjectionCXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "RadicatedConsecutive;Comment", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todas las cabeceras de recepcion de objeciones que estan en estado de
    ''' 2-Confirmada sin evaluar y 5-Confirmada evaluada
    ''' </summary>
    ''' <returns></returns>
    Public Function ListObjectionReceptionCByState() As XPInstantFeedbackSource
        'Dim criteria As CriteriaOperator = CriteriaOperator.Parse("State In ('2', '5')") '2 - Confirmada sin evaluar : 5 -Confirmada evaluda
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("State in('1','2') AND Glosas_GlosaObjectionsReceptionDs[PortfolioGlosaId.State IN ('2','3') AND DocumentType = 1] OR Glosas_GlosaObjectionsReceptionDs[PortfolioGlosaId.State IN ('5','6') AND DocumentType = 2]") '2 - Confirmada sin evaluar : 5 -Confirmada evaluda
        '  Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Glosas_GlosaObjectionsReceptionDs[PortfolioGlosaId.State IN ('2','3') AND DocumentType = 1] OR Glosas_GlosaObjectionsReceptionDs[PortfolioGlosaId.State IN ('5','6') AND DocumentType = 2]") '2 - Confirmada sin evaluar : 5 -Confirmada evaluda
        classEntity = sessionNew.GetClassInfo(GetType(GlosasObjectionCXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;State;DocumentNumber;DocumentDate;RadicatedConsecutive;CustomerId.Name;CustomerId.Nit", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Obtiene las compañias.
    ''' </summary>
    ''' <returns></returns>
    Public Function GetCompany() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(GlosasCompanyXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Codigo;Descripcion", Nothing)
        Return serverMode
    End Function

    Public Function GetCustomersList() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(Glosas_SP_CustomerListResult))
        serverMode = New XPInstantFeedbackSource(classEntity, "Codigo;Descripcion", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Obtiene los objetos de tipos Cartera Glosa.
    ''' </summary>
    ''' <returns></returns>
    Public Function GetXPOPortfolioGlosa(ByVal Nit As String) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = Nothing
        criteria = CriteriaOperator.Parse("StatusTotal <> 1 AND Nit='" & Nit & "' AND Not([State] In ('1', '4', '7', '8', '10','13'))")
        '    criteria = CriteriaOperator.Parse("StatusTotal <> 1 AND Nit='" & Nit & "' AND Not([State] In ('1', '4','8', '9', '10','13')) ") 'sacamos el estado 7 que es pendiente sacar factura por conciliar
        classEntity = sessionNew.GetClassInfo(GetType(GlosasPortfolioGlosaXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "InvoiceNumber;InvoiceDate;PatientCode;PatientName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista facturas para traslado a cobro jurídico
    ''' </summary>
    ''' <param name="Nit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListPortfolio_AccountReceivable(ByVal Nit As String) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = Nothing
        criteria = CriteriaOperator.Parse("ThirdPartyId.Nit='" & Nit & "' AND [PortfolioStatus] In ('3')")
        classEntity = sessionNew.GetClassInfo(GetType(Portfolio_AccountReceivable))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;InvoiceNumber;AccountReceivableDate;ThirdPartyId.NitName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Obtiene las facturas que estan disponibles para pagos parciales de tipos Cartera Glosa.
    ''' </summary>
    ''' <returns></returns>
    Public Function ListXpoInvoicesByPartialPayments(ByVal Nit As String) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = Nothing
        criteria = CriteriaOperator.Parse("Nit='" & Nit & "' AND ([State] In ('11','12','14')) AND BalanceGlosa > 0")
        classEntity = sessionNew.GetClassInfo(GetType(GlosasPortfolioGlosaXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "InvoiceNumber;InvoiceDate;PatientCode;PatientName;BalanceGlosa", criteria)
        Return serverMode
    End Function


    ''' <summary>
    ''' Obtiene una lista de Parametros de Interface
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetParameterInterface() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(GlosasParametersInterfaceXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;ContainerName;CompanyName", Nothing)
        Return serverMode
    End Function

#Region ""
    ''' <summary>
    ''' Lista de detalles de radicado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListXPCollectionRadicateInvoiceD(ByVal radicateinvoiceCid As Integer?, Optional ByVal InvalidateOffice As Boolean = False) As XPCollection
        Dim sessionNew = New Session()
        Dim criteria As CriteriaOperator
        Dim collect As XPCollection
        If InvalidateOffice = True Then
            criteria = CriteriaOperator.Parse("RadicateInvoiceCId =" & radicateinvoiceCid)
        Else
            If radicateinvoiceCid Is Nothing Then
                criteria = CriteriaOperator.Parse("RadicateInvoiceCId = 0")
            Else
                criteria = CriteriaOperator.Parse("State <> 4 AND RadicateInvoiceCId =" & radicateinvoiceCid)
            End If
        End If
        classEntity = sessionNew.GetClassInfo(GetType(Glosas_RadicateInvoiceDxpo))
        collect = New XPCollection(sessionNew, classEntity, criteria)
        sessionNew.Disconnect()
        Return collect
    End Function


    ''' <summary>
    ''' Lista de detalles de radicado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListViewRadicateInvoiceDetail(ByVal radicateinvoiceCid As Integer?, Optional ByVal InvalidateOffice As Boolean = False) As XPCollection
        Dim sessionNew = New Session()
        Dim criteria As CriteriaOperator
        Dim collect As XPCollection
        If InvalidateOffice = True Then
            criteria = CriteriaOperator.Parse("RadicateInvoiceCId =" & radicateinvoiceCid)
        Else
            If radicateinvoiceCid Is Nothing Then
                criteria = CriteriaOperator.Parse("RadicateInvoiceCId = 0")
            Else
                criteria = CriteriaOperator.Parse("State <> 4 AND RadicateInvoiceCId =" & radicateinvoiceCid)
            End If
        End If
        classEntity = sessionNew.GetClassInfo(GetType(PortfolioViewRadicateInvoiceDetailXpo))
        collect = New XPCollection(sessionNew, classEntity, criteria)
        sessionNew.Disconnect()
        Return collect
    End Function


    ''' <summary>
    ''' Lista de detalles de radicado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListXPCollectionRadicateInvoiceD() As XPCollection
        Dim sessionNew = New Session()
        classEntity = sessionNew.GetClassInfo(GetType(Glosas_RadicateInvoiceDxpo))
        Dim collection As New XPCollection(sessionNew, classEntity)
        collection.LoadingEnabled = False
        Return collection
    End Function

#End Region

#Region "GetEmployee LinqInstantFeedbackSpurce"
    Private WithEvents vlinq As New LinqInstantFeedbackSource

    ''' <summary>
    ''' Obtiene todos los empleados
    ''' </summary>
    Public Function ListObjectionsReceptionD() As LinqInstantFeedbackSource
        vlinq.KeyExpression = "Id"
        Return vlinq
    End Function

    Private Sub OnGetQueryable(ByVal sender As Object, ByVal e As GetQueryableEventArgs) Handles vlinq.GetQueryable
        Try
            Dim sessionNew = New Session(XpoDefault.DataLayer)
            Dim tableObjD As XPQuery(Of GlosasObjectionDXpo) = New XPQuery(Of GlosasObjectionDXpo)(sessionNew)
            Dim tableObjc As XPQuery(Of GlosasObjectionCXpo) = New XPQuery(Of GlosasObjectionCXpo)(sessionNew)

            Dim TmpQueryableSource = From T1 In tableObjD
                                     Join T2 In tableObjc On T1.GlosaObjectionsReceptionCId.Id Equals T2.Id
                                      Select New With {.id = T1.Id, .RadicatedConsecutive = T2.RadicatedConsecutive, .InvoiceNumber = T1.InvoiceNumber, .NitName = T2.CustomerId.NitName, .DocumentDate = T2.DocumentDate, .State = If(T2.State = "2", "Confirmado", If(T2.State = "1", "Sin Confirmar", "Anulado"))}
            ' Select T1.Id, T2.RadicatedConsecutive, T1.InvoiceNumber, T2.CustomerId.NitName, T2.DocumentDate,t2.State if
            'Dim prueba = TmpQueryableSource.ToList
            e.QueryableSource = TmpQueryableSource
            e.Tag = tableObjD
        Catch ex As Exception
            Dim gasd = ex.Message
        End Try
    End Sub

    Private Sub DismissQueryable(ByVal sender As Object, ByVal e As GetQueryableEventArgs) Handles vlinq.DismissQueryable
        Try
            'Dispose of the DataContext 
            CType(e.Tag, Object).Dispose()
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub
#End Region

#Region "Devolutions LinqInstantFeedBackSource"
    Private WithEvents vlinqDevolutions As New LinqInstantFeedbackSource

    ''' <summary>
    ''' Obtiene todos los empleados
    ''' </summary>
    Public Function GetDevolutionCList() As LinqInstantFeedbackSource
        vlinqDevolutions.KeyExpression = "Id"
        Return vlinqDevolutions
    End Function

    Private Sub OnGetQueryableDevolutions(ByVal sender As Object, ByVal e As GetQueryableEventArgs) Handles vlinqDevolutions.GetQueryable
        Try
            Dim sessionNew = New Session(XpoDefault.DataLayer)
            Dim tableObjD As XPQuery(Of GlosaDevolutionsReceptionDXpo) = New XPQuery(Of GlosaDevolutionsReceptionDXpo)(sessionNew)
            Dim tableObjc As XPQuery(Of GlosaDevolutionsReceptionCXpo) = New XPQuery(Of GlosaDevolutionsReceptionCXpo)(sessionNew)
            Dim TmpQueryableSource = From T1 In tableObjD
                                     Join T2 In tableObjc On T1.GlosaDevolutionsReceptionCId.Id Equals T2.Id
                                    Select New With {.id = T1.Id, .RadicatedConsecutive = T2.RadicatedConsecutive, .InvoiceNumber = T1.InvoiceNumber, .NitName = T2.CustomerId.NitName, .DocumentDate = T2.DocumentDate, .State = If(T2.State = "2", "Confirmado", If(T2.State = "1", "Sin Confirmar", "Anulado"))}

            'select T1.Id, T2.RadicatedConsecutive, T1.InvoiceNumber, NitName = T2.CustomerId.NitName, T2.DocumentDate
            '  Dim prueba = TmpQueryableSource.ToList
            e.QueryableSource = TmpQueryableSource
            e.Tag = tableObjD
        Catch ex As Exception
        End Try
    End Sub

    Private Sub DismissQueryableDevolutions(ByVal sender As Object, ByVal e As GetQueryableEventArgs) Handles vlinqDevolutions.DismissQueryable
        Try
            'Dispose of the DataContext 
            CType(e.Tag, Object).Dispose()
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub
#End Region

#Region "TransferJuridicalDebt LinqInstantFeedBackSource"
    Private WithEvents vlinqJuridical As New LinqInstantFeedbackSource

    ''' <summary>
    ''' Obtiene todos los empleados
    ''' </summary>
    Public Function GetTransferJuridicalCList() As LinqInstantFeedbackSource
        vlinqJuridical.KeyExpression = "Id"
        Return vlinqJuridical
    End Function

    Private Sub OnGetQueryableTransferJuridical(ByVal sender As Object, ByVal e As GetQueryableEventArgs) Handles vlinqJuridical.GetQueryable
        Try
            Dim sessionNew = New Session(XpoDefault.DataLayer)
            Dim tableObjD As XPQuery(Of Glosas_TransferJuridicalDebtCollectionD) = New XPQuery(Of Glosas_TransferJuridicalDebtCollectionD)(sessionNew)
            Dim tableObjc As XPQuery(Of Glosas_TransferJuridicalDebtCollectionC) = New XPQuery(Of Glosas_TransferJuridicalDebtCollectionC)(sessionNew)
            Dim TmpQueryableSource = From T1 In tableObjD
                                     Join T2 In tableObjc On T1.TransferJuridicalDebtCollectionCId.Id Equals T2.Id
                                     Select T1.Id, T2.JuridicalTransferConsecutive, T1.InvoiceNumber, NitName = T2.CustomerId.NitName.Trim, T2.DocumentDate
            ' Dim prueba = TmpQueryableSource.ToList
            e.QueryableSource = TmpQueryableSource
            e.Tag = tableObjD
        Catch ex As Exception
        End Try
    End Sub

    Private Sub DismissQueryableTransferJuridical(ByVal sender As Object, ByVal e As GetQueryableEventArgs) Handles vlinqJuridical.DismissQueryable
        Try
            'Dispose of the DataContext 
            CType(e.Tag, Object).Dispose()
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub
#End Region

#Region "InoviceRadicateD LinqInstantFeedBackSource"
    Private WithEvents vlinqInvoice As New LinqInstantFeedbackSource

    ''' <summary>
    ''' Obtiene todos los radicados
    ''' </summary>
    Public Function ListInvoiceRadicate() As LinqInstantFeedbackSource
        vlinqInvoice.KeyExpression = "Id"
        Return vlinqInvoice
    End Function

    Private Sub OnGetQueryableInoviceRadicateD(ByVal sender As Object, ByVal e As GetQueryableEventArgs) Handles vlinqInvoice.GetQueryable
        Try
            Dim sessionNew = New Session(XpoDefault.DataLayer)
            Dim tableObjD As XPQuery(Of Glosas_RadicateInvoiceD) = New XPQuery(Of Glosas_RadicateInvoiceD)(sessionNew)
            Dim tableObjc As XPQuery(Of Glosas_RadicateInvoiceC) = New XPQuery(Of Glosas_RadicateInvoiceC)(sessionNew)
            Dim TmpQueryableSource = From T1 In tableObjD
                                     Join T2 In tableObjc On T1.RadicateInvoiceCId.Id Equals T2.Id
                                     Select New With {.id = T1.Id, .RadicatedConsecutive = T2.RadicatedConsecutive, .InvoiceNumber = T1.InvoiceNumber, .NitName = T2.CustomerId.NitName, .DocumentDate = T2.DocumentDate, .State = If(T2.State = "2", "Confirmado", If(T2.State = "1", "Sin Confirmar", "Anulado"))}
            e.QueryableSource = TmpQueryableSource
            e.Tag = tableObjD
        Catch ex As Exception
        End Try
    End Sub

    Private Sub DismissQueryableInvoiceRadicateD(ByVal sender As Object, ByVal e As GetQueryableEventArgs) Handles vlinqInvoice.DismissQueryable
        Try
            'Dispose of the DataContext 
            CType(e.Tag, Object).Dispose()
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub
#End Region

#Region "InoviceRadicateD DateConfirm LinqInstantFeedBackSource"
    Private WithEvents vlinqInvoiceUpdatedateConfirm As New LinqInstantFeedbackSource

    ''' <summary>
    ''' Obtiene todos los empleados
    ''' </summary>
    Public Function ListInvoiceRadicateConfirm() As LinqInstantFeedbackSource
        vlinqInvoiceUpdatedateConfirm.KeyExpression = "Id"
        Return vlinqInvoiceUpdatedateConfirm
    End Function

    Private Sub OnGetQueryableInoviceRadicateDDateConfirm(ByVal sender As Object, ByVal e As GetQueryableEventArgs) Handles vlinqInvoiceUpdatedateConfirm.GetQueryable
        Try
            Dim sessionNew = New Session(XpoDefault.DataLayer)
            Dim State As String = "2"
            Dim tableObjD As XPQuery(Of Glosas_RadicateInvoiceD) = New XPQuery(Of Glosas_RadicateInvoiceD)(sessionNew)
            Dim tableObjc As XPQuery(Of Glosas_RadicateInvoiceC) = New XPQuery(Of Glosas_RadicateInvoiceC)(sessionNew)
            Dim TmpQueryableSource = From T1 In tableObjD
                                     Join T2 In tableObjc On T1.RadicateInvoiceCId.Id Equals T2.Id
                                     Where T1.State = State
                                     Select New With {.id = T1.Id, .RadicatedConsecutive = T2.RadicatedConsecutive, .InvoiceNumber = T1.InvoiceNumber, .NitName = T2.CustomerId.NitName, .DocumentDate = T2.DocumentDate, .State = If(T2.State = "2", "Confirmado", If(T2.State = "1", "Sin Confirmar", "Anulado"))}
            e.QueryableSource = TmpQueryableSource
            e.Tag = tableObjD
        Catch ex As Exception
        End Try
    End Sub

    Private Sub DismissQueryableInvoiceRadicateDDateConfirm(ByVal sender As Object, ByVal e As GetQueryableEventArgs) Handles vlinqInvoiceUpdatedateConfirm.DismissQueryable
        Try
            'Dispose of the DataContext 
            CType(e.Tag, Object).Dispose()
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub
#End Region

#Region "Evaluation Invoice LinqInstantFeedBackSource"

    Private WithEvents vlinqEvaluationInvoice As New LinqInstantFeedbackSource
    Private _criteriaFilter As String = String.Empty

    ''' <summary>
    ''' Obtiene todas las facturas para evaluar del responsable
    ''' </summary>
    ''' <param name="responsibleCode">Código de usuario del responsable</param>
    Public Function ListEvaluationInvoiceByResponsible(ByVal responsibleCode As String) As LinqInstantFeedbackSource
        vlinqEvaluationInvoice.KeyExpression = "Id"
        _criteriaFilter = responsibleCode
        Return vlinqEvaluationInvoice
    End Function
    Private Sub OnGetQueryableEvaluationInovice(ByVal sender As Object, ByVal e As GetQueryableEventArgs) Handles vlinqEvaluationInvoice.GetQueryable
        Try
            Const UNO As String = "1", DOS As String = "2", CUATRO As String = "4", CINCO As String = "5"
            Dim sessionNew = New Session(XpoDefault.DataLayer)
            Dim tableObjC As XPQuery(Of Glosas_GlosaObjectionsReceptionCXpo) = New XPQuery(Of Glosas_GlosaObjectionsReceptionCXpo)(sessionNew)
            Dim tableObjD As XPQuery(Of Glosas_GlosaObjectionsReceptionDXpo) = New XPQuery(Of Glosas_GlosaObjectionsReceptionDXpo)(sessionNew)
            Dim tableCust As XPQuery(Of Common_CustomerXpo) = New XPQuery(Of Common_CustomerXpo)(sessionNew)
            Dim tablePort As XPQuery(Of Glosas_GlosaPortfolioGlosadaXpo) = New XPQuery(Of Glosas_GlosaPortfolioGlosadaXpo)(sessionNew)
            Dim tableMov As XPQuery(Of Glosas_GlosaMovementGlosaXpo) = New XPQuery(Of Glosas_GlosaMovementGlosaXpo)(sessionNew)
            Dim tableResp1 As XPQuery(Of Glosas_ResponsibleXpo) = New XPQuery(Of Glosas_ResponsibleXpo)(sessionNew)
            Dim tableResp2 As XPQuery(Of Glosas_ResponsibleXpo) = New XPQuery(Of Glosas_ResponsibleXpo)(sessionNew)
            Dim TmpQueryableSource = (From objD In tableObjD _
                                     Join objC In tableObjC On objC.Id Equals objD.GlosaObjectionsReceptionCId.Id _
                                     Join cust In tableCust On cust.Id Equals objC.CustomerId.Id _
                                     Join port In tablePort On port.Id Equals objD.PortfolioGlosaId.Id _
                                     Join mov In tableMov On mov.InvoiceNumber Equals objD.InvoiceNumber _
                                     Join resp1 In tableResp1 On resp1.Id Equals mov.ResponsibleId.Id _
                                     Join resp2 In tableResp2 On resp2.Id Equals mov.ResponsibleReiterationId.Id _
                                     Where ((objD.DocumentType = UNO And (port.State = DOS Or port.State = UNO)) Or (objD.DocumentType = DOS And (port.State = CINCO Or port.State = CUATRO))) _
                                     And ((mov.State <> DOS And mov.State <> CUATRO) And (resp1.CodeUser = _criteriaFilter Or resp2.CodeUser = _criteriaFilter)) _
                                     And ((mov.State <> DOS And mov.State <> CUATRO) And (resp1.CodeUser = _criteriaFilter)) _
                                     Select Id = objD.Id, State = objD.State, Name = cust.Name, PatientName = port.PatientName, InvoiceNumber = objD.InvoiceNumber, InvoiceValueEntity = port.InvoiceValueEntity, ValueGlosado = port.ValueGlosado, ValueReiterated = port.ValueReiterated, RadicatedDate = port.RadicatedDate, IngressNumber = port.IngressNumber, CustomerId = cust.Id, DocumentType = objD.DocumentType, Comment = objD.Comment, NitToPersist = cust.Nit, PatientCode = port.PatientCode).Distinct()
            'Dim prueba = TmpQueryableSource.ToList
            e.QueryableSource = TmpQueryableSource
            e.Tag = tableObjD
        Catch ex As Exception
            Debug.WriteLine(ex.Message)
        End Try
    End Sub
    Private Sub DismissQueryableEvaluationInvoice(ByVal sender As Object, ByVal e As GetQueryableEventArgs) Handles vlinqEvaluationInvoice.DismissQueryable
        Try
            'Dispose of the DataContext 
            CType(e.Tag, Object).Dispose()
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub

#End Region

#Region "conciliation LinqInstantFeedBackSource"
    Private WithEvents vlinqConciliation As New LinqInstantFeedbackSource

    ''' <summary>
    ''' Obtiene todos los empleados
    ''' </summary>
    Public Function GetConciliationCList() As LinqInstantFeedbackSource
        vlinqConciliation.KeyExpression = "Id"
        Return vlinqConciliation
    End Function

    Private Sub OnGetQueryableConciliationD(ByVal sender As Object, ByVal e As GetQueryableEventArgs) Handles vlinqConciliation.GetQueryable
        Try
            Dim sessionNew = New Session(XpoDefault.DataLayer)
            Dim tableObjD As XPQuery(Of Glosas_ConciliationD) = New XPQuery(Of Glosas_ConciliationD)(sessionNew)
            Dim tableObjc As XPQuery(Of Glosas_ConciliationC) = New XPQuery(Of Glosas_ConciliationC)(sessionNew)
            Dim TmpQueryableSource = From T1 In tableObjD
                                     Join T2 In tableObjc On T1.ConciliationCId.Id Equals T2.Id
                                     Select New With {.id = T1.Id, .ConciliationConsecutive = T2.ConciliationConsecutive, .InvoiceNumber = T1.InvoiceNumber, .NitName = T2.NitName, .DocumentDate = T2.DocumentDate, .State = If(T2.State = "2", "Confirmado", If(T2.State = "1", "Sin Confirmar", "Anulado"))}
            'Select T1.Id, T2.RadicatedConsecutive, T1.InvoiceNumber, NitName = T2.CustomerId.NitName.Trim, T2.DocumentDate

            ' Dim prueba = TmpQueryableSource.ToList
            e.QueryableSource = TmpQueryableSource
            e.Tag = tableObjD
        Catch ex As Exception
        End Try
    End Sub

    Private Sub DismissQueryableConciliationD(ByVal sender As Object, ByVal e As GetQueryableEventArgs) Handles vlinqConciliation.DismissQueryable
        Try
            'Dispose of the DataContext 
            CType(e.Tag, Object).Dispose()
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub
#End Region

#Region "conciliation invocieListExport Excel PLinqServerModeSource"

    Private _ConcatStrInvoice As List(Of String)
    ''' <summary>
    ''' Obtiene todos los empleados
    ''' </summary>
    Public Function GetInvoiceExportExcel(ByVal LitConcatStrInvoice As List(Of String), ByVal OnlyMovements As Boolean) As PLinqServerModeSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        _ConcatStrInvoice = LitConcatStrInvoice
        Dim P As New PLinqServerModeSource
        If OnlyMovements = True Then
            Dim tview As XPQuery(Of Glosas_ExportGlosaExcel) = New XPQuery(Of Glosas_ExportGlosaExcel)(sessionNew)
            Dim TmpQueryableSource = From T1 In tview
                               Where _ConcatStrInvoice.Contains(T1.idInvoice)
                               Select T1
            P.Source = TmpQueryableSource.ToList()

        Else
            Dim tview As XPQuery(Of Glosas_ExportGlosaExcelAll) = New XPQuery(Of Glosas_ExportGlosaExcelAll)(sessionNew)
            Dim TmpQueryableSource = From T1 In tview
                                   Where _ConcatStrInvoice.Contains(T1.idInvoice)
                                   Select T1
            P.Source = TmpQueryableSource.ToList()
        End If
        Return P

        '_ConcatStrInvoice = LitConcatStrInvoice

        'Dim sessionNew = New Session(XpoDefault.DataLayer)
        'Dim tableDetail As XPQuery(Of Glosas_GlosaInvoiceDetail) = New XPQuery(Of Glosas_GlosaInvoiceDetail)(sessionNew)
        'Dim tableMov As XPQuery(Of GlosaMovementGlosaXpo) = New XPQuery(Of GlosaMovementGlosaXpo)(sessionNew)
        'Dim tableConcept As XPQuery(Of Common_ConceptGlosas) = New XPQuery(Of Common_ConceptGlosas)(sessionNew)
        'Dim tableObjD As XPQuery(Of GlosasObjectionDXpo) = New XPQuery(Of GlosasObjectionDXpo)(sessionNew)
        'Dim tableObjC As XPQuery(Of GlosasObjectionCXpo) = New XPQuery(Of GlosasObjectionCXpo)(sessionNew)
        'Dim tablePortfolio As XPQuery(Of Glosas_GlosaPortfolioGlosadaXpo) = New XPQuery(Of Glosas_GlosaPortfolioGlosadaXpo)(sessionNew)
        'Dim tableConceptEvaluation As XPQuery(Of Common_ConceptGlosas) = New XPQuery(Of Common_ConceptGlosas)(sessionNew)
        'Dim tableDetailqx As XPQuery(Of Glosas_GlosaInvoiceDetailQX) = New XPQuery(Of Glosas_GlosaInvoiceDetailQX)(sessionNew)

        'Dim TmpDocumentype As String = 1
        'Dim TmpQueryableSource = From T1 In tableDetail
        '                         Group Join T2 In tableMov On T2.InvoiceDetailId Equals T1.Id
        '                         Into VarGroup = Group
        '                         From tt1 In VarGroup.DefaultIfEmpty()




        'Where _ConcatStrInvoice.Contains(T6.Id) And T4.DocumentType = TmpDocumentype
        '  Select New With {T1.Id, .CodeMovement = T2.Id, T1.InvoiceNumber, T1.ServiceCode, T1.ServiceName, .ServiceNameQX = "", T1.CostCenterCode, T1.CostCenterName, T2.MainGlosa, T2.CodeGlosa, T3.NameSpecific, T2.RationaleGlosa, T2.ValueGlosado, T2.ValueAcceptedFirstInstance, T2.ValueReiterated, T2.ValueReiterationBalance, T2.ValueAcceptedSecondInstance,
        'T2.ValueAcceptedIPSconciliation, T2.ValueAcceptedEAPBconciliation, T2.ValuePendingConciliation, T5.RadicatedConsecutive, T5.DocumentDate, T2.ValuePayments, T6.InvoiceDate, T6.RadicatedNumber, T6.RadicatedDate, .CodeConceptEvaluation = T7.Code, .NameSpecificEvaluation = T7.NameSpecific, T2.JustificationGlosaText, T6.IngressNumber, T6.IngressDate, T6.PatientCode}

        'Dim TmpQueryableSourceQX = From T1 In tableDetail
        '                         Join t8 In tableDetailqx On T1.Id Equals t8.InvoiceDetailId.Id
        '                         Join T2 In tableMov On t8.Id Equals T2.InvoiceDetailIdQX
        '                         Join T3 In tableConcept On T2.CodeGlosaId Equals T3.Id
        '                         Join T4 In tableObjD On T4.Id Equals T1.ObjectionsReceptionDId.Id
        '                         Join T5 In tableObjC On T5.Id Equals T4.GlosaObjectionsReceptionCId.Id
        '                         Join T6 In tablePortfolio On T6.Id Equals T4.PortfolioGlosaId.Id
        '                         Join T7 In tableConceptEvaluation On T7.Id Equals T2.IdGlosaEvaluation
        'Where _ConcatStrInvoice.Contains(T6.Id) And T4.DocumentType = TmpDocumentype
        '  Select New With {T1.Id, .CodeMovement = T2.Id, T1.InvoiceNumber, T1.ServiceCode, T1.ServiceName, .ServiceNameQX = t8.ServiceName, T1.CostCenterCode, T1.CostCenterName, T2.MainGlosa, T2.CodeGlosa, T3.NameSpecific, T2.RationaleGlosa, T2.ValueGlosado, T2.ValueAcceptedFirstInstance, T2.ValueReiterated, T2.ValueReiterationBalance, T2.ValueAcceptedSecondInstance,
        'T2.ValueAcceptedIPSconciliation, T2.ValueAcceptedEAPBconciliation, T2.ValuePendingConciliation, T5.RadicatedConsecutive, T5.DocumentDate, T2.ValuePayments, T6.InvoiceDate, T6.RadicatedNumber, T6.RadicatedDate, .CodeConceptEvaluation = T7.Code, .NameSpecificEvaluation = T7.NameSpecific, T2.JustificationGlosaText, T6.IngressNumber, T6.IngressDate, T6.PatientCode}

        '  Dim a = TmpQueryableSource.ToList()
        ' Dim b = TmpQueryableSourceQX.ToList()
        'a.AddRange(b.ToList())

        ' Dim P As New PLinqServerModeSource
        ' P.Source = a ' TmpQueryableSource.ToList()
        ' Return P
    End Function

#End Region


#Region "ReceptionObjection invocieListExport Excel para Glosa PLinqServerModeSource"

    Private _ConcatStrInvoiceGlosa As List(Of String)
    ''' <summary>
    ''' Obtiene factura a exportar
    ''' </summary>
    Public Function ListGetInvoiceExportExcelGlosa(ByVal LitConcatStrInvoice As List(Of String)) As PLinqServerModeSource
        _ConcatStrInvoiceGlosa = LitConcatStrInvoice

        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim tableDetail As XPQuery(Of Glosas_GlosaInvoiceDetail) = New XPQuery(Of Glosas_GlosaInvoiceDetail)(sessionNew)
        Dim tableDetailQX As XPQuery(Of Glosas_GlosaInvoiceDetailQX) = New XPQuery(Of Glosas_GlosaInvoiceDetailQX)(sessionNew)

        'Dim TmpQueryableSource = (From T1 In tableDetail
        '                         Join T2 In tableDetailQX On T1.Id Equals T2.InvoiceDetailId.Id
        'Where _ConcatStrInvoiceGlosa.Contains(T1.InvoiceNumber) And T1.Glosas_GlosaInvoiceDetailQX.Count = 0 And T1.InvoicedValue > 0
        '  Select New With {T1.InvoiceNumber, T1.Id, .IdQx = 0, T1.CostCenterCode, T1.ServiceCode, T1.ServiceName, T1.ValueServiceManual, T1.UnitValue, T1.Ammount, T1.InvoicedValue}).Union(From T2 In tableDetailQX
        '                         Join T1 In tableDetail On T1.Id Equals T2.InvoiceDetailId.Id
        'Where _ConcatStrInvoiceGlosa.Contains(T1.InvoiceNumber) And T2.InvoicedValue > 0
        '  Select New With {T1.InvoiceNumber, T1.Id, .IdQx = T2.Id, T2.CostCenterCode, T2.ServiceCode, T2.ServiceName, T2.ValueServiceManual, T2.UnitValue, T1.Ammount, T2.InvoicedValue})

        Dim TmpQueryableSource = (From T1 In tableDetail
                                   Where _ConcatStrInvoiceGlosa.Contains(T1.InvoiceNumber) And T1.Glosas_GlosaInvoiceDetailQX.Count = 0 And T1.InvoicedValue > 0
         Select New With {T1.InvoiceNumber, T1.Id, .IdQx = 0, T1.CostCenterCode, T1.ServiceCode, T1.ServiceName, T1.ValueServiceManual, T1.UnitValue, T1.Ammount, T1.InvoicedValue}).Union(From T2 In tableDetailQX
                                Join T1 In tableDetail On T1.Id Equals T2.InvoiceDetailId.Id
       Where _ConcatStrInvoiceGlosa.Contains(T1.InvoiceNumber) And T2.InvoicedValue > 0
         Select New With {T1.InvoiceNumber, T1.Id, .IdQx = T2.Id, T2.CostCenterCode, T2.ServiceCode, T2.ServiceName, T2.ValueServiceManual, T2.UnitValue, T1.Ammount, T2.InvoicedValue})

        Dim b As New PLinqServerModeSource
        b.Source = TmpQueryableSource.ToList()
        Return b
    End Function

#End Region


#Region "Listar ObjectionReceptionD con PLinqServerModeSource"

    ''' <summary>
    ''' Obtiene todos los empleados
    ''' </summary>
    Public Function ListGlosasObjectionD(ByVal IdReceptionC As String) As PLinqServerModeSource

        Dim sessionNew = New Session(XpoDefault.DataLayer)

        Dim tablePorfolio As XPQuery(Of Glosas_GlosaPortfolioGlosadaXpo) = New XPQuery(Of Glosas_GlosaPortfolioGlosadaXpo)(sessionNew)
        Dim tableObjD As XPQuery(Of GlosasObjectionDXpo) = New XPQuery(Of GlosasObjectionDXpo)(sessionNew)
        Dim tableObjC As XPQuery(Of GlosasObjectionCXpo) = New XPQuery(Of GlosasObjectionCXpo)(sessionNew)
        Dim tableCustomer As XPQuery(Of GlosasCustomerXpo) = New XPQuery(Of GlosasCustomerXpo)(sessionNew)

        Dim TmpDocumentype As String = 1


        Const _DocumentTypeGlosa As String = "1"
        Const _DocumentTypeReiteration As String = "2"

        Dim TmpQueryableSource = From T1 In tableObjD
                                 Join T2 In tablePorfolio On T2.Id Equals T1.PortfolioGlosaId.Id
                                 Join t3 In tableObjC On t3.Id Equals T1.GlosaObjectionsReceptionCId.Id
                                 Join t4 In tableCustomer On t4.Id Equals t3.CustomerId.Id
                                Where t3.Id = IdReceptionC And (((T2.State = 2 Or T2.State = 3) And T1.DocumentType = _DocumentTypeGlosa) Or ((T2.State = 5 Or T2.State = 6) And T1.DocumentType = _DocumentTypeReiteration))
          Select New With {.Id = T1.Id, .State = T1.State.ToString(), .DocumentType = T1.DocumentType.ToString(), .StatePortfolio = T2.State, .IngressNumber = T2.IngressNumber, .CustomerId = t4.Id, .InvoiceNumber = T1.InvoiceNumber, .ValueGlosado = T2.ValueGlosado, .ValueReiterated = T2.ValueReiterated, .PatientName = T2.PatientName, .InvoiceValueEntity = T2.InvoiceValueEntity,
                           .StateRecord = If(T2.State = "3" Or T2.State = "6", True, False), .InvoiceValuePacient = T2.InvoiceValuePacient}
        Dim b As New PLinqServerModeSource
        b.Source = TmpQueryableSource.ToList()
        Return b
    End Function

#End Region

#Region "PartialPayments LinqInstantFeedBackSource"
    Private WithEvents vlinqInvoicePartialPayments As New LinqInstantFeedbackSource

    ''' <summary>
    ''' Obtiene todos los empleados
    ''' </summary>
    Public Function ListInvoicePartialPayments() As LinqInstantFeedbackSource
        vlinqInvoicePartialPayments.KeyExpression = "Id"
        Return vlinqInvoicePartialPayments
    End Function

    Private Sub OnGetQueryableInovicePartialPayments(ByVal sender As Object, ByVal e As GetQueryableEventArgs) Handles vlinqInvoicePartialPayments.GetQueryable
        Try
            Dim sessionNew = New Session(XpoDefault.DataLayer)
            Dim tableObjD As XPQuery(Of Glosas_PartialPaymentsDXpo) = New XPQuery(Of Glosas_PartialPaymentsDXpo)(sessionNew)
            Dim tableObjc As XPQuery(Of Glosas_PartialPaymentsCXpo) = New XPQuery(Of Glosas_PartialPaymentsCXpo)(sessionNew)
            Dim TmpQueryableSource = From T1 In tableObjD
                                     Join T2 In tableObjc On T1.PartialPaymentsCId.Id Equals T2.Id
                                     Select New With {.id = T1.Id, .RadicatedConsecutive = T2.RadicatedConsecutive, .InvoiceNumber = T1.InvoiceNumber, .NitName = T2.CustomerId.NitName, .DocumentDate = T2.DocumentDate, .State = If(T2.State = "2", "Confirmado", If(T2.State = "1", "Sin Confirmar", "Anulado"))}
            e.QueryableSource = TmpQueryableSource
            e.Tag = tableObjD
        Catch ex As Exception
        End Try
    End Sub

    Private Sub DismissQueryableInvoicePartialPayments(ByVal sender As Object, ByVal e As GetQueryableEventArgs) Handles vlinqInvoicePartialPayments.DismissQueryable
        Try
            'Dispose of the DataContext 
            CType(e.Tag, Object).Dispose()
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub
#End Region

#End Region

End Class
