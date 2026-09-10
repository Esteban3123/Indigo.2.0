'***********************************************************************
' Assembly         : Presentacion.Common.MVP
' Author           : Jhossept Kevin Garay Rodriguez
' Created          : 21-02-2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities
Imports Presentation.CloudAgent
Imports Domain.Base.Entities
Imports Presentation.Base
Imports System.ServiceModel
Imports Presentation.Security.MVP
Imports Infrastructure.Data.Xpo
Imports DevExpress.Xpo
Imports Domain.Crystal.Entities
#End Region

Public Class MAdmissions
    Inherits ModelBase
    Implements IDisposable

#Region "Fields"

    ''' <summary>
    ''' Referencia a los valores de session
    ''' </summary>
    Private _indigoSessionValues As SessionValues

    ''' <summary>
    ''' Id del frontal
    ''' </summary>
    Private _tagForm As String

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    Public Sub New(ByVal tag As String)
        MyBase.New(tag)
        Me._tagForm = tag
        Me._indigoSessionValues = SessionValues.Instance
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene el tercero por nit del paciente
    ''' </summary>
    ''' <param name="Identification"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Async Function GetThirdpartyPatient(Identification As String) As Task(Of Domain.Entities.ThirdParty)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetThirdPartyByNitAsync(Identification, Indigo)
    End Function

    ''' <summary>
    ''' Cargar usuario de crystal
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Async Function GetUsuarioCrystal(ByVal code As String) As Task(Of SEGusuaru)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCrystal.GetusuarioCrystalAsync(code)
    End Function

    ''' <summary>
    ''' Lista las unidades funcionales por permiso de usuario
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListViewListFuncionalUnitAuthorization(ByVal CodeGroup As String, ByVal CodeUsers As String) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.HisContainer).CrystalService.ListViewListFuncionalUnitAuthorization(CodeGroup, CodeUsers)
    End Function

    ''' <summary>
    ''' Lista las entidades administradoras 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListHealthAdministratorByType(type As Byte) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.ListHealthAdministratorByType(type)
    End Function


    ''' <summary>
    ''' Lista las entidades administradoras 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListHealthAdministratorByNotType(type As Byte) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.ListHealthAdministratorByNotType(type)
    End Function

    ''' <summary>
    ''' Lista las camas
    ''' </summary>
    ''' <param name="center"></param>
    ''' <param name="uf"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListBeds(center As String, uf As String) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Me._indigoSessionValues.HisContainer).CrystalService.ListBeds(center, uf)
    End Function

    ''' <summary>
    ''' Lista las unidades FUncionales
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAllFunctionalUnit()
        Return XpoServiceEx.Instance(Me._indigoSessionValues.HisContainer).CrystalService.ListUF()
    End Function

    ''' <summary>
    ''' Lista los municipios
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAllTown()
        Return XpoServiceEx.Instance(Me._indigoSessionValues.HisContainer).CrystalService.ListTown()
    End Function

    ''' <summary>
    ''' Lista las IPS
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAllIPS()
        Return XpoServiceEx.Instance(Me._indigoSessionValues.HisContainer).CrystalService.ListIPS()
    End Function

    ''' <summary>
    ''' Lista los salarios minimos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAllMinWage()
        Return XpoServiceEx.Instance(Me._indigoSessionValues.HisContainer).CrystalService.ListMinWage()
    End Function

    ''' <summary>
    ''' funcion para listar todos los pacientes xpo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAllPatients()
        Return XpoServiceEx.Instance(Me._indigoSessionValues.HisContainer).CrystalService.ListAllPatients()
    End Function

    ''' <summary>
    ''' Lista los centros de atención
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListCenter() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Me._indigoSessionValues.HisContainer).CrystalService.ListCenters()
    End Function

    ''' <summary>
    ''' Lista los grupos de atención
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListCareGroup() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Me._indigoSessionValues.TransactionalContainer).ContractService.ListCareGroupByStatus(True)
    End Function

    ''' <summary>
    ''' Obtener Ingreso Por Código
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Async Function GetAdmissionsByCode(Code As String) As Task(Of ActionResult(Of ADINGRESO))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCrystal.GetAdmissionByCodeAsync(Code)
    End Function

    ''' <summary>
    ''' Obtener Ingreso Por Código
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAdmissionsByCodeSimple(Code As String) As ActionResult(Of ADINGRESO)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoCrystal.GetAdmissionByCode(Code)
    End Function

    ''' <summary>
    ''' Obtener Ingreso Por Código
    ''' </summary>
    ''' <param name="Identification"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Async Function GetAdmissionsByPatient(Identification As String) As Task(Of ActionResult(Of List(Of ADINGRESO)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCrystal.GetAdmissionsByPatientAsync(Identification)
    End Function

    ''' <summary>
    ''' Elimina un ingreso
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Async Function DeleteAdmission(Code As String) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCrystal.DeleteAdmissionAsync(Code)
    End Function

    ''' <summary>
    ''' Guarda el ingreso
    ''' </summary>
    ''' <param name="admission"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Async Function SaveAdmission(admission As ADINGRESO) As Task(Of ActionResult(Of ADINGRESO))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCrystal.SaveAdmissionAsync(admission, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Actualiza el estado un ingreso
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Async Function UpdateStatusAdmission(Code As String, Status As String, Justificacion As String) As Task(Of ActionResult(Of ADINGRESO))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCrystal.UpdateStatusAdmissionAsync(Code, Status, Justificacion, Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Actualiza el estado un ingreso
    ''' </summary>
    ''' <param name="codcenate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Async Function GetCenterParameters(codcenate As String) As Task(Of ActionResult(Of ADPARAMET))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCrystal.GetCentersParametersAsync(codcenate)
    End Function

    Async Function GetDateTriage(paciente As String) As Task(Of DateTime)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCrystal.GetTriageDateAsync(paciente)
    End Function

    ''' <summary>
    ''' valida el ingreso que no este en estado fadturado,anulado, cerrado, si tiene egreso de cama y si tiene alta medica
    ''' </summary>
    ''' <param name="AdmissionCode"></param>
    ''' <returns></returns>
    Async Function AdmisionValidations(AdmissionCode As String) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCrystal.AdmisionValidationsAsync(AdmissionCode)
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: desechar estado administrado (objetos administrados).
            End If

            ' TODO: liberar recursos no administrados (objetos no administrados) e invalidar Finalize() below.
            ' TODO: Establecer campos grandes como Null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: invalidar Finalize() sólo si la instrucción Dispose(ByVal disposing As Boolean) anterior tiene código para liberar recursos no administrados.
    'Protected Overrides Sub Finalize()
    '    ' No cambie este código. Ponga el código de limpieza en la instrucción Dispose(ByVal disposing As Boolean) anterior.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' Visual Basic agregó este código para implementar correctamente el patrón descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' No cambie este código. Coloque el código de limpieza en Dispose(disposing As Boolean).
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region
End Class
