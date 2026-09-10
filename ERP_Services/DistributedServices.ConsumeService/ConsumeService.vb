Imports Newtonsoft.Json.Linq
Imports System.Text
Imports System.Net
Imports Domain.Base.Entities
Imports System.IO
Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities
Imports Newtonsoft.Json
Imports Infrastructure.CrossCutting.Exceptions

Public Class ConsumeService

#Region "Builder"

    Private uri As String

    Public Sub New()
        Me.uri = "http://www.heon.com.co/ApiMedicationV2/MedicationRequest/"
    End Sub

#End Region

#Region "Create Urls"

    Private ReadOnly Property GetUrlListMedicalOrderRecipesByDate(LogisticOperator As Integer, OfficeType As Integer, HabilitationCode As String, InitialDate As String, EndDate As String)
        Get
            Return Me.uri & "Despacho/Listado/" & LogisticOperator.ToString() & "/" & OfficeType.ToString() & "/?codigoHabilitacion=" & HabilitationCode.Trim() & "&fechaInicial=" & InitialDate & "&fechaFinal=" & EndDate
        End Get
    End Property

    Private ReadOnly Property GetUrlMedicalOrderRecipe(LogisticOperator As Integer, OfficeType As Integer, PatientIdentification As String, TypeIdentification As Integer)
        Get
            Return Me.uri & "Despacho/DespachoPaciente/" & LogisticOperator.ToString() & "/" & OfficeType.ToString() & "/?tipoIdentificacion=" & TypeIdentification.ToString() & "&identificacion=" & PatientIdentification
        End Get
    End Property

    Private ReadOnly Property GetUrlListMedicalOrderRecipesReturnedByDate(LogisticOperator As Integer, OfficeType As Integer, HabilitationCode As Date, InitialDate As Date, EndDate As Date)
        Get
            Return Me.uri & "Devolucion/Listado/" & LogisticOperator.ToString() & "/" & OfficeType.ToString() & "/?codigoHabilitacion=" & HabilitationCode & "&fechaInicial=" & InitialDate.ToString("yyyy-MM-dd") & "&fechaFinal=" & EndDate.ToString("yyyy-MM-dd")
        End Get
    End Property

    Private ReadOnly Property GetUrlMedicalOrderRecipeReturned(LogisticOperator As Integer, OfficeType As Integer, recetarioOMedica As String)
        Get
            Return Me.uri & "Devolucion/RecetarioOrdenMedica/" + LogisticOperator.ToString() + "/" + OfficeType.ToString() + "/?recetarioOMedica=" + recetarioOMedica
        End Get
    End Property

    Private ReadOnly Property GetUrlRecetarioOrdenMedicaConfirmed()
        Get
            Return Me.uri & "Confirmacion"
        End Get
    End Property

#End Region

#Region "Request"

    ''' <summary>
    ''' Metodo que obtiene la dispensación de HEON
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDispensingByPatient(parameters As String) As ActionResult(Of WebServiceObject)
        Try
            Dim args As Object = Utils.DeserializeJsonToObject(parameters)

            Dim resultResponse = ConsumeServiceSingleton.instance.GetObjectByUrl(GetUrlMedicalOrderRecipe(args.LogisticOperator, args.OfficeType, args.PatientIdentification, args.TypeIdentification))
            If resultResponse.StateResult = False Then
                Return New ActionResult(Of WebServiceObject) With {.ObjectEmbbeded = Nothing, .StateResult = False, .StatusCode = resultResponse.StatusCode, .Message = resultResponse.Message}
            End If

            Dim result = JsonConvert.DeserializeObject(Of WebServiceObject)(resultResponse.ObjectEmbbeded)
            result.JsonSolicitud = resultResponse.ObjectEmbbeded

            Return New ActionResult(Of WebServiceObject) With {.ObjectEmbbeded = result, .StateResult = True, .StatusCode = eStatusResult.SUCCESS}
        Catch ex As WebException
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of WebServiceObject) With {.ObjectEmbbeded = Nothing, .StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = ex.Message}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of WebServiceObject) With {.ObjectEmbbeded = Nothing, .StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = ex.Message}
        End Try
    End Function

    ''' <summary>
    ''' Metodo que obtiene un listado de dispensación por rango de fechas
    ''' </summary>
    ''' <returns></returns>
    Public Function GetDispensingByDateRange(parameters As String) As ActionResult(Of String)
        Try
            Dim args As Object = Utils.DeserializeJsonToObject(parameters)

            Dim InitialDateString = args.InitialDate.Year.ToString() + "-" + args.InitialDate.Month.ToString().PadLeft(2, "0") + "-" + args.InitialDate.Day.ToString().PadLeft(2, "0") + "T" + args.InitialDate.Hour.ToString().PadLeft(2, "0") + ":" + args.InitialDate.Minute.ToString().PadLeft(2, "0") + ":" + args.InitialDate.Second.ToString().PadLeft(2, "0")
            Dim EndDateString = args.EndDate.Year.ToString() + "-" + args.EndDate.Month.ToString().PadLeft(2, "0") + "-" + args.EndDate.Day.ToString().PadLeft(2, "0") + "T" + args.EndDate.Hour.ToString().PadLeft(2, "0") + ":" + args.EndDate.Minute.ToString().PadLeft(2, "0") + ":" + args.EndDate.Second.ToString().PadLeft(2, "0")

            Dim resultResponse = ConsumeServiceSingleton.instance.GetObjectByUrl(GetUrlListMedicalOrderRecipesByDate(args.LogisticOperator, args.OfficeType, args.HabilitationCode, InitialDateString, EndDateString))
            If resultResponse.StateResult = False Then
                Return New ActionResult(Of String) With {.ObjectEmbbeded = Nothing, .StateResult = False, .StatusCode = resultResponse.StatusCode, .Message = resultResponse.Message}
            End If

            Return New ActionResult(Of String) With {.ObjectEmbbeded = resultResponse.ObjectEmbbeded, .StateResult = True, .StatusCode = eStatusResult.SUCCESS}
        Catch ex As WebException
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of String) With {.ObjectEmbbeded = Nothing, .StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = ex.Message}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of String) With {.ObjectEmbbeded = Nothing, .StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = ex.Message}
        End Try
    End Function

#End Region

#Region "Response"

    ''' <summary>
    ''' Metodo que envia la confirmación de la dispensación realizada
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function PostConfirmDispensing(json As String) As ActionResult(Of List(Of ConfirmationObject))
        Try
            Dim resultResponse = ConsumeServiceSingleton.instance.PostObjectByUrlAndJson(GetUrlRecetarioOrdenMedicaConfirmed(), json)
            If resultResponse.StateResult = False Then
                Return New ActionResult(Of List(Of ConfirmationObject)) With {.ObjectEmbbeded = Nothing, .StateResult = False, .StatusCode = resultResponse.StatusCode, .Message = resultResponse.Message}
            End If

            Dim status As Boolean = True
            Dim result = JsonConvert.DeserializeObject(Of List(Of ConfirmationObject))(resultResponse.ObjectEmbbeded)
            If result.Where(Function(i) i.idConfirmacion = 0).Count() > 0 Then
                status = False
            End If

            Return New ActionResult(Of List(Of ConfirmationObject)) With {.ObjectEmbbeded = result, .StateResult = status, .StatusCode = eStatusResult.SUCCESS, .Message = resultResponse.ObjectEmbbeded}
        Catch ex As WebException
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of ConfirmationObject)) With {.ObjectEmbbeded = Nothing, .StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = ex.Message}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of ConfirmationObject)) With {.ObjectEmbbeded = Nothing, .StateResult = False, .StatusCode = eStatusResult.EXCEPTION, .Message = ex.Message}
        End Try
    End Function

#End Region

End Class