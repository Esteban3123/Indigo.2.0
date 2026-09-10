'***********************************************************************
' Assembly         : Application.Billing
' Author           : Carlos Mario Arias Rubiano
' Created          : 28/02/2020
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region

Public Interface IAdmissionsSequenseAdminService
    Inherits IDisposable

#Region "Methods"

    ''' <summary>
    ''' Obtiene la secuencia numerica para un formulario
    ''' </summary>
    ''' <param name="idForm">Id del formulario a consultar</param>
    ''' <returns>Secuencia numerica</returns>
    Function GetSequenseByIdForm(idForm As String) As AdmissionsSequence

    ''' <summary>
    ''' Obtiene un grupo de secuencias numericas por su id de configuración
    ''' </summary>
    ''' <param name="id">Id de la configuración de la secuencia</param>
    ''' <returns>Grupo de secuencias numericas</returns>
    Function GetNumericSequenseGroupById(ByVal id As Int32) As List(Of String)

    ''' <summary>
    ''' Guarda la secuencia
    ''' </summary>
    ''' <param name="seq"></param>
    ''' <returns></returns>
    Function SaveSequence(ByVal seq As AdmissionsSequence) As ActionResult

#End Region

End Interface
