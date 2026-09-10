'***********************************************************************
' Assembly         : Infrastructure.Data.Xpo.MedicalFeesRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 15/12/2014
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

#End Region

''' <summary>
''' Clase que expone los servicios de los repositorios
''' </summary>
Public Class MedicalFeesServiceXpo
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
    ''' <summary>
    ''' id del grupo de servicio
    ''' </summary>
    ''' <remarks></remarks>
    Private _idCareGroup As Integer
    ''' <summary>
    ''' tipo de servicio ips
    ''' </summary>
    ''' <remarks></remarks>
    Private _serviceManual As Integer
#End Region

#Region "Builders"

    ''' <summary>
    ''' Incializa una nueva instancia de la clase
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

#Region "Public Methods"

    ''' <summary>
    ''' Lista todos los contratos profesionales de la salud 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListMedicalFeesContract() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(MedicalFeesContractXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;ContractName;ContractNumber;ContractType;ContractTypeName;Status;StatusName", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Obtiene un contrato por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMedicalFeesContractById(id As Integer) As XPCollection
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Id=" & id & "")
        Dim collect As XPCollection = New XPCollection(sessionNew, GetType(MedicalFeesContractXpo), criteria)
        Return collect
    End Function

    ''' <summary>
    ''' Obtiene un contrato por id
    ''' </summary>
    ''' <param name="MedicalFeesLiquidationId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListViewMedicalFeesLiquidationDetailByMedicalFeesLiquidationId(MedicalFeesLiquidationId As Integer) As XPCollection
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("MedicalFeesLiquidacionId=" & MedicalFeesLiquidationId & "")
        Dim collect As XPCollection = New XPCollection(sessionNew, GetType(ViewListMedicalFeesLiquidationDetailXpo), criteria)
        Return collect
    End Function

    ''' <summary>
    ''' Obtiene los medicos que estan asociados al contrato enviado
    ''' </summary>
    ''' <param name="medicalFeesContractId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListHealthProfessionalContractByMedicalFeesContractId(medicalFeesContractId As Integer) As XPCollection
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("MedicalFeesContractId.Id='" & medicalFeesContractId & "'")
        Dim collect As XPCollection = New XPCollection(sessionNew, GetType(HealthProfessionalContractXpo), criteria)
        Return collect
    End Function

    ''' <summary>
    ''' Obtiene un contrato por id
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListHealthProfessionalContractByHealthProfessionalCode(code As String) As XPCollection
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("HealthProfessionalCode='" & code & "'")
        Dim collect As XPCollection = New XPCollection(sessionNew, GetType(HealthProfessionalContractXpo), criteria)
        Return collect
    End Function

    ''' <summary>
    ''' Lista todas las liquidaciones de honorarios medicos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListMedicalFeesLiquidation() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(MedicalFeesLiquidationXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;MedicalFeesContractId.CodeName;InitialDate;EndDate;Status;StatusName;LiquidationTypeName;HealthProfessionalCode", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista los contratos profesionales de la salud por estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListMedicalFeesContractByStatus(status As Boolean) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & "")
        classEntity = sessionNew.GetClassInfo(GetType(MedicalFeesContractXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;ContractName;ContractNumber;ContractType;ContractTypeName;Status;StatusName;CodeName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista los contratos profesionales de la salud por estado y por tipo de contrato
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListMedicalFeesContractByStatusAndContractType(status As Boolean, contractType As Integer) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & " And ContractType=" & contractType & "")
        classEntity = sessionNew.GetClassInfo(GetType(MedicalFeesContractXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;ContractName;ContractNumber;ContractType;ContractTypeName;Status;StatusName;CodeName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista las causaciones por contrato
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListMedicalFeesCausationByMedicalFeesContractId(medicalFeesContractId As Integer) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("MedicalFeesContractId=" & medicalFeesContractId & "")
        classEntity = sessionNew.GetClassInfo(GetType(MedicalFeesCausationXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;AdmissionNumber;PatientCode;ThirdPartyId.NitName;MedicalFeesContractId.CodeName;ServiceOrderId.Code;AmountPayable;MedicalFeesContractValue;InvoiceQuantity;TotalAmountPayable;AdmissionNumberPatientCode", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista las causaciones de pagos para liquidacion de honorarios
    ''' Este metodo llama la vista creada
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListCausationByMedicalFeesContractIdViewXpo(listMedicalFeesContractId As List(Of Integer), medicalFeesContractId As Integer?, initialDate As DateTime, endDate As DateTime, healthProfessionalCode As String) As XPCollection
        Dim operatorIn As String = String.Empty
        Dim counter As Integer = 0
        If listMedicalFeesContractId IsNot Nothing AndAlso listMedicalFeesContractId.Count > 0 Then
            For Each item As String In listMedicalFeesContractId
                If counter <> 0 Then
                    operatorIn += ","
                Else
                    counter = 1
                End If
                operatorIn += item
            Next
        End If

        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator
        If healthProfessionalCode <> String.Empty Then
            criteria = CriteriaOperator.And(
            New BinaryOperator("CausationDate", initialDate, BinaryOperatorType.GreaterOrEqual),
            New BinaryOperator("CausationDate", endDate, BinaryOperatorType.LessOrEqual))
            criteria = CriteriaOperator.Parse(criteria.ToString() + " AND [MedicalFeesContractId] in (" & operatorIn & ") AND Trim(HealthProfessionalCode)='" & healthProfessionalCode.Trim & "' And (Status=1 Or Status=2)")
        Else
            criteria = CriteriaOperator.And(
            New BinaryOperator("CausationDate", initialDate, BinaryOperatorType.GreaterOrEqual),
            New BinaryOperator("CausationDate", endDate, BinaryOperatorType.LessOrEqual))
            criteria = CriteriaOperator.Parse(criteria.ToString() + " AND MedicalFeesContractId=" & medicalFeesContractId & " And (Status=1 Or Status=2)")
        End If
        Dim collect As XPCollection = New XPCollection(sessionNew, GetType(ViewListPaymentsMedicalFeesLiquidationXpo), criteria)
        Return collect
    End Function

    ''' <summary>
    ''' Lista las causaciones por contrato 0 por medico para pagos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListCausationByMedicalFeesContractId(listMedicalFeesContractId As List(Of Integer), medicalFeesContractId As Integer?, initialDate As DateTime, endDate As DateTime, healthProfessionalCode As String) As XPCollection
        Dim operatorIn As String = String.Empty
        Dim counter As Integer = 0
        If listMedicalFeesContractId IsNot Nothing AndAlso listMedicalFeesContractId.Count > 0 Then
            For Each item As String In listMedicalFeesContractId
                If counter <> 0 Then
                    operatorIn += ","
                Else
                    counter = 1
                End If
                operatorIn += item
            Next
        End If

        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator
        If healthProfessionalCode <> String.Empty Then
            criteria = CriteriaOperator.And(
            New BinaryOperator("CausationDate", initialDate, BinaryOperatorType.GreaterOrEqual),
            New BinaryOperator("CausationDate", endDate, BinaryOperatorType.LessOrEqual))
            criteria = CriteriaOperator.Parse(criteria.ToString() + " AND [MedicalFeesContractId] in (" & operatorIn & ") AND Trim(HealthProfessionalCode)='" & healthProfessionalCode.Trim & "' And (Status=1 Or Status=2)")
        Else
            criteria = CriteriaOperator.And(
            New BinaryOperator("CausationDate", initialDate, BinaryOperatorType.GreaterOrEqual),
            New BinaryOperator("CausationDate", endDate, BinaryOperatorType.LessOrEqual))
            criteria = CriteriaOperator.Parse(criteria.ToString() + " AND MedicalFeesContractId=" & medicalFeesContractId & " And (Status=1 Or Status=2)")
        End If
        Dim collect As XPCollection = New XPCollection(sessionNew, GetType(MedicalFeesCausationXpo), criteria)
        Return collect
    End Function

    ''' <summary>
    ''' Lista las causaciones por contrato para deducciones
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListCausationByMedicalFeesContractIdForDeductions(listMedicalFeesContractId As List(Of Integer), medicalFeesContractId As Integer?, healthProfessionalCode As String) As XPCollection
        Dim operatorIn As String = String.Empty
        Dim counter As Integer = 0
        If listMedicalFeesContractId IsNot Nothing AndAlso listMedicalFeesContractId.Count > 0 Then
            For Each item As String In listMedicalFeesContractId
                If counter <> 0 Then
                    operatorIn += ","
                Else
                    counter = 1
                End If
                operatorIn += item
            Next
        End If

        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator
        If healthProfessionalCode = String.Empty Then
            criteria = CriteriaOperator.Parse("MedicalFeesContractId=" & medicalFeesContractId & " And Status=3 And InvoiceReversal=True And ReassessmentForReversal=False")
        Else
            criteria = CriteriaOperator.Parse("[MedicalFeesContractId] in (" & operatorIn & ") And Status=3 And InvoiceReversal=True And ReassessmentForReversal=False And Trim(HealthProfessionalCode)='" & healthProfessionalCode & "'")
        End If
        Dim collect As XPCollection = New XPCollection(sessionNew, GetType(MedicalFeesCausationXpo), criteria)
        Return collect
    End Function

    ''' <summary>
    ''' Lista las causaciones por contrato para glosas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListCausationByMedicalFeesContractIdForGlosas(listMedicalFeesContractId As List(Of Integer), medicalFeesContractId As Integer?, healthProfessionalCode As String) As XPCollection
        Dim operatorIn As String = String.Empty
        Dim counter As Integer = 0
        If listMedicalFeesContractId IsNot Nothing AndAlso listMedicalFeesContractId.Count > 0 Then
            For Each item As String In listMedicalFeesContractId
                If counter <> 0 Then
                    operatorIn += ","
                Else
                    counter = 1
                End If
                operatorIn += item
            Next
        End If

        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator
        If healthProfessionalCode = String.Empty Then
            criteria = CriteriaOperator.Parse("MedicalFeesContractId=" & medicalFeesContractId & " And Status=3 And ObjectionAccepted=True And ReassessmentForObjection=False")
        Else
            criteria = CriteriaOperator.Parse("[MedicalFeesContractId] in (" & operatorIn & ") And Status=3 And ObjectionAccepted=True And ReassessmentForObjection=False And Trim(HealthProfessionalCode)='" & healthProfessionalCode & "'")
        End If
        Dim collect As XPCollection = New XPCollection(sessionNew, GetType(MedicalFeesCausationXpo), criteria)
        Return collect
    End Function

    '' <summary>
    ''' lista de causaciones con nombre del paciente
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListMedicalFeesLiquidationDetailReport(MedicalFeesLiquidacionId As Integer) As XPCollection(Of ViewMedicalFeesReport)
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Id =" & MedicalFeesLiquidacionId & "")
        Dim collect As XPCollection(Of ViewMedicalFeesReport) = New XPCollection(Of ViewMedicalFeesReport)(sessionNew, criteria)
        'collect.Sorting.Add(New SortProperty("------", DevExpress.Xpo.DB.SortingDirection.Descending))
        Return collect
    End Function

#End Region

End Class
