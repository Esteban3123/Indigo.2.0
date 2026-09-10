'************************************************************
' Assembly         : Domain.MedicalFees
' Author           : Carlos Mario Arias Rubiano
' Created          : 28/01/2015
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Domain.Base
#End Region

Public Interface IHealthProfessionalContractRepository
    Inherits IRepository(Of HealthProfessionalContract)

    ''' <summary>
    ''' Obtiene el listado de detalles de contratos que tiene asociado el médico
    ''' </summary>
    ''' <param name="healthProfessionalCode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetListHealthProfessionalContractByHealthProfessionalCode(healthProfessionalCode As String, Optional tracking As Boolean = True) As List(Of HealthProfessionalContract)

    ''' <summary>
    ''' Obtiene un contrato asociado al medico por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetHealthProfessionalContractById(id As Integer) As HealthProfessionalContract

    ''' <summary>
    ''' Obtiene el listado de contratos que tiene asociado el médico y son de tipo estandar
    ''' </summary>
    ''' <param name="healthProfessionalCode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListHealthProfessionalContractWithTypeStandard(healthProfessionalCode As String) As List(Of HealthProfessionalContract)

End Interface
