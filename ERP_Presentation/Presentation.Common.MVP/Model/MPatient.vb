'***********************************************************************
' Assembly         : Presentacion.Common.MVP
' Author           : Jhossept Kevin Garay Rodriguez
' Created          : 17-01-2015
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

Public Class MPatient
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
    ''' funcion para listar todos los pacientes xpo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAllPatients()
        Return XpoServiceEx.Instance(Me._indigoSessionValues.HisContainer).CrystalService.ListAllPatients()
    End Function
    ''' <summary>
    ''' Funcion para listarlas ciudades
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAllCities()
        Return XpoServiceEx.Instance(Me._indigoSessionValues.TransactionalContainer).CommonService.ListAllCities(True)
    End Function

    ''' <summary>
    ''' Carga las entidades administradoras de salud
    ''' </summary>
    ''' <remarks></remarks>
    Public Function ListHealthAdministratorByType(ByVal type As Byte)
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.ListHealthAdministratorByType(type)
    End Function

    ''' <summary>
    ''' Carga las entidades administradoras de salud
    ''' </summary>
    ''' <remarks></remarks>
    Public Function ListHealthAdministratorByNotType(ByVal type As Byte)
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.ListHealthAdministratorByNotType(type)
    End Function

    ''' <summary>
    ''' Lists all patients.
    ''' </summary>
    ''' <returns></returns>
    Public Function ListAllEntityCups()
        Return XpoServiceEx.Instance(Me._indigoSessionValues.TransactionalContainer).ContractService.ListCupsEntity()
        'Return XpoServiceEx.Instance(Me._indigoSessionValues.HisContainer).CrystalService.ListCupsEntityByStatus(True, Me._indigoSessionValues.HisContainer)
    End Function

    ''' <summary>
    ''' lista las especialidades por estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListActivities() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Me._indigoSessionValues.HisContainer).CrystalService.ListActivities()
    End Function

    ''' <summary>
    ''' Funcion para obtener las empresas
    ''' </summary>
    ''' <returns>Objeto</returns>
    Function ListCompanyHIS() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Me._indigoSessionValues.HisContainer).CrystalService.ListCompany()
    End Function

    ''' <summary>
    ''' Funcion para obtener las ubicaciones
    ''' </summary>
    ''' <returns>Objeto</returns>
    Function ListLocationHIS() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Me._indigoSessionValues.HisContainer).CrystalService.ListLocations()
    End Function

    ''' <summary>
    ''' Funcion para obtener los grupos étnicos
    ''' </summary>
    ''' <returns>Objeto</returns>
    Function ListEthnicGroupHIS() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Me._indigoSessionValues.HisContainer).CrystalService.ListEthnicGroup()
    End Function

    ''' <summary>
    ''' Funcion para obtener los Niveles
    ''' </summary>
    ''' <returns>Objeto</returns>
    Function ListLevelsSHIS() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Me._indigoSessionValues.HisContainer).CrystalService.ListLevels()
    End Function

    ''' <summary>
    ''' Funcion para obtener los Niveles de educación
    ''' </summary>
    ''' <returns>Objeto</returns>
    Function ListEducationLevelsHIS() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Me._indigoSessionValues.HisContainer).CrystalService.ListEducationLevels()
    End Function

    ''' <summary>
    ''' Funcion para obtener los Lenguajes
    ''' </summary>
    ''' <returns>Objeto</returns>
    Function ListLanguageHIS() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Me._indigoSessionValues.HisContainer).CrystalService.ListLanguage()
    End Function

    ''' <summary>
    ''' Funcion para obtener las creencias
    ''' </summary>
    ''' <returns>Objeto</returns>
    Function ListBeliefHIS() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Me._indigoSessionValues.HisContainer).CrystalService.ListBelief()
    End Function

    ''' <summary>
    ''' Funcion para obtener las creencias
    ''' </summary>
    ''' <returns>Objeto</returns>
    Function ListDisabilityHIS() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Me._indigoSessionValues.HisContainer).CrystalService.ListDisability()
    End Function

    ''' <summary>
    ''' Funcion para obtener los grupos especiales
    ''' </summary>
    ''' <returns>Objeto</returns>
    Function ListSpecialGroupsHIS() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Me._indigoSessionValues.HisContainer).CrystalService.ListSpecialGroups()
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
    ''' Lista los centros de atención
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListCenter() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Me._indigoSessionValues.HisContainer).CrystalService.ListCenters()
    End Function

    ''' <summary>
    ''' Obtiene un paiente por identifiación
    ''' </summary>
    ''' <param name="Identification"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Async Function GetPacientByIdentification(Identification As String) As Task(Of ActionResult(Of INPACIENT))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCrystal.GetPatientByIdentificationAsync(Identification)
    End Function

    ''' <summary>
    ''' Metodo para guardar paciente
    ''' </summary>
    ''' <param name="patient"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Async Function SavePatient(patient As INPACIENT) As Task(Of Domain.Base.Entities.ActionResult(Of INPACIENT))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCrystal.SavePatientAsync(patient, Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Metodo para eliminar paciente
    ''' </summary>
    ''' <param name="Identification"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Async Function DeletePatient(Identification As String) As Task(Of Domain.Base.Entities.ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCrystal.DeletePatientAsync(Identification, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtener Nivel Por Código
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetLevelByCode(Code As String) As ADNIVELES
        Return IndigoConecta.Instancia.CurrentCloud.IndigoCrystal.GetLevelByCode(Code)
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
