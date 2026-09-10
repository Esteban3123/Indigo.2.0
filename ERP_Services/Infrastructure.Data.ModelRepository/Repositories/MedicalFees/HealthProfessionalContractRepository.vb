'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 28/01/2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class HealthProfessionalContractRepository
    Inherits GenericRepository(Of HealthProfessionalContract)
    Implements IHealthProfessionalContractRepository

#Region "Fields"

    ''' <summary>
    ''' Contexto
    ''' </summary>
    ''' <remarks></remarks>
    Private _context As IGlobalModelUnitOfWork

#End Region

#Region "Builder"

    ''' <summary>
    ''' Inicia el contexto de payments
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene un detalle de contrato que tiene asociado el medico por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetHealthProfessionalContractById(id As Integer) As HealthProfessionalContract Implements IHealthProfessionalContractRepository.GetHealthProfessionalContractById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim res = (From h In _context.HealthProfessionalContract Where h.Id = id Select h).FirstOrDefault
        Return res
    End Function

    ''' <summary>
    ''' Obtiene un listado de detalles de contrato que tiene asociado el medico
    ''' </summary>
    ''' <param name="healthProfessionalCode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetListHealthProfessionalContractByHealthProfessionalCode(healthProfessionalCode As String, Optional tracking As Boolean = True) As List(Of HealthProfessionalContract) Implements IHealthProfessionalContractRepository.GetListHealthProfessionalContractByHealthProfessionalCode
        If healthProfessionalCode Is String.Empty Then
            Throw New ArgumentNullException("healthProfessionalCode")
        End If
        Dim listRes
        If tracking Then
            listRes = (From h In _context.HealthProfessionalContract Where h.HealthProfessionalCode = healthProfessionalCode Select h).ToList
        Else
            listRes = (From h In _context.HealthProfessionalContract.AsNoTracking Where h.HealthProfessionalCode = healthProfessionalCode Select h).ToList
        End If
        If listRes.Count > 0 Then
            If tracking Then
                For Each item As HealthProfessionalContract In listRes
                    Dim medicalFeesContract = (From mfc In _context.MedicalFeesContract.AsNoTracking Where mfc.Id = item.MedicalFeesContractId Select mfc).FirstOrDefault
                    item.MedicalFeesContractDescription = medicalFeesContract.Code + " - " + medicalFeesContract.ContractName
                Next
            End If
            Return listRes
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Obtiene el listado de contratos que tiene asociado el medico y que son de tipo estandar
    ''' </summary>
    ''' <param name="healthProfessionalCode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListHealthProfessionalContractWithTypeStandard(healthProfessionalCode As String) As List(Of HealthProfessionalContract) Implements IHealthProfessionalContractRepository.ListHealthProfessionalContractWithTypeStandard
        If healthProfessionalCode Is String.Empty Then
            Throw New ArgumentNullException("healthProfessionalCode")
        End If
        Dim ListHealthProfessionalContract = (From l In _context.HealthProfessionalContract.AsNoTracking.Include("MedicalFeesContract").AsNoTracking
                                              Where l.HealthProfessionalCode = healthProfessionalCode
                                              Select l).ToList
        If ListHealthProfessionalContract Is Nothing OrElse ListHealthProfessionalContract.Count = 0 Then
            Throw New ArgumentNullException("El médico seleccionado no tiene asociado contratos.")
        End If
        Dim ListReturn = (From l In ListHealthProfessionalContract Where l.MedicalFeesContract.ContractType = 1 Select l).ToList
        If ListReturn Is Nothing OrElse ListReturn.Count = 0 Then
            Throw New ArgumentNullException("El médico seleccionado no tiene asociado contratos de tipo estandar.")
        End If
        Return ListReturn
    End Function

#End Region

End Class
