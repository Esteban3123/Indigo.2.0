'***********************************************************************
' Assembly         : Presentacion.Billing.MVP
' Author           : Oscar stiven astudillo
' Created          : 2024-11-18
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports DevExpress.Xpo
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.CommonRepository
Imports Infrastructure.Data.Xpo.ContractRepository
Imports Infrastructure.Data.Xpo.CrystalRepository
Imports Presentation.Base

#End Region

Public Class PAdjustementNoteRIPS

#Region "Fields"
    ''' <summary>
    ''' variable que obtiene los valores de la sesion
    ''' </summary>
    Private _indigo As SessionValues
#End Region


#Region "Builder"

    ''' <summary>
    ''' Constructor que comunica con la interfaz
    ''' </summary>
    Public Sub New()
        _indigo = SessionValues.Instance
    End Sub

#End Region


#Region "Methods"

    ''' <summary>
    ''' Obtiene los tipos de  identificación activos
    ''' </summary>
    ''' <returns></returns>
    Public Function GetTypeIdentification()
        Return XpoServiceEx.Instance(_indigo.TransactionalContainer).CommonService.GetCollection(Of CrystalRepository.ADTIPOIDENTIFICAXpo)(Nothing, "ESTADO=1")
    End Function


    ''' <summary>
    ''' Genera listado con los tipos de paciente
    ''' </summary>
    ''' <returns></returns>
    Public Function GetTypePatient()
        Return New List(Of Object) From {
        New With {.Code = 1, .Name = "Contributivo"},
        New With {.Code = 2, .Name = "Subsidiado"},
        New With {.Code = 3, .Name = "No afiliado"},
        New With {.Code = 4, .Name = "Particular"},
        New With {.Code = 9, .Name = "Especial o excepción"},
        New With {.Code = 10, .Name = "Personas privadas de la libertad a cargo del Fondo Nacional de Salud"},
        New With {.Code = 11, .Name = "Tomador / amparado ARL"},
        New With {.Code = 12, .Name = "Tomador / amparado SOAT"},
        New With {.Code = 13, .Name = "Tomador / amparado planes voluntarios de salud"}
    }

    End Function

    ''' <summary>
    ''' Propiedad que almacena el listado completo de los sexos biológicos
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property BiologicalSexList() As List(Of Object)
        Get
            Return New List(Of Object) From {
                New With {.Code = "M", .Name = "Masculino"},
                New With {.Code = "F", .Name = "Femenino"},
                New With {.Code = "I", .Name = "Ambiguo"}
            }
        End Get
    End Property

    ''' <summary>
    ''' Genera listado con los tipos de sexo biologico 
    ''' </summary>
    ''' <returns></returns>
    Public Function GetBiologicalSex(Optional IsBorn As Boolean = False)
        If IsBorn Then
            Return BiologicalSexList.ToList()
        Else
            Return BiologicalSexList.Where(Function(x) x.Code <> "I").ToList()
        End If
    End Function

    ''' <summary>
    ''' Genera listado con los paises activos
    ''' </summary>
    ''' <returns></returns>
    Public Function GetCountry()
        Return XpoServiceEx.Instance(_indigo.TransactionalContainer).CommonService.GetCollection(Of CommonCountryXpo)(Nothing, "State=1")
    End Function

    ''' <summary>
    ''' Genera listado con los  municipios activos
    ''' </summary>
    ''' <returns></returns>
    Public Function GetCity()
        Return XpoServiceEx.Instance(_indigo.TransactionalContainer).CommonService.GetCollection(Of CommonCityXpo)(Nothing, "State=1")
    End Function

    ''' <summary>
    ''' Genera listado con los tipo de zona territorial
    ''' </summary>
    ''' <returns></returns>
    Public Function GetTerritorialZone()
        Return New List(Of Object) From {
        New With {.Code = 1, .Name = "Rural"},
        New With {.Code = 2, .Name = "Urbana"}
    }
    End Function

    ''' <summary>
    ''' Genera listado de acuerdo si maneja incapacidad
    ''' </summary>
    ''' <returns></returns>
    Public Function GetInability(Optional ByVal showBothOptions As Boolean = True) As List(Of Object)
        If showBothOptions Then
            Return New List(Of Object) From {
            New With {.Value = "SI"},
            New With {.Value = "NO"}
        }
        Else
            Return New List(Of Object) From {
            New With {.Value = "NO"}
        }
        End If
    End Function

    ''' <summary>
    ''' Valida si el numero de ingreso tiene incapacidad
    ''' </summary>
    ''' <returns></returns>
    Public Function GetInabilityByAdmissionNumber(ByVal admissionNumber As String)
        Dim filter As String = $"NUMINGRES = '{admissionNumber}'"
        Return XpoServiceEx.Instance(SessionValues.Instance.HisContainer).CrystalService.GetCollection(Of HCINCAPACXpo)(Nothing, filter).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Listado de la condición destino usuario egreso según la tabla de referencia sispro
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property GetConditionDestinationEgressUser()
        Get
            Return New List(Of Object) From {
            New With {.Code = 1, .Name = "Paciente con destino a su domicilio"},
            New With {.Code = 2, .Name = "Paciente muerto"},
            New With {.Code = 3, .Name = "Paciente derivado a otro servicio"},
            New With {.Code = 4, .Name = "Referido a otra institución"},
            New With {.Code = 5, .Name = "Contrareferido a otra institución"},
            New With {.Code = 6, .Name = "Derivado o referido a hospitalización domiciliara"},
            New With {.Code = 7, .Name = "Derivado a servicio social"},
            New With {.Code = 8, .Name = "Paciente continua en el servicio (Corte Facturación)"}
        }
        End Get
    End Property

    ''' <summary>
    ''' Genera listado con los datos del maestro vías ingreso servicios salud
    ''' </summary>
    ''' <returns></returns>
    Public Function GetEntryRoutesHealthServices()
        Return XpoServiceEx.Instance(SessionValues.Instance.HisContainer).CrystalService.GetCollection(Of EntryRoutesHealthServicesXpo)(Nothing, Nothing)
    End Function

    ''' <summary>
    ''' Genera listado con los datos del maestro causas de atención
    ''' </summary>
    ''' <returns></returns>
    Public Function GetCausesOfAttention()
        Return XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer).BillingService.ListXPInstantFeedbackSource(Of BillingRepository.CausesofattentionXpo)("Status=1")
    End Function

    ''' <summary>
    ''' Obtiene las modalidades de atención
    ''' </summary>
    ''' <returns></returns>
    Public Function GetAdmissionModalities()
        Return XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer).CrystalService.ListXPInstantFeedbackSource(Of AdmissionModalitiesXpo)("Status=1")
    End Function

    ''' <summary>
    ''' Obtiene los Grupos de Servicios RIPS activos
    ''' </summary>
    ''' <returns></returns>
    Public Function GetRIPSServiceGroup()
        Return XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer).ContractService.ListXPInstantFeedbackSource(Of RIPSServiceGroupsXpo)("Status=1")
    End Function

    ''' <summary>
    ''' Obtiene los Servicios RIPS activos
    ''' </summary>
    ''' <returns></returns>
    Public Function GetRIPSServices()
        Return XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer).ContractService.ListXPInstantFeedbackSource(Of RIPSServicesXpo)("Status=1")
    End Function

    ''' <summary>
    ''' Obtiene las Finalidades tecnonología salud activas
    ''' </summary>
    ''' <returns></returns>
    Public Function GetHealthPurposes()
        Return XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer).CrystalService.ListXPInstantFeedbackSource(Of HealthPurposesXpo)("Status=1")
    End Function
#End Region

End Class
