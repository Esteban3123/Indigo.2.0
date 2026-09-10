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
Public Class MedicalFeesServiceXpoEx
    Inherits XpoBaseService
    Implements IDisposable

#Region "Public Methods"

    ''' <summary>
    ''' Obtiene un contrato por id
    ''' </summary>
    ''' <param name="MedicalFeesLiquidationId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListViewMedicalFeesLiquidationDetailByMedicalFeesLiquidationId(MedicalFeesLiquidationId As Integer) As XPCollection
        Dim session As New IndigoXPOSession(Of ViewListMedicalFeesLiquidationDetailXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("MedicalFeesLiquidacionId=" & MedicalFeesLiquidationId & "")
        Dim collect As XPCollection = New XPCollection(session, GetType(ViewListMedicalFeesLiquidationDetailXpo), criteria)
        Return collect
        'End Using
    End Function

    ''' <summary>
    ''' Obtiene los detalles de pago un contrato por id
    ''' </summary>
    ''' <param name="MedicalFeesLiquidationId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListViewMedicalFeesLiquidationPayByMedicalFeesLiquidationId(MedicalFeesLiquidationId As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ViewListMedicalFeesLiquidationDetailXpo)()
        Dim classEntity = session.GetClassInfo(GetType(ViewListMedicalFeesLiquidationDetailXpo))
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("MedicalFeesLiquidacionId=" & MedicalFeesLiquidationId & " And LiquidationType = 1")
        Return New XPInstantFeedbackSource(classEntity,
                                           "ServiceDate;MedicalFeesCausationId;MedicalFeesLiquidacionId;LiquidationType;CausationDate;StatusCausation;MedicalFeesContractValue;InvoiceReversal;TotalAmountPayable;InvoiceQuantity;AmountPayable;IPSServiceName;ServiceOrderCode;ThirdPartyDescription;PatientCode;AdmissionNumber;Id",
                                           criteria)
    End Function

    '' <summary>
    ''' lista de causaciones con nombre del paciente vista
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListMedicalFeesLiquidationDetailReport(MedicalFeesLiquidacionId As Integer) As XPCollection(Of ViewMedicalFeesReport)
        Dim session As New IndigoXPOSession(Of ViewMedicalFeesReport)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Id =" & MedicalFeesLiquidacionId & "")
        Dim collect As XPCollection(Of ViewMedicalFeesReport) = New XPCollection(Of ViewMedicalFeesReport)(session, criteria)
        'collect.Sorting.Add(New SortProperty("------", DevExpress.Xpo.DB.SortingDirection.Descending))
        Return collect
        'End Using
    End Function

    ''' <summary>
    ''' Lista todos los contratos profesionales de la salud 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListMedicalFeesContract() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of MedicalFeesContractXpo)()
        Dim classEntity = session.GetClassInfo(GetType(MedicalFeesContractXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;ContractName;ContractNumber;ContractType;ContractTypeName;Status;StatusName", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Obtiene un contrato por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMedicalFeesContractById(id As Integer) As XPCollection
        Dim session As New IndigoXPOSession(Of MedicalFeesContractXpo)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Id=" & id & "")
        Dim collect As XPCollection = New XPCollection(session, GetType(MedicalFeesContractXpo), criteria)
        Return collect
        'End Using
    End Function

    ''' <summary>
    ''' Obtiene los medicos que estan asociados al contrato enviado
    ''' </summary>
    ''' <param name="medicalFeesContractId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListHealthProfessionalContractByMedicalFeesContractId(medicalFeesContractId As Integer) As XPCollection
        Dim session As New IndigoXPOSession(Of HealthProfessionalContractXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("MedicalFeesContractId.Id='" & medicalFeesContractId & "'")
        Dim collect As XPCollection = New XPCollection(session, GetType(HealthProfessionalContractXpo), criteria)
        Return collect
        'End Using
    End Function

    ''' <summary>
    ''' Obtiene un contrato por id
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListHealthProfessionalContractByHealthProfessionalCode(code As String) As XPCollection
        Dim session As New IndigoXPOSession(Of HealthProfessionalContractXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("HealthProfessionalCode='" & code & "'")
        Dim collect As XPCollection = New XPCollection(session, GetType(HealthProfessionalContractXpo), criteria)
        Return collect
        'End Using
    End Function

    ''' <summary>
    ''' Lista todas las liquidaciones de honorarios medicos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListMedicalFeesLiquidation() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of MedicalFeesLiquidationXpo)()
        Dim classEntity = session.GetClassInfo(GetType(MedicalFeesLiquidationXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;MedicalFeesContractId.CodeName;InitialDate;EndDate;Status;StatusName;LiquidationTypeName;HealthProfessionalCode;LiquidationType", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista los contratos profesionales de la salud por estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListMedicalFeesContractByStatus(status As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of MedicalFeesContractXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & "")
        Dim classEntity = session.GetClassInfo(GetType(MedicalFeesContractXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;ContractName;ContractNumber;ContractType;ContractTypeName;Status;StatusName;CodeName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista los contratos profesionales de la salud por estado y por tipo de contrato
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListMedicalFeesContractByStatusAndContractType(status As Boolean, contractType As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of MedicalFeesContractXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & " And ContractType=" & contractType & "")
        Dim classEntity = session.GetClassInfo(GetType(MedicalFeesContractXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;ContractName;ContractNumber;ContractType;ContractTypeName;Status;StatusName;CodeName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista las causaciones por contrato
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListMedicalFeesCausationByMedicalFeesContractId(medicalFeesContractId As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of MedicalFeesCausationXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("MedicalFeesContractId=" & medicalFeesContractId & "")
        Dim classEntity = session.GetClassInfo(GetType(MedicalFeesCausationXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;AdmissionNumber;PatientCode;ThirdPartyId.NitName;MedicalFeesContractId.CodeName;ServiceOrderId.Code;AmountPayable;MedicalFeesContractValue;InvoiceQuantity;TotalAmountPayable;AdmissionNumberPatientCode", criteria)
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

        Dim session As New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator
        If healthProfessionalCode <> String.Empty Then
            criteria = CriteriaOperator.And(
        New BinaryOperator("CausationDate", initialDate.ToString("yyyyMMdd HH:mm:ss"), BinaryOperatorType.GreaterOrEqual),
        New BinaryOperator("CausationDate", endDate.ToString("yyyyMMdd HH:mm:ss"), BinaryOperatorType.LessOrEqual))
            criteria = CriteriaOperator.Parse(criteria.ToString() + " AND [MedicalFeesContractId] in (" & operatorIn & ") AND Trim(HealthProfessionalCode)='" & healthProfessionalCode.Trim & "' And (Status=1 Or Status=2)")
        Else
            criteria = CriteriaOperator.And(
        New BinaryOperator("CausationDate", initialDate.ToString("yyyyMMdd HH:mm:ss"), BinaryOperatorType.GreaterOrEqual),
        New BinaryOperator("CausationDate", endDate.ToString("yyyyMMdd HH:mm:ss"), BinaryOperatorType.LessOrEqual))
            criteria = CriteriaOperator.Parse(criteria.ToString() + " AND MedicalFeesContractId=" & medicalFeesContractId & " And (Status=1 Or Status=2)")
        End If
        Dim collect As XPCollection = New XPCollection(session, GetType(ViewListPaymentsMedicalFeesLiquidationXpo), criteria)
        Return collect
        'End Using
    End Function

    ''' <summary>
    ''' Lista las causaciones de pagos para liquidacion de honorarios
    ''' Este metodo llama la vista creada
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListCausationByMedicalFeesContractIdViewXpoXpInstantFeedBackSource(listMedicalFeesContractId As List(Of Integer), medicalFeesContractId As Integer?, initialDate As DateTime, endDate As DateTime, healthProfessionalCode As String) As XPInstantFeedbackSource
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

        Dim session As New Session(XpoDefault.DataLayer)
        Dim classEntity = session.GetClassInfo(GetType(ViewListPaymentsMedicalFeesLiquidationXpo))
        Dim criteria As CriteriaOperator
        If healthProfessionalCode <> String.Empty Then
            criteria = CriteriaOperator.And(
        New BinaryOperator("CausationDate", initialDate.ToString("yyyyMMdd HH:mm:ss"), BinaryOperatorType.GreaterOrEqual),
        New BinaryOperator("CausationDate", endDate.ToString("yyyyMMdd HH:mm:ss"), BinaryOperatorType.LessOrEqual))
            criteria = CriteriaOperator.Parse(criteria.ToString() + " AND [MedicalFeesContractId] in (" & operatorIn & ") AND Trim(HealthProfessionalCode)='" & healthProfessionalCode.Trim & "' And (Status=1 Or Status=2)")
        Else
            criteria = CriteriaOperator.And(
        New BinaryOperator("CausationDate", initialDate.ToString("yyyyMMdd HH:mm:ss"), BinaryOperatorType.GreaterOrEqual),
        New BinaryOperator("CausationDate", endDate.ToString("yyyyMMdd HH:mm:ss"), BinaryOperatorType.LessOrEqual))
            criteria = CriteriaOperator.Parse(criteria.ToString() + " AND MedicalFeesContractId=" & medicalFeesContractId & " And (Status=1 Or Status=2)")
        End If

        Return New XPInstantFeedbackSource(classEntity, Nothing, criteria)
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

        Dim session As New Session(XpoDefault.DataLayer)
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
        Dim collect As XPCollection = New XPCollection(session, GetType(MedicalFeesCausationXpo), criteria)
        Return collect
        'End Using
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

        Dim session As New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator
        If healthProfessionalCode = String.Empty Then
            criteria = CriteriaOperator.Parse("MedicalFeesContractId=" & medicalFeesContractId & " And Status=3 And InvoiceReversal=True And ReassessmentForReversal=False")
        Else
            criteria = CriteriaOperator.Parse("[MedicalFeesContractId] in (" & operatorIn & ") And Status=3 And InvoiceReversal=True And ReassessmentForReversal=False And Trim(HealthProfessionalCode)='" & healthProfessionalCode & "'")
        End If
        Dim collect As XPCollection = New XPCollection(session, GetType(MedicalFeesCausationXpo), criteria)
        Return collect
        'End Using
    End Function

    ''' <summary>
    ''' lista los Conceptos de Glosas Honorarios Médicos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListGlosaMedicalFeesConcepts() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of GlosaMedicalFeesConceptsXpo)()
        Dim classEntity = session.GetClassInfo(GetType(GlosaMedicalFeesConceptsXpo))
        Return New XPInstantFeedbackSource(classEntity, "Id;Code;Name;ThirdPartyId.Name;StatusName;CodeName", Nothing)
    End Function

    ''' <summary>
    ''' lista los Conceptos de Glosas Honorarios Médicos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListGlosaMedicalFees() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of GlosaMedicalFeesXpo)()
        Dim classEntity = session.GetClassInfo(GetType(GlosaMedicalFeesXpo))
        Return New XPInstantFeedbackSource(classEntity, "Id;Code;SupplierId.CodeName;DocumentDate;StatusName;PendingValue;GlossedValue", Nothing)
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

        Dim session As New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator
        If healthProfessionalCode = String.Empty Then
            criteria = CriteriaOperator.Parse("MedicalFeesContractId=" & medicalFeesContractId & " And Status=3 And ObjectionAccepted=True And ReassessmentForObjection=False")
        Else
            criteria = CriteriaOperator.Parse("[MedicalFeesContractId] in (" & operatorIn & ") And Status=3 And ObjectionAccepted=True And ReassessmentForObjection=False And Trim(HealthProfessionalCode)='" & healthProfessionalCode & "'")
        End If
        Dim collect As XPCollection = New XPCollection(session, GetType(MedicalFeesCausationXpo), criteria)
        Return collect
        'End Using
    End Function

    ''' <summary>
    ''' Obtiene las causaciones no reconocidas (sin reconocimiento de costo)
    ''' Consulta la vista ViewListCausationwithoutRecognition
    ''' </summary>
    ''' <param name="operativeUnitId">ID de la unidad operativa</param>
    ''' <returns>XPInstantFeedbackSource con las causaciones no reconocidas (solo visualización)</returns>
    ''' <remarks>
    ''' Esta función usa XPInstantFeedbackSource para eficiencia en la visualización.
    ''' El SP de reconocimiento consultará internamente la vista, no necesita pasar todos los registros.
    ''' </remarks>
    Public Function GetCausationWithoutRecognition(operativeUnitId As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ViewListCausationwithoutRecognition)()
        Dim classEntity = session.GetClassInfo(GetType(ViewListCausationwithoutRecognition))
        Dim criteria As CriteriaOperator = Nothing
        
        ' Aplicar filtro por unidad operativa si se proporciona
        If operativeUnitId > 0 Then
            criteria = CriteriaOperator.Parse("OperatingUnitId=" & operativeUnitId)
        End If
        
        ' Retornar XPInstantFeedbackSource con los campos principales (incluye SupplierId para agrupación)
        Return New XPInstantFeedbackSource(classEntity,
                                           "Id;ProfessionalCodeName;ContractCodeName;ContractTypeName;SupplierName;AdmissionNumber;PatientCodeName;CareGroupCodeName;HealthAdministratorCodeName;CupsEntityCodeName;InvoiceQuantity;ServiceDate;FunctionalUnitCodeName;TotalSalesPrice;CausationDate;CausationValue;ServiceStatus;InvoiceNumber;OperativeUnitId;SupplierId",
                                           criteria)
    End Function

    ''' <summary>
    ''' Obtiene las causaciones reconocidas para reversar
    ''' Consulta la vista ViewListCausationwithRecognition
    ''' </summary>
    ''' <param name="operativeUnitId">ID de la unidad operativa</param>
    ''' <returns>XPInstantFeedbackSource con las causaciones reconocidas (para reversión)</returns>
    ''' <remarks>
    ''' Esta vista incluye información completa del reconocimiento:
    ''' - CausationRecognitionId: Para identificar el reconocimiento a reversar
    ''' - RecognitionDate: Fecha del reconocimiento
    ''' - TotalRecognition: Valor total del reconocimiento por proveedor
    ''' - JournalVoucherConsecutive: Consecutivo del comprobante contable
    ''' - SupplierId: Para agrupar por proveedor en la reversión
    ''' </remarks>
    Public Function GetCausationWithRecognition(operativeUnitId As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ViewListCausationwithRecognition)()
        Dim classEntity = session.GetClassInfo(GetType(ViewListCausationwithRecognition))
        Dim criteria As CriteriaOperator = Nothing

        ' Aplicar filtro por unidad operativa si se proporciona
        If operativeUnitId > 0 Then
            criteria = CriteriaOperator.Parse("OperatingUnitId=" & operativeUnitId)
        End If

        ' Retornar XPInstantFeedbackSource con los campos principales
        ' Incluye campos adicionales del reconocimiento para la reversión
        Return New XPInstantFeedbackSource(classEntity,
                                           "CausationId;CausationRecognitionId;RecognitionDate;TotalRecognition;JournalVoucherConsecutive;RecognitionState;ProfessionalCodeName;ContractCodeName;ContractTypeName;SupplierName;SupplierId;AdmissionNumber;PatientCodeName;CareGroupCodeName;HealthAdministratorCodeName;CupsEntityCodeName;InvoiceQuantity;ServiceDate;FunctionalUnitCodeName;TotalSalesPrice;CausationDate;CausationValue;TotalAmountPayable;ServiceStatus;InvoiceNumber;OperatingUnitId;RecognitionUser",
                                           criteria)
    End Function

    ''' <summary>
    ''' Obtiene las causaciones pendientes por causar (con errores de configuración)
    ''' Consulta la vista ViewListCausationPending basada en la tabla MedicalFees.CausationPending
    ''' </summary>
    ''' <param name="operativeUnitId">ID de la unidad operativa</param>
    ''' <returns>XPInstantFeedbackSource con las causaciones pendientes (con errores)</returns>
    ''' <remarks>
    ''' Esta vista se basa en la tabla CausationPending que almacena causaciones con errores
    ''' La columna Data contiene un JSON que se deserializa para obtener:
    '''    - ServiceOrderDetailId: Para hacer JOINs con otras tablas
    '''    - InvoicedQuantity: Cantidad del servicio facturado
    ''' Incluye columna ErrorMessage con la descripción del error de configuración
    ''' Esta es una vista de solo consulta/información, no se pueden procesar estas causaciones
    '''    hasta que se corrijan los errores de configuración en los módulos correspondientes
    ''' </remarks>
    Public Function GetCausationPending(operativeUnitId As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ViewListCausationPending)()
        Dim classEntity = session.GetClassInfo(GetType(ViewListCausationPending))
        Dim criteria As CriteriaOperator = Nothing

        ' Aplicar filtro por unidad operativa si se proporciona
        If operativeUnitId > 0 Then
            criteria = CriteriaOperator.Parse("OperatingUnitId=" & operativeUnitId)
        End If

        ' Retornar XPInstantFeedbackSource con todos los campos principales
        ' Incluye ErrorMessage que describe el error de configuración
        Return New XPInstantFeedbackSource(classEntity,
                                           "Id;ServiceOrderDetailId;ProfessionalCodeName;ContractCodeName;ContractTypeName;SupplierName;SupplierId;AdmissionNumber;PatientCodeName;CareGroupCodeName;HealthAdministratorCodeName;CupsEntityCodeName;InvoiceQuantity;ServiceDate;FunctionalUnitCodeName;TotalSalesPrice;CausationDate;CausationValue;ServiceStatus;InvoiceNumber;ErrorMessage;OperatingUnitId",
                                           criteria)
    End Function

#End Region

#Region "Informes"

    ''' <summary>
    ''' Función para obtener una lista de datos de una entidad XPO
    ''' </summary>
    ''' <typeparam name="T">Objeto XPO</typeparam>
    ''' <param name="Fun">Objeto tipo Función opcional para filtrar los datos necesarios</param>
    ''' <returns>Lista tipo T</returns>
    Public Function GetCollection(Of T)(Optional Fun As Func(Of T, Boolean) = Nothing, Optional criteria As String = Nothing) As List(Of T)
        Dim result = Me.LoadCollection(Of T)(XpoDefault.DataLayer, Fun, criteria)
        result.Sort()
        Return result
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
