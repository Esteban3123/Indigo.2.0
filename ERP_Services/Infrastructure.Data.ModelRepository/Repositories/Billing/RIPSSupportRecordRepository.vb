'***********************************************************************
' Assembly         : Infrastructure.Data.ModelRepository
' Author           : Generated
' Created          : 2024
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.Data.Entity
Imports System.Linq
Imports System.Threading.Tasks
Imports Domain.Crystal
Imports Domain.Entities
Imports Domain.Payroll
Imports Infrastructure.Data.Base
Imports Infrastructure.Data.CrystalRepository


#End Region

Public Class RIPSSupportRecordRepository
    Inherits GenericRepository(Of RIPSSupportRecord)
    Implements IRIPSSupportRecordRepository

    'Contexto del repositorio
    Private _context As IGlobalModelUnitOfWork

    Private _functionalUnitRepository As IFunctionalUnitRepository
    Private _healthProfessionalRepository As IHealthProfessionalRepository
    Private _thirdpartyRepository As IThirdPartyRepository
    Private _admissionRepository As IAdmissionRepository
    Private _cupsRepository As ICupsEntityRepository

    ''' <summary>
    ''' Constructor del repositorio
    ''' </summary>
    ''' <param name="context">Contexto del repositorio</param>
    Public Sub New(context As IGlobalModelUnitOfWork,
                   functionalUnitRepository As IFunctionalUnitRepository,
                   healthProfessionalRepository As IHealthProfessionalRepository,
                   thirdpartyRepository As IThirdPartyRepository,
                   admissionRepository As IAdmissionRepository,
                   cupsRepository As ICupsEntityRepository
                   )

        MyBase.New(context)
        _context = context
        _functionalUnitRepository = functionalUnitRepository
        _healthProfessionalRepository = healthProfessionalRepository
        _thirdpartyRepository = thirdpartyRepository
        _admissionRepository = admissionRepository
        _cupsRepository = cupsRepository

    End Sub

    ''' <summary>
    ''' Obtiene un registro de soporte RIPS por su identificador
    ''' </summary>
    ''' <param name="id">Identificador del registro de soporte RIPS</param>
    ''' <returns>Registro de soporte RIPS</returns>
    Public Async Function GetRIPSSupportRecordByIdAsync(id As Integer) As Task(Of RIPSSupportRecord) Implements IRIPSSupportRecordRepository.GetRIPSSupportRecordByIdAsync
        Return Await (From r In _context.RIPSSupportRecord.Include("RIPSSupportRecordDetail")
                      Where r.Id = id
                      Select r).FirstOrDefaultAsync()
    End Function

    ''' <summary>
    ''' Obtiene un registro de soporte RIPS por su código y con propiedades extendidas
    ''' </summary>
    ''' <param name="code">Código del registro de soporte RIPS</param>
    ''' <returns>Registro de soporte RIPS</returns>
    Public Async Function GetRIPSSupportRecordByCodeAsync(code As String) As Task(Of RIPSSupportRecord) Implements IRIPSSupportRecordRepository.GetRIPSSupportRecordByCodeAsync
        Try
            Dim ripsRecord = Await (From r In _context.RIPSSupportRecord.Include("RIPSSupportRecordDetail")
                                    Where r.Code = code
                                    Select r).FirstOrDefaultAsync()
            Dim patientCode = _admissionRepository.FirstOrDefault(Function(ad) ad.NUMINGRES = ripsRecord.AdmissionNumber).IPCODPACI

            For Each detail In ripsRecord.RIPSSupportRecordDetail

                Dim thirdparty = _thirdpartyRepository.GetThirdPartyById(detail.PerformsHealthProfessionalThirdPartyId)
                Dim professional = _healthProfessionalRepository.GetHealthProfessionalByCode(thirdparty.Nit)
                Dim cupsCode = _cupsRepository.FirstOrDefault(Function(c) c.Id = detail.StayCUPSEntityId)?.Code
                Dim cupsEntity = _cupsRepository.GetCupsEntityWithContractDescriptions(cupsCode)

                detail.AdmissionNumber = ripsRecord.AdmissionNumber
                detail.PatientCode = patientCode
                detail.Quantity = detail.CalculatedDaysStay
                detail.SpecialtyCode = detail.PerformsProfessionalSpecialty
                detail.FunctionalUnitCode = _functionalUnitRepository.GetFunctionalUnitById(detail.FunctionalUnitId)?.Code
                detail.CUPSCode = cupsCode
                detail.ProfessionalCode = professional.CODPROSAL
                detail.NitMedico = professional.CODIGONIT

                If detail.StayCUPSEntityId IsNot Nothing AndAlso detail.StayCUPSEntityContractDescriptionId IsNot Nothing Then
                    detail.CUPSDescriptionId = cupsEntity.CUPSEntityContractDescriptions.FirstOrDefault(Function(c) c.Id = detail.StayCUPSEntityContractDescriptionId)?.Id
                    detail.ContractDescriptionId = cupsEntity.CUPSEntityContractDescriptions.FirstOrDefault(Function(c) c.Id = detail.StayCUPSEntityContractDescriptionId)?.ContractDescriptions.Id
                End If
            Next

            Return ripsRecord
        Catch ex As Exception

        End Try

    End Function

    ''' <summary>
    ''' Lista todos los registros de soporte RIPS
    ''' </summary>
    ''' <returns>Lista de registros de soporte RIPS</returns>
    Public Async Function ListAllRIPSSupportRecordsAsync() As Task(Of List(Of RIPSSupportRecord)) Implements IRIPSSupportRecordRepository.ListAllRIPSSupportRecordsAsync
        Return Await (From r In _context.RIPSSupportRecord.Include("RIPSSupportRecordDetail")
                      Select r).ToListAsync()
    End Function

    ''' <summary>
    ''' Obtiene Status y Code sin tracking (para validaciones)
    ''' </summary>
    Public Async Function GetRIPSSupportRecordStatusByIdAsync(id As Integer) As Task(Of Tuple(Of Byte, String)) Implements IRIPSSupportRecordRepository.GetRIPSSupportRecordStatusByIdAsync
        Dim result = Await (From r In _context.RIPSSupportRecord.AsNoTracking()
                            Where r.Id = id
                            Select New With {r.Status, r.Code}).FirstOrDefaultAsync()

        If result Is Nothing Then
            Return Nothing
        End If

        Return New Tuple(Of Byte, String)(result.Status, result.Code)
    End Function

End Class
