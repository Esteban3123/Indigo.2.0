#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.CommonRepository
Imports Presentation.Base

#End Region

Public Class PElectronicPayrollTraceability

#Region "Fields"

    ''' <summary>
    ''' variable para comunicar con la interfaz
    ''' </summary>
    Dim View As IElectronicPayrollTraceability

    ''' <summary>
    ''' variable que obtiene los valores de la sesion
    ''' </summary>
    Dim Indigo As SessionValues

#End Region

#Region "Builder"

    ''' <summary>
    ''' Constructor que comunica con la interfaz
    ''' </summary>
    Public Sub New(ByRef iView As IElectronicPayrollTraceability)
        'If iView Is Nothing Then
        '    Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        'End If
        View = iView
        Indigo = SessionValues.Instance
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene las soportes de pago de nomina electronica
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub GetElectronicPayrollPaymentSupports()
        View.ElectronicPayrollPaymentSupportXpo = XpoServiceEx.Instance(Me.Indigo.TransactionalContainer).PayrollService.ListElectronicPayrollPaymentSupports()
    End Sub

    ''' <summary>
    ''' Obtiene las notas debito
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub GetAdjustmentNotes()
        View.AdjustmentNoteXpo = XpoServiceEx.Instance(Me.Indigo.TransactionalContainer).PayrollService.ListElectronicPayrollsAdjustmentNotes()
    End Sub

    ''' <summary>
    ''' Obtiene los detalles
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetDetails(ElectronicPayrollId As Integer) As DevExpress.Xpo.XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Me.Indigo.TransactionalContainer).PayrollService.ListElectronicPayrollDetails(ElectronicPayrollId)
    End Function

    ''' <summary>
    ''' Obtiene las notificaciones
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetNotifications(ElectronicPayrollId As Integer) As DevExpress.Xpo.XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Me.Indigo.TransactionalContainer).PayrollService.ListElectronicPayrollNotifications(ElectronicPayrollId)
    End Function

    ''' <summary>
    ''' Lista los solicitudes con valoración
    ''' </summary>
    ''' <remarks></remarks>
    Public Function ListEmailsByThirdPartyId(thirdPartyId) As List(Of Domain.Entities.Email)
        Dim listEmails As New List(Of Domain.Entities.Email)
        Dim filter As String = "Id = " & thirdPartyId

        Dim thirdPartyXpo = XpoServiceEx.Instance(Me.Indigo.TransactionalContainer).AccountingService.GetCollection(Of Infrastructure.Data.Xpo.AccountingRepository.CommonThirdPartyXpo)(Nothing, filter).FirstOrDefault()
        If thirdPartyXpo IsNot Nothing Then
            Dim listEmailXpo = thirdPartyXpo.PersonId.CommonEmailCollection
            For Each email In listEmailXpo
                listEmails.Add(New Domain.Entities.Email With {.Email1 = email.Email})
            Next
        End If

        Return listEmails
    End Function

    ''' <summary>
    ''' Obtiene informacion de los soportes de pago de nomina electronica
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetElectronicPayrollInformation(ElectronicPayrollIds As String) As List(Of PayrollRepository.ElectronicPayrollInformation)
        Dim filter As String = "ElectronicPayrollId in (" & ElectronicPayrollIds & ")"
        Return XpoServiceEx.Instance(Me.Indigo.TransactionalContainer).PayrollService.GetCollection(Of PayrollRepository.ElectronicPayrollInformation)(Nothing, filter).ToList()
    End Function

    ''' <summary>
    ''' Obtiene la ciudad por código
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    Public Function GetCityByCode(code As String) As CommonCityXpo
        Return XpoServiceEx.Instance(Me.Indigo.TransactionalContainer).CommonService.GetCityByCode(code)
    End Function

    ''' <summary>
    ''' Obtiene departamento por código
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    Public Function GetDepartmentByCode(code As String) As CommonDepartmentXpo
        Return XpoServiceEx.Instance(Me.Indigo.TransactionalContainer).CommonService.GetDepartmentByCode(code)
    End Function

    ''' <summary>
    ''' Obtiene país por código estándar
    ''' </summary>
    ''' <param name="standardCode"></param>
    ''' <returns></returns>
    Public Function GetCountryByStandardCode(standardCode As String) As CommonCountryXpo
        Return XpoServiceEx.Instance(Me.Indigo.TransactionalContainer).CommonService.GetCountryByStandardCode(standardCode)
    End Function

#End Region

End Class
