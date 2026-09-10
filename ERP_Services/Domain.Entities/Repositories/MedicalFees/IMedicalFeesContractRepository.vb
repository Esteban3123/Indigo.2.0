'************************************************************
' Assembly         : Domain.MedicalFees
' Author           : Carlos Mario Arias Rubiano
' Created          : 11/12/2014
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Domain.Base
#End Region

Public Interface IMedicalFeesContractRepository
    Inherits IRepository(Of MedicalFeesContract)

    ''' <summary>
    ''' Obtiene un contrato para liquidacion por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetMedicalFeesContract(code As String) As MedicalFeesContract

    ''' <summary>
    ''' Obtiene un contrato para la liquidacion por id
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetMedicalFeesContractById(id As Integer) As MedicalFeesContract

    ''' <summary>
    ''' Consulta todos los contratos de profesionales de la salud
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetListMedicalFeesContract() As List(Of MedicalFeesContract)



End Interface
